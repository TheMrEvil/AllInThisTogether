using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020001FD RID: 509
	internal sealed class MetaType
	{
		// Token: 0x060018B4 RID: 6324 RVA: 0x000723B8 File Offset: 0x000705B8
		public MetaType(byte precision, byte scale, int fixedLength, bool isFixed, bool isLong, bool isPlp, byte tdsType, byte nullableTdsType, string typeName, Type classType, Type sqlType, SqlDbType sqldbType, DbType dbType, byte propBytes)
		{
			this.Precision = precision;
			this.Scale = scale;
			this.FixedLength = fixedLength;
			this.IsFixed = isFixed;
			this.IsLong = isLong;
			this.IsPlp = isPlp;
			this.TDSType = tdsType;
			this.NullableType = nullableTdsType;
			this.TypeName = typeName;
			this.SqlDbType = sqldbType;
			this.DbType = dbType;
			this.ClassType = classType;
			this.SqlType = sqlType;
			this.PropBytes = propBytes;
			this.IsAnsiType = MetaType._IsAnsiType(sqldbType);
			this.IsBinType = MetaType._IsBinType(sqldbType);
			this.IsCharType = MetaType._IsCharType(sqldbType);
			this.IsNCharType = MetaType._IsNCharType(sqldbType);
			this.IsSizeInCharacters = MetaType._IsSizeInCharacters(sqldbType);
			this.IsNewKatmaiType = MetaType._IsNewKatmaiType(sqldbType);
			this.IsVarTime = MetaType._IsVarTime(sqldbType);
			this.Is70Supported = MetaType._Is70Supported(this.SqlDbType);
			this.Is80Supported = MetaType._Is80Supported(this.SqlDbType);
			this.Is90Supported = MetaType._Is90Supported(this.SqlDbType);
			this.Is100Supported = MetaType._Is100Supported(this.SqlDbType);
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x00006D64 File Offset: 0x00004F64
		public int TypeId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00005D26 File Offset: 0x00003F26
		private static bool _IsAnsiType(SqlDbType type)
		{
			return type == SqlDbType.Char || type == SqlDbType.VarChar || type == SqlDbType.Text;
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x000724D7 File Offset: 0x000706D7
		private static bool _IsSizeInCharacters(SqlDbType type)
		{
			return type == SqlDbType.NChar || type == SqlDbType.NVarChar || type == SqlDbType.Xml || type == SqlDbType.NText;
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x000724EF File Offset: 0x000706EF
		private static bool _IsCharType(SqlDbType type)
		{
			return type == SqlDbType.NChar || type == SqlDbType.NVarChar || type == SqlDbType.NText || type == SqlDbType.Char || type == SqlDbType.VarChar || type == SqlDbType.Text || type == SqlDbType.Xml;
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00072515 File Offset: 0x00070715
		private static bool _IsNCharType(SqlDbType type)
		{
			return type == SqlDbType.NChar || type == SqlDbType.NVarChar || type == SqlDbType.NText || type == SqlDbType.Xml;
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0007252D File Offset: 0x0007072D
		private static bool _IsBinType(SqlDbType type)
		{
			return type == SqlDbType.Image || type == SqlDbType.Binary || type == SqlDbType.VarBinary || type == SqlDbType.Timestamp || type == SqlDbType.Udt || type == (SqlDbType)24;
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0007254D File Offset: 0x0007074D
		private static bool _Is70Supported(SqlDbType type)
		{
			return type != SqlDbType.BigInt && type > SqlDbType.BigInt && type <= SqlDbType.VarChar;
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x00072560 File Offset: 0x00070760
		private static bool _Is80Supported(SqlDbType type)
		{
			return type >= SqlDbType.BigInt && type <= SqlDbType.Variant;
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x00072570 File Offset: 0x00070770
		private static bool _Is90Supported(SqlDbType type)
		{
			return MetaType._Is80Supported(type) || SqlDbType.Xml == type || SqlDbType.Udt == type;
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x00072586 File Offset: 0x00070786
		private static bool _Is100Supported(SqlDbType type)
		{
			return MetaType._Is90Supported(type) || SqlDbType.Date == type || SqlDbType.Time == type || SqlDbType.DateTime2 == type || SqlDbType.DateTimeOffset == type;
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x000725A6 File Offset: 0x000707A6
		private static bool _IsNewKatmaiType(SqlDbType type)
		{
			return SqlDbType.Structured == type;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x000725AD File Offset: 0x000707AD
		internal static bool _IsVarTime(SqlDbType type)
		{
			return type == SqlDbType.Time || type == SqlDbType.DateTime2 || type == SqlDbType.DateTimeOffset;
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x000725C0 File Offset: 0x000707C0
		internal static MetaType GetMetaTypeFromSqlDbType(SqlDbType target, bool isMultiValued)
		{
			switch (target)
			{
			case SqlDbType.BigInt:
				return MetaType.s_metaBigInt;
			case SqlDbType.Binary:
				return MetaType.s_metaBinary;
			case SqlDbType.Bit:
				return MetaType.s_metaBit;
			case SqlDbType.Char:
				return MetaType.s_metaChar;
			case SqlDbType.DateTime:
				return MetaType.s_metaDateTime;
			case SqlDbType.Decimal:
				return MetaType.MetaDecimal;
			case SqlDbType.Float:
				return MetaType.s_metaFloat;
			case SqlDbType.Image:
				return MetaType.MetaImage;
			case SqlDbType.Int:
				return MetaType.s_metaInt;
			case SqlDbType.Money:
				return MetaType.s_metaMoney;
			case SqlDbType.NChar:
				return MetaType.s_metaNChar;
			case SqlDbType.NText:
				return MetaType.MetaNText;
			case SqlDbType.NVarChar:
				return MetaType.MetaNVarChar;
			case SqlDbType.Real:
				return MetaType.s_metaReal;
			case SqlDbType.UniqueIdentifier:
				return MetaType.s_metaUniqueId;
			case SqlDbType.SmallDateTime:
				return MetaType.s_metaSmallDateTime;
			case SqlDbType.SmallInt:
				return MetaType.s_metaSmallInt;
			case SqlDbType.SmallMoney:
				return MetaType.s_metaSmallMoney;
			case SqlDbType.Text:
				return MetaType.MetaText;
			case SqlDbType.Timestamp:
				return MetaType.s_metaTimestamp;
			case SqlDbType.TinyInt:
				return MetaType.s_metaTinyInt;
			case SqlDbType.VarBinary:
				return MetaType.MetaVarBinary;
			case SqlDbType.VarChar:
				return MetaType.s_metaVarChar;
			case SqlDbType.Variant:
				return MetaType.s_metaVariant;
			case (SqlDbType)24:
				return MetaType.s_metaSmallVarBinary;
			case SqlDbType.Xml:
				return MetaType.MetaXml;
			case SqlDbType.Udt:
				return MetaType.MetaUdt;
			case SqlDbType.Structured:
				if (isMultiValued)
				{
					return MetaType.s_metaTable;
				}
				return MetaType.s_metaSUDT;
			case SqlDbType.Date:
				return MetaType.s_metaDate;
			case SqlDbType.Time:
				return MetaType.MetaTime;
			case SqlDbType.DateTime2:
				return MetaType.s_metaDateTime2;
			case SqlDbType.DateTimeOffset:
				return MetaType.MetaDateTimeOffset;
			}
			throw SQL.InvalidSqlDbType(target);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00072734 File Offset: 0x00070934
		internal static MetaType GetMetaTypeFromDbType(DbType target)
		{
			switch (target)
			{
			case DbType.AnsiString:
				return MetaType.s_metaVarChar;
			case DbType.Binary:
				return MetaType.MetaVarBinary;
			case DbType.Byte:
				return MetaType.s_metaTinyInt;
			case DbType.Boolean:
				return MetaType.s_metaBit;
			case DbType.Currency:
				return MetaType.s_metaMoney;
			case DbType.Date:
			case DbType.DateTime:
				return MetaType.s_metaDateTime;
			case DbType.Decimal:
				return MetaType.MetaDecimal;
			case DbType.Double:
				return MetaType.s_metaFloat;
			case DbType.Guid:
				return MetaType.s_metaUniqueId;
			case DbType.Int16:
				return MetaType.s_metaSmallInt;
			case DbType.Int32:
				return MetaType.s_metaInt;
			case DbType.Int64:
				return MetaType.s_metaBigInt;
			case DbType.Object:
				return MetaType.s_metaVariant;
			case DbType.Single:
				return MetaType.s_metaReal;
			case DbType.String:
				return MetaType.MetaNVarChar;
			case DbType.Time:
				return MetaType.s_metaDateTime;
			case DbType.AnsiStringFixedLength:
				return MetaType.s_metaChar;
			case DbType.StringFixedLength:
				return MetaType.s_metaNChar;
			case DbType.Xml:
				return MetaType.MetaXml;
			case DbType.DateTime2:
				return MetaType.s_metaDateTime2;
			case DbType.DateTimeOffset:
				return MetaType.MetaDateTimeOffset;
			}
			throw ADP.DbTypeNotSupported(target, typeof(SqlDbType));
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00072848 File Offset: 0x00070A48
		internal static MetaType GetMaxMetaTypeFromMetaType(MetaType mt)
		{
			SqlDbType sqlDbType = mt.SqlDbType;
			if (sqlDbType <= SqlDbType.NChar)
			{
				if (sqlDbType != SqlDbType.Binary)
				{
					if (sqlDbType == SqlDbType.Char)
					{
						goto IL_3E;
					}
					if (sqlDbType != SqlDbType.NChar)
					{
						return mt;
					}
					goto IL_44;
				}
			}
			else if (sqlDbType <= SqlDbType.VarBinary)
			{
				if (sqlDbType == SqlDbType.NVarChar)
				{
					goto IL_44;
				}
				if (sqlDbType != SqlDbType.VarBinary)
				{
					return mt;
				}
			}
			else
			{
				if (sqlDbType == SqlDbType.VarChar)
				{
					goto IL_3E;
				}
				if (sqlDbType != SqlDbType.Udt)
				{
					return mt;
				}
				return MetaType.s_metaMaxUdt;
			}
			return MetaType.MetaMaxVarBinary;
			IL_3E:
			return MetaType.MetaMaxVarChar;
			IL_44:
			return MetaType.MetaMaxNVarChar;
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x000728A6 File Offset: 0x00070AA6
		internal static MetaType GetMetaTypeFromType(Type dataType)
		{
			return MetaType.GetMetaTypeFromValue(dataType, null, false, true);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x000728B1 File Offset: 0x00070AB1
		internal static MetaType GetMetaTypeFromValue(object value, bool streamAllowed = true)
		{
			return MetaType.GetMetaTypeFromValue(value.GetType(), value, true, streamAllowed);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000728C4 File Offset: 0x00070AC4
		private static MetaType GetMetaTypeFromValue(Type dataType, object value, bool inferLen, bool streamAllowed)
		{
			switch (Type.GetTypeCode(dataType))
			{
			case TypeCode.Empty:
				throw ADP.InvalidDataType(TypeCode.Empty);
			case TypeCode.Object:
				if (dataType == typeof(byte[]))
				{
					if (!inferLen || ((byte[])value).Length <= 8000)
					{
						return MetaType.MetaVarBinary;
					}
					return MetaType.MetaImage;
				}
				else
				{
					if (dataType == typeof(Guid))
					{
						return MetaType.s_metaUniqueId;
					}
					if (dataType == typeof(object))
					{
						return MetaType.s_metaVariant;
					}
					if (dataType == typeof(SqlBinary))
					{
						return MetaType.MetaVarBinary;
					}
					if (dataType == typeof(SqlBoolean))
					{
						return MetaType.s_metaBit;
					}
					if (dataType == typeof(SqlByte))
					{
						return MetaType.s_metaTinyInt;
					}
					if (dataType == typeof(SqlBytes))
					{
						return MetaType.MetaVarBinary;
					}
					if (dataType == typeof(SqlChars))
					{
						return MetaType.MetaNVarChar;
					}
					if (dataType == typeof(SqlDateTime))
					{
						return MetaType.s_metaDateTime;
					}
					if (dataType == typeof(SqlDouble))
					{
						return MetaType.s_metaFloat;
					}
					if (dataType == typeof(SqlGuid))
					{
						return MetaType.s_metaUniqueId;
					}
					if (dataType == typeof(SqlInt16))
					{
						return MetaType.s_metaSmallInt;
					}
					if (dataType == typeof(SqlInt32))
					{
						return MetaType.s_metaInt;
					}
					if (dataType == typeof(SqlInt64))
					{
						return MetaType.s_metaBigInt;
					}
					if (dataType == typeof(SqlMoney))
					{
						return MetaType.s_metaMoney;
					}
					if (dataType == typeof(SqlDecimal))
					{
						return MetaType.MetaDecimal;
					}
					if (dataType == typeof(SqlSingle))
					{
						return MetaType.s_metaReal;
					}
					if (dataType == typeof(SqlXml))
					{
						return MetaType.MetaXml;
					}
					if (dataType == typeof(SqlString))
					{
						if (!inferLen || ((SqlString)value).IsNull)
						{
							return MetaType.MetaNVarChar;
						}
						return MetaType.PromoteStringType(((SqlString)value).Value);
					}
					else
					{
						if (dataType == typeof(IEnumerable<DbDataRecord>) || dataType == typeof(DataTable))
						{
							return MetaType.s_metaTable;
						}
						if (dataType == typeof(TimeSpan))
						{
							return MetaType.MetaTime;
						}
						if (dataType == typeof(DateTimeOffset))
						{
							return MetaType.MetaDateTimeOffset;
						}
						if (SqlUdtInfo.TryGetFromType(dataType) != null)
						{
							return MetaType.MetaUdt;
						}
						if (streamAllowed)
						{
							if (typeof(Stream).IsAssignableFrom(dataType))
							{
								return MetaType.MetaVarBinary;
							}
							if (typeof(TextReader).IsAssignableFrom(dataType))
							{
								return MetaType.MetaNVarChar;
							}
							if (typeof(XmlReader).IsAssignableFrom(dataType))
							{
								return MetaType.MetaXml;
							}
						}
						throw ADP.UnknownDataType(dataType);
					}
				}
				break;
			case TypeCode.DBNull:
				throw ADP.InvalidDataType(TypeCode.DBNull);
			case TypeCode.Boolean:
				return MetaType.s_metaBit;
			case TypeCode.Char:
				throw ADP.InvalidDataType(TypeCode.Char);
			case TypeCode.SByte:
				throw ADP.InvalidDataType(TypeCode.SByte);
			case TypeCode.Byte:
				return MetaType.s_metaTinyInt;
			case TypeCode.Int16:
				return MetaType.s_metaSmallInt;
			case TypeCode.UInt16:
				throw ADP.InvalidDataType(TypeCode.UInt16);
			case TypeCode.Int32:
				return MetaType.s_metaInt;
			case TypeCode.UInt32:
				throw ADP.InvalidDataType(TypeCode.UInt32);
			case TypeCode.Int64:
				return MetaType.s_metaBigInt;
			case TypeCode.UInt64:
				throw ADP.InvalidDataType(TypeCode.UInt64);
			case TypeCode.Single:
				return MetaType.s_metaReal;
			case TypeCode.Double:
				return MetaType.s_metaFloat;
			case TypeCode.Decimal:
				return MetaType.MetaDecimal;
			case TypeCode.DateTime:
				return MetaType.s_metaDateTime;
			case TypeCode.String:
				if (!inferLen)
				{
					return MetaType.MetaNVarChar;
				}
				return MetaType.PromoteStringType((string)value);
			}
			throw ADP.UnknownDataTypeCode(dataType, Type.GetTypeCode(dataType));
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x00072C7C File Offset: 0x00070E7C
		internal static object GetNullSqlValue(Type sqlType)
		{
			if (sqlType == typeof(SqlSingle))
			{
				return SqlSingle.Null;
			}
			if (sqlType == typeof(SqlString))
			{
				return SqlString.Null;
			}
			if (sqlType == typeof(SqlDouble))
			{
				return SqlDouble.Null;
			}
			if (sqlType == typeof(SqlBinary))
			{
				return SqlBinary.Null;
			}
			if (sqlType == typeof(SqlGuid))
			{
				return SqlGuid.Null;
			}
			if (sqlType == typeof(SqlBoolean))
			{
				return SqlBoolean.Null;
			}
			if (sqlType == typeof(SqlByte))
			{
				return SqlByte.Null;
			}
			if (sqlType == typeof(SqlInt16))
			{
				return SqlInt16.Null;
			}
			if (sqlType == typeof(SqlInt32))
			{
				return SqlInt32.Null;
			}
			if (sqlType == typeof(SqlInt64))
			{
				return SqlInt64.Null;
			}
			if (sqlType == typeof(SqlDecimal))
			{
				return SqlDecimal.Null;
			}
			if (sqlType == typeof(SqlDateTime))
			{
				return SqlDateTime.Null;
			}
			if (sqlType == typeof(SqlMoney))
			{
				return SqlMoney.Null;
			}
			if (sqlType == typeof(SqlXml))
			{
				return SqlXml.Null;
			}
			if (sqlType == typeof(object))
			{
				return DBNull.Value;
			}
			if (sqlType == typeof(IEnumerable<DbDataRecord>))
			{
				return DBNull.Value;
			}
			if (sqlType == typeof(DataTable))
			{
				return DBNull.Value;
			}
			if (sqlType == typeof(DateTime))
			{
				return DBNull.Value;
			}
			if (sqlType == typeof(TimeSpan))
			{
				return DBNull.Value;
			}
			sqlType == typeof(DateTimeOffset);
			return DBNull.Value;
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x00072EA8 File Offset: 0x000710A8
		internal static MetaType PromoteStringType(string s)
		{
			if (s.Length << 1 > 8000)
			{
				return MetaType.s_metaVarChar;
			}
			return MetaType.MetaNVarChar;
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x00072EC4 File Offset: 0x000710C4
		internal static object GetComValueFromSqlVariant(object sqlVal)
		{
			object result = null;
			if (ADP.IsNull(sqlVal))
			{
				return result;
			}
			if (sqlVal is SqlSingle)
			{
				result = ((SqlSingle)sqlVal).Value;
			}
			else if (sqlVal is SqlString)
			{
				result = ((SqlString)sqlVal).Value;
			}
			else if (sqlVal is SqlDouble)
			{
				result = ((SqlDouble)sqlVal).Value;
			}
			else if (sqlVal is SqlBinary)
			{
				result = ((SqlBinary)sqlVal).Value;
			}
			else if (sqlVal is SqlGuid)
			{
				result = ((SqlGuid)sqlVal).Value;
			}
			else if (sqlVal is SqlBoolean)
			{
				result = ((SqlBoolean)sqlVal).Value;
			}
			else if (sqlVal is SqlByte)
			{
				result = ((SqlByte)sqlVal).Value;
			}
			else if (sqlVal is SqlInt16)
			{
				result = ((SqlInt16)sqlVal).Value;
			}
			else if (sqlVal is SqlInt32)
			{
				result = ((SqlInt32)sqlVal).Value;
			}
			else if (sqlVal is SqlInt64)
			{
				result = ((SqlInt64)sqlVal).Value;
			}
			else if (sqlVal is SqlDecimal)
			{
				result = ((SqlDecimal)sqlVal).Value;
			}
			else if (sqlVal is SqlDateTime)
			{
				result = ((SqlDateTime)sqlVal).Value;
			}
			else if (sqlVal is SqlMoney)
			{
				result = ((SqlMoney)sqlVal).Value;
			}
			else if (sqlVal is SqlXml)
			{
				result = ((SqlXml)sqlVal).Value;
			}
			return result;
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x00073093 File Offset: 0x00071293
		[Conditional("DEBUG")]
		private static void AssertIsUserDefinedTypeInstance(object sqlValue, string failedAssertMessage)
		{
			SqlUserDefinedTypeAttribute[] array = (SqlUserDefinedTypeAttribute[])sqlValue.GetType().GetCustomAttributes(typeof(SqlUserDefinedTypeAttribute), true);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x000730B4 File Offset: 0x000712B4
		internal static object GetSqlValueFromComVariant(object comVal)
		{
			object result = null;
			if (comVal != null && DBNull.Value != comVal)
			{
				if (comVal is float)
				{
					result = new SqlSingle((float)comVal);
				}
				else if (comVal is string)
				{
					result = new SqlString((string)comVal);
				}
				else if (comVal is double)
				{
					result = new SqlDouble((double)comVal);
				}
				else if (comVal is byte[])
				{
					result = new SqlBinary((byte[])comVal);
				}
				else if (comVal is char)
				{
					result = new SqlString(((char)comVal).ToString());
				}
				else if (comVal is char[])
				{
					result = new SqlChars((char[])comVal);
				}
				else if (comVal is Guid)
				{
					result = new SqlGuid((Guid)comVal);
				}
				else if (comVal is bool)
				{
					result = new SqlBoolean((bool)comVal);
				}
				else if (comVal is byte)
				{
					result = new SqlByte((byte)comVal);
				}
				else if (comVal is short)
				{
					result = new SqlInt16((short)comVal);
				}
				else if (comVal is int)
				{
					result = new SqlInt32((int)comVal);
				}
				else if (comVal is long)
				{
					result = new SqlInt64((long)comVal);
				}
				else if (comVal is decimal)
				{
					result = new SqlDecimal((decimal)comVal);
				}
				else if (comVal is DateTime)
				{
					result = new SqlDateTime((DateTime)comVal);
				}
				else if (comVal is XmlReader)
				{
					result = new SqlXml((XmlReader)comVal);
				}
				else if (comVal is TimeSpan || comVal is DateTimeOffset)
				{
					result = comVal;
				}
			}
			return result;
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00073298 File Offset: 0x00071498
		internal static SqlDbType GetSqlDbTypeFromOleDbType(short dbType, string typeName)
		{
			return SqlDbType.Variant;
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0007329C File Offset: 0x0007149C
		internal static MetaType GetSqlDataType(int tdsType, uint userType, int length)
		{
			if (tdsType <= 165)
			{
				if (tdsType <= 111)
				{
					switch (tdsType)
					{
					case 31:
					case 32:
					case 33:
					case 44:
					case 46:
					case 49:
					case 51:
					case 53:
					case 54:
					case 55:
					case 57:
						goto IL_279;
					case 34:
						return MetaType.MetaImage;
					case 35:
						return MetaType.MetaText;
					case 36:
						return MetaType.s_metaUniqueId;
					case 37:
						return MetaType.s_metaSmallVarBinary;
					case 38:
						if (4 > length)
						{
							if (2 != length)
							{
								return MetaType.s_metaTinyInt;
							}
							return MetaType.s_metaSmallInt;
						}
						else
						{
							if (4 != length)
							{
								return MetaType.s_metaBigInt;
							}
							return MetaType.s_metaInt;
						}
						break;
					case 39:
						goto IL_1C6;
					case 40:
						return MetaType.s_metaDate;
					case 41:
						return MetaType.MetaTime;
					case 42:
						return MetaType.s_metaDateTime2;
					case 43:
						return MetaType.MetaDateTimeOffset;
					case 45:
						goto IL_1CC;
					case 47:
						goto IL_1E3;
					case 48:
						return MetaType.s_metaTinyInt;
					case 50:
						break;
					case 52:
						return MetaType.s_metaSmallInt;
					case 56:
						return MetaType.s_metaInt;
					case 58:
						return MetaType.s_metaSmallDateTime;
					case 59:
						return MetaType.s_metaReal;
					case 60:
						return MetaType.s_metaMoney;
					case 61:
						return MetaType.s_metaDateTime;
					case 62:
						return MetaType.s_metaFloat;
					default:
						switch (tdsType)
						{
						case 98:
							return MetaType.s_metaVariant;
						case 99:
							return MetaType.MetaNText;
						case 100:
						case 101:
						case 102:
						case 103:
						case 105:
						case 107:
							goto IL_279;
						case 104:
							break;
						case 106:
						case 108:
							return MetaType.MetaDecimal;
						case 109:
							if (4 != length)
							{
								return MetaType.s_metaFloat;
							}
							return MetaType.s_metaReal;
						case 110:
							if (4 != length)
							{
								return MetaType.s_metaMoney;
							}
							return MetaType.s_metaSmallMoney;
						case 111:
							if (4 != length)
							{
								return MetaType.s_metaDateTime;
							}
							return MetaType.s_metaSmallDateTime;
						default:
							goto IL_279;
						}
						break;
					}
					return MetaType.s_metaBit;
				}
				if (tdsType == 122)
				{
					return MetaType.s_metaSmallMoney;
				}
				if (tdsType == 127)
				{
					return MetaType.s_metaBigInt;
				}
				if (tdsType != 165)
				{
					goto IL_279;
				}
				return MetaType.MetaVarBinary;
			}
			else if (tdsType <= 173)
			{
				if (tdsType != 167)
				{
					if (tdsType != 173)
					{
						goto IL_279;
					}
					goto IL_1CC;
				}
			}
			else
			{
				if (tdsType == 175)
				{
					goto IL_1E3;
				}
				if (tdsType == 231)
				{
					return MetaType.MetaNVarChar;
				}
				switch (tdsType)
				{
				case 239:
					return MetaType.s_metaNChar;
				case 240:
					return MetaType.MetaUdt;
				case 241:
					return MetaType.MetaXml;
				case 242:
					goto IL_279;
				case 243:
					return MetaType.s_metaTable;
				default:
					goto IL_279;
				}
			}
			IL_1C6:
			return MetaType.s_metaVarChar;
			IL_1CC:
			if (80U != userType)
			{
				return MetaType.s_metaBinary;
			}
			return MetaType.s_metaTimestamp;
			IL_1E3:
			return MetaType.s_metaChar;
			IL_279:
			throw SQL.InvalidSqlDbType((SqlDbType)tdsType);
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00073528 File Offset: 0x00071728
		internal static MetaType GetDefaultMetaType()
		{
			return MetaType.MetaNVarChar;
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0007352F File Offset: 0x0007172F
		internal static string GetStringFromXml(XmlReader xmlreader)
		{
			return new SqlXml(xmlreader).Value;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0007353C File Offset: 0x0007173C
		public static TdsDateTime FromDateTime(DateTime dateTime, byte cb)
		{
			TdsDateTime result = default(TdsDateTime);
			SqlDateTime sqlDateTime;
			if (cb == 8)
			{
				sqlDateTime = new SqlDateTime(dateTime);
				result.time = sqlDateTime.TimeTicks;
			}
			else
			{
				sqlDateTime = new SqlDateTime(dateTime.AddSeconds(30.0));
				result.time = sqlDateTime.TimeTicks / SqlDateTime.SQLTicksPerMinute;
			}
			result.days = sqlDateTime.DayTicks;
			return result;
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x000735A8 File Offset: 0x000717A8
		public static DateTime ToDateTime(int sqlDays, int sqlTime, int length)
		{
			if (length == 4)
			{
				return new SqlDateTime(sqlDays, sqlTime * SqlDateTime.SQLTicksPerMinute).Value;
			}
			return new SqlDateTime(sqlDays, sqlTime).Value;
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x000735DE File Offset: 0x000717DE
		internal static int GetTimeSizeFromScale(byte scale)
		{
			if (scale <= 2)
			{
				return 3;
			}
			if (scale <= 4)
			{
				return 4;
			}
			return 5;
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x000735F0 File Offset: 0x000717F0
		// Note: this type is marked as 'beforefieldinit'.
		static MetaType()
		{
		}

		// Token: 0x04000FCD RID: 4045
		internal readonly Type ClassType;

		// Token: 0x04000FCE RID: 4046
		internal readonly Type SqlType;

		// Token: 0x04000FCF RID: 4047
		internal readonly int FixedLength;

		// Token: 0x04000FD0 RID: 4048
		internal readonly bool IsFixed;

		// Token: 0x04000FD1 RID: 4049
		internal readonly bool IsLong;

		// Token: 0x04000FD2 RID: 4050
		internal readonly bool IsPlp;

		// Token: 0x04000FD3 RID: 4051
		internal readonly byte Precision;

		// Token: 0x04000FD4 RID: 4052
		internal readonly byte Scale;

		// Token: 0x04000FD5 RID: 4053
		internal readonly byte TDSType;

		// Token: 0x04000FD6 RID: 4054
		internal readonly byte NullableType;

		// Token: 0x04000FD7 RID: 4055
		internal readonly string TypeName;

		// Token: 0x04000FD8 RID: 4056
		internal readonly SqlDbType SqlDbType;

		// Token: 0x04000FD9 RID: 4057
		internal readonly DbType DbType;

		// Token: 0x04000FDA RID: 4058
		internal readonly byte PropBytes;

		// Token: 0x04000FDB RID: 4059
		internal readonly bool IsAnsiType;

		// Token: 0x04000FDC RID: 4060
		internal readonly bool IsBinType;

		// Token: 0x04000FDD RID: 4061
		internal readonly bool IsCharType;

		// Token: 0x04000FDE RID: 4062
		internal readonly bool IsNCharType;

		// Token: 0x04000FDF RID: 4063
		internal readonly bool IsSizeInCharacters;

		// Token: 0x04000FE0 RID: 4064
		internal readonly bool IsNewKatmaiType;

		// Token: 0x04000FE1 RID: 4065
		internal readonly bool IsVarTime;

		// Token: 0x04000FE2 RID: 4066
		internal readonly bool Is70Supported;

		// Token: 0x04000FE3 RID: 4067
		internal readonly bool Is80Supported;

		// Token: 0x04000FE4 RID: 4068
		internal readonly bool Is90Supported;

		// Token: 0x04000FE5 RID: 4069
		internal readonly bool Is100Supported;

		// Token: 0x04000FE6 RID: 4070
		private static readonly MetaType s_metaBigInt = new MetaType(19, byte.MaxValue, 8, true, false, false, 127, 38, "bigint", typeof(long), typeof(SqlInt64), SqlDbType.BigInt, DbType.Int64, 0);

		// Token: 0x04000FE7 RID: 4071
		private static readonly MetaType s_metaFloat = new MetaType(15, byte.MaxValue, 8, true, false, false, 62, 109, "float", typeof(double), typeof(SqlDouble), SqlDbType.Float, DbType.Double, 0);

		// Token: 0x04000FE8 RID: 4072
		private static readonly MetaType s_metaReal = new MetaType(7, byte.MaxValue, 4, true, false, false, 59, 109, "real", typeof(float), typeof(SqlSingle), SqlDbType.Real, DbType.Single, 0);

		// Token: 0x04000FE9 RID: 4073
		private static readonly MetaType s_metaBinary = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 173, 173, "binary", typeof(byte[]), typeof(SqlBinary), SqlDbType.Binary, DbType.Binary, 2);

		// Token: 0x04000FEA RID: 4074
		private static readonly MetaType s_metaTimestamp = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 173, 173, "timestamp", typeof(byte[]), typeof(SqlBinary), SqlDbType.Timestamp, DbType.Binary, 2);

		// Token: 0x04000FEB RID: 4075
		internal static readonly MetaType MetaVarBinary = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 165, 165, "varbinary", typeof(byte[]), typeof(SqlBinary), SqlDbType.VarBinary, DbType.Binary, 2);

		// Token: 0x04000FEC RID: 4076
		internal static readonly MetaType MetaMaxVarBinary = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, true, 165, 165, "varbinary", typeof(byte[]), typeof(SqlBinary), SqlDbType.VarBinary, DbType.Binary, 2);

		// Token: 0x04000FED RID: 4077
		private static readonly MetaType s_metaSmallVarBinary = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 37, 173, ADP.StrEmpty, typeof(byte[]), typeof(SqlBinary), (SqlDbType)24, DbType.Binary, 2);

		// Token: 0x04000FEE RID: 4078
		internal static readonly MetaType MetaImage = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, false, 34, 34, "image", typeof(byte[]), typeof(SqlBinary), SqlDbType.Image, DbType.Binary, 0);

		// Token: 0x04000FEF RID: 4079
		private static readonly MetaType s_metaBit = new MetaType(byte.MaxValue, byte.MaxValue, 1, true, false, false, 50, 104, "bit", typeof(bool), typeof(SqlBoolean), SqlDbType.Bit, DbType.Boolean, 0);

		// Token: 0x04000FF0 RID: 4080
		private static readonly MetaType s_metaTinyInt = new MetaType(3, byte.MaxValue, 1, true, false, false, 48, 38, "tinyint", typeof(byte), typeof(SqlByte), SqlDbType.TinyInt, DbType.Byte, 0);

		// Token: 0x04000FF1 RID: 4081
		private static readonly MetaType s_metaSmallInt = new MetaType(5, byte.MaxValue, 2, true, false, false, 52, 38, "smallint", typeof(short), typeof(SqlInt16), SqlDbType.SmallInt, DbType.Int16, 0);

		// Token: 0x04000FF2 RID: 4082
		private static readonly MetaType s_metaInt = new MetaType(10, byte.MaxValue, 4, true, false, false, 56, 38, "int", typeof(int), typeof(SqlInt32), SqlDbType.Int, DbType.Int32, 0);

		// Token: 0x04000FF3 RID: 4083
		private static readonly MetaType s_metaChar = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 175, 175, "char", typeof(string), typeof(SqlString), SqlDbType.Char, DbType.AnsiStringFixedLength, 7);

		// Token: 0x04000FF4 RID: 4084
		private static readonly MetaType s_metaVarChar = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 167, 167, "varchar", typeof(string), typeof(SqlString), SqlDbType.VarChar, DbType.AnsiString, 7);

		// Token: 0x04000FF5 RID: 4085
		internal static readonly MetaType MetaMaxVarChar = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, true, 167, 167, "varchar", typeof(string), typeof(SqlString), SqlDbType.VarChar, DbType.AnsiString, 7);

		// Token: 0x04000FF6 RID: 4086
		internal static readonly MetaType MetaText = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, false, 35, 35, "text", typeof(string), typeof(SqlString), SqlDbType.Text, DbType.AnsiString, 0);

		// Token: 0x04000FF7 RID: 4087
		private static readonly MetaType s_metaNChar = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 239, 239, "nchar", typeof(string), typeof(SqlString), SqlDbType.NChar, DbType.StringFixedLength, 7);

		// Token: 0x04000FF8 RID: 4088
		internal static readonly MetaType MetaNVarChar = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 231, 231, "nvarchar", typeof(string), typeof(SqlString), SqlDbType.NVarChar, DbType.String, 7);

		// Token: 0x04000FF9 RID: 4089
		internal static readonly MetaType MetaMaxNVarChar = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, true, 231, 231, "nvarchar", typeof(string), typeof(SqlString), SqlDbType.NVarChar, DbType.String, 7);

		// Token: 0x04000FFA RID: 4090
		internal static readonly MetaType MetaNText = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, false, 99, 99, "ntext", typeof(string), typeof(SqlString), SqlDbType.NText, DbType.String, 7);

		// Token: 0x04000FFB RID: 4091
		internal static readonly MetaType MetaDecimal = new MetaType(38, 4, 17, true, false, false, 108, 108, "decimal", typeof(decimal), typeof(SqlDecimal), SqlDbType.Decimal, DbType.Decimal, 2);

		// Token: 0x04000FFC RID: 4092
		internal static readonly MetaType MetaXml = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, true, 241, 241, "xml", typeof(string), typeof(SqlXml), SqlDbType.Xml, DbType.Xml, 0);

		// Token: 0x04000FFD RID: 4093
		private static readonly MetaType s_metaDateTime = new MetaType(23, 3, 8, true, false, false, 61, 111, "datetime", typeof(DateTime), typeof(SqlDateTime), SqlDbType.DateTime, DbType.DateTime, 0);

		// Token: 0x04000FFE RID: 4094
		private static readonly MetaType s_metaSmallDateTime = new MetaType(16, 0, 4, true, false, false, 58, 111, "smalldatetime", typeof(DateTime), typeof(SqlDateTime), SqlDbType.SmallDateTime, DbType.DateTime, 0);

		// Token: 0x04000FFF RID: 4095
		private static readonly MetaType s_metaMoney = new MetaType(19, byte.MaxValue, 8, true, false, false, 60, 110, "money", typeof(decimal), typeof(SqlMoney), SqlDbType.Money, DbType.Currency, 0);

		// Token: 0x04001000 RID: 4096
		private static readonly MetaType s_metaSmallMoney = new MetaType(10, byte.MaxValue, 4, true, false, false, 122, 110, "smallmoney", typeof(decimal), typeof(SqlMoney), SqlDbType.SmallMoney, DbType.Currency, 0);

		// Token: 0x04001001 RID: 4097
		private static readonly MetaType s_metaUniqueId = new MetaType(byte.MaxValue, byte.MaxValue, 16, true, false, false, 36, 36, "uniqueidentifier", typeof(Guid), typeof(SqlGuid), SqlDbType.UniqueIdentifier, DbType.Guid, 0);

		// Token: 0x04001002 RID: 4098
		private static readonly MetaType s_metaVariant = new MetaType(byte.MaxValue, byte.MaxValue, -1, true, false, false, 98, 98, "sql_variant", typeof(object), typeof(object), SqlDbType.Variant, DbType.Object, 0);

		// Token: 0x04001003 RID: 4099
		internal static readonly MetaType MetaUdt = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, true, 240, 240, "udt", typeof(object), typeof(object), SqlDbType.Udt, DbType.Object, 0);

		// Token: 0x04001004 RID: 4100
		private static readonly MetaType s_metaMaxUdt = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, true, 240, 240, "udt", typeof(object), typeof(object), SqlDbType.Udt, DbType.Object, 0);

		// Token: 0x04001005 RID: 4101
		private static readonly MetaType s_metaTable = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 243, 243, "table", typeof(IEnumerable<DbDataRecord>), typeof(IEnumerable<DbDataRecord>), SqlDbType.Structured, DbType.Object, 0);

		// Token: 0x04001006 RID: 4102
		private static readonly MetaType s_metaSUDT = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 31, 31, "", typeof(SqlDataRecord), typeof(SqlDataRecord), SqlDbType.Structured, DbType.Object, 0);

		// Token: 0x04001007 RID: 4103
		private static readonly MetaType s_metaDate = new MetaType(byte.MaxValue, byte.MaxValue, 3, true, false, false, 40, 40, "date", typeof(DateTime), typeof(DateTime), SqlDbType.Date, DbType.Date, 0);

		// Token: 0x04001008 RID: 4104
		internal static readonly MetaType MetaTime = new MetaType(byte.MaxValue, 7, -1, false, false, false, 41, 41, "time", typeof(TimeSpan), typeof(TimeSpan), SqlDbType.Time, DbType.Time, 1);

		// Token: 0x04001009 RID: 4105
		private static readonly MetaType s_metaDateTime2 = new MetaType(byte.MaxValue, 7, -1, false, false, false, 42, 42, "datetime2", typeof(DateTime), typeof(DateTime), SqlDbType.DateTime2, DbType.DateTime2, 1);

		// Token: 0x0400100A RID: 4106
		internal static readonly MetaType MetaDateTimeOffset = new MetaType(byte.MaxValue, 7, -1, false, false, false, 43, 43, "datetimeoffset", typeof(DateTimeOffset), typeof(DateTimeOffset), SqlDbType.DateTimeOffset, DbType.DateTimeOffset, 1);

		// Token: 0x020001FE RID: 510
		private static class MetaTypeName
		{
			// Token: 0x0400100B RID: 4107
			public const string BIGINT = "bigint";

			// Token: 0x0400100C RID: 4108
			public const string BINARY = "binary";

			// Token: 0x0400100D RID: 4109
			public const string BIT = "bit";

			// Token: 0x0400100E RID: 4110
			public const string CHAR = "char";

			// Token: 0x0400100F RID: 4111
			public const string DATETIME = "datetime";

			// Token: 0x04001010 RID: 4112
			public const string DECIMAL = "decimal";

			// Token: 0x04001011 RID: 4113
			public const string FLOAT = "float";

			// Token: 0x04001012 RID: 4114
			public const string IMAGE = "image";

			// Token: 0x04001013 RID: 4115
			public const string INT = "int";

			// Token: 0x04001014 RID: 4116
			public const string MONEY = "money";

			// Token: 0x04001015 RID: 4117
			public const string NCHAR = "nchar";

			// Token: 0x04001016 RID: 4118
			public const string NTEXT = "ntext";

			// Token: 0x04001017 RID: 4119
			public const string NVARCHAR = "nvarchar";

			// Token: 0x04001018 RID: 4120
			public const string REAL = "real";

			// Token: 0x04001019 RID: 4121
			public const string ROWGUID = "uniqueidentifier";

			// Token: 0x0400101A RID: 4122
			public const string SMALLDATETIME = "smalldatetime";

			// Token: 0x0400101B RID: 4123
			public const string SMALLINT = "smallint";

			// Token: 0x0400101C RID: 4124
			public const string SMALLMONEY = "smallmoney";

			// Token: 0x0400101D RID: 4125
			public const string TEXT = "text";

			// Token: 0x0400101E RID: 4126
			public const string TIMESTAMP = "timestamp";

			// Token: 0x0400101F RID: 4127
			public const string TINYINT = "tinyint";

			// Token: 0x04001020 RID: 4128
			public const string UDT = "udt";

			// Token: 0x04001021 RID: 4129
			public const string VARBINARY = "varbinary";

			// Token: 0x04001022 RID: 4130
			public const string VARCHAR = "varchar";

			// Token: 0x04001023 RID: 4131
			public const string VARIANT = "sql_variant";

			// Token: 0x04001024 RID: 4132
			public const string XML = "xml";

			// Token: 0x04001025 RID: 4133
			public const string TABLE = "table";

			// Token: 0x04001026 RID: 4134
			public const string DATE = "date";

			// Token: 0x04001027 RID: 4135
			public const string TIME = "time";

			// Token: 0x04001028 RID: 4136
			public const string DATETIME2 = "datetime2";

			// Token: 0x04001029 RID: 4137
			public const string DATETIMEOFFSET = "datetimeoffset";
		}
	}
}
