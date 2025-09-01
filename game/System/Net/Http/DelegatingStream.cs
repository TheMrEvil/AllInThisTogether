using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x0200083F RID: 2111
	internal abstract class DelegatingStream : Stream
	{
		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x0600433A RID: 17210 RVA: 0x000EA33B File Offset: 0x000E853B
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x0600433B RID: 17211 RVA: 0x000EA348 File Offset: 0x000E8548
		public override bool CanSeek
		{
			get
			{
				return this._innerStream.CanSeek;
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x0600433C RID: 17212 RVA: 0x000EA355 File Offset: 0x000E8555
		public override bool CanWrite
		{
			get
			{
				return this._innerStream.CanWrite;
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x0600433D RID: 17213 RVA: 0x000EA362 File Offset: 0x000E8562
		public override long Length
		{
			get
			{
				return this._innerStream.Length;
			}
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x0600433E RID: 17214 RVA: 0x000EA36F File Offset: 0x000E856F
		// (set) Token: 0x0600433F RID: 17215 RVA: 0x000EA37C File Offset: 0x000E857C
		public override long Position
		{
			get
			{
				return this._innerStream.Position;
			}
			set
			{
				this._innerStream.Position = value;
			}
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06004340 RID: 17216 RVA: 0x000EA38A File Offset: 0x000E858A
		// (set) Token: 0x06004341 RID: 17217 RVA: 0x000EA397 File Offset: 0x000E8597
		public override int ReadTimeout
		{
			get
			{
				return this._innerStream.ReadTimeout;
			}
			set
			{
				this._innerStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06004342 RID: 17218 RVA: 0x000EA3A5 File Offset: 0x000E85A5
		public override bool CanTimeout
		{
			get
			{
				return this._innerStream.CanTimeout;
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06004343 RID: 17219 RVA: 0x000EA3B2 File Offset: 0x000E85B2
		// (set) Token: 0x06004344 RID: 17220 RVA: 0x000EA3BF File Offset: 0x000E85BF
		public override int WriteTimeout
		{
			get
			{
				return this._innerStream.WriteTimeout;
			}
			set
			{
				this._innerStream.WriteTimeout = value;
			}
		}

		// Token: 0x06004345 RID: 17221 RVA: 0x000EA3CD File Offset: 0x000E85CD
		protected DelegatingStream(Stream innerStream)
		{
			this._innerStream = innerStream;
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x000EA3DC File Offset: 0x000E85DC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._innerStream.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x000EA3F3 File Offset: 0x000E85F3
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._innerStream.Seek(offset, origin);
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x000EA402 File Offset: 0x000E8602
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this._innerStream.Read(buffer, offset, count);
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x000EA412 File Offset: 0x000E8612
		public override int Read(Span<byte> buffer)
		{
			return this._innerStream.Read(buffer);
		}

		// Token: 0x0600434A RID: 17226 RVA: 0x000EA420 File Offset: 0x000E8620
		public override int ReadByte()
		{
			return this._innerStream.ReadByte();
		}

		// Token: 0x0600434B RID: 17227 RVA: 0x000EA42D File Offset: 0x000E862D
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._innerStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x0600434C RID: 17228 RVA: 0x000EA43F File Offset: 0x000E863F
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._innerStream.ReadAsync(buffer, cancellationToken);
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x000EA44E File Offset: 0x000E864E
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this._innerStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x0600434E RID: 17230 RVA: 0x000EA462 File Offset: 0x000E8662
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this._innerStream.EndRead(asyncResult);
		}

		// Token: 0x0600434F RID: 17231 RVA: 0x000EA470 File Offset: 0x000E8670
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x06004350 RID: 17232 RVA: 0x000EA47D File Offset: 0x000E867D
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._innerStream.FlushAsync(cancellationToken);
		}

		// Token: 0x06004351 RID: 17233 RVA: 0x000EA48B File Offset: 0x000E868B
		public override void SetLength(long value)
		{
			this._innerStream.SetLength(value);
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x000EA499 File Offset: 0x000E8699
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x000EA4A9 File Offset: 0x000E86A9
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			this._innerStream.Write(buffer);
		}

		// Token: 0x06004354 RID: 17236 RVA: 0x000EA4B7 File Offset: 0x000E86B7
		public override void WriteByte(byte value)
		{
			this._innerStream.WriteByte(value);
		}

		// Token: 0x06004355 RID: 17237 RVA: 0x000EA4C5 File Offset: 0x000E86C5
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._innerStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06004356 RID: 17238 RVA: 0x000EA4D7 File Offset: 0x000E86D7
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._innerStream.WriteAsync(buffer, cancellationToken);
		}

		// Token: 0x06004357 RID: 17239 RVA: 0x000EA4E6 File Offset: 0x000E86E6
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this._innerStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06004358 RID: 17240 RVA: 0x000EA4FA File Offset: 0x000E86FA
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this._innerStream.EndWrite(asyncResult);
		}

		// Token: 0x06004359 RID: 17241 RVA: 0x000EA508 File Offset: 0x000E8708
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			return this._innerStream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		// Token: 0x040028C5 RID: 10437
		private readonly Stream _innerStream;
	}
}
