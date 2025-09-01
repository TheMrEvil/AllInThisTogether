using System;

namespace Steamworks
{
	// Token: 0x02000153 RID: 339
	public enum EChatRoomEnterResponse
	{
		// Token: 0x0400085B RID: 2139
		k_EChatRoomEnterResponseSuccess = 1,
		// Token: 0x0400085C RID: 2140
		k_EChatRoomEnterResponseDoesntExist,
		// Token: 0x0400085D RID: 2141
		k_EChatRoomEnterResponseNotAllowed,
		// Token: 0x0400085E RID: 2142
		k_EChatRoomEnterResponseFull,
		// Token: 0x0400085F RID: 2143
		k_EChatRoomEnterResponseError,
		// Token: 0x04000860 RID: 2144
		k_EChatRoomEnterResponseBanned,
		// Token: 0x04000861 RID: 2145
		k_EChatRoomEnterResponseLimited,
		// Token: 0x04000862 RID: 2146
		k_EChatRoomEnterResponseClanDisabled,
		// Token: 0x04000863 RID: 2147
		k_EChatRoomEnterResponseCommunityBan,
		// Token: 0x04000864 RID: 2148
		k_EChatRoomEnterResponseMemberBlockedYou,
		// Token: 0x04000865 RID: 2149
		k_EChatRoomEnterResponseYouBlockedMember,
		// Token: 0x04000866 RID: 2150
		k_EChatRoomEnterResponseRatelimitExceeded = 15
	}
}
