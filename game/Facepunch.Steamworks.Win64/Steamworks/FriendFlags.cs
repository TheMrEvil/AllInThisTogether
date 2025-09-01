using System;

namespace Steamworks
{
	// Token: 0x0200004F RID: 79
	internal enum FriendFlags
	{
		// Token: 0x04000299 RID: 665
		None,
		// Token: 0x0400029A RID: 666
		Blocked,
		// Token: 0x0400029B RID: 667
		FriendshipRequested,
		// Token: 0x0400029C RID: 668
		Immediate = 4,
		// Token: 0x0400029D RID: 669
		ClanMember = 8,
		// Token: 0x0400029E RID: 670
		OnGameServer = 16,
		// Token: 0x0400029F RID: 671
		RequestingFriendship = 128,
		// Token: 0x040002A0 RID: 672
		RequestingInfo = 256,
		// Token: 0x040002A1 RID: 673
		Ignored = 512,
		// Token: 0x040002A2 RID: 674
		IgnoredFriend = 1024,
		// Token: 0x040002A3 RID: 675
		ChatMember = 4096,
		// Token: 0x040002A4 RID: 676
		All = 65535
	}
}
