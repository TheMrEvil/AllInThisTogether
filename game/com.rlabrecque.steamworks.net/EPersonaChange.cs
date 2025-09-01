using System;

namespace Steamworks
{
	// Token: 0x0200010A RID: 266
	[Flags]
	public enum EPersonaChange
	{
		// Token: 0x04000403 RID: 1027
		k_EPersonaChangeName = 1,
		// Token: 0x04000404 RID: 1028
		k_EPersonaChangeStatus = 2,
		// Token: 0x04000405 RID: 1029
		k_EPersonaChangeComeOnline = 4,
		// Token: 0x04000406 RID: 1030
		k_EPersonaChangeGoneOffline = 8,
		// Token: 0x04000407 RID: 1031
		k_EPersonaChangeGamePlayed = 16,
		// Token: 0x04000408 RID: 1032
		k_EPersonaChangeGameServer = 32,
		// Token: 0x04000409 RID: 1033
		k_EPersonaChangeAvatar = 64,
		// Token: 0x0400040A RID: 1034
		k_EPersonaChangeJoinedSource = 128,
		// Token: 0x0400040B RID: 1035
		k_EPersonaChangeLeftSource = 256,
		// Token: 0x0400040C RID: 1036
		k_EPersonaChangeRelationshipChanged = 512,
		// Token: 0x0400040D RID: 1037
		k_EPersonaChangeNameFirstSet = 1024,
		// Token: 0x0400040E RID: 1038
		k_EPersonaChangeBroadcast = 2048,
		// Token: 0x0400040F RID: 1039
		k_EPersonaChangeNickname = 4096,
		// Token: 0x04000410 RID: 1040
		k_EPersonaChangeSteamLevel = 8192,
		// Token: 0x04000411 RID: 1041
		k_EPersonaChangeRichPresence = 16384
	}
}
