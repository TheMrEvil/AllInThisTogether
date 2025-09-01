using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000012 RID: 18
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/Tilemap/TilemapScripting.h")]
	public struct TileData
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00002DB0 File Offset: 0x00000FB0
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00002DD2 File Offset: 0x00000FD2
		public Sprite sprite
		{
			get
			{
				return Object.ForceLoadFromInstanceID(this.m_Sprite) as Sprite;
			}
			set
			{
				this.m_Sprite = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00002DF0 File Offset: 0x00000FF0
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00002E08 File Offset: 0x00001008
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00002E14 File Offset: 0x00001014
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00002E2C File Offset: 0x0000102C
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00002E38 File Offset: 0x00001038
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00002E5A File Offset: 0x0000105A
		public GameObject gameObject
		{
			get
			{
				return Object.ForceLoadFromInstanceID(this.m_GameObject) as GameObject;
			}
			set
			{
				this.m_GameObject = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00002E78 File Offset: 0x00001078
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00002E90 File Offset: 0x00001090
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

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00002E9C File Offset: 0x0000109C
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00002EB4 File Offset: 0x000010B4
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

		// Token: 0x060000DF RID: 223 RVA: 0x00002EC0 File Offset: 0x000010C0
		private static TileData CreateDefault()
		{
			return new TileData
			{
				color = Color.white,
				transform = Matrix4x4.identity,
				flags = TileFlags.None,
				colliderType = Tile.ColliderType.None
			};
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00002F07 File Offset: 0x00001107
		// Note: this type is marked as 'beforefieldinit'.
		static TileData()
		{
		}

		// Token: 0x0400003C RID: 60
		private int m_Sprite;

		// Token: 0x0400003D RID: 61
		private Color m_Color;

		// Token: 0x0400003E RID: 62
		private Matrix4x4 m_Transform;

		// Token: 0x0400003F RID: 63
		private int m_GameObject;

		// Token: 0x04000040 RID: 64
		private TileFlags m_Flags;

		// Token: 0x04000041 RID: 65
		private Tile.ColliderType m_ColliderType;

		// Token: 0x04000042 RID: 66
		internal static readonly TileData Default = TileData.CreateDefault();
	}
}
