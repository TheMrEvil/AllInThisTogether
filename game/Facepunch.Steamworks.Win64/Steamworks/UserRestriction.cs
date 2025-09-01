using System;

namespace Steamworks
{
	// Token: 0x02000050 RID: 80
	internal enum UserRestriction
	{
		// Token: 0x040002A6 RID: 678
		None,
		// Token: 0x040002A7 RID: 679
		Unknown,
		// Token: 0x040002A8 RID: 680
		AnyChat,
		// Token: 0x040002A9 RID: 681
		VoiceChat = 4,
		// Token: 0x040002AA RID: 682
		GroupChat = 8,
		// Token: 0x040002AB RID: 683
		Rating = 16,
		// Token: 0x040002AC RID: 684
		GameInvites = 32,
		// Token: 0x040002AD RID: 685
		Trading = 64
	}
}
