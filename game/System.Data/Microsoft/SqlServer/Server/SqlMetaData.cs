using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Specifies and retrieves metadata information from parameters and columns of <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000040 RID: 64
	public sealed class SqlMetaData
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name and type.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A <see langword="SqlDbType" /> that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
		// Token: 0x06000396 RID: 918 RVA: 0x0000D34D File Offset: 0x0000B54D
		public SqlMetaData(string name, SqlDbType dbType)
		{
			this.Construct(name, dbType, false, false, SortOrder.Unspecified, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, and default server. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x06000397 RID: 919 RVA: 0x0000D361 File Offset: 0x0000B561
		public SqlMetaData(string name, SqlDbType dbType, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, and maximum length.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
		// Token: 0x06000398 RID: 920 RVA: 0x0000D378 File Offset: 0x0000B578
		public SqlMetaData(string name, SqlDbType dbType, long maxLength)
		{
			this.Construct(name, dbType, maxLength, false, false, SortOrder.Unspecified, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, and server default. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x06000399 RID: 921 RVA: 0x0000D38D File Offset: 0x0000B58D
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, maxLength, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, and user-defined type (UDT).</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />, or <paramref name="userDefinedType" /> points to a type that does not have <see cref="T:Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute" /> declared.</exception>
		// Token: 0x0600039A RID: 922 RVA: 0x0000D3A8 File Offset: 0x0000B5A8
		public SqlMetaData(string name, SqlDbType dbType, Type userDefinedType)
		{
			this.Construct(name, dbType, userDefinedType, null, false, false, SortOrder.Unspecified, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, user-defined type (UDT), and SQLServer type.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
		/// <param name="serverTypeName">The SQL Server type name for <paramref name="userDefinedType" />.</param>
		// Token: 0x0600039B RID: 923 RVA: 0x0000D3CC File Offset: 0x0000B5CC
		public SqlMetaData(string name, SqlDbType dbType, Type userDefinedType, string serverTypeName)
		{
			this.Construct(name, dbType, userDefinedType, serverTypeName, false, false, SortOrder.Unspecified, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, user-defined type, SQL Server type, and server default. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
		/// <param name="serverTypeName">The SQL Server type name for <paramref name="userDefinedType" />.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x0600039C RID: 924 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
		public SqlMetaData(string name, SqlDbType dbType, Type userDefinedType, string serverTypeName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, userDefinedType, serverTypeName, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, precision, and scale.</summary>
		/// <param name="name">The name of the parameter or column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="precision">The precision of the parameter or column.</param>
		/// <param name="scale">The scale of the parameter or column.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A <see langword="SqlDbType" /> that is not allowed was passed to the constructor as <paramref name="dbType" />, or <paramref name="scale" /> was greater than <paramref name="precision" />.</exception>
		// Token: 0x0600039D RID: 925 RVA: 0x0000D418 File Offset: 0x0000B618
		public SqlMetaData(string name, SqlDbType dbType, byte precision, byte scale)
		{
			this.Construct(name, dbType, precision, scale, false, false, SortOrder.Unspecified, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, precision, scale, and server default. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="precision">The precision of the parameter or column.</param>
		/// <param name="scale">The scale of the parameter or column.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x0600039E RID: 926 RVA: 0x0000D43C File Offset: 0x0000B63C
		public SqlMetaData(string name, SqlDbType dbType, byte precision, byte scale, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, precision, scale, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, locale, and compare options.</summary>
		/// <param name="name">The name of the parameter or column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type.</param>
		/// <param name="locale">The locale ID of the parameter or column.</param>
		/// <param name="compareOptions">The comparison rules of the parameter or column.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
		// Token: 0x0600039F RID: 927 RVA: 0x0000D464 File Offset: 0x0000B664
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, long locale, SqlCompareOptions compareOptions)
		{
			this.Construct(name, dbType, maxLength, locale, compareOptions, false, false, SortOrder.Unspecified, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, locale, compare options, and server default. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type.</param>
		/// <param name="locale">The locale ID of the parameter or column.</param>
		/// <param name="compareOptions">The comparison rules of the parameter or column.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x060003A0 RID: 928 RVA: 0x0000D488 File Offset: 0x0000B688
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, long locale, SqlCompareOptions compareOptions, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, maxLength, locale, compareOptions, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, database name, owning schema, object name, and default server. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="database">The database name of the XML schema collection of a typed XML instance.</param>
		/// <param name="owningSchema">The relational schema name of the XML schema collection of a typed XML instance.</param>
		/// <param name="objectName">The name of the XML schema collection of a typed XML instance.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x060003A1 RID: 929 RVA: 0x0000D4B0 File Offset: 0x0000B6B0
		public SqlMetaData(string name, SqlDbType dbType, string database, string owningSchema, string objectName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, database, owningSchema, objectName, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, precision, scale, locale ID, compare options, and user-defined type (UDT).</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type.</param>
		/// <param name="precision">The precision of the parameter or column.</param>
		/// <param name="scale">The scale of the parameter or column.</param>
		/// <param name="locale">The locale ID of the parameter or column.</param>
		/// <param name="compareOptions">The comparison rules of the parameter or column.</param>
		/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A <see langword="SqlDbType" /> that is not allowed was passed to the constructor as <paramref name="dbType" />, or <paramref name="userDefinedType" /> points to a type that does not have <see cref="T:Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute" /> declared.</exception>
		// Token: 0x060003A2 RID: 930 RVA: 0x0000D4D8 File Offset: 0x0000B6D8
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, byte precision, byte scale, long locale, SqlCompareOptions compareOptions, Type userDefinedType) : this(name, dbType, maxLength, precision, scale, locale, compareOptions, userDefinedType, false, false, SortOrder.Unspecified, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, precision, scale, locale ID, compare options, and user-defined type (UDT). This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type.</param>
		/// <param name="precision">The precision of the parameter or column.</param>
		/// <param name="scale">The scale of the parameter or column.</param>
		/// <param name="localeId">The locale ID of the parameter or column.</param>
		/// <param name="compareOptions">The comparison rules of the parameter or column.</param>
		/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x060003A3 RID: 931 RVA: 0x0000D4FC File Offset: 0x0000B6FC
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			switch (dbType)
			{
			case SqlDbType.BigInt:
			case SqlDbType.Bit:
			case SqlDbType.DateTime:
			case SqlDbType.Float:
			case SqlDbType.Image:
			case SqlDbType.Int:
			case SqlDbType.Money:
			case SqlDbType.Real:
			case SqlDbType.UniqueIdentifier:
			case SqlDbType.SmallDateTime:
			case SqlDbType.SmallInt:
			case SqlDbType.SmallMoney:
			case SqlDbType.Timestamp:
			case SqlDbType.TinyInt:
			case SqlDbType.Xml:
			case SqlDbType.Date:
				this.Construct(name, dbType, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Binary:
			case SqlDbType.VarBinary:
				this.Construct(name, dbType, maxLength, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Char:
			case SqlDbType.NChar:
			case SqlDbType.NVarChar:
			case SqlDbType.VarChar:
				this.Construct(name, dbType, maxLength, localeId, compareOptions, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Decimal:
			case SqlDbType.Time:
			case SqlDbType.DateTime2:
			case SqlDbType.DateTimeOffset:
				this.Construct(name, dbType, precision, scale, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.NText:
			case SqlDbType.Text:
				this.Construct(name, dbType, SqlMetaData.Max, localeId, compareOptions, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Variant:
				this.Construct(name, dbType, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Udt:
				this.Construct(name, dbType, userDefinedType, "", useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			}
			SQL.InvalidSqlDbTypeForConstructor(dbType);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, database name, owning schema, and object name.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="database">The database name of the XML schema collection of a typed XML instance.</param>
		/// <param name="owningSchema">The relational schema name of the XML schema collection of a typed XML instance.</param>
		/// <param name="objectName">The name of the XML schema collection of a typed XML instance.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is <see langword="null" />, or <paramref name="objectName" /> is <see langword="null" /> when <paramref name="database" /> and <paramref name="owningSchema" /> are non-<see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
		// Token: 0x060003A4 RID: 932 RVA: 0x0000D640 File Offset: 0x0000B840
		public SqlMetaData(string name, SqlDbType dbType, string database, string owningSchema, string objectName)
		{
			this.Construct(name, dbType, database, owningSchema, objectName, false, false, SortOrder.Unspecified, -1);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000D664 File Offset: 0x0000B864
		internal SqlMetaData(string name, SqlDbType sqlDBType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, string xmlSchemaCollectionDatabase, string xmlSchemaCollectionOwningSchema, string xmlSchemaCollectionName, bool partialLength, Type udtType)
		{
			this.AssertNameIsValid(name);
			this._strName = name;
			this._sqlDbType = sqlDBType;
			this._lMaxLength = maxLength;
			this._bPrecision = precision;
			this._bScale = scale;
			this._lLocale = localeId;
			this._eCompareOptions = compareOptions;
			this._xmlSchemaCollectionDatabase = xmlSchemaCollectionDatabase;
			this._xmlSchemaCollectionOwningSchema = xmlSchemaCollectionOwningSchema;
			this._xmlSchemaCollectionName = xmlSchemaCollectionName;
			this._bPartialLength = partialLength;
			this._udtType = udtType;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000D6DC File Offset: 0x0000B8DC
		private SqlMetaData(string name, SqlDbType sqlDbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, bool partialLength)
		{
			this.AssertNameIsValid(name);
			this._strName = name;
			this._sqlDbType = sqlDbType;
			this._lMaxLength = maxLength;
			this._bPrecision = precision;
			this._bScale = scale;
			this._lLocale = localeId;
			this._eCompareOptions = compareOptions;
			this._bPartialLength = partialLength;
			this._udtType = null;
		}

		/// <summary>Gets the comparison rules used for the column or parameter.</summary>
		/// <returns>The comparison rules used for the column or parameter as a <see cref="T:System.Data.SqlTypes.SqlCompareOptions" />.</returns>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000D73A File Offset: 0x0000B93A
		public SqlCompareOptions CompareOptions
		{
			get
			{
				return this._eCompareOptions;
			}
		}

		/// <summary>Gets the data type of the column or parameter.</summary>
		/// <returns>The data type of the column or parameter as a <see cref="T:System.Data.DbType" />.</returns>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000D742 File Offset: 0x0000B942
		public DbType DbType
		{
			get
			{
				return SqlMetaData.sxm_rgSqlDbTypeToDbType[(int)this._sqlDbType];
			}
		}

		/// <summary>Indicates if the column in the table-valued parameter is unique.</summary>
		/// <returns>A <see langword="Boolean" /> value.</returns>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000D750 File Offset: 0x0000B950
		public bool IsUniqueKey
		{
			get
			{
				return this._isUniqueKey;
			}
		}

		/// <summary>Gets the locale ID of the column or parameter.</summary>
		/// <returns>The locale ID of the column or parameter as a <see cref="T:System.Int64" />.</returns>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000D758 File Offset: 0x0000B958
		public long LocaleId
		{
			get
			{
				return this._lLocale;
			}
		}

		/// <summary>Gets the length of <see langword="text" />, <see langword="ntext" />, and <see langword="image" /> data types.</summary>
		/// <returns>The length of <see langword="text" />, <see langword="ntext" />, and <see langword="image" /> data types.</returns>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000D760 File Offset: 0x0000B960
		public static long Max
		{
			get
			{
				return -1L;
			}
		}

		/// <summary>Gets the maximum length of the column or parameter.</summary>
		/// <returns>The maximum length of the column or parameter as a <see cref="T:System.Int64" />.</returns>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000D764 File Offset: 0x0000B964
		public long MaxLength
		{
			get
			{
				return this._lMaxLength;
			}
		}

		/// <summary>Gets the name of the column or parameter.</summary>
		/// <returns>The name of the column or parameter as a <see cref="T:System.String" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="Name" /> specified in the constructor is longer than 128 characters.</exception>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000D76C File Offset: 0x0000B96C
		public string Name
		{
			get
			{
				return this._strName;
			}
		}

		/// <summary>Gets the precision of the column or parameter.</summary>
		/// <returns>The precision of the column or parameter as a <see cref="T:System.Byte" />.</returns>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000D774 File Offset: 0x0000B974
		public byte Precision
		{
			get
			{
				return this._bPrecision;
			}
		}

		/// <summary>Gets the scale of the column or parameter.</summary>
		/// <returns>The scale of the column or parameter.</returns>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000D77C File Offset: 0x0000B97C
		public byte Scale
		{
			get
			{
				return this._bScale;
			}
		}

		/// <summary>Returns the sort order for a column.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SortOrder" /> object.</returns>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000D784 File Offset: 0x0000B984
		public SortOrder SortOrder
		{
			get
			{
				return this._columnSortOrder;
			}
		}

		/// <summary>Returns the ordinal of the sort column.</summary>
		/// <returns>The ordinal of the sort column.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000D78C File Offset: 0x0000B98C
		public int SortOrdinal
		{
			get
			{
				return this._sortOrdinal;
			}
		}

		/// <summary>Gets the data type of the column or parameter.</summary>
		/// <returns>The data type of the column or parameter as a <see cref="T:System.Data.DbType" />.</returns>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000D794 File Offset: 0x0000B994
		public SqlDbType SqlDbType
		{
			get
			{
				return this._sqlDbType;
			}
		}

		/// <summary>Gets the common language runtime (CLR) type of a user-defined type (UDT).</summary>
		/// <returns>The CLR type name of a user-defined type as a <see cref="T:System.Type" />.</returns>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000D79C File Offset: 0x0000B99C
		public Type Type
		{
			get
			{
				return this._udtType;
			}
		}

		/// <summary>Gets the three-part name of the user-defined type (UDT) or the SQL Server type represented by the instance.</summary>
		/// <returns>The name of the UDT or SQL Server type as a <see cref="T:System.String" />.</returns>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000D7A4 File Offset: 0x0000B9A4
		public string TypeName
		{
			get
			{
				if (this._serverTypeName != null)
				{
					return this._serverTypeName;
				}
				if (this.SqlDbType == SqlDbType.Udt)
				{
					return this.UdtTypeName;
				}
				return SqlMetaData.sxm_rgDefaults[(int)this.SqlDbType].Name;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000D7D7 File Offset: 0x0000B9D7
		internal string ServerTypeName
		{
			get
			{
				return this._serverTypeName;
			}
		}

		/// <summary>Reports whether this column should use the default server value.</summary>
		/// <returns>A <see langword="Boolean" /> value.</returns>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000D7DF File Offset: 0x0000B9DF
		public bool UseServerDefault
		{
			get
			{
				return this._useServerDefault;
			}
		}

		/// <summary>Gets the name of the database where the schema collection for this XML instance is located.</summary>
		/// <returns>The name of the database where the schema collection for this XML instance is located as a <see cref="T:System.String" />.</returns>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000D7E7 File Offset: 0x0000B9E7
		public string XmlSchemaCollectionDatabase
		{
			get
			{
				return this._xmlSchemaCollectionDatabase;
			}
		}

		/// <summary>Gets the name of the schema collection for this XML instance.</summary>
		/// <returns>The name of the schema collection for this XML instance as a <see cref="T:System.String" />.</returns>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000D7EF File Offset: 0x0000B9EF
		public string XmlSchemaCollectionName
		{
			get
			{
				return this._xmlSchemaCollectionName;
			}
		}

		/// <summary>Gets the owning relational schema where the schema collection for this XML instance is located.</summary>
		/// <returns>The owning relational schema where the schema collection for this XML instance is located as a <see cref="T:System.String" />.</returns>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000D7F7 File Offset: 0x0000B9F7
		public string XmlSchemaCollectionOwningSchema
		{
			get
			{
				return this._xmlSchemaCollectionOwningSchema;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000D7FF File Offset: 0x0000B9FF
		internal bool IsPartialLength
		{
			get
			{
				return this._bPartialLength;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000D807 File Offset: 0x0000BA07
		internal string UdtTypeName
		{
			get
			{
				if (this.SqlDbType != SqlDbType.Udt)
				{
					return null;
				}
				if (this._udtType == null)
				{
					return null;
				}
				return this._udtType.FullName;
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000D830 File Offset: 0x0000BA30
		private void Construct(string name, SqlDbType dbType, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (dbType != SqlDbType.BigInt && SqlDbType.Bit != dbType && SqlDbType.DateTime != dbType && SqlDbType.Date != dbType && SqlDbType.DateTime2 != dbType && SqlDbType.DateTimeOffset != dbType && SqlDbType.Decimal != dbType && SqlDbType.Float != dbType && SqlDbType.Image != dbType && SqlDbType.Int != dbType && SqlDbType.Money != dbType && SqlDbType.NText != dbType && SqlDbType.Real != dbType && SqlDbType.SmallDateTime != dbType && SqlDbType.SmallInt != dbType && SqlDbType.SmallMoney != dbType && SqlDbType.Text != dbType && SqlDbType.Time != dbType && SqlDbType.Timestamp != dbType && SqlDbType.TinyInt != dbType && SqlDbType.UniqueIdentifier != dbType && SqlDbType.Variant != dbType && SqlDbType.Xml != dbType)
			{
				throw SQL.InvalidSqlDbTypeForConstructor(dbType);
			}
			this.SetDefaultsForType(dbType);
			if (SqlDbType.NText == dbType || SqlDbType.Text == dbType)
			{
				this._lLocale = (long)CultureInfo.CurrentCulture.LCID;
			}
			this._strName = name;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000D908 File Offset: 0x0000BB08
		private void Construct(string name, SqlDbType dbType, long maxLength, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			long lLocale = 0L;
			if (SqlDbType.Char == dbType)
			{
				if (maxLength > 8000L || maxLength < 0L)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
				lLocale = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.VarChar == dbType)
			{
				if ((maxLength > 8000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
				lLocale = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.NChar == dbType)
			{
				if (maxLength > 4000L || maxLength < 0L)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
				lLocale = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.NVarChar == dbType)
			{
				if ((maxLength > 4000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
				lLocale = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.NText == dbType || SqlDbType.Text == dbType)
			{
				if (SqlMetaData.Max != maxLength)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
				lLocale = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.Binary == dbType)
			{
				if (maxLength > 8000L || maxLength < 0L)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
			}
			else if (SqlDbType.VarBinary == dbType)
			{
				if ((maxLength > 8000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
			}
			else
			{
				if (SqlDbType.Image != dbType)
				{
					throw SQL.InvalidSqlDbTypeForConstructor(dbType);
				}
				if (SqlMetaData.Max != maxLength)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
			}
			this.SetDefaultsForType(dbType);
			this._strName = name;
			this._lMaxLength = maxLength;
			this._lLocale = lLocale;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000DBBC File Offset: 0x0000BDBC
		private void Construct(string name, SqlDbType dbType, long maxLength, long locale, SqlCompareOptions compareOptions, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Char == dbType)
			{
				if (maxLength > 8000L || maxLength < 0L)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
			}
			else if (SqlDbType.VarChar == dbType)
			{
				if ((maxLength > 8000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
			}
			else if (SqlDbType.NChar == dbType)
			{
				if (maxLength > 4000L || maxLength < 0L)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
			}
			else if (SqlDbType.NVarChar == dbType)
			{
				if ((maxLength > 4000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
			}
			else
			{
				if (SqlDbType.NText != dbType && SqlDbType.Text != dbType)
				{
					throw SQL.InvalidSqlDbTypeForConstructor(dbType);
				}
				if (SqlMetaData.Max != maxLength)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
			}
			if (SqlCompareOptions.BinarySort != compareOptions && (~(SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreNonSpace | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth) & compareOptions) != SqlCompareOptions.None)
			{
				throw ADP.InvalidEnumerationValue(typeof(SqlCompareOptions), (int)compareOptions);
			}
			this.SetDefaultsForType(dbType);
			this._strName = name;
			this._lMaxLength = maxLength;
			this._lLocale = locale;
			this._eCompareOptions = compareOptions;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000DD94 File Offset: 0x0000BF94
		private void Construct(string name, SqlDbType dbType, byte precision, byte scale, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Decimal == dbType)
			{
				if (precision > SqlDecimal.MaxPrecision || scale > precision)
				{
					throw SQL.PrecisionValueOutOfRange(precision);
				}
				if (scale > SqlDecimal.MaxScale)
				{
					throw SQL.ScaleValueOutOfRange(scale);
				}
			}
			else
			{
				if (SqlDbType.Time != dbType && SqlDbType.DateTime2 != dbType && SqlDbType.DateTimeOffset != dbType)
				{
					throw SQL.InvalidSqlDbTypeForConstructor(dbType);
				}
				if (scale > 7)
				{
					throw SQL.TimeScaleValueOutOfRange(scale);
				}
			}
			this.SetDefaultsForType(dbType);
			this._strName = name;
			this._bPrecision = precision;
			this._bScale = scale;
			if (SqlDbType.Decimal == dbType)
			{
				this._lMaxLength = (long)((ulong)SqlMetaData.s_maxLenFromPrecision[(int)(precision - 1)]);
			}
			else
			{
				this._lMaxLength -= (long)((ulong)SqlMetaData.s_maxVarTimeLenOffsetFromScale[(int)scale]);
			}
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000DE68 File Offset: 0x0000C068
		private void Construct(string name, SqlDbType dbType, Type userDefinedType, string serverTypeName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Udt != dbType)
			{
				throw SQL.InvalidSqlDbTypeForConstructor(dbType);
			}
			if (null == userDefinedType)
			{
				throw ADP.ArgumentNull("userDefinedType");
			}
			this.SetDefaultsForType(SqlDbType.Udt);
			this._strName = name;
			this._lMaxLength = (long)SerializationHelperSql9.GetUdtMaxLength(userDefinedType);
			this._udtType = userDefinedType;
			this._serverTypeName = serverTypeName;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
		private void Construct(string name, SqlDbType dbType, string database, string owningSchema, string objectName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Xml != dbType)
			{
				throw SQL.InvalidSqlDbTypeForConstructor(dbType);
			}
			if ((database != null || owningSchema != null) && objectName == null)
			{
				throw ADP.ArgumentNull("objectName");
			}
			this.SetDefaultsForType(SqlDbType.Xml);
			this._strName = name;
			this._xmlSchemaCollectionDatabase = database;
			this._xmlSchemaCollectionOwningSchema = owningSchema;
			this._xmlSchemaCollectionName = objectName;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000DF7A File Offset: 0x0000C17A
		private void AssertNameIsValid(string name)
		{
			if (name == null)
			{
				throw ADP.ArgumentNull("name");
			}
			if (128L < (long)name.Length)
			{
				throw SQL.NameTooLong("name");
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		private void ValidateSortOrder(SortOrder columnSortOrder, int sortOrdinal)
		{
			if (SortOrder.Unspecified != columnSortOrder && columnSortOrder != SortOrder.Ascending && SortOrder.Descending != columnSortOrder)
			{
				throw SQL.InvalidSortOrder(columnSortOrder);
			}
			if (SortOrder.Unspecified == columnSortOrder != (-1 == sortOrdinal))
			{
				throw SQL.MustSpecifyBothSortOrderAndOrdinal(columnSortOrder, sortOrdinal);
			}
		}

		/// <summary>Validates the specified <see cref="T:System.Int16" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Int16" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003C4 RID: 964 RVA: 0x0000DFCA File Offset: 0x0000C1CA
		public short Adjust(short value)
		{
			if (SqlDbType.SmallInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Int32" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Int32" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003C5 RID: 965 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		public int Adjust(int value)
		{
			if (SqlDbType.Int != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Int64" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Int64" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003C6 RID: 966 RVA: 0x0000DFED File Offset: 0x0000C1ED
		public long Adjust(long value)
		{
			if (this.SqlDbType != SqlDbType.BigInt)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Single" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Single" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003C7 RID: 967 RVA: 0x0000DFFD File Offset: 0x0000C1FD
		public float Adjust(float value)
		{
			if (SqlDbType.Real != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Double" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Double" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003C8 RID: 968 RVA: 0x0000E00F File Offset: 0x0000C20F
		public double Adjust(double value)
		{
			if (SqlDbType.Float != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.String" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.String" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003C9 RID: 969 RVA: 0x0000E020 File Offset: 0x0000C220
		public string Adjust(string value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (value != null && (long)value.Length < this.MaxLength)
				{
					value = value.PadRight((int)this.MaxLength);
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null)
			{
				return null;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value = value.Remove((int)this.MaxLength, (int)((long)value.Length - this.MaxLength));
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Decimal" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003CA RID: 970 RVA: 0x0000E0D0 File Offset: 0x0000C2D0
		public decimal Adjust(decimal value)
		{
			if (SqlDbType.Decimal != this.SqlDbType && SqlDbType.Money != this.SqlDbType && SqlDbType.SmallMoney != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (SqlDbType.Decimal != this.SqlDbType)
			{
				this.VerifyMoneyRange(new SqlMoney(value));
				return value;
			}
			return this.InternalAdjustSqlDecimal(new SqlDecimal(value)).Value;
		}

		/// <summary>Validates the specified <see cref="T:System.DateTime" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003CB RID: 971 RVA: 0x0000E12C File Offset: 0x0000C32C
		public DateTime Adjust(DateTime value)
		{
			if (SqlDbType.DateTime == this.SqlDbType || SqlDbType.SmallDateTime == this.SqlDbType)
			{
				this.VerifyDateTimeRange(value);
			}
			else
			{
				if (SqlDbType.DateTime2 == this.SqlDbType)
				{
					return new DateTime(this.InternalAdjustTimeTicks(value.Ticks));
				}
				if (SqlDbType.Date == this.SqlDbType)
				{
					return value.Date;
				}
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Guid" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Guid" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003CC RID: 972 RVA: 0x0000E18A File Offset: 0x0000C38A
		public Guid Adjust(Guid value)
		{
			if (SqlDbType.UniqueIdentifier != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003CD RID: 973 RVA: 0x0000E19C File Offset: 0x0000C39C
		public SqlBoolean Adjust(SqlBoolean value)
		{
			if (SqlDbType.Bit != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlByte" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003CE RID: 974 RVA: 0x0000E1AD File Offset: 0x0000C3AD
		public SqlByte Adjust(SqlByte value)
		{
			if (SqlDbType.TinyInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlInt16" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003CF RID: 975 RVA: 0x0000DFCA File Offset: 0x0000C1CA
		public SqlInt16 Adjust(SqlInt16 value)
		{
			if (SqlDbType.SmallInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003D0 RID: 976 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		public SqlInt32 Adjust(SqlInt32 value)
		{
			if (SqlDbType.Int != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlInt64" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003D1 RID: 977 RVA: 0x0000DFED File Offset: 0x0000C1ED
		public SqlInt64 Adjust(SqlInt64 value)
		{
			if (this.SqlDbType != SqlDbType.BigInt)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003D2 RID: 978 RVA: 0x0000DFFD File Offset: 0x0000C1FD
		public SqlSingle Adjust(SqlSingle value)
		{
			if (SqlDbType.Real != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlDouble" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlDouble" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003D3 RID: 979 RVA: 0x0000E00F File Offset: 0x0000C20F
		public SqlDouble Adjust(SqlDouble value)
		{
			if (SqlDbType.Float != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlMoney" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003D4 RID: 980 RVA: 0x0000E1BF File Offset: 0x0000C3BF
		public SqlMoney Adjust(SqlMoney value)
		{
			if (SqlDbType.Money != this.SqlDbType && SqlDbType.SmallMoney != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (!value.IsNull)
			{
				this.VerifyMoneyRange(value);
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003D5 RID: 981 RVA: 0x0000E1EB File Offset: 0x0000C3EB
		public SqlDateTime Adjust(SqlDateTime value)
		{
			if (SqlDbType.DateTime != this.SqlDbType && SqlDbType.SmallDateTime != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (!value.IsNull)
			{
				this.VerifyDateTimeRange(value.Value);
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003D6 RID: 982 RVA: 0x0000E21C File Offset: 0x0000C41C
		public SqlDecimal Adjust(SqlDecimal value)
		{
			if (SqlDbType.Decimal != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return this.InternalAdjustSqlDecimal(value);
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlString" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003D7 RID: 983 RVA: 0x0000E234 File Offset: 0x0000C434
		public SqlString Adjust(SqlString value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (!value.IsNull && (long)value.Value.Length < this.MaxLength)
				{
					return new SqlString(value.Value.PadRight((int)this.MaxLength));
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value.IsNull)
			{
				return value;
			}
			if ((long)value.Value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value = new SqlString(value.Value.Remove((int)this.MaxLength, (int)((long)value.Value.Length - this.MaxLength)));
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlBinary" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlBinary" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003D8 RID: 984 RVA: 0x0000E318 File Offset: 0x0000C518
		public SqlBinary Adjust(SqlBinary value)
		{
			if (SqlDbType.Binary == this.SqlDbType || SqlDbType.Timestamp == this.SqlDbType)
			{
				if (!value.IsNull && (long)value.Length < this.MaxLength)
				{
					byte[] value2 = value.Value;
					byte[] array = new byte[this.MaxLength];
					Buffer.BlockCopy(value2, 0, array, 0, value2.Length);
					Array.Clear(array, value2.Length, array.Length - value2.Length);
					return new SqlBinary(array);
				}
			}
			else if (SqlDbType.VarBinary != this.SqlDbType && SqlDbType.Image != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value.IsNull)
			{
				return value;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				Array value3 = value.Value;
				byte[] array2 = new byte[this.MaxLength];
				Buffer.BlockCopy(value3, 0, array2, 0, (int)this.MaxLength);
				value = new SqlBinary(array2);
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlGuid" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlGuid" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003D9 RID: 985 RVA: 0x0000E18A File Offset: 0x0000C38A
		public SqlGuid Adjust(SqlGuid value)
		{
			if (SqlDbType.UniqueIdentifier != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlChars" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlChars" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003DA RID: 986 RVA: 0x0000E3F8 File Offset: 0x0000C5F8
		public SqlChars Adjust(SqlChars value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (value != null && !value.IsNull)
				{
					long length = value.Length;
					if (length < this.MaxLength)
					{
						if (value.MaxLength < this.MaxLength)
						{
							char[] array = new char[(int)this.MaxLength];
							Buffer.BlockCopy(value.Buffer, 0, array, 0, (int)length);
							value = new SqlChars(array);
						}
						char[] buffer = value.Buffer;
						for (long num = length; num < this.MaxLength; num += 1L)
						{
							buffer[(int)(checked((IntPtr)num))] = ' ';
						}
						value.SetLength(this.MaxLength);
						return value;
					}
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null || value.IsNull)
			{
				return value;
			}
			if (value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value.SetLength(this.MaxLength);
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlBytes" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlBytes" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003DB RID: 987 RVA: 0x0000E504 File Offset: 0x0000C704
		public SqlBytes Adjust(SqlBytes value)
		{
			if (SqlDbType.Binary == this.SqlDbType || SqlDbType.Timestamp == this.SqlDbType)
			{
				if (value != null && !value.IsNull)
				{
					int num = (int)value.Length;
					if ((long)num < this.MaxLength)
					{
						if (value.MaxLength < this.MaxLength)
						{
							byte[] array = new byte[this.MaxLength];
							Buffer.BlockCopy(value.Buffer, 0, array, 0, num);
							value = new SqlBytes(array);
						}
						byte[] buffer = value.Buffer;
						Array.Clear(buffer, num, buffer.Length - num);
						value.SetLength(this.MaxLength);
						return value;
					}
				}
			}
			else if (SqlDbType.VarBinary != this.SqlDbType && SqlDbType.Image != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null || value.IsNull)
			{
				return value;
			}
			if (value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value.SetLength(this.MaxLength);
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlXml" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlXml" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003DC RID: 988 RVA: 0x0000E5E4 File Offset: 0x0000C7E4
		public SqlXml Adjust(SqlXml value)
		{
			if (SqlDbType.Xml != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.TimeSpan" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as an array of <see cref="T:System.TimeSpan" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003DD RID: 989 RVA: 0x0000E5F6 File Offset: 0x0000C7F6
		public TimeSpan Adjust(TimeSpan value)
		{
			if (SqlDbType.Time != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			this.VerifyTimeRange(value);
			return new TimeSpan(this.InternalAdjustTimeTicks(value.Ticks));
		}

		/// <summary>Validates the specified <see cref="T:System.DateTimeOffset" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as an array of <see cref="T:System.DateTimeOffset" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003DE RID: 990 RVA: 0x0000E620 File Offset: 0x0000C820
		public DateTimeOffset Adjust(DateTimeOffset value)
		{
			if (SqlDbType.DateTimeOffset != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return new DateTimeOffset(this.InternalAdjustTimeTicks(value.Ticks), value.Offset);
		}

		/// <summary>Validates the specified <see cref="T:System.Object" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Object" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003DF RID: 991 RVA: 0x0000E64C File Offset: 0x0000C84C
		public object Adjust(object value)
		{
			if (value == null)
			{
				return null;
			}
			Type type = value.GetType();
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Empty:
				throw ADP.InvalidDataType(TypeCode.Empty);
			case TypeCode.Object:
				if (type == typeof(byte[]))
				{
					return this.Adjust((byte[])value);
				}
				if (type == typeof(char[]))
				{
					return this.Adjust((char[])value);
				}
				if (type == typeof(Guid))
				{
					return this.Adjust((Guid)value);
				}
				if (type == typeof(object))
				{
					throw ADP.InvalidDataType(TypeCode.UInt64);
				}
				if (type == typeof(SqlBinary))
				{
					return this.Adjust((SqlBinary)value);
				}
				if (type == typeof(SqlBoolean))
				{
					return this.Adjust((SqlBoolean)value);
				}
				if (type == typeof(SqlByte))
				{
					return this.Adjust((SqlByte)value);
				}
				if (type == typeof(SqlDateTime))
				{
					return this.Adjust((SqlDateTime)value);
				}
				if (type == typeof(SqlDouble))
				{
					return this.Adjust((SqlDouble)value);
				}
				if (type == typeof(SqlGuid))
				{
					return this.Adjust((SqlGuid)value);
				}
				if (type == typeof(SqlInt16))
				{
					return this.Adjust((SqlInt16)value);
				}
				if (type == typeof(SqlInt32))
				{
					return this.Adjust((SqlInt32)value);
				}
				if (type == typeof(SqlInt64))
				{
					return this.Adjust((SqlInt64)value);
				}
				if (type == typeof(SqlMoney))
				{
					return this.Adjust((SqlMoney)value);
				}
				if (type == typeof(SqlDecimal))
				{
					return this.Adjust((SqlDecimal)value);
				}
				if (type == typeof(SqlSingle))
				{
					return this.Adjust((SqlSingle)value);
				}
				if (type == typeof(SqlString))
				{
					return this.Adjust((SqlString)value);
				}
				if (type == typeof(SqlChars))
				{
					return this.Adjust((SqlChars)value);
				}
				if (type == typeof(SqlBytes))
				{
					return this.Adjust((SqlBytes)value);
				}
				if (type == typeof(SqlXml))
				{
					return this.Adjust((SqlXml)value);
				}
				if (type == typeof(TimeSpan))
				{
					return this.Adjust((TimeSpan)value);
				}
				if (type == typeof(DateTimeOffset))
				{
					return this.Adjust((DateTimeOffset)value);
				}
				throw ADP.UnknownDataType(type);
			case TypeCode.DBNull:
				return value;
			case TypeCode.Boolean:
				return this.Adjust((bool)value);
			case TypeCode.Char:
				return this.Adjust((char)value);
			case TypeCode.SByte:
				throw ADP.InvalidDataType(TypeCode.SByte);
			case TypeCode.Byte:
				return this.Adjust((byte)value);
			case TypeCode.Int16:
				return this.Adjust((short)value);
			case TypeCode.UInt16:
				throw ADP.InvalidDataType(TypeCode.UInt16);
			case TypeCode.Int32:
				return this.Adjust((int)value);
			case TypeCode.UInt32:
				throw ADP.InvalidDataType(TypeCode.UInt32);
			case TypeCode.Int64:
				return this.Adjust((long)value);
			case TypeCode.UInt64:
				throw ADP.InvalidDataType(TypeCode.UInt64);
			case TypeCode.Single:
				return this.Adjust((float)value);
			case TypeCode.Double:
				return this.Adjust((double)value);
			case TypeCode.Decimal:
				return this.Adjust((decimal)value);
			case TypeCode.DateTime:
				return this.Adjust((DateTime)value);
			case TypeCode.String:
				return this.Adjust((string)value);
			}
			throw ADP.UnknownDataTypeCode(type, Type.GetTypeCode(type));
		}

		/// <summary>Infers the metadata from the specified object and returns it as a <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</summary>
		/// <param name="value">The object used from which the metadata is inferred.</param>
		/// <param name="name">The name assigned to the returned <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The inferred metadata as a <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x060003E0 RID: 992 RVA: 0x0000EB6C File Offset: 0x0000CD6C
		public static SqlMetaData InferFromValue(object value, string name)
		{
			if (value == null)
			{
				throw ADP.ArgumentNull("value");
			}
			Type type = value.GetType();
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Empty:
				throw ADP.InvalidDataType(TypeCode.Empty);
			case TypeCode.Object:
				if (type == typeof(byte[]))
				{
					long num = (long)((byte[])value).Length;
					if (num < 1L)
					{
						num = 1L;
					}
					if (8000L < num)
					{
						num = SqlMetaData.Max;
					}
					return new SqlMetaData(name, SqlDbType.VarBinary, num);
				}
				if (type == typeof(char[]))
				{
					long num2 = (long)((char[])value).Length;
					if (num2 < 1L)
					{
						num2 = 1L;
					}
					if (4000L < num2)
					{
						num2 = SqlMetaData.Max;
					}
					return new SqlMetaData(name, SqlDbType.NVarChar, num2);
				}
				if (type == typeof(Guid))
				{
					return new SqlMetaData(name, SqlDbType.UniqueIdentifier);
				}
				if (type == typeof(object))
				{
					return new SqlMetaData(name, SqlDbType.Variant);
				}
				if (type == typeof(SqlBinary))
				{
					SqlBinary sqlBinary = (SqlBinary)value;
					long num3;
					if (!sqlBinary.IsNull)
					{
						num3 = (long)sqlBinary.Length;
						if (num3 < 1L)
						{
							num3 = 1L;
						}
						if (8000L < num3)
						{
							num3 = SqlMetaData.Max;
						}
					}
					else
					{
						num3 = SqlMetaData.sxm_rgDefaults[21].MaxLength;
					}
					return new SqlMetaData(name, SqlDbType.VarBinary, num3);
				}
				if (type == typeof(SqlBoolean))
				{
					return new SqlMetaData(name, SqlDbType.Bit);
				}
				if (type == typeof(SqlByte))
				{
					return new SqlMetaData(name, SqlDbType.TinyInt);
				}
				if (type == typeof(SqlDateTime))
				{
					return new SqlMetaData(name, SqlDbType.DateTime);
				}
				if (type == typeof(SqlDouble))
				{
					return new SqlMetaData(name, SqlDbType.Float);
				}
				if (type == typeof(SqlGuid))
				{
					return new SqlMetaData(name, SqlDbType.UniqueIdentifier);
				}
				if (type == typeof(SqlInt16))
				{
					return new SqlMetaData(name, SqlDbType.SmallInt);
				}
				if (type == typeof(SqlInt32))
				{
					return new SqlMetaData(name, SqlDbType.Int);
				}
				if (type == typeof(SqlInt64))
				{
					return new SqlMetaData(name, SqlDbType.BigInt);
				}
				if (type == typeof(SqlMoney))
				{
					return new SqlMetaData(name, SqlDbType.Money);
				}
				if (type == typeof(SqlDecimal))
				{
					SqlDecimal sqlDecimal = (SqlDecimal)value;
					byte precision;
					byte scale;
					if (!sqlDecimal.IsNull)
					{
						precision = sqlDecimal.Precision;
						scale = sqlDecimal.Scale;
					}
					else
					{
						precision = SqlMetaData.sxm_rgDefaults[5].Precision;
						scale = SqlMetaData.sxm_rgDefaults[5].Scale;
					}
					return new SqlMetaData(name, SqlDbType.Decimal, precision, scale);
				}
				if (type == typeof(SqlSingle))
				{
					return new SqlMetaData(name, SqlDbType.Real);
				}
				if (type == typeof(SqlString))
				{
					SqlString sqlString = (SqlString)value;
					if (!sqlString.IsNull)
					{
						long num4 = (long)sqlString.Value.Length;
						if (num4 < 1L)
						{
							num4 = 1L;
						}
						if (num4 > 4000L)
						{
							num4 = SqlMetaData.Max;
						}
						return new SqlMetaData(name, SqlDbType.NVarChar, num4, (long)sqlString.LCID, sqlString.SqlCompareOptions);
					}
					return new SqlMetaData(name, SqlDbType.NVarChar, SqlMetaData.sxm_rgDefaults[12].MaxLength);
				}
				else
				{
					if (type == typeof(SqlChars))
					{
						SqlChars sqlChars = (SqlChars)value;
						long num5;
						if (!sqlChars.IsNull)
						{
							num5 = sqlChars.Length;
							if (num5 < 1L)
							{
								num5 = 1L;
							}
							if (num5 > 4000L)
							{
								num5 = SqlMetaData.Max;
							}
						}
						else
						{
							num5 = SqlMetaData.sxm_rgDefaults[12].MaxLength;
						}
						return new SqlMetaData(name, SqlDbType.NVarChar, num5);
					}
					if (type == typeof(SqlBytes))
					{
						SqlBytes sqlBytes = (SqlBytes)value;
						long num6;
						if (!sqlBytes.IsNull)
						{
							num6 = sqlBytes.Length;
							if (num6 < 1L)
							{
								num6 = 1L;
							}
							else if (8000L < num6)
							{
								num6 = SqlMetaData.Max;
							}
						}
						else
						{
							num6 = SqlMetaData.sxm_rgDefaults[21].MaxLength;
						}
						return new SqlMetaData(name, SqlDbType.VarBinary, num6);
					}
					if (type == typeof(SqlXml))
					{
						return new SqlMetaData(name, SqlDbType.Xml);
					}
					if (type == typeof(TimeSpan))
					{
						return new SqlMetaData(name, SqlDbType.Time, 0, SqlMetaData.InferScaleFromTimeTicks(((TimeSpan)value).Ticks));
					}
					if (type == typeof(DateTimeOffset))
					{
						return new SqlMetaData(name, SqlDbType.DateTimeOffset, 0, SqlMetaData.InferScaleFromTimeTicks(((DateTimeOffset)value).Ticks));
					}
					throw ADP.UnknownDataType(type);
				}
				break;
			case TypeCode.DBNull:
				throw ADP.InvalidDataType(TypeCode.DBNull);
			case TypeCode.Boolean:
				return new SqlMetaData(name, SqlDbType.Bit);
			case TypeCode.Char:
				return new SqlMetaData(name, SqlDbType.NVarChar, 1L);
			case TypeCode.SByte:
				throw ADP.InvalidDataType(TypeCode.SByte);
			case TypeCode.Byte:
				return new SqlMetaData(name, SqlDbType.TinyInt);
			case TypeCode.Int16:
				return new SqlMetaData(name, SqlDbType.SmallInt);
			case TypeCode.UInt16:
				throw ADP.InvalidDataType(TypeCode.UInt16);
			case TypeCode.Int32:
				return new SqlMetaData(name, SqlDbType.Int);
			case TypeCode.UInt32:
				throw ADP.InvalidDataType(TypeCode.UInt32);
			case TypeCode.Int64:
				return new SqlMetaData(name, SqlDbType.BigInt);
			case TypeCode.UInt64:
				throw ADP.InvalidDataType(TypeCode.UInt64);
			case TypeCode.Single:
				return new SqlMetaData(name, SqlDbType.Real);
			case TypeCode.Double:
				return new SqlMetaData(name, SqlDbType.Float);
			case TypeCode.Decimal:
			{
				SqlDecimal sqlDecimal2 = new SqlDecimal((decimal)value);
				return new SqlMetaData(name, SqlDbType.Decimal, sqlDecimal2.Precision, sqlDecimal2.Scale);
			}
			case TypeCode.DateTime:
				return new SqlMetaData(name, SqlDbType.DateTime);
			case TypeCode.String:
			{
				long num7 = (long)((string)value).Length;
				if (num7 < 1L)
				{
					num7 = 1L;
				}
				if (4000L < num7)
				{
					num7 = SqlMetaData.Max;
				}
				return new SqlMetaData(name, SqlDbType.NVarChar, num7);
			}
			}
			throw ADP.UnknownDataTypeCode(type, Type.GetTypeCode(type));
		}

		/// <summary>Validates the specified <see cref="T:System.Boolean" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Boolean" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003E1 RID: 993 RVA: 0x0000E19C File Offset: 0x0000C39C
		public bool Adjust(bool value)
		{
			if (SqlDbType.Bit != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Byte" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Byte" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003E2 RID: 994 RVA: 0x0000E1AD File Offset: 0x0000C3AD
		public byte Adjust(byte value)
		{
			if (SqlDbType.TinyInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified array of <see cref="T:System.Byte" /> values against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as an array of <see cref="T:System.Byte" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003E3 RID: 995 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
		public byte[] Adjust(byte[] value)
		{
			if (SqlDbType.Binary == this.SqlDbType || SqlDbType.Timestamp == this.SqlDbType)
			{
				if (value != null && (long)value.Length < this.MaxLength)
				{
					byte[] array = new byte[this.MaxLength];
					Buffer.BlockCopy(value, 0, array, 0, value.Length);
					Array.Clear(array, value.Length, array.Length - value.Length);
					return array;
				}
			}
			else if (SqlDbType.VarBinary != this.SqlDbType && SqlDbType.Image != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null)
			{
				return null;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				byte[] array2 = new byte[this.MaxLength];
				Buffer.BlockCopy(value, 0, array2, 0, (int)this.MaxLength);
				value = array2;
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Char" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as a <see cref="T:System.Char" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003E4 RID: 996 RVA: 0x0000F274 File Offset: 0x0000D474
		public char Adjust(char value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (1L != this.MaxLength)
				{
					SqlMetaData.ThrowInvalidType();
				}
			}
			else if (1L > this.MaxLength || (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType))
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified array of <see cref="T:System.Char" /> values against the metadata, and adjusts the value if necessary.</summary>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <returns>The adjusted value as an array <see cref="T:System.Char" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
		// Token: 0x060003E5 RID: 997 RVA: 0x0000F2E0 File Offset: 0x0000D4E0
		public char[] Adjust(char[] value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (value != null)
				{
					long num = (long)value.Length;
					if (num < this.MaxLength)
					{
						char[] array = new char[(int)this.MaxLength];
						Buffer.BlockCopy(value, 0, array, 0, (int)num);
						for (long num2 = num; num2 < (long)array.Length; num2 += 1L)
						{
							array[(int)(checked((IntPtr)num2))] = ' ';
						}
						return array;
					}
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null)
			{
				return null;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				char[] array2 = new char[this.MaxLength];
				Buffer.BlockCopy(value, 0, array2, 0, (int)this.MaxLength);
				value = array2;
			}
			return value;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		internal static SqlMetaData GetPartialLengthMetaData(SqlMetaData md)
		{
			if (md.IsPartialLength)
			{
				return md;
			}
			if (md.SqlDbType == SqlDbType.Xml)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (md.SqlDbType == SqlDbType.NVarChar || md.SqlDbType == SqlDbType.VarChar || md.SqlDbType == SqlDbType.VarBinary)
			{
				return new SqlMetaData(md.Name, md.SqlDbType, SqlMetaData.Max, 0, 0, md.LocaleId, md.CompareOptions, null, null, null, true, md.Type);
			}
			return md;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000F424 File Offset: 0x0000D624
		private static void ThrowInvalidType()
		{
			throw ADP.InvalidMetaDataValue();
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000F42B File Offset: 0x0000D62B
		private void VerifyDateTimeRange(DateTime value)
		{
			if (SqlDbType.SmallDateTime == this.SqlDbType && (SqlMetaData.s_dtSmallMax < value || SqlMetaData.s_dtSmallMin > value))
			{
				SqlMetaData.ThrowInvalidType();
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000F458 File Offset: 0x0000D658
		private void VerifyMoneyRange(SqlMoney value)
		{
			if (SqlDbType.SmallMoney == this.SqlDbType && ((SqlMetaData.s_smSmallMax < value).Value || (SqlMetaData.s_smSmallMin > value).Value))
			{
				SqlMetaData.ThrowInvalidType();
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
		private SqlDecimal InternalAdjustSqlDecimal(SqlDecimal value)
		{
			if (!value.IsNull && (value.Precision != this.Precision || value.Scale != this.Scale))
			{
				if (value.Scale != this.Scale)
				{
					value = SqlDecimal.AdjustScale(value, (int)(this.Scale - value.Scale), false);
				}
				return SqlDecimal.ConvertToPrecScale(value, (int)this.Precision, (int)this.Scale);
			}
			return value;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000F50E File Offset: 0x0000D70E
		private void VerifyTimeRange(TimeSpan value)
		{
			if (SqlDbType.Time == this.SqlDbType && (SqlMetaData.s_timeMin > value || value > SqlMetaData.s_timeMax))
			{
				SqlMetaData.ThrowInvalidType();
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000F539 File Offset: 0x0000D739
		private long InternalAdjustTimeTicks(long ticks)
		{
			return ticks / SqlMetaData.s_unitTicksFromScale[(int)this.Scale] * SqlMetaData.s_unitTicksFromScale[(int)this.Scale];
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000F558 File Offset: 0x0000D758
		private static byte InferScaleFromTimeTicks(long ticks)
		{
			for (byte b = 0; b < 7; b += 1)
			{
				if (ticks / SqlMetaData.s_unitTicksFromScale[(int)b] * SqlMetaData.s_unitTicksFromScale[(int)b] == ticks)
				{
					return b;
				}
			}
			return 7;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000F58C File Offset: 0x0000D78C
		private void SetDefaultsForType(SqlDbType dbType)
		{
			if (SqlDbType.BigInt <= dbType && SqlDbType.DateTimeOffset >= dbType)
			{
				SqlMetaData sqlMetaData = SqlMetaData.sxm_rgDefaults[(int)dbType];
				this._sqlDbType = dbType;
				this._lMaxLength = sqlMetaData.MaxLength;
				this._bPrecision = sqlMetaData.Precision;
				this._bScale = sqlMetaData.Scale;
				this._lLocale = sqlMetaData.LocaleId;
				this._eCompareOptions = sqlMetaData.CompareOptions;
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000F5F0 File Offset: 0x0000D7F0
		// Note: this type is marked as 'beforefieldinit'.
		static SqlMetaData()
		{
		}

		// Token: 0x04000505 RID: 1285
		private string _strName;

		// Token: 0x04000506 RID: 1286
		private long _lMaxLength;

		// Token: 0x04000507 RID: 1287
		private SqlDbType _sqlDbType;

		// Token: 0x04000508 RID: 1288
		private byte _bPrecision;

		// Token: 0x04000509 RID: 1289
		private byte _bScale;

		// Token: 0x0400050A RID: 1290
		private long _lLocale;

		// Token: 0x0400050B RID: 1291
		private SqlCompareOptions _eCompareOptions;

		// Token: 0x0400050C RID: 1292
		private string _xmlSchemaCollectionDatabase;

		// Token: 0x0400050D RID: 1293
		private string _xmlSchemaCollectionOwningSchema;

		// Token: 0x0400050E RID: 1294
		private string _xmlSchemaCollectionName;

		// Token: 0x0400050F RID: 1295
		private string _serverTypeName;

		// Token: 0x04000510 RID: 1296
		private bool _bPartialLength;

		// Token: 0x04000511 RID: 1297
		private Type _udtType;

		// Token: 0x04000512 RID: 1298
		private bool _useServerDefault;

		// Token: 0x04000513 RID: 1299
		private bool _isUniqueKey;

		// Token: 0x04000514 RID: 1300
		private SortOrder _columnSortOrder;

		// Token: 0x04000515 RID: 1301
		private int _sortOrdinal;

		// Token: 0x04000516 RID: 1302
		private const long x_lMax = -1L;

		// Token: 0x04000517 RID: 1303
		private const long x_lServerMaxUnicode = 4000L;

		// Token: 0x04000518 RID: 1304
		private const long x_lServerMaxANSI = 8000L;

		// Token: 0x04000519 RID: 1305
		private const long x_lServerMaxBinary = 8000L;

		// Token: 0x0400051A RID: 1306
		private const bool x_defaultUseServerDefault = false;

		// Token: 0x0400051B RID: 1307
		private const bool x_defaultIsUniqueKey = false;

		// Token: 0x0400051C RID: 1308
		private const SortOrder x_defaultColumnSortOrder = SortOrder.Unspecified;

		// Token: 0x0400051D RID: 1309
		private const int x_defaultSortOrdinal = -1;

		// Token: 0x0400051E RID: 1310
		private const SqlCompareOptions x_eDefaultStringCompareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;

		// Token: 0x0400051F RID: 1311
		private static byte[] s_maxLenFromPrecision = new byte[]
		{
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			9,
			9,
			9,
			9,
			9,
			9,
			9,
			9,
			9,
			9,
			13,
			13,
			13,
			13,
			13,
			13,
			13,
			13,
			13,
			17,
			17,
			17,
			17,
			17,
			17,
			17,
			17,
			17,
			17
		};

		// Token: 0x04000520 RID: 1312
		private const byte MaxTimeScale = 7;

		// Token: 0x04000521 RID: 1313
		private static byte[] s_maxVarTimeLenOffsetFromScale = new byte[]
		{
			2,
			2,
			2,
			1,
			1,
			0,
			0,
			0
		};

		// Token: 0x04000522 RID: 1314
		private static readonly DateTime s_dtSmallMax = new DateTime(2079, 6, 6, 23, 59, 29, 998);

		// Token: 0x04000523 RID: 1315
		private static readonly DateTime s_dtSmallMin = new DateTime(1899, 12, 31, 23, 59, 29, 999);

		// Token: 0x04000524 RID: 1316
		private static readonly SqlMoney s_smSmallMax = new SqlMoney(214748.3647m);

		// Token: 0x04000525 RID: 1317
		private static readonly SqlMoney s_smSmallMin = new SqlMoney(-214748.3648m);

		// Token: 0x04000526 RID: 1318
		private static readonly TimeSpan s_timeMin = TimeSpan.Zero;

		// Token: 0x04000527 RID: 1319
		private static readonly TimeSpan s_timeMax = new TimeSpan(863999999999L);

		// Token: 0x04000528 RID: 1320
		private static readonly long[] s_unitTicksFromScale = new long[]
		{
			10000000L,
			1000000L,
			100000L,
			10000L,
			1000L,
			100L,
			10L,
			1L
		};

		// Token: 0x04000529 RID: 1321
		private static DbType[] sxm_rgSqlDbTypeToDbType = new DbType[]
		{
			DbType.Int64,
			DbType.Binary,
			DbType.Boolean,
			DbType.AnsiString,
			DbType.DateTime,
			DbType.Decimal,
			DbType.Double,
			DbType.Binary,
			DbType.Int32,
			DbType.Currency,
			DbType.String,
			DbType.String,
			DbType.String,
			DbType.Single,
			DbType.Guid,
			DbType.DateTime,
			DbType.Int16,
			DbType.Currency,
			DbType.AnsiString,
			DbType.Binary,
			DbType.Byte,
			DbType.Binary,
			DbType.AnsiString,
			DbType.Object,
			DbType.Object,
			DbType.Xml,
			DbType.String,
			DbType.String,
			DbType.String,
			DbType.Object,
			DbType.Object,
			DbType.Date,
			DbType.Time,
			DbType.DateTime2,
			DbType.DateTimeOffset
		};

		// Token: 0x0400052A RID: 1322
		internal static SqlMetaData[] sxm_rgDefaults = new SqlMetaData[]
		{
			new SqlMetaData("bigint", SqlDbType.BigInt, 8L, 19, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("binary", SqlDbType.Binary, 1L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("bit", SqlDbType.Bit, 1L, 1, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("char", SqlDbType.Char, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("datetime", SqlDbType.DateTime, 8L, 23, 3, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("decimal", SqlDbType.Decimal, 9L, 18, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("float", SqlDbType.Float, 8L, 53, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("image", SqlDbType.Image, -1L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("int", SqlDbType.Int, 4L, 10, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("money", SqlDbType.Money, 8L, 19, 4, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("nchar", SqlDbType.NChar, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("ntext", SqlDbType.NText, -1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 4000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("real", SqlDbType.Real, 4L, 24, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("uniqueidentifier", SqlDbType.UniqueIdentifier, 16L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("smalldatetime", SqlDbType.SmallDateTime, 4L, 16, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("smallint", SqlDbType.SmallInt, 2L, 5, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("smallmoney", SqlDbType.SmallMoney, 4L, 10, 4, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("text", SqlDbType.Text, -1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("timestamp", SqlDbType.Timestamp, 8L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("tinyint", SqlDbType.TinyInt, 1L, 3, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("varbinary", SqlDbType.VarBinary, 8000L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("varchar", SqlDbType.VarChar, 8000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("sql_variant", SqlDbType.Variant, 8016L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("xml", SqlDbType.Xml, -1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, true),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 4000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 4000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("udt", SqlDbType.Udt, 0L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("table", SqlDbType.Structured, 0L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("date", SqlDbType.Date, 3L, 10, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("time", SqlDbType.Time, 5L, 0, 7, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("datetime2", SqlDbType.DateTime2, 8L, 0, 7, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("datetimeoffset", SqlDbType.DateTimeOffset, 10L, 0, 7, 0L, SqlCompareOptions.None, false)
		};
	}
}
