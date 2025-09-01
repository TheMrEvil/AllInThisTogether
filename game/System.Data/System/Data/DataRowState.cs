using System;

namespace System.Data
{
	/// <summary>Gets the state of a <see cref="T:System.Data.DataRow" /> object.</summary>
	// Token: 0x020000C6 RID: 198
	[Flags]
	public enum DataRowState
	{
		/// <summary>The row has been created but is not part of any <see cref="T:System.Data.DataRowCollection" />. A <see cref="T:System.Data.DataRow" /> is in this state immediately after it has been created and before it is added to a collection, or if it has been removed from a collection.</summary>
		// Token: 0x040007E9 RID: 2025
		Detached = 1,
		/// <summary>The row has not changed since <see cref="M:System.Data.DataRow.AcceptChanges" /> was last called.</summary>
		// Token: 0x040007EA RID: 2026
		Unchanged = 2,
		/// <summary>The row has been added to a <see cref="T:System.Data.DataRowCollection" />, and <see cref="M:System.Data.DataRow.AcceptChanges" /> has not been called.</summary>
		// Token: 0x040007EB RID: 2027
		Added = 4,
		/// <summary>The row was deleted using the <see cref="M:System.Data.DataRow.Delete" /> method of the <see cref="T:System.Data.DataRow" />.</summary>
		// Token: 0x040007EC RID: 2028
		Deleted = 8,
		/// <summary>The row has been modified and <see cref="M:System.Data.DataRow.AcceptChanges" /> has not been called.</summary>
		// Token: 0x040007ED RID: 2029
		Modified = 16
	}
}
