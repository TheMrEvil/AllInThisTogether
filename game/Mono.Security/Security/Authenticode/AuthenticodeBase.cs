using System;
using System.IO;
using System.Security.Cryptography;

namespace Mono.Security.Authenticode
{
	// Token: 0x02000065 RID: 101
	public class AuthenticodeBase
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00014D72 File Offset: 0x00012F72
		internal bool PE64
		{
			get
			{
				if (this.blockNo < 1)
				{
					this.ReadFirstBlock();
				}
				return this.pe64;
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00014D89 File Offset: 0x00012F89
		public AuthenticodeBase()
		{
			this.fileblock = new byte[4096];
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00014DA1 File Offset: 0x00012FA1
		internal int PEOffset
		{
			get
			{
				if (this.blockNo < 1)
				{
					this.ReadFirstBlock();
				}
				return this.peOffset;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00014DB8 File Offset: 0x00012FB8
		internal int CoffSymbolTableOffset
		{
			get
			{
				if (this.blockNo < 1)
				{
					this.ReadFirstBlock();
				}
				return this.coffSymbolTableOffset;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00014DCF File Offset: 0x00012FCF
		internal int SecurityOffset
		{
			get
			{
				if (this.blockNo < 1)
				{
					this.ReadFirstBlock();
				}
				return this.dirSecurityOffset;
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00014DE6 File Offset: 0x00012FE6
		internal void Open(string filename)
		{
			if (this.fs != null)
			{
				this.Close();
			}
			this.fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
			this.blockNo = 0;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00014E0C File Offset: 0x0001300C
		internal void Open(byte[] rawdata)
		{
			if (this.fs != null)
			{
				this.Close();
			}
			this.fs = new MemoryStream(rawdata, false);
			this.blockNo = 0;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00014E30 File Offset: 0x00013030
		internal void Close()
		{
			if (this.fs != null)
			{
				this.fs.Close();
				this.fs = null;
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00014E4C File Offset: 0x0001304C
		internal void ReadFirstBlock()
		{
			int num = this.ProcessFirstBlock();
			if (num != 0)
			{
				throw new NotSupportedException(Locale.GetText("Cannot sign non PE files, e.g. .CAB or .MSI files (error {0}).", new object[]
				{
					num
				}));
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00014E84 File Offset: 0x00013084
		internal int ProcessFirstBlock()
		{
			if (this.fs == null)
			{
				return 1;
			}
			this.fs.Position = 0L;
			this.blockLength = this.fs.Read(this.fileblock, 0, this.fileblock.Length);
			this.blockNo = 1;
			if (this.blockLength < 64)
			{
				return 2;
			}
			if (BitConverterLE.ToUInt16(this.fileblock, 0) != 23117)
			{
				return 3;
			}
			this.peOffset = BitConverterLE.ToInt32(this.fileblock, 60);
			if (this.peOffset > this.fileblock.Length)
			{
				throw new NotSupportedException(string.Format(Locale.GetText("Header size too big (> {0} bytes)."), this.fileblock.Length));
			}
			if ((long)this.peOffset > this.fs.Length)
			{
				return 4;
			}
			if (BitConverterLE.ToUInt32(this.fileblock, this.peOffset) != 17744U)
			{
				return 5;
			}
			ushort num = BitConverterLE.ToUInt16(this.fileblock, this.peOffset + 24);
			this.pe64 = (num == 523);
			if (this.pe64)
			{
				this.dirSecurityOffset = BitConverterLE.ToInt32(this.fileblock, this.peOffset + 168);
				this.dirSecuritySize = BitConverterLE.ToInt32(this.fileblock, this.peOffset + 168 + 4);
			}
			else
			{
				this.dirSecurityOffset = BitConverterLE.ToInt32(this.fileblock, this.peOffset + 152);
				this.dirSecuritySize = BitConverterLE.ToInt32(this.fileblock, this.peOffset + 156);
			}
			this.coffSymbolTableOffset = BitConverterLE.ToInt32(this.fileblock, this.peOffset + 12);
			return 0;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00015024 File Offset: 0x00013224
		internal byte[] GetSecurityEntry()
		{
			if (this.blockNo < 1)
			{
				this.ReadFirstBlock();
			}
			if (this.dirSecuritySize > 8)
			{
				byte[] array = new byte[this.dirSecuritySize - 8];
				this.fs.Position = (long)(this.dirSecurityOffset + 8);
				this.fs.Read(array, 0, array.Length);
				return array;
			}
			return null;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00015080 File Offset: 0x00013280
		internal byte[] GetHash(HashAlgorithm hash)
		{
			if (this.blockNo < 1)
			{
				this.ReadFirstBlock();
			}
			this.fs.Position = (long)this.blockLength;
			int num = 0;
			long num2;
			if (this.dirSecurityOffset > 0)
			{
				if (this.dirSecurityOffset < this.blockLength)
				{
					this.blockLength = this.dirSecurityOffset;
					num2 = 0L;
				}
				else
				{
					num2 = (long)(this.dirSecurityOffset - this.blockLength);
				}
			}
			else if (this.coffSymbolTableOffset > 0)
			{
				this.fileblock[this.PEOffset + 12] = 0;
				this.fileblock[this.PEOffset + 13] = 0;
				this.fileblock[this.PEOffset + 14] = 0;
				this.fileblock[this.PEOffset + 15] = 0;
				this.fileblock[this.PEOffset + 16] = 0;
				this.fileblock[this.PEOffset + 17] = 0;
				this.fileblock[this.PEOffset + 18] = 0;
				this.fileblock[this.PEOffset + 19] = 0;
				if (this.coffSymbolTableOffset < this.blockLength)
				{
					this.blockLength = this.coffSymbolTableOffset;
					num2 = 0L;
				}
				else
				{
					num2 = (long)(this.coffSymbolTableOffset - this.blockLength);
				}
			}
			else
			{
				num = (int)(this.fs.Length & 7L);
				if (num > 0)
				{
					num = 8 - num;
				}
				num2 = this.fs.Length - (long)this.blockLength;
			}
			int num3 = this.peOffset + 88;
			hash.TransformBlock(this.fileblock, 0, num3, this.fileblock, 0);
			num3 += 4;
			if (this.pe64)
			{
				hash.TransformBlock(this.fileblock, num3, 76, this.fileblock, num3);
				num3 += 84;
			}
			else
			{
				hash.TransformBlock(this.fileblock, num3, 60, this.fileblock, num3);
				num3 += 68;
			}
			if (num2 == 0L)
			{
				hash.TransformFinalBlock(this.fileblock, num3, this.blockLength - num3);
			}
			else
			{
				hash.TransformBlock(this.fileblock, num3, this.blockLength - num3, this.fileblock, num3);
				long num4 = num2 >> 12;
				int num5 = (int)(num2 - (num4 << 12));
				if (num5 == 0)
				{
					num4 -= 1L;
					num5 = 4096;
				}
				for (;;)
				{
					long num6 = num4;
					num4 = num6 - 1L;
					if (num6 <= 0L)
					{
						break;
					}
					this.fs.Read(this.fileblock, 0, this.fileblock.Length);
					hash.TransformBlock(this.fileblock, 0, this.fileblock.Length, this.fileblock, 0);
				}
				if (this.fs.Read(this.fileblock, 0, num5) != num5)
				{
					return null;
				}
				if (num > 0)
				{
					hash.TransformBlock(this.fileblock, 0, num5, this.fileblock, 0);
					hash.TransformFinalBlock(new byte[num], 0, num);
				}
				else
				{
					hash.TransformFinalBlock(this.fileblock, 0, num5);
				}
			}
			return hash.Hash;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00015340 File Offset: 0x00013540
		protected byte[] HashFile(string fileName, string hashName)
		{
			byte[] result;
			try
			{
				this.Open(fileName);
				HashAlgorithm hash = HashAlgorithm.Create(hashName);
				byte[] hash2 = this.GetHash(hash);
				this.Close();
				result = hash2;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04000305 RID: 773
		public const string spcIndirectDataContext = "1.3.6.1.4.1.311.2.1.4";

		// Token: 0x04000306 RID: 774
		private byte[] fileblock;

		// Token: 0x04000307 RID: 775
		private Stream fs;

		// Token: 0x04000308 RID: 776
		private int blockNo;

		// Token: 0x04000309 RID: 777
		private int blockLength;

		// Token: 0x0400030A RID: 778
		private int peOffset;

		// Token: 0x0400030B RID: 779
		private int dirSecurityOffset;

		// Token: 0x0400030C RID: 780
		private int dirSecuritySize;

		// Token: 0x0400030D RID: 781
		private int coffSymbolTableOffset;

		// Token: 0x0400030E RID: 782
		private bool pe64;
	}
}
