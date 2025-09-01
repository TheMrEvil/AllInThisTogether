using System;

namespace MK.Toon
{
	// Token: 0x0200001F RID: 31
	public enum RenderQueue
	{
		// Token: 0x0400009B RID: 155
		Background = 1000,
		// Token: 0x0400009C RID: 156
		Geometry = 2000,
		// Token: 0x0400009D RID: 157
		AlphaTest = 2450,
		// Token: 0x0400009E RID: 158
		GeometryLast = 2500,
		// Token: 0x0400009F RID: 159
		Transparent = 3000,
		// Token: 0x040000A0 RID: 160
		Overlay = 4000
	}
}
