using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020006C3 RID: 1731
	internal abstract class WebConnectionStream : Stream
	{
		// Token: 0x06003797 RID: 14231 RVA: 0x000C38A8 File Offset: 0x000C1AA8
		protected WebConnectionStream(WebConnection cnc, WebOperation operation)
		{
			this.Connection = cnc;
			this.Operation = operation;
			this.Request = operation.Request;
			this.read_timeout = this.Request.ReadWriteTimeout;
			this.write_timeout = this.read_timeout;
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x000C38FD File Offset: 0x000C1AFD
		internal HttpWebRequest Request
		{
			[CompilerGenerated]
			get
			{
				return this.<Request>k__BackingField;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x000C3905 File Offset: 0x000C1B05
		internal WebConnection Connection
		{
			[CompilerGenerated]
			get
			{
				return this.<Connection>k__BackingField;
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x0600379A RID: 14234 RVA: 0x000C390D File Offset: 0x000C1B0D
		internal WebOperation Operation
		{
			[CompilerGenerated]
			get
			{
				return this.<Operation>k__BackingField;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x0600379B RID: 14235 RVA: 0x000C3915 File Offset: 0x000C1B15
		internal ServicePoint ServicePoint
		{
			get
			{
				return this.Connection.ServicePoint;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x0600379C RID: 14236 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x0600379D RID: 14237 RVA: 0x000C3922 File Offset: 0x000C1B22
		// (set) Token: 0x0600379E RID: 14238 RVA: 0x000C392A File Offset: 0x000C1B2A
		public override int ReadTimeout
		{
			get
			{
				return this.read_timeout;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.read_timeout = value;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x0600379F RID: 14239 RVA: 0x000C3942 File Offset: 0x000C1B42
		// (set) Token: 0x060037A0 RID: 14240 RVA: 0x000C394A File Offset: 0x000C1B4A
		public override int WriteTimeout
		{
			get
			{
				return this.write_timeout;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.write_timeout = value;
			}
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x000C3962 File Offset: 0x000C1B62
		protected Exception GetException(Exception e)
		{
			e = HttpWebRequest.FlattenException(e);
			if (e is WebException)
			{
				return e;
			}
			if (this.Operation.Aborted || e is OperationCanceledException || e is ObjectDisposedException)
			{
				return HttpWebRequest.CreateRequestAbortedException();
			}
			return e;
		}

		// Token: 0x060037A2 RID: 14242
		protected abstract bool TryReadFromBufferedContent(byte[] buffer, int offset, int count, out int result);

		// Token: 0x060037A3 RID: 14243 RVA: 0x000C399C File Offset: 0x000C1B9C
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("The stream does not support reading.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || num - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int result;
			if (this.TryReadFromBufferedContent(buffer, offset, count, out result))
			{
				return result;
			}
			this.Operation.ThrowIfClosedOrDisposed();
			int result2;
			try
			{
				result2 = this.ReadAsync(buffer, offset, count, CancellationToken.None).Result;
			}
			catch (Exception e)
			{
				throw this.GetException(e);
			}
			return result2;
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000C3A40 File Offset: 0x000C1C40
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cb, object state)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("The stream does not support reading.");
			}
			this.Operation.ThrowIfClosedOrDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || num - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, count, CancellationToken.None), cb, state);
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x000C3ABC File Offset: 0x000C1CBC
		public override int EndRead(IAsyncResult r)
		{
			if (r == null)
			{
				throw new ArgumentNullException("r");
			}
			int result;
			try
			{
				result = TaskToApm.End<int>(r);
			}
			catch (Exception e)
			{
				throw this.GetException(e);
			}
			return result;
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x000C3AFC File Offset: 0x000C1CFC
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cb, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || num - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("The stream does not support writing.");
			}
			this.Operation.ThrowIfClosedOrDisposed();
			return TaskToApm.Begin(this.WriteAsync(buffer, offset, count, CancellationToken.None), cb, state);
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x000C3B78 File Offset: 0x000C1D78
		public override void EndWrite(IAsyncResult r)
		{
			if (r == null)
			{
				throw new ArgumentNullException("r");
			}
			try
			{
				TaskToApm.End(r);
			}
			catch (Exception e)
			{
				throw this.GetException(e);
			}
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x000C3BB8 File Offset: 0x000C1DB8
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || num - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("The stream does not support writing.");
			}
			this.Operation.ThrowIfClosedOrDisposed();
			try
			{
				base.WriteAsync(buffer, offset, count).Wait();
			}
			catch (Exception e)
			{
				throw this.GetException(e);
			}
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Flush()
		{
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x000C3C48 File Offset: 0x000C1E48
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.CompletedTask;
			}
			return Task.FromCancellation(cancellationToken);
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x000C3C5F File Offset: 0x000C1E5F
		internal void InternalClose()
		{
			this.disposed = true;
		}

		// Token: 0x060037AC RID: 14252
		protected abstract void Close_internal(ref bool disposed);

		// Token: 0x060037AD RID: 14253 RVA: 0x000C3C68 File Offset: 0x000C1E68
		public override void Close()
		{
			this.Close_internal(ref this.disposed);
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000C3C76 File Offset: 0x000C1E76
		public override long Seek(long a, SeekOrigin b)
		{
			throw new NotSupportedException("This stream does not support seek operations.");
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000C3C76 File Offset: 0x000C1E76
		public override void SetLength(long a)
		{
			throw new NotSupportedException("This stream does not support seek operations.");
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x060037B0 RID: 14256 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x060037B1 RID: 14257 RVA: 0x000C3C76 File Offset: 0x000C1E76
		public override long Length
		{
			get
			{
				throw new NotSupportedException("This stream does not support seek operations.");
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x060037B2 RID: 14258 RVA: 0x000C3C76 File Offset: 0x000C1E76
		// (set) Token: 0x060037B3 RID: 14259 RVA: 0x000C3C76 File Offset: 0x000C1E76
		public override long Position
		{
			get
			{
				throw new NotSupportedException("This stream does not support seek operations.");
			}
			set
			{
				throw new NotSupportedException("This stream does not support seek operations.");
			}
		}

		// Token: 0x0400207A RID: 8314
		protected bool closed;

		// Token: 0x0400207B RID: 8315
		private bool disposed;

		// Token: 0x0400207C RID: 8316
		private object locker = new object();

		// Token: 0x0400207D RID: 8317
		private int read_timeout;

		// Token: 0x0400207E RID: 8318
		private int write_timeout;

		// Token: 0x0400207F RID: 8319
		internal bool IgnoreIOErrors;

		// Token: 0x04002080 RID: 8320
		[CompilerGenerated]
		private readonly HttpWebRequest <Request>k__BackingField;

		// Token: 0x04002081 RID: 8321
		[CompilerGenerated]
		private readonly WebConnection <Connection>k__BackingField;

		// Token: 0x04002082 RID: 8322
		[CompilerGenerated]
		private readonly WebOperation <Operation>k__BackingField;
	}
}
