using System;

namespace System.Net
{
	// Token: 0x020005DE RID: 1502
	internal static class NclConstants
	{
		// Token: 0x06003047 RID: 12359 RVA: 0x000A67B4 File Offset: 0x000A49B4
		// Note: this type is marked as 'beforefieldinit'.
		static NclConstants()
		{
		}

		// Token: 0x04001B02 RID: 6914
		internal static readonly object Sentinel = new object();

		// Token: 0x04001B03 RID: 6915
		internal static readonly object[] EmptyObjectArray = new object[0];

		// Token: 0x04001B04 RID: 6916
		internal static readonly Uri[] EmptyUriArray = new Uri[0];

		// Token: 0x04001B05 RID: 6917
		internal static readonly byte[] CRLF = new byte[]
		{
			13,
			10
		};

		// Token: 0x04001B06 RID: 6918
		internal static readonly byte[] ChunkTerminator = new byte[]
		{
			48,
			13,
			10,
			13,
			10
		};
	}
}
