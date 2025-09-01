using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000CE RID: 206
	[Flags]
	public enum CollectionAccessType
	{
		// Token: 0x04000261 RID: 609
		None = 0,
		// Token: 0x04000262 RID: 610
		Read = 1,
		// Token: 0x04000263 RID: 611
		ModifyExistingContent = 2,
		// Token: 0x04000264 RID: 612
		UpdatedContent = 6
	}
}
