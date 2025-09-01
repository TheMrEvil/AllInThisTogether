using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020006CB RID: 1739
	internal abstract class WebReadStream : Stream
	{
		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x060037F4 RID: 14324 RVA: 0x000C4F56 File Offset: 0x000C3156
		public WebOperation Operation
		{
			[CompilerGenerated]
			get
			{
				return this.<Operation>k__BackingField;
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x060037F5 RID: 14325 RVA: 0x000C4F5E File Offset: 0x000C315E
		protected Stream InnerStream
		{
			[CompilerGenerated]
			get
			{
				return this.<InnerStream>k__BackingField;
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x060037F6 RID: 14326 RVA: 0x00002F6A File Offset: 0x0000116A
		internal string ME
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x000C4F66 File Offset: 0x000C3166
		public WebReadStream(WebOperation operation, Stream innerStream)
		{
			this.Operation = operation;
			this.InnerStream = innerStream;
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x060037F8 RID: 14328 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x060037F9 RID: 14329 RVA: 0x000044FA File Offset: 0x000026FA
		// (set) Token: 0x060037FA RID: 14330 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x060037FB RID: 14331 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x060037FC RID: 14332 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x060037FD RID: 14333 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x000044FA File Offset: 0x000026FA
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x000044FA File Offset: 0x000026FA
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x000044FA File Offset: 0x000026FA
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x000C4F7C File Offset: 0x000C317C
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

		// Token: 0x06003803 RID: 14339 RVA: 0x000C4FB4 File Offset: 0x000C31B4
		public override int Read(byte[] buffer, int offset, int size)
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
			if (size < 0 || num - offset < size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			int result;
			try
			{
				result = this.ReadAsync(buffer, offset, size, CancellationToken.None).Result;
			}
			catch (Exception e)
			{
				throw this.GetException(e);
			}
			return result;
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x000C504C File Offset: 0x000C324C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback cb, object state)
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
			if (size < 0 || num - offset < size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, size, CancellationToken.None), cb, state);
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x000C50C8 File Offset: 0x000C32C8
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

		// Token: 0x06003806 RID: 14342 RVA: 0x000C5108 File Offset: 0x000C3308
		public sealed override Task<int> ReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			WebReadStream.<ReadAsync>d__28 <ReadAsync>d__;
			<ReadAsync>d__.<>4__this = this;
			<ReadAsync>d__.buffer = buffer;
			<ReadAsync>d__.offset = offset;
			<ReadAsync>d__.size = size;
			<ReadAsync>d__.cancellationToken = cancellationToken;
			<ReadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadAsync>d__.<>1__state = -1;
			<ReadAsync>d__.<>t__builder.Start<WebReadStream.<ReadAsync>d__28>(ref <ReadAsync>d__);
			return <ReadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06003807 RID: 14343
		protected abstract Task<int> ProcessReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken);

		// Token: 0x06003808 RID: 14344 RVA: 0x000C516C File Offset: 0x000C336C
		internal virtual Task FinishReading(CancellationToken cancellationToken)
		{
			this.Operation.ThrowIfDisposed(cancellationToken);
			WebReadStream webReadStream = this.InnerStream as WebReadStream;
			if (webReadStream != null)
			{
				return webReadStream.FinishReading(cancellationToken);
			}
			return Task.CompletedTask;
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x000C51A1 File Offset: 0x000C33A1
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				if (this.InnerStream != null)
				{
					this.InnerStream.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x040020C2 RID: 8386
		[CompilerGenerated]
		private readonly WebOperation <Operation>k__BackingField;

		// Token: 0x040020C3 RID: 8387
		[CompilerGenerated]
		private readonly Stream <InnerStream>k__BackingField;

		// Token: 0x040020C4 RID: 8388
		private bool disposed;

		// Token: 0x020006CC RID: 1740
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAsync>d__28 : IAsyncStateMachine
		{
			// Token: 0x0600380A RID: 14346 RVA: 0x000C51D0 File Offset: 0x000C33D0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebReadStream webReadStream = this.<>4__this;
				int result2;
				try
				{
					if (num > 1)
					{
						webReadStream.Operation.ThrowIfDisposed(this.cancellationToken);
						if (this.buffer == null)
						{
							throw new ArgumentNullException("buffer");
						}
						int num2 = this.buffer.Length;
						if (this.offset < 0 || num2 < this.offset)
						{
							throw new ArgumentOutOfRangeException("offset");
						}
						if (this.size < 0 || num2 - this.offset < this.size)
						{
							throw new ArgumentOutOfRangeException("size");
						}
					}
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
						if (num != 0)
						{
							if (num == 1)
							{
								awaiter = this.<>u__2;
								this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								num = (this.<>1__state = -1);
								goto IL_17D;
							}
							awaiter2 = webReadStream.ProcessReadAsync(this.buffer, this.offset, this.size, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, WebReadStream.<ReadAsync>d__28>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						int result = awaiter2.GetResult();
						if (result != 0)
						{
							result2 = result;
							goto IL_1AC;
						}
						awaiter = webReadStream.FinishReading(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 1);
							this.<>u__2 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebReadStream.<ReadAsync>d__28>(ref awaiter, ref this);
							return;
						}
						IL_17D:
						awaiter.GetResult();
						result2 = 0;
					}
					catch (OperationCanceledException)
					{
						throw;
					}
					catch (Exception)
					{
						throw;
					}
					finally
					{
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1AC:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x0600380B RID: 14347 RVA: 0x000C5404 File Offset: 0x000C3604
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040020C5 RID: 8389
			public int <>1__state;

			// Token: 0x040020C6 RID: 8390
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x040020C7 RID: 8391
			public WebReadStream <>4__this;

			// Token: 0x040020C8 RID: 8392
			public CancellationToken cancellationToken;

			// Token: 0x040020C9 RID: 8393
			public byte[] buffer;

			// Token: 0x040020CA RID: 8394
			public int offset;

			// Token: 0x040020CB RID: 8395
			public int size;

			// Token: 0x040020CC RID: 8396
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040020CD RID: 8397
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}
	}
}
