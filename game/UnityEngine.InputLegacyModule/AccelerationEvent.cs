using System;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	public struct AccelerationEvent
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002248 File Offset: 0x00000448
		public Vector3 acceleration
		{
			get
			{
				return new Vector3(this.x, this.y, this.z);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002274 File Offset: 0x00000474
		public float deltaTime
		{
			get
			{
				return this.m_TimeDelta;
			}
		}

		// Token: 0x04000025 RID: 37
		internal float x;

		// Token: 0x04000026 RID: 38
		internal float y;

		// Token: 0x04000027 RID: 39
		internal float z;

		// Token: 0x04000028 RID: 40
		internal float m_TimeDelta;
	}
}
