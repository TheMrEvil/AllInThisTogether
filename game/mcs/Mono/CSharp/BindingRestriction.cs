using System;

namespace Mono.CSharp
{
	// Token: 0x02000245 RID: 581
	[Flags]
	public enum BindingRestriction
	{
		// Token: 0x04000AB8 RID: 2744
		None = 0,
		// Token: 0x04000AB9 RID: 2745
		DeclaredOnly = 2,
		// Token: 0x04000ABA RID: 2746
		InstanceOnly = 4,
		// Token: 0x04000ABB RID: 2747
		NoAccessors = 8,
		// Token: 0x04000ABC RID: 2748
		OverrideOnly = 16
	}
}
