using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace MagicaCloth2
{
	// Token: 0x020000B5 RID: 181
	public class SimpleDistanceReduction : StepReductionBase
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x0001BCAC File Offset: 0x00019EAC
		public SimpleDistanceReduction(string name, VirtualMesh mesh, ReductionWorkData workingData, float startMergeLength, float endMergeLength, int maxStep, bool dontMakeLine, float joinPositionAdjustment) : base("SimpleDistanceReduction [" + name + "]", mesh, workingData, startMergeLength, endMergeLength, maxStep, dontMakeLine, joinPositionAdjustment)
		{
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0001BCDB File Offset: 0x00019EDB
		public override void Dispose()
		{
			base.Dispose();
			this.gridMap.Dispose();
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0001BCEE File Offset: 0x00019EEE
		protected override void StepInitialize()
		{
			base.StepInitialize();
			this.gridMap = new GridMap<int>(this.vmesh.VertexCount);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0001BD0C File Offset: 0x00019F0C
		protected override void CustomReductionStep()
		{
			float gridSize = this.nowMergeLength * 2f;
			this.gridMap.GetMultiHashMap().Clear();
			new SimpleDistanceReduction.InitGridJob
			{
				vcnt = this.vmesh.VertexCount,
				gridSize = gridSize,
				localPositions = this.vmesh.localPositions.GetNativeArray(),
				joinIndices = this.workData.vertexJoinIndices,
				gridMap = this.gridMap.GetMultiHashMap()
			}.Run<SimpleDistanceReduction.InitGridJob>();
			new SimpleDistanceReduction.SearchJoinEdgeJob
			{
				vcnt = this.vmesh.VertexCount,
				gridSize = gridSize,
				radius = this.nowMergeLength,
				localPositions = this.vmesh.localPositions.GetNativeArray(),
				joinIndices = this.workData.vertexJoinIndices,
				vertexToVertexMap = this.workData.vertexToVertexMap,
				gridMap = this.gridMap.GetMultiHashMap(),
				joinEdgeList = this.joinEdgeList
			}.Run<SimpleDistanceReduction.SearchJoinEdgeJob>();
		}

		// Token: 0x040005A3 RID: 1443
		private GridMap<int> gridMap;

		// Token: 0x020000B6 RID: 182
		[BurstCompile]
		private struct InitGridJob : IJob
		{
			// Token: 0x060002AA RID: 682 RVA: 0x0001BE2C File Offset: 0x0001A02C
			public void Execute()
			{
				for (int i = 0; i < this.vcnt; i++)
				{
					if (this.joinIndices[i] < 0)
					{
						GridMap<int>.AddGrid(this.localPositions[i], i, this.gridMap, this.gridSize);
					}
				}
			}

			// Token: 0x040005A4 RID: 1444
			public int vcnt;

			// Token: 0x040005A5 RID: 1445
			public float gridSize;

			// Token: 0x040005A6 RID: 1446
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x040005A7 RID: 1447
			[ReadOnly]
			public NativeArray<int> joinIndices;

			// Token: 0x040005A8 RID: 1448
			public NativeParallelMultiHashMap<int3, int> gridMap;
		}

		// Token: 0x020000B7 RID: 183
		[BurstCompile]
		private struct SearchJoinEdgeJob : IJob
		{
			// Token: 0x060002AB RID: 683 RVA: 0x0001BE78 File Offset: 0x0001A078
			public void Execute()
			{
				for (int i = 0; i < this.vcnt; i++)
				{
					if (this.joinIndices[i] < 0)
					{
						float3 @float = this.localPositions[i];
						float num = (float)math.max(this.vertexToVertexMap.CountValuesForKey((ushort)i) - 1, 1);
						foreach (int3 key in GridMap<int>.GetArea(@float, this.radius, this.gridMap, this.gridSize))
						{
							if (this.gridMap.ContainsKey(key))
							{
								foreach (int num2 in this.gridMap.GetValuesForKey(key))
								{
									if (num2 != i)
									{
										float3 y = this.localPositions[num2];
										float num3 = math.distance(@float, y);
										if (num3 <= this.radius)
										{
											float num4 = (float)math.max(this.vertexToVertexMap.CountValuesForKey((ushort)num2) - 1, 1);
											float cost = num3 * (1f + (num + num4) / 2f);
											StepReductionBase.JoinEdge joinEdge = new StepReductionBase.JoinEdge
											{
												vertexPair = new int2(i, num2),
												cost = cost
											};
											this.joinEdgeList.Add(joinEdge);
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x040005A9 RID: 1449
			public int vcnt;

			// Token: 0x040005AA RID: 1450
			public float gridSize;

			// Token: 0x040005AB RID: 1451
			public float radius;

			// Token: 0x040005AC RID: 1452
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x040005AD RID: 1453
			[ReadOnly]
			public NativeArray<int> joinIndices;

			// Token: 0x040005AE RID: 1454
			[ReadOnly]
			public NativeParallelMultiHashMap<ushort, ushort> vertexToVertexMap;

			// Token: 0x040005AF RID: 1455
			[ReadOnly]
			public NativeParallelMultiHashMap<int3, int> gridMap;

			// Token: 0x040005B0 RID: 1456
			[WriteOnly]
			public NativeList<StepReductionBase.JoinEdge> joinEdgeList;
		}
	}
}
