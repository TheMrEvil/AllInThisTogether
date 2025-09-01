using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x020000AC RID: 172
	public class SameDistanceReduction : IDisposable
	{
		// Token: 0x06000294 RID: 660 RVA: 0x0001B0EB File Offset: 0x000192EB
		public SameDistanceReduction()
		{
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0001B100 File Offset: 0x00019300
		public SameDistanceReduction(string name, VirtualMesh mesh, ReductionWorkData workingData, float mergeLength)
		{
			this.name = name;
			this.vmesh = mesh;
			this.workData = workingData;
			this.result = ResultCode.None;
			this.mergeLength = math.max(mergeLength, 1E-09f);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0001B150 File Offset: 0x00019350
		public virtual void Dispose()
		{
			if (this.joinPairSet.IsCreated)
			{
				this.joinPairSet.Dispose();
			}
			if (this.resultRef.IsCreated)
			{
				this.resultRef.Dispose();
			}
			GridMap<int> gridMap = this.gridMap;
			if (gridMap == null)
			{
				return;
			}
			gridMap.Dispose();
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0001B19D File Offset: 0x0001939D
		public ResultCode Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0001B1A8 File Offset: 0x000193A8
		public ResultCode Reduction()
		{
			this.result.Clear();
			try
			{
				this.gridMap = new GridMap<int>(this.vmesh.VertexCount);
				float gridSize = this.mergeLength * 2f;
				this.joinPairSet = new NativeParallelHashSet<int2>(this.vmesh.VertexCount / 4, Allocator.Persistent);
				this.resultRef = new NativeReference<int>(Allocator.Persistent, NativeArrayOptions.ClearMemory);
				new SameDistanceReduction.InitGridJob
				{
					vcnt = this.vmesh.VertexCount,
					gridSize = gridSize,
					localPositions = this.vmesh.localPositions.GetNativeArray(),
					joinIndices = this.workData.vertexJoinIndices,
					gridMap = this.gridMap.GetMultiHashMap()
				}.Run<SameDistanceReduction.InitGridJob>();
				new SameDistanceReduction.SearchJoinJob
				{
					vcnt = this.vmesh.VertexCount,
					gridSize = gridSize,
					radius = this.mergeLength,
					localPositions = this.vmesh.localPositions.GetNativeArray(),
					joinIndices = this.workData.vertexJoinIndices,
					gridMap = this.gridMap.GetMultiHashMap(),
					joinPairSet = this.joinPairSet
				}.Run<SameDistanceReduction.SearchJoinJob>();
				new SameDistanceReduction.JoinJob
				{
					joinPairSet = this.joinPairSet,
					joinIndices = this.workData.vertexJoinIndices,
					vertexToVertexMap = this.workData.vertexToVertexMap,
					boneWeights = this.vmesh.boneWeights.GetNativeArray(),
					attributes = this.vmesh.attributes.GetNativeArray(),
					result = this.resultRef
				}.Run<SameDistanceReduction.JoinJob>();
				this.UpdateJoinAndLink();
				this.UpdateReductionResultJob();
				int value = this.resultRef.Value;
				this.workData.removeVertexCount += value;
				this.result.SetSuccess();
			}
			catch (Exception message)
			{
				Debug.LogError(message);
				this.result.SetError(Define.Result.Reduction_SameDistanceException);
			}
			return this.result;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0001B3E0 File Offset: 0x000195E0
		private void UpdateJoinAndLink()
		{
			new SameDistanceReduction.UpdateJoinIndexJob
			{
				joinIndices = this.workData.vertexJoinIndices
			}.Run(this.vmesh.VertexCount);
			new SameDistanceReduction.UpdateLinkIndexJob
			{
				joinIndices = this.workData.vertexJoinIndices,
				vertexToVertexMap = this.workData.vertexToVertexMap
			}.Run(this.vmesh.VertexCount);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0001B458 File Offset: 0x00019658
		private void UpdateReductionResultJob()
		{
			new SameDistanceReduction.FinalMergeVertexJob
			{
				joinIndices = this.workData.vertexJoinIndices,
				localNormals = this.vmesh.localNormals.GetNativeArray(),
				boneWeights = this.vmesh.boneWeights.GetNativeArray()
			}.Run(this.vmesh.VertexCount);
		}

		// Token: 0x0400057C RID: 1404
		private string name = string.Empty;

		// Token: 0x0400057D RID: 1405
		private VirtualMesh vmesh;

		// Token: 0x0400057E RID: 1406
		private ReductionWorkData workData;

		// Token: 0x0400057F RID: 1407
		private ResultCode result;

		// Token: 0x04000580 RID: 1408
		private float mergeLength;

		// Token: 0x04000581 RID: 1409
		private GridMap<int> gridMap;

		// Token: 0x04000582 RID: 1410
		private NativeParallelHashSet<int2> joinPairSet;

		// Token: 0x04000583 RID: 1411
		private NativeReference<int> resultRef;

		// Token: 0x020000AD RID: 173
		[BurstCompile]
		private struct InitGridJob : IJob
		{
			// Token: 0x0600029B RID: 667 RVA: 0x0001B4C0 File Offset: 0x000196C0
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

			// Token: 0x04000584 RID: 1412
			public int vcnt;

			// Token: 0x04000585 RID: 1413
			public float gridSize;

			// Token: 0x04000586 RID: 1414
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x04000587 RID: 1415
			[ReadOnly]
			public NativeArray<int> joinIndices;

			// Token: 0x04000588 RID: 1416
			public NativeParallelMultiHashMap<int3, int> gridMap;
		}

		// Token: 0x020000AE RID: 174
		[BurstCompile]
		private struct SearchJoinJob : IJob
		{
			// Token: 0x0600029C RID: 668 RVA: 0x0001B50C File Offset: 0x0001970C
			public void Execute()
			{
				for (int i = 0; i < this.vcnt; i++)
				{
					if (this.joinIndices[i] < 0)
					{
						float3 @float = this.localPositions[i];
						foreach (int3 key in GridMap<int>.GetArea(@float, this.radius, this.gridMap, this.gridSize))
						{
							if (this.gridMap.ContainsKey(key))
							{
								foreach (int num in this.gridMap.GetValuesForKey(key))
								{
									if (num != i)
									{
										float3 y = this.localPositions[num];
										if (math.distance(@float, y) <= this.radius)
										{
											int2 item = DataUtility.PackInt2(i, num);
											this.joinPairSet.Add(item);
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x04000589 RID: 1417
			public int vcnt;

			// Token: 0x0400058A RID: 1418
			public float gridSize;

			// Token: 0x0400058B RID: 1419
			public float radius;

			// Token: 0x0400058C RID: 1420
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x0400058D RID: 1421
			[ReadOnly]
			public NativeArray<int> joinIndices;

			// Token: 0x0400058E RID: 1422
			[ReadOnly]
			public NativeParallelMultiHashMap<int3, int> gridMap;

			// Token: 0x0400058F RID: 1423
			public NativeParallelHashSet<int2> joinPairSet;
		}

		// Token: 0x020000AF RID: 175
		[BurstCompile]
		private struct JoinJob : IJob
		{
			// Token: 0x0600029D RID: 669 RVA: 0x0001B640 File Offset: 0x00019840
			public void Execute()
			{
				FixedList512Bytes<ushort> fixedList512Bytes = default(FixedList512Bytes<ushort>);
				int num = 0;
				foreach (int2 @int in this.joinPairSet)
				{
					int num2 = @int[0];
					int num3 = @int[1];
					while (this.joinIndices[num3] >= 0)
					{
						num3 = this.joinIndices[num3];
					}
					while (this.joinIndices[num2] >= 0)
					{
						num2 = this.joinIndices[num2];
					}
					if (num3 != num2)
					{
						this.joinIndices[num3] = num2;
						num++;
						fixedList512Bytes.Clear();
						using (NativeParallelMultiHashMap<ushort, ushort>.Enumerator enumerator2 = this.vertexToVertexMap.GetValuesForKey((ushort)num3).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								int num4 = (int)enumerator2.Current;
								while (this.joinIndices[num4] >= 0)
								{
									num4 = this.joinIndices[num4];
								}
								if (num4 != num3 && num4 != num2)
								{
									ref fixedList512Bytes.Set((ushort)num4);
								}
							}
						}
						using (NativeParallelMultiHashMap<ushort, ushort>.Enumerator enumerator2 = this.vertexToVertexMap.GetValuesForKey((ushort)num2).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								int num5 = (int)enumerator2.Current;
								while (this.joinIndices[num5] >= 0)
								{
									num5 = this.joinIndices[num5];
								}
								if (num5 != num3 && num5 != num2)
								{
									ref fixedList512Bytes.Set((ushort)num5);
								}
							}
						}
						this.vertexToVertexMap.Remove((ushort)num2);
						for (int i = 0; i < fixedList512Bytes.Length; i++)
						{
							this.vertexToVertexMap.Add((ushort)num2, fixedList512Bytes[i]);
						}
						VirtualMeshBoneWeight value = this.boneWeights[num2];
						VirtualMeshBoneWeight virtualMeshBoneWeight = this.boneWeights[num3];
						value.AddWeight(virtualMeshBoneWeight);
						this.boneWeights[num2] = value;
						VertexAttribute attr = this.attributes[num3];
						VertexAttribute attr2 = this.attributes[num2];
						this.attributes[num2] = VertexAttribute.JoinAttribute(attr, attr2);
						this.attributes[num3] = VertexAttribute.Invalid;
					}
				}
				this.result.Value = num;
			}

			// Token: 0x04000590 RID: 1424
			[ReadOnly]
			public NativeParallelHashSet<int2> joinPairSet;

			// Token: 0x04000591 RID: 1425
			public NativeArray<int> joinIndices;

			// Token: 0x04000592 RID: 1426
			public NativeParallelMultiHashMap<ushort, ushort> vertexToVertexMap;

			// Token: 0x04000593 RID: 1427
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			// Token: 0x04000594 RID: 1428
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x04000595 RID: 1429
			public NativeReference<int> result;
		}

		// Token: 0x020000B0 RID: 176
		[BurstCompile]
		private struct UpdateJoinIndexJob : IJobParallelFor
		{
			// Token: 0x0600029E RID: 670 RVA: 0x0001B900 File Offset: 0x00019B00
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

			// Token: 0x04000596 RID: 1430
			[NativeDisableParallelForRestriction]
			public NativeArray<int> joinIndices;
		}

		// Token: 0x020000B1 RID: 177
		[BurstCompile]
		private struct UpdateLinkIndexJob : IJobParallelFor
		{
			// Token: 0x0600029F RID: 671 RVA: 0x0001B94C File Offset: 0x00019B4C
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

			// Token: 0x04000597 RID: 1431
			[NativeDisableParallelForRestriction]
			public NativeArray<int> joinIndices;

			// Token: 0x04000598 RID: 1432
			public NativeParallelMultiHashMap<ushort, ushort> vertexToVertexMap;
		}

		// Token: 0x020000B2 RID: 178
		[BurstCompile]
		private struct FinalMergeVertexJob : IJobParallelFor
		{
			// Token: 0x060002A0 RID: 672 RVA: 0x0001BA18 File Offset: 0x00019C18
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

			// Token: 0x04000599 RID: 1433
			[ReadOnly]
			public NativeArray<int> joinIndices;

			// Token: 0x0400059A RID: 1434
			public NativeArray<float3> localNormals;

			// Token: 0x0400059B RID: 1435
			public NativeArray<VirtualMeshBoneWeight> boneWeights;
		}
	}
}
