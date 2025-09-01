using System;

namespace Steamworks
{
	// Token: 0x02000104 RID: 260
	[Flags]
	public enum EFriendFlags
	{
		// Token: 0x040003D3 RID: 979
		k_EFriendFlagNone = 0,
		// Token: 0x040003D4 RID: 980
		k_EFriendFlagBlocked = 1,
		// Token: 0x040003D5 RID: 981
		k_EFriendFlagFriendshipRequested = 2,
		// Token: 0x040003D6 RID: 982
		k_EFriendFlagImmediate = 4,
		// Token: 0x040003D7 RID: 983
		k_EFriendFlagClanMember = 8,
		// Token: 0x040003D8 RID: 984
		k_EFriendFlagOnGameServer = 16,
		// Token: 0x040003D9 RID: 985
		k_EFriendFlagRequestingFriendship = 128,
		// Token: 0x040003DA RID: 986
		k_EFriendFlagRequestingInfo = 256,
		// Token: 0x040003DB RID: 987
		k_EFriendFlagIgnored = 512,
		// Token: 0x040003DC RID: 988
		k_EFriendFlagIgnoredFriend = 1024,
		// Token: 0x040003DD RID: 989
		k_EFriendFlagChatMember = 4096,
		// Token: 0x040003DE RID: 990
		k_EFriendFlagAll = 65535
	}
}
