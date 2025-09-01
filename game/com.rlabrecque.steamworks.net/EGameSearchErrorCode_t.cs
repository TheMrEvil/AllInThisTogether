using System;

namespace Steamworks
{
	// Token: 0x0200015B RID: 347
	public enum EGameSearchErrorCode_t
	{
		// Token: 0x040008B3 RID: 2227
		k_EGameSearchErrorCode_OK = 1,
		// Token: 0x040008B4 RID: 2228
		k_EGameSearchErrorCode_Failed_Search_Already_In_Progress,
		// Token: 0x040008B5 RID: 2229
		k_EGameSearchErrorCode_Failed_No_Search_In_Progress,
		// Token: 0x040008B6 RID: 2230
		k_EGameSearchErrorCode_Failed_Not_Lobby_Leader,
		// Token: 0x040008B7 RID: 2231
		k_EGameSearchErrorCode_Failed_No_Host_Available,
		// Token: 0x040008B8 RID: 2232
		k_EGameSearchErrorCode_Failed_Search_Params_Invalid,
		// Token: 0x040008B9 RID: 2233
		k_EGameSearchErrorCode_Failed_Offline,
		// Token: 0x040008BA RID: 2234
		k_EGameSearchErrorCode_Failed_NotAuthorized,
		// Token: 0x040008BB RID: 2235
		k_EGameSearchErrorCode_Failed_Unknown_Error
	}
}
