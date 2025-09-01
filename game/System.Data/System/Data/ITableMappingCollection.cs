using System;
using System.Collections;

namespace System.Data
{
	/// <summary>Contains a collection of TableMapping objects, and is implemented by the <see cref="T:System.Data.Common.DataTableMappingCollection" />, which is used in common by .NET Framework data providers.</summary>
	// Token: 0x02000109 RID: 265
	public interface ITableMappingCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Gets or sets the instance of <see cref="T:System.Data.ITableMapping" /> with the specified <see cref="P:System.Data.ITableMapping.SourceTable" /> name.</summary>
		/// <param name="index">The <see langword="SourceTable" /> name of the <see cref="T:System.Data.ITableMapping" />.</param>
		/// <returns>The instance of <see cref="T:System.Data.ITableMapping" /> with the specified <see langword="SourceTable" /> name.</returns>
		// Token: 0x170002B4 RID: 692
		object this[string index]
		{
			get;
			set;
		}

		/// <summary>Adds a table mapping to the collection.</summary>
		/// <param name="sourceTableName">The case-sensitive name of the source table.</param>
		/// <param name="dataSetTableName">The name of the <see cref="T:System.Data.DataSet" /> table.</param>
		/// <returns>A reference to the newly-mapped <see cref="T:System.Data.ITableMapping" /> object.</returns>
		// Token: 0x06000F7D RID: 3965
		ITableMapping Add(string sourceTableName, string dataSetTableName);

		/// <summary>Gets a value indicating whether the collection contains a table mapping with the specified source table name.</summary>
		/// <param name="sourceTableName">The case-sensitive name of the source table.</param>
		/// <returns>
		///   <see langword="true" /> if a table mapping with the specified source table name exists, otherwise <see langword="false" />.</returns>
		// Token: 0x06000F7E RID: 3966
		bool Contains(string sourceTableName);

		/// <summary>Gets the TableMapping object with the specified <see cref="T:System.Data.DataSet" /> table name.</summary>
		/// <param name="dataSetTableName">The name of the <see langword="DataSet" /> table within the collection.</param>
		/// <returns>The TableMapping object with the specified <see langword="DataSet" /> table name.</returns>
		// Token: 0x06000F7F RID: 3967
		ITableMapping GetByDataSetTable(string dataSetTableName);

		/// <summary>Gets the location of the <see cref="T:System.Data.ITableMapping" /> object within the collection.</summary>
		/// <param name="sourceTableName">The case-sensitive name of the source table.</param>
		/// <returns>The zero-based location of the <see cref="T:System.Data.ITableMapping" /> object within the collection.</returns>
		// Token: 0x06000F80 RID: 3968
		int IndexOf(string sourceTableName);

		/// <summary>Removes the <see cref="T:System.Data.ITableMapping" /> object with the specified <see cref="P:System.Data.ITableMapping.SourceTable" /> name from the collection.</summary>
		/// <param name="sourceTableName">The case-sensitive name of the <see langword="SourceTable" />.</param>
		// Token: 0x06000F81 RID: 3969
		void RemoveAt(string sourceTableName);
	}
}
