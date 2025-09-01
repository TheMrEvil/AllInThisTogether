using System;

namespace System.Data.Common
{
	/// <summary>Provides a list of constants for the well-known MetaDataCollections: DataSourceInformation, DataTypes, MetaDataCollections, ReservedWords, and Restrictions.</summary>
	// Token: 0x02000399 RID: 921
	public static class DbMetaDataCollectionNames
	{
		// Token: 0x06002CB8 RID: 11448 RVA: 0x000BECD0 File Offset: 0x000BCED0
		// Note: this type is marked as 'beforefieldinit'.
		static DbMetaDataCollectionNames()
		{
		}

		/// <summary>A constant for use with the <see cref="M:System.Data.Common.DbConnection.GetSchema" /> method that represents the MetaDataCollections collection.</summary>
		// Token: 0x04001B59 RID: 7001
		public static readonly string MetaDataCollections = "MetaDataCollections";

		/// <summary>A constant for use with the <see cref="M:System.Data.Common.DbConnection.GetSchema" /> method that represents the DataSourceInformation collection.</summary>
		// Token: 0x04001B5A RID: 7002
		public static readonly string DataSourceInformation = "DataSourceInformation";

		/// <summary>A constant for use with the <see cref="M:System.Data.Common.DbConnection.GetSchema" /> method that represents the DataTypes collection.</summary>
		// Token: 0x04001B5B RID: 7003
		public static readonly string DataTypes = "DataTypes";

		/// <summary>A constant for use with the <see cref="M:System.Data.Common.DbConnection.GetSchema" /> method that represents the Restrictions collection.</summary>
		// Token: 0x04001B5C RID: 7004
		public static readonly string Restrictions = "Restrictions";

		/// <summary>A constant for use with the <see cref="M:System.Data.Common.DbConnection.GetSchema" /> method that represents the ReservedWords collection.</summary>
		// Token: 0x04001B5D RID: 7005
		public static readonly string ReservedWords = "ReservedWords";
	}
}
