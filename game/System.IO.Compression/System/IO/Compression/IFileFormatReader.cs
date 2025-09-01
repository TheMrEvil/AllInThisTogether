using System;

namespace System.IO.Compression
{
	// Token: 0x0200001E RID: 30
	internal interface IFileFormatReader
	{
		// Token: 0x060000BF RID: 191
		bool ReadHeader(InputBuffer input);

		// Token: 0x060000C0 RID: 192
		bool ReadFooter(InputBuffer input);

		// Token: 0x060000C1 RID: 193
		void UpdateWithBytesRead(byte[] buffer, int offset, int bytesToCopy);

		// Token: 0x060000C2 RID: 194
		void Validate();
	}
}
