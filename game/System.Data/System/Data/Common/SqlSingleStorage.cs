using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x020003B7 RID: 951
	internal sealed class SqlSingleStorage : DataStorage
	{
		// Token: 0x06002E45 RID: 11845 RVA: 0x000C60F4 File Offset: 0x000C42F4
		public SqlSingleStorage(DataColumn column) : base(column, typeof(SqlSingle), SqlSingle.Null, SqlSingle.Null, StorageType.SqlSingle)
		{
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x000C6120 File Offset: 0x000C4320
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					SqlSingle sqlSingle = 0f;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlSingle += this._values[num];
							flag = true;
						}
					}
					if (flag)
					{
						return sqlSingle;
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
							x += this._values[num3].ToSqlDouble();
							num2++;
							flag = true;
						}
					}
					if (flag)
					{
						0f;
						return (x / (double)num2).ToSqlSingle();
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					SqlSingle sqlSingle2 = SqlSingle.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlSingle.LessThan(this._values[num4], sqlSingle2).IsTrue)
							{
								sqlSingle2 = this._values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlSingle2;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlSingle sqlSingle3 = SqlSingle.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlSingle.GreaterThan(this._values[num5], sqlSingle3).IsTrue)
							{
								sqlSingle3 = this._values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlSingle3;
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
				throw ExprException.Overflow(typeof(SqlSingle));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x000C6568 File Offset: 0x000C4768
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x000C6587 File Offset: 0x000C4787
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlSingle)value);
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x000C65A0 File Offset: 0x000C47A0
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlSingle(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x000C65B7 File Offset: 0x000C47B7
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x000C65D1 File Offset: 0x000C47D1
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x000C65E4 File Offset: 0x000C47E4
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x000C65F7 File Offset: 0x000C47F7
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlSingle(value);
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x000C660C File Offset: 0x000C480C
		public override void SetCapacity(int capacity)
		{
			SqlSingle[] array = new SqlSingle[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x000C664C File Offset: 0x000C484C
		public override object ConvertXmlToObject(string s)
		{
			SqlSingle sqlSingle = default(SqlSingle);
			TextReader input = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlSingle;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlSingle)xmlSerializable;
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x000C66B4 File Offset: 0x000C48B4
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x000C6704 File Offset: 0x000C4904
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlSingle[recordCount];
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x000C670C File Offset: 0x000C490C
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlSingle[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x000C6736 File Offset: 0x000C4936
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlSingle[])store;
		}

		// Token: 0x04001BB8 RID: 7096
		private SqlSingle[] _values;
	}
}
