using System;

namespace System.Xml
{
	// Token: 0x02000087 RID: 135
	internal static class MimeGlobals
	{
		// Token: 0x06000735 RID: 1845 RVA: 0x0001F2D4 File Offset: 0x0001D4D4
		// Note: this type is marked as 'beforefieldinit'.
		static MimeGlobals()
		{
		}

		// Token: 0x0400034E RID: 846
		internal static string MimeVersionHeader = "MIME-Version";

		// Token: 0x0400034F RID: 847
		internal static string DefaultVersion = "1.0";

		// Token: 0x04000350 RID: 848
		internal static string ContentIDScheme = "cid:";

		// Token: 0x04000351 RID: 849
		internal static string ContentIDHeader = "Content-ID";

		// Token: 0x04000352 RID: 850
		internal static string ContentTypeHeader = "Content-Type";

		// Token: 0x04000353 RID: 851
		internal static string ContentTransferEncodingHeader = "Content-Transfer-Encoding";

		// Token: 0x04000354 RID: 852
		internal static string EncodingBinary = "binary";

		// Token: 0x04000355 RID: 853
		internal static string Encoding8bit = "8bit";

		// Token: 0x04000356 RID: 854
		internal static byte[] COLONSPACE = new byte[]
		{
			58,
			32
		};

		// Token: 0x04000357 RID: 855
		internal static byte[] DASHDASH = new byte[]
		{
			45,
			45
		};

		// Token: 0x04000358 RID: 856
		internal static byte[] CRLF = new byte[]
		{
			13,
			10
		};

		// Token: 0x04000359 RID: 857
		internal static byte[] BoundaryPrefix = new byte[]
		{
			13,
			10,
			45,
			45
		};
	}
}
