using System;

namespace System.Data.Odbc
{
	/// <summary>Provides static values that are used for the column names in the <see cref="T:System.Data.Odbc.OdbcMetaDataCollectionNames" /> objects contained in the <see cref="T:System.Data.DataTable" />. The <see cref="T:System.Data.DataTable" /> is created by the GetSchema method.</summary>
	// Token: 0x020002F0 RID: 752
	public static class OdbcMetaDataColumnNames
	{
		// Token: 0x06002131 RID: 8497 RVA: 0x0009ABAB File Offset: 0x00098DAB
		// Note: this type is marked as 'beforefieldinit'.
		static OdbcMetaDataColumnNames()
		{
		}

		/// <summary>Used by the GetSchema method to create the BooleanFalseLiteral column.</summary>
		// Token: 0x040017C7 RID: 6087
		public static readonly string BooleanFalseLiteral = "BooleanFalseLiteral";

		/// <summary>Used by the GetSchema method to create the BooleanTrueLiteral column.</summary>
		// Token: 0x040017C8 RID: 6088
		public static readonly string BooleanTrueLiteral = "BooleanTrueLiteral";

		/// <summary>Used by the GetSchema method to create the SQLType column.</summary>
		// Token: 0x040017C9 RID: 6089
		public static readonly string SQLType = "SQLType";
	}
}
