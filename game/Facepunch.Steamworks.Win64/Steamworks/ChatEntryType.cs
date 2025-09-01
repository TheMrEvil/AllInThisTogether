using System;

namespace Steamworks
{
	// Token: 0x0200003E RID: 62
	internal enum ChatEntryType
	{
		// Token: 0x040001D3 RID: 467
		Invalid,
		// Token: 0x040001D4 RID: 468
		ChatMsg,
		// Token: 0x040001D5 RID: 469
		Typing,
		// Token: 0x040001D6 RID: 470
		InviteGame,
		// Token: 0x040001D7 RID: 471
		Emote,
		// Token: 0x040001D8 RID: 472
		LeftConversation = 6,
		// Token: 0x040001D9 RID: 473
		Entered,
		// Token: 0x040001DA RID: 474
		WasKicked,
		// Token: 0x040001DB RID: 475
		WasBanned,
		// Token: 0x040001DC RID: 476
		Disconnected,
		// Token: 0x040001DD RID: 477
		HistoricalChat,
		// Token: 0x040001DE RID: 478
		LinkBlocked = 14
	}
}
