using System;

namespace Unity.Collections
{
	// Token: 0x02000099 RID: 153
	public static class NativeSliceExtensions
	{
		// Token: 0x060002AE RID: 686 RVA: 0x00005140 File Offset: 0x00003340
		public static NativeSlice<T> Slice<T>(this NativeArray<T> thisArray) where T : struct
		{
			return new NativeSlice<T>(thisArray);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00005158 File Offset: 0x00003358
		public static NativeSlice<T> Slice<T>(this NativeArray<T> thisArray, int start) where T : struct
		{
			return new NativeSlice<T>(thisArray, start);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00005174 File Offset: 0x00003374
		public static NativeSlice<T> Slice<T>(this NativeArray<T> thisArray, int start, int length) where T : struct
		{
			return new NativeSlice<T>(thisArray, start, length);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00005190 File Offset: 0x00003390
		public static NativeSlice<T> Slice<T>(this NativeSlice<T> thisSlice) where T : struct
		{
			return thisSlice;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000051A4 File Offset: 0x000033A4
		public static NativeSlice<T> Slice<T>(this NativeSlice<T> thisSlice, int start) where T : struct
		{
			return new NativeSlice<T>(thisSlice, start);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000051C0 File Offset: 0x000033C0
		public static NativeSlice<T> Slice<T>(this NativeSlice<T> thisSlice, int start, int length) where T : struct
		{
			return new NativeSlice<T>(thisSlice, start, length);
		}
	}
}
