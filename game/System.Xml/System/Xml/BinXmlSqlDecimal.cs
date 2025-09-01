using System;
using System.Diagnostics;
using System.IO;

namespace System.Xml
{
	// Token: 0x0200001E RID: 30
	internal struct BinXmlSqlDecimal
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003BB2 File Offset: 0x00001DB2
		public bool IsPositive
		{
			get
			{
				return this.m_bSign == 0;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public BinXmlSqlDecimal(byte[] data, int offset, bool trim)
		{
			byte b = data[offset];
			if (b <= 11)
			{
				if (b == 7)
				{
					this.m_bLen = 1;
					goto IL_50;
				}
				if (b == 11)
				{
					this.m_bLen = 2;
					goto IL_50;
				}
			}
			else
			{
				if (b == 15)
				{
					this.m_bLen = 3;
					goto IL_50;
				}
				if (b == 19)
				{
					this.m_bLen = 4;
					goto IL_50;
				}
			}
			throw new XmlException("Unable to parse data as SQL_DECIMAL.", null);
			IL_50:
			this.m_bPrec = data[offset + 1];
			this.m_bScale = data[offset + 2];
			this.m_bSign = ((data[offset + 3] == 0) ? 1 : 0);
			this.m_data1 = BinXmlSqlDecimal.UIntFromByteArray(data, offset + 4);
			this.m_data2 = ((this.m_bLen > 1) ? BinXmlSqlDecimal.UIntFromByteArray(data, offset + 8) : 0U);
			this.m_data3 = ((this.m_bLen > 2) ? BinXmlSqlDecimal.UIntFromByteArray(data, offset + 12) : 0U);
			this.m_data4 = ((this.m_bLen > 3) ? BinXmlSqlDecimal.UIntFromByteArray(data, offset + 16) : 0U);
			if (this.m_bLen == 4 && this.m_data4 == 0U)
			{
				this.m_bLen = 3;
			}
			if (this.m_bLen == 3 && this.m_data3 == 0U)
			{
				this.m_bLen = 2;
			}
			if (this.m_bLen == 2 && this.m_data2 == 0U)
			{
				this.m_bLen = 1;
			}
			if (trim)
			{
				this.TrimTrailingZeros();
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003CF8 File Offset: 0x00001EF8
		public void Write(Stream strm)
		{
			strm.WriteByte(this.m_bLen * 4 + 3);
			strm.WriteByte(this.m_bPrec);
			strm.WriteByte(this.m_bScale);
			strm.WriteByte((this.m_bSign == 0) ? 1 : 0);
			this.WriteUI4(this.m_data1, strm);
			if (this.m_bLen > 1)
			{
				this.WriteUI4(this.m_data2, strm);
				if (this.m_bLen > 2)
				{
					this.WriteUI4(this.m_data3, strm);
					if (this.m_bLen > 3)
					{
						this.WriteUI4(this.m_data4, strm);
					}
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003D90 File Offset: 0x00001F90
		private void WriteUI4(uint val, Stream strm)
		{
			strm.WriteByte((byte)(val & 255U));
			strm.WriteByte((byte)(val >> 8 & 255U));
			strm.WriteByte((byte)(val >> 16 & 255U));
			strm.WriteByte((byte)(val >> 24 & 255U));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003DDD File Offset: 0x00001FDD
		private static uint UIntFromByteArray(byte[] data, int offset)
		{
			return (uint)((int)data[offset] | (int)data[offset + 1] << 8 | (int)data[offset + 2] << 16 | (int)data[offset + 3] << 24);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003DFC File Offset: 0x00001FFC
		private bool FZero()
		{
			return this.m_data1 == 0U && this.m_bLen <= 1;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003E14 File Offset: 0x00002014
		private void StoreFromWorkingArray(uint[] rguiData)
		{
			this.m_data1 = rguiData[0];
			this.m_data2 = rguiData[1];
			this.m_data3 = rguiData[2];
			this.m_data4 = rguiData[3];
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003E3C File Offset: 0x0000203C
		private bool FGt10_38(uint[] rglData)
		{
			return (ulong)rglData[3] >= 1262177448UL && ((ulong)rglData[3] > 1262177448UL || (ulong)rglData[2] > 1518781562UL || ((ulong)rglData[2] == 1518781562UL && (ulong)rglData[1] >= 160047680UL));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003E90 File Offset: 0x00002090
		private static void MpDiv1(uint[] rgulU, ref int ciulU, uint iulD, out uint iulR)
		{
			uint num = 0U;
			ulong num2 = (ulong)iulD;
			int i = ciulU;
			while (i > 0)
			{
				i--;
				ulong num3 = ((ulong)num << 32) + (ulong)rgulU[i];
				rgulU[i] = (uint)(num3 / num2);
				num = (uint)(num3 - (ulong)rgulU[i] * num2);
			}
			iulR = num;
			BinXmlSqlDecimal.MpNormalize(rgulU, ref ciulU);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003ED5 File Offset: 0x000020D5
		private static void MpNormalize(uint[] rgulU, ref int ciulU)
		{
			while (ciulU > 1 && rgulU[ciulU - 1] == 0U)
			{
				ciulU--;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003EEC File Offset: 0x000020EC
		internal void AdjustScale(int digits, bool fRound)
		{
			bool flag = false;
			int i = digits;
			if (i + (int)this.m_bScale < 0)
			{
				throw new XmlException("Numeric arithmetic causes truncation.", null);
			}
			if (i + (int)this.m_bScale > (int)BinXmlSqlDecimal.NUMERIC_MAX_PRECISION)
			{
				throw new XmlException("Arithmetic Overflow.", null);
			}
			byte bScale = (byte)(i + (int)this.m_bScale);
			byte bPrec = (byte)Math.Min((int)BinXmlSqlDecimal.NUMERIC_MAX_PRECISION, Math.Max(1, i + (int)this.m_bPrec));
			if (i > 0)
			{
				this.m_bScale = bScale;
				this.m_bPrec = bPrec;
				while (i > 0)
				{
					uint num;
					if (i >= 9)
					{
						num = BinXmlSqlDecimal.x_rgulShiftBase[8];
						i -= 9;
					}
					else
					{
						num = BinXmlSqlDecimal.x_rgulShiftBase[i - 1];
						i = 0;
					}
					this.MultByULong(num);
				}
			}
			else if (i < 0)
			{
				uint num;
				uint num2;
				do
				{
					if (i <= -9)
					{
						num = BinXmlSqlDecimal.x_rgulShiftBase[8];
						i += 9;
					}
					else
					{
						num = BinXmlSqlDecimal.x_rgulShiftBase[-i - 1];
						i = 0;
					}
					num2 = this.DivByULong(num);
				}
				while (i < 0);
				flag = (num2 >= num / 2U);
				this.m_bScale = bScale;
				this.m_bPrec = bPrec;
			}
			if (flag && fRound)
			{
				this.AddULong(1U);
				return;
			}
			if (this.FZero())
			{
				this.m_bSign = 0;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004010 File Offset: 0x00002210
		private void AddULong(uint ulAdd)
		{
			ulong num = (ulong)ulAdd;
			int bLen = (int)this.m_bLen;
			uint[] array = new uint[]
			{
				this.m_data1,
				this.m_data2,
				this.m_data3,
				this.m_data4
			};
			int num2 = 0;
			for (;;)
			{
				num += (ulong)array[num2];
				array[num2] = (uint)num;
				num >>= 32;
				if (num == 0UL)
				{
					break;
				}
				num2++;
				if (num2 >= bLen)
				{
					goto Block_2;
				}
			}
			this.StoreFromWorkingArray(array);
			return;
			Block_2:
			if (num2 == BinXmlSqlDecimal.x_cNumeMax)
			{
				throw new XmlException("Arithmetic Overflow.", null);
			}
			array[num2] = (uint)num;
			this.m_bLen += 1;
			if (this.FGt10_38(array))
			{
				throw new XmlException("Arithmetic Overflow.", null);
			}
			this.StoreFromWorkingArray(array);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000040BC File Offset: 0x000022BC
		private void MultByULong(uint uiMultiplier)
		{
			int bLen = (int)this.m_bLen;
			ulong num = 0UL;
			uint[] array = new uint[]
			{
				this.m_data1,
				this.m_data2,
				this.m_data3,
				this.m_data4
			};
			for (int i = 0; i < bLen; i++)
			{
				ulong num2 = (ulong)array[i] * (ulong)uiMultiplier;
				num += num2;
				if (num < num2)
				{
					num2 = BinXmlSqlDecimal.x_ulInt32Base;
				}
				else
				{
					num2 = 0UL;
				}
				array[i] = (uint)num;
				num = (num >> 32) + num2;
			}
			if (num != 0UL)
			{
				if (bLen == BinXmlSqlDecimal.x_cNumeMax)
				{
					throw new XmlException("Arithmetic Overflow.", null);
				}
				array[bLen] = (uint)num;
				this.m_bLen += 1;
			}
			if (this.FGt10_38(array))
			{
				throw new XmlException("Arithmetic Overflow.", null);
			}
			this.StoreFromWorkingArray(array);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004184 File Offset: 0x00002384
		internal uint DivByULong(uint iDivisor)
		{
			ulong num = (ulong)iDivisor;
			ulong num2 = 0UL;
			bool flag = true;
			if (num == 0UL)
			{
				throw new XmlException("Divide by zero error encountered.", null);
			}
			uint[] array = new uint[]
			{
				this.m_data1,
				this.m_data2,
				this.m_data3,
				this.m_data4
			};
			for (int i = (int)this.m_bLen; i > 0; i--)
			{
				num2 = (num2 << 32) + (ulong)array[i - 1];
				uint num3 = (uint)(num2 / num);
				array[i - 1] = num3;
				num2 %= num;
				flag = (flag && num3 == 0U);
				if (flag)
				{
					this.m_bLen -= 1;
				}
			}
			this.StoreFromWorkingArray(array);
			if (flag)
			{
				this.m_bLen = 1;
			}
			return (uint)num2;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000423B File Offset: 0x0000243B
		private static byte CLenFromPrec(byte bPrec)
		{
			return BinXmlSqlDecimal.rgCLenFromPrec[(int)(bPrec - 1)];
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004246 File Offset: 0x00002446
		private static char ChFromDigit(uint uiDigit)
		{
			return (char)(uiDigit + 48U);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004250 File Offset: 0x00002450
		public decimal ToDecimal()
		{
			if (this.m_data4 != 0U || this.m_bScale > 28)
			{
				throw new XmlException("Arithmetic Overflow.", null);
			}
			return new decimal((int)this.m_data1, (int)this.m_data2, (int)this.m_data3, !this.IsPositive, this.m_bScale);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000042A4 File Offset: 0x000024A4
		private void TrimTrailingZeros()
		{
			uint[] array = new uint[]
			{
				this.m_data1,
				this.m_data2,
				this.m_data3,
				this.m_data4
			};
			int bLen = (int)this.m_bLen;
			if (bLen == 1 && array[0] == 0U)
			{
				this.m_bScale = 0;
				return;
			}
			while (this.m_bScale > 0 && (bLen > 1 || array[0] != 0U))
			{
				uint num;
				BinXmlSqlDecimal.MpDiv1(array, ref bLen, 10U, out num);
				if (num != 0U)
				{
					break;
				}
				this.m_data1 = array[0];
				this.m_data2 = array[1];
				this.m_data3 = array[2];
				this.m_data4 = array[3];
				this.m_bScale -= 1;
			}
			if (this.m_bLen == 4 && this.m_data4 == 0U)
			{
				this.m_bLen = 3;
			}
			if (this.m_bLen == 3 && this.m_data3 == 0U)
			{
				this.m_bLen = 2;
			}
			if (this.m_bLen == 2 && this.m_data2 == 0U)
			{
				this.m_bLen = 1;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004390 File Offset: 0x00002590
		public override string ToString()
		{
			uint[] array = new uint[]
			{
				this.m_data1,
				this.m_data2,
				this.m_data3,
				this.m_data4
			};
			int bLen = (int)this.m_bLen;
			char[] array2 = new char[(int)(BinXmlSqlDecimal.NUMERIC_MAX_PRECISION + 1)];
			int i = 0;
			while (bLen > 1 || array[0] != 0U)
			{
				uint uiDigit;
				BinXmlSqlDecimal.MpDiv1(array, ref bLen, 10U, out uiDigit);
				array2[i++] = BinXmlSqlDecimal.ChFromDigit(uiDigit);
			}
			while (i <= (int)this.m_bScale)
			{
				array2[i++] = BinXmlSqlDecimal.ChFromDigit(0U);
			}
			bool isPositive = this.IsPositive;
			int num = isPositive ? i : (i + 1);
			if (this.m_bScale > 0)
			{
				num++;
			}
			char[] array3 = new char[num];
			int num2 = 0;
			if (!isPositive)
			{
				array3[num2++] = '-';
			}
			while (i > 0)
			{
				if (i-- == (int)this.m_bScale)
				{
					array3[num2++] = '.';
				}
				array3[num2++] = array2[i];
			}
			return new string(array3);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004488 File Offset: 0x00002688
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			object obj = (new uint[]
			{
				this.m_data1,
				this.m_data2,
				this.m_data3,
				this.m_data4
			})[(int)(this.m_bLen - 1)];
			for (int i = (int)this.m_bLen; i < BinXmlSqlDecimal.x_cNumeMax; i++)
			{
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000044E0 File Offset: 0x000026E0
		// Note: this type is marked as 'beforefieldinit'.
		static BinXmlSqlDecimal()
		{
		}

		// Token: 0x04000554 RID: 1364
		internal byte m_bLen;

		// Token: 0x04000555 RID: 1365
		internal byte m_bPrec;

		// Token: 0x04000556 RID: 1366
		internal byte m_bScale;

		// Token: 0x04000557 RID: 1367
		internal byte m_bSign;

		// Token: 0x04000558 RID: 1368
		internal uint m_data1;

		// Token: 0x04000559 RID: 1369
		internal uint m_data2;

		// Token: 0x0400055A RID: 1370
		internal uint m_data3;

		// Token: 0x0400055B RID: 1371
		internal uint m_data4;

		// Token: 0x0400055C RID: 1372
		private static readonly byte NUMERIC_MAX_PRECISION = 38;

		// Token: 0x0400055D RID: 1373
		private static readonly byte MaxPrecision = BinXmlSqlDecimal.NUMERIC_MAX_PRECISION;

		// Token: 0x0400055E RID: 1374
		private static readonly byte MaxScale = BinXmlSqlDecimal.NUMERIC_MAX_PRECISION;

		// Token: 0x0400055F RID: 1375
		private static readonly int x_cNumeMax = 4;

		// Token: 0x04000560 RID: 1376
		private static readonly long x_lInt32Base = 4294967296L;

		// Token: 0x04000561 RID: 1377
		private static readonly ulong x_ulInt32Base = 4294967296UL;

		// Token: 0x04000562 RID: 1378
		private static readonly ulong x_ulInt32BaseForMod = BinXmlSqlDecimal.x_ulInt32Base - 1UL;

		// Token: 0x04000563 RID: 1379
		internal static readonly ulong x_llMax = 9223372036854775807UL;

		// Token: 0x04000564 RID: 1380
		private static readonly double DUINT_BASE = (double)BinXmlSqlDecimal.x_lInt32Base;

		// Token: 0x04000565 RID: 1381
		private static readonly double DUINT_BASE2 = BinXmlSqlDecimal.DUINT_BASE * BinXmlSqlDecimal.DUINT_BASE;

		// Token: 0x04000566 RID: 1382
		private static readonly double DUINT_BASE3 = BinXmlSqlDecimal.DUINT_BASE2 * BinXmlSqlDecimal.DUINT_BASE;

		// Token: 0x04000567 RID: 1383
		private static readonly uint[] x_rgulShiftBase = new uint[]
		{
			10U,
			100U,
			1000U,
			10000U,
			100000U,
			1000000U,
			10000000U,
			100000000U,
			1000000000U
		};

		// Token: 0x04000568 RID: 1384
		private static readonly byte[] rgCLenFromPrec = new byte[]
		{
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4
		};
	}
}
