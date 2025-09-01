using System;

namespace System.IO.Compression
{
	// Token: 0x02000018 RID: 24
	internal sealed class DeflaterManaged : IDisposable
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00003F42 File Offset: 0x00002142
		internal DeflaterManaged()
		{
			this._deflateEncoder = new FastEncoder();
			this._copyEncoder = new CopyEncoder();
			this._input = new DeflateInput();
			this._output = new OutputBuffer();
			this._processingState = DeflaterManaged.DeflaterState.NotStarted;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003F7D File Offset: 0x0000217D
		internal bool NeedsInput()
		{
			return this._input.Count == 0 && this._deflateEncoder.BytesInHistory == 0;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003F9C File Offset: 0x0000219C
		internal void SetInput(byte[] inputBuffer, int startIndex, int count)
		{
			this._input.Buffer = inputBuffer;
			this._input.Count = count;
			this._input.StartIndex = startIndex;
			if (count > 0 && count < 256)
			{
				DeflaterManaged.DeflaterState processingState = this._processingState;
				if (processingState != DeflaterManaged.DeflaterState.NotStarted)
				{
					if (processingState == DeflaterManaged.DeflaterState.CompressThenCheck)
					{
						this._processingState = DeflaterManaged.DeflaterState.HandlingSmallData;
						return;
					}
					if (processingState != DeflaterManaged.DeflaterState.CheckingForIncompressible)
					{
						return;
					}
				}
				this._processingState = DeflaterManaged.DeflaterState.StartingSmallData;
				return;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003FFC File Offset: 0x000021FC
		internal int GetDeflateOutput(byte[] outputBuffer)
		{
			this._output.UpdateBuffer(outputBuffer);
			switch (this._processingState)
			{
			case DeflaterManaged.DeflaterState.NotStarted:
			{
				DeflateInput.InputState state = this._input.DumpState();
				OutputBuffer.BufferState state2 = this._output.DumpState();
				this._deflateEncoder.GetBlockHeader(this._output);
				this._deflateEncoder.GetCompressedData(this._input, this._output);
				if (!this.UseCompressed(this._deflateEncoder.LastCompressionRatio))
				{
					this._input.RestoreState(state);
					this._output.RestoreState(state2);
					this._copyEncoder.GetBlock(this._input, this._output, false);
					this.FlushInputWindows();
					this._processingState = DeflaterManaged.DeflaterState.CheckingForIncompressible;
					goto IL_23A;
				}
				this._processingState = DeflaterManaged.DeflaterState.CompressThenCheck;
				goto IL_23A;
			}
			case DeflaterManaged.DeflaterState.SlowDownForIncompressible1:
				this._deflateEncoder.GetBlockFooter(this._output);
				this._processingState = DeflaterManaged.DeflaterState.SlowDownForIncompressible2;
				break;
			case DeflaterManaged.DeflaterState.SlowDownForIncompressible2:
				break;
			case DeflaterManaged.DeflaterState.StartingSmallData:
				this._deflateEncoder.GetBlockHeader(this._output);
				this._processingState = DeflaterManaged.DeflaterState.HandlingSmallData;
				goto IL_223;
			case DeflaterManaged.DeflaterState.CompressThenCheck:
				this._deflateEncoder.GetCompressedData(this._input, this._output);
				if (!this.UseCompressed(this._deflateEncoder.LastCompressionRatio))
				{
					this._processingState = DeflaterManaged.DeflaterState.SlowDownForIncompressible1;
					this._inputFromHistory = this._deflateEncoder.UnprocessedInput;
					goto IL_23A;
				}
				goto IL_23A;
			case DeflaterManaged.DeflaterState.CheckingForIncompressible:
			{
				DeflateInput.InputState state3 = this._input.DumpState();
				OutputBuffer.BufferState state4 = this._output.DumpState();
				this._deflateEncoder.GetBlock(this._input, this._output, 8072);
				if (!this.UseCompressed(this._deflateEncoder.LastCompressionRatio))
				{
					this._input.RestoreState(state3);
					this._output.RestoreState(state4);
					this._copyEncoder.GetBlock(this._input, this._output, false);
					this.FlushInputWindows();
					goto IL_23A;
				}
				goto IL_23A;
			}
			case DeflaterManaged.DeflaterState.HandlingSmallData:
				goto IL_223;
			default:
				goto IL_23A;
			}
			if (this._inputFromHistory.Count > 0)
			{
				this._copyEncoder.GetBlock(this._inputFromHistory, this._output, false);
			}
			if (this._inputFromHistory.Count == 0)
			{
				this._deflateEncoder.FlushInput();
				this._processingState = DeflaterManaged.DeflaterState.CheckingForIncompressible;
				goto IL_23A;
			}
			goto IL_23A;
			IL_223:
			this._deflateEncoder.GetCompressedData(this._input, this._output);
			IL_23A:
			return this._output.BytesWritten;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004250 File Offset: 0x00002450
		internal bool Finish(byte[] outputBuffer, out int bytesRead)
		{
			if (this._processingState == DeflaterManaged.DeflaterState.NotStarted)
			{
				bytesRead = 0;
				return true;
			}
			this._output.UpdateBuffer(outputBuffer);
			if (this._processingState == DeflaterManaged.DeflaterState.CompressThenCheck || this._processingState == DeflaterManaged.DeflaterState.HandlingSmallData || this._processingState == DeflaterManaged.DeflaterState.SlowDownForIncompressible1)
			{
				this._deflateEncoder.GetBlockFooter(this._output);
			}
			this.WriteFinal();
			bytesRead = this._output.BytesWritten;
			return true;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000042B6 File Offset: 0x000024B6
		private bool UseCompressed(double ratio)
		{
			return ratio <= 1.0;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000042C7 File Offset: 0x000024C7
		private void FlushInputWindows()
		{
			this._deflateEncoder.FlushInput();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000042D4 File Offset: 0x000024D4
		private void WriteFinal()
		{
			this._copyEncoder.GetBlock(null, this._output, true);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000042E9 File Offset: 0x000024E9
		public void Dispose()
		{
		}

		// Token: 0x040000D7 RID: 215
		private const int MinBlockSize = 256;

		// Token: 0x040000D8 RID: 216
		private const int MaxHeaderFooterGoo = 120;

		// Token: 0x040000D9 RID: 217
		private const int CleanCopySize = 8072;

		// Token: 0x040000DA RID: 218
		private const double BadCompressionThreshold = 1.0;

		// Token: 0x040000DB RID: 219
		private readonly FastEncoder _deflateEncoder;

		// Token: 0x040000DC RID: 220
		private readonly CopyEncoder _copyEncoder;

		// Token: 0x040000DD RID: 221
		private readonly DeflateInput _input;

		// Token: 0x040000DE RID: 222
		private readonly OutputBuffer _output;

		// Token: 0x040000DF RID: 223
		private DeflaterManaged.DeflaterState _processingState;

		// Token: 0x040000E0 RID: 224
		private DeflateInput _inputFromHistory;

		// Token: 0x02000019 RID: 25
		private enum DeflaterState
		{
			// Token: 0x040000E2 RID: 226
			NotStarted,
			// Token: 0x040000E3 RID: 227
			SlowDownForIncompressible1,
			// Token: 0x040000E4 RID: 228
			SlowDownForIncompressible2,
			// Token: 0x040000E5 RID: 229
			StartingSmallData,
			// Token: 0x040000E6 RID: 230
			CompressThenCheck,
			// Token: 0x040000E7 RID: 231
			CheckingForIncompressible,
			// Token: 0x040000E8 RID: 232
			HandlingSmallData
		}
	}
}
