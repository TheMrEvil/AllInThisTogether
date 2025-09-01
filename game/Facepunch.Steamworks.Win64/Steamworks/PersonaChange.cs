using System;

namespace Steamworks
{
	// Token: 0x02000053 RID: 83
	internal enum PersonaChange
	{
		// Token: 0x040002B6 RID: 694
		Name = 1,
		// Token: 0x040002B7 RID: 695
		Status,
		// Token: 0x040002B8 RID: 696
		ComeOnline = 4,
		// Token: 0x040002B9 RID: 697
		GoneOffline = 8,
		// Token: 0x040002BA RID: 698
		GamePlayed = 16,
		// Token: 0x040002BB RID: 699
		GameServer = 32,
		// Token: 0x040002BC RID: 700
		Avatar = 64,
		// Token: 0x040002BD RID: 701
		JoinedSource = 128,
		// Token: 0x040002BE RID: 702
		LeftSource = 256,
		// Token: 0x040002BF RID: 703
		RelationshipChanged = 512,
		// Token: 0x040002C0 RID: 704
		NameFirstSet = 1024,
		// Token: 0x040002C1 RID: 705
		Broadcast = 2048,
		// Token: 0x040002C2 RID: 706
		Nickname = 4096,
		// Token: 0x040002C3 RID: 707
		SteamLevel = 8192,
		// Token: 0x040002C4 RID: 708
		RichPresence = 16384
	}
}
