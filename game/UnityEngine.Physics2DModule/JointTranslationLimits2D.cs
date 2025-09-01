using System;

namespace UnityEngine
{
	// Token: 0x0200001A RID: 26
	public struct JointTranslationLimits2D
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00006BFC File Offset: 0x00004DFC
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00006C14 File Offset: 0x00004E14
		public float min
		{
			get
			{
				return this.m_LowerTranslation;
			}
			set
			{
				this.m_LowerTranslation = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00006C20 File Offset: 0x00004E20
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00006C38 File Offset: 0x00004E38
		public float max
		{
			get
			{
				return this.m_UpperTranslation;
			}
			set
			{
				this.m_UpperTranslation = value;
			}
		}

		// Token: 0x04000071 RID: 113
		private float m_LowerTranslation;

		// Token: 0x04000072 RID: 114
		private float m_UpperTranslation;
	}
}
