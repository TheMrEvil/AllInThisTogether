using System;

namespace System.Data
{
	/// <summary>Specifies the action to take with regard to the current and remaining rows during an <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</summary>
	// Token: 0x02000149 RID: 329
	public enum UpdateStatus
	{
		/// <summary>The <see cref="T:System.Data.Common.DataAdapter" /> is to continue proccessing rows.</summary>
		// Token: 0x04000B64 RID: 2916
		Continue,
		/// <summary>The event handler reports that the update should be treated as an error.</summary>
		// Token: 0x04000B65 RID: 2917
		ErrorsOccurred,
		/// <summary>The current row is not to be updated.</summary>
		// Token: 0x04000B66 RID: 2918
		SkipCurrentRow,
		/// <summary>The current row and all remaining rows are not to be updated.</summary>
		// Token: 0x04000B67 RID: 2919
		SkipAllRemainingRows
	}
}
