using System;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	public struct JointDrive
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002210 File Offset: 0x00000410
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002228 File Offset: 0x00000428
		public float positionSpring
		{
			get
			{
				return this.m_PositionSpring;
			}
			set
			{
				this.m_PositionSpring = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002234 File Offset: 0x00000434
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000224C File Offset: 0x0000044C
		public float positionDamper
		{
			get
			{
				return this.m_PositionDamper;
			}
			set
			{
				this.m_PositionDamper = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002258 File Offset: 0x00000458
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002270 File Offset: 0x00000470
		public float maximumForce
		{
			get
			{
				return this.m_MaximumForce;
			}
			set
			{
				this.m_MaximumForce = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000227C File Offset: 0x0000047C
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002193 File Offset: 0x00000393
		[Obsolete("JointDriveMode is obsolete")]
		public JointDriveMode mode
		{
			get
			{
				return JointDriveMode.None;
			}
			set
			{
			}
		}

		// Token: 0x04000026 RID: 38
		private float m_PositionSpring;

		// Token: 0x04000027 RID: 39
		private float m_PositionDamper;

		// Token: 0x04000028 RID: 40
		private float m_MaximumForce;
	}
}
