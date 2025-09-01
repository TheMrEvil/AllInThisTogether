using System;

namespace Steamworks
{
	// Token: 0x0200003C RID: 60
	internal enum AppType
	{
		// Token: 0x040001B6 RID: 438
		Invalid,
		// Token: 0x040001B7 RID: 439
		Game,
		// Token: 0x040001B8 RID: 440
		Application,
		// Token: 0x040001B9 RID: 441
		Tool = 4,
		// Token: 0x040001BA RID: 442
		Demo = 8,
		// Token: 0x040001BB RID: 443
		Media_DEPRECATED = 16,
		// Token: 0x040001BC RID: 444
		DLC = 32,
		// Token: 0x040001BD RID: 445
		Guide = 64,
		// Token: 0x040001BE RID: 446
		Driver = 128,
		// Token: 0x040001BF RID: 447
		Config = 256,
		// Token: 0x040001C0 RID: 448
		Hardware = 512,
		// Token: 0x040001C1 RID: 449
		Franchise = 1024,
		// Token: 0x040001C2 RID: 450
		Video = 2048,
		// Token: 0x040001C3 RID: 451
		Plugin = 4096,
		// Token: 0x040001C4 RID: 452
		MusicAlbum = 8192,
		// Token: 0x040001C5 RID: 453
		Series = 16384,
		// Token: 0x040001C6 RID: 454
		Comic_UNUSED = 32768,
		// Token: 0x040001C7 RID: 455
		Beta = 65536,
		// Token: 0x040001C8 RID: 456
		Shortcut = 1073741824,
		// Token: 0x040001C9 RID: 457
		DepotOnly = -2147483648
	}
}
