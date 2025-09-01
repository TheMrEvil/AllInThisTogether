using System;

namespace UnityEngine
{
	// Token: 0x02000015 RID: 21
	public struct ColliderDistance2D
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00006468 File Offset: 0x00004668
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00006480 File Offset: 0x00004680
		public Vector2 pointA
		{
			get
			{
				return this.m_PointA;
			}
			set
			{
				this.m_PointA = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000648C File Offset: 0x0000468C
		// (set) Token: 0x06000208 RID: 520 RVA: 0x000064A4 File Offset: 0x000046A4
		public Vector2 pointB
		{
			get
			{
				return this.m_PointB;
			}
			set
			{
				this.m_PointB = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000064B0 File Offset: 0x000046B0
		public Vector2 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000064C8 File Offset: 0x000046C8
		// (set) Token: 0x0600020B RID: 523 RVA: 0x000064E0 File Offset: 0x000046E0
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

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000064EC File Offset: 0x000046EC
		public bool isOverlapped
		{
			get
			{
				return this.m_Distance < 0f;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000650C File Offset: 0x0000470C
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00006527 File Offset: 0x00004727
		public bool isValid
		{
			get
			{
				return this.m_IsValid != 0;
			}
			set
			{
				this.m_IsValid = (value ? 1 : 0);
			}
		}

		// Token: 0x0400004A RID: 74
		private Vector2 m_PointA;

		// Token: 0x0400004B RID: 75
		private Vector2 m_PointB;

		// Token: 0x0400004C RID: 76
		private Vector2 m_Normal;

		// Token: 0x0400004D RID: 77
		private float m_Distance;

		// Token: 0x0400004E RID: 78
		private int m_IsValid;
	}
}
