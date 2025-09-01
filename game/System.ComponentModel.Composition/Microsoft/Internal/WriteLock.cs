using System;
using System.Threading;

namespace Microsoft.Internal
{
	// Token: 0x0200000A RID: 10
	internal struct WriteLock : IDisposable
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002817 File Offset: 0x00000A17
		public WriteLock(Lock @lock)
		{
			this._isDisposed = 0;
			this._lock = @lock;
			this._lock.EnterWriteLock();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002832 File Offset: 0x00000A32
		public void Dispose()
		{
			if (Interlocked.CompareExchange(ref this._isDisposed, 1, 0) == 0)
			{
				this._lock.ExitWriteLock();
			}
		}

		// Token: 0x04000040 RID: 64
		private readonly Lock _lock;

		// Token: 0x04000041 RID: 65
		private int _isDisposed;
	}
}
