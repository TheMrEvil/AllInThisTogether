using System;

namespace Steamworks.Data
{
	// Token: 0x020000D4 RID: 212
	[Flags]
	public enum SendType
	{
		// Token: 0x040007DE RID: 2014
		Unreliable = 0,
		// Token: 0x040007DF RID: 2015
		NoNagle = 1,
		// Token: 0x040007E0 RID: 2016
		NoDelay = 4,
		// Token: 0x040007E1 RID: 2017
		Reliable = 8
	}
}
