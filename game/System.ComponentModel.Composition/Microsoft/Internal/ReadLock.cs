using System;
using System.Threading;

namespace Microsoft.Internal
{
	// Token: 0x02000009 RID: 9
	internal struct ReadLock : IDisposable
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000027E0 File Offset: 0x000009E0
		public ReadLock(Lock @lock)
		{
			this._isDisposed = 0;
			this._lock = @lock;
			this._lock.EnterReadLock();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027FB File Offset: 0x000009FB
		public void Dispose()
		{
			if (Interlocked.CompareExchange(ref this._isDisposed, 1, 0) == 0)
			{
				this._lock.ExitReadLock();
			}
		}

		// Token: 0x0400003E RID: 62
		private readonly Lock _lock;

		// Token: 0x0400003F RID: 63
		private int _isDisposed;
	}
}
