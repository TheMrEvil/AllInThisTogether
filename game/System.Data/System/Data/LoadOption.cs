using System;

namespace System.Data
{
	/// <summary>Controls how the values from the data source will be applied to existing rows when using the <see cref="Overload:System.Data.DataTable.Load" /> or <see cref="Overload:System.Data.DataSet.Load" /> method.</summary>
	// Token: 0x0200010C RID: 268
	public enum LoadOption
	{
		/// <summary>The incoming values for this row will be written to both the current value and the original value versions of the data for each column.</summary>
		// Token: 0x04000978 RID: 2424
		OverwriteChanges = 1,
		/// <summary>The incoming values for this row will be written to the original value version of each column. The current version of the data in each column will not be changed.  This is the default.</summary>
		// Token: 0x04000979 RID: 2425
		PreserveChanges,
		/// <summary>The incoming values for this row will be written to the current version of each column. The original version of each column's data will not be changed.</summary>
		// Token: 0x0400097A RID: 2426
		Upsert
	}
}
