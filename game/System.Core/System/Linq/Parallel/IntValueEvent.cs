using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001F1 RID: 497
	internal class IntValueEvent : ManualResetEventSlim
	{
		// Token: 0x06000C35 RID: 3125 RVA: 0x0002ADC3 File Offset: 0x00028FC3
		internal IntValueEvent() : base(false)
		{
			this.Value = 0;
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0002ADD3 File Offset: 0x00028FD3
		internal void Set(int index)
		{
			this.Value = index;
			base.Set();
		}

		// Token: 0x040008A2 RID: 2210
		internal int Value;
	}
}
