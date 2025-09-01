using System;

namespace UnityEngine
{
	// Token: 0x02000178 RID: 376
	public enum ScreenOrientation
	{
		// Token: 0x040004CF RID: 1231
		[Obsolete("Enum member Unknown has been deprecated.", false)]
		Unknown,
		// Token: 0x040004D0 RID: 1232
		[Obsolete("Use LandscapeLeft instead (UnityUpgradable) -> LandscapeLeft", true)]
		Landscape = 3,
		// Token: 0x040004D1 RID: 1233
		Portrait = 1,
		// Token: 0x040004D2 RID: 1234
		PortraitUpsideDown,
		// Token: 0x040004D3 RID: 1235
		LandscapeLeft,
		// Token: 0x040004D4 RID: 1236
		LandscapeRight,
		// Token: 0x040004D5 RID: 1237
		AutoRotation
	}
}
