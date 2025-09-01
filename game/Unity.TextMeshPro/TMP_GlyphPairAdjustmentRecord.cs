using System;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	public class TMP_GlyphPairAdjustmentRecord
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0001CF6D File Offset: 0x0001B16D
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0001CF75 File Offset: 0x0001B175
		public TMP_GlyphAdjustmentRecord firstAdjustmentRecord
		{
			get
			{
				return this.m_FirstAdjustmentRecord;
			}
			set
			{
				this.m_FirstAdjustmentRecord = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0001CF7E File Offset: 0x0001B17E
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0001CF86 File Offset: 0x0001B186
		public TMP_GlyphAdjustmentRecord secondAdjustmentRecord
		{
			get
			{
				return this.m_SecondAdjustmentRecord;
			}
			set
			{
				this.m_SecondAdjustmentRecord = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0001CF8F File Offset: 0x0001B18F
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0001CF97 File Offset: 0x0001B197
		public FontFeatureLookupFlags featureLookupFlags
		{
			get
			{
				return this.m_FeatureLookupFlags;
			}
			set
			{
				this.m_FeatureLookupFlags = value;
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0001CFA0 File Offset: 0x0001B1A0
		public TMP_GlyphPairAdjustmentRecord(TMP_GlyphAdjustmentRecord firstAdjustmentRecord, TMP_GlyphAdjustmentRecord secondAdjustmentRecord)
		{
			this.m_FirstAdjustmentRecord = firstAdjustmentRecord;
			this.m_SecondAdjustmentRecord = secondAdjustmentRecord;
			this.m_FeatureLookupFlags = FontFeatureLookupFlags.None;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0001CFBD File Offset: 0x0001B1BD
		internal TMP_GlyphPairAdjustmentRecord(GlyphPairAdjustmentRecord glyphPairAdjustmentRecord)
		{
			this.m_FirstAdjustmentRecord = new TMP_GlyphAdjustmentRecord(glyphPairAdjustmentRecord.firstAdjustmentRecord);
			this.m_SecondAdjustmentRecord = new TMP_GlyphAdjustmentRecord(glyphPairAdjustmentRecord.secondAdjustmentRecord);
			this.m_FeatureLookupFlags = FontFeatureLookupFlags.None;
		}

		// Token: 0x040001F8 RID: 504
		[SerializeField]
		internal TMP_GlyphAdjustmentRecord m_FirstAdjustmentRecord;

		// Token: 0x040001F9 RID: 505
		[SerializeField]
		internal TMP_GlyphAdjustmentRecord m_SecondAdjustmentRecord;

		// Token: 0x040001FA RID: 506
		[SerializeField]
		internal FontFeatureLookupFlags m_FeatureLookupFlags;
	}
}
