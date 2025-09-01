using System;
using System.IO;

namespace System.Security.Cryptography
{
	// Token: 0x0200049B RID: 1179
	internal sealed class TailStream : Stream
	{
		// Token: 0x06002F33 RID: 12083 RVA: 0x000A8417 File Offset: 0x000A6617
		public TailStream(int bufferSize)
		{
			this._Buffer = new byte[bufferSize];
			this._BufferSize = bufferSize;
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x000A471D File Offset: 0x000A291D
		public void Clear()
		{
			this.Close();
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x000A8434 File Offset: 0x000A6634
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this._Buffer != null)
					{
						Array.Clear(this._Buffer, 0, this._Buffer.Length);
					}
					this._Buffer = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06002F36 RID: 12086 RVA: 0x000A8484 File Offset: 0x000A6684
		public byte[] Buffer
		{
			get
			{
				return (byte[])this._Buffer.Clone();
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06002F37 RID: 12087 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06002F38 RID: 12088 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06002F39 RID: 12089 RVA: 0x000A8496 File Offset: 0x000A6696
		public override bool CanWrite
		{
			get
			{
				return this._Buffer != null;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x000A84A1 File Offset: 0x000A66A1
		public override long Length
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x000A84A1 File Offset: 0x000A66A1
		// (set) Token: 0x06002F3C RID: 12092 RVA: 0x000A84A1 File Offset: 0x000A66A1
		public override long Position
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
			}
			set
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
			}
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public override void Flush()
		{
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x000A84A1 File Offset: 0x000A66A1
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x000A84A1 File Offset: 0x000A66A1
		public override void SetLength(long value)
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x000A84B2 File Offset: 0x000A66B2
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support reading."));
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x000A84C4 File Offset: 0x000A66C4
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._Buffer == null)
			{
				throw new ObjectDisposedException("TailStream");
			}
			if (count == 0)
			{
				return;
			}
			if (this._BufferFull)
			{
				if (count > this._BufferSize)
				{
					System.Buffer.InternalBlockCopy(buffer, offset + count - this._BufferSize, this._Buffer, 0, this._BufferSize);
					return;
				}
				System.Buffer.InternalBlockCopy(this._Buffer, this._BufferSize - count, this._Buffer, 0, this._BufferSize - count);
				System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferSize - count, count);
				return;
			}
			else
			{
				if (count > this._BufferSize)
				{
					System.Buffer.InternalBlockCopy(buffer, offset + count - this._BufferSize, this._Buffer, 0, this._BufferSize);
					this._BufferFull = true;
					return;
				}
				if (count + this._BufferIndex >= this._BufferSize)
				{
					System.Buffer.InternalBlockCopy(this._Buffer, this._BufferIndex + count - this._BufferSize, this._Buffer, 0, this._BufferSize - count);
					System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferIndex, count);
					this._BufferFull = true;
					return;
				}
				System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferIndex, count);
				this._BufferIndex += count;
				return;
			}
		}

		// Token: 0x04002184 RID: 8580
		private byte[] _Buffer;

		// Token: 0x04002185 RID: 8581
		private int _BufferSize;

		// Token: 0x04002186 RID: 8582
		private int _BufferIndex;

		// Token: 0x04002187 RID: 8583
		private bool _BufferFull;
	}
}
