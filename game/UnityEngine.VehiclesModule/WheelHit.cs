using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/Vehicles/WheelCollider.h")]
	public struct WheelHit
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
			set
			{
				this.m_Collider = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002074 File Offset: 0x00000274
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000208C File Offset: 0x0000028C
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
			set
			{
				this.m_Point = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002098 File Offset: 0x00000298
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020B0 File Offset: 0x000002B0
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

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020BC File Offset: 0x000002BC
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000020D4 File Offset: 0x000002D4
		public Vector3 forwardDir
		{
			get
			{
				return this.m_ForwardDir;
			}
			set
			{
				this.m_ForwardDir = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020E0 File Offset: 0x000002E0
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020F8 File Offset: 0x000002F8
		public Vector3 sidewaysDir
		{
			get
			{
				return this.m_SidewaysDir;
			}
			set
			{
				this.m_SidewaysDir = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002104 File Offset: 0x00000304
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000211C File Offset: 0x0000031C
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

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002128 File Offset: 0x00000328
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002140 File Offset: 0x00000340
		public float forwardSlip
		{
			get
			{
				return this.m_ForwardSlip;
			}
			set
			{
				this.m_ForwardSlip = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000214C File Offset: 0x0000034C
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002164 File Offset: 0x00000364
		public float sidewaysSlip
		{
			get
			{
				return this.m_SidewaysSlip;
			}
			set
			{
				this.m_SidewaysSlip = value;
			}
		}

		// Token: 0x04000001 RID: 1
		[NativeName("point")]
		private Vector3 m_Point;

		// Token: 0x04000002 RID: 2
		[NativeName("normal")]
		private Vector3 m_Normal;

		// Token: 0x04000003 RID: 3
		[NativeName("forwardDir")]
		private Vector3 m_ForwardDir;

		// Token: 0x04000004 RID: 4
		[NativeName("sidewaysDir")]
		private Vector3 m_SidewaysDir;

		// Token: 0x04000005 RID: 5
		[NativeName("force")]
		private float m_Force;

		// Token: 0x04000006 RID: 6
		[NativeName("forwardSlip")]
		private float m_ForwardSlip;

		// Token: 0x04000007 RID: 7
		[NativeName("sidewaysSlip")]
		private float m_SidewaysSlip;

		// Token: 0x04000008 RID: 8
		[NativeName("collider")]
		private Collider m_Collider;
	}
}
