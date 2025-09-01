using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000B3 RID: 179
	public static class TileLayoutUtils
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x0001C388 File Offset: 0x0001A588
		public static bool TryLayoutByTiles(RectInt src, uint tileSize, out RectInt main, out RectInt topRow, out RectInt rightCol, out RectInt topRight)
		{
			if ((long)src.width < (long)((ulong)tileSize) || (long)src.height < (long)((ulong)tileSize))
			{
				main = new RectInt(0, 0, 0, 0);
				topRow = new RectInt(0, 0, 0, 0);
				rightCol = new RectInt(0, 0, 0, 0);
				topRight = new RectInt(0, 0, 0, 0);
				return false;
			}
			int num = src.height / (int)tileSize;
			int num2 = src.width / (int)tileSize * (int)tileSize;
			int num3 = num * (int)tileSize;
			main = new RectInt
			{
				x = src.x,
				y = src.y,
				width = num2,
				height = num3
			};
			topRow = new RectInt
			{
				x = src.x,
				y = src.y + num3,
				width = num2,
				height = src.height - num3
			};
			rightCol = new RectInt
			{
				x = src.x + num2,
				y = src.y,
				width = src.width - num2,
				height = num3
			};
			topRight = new RectInt
			{
				x = src.x + num2,
				y = src.y + num3,
				width = src.width - num2,
				height = src.height - num3
			};
			return true;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001C51C File Offset: 0x0001A71C
		public static bool TryLayoutByRow(RectInt src, uint tileSize, out RectInt main, out RectInt other)
		{
			if ((long)src.height < (long)((ulong)tileSize))
			{
				main = new RectInt(0, 0, 0, 0);
				other = new RectInt(0, 0, 0, 0);
				return false;
			}
			int num = src.height / (int)tileSize * (int)tileSize;
			main = new RectInt
			{
				x = src.x,
				y = src.y,
				width = src.width,
				height = num
			};
			other = new RectInt
			{
				x = src.x,
				y = src.y + num,
				width = src.width,
				height = src.height - num
			};
			return true;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001C5F0 File Offset: 0x0001A7F0
		public static bool TryLayoutByCol(RectInt src, uint tileSize, out RectInt main, out RectInt other)
		{
			if ((long)src.width < (long)((ulong)tileSize))
			{
				main = new RectInt(0, 0, 0, 0);
				other = new RectInt(0, 0, 0, 0);
				return false;
			}
			int num = src.width / (int)tileSize * (int)tileSize;
			main = new RectInt
			{
				x = src.x,
				y = src.y,
				width = num,
				height = src.height
			};
			other = new RectInt
			{
				x = src.x + num,
				y = src.y,
				width = src.width - num,
				height = src.height
			};
			return true;
		}
	}
}
