using System;
using System.Diagnostics;

namespace System.IO.Compression
{
	// Token: 0x0200001C RID: 28
	internal sealed class FastEncoderWindow
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00004797 File Offset: 0x00002997
		public FastEncoderWindow()
		{
			this.ResetWindow();
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000047A5 File Offset: 0x000029A5
		public int BytesAvailable
		{
			get
			{
				return this._bufEnd - this._bufPos;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000047B4 File Offset: 0x000029B4
		public DeflateInput UnprocessedInput
		{
			get
			{
				return new DeflateInput
				{
					Buffer = this._window,
					StartIndex = this._bufPos,
					Count = this._bufEnd - this._bufPos
				};
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000047E6 File Offset: 0x000029E6
		public void FlushWindow()
		{
			this.ResetWindow();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000047F0 File Offset: 0x000029F0
		private void ResetWindow()
		{
			this._window = new byte[16646];
			this._prev = new ushort[8450];
			this._lookup = new ushort[2048];
			this._bufPos = 8192;
			this._bufEnd = this._bufPos;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004844 File Offset: 0x00002A44
		public int FreeWindowSpace
		{
			get
			{
				return 16384 - this._bufEnd;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004852 File Offset: 0x00002A52
		public void CopyBytes(byte[] inputBuffer, int startIndex, int count)
		{
			Array.Copy(inputBuffer, startIndex, this._window, this._bufEnd, count);
			this._bufEnd += count;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004878 File Offset: 0x00002A78
		public void MoveWindows()
		{
			Array.Copy(this._window, this._bufPos - 8192, this._window, 0, 8192);
			for (int i = 0; i < 2048; i++)
			{
				int num = (int)(this._lookup[i] - 8192);
				if (num <= 0)
				{
					this._lookup[i] = 0;
				}
				else
				{
					this._lookup[i] = (ushort)num;
				}
			}
			for (int i = 0; i < 8192; i++)
			{
				long num2 = (long)((ulong)this._prev[i] - 8192UL);
				if (num2 <= 0L)
				{
					this._prev[i] = 0;
				}
				else
				{
					this._prev[i] = (ushort)num2;
				}
			}
			this._bufPos = 8192;
			this._bufEnd = this._bufPos;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004932 File Offset: 0x00002B32
		private uint HashValue(uint hash, byte b)
		{
			return hash << 4 ^ (uint)b;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000493C File Offset: 0x00002B3C
		private uint InsertString(ref uint hash)
		{
			hash = this.HashValue(hash, this._window[this._bufPos + 2]);
			uint num = (uint)this._lookup[(int)(hash & 2047U)];
			this._lookup[(int)(hash & 2047U)] = (ushort)this._bufPos;
			this._prev[this._bufPos & 8191] = (ushort)num;
			return num;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000049A0 File Offset: 0x00002BA0
		private void InsertStrings(ref uint hash, int matchLen)
		{
			if (this._bufEnd - this._bufPos <= matchLen)
			{
				this._bufPos += matchLen - 1;
				return;
			}
			while (--matchLen > 0)
			{
				this.InsertString(ref hash);
				this._bufPos++;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000049F0 File Offset: 0x00002BF0
		internal bool GetNextSymbolOrMatch(Match match)
		{
			uint hash = this.HashValue(0U, this._window[this._bufPos]);
			hash = this.HashValue(hash, this._window[this._bufPos + 1]);
			int position = 0;
			int num;
			if (this._bufEnd - this._bufPos <= 3)
			{
				num = 0;
			}
			else
			{
				int num2 = (int)this.InsertString(ref hash);
				if (num2 != 0)
				{
					num = this.FindMatch(num2, out position, 32, 32);
					if (this._bufPos + num > this._bufEnd)
					{
						num = this._bufEnd - this._bufPos;
					}
				}
				else
				{
					num = 0;
				}
			}
			if (num < 3)
			{
				match.State = MatchState.HasSymbol;
				match.Symbol = this._window[this._bufPos];
				this._bufPos++;
			}
			else
			{
				this._bufPos++;
				if (num <= 6)
				{
					int position2 = 0;
					int num3 = (int)this.InsertString(ref hash);
					int num4;
					if (num3 != 0)
					{
						num4 = this.FindMatch(num3, out position2, (num < 4) ? 32 : 8, 32);
						if (this._bufPos + num4 > this._bufEnd)
						{
							num4 = this._bufEnd - this._bufPos;
						}
					}
					else
					{
						num4 = 0;
					}
					if (num4 > num)
					{
						match.State = MatchState.HasSymbolAndMatch;
						match.Symbol = this._window[this._bufPos - 1];
						match.Position = position2;
						match.Length = num4;
						this._bufPos++;
						num = num4;
						this.InsertStrings(ref hash, num);
					}
					else
					{
						match.State = MatchState.HasMatch;
						match.Position = position;
						match.Length = num;
						num--;
						this._bufPos++;
						this.InsertStrings(ref hash, num);
					}
				}
				else
				{
					match.State = MatchState.HasMatch;
					match.Position = position;
					match.Length = num;
					this.InsertStrings(ref hash, num);
				}
			}
			if (this._bufPos == 16384)
			{
				this.MoveWindows();
			}
			return true;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004BC0 File Offset: 0x00002DC0
		private int FindMatch(int search, out int matchPos, int searchDepth, int niceLength)
		{
			int num = 0;
			int num2 = 0;
			int num3 = this._bufPos - 8192;
			byte b = this._window[this._bufPos];
			while (search > num3)
			{
				if (this._window[search + num] == b)
				{
					int num4 = 0;
					while (num4 < 258 && this._window[this._bufPos + num4] == this._window[search + num4])
					{
						num4++;
					}
					if (num4 > num)
					{
						num = num4;
						num2 = search;
						if (num4 > 32)
						{
							break;
						}
						b = this._window[this._bufPos + num4];
					}
				}
				if (--searchDepth == 0)
				{
					break;
				}
				search = (int)this._prev[search & 8191];
			}
			matchPos = this._bufPos - num2 - 1;
			if (num == 3 && matchPos >= 16384)
			{
				return 0;
			}
			return num;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000042E9 File Offset: 0x000024E9
		[Conditional("DEBUG")]
		private void DebugAssertVerifyHashes()
		{
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000042E9 File Offset: 0x000024E9
		[Conditional("DEBUG")]
		private void DebugAssertRecalculatedHashesAreEqual(int position1, int position2, string message = "")
		{
		}

		// Token: 0x040000FE RID: 254
		private byte[] _window;

		// Token: 0x040000FF RID: 255
		private int _bufPos;

		// Token: 0x04000100 RID: 256
		private int _bufEnd;

		// Token: 0x04000101 RID: 257
		private const int FastEncoderHashShift = 4;

		// Token: 0x04000102 RID: 258
		private const int FastEncoderHashtableSize = 2048;

		// Token: 0x04000103 RID: 259
		private const int FastEncoderHashMask = 2047;

		// Token: 0x04000104 RID: 260
		private const int FastEncoderWindowSize = 8192;

		// Token: 0x04000105 RID: 261
		private const int FastEncoderWindowMask = 8191;

		// Token: 0x04000106 RID: 262
		private const int FastEncoderMatch3DistThreshold = 16384;

		// Token: 0x04000107 RID: 263
		internal const int MaxMatch = 258;

		// Token: 0x04000108 RID: 264
		internal const int MinMatch = 3;

		// Token: 0x04000109 RID: 265
		private const int SearchDepth = 32;

		// Token: 0x0400010A RID: 266
		private const int GoodLength = 4;

		// Token: 0x0400010B RID: 267
		private const int NiceLength = 32;

		// Token: 0x0400010C RID: 268
		private const int LazyMatchThreshold = 6;

		// Token: 0x0400010D RID: 269
		private ushort[] _prev;

		// Token: 0x0400010E RID: 270
		private ushort[] _lookup;
	}
}
