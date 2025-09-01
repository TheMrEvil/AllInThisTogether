using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x0200000E RID: 14
	[MovedFrom("UnityEngine")]
	public struct NavMeshHit
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00002578 File Offset: 0x00000778
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00002590 File Offset: 0x00000790
		public Vector3 position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000259C File Offset: 0x0000079C
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x000025B4 File Offset: 0x000007B4
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
			set
			{
				this.m_Normal = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000025C0 File Offset: 0x000007C0
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000025D8 File Offset: 0x000007D8
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000025E4 File Offset: 0x000007E4
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000025FC File Offset: 0x000007FC
		public int mask
		{
			get
			{
				return this.m_Mask;
			}
			set
			{
				this.m_Mask = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00002608 File Offset: 0x00000808
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00002623 File Offset: 0x00000823
		public bool hit
		{
			get
			{
				return this.m_Hit != 0;
			}
			set
			{
				this.m_Hit = (value ? 1 : 0);
			}
		}

		// Token: 0x0400001A RID: 26
		private Vector3 m_Position;

		// Token: 0x0400001B RID: 27
		private Vector3 m_Normal;

		// Token: 0x0400001C RID: 28
		private float m_Distance;

		// Token: 0x0400001D RID: 29
		private int m_Mask;

		// Token: 0x0400001E RID: 30
		private int m_Hit;
	}
}
