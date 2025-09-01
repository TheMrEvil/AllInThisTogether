using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x020000AB RID: 171
	public static class NativeSliceUnsafeUtility
	{
		// Token: 0x060002EC RID: 748 RVA: 0x0000577C File Offset: 0x0000397C
		public unsafe static NativeSlice<T> ConvertExistingDataToNativeSlice<T>(void* dataPointer, int stride, int length) where T : struct
		{
			bool flag = length < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid length of '{0}'. It must be greater than 0.", length), "length");
			}
			bool flag2 = stride < 0;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Invalid stride '{0}'. It must be greater than 0.", stride), "stride");
			}
			return new NativeSlice<T>
			{
				m_Stride = stride,
				m_Buffer = (byte*)dataPointer,
				m_Length = length
			};
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000057FC File Offset: 0x000039FC
		public unsafe static void* GetUnsafePtr<T>(this NativeSlice<T> nativeSlice) where T : struct
		{
			return (void*)nativeSlice.m_Buffer;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00005814 File Offset: 0x00003A14
		public unsafe static void* GetUnsafeReadOnlyPtr<T>(this NativeSlice<T> nativeSlice) where T : struct
		{
			return (void*)nativeSlice.m_Buffer;
		}
	}
}
