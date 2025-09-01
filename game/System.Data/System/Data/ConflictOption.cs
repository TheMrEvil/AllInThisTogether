using System;

namespace System.Data
{
	/// <summary>Specifies how conflicting changes to the data source will be detected and resolved.</summary>
	// Token: 0x020000A7 RID: 167
	public enum ConflictOption
	{
		/// <summary>Update and delete statements will include all searchable columns from the table in the WHERE clause. This is equivalent to specifying <see langword="CompareAllValuesUpdate" /> | <see langword="CompareAllValuesDelete" />.</summary>
		// Token: 0x04000773 RID: 1907
		CompareAllSearchableValues = 1,
		/// <summary>If any Timestamp columns exist in the table, they are used in the WHERE clause for all generated update statements. This is equivalent to specifying <see langword="CompareRowVersionUpdate" /> | <see langword="CompareRowVersionDelete" />.</summary>
		// Token: 0x04000774 RID: 1908
		CompareRowVersion,
		/// <summary>All update and delete statements include only <see cref="P:System.Data.DataTable.PrimaryKey" /> columns in the WHERE clause. If no <see cref="P:System.Data.DataTable.PrimaryKey" /> is defined, all searchable columns are included in the WHERE clause. This is equivalent to <see langword="OverwriteChangesUpdate" /> | <see langword="OverwriteChangesDelete" />.</summary>
		// Token: 0x04000775 RID: 1909
		OverwriteChanges
	}
}
