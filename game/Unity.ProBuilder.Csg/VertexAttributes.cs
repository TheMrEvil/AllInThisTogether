using System;

namespace UnityEngine.ProBuilder.Csg
{
	// Token: 0x02000007 RID: 7
	[Flags]
	internal enum VertexAttributes
	{
		// Token: 0x04000017 RID: 23
		Position = 1,
		// Token: 0x04000018 RID: 24
		Texture0 = 2,
		// Token: 0x04000019 RID: 25
		Texture1 = 4,
		// Token: 0x0400001A RID: 26
		Lightmap = 4,
		// Token: 0x0400001B RID: 27
		Texture2 = 8,
		// Token: 0x0400001C RID: 28
		Texture3 = 16,
		// Token: 0x0400001D RID: 29
		Color = 32,
		// Token: 0x0400001E RID: 30
		Normal = 64,
		// Token: 0x0400001F RID: 31
		Tangent = 128,
		// Token: 0x04000020 RID: 32
		All = 255
	}
}
