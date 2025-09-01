using System;

namespace UnityEngine
{
	// Token: 0x02000019 RID: 25
	public struct JointAngleLimits2D
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00006BB4 File Offset: 0x00004DB4
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00006BCC File Offset: 0x00004DCC
		public float min
		{
			get
			{
				return this.m_LowerAngle;
			}
			set
			{
				this.m_LowerAngle = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00006BD8 File Offset: 0x00004DD8
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00006BF0 File Offset: 0x00004DF0
		public float max
		{
			get
			{
				return this.m_UpperAngle;
			}
			set
			{
				this.m_UpperAngle = value;
			}
		}

		// Token: 0x0400006F RID: 111
		private float m_LowerAngle;

		// Token: 0x04000070 RID: 112
		private float m_UpperAngle;
	}
}
