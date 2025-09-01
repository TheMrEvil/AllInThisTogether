using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000AF RID: 175
	[NativeContainer]
	[DebuggerDisplay("Length = {Length}, IsCreated = {IsCreated}")]
	[BurstCompatible]
	public struct NativeBitArray : INativeDisposable, IDisposable
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x00015896 File Offset: 0x00013A96
		public NativeBitArray(int numBits, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
		{
			this = new NativeBitArray(numBits, allocator, options, 2);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000158A2 File Offset: 0x00013AA2
		private NativeBitArray(int numBits, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options, int disposeSentinelStackDepth)
		{
			this.m_BitArray = new UnsafeBitArray(numBits, allocator, options);
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x000158B2 File Offset: 0x00013AB2
		public bool IsCreated
		{
			get
			{
				return this.m_BitArray.IsCreated;
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x000158BF File Offset: 0x00013ABF
		public void Dispose()
		{
			this.m_BitArray.Dispose();
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x000158CC File Offset: 0x00013ACC
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return this.m_BitArray.Dispose(inputDeps);
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x000158DA File Offset: 0x00013ADA
		public int Length
		{
			get
			{
				return CollectionHelper.AssumePositive(this.m_BitArray.Length);
			}
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000158EC File Offset: 0x00013AEC
		public void Clear()
		{
			this.m_BitArray.Clear();
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000158FC File Offset: 0x00013AFC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe NativeArray<T> AsNativeArray<[IsUnmanaged] T>() where T : struct, ValueType
		{
			int num = UnsafeUtility.SizeOf<T>() * 8;
			int length = this.m_BitArray.Length / num;
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.m_BitArray.Ptr, length, Allocator.None);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00015931 File Offset: 0x00013B31
		public void Set(int pos, bool value)
		{
			this.m_BitArray.Set(pos, value);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00015940 File Offset: 0x00013B40
		public void SetBits(int pos, bool value, int numBits)
		{
			this.m_BitArray.SetBits(pos, value, numBits);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00015950 File Offset: 0x00013B50
		public void SetBits(int pos, ulong value, int numBits = 1)
		{
			this.m_BitArray.SetBits(pos, value, numBits);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00015960 File Offset: 0x00013B60
		public ulong GetBits(int pos, int numBits = 1)
		{
			return this.m_BitArray.GetBits(pos, numBits);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001596F File Offset: 0x00013B6F
		public bool IsSet(int pos)
		{
			return this.m_BitArray.IsSet(pos);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001597D File Offset: 0x00013B7D
		public void Copy(int dstPos, int srcPos, int numBits)
		{
			this.m_BitArray.Copy(dstPos, srcPos, numBits);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001598D File Offset: 0x00013B8D
		public void Copy(int dstPos, ref NativeBitArray srcBitArray, int srcPos, int numBits)
		{
			this.m_BitArray.Copy(dstPos, ref srcBitArray.m_BitArray, srcPos, numBits);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x000159A4 File Offset: 0x00013BA4
		public int Find(int pos, int numBits)
		{
			return this.m_BitArray.Find(pos, numBits);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x000159B3 File Offset: 0x00013BB3
		public int Find(int pos, int count, int numBits)
		{
			return this.m_BitArray.Find(pos, count, numBits);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x000159C3 File Offset: 0x00013BC3
		public bool TestNone(int pos, int numBits = 1)
		{
			return this.m_BitArray.TestNone(pos, numBits);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x000159D2 File Offset: 0x00013BD2
		public bool TestAny(int pos, int numBits = 1)
		{
			return this.m_BitArray.TestAny(pos, numBits);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000159E1 File Offset: 0x00013BE1
		public bool TestAll(int pos, int numBits = 1)
		{
			return this.m_BitArray.TestAll(pos, numBits);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x000159F0 File Offset: 0x00013BF0
		public int CountBits(int pos, int numBits = 1)
		{
			return this.m_BitArray.CountBits(pos, numBits);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckRead()
		{
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00015A00 File Offset: 0x00013C00
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReadBounds<[IsUnmanaged] T>() where T : struct, ValueType
		{
			int num = UnsafeUtility.SizeOf<T>() * 8;
			int num2 = this.m_BitArray.Length / num;
			if (num2 == 0)
			{
				throw new InvalidOperationException(string.Format("Number of bits in the NativeBitArray {0} is not sufficient to cast to NativeArray<T> {1}.", this.m_BitArray.Length, UnsafeUtility.SizeOf<T>() * 8));
			}
			if (this.m_BitArray.Length != num * num2)
			{
				throw new InvalidOperationException(string.Format("Number of bits in the NativeBitArray {0} couldn't hold multiple of T {1}. Output array would be truncated.", this.m_BitArray.Length, UnsafeUtility.SizeOf<T>()));
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWrite()
		{
		}

		// Token: 0x0400027E RID: 638
		[NativeDisableUnsafePtrRestriction]
		internal UnsafeBitArray m_BitArray;
	}
}
