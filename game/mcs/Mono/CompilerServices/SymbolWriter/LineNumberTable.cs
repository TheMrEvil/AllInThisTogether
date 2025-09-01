using System;
using System.Collections.Generic;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200031C RID: 796
	public class LineNumberTable
	{
		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06002535 RID: 9525 RVA: 0x000B19CE File Offset: 0x000AFBCE
		public LineNumberEntry[] LineNumbers
		{
			get
			{
				return this._line_numbers;
			}
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000B19D8 File Offset: 0x000AFBD8
		protected LineNumberTable(MonoSymbolFile file)
		{
			this.LineBase = file.OffsetTable.LineNumberTable_LineBase;
			this.LineRange = file.OffsetTable.LineNumberTable_LineRange;
			this.OpcodeBase = (byte)file.OffsetTable.LineNumberTable_OpcodeBase;
			this.MaxAddressIncrement = (int)(byte.MaxValue - this.OpcodeBase) / this.LineRange;
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000B1A38 File Offset: 0x000AFC38
		internal LineNumberTable(MonoSymbolFile file, LineNumberEntry[] lines) : this(file)
		{
			this._line_numbers = lines;
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x000B1A48 File Offset: 0x000AFC48
		internal void Write(MonoSymbolFile file, MyBinaryWriter bw, bool hasColumnsInfo, bool hasEndInfo)
		{
			int num = (int)bw.BaseStream.Position;
			bool flag = false;
			int num2 = 1;
			int num3 = 0;
			int num4 = 1;
			for (int i = 0; i < this.LineNumbers.Length; i++)
			{
				int num5 = this.LineNumbers[i].Row - num2;
				int num6 = this.LineNumbers[i].Offset - num3;
				if (this.LineNumbers[i].File != num4)
				{
					bw.Write(4);
					bw.WriteLeb128(this.LineNumbers[i].File);
					num4 = this.LineNumbers[i].File;
				}
				if (this.LineNumbers[i].IsHidden != flag)
				{
					bw.Write(0);
					bw.Write(1);
					bw.Write(64);
					flag = this.LineNumbers[i].IsHidden;
				}
				if (num6 >= this.MaxAddressIncrement)
				{
					if (num6 < 2 * this.MaxAddressIncrement)
					{
						bw.Write(8);
						num6 -= this.MaxAddressIncrement;
					}
					else
					{
						bw.Write(2);
						bw.WriteLeb128(num6);
						num6 = 0;
					}
				}
				if (num5 < this.LineBase || num5 >= this.LineBase + this.LineRange)
				{
					bw.Write(3);
					bw.WriteLeb128(num5);
					if (num6 != 0)
					{
						bw.Write(2);
						bw.WriteLeb128(num6);
					}
					bw.Write(1);
				}
				else
				{
					byte value = (byte)(num5 - this.LineBase + this.LineRange * num6 + (int)this.OpcodeBase);
					bw.Write(value);
				}
				num2 = this.LineNumbers[i].Row;
				num3 = this.LineNumbers[i].Offset;
			}
			bw.Write(0);
			bw.Write(1);
			bw.Write(1);
			if (hasColumnsInfo)
			{
				for (int j = 0; j < this.LineNumbers.Length; j++)
				{
					LineNumberEntry lineNumberEntry = this.LineNumbers[j];
					if (lineNumberEntry.Row >= 0)
					{
						bw.WriteLeb128(lineNumberEntry.Column);
					}
				}
			}
			if (hasEndInfo)
			{
				for (int k = 0; k < this.LineNumbers.Length; k++)
				{
					LineNumberEntry lineNumberEntry2 = this.LineNumbers[k];
					if (lineNumberEntry2.EndRow == -1 || lineNumberEntry2.EndColumn == -1 || lineNumberEntry2.Row > lineNumberEntry2.EndRow)
					{
						bw.WriteLeb128(16777215);
					}
					else
					{
						bw.WriteLeb128(lineNumberEntry2.EndRow - lineNumberEntry2.Row);
						bw.WriteLeb128(lineNumberEntry2.EndColumn);
					}
				}
			}
			file.ExtendedLineNumberSize += (int)bw.BaseStream.Position - num;
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x000B1CC9 File Offset: 0x000AFEC9
		internal static LineNumberTable Read(MonoSymbolFile file, MyBinaryReader br, bool readColumnsInfo, bool readEndInfo)
		{
			LineNumberTable lineNumberTable = new LineNumberTable(file);
			lineNumberTable.DoRead(file, br, readColumnsInfo, readEndInfo);
			return lineNumberTable;
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x000B1CDC File Offset: 0x000AFEDC
		private void DoRead(MonoSymbolFile file, MyBinaryReader br, bool includesColumns, bool includesEnds)
		{
			List<LineNumberEntry> list = new List<LineNumberEntry>();
			bool flag = false;
			bool flag2 = false;
			int num = 1;
			int num2 = 0;
			int file2 = 1;
			byte b;
			for (;;)
			{
				b = br.ReadByte();
				if (b == 0)
				{
					byte b2 = br.ReadByte();
					long position = br.BaseStream.Position + (long)((ulong)b2);
					b = br.ReadByte();
					if (b == 1)
					{
						break;
					}
					if (b == 64)
					{
						flag = !flag;
						flag2 = true;
					}
					else if (b < 64 || b > 127)
					{
						goto IL_7F;
					}
					br.BaseStream.Position = position;
				}
				else
				{
					if (b < this.OpcodeBase)
					{
						switch (b)
						{
						case 1:
							list.Add(new LineNumberEntry(file2, num, -1, num2, flag));
							flag2 = false;
							continue;
						case 2:
							num2 += br.ReadLeb128();
							flag2 = true;
							continue;
						case 3:
							num += br.ReadLeb128();
							flag2 = true;
							continue;
						case 4:
							file2 = br.ReadLeb128();
							flag2 = true;
							continue;
						case 8:
							num2 += this.MaxAddressIncrement;
							flag2 = true;
							continue;
						}
						goto Block_7;
					}
					b -= this.OpcodeBase;
					num2 += (int)b / this.LineRange;
					num += this.LineBase + (int)b % this.LineRange;
					list.Add(new LineNumberEntry(file2, num, -1, num2, flag));
					flag2 = false;
				}
			}
			if (flag2)
			{
				list.Add(new LineNumberEntry(file2, num, -1, num2, flag));
			}
			this._line_numbers = list.ToArray();
			if (includesColumns)
			{
				for (int i = 0; i < this._line_numbers.Length; i++)
				{
					LineNumberEntry lineNumberEntry = this._line_numbers[i];
					if (lineNumberEntry.Row >= 0)
					{
						lineNumberEntry.Column = br.ReadLeb128();
					}
				}
			}
			if (includesEnds)
			{
				for (int j = 0; j < this._line_numbers.Length; j++)
				{
					LineNumberEntry lineNumberEntry2 = this._line_numbers[j];
					int num3 = br.ReadLeb128();
					if (num3 == 16777215)
					{
						lineNumberEntry2.EndRow = -1;
						lineNumberEntry2.EndColumn = -1;
					}
					else
					{
						lineNumberEntry2.Row += num3;
						lineNumberEntry2.EndColumn = br.ReadLeb128();
					}
				}
			}
			return;
			IL_7F:
			throw new MonoSymbolFileException("Unknown extended opcode {0:x}", new object[]
			{
				b
			});
			Block_7:
			throw new MonoSymbolFileException("Unknown standard opcode {0:x} in LNT", new object[]
			{
				b
			});
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x000B1F38 File Offset: 0x000B0138
		public bool GetMethodBounds(out LineNumberEntry start, out LineNumberEntry end)
		{
			if (this._line_numbers.Length > 1)
			{
				start = this._line_numbers[0];
				end = this._line_numbers[this._line_numbers.Length - 1];
				return true;
			}
			start = LineNumberEntry.Null;
			end = LineNumberEntry.Null;
			return false;
		}

		// Token: 0x04000DF5 RID: 3573
		protected LineNumberEntry[] _line_numbers;

		// Token: 0x04000DF6 RID: 3574
		public readonly int LineBase;

		// Token: 0x04000DF7 RID: 3575
		public readonly int LineRange;

		// Token: 0x04000DF8 RID: 3576
		public readonly byte OpcodeBase;

		// Token: 0x04000DF9 RID: 3577
		public readonly int MaxAddressIncrement;

		// Token: 0x04000DFA RID: 3578
		public const int Default_LineBase = -1;

		// Token: 0x04000DFB RID: 3579
		public const int Default_LineRange = 8;

		// Token: 0x04000DFC RID: 3580
		public const byte Default_OpcodeBase = 9;

		// Token: 0x04000DFD RID: 3581
		public const byte DW_LNS_copy = 1;

		// Token: 0x04000DFE RID: 3582
		public const byte DW_LNS_advance_pc = 2;

		// Token: 0x04000DFF RID: 3583
		public const byte DW_LNS_advance_line = 3;

		// Token: 0x04000E00 RID: 3584
		public const byte DW_LNS_set_file = 4;

		// Token: 0x04000E01 RID: 3585
		public const byte DW_LNS_const_add_pc = 8;

		// Token: 0x04000E02 RID: 3586
		public const byte DW_LNE_end_sequence = 1;

		// Token: 0x04000E03 RID: 3587
		public const byte DW_LNE_MONO_negate_is_hidden = 64;

		// Token: 0x04000E04 RID: 3588
		internal const byte DW_LNE_MONO__extensions_start = 64;

		// Token: 0x04000E05 RID: 3589
		internal const byte DW_LNE_MONO__extensions_end = 127;
	}
}
