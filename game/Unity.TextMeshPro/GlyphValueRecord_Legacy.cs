using System;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	public struct GlyphValueRecord_Legacy
	{
		// Token: 0x06000204 RID: 516 RVA: 0x0001C544 File Offset: 0x0001A744
		internal GlyphValueRecord_Legacy(GlyphValueRecord valueRecord)
		{
			this.xPlacement = valueRecord.xPlacement;
			this.yPlacement = valueRecord.yPlacement;
			this.xAdvance = valueRecord.xAdvance;
			this.yAdvance = valueRecord.yAdvance;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0001C57C File Offset: 0x0001A77C
		public static GlyphValueRecord_Legacy operator +(GlyphValueRecord_Legacy a, GlyphValueRecord_Legacy b)
		{
			GlyphValueRecord_Legacy result;
			result.xPlacement = a.xPlacement + b.xPlacement;
			result.yPlacement = a.yPlacement + b.yPlacement;
			result.xAdvance = a.xAdvance + b.xAdvance;
			result.yAdvance = a.yAdvance + b.yAdvance;
			return result;
		}

		// Token: 0x040001DE RID: 478
		public float xPlacement;

		// Token: 0x040001DF RID: 479
		public float yPlacement;

		// Token: 0x040001E0 RID: 480
		public float xAdvance;

		// Token: 0x040001E1 RID: 481
		public float yAdvance;
	}
}
