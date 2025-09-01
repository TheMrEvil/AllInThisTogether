using System;

namespace System.Reflection
{
	// Token: 0x020008E4 RID: 2276
	[Flags]
	[Serializable]
	internal enum MethodSemanticsAttributes
	{
		// Token: 0x04002FDF RID: 12255
		Setter = 1,
		// Token: 0x04002FE0 RID: 12256
		Getter = 2,
		// Token: 0x04002FE1 RID: 12257
		Other = 4,
		// Token: 0x04002FE2 RID: 12258
		AddOn = 8,
		// Token: 0x04002FE3 RID: 12259
		RemoveOn = 16,
		// Token: 0x04002FE4 RID: 12260
		Fire = 32
	}
}
