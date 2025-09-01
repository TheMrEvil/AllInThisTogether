using System;

namespace Photon.Realtime
{
	// Token: 0x0200001E RID: 30
	internal enum RoomOptionBit
	{
		// Token: 0x040000B3 RID: 179
		CheckUserOnJoin = 1,
		// Token: 0x040000B4 RID: 180
		DeleteCacheOnLeave,
		// Token: 0x040000B5 RID: 181
		SuppressRoomEvents = 4,
		// Token: 0x040000B6 RID: 182
		PublishUserId = 8,
		// Token: 0x040000B7 RID: 183
		DeleteNullProps = 16,
		// Token: 0x040000B8 RID: 184
		BroadcastPropsChangeToAll = 32,
		// Token: 0x040000B9 RID: 185
		SuppressPlayerInfo = 64
	}
}
