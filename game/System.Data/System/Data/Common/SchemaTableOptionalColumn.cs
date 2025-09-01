using System;

namespace System.Data.Common
{
	/// <summary>Describes optional column metadata of the schema for a database table.</summary>
	// Token: 0x020003BB RID: 955
	public static class SchemaTableOptionalColumn
	{
		// Token: 0x06002E75 RID: 11893 RVA: 0x000C6F58 File Offset: 0x000C5158
		// Note: this type is marked as 'beforefieldinit'.
		static SchemaTableOptionalColumn()
		{
		}

		/// <summary>Specifies the provider-specific data type of the column.</summary>
		// Token: 0x04001BCC RID: 7116
		public static readonly string ProviderSpecificDataType = "ProviderSpecificDataType";

		/// <summary>Specifies whether the column values in the column are automatically incremented.</summary>
		// Token: 0x04001BCD RID: 7117
		public static readonly string IsAutoIncrement = "IsAutoIncrement";

		/// <summary>Specifies whether this column is hidden.</summary>
		// Token: 0x04001BCE RID: 7118
		public static readonly string IsHidden = "IsHidden";

		/// <summary>Specifies whether this column is read-only.</summary>
		// Token: 0x04001BCF RID: 7119
		public static readonly string IsReadOnly = "IsReadOnly";

		/// <summary>Specifies whether this column contains row version information.</summary>
		// Token: 0x04001BD0 RID: 7120
		public static readonly string IsRowVersion = "IsRowVersion";

		/// <summary>The server name of the column.</summary>
		// Token: 0x04001BD1 RID: 7121
		public static readonly string BaseServerName = "BaseServerName";

		/// <summary>The name of the catalog associated with the results of the latest query.</summary>
		// Token: 0x04001BD2 RID: 7122
		public static readonly string BaseCatalogName = "BaseCatalogName";

		/// <summary>Specifies the value at which the series for new identity columns is assigned.</summary>
		// Token: 0x04001BD3 RID: 7123
		public static readonly string AutoIncrementSeed = "AutoIncrementSeed";

		/// <summary>Specifies the increment between values in the identity column.</summary>
		// Token: 0x04001BD4 RID: 7124
		public static readonly string AutoIncrementStep = "AutoIncrementStep";

		/// <summary>The default value for the column.</summary>
		// Token: 0x04001BD5 RID: 7125
		public static readonly string DefaultValue = "DefaultValue";

		/// <summary>The expression used to compute the column.</summary>
		// Token: 0x04001BD6 RID: 7126
		public static readonly string Expression = "Expression";

		/// <summary>The namespace for the table that contains the column.</summary>
		// Token: 0x04001BD7 RID: 7127
		public static readonly string BaseTableNamespace = "BaseTableNamespace";

		/// <summary>The namespace of the column.</summary>
		// Token: 0x04001BD8 RID: 7128
		public static readonly string BaseColumnNamespace = "BaseColumnNamespace";

		/// <summary>Specifies the mapping for the column.</summary>
		// Token: 0x04001BD9 RID: 7129
		public static readonly string ColumnMapping = "ColumnMapping";
	}
}
