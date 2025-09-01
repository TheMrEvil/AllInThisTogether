using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	// Token: 0x02000003 RID: 3
	[MovedFrom("UnityEngine.Experimental.U2D")]
	public struct SpriteShapeSegment
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		public int geomIndex
		{
			get
			{
				return this.m_GeomIndex;
			}
			set
			{
				this.m_GeomIndex = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002074 File Offset: 0x00000274
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000208C File Offset: 0x0000028C
		public int indexCount
		{
			get
			{
				return this.m_IndexCount;
			}
			set
			{
				this.m_IndexCount = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002098 File Offset: 0x00000298
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020B0 File Offset: 0x000002B0
		public int vertexCount
		{
			get
			{
				return this.m_VertexCount;
			}
			set
			{
				this.m_VertexCount = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020BC File Offset: 0x000002BC
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000020D4 File Offset: 0x000002D4
		public int spriteIndex
		{
			get
			{
				return this.m_SpriteIndex;
			}
			set
			{
				this.m_SpriteIndex = value;
			}
		}

		// Token: 0x0400000E RID: 14
		private int m_GeomIndex;

		// Token: 0x0400000F RID: 15
		private int m_IndexCount;

		// Token: 0x04000010 RID: 16
		private int m_VertexCount;

		// Token: 0x04000011 RID: 17
		private int m_SpriteIndex;
	}
}
