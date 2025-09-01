using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using Microsoft.SqlServer.Server;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Represents a parameter to a <see cref="T:System.Data.SqlClient.SqlCommand" /> and optionally its mapping to <see cref="T:System.Data.DataSet" /> columns. This class cannot be inherited. For more information on parameters, see Configuring Parameters and Parameter Data Types.</summary>
	// Token: 0x0200021A RID: 538
	[TypeConverter(typeof(SqlParameter.SqlParameterConverter))]
	public sealed class SqlParameter : DbParameter, IDbDataParameter, IDataParameter, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class.</summary>
		// Token: 0x060019C4 RID: 6596 RVA: 0x0007732A File Offset: 0x0007552A
		public SqlParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class that uses the parameter name and the data type.</summary>
		/// <param name="parameterName">The name of the parameter to map.</param>
		/// <param name="dbType">One of the <see cref="T:System.Data.SqlDbType" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dbType" /> parameter is an invalid back-end data type.</exception>
		// Token: 0x060019C5 RID: 6597 RVA: 0x00077340 File Offset: 0x00075540
		public SqlParameter(string parameterName, SqlDbType dbType) : this()
		{
			this.ParameterName = parameterName;
			this.SqlDbType = dbType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class that uses the parameter name and a value of the new <see cref="T:System.Data.SqlClient.SqlParameter" />.</summary>
		/// <param name="parameterName">The name of the parameter to map.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.SqlClient.SqlParameter" />.</param>
		// Token: 0x060019C6 RID: 6598 RVA: 0x00077356 File Offset: 0x00075556
		public SqlParameter(string parameterName, object value) : this()
		{
			this.ParameterName = parameterName;
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class that uses the parameter name, the <see cref="T:System.Data.SqlDbType" />, and the size.</summary>
		/// <param name="parameterName">The name of the parameter to map.</param>
		/// <param name="dbType">One of the <see cref="T:System.Data.SqlDbType" /> values.</param>
		/// <param name="size">The length of the parameter.</param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dbType" /> parameter is an invalid back-end data type.</exception>
		// Token: 0x060019C7 RID: 6599 RVA: 0x0007736C File Offset: 0x0007556C
		public SqlParameter(string parameterName, SqlDbType dbType, int size) : this()
		{
			this.ParameterName = parameterName;
			this.SqlDbType = dbType;
			this.Size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class that uses the parameter name, the <see cref="T:System.Data.SqlDbType" />, the size, and the source column name.</summary>
		/// <param name="parameterName">The name of the parameter to map.</param>
		/// <param name="dbType">One of the <see cref="T:System.Data.SqlDbType" /> values.</param>
		/// <param name="size">The length of the parameter.</param>
		/// <param name="sourceColumn">The name of the source column (<see cref="P:System.Data.SqlClient.SqlParameter.SourceColumn" />) if this <see cref="T:System.Data.SqlClient.SqlParameter" /> is used in a call to <see cref="Overload:System.Data.Common.DbDataAdapter.Update" />.</param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dbType" /> parameter is an invalid back-end data type.</exception>
		// Token: 0x060019C8 RID: 6600 RVA: 0x00077389 File Offset: 0x00075589
		public SqlParameter(string parameterName, SqlDbType dbType, int size, string sourceColumn) : this()
		{
			this.ParameterName = parameterName;
			this.SqlDbType = dbType;
			this.Size = size;
			this.SourceColumn = sourceColumn;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class that uses the parameter name, the type of the parameter, the size of the parameter, a <see cref="T:System.Data.ParameterDirection" />, the precision of the parameter, the scale of the parameter, the source column, a <see cref="T:System.Data.DataRowVersion" /> to use, and the value of the parameter.</summary>
		/// <param name="parameterName">The name of the parameter to map.</param>
		/// <param name="dbType">One of the <see cref="T:System.Data.SqlDbType" /> values.</param>
		/// <param name="size">The length of the parameter.</param>
		/// <param name="direction">One of the <see cref="T:System.Data.ParameterDirection" /> values.</param>
		/// <param name="isNullable">
		///   <see langword="true" /> if the value of the field can be null; otherwise, <see langword="false" />.</param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> is resolved.</param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> is resolved.</param>
		/// <param name="sourceColumn">The name of the source column (<see cref="P:System.Data.SqlClient.SqlParameter.SourceColumn" />) if this <see cref="T:System.Data.SqlClient.SqlParameter" /> is used in a call to <see cref="Overload:System.Data.Common.DbDataAdapter.Update" />.</param>
		/// <param name="sourceVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.SqlClient.SqlParameter" />.</param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dbType" /> parameter is an invalid back-end data type.</exception>
		// Token: 0x060019C9 RID: 6601 RVA: 0x000773AE File Offset: 0x000755AE
		public SqlParameter(string parameterName, SqlDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value) : this(parameterName, dbType, size, sourceColumn)
		{
			this.Direction = direction;
			this.IsNullable = isNullable;
			this.Precision = precision;
			this.Scale = scale;
			this.SourceVersion = sourceVersion;
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class that uses the parameter name, the type of the parameter, the length of the parameter the direction, the precision, the scale, the name of the source column, one of the <see cref="T:System.Data.DataRowVersion" /> values, a Boolean for source column mapping, the value of the <see langword="SqlParameter" />, the name of the database where the schema collection for this XML instance is located, the owning relational schema where the schema collection for this XML instance is located, and the name of the schema collection for this parameter.</summary>
		/// <param name="parameterName">The name of the parameter to map.</param>
		/// <param name="dbType">One of the <see cref="T:System.Data.SqlDbType" /> values.</param>
		/// <param name="size">The length of the parameter.</param>
		/// <param name="direction">One of the <see cref="T:System.Data.ParameterDirection" /> values.</param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> is resolved.</param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> is resolved.</param>
		/// <param name="sourceColumn">The name of the source column (<see cref="P:System.Data.SqlClient.SqlParameter.SourceColumn" />) if this <see cref="T:System.Data.SqlClient.SqlParameter" /> is used in a call to <see cref="Overload:System.Data.Common.DbDataAdapter.Update" />.</param>
		/// <param name="sourceVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values.</param>
		/// <param name="sourceColumnNullMapping">
		///   <see langword="true" /> if the source column is nullable; <see langword="false" /> if it is not.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.SqlClient.SqlParameter" />.</param>
		/// <param name="xmlSchemaCollectionDatabase">The name of the database where the schema collection for this XML instance is located.</param>
		/// <param name="xmlSchemaCollectionOwningSchema">The owning relational schema where the schema collection for this XML instance is located.</param>
		/// <param name="xmlSchemaCollectionName">The name of the schema collection for this parameter.</param>
		// Token: 0x060019CA RID: 6602 RVA: 0x000773EC File Offset: 0x000755EC
		public SqlParameter(string parameterName, SqlDbType dbType, int size, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value, string xmlSchemaCollectionDatabase, string xmlSchemaCollectionOwningSchema, string xmlSchemaCollectionName) : this()
		{
			this.ParameterName = parameterName;
			this.SqlDbType = dbType;
			this.Size = size;
			this.Direction = direction;
			this.Precision = precision;
			this.Scale = scale;
			this.SourceColumn = sourceColumn;
			this.SourceVersion = sourceVersion;
			this.SourceColumnNullMapping = sourceColumnNullMapping;
			this.Value = value;
			this.XmlSchemaCollectionDatabase = xmlSchemaCollectionDatabase;
			this.XmlSchemaCollectionOwningSchema = xmlSchemaCollectionOwningSchema;
			this.XmlSchemaCollectionName = xmlSchemaCollectionName;
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x00077464 File Offset: 0x00075664
		private SqlParameter(SqlParameter source) : this()
		{
			ADP.CheckArgumentNull(source, "source");
			source.CloneHelper(this);
			ICloneable cloneable = this._value as ICloneable;
			if (cloneable != null)
			{
				this._value = cloneable.Clone();
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x000774A4 File Offset: 0x000756A4
		// (set) Token: 0x060019CD RID: 6605 RVA: 0x000774AC File Offset: 0x000756AC
		internal SqlCollation Collation
		{
			get
			{
				return this._collation;
			}
			set
			{
				this._collation = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Globalization.CompareInfo" /> object that defines how string comparisons should be performed for this parameter.</summary>
		/// <returns>A <see cref="T:System.Globalization.CompareInfo" /> object that defines string comparison for this parameter.</returns>
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060019CE RID: 6606 RVA: 0x000774B8 File Offset: 0x000756B8
		// (set) Token: 0x060019CF RID: 6607 RVA: 0x000774D8 File Offset: 0x000756D8
		public SqlCompareOptions CompareInfo
		{
			get
			{
				SqlCollation collation = this._collation;
				if (collation != null)
				{
					return collation.SqlCompareOptions;
				}
				return SqlCompareOptions.None;
			}
			set
			{
				SqlCollation sqlCollation = this._collation;
				if (sqlCollation == null)
				{
					sqlCollation = (this._collation = new SqlCollation());
				}
				SqlCompareOptions sqlCompareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreNonSpace | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth | SqlCompareOptions.BinarySort | SqlCompareOptions.BinarySort2;
				if ((value & sqlCompareOptions) != value)
				{
					throw ADP.ArgumentOutOfRange("CompareInfo");
				}
				sqlCollation.SqlCompareOptions = value;
			}
		}

		/// <summary>Gets the name of the database where the schema collection for this XML instance is located.</summary>
		/// <returns>The name of the database where the schema collection for this XML instance is located.</returns>
		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060019D0 RID: 6608 RVA: 0x0007751A File Offset: 0x0007571A
		// (set) Token: 0x060019D1 RID: 6609 RVA: 0x0007752B File Offset: 0x0007572B
		public string XmlSchemaCollectionDatabase
		{
			get
			{
				return this._xmlSchemaCollectionDatabase ?? ADP.StrEmpty;
			}
			set
			{
				this._xmlSchemaCollectionDatabase = value;
			}
		}

		/// <summary>The owning relational schema where the schema collection for this XML instance is located.</summary>
		/// <returns>The owning relational schema for this XML instance.</returns>
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x00077534 File Offset: 0x00075734
		// (set) Token: 0x060019D3 RID: 6611 RVA: 0x00077545 File Offset: 0x00075745
		public string XmlSchemaCollectionOwningSchema
		{
			get
			{
				return this._xmlSchemaCollectionOwningSchema ?? ADP.StrEmpty;
			}
			set
			{
				this._xmlSchemaCollectionOwningSchema = value;
			}
		}

		/// <summary>Gets the name of the schema collection for this XML instance.</summary>
		/// <returns>The name of the schema collection for this XML instance.</returns>
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x0007754E File Offset: 0x0007574E
		// (set) Token: 0x060019D5 RID: 6613 RVA: 0x0007755F File Offset: 0x0007575F
		public string XmlSchemaCollectionName
		{
			get
			{
				return this._xmlSchemaCollectionName ?? ADP.StrEmpty;
			}
			set
			{
				this._xmlSchemaCollectionName = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlDbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.SqlDbType" /> values. The default is <see langword="NVarChar" />.</returns>
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x00077568 File Offset: 0x00075768
		// (set) Token: 0x060019D7 RID: 6615 RVA: 0x00077578 File Offset: 0x00075778
		public override DbType DbType
		{
			get
			{
				return this.GetMetaTypeOnly().DbType;
			}
			set
			{
				MetaType metaType = this._metaType;
				if (metaType == null || metaType.DbType != value || value == DbType.Date || value == DbType.Time)
				{
					this.PropertyTypeChanging();
					this._metaType = MetaType.GetMetaTypeFromDbType(value);
				}
			}
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.SqlClient.SqlParameter" />.</summary>
		// Token: 0x060019D8 RID: 6616 RVA: 0x000775B3 File Offset: 0x000757B3
		public override void ResetDbType()
		{
			this.ResetSqlDbType();
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x000775BB File Offset: 0x000757BB
		// (set) Token: 0x060019DA RID: 6618 RVA: 0x000775C3 File Offset: 0x000757C3
		internal MetaType InternalMetaType
		{
			get
			{
				return this._internalMetaType;
			}
			set
			{
				this._internalMetaType = value;
			}
		}

		/// <summary>Gets or sets the locale identifier that determines conventions and language for a particular region.</summary>
		/// <returns>The locale identifier associated with the parameter.</returns>
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x000775CC File Offset: 0x000757CC
		// (set) Token: 0x060019DC RID: 6620 RVA: 0x000775EC File Offset: 0x000757EC
		public int LocaleId
		{
			get
			{
				SqlCollation collation = this._collation;
				if (collation != null)
				{
					return collation.LCID;
				}
				return 0;
			}
			set
			{
				SqlCollation sqlCollation = this._collation;
				if (sqlCollation == null)
				{
					sqlCollation = (this._collation = new SqlCollation());
				}
				if ((long)value != (1048575L & (long)value))
				{
					throw ADP.ArgumentOutOfRange("LocaleId");
				}
				sqlCollation.LCID = value;
			}
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00077630 File Offset: 0x00075830
		internal SmiParameterMetaData MetaDataForSmi(out ParameterPeekAheadValue peekAhead)
		{
			peekAhead = null;
			MetaType metaType = this.ValidateTypeLengths();
			long num = (long)this.GetActualSize();
			long num2 = (long)this.Size;
			if (!metaType.IsLong)
			{
				if (SqlDbType.NChar == metaType.SqlDbType || SqlDbType.NVarChar == metaType.SqlDbType)
				{
					num /= 2L;
				}
				if (num > num2)
				{
					num2 = num;
				}
			}
			if (num2 == 0L)
			{
				if (SqlDbType.Binary == metaType.SqlDbType || SqlDbType.VarBinary == metaType.SqlDbType)
				{
					num2 = 8000L;
				}
				else if (SqlDbType.Char == metaType.SqlDbType || SqlDbType.VarChar == metaType.SqlDbType)
				{
					num2 = 8000L;
				}
				else if (SqlDbType.NChar == metaType.SqlDbType || SqlDbType.NVarChar == metaType.SqlDbType)
				{
					num2 = 4000L;
				}
			}
			else if ((num2 > 8000L && (SqlDbType.Binary == metaType.SqlDbType || SqlDbType.VarBinary == metaType.SqlDbType)) || (num2 > 8000L && (SqlDbType.Char == metaType.SqlDbType || SqlDbType.VarChar == metaType.SqlDbType)) || (num2 > 4000L && (SqlDbType.NChar == metaType.SqlDbType || SqlDbType.NVarChar == metaType.SqlDbType)))
			{
				num2 = -1L;
			}
			int num3 = this.LocaleId;
			if (num3 == 0 && metaType.IsCharType)
			{
				object coercedValue = this.GetCoercedValue();
				if (coercedValue is SqlString && !((SqlString)coercedValue).IsNull)
				{
					num3 = ((SqlString)coercedValue).LCID;
				}
				else
				{
					num3 = CultureInfo.CurrentCulture.LCID;
				}
			}
			SqlCompareOptions sqlCompareOptions = this.CompareInfo;
			if (sqlCompareOptions == SqlCompareOptions.None && metaType.IsCharType)
			{
				object coercedValue2 = this.GetCoercedValue();
				if (coercedValue2 is SqlString && !((SqlString)coercedValue2).IsNull)
				{
					sqlCompareOptions = ((SqlString)coercedValue2).SqlCompareOptions;
				}
				else
				{
					sqlCompareOptions = SmiMetaData.GetDefaultForType(metaType.SqlDbType).CompareOptions;
				}
			}
			string text = null;
			string text2 = null;
			string text3 = null;
			if (SqlDbType.Xml == metaType.SqlDbType)
			{
				text = this.XmlSchemaCollectionDatabase;
				text2 = this.XmlSchemaCollectionOwningSchema;
				text3 = this.XmlSchemaCollectionName;
			}
			else if (SqlDbType.Udt == metaType.SqlDbType || (SqlDbType.Structured == metaType.SqlDbType && !string.IsNullOrEmpty(this.TypeName)))
			{
				string[] array;
				if (SqlDbType.Udt == metaType.SqlDbType)
				{
					array = SqlParameter.ParseTypeName(this.UdtTypeName, true);
				}
				else
				{
					array = SqlParameter.ParseTypeName(this.TypeName, false);
				}
				if (1 == array.Length)
				{
					text3 = array[0];
				}
				else if (2 == array.Length)
				{
					text2 = array[0];
					text3 = array[1];
				}
				else
				{
					if (3 != array.Length)
					{
						throw ADP.ArgumentOutOfRange("names");
					}
					text = array[0];
					text2 = array[1];
					text3 = array[2];
				}
				if ((!string.IsNullOrEmpty(text) && 255 < text.Length) || (!string.IsNullOrEmpty(text2) && 255 < text2.Length) || (!string.IsNullOrEmpty(text3) && 255 < text3.Length))
				{
					throw ADP.ArgumentOutOfRange("names");
				}
			}
			byte b = this.GetActualPrecision();
			byte actualScale = this.GetActualScale();
			if (SqlDbType.Decimal == metaType.SqlDbType && b == 0)
			{
				b = 29;
			}
			List<SmiExtendedMetaData> fieldMetaData = null;
			SmiMetaDataPropertyCollection extendedProperties = null;
			if (SqlDbType.Structured == metaType.SqlDbType)
			{
				this.GetActualFieldsAndProperties(out fieldMetaData, out extendedProperties, out peekAhead);
			}
			return new SmiParameterMetaData(metaType.SqlDbType, num2, b, actualScale, (long)num3, sqlCompareOptions, null, SqlDbType.Structured == metaType.SqlDbType, fieldMetaData, extendedProperties, this.ParameterNameFixed, text, text2, text3, this.Direction);
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x0007796B File Offset: 0x00075B6B
		// (set) Token: 0x060019DF RID: 6623 RVA: 0x00077973 File Offset: 0x00075B73
		internal bool ParameterIsSqlType
		{
			get
			{
				return this._isSqlParameterSqlType;
			}
			set
			{
				this._isSqlParameterSqlType = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.SqlClient.SqlParameter" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.SqlClient.SqlParameter" />. The default is an empty string.</returns>
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060019E0 RID: 6624 RVA: 0x0007797C File Offset: 0x00075B7C
		// (set) Token: 0x060019E1 RID: 6625 RVA: 0x00077990 File Offset: 0x00075B90
		public override string ParameterName
		{
			get
			{
				return this._parameterName ?? ADP.StrEmpty;
			}
			set
			{
				if (!string.IsNullOrEmpty(value) && value.Length >= 128 && ('@' != value[0] || value.Length > 128))
				{
					throw SQL.InvalidParameterNameLength(value);
				}
				if (this._parameterName != value)
				{
					this.PropertyChanging();
					this._parameterName = value;
					return;
				}
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x000779F0 File Offset: 0x00075BF0
		internal string ParameterNameFixed
		{
			get
			{
				string text = this.ParameterName;
				if (0 < text.Length && '@' != text[0])
				{
					text = "@" + text;
				}
				return text;
			}
		}

		/// <summary>Gets or sets the maximum number of digits used to represent the <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> property.</summary>
		/// <returns>The maximum number of digits used to represent the <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> property. The default value is 0. This indicates that the data provider sets the precision for <see cref="P:System.Data.SqlClient.SqlParameter.Value" />.</returns>
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x00077A25 File Offset: 0x00075C25
		// (set) Token: 0x060019E4 RID: 6628 RVA: 0x00077A2D File Offset: 0x00075C2D
		[DefaultValue(0)]
		public new byte Precision
		{
			get
			{
				return this.PrecisionInternal;
			}
			set
			{
				this.PrecisionInternal = value;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x00077A38 File Offset: 0x00075C38
		// (set) Token: 0x060019E6 RID: 6630 RVA: 0x00077A68 File Offset: 0x00075C68
		internal byte PrecisionInternal
		{
			get
			{
				byte b = this._precision;
				SqlDbType metaSqlDbTypeOnly = this.GetMetaSqlDbTypeOnly();
				if (b == 0 && SqlDbType.Decimal == metaSqlDbTypeOnly)
				{
					b = this.ValuePrecision(this.SqlValue);
				}
				return b;
			}
			set
			{
				if (this.SqlDbType == SqlDbType.Decimal && value > 38)
				{
					throw SQL.PrecisionValueOutOfRange(value);
				}
				if (this._precision != value)
				{
					this.PropertyChanging();
					this._precision = value;
				}
			}
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00077A95 File Offset: 0x00075C95
		private bool ShouldSerializePrecision()
		{
			return this._precision > 0;
		}

		/// <summary>Gets or sets the number of decimal places to which <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> is resolved.</summary>
		/// <returns>The number of decimal places to which <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> is resolved. The default is 0.</returns>
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x00077AA0 File Offset: 0x00075CA0
		// (set) Token: 0x060019E9 RID: 6633 RVA: 0x00077AA8 File Offset: 0x00075CA8
		[DefaultValue(0)]
		public new byte Scale
		{
			get
			{
				return this.ScaleInternal;
			}
			set
			{
				this.ScaleInternal = value;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x00077AB4 File Offset: 0x00075CB4
		// (set) Token: 0x060019EB RID: 6635 RVA: 0x00077AE4 File Offset: 0x00075CE4
		internal byte ScaleInternal
		{
			get
			{
				byte b = this._scale;
				SqlDbType metaSqlDbTypeOnly = this.GetMetaSqlDbTypeOnly();
				if (b == 0 && SqlDbType.Decimal == metaSqlDbTypeOnly)
				{
					b = this.ValueScale(this.SqlValue);
				}
				return b;
			}
			set
			{
				if (this._scale != value || !this._hasScale)
				{
					this.PropertyChanging();
					this._scale = value;
					this._hasScale = true;
					this._actualSize = -1;
				}
			}
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00077B12 File Offset: 0x00075D12
		private bool ShouldSerializeScale()
		{
			return this._scale > 0;
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlDbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.SqlDbType" /> values. The default is <see langword="NVarChar" />.</returns>
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x00077B1D File Offset: 0x00075D1D
		// (set) Token: 0x060019EE RID: 6638 RVA: 0x00077B2C File Offset: 0x00075D2C
		[DbProviderSpecificTypeProperty(true)]
		public SqlDbType SqlDbType
		{
			get
			{
				return this.GetMetaTypeOnly().SqlDbType;
			}
			set
			{
				MetaType metaType = this._metaType;
				if ((SqlDbType)24 == value)
				{
					throw SQL.InvalidSqlDbType(value);
				}
				if (metaType == null || metaType.SqlDbType != value)
				{
					this.PropertyTypeChanging();
					this._metaType = MetaType.GetMetaTypeFromSqlDbType(value, value == SqlDbType.Structured);
				}
			}
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00077B6F File Offset: 0x00075D6F
		private bool ShouldSerializeSqlDbType()
		{
			return this._metaType != null;
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.SqlClient.SqlParameter" />.</summary>
		// Token: 0x060019F0 RID: 6640 RVA: 0x00077B7A File Offset: 0x00075D7A
		public void ResetSqlDbType()
		{
			if (this._metaType != null)
			{
				this.PropertyTypeChanging();
				this._metaType = null;
			}
		}

		/// <summary>Gets or sets the value of the parameter as an SQL type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter, using SQL types. The default value is null.</returns>
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x00077B94 File Offset: 0x00075D94
		// (set) Token: 0x060019F2 RID: 6642 RVA: 0x00077C35 File Offset: 0x00075E35
		public object SqlValue
		{
			get
			{
				if (this._udtLoadError != null)
				{
					throw this._udtLoadError;
				}
				if (this._value != null)
				{
					if (this._value == DBNull.Value)
					{
						return MetaType.GetNullSqlValue(this.GetMetaTypeOnly().SqlType);
					}
					if (this._value is INullable)
					{
						return this._value;
					}
					if (this._value is DateTime)
					{
						SqlDbType sqlDbType = this.GetMetaTypeOnly().SqlDbType;
						if (sqlDbType == SqlDbType.Date || sqlDbType == SqlDbType.DateTime2)
						{
							return this._value;
						}
					}
					return MetaType.GetSqlValueFromComVariant(this._value);
				}
				else
				{
					if (this._sqlBufferReturnValue != null)
					{
						return this._sqlBufferReturnValue.SqlValue;
					}
					return null;
				}
			}
			set
			{
				this.Value = value;
			}
		}

		/// <summary>Gets or sets a <see langword="string" /> that represents a user-defined type as a parameter.</summary>
		/// <returns>A <see langword="string" /> that represents the fully qualified name of a user-defined type in the database.</returns>
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x00077C3E File Offset: 0x00075E3E
		// (set) Token: 0x060019F4 RID: 6644 RVA: 0x00077C4F File Offset: 0x00075E4F
		public string UdtTypeName
		{
			get
			{
				return this._udtTypeName ?? ADP.StrEmpty;
			}
			set
			{
				this._udtTypeName = value;
			}
		}

		/// <summary>Gets or sets the type name for a table-valued parameter.</summary>
		/// <returns>The type name of the specified table-valued parameter.</returns>
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x00077C58 File Offset: 0x00075E58
		// (set) Token: 0x060019F6 RID: 6646 RVA: 0x00077C69 File Offset: 0x00075E69
		public string TypeName
		{
			get
			{
				return this._typeName ?? ADP.StrEmpty;
			}
			set
			{
				this._typeName = value;
			}
		}

		/// <summary>Gets or sets the value of the parameter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x00077C74 File Offset: 0x00075E74
		// (set) Token: 0x060019F8 RID: 6648 RVA: 0x00077CC8 File Offset: 0x00075EC8
		[TypeConverter(typeof(StringConverter))]
		public override object Value
		{
			get
			{
				if (this._udtLoadError != null)
				{
					throw this._udtLoadError;
				}
				if (this._value != null)
				{
					return this._value;
				}
				if (this._sqlBufferReturnValue == null)
				{
					return null;
				}
				if (this.ParameterIsSqlType)
				{
					return this._sqlBufferReturnValue.SqlValue;
				}
				return this._sqlBufferReturnValue.Value;
			}
			set
			{
				this._value = value;
				this._sqlBufferReturnValue = null;
				this._coercedValue = null;
				this._valueAsINullable = (this._value as INullable);
				this._isSqlParameterSqlType = (this._valueAsINullable != null);
				this._isNull = (this._value == null || this._value == DBNull.Value || (this._isSqlParameterSqlType && this._valueAsINullable.IsNull));
				this._udtLoadError = null;
				this._actualSize = -1;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x00077D4C File Offset: 0x00075F4C
		internal INullable ValueAsINullable
		{
			get
			{
				return this._valueAsINullable;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x00077D54 File Offset: 0x00075F54
		internal bool IsNull
		{
			get
			{
				if (this._internalMetaType.SqlDbType == SqlDbType.Udt)
				{
					this._isNull = (this._value == null || this._value == DBNull.Value || (this._isSqlParameterSqlType && this._valueAsINullable.IsNull));
				}
				return this._isNull;
			}
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x00077DAC File Offset: 0x00075FAC
		internal int GetActualSize()
		{
			MetaType metaType = this.InternalMetaType;
			SqlDbType sqlDbType = metaType.SqlDbType;
			if (this._actualSize == -1 || sqlDbType == SqlDbType.Udt)
			{
				this._actualSize = 0;
				object coercedValue = this.GetCoercedValue();
				bool flag = false;
				if (this.IsNull && !metaType.IsVarTime)
				{
					return 0;
				}
				if (sqlDbType == SqlDbType.Variant)
				{
					metaType = MetaType.GetMetaTypeFromValue(coercedValue, false);
					sqlDbType = MetaType.GetSqlDataType((int)metaType.TDSType, 0U, 0).SqlDbType;
					flag = true;
				}
				if (metaType.IsFixed)
				{
					this._actualSize = metaType.FixedLength;
				}
				else
				{
					int num = 0;
					if (sqlDbType <= SqlDbType.Char)
					{
						if (sqlDbType == SqlDbType.Binary)
						{
							goto IL_1E7;
						}
						if (sqlDbType != SqlDbType.Char)
						{
							goto IL_2B8;
						}
					}
					else
					{
						if (sqlDbType != SqlDbType.Image)
						{
							if (sqlDbType - SqlDbType.NChar > 2)
							{
								switch (sqlDbType)
								{
								case SqlDbType.Text:
								case SqlDbType.VarChar:
									goto IL_174;
								case SqlDbType.Timestamp:
								case SqlDbType.VarBinary:
									goto IL_1E7;
								case SqlDbType.TinyInt:
								case SqlDbType.Variant:
								case (SqlDbType)24:
								case (SqlDbType)26:
								case (SqlDbType)27:
								case (SqlDbType)28:
								case SqlDbType.Date:
									goto IL_2B8;
								case SqlDbType.Xml:
									break;
								case SqlDbType.Udt:
									if (!this.IsNull)
									{
										num = SerializationHelperSql9.SizeInBytes(coercedValue);
										goto IL_2B8;
									}
									goto IL_2B8;
								case SqlDbType.Structured:
									num = -1;
									goto IL_2B8;
								case SqlDbType.Time:
									this._actualSize = (flag ? 5 : MetaType.GetTimeSizeFromScale(this.GetActualScale()));
									goto IL_2B8;
								case SqlDbType.DateTime2:
									this._actualSize = 3 + (flag ? 5 : MetaType.GetTimeSizeFromScale(this.GetActualScale()));
									goto IL_2B8;
								case SqlDbType.DateTimeOffset:
									this._actualSize = 5 + (flag ? 5 : MetaType.GetTimeSizeFromScale(this.GetActualScale()));
									goto IL_2B8;
								default:
									goto IL_2B8;
								}
							}
							num = ((!this._isNull && !this._coercedValueIsDataFeed) ? SqlParameter.StringSize(coercedValue, this._coercedValueIsSqlType) : 0);
							this._actualSize = (this.ShouldSerializeSize() ? this.Size : 0);
							this._actualSize = ((this.ShouldSerializeSize() && this._actualSize <= num) ? this._actualSize : num);
							if (this._actualSize == -1)
							{
								this._actualSize = num;
							}
							this._actualSize <<= 1;
							goto IL_2B8;
						}
						goto IL_1E7;
					}
					IL_174:
					num = ((!this._isNull && !this._coercedValueIsDataFeed) ? SqlParameter.StringSize(coercedValue, this._coercedValueIsSqlType) : 0);
					this._actualSize = (this.ShouldSerializeSize() ? this.Size : 0);
					this._actualSize = ((this.ShouldSerializeSize() && this._actualSize <= num) ? this._actualSize : num);
					if (this._actualSize == -1)
					{
						this._actualSize = num;
						goto IL_2B8;
					}
					goto IL_2B8;
					IL_1E7:
					num = ((!this._isNull && !this._coercedValueIsDataFeed) ? SqlParameter.BinarySize(coercedValue, this._coercedValueIsSqlType) : 0);
					this._actualSize = (this.ShouldSerializeSize() ? this.Size : 0);
					this._actualSize = ((this.ShouldSerializeSize() && this._actualSize <= num) ? this._actualSize : num);
					if (this._actualSize == -1)
					{
						this._actualSize = num;
					}
					IL_2B8:
					if (flag && num > 8000)
					{
						throw SQL.ParameterInvalidVariant(this.ParameterName);
					}
				}
			}
			return this._actualSize;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x060019FC RID: 6652 RVA: 0x0007808F File Offset: 0x0007628F
		object ICloneable.Clone()
		{
			return new SqlParameter(this);
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00078098 File Offset: 0x00076298
		internal static object CoerceValue(object value, MetaType destinationType, out bool coercedToDataFeed, out bool typeChanged, bool allowStreaming = true)
		{
			coercedToDataFeed = false;
			typeChanged = false;
			Type type = value.GetType();
			if (typeof(object) != destinationType.ClassType && type != destinationType.ClassType && (type != destinationType.SqlType || SqlDbType.Xml == destinationType.SqlDbType))
			{
				try
				{
					typeChanged = true;
					if (typeof(string) == destinationType.ClassType)
					{
						if (typeof(SqlXml) == type)
						{
							value = MetaType.GetStringFromXml(((SqlXml)value).CreateReader());
						}
						else if (typeof(SqlString) == type)
						{
							typeChanged = false;
						}
						else if (typeof(XmlReader).IsAssignableFrom(type))
						{
							if (allowStreaming)
							{
								coercedToDataFeed = true;
								value = new XmlDataFeed((XmlReader)value);
							}
							else
							{
								value = MetaType.GetStringFromXml((XmlReader)value);
							}
						}
						else if (typeof(char[]) == type)
						{
							value = new string((char[])value);
						}
						else if (typeof(SqlChars) == type)
						{
							value = new string(((SqlChars)value).Value);
						}
						else if (value is TextReader && allowStreaming)
						{
							coercedToDataFeed = true;
							value = new TextDataFeed((TextReader)value);
						}
						else
						{
							value = Convert.ChangeType(value, destinationType.ClassType, null);
						}
					}
					else if (DbType.Currency == destinationType.DbType && typeof(string) == type)
					{
						value = decimal.Parse((string)value, NumberStyles.Currency, null);
					}
					else if (typeof(SqlBytes) == type && typeof(byte[]) == destinationType.ClassType)
					{
						typeChanged = false;
					}
					else if (typeof(string) == type && SqlDbType.Time == destinationType.SqlDbType)
					{
						value = TimeSpan.Parse((string)value);
					}
					else if (typeof(string) == type && SqlDbType.DateTimeOffset == destinationType.SqlDbType)
					{
						value = DateTimeOffset.Parse((string)value, null);
					}
					else if (typeof(DateTime) == type && SqlDbType.DateTimeOffset == destinationType.SqlDbType)
					{
						value = new DateTimeOffset((DateTime)value);
					}
					else if (243 == destinationType.TDSType && (value is DataTable || value is DbDataReader || value is IEnumerable<SqlDataRecord>))
					{
						typeChanged = false;
					}
					else if (destinationType.ClassType == typeof(byte[]) && value is Stream && allowStreaming)
					{
						coercedToDataFeed = true;
						value = new StreamDataFeed((Stream)value);
					}
					else
					{
						value = Convert.ChangeType(value, destinationType.ClassType, null);
					}
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableExceptionType(ex))
					{
						throw;
					}
					throw ADP.ParameterConversionFailed(value, destinationType.ClassType, ex);
				}
			}
			return value;
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x000783C8 File Offset: 0x000765C8
		internal void FixStreamDataForNonPLP()
		{
			object coercedValue = this.GetCoercedValue();
			if (!this._coercedValueIsDataFeed)
			{
				return;
			}
			this._coercedValueIsDataFeed = false;
			if (coercedValue is TextDataFeed)
			{
				if (this.Size > 0)
				{
					char[] array = new char[this.Size];
					int length = ((TextDataFeed)coercedValue)._source.ReadBlock(array, 0, this.Size);
					this.CoercedValue = new string(array, 0, length);
					return;
				}
				this.CoercedValue = ((TextDataFeed)coercedValue)._source.ReadToEnd();
				return;
			}
			else if (coercedValue is StreamDataFeed)
			{
				if (this.Size > 0)
				{
					byte[] array2 = new byte[this.Size];
					int i = 0;
					Stream source = ((StreamDataFeed)coercedValue)._source;
					while (i < this.Size)
					{
						int num = source.Read(array2, i, this.Size - i);
						if (num == 0)
						{
							break;
						}
						i += num;
					}
					if (i < this.Size)
					{
						Array.Resize<byte>(ref array2, i);
					}
					this.CoercedValue = array2;
					return;
				}
				MemoryStream memoryStream = new MemoryStream();
				((StreamDataFeed)coercedValue)._source.CopyTo(memoryStream);
				this.CoercedValue = memoryStream.ToArray();
				return;
			}
			else
			{
				if (coercedValue is XmlDataFeed)
				{
					this.CoercedValue = MetaType.GetStringFromXml(((XmlDataFeed)coercedValue)._source);
					return;
				}
				return;
			}
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00078508 File Offset: 0x00076708
		private void CloneHelper(SqlParameter destination)
		{
			destination._value = this._value;
			destination._direction = this._direction;
			destination._size = this._size;
			destination._offset = this._offset;
			destination._sourceColumn = this._sourceColumn;
			destination._sourceVersion = this._sourceVersion;
			destination._sourceColumnNullMapping = this._sourceColumnNullMapping;
			destination._isNullable = this._isNullable;
			destination._metaType = this._metaType;
			destination._collation = this._collation;
			destination._xmlSchemaCollectionDatabase = this._xmlSchemaCollectionDatabase;
			destination._xmlSchemaCollectionOwningSchema = this._xmlSchemaCollectionOwningSchema;
			destination._xmlSchemaCollectionName = this._xmlSchemaCollectionName;
			destination._udtTypeName = this._udtTypeName;
			destination._typeName = this._typeName;
			destination._udtLoadError = this._udtLoadError;
			destination._parameterName = this._parameterName;
			destination._precision = this._precision;
			destination._scale = this._scale;
			destination._sqlBufferReturnValue = this._sqlBufferReturnValue;
			destination._isSqlParameterSqlType = this._isSqlParameterSqlType;
			destination._internalMetaType = this._internalMetaType;
			destination.CoercedValue = this.CoercedValue;
			destination._valueAsINullable = this._valueAsINullable;
			destination._isNull = this._isNull;
			destination._coercedValueIsDataFeed = this._coercedValueIsDataFeed;
			destination._coercedValueIsSqlType = this._coercedValueIsSqlType;
			destination._actualSize = this._actualSize;
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when you load <see cref="P:System.Data.SqlClient.SqlParameter.Value" /></summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is <see langword="Current" />.</returns>
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x00078668 File Offset: 0x00076868
		// (set) Token: 0x06001A01 RID: 6657 RVA: 0x00078686 File Offset: 0x00076886
		public override DataRowVersion SourceVersion
		{
			get
			{
				DataRowVersion sourceVersion = this._sourceVersion;
				if (sourceVersion == (DataRowVersion)0)
				{
					return DataRowVersion.Current;
				}
				return sourceVersion;
			}
			set
			{
				if (value <= DataRowVersion.Current)
				{
					if (value != DataRowVersion.Original && value != DataRowVersion.Current)
					{
						goto IL_32;
					}
				}
				else if (value != DataRowVersion.Proposed && value != DataRowVersion.Default)
				{
					goto IL_32;
				}
				this._sourceVersion = value;
				return;
				IL_32:
				throw ADP.InvalidDataRowVersion(value);
			}
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x000786C0 File Offset: 0x000768C0
		internal byte GetActualPrecision()
		{
			if (!this.ShouldSerializePrecision())
			{
				return this.ValuePrecision(this.CoercedValue);
			}
			return this.PrecisionInternal;
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x000786DD File Offset: 0x000768DD
		internal byte GetActualScale()
		{
			if (this.ShouldSerializeScale())
			{
				return this.ScaleInternal;
			}
			if (this.GetMetaTypeOnly().IsVarTime)
			{
				return 7;
			}
			return this.ValueScale(this.CoercedValue);
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x00078709 File Offset: 0x00076909
		internal int GetParameterSize()
		{
			if (!this.ShouldSerializeSize())
			{
				return this.ValueSize(this.CoercedValue);
			}
			return this.Size;
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x00078728 File Offset: 0x00076928
		private void GetActualFieldsAndProperties(out List<SmiExtendedMetaData> fields, out SmiMetaDataPropertyCollection props, out ParameterPeekAheadValue peekAhead)
		{
			fields = null;
			props = null;
			peekAhead = null;
			object coercedValue = this.GetCoercedValue();
			DataTable dataTable = coercedValue as DataTable;
			if (dataTable != null)
			{
				if (dataTable.Columns.Count <= 0)
				{
					throw SQL.NotEnoughColumnsInStructuredType();
				}
				fields = new List<SmiExtendedMetaData>(dataTable.Columns.Count);
				bool[] array = new bool[dataTable.Columns.Count];
				bool flag = false;
				if (dataTable.PrimaryKey != null && dataTable.PrimaryKey.Length != 0)
				{
					foreach (DataColumn dataColumn in dataTable.PrimaryKey)
					{
						array[dataColumn.Ordinal] = true;
						flag = true;
					}
				}
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					fields.Add(MetaDataUtilsSmi.SmiMetaDataFromDataColumn(dataTable.Columns[j], dataTable));
					if (!flag && dataTable.Columns[j].Unique)
					{
						array[j] = true;
						flag = true;
					}
				}
				if (flag)
				{
					props = new SmiMetaDataPropertyCollection();
					props[SmiPropertySelector.UniqueKey] = new SmiUniqueKeyProperty(new List<bool>(array));
					return;
				}
			}
			else if (coercedValue is SqlDataReader)
			{
				fields = new List<SmiExtendedMetaData>(((SqlDataReader)coercedValue).GetInternalSmiMetaData());
				if (fields.Count <= 0)
				{
					throw SQL.NotEnoughColumnsInStructuredType();
				}
				bool[] array2 = new bool[fields.Count];
				bool flag2 = false;
				for (int k = 0; k < fields.Count; k++)
				{
					SmiQueryMetaData smiQueryMetaData = fields[k] as SmiQueryMetaData;
					if (smiQueryMetaData != null && !smiQueryMetaData.IsKey.IsNull && smiQueryMetaData.IsKey.Value)
					{
						array2[k] = true;
						flag2 = true;
					}
				}
				if (flag2)
				{
					props = new SmiMetaDataPropertyCollection();
					props[SmiPropertySelector.UniqueKey] = new SmiUniqueKeyProperty(new List<bool>(array2));
					return;
				}
			}
			else
			{
				if (coercedValue is IEnumerable<SqlDataRecord>)
				{
					IEnumerator<SqlDataRecord> enumerator = ((IEnumerable<SqlDataRecord>)coercedValue).GetEnumerator();
					try
					{
						if (!enumerator.MoveNext())
						{
							throw SQL.IEnumerableOfSqlDataRecordHasNoRows();
						}
						SqlDataRecord sqlDataRecord = enumerator.Current;
						int fieldCount = sqlDataRecord.FieldCount;
						if (0 < fieldCount)
						{
							bool[] array3 = new bool[fieldCount];
							bool[] array4 = new bool[fieldCount];
							bool[] array5 = new bool[fieldCount];
							int num = -1;
							bool flag3 = false;
							bool flag4 = false;
							int num2 = 0;
							SmiOrderProperty.SmiColumnOrder[] array6 = new SmiOrderProperty.SmiColumnOrder[fieldCount];
							fields = new List<SmiExtendedMetaData>(fieldCount);
							for (int l = 0; l < fieldCount; l++)
							{
								SqlMetaData sqlMetaData = sqlDataRecord.GetSqlMetaData(l);
								fields.Add(MetaDataUtilsSmi.SqlMetaDataToSmiExtendedMetaData(sqlMetaData));
								if (sqlMetaData.IsUniqueKey)
								{
									array3[l] = true;
									flag3 = true;
								}
								if (sqlMetaData.UseServerDefault)
								{
									array4[l] = true;
									flag4 = true;
								}
								array6[l].Order = sqlMetaData.SortOrder;
								if (SortOrder.Unspecified != sqlMetaData.SortOrder)
								{
									if (fieldCount <= sqlMetaData.SortOrdinal)
									{
										throw SQL.SortOrdinalGreaterThanFieldCount(l, sqlMetaData.SortOrdinal);
									}
									if (array5[sqlMetaData.SortOrdinal])
									{
										throw SQL.DuplicateSortOrdinal(sqlMetaData.SortOrdinal);
									}
									array6[l].SortOrdinal = sqlMetaData.SortOrdinal;
									array5[sqlMetaData.SortOrdinal] = true;
									if (sqlMetaData.SortOrdinal > num)
									{
										num = sqlMetaData.SortOrdinal;
									}
									num2++;
								}
							}
							if (flag3)
							{
								props = new SmiMetaDataPropertyCollection();
								props[SmiPropertySelector.UniqueKey] = new SmiUniqueKeyProperty(new List<bool>(array3));
							}
							if (flag4)
							{
								if (props == null)
								{
									props = new SmiMetaDataPropertyCollection();
								}
								props[SmiPropertySelector.DefaultFields] = new SmiDefaultFieldsProperty(new List<bool>(array4));
							}
							if (0 < num2)
							{
								if (num >= num2)
								{
									int num3 = 0;
									while (num3 < num2 && array5[num3])
									{
										num3++;
									}
									throw SQL.MissingSortOrdinal(num3);
								}
								if (props == null)
								{
									props = new SmiMetaDataPropertyCollection();
								}
								props[SmiPropertySelector.SortOrder] = new SmiOrderProperty(new List<SmiOrderProperty.SmiColumnOrder>(array6));
							}
							peekAhead = new ParameterPeekAheadValue
							{
								Enumerator = enumerator,
								FirstRecord = sqlDataRecord
							};
							enumerator = null;
							return;
						}
						throw SQL.NotEnoughColumnsInStructuredType();
					}
					finally
					{
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
				}
				if (coercedValue is DbDataReader)
				{
					DataTable schemaTable = ((DbDataReader)coercedValue).GetSchemaTable();
					if (schemaTable.Rows.Count <= 0)
					{
						throw SQL.NotEnoughColumnsInStructuredType();
					}
					int count = schemaTable.Rows.Count;
					fields = new List<SmiExtendedMetaData>(count);
					bool[] array7 = new bool[count];
					bool flag5 = false;
					int ordinal = schemaTable.Columns[SchemaTableColumn.IsKey].Ordinal;
					int ordinal2 = schemaTable.Columns[SchemaTableColumn.ColumnOrdinal].Ordinal;
					for (int m = 0; m < count; m++)
					{
						DataRow dataRow = schemaTable.Rows[m];
						SmiExtendedMetaData smiExtendedMetaData = MetaDataUtilsSmi.SmiMetaDataFromSchemaTableRow(dataRow);
						int n = m;
						if (!dataRow.IsNull(ordinal2))
						{
							n = (int)dataRow[ordinal2];
						}
						if (n >= count || n < 0)
						{
							throw SQL.InvalidSchemaTableOrdinals();
						}
						while (n > fields.Count)
						{
							fields.Add(null);
						}
						if (fields.Count == n)
						{
							fields.Add(smiExtendedMetaData);
						}
						else
						{
							if (fields[n] != null)
							{
								throw SQL.InvalidSchemaTableOrdinals();
							}
							fields[n] = smiExtendedMetaData;
						}
						if (!dataRow.IsNull(ordinal) && (bool)dataRow[ordinal])
						{
							array7[n] = true;
							flag5 = true;
						}
					}
					if (flag5)
					{
						props = new SmiMetaDataPropertyCollection();
						props[SmiPropertySelector.UniqueKey] = new SmiUniqueKeyProperty(new List<bool>(array7));
					}
				}
			}
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00078C98 File Offset: 0x00076E98
		internal object GetCoercedValue()
		{
			if (this._coercedValue == null || this._internalMetaType.SqlDbType == SqlDbType.Udt)
			{
				bool flag = this.Value is DataFeed;
				if (this.IsNull || flag)
				{
					this._coercedValue = this.Value;
					this._coercedValueIsSqlType = (this._coercedValue != null && this._isSqlParameterSqlType);
					this._coercedValueIsDataFeed = flag;
					this._actualSize = (this.IsNull ? 0 : -1);
				}
				else
				{
					bool flag2;
					this._coercedValue = SqlParameter.CoerceValue(this.Value, this._internalMetaType, out this._coercedValueIsDataFeed, out flag2, true);
					this._coercedValueIsSqlType = (this._isSqlParameterSqlType && !flag2);
					this._actualSize = -1;
				}
			}
			return this._coercedValue;
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x00078D58 File Offset: 0x00076F58
		internal bool CoercedValueIsSqlType
		{
			get
			{
				if (this._coercedValue == null)
				{
					this.GetCoercedValue();
				}
				return this._coercedValueIsSqlType;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x00078D6F File Offset: 0x00076F6F
		internal bool CoercedValueIsDataFeed
		{
			get
			{
				if (this._coercedValue == null)
				{
					this.GetCoercedValue();
				}
				return this._coercedValueIsDataFeed;
			}
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x00007EED File Offset: 0x000060ED
		[Conditional("DEBUG")]
		internal void AssertCachedPropertiesAreValid()
		{
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00007EED File Offset: 0x000060ED
		[Conditional("DEBUG")]
		internal void AssertPropertiesAreValid(object value, bool? isSqlType = null, bool? isDataFeed = null, bool? isNull = null)
		{
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00078D88 File Offset: 0x00076F88
		private SqlDbType GetMetaSqlDbTypeOnly()
		{
			MetaType metaType = this._metaType;
			if (metaType == null)
			{
				metaType = MetaType.GetDefaultMetaType();
			}
			return metaType.SqlDbType;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00078DAC File Offset: 0x00076FAC
		private MetaType GetMetaTypeOnly()
		{
			if (this._metaType != null)
			{
				return this._metaType;
			}
			if (this._value != null && DBNull.Value != this._value)
			{
				Type type = this._value.GetType();
				if (typeof(char) == type)
				{
					this._value = this._value.ToString();
					type = typeof(string);
				}
				else if (typeof(char[]) == type)
				{
					this._value = new string((char[])this._value);
					type = typeof(string);
				}
				return MetaType.GetMetaTypeFromType(type);
			}
			if (this._sqlBufferReturnValue != null)
			{
				Type typeFromStorageType = this._sqlBufferReturnValue.GetTypeFromStorageType(this._isSqlParameterSqlType);
				if (null != typeFromStorageType)
				{
					return MetaType.GetMetaTypeFromType(typeFromStorageType);
				}
			}
			return MetaType.GetDefaultMetaType();
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00078E88 File Offset: 0x00077088
		internal void Prepare(SqlCommand cmd)
		{
			if (this._metaType == null)
			{
				throw ADP.PrepareParameterType(cmd);
			}
			if (!this.ShouldSerializeSize() && !this._metaType.IsFixed)
			{
				throw ADP.PrepareParameterSize(cmd);
			}
			if (!this.ShouldSerializePrecision() && !this.ShouldSerializeScale() && this._metaType.SqlDbType == SqlDbType.Decimal)
			{
				throw ADP.PrepareParameterScale(cmd, this.SqlDbType.ToString());
			}
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x00078EF9 File Offset: 0x000770F9
		private void PropertyChanging()
		{
			this._internalMetaType = null;
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x00078F02 File Offset: 0x00077102
		private void PropertyTypeChanging()
		{
			this.PropertyChanging();
			this.CoercedValue = null;
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x00078F14 File Offset: 0x00077114
		internal void SetSqlBuffer(SqlBuffer buff)
		{
			this._sqlBufferReturnValue = buff;
			this._value = null;
			this._coercedValue = null;
			this._isNull = this._sqlBufferReturnValue.IsNull;
			this._coercedValueIsDataFeed = false;
			this._coercedValueIsSqlType = false;
			this._udtLoadError = null;
			this._actualSize = -1;
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00078F63 File Offset: 0x00077163
		internal void SetUdtLoadError(Exception e)
		{
			this._udtLoadError = e;
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00078F6C File Offset: 0x0007716C
		internal void Validate(int index, bool isCommandProc)
		{
			MetaType metaTypeOnly = this.GetMetaTypeOnly();
			this._internalMetaType = metaTypeOnly;
			if (ADP.IsDirection(this, ParameterDirection.Output) && !ADP.IsDirection(this, ParameterDirection.ReturnValue) && !metaTypeOnly.IsFixed && !this.ShouldSerializeSize() && (this._value == null || Convert.IsDBNull(this._value)) && this.SqlDbType != SqlDbType.Timestamp && this.SqlDbType != SqlDbType.Udt && this.SqlDbType != SqlDbType.Xml && !metaTypeOnly.IsVarTime)
			{
				throw ADP.UninitializedParameterSize(index, metaTypeOnly.ClassType);
			}
			if (metaTypeOnly.SqlDbType != SqlDbType.Udt && this.Direction != ParameterDirection.Output)
			{
				this.GetCoercedValue();
			}
			if (metaTypeOnly.SqlDbType == SqlDbType.Udt)
			{
				if (string.IsNullOrEmpty(this.UdtTypeName))
				{
					throw SQL.MustSetUdtTypeNameForUdtParams();
				}
			}
			else if (!string.IsNullOrEmpty(this.UdtTypeName))
			{
				throw SQL.UnexpectedUdtTypeNameForNonUdtParams();
			}
			if (metaTypeOnly.SqlDbType == SqlDbType.Structured)
			{
				if (!isCommandProc && string.IsNullOrEmpty(this.TypeName))
				{
					throw SQL.MustSetTypeNameForParam(metaTypeOnly.TypeName, this.ParameterName);
				}
				if (ParameterDirection.Input != this.Direction)
				{
					throw SQL.UnsupportedTVPOutputParameter(this.Direction, this.ParameterName);
				}
				if (DBNull.Value == this.GetCoercedValue())
				{
					throw SQL.DBNullNotSupportedForTVPValues(this.ParameterName);
				}
			}
			else if (!string.IsNullOrEmpty(this.TypeName))
			{
				throw SQL.UnexpectedTypeNameForNonStructParams(this.ParameterName);
			}
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x000790B4 File Offset: 0x000772B4
		internal MetaType ValidateTypeLengths()
		{
			MetaType metaType = this.InternalMetaType;
			if (SqlDbType.Udt != metaType.SqlDbType && !metaType.IsFixed && !metaType.IsLong)
			{
				long num = (long)this.GetActualSize();
				long num2 = (long)this.Size;
				long num3;
				if (metaType.IsNCharType)
				{
					num3 = ((num2 * 2L > num) ? (num2 * 2L) : num);
				}
				else
				{
					num3 = ((num2 > num) ? num2 : num);
				}
				if (num3 > 8000L || this._coercedValueIsDataFeed || num2 == -1L || num == -1L)
				{
					metaType = MetaType.GetMaxMetaTypeFromMetaType(metaType);
					this._metaType = metaType;
					this.InternalMetaType = metaType;
					if (!metaType.IsPlp)
					{
						if (metaType.SqlDbType == SqlDbType.Xml)
						{
							throw ADP.InvalidMetaDataValue();
						}
						if (metaType.SqlDbType == SqlDbType.NVarChar || metaType.SqlDbType == SqlDbType.VarChar || metaType.SqlDbType == SqlDbType.VarBinary)
						{
							this.Size = -1;
						}
					}
				}
			}
			return metaType;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00079190 File Offset: 0x00077390
		private byte ValuePrecision(object value)
		{
			if (!(value is SqlDecimal))
			{
				return this.ValuePrecisionCore(value);
			}
			if (((SqlDecimal)value).IsNull)
			{
				return 0;
			}
			return ((SqlDecimal)value).Precision;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x000791D0 File Offset: 0x000773D0
		private byte ValueScale(object value)
		{
			if (!(value is SqlDecimal))
			{
				return this.ValueScaleCore(value);
			}
			if (((SqlDecimal)value).IsNull)
			{
				return 0;
			}
			return ((SqlDecimal)value).Scale;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00079210 File Offset: 0x00077410
		private static int StringSize(object value, bool isSqlType)
		{
			if (isSqlType)
			{
				if (value is SqlString)
				{
					return ((SqlString)value).Value.Length;
				}
				if (value is SqlChars)
				{
					return ((SqlChars)value).Value.Length;
				}
			}
			else
			{
				string text = value as string;
				if (text != null)
				{
					return text.Length;
				}
				char[] array = value as char[];
				if (array != null)
				{
					return array.Length;
				}
				if (value is char)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x0007927C File Offset: 0x0007747C
		private static int BinarySize(object value, bool isSqlType)
		{
			if (isSqlType)
			{
				if (value is SqlBinary)
				{
					return ((SqlBinary)value).Length;
				}
				if (value is SqlBytes)
				{
					return ((SqlBytes)value).Value.Length;
				}
			}
			else
			{
				byte[] array = value as byte[];
				if (array != null)
				{
					return array.Length;
				}
				if (value is byte)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x000792D4 File Offset: 0x000774D4
		private int ValueSize(object value)
		{
			if (value is SqlString)
			{
				if (((SqlString)value).IsNull)
				{
					return 0;
				}
				return ((SqlString)value).Value.Length;
			}
			else if (value is SqlChars)
			{
				if (((SqlChars)value).IsNull)
				{
					return 0;
				}
				return ((SqlChars)value).Value.Length;
			}
			else if (value is SqlBinary)
			{
				if (((SqlBinary)value).IsNull)
				{
					return 0;
				}
				return ((SqlBinary)value).Length;
			}
			else if (value is SqlBytes)
			{
				if (((SqlBytes)value).IsNull)
				{
					return 0;
				}
				return (int)((SqlBytes)value).Length;
			}
			else
			{
				if (value is DataFeed)
				{
					return 0;
				}
				return this.ValueSizeCore(value);
			}
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00079394 File Offset: 0x00077594
		internal static string[] ParseTypeName(string typeName, bool isUdtTypeName)
		{
			string[] result;
			try
			{
				string property = isUdtTypeName ? "SqlParameter.UdtTypeName is an invalid multipart name" : "SqlParameter.TypeName is an invalid multipart name";
				result = MultipartIdentifier.ParseMultipartIdentifier(typeName, "[\"", "]\"", '.', 3, true, property, true);
			}
			catch (ArgumentException)
			{
				if (isUdtTypeName)
				{
					throw SQL.InvalidUdt3PartNameFormat();
				}
				throw SQL.InvalidParameterTypeNameFormat();
			}
			return result;
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x000793EC File Offset: 0x000775EC
		// (set) Token: 0x06001A1B RID: 6683 RVA: 0x000793F4 File Offset: 0x000775F4
		private object CoercedValue
		{
			get
			{
				return this._coercedValue;
			}
			set
			{
				this._coercedValue = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter is input-only, output-only, bidirectional, or a stored procedure return value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is <see langword="Input" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values.</exception>
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x00079400 File Offset: 0x00077600
		// (set) Token: 0x06001A1D RID: 6685 RVA: 0x0007941A File Offset: 0x0007761A
		public override ParameterDirection Direction
		{
			get
			{
				ParameterDirection direction = this._direction;
				if (direction == (ParameterDirection)0)
				{
					return ParameterDirection.Input;
				}
				return direction;
			}
			set
			{
				if (this._direction == value)
				{
					return;
				}
				if (value - ParameterDirection.Input <= 2 || value == ParameterDirection.ReturnValue)
				{
					this.PropertyChanging();
					this._direction = value;
					return;
				}
				throw ADP.InvalidParameterDirection(value);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter accepts null values. <see cref="P:System.Data.SqlClient.SqlParameter.IsNullable" /> is not used to validate the parameter's value and will not prevent sending or receiving a null value when executing a command.</summary>
		/// <returns>
		///   <see langword="true" /> if null values are accepted; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x00079444 File Offset: 0x00077644
		// (set) Token: 0x06001A1F RID: 6687 RVA: 0x0007944C File Offset: 0x0007764C
		public override bool IsNullable
		{
			get
			{
				return this._isNullable;
			}
			set
			{
				this._isNullable = value;
			}
		}

		/// <summary>Gets or sets the offset to the <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> property.</summary>
		/// <returns>The offset to the <see cref="P:System.Data.SqlClient.SqlParameter.Value" />. The default is 0.</returns>
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x00079455 File Offset: 0x00077655
		// (set) Token: 0x06001A21 RID: 6689 RVA: 0x0007945D File Offset: 0x0007765D
		public int Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidOffsetValue(value);
				}
				this._offset = value;
			}
		}

		/// <summary>Gets or sets the maximum size, in bytes, of the data within the column.</summary>
		/// <returns>The maximum size, in bytes, of the data within the column. The default value is inferred from the parameter value.</returns>
		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x00079474 File Offset: 0x00077674
		// (set) Token: 0x06001A23 RID: 6691 RVA: 0x00079499 File Offset: 0x00077699
		public override int Size
		{
			get
			{
				int num = this._size;
				if (num == 0)
				{
					num = this.ValueSize(this.Value);
				}
				return num;
			}
			set
			{
				if (this._size != value)
				{
					if (value < -1)
					{
						throw ADP.InvalidSizeValue(value);
					}
					this.PropertyChanging();
					this._size = value;
				}
			}
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x000794BC File Offset: 0x000776BC
		private bool ShouldSerializeSize()
		{
			return this._size != 0;
		}

		/// <summary>Gets or sets the name of the source column mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.SqlClient.SqlParameter.Value" /></summary>
		/// <returns>The name of the source column mapped to the <see cref="T:System.Data.DataSet" />. The default is an empty string.</returns>
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x000794C8 File Offset: 0x000776C8
		// (set) Token: 0x06001A26 RID: 6694 RVA: 0x000794E6 File Offset: 0x000776E6
		public override string SourceColumn
		{
			get
			{
				string sourceColumn = this._sourceColumn;
				if (sourceColumn == null)
				{
					return ADP.StrEmpty;
				}
				return sourceColumn;
			}
			set
			{
				this._sourceColumn = value;
			}
		}

		/// <summary>Sets or gets a value which indicates whether the source column is nullable. This allows <see cref="T:System.Data.SqlClient.SqlCommandBuilder" /> to correctly generate Update statements for nullable columns.</summary>
		/// <returns>
		///   <see langword="true" /> if the source column is nullable; <see langword="false" /> if it is not.</returns>
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x000794EF File Offset: 0x000776EF
		// (set) Token: 0x06001A28 RID: 6696 RVA: 0x000794F7 File Offset: 0x000776F7
		public override bool SourceColumnNullMapping
		{
			get
			{
				return this._sourceColumnNullMapping;
			}
			set
			{
				this._sourceColumnNullMapping = value;
			}
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x00079500 File Offset: 0x00077700
		internal object CompareExchangeParent(object value, object comparand)
		{
			object parent = this._parent;
			if (comparand == parent)
			{
				this._parent = value;
			}
			return parent;
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x00079520 File Offset: 0x00077720
		internal void ResetParent()
		{
			this._parent = null;
		}

		/// <summary>Gets a string that contains the <see cref="P:System.Data.SqlClient.SqlParameter.ParameterName" />.</summary>
		/// <returns>A string that contains the <see cref="P:System.Data.SqlClient.SqlParameter.ParameterName" />.</returns>
		// Token: 0x06001A2B RID: 6699 RVA: 0x00079529 File Offset: 0x00077729
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00079534 File Offset: 0x00077734
		private byte ValuePrecisionCore(object value)
		{
			if (value is decimal)
			{
				return ((decimal)value).Precision;
			}
			return 0;
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0007955E File Offset: 0x0007775E
		private byte ValueScaleCore(object value)
		{
			if (value is decimal)
			{
				return (byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16);
			}
			return 0;
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00079584 File Offset: 0x00077784
		private int ValueSizeCore(object value)
		{
			if (!ADP.IsNull(value))
			{
				string text = value as string;
				if (text != null)
				{
					return text.Length;
				}
				byte[] array = value as byte[];
				if (array != null)
				{
					return array.Length;
				}
				char[] array2 = value as char[];
				if (array2 != null)
				{
					return array2.Length;
				}
				if (value is byte || value is char)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000795DC File Offset: 0x000777DC
		internal void CopyTo(SqlParameter destination)
		{
			ADP.CheckArgumentNull(destination, "destination");
			destination._value = this._value;
			destination._direction = this._direction;
			destination._size = this._size;
			destination._offset = this._offset;
			destination._sourceColumn = this._sourceColumn;
			destination._sourceVersion = this._sourceVersion;
			destination._sourceColumnNullMapping = this._sourceColumnNullMapping;
			destination._isNullable = this._isNullable;
			destination._parameterName = this._parameterName;
			destination._isNull = this._isNull;
		}

		/// <summary>Enforces encryption of a parameter when using Always Encrypted. If SQL Server informs the driver that the parameter does not need to be encrypted, the query using the parameter will fail. This property provides additional protection against security attacks that involve a compromised SQL Server providing incorrect encryption metadata to the client, which may lead to data disclosure.</summary>
		/// <returns>
		///   <see langword="true" /> if the parameter has a force column encryption; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x0007966C File Offset: 0x0007786C
		// (set) Token: 0x06001A31 RID: 6705 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public bool ForceColumnEncryption
		{
			[CompilerGenerated]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040010D1 RID: 4305
		private MetaType _metaType;

		// Token: 0x040010D2 RID: 4306
		private SqlCollation _collation;

		// Token: 0x040010D3 RID: 4307
		private string _xmlSchemaCollectionDatabase;

		// Token: 0x040010D4 RID: 4308
		private string _xmlSchemaCollectionOwningSchema;

		// Token: 0x040010D5 RID: 4309
		private string _xmlSchemaCollectionName;

		// Token: 0x040010D6 RID: 4310
		private string _udtTypeName;

		// Token: 0x040010D7 RID: 4311
		private string _typeName;

		// Token: 0x040010D8 RID: 4312
		private Exception _udtLoadError;

		// Token: 0x040010D9 RID: 4313
		private string _parameterName;

		// Token: 0x040010DA RID: 4314
		private byte _precision;

		// Token: 0x040010DB RID: 4315
		private byte _scale;

		// Token: 0x040010DC RID: 4316
		private bool _hasScale;

		// Token: 0x040010DD RID: 4317
		private MetaType _internalMetaType;

		// Token: 0x040010DE RID: 4318
		private SqlBuffer _sqlBufferReturnValue;

		// Token: 0x040010DF RID: 4319
		private INullable _valueAsINullable;

		// Token: 0x040010E0 RID: 4320
		private bool _isSqlParameterSqlType;

		// Token: 0x040010E1 RID: 4321
		private bool _isNull = true;

		// Token: 0x040010E2 RID: 4322
		private bool _coercedValueIsSqlType;

		// Token: 0x040010E3 RID: 4323
		private bool _coercedValueIsDataFeed;

		// Token: 0x040010E4 RID: 4324
		private int _actualSize = -1;

		// Token: 0x040010E5 RID: 4325
		private DataRowVersion _sourceVersion;

		// Token: 0x040010E6 RID: 4326
		private object _value;

		// Token: 0x040010E7 RID: 4327
		private object _parent;

		// Token: 0x040010E8 RID: 4328
		private ParameterDirection _direction;

		// Token: 0x040010E9 RID: 4329
		private int _size;

		// Token: 0x040010EA RID: 4330
		private int _offset;

		// Token: 0x040010EB RID: 4331
		private string _sourceColumn;

		// Token: 0x040010EC RID: 4332
		private bool _sourceColumnNullMapping;

		// Token: 0x040010ED RID: 4333
		private bool _isNullable;

		// Token: 0x040010EE RID: 4334
		private object _coercedValue;

		// Token: 0x0200021B RID: 539
		internal sealed class SqlParameterConverter : ExpandableObjectConverter
		{
			// Token: 0x06001A32 RID: 6706 RVA: 0x0002C704 File Offset: 0x0002A904
			public SqlParameterConverter()
			{
			}

			// Token: 0x06001A33 RID: 6707 RVA: 0x00079687 File Offset: 0x00077887
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06001A34 RID: 6708 RVA: 0x000796A8 File Offset: 0x000778A8
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (typeof(InstanceDescriptor) == destinationType && value is SqlParameter)
				{
					return this.ConvertToInstanceDescriptor(value as SqlParameter);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			// Token: 0x06001A35 RID: 6709 RVA: 0x00079700 File Offset: 0x00077900
			private InstanceDescriptor ConvertToInstanceDescriptor(SqlParameter p)
			{
				int num = 0;
				if (p.ShouldSerializeSqlDbType())
				{
					num |= 1;
				}
				if (p.ShouldSerializeSize())
				{
					num |= 2;
				}
				if (!string.IsNullOrEmpty(p.SourceColumn))
				{
					num |= 4;
				}
				if (p.Value != null)
				{
					num |= 8;
				}
				if (ParameterDirection.Input != p.Direction || p.IsNullable || p.ShouldSerializePrecision() || p.ShouldSerializeScale() || DataRowVersion.Current != p.SourceVersion)
				{
					num |= 16;
				}
				if (p.SourceColumnNullMapping || !string.IsNullOrEmpty(p.XmlSchemaCollectionDatabase) || !string.IsNullOrEmpty(p.XmlSchemaCollectionOwningSchema) || !string.IsNullOrEmpty(p.XmlSchemaCollectionName))
				{
					num |= 32;
				}
				Type[] types;
				object[] arguments;
				switch (num)
				{
				case 0:
				case 1:
					types = new Type[]
					{
						typeof(string),
						typeof(SqlDbType)
					};
					arguments = new object[]
					{
						p.ParameterName,
						p.SqlDbType
					};
					break;
				case 2:
				case 3:
					types = new Type[]
					{
						typeof(string),
						typeof(SqlDbType),
						typeof(int)
					};
					arguments = new object[]
					{
						p.ParameterName,
						p.SqlDbType,
						p.Size
					};
					break;
				case 4:
				case 5:
				case 6:
				case 7:
					types = new Type[]
					{
						typeof(string),
						typeof(SqlDbType),
						typeof(int),
						typeof(string)
					};
					arguments = new object[]
					{
						p.ParameterName,
						p.SqlDbType,
						p.Size,
						p.SourceColumn
					};
					break;
				case 8:
					types = new Type[]
					{
						typeof(string),
						typeof(object)
					};
					arguments = new object[]
					{
						p.ParameterName,
						p.Value
					};
					break;
				default:
					if ((32 & num) == 0)
					{
						types = new Type[]
						{
							typeof(string),
							typeof(SqlDbType),
							typeof(int),
							typeof(ParameterDirection),
							typeof(bool),
							typeof(byte),
							typeof(byte),
							typeof(string),
							typeof(DataRowVersion),
							typeof(object)
						};
						arguments = new object[]
						{
							p.ParameterName,
							p.SqlDbType,
							p.Size,
							p.Direction,
							p.IsNullable,
							p.PrecisionInternal,
							p.ScaleInternal,
							p.SourceColumn,
							p.SourceVersion,
							p.Value
						};
					}
					else
					{
						types = new Type[]
						{
							typeof(string),
							typeof(SqlDbType),
							typeof(int),
							typeof(ParameterDirection),
							typeof(byte),
							typeof(byte),
							typeof(string),
							typeof(DataRowVersion),
							typeof(bool),
							typeof(object),
							typeof(string),
							typeof(string),
							typeof(string)
						};
						arguments = new object[]
						{
							p.ParameterName,
							p.SqlDbType,
							p.Size,
							p.Direction,
							p.PrecisionInternal,
							p.ScaleInternal,
							p.SourceColumn,
							p.SourceVersion,
							p.SourceColumnNullMapping,
							p.Value,
							p.XmlSchemaCollectionDatabase,
							p.XmlSchemaCollectionOwningSchema,
							p.XmlSchemaCollectionName
						};
					}
					break;
				}
				return new InstanceDescriptor(typeof(SqlParameter).GetConstructor(types), arguments);
			}
		}
	}
}
