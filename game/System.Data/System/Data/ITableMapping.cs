using System;

namespace System.Data
{
	/// <summary>Associates a source table with a table in a <see cref="T:System.Data.DataSet" />, and is implemented by the <see cref="T:System.Data.Common.DataTableMapping" /> class, which is used in common by .NET Framework data providers.</summary>
	// Token: 0x02000108 RID: 264
	public interface ITableMapping
	{
		/// <summary>Gets the derived <see cref="T:System.Data.Common.DataColumnMappingCollection" /> for the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A collection of data column mappings.</returns>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000F76 RID: 3958
		IColumnMappingCollection ColumnMappings { get; }

		/// <summary>Gets or sets the case-insensitive name of the table within the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The case-insensitive name of the table within the <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000F77 RID: 3959
		// (set) Token: 0x06000F78 RID: 3960
		string DataSetTable { get; set; }

		/// <summary>Gets or sets the case-sensitive name of the source table.</summary>
		/// <returns>The case-sensitive name of the source table.</returns>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000F79 RID: 3961
		// (set) Token: 0x06000F7A RID: 3962
		string SourceTable { get; set; }
	}
}
