using System;
using System.IO;
using System.Runtime.Serialization;

namespace System.Xml
{
	// Token: 0x02000080 RID: 128
	internal class BufferedReadStream : Stream
	{
		// Token: 0x060006CC RID: 1740 RVA: 0x0001D3FF File Offset: 0x0001B5FF
		public BufferedReadStream(Stream stream) : this(stream, false)
		{
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001D409 File Offset: 0x0001B609
		public BufferedReadStream(Stream stream, bool readMore)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			this.stream = stream;
			this.readMore = readMore;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00003127 File Offset: 0x00001327
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00003127 File Offset: 0x00001327
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001D42D File Offset: 0x0001B62D
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001D43A File Offset: 0x0001B63A
		public override long Length
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Seek operation is not supported on this Stream.", new object[]
				{
					this.stream.GetType().FullName
				})));
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001D43A File Offset: 0x0001B63A
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x0001D43A File Offset: 0x0001B63A
		public override long Position
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Seek operation is not supported on this Stream.", new object[]
				{
					this.stream.GetType().FullName
				})));
			}
			set
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Seek operation is not supported on this Stream.", new object[]
				{
					this.stream.GetType().FullName
				})));
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001D46C File Offset: 0x0001B66C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanRead)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Read operation is not supported on the Stream.", new object[]
				{
					this.stream.GetType().FullName
				})));
			}
			return this.stream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001D4C1 File Offset: 0x0001B6C1
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Write operation is not supported on this '{0}' Stream.", new object[]
			{
				this.stream.GetType().FullName
			})));
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001D4F0 File Offset: 0x0001B6F0
		public override void Close()
		{
			this.stream.Close();
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001D500 File Offset: 0x0001B700
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (!this.CanRead)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Read operation is not supported on the Stream.", new object[]
				{
					this.stream.GetType().FullName
				})));
			}
			return this.stream.EndRead(asyncResult);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001D4C1 File Offset: 0x0001B6C1
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Write operation is not supported on this '{0}' Stream.", new object[]
			{
				this.stream.GetType().FullName
			})));
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001D54F File Offset: 0x0001B74F
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001D55C File Offset: 0x0001B75C
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Read operation is not supported on the Stream.", new object[]
				{
					this.stream.GetType().FullName
				})));
			}
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
			int num = 0;
			if (this.storedOffset < this.storedLength)
			{
				num = Math.Min(count, this.storedLength - this.storedOffset);
				Buffer.BlockCopy(this.storedBuffer, this.storedOffset, buffer, offset, num);
				this.storedOffset += num;
				if (num == count || !this.readMore)
				{
					return num;
				}
				offset += num;
				count -= num;
			}
			return num + this.stream.Read(buffer, offset, count);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001D6C0 File Offset: 0x0001B8C0
		public override int ReadByte()
		{
			if (this.storedOffset < this.storedLength)
			{
				byte[] array = this.storedBuffer;
				int num = this.storedOffset;
				this.storedOffset = num + 1;
				return array[num];
			}
			return base.ReadByte();
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001D6FC File Offset: 0x0001B8FC
		public int ReadBlock(byte[] buffer, int offset, int count)
		{
			int num = 0;
			int num2;
			while (num < count && (num2 = this.Read(buffer, offset + num, count - num)) != 0)
			{
				num += num2;
			}
			return num;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001D728 File Offset: 0x0001B928
		public void Push(byte[] buffer, int offset, int count)
		{
			if (count == 0)
			{
				return;
			}
			if (this.storedOffset == this.storedLength)
			{
				if (this.storedBuffer == null || this.storedBuffer.Length < count)
				{
					this.storedBuffer = new byte[count];
				}
				this.storedOffset = 0;
				this.storedLength = count;
			}
			else if (count <= this.storedOffset)
			{
				this.storedOffset -= count;
			}
			else if (count <= this.storedBuffer.Length - this.storedLength + this.storedOffset)
			{
				Buffer.BlockCopy(this.storedBuffer, this.storedOffset, this.storedBuffer, count, this.storedLength - this.storedOffset);
				this.storedLength += count - this.storedOffset;
				this.storedOffset = 0;
			}
			else
			{
				byte[] dst = new byte[count + this.storedLength - this.storedOffset];
				Buffer.BlockCopy(this.storedBuffer, this.storedOffset, dst, count, this.storedLength - this.storedOffset);
				this.storedLength += count - this.storedOffset;
				this.storedOffset = 0;
				this.storedBuffer = dst;
			}
			Buffer.BlockCopy(buffer, offset, this.storedBuffer, this.storedOffset, count);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001D43A File Offset: 0x0001B63A
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Seek operation is not supported on this Stream.", new object[]
			{
				this.stream.GetType().FullName
			})));
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001D43A File Offset: 0x0001B63A
		public override void SetLength(long value)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Seek operation is not supported on this Stream.", new object[]
			{
				this.stream.GetType().FullName
			})));
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001D4C1 File Offset: 0x0001B6C1
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(System.Runtime.Serialization.SR.GetString("Write operation is not supported on this '{0}' Stream.", new object[]
			{
				this.stream.GetType().FullName
			})));
		}

		// Token: 0x04000315 RID: 789
		private Stream stream;

		// Token: 0x04000316 RID: 790
		private byte[] storedBuffer;

		// Token: 0x04000317 RID: 791
		private int storedLength;

		// Token: 0x04000318 RID: 792
		private int storedOffset;

		// Token: 0x04000319 RID: 793
		private bool readMore;
	}
}
