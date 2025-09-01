using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000011 RID: 17
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class SplatPrototype
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00002C8C File Offset: 0x00000E8C
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public Texture2D texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				this.m_Texture = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00002CB0 File Offset: 0x00000EB0
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public Texture2D normalMap
		{
			get
			{
				return this.m_NormalMap;
			}
			set
			{
				this.m_NormalMap = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00002CD4 File Offset: 0x00000ED4
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00002CEC File Offset: 0x00000EEC
		public Vector2 tileSize
		{
			get
			{
				return this.m_TileSize;
			}
			set
			{
				this.m_TileSize = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00002CF8 File Offset: 0x00000EF8
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00002D10 File Offset: 0x00000F10
		public Vector2 tileOffset
		{
			get
			{
				return this.m_TileOffset;
			}
			set
			{
				this.m_TileOffset = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00002D1C File Offset: 0x00000F1C
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00002D54 File Offset: 0x00000F54
		public Color specular
		{
			get
			{
				return new Color(this.m_SpecularMetallic.x, this.m_SpecularMetallic.y, this.m_SpecularMetallic.z);
			}
			set
			{
				this.m_SpecularMetallic.x = value.r;
				this.m_SpecularMetallic.y = value.g;
				this.m_SpecularMetallic.z = value.b;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00002D8C File Offset: 0x00000F8C
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00002DA9 File Offset: 0x00000FA9
		public float metallic
		{
			get
			{
				return this.m_SpecularMetallic.w;
			}
			set
			{
				this.m_SpecularMetallic.w = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00002DB8 File Offset: 0x00000FB8
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public float smoothness
		{
			get
			{
				return this.m_Smoothness;
			}
			set
			{
				this.m_Smoothness = value;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00002DDC File Offset: 0x00000FDC
		public SplatPrototype()
		{
		}

		// Token: 0x04000032 RID: 50
		internal Texture2D m_Texture;

		// Token: 0x04000033 RID: 51
		internal Texture2D m_NormalMap;

		// Token: 0x04000034 RID: 52
		internal Vector2 m_TileSize = new Vector2(15f, 15f);

		// Token: 0x04000035 RID: 53
		internal Vector2 m_TileOffset = new Vector2(0f, 0f);

		// Token: 0x04000036 RID: 54
		internal Vector4 m_SpecularMetallic = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x04000037 RID: 55
		internal float m_Smoothness = 0f;
	}
}
