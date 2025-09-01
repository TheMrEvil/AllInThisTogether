using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000576 RID: 1398
	internal class DelegatedStream : Stream
	{
		// Token: 0x06002D26 RID: 11558 RVA: 0x0009AEB9 File Offset: 0x000990B9
		protected DelegatedStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this._stream = stream;
			this._netStream = (stream as NetworkStream);
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06002D27 RID: 11559 RVA: 0x0009AEE2 File Offset: 0x000990E2
		protected Stream BaseStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06002D28 RID: 11560 RVA: 0x0009AEEA File Offset: 0x000990EA
		public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06002D29 RID: 11561 RVA: 0x0009AEF7 File Offset: 0x000990F7
		public override bool CanSeek
		{
			get
			{
				return this._stream.CanSeek;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x0009AF04 File Offset: 0x00099104
		public override bool CanWrite
		{
			get
			{
				return this._stream.CanWrite;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002D2B RID: 11563 RVA: 0x0009AF11 File Offset: 0x00099111
		public override long Length
		{
			get
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException("Seeking is not supported on this stream.");
				}
				return this._stream.Length;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002D2C RID: 11564 RVA: 0x0009AF31 File Offset: 0x00099131
		// (set) Token: 0x06002D2D RID: 11565 RVA: 0x0009AF51 File Offset: 0x00099151
		public override long Position
		{
			get
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException("Seeking is not supported on this stream.");
				}
				return this._stream.Position;
			}
			set
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException("Seeking is not supported on this stream.");
				}
				this._stream.Position = value;
			}
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x0009AF74 File Offset: 0x00099174
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("Reading is not supported on this stream.");
			}
			IAsyncResult result;
			if (this._netStream != null)
			{
				result = this._netStream.BeginRead(buffer, offset, count, callback, state);
			}
			else
			{
				result = this._stream.BeginRead(buffer, offset, count, callback, state);
			}
			return result;
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x0009AFC8 File Offset: 0x000991C8
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Writing is not supported on this stream.");
			}
			IAsyncResult result;
			if (this._netStream != null)
			{
				result = this._netStream.BeginWrite(buffer, offset, count, callback, state);
			}
			else
			{
				result = this._stream.BeginWrite(buffer, offset, count, callback, state);
			}
			return result;
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x0009B01B File Offset: 0x0009921B
		public override void Close()
		{
			this._stream.Close();
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x0009B028 File Offset: 0x00099228
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("Reading is not supported on this stream.");
			}
			return this._stream.EndRead(asyncResult);
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x0009B049 File Offset: 0x00099249
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Writing is not supported on this stream.");
			}
			this._stream.EndWrite(asyncResult);
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x0009B06A File Offset: 0x0009926A
		public override void Flush()
		{
			this._stream.Flush();
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x0009B077 File Offset: 0x00099277
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._stream.FlushAsync(cancellationToken);
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x0009B085 File Offset: 0x00099285
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("Reading is not supported on this stream.");
			}
			return this._stream.Read(buffer, offset, count);
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x0009B0A8 File Offset: 0x000992A8
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("Reading is not supported on this stream.");
			}
			return this._stream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x0009B0CD File Offset: 0x000992CD
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException("Seeking is not supported on this stream.");
			}
			return this._stream.Seek(offset, origin);
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x0009B0EF File Offset: 0x000992EF
		public override void SetLength(long value)
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException("Seeking is not supported on this stream.");
			}
			this._stream.SetLength(value);
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x0009B110 File Offset: 0x00099310
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Writing is not supported on this stream.");
			}
			this._stream.Write(buffer, offset, count);
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x0009B133 File Offset: 0x00099333
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Writing is not supported on this stream.");
			}
			return this._stream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x04001896 RID: 6294
		private readonly Stream _stream;

		// Token: 0x04001897 RID: 6295
		private readonly NetworkStream _netStream;
	}
}
