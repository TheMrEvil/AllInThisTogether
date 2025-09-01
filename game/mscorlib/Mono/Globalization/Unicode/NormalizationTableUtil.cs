using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000073 RID: 115
	internal class NormalizationTableUtil
	{
		// Token: 0x060001AD RID: 429 RVA: 0x00006B88 File Offset: 0x00004D88
		static NormalizationTableUtil()
		{
			int[] starts = new int[]
			{
				0,
				2320,
				6912,
				9312,
				10624,
				11376,
				11616,
				11920,
				42864,
				42992,
				63744
			};
			int[] ends = new int[]
			{
				1760,
				4608,
				9008,
				9728,
				10976,
				11392,
				11632,
				13312,
				42880,
				43008,
				65536
			};
			int[] starts2 = new int[]
			{
				144,
				2336,
				7456,
				9312,
				9376,
				10752,
				11616,
				11920,
				63744
			};
			int[] ends2 = new int[]
			{
				1760,
				4352,
				9008,
				9376,
				9456,
				10976,
				11632,
				13312,
				65536
			};
			int[] starts3 = new int[]
			{
				752,
				1152,
				1424,
				2352,
				2480,
				2608,
				2736,
				2864,
				3008,
				3136,
				3248,
				3392,
				3520,
				3632,
				3760,
				3840,
				4144,
				4944,
				5904,
				6096,
				6304,
				6448,
				6672,
				7616,
				8400,
				12320,
				12432,
				43008,
				64272,
				65056
			};
			int[] ends3 = new int[]
			{
				864,
				1168,
				1872,
				2400,
				2512,
				2640,
				2768,
				2896,
				3024,
				3168,
				3280,
				3408,
				3536,
				3664,
				3792,
				4048,
				4160,
				4960,
				5952,
				6112,
				6320,
				6464,
				6688,
				7632,
				8432,
				12336,
				12448,
				43024,
				64288,
				65072
			};
			int[] starts4 = new int[]
			{
				1152,
				5136,
				5744
			};
			int[] ends4 = new int[]
			{
				4224,
				5504,
				8624
			};
			int[] starts5 = new int[]
			{
				0,
				2304,
				7424,
				9472,
				12288,
				15248,
				16400,
				19968,
				64320
			};
			int[] ends5 = new int[]
			{
				1792,
				4608,
				8960,
				9728,
				12640,
				15264,
				16432,
				40960,
				64336
			};
			NormalizationTableUtil.Prop = new CodePointIndexer(starts, ends, 0, 0);
			NormalizationTableUtil.Map = new CodePointIndexer(starts2, ends2, 0, 0);
			NormalizationTableUtil.Combining = new CodePointIndexer(starts3, ends3, 0, 0);
			NormalizationTableUtil.Composite = new CodePointIndexer(starts4, ends4, 0, 0);
			NormalizationTableUtil.Helper = new CodePointIndexer(starts5, ends5, 0, 0);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00006CA3 File Offset: 0x00004EA3
		public static int PropIdx(int cp)
		{
			return NormalizationTableUtil.Prop.ToIndex(cp);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006CB0 File Offset: 0x00004EB0
		public static int PropCP(int index)
		{
			return NormalizationTableUtil.Prop.ToCodePoint(index);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00006CBD File Offset: 0x00004EBD
		public static int PropCount
		{
			get
			{
				return NormalizationTableUtil.Prop.TotalCount;
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006CC9 File Offset: 0x00004EC9
		public static int MapIdx(int cp)
		{
			return NormalizationTableUtil.Map.ToIndex(cp);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00006CD6 File Offset: 0x00004ED6
		public static int MapCP(int index)
		{
			return NormalizationTableUtil.Map.ToCodePoint(index);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006CE3 File Offset: 0x00004EE3
		public static int CbIdx(int cp)
		{
			return NormalizationTableUtil.Combining.ToIndex(cp);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006CF0 File Offset: 0x00004EF0
		public static int CbCP(int index)
		{
			return NormalizationTableUtil.Combining.ToCodePoint(index);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00006CFD File Offset: 0x00004EFD
		public static int MapCount
		{
			get
			{
				return NormalizationTableUtil.Map.TotalCount;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000259F File Offset: 0x0000079F
		public NormalizationTableUtil()
		{
		}

		// Token: 0x04000E5A RID: 3674
		public static readonly CodePointIndexer Prop;

		// Token: 0x04000E5B RID: 3675
		public static readonly CodePointIndexer Map;

		// Token: 0x04000E5C RID: 3676
		public static readonly CodePointIndexer Combining;

		// Token: 0x04000E5D RID: 3677
		public static readonly CodePointIndexer Composite;

		// Token: 0x04000E5E RID: 3678
		public static readonly CodePointIndexer Helper;
	}
}
