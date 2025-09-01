using System;

namespace Steamworks
{
	// Token: 0x0200003F RID: 63
	public enum RoomEnter
	{
		// Token: 0x040001E0 RID: 480
		Success = 1,
		// Token: 0x040001E1 RID: 481
		DoesntExist,
		// Token: 0x040001E2 RID: 482
		NotAllowed,
		// Token: 0x040001E3 RID: 483
		Full,
		// Token: 0x040001E4 RID: 484
		Error,
		// Token: 0x040001E5 RID: 485
		Banned,
		// Token: 0x040001E6 RID: 486
		Limited,
		// Token: 0x040001E7 RID: 487
		ClanDisabled,
		// Token: 0x040001E8 RID: 488
		CommunityBan,
		// Token: 0x040001E9 RID: 489
		MemberBlockedYou,
		// Token: 0x040001EA RID: 490
		YouBlockedMember,
		// Token: 0x040001EB RID: 491
		RatelimitExceeded = 15
	}
}
