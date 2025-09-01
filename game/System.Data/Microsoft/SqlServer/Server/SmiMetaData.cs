using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200002A RID: 42
	internal class SmiMetaData
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00006DF5 File Offset: 0x00004FF5
		internal static SmiMetaData DefaultChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultChar_NoCollation.SqlDbType, SmiMetaData.DefaultChar_NoCollation.MaxLength, SmiMetaData.DefaultChar_NoCollation.Precision, SmiMetaData.DefaultChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00006E32 File Offset: 0x00005032
		internal static SmiMetaData DefaultNChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultNChar_NoCollation.SqlDbType, SmiMetaData.DefaultNChar_NoCollation.MaxLength, SmiMetaData.DefaultNChar_NoCollation.Precision, SmiMetaData.DefaultNChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00006E6F File Offset: 0x0000506F
		internal static SmiMetaData DefaultNText
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultNText_NoCollation.SqlDbType, SmiMetaData.DefaultNText_NoCollation.MaxLength, SmiMetaData.DefaultNText_NoCollation.Precision, SmiMetaData.DefaultNText_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00006EAC File Offset: 0x000050AC
		internal static SmiMetaData DefaultNVarChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultNVarChar_NoCollation.SqlDbType, SmiMetaData.DefaultNVarChar_NoCollation.MaxLength, SmiMetaData.DefaultNVarChar_NoCollation.Precision, SmiMetaData.DefaultNVarChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00006EE9 File Offset: 0x000050E9
		internal static SmiMetaData DefaultText
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultText_NoCollation.SqlDbType, SmiMetaData.DefaultText_NoCollation.MaxLength, SmiMetaData.DefaultText_NoCollation.Precision, SmiMetaData.DefaultText_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00006F26 File Offset: 0x00005126
		internal static SmiMetaData DefaultVarChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultVarChar_NoCollation.SqlDbType, SmiMetaData.DefaultVarChar_NoCollation.MaxLength, SmiMetaData.DefaultVarChar_NoCollation.Precision, SmiMetaData.DefaultVarChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006F64 File Offset: 0x00005164
		internal SmiMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null)
		{
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006F88 File Offset: 0x00005188
		internal SmiMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldTypes, SmiMetaDataPropertyCollection extendedProperties) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldTypes, extendedProperties)
		{
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006FB0 File Offset: 0x000051B0
		internal SmiMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldTypes, SmiMetaDataPropertyCollection extendedProperties)
		{
			this.SetDefaultsForType(dbType);
			switch (dbType)
			{
			case SqlDbType.Binary:
			case SqlDbType.VarBinary:
				this._maxLength = maxLength;
				break;
			case SqlDbType.Char:
			case SqlDbType.NChar:
			case SqlDbType.NVarChar:
			case SqlDbType.VarChar:
				this._maxLength = maxLength;
				this._localeId = localeId;
				this._compareOptions = compareOptions;
				break;
			case SqlDbType.Decimal:
				this._precision = precision;
				this._scale = scale;
				this._maxLength = (long)((ulong)SmiMetaData.s_maxLenFromPrecision[(int)(precision - 1)]);
				break;
			case SqlDbType.NText:
			case SqlDbType.Text:
				this._localeId = localeId;
				this._compareOptions = compareOptions;
				break;
			case SqlDbType.Udt:
				this._clrType = userDefinedType;
				if (null != userDefinedType)
				{
					this._maxLength = (long)SerializationHelperSql9.GetUdtMaxLength(userDefinedType);
				}
				else
				{
					this._maxLength = maxLength;
				}
				this._udtAssemblyQualifiedName = udtAssemblyQualifiedName;
				break;
			case SqlDbType.Structured:
				if (fieldTypes != null)
				{
					this._fieldMetaData = new List<SmiExtendedMetaData>(fieldTypes).AsReadOnly();
				}
				this._isMultiValued = isMultiValued;
				this._maxLength = (long)this._fieldMetaData.Count;
				break;
			case SqlDbType.Time:
				this._scale = scale;
				this._maxLength = (long)(5 - SmiMetaData.s_maxVarTimeLenOffsetFromScale[(int)scale]);
				break;
			case SqlDbType.DateTime2:
				this._scale = scale;
				this._maxLength = (long)(8 - SmiMetaData.s_maxVarTimeLenOffsetFromScale[(int)scale]);
				break;
			case SqlDbType.DateTimeOffset:
				this._scale = scale;
				this._maxLength = (long)(10 - SmiMetaData.s_maxVarTimeLenOffsetFromScale[(int)scale]);
				break;
			}
			if (extendedProperties != null)
			{
				extendedProperties.SetReadOnly();
				this._extendedProperties = extendedProperties;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007190 File Offset: 0x00005390
		internal bool IsValidMaxLengthForCtorGivenType(SqlDbType dbType, long maxLength)
		{
			bool result = true;
			switch (dbType)
			{
			case SqlDbType.Binary:
				result = (0L < maxLength && 8000L >= maxLength);
				break;
			case SqlDbType.Char:
				result = (0L < maxLength && 8000L >= maxLength);
				break;
			case SqlDbType.NChar:
				result = (0L < maxLength && 4000L >= maxLength);
				break;
			case SqlDbType.NVarChar:
				result = (-1L == maxLength || (0L < maxLength && 4000L >= maxLength));
				break;
			case SqlDbType.VarBinary:
				result = (-1L == maxLength || (0L < maxLength && 8000L >= maxLength));
				break;
			case SqlDbType.VarChar:
				result = (-1L == maxLength || (0L < maxLength && 8000L >= maxLength));
				break;
			}
			return result;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000072DA File Offset: 0x000054DA
		internal SqlCompareOptions CompareOptions
		{
			get
			{
				return this._compareOptions;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000072E2 File Offset: 0x000054E2
		internal long LocaleId
		{
			get
			{
				return this._localeId;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000072EA File Offset: 0x000054EA
		internal long MaxLength
		{
			get
			{
				return this._maxLength;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000072F2 File Offset: 0x000054F2
		internal byte Precision
		{
			get
			{
				return this._precision;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000072FA File Offset: 0x000054FA
		internal byte Scale
		{
			get
			{
				return this._scale;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00007302 File Offset: 0x00005502
		internal SqlDbType SqlDbType
		{
			get
			{
				return this._databaseType;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000730A File Offset: 0x0000550A
		internal Type Type
		{
			get
			{
				if (null == this._clrType && SqlDbType.Udt == this._databaseType && this._udtAssemblyQualifiedName != null)
				{
					this._clrType = Type.GetType(this._udtAssemblyQualifiedName, true);
				}
				return this._clrType;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00007344 File Offset: 0x00005544
		internal Type TypeWithoutThrowing
		{
			get
			{
				if (null == this._clrType && SqlDbType.Udt == this._databaseType && this._udtAssemblyQualifiedName != null)
				{
					this._clrType = Type.GetType(this._udtAssemblyQualifiedName, false);
				}
				return this._clrType;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00007380 File Offset: 0x00005580
		internal string TypeName
		{
			get
			{
				string result;
				if (SqlDbType.Udt == this._databaseType)
				{
					result = this.Type.FullName;
				}
				else
				{
					result = SmiMetaData.s_typeNameByDatabaseType[(int)this._databaseType];
				}
				return result;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000073B8 File Offset: 0x000055B8
		internal string AssemblyQualifiedName
		{
			get
			{
				string result = null;
				if (SqlDbType.Udt == this._databaseType)
				{
					if (this._udtAssemblyQualifiedName == null && this._clrType != null)
					{
						this._udtAssemblyQualifiedName = this._clrType.AssemblyQualifiedName;
					}
					result = this._udtAssemblyQualifiedName;
				}
				return result;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00007400 File Offset: 0x00005600
		internal bool IsMultiValued
		{
			get
			{
				return this._isMultiValued;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00007408 File Offset: 0x00005608
		internal IList<SmiExtendedMetaData> FieldMetaData
		{
			get
			{
				return this._fieldMetaData;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00007410 File Offset: 0x00005610
		internal SmiMetaDataPropertyCollection ExtendedProperties
		{
			get
			{
				return this._extendedProperties;
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007418 File Offset: 0x00005618
		internal static bool IsSupportedDbType(SqlDbType dbType)
		{
			return (SqlDbType.BigInt <= dbType && SqlDbType.Xml >= dbType) || (SqlDbType.Udt <= dbType && SqlDbType.DateTimeOffset >= dbType);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007434 File Offset: 0x00005634
		internal static SmiMetaData GetDefaultForType(SqlDbType dbType)
		{
			return SmiMetaData.s_defaultValues[(int)dbType];
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007440 File Offset: 0x00005640
		private SmiMetaData(SqlDbType sqlDbType, long maxLength, byte precision, byte scale, SqlCompareOptions compareOptions)
		{
			this._databaseType = sqlDbType;
			this._maxLength = maxLength;
			this._precision = precision;
			this._scale = scale;
			this._compareOptions = compareOptions;
			this._localeId = 0L;
			this._clrType = null;
			this._isMultiValued = false;
			this._fieldMetaData = SmiMetaData.s_emptyFieldList;
			this._extendedProperties = SmiMetaDataPropertyCollection.EmptyInstance;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000074A4 File Offset: 0x000056A4
		private void SetDefaultsForType(SqlDbType dbType)
		{
			SmiMetaData defaultForType = SmiMetaData.GetDefaultForType(dbType);
			this._databaseType = dbType;
			this._maxLength = defaultForType.MaxLength;
			this._precision = defaultForType.Precision;
			this._scale = defaultForType.Scale;
			this._localeId = defaultForType.LocaleId;
			this._compareOptions = defaultForType.CompareOptions;
			this._clrType = null;
			this._isMultiValued = defaultForType._isMultiValued;
			this._fieldMetaData = defaultForType._fieldMetaData;
			this._extendedProperties = defaultForType._extendedProperties;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007528 File Offset: 0x00005728
		// Note: this type is marked as 'beforefieldinit'.
		static SmiMetaData()
		{
		}

		// Token: 0x04000456 RID: 1110
		private SqlDbType _databaseType;

		// Token: 0x04000457 RID: 1111
		private long _maxLength;

		// Token: 0x04000458 RID: 1112
		private byte _precision;

		// Token: 0x04000459 RID: 1113
		private byte _scale;

		// Token: 0x0400045A RID: 1114
		private long _localeId;

		// Token: 0x0400045B RID: 1115
		private SqlCompareOptions _compareOptions;

		// Token: 0x0400045C RID: 1116
		private Type _clrType;

		// Token: 0x0400045D RID: 1117
		private string _udtAssemblyQualifiedName;

		// Token: 0x0400045E RID: 1118
		private bool _isMultiValued;

		// Token: 0x0400045F RID: 1119
		private IList<SmiExtendedMetaData> _fieldMetaData;

		// Token: 0x04000460 RID: 1120
		private SmiMetaDataPropertyCollection _extendedProperties;

		// Token: 0x04000461 RID: 1121
		internal const long UnlimitedMaxLengthIndicator = -1L;

		// Token: 0x04000462 RID: 1122
		internal const long MaxUnicodeCharacters = 4000L;

		// Token: 0x04000463 RID: 1123
		internal const long MaxANSICharacters = 8000L;

		// Token: 0x04000464 RID: 1124
		internal const long MaxBinaryLength = 8000L;

		// Token: 0x04000465 RID: 1125
		internal const int MinPrecision = 1;

		// Token: 0x04000466 RID: 1126
		internal const int MinScale = 0;

		// Token: 0x04000467 RID: 1127
		internal const int MaxTimeScale = 7;

		// Token: 0x04000468 RID: 1128
		internal static readonly DateTime MaxSmallDateTime = new DateTime(2079, 6, 6, 23, 59, 29, 998);

		// Token: 0x04000469 RID: 1129
		internal static readonly DateTime MinSmallDateTime = new DateTime(1899, 12, 31, 23, 59, 29, 999);

		// Token: 0x0400046A RID: 1130
		internal static readonly SqlMoney MaxSmallMoney = new SqlMoney(214748.3647m);

		// Token: 0x0400046B RID: 1131
		internal static readonly SqlMoney MinSmallMoney = new SqlMoney(-214748.3648m);

		// Token: 0x0400046C RID: 1132
		internal const SqlCompareOptions DefaultStringCompareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;

		// Token: 0x0400046D RID: 1133
		internal const long MaxNameLength = 128L;

		// Token: 0x0400046E RID: 1134
		private static readonly IList<SmiExtendedMetaData> s_emptyFieldList = new List<SmiExtendedMetaData>().AsReadOnly();

		// Token: 0x0400046F RID: 1135
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

		// Token: 0x04000470 RID: 1136
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

		// Token: 0x04000471 RID: 1137
		internal static readonly SmiMetaData DefaultBigInt = new SmiMetaData(SqlDbType.BigInt, 8L, 19, 0, SqlCompareOptions.None);

		// Token: 0x04000472 RID: 1138
		internal static readonly SmiMetaData DefaultBinary = new SmiMetaData(SqlDbType.Binary, 1L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04000473 RID: 1139
		internal static readonly SmiMetaData DefaultBit = new SmiMetaData(SqlDbType.Bit, 1L, 1, 0, SqlCompareOptions.None);

		// Token: 0x04000474 RID: 1140
		internal static readonly SmiMetaData DefaultChar_NoCollation = new SmiMetaData(SqlDbType.Char, 1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04000475 RID: 1141
		internal static readonly SmiMetaData DefaultDateTime = new SmiMetaData(SqlDbType.DateTime, 8L, 23, 3, SqlCompareOptions.None);

		// Token: 0x04000476 RID: 1142
		internal static readonly SmiMetaData DefaultDecimal = new SmiMetaData(SqlDbType.Decimal, 9L, 18, 0, SqlCompareOptions.None);

		// Token: 0x04000477 RID: 1143
		internal static readonly SmiMetaData DefaultFloat = new SmiMetaData(SqlDbType.Float, 8L, 53, 0, SqlCompareOptions.None);

		// Token: 0x04000478 RID: 1144
		internal static readonly SmiMetaData DefaultImage = new SmiMetaData(SqlDbType.Image, -1L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04000479 RID: 1145
		internal static readonly SmiMetaData DefaultInt = new SmiMetaData(SqlDbType.Int, 4L, 10, 0, SqlCompareOptions.None);

		// Token: 0x0400047A RID: 1146
		internal static readonly SmiMetaData DefaultMoney = new SmiMetaData(SqlDbType.Money, 8L, 19, 4, SqlCompareOptions.None);

		// Token: 0x0400047B RID: 1147
		internal static readonly SmiMetaData DefaultNChar_NoCollation = new SmiMetaData(SqlDbType.NChar, 1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x0400047C RID: 1148
		internal static readonly SmiMetaData DefaultNText_NoCollation = new SmiMetaData(SqlDbType.NText, -1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x0400047D RID: 1149
		internal static readonly SmiMetaData DefaultNVarChar_NoCollation = new SmiMetaData(SqlDbType.NVarChar, 4000L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x0400047E RID: 1150
		internal static readonly SmiMetaData DefaultReal = new SmiMetaData(SqlDbType.Real, 4L, 24, 0, SqlCompareOptions.None);

		// Token: 0x0400047F RID: 1151
		internal static readonly SmiMetaData DefaultUniqueIdentifier = new SmiMetaData(SqlDbType.UniqueIdentifier, 16L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04000480 RID: 1152
		internal static readonly SmiMetaData DefaultSmallDateTime = new SmiMetaData(SqlDbType.SmallDateTime, 4L, 16, 0, SqlCompareOptions.None);

		// Token: 0x04000481 RID: 1153
		internal static readonly SmiMetaData DefaultSmallInt = new SmiMetaData(SqlDbType.SmallInt, 2L, 5, 0, SqlCompareOptions.None);

		// Token: 0x04000482 RID: 1154
		internal static readonly SmiMetaData DefaultSmallMoney = new SmiMetaData(SqlDbType.SmallMoney, 4L, 10, 4, SqlCompareOptions.None);

		// Token: 0x04000483 RID: 1155
		internal static readonly SmiMetaData DefaultText_NoCollation = new SmiMetaData(SqlDbType.Text, -1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04000484 RID: 1156
		internal static readonly SmiMetaData DefaultTimestamp = new SmiMetaData(SqlDbType.Timestamp, 8L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04000485 RID: 1157
		internal static readonly SmiMetaData DefaultTinyInt = new SmiMetaData(SqlDbType.TinyInt, 1L, 3, 0, SqlCompareOptions.None);

		// Token: 0x04000486 RID: 1158
		internal static readonly SmiMetaData DefaultVarBinary = new SmiMetaData(SqlDbType.VarBinary, 8000L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04000487 RID: 1159
		internal static readonly SmiMetaData DefaultVarChar_NoCollation = new SmiMetaData(SqlDbType.VarChar, 8000L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04000488 RID: 1160
		internal static readonly SmiMetaData DefaultVariant = new SmiMetaData(SqlDbType.Variant, 8016L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04000489 RID: 1161
		internal static readonly SmiMetaData DefaultXml = new SmiMetaData(SqlDbType.Xml, -1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x0400048A RID: 1162
		internal static readonly SmiMetaData DefaultUdt_NoType = new SmiMetaData(SqlDbType.Udt, 0L, 0, 0, SqlCompareOptions.None);

		// Token: 0x0400048B RID: 1163
		internal static readonly SmiMetaData DefaultStructured = new SmiMetaData(SqlDbType.Structured, 0L, 0, 0, SqlCompareOptions.None);

		// Token: 0x0400048C RID: 1164
		internal static readonly SmiMetaData DefaultDate = new SmiMetaData(SqlDbType.Date, 3L, 10, 0, SqlCompareOptions.None);

		// Token: 0x0400048D RID: 1165
		internal static readonly SmiMetaData DefaultTime = new SmiMetaData(SqlDbType.Time, 5L, 0, 7, SqlCompareOptions.None);

		// Token: 0x0400048E RID: 1166
		internal static readonly SmiMetaData DefaultDateTime2 = new SmiMetaData(SqlDbType.DateTime2, 8L, 0, 7, SqlCompareOptions.None);

		// Token: 0x0400048F RID: 1167
		internal static readonly SmiMetaData DefaultDateTimeOffset = new SmiMetaData(SqlDbType.DateTimeOffset, 10L, 0, 7, SqlCompareOptions.None);

		// Token: 0x04000490 RID: 1168
		private static SmiMetaData[] s_defaultValues = new SmiMetaData[]
		{
			SmiMetaData.DefaultBigInt,
			SmiMetaData.DefaultBinary,
			SmiMetaData.DefaultBit,
			SmiMetaData.DefaultChar_NoCollation,
			SmiMetaData.DefaultDateTime,
			SmiMetaData.DefaultDecimal,
			SmiMetaData.DefaultFloat,
			SmiMetaData.DefaultImage,
			SmiMetaData.DefaultInt,
			SmiMetaData.DefaultMoney,
			SmiMetaData.DefaultNChar_NoCollation,
			SmiMetaData.DefaultNText_NoCollation,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultReal,
			SmiMetaData.DefaultUniqueIdentifier,
			SmiMetaData.DefaultSmallDateTime,
			SmiMetaData.DefaultSmallInt,
			SmiMetaData.DefaultSmallMoney,
			SmiMetaData.DefaultText_NoCollation,
			SmiMetaData.DefaultTimestamp,
			SmiMetaData.DefaultTinyInt,
			SmiMetaData.DefaultVarBinary,
			SmiMetaData.DefaultVarChar_NoCollation,
			SmiMetaData.DefaultVariant,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultXml,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultUdt_NoType,
			SmiMetaData.DefaultStructured,
			SmiMetaData.DefaultDate,
			SmiMetaData.DefaultTime,
			SmiMetaData.DefaultDateTime2,
			SmiMetaData.DefaultDateTimeOffset
		};

		// Token: 0x04000491 RID: 1169
		private static string[] s_typeNameByDatabaseType = new string[]
		{
			"bigint",
			"binary",
			"bit",
			"char",
			"datetime",
			"decimal",
			"float",
			"image",
			"int",
			"money",
			"nchar",
			"ntext",
			"nvarchar",
			"real",
			"uniqueidentifier",
			"smalldatetime",
			"smallint",
			"smallmoney",
			"text",
			"timestamp",
			"tinyint",
			"varbinary",
			"varchar",
			"sql_variant",
			null,
			"xml",
			null,
			null,
			null,
			string.Empty,
			string.Empty,
			"date",
			"time",
			"datetime2",
			"datetimeoffset"
		};
	}
}
