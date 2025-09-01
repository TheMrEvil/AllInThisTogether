using System;

namespace System.Data.SqlClient
{
	/// <summary>Provides a list of constants for use with the GetSchema method to retrieve metadata collections.</summary>
	// Token: 0x020001B0 RID: 432
	public static class SqlClientMetaDataCollectionNames
	{
		// Token: 0x0600153D RID: 5437 RVA: 0x00060C5C File Offset: 0x0005EE5C
		// Note: this type is marked as 'beforefieldinit'.
		static SqlClientMetaDataCollectionNames()
		{
		}

		/// <summary>A constant for use with the GetSchema method that represents the Columns collection.</summary>
		// Token: 0x04000D83 RID: 3459
		public static readonly string Columns = "Columns";

		/// <summary>A constant for use with the GetSchema method that represents the Databases collection.</summary>
		// Token: 0x04000D84 RID: 3460
		public static readonly string Databases = "Databases";

		/// <summary>A constant for use with the GetSchema method that represents the ForeignKeys collection.</summary>
		// Token: 0x04000D85 RID: 3461
		public static readonly string ForeignKeys = "ForeignKeys";

		/// <summary>A constant for use with the GetSchema method that represents the IndexColumns collection.</summary>
		// Token: 0x04000D86 RID: 3462
		public static readonly string IndexColumns = "IndexColumns";

		/// <summary>A constant for use with the GetSchema method that represents the Indexes collection.</summary>
		// Token: 0x04000D87 RID: 3463
		public static readonly string Indexes = "Indexes";

		/// <summary>A constant for use with the GetSchema method that represents the Parameters collection.</summary>
		// Token: 0x04000D88 RID: 3464
		public static readonly string Parameters = "Parameters";

		/// <summary>A constant for use with the GetSchema method that represents the ProcedureColumns collection.</summary>
		// Token: 0x04000D89 RID: 3465
		public static readonly string ProcedureColumns = "ProcedureColumns";

		/// <summary>A constant for use with the GetSchema method that represents the Procedures collection.</summary>
		// Token: 0x04000D8A RID: 3466
		public static readonly string Procedures = "Procedures";

		/// <summary>A constant for use with the GetSchema method that represents the Tables collection.</summary>
		// Token: 0x04000D8B RID: 3467
		public static readonly string Tables = "Tables";

		/// <summary>A constant for use with the GetSchema method that represents the UserDefinedTypes collection.</summary>
		// Token: 0x04000D8C RID: 3468
		public static readonly string UserDefinedTypes = "UserDefinedTypes";

		/// <summary>A constant for use with the GetSchema method that represents the Users collection.</summary>
		// Token: 0x04000D8D RID: 3469
		public static readonly string Users = "Users";

		/// <summary>A constant for use with the GetSchema method that represents the ViewColumns collection.</summary>
		// Token: 0x04000D8E RID: 3470
		public static readonly string ViewColumns = "ViewColumns";

		/// <summary>A constant for use with the GetSchema method that represents the Views collection.</summary>
		// Token: 0x04000D8F RID: 3471
		public static readonly string Views = "Views";
	}
}
