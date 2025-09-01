using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace QFSW.QC.Actions
{
	// Token: 0x0200007B RID: 123
	public class WaitKey : WaitUntil
	{
		// Token: 0x06000284 RID: 644 RVA: 0x0000A99C File Offset: 0x00008B9C
		public WaitKey(KeyCode key) : base(() => InputHelper.GetKeyDown(key))
		{
		}

		// Token: 0x020000C2 RID: 194
		[CompilerGenerated]
		private sealed class <>c__DisplayClass0_0
		{
			// Token: 0x060003AA RID: 938 RVA: 0x0000D156 File Offset: 0x0000B356
			public <>c__DisplayClass0_0()
			{
			}

			// Token: 0x060003AB RID: 939 RVA: 0x0000D15E File Offset: 0x0000B35E
			internal bool <.ctor>b__0()
			{
				return InputHelper.GetKeyDown(this.key);
			}

			// Token: 0x0400026F RID: 623
			public KeyCode key;
		}
	}
}
