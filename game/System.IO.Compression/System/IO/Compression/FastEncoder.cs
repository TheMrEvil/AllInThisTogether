using System;

namespace System.IO.Compression
{
	// Token: 0x0200001A RID: 26
	internal sealed class FastEncoder
	{
		// Token: 0x06000098 RID: 152 RVA: 0x000042EB File Offset: 0x000024EB
		public FastEncoder()
		{
			this._inputWindow = new FastEncoderWindow();
			this._currentMatch = new Match();
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004309 File Offset: 0x00002509
		internal int BytesInHistory
		{
			get
			{
				return this._inputWindow.BytesAvailable;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004316 File Offset: 0x00002516
		internal DeflateInput UnprocessedInput
		{
			get
			{
				return this._inputWindow.UnprocessedInput;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004323 File Offset: 0x00002523
		internal void FlushInput()
		{
			this._inputWindow.FlushWindow();
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004330 File Offset: 0x00002530
		internal double LastCompressionRatio
		{
			get
			{
				return this._lastCompressionRatio;
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004338 File Offset: 0x00002538
		internal void GetBlock(DeflateInput input, OutputBuffer output, int maxBytesToCopy)
		{
			FastEncoder.WriteDeflatePreamble(output);
			this.GetCompressedOutput(input, output, maxBytesToCopy);
			this.WriteEndOfBlock(output);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004350 File Offset: 0x00002550
		internal void GetCompressedData(DeflateInput input, OutputBuffer output)
		{
			this.GetCompressedOutput(input, output, -1);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000435B File Offset: 0x0000255B
		internal void GetBlockHeader(OutputBuffer output)
		{
			FastEncoder.WriteDeflatePreamble(output);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004363 File Offset: 0x00002563
		internal void GetBlockFooter(OutputBuffer output)
		{
			this.WriteEndOfBlock(output);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000436C File Offset: 0x0000256C
		private void GetCompressedOutput(DeflateInput input, OutputBuffer output, int maxBytesToCopy)
		{
			int bytesWritten = output.BytesWritten;
			int num = 0;
			int num2 = this.BytesInHistory + input.Count;
			do
			{
				int num3 = (input.Count < this._inputWindow.FreeWindowSpace) ? input.Count : this._inputWindow.FreeWindowSpace;
				if (maxBytesToCopy >= 1)
				{
					num3 = Math.Min(num3, maxBytesToCopy - num);
				}
				if (num3 > 0)
				{
					this._inputWindow.CopyBytes(input.Buffer, input.StartIndex, num3);
					input.ConsumeBytes(num3);
					num += num3;
				}
				this.GetCompressedOutput(output);
			}
			while (this.SafeToWriteTo(output) && this.InputAvailable(input) && (maxBytesToCopy < 1 || num < maxBytesToCopy));
			int num4 = output.BytesWritten - bytesWritten;
			int num5 = this.BytesInHistory + input.Count;
			int num6 = num2 - num5;
			if (num4 != 0)
			{
				this._lastCompressionRatio = (double)num4 / (double)num6;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004444 File Offset: 0x00002644
		private void GetCompressedOutput(OutputBuffer output)
		{
			while (this._inputWindow.BytesAvailable > 0 && this.SafeToWriteTo(output))
			{
				this._inputWindow.GetNextSymbolOrMatch(this._currentMatch);
				if (this._currentMatch.State == MatchState.HasSymbol)
				{
					FastEncoder.WriteChar(this._currentMatch.Symbol, output);
				}
				else if (this._currentMatch.State == MatchState.HasMatch)
				{
					FastEncoder.WriteMatch(this._currentMatch.Length, this._currentMatch.Position, output);
				}
				else
				{
					FastEncoder.WriteChar(this._currentMatch.Symbol, output);
					FastEncoder.WriteMatch(this._currentMatch.Length, this._currentMatch.Position, output);
				}
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000044FC File Offset: 0x000026FC
		private bool InputAvailable(DeflateInput input)
		{
			return input.Count > 0 || this.BytesInHistory > 0;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004512 File Offset: 0x00002712
		private bool SafeToWriteTo(OutputBuffer output)
		{
			return output.FreeBytes > 16;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004520 File Offset: 0x00002720
		private void WriteEndOfBlock(OutputBuffer output)
		{
			uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[256];
			int n = (int)(num & 31U);
			output.WriteBits(n, num >> 5);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004548 File Offset: 0x00002748
		internal static void WriteMatch(int matchLen, int matchPos, OutputBuffer output)
		{
			uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[254 + matchLen];
			int num2 = (int)(num & 31U);
			if (num2 <= 16)
			{
				output.WriteBits(num2, num >> 5);
			}
			else
			{
				output.WriteBits(16, num >> 5 & 65535U);
				output.WriteBits(num2 - 16, num >> 21);
			}
			num = FastEncoderStatics.FastEncoderDistanceCodeInfo[FastEncoderStatics.GetSlot(matchPos)];
			output.WriteBits((int)(num & 15U), num >> 8);
			int num3 = (int)(num >> 4 & 15U);
			if (num3 != 0)
			{
				output.WriteBits(num3, (uint)(matchPos & (int)FastEncoderStatics.BitMask[num3]));
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000045CC File Offset: 0x000027CC
		internal static void WriteChar(byte b, OutputBuffer output)
		{
			uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[(int)b];
			output.WriteBits((int)(num & 31U), num >> 5);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000045EE File Offset: 0x000027EE
		internal static void WriteDeflatePreamble(OutputBuffer output)
		{
			output.WriteBytes(FastEncoderStatics.FastEncoderTreeStructureData, 0, FastEncoderStatics.FastEncoderTreeStructureData.Length);
			output.WriteBits(9, 34U);
		}

		// Token: 0x040000E9 RID: 233
		private readonly FastEncoderWindow _inputWindow;

		// Token: 0x040000EA RID: 234
		private readonly Match _currentMatch;

		// Token: 0x040000EB RID: 235
		private double _lastCompressionRatio;
	}
}
