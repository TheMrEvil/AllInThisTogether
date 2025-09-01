using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace MagicaCloth2
{
	// Token: 0x020000B3 RID: 179
	public class ShapeDistanceReduction : StepReductionBase
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x0001BA74 File Offset: 0x00019C74
		public ShapeDistanceReduction(string name, VirtualMesh mesh, ReductionWorkData workingData, float startMergeLength, float endMergeLength, int maxStep, bool dontMakeLine, float joinPositionAdjustment) : base("ShapeReduction [" + name + "]", mesh, workingData, startMergeLength, endMergeLength, maxStep, dontMakeLine, joinPositionAdjustment)
		{
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0001BAA3 File Offset: 0x00019CA3
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0001BAAB File Offset: 0x00019CAB
		protected override void StepInitialize()
		{
			base.StepInitialize();
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0001BAB4 File Offset: 0x00019CB4
		protected override void CustomReductionStep()
		{
			new ShapeDistanceReduction.SearchJoinEdgeJob
			{
				vcnt = this.vmesh.VertexCount,
				radius = this.nowMergeLength,
				dontMakeLine = this.dontMakeLine,
				localPositions = this.vmesh.localPositions.GetNativeArray(),
				joinIndices = this.workData.vertexJoinIndices,
				vertexToVertexMap = this.workData.vertexToVertexMap,
				joinEdgeList = this.joinEdgeList
			}.Run<ShapeDistanceReduction.SearchJoinEdgeJob>();
		}

		// Token: 0x020000B4 RID: 180
		[BurstCompile]
		private struct SearchJoinEdgeJob : IJob
		{
			// Token: 0x060002A5 RID: 677 RVA: 0x0001BB44 File Offset: 0x00019D44
			public void Execute()
			{
				for (int i = 0; i < this.vcnt; i++)
				{
					if (this.joinIndices[i] < 0)
					{
						int num = this.vertexToVertexMap.CountValuesForKey((ushort)i);
						if (num != 0)
						{
							float3 x = this.localPositions[i];
							float num2 = (float)math.max(num - 1, 1);
							float num3 = float.MaxValue;
							int num4 = -1;
							foreach (ushort num5 in this.vertexToVertexMap.GetValuesForKey((ushort)i))
							{
								float3 y = this.localPositions[(int)num5];
								float num6 = math.distance(x, y);
								if (num6 <= this.radius)
								{
									float num7 = (float)math.max(this.vertexToVertexMap.CountValuesForKey(num5) - 1, 1);
									if (StepReductionBase.CheckJoin2(this.vertexToVertexMap, i, (int)num5, this.dontMakeLine))
									{
										float num8 = num6 * (1f + (num2 + num7) / 2f);
										if (num8 < num3)
										{
											num3 = num8;
											num4 = (int)num5;
										}
									}
								}
							}
							if (num4 >= 0)
							{
								StepReductionBase.JoinEdge joinEdge = new StepReductionBase.JoinEdge
								{
									vertexPair = new int2(i, num4),
									cost = num3
								};
								this.joinEdgeList.Add(joinEdge);
							}
						}
					}
				}
			}

			// Token: 0x0400059C RID: 1436
			public int vcnt;

			// Token: 0x0400059D RID: 1437
			public float radius;

			// Token: 0x0400059E RID: 1438
			public bool dontMakeLine;

			// Token: 0x0400059F RID: 1439
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x040005A0 RID: 1440
			[ReadOnly]
			public NativeArray<int> joinIndices;

			// Token: 0x040005A1 RID: 1441
			[ReadOnly]
			public NativeParallelMultiHashMap<ushort, ushort> vertexToVertexMap;

			// Token: 0x040005A2 RID: 1442
			public NativeList<StepReductionBase.JoinEdge> joinEdgeList;
		}
	}
}
