using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000154 RID: 340
	internal static class UnsafeTextExtensions
	{
		// Token: 0x06000C08 RID: 3080 RVA: 0x00024073 File Offset: 0x00022273
		public static ref UnsafeList<byte> AsUnsafeListOfBytes(this UnsafeText text)
		{
			return UnsafeUtility.As<UntypedUnsafeList, UnsafeList<byte>>(ref text.m_UntypedListData);
		}
	}
}
