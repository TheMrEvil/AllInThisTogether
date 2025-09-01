using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000035 RID: 53
	[UsedByNativeCode]
	[NativeHeader("Modules/Physics/MessageParameters.h")]
	public struct ContactPoint
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00005914 File Offset: 0x00003B14
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000592C File Offset: 0x00003B2C
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00005944 File Offset: 0x00003B44
		public Collider thisCollider
		{
			get
			{
				return ContactPoint.GetColliderByInstanceID(this.m_ThisColliderInstanceID);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00005964 File Offset: 0x00003B64
		public Collider otherCollider
		{
			get
			{
				return ContactPoint.GetColliderByInstanceID(this.m_OtherColliderInstanceID);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00005984 File Offset: 0x00003B84
		public float separation
		{
			get
			{
				return this.m_Separation;
			}
		}

		// Token: 0x060003F9 RID: 1017
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider GetColliderByInstanceID(int instanceID);

		// Token: 0x040000B7 RID: 183
		internal Vector3 m_Point;

		// Token: 0x040000B8 RID: 184
		internal Vector3 m_Normal;

		// Token: 0x040000B9 RID: 185
		internal int m_ThisColliderInstanceID;

		// Token: 0x040000BA RID: 186
		internal int m_OtherColliderInstanceID;

		// Token: 0x040000BB RID: 187
		internal float m_Separation;
	}
}
