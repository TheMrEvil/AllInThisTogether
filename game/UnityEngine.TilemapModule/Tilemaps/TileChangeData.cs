using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000014 RID: 20
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/Tilemap/TilemapScripting.h")]
	public struct TileChangeData
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00003090 File Offset: 0x00001290
		// (set) Token: 0x060000EF RID: 239 RVA: 0x000030A8 File Offset: 0x000012A8
		public Vector3Int position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000030B4 File Offset: 0x000012B4
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x000030D1 File Offset: 0x000012D1
		public TileBase tile
		{
			get
			{
				return (TileBase)this.m_TileAsset;
			}
			set
			{
				this.m_TileAsset = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000030DC File Offset: 0x000012DC
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x000030F4 File Offset: 0x000012F4
		public Color color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				this.m_Color = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00003100 File Offset: 0x00001300
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00003118 File Offset: 0x00001318
		public Matrix4x4 transform
		{
			get
			{
				return this.m_Transform;
			}
			set
			{
				this.m_Transform = value;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003122 File Offset: 0x00001322
		public TileChangeData(Vector3Int position, TileBase tile, Color color, Matrix4x4 transform)
		{
			this.m_Position = position;
			this.m_TileAsset = tile;
			this.m_Color = color;
			this.m_Transform = transform;
		}

		// Token: 0x04000049 RID: 73
		private Vector3Int m_Position;

		// Token: 0x0400004A RID: 74
		private Object m_TileAsset;

		// Token: 0x0400004B RID: 75
		private Color m_Color;

		// Token: 0x0400004C RID: 76
		private Matrix4x4 m_Transform;
	}
}
