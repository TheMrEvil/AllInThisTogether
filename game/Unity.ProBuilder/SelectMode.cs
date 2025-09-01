using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000032 RID: 50
	[Flags]
	public enum SelectMode
	{
		// Token: 0x0400011F RID: 287
		None = 0,
		// Token: 0x04000120 RID: 288
		Object = 1,
		// Token: 0x04000121 RID: 289
		Vertex = 2,
		// Token: 0x04000122 RID: 290
		Edge = 4,
		// Token: 0x04000123 RID: 291
		Face = 8,
		// Token: 0x04000124 RID: 292
		TextureFace = 16,
		// Token: 0x04000125 RID: 293
		TextureEdge = 32,
		// Token: 0x04000126 RID: 294
		TextureVertex = 64,
		// Token: 0x04000127 RID: 295
		InputTool = 128,
		// Token: 0x04000128 RID: 296
		Any = 65535
	}
}
