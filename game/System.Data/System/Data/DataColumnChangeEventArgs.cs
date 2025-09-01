using System;
using System.Runtime.CompilerServices;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="E:System.Data.DataTable.ColumnChanging" /> event.</summary>
	// Token: 0x020000B1 RID: 177
	public class DataColumnChangeEventArgs : EventArgs
	{
		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002CAD3 File Offset: 0x0002ACD3
		internal DataColumnChangeEventArgs(DataRow row)
		{
			this.Row = row;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataColumnChangeEventArgs" /> class.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> of the column with the changing value.</param>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> with the changing value.</param>
		/// <param name="value">The new value.</param>
		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002CAE2 File Offset: 0x0002ACE2
		public DataColumnChangeEventArgs(DataRow row, DataColumn column, object value)
		{
			this.Row = row;
			this._column = column;
			this.ProposedValue = value;
		}

		/// <summary>Gets the <see cref="T:System.Data.DataColumn" /> with a changing value.</summary>
		/// <returns>The <see cref="T:System.Data.DataColumn" /> with a changing value.</returns>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0002CAFF File Offset: 0x0002ACFF
		public DataColumn Column
		{
			get
			{
				return this._column;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataRow" /> of the column with a changing value.</summary>
		/// <returns>The <see cref="T:System.Data.DataRow" /> of the column with a changing value.</returns>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0002CB07 File Offset: 0x0002AD07
		public DataRow Row
		{
			[CompilerGenerated]
			get
			{
				return this.<Row>k__BackingField;
			}
		}

		/// <summary>Gets or sets the proposed new value for the column.</summary>
		/// <returns>The proposed value, of type <see cref="T:System.Object" />.</returns>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0002CB0F File Offset: 0x0002AD0F
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x0002CB17 File Offset: 0x0002AD17
		public object ProposedValue
		{
			[CompilerGenerated]
			get
			{
				return this.<ProposedValue>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ProposedValue>k__BackingField = value;
			}
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0002CB20 File Offset: 0x0002AD20
		internal void InitializeColumnChangeEvent(DataColumn column, object value)
		{
			this._column = column;
			this.ProposedValue = value;
		}

		// Token: 0x0400078E RID: 1934
		private DataColumn _column;

		// Token: 0x0400078F RID: 1935
		[CompilerGenerated]
		private readonly DataRow <Row>k__BackingField;

		// Token: 0x04000790 RID: 1936
		[CompilerGenerated]
		private object <ProposedValue>k__BackingField;
	}
}
