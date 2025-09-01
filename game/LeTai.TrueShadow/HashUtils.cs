using System;

namespace LeTai
{
	// Token: 0x02000003 RID: 3
	public static class HashUtils
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000229E File Offset: 0x0000049E
		public static int CombineHashCodes(int h1, int h2)
		{
			return (h1 << 5) + h1 ^ h2;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022A7 File Offset: 0x000004A7
		public static int CombineHashCodes(int h1, int h2, int h3)
		{
			return HashUtils.CombineHashCodes(HashUtils.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022B6 File Offset: 0x000004B6
		public static int CombineHashCodes(int h1, int h2, int h3, int h4)
		{
			return HashUtils.CombineHashCodes(HashUtils.CombineHashCodes(h1, h2), HashUtils.CombineHashCodes(h3, h4));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022CB File Offset: 0x000004CB
		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
		{
			return HashUtils.CombineHashCodes(HashUtils.CombineHashCodes(h1, h2, h3, h4), h5);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022DD File Offset: 0x000004DD
		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
		{
			return HashUtils.CombineHashCodes(HashUtils.CombineHashCodes(h1, h2, h3, h4), HashUtils.CombineHashCodes(h5, h6));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022F6 File Offset: 0x000004F6
		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
		{
			return HashUtils.CombineHashCodes(HashUtils.CombineHashCodes(h1, h2, h3, h4), HashUtils.CombineHashCodes(h5, h6, h7));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002311 File Offset: 0x00000511
		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
		{
			return HashUtils.CombineHashCodes(HashUtils.CombineHashCodes(h1, h2, h3, h4), HashUtils.CombineHashCodes(h5, h6, h7, h8));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000232E File Offset: 0x0000052E
		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9)
		{
			return HashUtils.CombineHashCodes(HashUtils.CombineHashCodes(h1, h2, h3, h4, h5, h6, h7, h8), h9);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002348 File Offset: 0x00000548
		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10)
		{
			return HashUtils.CombineHashCodes(HashUtils.CombineHashCodes(h1, h2, h3, h4, h5, h6, h7, h8), HashUtils.CombineHashCodes(h9, h10));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002369 File Offset: 0x00000569
		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11)
		{
			return HashUtils.CombineHashCodes(HashUtils.CombineHashCodes(h1, h2, h3, h4, h5, h6, h7, h8), HashUtils.CombineHashCodes(h9, h10, h11));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000238C File Offset: 0x0000058C
		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12)
		{
			return HashUtils.CombineHashCodes(HashUtils.CombineHashCodes(h1, h2, h3, h4, h5, h6, h7, h8), HashUtils.CombineHashCodes(h9, h10, h11, h12));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023B1 File Offset: 0x000005B1
		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13)
		{
			return HashUtils.CombineHashCodes(HashUtils.CombineHashCodes(h1, h2, h3, h4, h5, h6, h7, h8), HashUtils.CombineHashCodes(h9, h10, h11, h12, h13));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023D8 File Offset: 0x000005D8
		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14)
		{
			return HashUtils.CombineHashCodes(HashUtils.CombineHashCodes(h1, h2, h3, h4, h5, h6, h7, h8), HashUtils.CombineHashCodes(h9, h10, h11, h12, h13, h14));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002401 File Offset: 0x00000601
		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15)
		{
			return HashUtils.CombineHashCodes(HashUtils.CombineHashCodes(h1, h2, h3, h4, h5, h6, h7, h8), HashUtils.CombineHashCodes(h9, h10, h11, h12, h13, h14, h15));
		}
	}
}
