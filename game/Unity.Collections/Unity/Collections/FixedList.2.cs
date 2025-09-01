using System;
using System.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x02000053 RID: 83
	[BurstCompatible]
	internal struct FixedList
	{
		// Token: 0x060001BF RID: 447 RVA: 0x0000526A File Offset: 0x0000346A
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		internal static int PaddingBytes<T>() where T : struct
		{
			return math.max(0, math.min(6, (1 << math.tzcnt(UnsafeUtility.SizeOf<T>())) - 2));
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00005289 File Offset: 0x00003489
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		internal static int StorageBytes<BUFFER, T>() where BUFFER : struct where T : struct
		{
			return UnsafeUtility.SizeOf<BUFFER>() - FixedList.PaddingBytes<T>();
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00005296 File Offset: 0x00003496
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		internal static int Capacity<BUFFER, T>() where BUFFER : struct where T : struct
		{
			return FixedList.StorageBytes<BUFFER, T>() / UnsafeUtility.SizeOf<T>();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000052A4 File Offset: 0x000034A4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckResize<BUFFER, T>(int newLength) where BUFFER : struct where T : struct
		{
			int num = FixedList.Capacity<BUFFER, T>();
			if (newLength < 0 || newLength > num)
			{
				throw new IndexOutOfRangeException(string.Format("NewLength {0} is out of range of '{1}' Capacity.", newLength, num));
			}
		}
	}
}
