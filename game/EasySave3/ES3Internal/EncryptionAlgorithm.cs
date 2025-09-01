using System;
using System.IO;

namespace ES3Internal
{
	// Token: 0x020000D1 RID: 209
	public abstract class EncryptionAlgorithm
	{
		// Token: 0x0600041D RID: 1053
		public abstract byte[] Encrypt(byte[] bytes, string password, int bufferSize);

		// Token: 0x0600041E RID: 1054
		public abstract byte[] Decrypt(byte[] bytes, string password, int bufferSize);

		// Token: 0x0600041F RID: 1055
		public abstract void Encrypt(Stream input, Stream output, string password, int bufferSize);

		// Token: 0x06000420 RID: 1056
		public abstract void Decrypt(Stream input, Stream output, string password, int bufferSize);

		// Token: 0x06000421 RID: 1057 RVA: 0x0001AE48 File Offset: 0x00019048
		protected static void CopyStream(Stream input, Stream output, int bufferSize)
		{
			byte[] buffer = new byte[bufferSize];
			int count;
			while ((count = input.Read(buffer, 0, bufferSize)) > 0)
			{
				output.Write(buffer, 0, count);
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001AE75 File Offset: 0x00019075
		protected EncryptionAlgorithm()
		{
		}
	}
}
