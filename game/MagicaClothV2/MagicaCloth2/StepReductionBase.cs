using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x020000B8 RID: 184
	public abstract class StepReductionBase : IDisposable
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0001C018 File Offset: 0x0001A218
		public StepReductionBase()
		{
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0001C02C File Offset: 0x0001A22C
		public StepReductionBase(string name, VirtualMesh mesh, ReductionWorkData workingData, float startMergeLength, float endMergeLength, int maxStep, bool dontMakeLine, float joinPositionAdjustment)
		{
			this.name = name;
			this.vmesh = mesh;
			this.workData = workingData;
			this.result = ResultCode.None;
			this.startMergeLength = math.max(startMergeLength, 1E-09f);
			this.endMergeLength = math.max(endMergeLength, 1E-09f);
			this.maxStep = math.min(maxStep, 100);
			this.dontMakeLine = dontMakeLine;
			this.joinPositionAdjustment = joinPositionAdjustment;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0001C0B0 File Offset: 0x0001A2B0
		public virtual void Dispose()
		{
			if (this.joinEdgeList.IsCreated)
			{
				this.joinEdgeList.Dispose();
			}
			if (this.completeVertexSet.IsCreated)
			{
				this.completeVertexSet.Dispose();
			}
			if (this.removePairList.IsCreated)
			{
				this.removePairList.Dispose();
			}
			if (this.resultArray.IsCreated)
			{
				this.resultArray.Dispose();
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0001C11D File Offset: 0x0001A31D
		public ResultCode Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0001C128 File Offset: 0x0001A328
		public ResultCode Reduction()
		{
			this.result.Clear();
			try
			{
				this.StepInitialize();
				this.InitStep();
				while (this.nowStepIndex < this.maxStep)
				{
					this.ReductionStep();
					this.nowStepIndex++;
					if (this.IsEndStep())
					{
						break;
					}
					this.NextStep();
				}
				if (this.nowStepIndex < this.maxStep)
				{
					this.ReductionStep();
					this.nowStepIndex++;
				}
				this.UpdateReductionResultJob();
				int num = 0;
				for (int i = 0; i < this.nowStepIndex; i++)
				{
					num += this.resultArray[i];
				}
				this.workData.removeVertexCount += num;
				this.result.SetSuccess();
			}
			catch (Exception message)
			{
				Debug.LogError(message);
				if (!this.result.IsError())
				{
					if (this is SimpleDistanceReduction)
					{
						this.result.SetError(Define.Result.Reduction_SimpleDistanceException);
					}
					else if (this is ShapeDistanceReduction)
					{
						this.result.SetError(Define.Result.Reduction_ShapeDistanceException);
					}
					else
					{
						this.result.SetError(Define.Result.Reduction_Exception);
					}
				}
			}
			return this.result;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0001C258 File Offset: 0x0001A458
		private void InitStep()
		{
			this.nowStepIndex = 0;
			this.nowMergeLength = this.startMergeLength;
			this.nowStepScale = 2f;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0001C278 File Offset: 0x0001A478
		private bool IsEndStep()
		{
			return this.nowMergeLength == this.endMergeLength;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0001C288 File Offset: 0x0001A488
		private void NextStep()
		{
			this.nowStepScale = math.max(this.nowStepScale * 0.93f, 1.1f);
			this.nowMergeLength = math.min(this.nowMergeLength * this.nowStepScale, this.endMergeLength);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0001C2C4 File Offset: 0x0001A4C4
		private void ReductionStep()
		{
			this.PreReductionStep();
			this.CustomReductionStep();
			this.PostReductionStep();
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0001C2D8 File Offset: 0x0001A4D8
		protected virtual void StepInitialize()
		{
			int vertexCount = this.vmesh.VertexCount;
			this.joinEdgeList = new NativeList<StepReductionBase.JoinEdge>(vertexCount / 4, Allocator.Persistent);
			this.completeVertexSet = new NativeParallelHashSet<int>(vertexCount, Allocator.Persistent);
			this.removePairList = new NativeList<int2>(vertexCount, Allocator.Persistent);
			this.resultArray = new NativeArray<int>(this.maxStep, Allocator.Persistent, NativeArrayOptions.ClearMemory);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00005305 File Offset: 0x00003505
		protected virtual void CustomReductionStep()
		{
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0001C33C File Offset: 0x0001A53C
		private void PreReductionStep()
		{
			this.joinEdgeList.Clear();
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0001C349 File Offset: 0x0001A549
		private void PostReductionStep()
		{
			this.SortJoinEdge();
			this.DetermineJoinEdge();
			this.RunJoinEdge();
			this.UpdateJoinAndLink();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0001C363 File Offset: 0x0001A563
		private void SortJoinEdge()
		{
			this.joinEdgeList.Sort<StepReductionBase.JoinEdge>();
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0001C370 File Offset: 0x0001A570
		private void DetermineJoinEdge()
		{
			new StepReductionBase.DeterminJoinEdgeJob
			{
				stepIndex = this.nowStepIndex,
				mergeLength = this.nowMergeLength,
				joinEdgeList = this.joinEdgeList,
				completeVertexSet = this.completeVertexSet,
				removePairList = this.removePairList,
				resultArray = this.resultArray
			}.Run<StepReductionBase.DeterminJoinEdgeJob>();
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0001C3DC File Offset: 0x0001A5DC
		private void RunJoinEdge()
		{
			new StepReductionBase.JoinPairJob
			{
				joinPositionAdjustment = this.joinPositionAdjustment,
				removePairList = this.removePairList,
				localPositions = this.vmesh.localPositions.GetNativeArray(),
				localNormals = this.vmesh.localNormals.GetNativeArray(),
				joinIndices = this.workData.vertexJoinIndices,
				vertexToVertexMap = this.workData.vertexToVertexMap,
				boneWeights = this.vmesh.boneWeights.GetNativeArray(),
				attributes = this.vmesh.attributes.GetNativeArray()
			}.Run<StepReductionBase.JoinPairJob>();
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0001C494 File Offset: 0x0001A694
		private void UpdateJoinAndLink()
		{
			new StepReductionBase.UpdateJoinIndexJob
			{
				joinIndices = this.workData.vertexJoinIndices
			}.Run(this.vmesh.VertexCount);
			new StepReductionBase.UpdateLinkIndexJob
			{
				joinIndices = this.workData.vertexJoinIndices,
				vertexToVertexMap = this.workData.vertexToVertexMap
			}.Run(this.vmesh.VertexCount);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0001C50C File Offset: 0x0001A70C
		private void UpdateReductionResultJob()
		{
			new StepReductionBase.FinalMergeVertexJob
			{
				joinIndices = this.workData.vertexJoinIndices,
				localNormals = this.vmesh.localNormals.GetNativeArray(),
				boneWeights = this.vmesh.boneWeights.GetNativeArray()
			}.Run(this.vmesh.VertexCount);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0001C574 File Offset: 0x0001A774
		protected static bool CheckJoin2(in NativeParallelMultiHashMap<ushort, ushort> vertexToVertexMap, int vindex, int tvindex, bool dontMakeLine)
		{
			FixedList512Bytes<ushort> fixedList512Bytes = default(FixedList512Bytes<ushort>);
			NativeParallelMultiHashMap<ushort, ushort> nativeParallelMultiHashMap = vertexToVertexMap;
			foreach (ushort num in nativeParallelMultiHashMap.GetValuesForKey((ushort)vindex))
			{
				if ((int)num != vindex && (int)num != tvindex)
				{
					ref fixedList512Bytes.Set(num);
				}
			}
			nativeParallelMultiHashMap = vertexToVertexMap;
			foreach (ushort num2 in nativeParallelMultiHashMap.GetValuesForKey((ushort)tvindex))
			{
				if ((int)num2 != vindex && (int)num2 != tvindex)
				{
					ref fixedList512Bytes.Set(num2);
				}
			}
			if (fixedList512Bytes.Length == 0)
			{
				return false;
			}
			if (dontMakeLine)
			{
				FixedList512Bytes<ushort> fixedList512Bytes2 = default(FixedList512Bytes<ushort>);
				ref fixedList512Bytes2.Push(fixedList512Bytes[0]);
				while (fixedList512Bytes2.Length > 0)
				{
					ushort num3 = ref fixedList512Bytes2.Pop<ushort>();
					if (ref fixedList512Bytes.Contains(num3))
					{
						ref fixedList512Bytes.RemoveItemAtSwapBack(num3);
						nativeParallelMultiHashMap = vertexToVertexMap;
						foreach (ushort num4 in nativeParallelMultiHashMap.GetValuesForKey(num3))
						{
							if (ref fixedList512Bytes.Contains(num4))
							{
								ref fixedList512Bytes2.Push(num4);
							}
						}
					}
				}
				if (fixedList512Bytes.Length > 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0001C700 File Offset: 0x0001A900
		protected static bool CheckJoin(in NativeArray<FixedList128Bytes<ushort>> vertexToVertexArray, int vindex, int tvindex, in FixedList128Bytes<ushort> vlist, in FixedList128Bytes<ushort> tvlist, bool dontMakeLine)
		{
			FixedList128Bytes<ushort> fixedList128Bytes = default(FixedList128Bytes<ushort>);
			int num = 0;
			for (;;)
			{
				int num2 = num;
				FixedList128Bytes<ushort> fixedList128Bytes2 = vlist;
				if (num2 >= fixedList128Bytes2.Length)
				{
					break;
				}
				fixedList128Bytes2 = vlist;
				int num3 = (int)fixedList128Bytes2[num];
				if (num3 != vindex && num3 != tvindex)
				{
					ref fixedList128Bytes.SetLimit((ushort)num3);
				}
				num++;
			}
			int num4 = 0;
			for (;;)
			{
				int num5 = num4;
				FixedList128Bytes<ushort> fixedList128Bytes2 = tvlist;
				if (num5 >= fixedList128Bytes2.Length)
				{
					break;
				}
				fixedList128Bytes2 = tvlist;
				int num6 = (int)fixedList128Bytes2[num4];
				if (num6 != vindex && num6 != tvindex)
				{
					ref fixedList128Bytes.SetLimit((ushort)num6);
				}
				num4++;
			}
			if (fixedList128Bytes.Length == 0)
			{
				return false;
			}
			if (dontMakeLine)
			{
				FixedList512Bytes<ushort> fixedList512Bytes = default(FixedList512Bytes<ushort>);
				ref fixedList512Bytes.Push(fixedList128Bytes[0]);
				while (fixedList512Bytes.Length > 0)
				{
					ushort num7 = ref fixedList512Bytes.Pop<ushort>();
					if (ref fixedList128Bytes.Contains(num7))
					{
						ref fixedList128Bytes.RemoveItemAtSwapBack(num7);
						NativeArray<FixedList128Bytes<ushort>> nativeArray = vertexToVertexArray;
						FixedList128Bytes<ushort> fixedList128Bytes3 = nativeArray[(int)num7];
						for (int i = 0; i < fixedList128Bytes3.Length; i++)
						{
							ushort num8 = fixedList128Bytes3[i];
							if (ref fixedList128Bytes.Contains(num8))
							{
								ref fixedList512Bytes.Push(num8);
							}
						}
					}
				}
				if (fixedList128Bytes.Length > 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040005B1 RID: 1457
		protected string name = string.Empty;

		// Token: 0x040005B2 RID: 1458
		protected VirtualMesh vmesh;

		// Token: 0x040005B3 RID: 1459
		protected ReductionWorkData workData;

		// Token: 0x040005B4 RID: 1460
		protected ResultCode result;

		// Token: 0x040005B5 RID: 1461
		protected float startMergeLength;

		// Token: 0x040005B6 RID: 1462
		protected float endMergeLength;

		// Token: 0x040005B7 RID: 1463
		protected int maxStep;

		// Token: 0x040005B8 RID: 1464
		protected bool dontMakeLine;

		// Token: 0x040005B9 RID: 1465
		protected float joinPositionAdjustment;

		// Token: 0x040005BA RID: 1466
		protected int nowStepIndex;

		// Token: 0x040005BB RID: 1467
		protected float nowMergeLength;

		// Token: 0x040005BC RID: 1468
		protected float nowStepScale;

		// Token: 0x040005BD RID: 1469
		protected NativeList<StepReductionBase.JoinEdge> joinEdgeList;

		// Token: 0x040005BE RID: 1470
		private NativeParallelHashSet<int> completeVertexSet;

		// Token: 0x040005BF RID: 1471
		private NativeList<int2> removePairList;

		// Token: 0x040005C0 RID: 1472
		private NativeArray<int> resultArray;

		// Token: 0x020000B9 RID: 185
		public struct JoinEdge : IComparable<StepReductionBase.JoinEdge>
		{
			// Token: 0x060002C0 RID: 704 RVA: 0x0001C83C File Offset: 0x0001AA3C
			public bool Contains(in int2 pair)
			{
				return this.vertexPair.x == pair.x || this.vertexPair.x == pair.y || this.vertexPair.y == pair.x || this.vertexPair.y == pair.y;
			}

			// Token: 0x060002C1 RID: 705 RVA: 0x0001C898 File Offset: 0x0001AA98
			public int CompareTo(StepReductionBase.JoinEdge other)
			{
				if (this.cost == other.cost)
				{
					return 0;
				}
				if (this.cost >= other.cost)
				{
					return 1;
				}
				return -1;
			}

			// Token: 0x040005C1 RID: 1473
			public int2 vertexPair;

			// Token: 0x040005C2 RID: 1474
			public float cost;
		}

		// Token: 0x020000BA RID: 186
		[BurstCompile]
		private struct DeterminJoinEdgeJob : IJob
		{
			// Token: 0x060002C2 RID: 706 RVA: 0x0001C8BC File Offset: 0x0001AABC
			public void Execute()
			{
				this.completeVertexSet.Clear();
				this.removePairList.Clear();
				int num = 0;
				for (int i = 0; i < this.joinEdgeList.Length; i++)
				{
					int2 vertexPair = this.joinEdgeList[i].vertexPair;
					if (!this.completeVertexSet.Contains(vertexPair.x) && !this.completeVertexSet.Contains(vertexPair.y))
					{
						int2 xy = vertexPair.xy;
						this.removePairList.Add(xy);
						this.completeVertexSet.Add(vertexPair.x);
						this.completeVertexSet.Add(vertexPair.y);
						num++;
					}
				}
				this.resultArray[this.stepIndex] = num;
			}

			// Token: 0x040005C3 RID: 1475
			public int stepIndex;

			// Token: 0x040005C4 RID: 1476
			public float mergeLength;

			// Token: 0x040005C5 RID: 1477
			[ReadOnly]
			public NativeList<StepReductionBase.JoinEdge> joinEdgeList;

			// Token: 0x040005C6 RID: 1478
			public NativeParallelHashSet<int> completeVertexSet;

			// Token: 0x040005C7 RID: 1479
			public NativeList<int2> removePairList;

			// Token: 0x040005C8 RID: 1480
			public NativeArray<int> resultArray;
		}

		// Token: 0x020000BB RID: 187
		[BurstCompile]
		private struct JoinPairJob : IJob
		{
			// Token: 0x060002C3 RID: 707 RVA: 0x0001C984 File Offset: 0x0001AB84
			public void Execute()
			{
				for (int i = 0; i < this.removePairList.Length; i++)
				{
					int2 @int = this.removePairList[i];
					int x = @int.x;
					int y = @int.y;
					float3 y2 = this.localPositions[x];
					float3 x2 = this.localPositions[y];
					float3 lhs = this.localNormals[x];
					float3 rhs = this.localNormals[y];
					this.joinIndices[x] = y;
					float num = (float)math.max(this.vertexToVertexMap.CountValuesForKey((ushort)x) - 1, 1);
					float num2 = (float)math.max(this.vertexToVertexMap.CountValuesForKey((ushort)y) - 1, 1);
					float num3 = num2 / (num + num2);
					num3 = math.lerp(num3, 0.5f, this.joinPositionAdjustment);
					float3 value = math.lerp(x2, y2, num3);
					this.localPositions[y] = value;
					this.localNormals[y] = lhs + rhs;
					FixedList512Bytes<ushort> fixedList512Bytes = default(FixedList512Bytes<ushort>);
					foreach (ushort num4 in this.vertexToVertexMap.GetValuesForKey((ushort)x))
					{
						if ((int)num4 != x && (int)num4 != y)
						{
							ref fixedList512Bytes.Set(num4);
						}
					}
					foreach (ushort num5 in this.vertexToVertexMap.GetValuesForKey((ushort)y))
					{
						if ((int)num5 != x && (int)num5 != y)
						{
							ref fixedList512Bytes.Set(num5);
						}
					}
					this.vertexToVertexMap.Remove((ushort)y);
					for (int j = 0; j < fixedList512Bytes.Length; j++)
					{
						this.vertexToVertexMap.Add((ushort)y, fixedList512Bytes[j]);
					}
					VirtualMeshBoneWeight value2 = this.boneWeights[y];
					VirtualMeshBoneWeight virtualMeshBoneWeight = this.boneWeights[x];
					value2.AddWeight(virtualMeshBoneWeight);
					this.boneWeights[y] = value2;
					VertexAttribute attr = this.attributes[x];
					VertexAttribute attr2 = this.attributes[y];
					this.attributes[y] = VertexAttribute.JoinAttribute(attr, attr2);
					this.attributes[x] = VertexAttribute.Invalid;
				}
			}

			// Token: 0x040005C9 RID: 1481
			public float joinPositionAdjustment;

			// Token: 0x040005CA RID: 1482
			[ReadOnly]
			public NativeList<int2> removePairList;

			// Token: 0x040005CB RID: 1483
			public NativeArray<float3> localPositions;

			// Token: 0x040005CC RID: 1484
			public NativeArray<float3> localNormals;

			// Token: 0x040005CD RID: 1485
			public NativeParallelMultiHashMap<ushort, ushort> vertexToVertexMap;

			// Token: 0x040005CE RID: 1486
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			// Token: 0x040005CF RID: 1487
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x040005D0 RID: 1488
			public NativeArray<int> joinIndices;
		}

		// Token: 0x020000BC RID: 188
		[BurstCompile]
		private struct UpdateJoinIndexJob : IJobParallelFor
		{
			// Token: 0x060002C4 RID: 708 RVA: 0x0001CBF4 File Offset: 0x0001ADF4
			public void Execute(int vindex)
			{
				int num = this.joinIndices[vindex];
				if (num >= 0)
				{
					while (this.joinIndices[num] >= 0)
					{
						num = this.joinIndices[num];
					}
					this.joinIndices[vindex] = num;
				}
			}

			// Token: 0x040005D1 RID: 1489
			[NativeDisableParallelForRestriction]
			public NativeArray<int> joinIndices;
		}

		// Token: 0x020000BD RID: 189
		[BurstCompile]
		private struct UpdateLinkIndexJob : IJobParallelFor
		{
			// Token: 0x060002C5 RID: 709 RVA: 0x0001CC40 File Offset: 0x0001AE40
			public void Execute(int vindex)
			{
				if (this.joinIndices[vindex] >= 0)
				{
					return;
				}
				FixedList512Bytes<ushort> fixedList512Bytes = default(FixedList512Bytes<ushort>);
				using (NativeParallelMultiHashMap<ushort, ushort>.Enumerator enumerator = this.vertexToVertexMap.GetValuesForKey((ushort)vindex).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int num = (int)enumerator.Current;
						int num2 = this.joinIndices[num];
						if (num2 >= 0)
						{
							num = num2;
						}
						if (num != vindex)
						{
							ref fixedList512Bytes.Set((ushort)num);
						}
					}
				}
				this.vertexToVertexMap.Remove((ushort)vindex);
				for (int i = 0; i < fixedList512Bytes.Length; i++)
				{
					this.vertexToVertexMap.Add((ushort)vindex, fixedList512Bytes[i]);
				}
			}

			// Token: 0x040005D2 RID: 1490
			[NativeDisableParallelForRestriction]
			public NativeArray<int> joinIndices;

			// Token: 0x040005D3 RID: 1491
			public NativeParallelMultiHashMap<ushort, ushort> vertexToVertexMap;
		}

		// Token: 0x020000BE RID: 190
		[BurstCompile]
		private struct FinalMergeVertexJob : IJobParallelFor
		{
			// Token: 0x060002C6 RID: 710 RVA: 0x0001CD0C File Offset: 0x0001AF0C
			public void Execute(int vindex)
			{
				if (this.joinIndices[vindex] >= 0)
				{
					return;
				}
				this.localNormals[vindex] = math.normalize(this.localNormals[vindex]);
				VirtualMeshBoneWeight value = this.boneWeights[vindex];
				value.AdjustWeight();
				this.boneWeights[vindex] = value;
			}

			// Token: 0x040005D4 RID: 1492
			[ReadOnly]
			public NativeArray<int> joinIndices;

			// Token: 0x040005D5 RID: 1493
			public NativeArray<float3> localNormals;

			// Token: 0x040005D6 RID: 1494
			public NativeArray<VirtualMeshBoneWeight> boneWeights;
		}
	}
}
