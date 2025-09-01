using System;
using System.Runtime.CompilerServices;

namespace QFSW.QC.Actions
{
	// Token: 0x0200007D RID: 125
	public class WaitUntil : WaitWhile
	{
		// Token: 0x0600028A RID: 650 RVA: 0x0000AA04 File Offset: 0x00008C04
		public WaitUntil(Func<bool> condition) : base(() => !condition())
		{
		}

		// Token: 0x020000C3 RID: 195
		[CompilerGenerated]
		private sealed class <>c__DisplayClass0_0
		{
			// Token: 0x060003AC RID: 940 RVA: 0x0000D16B File Offset: 0x0000B36B
			public <>c__DisplayClass0_0()
			{
			}

			// Token: 0x060003AD RID: 941 RVA: 0x0000D173 File Offset: 0x0000B373
			internal bool <.ctor>b__0()
			{
				return !this.condition();
			}

			// Token: 0x04000270 RID: 624
			public Func<bool> condition;
		}
	}
}
