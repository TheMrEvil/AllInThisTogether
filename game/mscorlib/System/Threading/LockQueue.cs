using System;

namespace System.Threading
{
	// Token: 0x020002F2 RID: 754
	internal class LockQueue
	{
		// Token: 0x060020D5 RID: 8405 RVA: 0x00076B31 File Offset: 0x00074D31
		public LockQueue(ReaderWriterLock rwlock)
		{
			this.rwlock = rwlock;
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x00076B40 File Offset: 0x00074D40
		public bool Wait(int timeout)
		{
			bool flag = false;
			bool result;
			try
			{
				lock (this)
				{
					this.lockCount++;
					Monitor.Exit(this.rwlock);
					flag = true;
					result = Monitor.Wait(this, timeout);
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Enter(this.rwlock);
					this.lockCount--;
				}
			}
			return result;
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x00076BC4 File Offset: 0x00074DC4
		public bool IsEmpty
		{
			get
			{
				bool result;
				lock (this)
				{
					result = (this.lockCount == 0);
				}
				return result;
			}
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x00076C04 File Offset: 0x00074E04
		public void Pulse()
		{
			lock (this)
			{
				Monitor.Pulse(this);
			}
		}

		// Token: 0x04001B70 RID: 7024
		private ReaderWriterLock rwlock;

		// Token: 0x04001B71 RID: 7025
		private int lockCount;
	}
}
