using System;

namespace System.Xml.Xsl
{
	// Token: 0x02000342 RID: 834
	[Flags]
	internal enum XslFlags
	{
		// Token: 0x04001C2A RID: 7210
		None = 0,
		// Token: 0x04001C2B RID: 7211
		String = 1,
		// Token: 0x04001C2C RID: 7212
		Number = 2,
		// Token: 0x04001C2D RID: 7213
		Boolean = 4,
		// Token: 0x04001C2E RID: 7214
		Node = 8,
		// Token: 0x04001C2F RID: 7215
		Nodeset = 16,
		// Token: 0x04001C30 RID: 7216
		Rtf = 32,
		// Token: 0x04001C31 RID: 7217
		TypeFilter = 63,
		// Token: 0x04001C32 RID: 7218
		AnyType = 63,
		// Token: 0x04001C33 RID: 7219
		Current = 256,
		// Token: 0x04001C34 RID: 7220
		Position = 512,
		// Token: 0x04001C35 RID: 7221
		Last = 1024,
		// Token: 0x04001C36 RID: 7222
		FocusFilter = 1792,
		// Token: 0x04001C37 RID: 7223
		FullFocus = 1792,
		// Token: 0x04001C38 RID: 7224
		HasCalls = 4096,
		// Token: 0x04001C39 RID: 7225
		MayBeDefault = 8192,
		// Token: 0x04001C3A RID: 7226
		SideEffects = 16384,
		// Token: 0x04001C3B RID: 7227
		Stop = 32768
	}
}
