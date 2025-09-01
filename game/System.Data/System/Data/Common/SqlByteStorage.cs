using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x020003AC RID: 940
	internal sealed class SqlByteStorage : DataStorage
	{
		// Token: 0x06002DA2 RID: 11682 RVA: 0x000C2AA8 File Offset: 0x000C0CA8
		public SqlByteStorage(DataColumn column) : base(column, typeof(SqlByte), SqlByte.Null, SqlByte.Null, StorageType.SqlByte)
		{
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000C2AD4 File Offset: 0x000C0CD4
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
						return (x / (long)num2).ToSqlByte();
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					SqlByte sqlByte = SqlByte.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlByte.LessThan(this._values[num4], sqlByte).IsTrue)
							{
								sqlByte = this._values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlByte;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlByte sqlByte2 = SqlByte.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlByte.GreaterThan(this._values[num5], sqlByte2).IsTrue)
							{
								sqlByte2 = this._values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlByte2;
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
				throw ExprException.Overflow(typeof(SqlByte));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000C2F10 File Offset: 0x000C1110
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000C2F2F File Offset: 0x000C112F
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlByte)value);
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x000C2F48 File Offset: 0x000C1148
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlByte(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x000C2F5F File Offset: 0x000C115F
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000C2F79 File Offset: 0x000C1179
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000C2F8C File Offset: 0x000C118C
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000C2F9F File Offset: 0x000C119F
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlByte(value);
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000C2FB4 File Offset: 0x000C11B4
		public override void SetCapacity(int capacity)
		{
			SqlByte[] array = new SqlByte[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x000C2FF4 File Offset: 0x000C11F4
		public override object ConvertXmlToObject(string s)
		{
			SqlByte sqlByte = default(SqlByte);
			TextReader input = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlByte;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlByte)xmlSerializable;
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x000C305C File Offset: 0x000C125C
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x000C30AC File Offset: 0x000C12AC
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlByte[recordCount];
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x000C30B4 File Offset: 0x000C12B4
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlByte[])store)[storeIndex] = this._values[record];
			nullbits.Set(record, this.IsNull(record));
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x000C30DD File Offset: 0x000C12DD
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlByte[])store;
		}

		// Token: 0x04001BAD RID: 7085
		private SqlByte[] _values;
	}
}
