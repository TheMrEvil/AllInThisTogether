using System;

namespace System.Data
{
	/// <summary>Determines the action that occurs when a mapping is missing from a source table or a source column.</summary>
	// Token: 0x02000111 RID: 273
	public enum MissingMappingAction
	{
		/// <summary>The source column or source table is created and added to the <see cref="T:System.Data.DataSet" /> using its original name.</summary>
		// Token: 0x04000989 RID: 2441
		Passthrough = 1,
		/// <summary>The column or table not having a mapping is ignored. Returns <see langword="null" />.</summary>
		// Token: 0x0400098A RID: 2442
		Ignore,
		/// <summary>An <see cref="T:System.InvalidOperationException" /> is generated if the specified column mapping is missing.</summary>
		// Token: 0x0400098B RID: 2443
		Error
	}
}
