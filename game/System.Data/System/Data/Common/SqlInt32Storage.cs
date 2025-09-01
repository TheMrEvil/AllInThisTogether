using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x020003B4 RID: 948
	internal sealed class SqlInt32Storage : DataStorage
	{
		// Token: 0x06002E18 RID: 11800 RVA: 0x000C4E28 File Offset: 0x000C3028
		public SqlInt32Storage(DataColumn column) : base(column, typeof(SqlInt32), SqlInt32.Null, SqlInt32.Null, StorageType.SqlInt32)
		{
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x000C4E54 File Offset: 0x000C3054
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					SqlInt64 sqlInt = 0L;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlInt += this._values[num];
							flag = true;
						}
					}
					if (flag)
					{
						return sqlInt;
					}
					return this._nullValue;
				}
				case AggregateType.Mean:
				{
					SqlInt64 x = 0L;
					int num2 = 0;
					foreach (int num3 in records)
					{
						if (!this.IsNull(num3))
						{
							x += this._values[num3].ToSqlInt64();
							num2++;
							flag = true;
						}
					}
					if (flag)
					{
						0;
						return (x / (long)num2).ToSqlInt32();
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					SqlInt32 sqlInt2 = SqlInt32.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlInt32.LessThan(this._values[num4], sqlInt2).IsTrue)
							{
								sqlInt2 = this._values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlInt2;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlInt32 sqlInt3 = SqlInt32.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlInt32.GreaterThan(this._values[num5], sqlInt3).IsTrue)
							{
								sqlInt3 = this._values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlInt3;
					}
					return this._nullValue;
				}
				case AggregateType.First:
					if (records.Length != 0)
					{
						return this._values[records[0]];
					}
					return null;
				case AggregateType.Count:
				{
					int num6 = 0;
					for (int l = 0; l < records.Length; l++)
					{
						if (!this.IsNull(records[l]))
						{
							num6++;
						}
					}
					return num6;
				}
				case AggregateType.Var:
				case AggregateType.StDev:
				{
					int num6 = 0;
					SqlDouble sqlDouble = 0.0;
					0.0;
					SqlDouble sqlDouble2 = 0.0;
					SqlDouble sqlDouble3 = 0.0;
					foreach (int num7 in records)
					{
						if (!this.IsNull(num7))
						{
							sqlDouble2 += this._values[num7].ToSqlDouble();
							sqlDouble3 += this._values[num7].ToSqlDouble() * this._values[num7].ToSqlDouble();
							num6++;
						}
					}
					if (num6 <= 1)
					{
						return this._nullValue;
					}
					sqlDouble = (double)num6 * sqlDouble3 - sqlDouble2 * sqlDouble2;
					SqlBoolean sqlBoolean = sqlDouble / (sqlDouble2 * sqlDouble2) < 1E-15;
					if (sqlBoolean ? sqlBoolean : (sqlBoolean | sqlDouble < 0.0))
					{
						sqlDouble = 0.0;
					}
					else
					{
						sqlDouble /= (double)(num6 * (num6 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(sqlDouble.Value);
					}
					return sqlDouble;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlInt32));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x000C5290 File Offset: 0x000C3490
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x000C52AF File Offset: 0x000C34AF
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlInt32)value);
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x000C52C8 File Offset: 0x000C34C8
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlInt32(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x000C52DF File Offset: 0x000C34DF
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x000C52F9 File Offset: 0x000C34F9
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x000C530C File Offset: 0x000C350C
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x000C531F File Offset: 0x000C351F
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlInt32(value);
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x000C5334 File Offset: 0x000C3534
		public override void SetCapacity(int capacity)
		{
			SqlInt32[] array = new SqlInt32[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000C5374 File Offset: 0x000C3574
		public override object ConvertXmlToObject(string s)
		{
			SqlInt32 sqlInt = default(SqlInt32);
			TextReader input = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlInt;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlInt32)xmlSerializable;
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000C53DC File Offset: 0x000C35DC
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x000C542C File Offset: 0x000C362C
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlInt32[recordCount];
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x000C5434 File Offset: 0x000C3634
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlInt32[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000C545E File Offset: 0x000C365E
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlInt32[])store;
		}

		// Token: 0x04001BB5 RID: 7093
		private SqlInt32[] _values;
	}
}
