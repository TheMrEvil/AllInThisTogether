using System;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x0200000A RID: 10
	[RequiredByNativeCode]
	public abstract class TileBase : ScriptableObject
	{
		// Token: 0x06000035 RID: 53 RVA: 0x0000279D File Offset: 0x0000099D
		[RequiredByNativeCode]
		public virtual void RefreshTile(Vector3Int position, ITilemap tilemap)
		{
			tilemap.RefreshTile(position);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002101 File Offset: 0x00000301
		[RequiredByNativeCode]
		public virtual void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027A8 File Offset: 0x000009A8
		private TileData GetTileDataNoRef(Vector3Int position, ITilemap tilemap)
		{
			TileData result = default(TileData);
			this.GetTileData(position, tilemap, ref result);
			return result;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000027D0 File Offset: 0x000009D0
		[RequiredByNativeCode]
		public virtual bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
		{
			return false;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000027E4 File Offset: 0x000009E4
		private TileAnimationData GetTileAnimationDataNoRef(Vector3Int position, ITilemap tilemap)
		{
			TileAnimationData result = default(TileAnimationData);
			this.GetTileAnimationData(position, tilemap, ref result);
			return result;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000280A File Offset: 0x00000A0A
		[RequiredByNativeCode]
		private void GetTileAnimationDataRef(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData, ref bool hasAnimation)
		{
			hasAnimation = this.GetTileAnimationData(position, tilemap, ref tileAnimationData);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000281C File Offset: 0x00000A1C
		[RequiredByNativeCode]
		public virtual bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
		{
			return false;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000282F File Offset: 0x00000A2F
		[RequiredByNativeCode]
		private void StartUpRef(Vector3Int position, ITilemap tilemap, GameObject go, ref bool startUpInvokedByUser)
		{
			startUpInvokedByUser = this.StartUp(position, tilemap, go);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002201 File Offset: 0x00000401
		protected TileBase()
		{
		}
	}
}
