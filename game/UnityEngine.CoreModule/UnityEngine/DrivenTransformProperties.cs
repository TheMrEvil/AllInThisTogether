using System;

namespace UnityEngine
{
	// Token: 0x02000259 RID: 601
	[Flags]
	public enum DrivenTransformProperties
	{
		// Token: 0x04000897 RID: 2199
		None = 0,
		// Token: 0x04000898 RID: 2200
		All = -1,
		// Token: 0x04000899 RID: 2201
		AnchoredPositionX = 2,
		// Token: 0x0400089A RID: 2202
		AnchoredPositionY = 4,
		// Token: 0x0400089B RID: 2203
		AnchoredPositionZ = 8,
		// Token: 0x0400089C RID: 2204
		Rotation = 16,
		// Token: 0x0400089D RID: 2205
		ScaleX = 32,
		// Token: 0x0400089E RID: 2206
		ScaleY = 64,
		// Token: 0x0400089F RID: 2207
		ScaleZ = 128,
		// Token: 0x040008A0 RID: 2208
		AnchorMinX = 256,
		// Token: 0x040008A1 RID: 2209
		AnchorMinY = 512,
		// Token: 0x040008A2 RID: 2210
		AnchorMaxX = 1024,
		// Token: 0x040008A3 RID: 2211
		AnchorMaxY = 2048,
		// Token: 0x040008A4 RID: 2212
		SizeDeltaX = 4096,
		// Token: 0x040008A5 RID: 2213
		SizeDeltaY = 8192,
		// Token: 0x040008A6 RID: 2214
		PivotX = 16384,
		// Token: 0x040008A7 RID: 2215
		PivotY = 32768,
		// Token: 0x040008A8 RID: 2216
		AnchoredPosition = 6,
		// Token: 0x040008A9 RID: 2217
		AnchoredPosition3D = 14,
		// Token: 0x040008AA RID: 2218
		Scale = 224,
		// Token: 0x040008AB RID: 2219
		AnchorMin = 768,
		// Token: 0x040008AC RID: 2220
		AnchorMax = 3072,
		// Token: 0x040008AD RID: 2221
		Anchors = 3840,
		// Token: 0x040008AE RID: 2222
		SizeDelta = 12288,
		// Token: 0x040008AF RID: 2223
		Pivot = 49152
	}
}
