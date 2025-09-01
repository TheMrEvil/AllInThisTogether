using System;
using System.Runtime.CompilerServices;

namespace System.Data
{
	/// <summary>Occurs when a target and source <see langword="DataRow" /> have the same primary key value, and the <see cref="P:System.Data.DataSet.EnforceConstraints" /> property is set to true.</summary>
	// Token: 0x0200010E RID: 270
	public class MergeFailedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of a <see cref="T:System.Data.MergeFailedEventArgs" /> class with the <see cref="T:System.Data.DataTable" /> and a description of the merge conflict.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> object.</param>
		/// <param name="conflict">A description of the merge conflict.</param>
		// Token: 0x06000F82 RID: 3970 RVA: 0x0003EC03 File Offset: 0x0003CE03
		public MergeFailedEventArgs(DataTable table, string conflict)
		{
			this.Table = table;
			this.Conflict = conflict;
		}

		/// <summary>Returns the <see cref="T:System.Data.DataTable" /> object.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> object.</returns>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0003EC19 File Offset: 0x0003CE19
		public DataTable Table
		{
			[CompilerGenerated]
			get
			{
				return this.<Table>k__BackingField;
			}
		}

		/// <summary>Returns a description of the merge conflict.</summary>
		/// <returns>A description of the merge conflict.</returns>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x0003EC21 File Offset: 0x0003CE21
		public string Conflict
		{
			[CompilerGenerated]
			get
			{
				return this.<Conflict>k__BackingField;
			}
		}

		// Token: 0x04000980 RID: 2432
		[CompilerGenerated]
		private readonly DataTable <Table>k__BackingField;

		// Token: 0x04000981 RID: 2433
		[CompilerGenerated]
		private readonly string <Conflict>k__BackingField;
	}
}
