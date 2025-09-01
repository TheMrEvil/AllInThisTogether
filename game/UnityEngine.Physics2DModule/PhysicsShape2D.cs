using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000012 RID: 18
	[UsedByNativeCode]
	[NativeHeader(Header = "Modules/Physics2D/Public/PhysicsScripting2D.h")]
	public struct PhysicsShape2D
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000055EC File Offset: 0x000037EC
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00005604 File Offset: 0x00003804
		public PhysicsShapeType2D shapeType
		{
			get
			{
				return this.m_ShapeType;
			}
			set
			{
				this.m_ShapeType = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00005610 File Offset: 0x00003810
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00005628 File Offset: 0x00003828
		public float radius
		{
			get
			{
				return this.m_Radius;
			}
			set
			{
				bool flag = value < 0f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("radius cannot be negative.");
				}
				bool flag2 = float.IsNaN(value) || float.IsInfinity(value);
				if (flag2)
				{
					throw new ArgumentException("radius contains an invalid value.");
				}
				this.m_Radius = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00005674 File Offset: 0x00003874
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000568C File Offset: 0x0000388C
		public int vertexStartIndex
		{
			get
			{
				return this.m_VertexStartIndex;
			}
			set
			{
				bool flag = value < 0;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("vertexStartIndex cannot be negative.");
				}
				this.m_VertexStartIndex = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000056B4 File Offset: 0x000038B4
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x000056CC File Offset: 0x000038CC
		public int vertexCount
		{
			get
			{
				return this.m_VertexCount;
			}
			set
			{
				bool flag = value < 1;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("vertexCount cannot be less than one.");
				}
				this.m_VertexCount = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000056F4 File Offset: 0x000038F4
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000570F File Offset: 0x0000390F
		public bool useAdjacentStart
		{
			get
			{
				return this.m_UseAdjacentStart != 0;
			}
			set
			{
				this.m_UseAdjacentStart = (value ? 1 : 0);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00005720 File Offset: 0x00003920
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000573B File Offset: 0x0000393B
		public bool useAdjacentEnd
		{
			get
			{
				return this.m_UseAdjacentEnd != 0;
			}
			set
			{
				this.m_UseAdjacentEnd = (value ? 1 : 0);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000574C File Offset: 0x0000394C
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00005764 File Offset: 0x00003964
		public Vector2 adjacentStart
		{
			get
			{
				return this.m_AdjacentStart;
			}
			set
			{
				bool flag = float.IsNaN(value.x) || float.IsNaN(value.y) || float.IsInfinity(value.x) || float.IsInfinity(value.y);
				if (flag)
				{
					throw new ArgumentException("adjacentStart contains an invalid value.");
				}
				this.m_AdjacentStart = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000057C0 File Offset: 0x000039C0
		// (set) Token: 0x060001EA RID: 490 RVA: 0x000057D8 File Offset: 0x000039D8
		public Vector2 adjacentEnd
		{
			get
			{
				return this.m_AdjacentEnd;
			}
			set
			{
				bool flag = float.IsNaN(value.x) || float.IsNaN(value.y) || float.IsInfinity(value.x) || float.IsInfinity(value.y);
				if (flag)
				{
					throw new ArgumentException("adjacentEnd contains an invalid value.");
				}
				this.m_AdjacentEnd = value;
			}
		}

		// Token: 0x0400003D RID: 61
		private PhysicsShapeType2D m_ShapeType;

		// Token: 0x0400003E RID: 62
		private float m_Radius;

		// Token: 0x0400003F RID: 63
		private int m_VertexStartIndex;

		// Token: 0x04000040 RID: 64
		private int m_VertexCount;

		// Token: 0x04000041 RID: 65
		private int m_UseAdjacentStart;

		// Token: 0x04000042 RID: 66
		private int m_UseAdjacentEnd;

		// Token: 0x04000043 RID: 67
		private Vector2 m_AdjacentStart;

		// Token: 0x04000044 RID: 68
		private Vector2 m_AdjacentEnd;
	}
}
