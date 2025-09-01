using System;

namespace System.Data.Common
{
	/// <summary>Describes the column metadata of the schema for a database table.</summary>
	// Token: 0x020003BA RID: 954
	public static class SchemaTableColumn
	{
		// Token: 0x06002E74 RID: 11892 RVA: 0x000C6EA0 File Offset: 0x000C50A0
		// Note: this type is marked as 'beforefieldinit'.
		static SchemaTableColumn()
		{
		}

		/// <summary>Specifies the name of the column in the schema table.</summary>
		// Token: 0x04001BBB RID: 7099
		public static readonly string ColumnName = "ColumnName";

		/// <summary>Specifies the ordinal of the column.</summary>
		// Token: 0x04001BBC RID: 7100
		public static readonly string ColumnOrdinal = "ColumnOrdinal";

		/// <summary>Specifies the size of the column.</summary>
		// Token: 0x04001BBD RID: 7101
		public static readonly string ColumnSize = "ColumnSize";

		/// <summary>Specifies the precision of the column data, if the data is numeric.</summary>
		// Token: 0x04001BBE RID: 7102
		public static readonly string NumericPrecision = "NumericPrecision";

		/// <summary>Specifies the scale of the column data, if the data is numeric.</summary>
		// Token: 0x04001BBF RID: 7103
		public static readonly string NumericScale = "NumericScale";

		/// <summary>Specifies the type of data in the column.</summary>
		// Token: 0x04001BC0 RID: 7104
		public static readonly string DataType = "DataType";

		/// <summary>Specifies the provider-specific data type of the column.</summary>
		// Token: 0x04001BC1 RID: 7105
		public static readonly string ProviderType = "ProviderType";

		/// <summary>Specifies the non-versioned provider-specific data type of the column.</summary>
		// Token: 0x04001BC2 RID: 7106
		public static readonly string NonVersionedProviderType = "NonVersionedProviderType";

		/// <summary>Specifies whether this column contains long data.</summary>
		// Token: 0x04001BC3 RID: 7107
		public static readonly string IsLong = "IsLong";

		/// <summary>Specifies whether value <see langword="DBNull" /> is allowed.</summary>
		// Token: 0x04001BC4 RID: 7108
		public static readonly string AllowDBNull = "AllowDBNull";

		/// <summary>Specifies whether this column is aliased.</summary>
		// Token: 0x04001BC5 RID: 7109
		public static readonly string IsAliased = "IsAliased";

		/// <summary>Specifies whether this column is an expression.</summary>
		// Token: 0x04001BC6 RID: 7110
		public static readonly string IsExpression = "IsExpression";

		/// <summary>Specifies whether this column is a key for the table.</summary>
		// Token: 0x04001BC7 RID: 7111
		public static readonly string IsKey = "IsKey";

		/// <summary>Specifies whether a unique constraint applies to this column.</summary>
		// Token: 0x04001BC8 RID: 7112
		public static readonly string IsUnique = "IsUnique";

		/// <summary>Specifies the name of the schema in the schema table.</summary>
		// Token: 0x04001BC9 RID: 7113
		public static readonly string BaseSchemaName = "BaseSchemaName";

		/// <summary>Specifies the name of the table in the schema table.</summary>
		// Token: 0x04001BCA RID: 7114
		public static readonly string BaseTableName = "BaseTableName";

		/// <summary>Specifies the name of the column in the schema table.</summary>
		// Token: 0x04001BCB RID: 7115
		public static readonly string BaseColumnName = "BaseColumnName";
	}
}
