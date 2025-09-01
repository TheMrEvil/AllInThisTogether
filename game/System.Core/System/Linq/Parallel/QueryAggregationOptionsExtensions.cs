using System;

namespace System.Linq.Parallel
{
	// Token: 0x020000F9 RID: 249
	internal static class QueryAggregationOptionsExtensions
	{
		// Token: 0x0600087F RID: 2175 RVA: 0x0001D178 File Offset: 0x0001B378
		public static bool IsValidQueryAggregationOption(this QueryAggregationOptions value)
		{
			return value == QueryAggregationOptions.None || value == QueryAggregationOptions.Associative || value == QueryAggregationOptions.Commutative || value == QueryAggregationOptions.AssociativeCommutative;
		}
	}
}
