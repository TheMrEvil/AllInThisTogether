using System;
using System.Runtime.CompilerServices;

namespace Febucci.UI.Core
{
	// Token: 0x02000038 RID: 56
	public class TypingInfo
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000067D2 File Offset: 0x000049D2
		// (set) Token: 0x0600014B RID: 331 RVA: 0x000067DA File Offset: 0x000049DA
		public float timePassed
		{
			[CompilerGenerated]
			get
			{
				return this.<timePassed>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<timePassed>k__BackingField = value;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000067E3 File Offset: 0x000049E3
		public TypingInfo()
		{
			this.speed = 1f;
			this.timePassed = 0f;
		}

		// Token: 0x040000DB RID: 219
		public float speed = 1f;

		// Token: 0x040000DC RID: 220
		[CompilerGenerated]
		private float <timePassed>k__BackingField;
	}
}
