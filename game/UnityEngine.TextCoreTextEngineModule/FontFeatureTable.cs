using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class FontFeatureTable
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00005F38 File Offset: 0x00004138
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00005F50 File Offset: 0x00004150
		internal List<GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords
		{
			get
			{
				return this.m_GlyphPairAdjustmentRecords;
			}
			set
			{
				this.m_GlyphPairAdjustmentRecords = value;
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005F5A File Offset: 0x0000415A
		public FontFeatureTable()
		{
			this.m_GlyphPairAdjustmentRecords = new List<GlyphPairAdjustmentRecord>();
			this.m_GlyphPairAdjustmentRecordLookup = new Dictionary<uint, GlyphPairAdjustmentRecord>();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005F7C File Offset: 0x0000417C
		public void SortGlyphPairAdjustmentRecords()
		{
			bool flag = this.m_GlyphPairAdjustmentRecords.Count > 0;
			if (flag)
			{
				this.m_GlyphPairAdjustmentRecords = (from s in this.m_GlyphPairAdjustmentRecords
				orderby s.firstAdjustmentRecord.glyphIndex, s.secondAdjustmentRecord.glyphIndex
				select s).ToList<GlyphPairAdjustmentRecord>();
			}
		}

		// Token: 0x04000066 RID: 102
		[SerializeField]
		internal List<GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecords;

		// Token: 0x04000067 RID: 103
		internal Dictionary<uint, GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecordLookup;

		// Token: 0x02000010 RID: 16
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600009A RID: 154 RVA: 0x00005FF5 File Offset: 0x000041F5
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600009B RID: 155 RVA: 0x000059A4 File Offset: 0x00003BA4
			public <>c()
			{
			}

			// Token: 0x0600009C RID: 156 RVA: 0x00006004 File Offset: 0x00004204
			internal uint <SortGlyphPairAdjustmentRecords>b__6_0(GlyphPairAdjustmentRecord s)
			{
				return s.firstAdjustmentRecord.glyphIndex;
			}

			// Token: 0x0600009D RID: 157 RVA: 0x00006020 File Offset: 0x00004220
			internal uint <SortGlyphPairAdjustmentRecords>b__6_1(GlyphPairAdjustmentRecord s)
			{
				return s.secondAdjustmentRecord.glyphIndex;
			}

			// Token: 0x04000068 RID: 104
			public static readonly FontFeatureTable.<>c <>9 = new FontFeatureTable.<>c();

			// Token: 0x04000069 RID: 105
			public static Func<GlyphPairAdjustmentRecord, uint> <>9__6_0;

			// Token: 0x0400006A RID: 106
			public static Func<GlyphPairAdjustmentRecord, uint> <>9__6_1;
		}
	}
}
