using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x020000CA RID: 202
	public static class JobUtility
	{
		// Token: 0x06000317 RID: 791 RVA: 0x0001DD74 File Offset: 0x0001BF74
		public static JobHandle Fill(NativeArray<int> array, int length, int value, JobHandle dependsOn = default(JobHandle))
		{
			return new JobUtility.FillJob<int>
			{
				value = value,
				array = array
			}.Schedule(length, 32, dependsOn);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0001DDA4 File Offset: 0x0001BFA4
		public static JobHandle Fill(NativeArray<Vector4> array, int length, Vector4 value, JobHandle dependsOn = default(JobHandle))
		{
			return new JobUtility.FillJob<Vector4>
			{
				value = value,
				array = array
			}.Schedule(length, 32, dependsOn);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0001DDD4 File Offset: 0x0001BFD4
		public static JobHandle Fill(NativeArray<VirtualMeshBoneWeight> array, int length, VirtualMeshBoneWeight value, JobHandle dependsOn = default(JobHandle))
		{
			return new JobUtility.FillJob<VirtualMeshBoneWeight>
			{
				value = value,
				array = array
			}.Schedule(length, 32, dependsOn);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0001DE04 File Offset: 0x0001C004
		public static JobHandle Fill(NativeArray<byte> array, int length, byte value, JobHandle dependsOn = default(JobHandle))
		{
			return new JobUtility.FillJob<byte>
			{
				value = value,
				array = array
			}.Schedule(length, 32, dependsOn);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0001DE34 File Offset: 0x0001C034
		public static void FillRun(NativeArray<int> array, int length, int value)
		{
			new JobUtility.FillJob<int>
			{
				value = value,
				array = array
			}.Run(length);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0001DE60 File Offset: 0x0001C060
		public static void FillRun(NativeArray<Vector4> array, int length, Vector4 value)
		{
			new JobUtility.FillJob<Vector4>
			{
				value = value,
				array = array
			}.Run(length);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0001DE8C File Offset: 0x0001C08C
		public static void FillRun(NativeArray<quaternion> array, int length, quaternion value)
		{
			new JobUtility.FillJob<quaternion>
			{
				value = value,
				array = array
			}.Run(length);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0001DEB8 File Offset: 0x0001C0B8
		public static void FillRun(NativeArray<VirtualMeshBoneWeight> array, int length, VirtualMeshBoneWeight value)
		{
			new JobUtility.FillJob<VirtualMeshBoneWeight>
			{
				value = value,
				array = array
			}.Run(length);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0001DEE4 File Offset: 0x0001C0E4
		public static JobHandle Fill(NativeArray<int> array, int startIndex, int length, int value, JobHandle dependsOn = default(JobHandle))
		{
			return new JobUtility.FillJob2<int>
			{
				value = value,
				startIndex = startIndex,
				array = array
			}.Schedule(length, 32, dependsOn);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0001DF1C File Offset: 0x0001C11C
		public static JobHandle Fill(NativeReference<int> reference, int value, JobHandle dependsOn = default(JobHandle))
		{
			return new JobUtility.FillRefJob<int>
			{
				value = value,
				reference = reference
			}.Schedule(dependsOn);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0001DF48 File Offset: 0x0001C148
		public static JobHandle SerialNumber(NativeArray<int> array, int length, JobHandle dependsOn = default(JobHandle))
		{
			return new JobUtility.SerialNumberJob
			{
				array = array
			}.Schedule(length, 32, dependsOn);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0001DF70 File Offset: 0x0001C170
		public static void SerialNumberRun(NativeArray<int> array, int length)
		{
			new JobUtility.SerialNumberJob
			{
				array = array
			}.Run(length);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001DF94 File Offset: 0x0001C194
		public static JobHandle ConvertHashSetToNativeList(NativeParallelHashSet<int> hashSet, NativeList<int> list, JobHandle dependsOn = default(JobHandle))
		{
			return new JobUtility.ConvertHashSetToListJob<int>
			{
				hashSet = hashSet,
				list = list
			}.Schedule(dependsOn);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001DFC0 File Offset: 0x0001C1C0
		public static JobHandle ConvertMultiHashMapKeyToNativeList(NativeParallelMultiHashMap<int2, int> hashMap, NativeList<int2> keyList, JobHandle dependsOn = default(JobHandle))
		{
			return new JobUtility.ConvertMultiHashMapKeyToListJob<int2, int>
			{
				hashMap = hashMap,
				list = keyList
			}.Schedule(dependsOn);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0001DFEC File Offset: 0x0001C1EC
		public static JobHandle ConvertHashSetKeyToNativeList(NativeParallelHashSet<int2> hashSet, NativeList<int2> keyList, JobHandle dependsOn = default(JobHandle))
		{
			return new JobUtility.ConvertHashSetKeyToListJob<int2>
			{
				hashSet = hashSet,
				list = keyList
			}.Schedule(dependsOn);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0001E018 File Offset: 0x0001C218
		public static JobHandle ConvertHashSetKeyToNativeList(NativeParallelHashSet<int4> hashSet, NativeList<int4> keyList, JobHandle dependsOn = default(JobHandle))
		{
			return new JobUtility.ConvertHashSetKeyToListJob<int4>
			{
				hashSet = hashSet,
				list = keyList
			}.Schedule(dependsOn);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0001E044 File Offset: 0x0001C244
		public static JobHandle CalcAABB(NativeArray<float3> positions, int length, NativeReference<AABB> outAABB, JobHandle dependsOn = default(JobHandle))
		{
			return new JobUtility.CalcAABBJob
			{
				length = length,
				positions = positions,
				outAABB = outAABB
			}.Schedule(dependsOn);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0001E078 File Offset: 0x0001C278
		public static void CalcAABBRun(NativeArray<float3> positions, int length, NativeReference<AABB> outAABB)
		{
			new JobUtility.CalcAABBJob
			{
				length = length,
				positions = positions,
				outAABB = outAABB
			}.Run<JobUtility.CalcAABBJob>();
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0001E0AC File Offset: 0x0001C2AC
		public static JobHandle CalcAABB(NativeList<float3> positions, NativeReference<AABB> outAABB, JobHandle dependsOn = default(JobHandle))
		{
			return new JobUtility.CalcAABBDeferJob
			{
				positions = positions,
				outAABB = outAABB
			}.Schedule(dependsOn);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0001E0D8 File Offset: 0x0001C2D8
		public static void CalcAABBRun(NativeList<float3> positions, NativeReference<AABB> outAABB)
		{
			new JobUtility.CalcAABBDeferJob
			{
				positions = positions,
				outAABB = outAABB
			}.Run<JobUtility.CalcAABBDeferJob>();
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0001E104 File Offset: 0x0001C304
		private static AABB CalcAABBInternal(in NativeArray<float3> positions, int length)
		{
			NativeArray<float3> nativeArray = positions;
			if (nativeArray.Length == 0)
			{
				return default(AABB);
			}
			float3 x = float.MaxValue;
			float3 x2 = float.MinValue;
			for (int i = 0; i < length; i++)
			{
				nativeArray = positions;
				float3 y = nativeArray[i];
				x = math.min(x, y);
				x2 = math.max(x2, y);
			}
			return new AABB(ref x, ref x2);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0001E180 File Offset: 0x0001C380
		public static JobHandle CalcUVWithSphereMapping(NativeArray<float3> positions, int length, NativeReference<AABB> aabb, NativeArray<float2> outUVs, JobHandle dependsOn = default(JobHandle))
		{
			return new JobUtility.CalcUVJob
			{
				positions = positions,
				aabb = aabb,
				uvs = outUVs
			}.Schedule(length, 32, dependsOn);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0001E1B8 File Offset: 0x0001C3B8
		public static void CalcUVWithSphereMappingRun(NativeArray<float3> positions, int length, NativeReference<AABB> aabb, NativeArray<float2> outUVs)
		{
			new JobUtility.CalcUVJob
			{
				positions = positions,
				aabb = aabb,
				uvs = outUVs
			}.Run(length);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001E1EC File Offset: 0x0001C3EC
		public static JobHandle TransformPosition(NativeArray<float3> positions, int length, in float4x4 toM, JobHandle dependsOn = default(JobHandle))
		{
			return new JobUtility.TransformPositionJob
			{
				toM = toM,
				positions = positions
			}.Schedule(length, 32, dependsOn);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0001E220 File Offset: 0x0001C420
		public static void TransformPositionRun(NativeArray<float3> positions, int length, in float4x4 toM)
		{
			new JobUtility.TransformPositionJob
			{
				toM = toM,
				positions = positions
			}.Run(length);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0001E254 File Offset: 0x0001C454
		public static JobHandle TransformPosition(NativeArray<float3> srcPositions, NativeArray<float3> dstPositions, int length, in float4x4 toM, JobHandle dependsOn = default(JobHandle))
		{
			return new JobUtility.TransformPositionJob2
			{
				toM = toM,
				srcPositions = srcPositions,
				dstPositions = dstPositions
			}.Schedule(length, 32, dependsOn);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001E294 File Offset: 0x0001C494
		public static void TransformPositionRun(NativeArray<float3> srcPositions, NativeArray<float3> dstPositions, int length, in float4x4 toM)
		{
			new JobUtility.TransformPositionJob2
			{
				toM = toM,
				srcPositions = srcPositions,
				dstPositions = dstPositions
			}.Run(length);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0001E2D0 File Offset: 0x0001C4D0
		public static NativeParallelMultiHashMap<int, ushort> ToNativeMultiHashMap(in NativeArray<uint> indexArray, in NativeArray<ushort> dataArray)
		{
			NativeArray<ushort> nativeArray = dataArray;
			NativeParallelMultiHashMap<int, ushort> nativeParallelMultiHashMap = new NativeParallelMultiHashMap<int, ushort>(nativeArray.Length, Allocator.Persistent);
			new JobUtility.ConvertArrayToMapJob<ushort>
			{
				indexArray = indexArray,
				dataArray = dataArray,
				map = nativeParallelMultiHashMap
			}.Run<JobUtility.ConvertArrayToMapJob<ushort>>();
			return nativeParallelMultiHashMap;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001E32C File Offset: 0x0001C52C
		public static JobHandle ClearReference(NativeReference<int> reference, JobHandle jobHandle)
		{
			return new JobUtility.ClearReferenceJob
			{
				reference = reference
			}.Schedule(jobHandle);
		}

		// Token: 0x020000CB RID: 203
		[BurstCompile]
		private struct FillJob<[IsUnmanaged] T> : IJobParallelFor where T : struct, ValueType
		{
			// Token: 0x06000334 RID: 820 RVA: 0x0001E350 File Offset: 0x0001C550
			public void Execute(int index)
			{
				this.array[index] = this.value;
			}

			// Token: 0x04000604 RID: 1540
			public T value;

			// Token: 0x04000605 RID: 1541
			[WriteOnly]
			public NativeArray<T> array;
		}

		// Token: 0x020000CC RID: 204
		[BurstCompile]
		private struct FillJob2<[IsUnmanaged] T> : IJobParallelFor where T : struct, ValueType
		{
			// Token: 0x06000335 RID: 821 RVA: 0x0001E364 File Offset: 0x0001C564
			public void Execute(int index)
			{
				this.array[this.startIndex + index] = this.value;
			}

			// Token: 0x04000606 RID: 1542
			public T value;

			// Token: 0x04000607 RID: 1543
			public int startIndex;

			// Token: 0x04000608 RID: 1544
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<T> array;
		}

		// Token: 0x020000CD RID: 205
		[BurstCompile]
		private struct FillRefJob<[IsUnmanaged] T> : IJob where T : struct, ValueType
		{
			// Token: 0x06000336 RID: 822 RVA: 0x0001E37F File Offset: 0x0001C57F
			public void Execute()
			{
				this.reference.Value = this.value;
			}

			// Token: 0x04000609 RID: 1545
			public T value;

			// Token: 0x0400060A RID: 1546
			[WriteOnly]
			public NativeReference<T> reference;
		}

		// Token: 0x020000CE RID: 206
		[BurstCompile]
		private struct SerialNumberJob : IJobParallelFor
		{
			// Token: 0x06000337 RID: 823 RVA: 0x0001E392 File Offset: 0x0001C592
			public void Execute(int index)
			{
				this.array[index] = index;
			}

			// Token: 0x0400060B RID: 1547
			[WriteOnly]
			public NativeArray<int> array;
		}

		// Token: 0x020000CF RID: 207
		[BurstCompile]
		private struct ConvertHashSetToListJob<[IsUnmanaged] T> : IJob where T : struct, ValueType, IEquatable<T>
		{
			// Token: 0x06000338 RID: 824 RVA: 0x0001E3A4 File Offset: 0x0001C5A4
			public void Execute()
			{
				foreach (T value in this.hashSet)
				{
					this.list.AddNoResize(value);
				}
			}

			// Token: 0x0400060C RID: 1548
			[ReadOnly]
			public NativeParallelHashSet<T> hashSet;

			// Token: 0x0400060D RID: 1549
			[WriteOnly]
			public NativeList<T> list;
		}

		// Token: 0x020000D0 RID: 208
		[BurstCompile]
		private struct ConvertMultiHashMapKeyToListJob<[IsUnmanaged] T, [IsUnmanaged] U> : IJob where T : struct, ValueType, IEquatable<T> where U : struct, ValueType
		{
			// Token: 0x06000339 RID: 825 RVA: 0x0001E3FC File Offset: 0x0001C5FC
			public void Execute()
			{
				NativeParallelHashSet<T> nativeParallelHashSet = new NativeParallelHashSet<T>(this.hashMap.Count(), Allocator.Temp);
				NativeArray<T> keyArray = this.hashMap.GetKeyArray(Allocator.Temp);
				for (int i = 0; i < keyArray.Length; i++)
				{
					nativeParallelHashSet.Add(keyArray[i]);
				}
				foreach (T t in nativeParallelHashSet)
				{
					this.list.Add(t);
				}
			}

			// Token: 0x0400060E RID: 1550
			[ReadOnly]
			public NativeParallelMultiHashMap<T, U> hashMap;

			// Token: 0x0400060F RID: 1551
			[WriteOnly]
			public NativeList<T> list;
		}

		// Token: 0x020000D1 RID: 209
		[BurstCompile]
		private struct ConvertHashSetKeyToListJob<[IsUnmanaged] T> : IJob where T : struct, ValueType, IEquatable<T>
		{
			// Token: 0x0600033A RID: 826 RVA: 0x0001E4A0 File Offset: 0x0001C6A0
			public void Execute()
			{
				foreach (T t in this.hashSet)
				{
					this.list.Add(t);
				}
			}

			// Token: 0x04000610 RID: 1552
			[ReadOnly]
			public NativeParallelHashSet<T> hashSet;

			// Token: 0x04000611 RID: 1553
			[WriteOnly]
			public NativeList<T> list;
		}

		// Token: 0x020000D2 RID: 210
		[BurstCompile]
		private struct CalcAABBJob : IJob
		{
			// Token: 0x0600033B RID: 827 RVA: 0x0001E4FC File Offset: 0x0001C6FC
			public void Execute()
			{
				this.outAABB.Value = JobUtility.CalcAABBInternal(this.positions, this.length);
			}

			// Token: 0x04000612 RID: 1554
			public int length;

			// Token: 0x04000613 RID: 1555
			[ReadOnly]
			public NativeArray<float3> positions;

			// Token: 0x04000614 RID: 1556
			public NativeReference<AABB> outAABB;
		}

		// Token: 0x020000D3 RID: 211
		[BurstCompile]
		private struct CalcAABBDeferJob : IJob
		{
			// Token: 0x0600033C RID: 828 RVA: 0x0001E51C File Offset: 0x0001C71C
			public void Execute()
			{
				NativeArray<float3> nativeArray = this.positions.AsArray();
				this.outAABB.Value = JobUtility.CalcAABBInternal(nativeArray, this.positions.Length);
			}

			// Token: 0x04000615 RID: 1557
			[ReadOnly]
			public NativeList<float3> positions;

			// Token: 0x04000616 RID: 1558
			public NativeReference<AABB> outAABB;
		}

		// Token: 0x020000D4 RID: 212
		[BurstCompile]
		private struct CalcUVJob : IJobParallelFor
		{
			// Token: 0x0600033D RID: 829 RVA: 0x0001E554 File Offset: 0x0001C754
			public void Execute(int index)
			{
				float3 @float = this.positions[index] - this.aabb.Value.Center;
				@float = math.normalize(@float);
				float num = math.atan2(@float.x, @float.z);
				num = math.clamp(math.unlerp(-3.1415927f, 3.1415927f, num), 0f, 1f);
				float x = math.dot(math.up(), @float);
				x = math.clamp(math.unlerp(1f, -1f, x), 0f, 1f);
				float2 float2 = new float2(x, num);
				float rhs = (float)index * 0.0001234f;
				float2 = float2 * 10f + rhs;
				this.uvs[index] = float2;
			}

			// Token: 0x04000617 RID: 1559
			[ReadOnly]
			public NativeArray<float3> positions;

			// Token: 0x04000618 RID: 1560
			[ReadOnly]
			public NativeReference<AABB> aabb;

			// Token: 0x04000619 RID: 1561
			[WriteOnly]
			public NativeArray<float2> uvs;
		}

		// Token: 0x020000D5 RID: 213
		[BurstCompile]
		public struct AddIntDataCopyJob : IJobParallelFor
		{
			// Token: 0x0600033E RID: 830 RVA: 0x0001E620 File Offset: 0x0001C820
			public void Execute(int index)
			{
				int index2 = this.dstOffset + index;
				int num = this.srcData[index];
				num += this.addData;
				this.dstData[index2] = num;
			}

			// Token: 0x0400061A RID: 1562
			public int dstOffset;

			// Token: 0x0400061B RID: 1563
			public int addData;

			// Token: 0x0400061C RID: 1564
			[ReadOnly]
			public NativeArray<int> srcData;

			// Token: 0x0400061D RID: 1565
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<int> dstData;
		}

		// Token: 0x020000D6 RID: 214
		[BurstCompile]
		public struct AddInt2DataCopyJob : IJobParallelFor
		{
			// Token: 0x0600033F RID: 831 RVA: 0x0001E65C File Offset: 0x0001C85C
			public void Execute(int index)
			{
				int index2 = this.dstOffset + index;
				int2 @int = this.srcData[index];
				@int += this.addData;
				this.dstData[index2] = @int;
			}

			// Token: 0x0400061E RID: 1566
			public int dstOffset;

			// Token: 0x0400061F RID: 1567
			public int2 addData;

			// Token: 0x04000620 RID: 1568
			[ReadOnly]
			public NativeArray<int2> srcData;

			// Token: 0x04000621 RID: 1569
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<int2> dstData;
		}

		// Token: 0x020000D7 RID: 215
		[BurstCompile]
		public struct AddInt3DataCopyJob : IJobParallelFor
		{
			// Token: 0x06000340 RID: 832 RVA: 0x0001E69C File Offset: 0x0001C89C
			public void Execute(int index)
			{
				int index2 = this.dstOffset + index;
				int3 @int = this.srcData[index];
				@int += this.addData;
				this.dstData[index2] = @int;
			}

			// Token: 0x04000622 RID: 1570
			public int dstOffset;

			// Token: 0x04000623 RID: 1571
			public int3 addData;

			// Token: 0x04000624 RID: 1572
			[ReadOnly]
			public NativeArray<int3> srcData;

			// Token: 0x04000625 RID: 1573
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<int3> dstData;
		}

		// Token: 0x020000D8 RID: 216
		[BurstCompile]
		public struct TransformPositionJob : IJobParallelFor
		{
			// Token: 0x06000341 RID: 833 RVA: 0x0001E6DC File Offset: 0x0001C8DC
			public void Execute(int vindex)
			{
				float3 @float = this.positions[vindex];
				this.positions[vindex] = MathUtility.TransformPoint(@float, this.toM);
			}

			// Token: 0x04000626 RID: 1574
			public float4x4 toM;

			// Token: 0x04000627 RID: 1575
			public NativeArray<float3> positions;
		}

		// Token: 0x020000D9 RID: 217
		[BurstCompile]
		public struct TransformPositionJob2 : IJobParallelFor
		{
			// Token: 0x06000342 RID: 834 RVA: 0x0001E710 File Offset: 0x0001C910
			public void Execute(int vindex)
			{
				float3 @float = this.srcPositions[vindex];
				this.dstPositions[vindex] = MathUtility.TransformPoint(@float, this.toM);
			}

			// Token: 0x04000628 RID: 1576
			public float4x4 toM;

			// Token: 0x04000629 RID: 1577
			[ReadOnly]
			public NativeArray<float3> srcPositions;

			// Token: 0x0400062A RID: 1578
			[WriteOnly]
			public NativeArray<float3> dstPositions;
		}

		// Token: 0x020000DA RID: 218
		[BurstCompile]
		private struct ConvertArrayToMapJob<[IsUnmanaged] TData> : IJob where TData : struct, ValueType
		{
			// Token: 0x06000343 RID: 835 RVA: 0x0001E744 File Offset: 0x0001C944
			public void Execute()
			{
				int length = this.indexArray.Length;
				for (int i = 0; i < length; i++)
				{
					int num;
					int num2;
					DataUtility.Unpack10_22(this.indexArray[i], out num, out num2);
					for (int j = 0; j < num; j++)
					{
						TData item = this.dataArray[num2 + j];
						this.map.Add(i, item);
					}
				}
			}

			// Token: 0x0400062B RID: 1579
			[ReadOnly]
			public NativeArray<uint> indexArray;

			// Token: 0x0400062C RID: 1580
			[ReadOnly]
			public NativeArray<TData> dataArray;

			// Token: 0x0400062D RID: 1581
			[WriteOnly]
			public NativeParallelMultiHashMap<int, TData> map;
		}

		// Token: 0x020000DB RID: 219
		[BurstCompile]
		private struct ClearReferenceJob : IJob
		{
			// Token: 0x06000344 RID: 836 RVA: 0x0001E7AD File Offset: 0x0001C9AD
			public void Execute()
			{
				this.reference.Value = 0;
			}

			// Token: 0x0400062E RID: 1582
			public NativeReference<int> reference;
		}
	}
}
