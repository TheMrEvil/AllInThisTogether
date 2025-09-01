using System;

namespace System.Linq.Expressions
{
	// Token: 0x020002A3 RID: 675
	internal static class Utils
	{
		// Token: 0x06001419 RID: 5145 RVA: 0x0003DB0C File Offset: 0x0003BD0C
		public static ConstantExpression Constant(bool value)
		{
			if (!value)
			{
				return Utils.s_false;
			}
			return Utils.s_true;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0003DB1C File Offset: 0x0003BD1C
		public static ConstantExpression Constant(int value)
		{
			switch (value)
			{
			case -1:
				return Utils.s_m1;
			case 0:
				return Utils.s_0;
			case 1:
				return Utils.s_1;
			case 2:
				return Utils.s_2;
			case 3:
				return Utils.s_3;
			default:
				return Expression.Constant(value);
			}
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0003DB70 File Offset: 0x0003BD70
		// Note: this type is marked as 'beforefieldinit'.
		static Utils()
		{
		}

		// Token: 0x04000A6E RID: 2670
		public static readonly object BoxedFalse = false;

		// Token: 0x04000A6F RID: 2671
		public static readonly object BoxedTrue = true;

		// Token: 0x04000A70 RID: 2672
		public static readonly object BoxedIntM1 = -1;

		// Token: 0x04000A71 RID: 2673
		public static readonly object BoxedInt0 = 0;

		// Token: 0x04000A72 RID: 2674
		public static readonly object BoxedInt1 = 1;

		// Token: 0x04000A73 RID: 2675
		public static readonly object BoxedInt2 = 2;

		// Token: 0x04000A74 RID: 2676
		public static readonly object BoxedInt3 = 3;

		// Token: 0x04000A75 RID: 2677
		public static readonly object BoxedDefaultSByte = 0;

		// Token: 0x04000A76 RID: 2678
		public static readonly object BoxedDefaultChar = '\0';

		// Token: 0x04000A77 RID: 2679
		public static readonly object BoxedDefaultInt16 = 0;

		// Token: 0x04000A78 RID: 2680
		public static readonly object BoxedDefaultInt64 = 0L;

		// Token: 0x04000A79 RID: 2681
		public static readonly object BoxedDefaultByte = 0;

		// Token: 0x04000A7A RID: 2682
		public static readonly object BoxedDefaultUInt16 = 0;

		// Token: 0x04000A7B RID: 2683
		public static readonly object BoxedDefaultUInt32 = 0U;

		// Token: 0x04000A7C RID: 2684
		public static readonly object BoxedDefaultUInt64 = 0UL;

		// Token: 0x04000A7D RID: 2685
		public static readonly object BoxedDefaultSingle = 0f;

		// Token: 0x04000A7E RID: 2686
		public static readonly object BoxedDefaultDouble = 0.0;

		// Token: 0x04000A7F RID: 2687
		public static readonly object BoxedDefaultDecimal = 0m;

		// Token: 0x04000A80 RID: 2688
		public static readonly object BoxedDefaultDateTime = default(DateTime);

		// Token: 0x04000A81 RID: 2689
		private static readonly ConstantExpression s_true = Expression.Constant(Utils.BoxedTrue);

		// Token: 0x04000A82 RID: 2690
		private static readonly ConstantExpression s_false = Expression.Constant(Utils.BoxedFalse);

		// Token: 0x04000A83 RID: 2691
		private static readonly ConstantExpression s_m1 = Expression.Constant(Utils.BoxedIntM1);

		// Token: 0x04000A84 RID: 2692
		private static readonly ConstantExpression s_0 = Expression.Constant(Utils.BoxedInt0);

		// Token: 0x04000A85 RID: 2693
		private static readonly ConstantExpression s_1 = Expression.Constant(Utils.BoxedInt1);

		// Token: 0x04000A86 RID: 2694
		private static readonly ConstantExpression s_2 = Expression.Constant(Utils.BoxedInt2);

		// Token: 0x04000A87 RID: 2695
		private static readonly ConstantExpression s_3 = Expression.Constant(Utils.BoxedInt3);

		// Token: 0x04000A88 RID: 2696
		public static readonly DefaultExpression Empty = Expression.Empty();

		// Token: 0x04000A89 RID: 2697
		public static readonly ConstantExpression Null = Expression.Constant(null);
	}
}
