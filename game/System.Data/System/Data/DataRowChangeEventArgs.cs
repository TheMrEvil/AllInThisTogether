using System;
using System.Runtime.CompilerServices;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="E:System.Data.DataTable.RowChanged" />, <see cref="E:System.Data.DataTable.RowChanging" />, <see cref="M:System.Data.DataTable.OnRowDeleting(System.Data.DataRowChangeEventArgs)" />, and <see cref="M:System.Data.DataTable.OnRowDeleted(System.Data.DataRowChangeEventArgs)" /> events.</summary>
	// Token: 0x020000C0 RID: 192
	public class DataRowChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRowChangeEventArgs" /> class.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> upon which an action is occuring.</param>
		/// <param name="action">One of the <see cref="T:System.Data.DataRowAction" /> values.</param>
		// Token: 0x06000BFC RID: 3068 RVA: 0x00031FD6 File Offset: 0x000301D6
		public DataRowChangeEventArgs(DataRow row, DataRowAction action)
		{
			this.Row = row;
			this.Action = action;
		}

		/// <summary>Gets the row upon which an action has occurred.</summary>
		/// <returns>The <see cref="T:System.Data.DataRow" /> upon which an action has occurred.</returns>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x00031FEC File Offset: 0x000301EC
		public DataRow Row
		{
			[CompilerGenerated]
			get
			{
				return this.<Row>k__BackingField;
			}
		}

		/// <summary>Gets the action that has occurred on a <see cref="T:System.Data.DataRow" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowAction" /> values.</returns>
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x00031FF4 File Offset: 0x000301F4
		public DataRowAction Action
		{
			[CompilerGenerated]
			get
			{
				return this.<Action>k__BackingField;
			}
		}

		// Token: 0x040007E3 RID: 2019
		[CompilerGenerated]
		private readonly DataRow <Row>k__BackingField;

		// Token: 0x040007E4 RID: 2020
		[CompilerGenerated]
		private readonly DataRowAction <Action>k__BackingField;
	}
}
