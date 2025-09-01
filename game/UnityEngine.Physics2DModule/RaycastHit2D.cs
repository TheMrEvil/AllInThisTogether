using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001D RID: 29
	[NativeHeader("Runtime/Interfaces/IPhysics2D.h")]
	[NativeClass("RaycastHit2D", "struct RaycastHit2D;")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct RaycastHit2D
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00006CF8 File Offset: 0x00004EF8
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00006D10 File Offset: 0x00004F10
		public Vector2 centroid
		{
			get
			{
				return this.m_Centroid;
			}
			set
			{
				this.m_Centroid = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00006D1C File Offset: 0x00004F1C
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00006D34 File Offset: 0x00004F34
		public Vector2 point
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00006D40 File Offset: 0x00004F40
		// (set) Token: 0x06000253 RID: 595 RVA: 0x00006D58 File Offset: 0x00004F58
		public Vector2 normal
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00006D64 File Offset: 0x00004F64
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00006D7C File Offset: 0x00004F7C
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

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00006D88 File Offset: 0x00004F88
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00006DA0 File Offset: 0x00004FA0
		public float fraction
		{
			get
			{
				return this.m_Fraction;
			}
			set
			{
				this.m_Fraction = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00006DAC File Offset: 0x00004FAC
		public Collider2D collider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Collider) as Collider2D;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00006DD0 File Offset: 0x00004FD0
		public Rigidbody2D rigidbody
		{
			get
			{
				return (this.collider != null) ? this.collider.attachedRigidbody : null;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00006E00 File Offset: 0x00005000
		public Transform transform
		{
			get
			{
				Rigidbody2D rigidbody = this.rigidbody;
				bool flag = rigidbody != null;
				Transform result;
				if (flag)
				{
					result = rigidbody.transform;
				}
				else
				{
					bool flag2 = this.collider != null;
					if (flag2)
					{
						result = this.collider.transform;
					}
					else
					{
						result = null;
					}
				}
				return result;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00006E4C File Offset: 0x0000504C
		public static implicit operator bool(RaycastHit2D hit)
		{
			return hit.collider != null;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00006E6C File Offset: 0x0000506C
		public int CompareTo(RaycastHit2D other)
		{
			bool flag = this.collider == null;
			int result;
			if (flag)
			{
				result = 1;
			}
			else
			{
				bool flag2 = other.collider == null;
				if (flag2)
				{
					result = -1;
				}
				else
				{
					result = this.fraction.CompareTo(other.fraction);
				}
			}
			return result;
		}

		// Token: 0x04000078 RID: 120
		[NativeName("centroid")]
		private Vector2 m_Centroid;

		// Token: 0x04000079 RID: 121
		[NativeName("point")]
		private Vector2 m_Point;

		// Token: 0x0400007A RID: 122
		[NativeName("normal")]
		private Vector2 m_Normal;

		// Token: 0x0400007B RID: 123
		[NativeName("distance")]
		private float m_Distance;

		// Token: 0x0400007C RID: 124
		[NativeName("fraction")]
		private float m_Fraction;

		// Token: 0x0400007D RID: 125
		[NativeName("collider")]
		private int m_Collider;
	}
}
