using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Net
{
	// Token: 0x020006A2 RID: 1698
	internal class MonoChunkParser
	{
		// Token: 0x06003662 RID: 13922 RVA: 0x000BEB9C File Offset: 0x000BCD9C
		public MonoChunkParser(WebHeaderCollection headers)
		{
			this.headers = headers;
			this.saved = new StringBuilder();
			this.chunks = new ArrayList();
			this.chunkSize = -1;
			this.totalWritten = 0;
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x000BEBCF File Offset: 0x000BCDCF
		public void WriteAndReadBack(byte[] buffer, int offset, int size, ref int read)
		{
			if (offset + read > 0)
			{
				this.Write(buffer, offset, offset + read);
			}
			read = this.Read(buffer, offset, size);
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000BEBF2 File Offset: 0x000BCDF2
		public int Read(byte[] buffer, int offset, int size)
		{
			return this.ReadFromChunks(buffer, offset, size);
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x000BEC00 File Offset: 0x000BCE00
		private int ReadFromChunks(byte[] buffer, int offset, int size)
		{
			int count = this.chunks.Count;
			int num = 0;
			List<MonoChunkParser.Chunk> list = new List<MonoChunkParser.Chunk>(count);
			for (int i = 0; i < count; i++)
			{
				MonoChunkParser.Chunk chunk = (MonoChunkParser.Chunk)this.chunks[i];
				if (chunk.Offset == chunk.Bytes.Length)
				{
					list.Add(chunk);
				}
				else
				{
					num += chunk.Read(buffer, offset + num, size - num);
					if (num == size)
					{
						break;
					}
				}
			}
			foreach (MonoChunkParser.Chunk obj in list)
			{
				this.chunks.Remove(obj);
			}
			return num;
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x000BECBC File Offset: 0x000BCEBC
		public void Write(byte[] buffer, int offset, int size)
		{
			if (offset < size)
			{
				this.InternalWrite(buffer, ref offset, size);
			}
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x000BECCC File Offset: 0x000BCECC
		private void InternalWrite(byte[] buffer, ref int offset, int size)
		{
			if (this.state == MonoChunkParser.State.None || this.state == MonoChunkParser.State.PartialSize)
			{
				this.state = this.GetChunkSize(buffer, ref offset, size);
				if (this.state == MonoChunkParser.State.PartialSize)
				{
					return;
				}
				this.saved.Length = 0;
				this.sawCR = false;
				this.gotit = false;
			}
			if (this.state == MonoChunkParser.State.Body && offset < size)
			{
				this.state = this.ReadBody(buffer, ref offset, size);
				if (this.state == MonoChunkParser.State.Body)
				{
					return;
				}
			}
			if (this.state == MonoChunkParser.State.BodyFinished && offset < size)
			{
				this.state = this.ReadCRLF(buffer, ref offset, size);
				if (this.state == MonoChunkParser.State.BodyFinished)
				{
					return;
				}
				this.sawCR = false;
			}
			if (this.state == MonoChunkParser.State.Trailer && offset < size)
			{
				this.state = this.ReadTrailer(buffer, ref offset, size);
				if (this.state == MonoChunkParser.State.Trailer)
				{
					return;
				}
				this.saved.Length = 0;
				this.sawCR = false;
				this.gotit = false;
			}
			if (offset < size)
			{
				this.InternalWrite(buffer, ref offset, size);
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06003668 RID: 13928 RVA: 0x000BEDC1 File Offset: 0x000BCFC1
		public bool WantMore
		{
			get
			{
				return this.chunkRead != this.chunkSize || this.chunkSize != 0 || this.state > MonoChunkParser.State.None;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06003669 RID: 13929 RVA: 0x000BEDE4 File Offset: 0x000BCFE4
		public bool DataAvailable
		{
			get
			{
				int count = this.chunks.Count;
				for (int i = 0; i < count; i++)
				{
					MonoChunkParser.Chunk chunk = (MonoChunkParser.Chunk)this.chunks[i];
					if (chunk != null && chunk.Bytes != null && chunk.Bytes.Length != 0 && chunk.Offset < chunk.Bytes.Length)
					{
						return this.state != MonoChunkParser.State.Body;
					}
				}
				return false;
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x0600366A RID: 13930 RVA: 0x000BEE4D File Offset: 0x000BD04D
		public int TotalDataSize
		{
			get
			{
				return this.totalWritten;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x0600366B RID: 13931 RVA: 0x000BEE55 File Offset: 0x000BD055
		public int ChunkLeft
		{
			get
			{
				return this.chunkSize - this.chunkRead;
			}
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x000BEE64 File Offset: 0x000BD064
		private MonoChunkParser.State ReadBody(byte[] buffer, ref int offset, int size)
		{
			if (this.chunkSize == 0)
			{
				return MonoChunkParser.State.BodyFinished;
			}
			int num = size - offset;
			if (num + this.chunkRead > this.chunkSize)
			{
				num = this.chunkSize - this.chunkRead;
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(buffer, offset, array, 0, num);
			this.chunks.Add(new MonoChunkParser.Chunk(array));
			offset += num;
			this.chunkRead += num;
			this.totalWritten += num;
			if (this.chunkRead != this.chunkSize)
			{
				return MonoChunkParser.State.Body;
			}
			return MonoChunkParser.State.BodyFinished;
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000BEEF8 File Offset: 0x000BD0F8
		private MonoChunkParser.State GetChunkSize(byte[] buffer, ref int offset, int size)
		{
			this.chunkRead = 0;
			this.chunkSize = 0;
			char c = '\0';
			while (offset < size)
			{
				int num = offset;
				offset = num + 1;
				c = (char)buffer[num];
				if (c == '\r')
				{
					if (this.sawCR)
					{
						MonoChunkParser.ThrowProtocolViolation("2 CR found");
					}
					this.sawCR = true;
				}
				else
				{
					if (this.sawCR && c == '\n')
					{
						break;
					}
					if (c == ' ')
					{
						this.gotit = true;
					}
					if (!this.gotit)
					{
						this.saved.Append(c);
					}
					if (this.saved.Length > 20)
					{
						MonoChunkParser.ThrowProtocolViolation("chunk size too long.");
					}
				}
			}
			if (!this.sawCR || c != '\n')
			{
				if (offset < size)
				{
					MonoChunkParser.ThrowProtocolViolation("Missing \\n");
				}
				try
				{
					if (this.saved.Length > 0)
					{
						this.chunkSize = int.Parse(MonoChunkParser.RemoveChunkExtension(this.saved.ToString()), NumberStyles.HexNumber);
					}
				}
				catch (Exception)
				{
					MonoChunkParser.ThrowProtocolViolation("Cannot parse chunk size.");
				}
				return MonoChunkParser.State.PartialSize;
			}
			this.chunkRead = 0;
			try
			{
				this.chunkSize = int.Parse(MonoChunkParser.RemoveChunkExtension(this.saved.ToString()), NumberStyles.HexNumber);
			}
			catch (Exception)
			{
				MonoChunkParser.ThrowProtocolViolation("Cannot parse chunk size.");
			}
			if (this.chunkSize == 0)
			{
				this.trailerState = 2;
				return MonoChunkParser.State.Trailer;
			}
			return MonoChunkParser.State.Body;
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x000BF050 File Offset: 0x000BD250
		private static string RemoveChunkExtension(string input)
		{
			int num = input.IndexOf(';');
			if (num == -1)
			{
				return input;
			}
			return input.Substring(0, num);
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x000BF074 File Offset: 0x000BD274
		private MonoChunkParser.State ReadCRLF(byte[] buffer, ref int offset, int size)
		{
			if (!this.sawCR)
			{
				int num = offset;
				offset = num + 1;
				if (buffer[num] != 13)
				{
					MonoChunkParser.ThrowProtocolViolation("Expecting \\r");
				}
				this.sawCR = true;
				if (offset == size)
				{
					return MonoChunkParser.State.BodyFinished;
				}
			}
			if (this.sawCR)
			{
				int num = offset;
				offset = num + 1;
				if (buffer[num] != 10)
				{
					MonoChunkParser.ThrowProtocolViolation("Expecting \\n");
				}
			}
			return MonoChunkParser.State.None;
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000BF0D4 File Offset: 0x000BD2D4
		private MonoChunkParser.State ReadTrailer(byte[] buffer, ref int offset, int size)
		{
			if (this.trailerState == 2 && buffer[offset] == 13 && this.saved.Length == 0)
			{
				offset++;
				if (offset < size && buffer[offset] == 10)
				{
					offset++;
					return MonoChunkParser.State.None;
				}
				offset--;
			}
			int num = this.trailerState;
			while (offset < size && num < 4)
			{
				int num2 = offset;
				offset = num2 + 1;
				char c = (char)buffer[num2];
				if ((num == 0 || num == 2) && c == '\r')
				{
					num++;
				}
				else if ((num == 1 || num == 3) && c == '\n')
				{
					num++;
				}
				else if (num >= 0)
				{
					this.saved.Append(c);
					num = 0;
					if (this.saved.Length > 4196)
					{
						MonoChunkParser.ThrowProtocolViolation("Error reading trailer (too long).");
					}
				}
			}
			if (num < 4)
			{
				this.trailerState = num;
				if (offset < size)
				{
					MonoChunkParser.ThrowProtocolViolation("Error reading trailer.");
				}
				return MonoChunkParser.State.Trailer;
			}
			StringReader stringReader = new StringReader(this.saved.ToString());
			string text;
			while ((text = stringReader.ReadLine()) != null && text != "")
			{
				this.headers.Add(text);
			}
			return MonoChunkParser.State.None;
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x000BF1EA File Offset: 0x000BD3EA
		private static void ThrowProtocolViolation(string message)
		{
			throw new WebException(message, null, WebExceptionStatus.ServerProtocolViolation, null);
		}

		// Token: 0x04001FAC RID: 8108
		private WebHeaderCollection headers;

		// Token: 0x04001FAD RID: 8109
		private int chunkSize;

		// Token: 0x04001FAE RID: 8110
		private int chunkRead;

		// Token: 0x04001FAF RID: 8111
		private int totalWritten;

		// Token: 0x04001FB0 RID: 8112
		private MonoChunkParser.State state;

		// Token: 0x04001FB1 RID: 8113
		private StringBuilder saved;

		// Token: 0x04001FB2 RID: 8114
		private bool sawCR;

		// Token: 0x04001FB3 RID: 8115
		private bool gotit;

		// Token: 0x04001FB4 RID: 8116
		private int trailerState;

		// Token: 0x04001FB5 RID: 8117
		private ArrayList chunks;

		// Token: 0x020006A3 RID: 1699
		private enum State
		{
			// Token: 0x04001FB7 RID: 8119
			None,
			// Token: 0x04001FB8 RID: 8120
			PartialSize,
			// Token: 0x04001FB9 RID: 8121
			Body,
			// Token: 0x04001FBA RID: 8122
			BodyFinished,
			// Token: 0x04001FBB RID: 8123
			Trailer
		}

		// Token: 0x020006A4 RID: 1700
		private class Chunk
		{
			// Token: 0x06003672 RID: 13938 RVA: 0x000BF1F6 File Offset: 0x000BD3F6
			public Chunk(byte[] chunk)
			{
				this.Bytes = chunk;
			}

			// Token: 0x06003673 RID: 13939 RVA: 0x000BF208 File Offset: 0x000BD408
			public int Read(byte[] buffer, int offset, int size)
			{
				int num = (size > this.Bytes.Length - this.Offset) ? (this.Bytes.Length - this.Offset) : size;
				Buffer.BlockCopy(this.Bytes, this.Offset, buffer, offset, num);
				this.Offset += num;
				return num;
			}

			// Token: 0x04001FBC RID: 8124
			public byte[] Bytes;

			// Token: 0x04001FBD RID: 8125
			public int Offset;
		}
	}
}
