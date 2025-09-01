using System;
using System.IO;

namespace ES3Internal
{
	// Token: 0x020000D3 RID: 211
	public class UnbufferedCryptoStream : MemoryStream
	{
		// Token: 0x06000428 RID: 1064 RVA: 0x0001B0BC File Offset: 0x000192BC
		public UnbufferedCryptoStream(Stream stream, bool isReadStream, string password, int bufferSize, EncryptionAlgorithm alg)
		{
			this.stream = stream;
			this.isReadStream = isReadStream;
			this.password = password;
			this.bufferSize = bufferSize;
			this.alg = alg;
			if (isReadStream)
			{
				alg.Decrypt(stream, this, password, bufferSize);
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001B0F8 File Offset: 0x000192F8
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (!this.isReadStream)
			{
				this.alg.Encrypt(this, this.stream, this.password, this.bufferSize);
			}
			this.stream.Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x04000129 RID: 297
		private readonly Stream stream;

		// Token: 0x0400012A RID: 298
		private readonly bool isReadStream;

		// Token: 0x0400012B RID: 299
		private string password;

		// Token: 0x0400012C RID: 300
		private int bufferSize;

		// Token: 0x0400012D RID: 301
		private EncryptionAlgorithm alg;

		// Token: 0x0400012E RID: 302
		private bool disposed;
	}
}
