using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Tilemaps
{
	// Token: 0x0200000B RID: 11
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Grid/Public/GridMarshalling.h")]
	[NativeHeader("Modules/Grid/Public/Grid.h")]
	[NativeHeader("Runtime/Graphics/SpriteFrame.h")]
	[NativeHeader("Modules/Tilemap/Public/TilemapTile.h")]
	[NativeType(Header = "Modules/Tilemap/Public/Tilemap.h")]
	[NativeHeader("Modules/Tilemap/Public/TilemapMarshalling.h")]
	public sealed class Tilemap : GridLayout
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003E RID: 62
		public extern Grid layoutGrid { [NativeMethod(Name = "GetAttachedGrid")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600003F RID: 63 RVA: 0x00002840 File Offset: 0x00000A40
		public Vector3 GetCellCenterLocal(Vector3Int position)
		{
			return base.CellToLocalInterpolated(position) + base.CellToLocalInterpolated(this.tileAnchor);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002870 File Offset: 0x00000A70
		public Vector3 GetCellCenterWorld(Vector3Int position)
		{
			return base.LocalToWorld(base.CellToLocalInterpolated(position) + base.CellToLocalInterpolated(this.tileAnchor));
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000028A8 File Offset: 0x00000AA8
		public BoundsInt cellBounds
		{
			get
			{
				return new BoundsInt(this.origin, this.size);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000028CC File Offset: 0x00000ACC
		[NativeProperty("TilemapBoundsScripting")]
		public Bounds localBounds
		{
			get
			{
				Bounds result;
				this.get_localBounds_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000028E4 File Offset: 0x00000AE4
		[NativeProperty("TilemapFrameBoundsScripting")]
		internal Bounds localFrameBounds
		{
			get
			{
				Bounds result;
				this.get_localFrameBounds_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000044 RID: 68
		// (set) Token: 0x06000045 RID: 69
		public extern float animationFrameRate { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000028FC File Offset: 0x00000AFC
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002912 File Offset: 0x00000B12
		public Color color
		{
			get
			{
				Color result;
				this.get_color_Injected(out result);
				return result;
			}
			set
			{
				this.set_color_Injected(ref value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000291C File Offset: 0x00000B1C
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002932 File Offset: 0x00000B32
		public Vector3Int origin
		{
			get
			{
				Vector3Int result;
				this.get_origin_Injected(out result);
				return result;
			}
			set
			{
				this.set_origin_Injected(ref value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000293C File Offset: 0x00000B3C
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002952 File Offset: 0x00000B52
		public Vector3Int size
		{
			get
			{
				Vector3Int result;
				this.get_size_Injected(out result);
				return result;
			}
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000295C File Offset: 0x00000B5C
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002972 File Offset: 0x00000B72
		[NativeProperty(Name = "TileAnchorScripting")]
		public Vector3 tileAnchor
		{
			get
			{
				Vector3 result;
				this.get_tileAnchor_Injected(out result);
				return result;
			}
			set
			{
				this.set_tileAnchor_Injected(ref value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004E RID: 78
		// (set) Token: 0x0600004F RID: 79
		public extern Tilemap.Orientation orientation { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000297C File Offset: 0x00000B7C
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002992 File Offset: 0x00000B92
		public Matrix4x4 orientationMatrix
		{
			[NativeMethod(Name = "GetTileOrientationMatrix")]
			get
			{
				Matrix4x4 result;
				this.get_orientationMatrix_Injected(out result);
				return result;
			}
			[NativeMethod(Name = "SetOrientationMatrix")]
			set
			{
				this.set_orientationMatrix_Injected(ref value);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000299C File Offset: 0x00000B9C
		internal Object GetTileAsset(Vector3Int position)
		{
			return this.GetTileAsset_Injected(ref position);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000029A8 File Offset: 0x00000BA8
		public TileBase GetTile(Vector3Int position)
		{
			return this.GetTileAsset(position) as TileBase;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000029C8 File Offset: 0x00000BC8
		public T GetTile<T>(Vector3Int position) where T : TileBase
		{
			return this.GetTileAsset(position) as T;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000029EB File Offset: 0x00000BEB
		internal Object[] GetTileAssetsBlock(Vector3Int position, Vector3Int blockDimensions)
		{
			return this.GetTileAssetsBlock_Injected(ref position, ref blockDimensions);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000029F8 File Offset: 0x00000BF8
		public TileBase[] GetTilesBlock(BoundsInt bounds)
		{
			Object[] tileAssetsBlock = this.GetTileAssetsBlock(bounds.min, bounds.size);
			TileBase[] array = new TileBase[tileAssetsBlock.Length];
			for (int i = 0; i < tileAssetsBlock.Length; i++)
			{
				array[i] = (TileBase)tileAssetsBlock[i];
			}
			return array;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002A4A File Offset: 0x00000C4A
		[FreeFunction(Name = "TilemapBindings::GetTileAssetsBlockNonAlloc", HasExplicitThis = true)]
		internal int GetTileAssetsBlockNonAlloc(Vector3Int startPosition, Vector3Int endPosition, [Unmarshalled] Object[] tiles)
		{
			return this.GetTileAssetsBlockNonAlloc_Injected(ref startPosition, ref endPosition, tiles);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002A58 File Offset: 0x00000C58
		public int GetTilesBlockNonAlloc(BoundsInt bounds, TileBase[] tiles)
		{
			return this.GetTileAssetsBlockNonAlloc(bounds.min, bounds.size, tiles);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002A81 File Offset: 0x00000C81
		public int GetTilesRangeCount(Vector3Int startPosition, Vector3Int endPosition)
		{
			return this.GetTilesRangeCount_Injected(ref startPosition, ref endPosition);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002A8D File Offset: 0x00000C8D
		[FreeFunction(Name = "TilemapBindings::GetTileAssetsRangeNonAlloc", HasExplicitThis = true)]
		internal int GetTileAssetsRangeNonAlloc(Vector3Int startPosition, Vector3Int endPosition, [Unmarshalled] Vector3Int[] positions, [Unmarshalled] Object[] tiles)
		{
			return this.GetTileAssetsRangeNonAlloc_Injected(ref startPosition, ref endPosition, positions, tiles);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002A9C File Offset: 0x00000C9C
		public int GetTilesRangeNonAlloc(Vector3Int startPosition, Vector3Int endPosition, Vector3Int[] positions, TileBase[] tiles)
		{
			return this.GetTileAssetsRangeNonAlloc(startPosition, endPosition, positions, tiles);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002ABB File Offset: 0x00000CBB
		internal void SetTileAsset(Vector3Int position, Object tile)
		{
			this.SetTileAsset_Injected(ref position, tile);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002AC6 File Offset: 0x00000CC6
		public void SetTile(Vector3Int position, TileBase tile)
		{
			this.SetTileAsset(position, tile);
		}

		// Token: 0x0600005E RID: 94
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetTileAssets(Vector3Int[] positionArray, Object[] tileArray);

		// Token: 0x0600005F RID: 95 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public void SetTiles(Vector3Int[] positionArray, TileBase[] tileArray)
		{
			this.SetTileAssets(positionArray, tileArray);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002AED File Offset: 0x00000CED
		[NativeMethod(Name = "SetTileAssetsBlock")]
		private void INTERNAL_CALL_SetTileAssetsBlock(Vector3Int position, Vector3Int blockDimensions, Object[] tileArray)
		{
			this.INTERNAL_CALL_SetTileAssetsBlock_Injected(ref position, ref blockDimensions, tileArray);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002AFC File Offset: 0x00000CFC
		public void SetTilesBlock(BoundsInt position, TileBase[] tileArray)
		{
			this.INTERNAL_CALL_SetTileAssetsBlock(position.min, position.size, tileArray);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002B22 File Offset: 0x00000D22
		[NativeMethod(Name = "SetTileChangeData")]
		public void SetTile(TileChangeData tileChangeData, bool ignoreLockFlags)
		{
			this.SetTile_Injected(ref tileChangeData, ignoreLockFlags);
		}

		// Token: 0x06000063 RID: 99
		[NativeMethod(Name = "SetTileChangeDataArray")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTiles(TileChangeData[] tileChangeDataArray, bool ignoreLockFlags);

		// Token: 0x06000064 RID: 100 RVA: 0x00002B30 File Offset: 0x00000D30
		public bool HasTile(Vector3Int position)
		{
			return this.GetTileAsset(position) != null;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002B4F File Offset: 0x00000D4F
		[NativeMethod(Name = "RefreshTileAsset")]
		public void RefreshTile(Vector3Int position)
		{
			this.RefreshTile_Injected(ref position);
		}

		// Token: 0x06000066 RID: 102
		[FreeFunction(Name = "TilemapBindings::RefreshTileAssetsNative", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe extern void RefreshTilesNative(void* positions, int count);

		// Token: 0x06000067 RID: 103
		[NativeMethod(Name = "RefreshAllTileAssets")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RefreshAllTiles();

		// Token: 0x06000068 RID: 104
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SwapTileAsset(Object changeTile, Object newTile);

		// Token: 0x06000069 RID: 105 RVA: 0x00002B59 File Offset: 0x00000D59
		public void SwapTile(TileBase changeTile, TileBase newTile)
		{
			this.SwapTileAsset(changeTile, newTile);
		}

		// Token: 0x0600006A RID: 106
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool ContainsTileAsset(Object tileAsset);

		// Token: 0x0600006B RID: 107 RVA: 0x00002B68 File Offset: 0x00000D68
		public bool ContainsTile(TileBase tileAsset)
		{
			return this.ContainsTileAsset(tileAsset);
		}

		// Token: 0x0600006C RID: 108
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetUsedTilesCount();

		// Token: 0x0600006D RID: 109
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetUsedSpritesCount();

		// Token: 0x0600006E RID: 110 RVA: 0x00002B84 File Offset: 0x00000D84
		public int GetUsedTilesNonAlloc(TileBase[] usedTiles)
		{
			return this.Internal_GetUsedTilesNonAlloc(usedTiles);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public int GetUsedSpritesNonAlloc(Sprite[] usedSprites)
		{
			return this.Internal_GetUsedSpritesNonAlloc(usedSprites);
		}

		// Token: 0x06000070 RID: 112
		[FreeFunction(Name = "TilemapBindings::GetUsedTilesNonAlloc", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int Internal_GetUsedTilesNonAlloc([Unmarshalled] Object[] usedTiles);

		// Token: 0x06000071 RID: 113
		[FreeFunction(Name = "TilemapBindings::GetUsedSpritesNonAlloc", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int Internal_GetUsedSpritesNonAlloc([Unmarshalled] Object[] usedSprites);

		// Token: 0x06000072 RID: 114 RVA: 0x00002BBB File Offset: 0x00000DBB
		public Sprite GetSprite(Vector3Int position)
		{
			return this.GetSprite_Injected(ref position);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public Matrix4x4 GetTransformMatrix(Vector3Int position)
		{
			Matrix4x4 result;
			this.GetTransformMatrix_Injected(ref position, out result);
			return result;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002BE0 File Offset: 0x00000DE0
		public void SetTransformMatrix(Vector3Int position, Matrix4x4 transform)
		{
			this.SetTransformMatrix_Injected(ref position, ref transform);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002BEC File Offset: 0x00000DEC
		[NativeMethod(Name = "GetTileColor")]
		public Color GetColor(Vector3Int position)
		{
			Color result;
			this.GetColor_Injected(ref position, out result);
			return result;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002C04 File Offset: 0x00000E04
		[NativeMethod(Name = "SetTileColor")]
		public void SetColor(Vector3Int position, Color color)
		{
			this.SetColor_Injected(ref position, ref color);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002C10 File Offset: 0x00000E10
		public TileFlags GetTileFlags(Vector3Int position)
		{
			return this.GetTileFlags_Injected(ref position);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002C1A File Offset: 0x00000E1A
		public void SetTileFlags(Vector3Int position, TileFlags flags)
		{
			this.SetTileFlags_Injected(ref position, flags);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002C25 File Offset: 0x00000E25
		public void AddTileFlags(Vector3Int position, TileFlags flags)
		{
			this.AddTileFlags_Injected(ref position, flags);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002C30 File Offset: 0x00000E30
		public void RemoveTileFlags(Vector3Int position, TileFlags flags)
		{
			this.RemoveTileFlags_Injected(ref position, flags);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002C3B File Offset: 0x00000E3B
		[NativeMethod(Name = "GetTileInstantiatedObject")]
		public GameObject GetInstantiatedObject(Vector3Int position)
		{
			return this.GetInstantiatedObject_Injected(ref position);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002C45 File Offset: 0x00000E45
		[NativeMethod(Name = "GetTileObjectToInstantiate")]
		public GameObject GetObjectToInstantiate(Vector3Int position)
		{
			return this.GetObjectToInstantiate_Injected(ref position);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002C4F File Offset: 0x00000E4F
		[NativeMethod(Name = "SetTileColliderType")]
		public void SetColliderType(Vector3Int position, Tile.ColliderType colliderType)
		{
			this.SetColliderType_Injected(ref position, colliderType);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002C5A File Offset: 0x00000E5A
		[NativeMethod(Name = "GetTileColliderType")]
		public Tile.ColliderType GetColliderType(Vector3Int position)
		{
			return this.GetColliderType_Injected(ref position);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002C64 File Offset: 0x00000E64
		[NativeMethod(Name = "GetTileAnimationFrameCount")]
		public int GetAnimationFrameCount(Vector3Int position)
		{
			return this.GetAnimationFrameCount_Injected(ref position);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002C6E File Offset: 0x00000E6E
		[NativeMethod(Name = "GetTileAnimationFrame")]
		public int GetAnimationFrame(Vector3Int position)
		{
			return this.GetAnimationFrame_Injected(ref position);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002C78 File Offset: 0x00000E78
		[NativeMethod(Name = "SetTileAnimationFrame")]
		public void SetAnimationFrame(Vector3Int position, int frame)
		{
			this.SetAnimationFrame_Injected(ref position, frame);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002C83 File Offset: 0x00000E83
		[NativeMethod(Name = "GetTileAnimationTime")]
		public float GetAnimationTime(Vector3Int position)
		{
			return this.GetAnimationTime_Injected(ref position);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002C8D File Offset: 0x00000E8D
		[NativeMethod(Name = "SetTileAnimationTime")]
		public void SetAnimationTime(Vector3Int position, float time)
		{
			this.SetAnimationTime_Injected(ref position, time);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002C98 File Offset: 0x00000E98
		public void FloodFill(Vector3Int position, TileBase tile)
		{
			this.FloodFillTileAsset(position, tile);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002CA4 File Offset: 0x00000EA4
		[NativeMethod(Name = "FloodFill")]
		private void FloodFillTileAsset(Vector3Int position, Object tile)
		{
			this.FloodFillTileAsset_Injected(ref position, tile);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002CAF File Offset: 0x00000EAF
		public void BoxFill(Vector3Int position, TileBase tile, int startX, int startY, int endX, int endY)
		{
			this.BoxFillTileAsset(position, tile, startX, startY, endX, endY);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002CC2 File Offset: 0x00000EC2
		[NativeMethod(Name = "BoxFill")]
		private void BoxFillTileAsset(Vector3Int position, Object tile, int startX, int startY, int endX, int endY)
		{
			this.BoxFillTileAsset_Injected(ref position, tile, startX, startY, endX, endY);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public void InsertCells(Vector3Int position, Vector3Int insertCells)
		{
			this.InsertCells(position, insertCells.x, insertCells.y, insertCells.z);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public void InsertCells(Vector3Int position, int numColumns, int numRows, int numLayers)
		{
			this.InsertCells_Injected(ref position, numColumns, numRows, numLayers);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002D02 File Offset: 0x00000F02
		public void DeleteCells(Vector3Int position, Vector3Int deleteCells)
		{
			this.DeleteCells(position, deleteCells.x, deleteCells.y, deleteCells.z);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002D22 File Offset: 0x00000F22
		public void DeleteCells(Vector3Int position, int numColumns, int numRows, int numLayers)
		{
			this.DeleteCells_Injected(ref position, numColumns, numRows, numLayers);
		}

		// Token: 0x0600008C RID: 140
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearAllTiles();

		// Token: 0x0600008D RID: 141
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResizeBounds();

		// Token: 0x0600008E RID: 142
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CompressBounds();

		// Token: 0x0600008F RID: 143 RVA: 0x00002D30 File Offset: 0x00000F30
		public Tilemap()
		{
		}

		// Token: 0x06000090 RID: 144
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localBounds_Injected(out Bounds ret);

		// Token: 0x06000091 RID: 145
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localFrameBounds_Injected(out Bounds ret);

		// Token: 0x06000092 RID: 146
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_color_Injected(out Color ret);

		// Token: 0x06000093 RID: 147
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_color_Injected(ref Color value);

		// Token: 0x06000094 RID: 148
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_origin_Injected(out Vector3Int ret);

		// Token: 0x06000095 RID: 149
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_origin_Injected(ref Vector3Int value);

		// Token: 0x06000096 RID: 150
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_size_Injected(out Vector3Int ret);

		// Token: 0x06000097 RID: 151
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_size_Injected(ref Vector3Int value);

		// Token: 0x06000098 RID: 152
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_tileAnchor_Injected(out Vector3 ret);

		// Token: 0x06000099 RID: 153
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_tileAnchor_Injected(ref Vector3 value);

		// Token: 0x0600009A RID: 154
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_orientationMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x0600009B RID: 155
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_orientationMatrix_Injected(ref Matrix4x4 value);

		// Token: 0x0600009C RID: 156
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Object GetTileAsset_Injected(ref Vector3Int position);

		// Token: 0x0600009D RID: 157
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Object[] GetTileAssetsBlock_Injected(ref Vector3Int position, ref Vector3Int blockDimensions);

		// Token: 0x0600009E RID: 158
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetTileAssetsBlockNonAlloc_Injected(ref Vector3Int startPosition, ref Vector3Int endPosition, Object[] tiles);

		// Token: 0x0600009F RID: 159
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetTilesRangeCount_Injected(ref Vector3Int startPosition, ref Vector3Int endPosition);

		// Token: 0x060000A0 RID: 160
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetTileAssetsRangeNonAlloc_Injected(ref Vector3Int startPosition, ref Vector3Int endPosition, Vector3Int[] positions, Object[] tiles);

		// Token: 0x060000A1 RID: 161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTileAsset_Injected(ref Vector3Int position, Object tile);

		// Token: 0x060000A2 RID: 162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_CALL_SetTileAssetsBlock_Injected(ref Vector3Int position, ref Vector3Int blockDimensions, Object[] tileArray);

		// Token: 0x060000A3 RID: 163
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTile_Injected(ref TileChangeData tileChangeData, bool ignoreLockFlags);

		// Token: 0x060000A4 RID: 164
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RefreshTile_Injected(ref Vector3Int position);

		// Token: 0x060000A5 RID: 165
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Sprite GetSprite_Injected(ref Vector3Int position);

		// Token: 0x060000A6 RID: 166
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetTransformMatrix_Injected(ref Vector3Int position, out Matrix4x4 ret);

		// Token: 0x060000A7 RID: 167
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTransformMatrix_Injected(ref Vector3Int position, ref Matrix4x4 transform);

		// Token: 0x060000A8 RID: 168
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetColor_Injected(ref Vector3Int position, out Color ret);

		// Token: 0x060000A9 RID: 169
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetColor_Injected(ref Vector3Int position, ref Color color);

		// Token: 0x060000AA RID: 170
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern TileFlags GetTileFlags_Injected(ref Vector3Int position);

		// Token: 0x060000AB RID: 171
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTileFlags_Injected(ref Vector3Int position, TileFlags flags);

		// Token: 0x060000AC RID: 172
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddTileFlags_Injected(ref Vector3Int position, TileFlags flags);

		// Token: 0x060000AD RID: 173
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RemoveTileFlags_Injected(ref Vector3Int position, TileFlags flags);

		// Token: 0x060000AE RID: 174
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern GameObject GetInstantiatedObject_Injected(ref Vector3Int position);

		// Token: 0x060000AF RID: 175
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern GameObject GetObjectToInstantiate_Injected(ref Vector3Int position);

		// Token: 0x060000B0 RID: 176
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetColliderType_Injected(ref Vector3Int position, Tile.ColliderType colliderType);

		// Token: 0x060000B1 RID: 177
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Tile.ColliderType GetColliderType_Injected(ref Vector3Int position);

		// Token: 0x060000B2 RID: 178
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetAnimationFrameCount_Injected(ref Vector3Int position);

		// Token: 0x060000B3 RID: 179
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetAnimationFrame_Injected(ref Vector3Int position);

		// Token: 0x060000B4 RID: 180
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetAnimationFrame_Injected(ref Vector3Int position, int frame);

		// Token: 0x060000B5 RID: 181
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetAnimationTime_Injected(ref Vector3Int position);

		// Token: 0x060000B6 RID: 182
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetAnimationTime_Injected(ref Vector3Int position, float time);

		// Token: 0x060000B7 RID: 183
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void FloodFillTileAsset_Injected(ref Vector3Int position, Object tile);

		// Token: 0x060000B8 RID: 184
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void BoxFillTileAsset_Injected(ref Vector3Int position, Object tile, int startX, int startY, int endX, int endY);

		// Token: 0x060000B9 RID: 185
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InsertCells_Injected(ref Vector3Int position, int numColumns, int numRows, int numLayers);

		// Token: 0x060000BA RID: 186
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void DeleteCells_Injected(ref Vector3Int position, int numColumns, int numRows, int numLayers);

		// Token: 0x0200000C RID: 12
		public enum Orientation
		{
			// Token: 0x04000023 RID: 35
			XY,
			// Token: 0x04000024 RID: 36
			XZ,
			// Token: 0x04000025 RID: 37
			YX,
			// Token: 0x04000026 RID: 38
			YZ,
			// Token: 0x04000027 RID: 39
			ZX,
			// Token: 0x04000028 RID: 40
			ZY,
			// Token: 0x04000029 RID: 41
			Custom
		}
	}
}
