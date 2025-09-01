using System;
using System.Reflection;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	// Token: 0x020004C4 RID: 1220
	internal static class Utils
	{
		// Token: 0x060030B4 RID: 12468 RVA: 0x00004BF9 File Offset: 0x00002DF9
		static Utils()
		{
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x000B1307 File Offset: 0x000AF507
		internal static RNGCryptoServiceProvider StaticRandomNumberGenerator
		{
			get
			{
				if (Utils._rng == null)
				{
					Utils._rng = new RNGCryptoServiceProvider();
				}
				return Utils._rng;
			}
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000B1328 File Offset: 0x000AF528
		internal static byte[] GenerateRandom(int keySize)
		{
			byte[] array = new byte[keySize];
			Utils.StaticRandomNumberGenerator.GetBytes(array);
			return array;
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x000040F7 File Offset: 0x000022F7
		[SecurityCritical]
		internal static bool HasAlgorithm(int dwCalg, int dwKeySize)
		{
			return true;
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x000B1348 File Offset: 0x000AF548
		internal static string DiscardWhiteSpaces(string inputBuffer)
		{
			return Utils.DiscardWhiteSpaces(inputBuffer, 0, inputBuffer.Length);
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x000B1358 File Offset: 0x000AF558
		internal static string DiscardWhiteSpaces(string inputBuffer, int inputOffset, int inputCount)
		{
			int num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (char.IsWhiteSpace(inputBuffer[inputOffset + i]))
				{
					num++;
				}
			}
			char[] array = new char[inputCount - num];
			num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (!char.IsWhiteSpace(inputBuffer[inputOffset + i]))
				{
					array[num++] = inputBuffer[inputOffset + i];
				}
			}
			return new string(array);
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000B13C4 File Offset: 0x000AF5C4
		internal static int ConvertByteArrayToInt(byte[] input)
		{
			int num = 0;
			for (int i = 0; i < input.Length; i++)
			{
				num *= 256;
				num += (int)input[i];
			}
			return num;
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000B13F0 File Offset: 0x000AF5F0
		internal static byte[] ConvertIntToByteArray(int dwInput)
		{
			byte[] array = new byte[8];
			int num = 0;
			if (dwInput == 0)
			{
				return new byte[1];
			}
			int i = dwInput;
			while (i > 0)
			{
				int num2 = i % 256;
				array[num] = (byte)num2;
				i = (i - num2) / 256;
				num++;
			}
			byte[] array2 = new byte[num];
			for (int j = 0; j < num; j++)
			{
				array2[j] = array[num - j - 1];
			}
			return array2;
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000B145C File Offset: 0x000AF65C
		internal static void ConvertIntToByteArray(uint dwInput, ref byte[] counter)
		{
			uint num = dwInput;
			int num2 = 0;
			Array.Clear(counter, 0, counter.Length);
			if (dwInput == 0U)
			{
				return;
			}
			while (num > 0U)
			{
				uint num3 = num % 256U;
				counter[3 - num2] = (byte)num3;
				num = (num - num3) / 256U;
				num2++;
			}
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000B14A0 File Offset: 0x000AF6A0
		internal static byte[] FixupKeyParity(byte[] key)
		{
			byte[] array = new byte[key.Length];
			for (int i = 0; i < key.Length; i++)
			{
				array[i] = (key[i] & 254);
				byte b = (byte)((int)(array[i] & 15) ^ array[i] >> 4);
				byte b2 = (byte)((int)(b & 3) ^ b >> 2);
				if ((byte)((int)(b2 & 1) ^ b2 >> 1) == 0)
				{
					byte[] array2 = array;
					int num = i;
					array2[num] |= 1;
				}
			}
			return array;
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x000B1500 File Offset: 0x000AF700
		[SecurityCritical]
		internal unsafe static void DWORDFromLittleEndian(uint* x, int digits, byte* block)
		{
			int i = 0;
			int num = 0;
			while (i < digits)
			{
				x[i] = (uint)((int)block[num] | (int)block[num + 1] << 8 | (int)block[num + 2] << 16 | (int)block[num + 3] << 24);
				i++;
				num += 4;
			}
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x000B1548 File Offset: 0x000AF748
		internal static void DWORDToLittleEndian(byte[] block, uint[] x, int digits)
		{
			int i = 0;
			int num = 0;
			while (i < digits)
			{
				block[num] = (byte)(x[i] & 255U);
				block[num + 1] = (byte)(x[i] >> 8 & 255U);
				block[num + 2] = (byte)(x[i] >> 16 & 255U);
				block[num + 3] = (byte)(x[i] >> 24 & 255U);
				i++;
				num += 4;
			}
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000B15AC File Offset: 0x000AF7AC
		[SecurityCritical]
		internal unsafe static void DWORDFromBigEndian(uint* x, int digits, byte* block)
		{
			int i = 0;
			int num = 0;
			while (i < digits)
			{
				x[i] = (uint)((int)block[num] << 24 | (int)block[num + 1] << 16 | (int)block[num + 2] << 8 | (int)block[num + 3]);
				i++;
				num += 4;
			}
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x000B15F4 File Offset: 0x000AF7F4
		internal static void DWORDToBigEndian(byte[] block, uint[] x, int digits)
		{
			int i = 0;
			int num = 0;
			while (i < digits)
			{
				block[num] = (byte)(x[i] >> 24 & 255U);
				block[num + 1] = (byte)(x[i] >> 16 & 255U);
				block[num + 2] = (byte)(x[i] >> 8 & 255U);
				block[num + 3] = (byte)(x[i] & 255U);
				i++;
				num += 4;
			}
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x000B1658 File Offset: 0x000AF858
		[SecurityCritical]
		internal unsafe static void QuadWordFromBigEndian(ulong* x, int digits, byte* block)
		{
			int i = 0;
			int num = 0;
			while (i < digits)
			{
				x[i] = ((ulong)block[num] << 56 | (ulong)block[num + 1] << 48 | (ulong)block[num + 2] << 40 | (ulong)block[num + 3] << 32 | (ulong)block[num + 4] << 24 | (ulong)block[num + 5] << 16 | (ulong)block[num + 6] << 8 | (ulong)block[num + 7]);
				i++;
				num += 8;
			}
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x000B16D0 File Offset: 0x000AF8D0
		internal static void QuadWordToBigEndian(byte[] block, ulong[] x, int digits)
		{
			int i = 0;
			int num = 0;
			while (i < digits)
			{
				block[num] = (byte)(x[i] >> 56 & 255UL);
				block[num + 1] = (byte)(x[i] >> 48 & 255UL);
				block[num + 2] = (byte)(x[i] >> 40 & 255UL);
				block[num + 3] = (byte)(x[i] >> 32 & 255UL);
				block[num + 4] = (byte)(x[i] >> 24 & 255UL);
				block[num + 5] = (byte)(x[i] >> 16 & 255UL);
				block[num + 6] = (byte)(x[i] >> 8 & 255UL);
				block[num + 7] = (byte)(x[i] & 255UL);
				i++;
				num += 8;
			}
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x000B1787 File Offset: 0x000AF987
		internal static byte[] Int(uint i)
		{
			return new byte[]
			{
				(byte)(i >> 24),
				(byte)(i >> 16),
				(byte)(i >> 8),
				(byte)i
			};
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000B17AB File Offset: 0x000AF9AB
		[SecurityCritical]
		internal static byte[] RsaOaepEncrypt(RSA rsa, HashAlgorithm hash, PKCS1MaskGenerationMethod mgf, RandomNumberGenerator rng, byte[] data)
		{
			return PKCS1.Encrypt_OAEP(rsa, hash, rng, data);
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000B17B7 File Offset: 0x000AF9B7
		[SecurityCritical]
		internal static byte[] RsaOaepDecrypt(RSA rsa, HashAlgorithm hash, PKCS1MaskGenerationMethod mgf, byte[] encryptedData)
		{
			byte[] array = PKCS1.Decrypt_OAEP(rsa, hash, encryptedData);
			if (array == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Error occurred while decoding OAEP padding."));
			}
			return array;
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x000B17D4 File Offset: 0x000AF9D4
		[SecurityCritical]
		internal static byte[] RsaPkcs1Padding(RSA rsa, byte[] oid, byte[] hash)
		{
			int num = rsa.KeySize / 8;
			byte[] array = new byte[num];
			byte[] array2 = new byte[oid.Length + 8 + hash.Length];
			array2[0] = 48;
			int num2 = array2.Length - 2;
			array2[1] = (byte)num2;
			array2[2] = 48;
			num2 = oid.Length + 2;
			array2[3] = (byte)num2;
			Buffer.InternalBlockCopy(oid, 0, array2, 4, oid.Length);
			array2[4 + oid.Length] = 5;
			array2[4 + oid.Length + 1] = 0;
			array2[4 + oid.Length + 2] = 4;
			array2[4 + oid.Length + 3] = (byte)hash.Length;
			Buffer.InternalBlockCopy(hash, 0, array2, oid.Length + 8, hash.Length);
			int num3 = num - array2.Length;
			if (num3 <= 2)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Object identifier (OID) is unknown."));
			}
			array[0] = 0;
			array[1] = 1;
			for (int i = 2; i < num3 - 1; i++)
			{
				array[i] = byte.MaxValue;
			}
			array[num3 - 1] = 0;
			Buffer.InternalBlockCopy(array2, 0, array, num3, array2.Length);
			return array;
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x000B18B8 File Offset: 0x000AFAB8
		internal static bool CompareBigIntArrays(byte[] lhs, byte[] rhs)
		{
			if (lhs == null)
			{
				return rhs == null;
			}
			int i = 0;
			int num = 0;
			while (i < lhs.Length)
			{
				if (lhs[i] != 0)
				{
					break;
				}
				i++;
			}
			while (num < rhs.Length && rhs[num] == 0)
			{
				num++;
			}
			int num2 = lhs.Length - i;
			if (rhs.Length - num != num2)
			{
				return false;
			}
			for (int j = 0; j < num2; j++)
			{
				if (lhs[i + j] != rhs[num + j])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x000B1920 File Offset: 0x000AFB20
		internal static HashAlgorithmName OidToHashAlgorithmName(string oid)
		{
			if (oid == "1.3.14.3.2.26")
			{
				return HashAlgorithmName.SHA1;
			}
			if (oid == "2.16.840.1.101.3.4.2.1")
			{
				return HashAlgorithmName.SHA256;
			}
			if (oid == "2.16.840.1.101.3.4.2.2")
			{
				return HashAlgorithmName.SHA384;
			}
			if (!(oid == "2.16.840.1.101.3.4.2.3"))
			{
				throw new NotSupportedException();
			}
			return HashAlgorithmName.SHA512;
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x000B1980 File Offset: 0x000AFB80
		internal static bool DoesRsaKeyOverride(RSA rsaKey, string methodName, Type[] parameterTypes)
		{
			Type type = rsaKey.GetType();
			return rsaKey is RSACryptoServiceProvider || type.FullName == "System.Security.Cryptography.RSACng" || Utils.DoesRsaKeyOverrideSlowPath(type, methodName, parameterTypes);
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x000B19BA File Offset: 0x000AFBBA
		private static bool DoesRsaKeyOverrideSlowPath(Type t, string methodName, Type[] parameterTypes)
		{
			return !(t.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public, null, parameterTypes, null).DeclaringType == typeof(RSA));
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000B19E1 File Offset: 0x000AFBE1
		internal static bool _ProduceLegacyHmacValues()
		{
			return Environment.GetEnvironmentVariable("legacyHMACMode") == "1";
		}

		// Token: 0x04002241 RID: 8769
		internal const int DefaultRsaProviderType = 1;

		// Token: 0x04002242 RID: 8770
		private static volatile RNGCryptoServiceProvider _rng;
	}
}
