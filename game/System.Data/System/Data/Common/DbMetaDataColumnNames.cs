using System;

namespace System.Data.Common
{
	/// <summary>Provides static values that are used for the column names in the MetaDataCollection objects contained in the <see cref="T:System.Data.DataTable" />. The <see cref="T:System.Data.DataTable" /> is created by the GetSchema method.</summary>
	// Token: 0x0200039A RID: 922
	public static class DbMetaDataColumnNames
	{
		// Token: 0x06002CB9 RID: 11449 RVA: 0x000BED04 File Offset: 0x000BCF04
		// Note: this type is marked as 'beforefieldinit'.
		static DbMetaDataColumnNames()
		{
		}

		/// <summary>Used by the GetSchema method to create the CollectionName column in the DataTypes collection.</summary>
		// Token: 0x04001B5E RID: 7006
		public static readonly string CollectionName = "CollectionName";

		/// <summary>Used by the GetSchema method to create the ColumnSize column in the DataTypes collection.</summary>
		// Token: 0x04001B5F RID: 7007
		public static readonly string ColumnSize = "ColumnSize";

		/// <summary>Used by the GetSchema method to create the CompositeIdentifierSeparatorPattern column in the DataSourceInformation collection.</summary>
		// Token: 0x04001B60 RID: 7008
		public static readonly string CompositeIdentifierSeparatorPattern = "CompositeIdentifierSeparatorPattern";

		/// <summary>Used by the GetSchema method to create the CreateFormat column in the DataTypes collection.</summary>
		// Token: 0x04001B61 RID: 7009
		public static readonly string CreateFormat = "CreateFormat";

		/// <summary>Used by the GetSchema method to create the CreateParameters column in the DataTypes collection.</summary>
		// Token: 0x04001B62 RID: 7010
		public static readonly string CreateParameters = "CreateParameters";

		/// <summary>Used by the GetSchema method to create the DataSourceProductName column in the DataSourceInformation collection.</summary>
		// Token: 0x04001B63 RID: 7011
		public static readonly string DataSourceProductName = "DataSourceProductName";

		/// <summary>Used by the GetSchema method to create the DataSourceProductVersion column in the DataSourceInformation collection.</summary>
		// Token: 0x04001B64 RID: 7012
		public static readonly string DataSourceProductVersion = "DataSourceProductVersion";

		/// <summary>Used by the GetSchema method to create the DataType column in the DataTypes collection.</summary>
		// Token: 0x04001B65 RID: 7013
		public static readonly string DataType = "DataType";

		/// <summary>Used by the GetSchema method to create the DataSourceProductVersionNormalized column in the DataSourceInformation collection.</summary>
		// Token: 0x04001B66 RID: 7014
		public static readonly string DataSourceProductVersionNormalized = "DataSourceProductVersionNormalized";

		/// <summary>Used by the GetSchema method to create the GroupByBehavior column in the DataSourceInformation collection.</summary>
		// Token: 0x04001B67 RID: 7015
		public static readonly string GroupByBehavior = "GroupByBehavior";

		/// <summary>Used by the GetSchema method to create the IdentifierCase column in the DataSourceInformation collection.</summary>
		// Token: 0x04001B68 RID: 7016
		public static readonly string IdentifierCase = "IdentifierCase";

		/// <summary>Used by the GetSchema method to create the IdentifierPattern column in the DataSourceInformation collection.</summary>
		// Token: 0x04001B69 RID: 7017
		public static readonly string IdentifierPattern = "IdentifierPattern";

		/// <summary>Used by the GetSchema method to create the IsAutoIncrementable column in the DataTypes collection.</summary>
		// Token: 0x04001B6A RID: 7018
		public static readonly string IsAutoIncrementable = "IsAutoIncrementable";

		/// <summary>Used by the GetSchema method to create the IsBestMatch column in the DataTypes collection.</summary>
		// Token: 0x04001B6B RID: 7019
		public static readonly string IsBestMatch = "IsBestMatch";

		/// <summary>Used by the GetSchema method to create the IsCaseSensitive column in the DataTypes collection.</summary>
		// Token: 0x04001B6C RID: 7020
		public static readonly string IsCaseSensitive = "IsCaseSensitive";

		/// <summary>Used by the GetSchema method to create the IsConcurrencyType column in the DataTypes collection.</summary>
		// Token: 0x04001B6D RID: 7021
		public static readonly string IsConcurrencyType = "IsConcurrencyType";

		/// <summary>Used by the GetSchema method to create the IsFixedLength column in the DataTypes collection.</summary>
		// Token: 0x04001B6E RID: 7022
		public static readonly string IsFixedLength = "IsFixedLength";

		/// <summary>Used by the GetSchema method to create the IsFixedPrecisionScale column in the DataTypes collection.</summary>
		// Token: 0x04001B6F RID: 7023
		public static readonly string IsFixedPrecisionScale = "IsFixedPrecisionScale";

		/// <summary>Used by the GetSchema method to create the IsLiteralSupported column in the DataTypes collection.</summary>
		// Token: 0x04001B70 RID: 7024
		public static readonly string IsLiteralSupported = "IsLiteralSupported";

		/// <summary>Used by the GetSchema method to create the IsLong column in the DataTypes collection.</summary>
		// Token: 0x04001B71 RID: 7025
		public static readonly string IsLong = "IsLong";

