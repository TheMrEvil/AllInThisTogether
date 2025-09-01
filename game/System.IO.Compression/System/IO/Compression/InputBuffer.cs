using System;

namespace System.IO.Compression
{
	// Token: 0x02000022 RID: 34
	internal sealed class InputBuffer
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005B64 File Offset: 0x00003D64
		public int AvailableBits
		{
			get
			{
				return this._bitsInBuffer;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005B6C File Offset: 0x00003D6C
		public int AvailableBytes
		{
			get
			{
				return this._end - this._start + this._bitsInBuffer / 8;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005B84 File Offset: 0x00003D84
		public bool EnsureBitsAvailable(int count)
		{
			if (this._bitsInBuffer < count)
			{
				if (this.NeedsInput())
				{
					return false;
				}
				uint bitBuffer = this._bitBuffer;
				byte[] buffer = this._buffer;
				int start = this._start;
				this._start = start + 1;
				this._bitBuffer = (bitBuffer | buffer[start] << (this._bitsInBuffer & 31));
				this._bitsInBuffer += 8;
				if (this._bitsInBuffer < count)
				{
					if (this.NeedsInput())
					{
						return false;
					}
					uint bitBuffer2 = this._bitBuffer;
					byte[] buffer2 = this._buffer;
					start = this._start;
					this._start = start + 1;
					this._bitBuffer = (bitBuffer2 | buffer2[start] << (this._bitsInBuffer & 31));
					this._bitsInBuffer += 8;
				}
			}
			return true;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005C38 File Offset: 0x00003E38
		public uint TryLoad16Bits()
		{
			if (this._bitsInBuffer < 8)
			{
				if (this._start < this._end)
				{
					uint bitBuffer = this._bitBuffer;
					byte[] buffer = this._buffer;
					int start = this._start;
					this._start = start + 1;
					this._bitBuffer = (bitBuffer | buffer[start] << (this._bitsInBuffer & 31));
					this._bitsInBuffer += 8;
				}
				if (this._start < this._end)
				{
					uint bitBuffer2 = this._bitBuffer;
					byte[] buffer2 = this._buffer;
					int start = this._start;
					this._start = start + 1;
					this._bitBuffer = (bitBuffer2 | buffer2[start] << (this._bitsInBuffer & 31));
					this._bitsInBuffer += 8;
				}
			}
			else if (this._bitsInBuffer < 16 && this._start < this._end)
			{
				uint bitBuffer3 = this._bitBuffer;
				byte[] buffer3 = this._buffer;
				int start = this._start;
				this._start = start + 1;
				this._bitBuffer = (bitBuffer3 | buffer3[start] << (this._bitsInBuffer & 31));
				this._bitsInBuffer += 8;
			}
			return this._bitBuffer;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005D47 File Offset: 0x00003F47
		private uint GetBitMask(int count)
		{
			return (1U << count) - 1U;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005D51 File Offset: 0x00003F51
		public int GetBits(int count)
		{
			if (!this.EnsureBitsAvailable(count))
			{
				return -1;
			}
			int result = (int)(this._bitBuffer & this.GetBitMask(count));
			this._bitBuffer >>= count;
			this._bitsInBuffer -= count;
			return result;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005D8C File Offset: 0x00003F8C
		public int CopyTo(byte[] output, int offset, int length)
		{
			int num = 0;
			while (this._bitsInBuffer > 0 && length > 0)
			{
				output[offset++] = (byte)this._bitBuffer;
				this._bitBuffer >>= 8;
				this._bitsInBuffer -= 8;
				length--;
				num++;
			}
			if (length == 0)
			{
				return num;
			}
			int num2 = this._end - this._start;
			if (length > num2)
			{
				length = num2;
			}
			Array.Copy(this._buffer, this._start, output, offset, length);
			this._start += length;
			return num + length;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005E1D File Offset: 0x0000401D
		public bool NeedsInput()
		{
			return this._start == this._end;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005E2D File Offset: 0x0000402D
		public void SetInput(byte[] buffer, int offset, int length)
		{
			this._buffer = buffer;
			this._start = offset;
			this._end = offset + length;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005E46 File Offset: 0x00004046
		public void SkipBits(int n)
		{
			this._bitBuffer >>= n;
			this._bitsInBuffer -= n;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005E67 File Offset: 0x00004067
		public void SkipToByteBoundary()
		{
			this._bitBuffer >>= this._bitsInBuffer % 8;
			this._bitsInBuffer -= this._bitsInBuffer % 8;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000353B File Offset: 0x0000173B
		public InputBuffer()
		{
		}

		// Token: 0x04000150 RID: 336
		private byte[] _buffer;

		// Token: 0x04000151 RID: 337
		private int _start;

		// Token: 0x04000152 RID: 338
		private int _end;

		// Token: 0x04000153 RID: 339
		private uint _bitBuffer;

		// Token: 0x04000154 RID: 340
		private int _bitsInBuffer;
	}
}
