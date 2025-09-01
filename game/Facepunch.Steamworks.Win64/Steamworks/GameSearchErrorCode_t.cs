using System;

namespace Steamworks
{
	// Token: 0x02000049 RID: 73
	internal enum GameSearchErrorCode_t
	{
		// Token: 0x0400026D RID: 621
		OK = 1,
		// Token: 0x0400026E RID: 622
		Failed_Search_Already_In_Progress,
		// Token: 0x0400026F RID: 623
		Failed_No_Search_In_Progress,
		// Token: 0x04000270 RID: 624
		Failed_Not_Lobby_Leader,
		// Token: 0x04000271 RID: 625
		Failed_No_Host_Available,
		// Token: 0x04000272 RID: 626
		Failed_Search_Params_Invalid,
		// Token: 0x04000273 RID: 627
		Failed_Offline,
		// Token: 0x04000274 RID: 628
		Failed_NotAuthorized,
		// Token: 0x04000275 RID: 629
		Failed_Unknown_Error
	}
}
