using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000C3 RID: 195
	[Flags]
	public enum ImplicitUseKindFlags
	{
		// Token: 0x0400024F RID: 591
		Default = 7,
		// Token: 0x04000250 RID: 592
		Access = 1,
		// Token: 0x04000251 RID: 593
		Assign = 2,
		// Token: 0x04000252 RID: 594
		InstantiatedWithFixedConstructorSignature = 4,
		// Token: 0x04000253 RID: 595
		InstantiatedNoFixedConstructorSignature = 8
	}
}
