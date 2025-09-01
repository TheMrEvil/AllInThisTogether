using System;

namespace System.Data
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.DataTable.RowChanging" />, <see cref="E:System.Data.DataTable.RowChanged" />, <see cref="E:System.Data.DataTable.RowDeleting" />, and <see cref="E:System.Data.DataTable.RowDeleted" /> events of a <see cref="T:System.Data.DataTable" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data.</param>
	// Token: 0x020000C1 RID: 193
	// (Invoke) Token: 0x06000C00 RID: 3072
	public delegate void DataRowChangeEventHandler(object sender, DataRowChangeEventArgs e);
}
