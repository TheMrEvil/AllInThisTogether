using System;
using System.IO;

namespace System.Net.Mime
{
	// Token: 0x020007FF RID: 2047
	internal interface IEncodableStream
	{
		// Token: 0x06004155 RID: 16725
		int DecodeBytes(byte[] buffer, int offset, int count);

		// Token: 0x06004156 RID: 16726
		int EncodeBytes(byte[] buffer, int offset, int count);

		// Token: 0x06004157 RID: 16727
		string GetEncodedString();

		// Token: 0x06004158 RID: 16728
		Stream GetStream();
	}
}
