using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x020003B6 RID: 950
	internal sealed class SqlMoneyStorage : DataStorage
	{
		// Token: 0x06002E36 RID: 11830 RVA: 0x000C5AAC File Offset: 0x000C3CAC
		public SqlMoneyStorage(DataColumn column) : base(column, typeof(SqlMoney), SqlMoney.Null, SqlMoney.Null, StorageType.SqlMoney)
		{
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x000C5AD8 File Offset: 0x000C3CD8
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					SqlDecimal sqlDecimal = 0L;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlDecimal += this._values[num];
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDecimal;
					}
					return this._nullValue;
				}
				case AggregateType.Mean:
				{
					SqlDecimal x = 0L;
					int num2 = 0;
					foreach (int num3 in records)
					{
						if (!this.IsNull(num3))
						{
							x += this._values[num3].ToSqlDecimal();
							num2++;
							flag = true;
						}
					}
					if (flag)
					{
						0L;
						return (x / (long)num2).ToSqlMoney();
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					SqlMoney sqlMoney = SqlMoney.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlMoney.LessThan(this._values[num4], sqlMoney).IsTrue)
							{
								sqlMoney = this._values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlMoney;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlMoney sqlMoney2 = SqlMoney.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlMoney.GreaterThan(this._values[num5], sqlMoney2).IsTrue)
							{
								sqlMoney2 = this._values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlMoney2;
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
				throw ExprException.Overflow(typeof(SqlMoney));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x000C5F18 File Offset: 0x000C4118
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000C5F37 File Offset: 0x000C4137
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlMoney)value);
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x000C5F50 File Offset: 0x000C4150
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlMoney(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000C5F67 File Offset: 0x000C4167
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000C5F81 File Offset: 0x000C4181
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x000C5F94 File Offset: 0x000C4194
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x000C5FA7 File Offset: 0x000C41A7
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlMoney(value);
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x000C5FBC File Offset: 0x000C41BC
		public override void SetCapacity(int capacity)
		{
			SqlMoney[] array = new SqlMoney[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x000C5FFC File Offset: 0x000C41FC
		public override object ConvertXmlToObject(string s)
		{
			SqlMoney sqlMoney = default(SqlMoney);
			TextReader input = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlMoney;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlMoney)xmlSerializable;
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x000C6064 File Offset: 0x000C4264
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000C60B4 File Offset: 0x000C42B4
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlMoney[recordCount];
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x000C60BC File Offset: 0x000C42BC
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlMoney[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000C60E6 File Offset: 0x000C42E6
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlMoney[])store;
		}

		// Token: 0x04001BB7 RID: 7095
		private SqlMoney[] _values;
	}
}
