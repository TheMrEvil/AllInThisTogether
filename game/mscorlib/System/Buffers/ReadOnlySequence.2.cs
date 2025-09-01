using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000AE9 RID: 2793
	internal static class ReadOnlySequence
	{
		// Token: 0x06006359 RID: 25433 RVA: 0x0014C949 File Offset: 0x0014AB49
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SegmentToSequenceStart(int startIndex)
		{
			return startIndex | 0;
		}

		// Token: 0x0600635A RID: 25434 RVA: 0x0014C949 File Offset: 0x0014AB49
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SegmentToSequenceEnd(int endIndex)
		{
			return endIndex | 0;
		}

		// Token: 0x0600635B RID: 25435 RVA: 0x0014C949 File Offset: 0x0014AB49
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ArrayToSequenceStart(int startIndex)
		{
			return startIndex | 0;
		}

		// Token: 0x0600635C RID: 25436 RVA: 0x0014C94E File Offset: 0x0014AB4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ArrayToSequenceEnd(int endIndex)
		{
			return endIndex | int.MinValue;
		}

		// Token: 0x0600635D RID: 25437 RVA: 0x0014C94E File Offset: 0x0014AB4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int MemoryManagerToSequenceStart(int startIndex)
		{
			return startIndex | int.MinValue;
		}

		// Token: 0x0600635E RID: 25438 RVA: 0x0014C949 File Offset: 0x0014AB49
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int MemoryManagerToSequenceEnd(int endIndex)
		{
			return endIndex | 0;
		}

		// Token: 0x0600635F RID: 25439 RVA: 0x0014C94E File Offset: 0x0014AB4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int StringToSequenceStart(int startIndex)
		{
			return startIndex | int.MinValue;
		}

		// Token: 0x06006360 RID: 25440 RVA: 0x0014C94E File Offset: 0x0014AB4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int StringToSequenceEnd(int endIndex)
		{
			return endIndex | int.MinValue;
		}

		// Token: 0x04003A71 RID: 14961
		public const int FlagBitMask = -2147483648;

		// Token: 0x04003A72 RID: 14962
		public const int IndexBitMask = 2147483647;

		// Token: 0x04003A73 RID: 14963
		public const int SegmentStartMask = 0;

		// Token: 0x04003A74 RID: 14964
		public const int SegmentEndMask = 0;

		// Token: 0x04003A75 RID: 14965
		public const int ArrayStartMask = 0;

		// Token: 0x04003A76 RID: 14966
		public const int ArrayEndMask = -2147483648;

		// Token: 0x04003A77 RID: 14967
		public const int MemoryManagerStartMask = -2147483648;

		// Token: 0x04003A78 RID: 14968
		public const int MemoryManagerEndMask = 0;

		// Token: 0x04003A79 RID: 14969
		public const int StringStartMask = -2147483648;

		// Token: 0x04003A7A RID: 14970
		public const int StringEndMask = -2147483648;
	}
}
