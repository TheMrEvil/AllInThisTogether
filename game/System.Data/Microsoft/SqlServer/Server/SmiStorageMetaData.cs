using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200002D RID: 45
	internal class SmiStorageMetaData : SmiExtendedMetaData
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x00007B94 File Offset: 0x00005D94
		internal SmiStorageMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity)
		{
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007BD0 File Offset: 0x00005DD0
		internal SmiStorageMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, false)
		{
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007C10 File Offset: 0x00005E10
		internal SmiStorageMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isColumnSet) : base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
			this._allowsDBNull = allowsDBNull;
			this._serverName = serverName;
			this._catalogName = catalogName;
			this._schemaName = schemaName;
			this._tableName = tableName;
			this._columnName = columnName;
			this._isKey = isKey;
			this._isIdentity = isIdentity;
			this._isColumnSet = isColumnSet;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00007C86 File Offset: 0x00005E86
		internal bool AllowsDBNull
		{
			get
			{
				return this._allowsDBNull;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00007C8E File Offset: 0x00005E8E
		internal string ServerName
		{
			get
			{
				return this._serverName;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00007C96 File Offset: 0x00005E96
		internal string CatalogName
		{
			get
			{
				return this._catalogName;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00007C9E File Offset: 0x00005E9E
		internal string SchemaName
		{
			get
			{
				return this._schemaName;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00007CA6 File Offset: 0x00005EA6
		internal string TableName
		{
			get
			{
				return this._tableName;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00007CAE File Offset: 0x00005EAE
		internal string ColumnName
		{
			get
			{
				return this._columnName;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00007CB6 File Offset: 0x00005EB6
		internal SqlBoolean IsKey
		{
			get
			{
				return this._isKey;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00007CBE File Offset: 0x00005EBE
		internal bool IsIdentity
		{
			get
			{
				return this._isIdentity;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00007CC6 File Offset: 0x00005EC6
		internal bool IsColumnSet
		{
			get
			{
				return this._isColumnSet;
			}
		}

		// Token: 0x04000497 RID: 1175
		private bool _allowsDBNull;

		// Token: 0x04000498 RID: 1176
		private string _serverName;

		// Token: 0x04000499 RID: 1177
		private string _catalogName;

		// Token: 0x0400049A RID: 1178
		private string _schemaName;

		// Token: 0x0400049B RID: 1179
		private string _tableName;

		// Token: 0x0400049C RID: 1180
		private string _columnName;

		// Token: 0x0400049D RID: 1181
		private SqlBoolean _isKey;

		// Token: 0x0400049E RID: 1182
		private bool _isIdentity;

		// Token: 0x0400049F RID: 1183
		private bool _isColumnSet;
	}
}
