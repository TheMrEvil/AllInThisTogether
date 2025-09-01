using System;

namespace UnityEngine.ProBuilder.Csg
{
	// Token: 0x02000006 RID: 6
	internal struct Vertex
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002BF5 File Offset: 0x00000DF5
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002BFD File Offset: 0x00000DFD
		public Vector3 position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.hasPosition = true;
				this.m_Position = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002C0D File Offset: 0x00000E0D
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002C15 File Offset: 0x00000E15
		public Color color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				this.hasColor = true;
				this.m_Color = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002C25 File Offset: 0x00000E25
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002C2D File Offset: 0x00000E2D
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
			set
			{
				this.hasNormal = true;
				this.m_Normal = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002C3D File Offset: 0x00000E3D
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002C45 File Offset: 0x00000E45
		public Vector4 tangent
		{
			get
			{
				return this.m_Tangent;
			}
			set
			{
				this.hasTangent = true;
				this.m_Tangent = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002C55 File Offset: 0x00000E55
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002C5D File Offset: 0x00000E5D
		public Vector2 uv0
		{
			get
			{
				return this.m_UV0;
			}
			set
			{
				this.hasUV0 = true;
				this.m_UV0 = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002C6D File Offset: 0x00000E6D
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002C75 File Offset: 0x00000E75
		public Vector2 uv2
		{
			get
			{
				return this.m_UV2;
			}
			set
			{
				this.hasUV2 = true;
				this.m_UV2 = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002C85 File Offset: 0x00000E85
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002C8D File Offset: 0x00000E8D
		public Vector4 uv3
		{
			get
			{
				return this.m_UV3;
			}
			set
			{
				this.hasUV3 = true;
				this.m_UV3 = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002C9D File Offset: 0x00000E9D
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002CA5 File Offset: 0x00000EA5
		public Vector4 uv4
		{
			get
			{
				return this.m_UV4;
			}
			set
			{
				this.hasUV4 = true;
				this.m_UV4 = value;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002CB5 File Offset: 0x00000EB5
		public bool HasArrays(VertexAttributes attribute)
		{
			return (this.m_Attributes & attribute) == attribute;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002CC2 File Offset: 0x00000EC2
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002CCF File Offset: 0x00000ECF
		public bool hasPosition
		{
			get
			{
				return (this.m_Attributes & VertexAttributes.Position) == VertexAttributes.Position;
			}
			private set
			{
				this.m_Attributes = (value ? (this.m_Attributes | VertexAttributes.Position) : (this.m_Attributes & ~VertexAttributes.Position));
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002CED File Offset: 0x00000EED
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002CFC File Offset: 0x00000EFC
		public bool hasColor
		{
			get
			{
				return (this.m_Attributes & VertexAttributes.Color) == VertexAttributes.Color;
			}
			private set
			{
				this.m_Attributes = (value ? (this.m_Attributes | VertexAttributes.Color) : (this.m_Attributes & ~VertexAttributes.Color));
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002D1B File Offset: 0x00000F1B
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002D2A File Offset: 0x00000F2A
		public bool hasNormal
		{
			get
			{
				return (this.m_Attributes & VertexAttributes.Normal) == VertexAttributes.Normal;
			}
			private set
			{
				this.m_Attributes = (value ? (this.m_Attributes | VertexAttributes.Normal) : (this.m_Attributes & ~VertexAttributes.Normal));
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002D49 File Offset: 0x00000F49
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002D5E File Offset: 0x00000F5E
		public bool hasTangent
		{
			get
			{
				return (this.m_Attributes & VertexAttributes.Tangent) == VertexAttributes.Tangent;
			}
			private set
			{
				this.m_Attributes = (value ? (this.m_Attributes | VertexAttributes.Tangent) : (this.m_Attributes & ~VertexAttributes.Tangent));
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002D83 File Offset: 0x00000F83
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002D90 File Offset: 0x00000F90
		public bool hasUV0
		{
			get
			{
				return (this.m_Attributes & VertexAttributes.Texture0) == VertexAttributes.Texture0;
			}
			private set
			{
				this.m_Attributes = (value ? (this.m_Attributes | VertexAttributes.Texture0) : (this.m_Attributes & ~VertexAttributes.Texture0));
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002DAE File Offset: 0x00000FAE
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002DBB File Offset: 0x00000FBB
		public bool hasUV2
		{
			get
			{
				return (this.m_Attributes & VertexAttributes.Texture1) == VertexAttributes.Texture1;
			}
			private set
			{
				this.m_Attributes = (value ? (this.m_Attributes | VertexAttributes.Texture1) : (this.m_Attributes & ~VertexAttributes.Texture1));
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002DD9 File Offset: 0x00000FD9
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002DE6 File Offset: 0x00000FE6
		public bool hasUV3
		{
			get
			{
				return (this.m_Attributes & VertexAttributes.Texture2) == VertexAttributes.Texture2;
			}
			private set
			{
				this.m_Attributes = (value ? (this.m_Attributes | VertexAttributes.Texture2) : (this.m_Attributes & ~VertexAttributes.Texture2));
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002E04 File Offset: 0x00001004
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002E13 File Offset: 0x00001013
		public bool hasUV4
		{
			get
			{
				return (this.m_Attributes & VertexAttributes.Texture3) == VertexAttributes.Texture3;
			}
			private set
			{
				this.m_Attributes = (value ? (this.m_Attributes | VertexAttributes.Texture3) : (this.m_Attributes & ~VertexAttributes.Texture3));
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002E32 File Offset: 0x00001032
		public void Flip()
		{
			if (this.hasNormal)
			{
				this.m_Normal *= -1f;
			}
			if (this.hasTangent)
			{
				this.m_Tangent *= -1f;
			}
		}

		// Token: 0x0400000D RID: 13
		private Vector3 m_Position;

		// Token: 0x0400000E RID: 14
		private Color m_Color;

		// Token: 0x0400000F RID: 15
		private Vector3 m_Normal;

		// Token: 0x04000010 RID: 16
		private Vector4 m_Tangent;

		// Token: 0x04000011 RID: 17
		private Vector2 m_UV0;

		// Token: 0x04000012 RID: 18
		private Vector2 m_UV2;

		// Token: 0x04000013 RID: 19
		private Vector4 m_UV3;

		// Token: 0x04000014 RID: 20
		private Vector4 m_UV4;

		// Token: 0x04000015 RID: 21
		private VertexAttributes m_Attributes;
	}
}
