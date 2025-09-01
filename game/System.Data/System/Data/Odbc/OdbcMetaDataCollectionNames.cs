using System;

namespace System.Data.Odbc
{
	/// <summary>Provides a list of constants for use with the GetSchema method to retrieve metadata collections.</summary>
	// Token: 0x020002EF RID: 751
	public static class OdbcMetaDataCollectionNames
	{
		// Token: 0x06002130 RID: 8496 RVA: 0x0009AB58 File Offset: 0x00098D58
		// Note: this type is marked as 'beforefieldinit'.
		static OdbcMetaDataCollectionNames()
		{
		}

		/// <summary>A constant for use with the GetSchema method that represents the Columns collection.</summary>
		// Token: 0x040017C0 RID: 6080
		public static readonly string Columns = "Columns";

		/// <summary>A constant for use with the GetSchema method that represents the Indexes collection.</summary>
		// Token: 0x040017C1 RID: 6081
		public static readonly string Indexes = "Indexes";

		/// <summary>A constant for use with the GetSchema method that represents the Procedures collection.</summary>
		// Token: 0x040017C2 RID: 6082
		public static readonly string Procedures = "Procedures";

		/// <summary>A constant for use with the GetSchema method that represents the ProcedureColumns collection.</summary>
		// Token: 0x040017C3 RID: 6083
		public static readonly string ProcedureColumns = "ProcedureColumns";

		/// <summary>A constant for use with the GetSchema method that represents the ProcedureParameters collection.</summary>
		// Token: 0x040017C4 RID: 6084
		public static readonly string ProcedureParameters = "ProcedureParameters";

		/// <summary>A constant for use with the GetSchema method that represents the Tables collection.</summary>
		// Token: 0x040017C5 RID: 6085
		public static readonly string Tables = "Tables";

		/// <summary>A constant for use with the GetSchema method that represents the Views collection.</summary>
		// Token: 0x040017C6 RID: 6086
		public static readonly string Views = "Views";
	}
}
