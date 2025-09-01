using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000024 RID: 36
	[UsedByNativeCode]
	[Serializable]
	internal struct MarkToMarkAdjustmentRecord
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00004FF8 File Offset: 0x000031F8
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00005010 File Offset: 0x00003210
		public uint baseMarkGlyphID
		{
			get
			{
				return this.m_BaseMarkGlyphID;
			}
			set
			{
				this.m_BaseMarkGlyphID = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000501C File Offset: 0x0000321C
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00005034 File Offset: 0x00003234
		public GlyphAnchorPoint baseMarkGlyphAnchorPoint
		{
			get
			{
				return this.m_BaseMarkGlyphAnchorPoint;
			}
			set
			{
				this.m_BaseMarkGlyphAnchorPoint = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00005040 File Offset: 0x00003240
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00005058 File Offset: 0x00003258
		public uint combiningMarkGlyphID
		{
			get
			{
				return this.m_CombiningMarkGlyphID;
			}
			set
			{
				this.m_CombiningMarkGlyphID = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00005064 File Offset: 0x00003264
		// (set) Token: 0x0600015C RID: 348 RVA: 0x0000507C File Offset: 0x0000327C
		public MarkPositionAdjustment combiningMarkPositionAdjustment
		{
			get
			{
				return this.m_CombiningMarkPositionAdjustment;
			}
			set
			{
				this.m_CombiningMarkPositionAdjustment = value;
			}
		}

		// Token: 0x040000CC RID: 204
		[SerializeField]
		[NativeName("baseMarkGlyphID")]
		private uint m_BaseMarkGlyphID;

		// Token: 0x040000CD RID: 205
		[SerializeField]
		[NativeName("baseMarkAnchor")]
		private GlyphAnchorPoint m_BaseMarkGlyphAnchorPoint;

		// Token: 0x040000CE RID: 206
		[NativeName("combiningMarkGlyphID")]
		[SerializeField]
		private uint m_CombiningMarkGlyphID;

		// Token: 0x040000CF RID: 207
		[NativeName("combiningMarkPositionAdjustment")]
		[SerializeField]
		private MarkPositionAdjustment m_CombiningMarkPositionAdjustment;
	}
}
