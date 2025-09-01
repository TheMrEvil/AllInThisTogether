using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000025 RID: 37
	[AttributeUsage(AttributeTargets.Field)]
	[VisibleToOtherModules]
	internal class IgnoreAttribute : Attribute, IBindingsAttribute
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000025DA File Offset: 0x000007DA
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000025E2 File Offset: 0x000007E2
		public bool DoesNotContributeToSize
		{
			[CompilerGenerated]
			get
			{
				return this.<DoesNotContributeToSize>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DoesNotContributeToSize>k__BackingField = value;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002093 File Offset: 0x00000293
		public IgnoreAttribute()
		{
		}

		// Token: 0x04000026 RID: 38
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <DoesNotContributeToSize>k__BackingField;
	}
}
