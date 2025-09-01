using System;

namespace Mono.CSharp
{
	// Token: 0x0200021C RID: 540
	[Flags]
	public enum SpecialConstraint
	{
		// Token: 0x04000A39 RID: 2617
		None = 0,
		// Token: 0x04000A3A RID: 2618
		Constructor = 4,
		// Token: 0x04000A3B RID: 2619
		Class = 8,
		// Token: 0x04000A3C RID: 2620
		Struct = 16
	}
}
