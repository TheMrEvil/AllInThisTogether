using System;

namespace UnityEngine
{
	// Token: 0x0200000D RID: 13
	public struct JointMotor
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002290 File Offset: 0x00000490
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000022A8 File Offset: 0x000004A8
		public float targetVelocity
		{
			get
			{
				return this.m_TargetVelocity;
			}
			set
			{
				this.m_TargetVelocity = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000022B4 File Offset: 0x000004B4
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000022CC File Offset: 0x000004CC
		public float force
		{
			get
			{
				return this.m_Force;
			}
			set
			{
				this.m_Force = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000022D8 File Offset: 0x000004D8
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000022F3 File Offset: 0x000004F3
		public bool freeSpin
		{
			get
			{
				return this.m_FreeSpin == 1;
			}
			set
			{
				this.m_FreeSpin = (value ? 1 : 0);
			}
		}

		// Token: 0x0400002D RID: 45
		private float m_TargetVelocity;

		// Token: 0x0400002E RID: 46
		private float m_Force;

		// Token: 0x0400002F RID: 47
		private int m_FreeSpin;
	}
}
