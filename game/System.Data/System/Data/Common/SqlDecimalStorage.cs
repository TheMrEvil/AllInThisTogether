using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x020003B0 RID: 944
	internal sealed class SqlDecimalStorage : DataStorage
	{
		// Token: 0x06002DDC RID: 11740 RVA: 0x000C38E3 File Offset: 0x000C1AE3
		public SqlDecimalStorage(DataColumn column) : base(column, typeof(SqlDecimal), SqlDecimal.Null, SqlDecimal.Null, StorageType.SqlDecimal)
		{
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x000C390C File Offset: 0x000C1B0C
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
							x += this._values[num3];
							num2++;
							flag = true;
						}
					}
					if (flag)
					{
						0L;
						return x / (long)num2;
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					SqlDecimal sqlDecimal2 = SqlDecimal.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlDecimal.LessThan(this._values[num4], sqlDecimal2).IsTrue)
							{
								sqlDecimal2 = this._values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDecimal2;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlDecimal sqlDecimal3 = SqlDecimal.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlDecimal.GreaterThan(this._values[num5], sqlDecimal3).IsTrue)
							{
								sqlDecimal3 = this._values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDecimal3;
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
				throw ExprException.Overflow(typeof(SqlDecimal));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x000C3D38 File Offset: 0x000C1F38
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x000C3D57 File Offset: 0x000C1F57
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlDecimal)value);
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x000C3D70 File Offset: 0x000C1F70
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlDecimal(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x000C3D87 File Offset: 0x000C1F87
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000C3DA1 File Offset: 0x000C1FA1
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x000C3DB4 File Offset: 0x000C1FB4
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x000C3DC7 File Offset: 0x000C1FC7
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlDecimal(value);
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x000C3DDC File Offset: 0x000C1FDC
		public override void SetCapacity(int capacity)
		{
			SqlDecimal[] array = new SqlDecimal[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x000C3E1C File Offset: 0x000C201C
		public override object ConvertXmlToObject(string s)
		{
			SqlDecimal sqlDecimal = default(SqlDecimal);
			TextReader input = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlDecimal;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlDecimal)xmlSerializable;
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x000C3E84 File Offset: 0x000C2084
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x000C3ED4 File Offset: 0x000C20D4
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlDecimal[recordCount];
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x000C3EDC File Offset: 0x000C20DC
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlDecimal[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x000C3F06 File Offset: 0x000C2106
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlDecimal[])store;
		}

		// Token: 0x04001BB1 RID: 7089
		private SqlDecimal[] _values;
	}
}
