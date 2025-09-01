using System;

namespace IKVM.Reflection
{
	// Token: 0x0200006F RID: 111
	[Flags]
	public enum UniverseOptions
	{
		// Token: 0x0400022C RID: 556
		None = 0,
		// Token: 0x0400022D RID: 557
		EnableFunctionPointers = 1,
		// Token: 0x0400022E RID: 558
		DisableFusion = 2,
		// Token: 0x0400022F RID: 559
		DisablePseudoCustomAttributeRetrieval = 4,
		// Token: 0x04000230 RID: 560
		DontProvideAutomaticDefaultConstructor = 8,
		// Token: 0x04000231 RID: 561
		MetadataOnly = 16,
		// Token: 0x04000232 RID: 562
		ResolveMissingMembers = 32,
		// Token: 0x04000233 RID: 563
		DisableWindowsRuntimeProjection = 64,
		// Token: 0x04000234 RID: 564
		DecodeVersionInfoAttributeBlobs = 128,
		// Token: 0x04000235 RID: 565
		DeterministicOutput = 256
	}
}
