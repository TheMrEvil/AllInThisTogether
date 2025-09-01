using System;
using System.Threading;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Hosting
{
	// Token: 0x020000CE RID: 206
	internal sealed class CompositionLock : IDisposable
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x0000FF14 File Offset: 0x0000E114
		public CompositionLock(bool isThreadSafe)
		{
			this._isThreadSafe = isThreadSafe;
			if (isThreadSafe)
			{
				this._stateLock = new Microsoft.Internal.Lock();
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000FF31 File Offset: 0x0000E131
		public void Dispose()
		{
			if (this._isThreadSafe && Interlocked.CompareExchange(ref this._isDisposed, 1, 0) == 0)
			{
				this._stateLock.Dispose();
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0000FF55 File Offset: 0x0000E155
		public bool IsThreadSafe
		{
			get
			{
				return this._isThreadSafe;
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000FF5D File Offset: 0x0000E15D
		private void EnterCompositionLock()
		{
			if (this._isThreadSafe)
			{
				Monitor.Enter(CompositionLock._compositionLock);
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000FF71 File Offset: 0x0000E171
		private void ExitCompositionLock()
		{
			if (this._isThreadSafe)
			{
				Monitor.Exit(CompositionLock._compositionLock);
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000FF85 File Offset: 0x0000E185
		public IDisposable LockComposition()
		{
			if (this._isThreadSafe)
			{
				return new CompositionLock.CompositionLockHolder(this);
			}
			return CompositionLock._EmptyLockHolder;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000FF9B File Offset: 0x0000E19B
		public IDisposable LockStateForRead()
		{
			if (this._isThreadSafe)
			{
				return new ReadLock(this._stateLock);
			}
			return CompositionLock._EmptyLockHolder;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000FFBB File Offset: 0x0000E1BB
		public IDisposable LockStateForWrite()
		{
			if (this._isThreadSafe)
			{
				return new WriteLock(this._stateLock);
			}
			return CompositionLock._EmptyLockHolder;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000FFDB File Offset: 0x0000E1DB
		// Note: this type is marked as 'beforefieldinit'.
		static CompositionLock()
		{
		}

		// Token: 0x04000247 RID: 583
		private readonly Microsoft.Internal.Lock _stateLock;

		// Token: 0x04000248 RID: 584
		private static object _compositionLock = new object();

		// Token: 0x04000249 RID: 585
		private int _isDisposed;

		// Token: 0x0400024A RID: 586
		private bool _isThreadSafe;

		// Token: 0x0400024B RID: 587
		private static readonly CompositionLock.EmptyLockHolder _EmptyLockHolder = new CompositionLock.EmptyLockHolder();

		// Token: 0x020000CF RID: 207
		public sealed class CompositionLockHolder : IDisposable
		{
			// Token: 0x06000553 RID: 1363 RVA: 0x0000FFF1 File Offset: 0x0000E1F1
			public CompositionLockHolder(CompositionLock @lock)
			{
				this._lock = @lock;
				this._isDisposed = 0;
				this._lock.EnterCompositionLock();
			}

			// Token: 0x06000554 RID: 1364 RVA: 0x00010012 File Offset: 0x0000E212
			public void Dispose()
			{
				if (Interlocked.CompareExchange(ref this._isDisposed, 1, 0) == 0)
				{
					this._lock.ExitCompositionLock();
				}
			}

			// Token: 0x0400024C RID: 588
			private CompositionLock _lock;

			// Token: 0x0400024D RID: 589
			private int _isDisposed;
		}

		// Token: 0x020000D0 RID: 208
		private sealed class EmptyLockHolder : IDisposable
		{
			// Token: 0x06000555 RID: 1365 RVA: 0x000028FF File Offset: 0x00000AFF
			public void Dispose()
			{
			}

			// Token: 0x06000556 RID: 1366 RVA: 0x00002BAC File Offset: 0x00000DAC
			public EmptyLockHolder()
			{
			}
		}
	}
}
