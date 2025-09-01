using System;

namespace System.IO.Compression
{
	// Token: 0x02000027 RID: 39
	internal sealed class OutputWindow
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00006138 File Offset: 0x00004338
		public void Write(byte b)
		{
			byte[] window = this._window;
			int end = this._end;
			this._end = end + 1;
			window[end] = b;
			this._end &= 262143;
			this._bytesUsed++;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006180 File Offset: 0x00004380
		public void WriteLengthDistance(int length, int distance)
		{
			this._bytesUsed += length;
			int num = this._end - distance & 262143;
			int num2 = 262144 - length;
			if (num > num2 || this._end >= num2)
			{
				while (length-- > 0)
				{
					byte[] window = this._window;
					int end = this._end;
					this._end = end + 1;
					window[end] = this._window[num++];
					this._end &= 262143;
					num &= 262143;
				}
				return;
			}
			if (length <= distance)
			{
				Array.Copy(this._window, num, this._window, this._end, length);
				this._end += length;
				return;
			}
			while (length-- > 0)
			{
				byte[] window2 = this._window;
				int end = this._end;
				this._end = end + 1;
				window2[end] = this._window[num++];
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006268 File Offset: 0x00004468
		public int CopyFrom(InputBuffer input, int length)
		{
			length = Math.Min(Math.Min(length, 262144 - this._bytesUsed), input.AvailableBytes);
			int num = 262144 - this._end;
			int num2;
			if (length > num)
			{
				num2 = input.CopyTo(this._window, this._end, num);
				if (num2 == num)
				{
					num2 += input.CopyTo(this._window, 0, length - num);
				}
			}
			else
			{
				num2 = input.CopyTo(this._window, this._end, length);
			}
			this._end = (this._end + num2 & 262143);
			this._bytesUsed += num2;
			return num2;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00006309 File Offset: 0x00004509
		public int FreeBytes
		{
			get
			{
				return 262144 - this._bytesUsed;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00006317 File Offset: 0x00004517
		public int AvailableBytes
		{
			get
			{
				return this._bytesUsed;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006320 File Offset: 0x00004520
		public int CopyTo(byte[] output, int offset, int length)
		{
			int num;
			if (length > this._bytesUsed)
			{
				num = this._end;
				length = this._bytesUsed;
			}
			else
			{
				num = (this._end - this._bytesUsed + length & 262143);
			}
			int num2 = length;
			int num3 = length - num;
			if (num3 > 0)
			{
				Array.Copy(this._window, 262144 - num3, output, offset, num3);
				offset += num3;
				length = num;
			}
			Array.Copy(this._window, num - length, output, offset, length);
			this._bytesUsed -= num2;
			return num2;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000063A4 File Offset: 0x000045A4
		public OutputWindow()
		{
		}

		// Token: 0x04000164 RID: 356
		private const int WindowSize = 262144;

		// Token: 0x04000165 RID: 357
		private const int WindowMask = 262143;

		// Token: 0x04000166 RID: 358
		private readonly byte[] _window = new byte[262144];

		// Token: 0x04000167 RID: 359
		private int _end;

		// Token: 0x04000168 RID: 360
		private int _bytesUsed;
	}
}
