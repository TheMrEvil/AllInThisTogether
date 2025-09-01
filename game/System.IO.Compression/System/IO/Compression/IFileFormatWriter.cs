using System;

namespace System.IO.Compression
{
	// Token: 0x0200001D RID: 29
	internal interface IFileFormatWriter
	{
		// Token: 0x060000BC RID: 188
		byte[] GetHeader();

		// Token: 0x060000BD RID: 189
		void UpdateWithBytesRead(byte[] buffer, int offset, int bytesToCopy);

		// Token: 0x060000BE RID: 190
		byte[] GetFooter();
	}
}
