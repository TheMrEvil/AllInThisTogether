using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TMPro
{
	// Token: 0x02000035 RID: 53
	[Serializable]
	public class KerningPair
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0001C5DA File Offset: 0x0001A7DA
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0001C5E2 File Offset: 0x0001A7E2
		public uint firstGlyph
		{
			get
			{
				return this.m_FirstGlyph;
			}
			set
			{
				this.m_FirstGlyph = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0001C5EB File Offset: 0x0001A7EB
		public GlyphValueRecord_Legacy firstGlyphAdjustments
		{
			get
			{
				return this.m_FirstGlyphAdjustments;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0001C5F3 File Offset: 0x0001A7F3
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0001C5FB File Offset: 0x0001A7FB
		public uint secondGlyph
		{
			get
			{
				return this.m_SecondGlyph;
			}
			set
			{
				this.m_SecondGlyph = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0001C604 File Offset: 0x0001A804
		public GlyphValueRecord_Legacy secondGlyphAdjustments
		{
			get
			{
				return this.m_SecondGlyphAdjustments;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0001C60C File Offset: 0x0001A80C
		public bool ignoreSpacingAdjustments
		{
			get
			{
				return this.m_IgnoreSpacingAdjustments;
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0001C614 File Offset: 0x0001A814
		public KerningPair()
		{
			this.m_FirstGlyph = 0U;
			this.m_FirstGlyphAdjustments = default(GlyphValueRecord_Legacy);
			this.m_SecondGlyph = 0U;
			this.m_SecondGlyphAdjustments = default(GlyphValueRecord_Legacy);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0001C642 File Offset: 0x0001A842
		public KerningPair(uint left, uint right, float offset)
		{
			this.firstGlyph = left;
			this.m_SecondGlyph = right;
			this.xOffset = offset;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0001C65F File Offset: 0x0001A85F
		public KerningPair(uint firstGlyph, GlyphValueRecord_Legacy firstGlyphAdjustments, uint secondGlyph, GlyphValueRecord_Legacy secondGlyphAdjustments)
		{
			this.m_FirstGlyph = firstGlyph;
			this.m_FirstGlyphAdjustments = firstGlyphAdjustments;
			this.m_SecondGlyph = secondGlyph;
			this.m_SecondGlyphAdjustments = secondGlyphAdjustments;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0001C684 File Offset: 0x0001A884
		internal void ConvertLegacyKerningData()
		{
			this.m_FirstGlyphAdjustments.xAdvance = this.xOffset;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0001C698 File Offset: 0x0001A898
		// Note: this type is marked as 'beforefieldinit'.
		static KerningPair()
		{
		}

		// Token: 0x040001E2 RID: 482
		[FormerlySerializedAs("AscII_Left")]
		[SerializeField]
		private uint m_FirstGlyph;

		// Token: 0x040001E3 RID: 483
		[SerializeField]
		private GlyphValueRecord_Legacy m_FirstGlyphAdjustments;

		// Token: 0x040001E4 RID: 484
		[FormerlySerializedAs("AscII_Right")]
		[SerializeField]
		private uint m_SecondGlyph;

		// Token: 0x040001E5 RID: 485
		[SerializeField]
		private GlyphValueRecord_Legacy m_SecondGlyphAdjustments;

		// Token: 0x040001E6 RID: 486
		[FormerlySerializedAs("XadvanceOffset")]
		public float xOffset;

		// Token: 0x040001E7 RID: 487
		internal static KerningPair empty = new KerningPair(0U, default(GlyphValueRecord_Legacy), 0U, default(GlyphValueRecord_Legacy));

		// Token: 0x040001E8 RID: 488
		[SerializeField]
		private bool m_IgnoreSpacingAdjustments;
	}
}
