using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200003F RID: 63
	internal static class ValueUtilsSmi
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x00009602 File Offset: 0x00007802
		internal static bool IsDBNull(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			return ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000960C File Offset: 0x0000780C
		internal static bool GetBoolean(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Boolean))
			{
				return ValueUtilsSmi.GetBoolean_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (bool)value;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000964C File Offset: 0x0000784C
		internal static byte GetByte(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Byte))
			{
				return ValueUtilsSmi.GetByte_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (byte)value;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000968C File Offset: 0x0000788C
		private static long GetBytesConversion(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, long fieldOffset, byte[] buffer, int bufferOffset, int length, bool throwOnNull)
		{
			object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
			if (sqlValue == null)
			{
				throw ADP.InvalidCast();
			}
			SqlBinary sqlBinary = (SqlBinary)sqlValue;
			if (sqlBinary.IsNull)
			{
				if (throwOnNull)
				{
					throw SQL.SqlNullValue();
				}
				return 0L;
			}
			else
			{
				if (buffer == null)
				{
					return (long)sqlBinary.Length;
				}
				length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength * 2L, (long)sqlBinary.Length, fieldOffset, buffer.Length, bufferOffset, length);
				Array.Copy(sqlBinary.Value, checked((int)fieldOffset), buffer, bufferOffset, length);
				return (long)length;
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00009718 File Offset: 0x00007918
		internal static long GetBytes(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiExtendedMetaData metaData, long fieldOffset, byte[] buffer, int bufferOffset, int length, bool throwOnNull)
		{
			if ((-1L != metaData.MaxLength && (SqlDbType.VarChar == metaData.SqlDbType || SqlDbType.NVarChar == metaData.SqlDbType || SqlDbType.Char == metaData.SqlDbType || SqlDbType.NChar == metaData.SqlDbType)) || SqlDbType.Xml == metaData.SqlDbType)
			{
				throw SQL.NonBlobColumn(metaData.Name);
			}
			return ValueUtilsSmi.GetBytesInternal(sink, getters, ordinal, metaData, fieldOffset, buffer, bufferOffset, length, throwOnNull);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00009780 File Offset: 0x00007980
		internal static long GetBytesInternal(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, long fieldOffset, byte[] buffer, int bufferOffset, int length, bool throwOnNull)
		{
			if (!ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.ByteArray))
			{
				return ValueUtilsSmi.GetBytesConversion(sink, getters, ordinal, metaData, fieldOffset, buffer, bufferOffset, length, throwOnNull);
			}
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				if (throwOnNull)
				{
					throw SQL.SqlNullValue();
				}
				ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, 0L, fieldOffset, buffer.Length, bufferOffset, length);
				return 0L;
			}
			else
			{
				long bytesLength_Unchecked = ValueUtilsSmi.GetBytesLength_Unchecked(sink, getters, ordinal);
				if (buffer == null)
				{
					return bytesLength_Unchecked;
				}
				if (MetaDataUtilsSmi.IsCharOrXmlType(metaData.SqlDbType))
				{
					length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength * 2L, bytesLength_Unchecked, fieldOffset, buffer.Length, bufferOffset, length);
				}
				else
				{
					length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, bytesLength_Unchecked, fieldOffset, buffer.Length, bufferOffset, length);
				}
				if (length > 0)
				{
					length = ValueUtilsSmi.GetBytes_Unchecked(sink, getters, ordinal, fieldOffset, buffer, bufferOffset, length);
				}
				return (long)length;
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000985C File Offset: 0x00007A5C
		internal static long GetChars(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.CharArray))
			{
				long charsLength_Unchecked = ValueUtilsSmi.GetCharsLength_Unchecked(sink, getters, ordinal);
				if (buffer == null)
				{
					return charsLength_Unchecked;
				}
				length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, charsLength_Unchecked, fieldOffset, buffer.Length, bufferOffset, length);
				if (length > 0)
				{
					length = ValueUtilsSmi.GetChars_Unchecked(sink, getters, ordinal, fieldOffset, buffer, bufferOffset, length);
				}
				return (long)length;
			}
			else
			{
				string text = (string)ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
				if (text == null)
				{
					throw ADP.InvalidCast();
				}
				if (buffer == null)
				{
					return (long)text.Length;
				}
				length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength * 2L, (long)text.Length, fieldOffset, buffer.Length, bufferOffset, length);
				text.CopyTo(checked((int)fieldOffset), buffer, bufferOffset, length);
				return (long)length;
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00009920 File Offset: 0x00007B20
		internal static DateTime GetDateTime(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.DateTime))
			{
				return ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (DateTime)value;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00009960 File Offset: 0x00007B60
		internal static DateTimeOffset GetDateTimeOffset(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, bool gettersSupportKatmaiDateTime)
		{
			if (gettersSupportKatmaiDateTime)
			{
				return ValueUtilsSmi.GetDateTimeOffset(sink, (SmiTypedGetterSetter)getters, ordinal, metaData);
			}
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (DateTimeOffset)value;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000099A1 File Offset: 0x00007BA1
		internal static DateTimeOffset GetDateTimeOffset(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.DateTimeOffset))
			{
				return ValueUtilsSmi.GetDateTimeOffset_Unchecked(sink, getters, ordinal);
			}
			return (DateTimeOffset)ValueUtilsSmi.GetValue200(sink, getters, ordinal, metaData);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000099CC File Offset: 0x00007BCC
		internal static decimal GetDecimal(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Decimal))
			{
				return ValueUtilsSmi.GetDecimal_PossiblyMoney(sink, getters, ordinal, metaData);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (decimal)value;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00009A10 File Offset: 0x00007C10
		internal static double GetDouble(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Double))
			{
				return ValueUtilsSmi.GetDouble_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (double)value;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00009A50 File Offset: 0x00007C50
		internal static Guid GetGuid(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Guid))
			{
				return ValueUtilsSmi.GetGuid_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (Guid)value;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00009A94 File Offset: 0x00007C94
		internal static short GetInt16(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Int16))
			{
				return ValueUtilsSmi.GetInt16_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (short)value;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00009AD4 File Offset: 0x00007CD4
		internal static int GetInt32(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Int32))
			{
				return ValueUtilsSmi.GetInt32_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (int)value;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00009B18 File Offset: 0x00007D18
		internal static long GetInt64(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Int64))
			{
				return ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (long)value;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00009B5C File Offset: 0x00007D5C
		internal static float GetSingle(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Single))
			{
				return ValueUtilsSmi.GetSingle_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (float)value;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00009BA0 File Offset: 0x00007DA0
		internal static SqlBinary GetSqlBinary(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlBinary))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					return SqlBinary.Null;
				}
				return ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				return (SqlBinary)sqlValue;
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00009BEC File Offset: 0x00007DEC
		internal static SqlBoolean GetSqlBoolean(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlBoolean))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					return SqlBoolean.Null;
				}
				return new SqlBoolean(ValueUtilsSmi.GetBoolean_Unchecked(sink, getters, ordinal));
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				return (SqlBoolean)sqlValue;
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00009C3C File Offset: 0x00007E3C
		internal static SqlByte GetSqlByte(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlByte))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					return SqlByte.Null;
				}
				return new SqlByte(ValueUtilsSmi.GetByte_Unchecked(sink, getters, ordinal));
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				return (SqlByte)sqlValue;
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00009C8C File Offset: 0x00007E8C
		internal static SqlBytes GetSqlBytes(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlBytes result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlBytes))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlBytes.Null;
				}
				else
				{
					long bytesLength_Unchecked = ValueUtilsSmi.GetBytesLength_Unchecked(sink, getters, ordinal);
					if (0L <= bytesLength_Unchecked && bytesLength_Unchecked < 8000L)
					{
						result = new SqlBytes(ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal));
					}
					else
					{
						result = new SqlBytes(ValueUtilsSmi.CopyIntoNewSmiScratchStream(new SmiGettersStream(sink, getters, ordinal, metaData), sink));
					}
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				SqlBinary sqlBinary = (SqlBinary)sqlValue;
				if (sqlBinary.IsNull)
				{
					result = SqlBytes.Null;
				}
				else
				{
					result = new SqlBytes(sqlBinary.Value);
				}
			}
			return result;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00009D2C File Offset: 0x00007F2C
		internal static SqlChars GetSqlChars(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlChars result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlChars))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlChars.Null;
				}
				else
				{
					result = new SqlChars(ValueUtilsSmi.GetCharArray_Unchecked(sink, getters, ordinal));
				}
			}
			else if (SqlDbType.Xml == metaData.SqlDbType)
			{
				SqlXml sqlXml_Unchecked = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal);
				if (sqlXml_Unchecked.IsNull)
				{
					result = SqlChars.Null;
				}
				else
				{
					result = new SqlChars(sqlXml_Unchecked.Value.ToCharArray());
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				SqlString sqlString = (SqlString)sqlValue;
				if (sqlString.IsNull)
				{
					result = SqlChars.Null;
				}
				else
				{
					result = new SqlChars(sqlString.Value.ToCharArray());
				}
			}
			return result;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00009DDC File Offset: 0x00007FDC
		internal static SqlDateTime GetSqlDateTime(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlDateTime result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlDateTime))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlDateTime.Null;
				}
				else
				{
					DateTime dateTime_Unchecked = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
					result = new SqlDateTime(dateTime_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlDateTime)sqlValue;
			}
			return result;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00009E34 File Offset: 0x00008034
		internal static SqlDecimal GetSqlDecimal(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlDecimal result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlDecimal))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlDecimal.Null;
				}
				else
				{
					result = ValueUtilsSmi.GetSqlDecimal_Unchecked(sink, getters, ordinal);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlDecimal)sqlValue;
			}
			return result;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00009E84 File Offset: 0x00008084
		internal static SqlDouble GetSqlDouble(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlDouble result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlDouble))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlDouble.Null;
				}
				else
				{
					double double_Unchecked = ValueUtilsSmi.GetDouble_Unchecked(sink, getters, ordinal);
					result = new SqlDouble(double_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlDouble)sqlValue;
			}
			return result;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00009EDC File Offset: 0x000080DC
		internal static SqlGuid GetSqlGuid(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlGuid result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlGuid))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlGuid.Null;
				}
				else
				{
					Guid guid_Unchecked = ValueUtilsSmi.GetGuid_Unchecked(sink, getters, ordinal);
					result = new SqlGuid(guid_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlGuid)sqlValue;
			}
			return result;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00009F34 File Offset: 0x00008134
		internal static SqlInt16 GetSqlInt16(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlInt16 result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlInt16))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlInt16.Null;
				}
				else
				{
					short int16_Unchecked = ValueUtilsSmi.GetInt16_Unchecked(sink, getters, ordinal);
					result = new SqlInt16(int16_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlInt16)sqlValue;
			}
			return result;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00009F8C File Offset: 0x0000818C
		internal static SqlInt32 GetSqlInt32(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlInt32 result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlInt32))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlInt32.Null;
				}
				else
				{
					int int32_Unchecked = ValueUtilsSmi.GetInt32_Unchecked(sink, getters, ordinal);
					result = new SqlInt32(int32_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlInt32)sqlValue;
			}
			return result;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00009FE4 File Offset: 0x000081E4
		internal static SqlInt64 GetSqlInt64(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlInt64 result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlInt64))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlInt64.Null;
				}
				else
				{
					long int64_Unchecked = ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal);
					result = new SqlInt64(int64_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlInt64)sqlValue;
			}
			return result;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000A03C File Offset: 0x0000823C
		internal static SqlMoney GetSqlMoney(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlMoney result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlMoney))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlMoney.Null;
				}
				else
				{
					result = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlMoney)sqlValue;
			}
			return result;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000A08C File Offset: 0x0000828C
		internal static SqlSingle GetSqlSingle(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlSingle result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlSingle))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlSingle.Null;
				}
				else
				{
					float single_Unchecked = ValueUtilsSmi.GetSingle_Unchecked(sink, getters, ordinal);
					result = new SqlSingle(single_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlSingle)sqlValue;
			}
			return result;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000A0E4 File Offset: 0x000082E4
		internal static SqlString GetSqlString(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlString result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlString))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlString.Null;
				}
				else
				{
					string string_Unchecked = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					result = new SqlString(string_Unchecked);
				}
			}
			else if (SqlDbType.Xml == metaData.SqlDbType)
			{
				SqlXml sqlXml_Unchecked = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal);
				if (sqlXml_Unchecked.IsNull)
				{
					result = SqlString.Null;
				}
				else
				{
					result = new SqlString(sqlXml_Unchecked.Value);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlString)sqlValue;
			}
			return result;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000A170 File Offset: 0x00008370
		internal static SqlXml GetSqlXml(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlXml result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlXml))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlXml.Null;
				}
				else
				{
					result = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlXml)sqlValue;
			}
			return result;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000A1C0 File Offset: 0x000083C0
		internal static string GetString(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.String))
			{
				return ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (string)value;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000A201 File Offset: 0x00008401
		internal static TimeSpan GetTimeSpan(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.TimeSpan))
			{
				return ValueUtilsSmi.GetTimeSpan_Unchecked(sink, getters, ordinal);
			}
			return (TimeSpan)ValueUtilsSmi.GetValue200(sink, getters, ordinal, metaData);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000A22C File Offset: 0x0000842C
		internal static object GetValue200(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal, SmiMetaData metaData)
		{
			object result;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				result = DBNull.Value;
			}
			else
			{
				SqlDbType sqlDbType = metaData.SqlDbType;
				if (sqlDbType != SqlDbType.Variant)
				{
					switch (sqlDbType)
					{
					case SqlDbType.Date:
					case SqlDbType.DateTime2:
						result = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
						break;
					case SqlDbType.Time:
						result = ValueUtilsSmi.GetTimeSpan_Unchecked(sink, getters, ordinal);
						break;
					case SqlDbType.DateTimeOffset:
						result = ValueUtilsSmi.GetDateTimeOffset_Unchecked(sink, getters, ordinal);
						break;
					default:
						result = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
						break;
					}
				}
				else
				{
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					result = ValueUtilsSmi.GetValue200(sink, getters, ordinal, metaData);
				}
			}
			return result;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000A2CC File Offset: 0x000084CC
		internal static object GetValue(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			object result = null;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				result = DBNull.Value;
			}
			else
			{
				switch (metaData.SqlDbType)
				{
				case SqlDbType.BigInt:
					result = ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Binary:
					result = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Bit:
					result = ValueUtilsSmi.GetBoolean_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Char:
					result = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.DateTime:
					result = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Decimal:
					result = ValueUtilsSmi.GetSqlDecimal_Unchecked(sink, getters, ordinal).Value;
					break;
				case SqlDbType.Float:
					result = ValueUtilsSmi.GetDouble_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Image:
					result = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Int:
					result = ValueUtilsSmi.GetInt32_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Money:
					result = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal).Value;
					break;
				case SqlDbType.NChar:
					result = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.NText:
					result = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.NVarChar:
					result = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Real:
					result = ValueUtilsSmi.GetSingle_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.UniqueIdentifier:
					result = ValueUtilsSmi.GetGuid_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.SmallDateTime:
					result = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.SmallInt:
					result = ValueUtilsSmi.GetInt16_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.SmallMoney:
					result = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal).Value;
					break;
				case SqlDbType.Text:
					result = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Timestamp:
					result = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.TinyInt:
					result = ValueUtilsSmi.GetByte_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.VarBinary:
					result = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.VarChar:
					result = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Variant:
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					result = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
					break;
				case SqlDbType.Xml:
					result = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal).Value;
					break;
				case SqlDbType.Udt:
					result = ValueUtilsSmi.GetUdt_LengthChecked(sink, getters, ordinal, metaData);
					break;
				}
			}
			return result;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000A53C File Offset: 0x0000873C
		internal static object GetSqlValue200(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal, SmiMetaData metaData)
		{
			object result;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				if (SqlDbType.Udt == metaData.SqlDbType)
				{
					result = ValueUtilsSmi.NullUdtInstance(metaData);
				}
				else
				{
					result = ValueUtilsSmi.s_typeSpecificNullForSqlValue[(int)metaData.SqlDbType];
				}
			}
			else
			{
				SqlDbType sqlDbType = metaData.SqlDbType;
				if (sqlDbType != SqlDbType.Variant)
				{
					switch (sqlDbType)
					{
					case SqlDbType.Date:
					case SqlDbType.DateTime2:
						result = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
						break;
					case SqlDbType.Time:
						result = ValueUtilsSmi.GetTimeSpan_Unchecked(sink, getters, ordinal);
						break;
					case SqlDbType.DateTimeOffset:
						result = ValueUtilsSmi.GetDateTimeOffset_Unchecked(sink, getters, ordinal);
						break;
					default:
						result = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
						break;
					}
				}
				else
				{
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					result = ValueUtilsSmi.GetSqlValue200(sink, getters, ordinal, metaData);
				}
			}
			return result;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000A5F8 File Offset: 0x000087F8
		internal static object GetSqlValue(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			object result = null;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				if (SqlDbType.Udt == metaData.SqlDbType)
				{
					result = ValueUtilsSmi.NullUdtInstance(metaData);
				}
				else
				{
					result = ValueUtilsSmi.s_typeSpecificNullForSqlValue[(int)metaData.SqlDbType];
				}
			}
			else
			{
				switch (metaData.SqlDbType)
				{
				case SqlDbType.BigInt:
					result = new SqlInt64(ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Binary:
					result = ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Bit:
					result = new SqlBoolean(ValueUtilsSmi.GetBoolean_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Char:
					result = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.DateTime:
					result = new SqlDateTime(ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Decimal:
					result = ValueUtilsSmi.GetSqlDecimal_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Float:
					result = new SqlDouble(ValueUtilsSmi.GetDouble_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Image:
					result = ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Int:
					result = new SqlInt32(ValueUtilsSmi.GetInt32_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Money:
					result = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.NChar:
					result = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.NText:
					result = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.NVarChar:
					result = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Real:
					result = new SqlSingle(ValueUtilsSmi.GetSingle_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.UniqueIdentifier:
					result = new SqlGuid(ValueUtilsSmi.GetGuid_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.SmallDateTime:
					result = new SqlDateTime(ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.SmallInt:
					result = new SqlInt16(ValueUtilsSmi.GetInt16_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.SmallMoney:
					result = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Text:
					result = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Timestamp:
					result = ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.TinyInt:
					result = new SqlByte(ValueUtilsSmi.GetByte_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.VarBinary:
					result = ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.VarChar:
					result = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Variant:
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					result = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
					break;
				case SqlDbType.Xml:
					result = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Udt:
					result = ValueUtilsSmi.GetUdt_LengthChecked(sink, getters, ordinal, metaData);
					break;
				}
			}
			return result;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000A8EB File Offset: 0x00008AEB
		internal static object NullUdtInstance(SmiMetaData metaData)
		{
			return metaData.Type.InvokeMember("Null", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, new object[0], CultureInfo.InvariantCulture);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000A90F File Offset: 0x00008B0F
		internal static void SetDBNull(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, bool value)
		{
			ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, ordinal);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000A919 File Offset: 0x00008B19
		internal static void SetBoolean(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, bool value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Boolean);
			ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000A92C File Offset: 0x00008B2C
		internal static void SetByte(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, byte value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Byte);
			ValueUtilsSmi.SetByte_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000A940 File Offset: 0x00008B40
		internal static long SetBytes(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.ByteArray);
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, fieldOffset, buffer.Length, bufferOffset, length);
			if (length == 0)
			{
				fieldOffset = 0L;
				bufferOffset = 0;
			}
			return (long)ValueUtilsSmi.SetBytes_Unchecked(sink, setters, ordinal, fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000A9A0 File Offset: 0x00008BA0
		internal static long SetBytesLength(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, long length)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.ByteArray);
			if (length < 0L)
			{
				throw ADP.InvalidDataLength(length);
			}
			if (metaData.MaxLength >= 0L && length > metaData.MaxLength)
			{
				length = metaData.MaxLength;
			}
			setters.SetBytesLength(sink, ordinal, length);
			sink.ProcessMessagesAndThrow();
			return length;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000A9F4 File Offset: 0x00008BF4
		internal static long SetChars(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.CharArray);
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, fieldOffset, buffer.Length, bufferOffset, length);
			if (length == 0)
			{
				fieldOffset = 0L;
				bufferOffset = 0;
			}
			return (long)ValueUtilsSmi.SetChars_Unchecked(sink, setters, ordinal, fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000AA53 File Offset: 0x00008C53
		internal static void SetDateTime(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.DateTime);
			ValueUtilsSmi.SetDateTime_Checked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000AA67 File Offset: 0x00008C67
		internal static void SetDateTimeOffset(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTimeOffset value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.DateTimeOffset);
			ValueUtilsSmi.SetDateTimeOffset_Unchecked(sink, (SmiTypedGetterSetter)setters, ordinal, value);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000AA80 File Offset: 0x00008C80
		internal static void SetDecimal(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, decimal value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Decimal);
			ValueUtilsSmi.SetDecimal_PossiblyMoney(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000AA94 File Offset: 0x00008C94
		internal static void SetDouble(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, double value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Double);
			ValueUtilsSmi.SetDouble_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000AAA7 File Offset: 0x00008CA7
		internal static void SetGuid(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, Guid value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Guid);
			ValueUtilsSmi.SetGuid_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000AABB File Offset: 0x00008CBB
		internal static void SetInt16(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, short value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Int16);
			ValueUtilsSmi.SetInt16_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000AACE File Offset: 0x00008CCE
		internal static void SetInt32(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, int value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Int32);
			ValueUtilsSmi.SetInt32_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000AAE2 File Offset: 0x00008CE2
		internal static void SetInt64(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, long value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Int64);
			ValueUtilsSmi.SetInt64_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000AAF6 File Offset: 0x00008CF6
		internal static void SetSingle(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, float value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Single);
			ValueUtilsSmi.SetSingle_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000AB0A File Offset: 0x00008D0A
		internal static void SetSqlBinary(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBinary value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlBinary);
			ValueUtilsSmi.SetSqlBinary_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000AB20 File Offset: 0x00008D20
		internal static void SetSqlBoolean(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBoolean value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlBoolean);
			ValueUtilsSmi.SetSqlBoolean_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000AB34 File Offset: 0x00008D34
		internal static void SetSqlByte(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlByte value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlByte);
			ValueUtilsSmi.SetSqlByte_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000AB48 File Offset: 0x00008D48
		internal static void SetSqlBytes(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBytes value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlBytes);
			ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000AB5E File Offset: 0x00008D5E
		internal static void SetSqlChars(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlChars value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlChars);
			ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000AB74 File Offset: 0x00008D74
		internal static void SetSqlDateTime(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDateTime value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlDateTime);
			ValueUtilsSmi.SetSqlDateTime_Checked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000AB89 File Offset: 0x00008D89
		internal static void SetSqlDecimal(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDecimal value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlDecimal);
			ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000AB9D File Offset: 0x00008D9D
		internal static void SetSqlDouble(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDouble value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlDouble);
			ValueUtilsSmi.SetSqlDouble_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000ABB1 File Offset: 0x00008DB1
		internal static void SetSqlGuid(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlGuid value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlGuid);
			ValueUtilsSmi.SetSqlGuid_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000ABC5 File Offset: 0x00008DC5
		internal static void SetSqlInt16(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlInt16 value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlInt16);
			ValueUtilsSmi.SetSqlInt16_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000ABD9 File Offset: 0x00008DD9
		internal static void SetSqlInt32(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlInt32 value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlInt32);
			ValueUtilsSmi.SetSqlInt32_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000ABED File Offset: 0x00008DED
		internal static void SetSqlInt64(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlInt64 value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlInt64);
			ValueUtilsSmi.SetSqlInt64_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000AC01 File Offset: 0x00008E01
		internal static void SetSqlMoney(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlMoney value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlMoney);
			ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000AC16 File Offset: 0x00008E16
		internal static void SetSqlSingle(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlSingle value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlSingle);
			ValueUtilsSmi.SetSqlSingle_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000AC2A File Offset: 0x00008E2A
		internal static void SetSqlString(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlString value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlString);
			ValueUtilsSmi.SetSqlString_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000AC40 File Offset: 0x00008E40
		internal static void SetSqlXml(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlXml value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlXml);
			ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000AC54 File Offset: 0x00008E54
		internal static void SetString(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, string value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.String);
			ValueUtilsSmi.SetString_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000AC6A File Offset: 0x00008E6A
		internal static void SetTimeSpan(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, TimeSpan value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.TimeSpan);
			ValueUtilsSmi.SetTimeSpan_Checked(sink, (SmiTypedGetterSetter)setters, ordinal, metaData, value);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000AC84 File Offset: 0x00008E84
		internal static void SetCompatibleValue(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, object value, ExtendedClrTypeCode typeCode, int offset)
		{
			switch (typeCode)
			{
			case ExtendedClrTypeCode.Invalid:
				throw ADP.UnknownDataType(value.GetType());
			case ExtendedClrTypeCode.Boolean:
				ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, ordinal, (bool)value);
				return;
			case ExtendedClrTypeCode.Byte:
				ValueUtilsSmi.SetByte_Unchecked(sink, setters, ordinal, (byte)value);
				return;
			case ExtendedClrTypeCode.Char:
			{
				char[] value2 = new char[]
				{
					(char)value
				};
				ValueUtilsSmi.SetCompatibleValue(sink, setters, ordinal, metaData, value2, ExtendedClrTypeCode.CharArray, 0);
				return;
			}
			case ExtendedClrTypeCode.DateTime:
				ValueUtilsSmi.SetDateTime_Checked(sink, setters, ordinal, metaData, (DateTime)value);
				return;
			case ExtendedClrTypeCode.DBNull:
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, ordinal);
				return;
			case ExtendedClrTypeCode.Decimal:
				ValueUtilsSmi.SetDecimal_PossiblyMoney(sink, setters, ordinal, metaData, (decimal)value);
				return;
			case ExtendedClrTypeCode.Double:
				ValueUtilsSmi.SetDouble_Unchecked(sink, setters, ordinal, (double)value);
				return;
			case ExtendedClrTypeCode.Empty:
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, ordinal);
				return;
			case ExtendedClrTypeCode.Int16:
				ValueUtilsSmi.SetInt16_Unchecked(sink, setters, ordinal, (short)value);
				return;
			case ExtendedClrTypeCode.Int32:
				ValueUtilsSmi.SetInt32_Unchecked(sink, setters, ordinal, (int)value);
				return;
			case ExtendedClrTypeCode.Int64:
				ValueUtilsSmi.SetInt64_Unchecked(sink, setters, ordinal, (long)value);
				return;
			case ExtendedClrTypeCode.SByte:
				throw ADP.InvalidCast();
			case ExtendedClrTypeCode.Single:
				ValueUtilsSmi.SetSingle_Unchecked(sink, setters, ordinal, (float)value);
				return;
			case ExtendedClrTypeCode.String:
				ValueUtilsSmi.SetString_LengthChecked(sink, setters, ordinal, metaData, (string)value, offset);
				return;
			case ExtendedClrTypeCode.UInt16:
				throw ADP.InvalidCast();
			case ExtendedClrTypeCode.UInt32:
				throw ADP.InvalidCast();
			case ExtendedClrTypeCode.UInt64:
				throw ADP.InvalidCast();
			case ExtendedClrTypeCode.Object:
				ValueUtilsSmi.SetUdt_LengthChecked(sink, setters, ordinal, metaData, value);
				return;
			case ExtendedClrTypeCode.ByteArray:
				ValueUtilsSmi.SetByteArray_LengthChecked(sink, setters, ordinal, metaData, (byte[])value, offset);
				return;
			case ExtendedClrTypeCode.CharArray:
				ValueUtilsSmi.SetCharArray_LengthChecked(sink, setters, ordinal, metaData, (char[])value, offset);
				return;
			case ExtendedClrTypeCode.Guid:
				ValueUtilsSmi.SetGuid_Unchecked(sink, setters, ordinal, (Guid)value);
				return;
			case ExtendedClrTypeCode.SqlBinary:
				ValueUtilsSmi.SetSqlBinary_LengthChecked(sink, setters, ordinal, metaData, (SqlBinary)value, offset);
				return;
			case ExtendedClrTypeCode.SqlBoolean:
				ValueUtilsSmi.SetSqlBoolean_Unchecked(sink, setters, ordinal, (SqlBoolean)value);
				return;
			case ExtendedClrTypeCode.SqlByte:
				ValueUtilsSmi.SetSqlByte_Unchecked(sink, setters, ordinal, (SqlByte)value);
				return;
			case ExtendedClrTypeCode.SqlDateTime:
				ValueUtilsSmi.SetSqlDateTime_Checked(sink, setters, ordinal, metaData, (SqlDateTime)value);
				return;
			case ExtendedClrTypeCode.SqlDouble:
				ValueUtilsSmi.SetSqlDouble_Unchecked(sink, setters, ordinal, (SqlDouble)value);
				return;
			case ExtendedClrTypeCode.SqlGuid:
				ValueUtilsSmi.SetSqlGuid_Unchecked(sink, setters, ordinal, (SqlGuid)value);
				return;
			case ExtendedClrTypeCode.SqlInt16:
				ValueUtilsSmi.SetSqlInt16_Unchecked(sink, setters, ordinal, (SqlInt16)value);
				return;
			case ExtendedClrTypeCode.SqlInt32:
				ValueUtilsSmi.SetSqlInt32_Unchecked(sink, setters, ordinal, (SqlInt32)value);
				return;
			case ExtendedClrTypeCode.SqlInt64:
				ValueUtilsSmi.SetSqlInt64_Unchecked(sink, setters, ordinal, (SqlInt64)value);
				return;
			case ExtendedClrTypeCode.SqlMoney:
				ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, ordinal, metaData, (SqlMoney)value);
				return;
			case ExtendedClrTypeCode.SqlDecimal:
				ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, ordinal, (SqlDecimal)value);
				return;
			case ExtendedClrTypeCode.SqlSingle:
				ValueUtilsSmi.SetSqlSingle_Unchecked(sink, setters, ordinal, (SqlSingle)value);
				return;
			case ExtendedClrTypeCode.SqlString:
				ValueUtilsSmi.SetSqlString_LengthChecked(sink, setters, ordinal, metaData, (SqlString)value, offset);
				return;
			case ExtendedClrTypeCode.SqlChars:
				ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, ordinal, metaData, (SqlChars)value, offset);
				return;
			case ExtendedClrTypeCode.SqlBytes:
				ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, ordinal, metaData, (SqlBytes)value, offset);
				return;
			case ExtendedClrTypeCode.SqlXml:
				ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, ordinal, (SqlXml)value);
				return;
			case ExtendedClrTypeCode.DataTable:
			case ExtendedClrTypeCode.DbDataReader:
			case ExtendedClrTypeCode.IEnumerableOfSqlDataRecord:
			case ExtendedClrTypeCode.TimeSpan:
			case ExtendedClrTypeCode.DateTimeOffset:
				break;
			case ExtendedClrTypeCode.Stream:
				ValueUtilsSmi.SetStream_Unchecked(sink, setters, ordinal, metaData, (StreamDataFeed)value);
				return;
			case ExtendedClrTypeCode.TextReader:
				ValueUtilsSmi.SetTextReader_Unchecked(sink, setters, ordinal, metaData, (TextDataFeed)value);
				return;
			case ExtendedClrTypeCode.XmlReader:
				ValueUtilsSmi.SetXmlReader_Unchecked(sink, setters, ordinal, ((XmlDataFeed)value)._source);
				break;
			default:
				return;
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000AFD4 File Offset: 0x000091D4
		internal static void SetCompatibleValueV200(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, object value, ExtendedClrTypeCode typeCode, int offset, int length, ParameterPeekAheadValue peekAhead, SqlBuffer.StorageType storageType)
		{
			if (typeCode != ExtendedClrTypeCode.DateTime)
			{
				ValueUtilsSmi.SetCompatibleValueV200(sink, setters, ordinal, metaData, value, typeCode, offset, length, peekAhead);
				return;
			}
			if (storageType == SqlBuffer.StorageType.DateTime2)
			{
				ValueUtilsSmi.SetDateTime2_Checked(sink, setters, ordinal, metaData, (DateTime)value);
				return;
			}
			if (storageType == SqlBuffer.StorageType.Date)
			{
				ValueUtilsSmi.SetDate_Checked(sink, setters, ordinal, metaData, (DateTime)value);
				return;
			}
			ValueUtilsSmi.SetDateTime_Checked(sink, setters, ordinal, metaData, (DateTime)value);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000B038 File Offset: 0x00009238
		internal static void SetCompatibleValueV200(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, object value, ExtendedClrTypeCode typeCode, int offset, int length, ParameterPeekAheadValue peekAhead)
		{
			switch (typeCode)
			{
			case ExtendedClrTypeCode.DataTable:
				ValueUtilsSmi.SetDataTable_Unchecked(sink, setters, ordinal, metaData, (DataTable)value);
				return;
			case ExtendedClrTypeCode.DbDataReader:
				ValueUtilsSmi.SetDbDataReader_Unchecked(sink, setters, ordinal, metaData, (DbDataReader)value);
				return;
			case ExtendedClrTypeCode.IEnumerableOfSqlDataRecord:
				ValueUtilsSmi.SetIEnumerableOfSqlDataRecord_Unchecked(sink, setters, ordinal, metaData, (IEnumerable<SqlDataRecord>)value, peekAhead);
				return;
			case ExtendedClrTypeCode.TimeSpan:
				ValueUtilsSmi.SetTimeSpan_Checked(sink, setters, ordinal, metaData, (TimeSpan)value);
				return;
			case ExtendedClrTypeCode.DateTimeOffset:
				ValueUtilsSmi.SetDateTimeOffset_Unchecked(sink, setters, ordinal, (DateTimeOffset)value);
				return;
			default:
				ValueUtilsSmi.SetCompatibleValue(sink, setters, ordinal, metaData, value, typeCode, offset);
				return;
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000B0CC File Offset: 0x000092CC
		private static void SetDataTable_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DataTable value)
		{
			setters = setters.GetTypedGetterSetter(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			ExtendedClrTypeCode[] array = new ExtendedClrTypeCode[metaData.FieldMetaData.Count];
			for (int i = 0; i < metaData.FieldMetaData.Count; i++)
			{
				array[i] = ExtendedClrTypeCode.Invalid;
			}
			foreach (object obj in value.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				setters.NewElement(sink);
				sink.ProcessMessagesAndThrow();
				for (int j = 0; j < metaData.FieldMetaData.Count; j++)
				{
					SmiMetaData smiMetaData = metaData.FieldMetaData[j];
					if (dataRow.IsNull(j))
					{
						ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, j);
					}
					else
					{
						object value2 = dataRow[j];
						if (ExtendedClrTypeCode.Invalid == array[j])
						{
							array[j] = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(smiMetaData.SqlDbType, smiMetaData.IsMultiValued, value2, smiMetaData.Type);
						}
						ValueUtilsSmi.SetCompatibleValueV200(sink, setters, j, smiMetaData, value2, array[j], 0, -1, null);
					}
				}
			}
			setters.EndElements(sink);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000B200 File Offset: 0x00009400
		internal static void FillCompatibleITypedSettersFromReader(SmiEventSink_Default sink, ITypedSettersV3 setters, SmiMetaData[] metaData, SqlDataReader reader)
		{
			for (int i = 0; i < metaData.Length; i++)
			{
				if (!reader.IsDBNull(i))
				{
					switch (metaData[i].SqlDbType)
					{
					case SqlDbType.BigInt:
						ValueUtilsSmi.SetInt64_Unchecked(sink, setters, i, reader.GetInt64(i));
						goto IL_2BB;
					case SqlDbType.Binary:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_2BB;
					case SqlDbType.Bit:
						ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, i, reader.GetBoolean(i));
						goto IL_2BB;
					case SqlDbType.Char:
						ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlChars(i), 0);
						goto IL_2BB;
					case SqlDbType.DateTime:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_2BB;
					case SqlDbType.Decimal:
						ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, i, reader.GetSqlDecimal(i));
						goto IL_2BB;
					case SqlDbType.Float:
						ValueUtilsSmi.SetDouble_Unchecked(sink, setters, i, reader.GetDouble(i));
						goto IL_2BB;
					case SqlDbType.Image:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_2BB;
					case SqlDbType.Int:
						ValueUtilsSmi.SetInt32_Unchecked(sink, setters, i, reader.GetInt32(i));
						goto IL_2BB;
					case SqlDbType.Money:
						ValueUtilsSmi.SetSqlMoney_Unchecked(sink, setters, i, metaData[i], reader.GetSqlMoney(i));
						goto IL_2BB;
					case SqlDbType.NChar:
					case SqlDbType.NText:
					case SqlDbType.NVarChar:
						ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlChars(i), 0);
						goto IL_2BB;
					case SqlDbType.Real:
						ValueUtilsSmi.SetSingle_Unchecked(sink, setters, i, reader.GetFloat(i));
						goto IL_2BB;
					case SqlDbType.UniqueIdentifier:
						ValueUtilsSmi.SetGuid_Unchecked(sink, setters, i, reader.GetGuid(i));
						goto IL_2BB;
					case SqlDbType.SmallDateTime:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_2BB;
					case SqlDbType.SmallInt:
						ValueUtilsSmi.SetInt16_Unchecked(sink, setters, i, reader.GetInt16(i));
						goto IL_2BB;
					case SqlDbType.SmallMoney:
						ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, i, metaData[i], reader.GetSqlMoney(i));
						goto IL_2BB;
					case SqlDbType.Text:
						ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlChars(i), 0);
						goto IL_2BB;
					case SqlDbType.Timestamp:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_2BB;
					case SqlDbType.TinyInt:
						ValueUtilsSmi.SetByte_Unchecked(sink, setters, i, reader.GetByte(i));
						goto IL_2BB;
					case SqlDbType.VarBinary:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_2BB;
					case SqlDbType.VarChar:
						ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlChars(i), 0);
						goto IL_2BB;
					case SqlDbType.Variant:
					{
						object sqlValue = reader.GetSqlValue(i);
						ExtendedClrTypeCode typeCode = MetaDataUtilsSmi.DetermineExtendedTypeCode(sqlValue);
						ValueUtilsSmi.SetCompatibleValue(sink, setters, i, metaData[i], sqlValue, typeCode, 0);
						goto IL_2BB;
					}
					case SqlDbType.Xml:
						ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, i, reader.GetSqlXml(i));
						goto IL_2BB;
					case SqlDbType.Udt:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_2BB;
					}
					throw ADP.NotSupported();
				}
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, i);
				IL_2BB:;
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000B4D8 File Offset: 0x000096D8
		internal static void FillCompatibleSettersFromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, IList<SmiExtendedMetaData> metaData, DbDataReader reader)
		{
			for (int i = 0; i < metaData.Count; i++)
			{
				if (!reader.IsDBNull(i))
				{
					switch (metaData[i].SqlDbType)
					{
					case SqlDbType.BigInt:
						ValueUtilsSmi.SetInt64_Unchecked(sink, setters, i, reader.GetInt64(i));
						goto IL_425;
					case SqlDbType.Binary:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					case SqlDbType.Bit:
						ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, i, reader.GetBoolean(i));
						goto IL_425;
					case SqlDbType.Char:
						ValueUtilsSmi.SetCharsOrString_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					case SqlDbType.DateTime:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_425;
					case SqlDbType.Decimal:
					{
						SqlDataReader sqlDataReader = reader as SqlDataReader;
						if (sqlDataReader != null)
						{
							ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, i, sqlDataReader.GetSqlDecimal(i));
							goto IL_425;
						}
						ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, i, new SqlDecimal(reader.GetDecimal(i)));
						goto IL_425;
					}
					case SqlDbType.Float:
						ValueUtilsSmi.SetDouble_Unchecked(sink, setters, i, reader.GetDouble(i));
						goto IL_425;
					case SqlDbType.Image:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					case SqlDbType.Int:
						ValueUtilsSmi.SetInt32_Unchecked(sink, setters, i, reader.GetInt32(i));
						goto IL_425;
					case SqlDbType.Money:
						ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, i, metaData[i], new SqlMoney(reader.GetDecimal(i)));
						goto IL_425;
					case SqlDbType.NChar:
					case SqlDbType.NText:
					case SqlDbType.NVarChar:
						ValueUtilsSmi.SetCharsOrString_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					case SqlDbType.Real:
						ValueUtilsSmi.SetSingle_Unchecked(sink, setters, i, reader.GetFloat(i));
						goto IL_425;
					case SqlDbType.UniqueIdentifier:
						ValueUtilsSmi.SetGuid_Unchecked(sink, setters, i, reader.GetGuid(i));
						goto IL_425;
					case SqlDbType.SmallDateTime:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_425;
					case SqlDbType.SmallInt:
						ValueUtilsSmi.SetInt16_Unchecked(sink, setters, i, reader.GetInt16(i));
						goto IL_425;
					case SqlDbType.SmallMoney:
						ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, i, metaData[i], new SqlMoney(reader.GetDecimal(i)));
						goto IL_425;
					case SqlDbType.Text:
						ValueUtilsSmi.SetCharsOrString_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					case SqlDbType.Timestamp:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					case SqlDbType.TinyInt:
						ValueUtilsSmi.SetByte_Unchecked(sink, setters, i, reader.GetByte(i));
						goto IL_425;
					case SqlDbType.VarBinary:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					case SqlDbType.VarChar:
						ValueUtilsSmi.SetCharsOrString_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					case SqlDbType.Variant:
					{
						SqlDataReader sqlDataReader2 = reader as SqlDataReader;
						SqlBuffer.StorageType storageType = SqlBuffer.StorageType.Empty;
						object value;
						if (sqlDataReader2 != null)
						{
							value = sqlDataReader2.GetSqlValue(i);
							storageType = sqlDataReader2.GetVariantInternalStorageType(i);
						}
						else
						{
							value = reader.GetValue(i);
						}
						ExtendedClrTypeCode typeCode = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(metaData[i].SqlDbType, metaData[i].IsMultiValued, value, null);
						if (storageType == SqlBuffer.StorageType.DateTime2 || storageType == SqlBuffer.StorageType.Date)
						{
							ValueUtilsSmi.SetCompatibleValueV200(sink, setters, i, metaData[i], value, typeCode, 0, 0, null, storageType);
							goto IL_425;
						}
						ValueUtilsSmi.SetCompatibleValueV200(sink, setters, i, metaData[i], value, typeCode, 0, 0, null);
						goto IL_425;
					}
					case SqlDbType.Xml:
					{
						SqlDataReader sqlDataReader3 = reader as SqlDataReader;
						if (sqlDataReader3 != null)
						{
							ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, i, sqlDataReader3.GetSqlXml(i));
							goto IL_425;
						}
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					}
					case SqlDbType.Udt:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					case SqlDbType.Date:
					case SqlDbType.DateTime2:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_425;
					case SqlDbType.Time:
					{
						SqlDataReader sqlDataReader4 = reader as SqlDataReader;
						TimeSpan value2;
						if (sqlDataReader4 != null)
						{
							value2 = sqlDataReader4.GetTimeSpan(i);
						}
						else
						{
							value2 = (TimeSpan)reader.GetValue(i);
						}
						ValueUtilsSmi.SetTimeSpan_Checked(sink, setters, i, metaData[i], value2);
						goto IL_425;
					}
					case SqlDbType.DateTimeOffset:
					{
						SqlDataReader sqlDataReader5 = reader as SqlDataReader;
						DateTimeOffset value3;
						if (sqlDataReader5 != null)
						{
							value3 = sqlDataReader5.GetDateTimeOffset(i);
						}
						else
						{
							value3 = (DateTimeOffset)reader.GetValue(i);
						}
						ValueUtilsSmi.SetDateTimeOffset_Unchecked(sink, setters, i, value3);
						goto IL_425;
					}
					}
					throw ADP.NotSupported();
				}
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, i);
				IL_425:;
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000B91C File Offset: 0x00009B1C
		internal static void FillCompatibleSettersFromRecord(SmiEventSink_Default sink, SmiTypedGetterSetter setters, SmiMetaData[] metaData, SqlDataRecord record, SmiDefaultFieldsProperty useDefaultValues)
		{
			for (int i = 0; i < metaData.Length; i++)
			{
				if (useDefaultValues == null || !useDefaultValues[i])
				{
					if (!record.IsDBNull(i))
					{
						switch (metaData[i].SqlDbType)
						{
						case SqlDbType.BigInt:
							ValueUtilsSmi.SetInt64_Unchecked(sink, setters, i, record.GetInt64(i));
							goto IL_32C;
						case SqlDbType.Binary:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_32C;
						case SqlDbType.Bit:
							ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, i, record.GetBoolean(i));
							goto IL_32C;
						case SqlDbType.Char:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_32C;
						case SqlDbType.DateTime:
							ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], record.GetDateTime(i));
							goto IL_32C;
						case SqlDbType.Decimal:
							ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, i, record.GetSqlDecimal(i));
							goto IL_32C;
						case SqlDbType.Float:
							ValueUtilsSmi.SetDouble_Unchecked(sink, setters, i, record.GetDouble(i));
							goto IL_32C;
						case SqlDbType.Image:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_32C;
						case SqlDbType.Int:
							ValueUtilsSmi.SetInt32_Unchecked(sink, setters, i, record.GetInt32(i));
							goto IL_32C;
						case SqlDbType.Money:
							ValueUtilsSmi.SetSqlMoney_Unchecked(sink, setters, i, metaData[i], record.GetSqlMoney(i));
							goto IL_32C;
						case SqlDbType.NChar:
						case SqlDbType.NText:
						case SqlDbType.NVarChar:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_32C;
						case SqlDbType.Real:
							ValueUtilsSmi.SetSingle_Unchecked(sink, setters, i, record.GetFloat(i));
							goto IL_32C;
						case SqlDbType.UniqueIdentifier:
							ValueUtilsSmi.SetGuid_Unchecked(sink, setters, i, record.GetGuid(i));
							goto IL_32C;
						case SqlDbType.SmallDateTime:
							ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], record.GetDateTime(i));
							goto IL_32C;
						case SqlDbType.SmallInt:
							ValueUtilsSmi.SetInt16_Unchecked(sink, setters, i, record.GetInt16(i));
							goto IL_32C;
						case SqlDbType.SmallMoney:
							ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, i, metaData[i], record.GetSqlMoney(i));
							goto IL_32C;
						case SqlDbType.Text:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_32C;
						case SqlDbType.Timestamp:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_32C;
						case SqlDbType.TinyInt:
							ValueUtilsSmi.SetByte_Unchecked(sink, setters, i, record.GetByte(i));
							goto IL_32C;
						case SqlDbType.VarBinary:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_32C;
						case SqlDbType.VarChar:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_32C;
						case SqlDbType.Variant:
						{
							object sqlValue = record.GetSqlValue(i);
							ExtendedClrTypeCode typeCode = MetaDataUtilsSmi.DetermineExtendedTypeCode(sqlValue);
							ValueUtilsSmi.SetCompatibleValueV200(sink, setters, i, metaData[i], sqlValue, typeCode, 0, -1, null);
							goto IL_32C;
						}
						case SqlDbType.Xml:
							ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, i, record.GetSqlXml(i));
							goto IL_32C;
						case SqlDbType.Udt:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_32C;
						case SqlDbType.Date:
						case SqlDbType.DateTime2:
							ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], record.GetDateTime(i));
							goto IL_32C;
						case SqlDbType.Time:
						{
							TimeSpan value;
							if (record != null)
							{
								value = record.GetTimeSpan(i);
							}
							else
							{
								value = (TimeSpan)record.GetValue(i);
							}
							ValueUtilsSmi.SetTimeSpan_Checked(sink, setters, i, metaData[i], value);
							goto IL_32C;
						}
						case SqlDbType.DateTimeOffset:
						{
							DateTimeOffset value2;
							if (record != null)
							{
								value2 = record.GetDateTimeOffset(i);
							}
							else
							{
								value2 = (DateTimeOffset)record.GetValue(i);
							}
							ValueUtilsSmi.SetDateTimeOffset_Unchecked(sink, setters, i, value2);
							goto IL_32C;
						}
						}
						throw ADP.NotSupported();
					}
					ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, i);
				}
				IL_32C:;
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000BC64 File Offset: 0x00009E64
		internal static Stream CopyIntoNewSmiScratchStream(Stream source, SmiEventSink_Default sink)
		{
			Stream stream = new MemoryStream();
			int num;
			if (source.CanSeek && 8000L > source.Length)
			{
				num = (int)source.Length;
			}
			else
			{
				num = 8000;
			}
			byte[] buffer = new byte[num];
			int count;
			while ((count = source.Read(buffer, 0, num)) != 0)
			{
				stream.Write(buffer, 0, count);
			}
			stream.Flush();
			stream.Seek(0L, SeekOrigin.Begin);
			return stream;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		private static object GetUdt_LengthChecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			object result;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				result = metaData.Type.InvokeMember("Null", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, new object[0], CultureInfo.InvariantCulture);
			}
			else
			{
				result = SerializationHelperSql9.Deserialize(new SmiGettersStream(sink, getters, ordinal, metaData), metaData.Type);
			}
			return result;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000BD24 File Offset: 0x00009F24
		private static decimal GetDecimal_PossiblyMoney(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			if (SqlDbType.Decimal == metaData.SqlDbType)
			{
				return ValueUtilsSmi.GetSqlDecimal_Unchecked(sink, getters, ordinal).Value;
			}
			return ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal).Value;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000BD5B File Offset: 0x00009F5B
		private static void SetDecimal_PossiblyMoney(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, decimal value)
		{
			if (SqlDbType.Decimal == metaData.SqlDbType || SqlDbType.Variant == metaData.SqlDbType)
			{
				ValueUtilsSmi.SetDecimal_Unchecked(sink, setters, ordinal, value);
				return;
			}
			ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, ordinal, metaData, new SqlMoney(value));
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000BD8B File Offset: 0x00009F8B
		private static void VerifyDateTimeRange(SqlDbType dbType, DateTime value)
		{
			if (SqlDbType.SmallDateTime == dbType && (ValueUtilsSmi.s_dtSmallMax < value || ValueUtilsSmi.s_dtSmallMin > value))
			{
				throw ADP.InvalidMetaDataValue();
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000BDB2 File Offset: 0x00009FB2
		private static void VerifyTimeRange(SqlDbType dbType, TimeSpan value)
		{
			if (SqlDbType.Time == dbType && (ValueUtilsSmi.s_timeMin > value || value > ValueUtilsSmi.s_timeMax))
			{
				throw ADP.InvalidMetaDataValue();
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000BDD9 File Offset: 0x00009FD9
		private static void SetDateTime_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			ValueUtilsSmi.VerifyDateTimeRange(metaData.SqlDbType, value);
			ValueUtilsSmi.SetDateTime_Unchecked(sink, setters, ordinal, (SqlDbType.Date == metaData.SqlDbType) ? value.Date : value);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000BE05 File Offset: 0x0000A005
		private static void SetTimeSpan_Checked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, TimeSpan value)
		{
			ValueUtilsSmi.VerifyTimeRange(metaData.SqlDbType, value);
			ValueUtilsSmi.SetTimeSpan_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000BE1E File Offset: 0x0000A01E
		private static void SetSqlDateTime_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDateTime value)
		{
			if (!value.IsNull)
			{
				ValueUtilsSmi.VerifyDateTimeRange(metaData.SqlDbType, value.Value);
			}
			ValueUtilsSmi.SetSqlDateTime_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000BE45 File Offset: 0x0000A045
		private static void SetDateTime2_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			ValueUtilsSmi.VerifyDateTimeRange(metaData.SqlDbType, value);
			ValueUtilsSmi.SetDateTime2_Unchecked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000BE5F File Offset: 0x0000A05F
		private static void SetDate_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			ValueUtilsSmi.VerifyDateTimeRange(metaData.SqlDbType, value);
			ValueUtilsSmi.SetDate_Unchecked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000BE7C File Offset: 0x0000A07C
		private static void SetSqlMoney_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlMoney value)
		{
			if (!value.IsNull && SqlDbType.SmallMoney == metaData.SqlDbType)
			{
				decimal value2 = value.Value;
				if (TdsEnums.SQL_SMALL_MONEY_MIN > value2 || TdsEnums.SQL_SMALL_MONEY_MAX < value2)
				{
					throw SQL.MoneyOverflow(value2.ToString(CultureInfo.InvariantCulture));
				}
			}
			ValueUtilsSmi.SetSqlMoney_Unchecked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		private static void SetByteArray_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, byte[] buffer, int offset)
		{
			int length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, buffer.Length, offset, buffer.Length - offset);
			ValueUtilsSmi.SetByteArray_Unchecked(sink, setters, ordinal, buffer, offset, length);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000BF1C File Offset: 0x0000A11C
		private static void SetCharArray_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, char[] buffer, int offset)
		{
			int length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, buffer.Length, offset, buffer.Length - offset);
			ValueUtilsSmi.SetCharArray_Unchecked(sink, setters, ordinal, buffer, offset, length);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000BF5C File Offset: 0x0000A15C
		private static void SetSqlBinary_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBinary value, int offset)
		{
			int length = 0;
			if (!value.IsNull)
			{
				length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, value.Length, offset, value.Length - offset);
			}
			ValueUtilsSmi.SetSqlBinary_Unchecked(sink, setters, ordinal, value, offset, length);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000BFAC File Offset: 0x0000A1AC
		private static void SetBytes_FromRecord(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDataRecord record, int offset)
		{
			long num = record.GetBytes(ordinal, 0L, null, 0, 0);
			if (num > 2147483647L)
			{
				num = -1L;
			}
			int num2 = checked(ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, (int)num, offset, (int)num));
			int num3;
			if (num2 > 8000 || num2 < 0)
			{
				num3 = 8000;
			}
			else
			{
				num3 = num2;
			}
			byte[] buffer = new byte[num3];
			long num4 = 1L;
			long num5 = (long)offset;
			long num6 = 0L;
			long bytes;
			while ((num2 < 0 || num6 < (long)num2) && (bytes = record.GetBytes(ordinal, num5, buffer, 0, num3)) != 0L && num4 != 0L)
			{
				num4 = (long)setters.SetBytes(sink, ordinal, num5, buffer, 0, checked((int)bytes));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num5 += num4;
					num6 += num4;
				}
			}
			setters.SetBytesLength(sink, ordinal, num5);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000C074 File Offset: 0x0000A274
		private static void SetBytes_FromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader reader, int offset)
		{
			int num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, -1, offset, -1);
			int num2 = 8000;
			byte[] buffer = new byte[num2];
			long num3 = 1L;
			long num4 = (long)offset;
			long num5 = 0L;
			long bytes;
			while ((num < 0 || num5 < (long)num) && (bytes = reader.GetBytes(ordinal, num4, buffer, 0, num2)) != 0L && num3 != 0L)
			{
				num3 = (long)setters.SetBytes(sink, ordinal, num4, buffer, 0, checked((int)bytes));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num4 += num3;
					num5 += num3;
				}
			}
			setters.SetBytesLength(sink, ordinal, num4);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000C110 File Offset: 0x0000A310
		private static void SetSqlBytes_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBytes value, int offset)
		{
			int num = 0;
			if (!value.IsNull)
			{
				long num2 = value.Length;
				if (num2 > 2147483647L)
				{
					num2 = -1L;
				}
				num = checked(ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, (int)num2, offset, (int)num2));
			}
			ValueUtilsSmi.SetSqlBytes_Unchecked(sink, setters, ordinal, value, 0, (long)num);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000C168 File Offset: 0x0000A368
		private static void SetChars_FromRecord(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDataRecord record, int offset)
		{
			long num = record.GetChars(ordinal, 0L, null, 0, 0);
			if (num > 2147483647L)
			{
				num = -1L;
			}
			int num2 = checked(ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, (int)num, offset, (int)num - offset));
			int num3;
			if (num2 > 4000 || num2 < 0)
			{
				if (MetaDataUtilsSmi.IsAnsiType(metaData.SqlDbType))
				{
					num3 = 8000;
				}
				else
				{
					num3 = 4000;
				}
			}
			else
			{
				num3 = num2;
			}
			char[] buffer = new char[num3];
			long num4 = 1L;
			long num5 = (long)offset;
			long num6 = 0L;
			long chars;
			while ((num2 < 0 || num6 < (long)num2) && (chars = record.GetChars(ordinal, num5, buffer, 0, num3)) != 0L && num4 != 0L)
			{
				num4 = (long)setters.SetChars(sink, ordinal, num5, buffer, 0, checked((int)chars));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num5 += num4;
					num6 += num4;
				}
			}
			setters.SetCharsLength(sink, ordinal, num5);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000C248 File Offset: 0x0000A448
		private static void SetCharsOrString_FromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader reader, int offset)
		{
			bool flag = false;
			try
			{
				ValueUtilsSmi.SetChars_FromReader(sink, setters, ordinal, metaData, reader, offset);
				flag = true;
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
			}
			if (!flag)
			{
				ValueUtilsSmi.SetString_FromReader(sink, setters, ordinal, metaData, reader, offset);
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000C294 File Offset: 0x0000A494
		private static void SetChars_FromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader reader, int offset)
		{
			int num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, -1, offset, -1);
			int num2;
			if (MetaDataUtilsSmi.IsAnsiType(metaData.SqlDbType))
			{
				num2 = 8000;
			}
			else
			{
				num2 = 4000;
			}
			char[] buffer = new char[num2];
			long num3 = 1L;
			long num4 = (long)offset;
			long num5 = 0L;
			long chars;
			while ((num < 0 || num5 < (long)num) && (chars = reader.GetChars(ordinal, num4, buffer, 0, num2)) != 0L && num3 != 0L)
			{
				num3 = (long)setters.SetChars(sink, ordinal, num4, buffer, 0, checked((int)chars));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num4 += num3;
					num5 += num3;
				}
			}
			setters.SetCharsLength(sink, ordinal, num4);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000C344 File Offset: 0x0000A544
		private static void SetString_FromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader reader, int offset)
		{
			string @string = reader.GetString(ordinal);
			int length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, (long)@string.Length, 0L, -1, offset, -1);
			setters.SetString(sink, ordinal, @string, offset, length);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000C38C File Offset: 0x0000A58C
		private static void SetSqlChars_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlChars value, int offset)
		{
			int length = 0;
			if (!value.IsNull)
			{
				long num = value.Length;
				if (num > 2147483647L)
				{
					num = -1L;
				}
				length = checked(ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, (int)num, offset, (int)num - offset));
			}
			ValueUtilsSmi.SetSqlChars_Unchecked(sink, setters, ordinal, value, 0, length);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000C3E4 File Offset: 0x0000A5E4
		private static void SetSqlString_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlString value, int offset)
		{
			if (value.IsNull)
			{
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, ordinal);
				return;
			}
			string value2 = value.Value;
			int length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, value2.Length, offset, value2.Length - offset);
			ValueUtilsSmi.SetSqlString_Unchecked(sink, setters, ordinal, metaData, value, offset, length);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000C440 File Offset: 0x0000A640
		private static void SetString_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, string value, int offset)
		{
			int length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, value.Length, offset, checked(value.Length - offset));
			ValueUtilsSmi.SetString_Unchecked(sink, setters, ordinal, value, offset, length);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000C483 File Offset: 0x0000A683
		private static void SetUdt_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, object value)
		{
			if (ADP.IsNull(value))
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			SerializationHelperSql9.Serialize(new SmiSettersStream(sink, setters, ordinal, metaData), value);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000C4AD File Offset: 0x0000A6AD
		private static void ThrowIfInvalidSetterAccess(SmiMetaData metaData, ExtendedClrTypeCode setterTypeCode)
		{
			if (!ValueUtilsSmi.CanAccessSetterDirectly(metaData, setterTypeCode))
			{
				throw ADP.InvalidCast();
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000C4BE File Offset: 0x0000A6BE
		private static void ThrowIfITypedGettersIsNull(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				throw SQL.SqlNullValue();
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
		private static bool CanAccessGetterDirectly(SmiMetaData metaData, ExtendedClrTypeCode setterTypeCode)
		{
			bool flag = ValueUtilsSmi.s_canAccessGetterDirectly[(int)setterTypeCode, (int)metaData.SqlDbType];
			if (flag && (ExtendedClrTypeCode.DataTable == setterTypeCode || ExtendedClrTypeCode.DbDataReader == setterTypeCode || ExtendedClrTypeCode.IEnumerableOfSqlDataRecord == setterTypeCode))
			{
				flag = metaData.IsMultiValued;
			}
			return flag;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000C50C File Offset: 0x0000A70C
		private static bool CanAccessSetterDirectly(SmiMetaData metaData, ExtendedClrTypeCode setterTypeCode)
		{
			bool flag = ValueUtilsSmi.s_canAccessSetterDirectly[(int)setterTypeCode, (int)metaData.SqlDbType];
			if (flag && (ExtendedClrTypeCode.DataTable == setterTypeCode || ExtendedClrTypeCode.DbDataReader == setterTypeCode || ExtendedClrTypeCode.IEnumerableOfSqlDataRecord == setterTypeCode))
			{
				flag = metaData.IsMultiValued;
			}
			return flag;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000C545 File Offset: 0x0000A745
		private static long PositiveMin(long first, long second)
		{
			if (first < 0L)
			{
				return second;
			}
			if (second < 0L)
			{
				return first;
			}
			return Math.Min(first, second);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000C55C File Offset: 0x0000A75C
		private static int CheckXetParameters(SqlDbType dbType, long maxLength, long actualLength, long fieldOffset, int bufferLength, int bufferOffset, int length)
		{
			if (0L > fieldOffset)
			{
				throw ADP.NegativeParameter("fieldOffset");
			}
			if (bufferOffset < 0)
			{
				throw ADP.InvalidDestinationBufferIndex(bufferLength, bufferOffset, "bufferOffset");
			}
			checked
			{
				if (bufferLength < 0)
				{
					length = (int)ValueUtilsSmi.PositiveMin(unchecked((long)length), ValueUtilsSmi.PositiveMin(maxLength, actualLength));
					if (length < -1)
					{
						length = -1;
					}
					return length;
				}
				if (bufferOffset > bufferLength)
				{
					throw ADP.InvalidDestinationBufferIndex(bufferLength, bufferOffset, "bufferOffset");
				}
				if (length + bufferOffset > bufferLength)
				{
					throw ADP.InvalidBufferSizeOrIndex(length, bufferOffset);
				}
			}
			if (length < 0)
			{
				throw ADP.InvalidDataLength((long)length);
			}
			if (0L <= actualLength && actualLength <= fieldOffset)
			{
				return 0;
			}
			length = Math.Min(length, bufferLength - bufferOffset);
			if (SqlDbType.Variant == dbType)
			{
				length = Math.Min(length, 8000);
			}
			if (0L <= actualLength)
			{
				length = (int)Math.Min((long)length, actualLength - fieldOffset);
			}
			else if (SqlDbType.Udt != dbType && 0L <= maxLength)
			{
				length = (int)Math.Min((long)length, maxLength - fieldOffset);
			}
			if (length < 0)
			{
				return 0;
			}
			return length;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000C647 File Offset: 0x0000A847
		private static bool IsDBNull_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			bool result = getters.IsDBNull(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return result;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000C657 File Offset: 0x0000A857
		private static bool GetBoolean_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			bool boolean = getters.GetBoolean(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return boolean;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000C667 File Offset: 0x0000A867
		private static byte GetByte_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			byte @byte = getters.GetByte(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @byte;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000C678 File Offset: 0x0000A878
		private static byte[] GetByteArray_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			int bytesLength = (int)getters.GetBytesLength(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			int num = bytesLength;
			byte[] array = new byte[num];
			getters.GetBytes(sink, ordinal, 0L, array, 0, num);
			sink.ProcessMessagesAndThrow();
			return array;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000C6B1 File Offset: 0x0000A8B1
		internal static int GetBytes_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int bytes = getters.GetBytes(sink, ordinal, fieldOffset, buffer, bufferOffset, length);
			sink.ProcessMessagesAndThrow();
			return bytes;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
		private static long GetBytesLength_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long bytesLength = getters.GetBytesLength(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return bytesLength;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
		private static char[] GetCharArray_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			int charsLength = (int)getters.GetCharsLength(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			int num = charsLength;
			char[] array = new char[num];
			getters.GetChars(sink, ordinal, 0L, array, 0, num);
			sink.ProcessMessagesAndThrow();
			return array;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000C711 File Offset: 0x0000A911
		internal static int GetChars_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int chars = getters.GetChars(sink, ordinal, fieldOffset, buffer, bufferOffset, length);
			sink.ProcessMessagesAndThrow();
			return chars;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000C728 File Offset: 0x0000A928
		private static long GetCharsLength_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long charsLength = getters.GetCharsLength(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return charsLength;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000C738 File Offset: 0x0000A938
		private static DateTime GetDateTime_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			DateTime dateTime = getters.GetDateTime(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return dateTime;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000C748 File Offset: 0x0000A948
		private static DateTimeOffset GetDateTimeOffset_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal)
		{
			DateTimeOffset dateTimeOffset = getters.GetDateTimeOffset(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return dateTimeOffset;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000C758 File Offset: 0x0000A958
		private static double GetDouble_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			double @double = getters.GetDouble(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @double;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000C768 File Offset: 0x0000A968
		private static Guid GetGuid_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			Guid guid = getters.GetGuid(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return guid;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000C778 File Offset: 0x0000A978
		private static short GetInt16_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			short @int = getters.GetInt16(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @int;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000C788 File Offset: 0x0000A988
		private static int GetInt32_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			int @int = getters.GetInt32(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @int;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000C798 File Offset: 0x0000A998
		private static long GetInt64_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long @int = getters.GetInt64(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @int;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		private static float GetSingle_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			float single = getters.GetSingle(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return single;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000C7B8 File Offset: 0x0000A9B8
		private static SqlBinary GetSqlBinary_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			return new SqlBinary(ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal));
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000C7C7 File Offset: 0x0000A9C7
		private static SqlDecimal GetSqlDecimal_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			SqlDecimal sqlDecimal = getters.GetSqlDecimal(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return sqlDecimal;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000C7D7 File Offset: 0x0000A9D7
		private static SqlMoney GetSqlMoney_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long @int = getters.GetInt64(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return SqlTypeWorkarounds.SqlMoneyCtor(@int, 1);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000C7ED File Offset: 0x0000A9ED
		private static SqlXml GetSqlXml_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			return new SqlXml(ValueUtilsSmi.CopyIntoNewSmiScratchStream(new SmiGettersStream(sink, getters, ordinal, SmiMetaData.DefaultXml), sink));
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000C807 File Offset: 0x0000AA07
		private static string GetString_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			string @string = getters.GetString(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @string;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000C817 File Offset: 0x0000AA17
		private static TimeSpan GetTimeSpan_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal)
		{
			TimeSpan timeSpan = getters.GetTimeSpan(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return timeSpan;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000C827 File Offset: 0x0000AA27
		private static void SetBoolean_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, bool value)
		{
			setters.SetBoolean(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000C838 File Offset: 0x0000AA38
		private static void SetByteArray_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, byte[] buffer, int bufferOffset, int length)
		{
			if (length > 0)
			{
				setters.SetBytes(sink, ordinal, 0L, buffer, bufferOffset, length);
				sink.ProcessMessagesAndThrow();
			}
			setters.SetBytesLength(sink, ordinal, (long)length);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000C868 File Offset: 0x0000AA68
		private static void SetStream_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metadata, StreamDataFeed feed)
		{
			long maxLength = metadata.MaxLength;
			byte[] buffer = new byte[4096];
			int num = 0;
			do
			{
				int num2 = 4096;
				if (maxLength > 0L && (long)(num + num2) > maxLength)
				{
					num2 = (int)(maxLength - (long)num);
				}
				int num3 = feed._source.Read(buffer, 0, num2);
				if (num3 == 0)
				{
					break;
				}
				setters.SetBytes(sink, ordinal, (long)num, buffer, 0, num3);
				sink.ProcessMessagesAndThrow();
				num += num3;
			}
			while (maxLength <= 0L || (long)num < maxLength);
			setters.SetBytesLength(sink, ordinal, (long)num);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		private static void SetTextReader_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metadata, TextDataFeed feed)
		{
			long maxLength = metadata.MaxLength;
			char[] buffer = new char[4096];
			int num = 0;
			do
			{
				int num2 = 4096;
				if (maxLength > 0L && (long)(num + num2) > maxLength)
				{
					num2 = (int)(maxLength - (long)num);
				}
				int num3 = feed._source.Read(buffer, 0, num2);
				if (num3 == 0)
				{
					break;
				}
				setters.SetChars(sink, ordinal, (long)num, buffer, 0, num3);
				sink.ProcessMessagesAndThrow();
				num += num3;
			}
			while (maxLength <= 0L || (long)num < maxLength);
			setters.SetCharsLength(sink, ordinal, (long)num);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000C970 File Offset: 0x0000AB70
		private static void SetByte_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, byte value)
		{
			setters.SetByte(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000C981 File Offset: 0x0000AB81
		private static int SetBytes_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int result = setters.SetBytes(sink, ordinal, fieldOffset, buffer, bufferOffset, length);
			sink.ProcessMessagesAndThrow();
			return result;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000C998 File Offset: 0x0000AB98
		private static void SetCharArray_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, char[] buffer, int bufferOffset, int length)
		{
			if (length > 0)
			{
				setters.SetChars(sink, ordinal, 0L, buffer, bufferOffset, length);
				sink.ProcessMessagesAndThrow();
			}
			setters.SetCharsLength(sink, ordinal, (long)length);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000C9C6 File Offset: 0x0000ABC6
		private static int SetChars_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int result = setters.SetChars(sink, ordinal, fieldOffset, buffer, bufferOffset, length);
			sink.ProcessMessagesAndThrow();
			return result;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000C9DD File Offset: 0x0000ABDD
		private static void SetDBNull_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal)
		{
			setters.SetDBNull(sink, ordinal);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000C9ED File Offset: 0x0000ABED
		private static void SetDecimal_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, decimal value)
		{
			setters.SetSqlDecimal(sink, ordinal, new SqlDecimal(value));
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000CA03 File Offset: 0x0000AC03
		private static void SetDateTime_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, DateTime value)
		{
			setters.SetDateTime(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000CA14 File Offset: 0x0000AC14
		private static void SetDateTime2_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			setters.SetVariantMetaData(sink, ordinal, SmiMetaData.DefaultDateTime2);
			setters.SetDateTime(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000CA33 File Offset: 0x0000AC33
		private static void SetDate_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			setters.SetVariantMetaData(sink, ordinal, SmiMetaData.DefaultDate);
			setters.SetDateTime(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000CA52 File Offset: 0x0000AC52
		private static void SetTimeSpan_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, TimeSpan value)
		{
			setters.SetTimeSpan(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000CA63 File Offset: 0x0000AC63
		private static void SetDateTimeOffset_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, DateTimeOffset value)
		{
			setters.SetDateTimeOffset(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000CA74 File Offset: 0x0000AC74
		private static void SetDouble_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, double value)
		{
			setters.SetDouble(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000CA85 File Offset: 0x0000AC85
		private static void SetGuid_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, Guid value)
		{
			setters.SetGuid(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000CA96 File Offset: 0x0000AC96
		private static void SetInt16_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, short value)
		{
			setters.SetInt16(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000CAA7 File Offset: 0x0000ACA7
		private static void SetInt32_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, int value)
		{
			setters.SetInt32(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000CAB8 File Offset: 0x0000ACB8
		private static void SetInt64_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, long value)
		{
			setters.SetInt64(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000CAC9 File Offset: 0x0000ACC9
		private static void SetSingle_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, float value)
		{
			setters.SetSingle(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000CADA File Offset: 0x0000ACDA
		private static void SetSqlBinary_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlBinary value, int offset, int length)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				ValueUtilsSmi.SetByteArray_Unchecked(sink, setters, ordinal, value.Value, offset, length);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000CB08 File Offset: 0x0000AD08
		private static void SetSqlBoolean_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlBoolean value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetBoolean(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000CB32 File Offset: 0x0000AD32
		private static void SetSqlByte_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlByte value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetByte(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000CB5C File Offset: 0x0000AD5C
		private static void SetSqlBytes_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlBytes value, int offset, long length)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			int num;
			if (length > 8000L || length < 0L)
			{
				num = 8000;
			}
			else
			{
				num = checked((int)length);
			}
			byte[] buffer = new byte[num];
			long num2 = 1L;
			long num3 = (long)offset;
			long num4 = 0L;
			long num5;
			while ((length < 0L || num4 < length) && (num5 = value.Read(num3, buffer, 0, num)) != 0L && num2 != 0L)
			{
				num2 = (long)setters.SetBytes(sink, ordinal, num3, buffer, 0, checked((int)num5));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num3 += num2;
					num4 += num2;
				}
			}
			setters.SetBytesLength(sink, ordinal, num3);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000CC04 File Offset: 0x0000AE04
		private static void SetSqlChars_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlChars value, int offset, int length)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			int num;
			if (length > 4000 || length < 0)
			{
				num = 4000;
			}
			else
			{
				num = length;
			}
			char[] buffer = new char[num];
			long num2 = 1L;
			long num3 = (long)offset;
			long num4 = 0L;
			long num5;
			while ((length < 0 || num4 < (long)length) && (num5 = value.Read(num3, buffer, 0, num)) != 0L && num2 != 0L)
			{
				num2 = (long)setters.SetChars(sink, ordinal, num3, buffer, 0, checked((int)num5));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num3 += num2;
					num4 += num2;
				}
			}
			setters.SetCharsLength(sink, ordinal, num3);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000CCA6 File Offset: 0x0000AEA6
		private static void SetSqlDateTime_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlDateTime value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetDateTime(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000CCD0 File Offset: 0x0000AED0
		private static void SetSqlDecimal_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlDecimal value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetSqlDecimal(sink, ordinal, value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000CCF4 File Offset: 0x0000AEF4
		private static void SetSqlDouble_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlDouble value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetDouble(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000CD1E File Offset: 0x0000AF1E
		private static void SetSqlGuid_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlGuid value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetGuid(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000CD48 File Offset: 0x0000AF48
		private static void SetSqlInt16_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlInt16 value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetInt16(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000CD72 File Offset: 0x0000AF72
		private static void SetSqlInt32_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlInt32 value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetInt32(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000CD9C File Offset: 0x0000AF9C
		private static void SetSqlInt64_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlInt64 value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetInt64(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000CDC8 File Offset: 0x0000AFC8
		private static void SetSqlMoney_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlMoney value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				if (SqlDbType.Variant == metaData.SqlDbType)
				{
					setters.SetVariantMetaData(sink, ordinal, SmiMetaData.DefaultMoney);
					sink.ProcessMessagesAndThrow();
				}
				setters.SetInt64(sink, ordinal, SqlTypeWorkarounds.SqlMoneyToSqlInternalRepresentation(value));
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000CE1A File Offset: 0x0000B01A
		private static void SetSqlSingle_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlSingle value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetSingle(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000CE44 File Offset: 0x0000B044
		private static void SetSqlString_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlString value, int offset, int length)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			if (SqlDbType.Variant == metaData.SqlDbType)
			{
				metaData = new SmiMetaData(SqlDbType.NVarChar, 4000L, 0, 0, (long)value.LCID, value.SqlCompareOptions, null);
				setters.SetVariantMetaData(sink, ordinal, metaData);
				sink.ProcessMessagesAndThrow();
			}
			ValueUtilsSmi.SetString_Unchecked(sink, setters, ordinal, value.Value, offset, length);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000CEB6 File Offset: 0x0000B0B6
		private static void SetSqlXml_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlXml value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			ValueUtilsSmi.SetXmlReader_Unchecked(sink, setters, ordinal, value.CreateReader());
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000CEE0 File Offset: 0x0000B0E0
		private static void SetXmlReader_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, XmlReader xmlReader)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.CloseOutput = false;
			xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
			xmlWriterSettings.Encoding = Encoding.Unicode;
			xmlWriterSettings.OmitXmlDeclaration = true;
			XmlWriter xmlWriter = XmlWriter.Create(new SmiSettersStream(sink, setters, ordinal, SmiMetaData.DefaultXml), xmlWriterSettings);
			xmlReader.Read();
			while (!xmlReader.EOF)
			{
				xmlWriter.WriteNode(xmlReader, true);
			}
			xmlWriter.Flush();
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000CF4C File Offset: 0x0000B14C
		private static void SetString_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, string value, int offset, int length)
		{
			setters.SetString(sink, ordinal, value, offset, length);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000CF64 File Offset: 0x0000B164
		private static void SetDbDataReader_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader value)
		{
			setters = setters.GetTypedGetterSetter(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			while (value.Read())
			{
				setters.NewElement(sink);
				sink.ProcessMessagesAndThrow();
				ValueUtilsSmi.FillCompatibleSettersFromReader(sink, setters, metaData.FieldMetaData, value);
			}
			setters.EndElements(sink);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000CFB8 File Offset: 0x0000B1B8
		private static void SetIEnumerableOfSqlDataRecord_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, IEnumerable<SqlDataRecord> value, ParameterPeekAheadValue peekAhead)
		{
			setters = setters.GetTypedGetterSetter(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			IEnumerator<SqlDataRecord> enumerator = null;
			try
			{
				SmiExtendedMetaData[] array = new SmiExtendedMetaData[metaData.FieldMetaData.Count];
				metaData.FieldMetaData.CopyTo(array, 0);
				SmiDefaultFieldsProperty useDefaultValues = (SmiDefaultFieldsProperty)metaData.ExtendedProperties[SmiPropertySelector.DefaultFields];
				int num = 1;
				if (peekAhead != null && peekAhead.FirstRecord != null)
				{
					enumerator = peekAhead.Enumerator;
					setters.NewElement(sink);
					sink.ProcessMessagesAndThrow();
					SmiTypedGetterSetter setters2 = setters;
					SmiMetaData[] metaData2 = array;
					ValueUtilsSmi.FillCompatibleSettersFromRecord(sink, setters2, metaData2, peekAhead.FirstRecord, useDefaultValues);
					num++;
				}
				else
				{
					enumerator = value.GetEnumerator();
				}
				using (enumerator)
				{
					while (enumerator.MoveNext())
					{
						setters.NewElement(sink);
						sink.ProcessMessagesAndThrow();
						SqlDataRecord sqlDataRecord = enumerator.Current;
						if (sqlDataRecord.FieldCount != array.Length)
						{
							throw SQL.EnumeratedRecordFieldCountChanged(num);
						}
						for (int i = 0; i < sqlDataRecord.FieldCount; i++)
						{
							if (!MetaDataUtilsSmi.IsCompatible(metaData.FieldMetaData[i], sqlDataRecord.GetSqlMetaData(i)))
							{
								throw SQL.EnumeratedRecordMetaDataChanged(sqlDataRecord.GetName(i), num);
							}
						}
						SmiTypedGetterSetter setters3 = setters;
						SmiMetaData[] metaData2 = array;
						ValueUtilsSmi.FillCompatibleSettersFromRecord(sink, setters3, metaData2, sqlDataRecord, useDefaultValues);
						num++;
					}
					setters.EndElements(sink);
					sink.ProcessMessagesAndThrow();
				}
			}
			finally
			{
				IDisposable disposable = enumerator;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000D13C File Offset: 0x0000B33C
		// Note: this type is marked as 'beforefieldinit'.
		static ValueUtilsSmi()
		{
		}

		// Token: 0x040004F7 RID: 1271
		private const int __maxByteChunkSize = 8000;

		// Token: 0x040004F8 RID: 1272
		private const int __maxCharChunkSize = 4000;

		// Token: 0x040004F9 RID: 1273
		private const int NoLengthLimit = -1;

		// Token: 0x040004FA RID: 1274
		private const int constBinBufferSize = 4096;

		// Token: 0x040004FB RID: 1275
		private const int constTextBufferSize = 4096;

		// Token: 0x040004FC RID: 1276
		private static object[] s_typeSpecificNullForSqlValue = new object[]
		{
			SqlInt64.Null,
			SqlBinary.Null,
			SqlBoolean.Null,
			SqlString.Null,
			SqlDateTime.Null,
			SqlDecimal.Null,
			SqlDouble.Null,
			SqlBinary.Null,
			SqlInt32.Null,
			SqlMoney.Null,
			SqlString.Null,
			SqlString.Null,
			SqlString.Null,
			SqlSingle.Null,
			SqlGuid.Null,
			SqlDateTime.Null,
			SqlInt16.Null,
			SqlMoney.Null,
			SqlString.Null,
			SqlBinary.Null,
			SqlByte.Null,
			SqlBinary.Null,
			SqlString.Null,
			DBNull.Value,
			null,
			SqlXml.Null,
			null,
			null,
			null,
			null,
			null,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value
		};

		// Token: 0x040004FD RID: 1277
		private static readonly DateTime s_dtSmallMax = new DateTime(2079, 6, 6, 23, 59, 29, 998);

		// Token: 0x040004FE RID: 1278
		private static readonly DateTime s_dtSmallMin = new DateTime(1899, 12, 31, 23, 59, 29, 999);

		// Token: 0x040004FF RID: 1279
		private static readonly TimeSpan s_timeMin = TimeSpan.Zero;

		// Token: 0x04000500 RID: 1280
		private static readonly TimeSpan s_timeMax = new TimeSpan(863999999999L);

		// Token: 0x04000501 RID: 1281
		private const bool X = true;

		// Token: 0x04000502 RID: 1282
		private const bool _ = false;

		// Token: 0x04000503 RID: 1283
		private static bool[,] s_canAccessGetterDirectly = new bool[,]
		{
			{
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				true,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				true,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				false,
				true,
				true,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				true,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true
			},
			{
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			}
		};

		// Token: 0x04000504 RID: 1284
		private static bool[,] s_canAccessSetterDirectly = new bool[,]
		{
			{
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				true,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				true,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				true,
				false,
				true,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				true,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				true,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				true,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				true,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			}
		};
	}
}
