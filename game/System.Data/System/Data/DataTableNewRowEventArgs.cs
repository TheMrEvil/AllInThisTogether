using System;
using System.Runtime.CompilerServices;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="M:System.Data.DataTable.NewRow" /> method.</summary>
	// Token: 0x020000CF RID: 207
	public sealed class DataTableNewRowEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Data.DataTableNewRowEventArgs" />.</summary>
		/// <param name="dataRow">The <see cref="T:System.Data.DataRow" /> being added.</param>
		// Token: 0x06000C8D RID: 3213 RVA: 0x0003374B File Offset: 0x0003194B
		public DataTableNewRowEventArgs(DataRow dataRow)
		{
			this.Row = dataRow;
		}

		/// <summary>Gets the row that is being added.</summary>
		/// <returns>The <see cref="T:System.Data.DataRow" /> that is being added.</returns>
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0003375A File Offset: 0x0003195A
		public DataRow Row
		{
			[CompilerGenerated]
			get
			{
				return this.<Row>k__BackingField;
			}
		}

		// Token: 0x0400080A RID: 2058
		[CompilerGenerated]
		private readonly DataRow <Row>k__BackingField;
	}
}
