using System;
using System.Runtime.CompilerServices;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="M:System.Data.DataTable.Clear" /> method.</summary>
	// Token: 0x020000CC RID: 204
	public sealed class DataTableClearEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTableClearEventArgs" /> class.</summary>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> whose rows are being cleared.</param>
		// Token: 0x06000C55 RID: 3157 RVA: 0x00032816 File Offset: 0x00030A16
		public DataTableClearEventArgs(DataTable dataTable)
		{
			this.Table = dataTable;
		}

		/// <summary>Gets the table whose rows are being cleared.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> whose rows are being cleared.</returns>
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00032825 File Offset: 0x00030A25
		public DataTable Table
		{
			[CompilerGenerated]
			get
			{
				return this.<Table>k__BackingField;
			}
		}

		/// <summary>Gets the table name whose rows are being cleared.</summary>
		/// <returns>A <see cref="T:System.String" /> indicating the table name.</returns>
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0003282D File Offset: 0x00030A2D
		public string TableName
		{
			get
			{
				return this.Table.TableName;
			}
		}

		/// <summary>Gets the namespace of the table whose rows are being cleared.</summary>
		/// <returns>A <see cref="T:System.String" /> indicating the namespace name.</returns>
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x0003283A File Offset: 0x00030A3A
		public string TableNamespace
		{
			get
			{
				return this.Table.Namespace;
			}
		}

		// Token: 0x04000801 RID: 2049
		[CompilerGenerated]
		private readonly DataTable <Table>k__BackingField;
	}
}
