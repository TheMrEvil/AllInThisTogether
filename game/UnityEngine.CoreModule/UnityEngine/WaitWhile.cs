using System;

namespace UnityEngine
{
	// Token: 0x02000230 RID: 560
	public sealed class WaitWhile : CustomYieldInstruction
	{
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x00026FDC File Offset: 0x000251DC
		public override bool keepWaiting
		{
			get
			{
				return this.m_Predicate();
			}
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x00026FF9 File Offset: 0x000251F9
		public WaitWhile(Func<bool> predicate)
		{
			this.m_Predicate = predicate;
		}

		// Token: 0x04000839 RID: 2105
		private Func<bool> m_Predicate;
	}
}
