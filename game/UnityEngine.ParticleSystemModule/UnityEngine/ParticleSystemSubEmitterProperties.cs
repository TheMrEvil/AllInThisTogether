using System;

namespace UnityEngine
{
	// Token: 0x02000050 RID: 80
	[Flags]
	public enum ParticleSystemSubEmitterProperties
	{
		// Token: 0x0400013D RID: 317
		InheritNothing = 0,
		// Token: 0x0400013E RID: 318
		InheritEverything = 31,
		// Token: 0x0400013F RID: 319
		InheritColor = 1,
		// Token: 0x04000140 RID: 320
		InheritSize = 2,
		// Token: 0x04000141 RID: 321
		InheritRotation = 4,
		// Token: 0x04000142 RID: 322
		InheritLifetime = 8,
		// Token: 0x04000143 RID: 323
		InheritDuration = 16
	}
}
