using System;

namespace System.IO.Compression
{
	// Token: 0x02000025 RID: 37
	internal sealed class OutputBuffer
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00005EDA File Offset: 0x000040DA
		internal void UpdateBuffer(byte[] output)
		{
			this._byteBuffer = output;
			this._pos = 0;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005EEA File Offset: 0x000040EA
		internal int BytesWritten
		{
			get
			{
				return this._pos;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00005EF2 File Offset: 0x000040F2
		internal int FreeBytes
		{
			get
			{
				return this._byteBuffer.Length - this._pos;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005F04 File Offset: 0x00004104
		internal void WriteUInt16(ushort value)
		{
			byte[] byteBuffer = this._byteBuffer;
			int pos = this._pos;
			this._pos = pos + 1;
			byteBuffer[pos] = (byte)value;
			byte[] byteBuffer2 = this._byteBuffer;
			pos = this._pos;
			this._pos = pos + 1;
			byteBuffer2[pos] = (byte)(value >> 8);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005F48 File Offset: 0x00004148
		internal void WriteBits(int n, uint bits)
		{
			this._bitBuf |= bits << this._bitCount;
			this._bitCount += n;
			if (this._bitCount >= 16)
			{
				byte[] byteBuffer = this._byteBuffer;
				int pos = this._pos;
				this._pos = pos + 1;
				byteBuffer[pos] = (byte)this._bitBuf;
				byte[] byteBuffer2 = this._byteBuffer;
				pos = this._pos;
				this._pos = pos + 1;
				byteBuffer2[pos] = (byte)(this._bitBuf >> 8);
				this._bitCount -= 16;
				this._bitBuf >>= 16;
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005FE4 File Offset: 0x000041E4
		internal void FlushBits()
		{
			while (this._bitCount >= 8)
			{
				byte[] byteBuffer = this._byteBuffer;
				int pos = this._pos;
				this._pos = pos + 1;
				byteBuffer[pos] = (byte)this._bitBuf;
				this._bitCount -= 8;
				this._bitBuf >>= 8;
			}
			if (this._bitCount > 0)
			{
				byte[] byteBuffer2 = this._byteBuffer;
				int pos = this._pos;
				this._pos = pos + 1;
				byteBuffer2[pos] = (byte)this._bitBuf;
				this._bitBuf = 0U;
				this._bitCount = 0;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000606D File Offset: 0x0000426D
		internal void WriteBytes(byte[] byteArray, int offset, int count)
		{
			if (this._bitCount == 0)
			{
				Array.Copy(byteArray, offset, this._byteBuffer, this._pos, count);
				this._pos += count;
				return;
			}
			this.WriteBytesUnaligned(byteArray, offset, count);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000060A4 File Offset: 0x000042A4
		private void WriteBytesUnaligned(byte[] byteArray, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				byte b = byteArray[offset + i];
				this.WriteByteUnaligned(b);
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000060CA File Offset: 0x000042CA
		private void WriteByteUnaligned(byte b)
		{
			this.WriteBits(8, (uint)b);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000060D4 File Offset: 0x000042D4
		internal int BitsInBuffer
		{
			get
			{
				return this._bitCount / 8 + 1;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000060E0 File Offset: 0x000042E0
		internal OutputBuffer.BufferState DumpState()
		{
			return new OutputBuffer.BufferState(this._pos, this._bitBuf, this._bitCount);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000060F9 File Offset: 0x000042F9
		internal void RestoreState(OutputBuffer.BufferState state)
		{
			this._pos = state._pos;
			this._bitBuf = state._bitBuf;
			this._bitCount = state._bitCount;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000353B File Offset: 0x0000173B
		public OutputBuffer()
		{
		}

		// Token: 0x0400015D RID: 349
		private byte[] _byteBuffer;

		// Token: 0x0400015E RID: 350
		private int _pos;

		// Token: 0x0400015F RID: 351
		private uint _bitBuf;

		// Token: 0x04000160 RID: 352
		private int _bitCount;

		// Token: 0x02000026 RID: 38
		internal readonly struct BufferState
		{
			// Token: 0x060000FA RID: 250 RVA: 0x0000611F File Offset: 0x0000431F
			internal BufferState(int pos, uint bitBuf, int bitCount)
			{
				this._pos = pos;
				this._bitBuf = bitBuf;
				this._bitCount = bitCount;
			}

			// Token: 0x04000161 RID: 353
			internal readonly int _pos;

			// Token: 0x04000162 RID: 354
			internal readonly uint _bitBuf;

			// Token: 0x04000163 RID: 355
			internal readonly int _bitCount;
		}
	}
}
