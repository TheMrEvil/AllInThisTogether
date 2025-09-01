using System;

namespace Mono.Btls
{
	// Token: 0x020000EE RID: 238
	[Flags]
	internal enum MonoBtlsSslRenegotiateMode
	{
		// Token: 0x040003D1 RID: 977
		NEVER = 0,
		// Token: 0x040003D2 RID: 978
		ONCE = 1,
		// Token: 0x040003D3 RID: 979
		FREELY = 2,
		// Token: 0x040003D4 RID: 980
		IGNORE = 3
	}
}
