using System;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security
{
	// Token: 0x0200007D RID: 125
	internal sealed class StrongName
	{
		// Token: 0x06000236 RID: 566 RVA: 0x0000259F File Offset: 0x0000079F
		public StrongName()
		{
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000AB8C File Offset: 0x00008D8C
		public StrongName(int keySize)
		{
			this.rsa = new RSAManaged(keySize);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		public StrongName(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length == 16)
			{
				int i = 0;
				int num = 0;
				while (i < data.Length)
				{
					num += (int)data[i++];
				}
				if (num == 4)
				{
					this.publicKey = (byte[])data.Clone();
					return;
				}
			}
			else
			{
				this.RSA = CryptoConvert.FromCapiKeyBlob(data);
				if (this.rsa == null)
				{
					throw new ArgumentException("data isn't a correctly encoded RSA public key");
				}
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000AC13 File Offset: 0x00008E13
		public StrongName(RSA rsa)
		{
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			this.RSA = rsa;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000AC30 File Offset: 0x00008E30
		private void InvalidateCache()
		{
			this.publicKey = null;
			this.keyToken = null;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000AC40 File Offset: 0x00008E40
		public bool CanSign
		{
			get
			{
				if (this.rsa == null)
				{
					return false;
				}
				if (this.RSA is RSACryptoServiceProvider)
				{
					return !(this.rsa as RSACryptoServiceProvider).PublicOnly;
				}
				if (this.RSA is RSAManaged)
				{
					return !(this.rsa as RSAManaged).PublicOnly;
				}
				bool result;
				try
				{
					RSAParameters rsaparameters = this.rsa.ExportParameters(true);
					result = (rsaparameters.D != null && rsaparameters.P != null && rsaparameters.Q != null);
				}
				catch (CryptographicException)
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000ACDC File Offset: 0x00008EDC
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000ACF7 File Offset: 0x00008EF7
		public RSA RSA
		{
			get
			{
				if (this.rsa == null)
				{
					this.rsa = RSA.Create();
				}
				return this.rsa;
			}
			set
			{
				this.rsa = value;
				this.InvalidateCache();
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000AD08 File Offset: 0x00008F08
		public byte[] PublicKey
		{
			get
			{
				if (this.publicKey == null)
				{
					byte[] array = CryptoConvert.ToCapiKeyBlob(this.rsa, false);
					this.publicKey = new byte[32 + (this.rsa.KeySize >> 3)];
					this.publicKey[0] = array[4];
					this.publicKey[1] = array[5];
					this.publicKey[2] = array[6];
					this.publicKey[3] = array[7];
					this.publicKey[4] = 4;
					this.publicKey[5] = 128;
					this.publicKey[6] = 0;
					this.publicKey[7] = 0;
					byte[] bytes = BitConverterLE.GetBytes(this.publicKey.Length - 12);
					this.publicKey[8] = bytes[0];
					this.publicKey[9] = bytes[1];
					this.publicKey[10] = bytes[2];
					this.publicKey[11] = bytes[3];
					this.publicKey[12] = 6;
					Buffer.BlockCopy(array, 1, this.publicKey, 13, this.publicKey.Length - 13);
					this.publicKey[23] = 49;
				}
				return (byte[])this.publicKey.Clone();
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000AE1C File Offset: 0x0000901C
		public byte[] PublicKeyToken
		{
			get
			{
				if (this.keyToken == null)
				{
					byte[] array = this.PublicKey;
					if (array == null)
					{
						return null;
					}
					byte[] array2 = StrongName.GetHashAlgorithm(this.TokenAlgorithm).ComputeHash(array);
					this.keyToken = new byte[8];
					Buffer.BlockCopy(array2, array2.Length - 8, this.keyToken, 0, 8);
					Array.Reverse<byte>(this.keyToken, 0, 8);
				}
				return (byte[])this.keyToken.Clone();
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000AE8B File Offset: 0x0000908B
		private static HashAlgorithm GetHashAlgorithm(string algorithm)
		{
			return HashAlgorithm.Create(algorithm);
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000AE93 File Offset: 0x00009093
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0000AEB0 File Offset: 0x000090B0
		public string TokenAlgorithm
		{
			get
			{
				if (this.tokenAlgorithm == null)
				{
					this.tokenAlgorithm = "SHA1";
				}
				return this.tokenAlgorithm;
			}
			set
			{
				string a = value.ToUpper(CultureInfo.InvariantCulture);
				if (a == "SHA1" || a == "MD5")
				{
					this.tokenAlgorithm = value;
					this.InvalidateCache();
					return;
				}
				throw new ArgumentException("Unsupported hash algorithm for token");
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000AEFB File Offset: 0x000090FB
		public byte[] GetBytes()
		{
			return CryptoConvert.ToCapiPrivateKeyBlob(this.RSA);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000AF08 File Offset: 0x00009108
		private uint RVAtoPosition(uint r, int sections, byte[] headers)
		{
			for (int i = 0; i < sections; i++)
			{
				uint num = BitConverterLE.ToUInt32(headers, i * 40 + 20);
				uint num2 = BitConverterLE.ToUInt32(headers, i * 40 + 12);
				int num3 = (int)BitConverterLE.ToUInt32(headers, i * 40 + 8);
				if (num2 <= r && (ulong)r < (ulong)num2 + (ulong)((long)num3))
				{
					return num + r - num2;
				}
			}
			return 0U;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000AF5E File Offset: 0x0000915E
		private static StrongName.StrongNameSignature Error(string a)
		{
			return null;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000AF64 File Offset: 0x00009164
		private static byte[] ReadMore(Stream stream, byte[] a, int newSize)
		{
			int num = a.Length;
			Array.Resize<byte>(ref a, newSize);
			if (newSize <= num)
			{
				return a;
			}
			int num2 = newSize - num;
			if (stream.Read(a, num, num2) != num2)
			{
				return null;
			}
			return a;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000AF98 File Offset: 0x00009198
		internal StrongName.StrongNameSignature StrongHash(Stream stream, StrongName.StrongNameOptions options)
		{
			byte[] array = new byte[64];
			int num = stream.Read(array, 0, 64);
			if (num == 64 && array[0] == 77 && array[1] == 90)
			{
				int num2 = BitConverterLE.ToInt32(array, 60);
				if (num2 < 64)
				{
					return StrongName.Error("peHeader_lt_64");
				}
				array = StrongName.ReadMore(stream, array, num2);
				if (array == null)
				{
					return StrongName.Error("read_mz2_failed");
				}
			}
			else
			{
				if (num < 4 || array[0] != 80 || array[1] != 69 || array[2] != 0 || array[3] != 0)
				{
					return StrongName.Error("read_mz_or_mzsig_failed");
				}
				stream.Position = 0L;
				array = new byte[0];
			}
			int num3 = 2;
			int num4 = 24 + num3;
			byte[] array2 = new byte[num4];
			if (stream.Read(array2, 0, num4) != num4 || array2[0] != 80 || array2[1] != 69 || array2[2] != 0 || array2[3] != 0)
			{
				return StrongName.Error("read_minimumHeadersSize_or_pesig_failed");
			}
			num3 = (int)BitConverterLE.ToUInt16(array2, 20);
			if (num3 < 2)
			{
				return StrongName.Error(string.Format("sizeOfOptionalHeader_lt_2 ${0}", num3));
			}
			int num5 = 24 + num3;
			if (num5 < 24)
			{
				return StrongName.Error("headers_overflow");
			}
			array2 = StrongName.ReadMore(stream, array2, num5);
			if (array2 == null)
			{
				return StrongName.Error("read_pe2_failed");
			}
			uint num6 = (uint)BitConverterLE.ToUInt16(array2, 24);
			int num7 = 0;
			bool flag = false;
			if (num6 != 267U)
			{
				if (num6 == 523U)
				{
					num7 = 16;
				}
				else
				{
					if (num6 != 263U)
					{
						return StrongName.Error("bad_magic_value");
					}
					flag = true;
				}
			}
			uint num8 = 0U;
			if (!flag)
			{
				if (num3 >= 116 + num7 + 4)
				{
					num8 = BitConverterLE.ToUInt32(array2, 116 + num7);
				}
				int num9 = 64;
				while (num9 < num3 && num9 < 68)
				{
					array2[24 + num9] = 0;
					num9++;
				}
				int num10 = 128 + num7;
				while (num10 < num3 && num10 < 136 + num7)
				{
					array2[24 + num10] = 0;
					num10++;
				}
			}
			int num11 = (int)BitConverterLE.ToUInt16(array2, 6);
			byte[] array3 = new byte[num11 * 40];
			if (stream.Read(array3, 0, array3.Length) != array3.Length)
			{
				return StrongName.Error("read_section_headers_failed");
			}
			uint num12 = 0U;
			uint num13 = 0U;
			uint num14 = 0U;
			uint num15 = 0U;
			if (15U < num8 && num3 >= 216 + num7)
			{
				uint r = BitConverterLE.ToUInt32(array2, 232 + num7);
				uint num16 = this.RVAtoPosition(r, num11, array3);
				int num17 = BitConverterLE.ToInt32(array2, 236 + num7);
				byte[] array4 = new byte[num17];
				stream.Position = (long)((ulong)num16);
				if (stream.Read(array4, 0, num17) != num17)
				{
					return StrongName.Error("read_cli_header_failed");
				}
				uint r2 = BitConverterLE.ToUInt32(array4, 32);
				num12 = this.RVAtoPosition(r2, num11, array3);
				num13 = BitConverterLE.ToUInt32(array4, 36);
				uint r3 = BitConverterLE.ToUInt32(array4, 8);
				num14 = this.RVAtoPosition(r3, num11, array3);
				num15 = BitConverterLE.ToUInt32(array4, 12);
			}
			StrongName.StrongNameSignature strongNameSignature = new StrongName.StrongNameSignature();
			strongNameSignature.SignaturePosition = num12;
			strongNameSignature.SignatureLength = num13;
			strongNameSignature.MetadataPosition = num14;
			strongNameSignature.MetadataLength = num15;
			using (HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.TokenAlgorithm))
			{
				if (options == StrongName.StrongNameOptions.Metadata)
				{
					hashAlgorithm.Initialize();
					byte[] buffer = new byte[num15];
					stream.Position = (long)((ulong)num14);
					if (stream.Read(buffer, 0, (int)num15) != (int)num15)
					{
						return StrongName.Error("read_cli_metadata_failed");
					}
					strongNameSignature.Hash = hashAlgorithm.ComputeHash(buffer);
					return strongNameSignature;
				}
				else
				{
					using (CryptoStream cryptoStream = new CryptoStream(Stream.Null, hashAlgorithm, CryptoStreamMode.Write))
					{
						cryptoStream.Write(array, 0, array.Length);
						cryptoStream.Write(array2, 0, array2.Length);
						cryptoStream.Write(array3, 0, array3.Length);
						for (int i = 0; i < num11; i++)
						{
							uint num18 = BitConverterLE.ToUInt32(array3, i * 40 + 20);
							int num19 = BitConverterLE.ToInt32(array3, i * 40 + 16);
							byte[] array5 = new byte[num19];
							stream.Position = (long)((ulong)num18);
							if (stream.Read(array5, 0, num19) != num19)
							{
								return StrongName.Error("read_section_failed");
							}
							if (num18 <= num12 && num12 < num18 + (uint)num19)
							{
								int num20 = (int)(num12 - num18);
								if (num20 > 0)
								{
									cryptoStream.Write(array5, 0, num20);
								}
								strongNameSignature.Signature = new byte[num13];
								Buffer.BlockCopy(array5, num20, strongNameSignature.Signature, 0, (int)num13);
								Array.Reverse<byte>(strongNameSignature.Signature);
								int num21 = (int)((long)num20 + (long)((ulong)num13));
								int num22 = num19 - num21;
								if (num22 > 0)
								{
									cryptoStream.Write(array5, num21, num22);
								}
							}
							else
							{
								cryptoStream.Write(array5, 0, num19);
							}
						}
					}
					strongNameSignature.Hash = hashAlgorithm.Hash;
				}
			}
			return strongNameSignature;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000B490 File Offset: 0x00009690
		public byte[] Hash(string fileName)
		{
			byte[] hash;
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				hash = this.StrongHash(fileStream, StrongName.StrongNameOptions.Metadata).Hash;
			}
			return hash;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000B4D0 File Offset: 0x000096D0
		public bool Sign(string fileName)
		{
			StrongName.StrongNameSignature strongNameSignature;
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				strongNameSignature = this.StrongHash(fileStream, StrongName.StrongNameOptions.Signature);
			}
			if (strongNameSignature.Hash == null)
			{
				return false;
			}
			byte[] array = null;
			try
			{
				RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(this.rsa);
				rsapkcs1SignatureFormatter.SetHashAlgorithm(this.TokenAlgorithm);
				array = rsapkcs1SignatureFormatter.CreateSignature(strongNameSignature.Hash);
				Array.Reverse<byte>(array);
			}
			catch (CryptographicException)
			{
				return false;
			}
			using (FileStream fileStream2 = File.OpenWrite(fileName))
			{
				fileStream2.Position = (long)((ulong)strongNameSignature.SignaturePosition);
				fileStream2.Write(array, 0, array.Length);
			}
			return true;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000B590 File Offset: 0x00009790
		public bool Verify(string fileName)
		{
			bool result;
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				result = this.Verify(fileStream);
			}
			return result;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000B5CC File Offset: 0x000097CC
		public bool Verify(Stream stream)
		{
			StrongName.StrongNameSignature strongNameSignature = this.StrongHash(stream, StrongName.StrongNameOptions.Signature);
			if (strongNameSignature.Hash == null)
			{
				return false;
			}
			bool result;
			try
			{
				AssemblyHashAlgorithm algorithm = AssemblyHashAlgorithm.SHA1;
				if (this.tokenAlgorithm == "MD5")
				{
					algorithm = AssemblyHashAlgorithm.MD5;
				}
				result = StrongName.Verify(this.rsa, algorithm, strongNameSignature.Hash, strongNameSignature.Signature);
			}
			catch (CryptographicException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000B63C File Offset: 0x0000983C
		public static bool IsAssemblyStrongnamed(string assemblyName)
		{
			if (!StrongName.initialized)
			{
				object obj = StrongName.lockObject;
				lock (obj)
				{
					if (!StrongName.initialized)
					{
						StrongNameManager.LoadConfig(Environment.GetMachineConfigPath());
						StrongName.initialized = true;
					}
				}
			}
			bool flag;
			try
			{
				AssemblyName assemblyName2 = AssemblyName.GetAssemblyName(assemblyName);
				if (assemblyName2 == null)
				{
					flag = false;
				}
				else
				{
					byte[] mappedPublicKey = StrongNameManager.GetMappedPublicKey(assemblyName2.GetPublicKeyToken());
					if (mappedPublicKey == null || mappedPublicKey.Length < 12)
					{
						mappedPublicKey = assemblyName2.GetPublicKey();
						if (mappedPublicKey == null || mappedPublicKey.Length < 12)
						{
							return false;
						}
					}
					if (!StrongNameManager.MustVerify(assemblyName2))
					{
						flag = true;
					}
					else
					{
						flag = new StrongName(CryptoConvert.FromCapiPublicKeyBlob(mappedPublicKey, 12)).Verify(assemblyName);
					}
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000B704 File Offset: 0x00009904
		public static bool VerifySignature(byte[] publicKey, int algorithm, byte[] hash, byte[] signature)
		{
			bool result;
			try
			{
				result = StrongName.Verify(CryptoConvert.FromCapiPublicKeyBlob(publicKey), (AssemblyHashAlgorithm)algorithm, hash, signature);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000B738 File Offset: 0x00009938
		private static bool Verify(RSA rsa, AssemblyHashAlgorithm algorithm, byte[] hash, byte[] signature)
		{
			RSAPKCS1SignatureDeformatter rsapkcs1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
			if (algorithm != AssemblyHashAlgorithm.None)
			{
				if (algorithm == AssemblyHashAlgorithm.MD5)
				{
					rsapkcs1SignatureDeformatter.SetHashAlgorithm("MD5");
					goto IL_34;
				}
				if (algorithm != AssemblyHashAlgorithm.SHA1)
				{
				}
			}
			rsapkcs1SignatureDeformatter.SetHashAlgorithm("SHA1");
			IL_34:
			return rsapkcs1SignatureDeformatter.VerifySignature(hash, signature);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000B781 File Offset: 0x00009981
		// Note: this type is marked as 'beforefieldinit'.
		static StrongName()
		{
		}

		// Token: 0x04000E99 RID: 3737
		private RSA rsa;

		// Token: 0x04000E9A RID: 3738
		private byte[] publicKey;

		// Token: 0x04000E9B RID: 3739
		private byte[] keyToken;

		// Token: 0x04000E9C RID: 3740
		private string tokenAlgorithm;

		// Token: 0x04000E9D RID: 3741
		private static object lockObject = new object();

		// Token: 0x04000E9E RID: 3742
		private static bool initialized;

		// Token: 0x0200007E RID: 126
		internal class StrongNameSignature
		{
			// Token: 0x1700002B RID: 43
			// (get) Token: 0x06000250 RID: 592 RVA: 0x0000B78D File Offset: 0x0000998D
			// (set) Token: 0x06000251 RID: 593 RVA: 0x0000B795 File Offset: 0x00009995
			public byte[] Hash
			{
				get
				{
					return this.hash;
				}
				set
				{
					this.hash = value;
				}
			}

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x06000252 RID: 594 RVA: 0x0000B79E File Offset: 0x0000999E
			// (set) Token: 0x06000253 RID: 595 RVA: 0x0000B7A6 File Offset: 0x000099A6
			public byte[] Signature
			{
				get
				{
					return this.signature;
				}
				set
				{
					this.signature = value;
				}
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x06000254 RID: 596 RVA: 0x0000B7AF File Offset: 0x000099AF
			// (set) Token: 0x06000255 RID: 597 RVA: 0x0000B7B7 File Offset: 0x000099B7
			public uint MetadataPosition
			{
				get
				{
					return this.metadataPosition;
				}
				set
				{
					this.metadataPosition = value;
				}
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x06000256 RID: 598 RVA: 0x0000B7C0 File Offset: 0x000099C0
			// (set) Token: 0x06000257 RID: 599 RVA: 0x0000B7C8 File Offset: 0x000099C8
			public uint MetadataLength
			{
				get
				{
					return this.metadataLength;
				}
				set
				{
					this.metadataLength = value;
				}
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000258 RID: 600 RVA: 0x0000B7D1 File Offset: 0x000099D1
			// (set) Token: 0x06000259 RID: 601 RVA: 0x0000B7D9 File Offset: 0x000099D9
			public uint SignaturePosition
			{
				get
				{
					return this.signaturePosition;
				}
				set
				{
					this.signaturePosition = value;
				}
			}

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x0600025A RID: 602 RVA: 0x0000B7E2 File Offset: 0x000099E2
			// (set) Token: 0x0600025B RID: 603 RVA: 0x0000B7EA File Offset: 0x000099EA
			public uint SignatureLength
			{
				get
				{
					return this.signatureLength;
				}
				set
				{
					this.signatureLength = value;
				}
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x0600025C RID: 604 RVA: 0x0000B7F3 File Offset: 0x000099F3
			// (set) Token: 0x0600025D RID: 605 RVA: 0x0000B7FB File Offset: 0x000099FB
			public byte CliFlag
			{
				get
				{
					return this.cliFlag;
				}
				set
				{
					this.cliFlag = value;
				}
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x0600025E RID: 606 RVA: 0x0000B804 File Offset: 0x00009A04
			// (set) Token: 0x0600025F RID: 607 RVA: 0x0000B80C File Offset: 0x00009A0C
			public uint CliFlagPosition
			{
				get
				{
					return this.cliFlagPosition;
				}
				set
				{
					this.cliFlagPosition = value;
				}
			}

			// Token: 0x06000260 RID: 608 RVA: 0x0000259F File Offset: 0x0000079F
			public StrongNameSignature()
			{
			}

			// Token: 0x04000E9F RID: 3743
			private byte[] hash;

			// Token: 0x04000EA0 RID: 3744
			private byte[] signature;

			// Token: 0x04000EA1 RID: 3745
			private uint signaturePosition;

			// Token: 0x04000EA2 RID: 3746
			private uint signatureLength;

			// Token: 0x04000EA3 RID: 3747
			private uint metadataPosition;

			// Token: 0x04000EA4 RID: 3748
			private uint metadataLength;

			// Token: 0x04000EA5 RID: 3749
			private byte cliFlag;

			// Token: 0x04000EA6 RID: 3750
			private uint cliFlagPosition;
		}

		// Token: 0x0200007F RID: 127
		internal enum StrongNameOptions
		{
			// Token: 0x04000EA8 RID: 3752
			Metadata,
			// Token: 0x04000EA9 RID: 3753
			Signature
		}
	}
}
