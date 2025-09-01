using System;

namespace System.Data
{
	/// <summary>Represents a set of command-related properties that are used to fill the <see cref="T:System.Data.DataSet" /> and update a data source, and is implemented by .NET Framework data providers that access relational databases.</summary>
	// Token: 0x02000105 RID: 261
	public interface IDbDataAdapter : IDataAdapter
	{
		/// <summary>Gets or sets an SQL statement used to select records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to select records from data source for placement in the data set.</returns>
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000F64 RID: 3940
		// (set) Token: 0x06000F65 RID: 3941
		IDbCommand SelectCommand { get; set; }

		/// <summary>Gets or sets an SQL statement used to insert new records into the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to insert records in the data source for new rows in the data set.</returns>
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000F66 RID: 3942
		// (set) Token: 0x06000F67 RID: 3943
		IDbCommand InsertCommand { get; set; }

		/// <summary>Gets or sets an SQL statement used to update records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to update records in the data source for modified rows in the data set.</returns>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000F68 RID: 3944
		// (set) Token: 0x06000F69 RID: 3945
		IDbCommand UpdateCommand { get; set; }

		/// <summary>Gets or sets an SQL statement for deleting records from the data set.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to delete records in the data source for deleted rows in the data set.</returns>
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000F6A RID: 3946
		// (set) Token: 0x06000F6B RID: 3947
		IDbCommand DeleteCommand { get; set; }
	}
}
