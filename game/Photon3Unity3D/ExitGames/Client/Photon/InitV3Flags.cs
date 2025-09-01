using System;

namespace ExitGames.Client.Photon
{
	// Token: 0x0200001B RID: 27
	[Flags]
	internal enum InitV3Flags : short
	{
		// Token: 0x040000DA RID: 218
		NoFlags = 0,
		// Token: 0x040000DB RID: 219
		EncryptionFlag = 1,
		// Token: 0x040000DC RID: 220
		IPv6Flag = 2,
		// Token: 0x040000DD RID: 221
		ReleaseSdkFlag = 4
	}
}
