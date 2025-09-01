using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Rendering;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000064 RID: 100
	[Serializable]
	public sealed class Vertex : IEquatable<Vertex>
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0002289C File Offset: 0x00020A9C
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x000228A4 File Offset: 0x00020AA4
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

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x000228B4 File Offset: 0x00020AB4
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x000228BC File Offset: 0x00020ABC
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

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x000228CC File Offset: 0x00020ACC
		// (set) Token: 0x060003CA RID: 970 RVA: 0x000228D4 File Offset: 0x00020AD4
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

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003CB RID: 971 RVA: 0x000228E4 File Offset: 0x00020AE4
		// (set) Token: 0x060003CC RID: 972 RVA: 0x000228EC File Offset: 0x00020AEC
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

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003CD RID: 973 RVA: 0x000228FC File Offset: 0x00020AFC
		// (set) Token: 0x060003CE RID: 974 RVA: 0x00022904 File Offset: 0x00020B04
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

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00022914 File Offset: 0x00020B14
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x0002291C File Offset: 0x00020B1C
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

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0002292C File Offset: 0x00020B2C
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x00022934 File Offset: 0x00020B34
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

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00022944 File Offset: 0x00020B44
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x0002294C File Offset: 0x00020B4C
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

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0002295C File Offset: 0x00020B5C
		internal MeshArrays attributes
		{
			get
			{
				return this.m_Attributes;
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00022964 File Offset: 0x00020B64
		public bool HasArrays(MeshArrays attribute)
		{
			return (this.m_Attributes & attribute) == attribute;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00022971 File Offset: 0x00020B71
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x0002297E File Offset: 0x00020B7E
		private bool hasPosition
		{
			get
			{
				return (this.m_Attributes & MeshArrays.Position) == MeshArrays.Position;
			}
			set
			{
				this.m_Attributes = (value ? (this.m_Attributes | MeshArrays.Position) : (this.m_Attributes & ~MeshArrays.Position));
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0002299C File Offset: 0x00020B9C
		// (set) Token: 0x060003DA RID: 986 RVA: 0x000229AB File Offset: 0x00020BAB
		private bool hasColor
		{
			get
			{
				return (this.m_Attributes & MeshArrays.Color) == MeshArrays.Color;
			}
			set
			{
				this.m_Attributes = (value ? (this.m_Attributes | MeshArrays.Color) : (this.m_Attributes & ~MeshArrays.Color));
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003DB RID: 987 RVA: 0x000229CA File Offset: 0x00020BCA
		// (set) Token: 0x060003DC RID: 988 RVA: 0x000229D9 File Offset: 0x00020BD9
		private bool hasNormal
		{
			get
			{
				return (this.m_Attributes & MeshArrays.Normal) == MeshArrays.Normal;
			}
			set
			{
				this.m_Attributes = (value ? (this.m_Attributes | MeshArrays.Normal) : (this.m_Attributes & ~MeshArrays.Normal));
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003DD RID: 989 RVA: 0x000229F8 File Offset: 0x00020BF8
		// (set) Token: 0x060003DE RID: 990 RVA: 0x00022A0D File Offset: 0x00020C0D
		private bool hasTangent
		{
			get
			{
				return (this.m_Attributes & MeshArrays.Tangent) == MeshArrays.Tangent;
			}
			set
			{
				this.m_Attributes = (value ? (this.m_Attributes | MeshArrays.Tangent) : (this.m_Attributes & ~MeshArrays.Tangent));
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00022A32 File Offset: 0x00020C32
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00022A3F File Offset: 0x00020C3F
		private bool hasUV0
		{
			get
			{
				return (this.m_Attributes & MeshArrays.Texture0) == MeshArrays.Texture0;
			}
			set
			{
				this.m_Attributes = (value ? (this.m_Attributes | MeshArrays.Texture0) : (this.m_Attributes & ~MeshArrays.Texture0));
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00022A5D File Offset: 0x00020C5D
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x00022A6A File Offset: 0x00020C6A
		private bool hasUV2
		{
			get
			{
				return (this.m_Attributes & MeshArrays.Texture1) == MeshArrays.Texture1;
			}
			set
			{
				this.m_Attributes = (value ? (this.m_Attributes | MeshArrays.Texture1) : (this.m_Attributes & ~MeshArrays.Texture1));
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00022A88 File Offset: 0x00020C88
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x00022A95 File Offset: 0x00020C95
		private bool hasUV3
		{
			get
			{
				return (this.m_Attributes & MeshArrays.Texture2) == MeshArrays.Texture2;
			}
			set
			{
				this.m_Attributes = (value ? (this.m_Attributes | MeshArrays.Texture2) : (this.m_Attributes & ~MeshArrays.Texture2));
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00022AB3 File Offset: 0x00020CB3
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x00022AC2 File Offset: 0x00020CC2
		private bool hasUV4
		{
			get
			{
				return (this.m_Attributes & MeshArrays.Texture3) == MeshArrays.Texture3;
			}
			set
			{
				this.m_Attributes = (value ? (this.m_Attributes | MeshArrays.Texture3) : (this.m_Attributes & ~MeshArrays.Texture3));
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00022AE1 File Offset: 0x00020CE1
		public Vertex()
		{
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00022AE9 File Offset: 0x00020CE9
		public override bool Equals(object obj)
		{
			return obj is Vertex && this.Equals((Vertex)obj);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00022B04 File Offset: 0x00020D04
		public bool Equals(Vertex other)
		{
			return other != null && (this.m_Position.Approx3(other.m_Position, 0.0001f) && this.m_Color.ApproxC(other.m_Color, 0.0001f) && this.m_Normal.Approx3(other.m_Normal, 0.0001f) && this.m_Tangent.Approx4(other.m_Tangent, 0.0001f) && this.m_UV0.Approx2(other.m_UV0, 0.0001f) && this.m_UV2.Approx2(other.m_UV2, 0.0001f) && this.m_UV3.Approx4(other.m_UV3, 0.0001f)) && this.m_UV4.Approx4(other.m_UV4, 0.0001f);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00022BDC File Offset: 0x00020DDC
		public bool Equals(Vertex other, MeshArrays mask)
		{
			return other != null && (((mask & MeshArrays.Position) != MeshArrays.Position || this.m_Position.Approx3(other.m_Position, 0.0001f)) && ((mask & MeshArrays.Color) != MeshArrays.Color || this.m_Color.ApproxC(other.m_Color, 0.0001f)) && ((mask & MeshArrays.Normal) != MeshArrays.Normal || this.m_Normal.Approx3(other.m_Normal, 0.0001f)) && ((mask & MeshArrays.Tangent) != MeshArrays.Tangent || this.m_Tangent.Approx4(other.m_Tangent, 0.0001f)) && ((mask & MeshArrays.Texture0) != MeshArrays.Texture0 || this.m_UV0.Approx2(other.m_UV0, 0.0001f)) && ((mask & MeshArrays.Texture1) != MeshArrays.Texture1 || this.m_UV2.Approx2(other.m_UV2, 0.0001f)) && ((mask & MeshArrays.Texture2) != MeshArrays.Texture2 || this.m_UV3.Approx4(other.m_UV3, 0.0001f))) && ((mask & MeshArrays.Texture3) != MeshArrays.Texture3 || this.m_UV4.Approx4(other.m_UV4, 0.0001f));
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00022CF7 File Offset: 0x00020EF7
		public override int GetHashCode()
		{
			return ((783 + VectorHash.GetHashCode(this.position)) * 29 + VectorHash.GetHashCode(this.uv0)) * 31 + VectorHash.GetHashCode(this.normal);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00022D28 File Offset: 0x00020F28
		public Vertex(Vertex vertex)
		{
			if (vertex == null)
			{
				throw new ArgumentNullException("vertex");
			}
			this.m_Position = vertex.m_Position;
			this.hasPosition = vertex.hasPosition;
			this.m_Color = vertex.m_Color;
			this.hasColor = vertex.hasColor;
			this.m_UV0 = vertex.m_UV0;
			this.hasUV0 = vertex.hasUV0;
			this.m_Normal = vertex.m_Normal;
			this.hasNormal = vertex.hasNormal;
			this.m_Tangent = vertex.m_Tangent;
			this.hasTangent = vertex.hasTangent;
			this.m_UV2 = vertex.m_UV2;
			this.hasUV2 = vertex.hasUV2;
			this.m_UV3 = vertex.m_UV3;
			this.hasUV3 = vertex.hasUV3;
			this.m_UV4 = vertex.m_UV4;
			this.hasUV4 = vertex.hasUV4;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00022E0F File Offset: 0x0002100F
		public static bool operator ==(Vertex a, Vertex b)
		{
			return a == b || (a != null && b != null && a.Equals(b));
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00022E26 File Offset: 0x00021026
		public static bool operator !=(Vertex a, Vertex b)
		{
			return !(a == b);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00022E32 File Offset: 0x00021032
		public static Vertex operator +(Vertex a, Vertex b)
		{
			return Vertex.Add(a, b);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00022E3B File Offset: 0x0002103B
		public static Vertex Add(Vertex a, Vertex b)
		{
			Vertex vertex = new Vertex(a);
			vertex.Add(b);
			return vertex;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00022E4C File Offset: 0x0002104C
		public void Add(Vertex b)
		{
			if (b == null)
			{
				throw new ArgumentNullException("b");
			}
			this.m_Position += b.m_Position;
			this.m_Color += b.m_Color;
			this.m_Normal += b.m_Normal;
			this.m_Tangent += b.m_Tangent;
			this.m_UV0 += b.m_UV0;
			this.m_UV2 += b.m_UV2;
			this.m_UV3 += b.m_UV3;
			this.m_UV4 += b.m_UV4;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00022F25 File Offset: 0x00021125
		public static Vertex operator -(Vertex a, Vertex b)
		{
			return Vertex.Subtract(a, b);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00022F2E File Offset: 0x0002112E
		public static Vertex Subtract(Vertex a, Vertex b)
		{
			Vertex vertex = new Vertex(a);
			vertex.Subtract(b);
			return vertex;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00022F40 File Offset: 0x00021140
		public void Subtract(Vertex b)
		{
			if (b == null)
			{
				throw new ArgumentNullException("b");
			}
			this.m_Position -= b.m_Position;
			this.m_Color -= b.m_Color;
			this.m_Normal -= b.m_Normal;
			this.m_Tangent -= b.m_Tangent;
			this.m_UV0 -= b.m_UV0;
			this.m_UV2 -= b.m_UV2;
			this.m_UV3 -= b.m_UV3;
			this.m_UV4 -= b.m_UV4;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00023019 File Offset: 0x00021219
		public static Vertex operator *(Vertex a, float value)
		{
			return Vertex.Multiply(a, value);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00023022 File Offset: 0x00021222
		public static Vertex Multiply(Vertex a, float value)
		{
			Vertex vertex = new Vertex(a);
			vertex.Multiply(value);
			return vertex;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00023034 File Offset: 0x00021234
		public void Multiply(float value)
		{
			this.m_Position *= value;
			this.m_Color *= value;
			this.m_Normal *= value;
			this.m_Tangent *= value;
			this.m_UV0 *= value;
			this.m_UV2 *= value;
			this.m_UV3 *= value;
			this.m_UV4 *= value;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000230D1 File Offset: 0x000212D1
		public static Vertex operator /(Vertex a, float value)
		{
			return Vertex.Divide(a, value);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000230DA File Offset: 0x000212DA
		public static Vertex Divide(Vertex a, float value)
		{
			Vertex vertex = new Vertex(a);
			vertex.Divide(value);
			return vertex;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000230EC File Offset: 0x000212EC
		public void Divide(float value)
		{
			this.m_Position /= value;
			this.m_Color /= value;
			this.m_Normal /= value;
			this.m_Tangent /= value;
			this.m_UV0 /= value;
			this.m_UV2 /= value;
			this.m_UV3 /= value;
			this.m_UV4 /= value;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0002318C File Offset: 0x0002138C
		public void Normalize()
		{
			this.m_Position.Normalize();
			Vector4 v = this.m_Color;
			v.Normalize();
			this.m_Color = v;
			this.m_Normal.Normalize();
			this.m_Tangent.Normalize();
			this.m_UV0.Normalize();
			this.m_UV2.Normalize();
			this.m_UV3.Normalize();
			this.m_UV4.Normalize();
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00023208 File Offset: 0x00021408
		public string ToString(string args = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.hasPosition)
			{
				stringBuilder.AppendLine("position: " + this.m_Position.ToString(args));
			}
			if (this.hasColor)
			{
				stringBuilder.AppendLine("color: " + this.m_Color.ToString(args));
			}
			if (this.hasNormal)
			{
				stringBuilder.AppendLine("normal: " + this.m_Normal.ToString(args));
			}
			if (this.hasTangent)
			{
				stringBuilder.AppendLine("tangent: " + this.m_Tangent.ToString(args));
			}
			if (this.hasUV0)
			{
				stringBuilder.AppendLine("uv0: " + this.m_UV0.ToString(args));
			}
			if (this.hasUV2)
			{
				stringBuilder.AppendLine("uv2: " + this.m_UV2.ToString(args));
			}
			if (this.hasUV3)
			{
				stringBuilder.AppendLine("uv3: " + this.m_UV3.ToString(args));
			}
			if (this.hasUV4)
			{
				stringBuilder.AppendLine("uv4: " + this.m_UV4.ToString(args));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0002334C File Offset: 0x0002154C
		public static void GetArrays(IList<Vertex> vertices, out Vector3[] position, out Color[] color, out Vector2[] uv0, out Vector3[] normal, out Vector4[] tangent, out Vector2[] uv2, out List<Vector4> uv3, out List<Vector4> uv4)
		{
			Vertex.GetArrays(vertices, out position, out color, out uv0, out normal, out tangent, out uv2, out uv3, out uv4, MeshArrays.All);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00023374 File Offset: 0x00021574
		public static void GetArrays(IList<Vertex> vertices, out Vector3[] position, out Color[] color, out Vector2[] uv0, out Vector3[] normal, out Vector4[] tangent, out Vector2[] uv2, out List<Vector4> uv3, out List<Vector4> uv4, MeshArrays attributes)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("vertices");
			}
			int count = vertices.Count;
			Vertex vertex = (count < 1) ? new Vertex() : vertices[0];
			bool flag = (attributes & MeshArrays.Position) == MeshArrays.Position && vertex.hasPosition;
			bool flag2 = (attributes & MeshArrays.Color) == MeshArrays.Color && vertex.hasColor;
			bool flag3 = (attributes & MeshArrays.Texture0) == MeshArrays.Texture0 && vertex.hasUV0;
			bool flag4 = (attributes & MeshArrays.Normal) == MeshArrays.Normal && vertex.hasNormal;
			bool flag5 = (attributes & MeshArrays.Tangent) == MeshArrays.Tangent && vertex.hasTangent;
			bool flag6 = (attributes & MeshArrays.Texture1) == MeshArrays.Texture1 && vertex.hasUV2;
			bool flag7 = (attributes & MeshArrays.Texture2) == MeshArrays.Texture2 && vertex.hasUV3;
			bool flag8 = (attributes & MeshArrays.Texture3) == MeshArrays.Texture3 && vertex.hasUV4;
			position = (flag ? new Vector3[count] : null);
			color = (flag2 ? new Color[count] : null);
			uv0 = (flag3 ? new Vector2[count] : null);
			normal = (flag4 ? new Vector3[count] : null);
			tangent = (flag5 ? new Vector4[count] : null);
			uv2 = (flag6 ? new Vector2[count] : null);
			uv3 = (flag7 ? new List<Vector4>(count) : null);
			uv4 = (flag8 ? new List<Vector4>(count) : null);
			for (int i = 0; i < count; i++)
			{
				if (flag)
				{
					position[i] = vertices[i].m_Position;
				}
				if (flag2)
				{
					color[i] = vertices[i].m_Color;
				}
				if (flag3)
				{
					uv0[i] = vertices[i].m_UV0;
				}
				if (flag4)
				{
					normal[i] = vertices[i].m_Normal;
				}
				if (flag5)
				{
					tangent[i] = vertices[i].m_Tangent;
				}
				if (flag6)
				{
					uv2[i] = vertices[i].m_UV2;
				}
				if (flag7)
				{
					uv3.Add(vertices[i].m_UV3);
				}
				if (flag8)
				{
					uv4.Add(vertices[i].m_UV4);
				}
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000235A8 File Offset: 0x000217A8
		public static void SetMesh(Mesh mesh, IList<Vertex> vertices)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (vertices == null)
			{
				throw new ArgumentNullException("vertices");
			}
			Vector3[] vertices2 = null;
			Color[] colors = null;
			Vector2[] uv = null;
			Vector3[] normals = null;
			Vector4[] tangents = null;
			Vector2[] uv2 = null;
			List<Vector4> list = null;
			List<Vector4> list2 = null;
			Vertex.GetArrays(vertices, out vertices2, out colors, out uv, out normals, out tangents, out uv2, out list, out list2);
			mesh.Clear();
			Vertex vertex = vertices[0];
			if (vertex.hasPosition)
			{
				mesh.vertices = vertices2;
			}
			if (vertex.hasColor)
			{
				mesh.colors = colors;
			}
			if (vertex.hasUV0)
			{
				mesh.uv = uv;
			}
			if (vertex.hasNormal)
			{
				mesh.normals = normals;
			}
			if (vertex.hasTangent)
			{
				mesh.tangents = tangents;
			}
			if (vertex.hasUV2)
			{
				mesh.uv2 = uv2;
			}
			if (vertex.hasUV3 && list != null)
			{
				mesh.SetUVs(2, list);
			}
			if (vertex.hasUV4 && list2 != null)
			{
				mesh.SetUVs(3, list2);
			}
			mesh.indexFormat = ((mesh.vertexCount > 65535) ? IndexFormat.UInt32 : IndexFormat.UInt16);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000236AC File Offset: 0x000218AC
		public static Vertex Average(IList<Vertex> vertices, IList<int> indexes = null)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("vertices");
			}
			Vertex vertex = new Vertex();
			int num = (indexes != null) ? indexes.Count : vertices.Count;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			int num9 = 0;
			for (int i = 0; i < num; i++)
			{
				int index = (indexes == null) ? i : indexes[i];
				if (vertices[index].hasPosition)
				{
					num2++;
					vertex.m_Position += vertices[index].m_Position;
				}
				if (vertices[index].hasColor)
				{
					num3++;
					vertex.m_Color += vertices[index].m_Color;
				}
				if (vertices[index].hasUV0)
				{
					num4++;
					vertex.m_UV0 += vertices[index].m_UV0;
				}
				if (vertices[index].hasNormal)
				{
					num5++;
					vertex.m_Normal += vertices[index].m_Normal;
				}
				if (vertices[index].hasTangent)
				{
					num6++;
					vertex.m_Tangent += vertices[index].m_Tangent;
				}
				if (vertices[index].hasUV2)
				{
					num7++;
					vertex.m_UV2 += vertices[index].m_UV2;
				}
				if (vertices[index].hasUV3)
				{
					num8++;
					vertex.m_UV3 += vertices[index].m_UV3;
				}
				if (vertices[index].hasUV4)
				{
					num9++;
					vertex.m_UV4 += vertices[index].m_UV4;
				}
			}
			if (num2 > 0)
			{
				vertex.hasPosition = true;
				vertex.m_Position *= 1f / (float)num2;
			}
			if (num3 > 0)
			{
				vertex.hasColor = true;
				vertex.m_Color *= 1f / (float)num3;
			}
			if (num4 > 0)
			{
				vertex.hasUV0 = true;
				vertex.m_UV0 *= 1f / (float)num4;
			}
			if (num5 > 0)
			{
				vertex.hasNormal = true;
				vertex.m_Normal *= 1f / (float)num5;
			}
			if (num6 > 0)
			{
				vertex.hasTangent = true;
				vertex.m_Tangent *= 1f / (float)num6;
			}
			if (num7 > 0)
			{
				vertex.hasUV2 = true;
				vertex.m_UV2 *= 1f / (float)num7;
			}
			if (num8 > 0)
			{
				vertex.hasUV3 = true;
				vertex.m_UV3 *= 1f / (float)num8;
			}
			if (num9 > 0)
			{
				vertex.hasUV4 = true;
				vertex.m_UV4 *= 1f / (float)num9;
			}
			return vertex;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000239E0 File Offset: 0x00021BE0
		public static Vertex Mix(Vertex x, Vertex y, float weight)
		{
			if (x == null || y == null)
			{
				throw new ArgumentNullException("x", "Mix does accept null vertices.");
			}
			float num = 1f - weight;
			Vertex vertex = new Vertex();
			vertex.m_Position = x.m_Position * num + y.m_Position * weight;
			if (x.hasColor && y.hasColor)
			{
				vertex.m_Color = x.m_Color * num + y.m_Color * weight;
			}
			else if (x.hasColor)
			{
				vertex.m_Color = x.m_Color;
			}
			else if (y.hasColor)
			{
				vertex.m_Color = y.m_Color;
			}
			if (x.hasNormal && y.hasNormal)
			{
				vertex.m_Normal = x.m_Normal * num + y.m_Normal * weight;
			}
			else if (x.hasNormal)
			{
				vertex.m_Normal = x.m_Normal;
			}
			else if (y.hasNormal)
			{
				vertex.m_Normal = y.m_Normal;
			}
			if (x.hasTangent && y.hasTangent)
			{
				vertex.m_Tangent = x.m_Tangent * num + y.m_Tangent * weight;
			}
			else if (x.hasTangent)
			{
				vertex.m_Tangent = x.m_Tangent;
			}
			else if (y.hasTangent)
			{
				vertex.m_Tangent = y.m_Tangent;
			}
			if (x.hasUV0 && y.hasUV0)
			{
				vertex.m_UV0 = x.m_UV0 * num + y.m_UV0 * weight;
			}
			else if (x.hasUV0)
			{
				vertex.m_UV0 = x.m_UV0;
			}
			else if (y.hasUV0)
			{
				vertex.m_UV0 = y.m_UV0;
			}
			if (x.hasUV2 && y.hasUV2)
			{
				vertex.m_UV2 = x.m_UV2 * num + y.m_UV2 * weight;
			}
			else if (x.hasUV2)
			{
				vertex.m_UV2 = x.m_UV2;
			}
			else if (y.hasUV2)
			{
				vertex.m_UV2 = y.m_UV2;
			}
			if (x.hasUV3 && y.hasUV3)
			{
				vertex.m_UV3 = x.m_UV3 * num + y.m_UV3 * weight;
			}
			else if (x.hasUV3)
			{
				vertex.m_UV3 = x.m_UV3;
			}
			else if (y.hasUV3)
			{
				vertex.m_UV3 = y.m_UV3;
			}
			if (x.hasUV4 && y.hasUV4)
			{
				vertex.m_UV4 = x.m_UV4 * num + y.m_UV4 * weight;
			}
			else if (x.hasUV4)
			{
				vertex.m_UV4 = x.m_UV4;
			}
			else if (y.hasUV4)
			{
				vertex.m_UV4 = y.m_UV4;
			}
			return vertex;
		}

		// Token: 0x0400021C RID: 540
		[SerializeField]
		private Vector3 m_Position;

		// Token: 0x0400021D RID: 541
		[SerializeField]
		private Color m_Color;

		// Token: 0x0400021E RID: 542
		[SerializeField]
		private Vector3 m_Normal;

		// Token: 0x0400021F RID: 543
		[SerializeField]
		private Vector4 m_Tangent;

		// Token: 0x04000220 RID: 544
		[SerializeField]
		private Vector2 m_UV0;

		// Token: 0x04000221 RID: 545
		[SerializeField]
		private Vector2 m_UV2;

		// Token: 0x04000222 RID: 546
		[SerializeField]
		private Vector4 m_UV3;

		// Token: 0x04000223 RID: 547
		[SerializeField]
		private Vector4 m_UV4;

		// Token: 0x04000224 RID: 548
		[SerializeField]
		private MeshArrays m_Attributes;
	}
}
