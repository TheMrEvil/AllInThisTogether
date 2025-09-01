using System;
using System.Runtime.CompilerServices;

namespace System.Data
{
	/// <summary>Provides additional information for the <see cref="E:System.Data.SqlClient.SqlCommand.StatementCompleted" /> event.</summary>
	// Token: 0x02000133 RID: 307
	public sealed class StatementCompletedEventArgs : EventArgs
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Data.StatementCompletedEventArgs" /> class.</summary>
		/// <param name="recordCount">Indicates the number of rows affected by the statement that caused the <see cref="E:System.Data.SqlClient.SqlCommand.StatementCompleted" /> event to occur.</param>
		// Token: 0x060010A9 RID: 4265 RVA: 0x00045C00 File Offset: 0x00043E00
		public StatementCompletedEventArgs(int recordCount)
		{
			this.RecordCount = recordCount;
		}

		/// <summary>Indicates the number of rows affected by the statement that caused the <see cref="E:System.Data.SqlClient.SqlCommand.StatementCompleted" /> event to occur.</summary>
		/// <returns>The number of rows affected.</returns>
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x00045C0F File Offset: 0x00043E0F
		public int RecordCount
		{
			[CompilerGenerated]
			get
			{
				return this.<RecordCount>k__BackingField;
			}
		}

		// Token: 0x04000A44 RID: 2628
		[CompilerGenerated]
		private readonly int <RecordCount>k__BackingField;
	}
}
