using System;

namespace System.Xml
{
	// Token: 0x0200002A RID: 42
	internal static class Bits
	{
		// Token: 0x0600015D RID: 349 RVA: 0x0000B0D4 File Offset: 0x000092D4
		public static int Count(uint num)
		{
			num = (num & Bits.MASK_0101010101010101) + (num >> 1 & Bits.MASK_0101010101010101);
			num = (num & Bits.MASK_0011001100110011) + (num >> 2 & Bits.MASK_0011001100110011);
			num = (num & Bits.MASK_0000111100001111) + (num >> 4 & Bits.MASK_0000111100001111);
			num = (num & Bits.MASK_0000000011111111) + (num >> 8 & Bits.MASK_0000000011111111);
			num = (num & Bits.MASK_1111111111111111) + (num >> 16);
			return (int)num;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000B13C File Offset: 0x0000933C
		public static bool ExactlyOne(uint num)
		{
			return num != 0U && (num & num - 1U) == 0U;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000B14B File Offset: 0x0000934B
		public static bool MoreThanOne(uint num)
		{
			return (num & num - 1U) > 0U;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000B155 File Offset: 0x00009355
		public static uint ClearLeast(uint num)
		{
			return num & num - 1U;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000B15C File Offset: 0x0000935C
		public static int LeastPosition(uint num)
		{
			if (num == 0U)
			{
				return 0;
			}
			return Bits.Count(num ^ num - 1U);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000B16D File Offset: 0x0000936D
		// Note: this type is marked as 'beforefieldinit'.
		static Bits()
		{
		}

		// Token: 0x040005CE RID: 1486
		private static readonly uint MASK_0101010101010101 = 1431655765U;

		// Token: 0x040005CF RID: 1487
		private static readonly uint MASK_0011001100110011 = 858993459U;

		// Token: 0x040005D0 RID: 1488
		private static readonly uint MASK_0000111100001111 = 252645135U;

		// Token: 0x040005D1 RID: 1489
		private static readonly uint MASK_0000000011111111 = 16711935U;

		// Token: 0x040005D2 RID: 1490
		private static readonly uint MASK_1111111111111111 = 65535U;
	}
}
