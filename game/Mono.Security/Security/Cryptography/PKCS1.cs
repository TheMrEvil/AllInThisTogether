using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200005B RID: 91
	public sealed class PKCS1
	{
		// Token: 0x06000376 RID: 886 RVA: 0x00012194 File Offset: 0x00010394
		private PKCS1()
		{
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001219C File Offset: 0x0001039C
		private static bool Compare(byte[] array1, byte[] array2)
		{
			bool flag = array1.Length == array2.Length;
			if (flag)
			{
				for (int i = 0; i < array1.Length; i++)
				{
					if (array1[i] != array2[i])
					{
						return false;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000121D0 File Offset: 0x000103D0
		private static byte[] xor(byte[] array1, byte[] array2)
		{
			byte[] array3 = new byte[array1.Length];
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i] = (array1[i] ^ array2[i]);
			}
			return array3;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00012200 File Offset: 0x00010400
		private static byte[] GetEmptyHash(HashAlgorithm hash)
		{
			if (hash is SHA1)
			{
				return PKCS1.emptySHA1;
			}
			if (hash is SHA256)
			{
				return PKCS1.emptySHA256;
			}
			if (hash is SHA384)
			{
				return PKCS1.emptySHA384;
			}
			if (hash is SHA512)
			{
				return PKCS1.emptySHA512;
			}
			return hash.ComputeHash(null);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0001224C File Offset: 0x0001044C
		public static byte[] I2OSP(int x, int size)
		{
			byte[] bytes = BitConverterLE.GetBytes(x);
			Array.Reverse<byte>(bytes, 0, bytes.Length);
			return PKCS1.I2OSP(bytes, size);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00012274 File Offset: 0x00010474
		public static byte[] I2OSP(byte[] x, int size)
		{
			byte[] array = new byte[size];
			Buffer.BlockCopy(x, 0, array, array.Length - x.Length, x.Length);
			return array;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001229C File Offset: 0x0001049C
		public static byte[] OS2IP(byte[] x)
		{
			int num = 0;
			while (x[num++] == 0 && num < x.Length)
			{
			}
			num--;
			if (num > 0)
			{
				byte[] array = new byte[x.Length - num];
				Buffer.BlockCopy(x, num, array, 0, array.Length);
				return array;
			}
			return x;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000122DC File Offset: 0x000104DC
		public static byte[] RSAEP(RSA rsa, byte[] m)
		{
			return rsa.EncryptValue(m);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000122E5 File Offset: 0x000104E5
		public static byte[] RSADP(RSA rsa, byte[] c)
		{
			return rsa.DecryptValue(c);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000122EE File Offset: 0x000104EE
		public static byte[] RSASP1(RSA rsa, byte[] m)
		{
			return rsa.DecryptValue(m);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000122F7 File Offset: 0x000104F7
		public static byte[] RSAVP1(RSA rsa, byte[] s)
		{
			return rsa.EncryptValue(s);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00012300 File Offset: 0x00010500
		public static byte[] Encrypt_OAEP(RSA rsa, HashAlgorithm hash, RandomNumberGenerator rng, byte[] M)
		{
			int num = rsa.KeySize / 8;
			int num2 = hash.HashSize / 8;
			if (M.Length > num - 2 * num2 - 2)
			{
				throw new CryptographicException("message too long");
			}
			byte[] emptyHash = PKCS1.GetEmptyHash(hash);
			int num3 = num - M.Length - 2 * num2 - 2;
			byte[] array = new byte[emptyHash.Length + num3 + 1 + M.Length];
			Buffer.BlockCopy(emptyHash, 0, array, 0, emptyHash.Length);
			array[emptyHash.Length + num3] = 1;
			Buffer.BlockCopy(M, 0, array, array.Length - M.Length, M.Length);
			byte[] array2 = new byte[num2];
			rng.GetBytes(array2);
			byte[] array3 = PKCS1.MGF1(hash, array2, num - num2 - 1);
			byte[] array4 = PKCS1.xor(array, array3);
			byte[] array5 = PKCS1.MGF1(hash, array4, num2);
			byte[] array6 = PKCS1.xor(array2, array5);
			byte[] array7 = new byte[array6.Length + array4.Length + 1];
			Buffer.BlockCopy(array6, 0, array7, 1, array6.Length);
			Buffer.BlockCopy(array4, 0, array7, array6.Length + 1, array4.Length);
			byte[] m = PKCS1.OS2IP(array7);
			return PKCS1.I2OSP(PKCS1.RSAEP(rsa, m), num);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00012414 File Offset: 0x00010614
		public static byte[] Decrypt_OAEP(RSA rsa, HashAlgorithm hash, byte[] C)
		{
			int num = rsa.KeySize / 8;
			int num2 = hash.HashSize / 8;
			if (num < 2 * num2 + 2 || C.Length != num)
			{
				throw new CryptographicException("decryption error");
			}
			byte[] c = PKCS1.OS2IP(C);
			byte[] array = PKCS1.I2OSP(PKCS1.RSADP(rsa, c), num);
			byte[] array2 = new byte[num2];
			Buffer.BlockCopy(array, 1, array2, 0, array2.Length);
			byte[] array3 = new byte[num - num2 - 1];
			Buffer.BlockCopy(array, array.Length - array3.Length, array3, 0, array3.Length);
			byte[] array4 = PKCS1.MGF1(hash, array3, num2);
			byte[] mgfSeed = PKCS1.xor(array2, array4);
			byte[] array5 = PKCS1.MGF1(hash, mgfSeed, num - num2 - 1);
			byte[] array6 = PKCS1.xor(array3, array5);
			byte[] emptyHash = PKCS1.GetEmptyHash(hash);
			byte[] array7 = new byte[emptyHash.Length];
			Buffer.BlockCopy(array6, 0, array7, 0, array7.Length);
			bool flag = PKCS1.Compare(emptyHash, array7);
			int num3 = emptyHash.Length;
			while (array6[num3] == 0)
			{
				num3++;
			}
			int num4 = array6.Length - num3 - 1;
			byte[] array8 = new byte[num4];
			Buffer.BlockCopy(array6, num3 + 1, array8, 0, num4);
			if (array[0] != 0 || !flag || array6[num3] != 1)
			{
				return null;
			}
			return array8;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00012540 File Offset: 0x00010740
		public static byte[] Encrypt_v15(RSA rsa, RandomNumberGenerator rng, byte[] M)
		{
			int num = rsa.KeySize / 8;
			if (M.Length > num - 11)
			{
				throw new CryptographicException("message too long");
			}
			int num2 = Math.Max(8, num - M.Length - 3);
			byte[] array = new byte[num2];
			rng.GetNonZeroBytes(array);
			byte[] array2 = new byte[num];
			array2[1] = 2;
			Buffer.BlockCopy(array, 0, array2, 2, num2);
			Buffer.BlockCopy(M, 0, array2, num - M.Length, M.Length);
			byte[] m = PKCS1.OS2IP(array2);
			return PKCS1.I2OSP(PKCS1.RSAEP(rsa, m), num);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000125C4 File Offset: 0x000107C4
		public static byte[] Decrypt_v15(RSA rsa, byte[] C)
		{
			int num = rsa.KeySize >> 3;
			if (num < 11 || C.Length > num)
			{
				throw new CryptographicException("decryption error");
			}
			byte[] c = PKCS1.OS2IP(C);
			byte[] array = PKCS1.I2OSP(PKCS1.RSADP(rsa, c), num);
			if (array[0] != 0 || array[1] != 2)
			{
				return null;
			}
			int num2 = 10;
			while (array[num2] != 0 && num2 < array.Length)
			{
				num2++;
			}
			if (array[num2] != 0)
			{
				return null;
			}
			num2++;
			byte[] array2 = new byte[array.Length - num2];
			Buffer.BlockCopy(array, num2, array2, 0, array2.Length);
			return array2;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00012650 File Offset: 0x00010850
		public static byte[] Sign_v15(RSA rsa, HashAlgorithm hash, byte[] hashValue)
		{
			int num = rsa.KeySize >> 3;
			byte[] m = PKCS1.OS2IP(PKCS1.Encode_v15(hash, hashValue, num));
			return PKCS1.I2OSP(PKCS1.RSASP1(rsa, m), num);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00012684 File Offset: 0x00010884
		internal static byte[] Sign_v15(RSA rsa, string hashName, byte[] hashValue)
		{
			byte[] result;
			using (HashAlgorithm hashAlgorithm = PKCS1.CreateFromName(hashName))
			{
				result = PKCS1.Sign_v15(rsa, hashAlgorithm, hashValue);
			}
			return result;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000126C0 File Offset: 0x000108C0
		public static bool Verify_v15(RSA rsa, HashAlgorithm hash, byte[] hashValue, byte[] signature)
		{
			return PKCS1.Verify_v15(rsa, hash, hashValue, signature, false);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000126CC File Offset: 0x000108CC
		internal static bool Verify_v15(RSA rsa, string hashName, byte[] hashValue, byte[] signature)
		{
			bool result;
			using (HashAlgorithm hashAlgorithm = PKCS1.CreateFromName(hashName))
			{
				result = PKCS1.Verify_v15(rsa, hashAlgorithm, hashValue, signature, false);
			}
			return result;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00012708 File Offset: 0x00010908
		public static bool Verify_v15(RSA rsa, HashAlgorithm hash, byte[] hashValue, byte[] signature, bool tryNonStandardEncoding)
		{
			int num = rsa.KeySize >> 3;
			byte[] s = PKCS1.OS2IP(signature);
			byte[] array = PKCS1.I2OSP(PKCS1.RSAVP1(rsa, s), num);
			bool flag = PKCS1.Compare(PKCS1.Encode_v15(hash, hashValue, num), array);
			if (flag || !tryNonStandardEncoding)
			{
				return flag;
			}
			if (array[0] != 0 || array[1] != 1)
			{
				return false;
			}
			int i;
			for (i = 2; i < array.Length - hashValue.Length - 1; i++)
			{
				if (array[i] != 255)
				{
					return false;
				}
			}
			if (array[i++] != 0)
			{
				return false;
			}
			byte[] array2 = new byte[hashValue.Length];
			Buffer.BlockCopy(array, i, array2, 0, array2.Length);
			return PKCS1.Compare(array2, hashValue);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000127AC File Offset: 0x000109AC
		public static byte[] Encode_v15(HashAlgorithm hash, byte[] hashValue, int emLength)
		{
			if (hashValue.Length != hash.HashSize >> 3)
			{
				throw new CryptographicException("bad hash length for " + hash.ToString());
			}
			string text = CryptoConfig.MapNameToOID(hash.ToString());
			byte[] array;
			if (text != null)
			{
				ASN1 asn = new ASN1(48);
				asn.Add(new ASN1(CryptoConfig.EncodeOID(text)));
				asn.Add(new ASN1(5));
				ASN1 asn2 = new ASN1(4, hashValue);
				ASN1 asn3 = new ASN1(48);
				asn3.Add(asn);
				asn3.Add(asn2);
				array = asn3.GetBytes();
			}
			else
			{
				array = hashValue;
			}
			Buffer.BlockCopy(hashValue, 0, array, array.Length - hashValue.Length, hashValue.Length);
			int num = Math.Max(8, emLength - array.Length - 3);
			byte[] array2 = new byte[num + array.Length + 3];
			array2[1] = 1;
			for (int i = 2; i < num + 2; i++)
			{
				array2[i] = byte.MaxValue;
			}
			Buffer.BlockCopy(array, 0, array2, num + 3, array.Length);
			return array2;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000128A0 File Offset: 0x00010AA0
		public static byte[] MGF1(HashAlgorithm hash, byte[] mgfSeed, int maskLen)
		{
			if (maskLen < 0)
			{
				throw new OverflowException();
			}
			int num = mgfSeed.Length;
			int num2 = hash.HashSize >> 3;
			int num3 = maskLen / num2;
			if (maskLen % num2 != 0)
			{
				num3++;
			}
			byte[] array = new byte[num3 * num2];
			byte[] array2 = new byte[num + 4];
			int num4 = 0;
			for (int i = 0; i < num3; i++)
			{
				Array src = PKCS1.I2OSP(i, 4);
				Buffer.BlockCopy(mgfSeed, 0, array2, 0, num);
				Buffer.BlockCopy(src, 0, array2, num, 4);
				Buffer.BlockCopy(hash.ComputeHash(array2), 0, array, num4, num2);
				num4 += num2;
			}
			byte[] array3 = new byte[maskLen];
			Buffer.BlockCopy(array, 0, array3, 0, maskLen);
			return array3;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00012944 File Offset: 0x00010B44
		internal static string HashNameFromOid(string oid, bool throwOnError = true)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(oid);
			if (num <= 719034781U)
			{
				if (num <= 601591448U)
				{
					if (num != 510574318U)
					{
						if (num != 601591448U)
						{
							goto IL_173;
						}
						if (!(oid == "1.2.840.113549.1.1.5"))
						{
							goto IL_173;
						}
					}
					else if (!(oid == "1.2.840.10040.4.3"))
					{
						goto IL_173;
					}
				}
				else if (num != 618369067U)
				{
					if (num != 702257162U)
					{
						if (num != 719034781U)
						{
							goto IL_173;
						}
						if (!(oid == "1.2.840.113549.1.1.2"))
						{
							goto IL_173;
						}
						return "MD2";
					}
					else
					{
						if (!(oid == "1.2.840.113549.1.1.3"))
						{
							goto IL_173;
						}
						return "MD4";
					}
				}
				else
				{
					if (!(oid == "1.2.840.113549.1.1.4"))
					{
						goto IL_173;
					}
					return "MD5";
				}
			}
			else if (num <= 2477476687U)
			{
				if (num != 875536856U)
				{
					if (num != 2477476687U)
					{
						goto IL_173;
					}
					if (!(oid == "1.2.840.113549.1.1.11"))
					{
						goto IL_173;
					}
					return "SHA256";
				}
				else if (!(oid == "1.3.14.3.2.29"))
				{
					goto IL_173;
				}
			}
			else if (num != 2494254306U)
			{
				if (num != 2511031925U)
				{
					if (num != 3493391575U)
					{
						goto IL_173;
					}
					if (!(oid == "1.3.36.3.3.1.2"))
					{
						goto IL_173;
					}
					return "RIPEMD160";
				}
				else
				{
					if (!(oid == "1.2.840.113549.1.1.13"))
					{
						goto IL_173;
					}
					return "SHA512";
				}
			}
			else
			{
				if (!(oid == "1.2.840.113549.1.1.12"))
				{
					goto IL_173;
				}
				return "SHA384";
			}
			return "SHA1";
			IL_173:
			if (throwOnError)
			{
				throw new CryptographicException("Unsupported hash algorithm: " + oid);
			}
			return null;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00012AD9 File Offset: 0x00010CD9
		internal static HashAlgorithm CreateFromOid(string oid)
		{
			return PKCS1.CreateFromName(PKCS1.HashNameFromOid(oid, true));
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00012AE7 File Offset: 0x00010CE7
		internal static HashAlgorithm CreateFromName(string name)
		{
			return HashAlgorithm.Create(name);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00012AF0 File Offset: 0x00010CF0
		// Note: this type is marked as 'beforefieldinit'.
		static PKCS1()
		{
		}

		// Token: 0x040002D6 RID: 726
		private static byte[] emptySHA1 = new byte[]
		{
			218,
			57,
			163,
			238,
			94,
			107,
			75,
			13,
			50,
			85,
			191,
			239,
			149,
			96,
			24,
			144,
			175,
			216,
			7,
			9
		};

		// Token: 0x040002D7 RID: 727
		private static byte[] emptySHA256 = new byte[]
		{
			227,
			176,
			196,
			66,
			152,
			252,
			28,
			20,
			154,
			251,
			244,
			200,
			153,
			111,
			185,
			36,
			39,
			174,
			65,
			228,
			100,
			155,
			147,
			76,
			164,
			149,
			153,
			27,
			120,
			82,
			184,
			85
		};

		// Token: 0x040002D8 RID: 728
		private static byte[] emptySHA384 = new byte[]
		{
			56,
			176,
			96,
			167,
			81,
			172,
			150,
			56,
			76,
			217,
			50,
			126,
			177,
			177,
			227,
			106,
			33,
			253,
			183,
			17,
			20,
			190,
			7,
			67,
			76,
			12,
			199,
			191,
			99,
			246,
			225,
			218,
			39,
			78,
			222,
			191,
			231,
			111,
			101,
			251,
			213,
			26,
			210,
			241,
			72,
			152,
			185,
			91
		};

		// Token: 0x040002D9 RID: 729
		private static byte[] emptySHA512 = new byte[]
		{
			207,
			131,
			225,
			53,
			126,
			239,
			184,
			189,
			241,
			84,
			40,
			80,
			214,
			109,
			128,
			7,
			214,
			32,
			228,
			5,
			11,
			87,
			21,
			220,
			131,
			244,
			169,
			33,
			211,
			108,
			233,
			206,
			71,
			208,
			209,
			60,
			93,
			133,
			242,
			176,
			byte.MaxValue,
			131,
			24,
			210,
			135,
			126,
			236,
			47,
			99,
			185,
			49,
			189,
			71,
			65,
			122,
			129,
			165,
			56,
			50,
			122,
			249,
			39,
			218,
			62
		};
	}
}
