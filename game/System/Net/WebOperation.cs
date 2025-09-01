using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020006C8 RID: 1736
	internal class WebOperation
	{
		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x060037CE RID: 14286 RVA: 0x000C4632 File Offset: 0x000C2832
		public HttpWebRequest Request
		{
			[CompilerGenerated]
			get
			{
				return this.<Request>k__BackingField;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x060037CF RID: 14287 RVA: 0x000C463A File Offset: 0x000C283A
		// (set) Token: 0x060037D0 RID: 14288 RVA: 0x000C4642 File Offset: 0x000C2842
		public WebConnection Connection
		{
			[CompilerGenerated]
			get
			{
				return this.<Connection>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Connection>k__BackingField = value;
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x060037D1 RID: 14289 RVA: 0x000C464B File Offset: 0x000C284B
		// (set) Token: 0x060037D2 RID: 14290 RVA: 0x000C4653 File Offset: 0x000C2853
		public ServicePoint ServicePoint
		{
			[CompilerGenerated]
			get
			{
				return this.<ServicePoint>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ServicePoint>k__BackingField = value;
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x060037D3 RID: 14291 RVA: 0x000C465C File Offset: 0x000C285C
		public BufferOffsetSize WriteBuffer
		{
			[CompilerGenerated]
			get
			{
				return this.<WriteBuffer>k__BackingField;
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x060037D4 RID: 14292 RVA: 0x000C4664 File Offset: 0x000C2864
		public bool IsNtlmChallenge
		{
			[CompilerGenerated]
			get
			{
				return this.<IsNtlmChallenge>k__BackingField;
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x060037D5 RID: 14293 RVA: 0x00002F6A File Offset: 0x0000116A
		internal string ME
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000C466C File Offset: 0x000C286C
		public WebOperation(HttpWebRequest request, BufferOffsetSize writeBuffer, bool isNtlmChallenge, CancellationToken cancellationToken)
		{
			this.Request = request;
			this.WriteBuffer = writeBuffer;
			this.IsNtlmChallenge = isNtlmChallenge;
			this.cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			this.requestTask = new WebCompletionSource<WebRequestStream>(true);
			this.requestWrittenTask = new WebCompletionSource<WebRequestStream>(true);
			this.responseTask = new WebCompletionSource<WebResponseStream>(true);
			this.finishedTask = new WebCompletionSource<ValueTuple<bool, WebOperation>>(true);
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x060037D7 RID: 14295 RVA: 0x000C46D1 File Offset: 0x000C28D1
		public bool Aborted
		{
			get
			{
				return this.disposedInfo != null || this.Request.Aborted || (this.cts != null && this.cts.IsCancellationRequested);
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x060037D8 RID: 14296 RVA: 0x000C4702 File Offset: 0x000C2902
		public bool Closed
		{
			get
			{
				return this.Aborted || this.closedInfo != null;
			}
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x000C4717 File Offset: 0x000C2917
		public void Abort()
		{
			if (!this.SetDisposed(ref this.disposedInfo).Item2)
			{
				return;
			}
			CancellationTokenSource cancellationTokenSource = this.cts;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
			this.SetCanceled();
			this.Close();
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x000C474C File Offset: 0x000C294C
		public void Close()
		{
			if (!this.SetDisposed(ref this.closedInfo).Item2)
			{
				return;
			}
			WebRequestStream webRequestStream = Interlocked.Exchange<WebRequestStream>(ref this.writeStream, null);
			if (webRequestStream != null)
			{
				try
				{
					webRequestStream.Close();
				}
				catch
				{
				}
			}
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x000C4798 File Offset: 0x000C2998
		private void SetCanceled()
		{
			OperationCanceledException error = new OperationCanceledException();
			this.requestTask.TrySetCanceled(error);
			this.requestWrittenTask.TrySetCanceled(error);
			this.responseTask.TrySetCanceled(error);
			this.Finish(false, error);
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x000C47DA File Offset: 0x000C29DA
		private void SetError(Exception error)
		{
			this.requestTask.TrySetException(error);
			this.requestWrittenTask.TrySetException(error);
			this.responseTask.TrySetException(error);
			this.Finish(false, error);
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000C480C File Offset: 0x000C2A0C
		private ValueTuple<ExceptionDispatchInfo, bool> SetDisposed(ref ExceptionDispatchInfo field)
		{
			ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(new WebException(SR.GetString("The request was canceled"), WebExceptionStatus.RequestCanceled));
			ExceptionDispatchInfo exceptionDispatchInfo2 = Interlocked.CompareExchange<ExceptionDispatchInfo>(ref field, exceptionDispatchInfo, null);
			return new ValueTuple<ExceptionDispatchInfo, bool>(exceptionDispatchInfo2 ?? exceptionDispatchInfo, exceptionDispatchInfo2 == null);
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x000C4847 File Offset: 0x000C2A47
		internal ExceptionDispatchInfo CheckDisposed(CancellationToken cancellationToken)
		{
			if (this.Aborted || cancellationToken.IsCancellationRequested)
			{
				return this.CheckThrowDisposed(false, ref this.disposedInfo);
			}
			return null;
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x000C4869 File Offset: 0x000C2A69
		internal void ThrowIfDisposed()
		{
			this.ThrowIfDisposed(CancellationToken.None);
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x000C4876 File Offset: 0x000C2A76
		internal void ThrowIfDisposed(CancellationToken cancellationToken)
		{
			if (this.Aborted || cancellationToken.IsCancellationRequested)
			{
				this.CheckThrowDisposed(true, ref this.disposedInfo);
			}
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x000C4897 File Offset: 0x000C2A97
		internal void ThrowIfClosedOrDisposed()
		{
			this.ThrowIfClosedOrDisposed(CancellationToken.None);
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x000C48A4 File Offset: 0x000C2AA4
		internal void ThrowIfClosedOrDisposed(CancellationToken cancellationToken)
		{
			if (this.Closed || cancellationToken.IsCancellationRequested)
			{
				this.CheckThrowDisposed(true, ref this.closedInfo);
			}
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x000C48C8 File Offset: 0x000C2AC8
		private ExceptionDispatchInfo CheckThrowDisposed(bool throwIt, ref ExceptionDispatchInfo field)
		{
			ValueTuple<ExceptionDispatchInfo, bool> valueTuple = this.SetDisposed(ref field);
			ExceptionDispatchInfo item = valueTuple.Item1;
			if (valueTuple.Item2)
			{
				CancellationTokenSource cancellationTokenSource = this.cts;
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
				}
			}
			if (throwIt)
			{
				item.Throw();
			}
			return item;
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x000C4908 File Offset: 0x000C2B08
		internal void RegisterRequest(ServicePoint servicePoint, WebConnection connection)
		{
			if (servicePoint == null)
			{
				throw new ArgumentNullException("servicePoint");
			}
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			lock (this)
			{
				if (Interlocked.CompareExchange(ref this.requestSent, 1, 0) != 0)
				{
					throw new InvalidOperationException("Invalid nested call.");
				}
				this.ServicePoint = servicePoint;
				this.Connection = connection;
			}
			this.cts.Token.Register(delegate()
			{
				this.Request.FinishedReading = true;
				this.SetDisposed(ref this.disposedInfo);
			});
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x000C49A4 File Offset: 0x000C2BA4
		public void SetPriorityRequest(WebOperation operation)
		{
			lock (this)
			{
				if (this.requestSent != 1 || this.ServicePoint == null || this.finished != 0)
				{
					throw new InvalidOperationException("Should never happen.");
				}
				if (Interlocked.CompareExchange<WebOperation>(ref this.priorityRequest, operation, null) != null)
				{
					throw new InvalidOperationException("Invalid nested request.");
				}
			}
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x000C4A18 File Offset: 0x000C2C18
		public Task<Stream> GetRequestStream()
		{
			WebOperation.<GetRequestStream>d__50 <GetRequestStream>d__;
			<GetRequestStream>d__.<>4__this = this;
			<GetRequestStream>d__.<>t__builder = AsyncTaskMethodBuilder<Stream>.Create();
			<GetRequestStream>d__.<>1__state = -1;
			<GetRequestStream>d__.<>t__builder.Start<WebOperation.<GetRequestStream>d__50>(ref <GetRequestStream>d__);
			return <GetRequestStream>d__.<>t__builder.Task;
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x000C4A5B File Offset: 0x000C2C5B
		internal Task<WebRequestStream> GetRequestStreamInternal()
		{
			return this.requestTask.WaitForCompletion();
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x000C4A68 File Offset: 0x000C2C68
		public Task WaitUntilRequestWritten()
		{
			return this.requestWrittenTask.WaitForCompletion();
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x060037E9 RID: 14313 RVA: 0x000C4A75 File Offset: 0x000C2C75
		public WebRequestStream WriteStream
		{
			get
			{
				this.ThrowIfDisposed();
				return this.writeStream;
			}
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x000C4A83 File Offset: 0x000C2C83
		public Task<WebResponseStream> GetResponseStream()
		{
			return this.responseTask.WaitForCompletion();
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x060037EB RID: 14315 RVA: 0x000C4A90 File Offset: 0x000C2C90
		internal WebCompletionSource<ValueTuple<bool, WebOperation>> Finished
		{
			get
			{
				return this.finishedTask;
			}
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x000C4A98 File Offset: 0x000C2C98
		internal void Run()
		{
			WebOperation.<Run>d__58 <Run>d__;
			<Run>d__.<>4__this = this;
			<Run>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Run>d__.<>1__state = -1;
			<Run>d__.<>t__builder.Start<WebOperation.<Run>d__58>(ref <Run>d__);
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x000C4ACF File Offset: 0x000C2CCF
		internal void CompleteRequestWritten(WebRequestStream stream, Exception error = null)
		{
			if (error != null)
			{
				this.SetError(error);
				return;
			}
			this.requestWrittenTask.TrySetCompleted(stream);
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x000C4AEC File Offset: 0x000C2CEC
		internal void Finish(bool ok, Exception error = null)
		{
			if (Interlocked.CompareExchange(ref this.finished, 1, 0) != 0)
			{
				return;
			}
			WebResponseStream webResponseStream;
			WebOperation webOperation;
			lock (this)
			{
				webResponseStream = Interlocked.Exchange<WebResponseStream>(ref this.responseStream, null);
				webOperation = Interlocked.Exchange<WebOperation>(ref this.priorityRequest, null);
				this.Request.FinishedReading = true;
			}
			if (error != null)
			{
				if (webOperation != null)
				{
					webOperation.SetError(error);
				}
				this.finishedTask.TrySetException(error);
				return;
			}
			bool item = !this.Aborted && ok && webResponseStream != null && webResponseStream.KeepAlive;
			if (webOperation != null && webOperation.Aborted)
			{
				webOperation = null;
				item = false;
			}
			this.finishedTask.TrySetCompleted(new ValueTuple<bool, WebOperation>(item, webOperation));
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x000C4BB4 File Offset: 0x000C2DB4
		[CompilerGenerated]
		private void <RegisterRequest>b__48_0()
		{
			this.Request.FinishedReading = true;
			this.SetDisposed(ref this.disposedInfo);
		}

		// Token: 0x040020A5 RID: 8357
		[CompilerGenerated]
		private readonly HttpWebRequest <Request>k__BackingField;

		// Token: 0x040020A6 RID: 8358
		[CompilerGenerated]
		private WebConnection <Connection>k__BackingField;

		// Token: 0x040020A7 RID: 8359
		[CompilerGenerated]
		private ServicePoint <ServicePoint>k__BackingField;

		// Token: 0x040020A8 RID: 8360
		[CompilerGenerated]
		private readonly BufferOffsetSize <WriteBuffer>k__BackingField;

		// Token: 0x040020A9 RID: 8361
		[CompilerGenerated]
		private readonly bool <IsNtlmChallenge>k__BackingField;

		// Token: 0x040020AA RID: 8362
		internal readonly int ID;

		// Token: 0x040020AB RID: 8363
		private CancellationTokenSource cts;

		// Token: 0x040020AC RID: 8364
		private WebCompletionSource<WebRequestStream> requestTask;

		// Token: 0x040020AD RID: 8365
		private WebCompletionSource<WebRequestStream> requestWrittenTask;

		// Token: 0x040020AE RID: 8366
		private WebCompletionSource<WebResponseStream> responseTask;

		// Token: 0x040020AF RID: 8367
		private WebCompletionSource<ValueTuple<bool, WebOperation>> finishedTask;

		// Token: 0x040020B0 RID: 8368
		private WebRequestStream writeStream;

		// Token: 0x040020B1 RID: 8369
		private WebResponseStream responseStream;

		// Token: 0x040020B2 RID: 8370
		private ExceptionDispatchInfo disposedInfo;

		// Token: 0x040020B3 RID: 8371
		private ExceptionDispatchInfo closedInfo;

		// Token: 0x040020B4 RID: 8372
		private WebOperation priorityRequest;

		// Token: 0x040020B5 RID: 8373
		private int requestSent;

		// Token: 0x040020B6 RID: 8374
		private int finished;

		// Token: 0x020006C9 RID: 1737
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <GetRequestStream>d__50 : IAsyncStateMachine
		{
			// Token: 0x060037F0 RID: 14320 RVA: 0x000C4BD0 File Offset: 0x000C2DD0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebOperation webOperation = this.<>4__this;
				Stream result;
				try
				{
					ConfiguredTaskAwaitable<WebRequestStream>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = webOperation.requestTask.WaitForCompletion().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<WebRequestStream>.ConfiguredTaskAwaiter, WebOperation.<GetRequestStream>d__50>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<WebRequestStream>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					result = awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060037F1 RID: 14321 RVA: 0x000C4C98 File Offset: 0x000C2E98
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040020B7 RID: 8375
			public int <>1__state;

			// Token: 0x040020B8 RID: 8376
			public AsyncTaskMethodBuilder<Stream> <>t__builder;

			// Token: 0x040020B9 RID: 8377
			public WebOperation <>4__this;

			// Token: 0x040020BA RID: 8378
			private ConfiguredTaskAwaitable<WebRequestStream>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020006CA RID: 1738
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <Run>d__58 : IAsyncStateMachine
		{
			// Token: 0x060037F2 RID: 14322 RVA: 0x000C4CA8 File Offset: 0x000C2EA8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebOperation webOperation = this.<>4__this;
				try
				{
					try
					{
						ConfiguredTaskAwaitable<WebRequestStream>.ConfiguredTaskAwaiter awaiter;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
						switch (num)
						{
						case 0:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<WebRequestStream>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							break;
						case 1:
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_12C;
						case 2:
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_1D9;
						default:
							webOperation.ThrowIfClosedOrDisposed();
							awaiter = webOperation.Connection.InitConnection(webOperation, webOperation.cts.Token).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<WebRequestStream>.ConfiguredTaskAwaiter, WebOperation.<Run>d__58>(ref awaiter, ref this);
								return;
							}
							break;
						}
						WebRequestStream result = awaiter.GetResult();
						this.<requestStream>5__2 = result;
						webOperation.ThrowIfClosedOrDisposed();
						webOperation.writeStream = this.<requestStream>5__2;
						awaiter2 = this.<requestStream>5__2.Initialize(webOperation.cts.Token).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebOperation.<Run>d__58>(ref awaiter2, ref this);
							return;
						}
						IL_12C:
						awaiter2.GetResult();
						webOperation.ThrowIfClosedOrDisposed();
						webOperation.requestTask.TrySetCompleted(this.<requestStream>5__2);
						this.<stream>5__3 = new WebResponseStream(this.<requestStream>5__2);
						webOperation.responseStream = this.<stream>5__3;
						awaiter2 = this.<stream>5__3.InitReadAsync(webOperation.cts.Token).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebOperation.<Run>d__58>(ref awaiter2, ref this);
							return;
						}
						IL_1D9:
						awaiter2.GetResult();
						webOperation.responseTask.TrySetCompleted(this.<stream>5__3);
						this.<requestStream>5__2 = null;
						this.<stream>5__3 = null;
					}
					catch (OperationCanceledException)
					{
						webOperation.SetCanceled();
					}
					catch (Exception error)
					{
						webOperation.SetError(error);
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

			// Token: 0x060037F3 RID: 14323 RVA: 0x000C4F48 File Offset: 0x000C3148
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040020BB RID: 8379
			public int <>1__state;

			// Token: 0x040020BC RID: 8380
			public AsyncVoidMethodBuilder <>t__builder;

			// Token: 0x040020BD RID: 8381
			public WebOperation <>4__this;

			// Token: 0x040020BE RID: 8382
			private WebRequestStream <requestStream>5__2;

			// Token: 0x040020BF RID: 8383
			private WebResponseStream <stream>5__3;

			// Token: 0x040020C0 RID: 8384
			private ConfiguredTaskAwaitable<WebRequestStream>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040020C1 RID: 8385
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}
	}
}
