using System;
using System.Collections.Generic;
using System.Linq.Parallel;
using System.Runtime.CompilerServices;

namespace System.Linq
{
	// Token: 0x02000079 RID: 121
	internal static class AggregationMinMaxHelpers<T>
	{
		// Token: 0x06000288 RID: 648 RVA: 0x00007F04 File Offset: 0x00006104
		private static T Reduce(IEnumerable<T> source, int sign)
		{
			Func<Pair<bool, T>, T, Pair<bool, T>> intermediateReduce = AggregationMinMaxHelpers<T>.MakeIntermediateReduceFunction(sign);
			Func<Pair<bool, T>, Pair<bool, T>, Pair<bool, T>> finalReduce = AggregationMinMaxHelpers<T>.MakeFinalReduceFunction(sign);
			Func<Pair<bool, T>, T> resultSelector = AggregationMinMaxHelpers<T>.MakeResultSelectorFunction();
			return new AssociativeAggregationOperator<T, Pair<bool, T>, T>(source, new Pair<bool, T>(false, default(T)), null, true, intermediateReduce, finalReduce, resultSelector, default(T) != null, QueryAggregationOptions.AssociativeCommutative).Aggregate();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00007F56 File Offset: 0x00006156
		internal static T ReduceMin(IEnumerable<T> source)
		{
			return AggregationMinMaxHelpers<T>.Reduce(source, -1);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00007F5F File Offset: 0x0000615F
		internal static T ReduceMax(IEnumerable<T> source)
		{
			return AggregationMinMaxHelpers<T>.Reduce(source, 1);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00007F68 File Offset: 0x00006168
		private static Func<Pair<bool, T>, T, Pair<bool, T>> MakeIntermediateReduceFunction(int sign)
		{
			Comparer<T> comparer = Util.GetDefaultComparer<T>();
			return delegate(Pair<bool, T> accumulator, T element)
			{
				if ((default(T) != null || element != null) && (!accumulator.First || Util.Sign(comparer.Compare(element, accumulator.Second)) == sign))
				{
					return new Pair<bool, T>(true, element);
				}
				return accumulator;
			};
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00007F8C File Offset: 0x0000618C
		private static Func<Pair<bool, T>, Pair<bool, T>, Pair<bool, T>> MakeFinalReduceFunction(int sign)
		{
			Comparer<T> comparer = Util.GetDefaultComparer<T>();
			return delegate(Pair<bool, T> accumulator, Pair<bool, T> element)
			{
				if (element.First && (!accumulator.First || Util.Sign(comparer.Compare(element.Second, accumulator.Second)) == sign))
				{
					return new Pair<bool, T>(true, element.Second);
				}
				return accumulator;
			};
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00007FB0 File Offset: 0x000061B0
		private static Func<Pair<bool, T>, T> MakeResultSelectorFunction()
		{
			return (Pair<bool, T> accumulator) => accumulator.Second;
		}

		// Token: 0x0200007A RID: 122
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x0600028E RID: 654 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x0600028F RID: 655 RVA: 0x00007FD4 File Offset: 0x000061D4
			internal Pair<bool, T> <MakeIntermediateReduceFunction>b__0(Pair<bool, T> accumulator, T element)
			{
				if ((default(T) != null || element != null) && (!accumulator.First || Util.Sign(this.comparer.Compare(element, accumulator.Second)) == this.sign))
				{
					return new Pair<bool, T>(true, element);
				}
				return accumulator;
			}

			// Token: 0x040003AC RID: 940
			public Comparer<T> comparer;

			// Token: 0x040003AD RID: 941
			public int sign;
		}

		// Token: 0x0200007B RID: 123
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x06000290 RID: 656 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x06000291 RID: 657 RVA: 0x0000802C File Offset: 0x0000622C
			internal Pair<bool, T> <MakeFinalReduceFunction>b__0(Pair<bool, T> accumulator, Pair<bool, T> element)
			{
				if (element.First && (!accumulator.First || Util.Sign(this.comparer.Compare(element.Second, accumulator.Second)) == this.sign))
				{
					return new Pair<bool, T>(true, element.Second);
				}
				return accumulator;
			}

			// Token: 0x040003AE RID: 942
			public Comparer<T> comparer;

			// Token: 0x040003AF RID: 943
			public int sign;
		}

		// Token: 0x0200007C RID: 124
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000292 RID: 658 RVA: 0x00008080 File Offset: 0x00006280
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000293 RID: 659 RVA: 0x00002162 File Offset: 0x00000362
			public <>c()
			{
			}

			// Token: 0x06000294 RID: 660 RVA: 0x0000808C File Offset: 0x0000628C
			internal T <MakeResultSelectorFunction>b__5_0(Pair<bool, T> accumulator)
			{
				return accumulator.Second;
			}

			// Token: 0x040003B0 RID: 944
			public static readonly AggregationMinMaxHelpers<T>.<>c <>9 = new AggregationMinMaxHelpers<T>.<>c();

			// Token: 0x040003B1 RID: 945
			public static Func<Pair<bool, T>, T> <>9__5_0;
		}
	}
}
