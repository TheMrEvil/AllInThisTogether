using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200005A RID: 90
	internal static class SymmetricKeyWrap
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x0000CA04 File Offset: 0x0000AC04
		internal static byte[] TripleDESKeyWrapEncrypt(byte[] rgbKey, byte[] rgbWrappedKeyData)
		{
			byte[] src;
			using (SHA1 sha = SHA1.Create())
			{
				src = sha.ComputeHash(rgbWrappedKeyData);
			}
			byte[] array = new byte[8];
			using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
			{
				randomNumberGenerator.GetBytes(array);
			}
			byte[] array2 = new byte[rgbWrappedKeyData.Length + 8];
			TripleDES tripleDES = null;
			ICryptoTransform cryptoTransform = null;
			ICryptoTransform cryptoTransform2 = null;
			byte[] result;
			try
			{
				tripleDES = TripleDES.Create();
				tripleDES.Padding = PaddingMode.None;
				cryptoTransform = tripleDES.CreateEncryptor(rgbKey, array);
				cryptoTransform2 = tripleDES.CreateEncryptor(rgbKey, SymmetricKeyWrap.s_rgbTripleDES_KW_IV);
				Buffer.BlockCopy(rgbWrappedKeyData, 0, array2, 0, rgbWrappedKeyData.Length);
				Buffer.BlockCopy(src, 0, array2, rgbWrappedKeyData.Length, 8);
				byte[] array3 = cryptoTransform.TransformFinalBlock(array2, 0, array2.Length);
				byte[] array4 = new byte[array.Length + array3.Length];
				Buffer.BlockCopy(array, 0, array4, 0, array.Length);
				Buffer.BlockCopy(array3, 0, array4, array.Length, array3.Length);
				Array.Reverse<byte>(array4);
				result = cryptoTransform2.TransformFinalBlock(array4, 0, array4.Length);
			}
			finally
			{
				if (cryptoTransform2 != null)
				{
					cryptoTransform2.Dispose();
				}
				if (cryptoTransform != null)
				{
					cryptoTransform.Dispose();
				}
				if (tripleDES != null)
				{
					tripleDES.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000CB44 File Offset: 0x0000AD44
		internal static byte[] TripleDESKeyWrapDecrypt(byte[] rgbKey, byte[] rgbEncryptedWrappedKeyData)
		{
			if (rgbEncryptedWrappedKeyData.Length != 32 && rgbEncryptedWrappedKeyData.Length != 40 && rgbEncryptedWrappedKeyData.Length != 48)
			{
				throw new CryptographicException("The length of the encrypted data in Key Wrap is either 32, 40 or 48 bytes.");
			}
			TripleDES tripleDES = null;
			ICryptoTransform cryptoTransform = null;
			ICryptoTransform cryptoTransform2 = null;
			byte[] result;
			try
			{
				tripleDES = TripleDES.Create();
				tripleDES.Padding = PaddingMode.None;
				cryptoTransform = tripleDES.CreateDecryptor(rgbKey, SymmetricKeyWrap.s_rgbTripleDES_KW_IV);
				byte[] array = cryptoTransform.TransformFinalBlock(rgbEncryptedWrappedKeyData, 0, rgbEncryptedWrappedKeyData.Length);
				Array.Reverse<byte>(array);
				byte[] array2 = new byte[8];
				Buffer.BlockCopy(array, 0, array2, 0, 8);
				byte[] array3 = new byte[array.Length - array2.Length];
				Buffer.BlockCopy(array, 8, array3, 0, array3.Length);
				cryptoTransform2 = tripleDES.CreateDecryptor(rgbKey, array2);
				byte[] array4 = cryptoTransform2.TransformFinalBlock(array3, 0, array3.Length);
				byte[] array5 = new byte[array4.Length - 8];
				Buffer.BlockCopy(array4, 0, array5, 0, array5.Length);
				using (SHA1 sha = SHA1.Create())
				{
					byte[] array6 = sha.ComputeHash(array5);
					int i = array5.Length;
					int num = 0;
					while (i < array4.Length)
					{
						if (array4[i] != array6[num])
						{
							throw new CryptographicException("Bad wrapped key size.");
						}
						i++;
						num++;
					}
					result = array5;
				}
			}
			finally
			{
				if (cryptoTransform2 != null)
				{
					cryptoTransform2.Dispose();
				}
				if (cryptoTransform != null)
				{
					cryptoTransform.Dispose();
				}
				if (tripleDES != null)
				{
					tripleDES.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000CC98 File Offset: 0x0000AE98
		internal static byte[] AESKeyWrapEncrypt(byte[] rgbKey, byte[] rgbWrappedKeyData)
		{
			int num = rgbWrappedKeyData.Length >> 3;
			if (rgbWrappedKeyData.Length % 8 != 0 || num <= 0)
			{
				throw new CryptographicException("The length of the encrypted data in Key Wrap is either 32, 40 or 48 bytes.");
			}
			Aes aes = null;
			ICryptoTransform cryptoTransform = null;
			byte[] result;
			try
			{
				aes = Aes.Create();
				aes.Key = rgbKey;
				aes.Mode = CipherMode.ECB;
				aes.Padding = PaddingMode.None;
				cryptoTransform = aes.CreateEncryptor();
				if (num == 1)
				{
					byte[] array = new byte[SymmetricKeyWrap.s_rgbAES_KW_IV.Length + rgbWrappedKeyData.Length];
					Buffer.BlockCopy(SymmetricKeyWrap.s_rgbAES_KW_IV, 0, array, 0, SymmetricKeyWrap.s_rgbAES_KW_IV.Length);
					Buffer.BlockCopy(rgbWrappedKeyData, 0, array, SymmetricKeyWrap.s_rgbAES_KW_IV.Length, rgbWrappedKeyData.Length);
					result = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
				}
				else
				{
					byte[] array2 = new byte[num + 1 << 3];
					Buffer.BlockCopy(rgbWrappedKeyData, 0, array2, 8, rgbWrappedKeyData.Length);
					byte[] array3 = new byte[8];
					byte[] array4 = new byte[16];
					Buffer.BlockCopy(SymmetricKeyWrap.s_rgbAES_KW_IV, 0, array3, 0, 8);
					for (int i = 0; i <= 5; i++)
					{
						for (int j = 1; j <= num; j++)
						{
							long num2 = (long)(j + i * num);
							Buffer.BlockCopy(array3, 0, array4, 0, 8);
							Buffer.BlockCopy(array2, 8 * j, array4, 8, 8);
							byte[] array5 = cryptoTransform.TransformFinalBlock(array4, 0, 16);
							for (int k = 0; k < 8; k++)
							{
								byte b = (byte)(num2 >> 8 * (7 - k) & 255L);
								array3[k] = (b ^ array5[k]);
							}
							Buffer.BlockCopy(array5, 8, array2, 8 * j, 8);
						}
					}
					Buffer.BlockCopy(array3, 0, array2, 0, 8);
					result = array2;
				}
			}
			finally
			{
				if (cryptoTransform != null)
				{
					cryptoTransform.Dispose();
				}
				if (aes != null)
				{
					aes.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000CE54 File Offset: 0x0000B054
		internal static byte[] AESKeyWrapDecrypt(byte[] rgbKey, byte[] rgbEncryptedWrappedKeyData)
		{
			int num = (rgbEncryptedWrappedKeyData.Length >> 3) - 1;
			if (rgbEncryptedWrappedKeyData.Length % 8 != 0 || num <= 0)
			{
				throw new CryptographicException("The length of the encrypted data in Key Wrap is either 32, 40 or 48 bytes.");
			}
			byte[] array = new byte[num << 3];
			Aes aes = null;
			ICryptoTransform cryptoTransform = null;
			byte[] result;
			try
			{
				aes = Aes.Create();
				aes.Key = rgbKey;
				aes.Mode = CipherMode.ECB;
				aes.Padding = PaddingMode.None;
				cryptoTransform = aes.CreateDecryptor();
				if (num == 1)
				{
					byte[] array2 = cryptoTransform.TransformFinalBlock(rgbEncryptedWrappedKeyData, 0, rgbEncryptedWrappedKeyData.Length);
					for (int i = 0; i < 8; i++)
					{
						if (array2[i] != SymmetricKeyWrap.s_rgbAES_KW_IV[i])
						{
							throw new CryptographicException("Bad wrapped key size.");
						}
					}
					Buffer.BlockCopy(array2, 8, array, 0, 8);
					result = array;
				}
				else
				{
					Buffer.BlockCopy(rgbEncryptedWrappedKeyData, 8, array, 0, array.Length);
					byte[] array3 = new byte[8];
					byte[] array4 = new byte[16];
					Buffer.BlockCopy(rgbEncryptedWrappedKeyData, 0, array3, 0, 8);
					for (int j = 5; j >= 0; j--)
					{
						for (int k = num; k >= 1; k--)
						{
							long num2 = (long)(k + j * num);
							for (int l = 0; l < 8; l++)
							{
								byte b = (byte)(num2 >> 8 * (7 - l) & 255L);
								byte[] array5 = array3;
								int num3 = l;
								array5[num3] ^= b;
							}
							Buffer.BlockCopy(array3, 0, array4, 0, 8);
							Buffer.BlockCopy(array, 8 * (k - 1), array4, 8, 8);
							byte[] src = cryptoTransform.TransformFinalBlock(array4, 0, 16);
							Buffer.BlockCopy(src, 8, array, 8 * (k - 1), 8);
							Buffer.BlockCopy(src, 0, array3, 0, 8);
						}
					}
					for (int m = 0; m < 8; m++)
					{
						if (array3[m] != SymmetricKeyWrap.s_rgbAES_KW_IV[m])
						{
							throw new CryptographicException("Bad wrapped key size.");
						}
					}
					result = array;
				}
			}
			finally
			{
				if (cryptoTransform != null)
				{
					cryptoTransform.Dispose();
				}
				if (aes != null)
				{
					aes.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000D034 File Offset: 0x0000B234
		// Note: this type is marked as 'beforefieldinit'.
		static SymmetricKeyWrap()
		{
		}

		// Token: 0x04000223 RID: 547
		private static readonly byte[] s_rgbTripleDES_KW_IV = new byte[]
		{
			74,
			221,
			162,
			44,
			121,
			232,
			33,
			5
		};

		// Token: 0x04000224 RID: 548
		private static readonly byte[] s_rgbAES_KW_IV = new byte[]
		{
			166,
			166,
			166,
			166,
			166,
			166,
			166,
			166
		};
	}
}
