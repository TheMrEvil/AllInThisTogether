using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x0200018C RID: 396
	internal sealed class SqlBuffer
	{
		// Token: 0x06001427 RID: 5159 RVA: 0x00003D93 File Offset: 0x00001F93
		internal SqlBuffer()
		{
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0005B3D9 File Offset: 0x000595D9
		private SqlBuffer(SqlBuffer value)
		{
			this._isNull = value._isNull;
			this._type = value._type;
			this._value = value._value;
			this._object = value._object;
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0005B411 File Offset: 0x00059611
		internal bool IsEmpty
		{
			get
			{
				return this._type == SqlBuffer.StorageType.Empty;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0005B41C File Offset: 0x0005961C
		internal bool IsNull
		{
			get
			{
				return this._isNull;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x0005B424 File Offset: 0x00059624
		internal SqlBuffer.StorageType VariantInternalStorageType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0005B42C File Offset: 0x0005962C
		// (set) Token: 0x0600142D RID: 5165 RVA: 0x0005B454 File Offset: 0x00059654
		internal bool Boolean
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Boolean == this._type)
				{
					return this._value._boolean;
				}
				return (bool)this.Value;
			}
			set
			{
				this._value._boolean = value;
				this._type = SqlBuffer.StorageType.Boolean;
				this._isNull = false;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x0005B470 File Offset: 0x00059670
		// (set) Token: 0x0600142F RID: 5167 RVA: 0x0005B498 File Offset: 0x00059698
		internal byte Byte
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Byte == this._type)
				{
					return this._value._byte;
				}
				return (byte)this.Value;
			}
			set
			{
				this._value._byte = value;
				this._type = SqlBuffer.StorageType.Byte;
				this._isNull = false;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x0005B4B4 File Offset: 0x000596B4
		internal byte[] ByteArray
		{
			get
			{
				this.ThrowIfNull();
				return this.SqlBinary.Value;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x0005B4D8 File Offset: 0x000596D8
		internal DateTime DateTime
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Date == this._type)
				{
					return DateTime.MinValue.AddDays((double)this._value._int32);
				}
				if (SqlBuffer.StorageType.DateTime2 == this._type)
				{
					return new DateTime(SqlBuffer.GetTicksFromDateTime2Info(this._value._dateTime2Info));
				}
				if (SqlBuffer.StorageType.DateTime == this._type)
				{
					return SqlTypeWorkarounds.SqlDateTimeToDateTime(this._value._dateTimeInfo.daypart, this._value._dateTimeInfo.timepart);
				}
				return (DateTime)this.Value;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0005B568 File Offset: 0x00059768
		internal decimal Decimal
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Decimal == this._type)
				{
					if (this._value._numericInfo.data4 != 0 || this._value._numericInfo.scale > 28)
					{
						throw new OverflowException(SQLResource.ConversionOverflowMessage);
					}
					return new decimal(this._value._numericInfo.data1, this._value._numericInfo.data2, this._value._numericInfo.data3, !this._value._numericInfo.positive, this._value._numericInfo.scale);
				}
				else
				{
					if (SqlBuffer.StorageType.Money == this._type)
					{
						long num = this._value._int64;
						bool isNegative = false;
						if (num < 0L)
						{
							isNegative = true;
							num = -num;
						}
						return new decimal((int)(num & (long)((ulong)-1)), (int)(num >> 32), 0, isNegative, 4);
					}
					return (decimal)this.Value;
				}
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0005B651 File Offset: 0x00059851
		// (set) Token: 0x06001434 RID: 5172 RVA: 0x0005B679 File Offset: 0x00059879
		internal double Double
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Double == this._type)
				{
					return this._value._double;
				}
				return (double)this.Value;
			}
			set
			{
				this._value._double = value;
				this._type = SqlBuffer.StorageType.Double;
				this._isNull = false;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x0005B698 File Offset: 0x00059898
		internal Guid Guid
		{
			get
			{
				this.ThrowIfNull();
				return this.SqlGuid.Value;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x0005B6B9 File Offset: 0x000598B9
		// (set) Token: 0x06001437 RID: 5175 RVA: 0x0005B6E1 File Offset: 0x000598E1
		internal short Int16
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Int16 == this._type)
				{
					return this._value._int16;
				}
				return (short)this.Value;
			}
			set
			{
				this._value._int16 = value;
				this._type = SqlBuffer.StorageType.Int16;
				this._isNull = false;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x0005B6FD File Offset: 0x000598FD
		// (set) Token: 0x06001439 RID: 5177 RVA: 0x0005B725 File Offset: 0x00059925
		internal int Int32
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Int32 == this._type)
				{
					return this._value._int32;
				}
				return (int)this.Value;
			}
			set
			{
				this._value._int32 = value;
				this._type = SqlBuffer.StorageType.Int32;
				this._isNull = false;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x0005B741 File Offset: 0x00059941
		// (set) Token: 0x0600143B RID: 5179 RVA: 0x0005B769 File Offset: 0x00059969
		internal long Int64
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Int64 == this._type)
				{
					return this._value._int64;
				}
				return (long)this.Value;
			}
			set
			{
				this._value._int64 = value;
				this._type = SqlBuffer.StorageType.Int64;
				this._isNull = false;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x0005B785 File Offset: 0x00059985
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x0005B7AE File Offset: 0x000599AE
		internal float Single
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Single == this._type)
				{
					return this._value._single;
				}
				return (float)this.Value;
			}
			set
			{
				this._value._single = value;
				this._type = SqlBuffer.StorageType.Single;
				this._isNull = false;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0005B7CC File Offset: 0x000599CC
		internal string String
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.String == this._type)
				{
					return (string)this._object;
				}
				if (SqlBuffer.StorageType.SqlCachedBuffer == this._type)
				{
					return ((SqlCachedBuffer)this._object).ToString();
				}
				return (string)this.Value;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0005B81C File Offset: 0x00059A1C
		internal string KatmaiDateTimeString
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Date == this._type)
				{
					return this.DateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
				}
				if (SqlBuffer.StorageType.Time == this._type)
				{
					byte scale = this._value._timeInfo.scale;
					return new DateTime(this._value._timeInfo.ticks).ToString(SqlBuffer.s_katmaiTimeFormatByScale[(int)scale], DateTimeFormatInfo.InvariantInfo);
				}
				if (SqlBuffer.StorageType.DateTime2 == this._type)
				{
					byte scale2 = this._value._dateTime2Info.timeInfo.scale;
					return this.DateTime.ToString(SqlBuffer.s_katmaiDateTime2FormatByScale[(int)scale2], DateTimeFormatInfo.InvariantInfo);
				}
				if (SqlBuffer.StorageType.DateTimeOffset == this._type)
				{
					DateTimeOffset dateTimeOffset = this.DateTimeOffset;
					byte scale3 = this._value._dateTimeOffsetInfo.dateTime2Info.timeInfo.scale;
					return dateTimeOffset.ToString(SqlBuffer.s_katmaiDateTimeOffsetFormatByScale[(int)scale3], DateTimeFormatInfo.InvariantInfo);
				}
				return (string)this.Value;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0005B920 File Offset: 0x00059B20
		internal SqlString KatmaiDateTimeSqlString
		{
			get
			{
				if (SqlBuffer.StorageType.Date != this._type && SqlBuffer.StorageType.Time != this._type && SqlBuffer.StorageType.DateTime2 != this._type && SqlBuffer.StorageType.DateTimeOffset != this._type)
				{
					return (SqlString)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlString.Null;
				}
				return new SqlString(this.KatmaiDateTimeString);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x0005B97A File Offset: 0x00059B7A
		internal TimeSpan Time
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Time == this._type)
				{
					return new TimeSpan(this._value._timeInfo.ticks);
				}
				return (TimeSpan)this.Value;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x0005B9B0 File Offset: 0x00059BB0
		internal DateTimeOffset DateTimeOffset
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.DateTimeOffset == this._type)
				{
					TimeSpan offset = new TimeSpan(0, (int)this._value._dateTimeOffsetInfo.offset, 0);
					return new DateTimeOffset(SqlBuffer.GetTicksFromDateTime2Info(this._value._dateTimeOffsetInfo.dateTime2Info) + offset.Ticks, offset);
				}
				return (DateTimeOffset)this.Value;
			}
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0005BA15 File Offset: 0x00059C15
		private static long GetTicksFromDateTime2Info(SqlBuffer.DateTime2Info dateTime2Info)
		{
			return (long)dateTime2Info.date * 864000000000L + dateTime2Info.timeInfo.ticks;
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0005BA34 File Offset: 0x00059C34
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x0005BA57 File Offset: 0x00059C57
		internal SqlBinary SqlBinary
		{
			get
			{
				if (SqlBuffer.StorageType.SqlBinary == this._type)
				{
					return (SqlBinary)this._object;
				}
				return (SqlBinary)this.SqlValue;
			}
			set
			{
				this._object = value;
				this._type = SqlBuffer.StorageType.SqlBinary;
				this._isNull = value.IsNull;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0005BA7A File Offset: 0x00059C7A
		internal SqlBoolean SqlBoolean
		{
			get
			{
				if (SqlBuffer.StorageType.Boolean != this._type)
				{
					return (SqlBoolean)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlBoolean.Null;
				}
				return new SqlBoolean(this._value._boolean);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0005BAAF File Offset: 0x00059CAF
		internal SqlByte SqlByte
		{
			get
			{
				if (SqlBuffer.StorageType.Byte != this._type)
				{
					return (SqlByte)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlByte.Null;
				}
				return new SqlByte(this._value._byte);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x0005BAE4 File Offset: 0x00059CE4
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x0005BB15 File Offset: 0x00059D15
		internal SqlCachedBuffer SqlCachedBuffer
		{
			get
			{
				if (SqlBuffer.StorageType.SqlCachedBuffer != this._type)
				{
					return (SqlCachedBuffer)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlCachedBuffer.Null;
				}
				return (SqlCachedBuffer)this._object;
			}
			set
			{
				this._object = value;
				this._type = SqlBuffer.StorageType.SqlCachedBuffer;
				this._isNull = value.IsNull;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x0005BB32 File Offset: 0x00059D32
		// (set) Token: 0x0600144B RID: 5195 RVA: 0x0005BB63 File Offset: 0x00059D63
		internal SqlXml SqlXml
		{
			get
			{
				if (SqlBuffer.StorageType.SqlXml != this._type)
				{
					return (SqlXml)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlXml.Null;
				}
				return (SqlXml)this._object;
			}
			set
			{
				this._object = value;
				this._type = SqlBuffer.StorageType.SqlXml;
				this._isNull = value.IsNull;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x0005BB80 File Offset: 0x00059D80
		internal SqlDateTime SqlDateTime
		{
			get
			{
				if (SqlBuffer.StorageType.DateTime != this._type)
				{
					return (SqlDateTime)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlDateTime.Null;
				}
				return new SqlDateTime(this._value._dateTimeInfo.daypart, this._value._dateTimeInfo.timepart);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x0005BBD8 File Offset: 0x00059DD8
		internal SqlDecimal SqlDecimal
		{
			get
			{
				if (SqlBuffer.StorageType.Decimal != this._type)
				{
					return (SqlDecimal)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlDecimal.Null;
				}
				return new SqlDecimal(this._value._numericInfo.precision, this._value._numericInfo.scale, this._value._numericInfo.positive, this._value._numericInfo.data1, this._value._numericInfo.data2, this._value._numericInfo.data3, this._value._numericInfo.data4);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x0005BC80 File Offset: 0x00059E80
		internal SqlDouble SqlDouble
		{
			get
			{
				if (SqlBuffer.StorageType.Double != this._type)
				{
					return (SqlDouble)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlDouble.Null;
				}
				return new SqlDouble(this._value._double);
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x0005BCB5 File Offset: 0x00059EB5
		// (set) Token: 0x06001450 RID: 5200 RVA: 0x0005BCD8 File Offset: 0x00059ED8
		internal SqlGuid SqlGuid
		{
			get
			{
				if (SqlBuffer.StorageType.SqlGuid == this._type)
				{
					return (SqlGuid)this._object;
				}
				return (SqlGuid)this.SqlValue;
			}
			set
			{
				this._object = value;
				this._type = SqlBuffer.StorageType.SqlGuid;
				this._isNull = value.IsNull;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x0005BCFB File Offset: 0x00059EFB
		internal SqlInt16 SqlInt16
		{
			get
			{
				if (SqlBuffer.StorageType.Int16 != this._type)
				{
					return (SqlInt16)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlInt16.Null;
				}
				return new SqlInt16(this._value._int16);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x0005BD30 File Offset: 0x00059F30
		internal SqlInt32 SqlInt32
		{
			get
			{
				if (SqlBuffer.StorageType.Int32 != this._type)
				{
					return (SqlInt32)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlInt32.Null;
				}
				return new SqlInt32(this._value._int32);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x0005BD65 File Offset: 0x00059F65
		internal SqlInt64 SqlInt64
		{
			get
			{
				if (SqlBuffer.StorageType.Int64 != this._type)
				{
					return (SqlInt64)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlInt64.Null;
				}
				return new SqlInt64(this._value._int64);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x0005BD9A File Offset: 0x00059F9A
		internal SqlMoney SqlMoney
		{
			get
			{
				if (SqlBuffer.StorageType.Money != this._type)
				{
					return (SqlMoney)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlMoney.Null;
				}
				return SqlTypeWorkarounds.SqlMoneyCtor(this._value._int64, 1);
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x0005BDD1 File Offset: 0x00059FD1
		internal SqlSingle SqlSingle
		{
			get
			{
				if (SqlBuffer.StorageType.Single != this._type)
				{
					return (SqlSingle)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlSingle.Null;
				}
				return new SqlSingle(this._value._single);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x0005BE08 File Offset: 0x0005A008
		internal SqlString SqlString
		{
			get
			{
				if (SqlBuffer.StorageType.String == this._type)
				{
					if (this.IsNull)
					{
						return SqlString.Null;
					}
					return new SqlString((string)this._object);
				}
				else
				{
					if (SqlBuffer.StorageType.SqlCachedBuffer != this._type)
					{
						return (SqlString)this.SqlValue;
					}
					SqlCachedBuffer sqlCachedBuffer = (SqlCachedBuffer)this._object;
					if (sqlCachedBuffer.IsNull)
					{
						return SqlString.Null;
					}
					return sqlCachedBuffer.ToSqlString();
				}
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x0005BE74 File Offset: 0x0005A074
		internal object SqlValue
		{
			get
			{
				switch (this._type)
				{
				case SqlBuffer.StorageType.Empty:
					return DBNull.Value;
				case SqlBuffer.StorageType.Boolean:
					return this.SqlBoolean;
				case SqlBuffer.StorageType.Byte:
					return this.SqlByte;
				case SqlBuffer.StorageType.DateTime:
					return this.SqlDateTime;
				case SqlBuffer.StorageType.Decimal:
					return this.SqlDecimal;
				case SqlBuffer.StorageType.Double:
					return this.SqlDouble;
				case SqlBuffer.StorageType.Int16:
					return this.SqlInt16;
				case SqlBuffer.StorageType.Int32:
					return this.SqlInt32;
				case SqlBuffer.StorageType.Int64:
					return this.SqlInt64;
				case SqlBuffer.StorageType.Money:
					return this.SqlMoney;
				case SqlBuffer.StorageType.Single:
					return this.SqlSingle;
				case SqlBuffer.StorageType.String:
					return this.SqlString;
				case SqlBuffer.StorageType.SqlBinary:
				case SqlBuffer.StorageType.SqlGuid:
					return this._object;
				case SqlBuffer.StorageType.SqlCachedBuffer:
				{
					SqlCachedBuffer sqlCachedBuffer = (SqlCachedBuffer)this._object;
					if (sqlCachedBuffer.IsNull)
					{
						return SqlXml.Null;
					}
					return sqlCachedBuffer.ToSqlXml();
				}
				case SqlBuffer.StorageType.SqlXml:
					if (this._isNull)
					{
						return SqlXml.Null;
					}
					return (SqlXml)this._object;
				case SqlBuffer.StorageType.Date:
				case SqlBuffer.StorageType.DateTime2:
					if (this._isNull)
					{
						return DBNull.Value;
					}
					return this.DateTime;
				case SqlBuffer.StorageType.DateTimeOffset:
					if (this._isNull)
					{
						return DBNull.Value;
					}
					return this.DateTimeOffset;
				case SqlBuffer.StorageType.Time:
					if (this._isNull)
					{
						return DBNull.Value;
					}
					return this.Time;
				default:
					return null;
				}
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0005C000 File Offset: 0x0005A200
		internal object Value
		{
			get
			{
				if (this.IsNull)
				{
					return DBNull.Value;
				}
				switch (this._type)
				{
				case SqlBuffer.StorageType.Empty:
					return DBNull.Value;
				case SqlBuffer.StorageType.Boolean:
					return this.Boolean;
				case SqlBuffer.StorageType.Byte:
					return this.Byte;
				case SqlBuffer.StorageType.DateTime:
					return this.DateTime;
				case SqlBuffer.StorageType.Decimal:
					return this.Decimal;
				case SqlBuffer.StorageType.Double:
					return this.Double;
				case SqlBuffer.StorageType.Int16:
					return this.Int16;
				case SqlBuffer.StorageType.Int32:
					return this.Int32;
				case SqlBuffer.StorageType.Int64:
					return this.Int64;
				case SqlBuffer.StorageType.Money:
					return this.Decimal;
				case SqlBuffer.StorageType.Single:
					return this.Single;
				case SqlBuffer.StorageType.String:
					return this.String;
				case SqlBuffer.StorageType.SqlBinary:
					return this.ByteArray;
				case SqlBuffer.StorageType.SqlCachedBuffer:
					return ((SqlCachedBuffer)this._object).ToString();
				case SqlBuffer.StorageType.SqlGuid:
					return this.Guid;
				case SqlBuffer.StorageType.SqlXml:
					return ((SqlXml)this._object).Value;
				case SqlBuffer.StorageType.Date:
					return this.DateTime;
				case SqlBuffer.StorageType.DateTime2:
					return this.DateTime;
				case SqlBuffer.StorageType.DateTimeOffset:
					return this.DateTimeOffset;
				case SqlBuffer.StorageType.Time:
					return this.Time;
				default:
					return null;
				}
			}
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0005C168 File Offset: 0x0005A368
		internal Type GetTypeFromStorageType(bool isSqlType)
		{
			if (isSqlType)
			{
				switch (this._type)
				{
				case SqlBuffer.StorageType.Empty:
					return null;
				case SqlBuffer.StorageType.Boolean:
					return typeof(SqlBoolean);
				case SqlBuffer.StorageType.Byte:
					return typeof(SqlByte);
				case SqlBuffer.StorageType.DateTime:
					return typeof(SqlDateTime);
				case SqlBuffer.StorageType.Decimal:
					return typeof(SqlDecimal);
				case SqlBuffer.StorageType.Double:
					return typeof(SqlDouble);
				case SqlBuffer.StorageType.Int16:
					return typeof(SqlInt16);
				case SqlBuffer.StorageType.Int32:
					return typeof(SqlInt32);
				case SqlBuffer.StorageType.Int64:
					return typeof(SqlInt64);
				case SqlBuffer.StorageType.Money:
					return typeof(SqlMoney);
				case SqlBuffer.StorageType.Single:
					return typeof(SqlSingle);
				case SqlBuffer.StorageType.String:
					return typeof(SqlString);
				case SqlBuffer.StorageType.SqlBinary:
					return typeof(object);
				case SqlBuffer.StorageType.SqlCachedBuffer:
					return typeof(SqlString);
				case SqlBuffer.StorageType.SqlGuid:
					return typeof(object);
				case SqlBuffer.StorageType.SqlXml:
					return typeof(SqlXml);
				}
			}
			else
			{
				switch (this._type)
				{
				case SqlBuffer.StorageType.Empty:
					return null;
				case SqlBuffer.StorageType.Boolean:
					return typeof(bool);
				case SqlBuffer.StorageType.Byte:
					return typeof(byte);
				case SqlBuffer.StorageType.DateTime:
					return typeof(DateTime);
				case SqlBuffer.StorageType.Decimal:
					return typeof(decimal);
				case SqlBuffer.StorageType.Double:
					return typeof(double);
				case SqlBuffer.StorageType.Int16:
					return typeof(short);
				case SqlBuffer.StorageType.Int32:
					return typeof(int);
				case SqlBuffer.StorageType.Int64:
					return typeof(long);
				case SqlBuffer.StorageType.Money:
					return typeof(decimal);
				case SqlBuffer.StorageType.Single:
					return typeof(float);
				case SqlBuffer.StorageType.String:
					return typeof(string);
				case SqlBuffer.StorageType.SqlBinary:
					return typeof(byte[]);
				case SqlBuffer.StorageType.SqlCachedBuffer:
					return typeof(string);
				case SqlBuffer.StorageType.SqlGuid:
					return typeof(Guid);
				case SqlBuffer.StorageType.SqlXml:
					return typeof(string);
				}
			}
			return null;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0005C370 File Offset: 0x0005A570
		internal static SqlBuffer[] CreateBufferArray(int length)
		{
			SqlBuffer[] array = new SqlBuffer[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new SqlBuffer();
			}
			return array;
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0005C39C File Offset: 0x0005A59C
		internal static SqlBuffer[] CloneBufferArray(SqlBuffer[] values)
		{
			SqlBuffer[] array = new SqlBuffer[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				array[i] = new SqlBuffer(values[i]);
			}
			return array;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0005C3CC File Offset: 0x0005A5CC
		internal static void Clear(SqlBuffer[] values)
		{
			if (values != null)
			{
				for (int i = 0; i < values.Length; i++)
				{
					values[i].Clear();
				}
			}
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0005C3F2 File Offset: 0x0005A5F2
		internal void Clear()
		{
			this._isNull = false;
			this._type = SqlBuffer.StorageType.Empty;
			this._object = null;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0005C409 File Offset: 0x0005A609
		internal void SetToDateTime(int daypart, int timepart)
		{
			this._value._dateTimeInfo.daypart = daypart;
			this._value._dateTimeInfo.timepart = timepart;
			this._type = SqlBuffer.StorageType.DateTime;
			this._isNull = false;
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0005C43C File Offset: 0x0005A63C
		internal void SetToDecimal(byte precision, byte scale, bool positive, int[] bits)
		{
			this._value._numericInfo.precision = precision;
			this._value._numericInfo.scale = scale;
			this._value._numericInfo.positive = positive;
			this._value._numericInfo.data1 = bits[0];
			this._value._numericInfo.data2 = bits[1];
			this._value._numericInfo.data3 = bits[2];
			this._value._numericInfo.data4 = bits[3];
			this._type = SqlBuffer.StorageType.Decimal;
			this._isNull = false;
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0005C4DA File Offset: 0x0005A6DA
		internal void SetToMoney(long value)
		{
			this._value._int64 = value;
			this._type = SqlBuffer.StorageType.Money;
			this._isNull = false;
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0005C4F7 File Offset: 0x0005A6F7
		internal void SetToNullOfType(SqlBuffer.StorageType storageType)
		{
			this._type = storageType;
			this._isNull = true;
			this._object = null;
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0005C50E File Offset: 0x0005A70E
		internal void SetToString(string value)
		{
			this._object = value;
			this._type = SqlBuffer.StorageType.String;
			this._isNull = false;
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0005C526 File Offset: 0x0005A726
		internal void SetToDate(byte[] bytes)
		{
			this._type = SqlBuffer.StorageType.Date;
			this._value._int32 = SqlBuffer.GetDateFromByteArray(bytes, 0);
			this._isNull = false;
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0005C54C File Offset: 0x0005A74C
		internal void SetToDate(DateTime date)
		{
			this._type = SqlBuffer.StorageType.Date;
			this._value._int32 = date.Subtract(DateTime.MinValue).Days;
			this._isNull = false;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0005C587 File Offset: 0x0005A787
		internal void SetToTime(byte[] bytes, int length, byte scale)
		{
			this._type = SqlBuffer.StorageType.Time;
			SqlBuffer.FillInTimeInfo(ref this._value._timeInfo, bytes, length, scale);
			this._isNull = false;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0005C5AB File Offset: 0x0005A7AB
		internal void SetToTime(TimeSpan timeSpan, byte scale)
		{
			this._type = SqlBuffer.StorageType.Time;
			this._value._timeInfo.ticks = timeSpan.Ticks;
			this._value._timeInfo.scale = scale;
			this._isNull = false;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0005C5E4 File Offset: 0x0005A7E4
		internal void SetToDateTime2(byte[] bytes, int length, byte scale)
		{
			this._type = SqlBuffer.StorageType.DateTime2;
			SqlBuffer.FillInTimeInfo(ref this._value._dateTime2Info.timeInfo, bytes, length - 3, scale);
			this._value._dateTime2Info.date = SqlBuffer.GetDateFromByteArray(bytes, length - 3);
			this._isNull = false;
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0005C634 File Offset: 0x0005A834
		internal void SetToDateTime2(DateTime dateTime, byte scale)
		{
			this._type = SqlBuffer.StorageType.DateTime2;
			this._value._dateTime2Info.timeInfo.ticks = dateTime.TimeOfDay.Ticks;
			this._value._dateTime2Info.timeInfo.scale = scale;
			this._value._dateTime2Info.date = dateTime.Subtract(DateTime.MinValue).Days;
			this._isNull = false;
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0005C6B0 File Offset: 0x0005A8B0
		internal void SetToDateTimeOffset(byte[] bytes, int length, byte scale)
		{
			this._type = SqlBuffer.StorageType.DateTimeOffset;
			SqlBuffer.FillInTimeInfo(ref this._value._dateTimeOffsetInfo.dateTime2Info.timeInfo, bytes, length - 5, scale);
			this._value._dateTimeOffsetInfo.dateTime2Info.date = SqlBuffer.GetDateFromByteArray(bytes, length - 5);
			this._value._dateTimeOffsetInfo.offset = (short)((int)bytes[length - 2] + ((int)bytes[length - 1] << 8));
			this._isNull = false;
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0005C728 File Offset: 0x0005A928
		internal void SetToDateTimeOffset(DateTimeOffset dateTimeOffset, byte scale)
		{
			this._type = SqlBuffer.StorageType.DateTimeOffset;
			DateTime utcDateTime = dateTimeOffset.UtcDateTime;
			this._value._dateTimeOffsetInfo.dateTime2Info.timeInfo.ticks = utcDateTime.TimeOfDay.Ticks;
			this._value._dateTimeOffsetInfo.dateTime2Info.timeInfo.scale = scale;
			this._value._dateTimeOffsetInfo.dateTime2Info.date = utcDateTime.Subtract(DateTime.MinValue).Days;
			this._value._dateTimeOffsetInfo.offset = (short)dateTimeOffset.Offset.TotalMinutes;
			this._isNull = false;
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0005C7DC File Offset: 0x0005A9DC
		private static void FillInTimeInfo(ref SqlBuffer.TimeInfo timeInfo, byte[] timeBytes, int length, byte scale)
		{
			long num = (long)((ulong)timeBytes[0] + ((ulong)timeBytes[1] << 8) + ((ulong)timeBytes[2] << 16));
			if (length > 3)
			{
				num += (long)((long)((ulong)timeBytes[3]) << 24);
			}
			if (length > 4)
			{
				num += (long)((long)((ulong)timeBytes[4]) << 32);
			}
			timeInfo.ticks = num * TdsEnums.TICKS_FROM_SCALE[(int)scale];
			timeInfo.scale = scale;
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0005C82F File Offset: 0x0005AA2F
		private static int GetDateFromByteArray(byte[] buf, int offset)
		{
			return (int)buf[offset] + ((int)buf[offset + 1] << 8) + ((int)buf[offset + 2] << 16);
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0005C845 File Offset: 0x0005AA45
		private void ThrowIfNull()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0005C858 File Offset: 0x0005AA58
		// Note: this type is marked as 'beforefieldinit'.
		static SqlBuffer()
		{
		}

		// Token: 0x04000CAD RID: 3245
		private bool _isNull;

		// Token: 0x04000CAE RID: 3246
		private SqlBuffer.StorageType _type;

		// Token: 0x04000CAF RID: 3247
		private SqlBuffer.Storage _value;

		// Token: 0x04000CB0 RID: 3248
		private object _object;

		// Token: 0x04000CB1 RID: 3249
		private static string[] s_katmaiDateTimeOffsetFormatByScale = new string[]
		{
			"yyyy-MM-dd HH:mm:ss zzz",
			"yyyy-MM-dd HH:mm:ss.f zzz",
			"yyyy-MM-dd HH:mm:ss.ff zzz",
			"yyyy-MM-dd HH:mm:ss.fff zzz",
			"yyyy-MM-dd HH:mm:ss.ffff zzz",
			"yyyy-MM-dd HH:mm:ss.fffff zzz",
			"yyyy-MM-dd HH:mm:ss.ffffff zzz",
			"yyyy-MM-dd HH:mm:ss.fffffff zzz"
		};

		// Token: 0x04000CB2 RID: 3250
		private static string[] s_katmaiDateTime2FormatByScale = new string[]
		{
			"yyyy-MM-dd HH:mm:ss",
			"yyyy-MM-dd HH:mm:ss.f",
			"yyyy-MM-dd HH:mm:ss.ff",
			"yyyy-MM-dd HH:mm:ss.fff",
			"yyyy-MM-dd HH:mm:ss.ffff",
			"yyyy-MM-dd HH:mm:ss.fffff",
			"yyyy-MM-dd HH:mm:ss.ffffff",
			"yyyy-MM-dd HH:mm:ss.fffffff"
		};

		// Token: 0x04000CB3 RID: 3251
		private static string[] s_katmaiTimeFormatByScale = new string[]
		{
			"HH:mm:ss",
			"HH:mm:ss.f",
			"HH:mm:ss.ff",
			"HH:mm:ss.fff",
			"HH:mm:ss.ffff",
			"HH:mm:ss.fffff",
			"HH:mm:ss.ffffff",
			"HH:mm:ss.fffffff"
		};

		// Token: 0x0200018D RID: 397
		internal enum StorageType
		{
			// Token: 0x04000CB5 RID: 3253
			Empty,
			// Token: 0x04000CB6 RID: 3254
			Boolean,
			// Token: 0x04000CB7 RID: 3255
			Byte,
			// Token: 0x04000CB8 RID: 3256
			DateTime,
			// Token: 0x04000CB9 RID: 3257
			Decimal,
			// Token: 0x04000CBA RID: 3258
			Double,
			// Token: 0x04000CBB RID: 3259
			Int16,
			// Token: 0x04000CBC RID: 3260
			Int32,
			// Token: 0x04000CBD RID: 3261
			Int64,
			// Token: 0x04000CBE RID: 3262
			Money,
			// Token: 0x04000CBF RID: 3263
			Single,
			// Token: 0x04000CC0 RID: 3264
			String,
			// Token: 0x04000CC1 RID: 3265
			SqlBinary,
			// Token: 0x04000CC2 RID: 3266
			SqlCachedBuffer,
			// Token: 0x04000CC3 RID: 3267
			SqlGuid,
			// Token: 0x04000CC4 RID: 3268
			SqlXml,
			// Token: 0x04000CC5 RID: 3269
			Date,
			// Token: 0x04000CC6 RID: 3270
			DateTime2,
			// Token: 0x04000CC7 RID: 3271
			DateTimeOffset,
			// Token: 0x04000CC8 RID: 3272
			Time
		}

		// Token: 0x0200018E RID: 398
		internal struct DateTimeInfo
		{
			// Token: 0x04000CC9 RID: 3273
			internal int daypart;

			// Token: 0x04000CCA RID: 3274
			internal int timepart;
		}

		// Token: 0x0200018F RID: 399
		internal struct NumericInfo
		{
			// Token: 0x04000CCB RID: 3275
			internal int data1;

			// Token: 0x04000CCC RID: 3276
			internal int data2;

			// Token: 0x04000CCD RID: 3277
			internal int data3;

			// Token: 0x04000CCE RID: 3278
			internal int data4;

			// Token: 0x04000CCF RID: 3279
			internal byte precision;

			// Token: 0x04000CD0 RID: 3280
			internal byte scale;

			// Token: 0x04000CD1 RID: 3281
			internal bool positive;
		}

		// Token: 0x02000190 RID: 400
		internal struct TimeInfo
		{
			// Token: 0x04000CD2 RID: 3282
			internal long ticks;

			// Token: 0x04000CD3 RID: 3283
			internal byte scale;
		}

		// Token: 0x02000191 RID: 401
		internal struct DateTime2Info
		{
			// Token: 0x04000CD4 RID: 3284
			internal int date;

			// Token: 0x04000CD5 RID: 3285
			internal SqlBuffer.TimeInfo timeInfo;
		}

		// Token: 0x02000192 RID: 402
		internal struct DateTimeOffsetInfo
		{
			// Token: 0x04000CD6 RID: 3286
			internal SqlBuffer.DateTime2Info dateTime2Info;

			// Token: 0x04000CD7 RID: 3287
			internal short offset;
		}

		// Token: 0x02000193 RID: 403
		[StructLayout(LayoutKind.Explicit)]
		internal struct Storage
		{
			// Token: 0x04000CD8 RID: 3288
			[FieldOffset(0)]
			internal bool _boolean;

			// Token: 0x04000CD9 RID: 3289
			[FieldOffset(0)]
			internal byte _byte;

			// Token: 0x04000CDA RID: 3290
			[FieldOffset(0)]
			internal SqlBuffer.DateTimeInfo _dateTimeInfo;

			// Token: 0x04000CDB RID: 3291
			[FieldOffset(0)]
			internal double _double;

			// Token: 0x04000CDC RID: 3292
			[FieldOffset(0)]
			internal SqlBuffer.NumericInfo _numericInfo;

			// Token: 0x04000CDD RID: 3293
			[FieldOffset(0)]
			internal short _int16;

			// Token: 0x04000CDE RID: 3294
			[FieldOffset(0)]
			internal int _int32;

			// Token: 0x04000CDF RID: 3295
			[FieldOffset(0)]
			internal long _int64;

			// Token: 0x04000CE0 RID: 3296
			[FieldOffset(0)]
			internal float _single;

			// Token: 0x04000CE1 RID: 3297
			[FieldOffset(0)]
			internal SqlBuffer.TimeInfo _timeInfo;

			// Token: 0x04000CE2 RID: 3298
			[FieldOffset(0)]
			internal SqlBuffer.DateTime2Info _dateTime2Info;

			// Token: 0x04000CE3 RID: 3299
			[FieldOffset(0)]
			internal SqlBuffer.DateTimeOffsetInfo _dateTimeOffsetInfo;
		}
	}
}
