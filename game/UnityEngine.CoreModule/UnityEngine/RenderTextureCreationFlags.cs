using System;

namespace UnityEngine
{
	// Token: 0x02000181 RID: 385
	[Flags]
	public enum RenderTextureCreationFlags
	{
		// Token: 0x04000558 RID: 1368
		MipMap = 1,
		// Token: 0x04000559 RID: 1369
		AutoGenerateMips = 2,
		// Token: 0x0400055A RID: 1370
		SRGB = 4,
		// Token: 0x0400055B RID: 1371
		EyeTexture = 8,
		// Token: 0x0400055C RID: 1372
		EnableRandomWrite = 16,
		// Token: 0x0400055D RID: 1373
		CreatedFromScript = 32,
		// Token: 0x0400055E RID: 1374
		AllowVerticalFlip = 128,
		// Token: 0x0400055F RID: 1375
		NoResolvedColorSurface = 256,
		// Token: 0x04000560 RID: 1376
		DynamicallyScalable = 1024,
		// Token: 0x04000561 RID: 1377
		BindMS = 2048
	}
}
