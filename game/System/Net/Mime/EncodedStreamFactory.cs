using System;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x020007FD RID: 2045
	internal class EncodedStreamFactory
	{
		// Token: 0x06004147 RID: 16711 RVA: 0x000E0F6E File Offset: 0x000DF16E
		internal IEncodableStream GetEncoder(TransferEncoding encoding, Stream stream)
		{
			if (encoding == TransferEncoding.Base64)
			{
				return new Base64Stream(stream, new Base64WriteStateInfo());
			}
			if (encoding == TransferEncoding.QuotedPrintable)
			{
				return new QuotedPrintableStream(stream, true);
			}
			if (encoding == TransferEncoding.SevenBit || encoding == TransferEncoding.EightBit)
			{
				return new EightBitStream(stream);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x000E0FA0 File Offset: 0x000DF1A0
		internal IEncodableStream GetEncoderForHeader(Encoding encoding, bool useBase64Encoding, int headerTextLength)
		{
			byte[] header = this.CreateHeader(encoding, useBase64Encoding);
			byte[] footer = this.CreateFooter();
			if (useBase64Encoding)
			{
				return new Base64Stream((Base64WriteStateInfo)new Base64WriteStateInfo(1024, header, footer, 70, headerTextLength));
			}
			return new QEncodedStream(new WriteStateInfoBase(1024, header, footer, 70, headerTextLength));
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x000E0FEE File Offset: 0x000DF1EE
		protected byte[] CreateHeader(Encoding encoding, bool useBase64Encoding)
		{
			return Encoding.ASCII.GetBytes("=?" + encoding.HeaderName + "?" + (useBase64Encoding ? "B?" : "Q?"));
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x000E101E File Offset: 0x000DF21E
		protected byte[] CreateFooter()
		{
			return new byte[]
			{
				63,
				61
			};
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x0000219B File Offset: 0x0000039B
		public EncodedStreamFactory()
		{
		}

		// Token: 0x0400279A RID: 10138
		internal const int DefaultMaxLineLength = 70;

		// Token: 0x0400279B RID: 10139
		private const int InitialBufferSize = 1024;
	}
}
