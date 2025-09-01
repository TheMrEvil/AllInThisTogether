using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200011D RID: 285
	[AttributeUsage(AttributeTargets.Method)]
	public class BeforeRenderOrderAttribute : Attribute
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0000BB4F File Offset: 0x00009D4F
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x0000BB57 File Offset: 0x00009D57
		public int order
		{
			[CompilerGenerated]
			get
			{
				return this.<order>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<order>k__BackingField = value;
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0000BB60 File Offset: 0x00009D60
		public BeforeRenderOrderAttribute(int order)
		{
			this.order = order;
		}

		// Token: 0x040003A2 RID: 930
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <order>k__BackingField;
	}
}
