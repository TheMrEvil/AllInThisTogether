using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200063A RID: 1594
	internal sealed class Semaphore : WaitHandle
	{
		// Token: 0x06003241 RID: 12865 RVA: 0x000ADD4C File Offset: 0x000ABF4C
		internal Semaphore(int initialCount, int maxCount)
		{
			lock (this)
			{
				int num;
				this.Handle = Semaphore.CreateSemaphore_internal(initialCount, maxCount, null, out num);
			}
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x000ADD98 File Offset: 0x000ABF98
		internal bool ReleaseSemaphore()
		{
			int num;
			return Semaphore.ReleaseSemaphore_internal(this.Handle, 1, out num);
		}
	}
}
