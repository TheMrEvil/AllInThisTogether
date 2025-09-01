using System;
using System.IO;
using System.Text;

namespace Mono.CSharp
{
	// Token: 0x020002CC RID: 716
	public class SeekableStreamReader : IDisposable
	{
		// Token: 0x06002257 RID: 8791 RVA: 0x000A807D File Offset: 0x000A627D
		public SeekableStreamReader(Stream stream, Encoding encoding, char[] sharedBuffer = null)
		{
			this.stream = stream;
			this.buffer = sharedBuffer;
			this.InitializeStream(2048);
			this.reader = new StreamReader(stream, encoding, true);
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000A80AC File Offset: 0x000A62AC
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000A80BC File Offset: 0x000A62BC
		private void InitializeStream(int read_length_inc)
		{
			this.read_ahead_length += read_length_inc;
			int num = this.read_ahead_length * 2;
			if (this.buffer == null || this.buffer.Length < num)
			{
				this.buffer = new char[num];
			}
			this.stream.Position = 0L;
			this.buffer_start = (this.char_count = (this.pos = 0));
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x000A8125 File Offset: 0x000A6325
		// (set) Token: 0x0600225B RID: 8795 RVA: 0x000A8134 File Offset: 0x000A6334
		public int Position
		{
			get
			{
				return this.buffer_start + this.pos;
			}
			set
			{
				if (value < this.buffer_start)
				{
					this.InitializeStream(this.read_ahead_length);
					this.reader = new StreamReader(this.stream, this.reader.CurrentEncoding, true);
				}
				while (value > this.buffer_start + this.char_count)
				{
					this.pos = this.char_count;
					if (!this.ReadBuffer())
					{
						throw new InternalErrorException("Seek beyond end of file: " + (this.buffer_start + this.char_count - value));
					}
				}
				this.pos = value - this.buffer_start;
			}
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000A81CC File Offset: 0x000A63CC
		private bool ReadBuffer()
		{
			int num = this.buffer.Length - this.char_count;
			if (num <= this.read_ahead_length)
			{
				int num2 = this.read_ahead_length - num;
				Array.Copy(this.buffer, num2, this.buffer, 0, this.char_count - num2);
				this.pos -= num2;
				this.char_count -= num2;
				this.buffer_start += num2;
				num += num2;
			}
			this.char_count += this.reader.Read(this.buffer, this.char_count, num);
			return this.pos < this.char_count;
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000A8278 File Offset: 0x000A6478
		public char[] ReadChars(int fromPosition, int toPosition)
		{
			char[] array = new char[toPosition - fromPosition];
			if (this.buffer_start <= fromPosition && toPosition <= this.buffer_start + this.buffer.Length)
			{
				Array.Copy(this.buffer, fromPosition - this.buffer_start, array, 0, array.Length);
				return array;
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000A82CA File Offset: 0x000A64CA
		public int Peek()
		{
			if (this.pos >= this.char_count && !this.ReadBuffer())
			{
				return -1;
			}
			return (int)this.buffer[this.pos];
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000A82F4 File Offset: 0x000A64F4
		public int Read()
		{
			if (this.pos >= this.char_count && !this.ReadBuffer())
			{
				return -1;
			}
			char[] array = this.buffer;
			int num = this.pos;
			this.pos = num + 1;
			return array[num];
		}

		// Token: 0x04000CA6 RID: 3238
		public const int DefaultReadAheadSize = 2048;

		// Token: 0x04000CA7 RID: 3239
		private StreamReader reader;

		// Token: 0x04000CA8 RID: 3240
		private Stream stream;

		// Token: 0x04000CA9 RID: 3241
		private char[] buffer;

		// Token: 0x04000CAA RID: 3242
		private int read_ahead_length;

		// Token: 0x04000CAB RID: 3243
		private int buffer_start;

		// Token: 0x04000CAC RID: 3244
		private int char_count;

		// Token: 0x04000CAD RID: 3245
		private int pos;
	}
}
