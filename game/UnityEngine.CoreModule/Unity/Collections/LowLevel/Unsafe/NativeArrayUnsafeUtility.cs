using System;
using System.Diagnostics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x020000AA RID: 170
	public static class NativeArrayUnsafeUtility
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x000056B8 File Offset: 0x000038B8
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckConvertArguments<T>(int length, Allocator allocator) where T : struct
		{
			bool flag = length < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("length", "Length must be >= 0");
			}
			NativeArray<T>.IsUnmanagedAndThrow();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000056E4 File Offset: 0x000038E4
		public unsafe static NativeArray<T> ConvertExistingDataToNativeArray<T>(void* dataPointer, int length, Allocator allocator) where T : struct
		{
			return new NativeArray<T>
			{
				m_Buffer = dataPointer,
				m_Length = length,
				m_AllocatorLabel = allocator
			};
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000571C File Offset: 0x0000391C
		public unsafe static void* GetUnsafePtr<T>(this NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00005734 File Offset: 0x00003934
		public unsafe static void* GetUnsafeReadOnlyPtr<T>(this NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000574C File Offset: 0x0000394C
		public unsafe static void* GetUnsafeReadOnlyPtr<T>(this NativeArray<T>.ReadOnly nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00005764 File Offset: 0x00003964
		public unsafe static void* GetUnsafeBufferPointerWithoutChecks<T>(NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}
	}
}
