using System;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security
{
	// Token: 0x0200000B RID: 11
	public sealed class StrongName
	{
		// Token: 0x0600004F RID: 79 RVA: 0x000038FB File Offset: 0x00001AFB
		public StrongName()
		{
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003903 File Offset: 0x00001B03
		public StrongName(int keySize)
		{
			this.rsa = new RSAManaged(keySize);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003918 File Offset: 0x00001B18
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

		// Token: 0x06000052 RID: 82 RVA: 0x0000398B File Offset: 0x00001B8B
		public StrongName(RSA rsa)
		{
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			this.RSA = rsa;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000039A8 File Offset: 0x00001BA8
		private void InvalidateCache()
		{
			this.publicKey = null;
			this.keyToken = null;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000039B8 File Offset: 0x00001BB8
		public bool CanSign
		{
			get
			{
				if (this.rsa == null)
				{
					return false;
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

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003A34 File Offset: 0x00001C34
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00003A4F File Offset: 0x00001C4F
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003A60 File Offset: 0x00001C60
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003B74 File Offset: 0x00001D74
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

		// Token: 0x06000059 RID: 89 RVA: 0x00003BE3 File Offset: 0x00001DE3
		private static HashAlgorithm GetHashAlgorithm(string algorithm)
		{
			return HashAlgorithm.Create(algorithm);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003BEB File Offset: 0x00001DEB
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003C08 File Offset: 0x00001E08
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

		// Token: 0x0600005C RID: 92 RVA: 0x00003C53 File Offset: 0x00001E53
		public byte[] GetBytes()
		{
			return CryptoConvert.ToCapiPrivateKeyBlob(this.RSA);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003C60 File Offset: 0x00001E60
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

		// Token: 0x0600005E RID: 94 RVA: 0x00003CB6 File Offset: 0x00001EB6
		private static StrongName.StrongNameSignature Error(string a)
		{
			return null;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003CBC File Offset: 0x00001EBC
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

		// Token: 0x06000060 RID: 96 RVA: 0x00003CF0 File Offset: 0x00001EF0
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

		// Token: 0x06000061 RID: 97 RVA: 0x000041E8 File Offset: 0x000023E8
		public byte[] Hash(string fileName)
		{
			byte[] hash;
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				hash = this.StrongHash(fileStream, StrongName.StrongNameOptions.Metadata).Hash;
			}
			return hash;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004228 File Offset: 0x00002428
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

		// Token: 0x06000063 RID: 99 RVA: 0x000042E8 File Offset: 0x000024E8
		public bool Verify(string fileName)
		{
			bool result;
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				result = this.Verify(fileStream);
			}
			return result;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004324 File Offset: 0x00002524
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

		// Token: 0x06000065 RID: 101 RVA: 0x00004394 File Offset: 0x00002594
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

		// Token: 0x04000047 RID: 71
		private RSA rsa;

		// Token: 0x04000048 RID: 72
		private byte[] publicKey;

		// Token: 0x04000049 RID: 73
		private byte[] keyToken;

		// Token: 0x0400004A RID: 74
		private string tokenAlgorithm;

		// Token: 0x02000083 RID: 131
		internal class StrongNameSignature
		{
			// Token: 0x17000157 RID: 343
			// (get) Token: 0x0600050C RID: 1292 RVA: 0x00019355 File Offset: 0x00017555
			// (set) Token: 0x0600050D RID: 1293 RVA: 0x0001935D File Offset: 0x0001755D
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

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x0600050E RID: 1294 RVA: 0x00019366 File Offset: 0x00017566
			// (set) Token: 0x0600050F RID: 1295 RVA: 0x0001936E File Offset: 0x0001756E
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

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x06000510 RID: 1296 RVA: 0x00019377 File Offset: 0x00017577
			// (set) Token: 0x06000511 RID: 1297 RVA: 0x0001937F File Offset: 0x0001757F
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

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x06000512 RID: 1298 RVA: 0x00019388 File Offset: 0x00017588
			// (set) Token: 0x06000513 RID: 1299 RVA: 0x00019390 File Offset: 0x00017590
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

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x06000514 RID: 1300 RVA: 0x00019399 File Offset: 0x00017599
			// (set) Token: 0x06000515 RID: 1301 RVA: 0x000193A1 File Offset: 0x000175A1
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

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x06000516 RID: 1302 RVA: 0x000193AA File Offset: 0x000175AA
			// (set) Token: 0x06000517 RID: 1303 RVA: 0x000193B2 File Offset: 0x000175B2
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

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x06000518 RID: 1304 RVA: 0x000193BB File Offset: 0x000175BB
			// (set) Token: 0x06000519 RID: 1305 RVA: 0x000193C3 File Offset: 0x000175C3
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

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x0600051A RID: 1306 RVA: 0x000193CC File Offset: 0x000175CC
			// (set) Token: 0x0600051B RID: 1307 RVA: 0x000193D4 File Offset: 0x000175D4
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

			// Token: 0x0600051C RID: 1308 RVA: 0x000193DD File Offset: 0x000175DD
			public StrongNameSignature()
			{
			}

			// Token: 0x040003BB RID: 955
			private byte[] hash;

			// Token: 0x040003BC RID: 956
			private byte[] signature;

			// Token: 0x040003BD RID: 957
			private uint signaturePosition;

			// Token: 0x040003BE RID: 958
			private uint signatureLength;

			// Token: 0x040003BF RID: 959
			private uint metadataPosition;

			// Token: 0x040003C0 RID: 960
			private uint metadataLength;

			// Token: 0x040003C1 RID: 961
			private byte cliFlag;

			// Token: 0x040003C2 RID: 962
			private uint cliFlagPosition;
		}

		// Token: 0x02000084 RID: 132
		internal enum StrongNameOptions
		{
			// Token: 0x040003C4 RID: 964
			Metadata,
			// Token: 0x040003C5 RID: 965
			Signature
		}
	}
}
