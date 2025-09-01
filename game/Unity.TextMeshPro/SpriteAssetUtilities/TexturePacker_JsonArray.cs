using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro.SpriteAssetUtilities
{
	// Token: 0x02000074 RID: 116
	public class TexturePacker_JsonArray
	{
		// Token: 0x060005D0 RID: 1488 RVA: 0x000382D3 File Offset: 0x000364D3
		public TexturePacker_JsonArray()
		{
		}

		// Token: 0x020000A8 RID: 168
		[Serializable]
		public struct SpriteFrame
		{
			// Token: 0x06000650 RID: 1616 RVA: 0x000391B0 File Offset: 0x000373B0
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"x: ",
					this.x.ToString("f2"),
					" y: ",
					this.y.ToString("f2"),
					" h: ",
					this.h.ToString("f2"),
					" w: ",
					this.w.ToString("f2")
				});
			}

			// Token: 0x0400060C RID: 1548
			public float x;

			// Token: 0x0400060D RID: 1549
			public float y;

			// Token: 0x0400060E RID: 1550
			public float w;

			// Token: 0x0400060F RID: 1551
			public float h;
		}

		// Token: 0x020000A9 RID: 169
		[Serializable]
		public struct SpriteSize
		{
			// Token: 0x06000651 RID: 1617 RVA: 0x00039234 File Offset: 0x00037434
			public override string ToString()
			{
				return "w: " + this.w.ToString("f2") + " h: " + this.h.ToString("f2");
			}

			// Token: 0x04000610 RID: 1552
			public float w;

			// Token: 0x04000611 RID: 1553
			public float h;
		}

		// Token: 0x020000AA RID: 170
		[Serializable]
		public struct Frame
		{
			// Token: 0x04000612 RID: 1554
			public string filename;

			// Token: 0x04000613 RID: 1555
			public TexturePacker_JsonArray.SpriteFrame frame;

			// Token: 0x04000614 RID: 1556
			public bool rotated;

			// Token: 0x04000615 RID: 1557
			public bool trimmed;

			// Token: 0x04000616 RID: 1558
			public TexturePacker_JsonArray.SpriteFrame spriteSourceSize;

			// Token: 0x04000617 RID: 1559
			public TexturePacker_JsonArray.SpriteSize sourceSize;

			// Token: 0x04000618 RID: 1560
			public Vector2 pivot;
		}

		// Token: 0x020000AB RID: 171
		[Serializable]
		public struct Meta
		{
			// Token: 0x04000619 RID: 1561
			public string app;

			// Token: 0x0400061A RID: 1562
			public string version;

			// Token: 0x0400061B RID: 1563
			public string image;

			// Token: 0x0400061C RID: 1564
			public string format;

			// Token: 0x0400061D RID: 1565
			public TexturePacker_JsonArray.SpriteSize size;

			// Token: 0x0400061E RID: 1566
			public float scale;

			// Token: 0x0400061F RID: 1567
			public string smartupdate;
		}

		// Token: 0x020000AC RID: 172
		[Serializable]
		public class SpriteDataObject
		{
			// Token: 0x06000652 RID: 1618 RVA: 0x00039265 File Offset: 0x00037465
			public SpriteDataObject()
			{
			}

			// Token: 0x04000620 RID: 1568
			public List<TexturePacker_JsonArray.Frame> frames;

			// Token: 0x04000621 RID: 1569
			public TexturePacker_JsonArray.Meta meta;
		}
	}
}
