using System;

namespace System.Threading
{
	// Token: 0x020002D6 RID: 726
	[Serializable]
	internal enum StackCrawlMark
	{
		// Token: 0x04001B25 RID: 6949
		LookForMe,
		// Token: 0x04001B26 RID: 6950
		LookForMyCaller,
		// Token: 0x04001B27 RID: 6951
		LookForMyCallersCaller,
		// Token: 0x04001B28 RID: 6952
		LookForThread
	}
}
