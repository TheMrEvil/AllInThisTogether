using System;

namespace System.Data
{
	/// <summary>Indicates the action that occurs when a <see cref="T:System.Data.ForeignKeyConstraint" /> is enforced.</summary>
	// Token: 0x02000122 RID: 290
	public enum Rule
	{
		/// <summary>No action taken on related rows.</summary>
		// Token: 0x040009DD RID: 2525
		None,
		/// <summary>Delete or update related rows. This is the default.</summary>
		// Token: 0x040009DE RID: 2526
		Cascade,
		/// <summary>Set values in related rows to <see langword="DBNull" />.</summary>
		// Token: 0x040009DF RID: 2527
		SetNull,
		/// <summary>Set values in related rows to the value contained in the <see cref="P:System.Data.DataColumn.DefaultValue" /> property.</summary>
		// Token: 0x040009E0 RID: 2528
		SetDefault
	}
}
