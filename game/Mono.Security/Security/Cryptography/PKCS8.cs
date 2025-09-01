using System;
using System.Collections;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200005C RID: 92
	public sealed class PKCS8
	{
		// Token: 0x06000390 RID: 912 RVA: 0x00012B59 File Offset: 0x00010D59
		private PKCS8()
		{
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00012B64 File Offset: 0x00010D64
		public static PKCS8.KeyInfo GetType(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			PKCS8.KeyInfo result = PKCS8.KeyInfo.Unknown;
			try
			{
				ASN1 asn = new ASN1(data);
				if (asn.Tag == 48 && asn.Count > 0)
				{
					byte tag = asn[0].Tag;
					if (tag != 2)
					{
						if (tag == 48)
						{
							result = PKCS8.KeyInfo.EncryptedPrivateKey;
						}
					}
					else
					{
						result = PKCS8.KeyInfo.PrivateKey;
					}
				}
			}
			catch
			{
				throw new CryptographicException("invalid ASN.1 data");
			}
			return result;
		}

		// Token: 0x0200009E RID: 158
		public enum KeyInfo
		{
			// Token: 0x040003F0 RID: 1008
			PrivateKey,
			// Token: 0x040003F1 RID: 1009
			EncryptedPrivateKey,
			// Token: 0x040003F2 RID: 1010
			Unknown
		}

		// Token: 0x0200009F RID: 159
		public class PrivateKeyInfo
		{
			// Token: 0x0600055D RID: 1373 RVA: 0x00019C8B File Offset: 0x00017E8B
			public PrivateKeyInfo()
			{
				this._version = 0;
				this._list = new ArrayList();
			}

			// Token: 0x0600055E RID: 1374 RVA: 0x00019CA5 File Offset: 0x00017EA5
			public PrivateKeyInfo(byte[] data) : this()
			{
				this.Decode(data);
			}

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x0600055F RID: 1375 RVA: 0x00019CB4 File Offset: 0x00017EB4
			// (set) Token: 0x06000560 RID: 1376 RVA: 0x00019CBC File Offset: 0x00017EBC
			public string Algorithm
			{
				get
				{
					return this._algorithm;
				}
				set
				{
					this._algorithm = value;
				}
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x06000561 RID: 1377 RVA: 0x00019CC5 File Offset: 0x00017EC5
			public ArrayList Attributes
			{
				get
				{
					return this._list;
				}
			}

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x06000562 RID: 1378 RVA: 0x00019CCD File Offset: 0x00017ECD
			// (set) Token: 0x06000563 RID: 1379 RVA: 0x00019CE9 File Offset: 0x00017EE9
			public byte[] PrivateKey
			{
				get
				{
					if (this._key == null)
					{
						return null;
					}
					return (byte[])this._key.Clone();
				}
				set
				{
					if (value == null)
					{
						throw new ArgumentNullException("PrivateKey");
					}
					this._key = (byte[])value.Clone();
				}
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x06000564 RID: 1380 RVA: 0x00019D0A File Offset: 0x00017F0A
			// (set) Token: 0x06000565 RID: 1381 RVA: 0x00019D12 File Offset: 0x00017F12
			public int Version
			{
				get
				{
					return this._version;
				}
				set
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("negative version");
					}
					this._version = value;
				}
			}

			// Token: 0x06000566 RID: 1382 RVA: 0x00019D2C File Offset: 0x00017F2C
			private void Decode(byte[] data)
			{
				ASN1 asn = new ASN1(data);
				if (asn.Tag != 48)
				{
					throw new CryptographicException("invalid PrivateKeyInfo");
				}
				ASN1 asn2 = asn[0];
				if (asn2.Tag != 2)
				{
					throw new CryptographicException("invalid version");
				}
				this._version = (int)asn2.Value[0];
				ASN1 asn3 = asn[1];
				if (asn3.Tag != 48)
				{
					throw new CryptographicException("invalid algorithm");
				}
				ASN1 asn4 = asn3[0];
				if (asn4.Tag != 6)
				{
					throw new CryptographicException("missing algorithm OID");
				}
				this._algorithm = ASN1Convert.ToOid(asn4);
				ASN1 asn5 = asn[2];
				this._key = asn5.Value;
				if (asn.Count > 3)
				{
					ASN1 asn6 = asn[3];
					for (int i = 0; i < asn6.Count; i++)
					{
						this._list.Add(asn6[i]);
					}
				}
			}

			// Token: 0x06000567 RID: 1383 RVA: 0x00019E14 File Offset: 0x00018014
			public byte[] GetBytes()
			{
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this._algorithm));
				asn.Add(new ASN1(5));
				ASN1 asn2 = new ASN1(48);
				asn2.Add(new ASN1(2, new byte[]
				{
					(byte)this._version
				}));
				asn2.Add(asn);
				asn2.Add(new ASN1(4, this._key));
				if (this._list.Count > 0)
				{
					ASN1 asn3 = new ASN1(160);
					foreach (object obj in this._list)
					{
						ASN1 asn4 = (ASN1)obj;
						asn3.Add(asn4);
					}
					asn2.Add(asn3);
				}
				return asn2.GetBytes();
			}

			// Token: 0x06000568 RID: 1384 RVA: 0x00019F04 File Offset: 0x00018104
			private static byte[] RemoveLeadingZero(byte[] bigInt)
			{
				int srcOffset = 0;
				int num = bigInt.Length;
				if (bigInt[0] == 0)
				{
					srcOffset = 1;
					num--;
				}
				byte[] array = new byte[num];
				Buffer.BlockCopy(bigInt, srcOffset, array, 0, num);
				return array;
			}

			// Token: 0x06000569 RID: 1385 RVA: 0x00019F34 File Offset: 0x00018134
			private static byte[] Normalize(byte[] bigInt, int length)
			{
				if (bigInt.Length == length)
				{
					return bigInt;
				}
				if (bigInt.Length > length)
				{
					return PKCS8.PrivateKeyInfo.RemoveLeadingZero(bigInt);
				}
				byte[] array = new byte[length];
				Buffer.BlockCopy(bigInt, 0, array, length - bigInt.Length, bigInt.Length);
				return array;
			}

			// Token: 0x0600056A RID: 1386 RVA: 0x00019F70 File Offset: 0x00018170
			public static RSA DecodeRSA(byte[] keypair)
			{
				ASN1 asn = new ASN1(keypair);
				if (asn.Tag != 48)
				{
					throw new CryptographicException("invalid private key format");
				}
				if (asn[0].Tag != 2)
				{
					throw new CryptographicException("missing version");
				}
				if (asn.Count < 9)
				{
					throw new CryptographicException("not enough key parameters");
				}
				RSAParameters rsaparameters = new RSAParameters
				{
					Modulus = PKCS8.PrivateKeyInfo.RemoveLeadingZero(asn[1].Value)
				};
				int num = rsaparameters.Modulus.Length;
				int length = num >> 1;
				rsaparameters.D = PKCS8.PrivateKeyInfo.Normalize(asn[3].Value, num);
				rsaparameters.DP = PKCS8.PrivateKeyInfo.Normalize(asn[6].Value, length);
				rsaparameters.DQ = PKCS8.PrivateKeyInfo.Normalize(asn[7].Value, length);
				rsaparameters.Exponent = PKCS8.PrivateKeyInfo.RemoveLeadingZero(asn[2].Value);
				rsaparameters.InverseQ = PKCS8.PrivateKeyInfo.Normalize(asn[8].Value, length);
				rsaparameters.P = PKCS8.PrivateKeyInfo.Normalize(asn[4].Value, length);
				rsaparameters.Q = PKCS8.PrivateKeyInfo.Normalize(asn[5].Value, length);
				RSA rsa = null;
				try
				{
					rsa = RSA.Create();
					rsa.ImportParameters(rsaparameters);
				}
				catch (CryptographicException)
				{
					rsa = new RSACryptoServiceProvider(new CspParameters
					{
						Flags = CspProviderFlags.UseMachineKeyStore
					});
					rsa.ImportParameters(rsaparameters);
				}
				return rsa;
			}

			// Token: 0x0600056B RID: 1387 RVA: 0x0001A0E8 File Offset: 0x000182E8
			public static byte[] Encode(RSA rsa)
			{
				RSAParameters rsaparameters = rsa.ExportParameters(true);
				ASN1 asn = new ASN1(48);
				asn.Add(new ASN1(2, new byte[1]));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.Modulus));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.Exponent));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.D));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.P));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.Q));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.DP));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.DQ));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.InverseQ));
				return asn.GetBytes();
			}

			// Token: 0x0600056C RID: 1388 RVA: 0x0001A1B0 File Offset: 0x000183B0
			public static DSA DecodeDSA(byte[] privateKey, DSAParameters dsaParameters)
			{
				ASN1 asn = new ASN1(privateKey);
				if (asn.Tag != 2)
				{
					throw new CryptographicException("invalid private key format");
				}
				dsaParameters.X = PKCS8.PrivateKeyInfo.Normalize(asn.Value, 20);
				DSA dsa = DSA.Create();
				dsa.ImportParameters(dsaParameters);
				return dsa;
			}

			// Token: 0x0600056D RID: 1389 RVA: 0x0001A1F8 File Offset: 0x000183F8
			public static byte[] Encode(DSA dsa)
			{
				return ASN1Convert.FromUnsignedBigInteger(dsa.ExportParameters(true).X).GetBytes();
			}

			// Token: 0x0600056E RID: 1390 RVA: 0x0001A210 File Offset: 0x00018410
			public static byte[] Encode(AsymmetricAlgorithm aa)
			{
				if (aa is RSA)
				{
					return PKCS8.PrivateKeyInfo.Encode((RSA)aa);
				}
				if (aa is DSA)
				{
					return PKCS8.PrivateKeyInfo.Encode((DSA)aa);
				}
				throw new CryptographicException("Unknown asymmetric algorithm {0}", aa.ToString());
			}

			// Token: 0x040003F3 RID: 1011
			private int _version;

			// Token: 0x040003F4 RID: 1012
			private string _algorithm;

			// Token: 0x040003F5 RID: 1013
			private byte[] _key;

			// Token: 0x040003F6 RID: 1014
			private ArrayList _list;
		}

		// Token: 0x020000A0 RID: 160
		public class EncryptedPrivateKeyInfo
		{
			// Token: 0x0600056F RID: 1391 RVA: 0x0001A24A File Offset: 0x0001844A
			public EncryptedPrivateKeyInfo()
			{
			}

			// Token: 0x06000570 RID: 1392 RVA: 0x0001A252 File Offset: 0x00018452
			public EncryptedPrivateKeyInfo(byte[] data) : this()
			{
				this.Decode(data);
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x06000571 RID: 1393 RVA: 0x0001A261 File Offset: 0x00018461
			// (set) Token: 0x06000572 RID: 1394 RVA: 0x0001A269 File Offset: 0x00018469
			public string Algorithm
			{
				get
				{
					return this._algorithm;
				}
				set
				{
					this._algorithm = value;
				}
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x06000573 RID: 1395 RVA: 0x0001A272 File Offset: 0x00018472
			// (set) Token: 0x06000574 RID: 1396 RVA: 0x0001A28E File Offset: 0x0001848E
			public byte[] EncryptedData
			{
				get
				{
					if (this._data != null)
					{
						return (byte[])this._data.Clone();
					}
					return null;
				}
				set
				{
					this._data = ((value == null) ? null : ((byte[])value.Clone()));
				}
			}

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x06000575 RID: 1397 RVA: 0x0001A2A7 File Offset: 0x000184A7
			// (set) Token: 0x06000576 RID: 1398 RVA: 0x0001A2DD File Offset: 0x000184DD
			public byte[] Salt
			{
				get
				{
					if (this._salt == null)
					{
						RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
						this._salt = new byte[8];
						randomNumberGenerator.GetBytes(this._salt);
					}
					return (byte[])this._salt.Clone();
				}
				set
				{
					this._salt = (byte[])value.Clone();
				}
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x06000577 RID: 1399 RVA: 0x0001A2F0 File Offset: 0x000184F0
			// (set) Token: 0x06000578 RID: 1400 RVA: 0x0001A2F8 File Offset: 0x000184F8
			public int IterationCount
			{
				get
				{
					return this._iterations;
				}
				set
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("IterationCount", "Negative");
					}
					this._iterations = value;
				}
			}

			// Token: 0x06000579 RID: 1401 RVA: 0x0001A318 File Offset: 0x00018518
			private void Decode(byte[] data)
			{
				ASN1 asn = new ASN1(data);
				if (asn.Tag != 48)
				{
					throw new CryptographicException("invalid EncryptedPrivateKeyInfo");
				}
				ASN1 asn2 = asn[0];
				if (asn2.Tag != 48)
				{
					throw new CryptographicException("invalid encryptionAlgorithm");
				}
				ASN1 asn3 = asn2[0];
				if (asn3.Tag != 6)
				{
					throw new CryptographicException("invalid algorithm");
				}
				this._algorithm = ASN1Convert.ToOid(asn3);
				if (asn2.Count > 1)
				{
					ASN1 asn4 = asn2[1];
					if (asn4.Tag != 48)
					{
						throw new CryptographicException("invalid parameters");
					}
					ASN1 asn5 = asn4[0];
					if (asn5.Tag != 4)
					{
						throw new CryptographicException("invalid salt");
					}
					this._salt = asn5.Value;
					ASN1 asn6 = asn4[1];
					if (asn6.Tag != 2)
					{
						throw new CryptographicException("invalid iterationCount");
					}
					this._iterations = ASN1Convert.ToInt32(asn6);
				}
				ASN1 asn7 = asn[1];
				if (asn7.Tag != 4)
				{
					throw new CryptographicException("invalid EncryptedData");
				}
				this._data = asn7.Value;
			}

			// Token: 0x0600057A RID: 1402 RVA: 0x0001A424 File Offset: 0x00018624
			public byte[] GetBytes()
			{
				if (this._algorithm == null)
				{
					throw new CryptographicException("No algorithm OID specified");
				}
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this._algorithm));
				if (this._iterations > 0 || this._salt != null)
				{
					ASN1 asn2 = new ASN1(4, this._salt);
					ASN1 asn3 = ASN1Convert.FromInt32(this._iterations);
					ASN1 asn4 = new ASN1(48);
					asn4.Add(asn2);
					asn4.Add(asn3);
					asn.Add(asn4);
				}
				ASN1 asn5 = new ASN1(4, this._data);
				ASN1 asn6 = new ASN1(48);
				asn6.Add(asn);
				asn6.Add(asn5);
				return asn6.GetBytes();
			}

			// Token: 0x040003F7 RID: 1015
			private string _algorithm;

			// Token: 0x040003F8 RID: 1016
			private byte[] _salt;

			// Token: 0x040003F9 RID: 1017
			private int _iterations;

			// Token: 0x040003FA RID: 1018
			private byte[] _data;
		}
	}
}
