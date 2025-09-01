using System;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200055B RID: 1371
	internal class ContextAwareResult : LazyAsyncResult
	{
		// Token: 0x06002C82 RID: 11394 RVA: 0x00097AF2 File Offset: 0x00095CF2
		private void SafeCaptureIdentity()
		{
			this._windowsIdentity = WindowsIdentity.GetCurrent();
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06002C83 RID: 11395 RVA: 0x00097B00 File Offset: 0x00095D00
		internal WindowsIdentity Identity
		{
			get
			{
				if (base.InternalPeekCompleted)
				{
					if ((this._flags & ContextAwareResult.StateFlags.ThreadSafeContextCopy) == ContextAwareResult.StateFlags.None)
					{
						NetEventSource.Fail(this, "Called on completed result.", "Identity");
					}
					throw new InvalidOperationException("This operation cannot be performed on a completed asynchronous result object.");
				}
				if (this._windowsIdentity != null)
				{
					return this._windowsIdentity;
				}
				if ((this._flags & ContextAwareResult.StateFlags.CaptureIdentity) == ContextAwareResult.StateFlags.None)
				{
					NetEventSource.Fail(this, "No identity captured - specify captureIdentity.", "Identity");
				}
				if ((this._flags & ContextAwareResult.StateFlags.PostBlockFinished) == ContextAwareResult.StateFlags.None)
				{
					if (this._lock == null)
					{
						NetEventSource.Fail(this, "Must lock (StartPostingAsyncOp()) { ... FinishPostingAsyncOp(); } when calling Identity (unless it's only called after FinishPostingAsyncOp).", "Identity");
					}
					object @lock = this._lock;
					lock (@lock)
					{
					}
				}
				if (base.InternalPeekCompleted)
				{
					if ((this._flags & ContextAwareResult.StateFlags.ThreadSafeContextCopy) == ContextAwareResult.StateFlags.None)
					{
						NetEventSource.Fail(this, "Result became completed during call.", "Identity");
					}
					throw new InvalidOperationException("This operation cannot be performed on a completed asynchronous result object.");
				}
				return this._windowsIdentity;
			}
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x00097BE8 File Offset: 0x00095DE8
		private void CleanupInternal()
		{
			if (this._windowsIdentity != null)
			{
				this._windowsIdentity.Dispose();
				this._windowsIdentity = null;
			}
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x00097C04 File Offset: 0x00095E04
		internal ContextAwareResult(object myObject, object myState, AsyncCallback myCallBack) : this(false, false, myObject, myState, myCallBack)
		{
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x00097C11 File Offset: 0x00095E11
		internal ContextAwareResult(bool captureIdentity, bool forceCaptureContext, object myObject, object myState, AsyncCallback myCallBack) : this(captureIdentity, forceCaptureContext, false, myObject, myState, myCallBack)
		{
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x00097C21 File Offset: 0x00095E21
		internal ContextAwareResult(bool captureIdentity, bool forceCaptureContext, bool threadSafeContextCopy, object myObject, object myState, AsyncCallback myCallBack) : base(myObject, myState, myCallBack)
		{
			if (forceCaptureContext)
			{
				this._flags = ContextAwareResult.StateFlags.CaptureContext;
			}
			if (captureIdentity)
			{
				this._flags |= ContextAwareResult.StateFlags.CaptureIdentity;
			}
			if (threadSafeContextCopy)
			{
				this._flags |= ContextAwareResult.StateFlags.ThreadSafeContextCopy;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06002C88 RID: 11400 RVA: 0x00097C5C File Offset: 0x00095E5C
		internal ExecutionContext ContextCopy
		{
			get
			{
				if (base.InternalPeekCompleted)
				{
					if ((this._flags & ContextAwareResult.StateFlags.ThreadSafeContextCopy) == ContextAwareResult.StateFlags.None)
					{
						NetEventSource.Fail(this, "Called on completed result.", "ContextCopy");
					}
					throw new InvalidOperationException("This operation cannot be performed on a completed asynchronous result object.");
				}
				ExecutionContext context = this._context;
				if (context != null)
				{
					return context;
				}
				if (base.AsyncCallback == null && (this._flags & ContextAwareResult.StateFlags.CaptureContext) == ContextAwareResult.StateFlags.None)
				{
					NetEventSource.Fail(this, "No context captured - specify a callback or forceCaptureContext.", "ContextCopy");
				}
				if ((this._flags & ContextAwareResult.StateFlags.PostBlockFinished) == ContextAwareResult.StateFlags.None)
				{
					if (this._lock == null)
					{
						NetEventSource.Fail(this, "Must lock (StartPostingAsyncOp()) { ... FinishPostingAsyncOp(); } when calling ContextCopy (unless it's only called after FinishPostingAsyncOp).", "ContextCopy");
					}
					object @lock = this._lock;
					lock (@lock)
					{
					}
				}
				if (base.InternalPeekCompleted)
				{
					if ((this._flags & ContextAwareResult.StateFlags.ThreadSafeContextCopy) == ContextAwareResult.StateFlags.None)
					{
						NetEventSource.Fail(this, "Result became completed during call.", "ContextCopy");
					}
					throw new InvalidOperationException("This operation cannot be performed on a completed asynchronous result object.");
				}
				return this._context;
			}
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x00097D4C File Offset: 0x00095F4C
		internal object StartPostingAsyncOp()
		{
			return this.StartPostingAsyncOp(true);
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x00097D55 File Offset: 0x00095F55
		internal object StartPostingAsyncOp(bool lockCapture)
		{
			if (base.InternalPeekCompleted)
			{
				NetEventSource.Fail(this, "Called on completed result.", "StartPostingAsyncOp");
			}
			this._lock = (lockCapture ? new object() : null);
			this._flags |= ContextAwareResult.StateFlags.PostBlockStarted;
			return this._lock;
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x00097D94 File Offset: 0x00095F94
		internal bool FinishPostingAsyncOp()
		{
			if ((this._flags & (ContextAwareResult.StateFlags.PostBlockStarted | ContextAwareResult.StateFlags.PostBlockFinished)) != ContextAwareResult.StateFlags.PostBlockStarted)
			{
				return false;
			}
			this._flags |= ContextAwareResult.StateFlags.PostBlockFinished;
			ExecutionContext executionContext = null;
			return this.CaptureOrComplete(ref executionContext, false);
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x00097DCC File Offset: 0x00095FCC
		internal bool FinishPostingAsyncOp(ref CallbackClosure closure)
		{
			if ((this._flags & (ContextAwareResult.StateFlags.PostBlockStarted | ContextAwareResult.StateFlags.PostBlockFinished)) != ContextAwareResult.StateFlags.PostBlockStarted)
			{
				return false;
			}
			this._flags |= ContextAwareResult.StateFlags.PostBlockFinished;
			CallbackClosure callbackClosure = closure;
			ExecutionContext executionContext;
			if (callbackClosure == null)
			{
				executionContext = null;
			}
			else if (!callbackClosure.IsCompatible(base.AsyncCallback))
			{
				closure = null;
				executionContext = null;
			}
			else
			{
				base.AsyncCallback = callbackClosure.AsyncCallback;
				executionContext = callbackClosure.Context;
			}
			bool result = this.CaptureOrComplete(ref executionContext, true);
			if (closure == null && base.AsyncCallback != null && executionContext != null)
			{
				closure = new CallbackClosure(executionContext, base.AsyncCallback);
			}
			return result;
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x00097E4E File Offset: 0x0009604E
		protected override void Cleanup()
		{
			base.Cleanup();
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "Cleanup");
			}
			this.CleanupInternal();
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x00097E70 File Offset: 0x00096070
		private bool CaptureOrComplete(ref ExecutionContext cachedContext, bool returnContext)
		{
			if ((this._flags & ContextAwareResult.StateFlags.PostBlockStarted) == ContextAwareResult.StateFlags.None)
			{
				NetEventSource.Fail(this, "Called without calling StartPostingAsyncOp.", "CaptureOrComplete");
			}
			bool flag = base.AsyncCallback != null || (this._flags & ContextAwareResult.StateFlags.CaptureContext) > ContextAwareResult.StateFlags.None;
			if ((this._flags & ContextAwareResult.StateFlags.CaptureIdentity) != ContextAwareResult.StateFlags.None && !base.InternalPeekCompleted && !flag)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "starting identity capture", "CaptureOrComplete");
				}
				this.SafeCaptureIdentity();
			}
			if (flag && !base.InternalPeekCompleted)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "starting capture", "CaptureOrComplete");
				}
				if (cachedContext == null)
				{
					cachedContext = ExecutionContext.Capture();
				}
				if (cachedContext != null)
				{
					if (!returnContext)
					{
						this._context = cachedContext;
						cachedContext = null;
					}
					else
					{
						this._context = cachedContext;
					}
				}
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("_context:{0}", new object[]
					{
						this._context
					}), "CaptureOrComplete");
				}
			}
			else
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "Skipping capture", "CaptureOrComplete");
				}
				cachedContext = null;
				if (base.AsyncCallback != null && !base.CompletedSynchronously)
				{
					NetEventSource.Fail(this, "Didn't capture context, but didn't complete synchronously!", "CaptureOrComplete");
				}
			}
			if (base.CompletedSynchronously)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "Completing synchronously", "CaptureOrComplete");
				}
				base.Complete(IntPtr.Zero);
				return true;
			}
			return false;
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x00097FC4 File Offset: 0x000961C4
		protected override void Complete(IntPtr userToken)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("_context(set):{0} userToken:{1}", new object[]
				{
					this._context != null,
					userToken
				}), "Complete");
			}
			if ((this._flags & ContextAwareResult.StateFlags.PostBlockStarted) == ContextAwareResult.StateFlags.None)
			{
				base.Complete(userToken);
				return;
			}
			if (base.CompletedSynchronously)
			{
				return;
			}
			ExecutionContext context = this._context;
			if (userToken != IntPtr.Zero || context == null)
			{
				base.Complete(userToken);
				return;
			}
			ExecutionContext.Run(context, delegate(object s)
			{
				((ContextAwareResult)s).CompleteCallback();
			}, this);
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x00098071 File Offset: 0x00096271
		private void CompleteCallback()
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, "Context set, calling callback.", "CompleteCallback");
			}
			base.Complete(IntPtr.Zero);
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002C91 RID: 11409 RVA: 0x00002F6A File Offset: 0x0000116A
		internal virtual EndPoint RemoteEndPoint
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040017EA RID: 6122
		private WindowsIdentity _windowsIdentity;

		// Token: 0x040017EB RID: 6123
		private volatile ExecutionContext _context;

		// Token: 0x040017EC RID: 6124
		private object _lock;

		// Token: 0x040017ED RID: 6125
		private ContextAwareResult.StateFlags _flags;

		// Token: 0x0200055C RID: 1372
		[Flags]
		private enum StateFlags : byte
		{
			// Token: 0x040017EF RID: 6127
			None = 0,
			// Token: 0x040017F0 RID: 6128
			CaptureIdentity = 1,
			// Token: 0x040017F1 RID: 6129
			CaptureContext = 2,
			// Token: 0x040017F2 RID: 6130
			ThreadSafeContextCopy = 4,
			// Token: 0x040017F3 RID: 6131
			PostBlockStarted = 8,
			// Token: 0x040017F4 RID: 6132
			PostBlockFinished = 16
		}

		// Token: 0x0200055D RID: 1373
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002C92 RID: 11410 RVA: 0x00098095 File Offset: 0x00096295
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002C93 RID: 11411 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c()
			{
			}

			// Token: 0x06002C94 RID: 11412 RVA: 0x000980A1 File Offset: 0x000962A1
			internal void <Complete>b__20_0(object s)
			{
				((ContextAwareResult)s).CompleteCallback();
			}

			// Token: 0x040017F5 RID: 6133
			public static readonly ContextAwareResult.<>c <>9 = new ContextAwareResult.<>c();

			// Token: 0x040017F6 RID: 6134
			public static ContextCallback <>9__20_0;
		}
	}
}
