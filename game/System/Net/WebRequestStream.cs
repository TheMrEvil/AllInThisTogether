using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020006CD RID: 1741
	internal class WebRequestStream : WebConnectionStream
	{
		// Token: 0x0600380C RID: 14348 RVA: 0x000C5414 File Offset: 0x000C3614
		public WebRequestStream(WebConnection connection, WebOperation operation, Stream stream, WebConnectionTunnel tunnel) : base(connection, operation)
		{
			this.InnerStream = stream;
			this.allowBuffering = operation.Request.InternalAllowBuffering;
			this.sendChunked = (operation.Request.SendChunked && operation.WriteBuffer == null);
			if (!this.sendChunked && this.allowBuffering && operation.WriteBuffer == null)
			{
				this.writeBuffer = new MemoryStream();
			}
			this.KeepAlive = base.Request.KeepAlive;
			if (((tunnel != null) ? tunnel.ProxyVersion : null) != null && ((tunnel != null) ? tunnel.ProxyVersion : null) != HttpVersion.Version11)
			{
				this.KeepAlive = 0;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x0600380D RID: 14349 RVA: 0x000C54CB File Offset: 0x000C36CB
		internal Stream InnerStream
		{
			[CompilerGenerated]
			get
			{
				return this.<InnerStream>k__BackingField;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x0600380E RID: 14350 RVA: 0x000C54D3 File Offset: 0x000C36D3
		public bool KeepAlive
		{
			[CompilerGenerated]
			get
			{
				return this.<KeepAlive>k__BackingField;
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x0600380F RID: 14351 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06003810 RID: 14352 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06003811 RID: 14353 RVA: 0x000C54DB File Offset: 0x000C36DB
		// (set) Token: 0x06003812 RID: 14354 RVA: 0x000C54E3 File Offset: 0x000C36E3
		internal bool SendChunked
		{
			get
			{
				return this.sendChunked;
			}
			set
			{
				this.sendChunked = value;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06003813 RID: 14355 RVA: 0x000C54EC File Offset: 0x000C36EC
		internal bool HasWriteBuffer
		{
			get
			{
				return base.Operation.WriteBuffer != null || this.writeBuffer != null;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06003814 RID: 14356 RVA: 0x000C5506 File Offset: 0x000C3706
		internal int WriteBufferLength
		{
			get
			{
				if (base.Operation.WriteBuffer != null)
				{
					return base.Operation.WriteBuffer.Size;
				}
				if (this.writeBuffer != null)
				{
					return (int)this.writeBuffer.Length;
				}
				return -1;
			}
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000C553C File Offset: 0x000C373C
		internal BufferOffsetSize GetWriteBuffer()
		{
			if (base.Operation.WriteBuffer != null)
			{
				return base.Operation.WriteBuffer;
			}
			if (this.writeBuffer == null || this.writeBuffer.Length == 0L)
			{
				return null;
			}
			return new BufferOffsetSize(this.writeBuffer.GetBuffer(), 0, (int)this.writeBuffer.Length, false);
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x000C5598 File Offset: 0x000C3798
		private Task FinishWriting(CancellationToken cancellationToken)
		{
			WebRequestStream.<FinishWriting>d__31 <FinishWriting>d__;
			<FinishWriting>d__.<>4__this = this;
			<FinishWriting>d__.cancellationToken = cancellationToken;
			<FinishWriting>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishWriting>d__.<>1__state = -1;
			<FinishWriting>d__.<>t__builder.Start<WebRequestStream.<FinishWriting>d__31>(ref <FinishWriting>d__);
			return <FinishWriting>d__.<>t__builder.Task;
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x000C55E4 File Offset: 0x000C37E4
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
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
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (base.Operation.WriteBuffer != null)
			{
				throw new InvalidOperationException();
			}
			WebCompletionSource webCompletionSource = new WebCompletionSource();
			if (Interlocked.CompareExchange<WebCompletionSource>(ref this.pendingWrite, webCompletionSource, null) != null)
			{
				throw new InvalidOperationException(SR.GetString("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress."));
			}
			return this.WriteAsyncInner(buffer, offset, count, webCompletionSource, cancellationToken);
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x000C5690 File Offset: 0x000C3890
		private Task WriteAsyncInner(byte[] buffer, int offset, int size, WebCompletionSource completion, CancellationToken cancellationToken)
		{
			WebRequestStream.<WriteAsyncInner>d__33 <WriteAsyncInner>d__;
			<WriteAsyncInner>d__.<>4__this = this;
			<WriteAsyncInner>d__.buffer = buffer;
			<WriteAsyncInner>d__.offset = offset;
			<WriteAsyncInner>d__.size = size;
			<WriteAsyncInner>d__.completion = completion;
			<WriteAsyncInner>d__.cancellationToken = cancellationToken;
			<WriteAsyncInner>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAsyncInner>d__.<>1__state = -1;
			<WriteAsyncInner>d__.<>t__builder.Start<WebRequestStream.<WriteAsyncInner>d__33>(ref <WriteAsyncInner>d__);
			return <WriteAsyncInner>d__.<>t__builder.Task;
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x000C5700 File Offset: 0x000C3900
		private Task ProcessWrite(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			WebRequestStream.<ProcessWrite>d__34 <ProcessWrite>d__;
			<ProcessWrite>d__.<>4__this = this;
			<ProcessWrite>d__.buffer = buffer;
			<ProcessWrite>d__.offset = offset;
			<ProcessWrite>d__.size = size;
			<ProcessWrite>d__.cancellationToken = cancellationToken;
			<ProcessWrite>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessWrite>d__.<>1__state = -1;
			<ProcessWrite>d__.<>t__builder.Start<WebRequestStream.<ProcessWrite>d__34>(ref <ProcessWrite>d__);
			return <ProcessWrite>d__.<>t__builder.Task;
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x000C5764 File Offset: 0x000C3964
		private void CheckWriteOverflow(long contentLength, long totalWritten, long size)
		{
			if (contentLength == -1L)
			{
				return;
			}
			long num = contentLength - totalWritten;
			if (size > num)
			{
				this.KillBuffer();
				this.closed = true;
				ProtocolViolationException ex = new ProtocolViolationException("The number of bytes to be written is greater than the specified ContentLength.");
				base.Operation.CompleteRequestWritten(this, ex);
				throw ex;
			}
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x000C57A8 File Offset: 0x000C39A8
		internal Task Initialize(CancellationToken cancellationToken)
		{
			WebRequestStream.<Initialize>d__36 <Initialize>d__;
			<Initialize>d__.<>4__this = this;
			<Initialize>d__.cancellationToken = cancellationToken;
			<Initialize>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Initialize>d__.<>1__state = -1;
			<Initialize>d__.<>t__builder.Start<WebRequestStream.<Initialize>d__36>(ref <Initialize>d__);
			return <Initialize>d__.<>t__builder.Task;
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x000C57F4 File Offset: 0x000C39F4
		private Task SetHeadersAsync(bool setInternalLength, CancellationToken cancellationToken)
		{
			WebRequestStream.<SetHeadersAsync>d__37 <SetHeadersAsync>d__;
			<SetHeadersAsync>d__.<>4__this = this;
			<SetHeadersAsync>d__.setInternalLength = setInternalLength;
			<SetHeadersAsync>d__.cancellationToken = cancellationToken;
			<SetHeadersAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SetHeadersAsync>d__.<>1__state = -1;
			<SetHeadersAsync>d__.<>t__builder.Start<WebRequestStream.<SetHeadersAsync>d__37>(ref <SetHeadersAsync>d__);
			return <SetHeadersAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x000C5848 File Offset: 0x000C3A48
		internal Task WriteRequestAsync(CancellationToken cancellationToken)
		{
			WebRequestStream.<WriteRequestAsync>d__38 <WriteRequestAsync>d__;
			<WriteRequestAsync>d__.<>4__this = this;
			<WriteRequestAsync>d__.cancellationToken = cancellationToken;
			<WriteRequestAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRequestAsync>d__.<>1__state = -1;
			<WriteRequestAsync>d__.<>t__builder.Start<WebRequestStream.<WriteRequestAsync>d__38>(ref <WriteRequestAsync>d__);
			return <WriteRequestAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x000C5894 File Offset: 0x000C3A94
		private Task WriteChunkTrailer_inner(CancellationToken cancellationToken)
		{
			WebRequestStream.<WriteChunkTrailer_inner>d__39 <WriteChunkTrailer_inner>d__;
			<WriteChunkTrailer_inner>d__.<>4__this = this;
			<WriteChunkTrailer_inner>d__.cancellationToken = cancellationToken;
			<WriteChunkTrailer_inner>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteChunkTrailer_inner>d__.<>1__state = -1;
			<WriteChunkTrailer_inner>d__.<>t__builder.Start<WebRequestStream.<WriteChunkTrailer_inner>d__39>(ref <WriteChunkTrailer_inner>d__);
			return <WriteChunkTrailer_inner>d__.<>t__builder.Task;
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x000C58E0 File Offset: 0x000C3AE0
		private Task WriteChunkTrailer()
		{
			WebRequestStream.<WriteChunkTrailer>d__40 <WriteChunkTrailer>d__;
			<WriteChunkTrailer>d__.<>4__this = this;
			<WriteChunkTrailer>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteChunkTrailer>d__.<>1__state = -1;
			<WriteChunkTrailer>d__.<>t__builder.Start<WebRequestStream.<WriteChunkTrailer>d__40>(ref <WriteChunkTrailer>d__);
			return <WriteChunkTrailer>d__.<>t__builder.Task;
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x000C5923 File Offset: 0x000C3B23
		internal void KillBuffer()
		{
			this.writeBuffer = null;
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x000C592C File Offset: 0x000C3B2C
		public override Task<int> ReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			return Task.FromException<int>(new NotSupportedException("The stream does not support reading."));
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x00011ECF File Offset: 0x000100CF
		protected override bool TryReadFromBufferedContent(byte[] buffer, int offset, int count, out int result)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x000C5940 File Offset: 0x000C3B40
		protected override void Close_internal(ref bool disposed)
		{
			if (disposed)
			{
				return;
			}
			disposed = true;
			if (this.sendChunked)
			{
				this.WriteChunkTrailer().Wait();
				return;
			}
			if (!this.allowBuffering || this.requestWritten)
			{
				base.Operation.CompleteRequestWritten(this, null);
				return;
			}
			long contentLength = base.Request.ContentLength;
			if (!this.sendChunked && !base.Operation.IsNtlmChallenge && contentLength != -1L && this.totalWritten != contentLength)
			{
				IOException innerException = new IOException("Cannot close the stream until all bytes are written");
				this.closed = true;
				disposed = true;
				WebException ex = new WebException("Request was cancelled.", WebExceptionStatus.RequestCanceled, WebExceptionInternalStatus.RequestFatal, innerException);
				base.Operation.CompleteRequestWritten(this, ex);
				throw ex;
			}
			disposed = true;
			base.Operation.CompleteRequestWritten(this, null);
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x000C59F8 File Offset: 0x000C3BF8
		// Note: this type is marked as 'beforefieldinit'.
		static WebRequestStream()
		{
		}

		// Token: 0x040020CE RID: 8398
		private static byte[] crlf = new byte[]
		{
			13,
			10
		};

		// Token: 0x040020CF RID: 8399
		private MemoryStream writeBuffer;

		// Token: 0x040020D0 RID: 8400
		private bool requestWritten;

		// Token: 0x040020D1 RID: 8401
		private bool allowBuffering;

		// Token: 0x040020D2 RID: 8402
		private bool sendChunked;

		// Token: 0x040020D3 RID: 8403
		private WebCompletionSource pendingWrite;

		// Token: 0x040020D4 RID: 8404
		private long totalWritten;

		// Token: 0x040020D5 RID: 8405
		private byte[] headers;

		// Token: 0x040020D6 RID: 8406
		private bool headersSent;

		// Token: 0x040020D7 RID: 8407
		private int completeRequestWritten;

		// Token: 0x040020D8 RID: 8408
		private int chunkTrailerWritten;

		// Token: 0x040020D9 RID: 8409
		internal readonly string ME;

		// Token: 0x040020DA RID: 8410
		[CompilerGenerated]
		private readonly Stream <InnerStream>k__BackingField;

		// Token: 0x040020DB RID: 8411
		[CompilerGenerated]
		private readonly bool <KeepAlive>k__BackingField;

		// Token: 0x020006CE RID: 1742
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FinishWriting>d__31 : IAsyncStateMachine
		{
			// Token: 0x06003825 RID: 14373 RVA: 0x000C5A10 File Offset: 0x000C3C10
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebRequestStream webRequestStream = this.<>4__this;
				try
				{
					if (num == 0 || Interlocked.CompareExchange(ref webRequestStream.completeRequestWritten, 1, 0) == 0)
					{
						try
						{
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
							if (num != 0)
							{
								webRequestStream.Operation.ThrowIfClosedOrDisposed(this.cancellationToken);
								if (!webRequestStream.sendChunked)
								{
									goto IL_A9;
								}
								awaiter = webRequestStream.WriteChunkTrailer_inner(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 0);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebRequestStream.<FinishWriting>d__31>(ref awaiter, ref this);
									return;
								}
							}
							else
							{
								awaiter = this.<>u__1;
								this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								num = (this.<>1__state = -1);
							}
							awaiter.GetResult();
							IL_A9:;
						}
						catch (Exception error)
						{
							webRequestStream.Operation.CompleteRequestWritten(webRequestStream, error);
							throw;
						}
						finally
						{
						}
						webRequestStream.Operation.CompleteRequestWritten(webRequestStream, null);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06003826 RID: 14374 RVA: 0x000C5B44 File Offset: 0x000C3D44
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040020DC RID: 8412
			public int <>1__state;

			// Token: 0x040020DD RID: 8413
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040020DE RID: 8414
			public WebRequestStream <>4__this;

			// Token: 0x040020DF RID: 8415
			public CancellationToken cancellationToken;

			// Token: 0x040020E0 RID: 8416
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020006CF RID: 1743
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAsyncInner>d__33 : IAsyncStateMachine
		{
			// Token: 0x06003827 RID: 14375 RVA: 0x000C5B54 File Offset: 0x000C3D54
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebRequestStream webRequestStream = this.<>4__this;
				try
				{
					try
					{
						TaskAwaiter awaiter;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
						if (num != 0)
						{
							if (num == 1)
							{
								awaiter = this.<>u__2;
								this.<>u__2 = default(TaskAwaiter);
								this.<>1__state = -1;
								goto IL_118;
							}
							awaiter2 = webRequestStream.ProcessWrite(this.buffer, this.offset, this.size, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebRequestStream.<WriteAsyncInner>d__33>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
						}
						awaiter2.GetResult();
						if (webRequestStream.Request.ContentLength <= 0L || webRequestStream.totalWritten != webRequestStream.Request.ContentLength)
						{
							goto IL_11F;
						}
						awaiter = webRequestStream.FinishWriting(this.cancellationToken).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__2 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, WebRequestStream.<WriteAsyncInner>d__33>(ref awaiter, ref this);
							return;
						}
						IL_118:
						awaiter.GetResult();
						IL_11F:
						webRequestStream.pendingWrite = null;
						this.completion.TrySetCompleted();
					}
					catch (Exception ex)
					{
						webRequestStream.KillBuffer();
						webRequestStream.closed = true;
						ExceptionDispatchInfo exceptionDispatchInfo = webRequestStream.Operation.CheckDisposed(this.cancellationToken);
						if (exceptionDispatchInfo != null)
						{
							ex = exceptionDispatchInfo.SourceException;
						}
						else if (ex is SocketException)
						{
							ex = new IOException("Error writing request", ex);
						}
						webRequestStream.Operation.CompleteRequestWritten(webRequestStream, ex);
						webRequestStream.pendingWrite = null;
						this.completion.TrySetException(ex);
						if (exceptionDispatchInfo != null)
						{
							exceptionDispatchInfo.Throw();
						}
						throw;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06003828 RID: 14376 RVA: 0x000C5D70 File Offset: 0x000C3F70
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040020E1 RID: 8417
			public int <>1__state;

			// Token: 0x040020E2 RID: 8418
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040020E3 RID: 8419
			public WebRequestStream <>4__this;

			// Token: 0x040020E4 RID: 8420
			public byte[] buffer;

			// Token: 0x040020E5 RID: 8421
			public int offset;

			// Token: 0x040020E6 RID: 8422
			public int size;

			// Token: 0x040020E7 RID: 8423
			public CancellationToken cancellationToken;

			// Token: 0x040020E8 RID: 8424
			public WebCompletionSource completion;

			// Token: 0x040020E9 RID: 8425
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040020EA RID: 8426
			private TaskAwaiter <>u__2;
		}

		// Token: 0x020006D0 RID: 1744
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ProcessWrite>d__34 : IAsyncStateMachine
		{
			// Token: 0x06003829 RID: 14377 RVA: 0x000C5D80 File Offset: 0x000C3F80
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebRequestStream webRequestStream = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						webRequestStream.Operation.ThrowIfClosedOrDisposed(this.cancellationToken);
						if (webRequestStream.sendChunked)
						{
							webRequestStream.requestWritten = true;
							string s = string.Format("{0:X}\r\n", this.size);
							byte[] bytes = Encoding.ASCII.GetBytes(s);
							int num2 = 2 + this.size + bytes.Length;
							byte[] dst = new byte[num2];
							Buffer.BlockCopy(bytes, 0, dst, 0, bytes.Length);
							Buffer.BlockCopy(this.buffer, this.offset, dst, bytes.Length, this.size);
							Buffer.BlockCopy(WebRequestStream.crlf, 0, dst, bytes.Length + this.size, WebRequestStream.crlf.Length);
							if (webRequestStream.allowBuffering)
							{
								if (webRequestStream.writeBuffer == null)
								{
									webRequestStream.writeBuffer = new MemoryStream();
								}
								webRequestStream.writeBuffer.Write(this.buffer, this.offset, this.size);
							}
							webRequestStream.totalWritten += (long)this.size;
							this.buffer = dst;
							this.offset = 0;
							this.size = num2;
						}
						else
						{
							webRequestStream.CheckWriteOverflow(webRequestStream.Request.ContentLength, webRequestStream.totalWritten, (long)this.size);
							if (webRequestStream.allowBuffering)
							{
								if (webRequestStream.writeBuffer == null)
								{
									webRequestStream.writeBuffer = new MemoryStream();
								}
								webRequestStream.writeBuffer.Write(this.buffer, this.offset, this.size);
								webRequestStream.totalWritten += (long)this.size;
								if (webRequestStream.Request.ContentLength <= 0L || webRequestStream.totalWritten < webRequestStream.Request.ContentLength)
								{
									goto IL_292;
								}
								webRequestStream.requestWritten = true;
								this.buffer = webRequestStream.writeBuffer.GetBuffer();
								this.offset = 0;
								this.size = (int)webRequestStream.totalWritten;
							}
							else
							{
								webRequestStream.totalWritten += (long)this.size;
							}
						}
						awaiter = webRequestStream.InnerStream.WriteAsync(this.buffer, this.offset, this.size, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebRequestStream.<ProcessWrite>d__34>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_292:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600382A RID: 14378 RVA: 0x000C6050 File Offset: 0x000C4250
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040020EB RID: 8427
			public int <>1__state;

			// Token: 0x040020EC RID: 8428
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040020ED RID: 8429
			public WebRequestStream <>4__this;

			// Token: 0x040020EE RID: 8430
			public CancellationToken cancellationToken;

			// Token: 0x040020EF RID: 8431
			public int size;

			// Token: 0x040020F0 RID: 8432
			public byte[] buffer;

			// Token: 0x040020F1 RID: 8433
			public int offset;

			// Token: 0x040020F2 RID: 8434
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020006D1 RID: 1745
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <Initialize>d__36 : IAsyncStateMachine
		{
			// Token: 0x0600382B RID: 14379 RVA: 0x000C6060 File Offset: 0x000C4260
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebRequestStream webRequestStream = this.<>4__this;
				try
				{
					TaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(TaskAwaiter);
							this.<>1__state = -1;
							goto IL_161;
						}
						webRequestStream.Operation.ThrowIfClosedOrDisposed(this.cancellationToken);
						if (webRequestStream.Operation.WriteBuffer != null)
						{
							if (webRequestStream.Operation.IsNtlmChallenge)
							{
								webRequestStream.Request.InternalContentLength = 0L;
							}
							else
							{
								webRequestStream.Request.InternalContentLength = (long)webRequestStream.Operation.WriteBuffer.Size;
							}
						}
						awaiter2 = webRequestStream.SetHeadersAsync(false, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebRequestStream.<Initialize>d__36>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter2.GetResult();
					webRequestStream.Operation.ThrowIfClosedOrDisposed(this.cancellationToken);
					if (webRequestStream.Operation.WriteBuffer == null || webRequestStream.Operation.IsNtlmChallenge)
					{
						goto IL_16E;
					}
					awaiter = webRequestStream.WriteRequestAsync(this.cancellationToken).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, WebRequestStream.<Initialize>d__36>(ref awaiter, ref this);
						return;
					}
					IL_161:
					awaiter.GetResult();
					webRequestStream.Close();
					IL_16E:;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600382C RID: 14380 RVA: 0x000C6228 File Offset: 0x000C4428
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040020F3 RID: 8435
			public int <>1__state;

			// Token: 0x040020F4 RID: 8436
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040020F5 RID: 8437
			public WebRequestStream <>4__this;

			// Token: 0x040020F6 RID: 8438
			public CancellationToken cancellationToken;

			// Token: 0x040020F7 RID: 8439
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040020F8 RID: 8440
			private TaskAwaiter <>u__2;
		}

		// Token: 0x020006D2 RID: 1746
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SetHeadersAsync>d__37 : IAsyncStateMachine
		{
			// Token: 0x0600382D RID: 14381 RVA: 0x000C6238 File Offset: 0x000C4438
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebRequestStream webRequestStream = this.<>4__this;
				try
				{
					if (num != 0)
					{
						webRequestStream.Operation.ThrowIfClosedOrDisposed(this.cancellationToken);
						if (webRequestStream.headersSent)
						{
							goto IL_23D;
						}
						string method = webRequestStream.Request.Method;
						bool flag = method == "GET" || method == "CONNECT" || method == "HEAD" || method == "TRACE";
						bool flag2 = method == "PROPFIND" || method == "PROPPATCH" || method == "MKCOL" || method == "COPY" || method == "MOVE" || method == "LOCK" || method == "UNLOCK";
						if (webRequestStream.Operation.IsNtlmChallenge)
						{
							flag = true;
						}
						if (this.setInternalLength && !flag && webRequestStream.HasWriteBuffer)
						{
							webRequestStream.Request.InternalContentLength = (long)webRequestStream.WriteBufferLength;
						}
						bool flag3 = !flag && (!webRequestStream.HasWriteBuffer || webRequestStream.Request.ContentLength > -1L);
						if (!webRequestStream.sendChunked && !flag3 && !flag && !flag2)
						{
							goto IL_23D;
						}
						webRequestStream.headersSent = true;
						webRequestStream.headers = webRequestStream.Request.GetRequestHeaders();
					}
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = webRequestStream.InnerStream.WriteAsync(webRequestStream.headers, 0, webRequestStream.headers.Length, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebRequestStream.<SetHeadersAsync>d__37>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
						}
						awaiter.GetResult();
						long contentLength = webRequestStream.Request.ContentLength;
						if (!webRequestStream.sendChunked && contentLength == 0L)
						{
							webRequestStream.requestWritten = true;
						}
					}
					catch (Exception ex)
					{
						if (ex is WebException || ex is OperationCanceledException)
						{
							throw;
						}
						throw new WebException("Error writing headers", WebExceptionStatus.SendFailure, WebExceptionInternalStatus.RequestFatal, ex);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_23D:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600382E RID: 14382 RVA: 0x000C64CC File Offset: 0x000C46CC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040020F9 RID: 8441
			public int <>1__state;

			// Token: 0x040020FA RID: 8442
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040020FB RID: 8443
			public WebRequestStream <>4__this;

			// Token: 0x040020FC RID: 8444
			public CancellationToken cancellationToken;

			// Token: 0x040020FD RID: 8445
			public bool setInternalLength;

			// Token: 0x040020FE RID: 8446
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020006D3 RID: 1747
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteRequestAsync>d__38 : IAsyncStateMachine
		{
			// Token: 0x0600382F RID: 14383 RVA: 0x000C64DC File Offset: 0x000C46DC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebRequestStream webRequestStream = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					TaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(TaskAwaiter);
						this.<>1__state = -1;
						goto IL_1DA;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(TaskAwaiter);
						this.<>1__state = -1;
						goto IL_23B;
					default:
						webRequestStream.Operation.ThrowIfClosedOrDisposed(this.cancellationToken);
						if (webRequestStream.requestWritten)
						{
							goto IL_264;
						}
						webRequestStream.requestWritten = true;
						if (webRequestStream.sendChunked || !webRequestStream.HasWriteBuffer)
						{
							goto IL_264;
						}
						this.<buffer>5__2 = webRequestStream.GetWriteBuffer();
						if (this.<buffer>5__2 != null && !webRequestStream.Operation.IsNtlmChallenge && webRequestStream.Request.ContentLength != -1L && webRequestStream.Request.ContentLength < (long)this.<buffer>5__2.Size)
						{
							webRequestStream.closed = true;
							WebException ex = new WebException("Specified Content-Length is less than the number of bytes to write", null, WebExceptionStatus.ServerProtocolViolation, null);
							webRequestStream.Operation.CompleteRequestWritten(webRequestStream, ex);
							throw ex;
						}
						awaiter = webRequestStream.SetHeadersAsync(true, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebRequestStream.<WriteRequestAsync>d__38>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					webRequestStream.Operation.ThrowIfClosedOrDisposed(this.cancellationToken);
					if (this.<buffer>5__2 == null || this.<buffer>5__2.Size <= 0)
					{
						goto IL_1E1;
					}
					awaiter2 = webRequestStream.InnerStream.WriteAsync(this.<buffer>5__2.Buffer, 0, this.<buffer>5__2.Size, this.cancellationToken).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, WebRequestStream.<WriteRequestAsync>d__38>(ref awaiter2, ref this);
						return;
					}
					IL_1DA:
					awaiter2.GetResult();
					IL_1E1:
					awaiter2 = webRequestStream.FinishWriting(this.cancellationToken).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, WebRequestStream.<WriteRequestAsync>d__38>(ref awaiter2, ref this);
						return;
					}
					IL_23B:
					awaiter2.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<buffer>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_264:
				this.<>1__state = -2;
				this.<buffer>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06003830 RID: 14384 RVA: 0x000C6784 File Offset: 0x000C4984
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040020FF RID: 8447
			public int <>1__state;

			// Token: 0x04002100 RID: 8448
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04002101 RID: 8449
			public WebRequestStream <>4__this;

			// Token: 0x04002102 RID: 8450
			public CancellationToken cancellationToken;

			// Token: 0x04002103 RID: 8451
			private BufferOffsetSize <buffer>5__2;

			// Token: 0x04002104 RID: 8452
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04002105 RID: 8453
			private TaskAwaiter <>u__2;
		}

		// Token: 0x020006D4 RID: 1748
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteChunkTrailer_inner>d__39 : IAsyncStateMachine
		{
			// Token: 0x06003831 RID: 14385 RVA: 0x000C6794 File Offset: 0x000C4994
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebRequestStream webRequestStream = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (Interlocked.CompareExchange(ref webRequestStream.chunkTrailerWritten, 1, 0) != 0)
						{
							goto IL_D6;
						}
						webRequestStream.Operation.ThrowIfClosedOrDisposed(this.cancellationToken);
						byte[] bytes = Encoding.ASCII.GetBytes("0\r\n\r\n");
						awaiter = webRequestStream.InnerStream.WriteAsync(bytes, 0, bytes.Length, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebRequestStream.<WriteChunkTrailer_inner>d__39>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_D6:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06003832 RID: 14386 RVA: 0x000C689C File Offset: 0x000C4A9C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002106 RID: 8454
			public int <>1__state;

			// Token: 0x04002107 RID: 8455
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04002108 RID: 8456
			public WebRequestStream <>4__this;

			// Token: 0x04002109 RID: 8457
			public CancellationToken cancellationToken;

			// Token: 0x0400210A RID: 8458
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020006D5 RID: 1749
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteChunkTrailer>d__40 : IAsyncStateMachine
		{
			// Token: 0x06003833 RID: 14387 RVA: 0x000C68AC File Offset: 0x000C4AAC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebRequestStream webRequestStream = this.<>4__this;
				try
				{
					if (num > 1)
					{
						this.<cts>5__2 = new CancellationTokenSource();
					}
					try
					{
						ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter awaiter;
						if (num == 0)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_EE;
						}
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
						if (num == 1)
						{
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_179;
						}
						this.<cts>5__2.CancelAfter(webRequestStream.WriteTimeout);
						this.<timeoutTask>5__3 = Task.Delay(webRequestStream.WriteTimeout, this.<cts>5__2.Token);
						IL_58:
						WebCompletionSource value = new WebCompletionSource();
						WebCompletionSource webCompletionSource = Interlocked.CompareExchange<WebCompletionSource>(ref webRequestStream.pendingWrite, value, null);
						if (webCompletionSource != null)
						{
							Task<object> task = webCompletionSource.WaitForCompletion();
							awaiter = Task.WhenAny(new Task[]
							{
								this.<timeoutTask>5__3,
								task
							}).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter, WebRequestStream.<WriteChunkTrailer>d__40>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = webRequestStream.WriteChunkTrailer_inner(this.<cts>5__2.Token).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 1);
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebRequestStream.<WriteChunkTrailer>d__40>(ref awaiter2, ref this);
								return;
							}
							goto IL_179;
						}
						IL_EE:
						if (awaiter.GetResult() == this.<timeoutTask>5__3)
						{
							throw new WebException("The operation has timed out.", WebExceptionStatus.Timeout);
						}
						goto IL_58;
						IL_179:
						awaiter2.GetResult();
						this.<timeoutTask>5__3 = null;
					}
					catch
					{
					}
					finally
					{
						if (num < 0)
						{
							webRequestStream.pendingWrite = null;
							this.<cts>5__2.Cancel();
							this.<cts>5__2.Dispose();
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<cts>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<cts>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06003834 RID: 14388 RVA: 0x000C6AF4 File Offset: 0x000C4CF4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400210B RID: 8459
			public int <>1__state;

			// Token: 0x0400210C RID: 8460
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400210D RID: 8461
			public WebRequestStream <>4__this;

			// Token: 0x0400210E RID: 8462
			private CancellationTokenSource <cts>5__2;

			// Token: 0x0400210F RID: 8463
			private Task <timeoutTask>5__3;

			// Token: 0x04002110 RID: 8464
			private ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04002111 RID: 8465
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}
	}
}
