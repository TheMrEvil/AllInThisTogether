using System;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x02000183 RID: 387
	internal enum JsonNodeType
	{
		// Token: 0x040009F1 RID: 2545
		None,
		// Token: 0x040009F2 RID: 2546
		Object,
		// Token: 0x040009F3 RID: 2547
		Element,
		// Token: 0x040009F4 RID: 2548
		EndElement,
		// Token: 0x040009F5 RID: 2549
		QuotedText,
		// Token: 0x040009F6 RID: 2550
		StandaloneText,
		// Token: 0x040009F7 RID: 2551
		Collection
	}
}