		/// <summary>Used by the GetSchema method to create the IsNullable column in the DataTypes collection.</summary>
		// Token: 0x04001B72 RID: 7026
		public static readonly string IsNullable = "IsNullable";

		/// <summary>Used by the GetSchema method to create the IsSearchable column in the DataTypes collection.</summary>
		// Token: 0x04001B73 RID: 7027
		public static readonly string IsSearchable = "IsSearchable";

		/// <summary>Used by the GetSchema method to create the IsSearchableWithLike column in the DataTypes collection.</summary>
		// Token: 0x04001B74 RID: 7028
		public static readonly string IsSearchableWithLike = "IsSearchableWithLike";

		/// <summary>Used by the GetSchema method to create the IsUnsigned column in the DataTypes collection.</summary>
		// Token: 0x04001B75 RID: 7029
		public static readonly string IsUnsigned = "IsUnsigned";

		/// <summary>Used by the GetSchema method to create the LiteralPrefix column in the DataTypes collection.</summary>
		// Token: 0x04001B76 RID: 7030
		public static readonly string LiteralPrefix = "LiteralPrefix";

		/// <summary>Used by the GetSchema method to create the LiteralSuffix column in the DataTypes collection.</summary>
		// Token: 0x04001B77 RID: 7031
		public static readonly string LiteralSuffix = "LiteralSuffix";

		/// <summary>Used by the GetSchema method to create the MaximumScale column in the DataTypes collection.</summary>
		// Token: 0x04001B78 RID: 7032
		public static readonly string MaximumScale = "MaximumScale";

		/// <summary>Used by the GetSchema method to create the MinimumScale column in the DataTypes collection.</summary>
		// Token: 0x04001B79 RID: 7033
		public static readonly string MinimumScale = "MinimumScale";

		/// <summary>Used by the GetSchema method to create the NumberOfIdentifierParts column in the MetaDataCollections collection.</summary>
		// Token: 0x04001B7A RID: 7034
		public static readonly string NumberOfIdentifierParts = "NumberOfIdentifierParts";

		/// <summary>Used by the GetSchema method to create the NumberOfRestrictions column in the MetaDataCollections collection.</summary>
		// Token: 0x04001B7B RID: 7035
		public static readonly string NumberOfRestrictions = "NumberOfRestrictions";

		/// <summary>Used by the GetSchema method to create the OrderByColumnsInSelect column in the DataSourceInformation collection.</summary>
		// Token: 0x04001B7C RID: 7036
		public static readonly string OrderByColumnsInSelect = "OrderByColumnsInSelect";

		/// <summary>Used by the GetSchema method to create the ParameterMarkerFormat column in the DataSourceInformation collection.</summary>
		// Token: 0x04001B7D RID: 7037
		public static readonly string ParameterMarkerFormat = "ParameterMarkerFormat";

		/// <summary>Used by the GetSchema method to create the ParameterMarkerPattern column in the DataSourceInformation collection.</summary>
		// Token: 0x04001B7E RID: 7038
		public static readonly string ParameterMarkerPattern = "ParameterMarkerPattern";

		/// <summary>Used by the GetSchema method to create the ParameterNameMaxLength column in the DataSourceInformation collection.</summary>
		// Token: 0x04001B7F RID: 7039
		public static readonly string ParameterNameMaxLength = "ParameterNameMaxLength";

		/// <summary>Used by the GetSchema method to create the ParameterNamePattern column in the DataSourceInformation collection.</summary>
		// Token: 0x04001B80 RID: 7040
		public static readonly string ParameterNamePattern = "ParameterNamePattern";

		/// <summary>Used by the GetSchema method to create the ProviderDbType column in the DataTypes collection.</summary>
		// Token: 0x04001B81 RID: 7041
		public static readonly string ProviderDbType = "ProviderDbType";

		/// <summary>Used by the GetSchema method to create the QuotedIdentifierCase column in the DataSourceInformation collection.</summary>
		// Token: 0x04001B82 RID: 7042
		public static readonly string QuotedIdentifierCase = "QuotedIdentifierCase";

		/// <summary>Used by the GetSchema method to create the QuotedIdentifierPattern column in the DataSourceInformation collection.</summary>
		// Token: 0x04001B83 RID: 7043
		public static readonly string QuotedIdentifierPattern = "QuotedIdentifierPattern";

		/// <summary>Used by the GetSchema method to create the ReservedWord column in the ReservedWords collection.</summary>
		// Token: 0x04001B84 RID: 7044
		public static readonly string ReservedWord = "ReservedWord";

		/// <summary>Used by the GetSchema method to create the StatementSeparatorPattern column in the DataSourceInformation collection.</summary>
		// Token: 0x04001B85 RID: 7045
		public static readonly string StatementSeparatorPattern = "StatementSeparatorPattern";

		/// <summary>Used by the GetSchema method to create the StringLiteralPattern column in the DataSourceInformation collection.</summary>
		// Token: 0x04001B86 RID: 7046
		public static readonly string StringLiteralPattern = "StringLiteralPattern";

		/// <summary>Used by the GetSchema method to create the SupportedJoinOperators column in the DataSourceInformation collection.</summary>
		// Token: 0x04001B87 RID: 7047
		public static readonly string SupportedJoinOperators = "SupportedJoinOperators";

		/// <summary>Used by the GetSchema method to create the TypeName column in the DataTypes collection.</summary>
		// Token: 0x04001B88 RID: 7048
		public static readonly string TypeName = "TypeName";
	}
}
