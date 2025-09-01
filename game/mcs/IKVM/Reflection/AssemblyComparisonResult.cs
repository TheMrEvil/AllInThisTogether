using System;

namespace IKVM.Reflection
{
	// Token: 0x0200006C RID: 108
	public enum AssemblyComparisonResult
	{
		// Token: 0x0400021F RID: 543
		Unknown,
		// Token: 0x04000220 RID: 544
		EquivalentFullMatch,
		// Token: 0x04000221 RID: 545
		EquivalentWeakNamed,
		// Token: 0x04000222 RID: 546
		EquivalentFXUnified,
		// Token: 0x04000223 RID: 547
		EquivalentUnified,
		// Token: 0x04000224 RID: 548
		NonEquivalentVersion,
		// Token: 0x04000225 RID: 549
		NonEquivalent,
		// Token: 0x04000226 RID: 550
		EquivalentPartialMatch,
		// Token: 0x04000227 RID: 551
		EquivalentPartialWeakNamed,
		// Token: 0x04000228 RID: 552
		EquivalentPartialUnified,
		// Token: 0x04000229 RID: 553
		EquivalentPartialFXUnified,
		// Token: 0x0400022A RID: 554
		NonEquivalentPartialVersion
	}
}
