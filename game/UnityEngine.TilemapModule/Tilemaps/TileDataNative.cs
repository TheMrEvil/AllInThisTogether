using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000013 RID: 19
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/Tilemap/TilemapScripting.h")]
	internal struct TileDataNative
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00002F14 File Offset: 0x00001114
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00002F2C File Offset: 0x0000112C
		public int sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				this.m_Sprite = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00002F38 File Offset: 0x00001138
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00002F50 File Offset: 0x00001150
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00002F5C File Offset: 0x0000115C
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00002F74 File Offset: 0x00001174
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00002F80 File Offset: 0x00001180
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00002F98 File Offset: 0x00001198
		public int gameObject
		{
			get
			{
				return this.m_GameObject;
			}
			set
			{
				this.m_GameObject = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00002FA4 File Offset: 0x000011A4
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00002FBC File Offset: 0x000011BC
		public TileFlags flags
		{
			get
			{
				return this.m_Flags;
			}
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00002FC8 File Offset: 0x000011C8
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00002FE0 File Offset: 0x000011E0
		public Tile.ColliderType colliderType
		{
			get
			{
				return this.m_ColliderType;
			}
			set
			{
				this.m_ColliderType = value;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00002FEC File Offset: 0x000011EC
		public static implicit operator TileDataNative(TileData td)
		{
			return new TileDataNative
			{
				sprite = ((td.sprite != null) ? td.sprite.GetInstanceID() : 0),
				color = td.color,
				transform = td.transform,
				gameObject = ((td.gameObject != null) ? td.gameObject.GetInstanceID() : 0),
				flags = td.flags,
				colliderType = td.colliderType
			};
		}

		// Token: 0x04000043 RID: 67
		private int m_Sprite;

		// Token: 0x04000044 RID: 68
		private Color m_Color;

		// Token: 0x04000045 RID: 69
		private Matrix4x4 m_Transform;

		// Token: 0x04000046 RID: 70
		private int m_GameObject;

		// Token: 0x04000047 RID: 71
		private TileFlags m_Flags;

		// Token: 0x04000048 RID: 72
		private Tile.ColliderType m_ColliderType;
	}
}
