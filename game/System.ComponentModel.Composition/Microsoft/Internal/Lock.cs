using System;
using System.Threading;

namespace Microsoft.Internal
{
	// Token: 0x0200000B RID: 11
	internal sealed class Lock : IDisposable
	{
		// Token: 0x06000026 RID: 38 RVA: 0x0000284E File Offset: 0x00000A4E
		public void EnterReadLock()
		{
			this._thisLock.EnterReadLock();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000285B File Offset: 0x00000A5B
		public void EnterWriteLock()
		{
			this._thisLock.EnterWriteLock();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002868 File Offset: 0x00000A68
		public void ExitReadLock()
		{
			this._thisLock.ExitReadLock();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002875 File Offset: 0x00000A75
		public void ExitWriteLock()
		{
			this._thisLock.ExitWriteLock();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002882 File Offset: 0x00000A82
		public void Dispose()
		{
			if (Interlocked.CompareExchange(ref this._isDisposed, 1, 0) == 0)
			{
				this._thisLock.Dispose();
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000289E File Offset: 0x00000A9E
		public Lock()
		{
		}

		// Token: 0x04000042 RID: 66
		private ReaderWriterLockSlim _thisLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

		// Token: 0x04000043 RID: 67
		private int _isDisposed;
	}
}
