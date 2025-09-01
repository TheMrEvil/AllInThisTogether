using System;

namespace System.Data
{
	/// <summary>Describes the version of data in a <see cref="T:System.Data.DataRow" />.</summary>
	// Token: 0x020000DB RID: 219
	[Flags]
	public enum DataViewRowState
	{
		/// <summary>None.</summary>
		// Token: 0x0400084D RID: 2125
		None = 0,
		/// <summary>An unchanged row.</summary>
		// Token: 0x0400084E RID: 2126
		Unchanged = 2,
		/// <summary>A new row.</summary>
		// Token: 0x0400084F RID: 2127
		Added = 4,
		/// <summary>A deleted row.</summary>
		// Token: 0x04000850 RID: 2128
		Deleted = 8,
		/// <summary>A current version of original data that has been modified (see <see langword="ModifiedOriginal" />).</summary>
		// Token: 0x04000851 RID: 2129
		ModifiedCurrent = 16,
		/// <summary>The original version of the data that was modified. (Although the data has since been modified, it is available as <see langword="ModifiedCurrent" />).</summary>
		// Token: 0x04000852 RID: 2130
		ModifiedOriginal = 32,
		/// <summary>Original rows including unchanged and deleted rows.</summary>
		// Token: 0x04000853 RID: 2131
		OriginalRows = 42,
		/// <summary>Current rows including unchanged, new, and modified rows. By default, <see cref="T:System.Data.DataViewRowState" /> is set to CurrentRows.</summary>
		// Token: 0x04000854 RID: 2132
		CurrentRows = 22
	}
}
