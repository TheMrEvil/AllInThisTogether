using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200002E RID: 46
	internal class SmiQueryMetaData : SmiStorageMetaData
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00007CD0 File Offset: 0x00005ED0
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, isReadOnly, isExpression, isAliased, isHidden)
		{
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007D14 File Offset: 0x00005F14
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, false, isReadOnly, isExpression, isAliased, isHidden)
		{
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00007D5C File Offset: 0x00005F5C
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isColumnSet, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden) : base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, isColumnSet)
		{
			this._isReadOnly = isReadOnly;
			this._isExpression = isExpression;
			this._isAliased = isAliased;
			this._isHidden = isHidden;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00007DBC File Offset: 0x00005FBC
		internal bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00007DC4 File Offset: 0x00005FC4
		internal SqlBoolean IsExpression
		{
			get
			{
				return this._isExpression;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00007DCC File Offset: 0x00005FCC
		internal SqlBoolean IsAliased
		{
			get
			{
				return this._isAliased;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00007DD4 File Offset: 0x00005FD4
		internal SqlBoolean IsHidden
		{
			get
			{
				return this._isHidden;
			}
		}

		// Token: 0x040004A0 RID: 1184
		private bool _isReadOnly;

		// Token: 0x040004A1 RID: 1185
		private SqlBoolean _isExpression;

		// Token: 0x040004A2 RID: 1186
		private SqlBoolean _isAliased;

		// Token: 0x040004A3 RID: 1187
		private SqlBoolean _isHidden;
	}
}
