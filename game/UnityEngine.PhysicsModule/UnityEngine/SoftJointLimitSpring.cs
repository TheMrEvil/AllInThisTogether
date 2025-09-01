using System;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	public struct SoftJointLimitSpring
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000021C8 File Offset: 0x000003C8
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000021E0 File Offset: 0x000003E0
		public float spring
		{
			get
			{
				return this.m_Spring;
			}
			set
			{
				this.m_Spring = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000021EC File Offset: 0x000003EC
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002204 File Offset: 0x00000404
		public float damper
		{
			get
			{
				return this.m_Damper;
			}
			set
			{
				this.m_Damper = value;
			}
		}

		// Token: 0x04000024 RID: 36
		private float m_Spring;

		// Token: 0x04000025 RID: 37
		private float m_Damper;
	}
}
