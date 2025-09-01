using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020002E2 RID: 738
	internal sealed class ThreadPoolWorkQueueThreadLocals
	{
		// Token: 0x06002025 RID: 8229 RVA: 0x000754FB File Offset: 0x000736FB
		public ThreadPoolWorkQueueThreadLocals(ThreadPoolWorkQueue tpq)
		{
			this.workQueue = tpq;
			this.workStealingQueue = new ThreadPoolWorkQueue.WorkStealingQueue();
			ThreadPoolWorkQueue.allThreadQueues.Add(this.workStealingQueue);
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x0007553C File Offset: 0x0007373C
		[SecurityCritical]
		private void CleanUp()
		{
			if (this.workStealingQueue != null)
			{
				if (this.workQueue != null)
				{
					bool flag = false;
					while (!flag)
					{
						try
						{
						}
						finally
						{
							IThreadPoolWorkItem callback = null;
							if (this.workStealingQueue.LocalPop(out callback))
							{
								this.workQueue.Enqueue(callback, true);
							}
							else
							{
								flag = true;
							}
						}
					}
				}
				ThreadPoolWorkQueue.allThreadQueues.Remove(this.workStealingQueue);
			}
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x000755A8 File Offset: 0x000737A8
		[SecuritySafeCritical]
		~ThreadPoolWorkQueueThreadLocals()
		{
			if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
			{
				this.CleanUp();
			}
		}

		// Token: 0x04001B3F RID: 6975
		[SecurityCritical]
		[ThreadStatic]
		public static ThreadPoolWorkQueueThreadLocals threadLocals;

		// Token: 0x04001B40 RID: 6976
		public readonly ThreadPoolWorkQueue workQueue;

		// Token: 0x04001B41 RID: 6977
		public readonly ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue;

		// Token: 0x04001B42 RID: 6978
		public readonly Random random = new Random(Thread.CurrentThread.ManagedThreadId);
	}
}
