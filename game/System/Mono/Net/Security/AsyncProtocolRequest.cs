using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Mono.Net.Security
{
	// Token: 0x0200008B RID: 139
	internal abstract class AsyncProtocolRequest
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000063EE File Offset: 0x000045EE
		public MobileAuthenticatedStream Parent
		{
			[CompilerGenerated]
			get
			{
				return this.<Parent>k__BackingField;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000063F6 File Offset: 0x000045F6
		public bool RunSynchronously
		{
			[CompilerGenerated]
			get
			{
				return this.<RunSynchronously>k__BackingField;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000063FE File Offset: 0x000045FE
		public int ID
		{
			get
			{
				return ++AsyncProtocolRequest.next_id;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000640D File Offset: 0x0000460D
		public string Name
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000641A File Offset: 0x0000461A
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00006422 File Offset: 0x00004622
		public int UserResult
		{
			[CompilerGenerated]
			get
			{
				return this.<UserResult>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<UserResult>k__BackingField = value;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000642B File Offset: 0x0000462B
		public AsyncProtocolRequest(MobileAuthenticatedStream parent, bool sync)
		{
			this.Parent = parent;
			this.RunSynchronously = sync;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("MONO_TLS_DEBUG")]
		protected void Debug(string message, params object[] args)
		{
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000644C File Offset: 0x0000464C
		internal void RequestRead(int size)
		{
			object obj = this.locker;
			lock (obj)
			{
				this.RequestedSize += size;
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00006494 File Offset: 0x00004694
		internal void RequestWrite()
		{
			this.WriteRequested = 1;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000064A0 File Offset: 0x000046A0
		internal Task<AsyncProtocolResult> StartOperation(CancellationToken cancellationToken)
		{
			AsyncProtocolRequest.<StartOperation>d__23 <StartOperation>d__;
			<StartOperation>d__.<>4__this = this;
			<StartOperation>d__.cancellationToken = cancellationToken;
			<StartOperation>d__.<>t__builder = AsyncTaskMethodBuilder<AsyncProtocolResult>.Create();
			<StartOperation>d__.<>1__state = -1;
			<StartOperation>d__.<>t__builder.Start<AsyncProtocolRequest.<StartOperation>d__23>(ref <StartOperation>d__);
			return <StartOperation>d__.<>t__builder.Task;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000064EC File Offset: 0x000046EC
		private Task ProcessOperation(CancellationToken cancellationToken)
		{
			AsyncProtocolRequest.<ProcessOperation>d__24 <ProcessOperation>d__;
			<ProcessOperation>d__.<>4__this = this;
			<ProcessOperation>d__.cancellationToken = cancellationToken;
			<ProcessOperation>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessOperation>d__.<>1__state = -1;
			<ProcessOperation>d__.<>t__builder.Start<AsyncProtocolRequest.<ProcessOperation>d__24>(ref <ProcessOperation>d__);
			return <ProcessOperation>d__.<>t__builder.Task;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00006538 File Offset: 0x00004738
		private Task<int?> InnerRead(CancellationToken cancellationToken)
		{
			AsyncProtocolRequest.<InnerRead>d__25 <InnerRead>d__;
			<InnerRead>d__.<>4__this = this;
			<InnerRead>d__.cancellationToken = cancellationToken;
			<InnerRead>d__.<>t__builder = AsyncTaskMethodBuilder<int?>.Create();
			<InnerRead>d__.<>1__state = -1;
			<InnerRead>d__.<>t__builder.Start<AsyncProtocolRequest.<InnerRead>d__25>(ref <InnerRead>d__);
			return <InnerRead>d__.<>t__builder.Task;
		}

		// Token: 0x06000230 RID: 560
		protected abstract AsyncOperationStatus Run(AsyncOperationStatus status);

		// Token: 0x06000231 RID: 561 RVA: 0x00006583 File Offset: 0x00004783
		public override string ToString()
		{
			return string.Format("[{0}]", this.Name);
		}

		// Token: 0x04000208 RID: 520
		[CompilerGenerated]
		private readonly MobileAuthenticatedStream <Parent>k__BackingField;

		// Token: 0x04000209 RID: 521
		[CompilerGenerated]
		private readonly bool <RunSynchronously>k__BackingField;

		// Token: 0x0400020A RID: 522
		[CompilerGenerated]
		private int <UserResult>k__BackingField;

		// Token: 0x0400020B RID: 523
		private int Started;

		// Token: 0x0400020C RID: 524
		private int RequestedSize;

		// Token: 0x0400020D RID: 525
		private int WriteRequested;

		// Token: 0x0400020E RID: 526
		private readonly object locker = new object();

		// Token: 0x0400020F RID: 527
		private static int next_id;

		// Token: 0x0200008C RID: 140
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <StartOperation>d__23 : IAsyncStateMachine
		{
			// Token: 0x06000232 RID: 562 RVA: 0x00006598 File Offset: 0x00004798
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				AsyncProtocolRequest asyncProtocolRequest = this.<>4__this;
				AsyncProtocolResult result;
				try
				{
					if (num != 0 && Interlocked.CompareExchange(ref asyncProtocolRequest.Started, 1, 0) != 0)
					{
						throw new InvalidOperationException();
					}
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = asyncProtocolRequest.ProcessOperation(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, AsyncProtocolRequest.<StartOperation>d__23>(ref awaiter, ref this);
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
						result = new AsyncProtocolResult(asyncProtocolRequest.UserResult);
					}
					catch (Exception exception)
					{
						result = new AsyncProtocolResult(asyncProtocolRequest.Parent.SetException(exception));
					}
				}
				catch (Exception exception2)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception2);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000233 RID: 563 RVA: 0x000066A8 File Offset: 0x000048A8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000210 RID: 528
			public int <>1__state;

			// Token: 0x04000211 RID: 529
			public AsyncTaskMethodBuilder<AsyncProtocolResult> <>t__builder;

			// Token: 0x04000212 RID: 530
			public AsyncProtocolRequest <>4__this;

			// Token: 0x04000213 RID: 531
			public CancellationToken cancellationToken;

			// Token: 0x04000214 RID: 532
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200008D RID: 141
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ProcessOperation>d__24 : IAsyncStateMachine
		{
			// Token: 0x06000234 RID: 564 RVA: 0x000066B8 File Offset: 0x000048B8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				AsyncProtocolRequest asyncProtocolRequest = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<int?>.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num != 1)
						{
							this.<status>5__2 = AsyncOperationStatus.Initialize;
							goto IL_1AC;
						}
						awaiter = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_199;
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int?>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					IL_93:
					int? result = awaiter2.GetResult();
					if (result != null)
					{
						int? num2 = result;
						int num3 = 0;
						if (num2.GetValueOrDefault() == num3 & num2 != null)
						{
							this.<status>5__2 = AsyncOperationStatus.ReadDone;
						}
						else
						{
							num2 = result;
							num3 = 0;
							if (num2.GetValueOrDefault() < num3 & num2 != null)
							{
								throw new IOException("Remote prematurely closed connection.");
							}
						}
					}
					AsyncOperationStatus asyncOperationStatus = this.<status>5__2;
					if (asyncOperationStatus <= AsyncOperationStatus.ReadDone)
					{
						try
						{
							this.<newStatus>5__3 = asyncProtocolRequest.Run(this.<status>5__2);
							goto IL_11C;
						}
						catch (Exception e)
						{
							throw MobileAuthenticatedStream.GetSSPIException(e);
						}
						goto IL_116;
						IL_11C:
						if (Interlocked.Exchange(ref asyncProtocolRequest.WriteRequested, 0) == 0)
						{
							goto IL_1A0;
						}
						awaiter = asyncProtocolRequest.Parent.InnerWrite(asyncProtocolRequest.RunSynchronously, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__2 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, AsyncProtocolRequest.<ProcessOperation>d__24>(ref awaiter, ref this);
							return;
						}
						goto IL_199;
					}
					IL_116:
					throw new InvalidOperationException();
					IL_199:
					awaiter.GetResult();
					IL_1A0:
					this.<status>5__2 = this.<newStatus>5__3;
					IL_1AC:
					if (this.<status>5__2 != AsyncOperationStatus.Complete)
					{
						this.cancellationToken.ThrowIfCancellationRequested();
						awaiter2 = asyncProtocolRequest.InnerRead(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int?>.ConfiguredTaskAwaiter, AsyncProtocolRequest.<ProcessOperation>d__24>(ref awaiter2, ref this);
							return;
						}
						goto IL_93;
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

			// Token: 0x06000235 RID: 565 RVA: 0x000068E0 File Offset: 0x00004AE0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000215 RID: 533
			public int <>1__state;

			// Token: 0x04000216 RID: 534
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000217 RID: 535
			public CancellationToken cancellationToken;

			// Token: 0x04000218 RID: 536
			public AsyncProtocolRequest <>4__this;

			// Token: 0x04000219 RID: 537
			private AsyncOperationStatus <status>5__2;

			// Token: 0x0400021A RID: 538
			private AsyncOperationStatus <newStatus>5__3;

			// Token: 0x0400021B RID: 539
			private ConfiguredTaskAwaitable<int?>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x0400021C RID: 540
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x0200008E RID: 142
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InnerRead>d__25 : IAsyncStateMachine
		{
			// Token: 0x06000236 RID: 566 RVA: 0x000068F0 File Offset: 0x00004AF0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				AsyncProtocolRequest asyncProtocolRequest = this.<>4__this;
				int? result2;
				try
				{
					if (num != 0)
					{
						this.<totalRead>5__2 = null;
						this.<requestedSize>5__3 = Interlocked.Exchange(ref asyncProtocolRequest.RequestedSize, 0);
						goto IL_133;
					}
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					this.<>1__state = -1;
					IL_AC:
					int result = awaiter.GetResult();
					if (result <= 0)
					{
						result2 = new int?(result);
						goto IL_161;
					}
					if (result > this.<requestedSize>5__3)
					{
						throw new InvalidOperationException();
					}
					this.<totalRead>5__2 += result;
					this.<requestedSize>5__3 -= result;
					int num2 = Interlocked.Exchange(ref asyncProtocolRequest.RequestedSize, 0);
					this.<requestedSize>5__3 += num2;
					IL_133:
					if (this.<requestedSize>5__3 <= 0)
					{
						result2 = this.<totalRead>5__2;
					}
					else
					{
						awaiter = asyncProtocolRequest.Parent.InnerRead(asyncProtocolRequest.RunSynchronously, this.<requestedSize>5__3, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, AsyncProtocolRequest.<InnerRead>d__25>(ref awaiter, ref this);
							return;
						}
						goto IL_AC;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_161:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06000237 RID: 567 RVA: 0x00006A90 File Offset: 0x00004C90
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400021D RID: 541
			public int <>1__state;

			// Token: 0x0400021E RID: 542
			public AsyncTaskMethodBuilder<int?> <>t__builder;

			// Token: 0x0400021F RID: 543
			public AsyncProtocolRequest <>4__this;

			// Token: 0x04000220 RID: 544
			public CancellationToken cancellationToken;

			// Token: 0x04000221 RID: 545
			private int? <totalRead>5__2;

			// Token: 0x04000222 RID: 546
			private int <requestedSize>5__3;

			// Token: 0x04000223 RID: 547
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
