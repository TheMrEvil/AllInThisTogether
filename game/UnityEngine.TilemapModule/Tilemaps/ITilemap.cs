using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000007 RID: 7
	[RequiredByNativeCode]
	public class ITilemap
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000220A File Offset: 0x0000040A
		internal ITilemap()
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002214 File Offset: 0x00000414
		internal void SetTilemapInstance(Tilemap tilemap)
		{
			this.m_Tilemap = tilemap;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002220 File Offset: 0x00000420
		public Vector3Int origin
		{
			get
			{
				return this.m_Tilemap.origin;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002240 File Offset: 0x00000440
		public Vector3Int size
		{
			get
			{
				return this.m_Tilemap.size;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002260 File Offset: 0x00000460
		public Bounds localBounds
		{
			get
			{
				return this.m_Tilemap.localBounds;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002280 File Offset: 0x00000480
		public BoundsInt cellBounds
		{
			get
			{
				return this.m_Tilemap.cellBounds;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022A0 File Offset: 0x000004A0
		public virtual Sprite GetSprite(Vector3Int position)
		{
			return this.m_Tilemap.GetSprite(position);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022C0 File Offset: 0x000004C0
		public virtual Color GetColor(Vector3Int position)
		{
			return this.m_Tilemap.GetColor(position);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022E0 File Offset: 0x000004E0
		public virtual Matrix4x4 GetTransformMatrix(Vector3Int position)
		{
			return this.m_Tilemap.GetTransformMatrix(position);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002300 File Offset: 0x00000500
		public virtual TileFlags GetTileFlags(Vector3Int position)
		{
			return this.m_Tilemap.GetTileFlags(position);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002320 File Offset: 0x00000520
		public virtual TileBase GetTile(Vector3Int position)
		{
			return this.m_Tilemap.GetTile(position);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002340 File Offset: 0x00000540
		public virtual T GetTile<T>(Vector3Int position) where T : TileBase
		{
			return this.m_Tilemap.GetTile<T>(position);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002360 File Offset: 0x00000560
		public void RefreshTile(Vector3Int position)
		{
			bool addToList = this.m_AddToList;
			if (addToList)
			{
				bool flag = this.m_RefreshCount >= this.m_RefreshPos.Length;
				if (flag)
				{
					NativeArray<Vector3Int> nativeArray = new NativeArray<Vector3Int>(Math.Max(1, this.m_RefreshCount * 2), Allocator.Temp, NativeArrayOptions.ClearMemory);
					NativeArray<Vector3Int>.Copy(this.m_RefreshPos, nativeArray, this.m_RefreshPos.Length);
					this.m_RefreshPos.Dispose();
					this.m_RefreshPos = nativeArray;
				}
				int refreshCount = this.m_RefreshCount;
				this.m_RefreshCount = refreshCount + 1;
				this.m_RefreshPos[refreshCount] = position;
			}
			else
			{
				this.m_Tilemap.RefreshTile(position);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002408 File Offset: 0x00000608
		public T GetComponent<T>()
		{
			return this.m_Tilemap.GetComponent<T>();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002428 File Offset: 0x00000628
		[RequiredByNativeCode]
		private static ITilemap CreateInstance()
		{
			ITilemap.s_Instance = new ITilemap();
			return ITilemap.s_Instance;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000244C File Offset: 0x0000064C
		[RequiredByNativeCode]
		private unsafe static void FindAllRefreshPositions(ITilemap tilemap, int count, IntPtr oldTilesIntPtr, IntPtr newTilesIntPtr, IntPtr positionsIntPtr)
		{
			tilemap.m_AddToList = true;
			NativeArray<Vector3Int> refreshPos = tilemap.m_RefreshPos;
			bool flag = !tilemap.m_RefreshPos.IsCreated || tilemap.m_RefreshPos.Length < count;
			if (flag)
			{
				tilemap.m_RefreshPos = new NativeArray<Vector3Int>(Math.Max(16, count), Allocator.Temp, NativeArrayOptions.ClearMemory);
			}
			tilemap.m_RefreshCount = 0;
			void* dataPointer = oldTilesIntPtr.ToPointer();
			void* dataPointer2 = newTilesIntPtr.ToPointer();
			void* dataPointer3 = positionsIntPtr.ToPointer();
			NativeArray<int> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(dataPointer, count, Allocator.Invalid);
			NativeArray<int> nativeArray2 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(dataPointer2, count, Allocator.Invalid);
			NativeArray<Vector3Int> nativeArray3 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3Int>(dataPointer3, count, Allocator.Invalid);
			for (int i = 0; i < count; i++)
			{
				int num = nativeArray[i];
				int num2 = nativeArray2[i];
				Vector3Int position = nativeArray3[i];
				bool flag2 = num != 0;
				if (flag2)
				{
					TileBase tileBase = (TileBase)Object.ForceLoadFromInstanceID(num);
					tileBase.RefreshTile(position, tilemap);
				}
				bool flag3 = num2 != 0;
				if (flag3)
				{
					TileBase tileBase2 = (TileBase)Object.ForceLoadFromInstanceID(num2);
					tileBase2.RefreshTile(position, tilemap);
				}
			}
			tilemap.m_Tilemap.RefreshTilesNative(tilemap.m_RefreshPos.m_Buffer, tilemap.m_RefreshCount);
			tilemap.m_RefreshPos.Dispose();
			tilemap.m_AddToList = false;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002594 File Offset: 0x00000794
		[RequiredByNativeCode]
		private unsafe static void GetAllTileData(ITilemap tilemap, int count, IntPtr tilesIntPtr, IntPtr positionsIntPtr, IntPtr outTileDataIntPtr)
		{
			void* dataPointer = tilesIntPtr.ToPointer();
			void* dataPointer2 = positionsIntPtr.ToPointer();
			void* dataPointer3 = outTileDataIntPtr.ToPointer();
			NativeArray<int> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(dataPointer, count, Allocator.Invalid);
			NativeArray<Vector3Int> nativeArray2 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3Int>(dataPointer2, count, Allocator.Invalid);
			NativeArray<TileData> nativeArray3 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<TileData>(dataPointer3, count, Allocator.Invalid);
			for (int i = 0; i < count; i++)
			{
				TileData @default = TileData.Default;
				int num = nativeArray[i];
				bool flag = num != 0;
				if (flag)
				{
					TileBase tileBase = (TileBase)Object.ForceLoadFromInstanceID(num);
					tileBase.GetTileData(nativeArray2[i], tilemap, ref @default);
				}
				nativeArray3[i] = @default;
			}
		}

		// Token: 0x04000013 RID: 19
		internal static ITilemap s_Instance;

		// Token: 0x04000014 RID: 20
		internal Tilemap m_Tilemap;

		// Token: 0x04000015 RID: 21
		internal bool m_AddToList;

		// Token: 0x04000016 RID: 22
		internal int m_RefreshCount;

		// Token: 0x04000017 RID: 23
		internal NativeArray<Vector3Int> m_RefreshPos;
	}
}
