using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x020003B1 RID: 945
	internal sealed class SqlDoubleStorage : DataStorage
	{
		// Token: 0x06002DEB RID: 11755 RVA: 0x000C3F14 File Offset: 0x000C2114
		public SqlDoubleStorage(DataColumn column) : base(column, typeof(SqlDouble), SqlDouble.Null, SqlDouble.Null, StorageType.SqlDouble)
		{
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x000C3F40 File Offset: 0x000C2140
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					SqlDouble sqlDouble = 0.0;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlDouble += this._values[num];
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDouble;
					}
					return this._nullValue;
				}
				case AggregateType.Mean:
				{
					SqlDouble x = 0.0;
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
						0.0;
						return x / (double)num2;
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					SqlDouble sqlDouble2 = SqlDouble.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlDouble.LessThan(this._values[num4], sqlDouble2).IsTrue)
							{
								sqlDouble2 = this._values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDouble2;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlDouble sqlDouble3 = SqlDouble.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlDouble.GreaterThan(this._values[num5], sqlDouble3).IsTrue)
							{
								sqlDouble3 = this._values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDouble3;
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
					SqlDouble sqlDouble4 = 0.0;
					0.0;
					SqlDouble sqlDouble5 = 0.0;
					SqlDouble sqlDouble6 = 0.0;
					foreach (int num7 in records)
					{
						if (!this.IsNull(num7))
						{
							sqlDouble5 += this._values[num7];
							sqlDouble6 += this._values[num7] * this._values[num7];
							num6++;
						}
					}
					if (num6 <= 1)
					{
						return this._nullValue;
					}
					sqlDouble4 = (double)num6 * sqlDouble6 - sqlDouble5 * sqlDouble5;
					SqlBoolean sqlBoolean = sqlDouble4 / (sqlDouble5 * sqlDouble5) < 1E-15;
					if (sqlBoolean ? sqlBoolean : (sqlBoolean | sqlDouble4 < 0.0))
					{
						sqlDouble4 = 0.0;
					}
					else
					{
						sqlDouble4 /= (double)(num6 * (num6 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(sqlDouble4.Value);
					}
					return sqlDouble4;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlDouble));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x000C4370 File Offset: 0x000C2570
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x000C438F File Offset: 0x000C258F
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlDouble)value);
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000C43A8 File Offset: 0x000C25A8
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlDouble(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x000C43BF File Offset: 0x000C25BF
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x000C43D9 File Offset: 0x000C25D9
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000C43EC File Offset: 0x000C25EC
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x000C43FF File Offset: 0x000C25FF
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlDouble(value);
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x000C4414 File Offset: 0x000C2614
		public override void SetCapacity(int capacity)
		{
			SqlDouble[] array = new SqlDouble[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x000C4454 File Offset: 0x000C2654
		public override object ConvertXmlToObject(string s)
		{
			SqlDouble sqlDouble = default(SqlDouble);
			TextReader input = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlDouble;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlDouble)xmlSerializable;
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x000C44BC File Offset: 0x000C26BC
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x000C450C File Offset: 0x000C270C
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlDouble[recordCount];
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x000C4514 File Offset: 0x000C2714
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlDouble[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x000C453E File Offset: 0x000C273E
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlDouble[])store;
		}

		// Token: 0x04001BB2 RID: 7090
		private SqlDouble[] _values;
	}
}
