using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001AA RID: 426
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FriendSessionStateInfo_t
	{
		// Token: 0x04000B5A RID: 2906
		internal uint IOnlineSessionInstances;

		// Token: 0x04000B5B RID: 2907
		internal byte IPublishedToFriendsSessionInstance;
	}
}
