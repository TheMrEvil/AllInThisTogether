using System;

namespace Photon.Realtime
{
	// Token: 0x0200002C RID: 44
	[Flags]
	public enum PropertyTypeFlag : byte
	{
		// Token: 0x0400017F RID: 383
		None = 0,
		// Token: 0x04000180 RID: 384
		Game = 1,
		// Token: 0x04000181 RID: 385
		Actor = 2,
		// Token: 0x04000182 RID: 386
		GameAndActor = 3
	}
}
