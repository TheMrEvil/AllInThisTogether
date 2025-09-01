using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Provides data for the <see cref="E:System.Data.OleDb.OleDbDataAdapter.RowUpdating" /> event.</summary>
	// Token: 0x02000171 RID: 369
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x060013CC RID: 5068 RVA: 0x00007EED File Offset: 0x000060ED
		protected override IDbCommand BaseCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbCommand" /> to execute when performing the <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbCommand" /> to execute when performing the <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</returns>
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x060013CE RID: 5070 RVA: 0x00007EED File Offset: 0x000060ED
		public new OleDbCommand Command
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbRowUpdatingEventArgs" /> class.</summary>
		/// <param name="dataRow">The <see cref="T:System.Data.DataRow" /> to <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> to execute during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</param>
		/// <param name="statementType">One of the <see cref="T:System.Data.StatementType" /> values that specifies the type of query executed.</param>
		/// <param name="tableMapping">The <see cref="T:System.Data.Common.DataTableMapping" /> sent through an <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</param>
		// Token: 0x060013CF RID: 5071 RVA: 0x0005AE29 File Offset: 0x00059029
		public OleDbRowUpdatingEventArgs(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(null, null, StatementType.Select, null)
		{
			throw ADP.OleDb();
		}
	}
}
