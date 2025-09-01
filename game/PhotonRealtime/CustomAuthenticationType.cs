using System;

namespace Photon.Realtime
{
	// Token: 0x02000033 RID: 51
	public enum CustomAuthenticationType : byte
	{
		// Token: 0x040001A6 RID: 422
		Custom,
		// Token: 0x040001A7 RID: 423
		Steam,
		// Token: 0x040001A8 RID: 424
		Facebook,
		// Token: 0x040001A9 RID: 425
		Oculus,
		// Token: 0x040001AA RID: 426
		PlayStation4,
		// Token: 0x040001AB RID: 427
		[Obsolete("Use PlayStation4 or PlayStation5 as needed")]
		PlayStation = 4,
		// Token: 0x040001AC RID: 428
		Xbox,
		// Token: 0x040001AD RID: 429
		Viveport = 10,
		// Token: 0x040001AE RID: 430
		NintendoSwitch,
		// Token: 0x040001AF RID: 431
		PlayStation5,
		// Token: 0x040001B0 RID: 432
		[Obsolete("Use PlayStation4 or PlayStation5 as needed")]
		Playstation5 = 12,
		// Token: 0x040001B1 RID: 433
		Epic,
		// Token: 0x040001B2 RID: 434
		FacebookGaming = 15,
		// Token: 0x040001B3 RID: 435
		None = 255
	}
}
