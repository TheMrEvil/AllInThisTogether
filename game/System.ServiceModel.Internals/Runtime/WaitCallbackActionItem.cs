using System;
using System.Runtime.CompilerServices;

namespace System.Runtime
{
	// Token: 0x02000039 RID: 57
	internal static class WaitCallbackActionItem
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00007BEB File Offset: 0x00005DEB
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00007BF2 File Offset: 0x00005DF2
		internal static bool ShouldUseActivity
		{
			[CompilerGenerated]
			get
			{
				return WaitCallbackActionItem.<ShouldUseActivity>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				WaitCallbackActionItem.<ShouldUseActivity>k__BackingField = value;
			}
		}

		// Token: 0x04000113 RID: 275
		[CompilerGenerated]
		private static bool <ShouldUseActivity>k__BackingField;
	}
}
