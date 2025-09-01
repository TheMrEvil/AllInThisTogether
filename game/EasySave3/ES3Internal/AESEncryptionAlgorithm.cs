using System;
using System.IO;
using System.Security.Cryptography;

namespace ES3Internal
{
	// Token: 0x020000D2 RID: 210
	public class AESEncryptionAlgorithm : EncryptionAlgorithm
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x0001AE80 File Offset: 0x00019080
		public override byte[] Encrypt(byte[] bytes, string password, int bufferSize)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					this.Encrypt(memoryStream, memoryStream2, password, bufferSize);
					result = memoryStream2.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001AEE0 File Offset: 0x000190E0
		public override byte[] Decrypt(byte[] bytes, string password, int bufferSize)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					this.Decrypt(memoryStream, memoryStream2, password, bufferSize);
					result = memoryStream2.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001AF40 File Offset: 0x00019140
		public override void Encrypt(Stream input, Stream output, string password, int bufferSize)
		{
			input.Position = 0L;
			using (Aes aes = Aes.Create())
			{
				aes.Mode = CipherMode.CBC;
				aes.Padding = PaddingMode.PKCS7;
				aes.GenerateIV();
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, aes.IV, 100);
				aes.Key = rfc2898DeriveBytes.GetBytes(16);
				output.Write(aes.IV, 0, 16);
				using (ICryptoTransform cryptoTransform = aes.CreateEncryptor())
				{
					using (CryptoStream cryptoStream = new CryptoStream(output, cryptoTransform, CryptoStreamMode.Write))
					{
						EncryptionAlgorithm.CopyStream(input, cryptoStream, bufferSize);
					}
				}
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001AFFC File Offset: 0x000191FC
		public override void Decrypt(Stream input, Stream output, string password, int bufferSize)
		{
			using (Aes aes = Aes.Create())
			{
				byte[] array = new byte[16];
				input.Read(array, 0, 16);
				aes.IV = array;
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, aes.IV, 100);
				aes.Key = rfc2898DeriveBytes.GetBytes(16);
				using (ICryptoTransform cryptoTransform = aes.CreateDecryptor())
				{
					using (CryptoStream cryptoStream = new CryptoStream(input, cryptoTransform, CryptoStreamMode.Read))
					{
						EncryptionAlgorithm.CopyStream(cryptoStream, output, bufferSize);
					}
				}
			}
			output.Position = 0L;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001B0B4 File Offset: 0x000192B4
		public AESEncryptionAlgorithm()
		{
		}

		// Token: 0x04000126 RID: 294
		private const int ivSize = 16;

		// Token: 0x04000127 RID: 295
		private const int keySize = 16;

		// Token: 0x04000128 RID: 296
		private const int pwIterations = 100;
	}
}
