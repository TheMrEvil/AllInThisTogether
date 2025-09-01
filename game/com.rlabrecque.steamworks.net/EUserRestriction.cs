using System;

namespace Steamworks
{
	// Token: 0x02000105 RID: 261
	public enum EUserRestriction
	{
		// Token: 0x040003E0 RID: 992
		k_nUserRestrictionNone,
		// Token: 0x040003E1 RID: 993
		k_nUserRestrictionUnknown,
		// Token: 0x040003E2 RID: 994
		k_nUserRestrictionAnyChat,
		// Token: 0x040003E3 RID: 995
		k_nUserRestrictionVoiceChat = 4,
		// Token: 0x040003E4 RID: 996
		k_nUserRestrictionGroupChat = 8,
		// Token: 0x040003E5 RID: 997
		k_nUserRestrictionRating = 16,
		// Token: 0x040003E6 RID: 998
		k_nUserRestrictionGameInvites = 32,
		// Token: 0x040003E7 RID: 999
		k_nUserRestrictionTrading = 64
	}
}
