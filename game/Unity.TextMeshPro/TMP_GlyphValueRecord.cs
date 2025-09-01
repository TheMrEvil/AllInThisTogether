using System;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x0200003A RID: 58
	[Serializable]
	public struct TMP_GlyphValueRecord
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0001CDE6 File Offset: 0x0001AFE6
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0001CDEE File Offset: 0x0001AFEE
		public float xPlacement
		{
			get
			{
				return this.m_XPlacement;
			}
			set
			{
				this.m_XPlacement = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0001CDF7 File Offset: 0x0001AFF7
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0001CDFF File Offset: 0x0001AFFF
		public float yPlacement
		{
			get
			{
				return this.m_YPlacement;
			}
			set
			{
				this.m_YPlacement = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0001CE08 File Offset: 0x0001B008
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0001CE10 File Offset: 0x0001B010
		public float xAdvance
		{
			get
			{
				return this.m_XAdvance;
			}
			set
			{
				this.m_XAdvance = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0001CE19 File Offset: 0x0001B019
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0001CE21 File Offset: 0x0001B021
		public float yAdvance
		{
			get
			{
				return this.m_YAdvance;
			}
			set
			{
				this.m_YAdvance = value;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0001CE2A File Offset: 0x0001B02A
		public TMP_GlyphValueRecord(float xPlacement, float yPlacement, float xAdvance, float yAdvance)
		{
			this.m_XPlacement = xPlacement;
			this.m_YPlacement = yPlacement;
			this.m_XAdvance = xAdvance;
			this.m_YAdvance = yAdvance;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0001CE49 File Offset: 0x0001B049
		internal TMP_GlyphValueRecord(GlyphValueRecord_Legacy valueRecord)
		{
			this.m_XPlacement = valueRecord.xPlacement;
			this.m_YPlacement = valueRecord.yPlacement;
			this.m_XAdvance = valueRecord.xAdvance;
			this.m_YAdvance = valueRecord.yAdvance;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0001CE7B File Offset: 0x0001B07B
		internal TMP_GlyphValueRecord(GlyphValueRecord valueRecord)
		{
			this.m_XPlacement = valueRecord.xPlacement;
			this.m_YPlacement = valueRecord.yPlacement;
			this.m_XAdvance = valueRecord.xAdvance;
			this.m_YAdvance = valueRecord.yAdvance;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0001CEB4 File Offset: 0x0001B0B4
		public static TMP_GlyphValueRecord operator +(TMP_GlyphValueRecord a, TMP_GlyphValueRecord b)
		{
			TMP_GlyphValueRecord result;
			result.m_XPlacement = a.xPlacement + b.xPlacement;
			result.m_YPlacement = a.yPlacement + b.yPlacement;
			result.m_XAdvance = a.xAdvance + b.xAdvance;
			result.m_YAdvance = a.yAdvance + b.yAdvance;
			return result;
		}

		// Token: 0x040001F2 RID: 498
		[SerializeField]
		internal float m_XPlacement;

		// Token: 0x040001F3 RID: 499
		[SerializeField]
		internal float m_YPlacement;

		// Token: 0x040001F4 RID: 500
		[SerializeField]
		internal float m_XAdvance;

		// Token: 0x040001F5 RID: 501
		[SerializeField]
		internal float m_YAdvance;
	}
}
