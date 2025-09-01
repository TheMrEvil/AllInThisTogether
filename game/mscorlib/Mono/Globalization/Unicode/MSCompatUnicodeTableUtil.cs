using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000072 RID: 114
	internal class MSCompatUnicodeTableUtil
	{
		// Token: 0x060001AB RID: 427 RVA: 0x00006A04 File Offset: 0x00004C04
		static MSCompatUnicodeTableUtil()
		{
			int[] starts = new int[]
			{
				0,
				40960,
				63744
			};
			int[] ends = new int[]
			{
				13312,
				42240,
				65536
			};
			int[] starts2 = new int[]
			{
				0,
				7680,
				12288,
				19968,
				44032,
				63744
			};
			int[] ends2 = new int[]
			{
				4608,
				10240,
				13312,
				40960,
				55216,
				65536
			};
			int[] starts3 = new int[]
			{
				0,
				7680,
				12288,
				19968,
				44032,
				63744
			};
			int[] ends3 = new int[]
			{
				4608,
				10240,
				13312,
				40960,
				55216,
				65536
			};
			int[] starts4 = new int[]
			{
				0,
				7680,
				12288,
				64256
			};
			int[] ends4 = new int[]
			{
				3840,
				10240,
				13312,
				65536
			};
			int[] starts5 = new int[]
			{
				0,
				7680,
				12288,
				64256
			};
			int[] ends5 = new int[]
			{
				4608,
				10240,
				13312,
				65536
			};
			int[] starts6 = new int[]
			{
				12544,
				19968,
				59392
			};
			int[] ends6 = new int[]
			{
				13312,
				40960,
				65536
			};
			int[] starts7 = new int[]
			{
				12544,
				19968,
				63744
			};
			int[] ends7 = new int[]
			{
				13312,
				40960,
				64256
			};
			MSCompatUnicodeTableUtil.Ignorable = new CodePointIndexer(starts, ends, -1, -1);
			MSCompatUnicodeTableUtil.Category = new CodePointIndexer(starts2, ends2, 0, 0);
			MSCompatUnicodeTableUtil.Level1 = new CodePointIndexer(starts3, ends3, 0, 0);
			MSCompatUnicodeTableUtil.Level2 = new CodePointIndexer(starts4, ends4, 0, 0);
			MSCompatUnicodeTableUtil.Level3 = new CodePointIndexer(starts5, ends5, 0, 0);
			MSCompatUnicodeTableUtil.CjkCHS = new CodePointIndexer(starts6, ends6, -1, -1);
			MSCompatUnicodeTableUtil.Cjk = new CodePointIndexer(starts7, ends7, -1, -1);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000259F File Offset: 0x0000079F
		public MSCompatUnicodeTableUtil()
		{
		}

		// Token: 0x04000E52 RID: 3666
		public const byte ResourceVersion = 3;

		// Token: 0x04000E53 RID: 3667
		public static readonly CodePointIndexer Ignorable;

		// Token: 0x04000E54 RID: 3668
		public static readonly CodePointIndexer Category;

		// Token: 0x04000E55 RID: 3669
		public static readonly CodePointIndexer Level1;

		// Token: 0x04000E56 RID: 3670
		public static readonly CodePointIndexer Level2;

		// Token: 0x04000E57 RID: 3671
		public static readonly CodePointIndexer Level3;

		// Token: 0x04000E58 RID: 3672
		public static readonly CodePointIndexer CjkCHS;

		// Token: 0x04000E59 RID: 3673
		public static readonly CodePointIndexer Cjk;
	}
}
