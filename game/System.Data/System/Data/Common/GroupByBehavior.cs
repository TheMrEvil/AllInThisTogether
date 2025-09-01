using System;

namespace System.Data.Common
{
	/// <summary>Specifies the relationship between the columns in a GROUP BY clause and the non-aggregated columns in the select-list of a SELECT statement.</summary>
	// Token: 0x020003A2 RID: 930
	public enum GroupByBehavior
	{
		/// <summary>The support for the GROUP BY clause is unknown.</summary>
		// Token: 0x04001B91 RID: 7057
		Unknown,
		/// <summary>The GROUP BY clause is not supported.</summary>
		// Token: 0x04001B92 RID: 7058
		NotSupported,
		/// <summary>There is no relationship between the columns in the GROUP BY clause and the nonaggregated columns in the SELECT list. You may group by any column.</summary>
		// Token: 0x04001B93 RID: 7059
		Unrelated,
		/// <summary>The GROUP BY clause must contain all nonaggregated columns in the select list, and can contain other columns not in the select list.</summary>
		// Token: 0x04001B94 RID: 7060
		MustContainAll,
		/// <summary>The GROUP BY clause must contain all nonaggregated columns in the select list, and must not contain other columns not in the select list.</summary>
		// Token: 0x04001B95 RID: 7061
		ExactMatch
	}
}
