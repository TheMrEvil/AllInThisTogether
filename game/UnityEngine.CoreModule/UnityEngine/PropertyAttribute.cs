using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001D5 RID: 469
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public abstract class PropertyAttribute : Attribute
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x0002316D File Offset: 0x0002136D
		// (set) Token: 0x060015D9 RID: 5593 RVA: 0x00023175 File Offset: 0x00021375
		public int order
		{
			[CompilerGenerated]
			get
			{
				return this.<order>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<order>k__BackingField = value;
			}
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00002050 File Offset: 0x00000250
		protected PropertyAttribute()
		{
		}

		// Token: 0x040007AE RID: 1966
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <order>k__BackingField;
	}
}
