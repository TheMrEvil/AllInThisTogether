using System;
using System.Runtime.CompilerServices;

namespace System.IO.Compression
{
	// Token: 0x0200001F RID: 31
	internal sealed class HuffmanTree
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004C85 File Offset: 0x00002E85
		public static HuffmanTree StaticLiteralLengthTree
		{
			[CompilerGenerated]
			get
			{
				return HuffmanTree.<StaticLiteralLengthTree>k__BackingField;
			}
		} = new HuffmanTree(HuffmanTree.GetStaticLiteralTreeLength());

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004C8C File Offset: 0x00002E8C
		public static HuffmanTree StaticDistanceTree
		{
			[CompilerGenerated]
			get
			{
				return HuffmanTree.<StaticDistanceTree>k__BackingField;
			}
		} = new HuffmanTree(HuffmanTree.GetStaticDistanceTreeLength());

		// Token: 0x060000C5 RID: 197 RVA: 0x00004C94 File Offset: 0x00002E94
		public HuffmanTree(byte[] codeLengths)
		{
			this._codeLengthArray = codeLengths;
			if (this._codeLengthArray.Length == 288)
			{
				this._tableBits = 9;
			}
			else
			{
				this._tableBits = 7;
			}
			this._tableMask = (1 << this._tableBits) - 1;
			this._table = new short[1 << this._tableBits];
			this._left = new short[2 * this._codeLengthArray.Length];
			this._right = new short[2 * this._codeLengthArray.Length];
			this.CreateTable();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004D28 File Offset: 0x00002F28
		private static byte[] GetStaticLiteralTreeLength()
		{
			byte[] array = new byte[288];
			for (int i = 0; i <= 143; i++)
			{
				array[i] = 8;
			}
			for (int j = 144; j <= 255; j++)
			{
				array[j] = 9;
			}
			for (int k = 256; k <= 279; k++)
			{
				array[k] = 7;
			}
			for (int l = 280; l <= 287; l++)
			{
				array[l] = 8;
			}
			return array;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004DA4 File Offset: 0x00002FA4
		private static byte[] GetStaticDistanceTreeLength()
		{
			byte[] array = new byte[32];
			for (int i = 0; i < 32; i++)
			{
				array[i] = 5;
			}
			return array;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004DCC File Offset: 0x00002FCC
		private uint[] CalculateHuffmanCode()
		{
			uint[] array = new uint[17];
			foreach (int num in this._codeLengthArray)
			{
				array[num] += 1U;
			}
			array[0] = 0U;
			uint[] array2 = new uint[17];
			uint num2 = 0U;
			for (int j = 1; j <= 16; j++)
			{
				num2 = num2 + array[j - 1] << 1;
				array2[j] = num2;
			}
			uint[] array3 = new uint[288];
			for (int k = 0; k < this._codeLengthArray.Length; k++)
			{
				int num3 = (int)this._codeLengthArray[k];
				if (num3 > 0)
				{
					array3[k] = FastEncoderStatics.BitReverse(array2[num3], num3);
					array2[num3] += 1U;
				}
			}
			return array3;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004E90 File Offset: 0x00003090
		private void CreateTable()
		{
			uint[] array = this.CalculateHuffmanCode();
			short num = (short)this._codeLengthArray.Length;
			for (int i = 0; i < this._codeLengthArray.Length; i++)
			{
				int num2 = (int)this._codeLengthArray[i];
				if (num2 > 0)
				{
					int num3 = (int)array[i];
					if (num2 > this._tableBits)
					{
						int num4 = num2 - this._tableBits;
						int num5 = 1 << this._tableBits;
						int num6 = num3 & (1 << this._tableBits) - 1;
						short[] array2 = this._table;
						do
						{
							short num7 = array2[num6];
							if (num7 == 0)
							{
								array2[num6] = -num;
								num7 = -num;
								num += 1;
							}
							if (num7 > 0)
							{
								goto Block_6;
							}
							if ((num3 & num5) == 0)
							{
								array2 = this._left;
							}
							else
							{
								array2 = this._right;
							}
							num6 = (int)(-(int)num7);
							num5 <<= 1;
							num4--;
						}
						while (num4 != 0);
						array2[num6] = (short)i;
						goto IL_119;
						Block_6:
						throw new InvalidDataException("Failed to construct a huffman tree using the length array. The stream might be corrupted.");
					}
					int num8 = 1 << num2;
					if (num3 >= num8)
					{
						throw new InvalidDataException("Failed to construct a huffman tree using the length array. The stream might be corrupted.");
					}
					int num9 = 1 << this._tableBits - num2;
					for (int j = 0; j < num9; j++)
					{
						this._table[num3] = (short)i;
						num3 += num8;
					}
				}
				IL_119:;
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004FC8 File Offset: 0x000031C8
		public int GetNextSymbol(InputBuffer input)
		{
			uint num = input.TryLoad16Bits();
			if (input.AvailableBits == 0)
			{
				return -1;
			}
			int num2 = (int)this._table[(int)(checked((IntPtr)(unchecked((ulong)num & (ulong)((long)this._tableMask)))))];
			if (num2 < 0)
			{
				uint num3 = 1U << this._tableBits;
				do
				{
					num2 = -num2;
					if ((num & num3) == 0U)
					{
						num2 = (int)this._left[num2];
					}
					else
					{
						num2 = (int)this._right[num2];
					}
					num3 <<= 1;
				}
				while (num2 < 0);
			}
			int num4 = (int)this._codeLengthArray[num2];
			if (num4 <= 0)
			{
				throw new InvalidDataException("Failed to construct a huffman tree using the length array. The stream might be corrupted.");
			}
			if (num4 > input.AvailableBits)
			{
				return -1;
			}
			input.SkipBits(num4);
			return num2;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005058 File Offset: 0x00003258
		// Note: this type is marked as 'beforefieldinit'.
		static HuffmanTree()
		{
		}

		// Token: 0x0400010F RID: 271
		internal const int MaxLiteralTreeElements = 288;

		// Token: 0x04000110 RID: 272
		internal const int MaxDistTreeElements = 32;

		// Token: 0x04000111 RID: 273
		internal const int EndOfBlockCode = 256;

		// Token: 0x04000112 RID: 274
		internal const int NumberOfCodeLengthTreeElements = 19;

		// Token: 0x04000113 RID: 275
		private readonly int _tableBits;

		// Token: 0x04000114 RID: 276
		private readonly short[] _table;

		// Token: 0x04000115 RID: 277
		private readonly short[] _left;

		// Token: 0x04000116 RID: 278
		private readonly short[] _right;

		// Token: 0x04000117 RID: 279
		private readonly byte[] _codeLengthArray;

		// Token: 0x04000118 RID: 280
		private readonly int _tableMask;

		// Token: 0x04000119 RID: 281
		[CompilerGenerated]
		private static readonly HuffmanTree <StaticLiteralLengthTree>k__BackingField;

		// Token: 0x0400011A RID: 282
		[CompilerGenerated]
		private static readonly HuffmanTree <StaticDistanceTree>k__BackingField;
	}
}
