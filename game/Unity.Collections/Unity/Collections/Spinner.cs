using System;
using System.Threading;

namespace Unity.Collections
{
	// Token: 0x020000E4 RID: 228
	internal struct Spinner
	{
		// Token: 0x060008BA RID: 2234 RVA: 0x00019E15 File Offset: 0x00018015
		public void Lock()
		{
			while (Interlocked.CompareExchange(ref this.m_value, 1, 0) != 0)
			{
			}
			Interlocked.MemoryBarrier();
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00019E2B File Offset: 0x0001802B
		public void Unlock()
		{
			Interlocked.MemoryBarrier();
			while (1 != Interlocked.CompareExchange(ref this.m_value, 0, 1))
			{
			}
		}

		// Token: 0x040002D8 RID: 728
		private int m_value;
	}
}
