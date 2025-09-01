using System;
using System.IO;
using System.Runtime.Serialization;

namespace System.Xml
{
	// Token: 0x02000073 RID: 115
	internal class DelimittedStreamReader
	{
		// Token: 0x0600068E RID: 1678 RVA: 0x0001C3C3 File Offset: 0x0001A5C3
		public DelimittedStreamReader(Stream stream)
		{
			this.stream = new BufferedReadStream(stream);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001C3DE File Offset: 0x0001A5DE
		public void Close()
		{
			this.stream.Close();
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001C3EC File Offset: 0x0001A5EC
		private void Close(DelimittedStreamReader.DelimittedReadStream caller)
		{
			if (this.currentStream == caller)
			{
				if (this.delimitter == null)
				{
					this.stream.Close();
				}
				else
				{
					if (this.scratch == null)
					{
						this.scratch = new byte[1024];
					}
					while (this.Read(caller, this.scratch, 0, this.scratch.Length) != 0)
					{
					}
				}
				this.currentStream = null;
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001C450 File Offset: 0x0001A650
		public Stream GetNextStream(byte[] delimitter)
		{
			if (this.currentStream != null)
			{
				this.currentStream.Close();
				this.currentStream = null;
			}
			if (!this.canGetNextStream)
			{
				return null;
			}
			this.delimitter = delimitter;
			this.canGetNextStream = (delimitter != null);
			this.currentStream = new DelimittedStreamReader.DelimittedReadStream(this);
			return this.currentStream;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001C4A4 File Offset: 0x0001A6A4
		private DelimittedStreamReader.MatchState MatchDelimitter(byte[] buffer, int start, int end)
		{
			if (this.delimitter.Length > end - start)
			{
				for (int i = end - start - 1; i >= 1; i--)
				{
					if (buffer[start + i] != this.delimitter[i])
					{
						return DelimittedStreamReader.MatchState.False;
					}
				}
				return DelimittedStreamReader.MatchState.InsufficientData;
			}
			for (int j = this.delimitter.Length - 1; j >= 1; j--)
			{
				if (buffer[start + j] != this.delimitter[j])
				{
					return DelimittedStreamReader.MatchState.False;
				}
			}
			return DelimittedStreamReader.MatchState.True;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001C508 File Offset: 0x0001A708
		private int ProcessRead(byte[] buffer, int offset, int read)
		{
			if (read == 0)
			{
				return read;
			}
			int i = offset;
			int num = offset + read;
			while (i < num)
			{
				if (buffer[i] == this.delimitter[0])
				{
					switch (this.MatchDelimitter(buffer, i, num))
					{
					case DelimittedStreamReader.MatchState.True:
					{
						int result = i - offset;
						i += this.delimitter.Length;
						this.stream.Push(buffer, i, num - i);
						this.currentStream = null;
						return result;
					}
					case DelimittedStreamReader.MatchState.InsufficientData:
					{
						int num2 = i - offset;
						if (num2 > 0)
						{
							this.stream.Push(buffer, i, num - i);
							return num2;
						}
						return -1;
					}
					}
				}
				i++;
			}
			return read;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001C598 File Offset: 0x0001A798
		private int Read(DelimittedStreamReader.DelimittedReadStream caller, byte[] buffer, int offset, int count)
		{
			if (this.currentStream != caller)
			{
				return 0;
			}
			int num = this.stream.Read(buffer, offset, count);
			if (num == 0)
			{
				this.canGetNextStream = false;
				this.currentStream = null;
				return num;
			}
			if (this.delimitter == null)
			{
				return num;
			}
			int num2 = this.ProcessRead(buffer, offset, num);
			if (num2 < 0)
			{
				if (this.matchBuffer == null || this.matchBuffer.Length < this.delimitter.Length - num)
				{
					this.matchBuffer = new byte[this.delimitter.Length - num];
				}
				int count2 = this.stream.ReadBlock(this.matchBuffer, 0, this.delimitter.Length - num);
				if (this.MatchRemainder(num, count2))
				{
					this.currentStream = null;
					num2 = 0;
				}
				else
				{
					this.stream.Push(this.matchBuffer, 0, count2);
					int num3 = 1;
					while (num3 < num && buffer[num3] != this.delimitter[0])
					{
						num3++;
					}
					if (num3 < num)
					{
						this.stream.Push(buffer, offset + num3, num - num3);
					}
					num2 = num3;
				}
			}
			return num2;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001C696 File Offset: 0x0001A896
		private bool MatchRemainder(int start, int count)
		{
			if (start + count != this.delimitter.Length)
			{
				return false;
			}
			for (count--; count >= 0; count--)
			{
				if (this.delimitter[start + count] != this.matchBuffer[count])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001C6CE File Offset: 0x0001A8CE
		internal void Push(byte[] buffer, int offset, int count)
		{
			this.stream.Push(buffer, offset, count);
		}

		// Token: 0x040002E4 RID: 740
		private bool canGetNextStream = true;

		// Token: 0x040002E5 RID: 741
		private DelimittedStreamReader.DelimittedReadStream currentStream;

		// Token: 0x040002E6 RID: 742
		private byte[] delimitter;

		// Token: 0x040002E7 RID: 743
		private byte[] matchBuffer;

		// Token: 0x040002E8 RID: 744
		private byte[] scratch;

		// Token: 0x040002E9 RID: 745
		private BufferedReadStream stream;

		// Token: 0x02000074 RID: 116
		private enum MatchState
		{
			// Token: 0x040002EB RID: 747
			True,
			// Token: 0x040002EC RID: 748
			False,
			// Token: 0x040002ED RID: 749
			InsufficientData
		}

		// Token: 0x02000075 RID: 117
		private class DelimittedReadStream : Stream
		{
			// Token: 0x06000697 RID: 1687 RVA: 0x0001C6DE File Offset: 0x0001A8DE
			public DelimittedReadStream(DelimittedStreamReader reader)
			{
				if (reader == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
				}
				this.reader = reader;
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x06000698 RID: 1688 RVA: 0x000066E8 File Offset: 0x000048E8
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x06000699 RID: 1689 RVA: 0x00003127 File Offset: 0x00001327
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x0600069A RID: 1690 RVA: 0x00003127 File Offset: 0x00001327
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001C6FB File Offset: 0x0001A8FB
			public override long Length
			{
				get
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Seek operation is not supported on this Stream.", new object[]
					{
						base.GetType().FullName
					})));
				}
			}

			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x0600069C RID: 1692 RVA: 0x0001C6FB File Offset: 0x0001A8FB
			// (set) Token: 0x0600069D RID: 1693 RVA: 0x0001C6FB File Offset: 0x0001A8FB
			public override long Position
			{
				get
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Seek operation is not supported on this Stream.", new object[]
					{
						base.GetType().FullName
					})));
				}
				set
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Seek operation is not supported on this Stream.", new object[]
					{
						base.GetType().FullName
					})));
				}
			}

