using System;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x0200003B RID: 59
	[Serializable]
	public struct TMP_GlyphAdjustmentRecord
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0001CF1A File Offset: 0x0001B11A
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0001CF22 File Offset: 0x0001B122
		public uint glyphIndex
		{
			get
			{
				return this.m_GlyphIndex;
			}
			set
			{
				this.m_GlyphIndex = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0001CF2B File Offset: 0x0001B12B
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0001CF33 File Offset: 0x0001B133
		public TMP_GlyphValueRecord glyphValueRecord
		{
			get
			{
				return this.m_GlyphValueRecord;
			}
			set
			{
				this.m_GlyphValueRecord = value;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0001CF3C File Offset: 0x0001B13C
		public TMP_GlyphAdjustmentRecord(uint glyphIndex, TMP_GlyphValueRecord glyphValueRecord)
		{
			this.m_GlyphIndex = glyphIndex;
			this.m_GlyphValueRecord = glyphValueRecord;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0001CF4C File Offset: 0x0001B14C
		internal TMP_GlyphAdjustmentRecord(GlyphAdjustmentRecord adjustmentRecord)
		{
			this.m_GlyphIndex = adjustmentRecord.glyphIndex;
			this.m_GlyphValueRecord = new TMP_GlyphValueRecord(adjustmentRecord.glyphValueRecord);
		}

		// Token: 0x040001F6 RID: 502
		[SerializeField]
		internal uint m_GlyphIndex;

		// Token: 0x040001F7 RID: 503
		[SerializeField]
		internal TMP_GlyphValueRecord m_GlyphValueRecord;
	}
}
