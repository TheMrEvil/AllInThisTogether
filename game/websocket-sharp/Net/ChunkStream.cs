using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace WebSocketSharp.Net
{
	// Token: 0x02000019 RID: 25
	internal class ChunkStream
	{
		// Token: 0x06000195 RID: 405 RVA: 0x0000B13B File Offset: 0x0000933B
		public ChunkStream(WebHeaderCollection headers)
		{
			this._headers = headers;
			this._chunkSize = -1;
			this._chunks = new List<Chunk>();
			this._saved = new StringBuilder();
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000B16C File Offset: 0x0000936C
		internal int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000B184 File Offset: 0x00009384
		internal byte[] EndBuffer
		{
			get
			{
				return this._endBuffer;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000B19C File Offset: 0x0000939C
		internal int Offset
		{
			get
			{
				return this._offset;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000B1B4 File Offset: 0x000093B4
		public WebHeaderCollection Headers
		{
			get
			{
				return this._headers;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000B1CC File Offset: 0x000093CC
		public bool WantsMore
		{
			get
			{
				return this._state < InputChunkState.End;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000B1E8 File Offset: 0x000093E8
		private int read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			int count2 = this._chunks.Count;
			for (int i = 0; i < count2; i++)
			{
				Chunk chunk = this._chunks[i];
				bool flag = chunk == null;
				if (!flag)
				{
					bool flag2 = chunk.ReadLeft == 0;
					if (flag2)
					{
						this._chunks[i] = null;
					}
					else
					{
						num += chunk.Read(buffer, offset + num, count - num);
						bool flag3 = num == count;
						if (flag3)
						{
							break;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000B274 File Offset: 0x00009474
		private InputChunkState seekCrLf(byte[] buffer, ref int offset, int length)
		{
			bool flag = !this._sawCr;
			int num;
			if (flag)
			{
				num = offset;
				offset = num + 1;
				bool flag2 = buffer[num] != 13;
				if (flag2)
				{
					ChunkStream.throwProtocolViolation("CR is expected.");
				}
				this._sawCr = true;
				bool flag3 = offset == length;
				if (flag3)
				{
					return InputChunkState.DataEnded;
				}
			}
			num = offset;
			offset = num + 1;
			bool flag4 = buffer[num] != 10;
			if (flag4)
			{
				ChunkStream.throwProtocolViolation("LF is expected.");
			}
			return InputChunkState.None;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000B2F4 File Offset: 0x000094F4
		private InputChunkState setChunkSize(byte[] buffer, ref int offset, int length)
		{
			byte b = 0;
			while (offset < length)
			{
				int num = offset;
				offset = num + 1;
				b = buffer[num];
				bool sawCr = this._sawCr;
				if (sawCr)
				{
					bool flag = b != 10;
					if (flag)
					{
						ChunkStream.throwProtocolViolation("LF is expected.");
					}
					break;
				}
				bool flag2 = b == 13;
				if (flag2)
				{
					this._sawCr = true;
				}
				else
				{
					bool flag3 = b == 10;
					if (flag3)
					{
						ChunkStream.throwProtocolViolation("LF is unexpected.");
					}
					bool gotIt = this._gotIt;
					if (!gotIt)
					{
						bool flag4 = b == 32 || b == 59;
						if (flag4)
						{
							this._gotIt = true;
						}
						else
						{
							this._saved.Append((char)b);
						}
					}
				}
			}
			bool flag5 = this._saved.Length > 20;
			if (flag5)
			{
				ChunkStream.throwProtocolViolation("The chunk size is too big.");
			}
			bool flag6 = b != 10;
			InputChunkState result;
			if (flag6)
			{
				result = InputChunkState.None;
			}
			else
			{
				string s = this._saved.ToString();
				try
				{
					this._chunkSize = int.Parse(s, NumberStyles.HexNumber);
				}
				catch
				{
					ChunkStream.throwProtocolViolation("The chunk size cannot be parsed.");
				}
				this._chunkRead = 0;
				bool flag7 = this._chunkSize == 0;
				if (flag7)
				{
					this._trailerState = 2;
					result = InputChunkState.Trailer;
				}
				else
				{
					result = InputChunkState.Data;
				}
			}
			return result;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000B44C File Offset: 0x0000964C
		private InputChunkState setTrailer(byte[] buffer, ref int offset, int length)
		{
			while (offset < length)
			{
				bool flag = this._trailerState == 4;
				if (flag)
				{
					break;
				}
				int num = offset;
				offset = num + 1;
				byte b = buffer[num];
				this._saved.Append((char)b);
				bool flag2 = this._trailerState == 1 || this._trailerState == 3;
				if (flag2)
				{
					bool flag3 = b != 10;
					if (flag3)
					{
						ChunkStream.throwProtocolViolation("LF is expected.");
					}
					this._trailerState++;
				}
				else
				{
					bool flag4 = b == 13;
					if (flag4)
					{
						this._trailerState++;
					}
					else
					{
						bool flag5 = b == 10;
						if (flag5)
						{
							ChunkStream.throwProtocolViolation("LF is unexpected.");
						}
						this._trailerState = 0;
					}
				}
			}
			int length2 = this._saved.Length;
			bool flag6 = length2 > 4196;
			if (flag6)
			{
				ChunkStream.throwProtocolViolation("The trailer is too long.");
			}
			bool flag7 = this._trailerState < 4;
			InputChunkState result;
			if (flag7)
			{
				result = InputChunkState.Trailer;
			}
			else
			{
				bool flag8 = length2 == 2;
				if (flag8)
				{
					result = InputChunkState.End;
				}
				else
				{
					this._saved.Length = length2 - 2;
					string s = this._saved.ToString();
					StringReader stringReader = new StringReader(s);
					for (;;)
					{
						string text = stringReader.ReadLine();
						bool flag9 = text == null || text.Length == 0;
						if (flag9)
						{
							break;
						}
						this._headers.Add(text);
					}
					result = InputChunkState.End;
				}
			}
			return result;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000B5C6 File Offset: 0x000097C6
		private static void throwProtocolViolation(string message)
		{
			throw new WebException(message, null, WebExceptionStatus.ServerProtocolViolation, null);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000B5D4 File Offset: 0x000097D4
		private void write(byte[] buffer, int offset, int length)
		{
			bool flag = this._state == InputChunkState.End;
			if (flag)
			{
				ChunkStream.throwProtocolViolation("The chunks were ended.");
			}
			bool flag2 = this._state == InputChunkState.None;
			if (flag2)
			{
				this._state = this.setChunkSize(buffer, ref offset, length);
				bool flag3 = this._state == InputChunkState.None;
				if (flag3)
				{
					return;
				}
				this._saved.Length = 0;
				this._sawCr = false;
				this._gotIt = false;
			}
			bool flag4 = this._state == InputChunkState.Data;
			if (flag4)
			{
				bool flag5 = offset >= length;
				if (flag5)
				{
					return;
				}
				this._state = this.writeData(buffer, ref offset, length);
				bool flag6 = this._state == InputChunkState.Data;
				if (flag6)
				{
					return;
				}
			}
			bool flag7 = this._state == InputChunkState.DataEnded;
			if (flag7)
			{
				bool flag8 = offset >= length;
				if (flag8)
				{
					return;
				}
				this._state = this.seekCrLf(buffer, ref offset, length);
				bool flag9 = this._state == InputChunkState.DataEnded;
				if (flag9)
				{
					return;
				}
				this._sawCr = false;
			}
			bool flag10 = this._state == InputChunkState.Trailer;
			if (flag10)
			{
				bool flag11 = offset >= length;
				if (flag11)
				{
					return;
				}
				this._state = this.setTrailer(buffer, ref offset, length);
				bool flag12 = this._state == InputChunkState.Trailer;
				if (flag12)
				{
					return;
				}
				this._saved.Length = 0;
			}
			bool flag13 = this._state == InputChunkState.End;
			if (flag13)
			{
				this._endBuffer = buffer;
				this._offset = offset;
				this._count = length - offset;
			}
			else
			{
				bool flag14 = offset >= length;
				if (!flag14)
				{
					this.write(buffer, offset, length);
				}
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000B76C File Offset: 0x0000996C
		private InputChunkState writeData(byte[] buffer, ref int offset, int length)
		{
			int num = length - offset;
			int num2 = this._chunkSize - this._chunkRead;
			bool flag = num > num2;
			if (flag)
			{
				num = num2;
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(buffer, offset, array, 0, num);
			Chunk item = new Chunk(array);
			this._chunks.Add(item);
			offset += num;
			this._chunkRead += num;
			return (this._chunkRead == this._chunkSize) ? InputChunkState.DataEnded : InputChunkState.Data;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000B7EC File Offset: 0x000099EC
		internal void ResetChunkStore()
		{
			this._chunkRead = 0;
			this._chunkSize = -1;
			this._chunks.Clear();
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000B80C File Offset: 0x00009A0C
		public int Read(byte[] buffer, int offset, int count)
		{
			bool flag = count <= 0;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = this.read(buffer, offset, count);
			}
			return result;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000B838 File Offset: 0x00009A38
		public void Write(byte[] buffer, int offset, int count)
		{
			bool flag = count <= 0;
			if (!flag)
			{
				this.write(buffer, offset, offset + count);
			}
		}

		// Token: 0x04000099 RID: 153
		private int _chunkRead;

		// Token: 0x0400009A RID: 154
		private int _chunkSize;

		// Token: 0x0400009B RID: 155
		private List<Chunk> _chunks;

		// Token: 0x0400009C RID: 156
		private int _count;

		// Token: 0x0400009D RID: 157
		private byte[] _endBuffer;

		// Token: 0x0400009E RID: 158
		private bool _gotIt;

		// Token: 0x0400009F RID: 159
		private WebHeaderCollection _headers;

		// Token: 0x040000A0 RID: 160
		private int _offset;

		// Token: 0x040000A1 RID: 161
		private StringBuilder _saved;

		// Token: 0x040000A2 RID: 162
		private bool _sawCr;

		// Token: 0x040000A3 RID: 163
		private InputChunkState _state;

		// Token: 0x040000A4 RID: 164
		private int _trailerState;
	}
}
