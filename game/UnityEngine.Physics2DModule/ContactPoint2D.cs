using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000018 RID: 24
	[RequiredByNativeCode(Optional = false, GenerateProxy = true)]
	[NativeClass("ScriptingContactPoint2D", "struct ScriptingContactPoint2D;")]
	[NativeHeader("Modules/Physics2D/Public/PhysicsScripting2D.h")]
	public struct ContactPoint2D
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00006A78 File Offset: 0x00004C78
		public Vector2 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00006A90 File Offset: 0x00004C90
		public Vector2 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00006AA8 File Offset: 0x00004CA8
		public float separation
		{
			get
			{
				return this.m_Separation;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00006AC0 File Offset: 0x00004CC0
		public float normalImpulse
		{
			get
			{
				return this.m_NormalImpulse;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00006AD8 File Offset: 0x00004CD8
		public float tangentImpulse
		{
			get
			{
				return this.m_TangentImpulse;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00006AF0 File Offset: 0x00004CF0
		public Vector2 relativeVelocity
		{
			get
			{
				return this.m_RelativeVelocity;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00006B08 File Offset: 0x00004D08
		public Collider2D collider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Collider) as Collider2D;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00006B2C File Offset: 0x00004D2C
		public Collider2D otherCollider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_OtherCollider) as Collider2D;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00006B50 File Offset: 0x00004D50
		public Rigidbody2D rigidbody
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Rigidbody) as Rigidbody2D;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00006B74 File Offset: 0x00004D74
		public Rigidbody2D otherRigidbody
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_OtherRigidbody) as Rigidbody2D;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00006B98 File Offset: 0x00004D98
		public bool enabled
		{
			get
			{
				return this.m_Enabled == 1;
			}
		}

		// Token: 0x04000064 RID: 100
		[NativeName("point")]
		private Vector2 m_Point;

		// Token: 0x04000065 RID: 101
		[NativeName("normal")]
		private Vector2 m_Normal;

		// Token: 0x04000066 RID: 102
		[NativeName("relativeVelocity")]
		private Vector2 m_RelativeVelocity;

		// Token: 0x04000067 RID: 103
		[NativeName("separation")]
		private float m_Separation;

		// Token: 0x04000068 RID: 104
		[NativeName("normalImpulse")]
		private float m_NormalImpulse;

		// Token: 0x04000069 RID: 105
		[NativeName("tangentImpulse")]
		private float m_TangentImpulse;

		// Token: 0x0400006A RID: 106
		[NativeName("collider")]
		private int m_Collider;

		// Token: 0x0400006B RID: 107
		[NativeName("otherCollider")]
		private int m_OtherCollider;

		// Token: 0x0400006C RID: 108
		[NativeName("rigidbody")]
		private int m_Rigidbody;

		// Token: 0x0400006D RID: 109
		[NativeName("otherRigidbody")]
		private int m_OtherRigidbody;

		// Token: 0x0400006E RID: 110
		[NativeName("enabled")]
		private int m_Enabled;
	}
}