			// Token: 0x0600069E RID: 1694 RVA: 0x0001C725 File Offset: 0x0001A925
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Write operation is not supported on this '{0}' Stream.", new object[]
				{
					base.GetType().FullName
				})));
			}

			// Token: 0x0600069F RID: 1695 RVA: 0x0001C74F File Offset: 0x0001A94F
			public override void Close()
			{
				this.reader.Close(this);
			}

			// Token: 0x060006A0 RID: 1696 RVA: 0x0001C725 File Offset: 0x0001A925
			public override void EndWrite(IAsyncResult asyncResult)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Write operation is not supported on this '{0}' Stream.", new object[]
				{
					base.GetType().FullName
				})));
			}

			// Token: 0x060006A1 RID: 1697 RVA: 0x0001C725 File Offset: 0x0001A925
			public override void Flush()
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Write operation is not supported on this '{0}' Stream.", new object[]
				{
					base.GetType().FullName
				})));
			}

			// Token: 0x060006A2 RID: 1698 RVA: 0x0001C760 File Offset: 0x0001A960
			public override int Read(byte[] buffer, int offset, int count)
			{
				if (buffer == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
				}
				if (offset < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
				}
				if (offset > buffer.Length)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
					{
						buffer.Length
					})));
				}
				if (count < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
				}
				if (count > buffer.Length - offset)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
					{
						buffer.Length - offset
					})));
				}
				return this.reader.Read(this, buffer, offset, count);
			}

			// Token: 0x060006A3 RID: 1699 RVA: 0x0001C6FB File Offset: 0x0001A8FB
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Seek operation is not supported on this Stream.", new object[]
				{
					base.GetType().FullName
				})));
			}

			// Token: 0x060006A4 RID: 1700 RVA: 0x0001C725 File Offset: 0x0001A925
			public override void SetLength(long value)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Write operation is not supported on this '{0}' Stream.", new object[]
				{
					base.GetType().FullName
				})));
			}

			// Token: 0x060006A5 RID: 1701 RVA: 0x0001C725 File Offset: 0x0001A925
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Write operation is not supported on this '{0}' Stream.", new object[]
				{
					base.GetType().FullName
				})));
			}

			// Token: 0x040002EE RID: 750
			private DelimittedStreamReader reader;
		}
	}
}
