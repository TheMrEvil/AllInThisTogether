using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Linq
{
	// Token: 0x020000EC RID: 236
	internal static class Utilities
	{
		// Token: 0x06000846 RID: 2118 RVA: 0x0001CA68 File Offset: 0x0001AC68
		public static bool AreEqualityComparersEqual<TSource>(IEqualityComparer<TSource> left, IEqualityComparer<TSource> right)
		{
			if (left == right)
			{
				return true;
			}
			EqualityComparer<TSource> @default = EqualityComparer<TSource>.Default;
			if (left == null)
			{
				return right == @default || right.Equals(@default);
			}
			if (right == null)
			{
				return left == @default || left.Equals(@default);
			}
			return left.Equals(right);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001CAAA File Offset: 0x0001ACAA
		public static Func<TSource, bool> CombinePredicates<TSource>(Func<TSource, bool> predicate1, Func<TSource, bool> predicate2)
		{
			return (TSource x) => predicate1(x) && predicate2(x);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001CACA File Offset: 0x0001ACCA
		public static Func<TSource, TResult> CombineSelectors<TSource, TMiddle, TResult>(Func<TSource, TMiddle> selector1, Func<TMiddle, TResult> selector2)
		{
			return (TSource x) => selector2(selector1(x));
		}

		// Token: 0x020000ED RID: 237
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0<TSource>
		{
			// Token: 0x06000849 RID: 2121 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x0600084A RID: 2122 RVA: 0x0001CAEA File Offset: 0x0001ACEA
			internal bool <CombinePredicates>b__0(TSource x)
			{
				return this.predicate1(x) && this.predicate2(x);
			}

			// Token: 0x040005C4 RID: 1476
			public Func<TSource, bool> predicate1;

			// Token: 0x040005C5 RID: 1477
			public Func<TSource, bool> predicate2;
		}

		// Token: 0x020000EE RID: 238
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0<TSource, TMiddle, TResult>
		{
			// Token: 0x0600084B RID: 2123 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x0600084C RID: 2124 RVA: 0x0001CB08 File Offset: 0x0001AD08
			internal TResult <CombineSelectors>b__0(TSource x)
			{
				return this.selector2(this.selector1(x));
			}

			// Token: 0x040005C6 RID: 1478
			public Func<TMiddle, TResult> selector2;

			// Token: 0x040005C7 RID: 1479
			public Func<TSource, TMiddle> selector1;
		}
	}
}
