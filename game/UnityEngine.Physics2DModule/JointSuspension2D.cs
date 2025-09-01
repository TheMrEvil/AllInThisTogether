using System;

namespace UnityEngine
{
	// Token: 0x0200001C RID: 28
	public struct JointSuspension2D
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00006C8C File Offset: 0x00004E8C
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00006CA4 File Offset: 0x00004EA4
		public float dampingRatio
		{
			get
			{
				return this.m_DampingRatio;
			}
			set
			{
				this.m_DampingRatio = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00006CB0 File Offset: 0x00004EB0
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00006CC8 File Offset: 0x00004EC8
		public float frequency
		{
			get
			{
				return this.m_Frequency;
			}
			set
			{
				this.m_Frequency = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00006CD4 File Offset: 0x00004ED4
		// (set) Token: 0x0600024D RID: 589 RVA: 0x00006CEC File Offset: 0x00004EEC
		public float angle
		{
			get
			{
				return this.m_Angle;
			}
			set
			{
				this.m_Angle = value;
			}
		}

		// Token: 0x04000075 RID: 117
		private float m_DampingRatio;

		// Token: 0x04000076 RID: 118
		private float m_Frequency;

		// Token: 0x04000077 RID: 119
		private float m_Angle;
	}
}
