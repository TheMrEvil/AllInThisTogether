using System;

namespace System.Data.SqlClient
{
	/// <summary>Specifies how rows of data are sorted.</summary>
	// Token: 0x0200018B RID: 395
	public enum SortOrder
	{
		/// <summary>The default. No sort order is specified.</summary>
		// Token: 0x04000CAA RID: 3242
		Unspecified = -1,
		/// <summary>Rows are sorted in ascending order.</summary>
		// Token: 0x04000CAB RID: 3243
		Ascending,
		/// <summary>Rows are sorted in descending order.</summary>
		// Token: 0x04000CAC RID: 3244
		Descending
	}
}
