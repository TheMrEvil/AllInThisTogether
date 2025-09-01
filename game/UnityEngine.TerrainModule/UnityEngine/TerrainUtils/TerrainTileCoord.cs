using System;

namespace UnityEngine.TerrainUtils
{
	// Token: 0x0200001A RID: 26
	public readonly struct TerrainTileCoord
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00004328 File Offset: 0x00002528
		public TerrainTileCoord(int tileX, int tileZ)
		{
			this.tileX = tileX;
			this.tileZ = tileZ;
		}

		// Token: 0x0400006A RID: 106
		public readonly int tileX;

		// Token: 0x0400006B RID: 107
		public readonly int tileZ;
	}
}
