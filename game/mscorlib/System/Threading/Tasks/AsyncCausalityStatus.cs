using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000382 RID: 898
	[FriendAccessAllowed]
	internal enum AsyncCausalityStatus
	{
		// Token: 0x04001D66 RID: 7526
		Started,
		// Token: 0x04001D67 RID: 7527
		Completed,
		// Token: 0x04001D68 RID: 7528
		Canceled,
		// Token: 0x04001D69 RID: 7529
		Error
	}
}
