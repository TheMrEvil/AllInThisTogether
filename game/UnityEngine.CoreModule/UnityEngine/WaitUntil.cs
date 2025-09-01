using System;

namespace UnityEngine
{
	// Token: 0x0200022F RID: 559
	public sealed class WaitUntil : CustomYieldInstruction
	{
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x00026FA8 File Offset: 0x000251A8
		public override bool keepWaiting
		{
			get
			{
				return !this.m_Predicate();
			}
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x00026FC8 File Offset: 0x000251C8
		public WaitUntil(Func<bool> predicate)
		{
			this.m_Predicate = predicate;
		}

		// Token: 0x04000838 RID: 2104
		private Func<bool> m_Predicate;
	}
}
