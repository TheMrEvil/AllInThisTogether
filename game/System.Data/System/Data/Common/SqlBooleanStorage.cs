using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x020003B9 RID: 953
	internal sealed class SqlBooleanStorage : DataStorage
	{
		// Token: 0x06002E65 RID: 11877 RVA: 0x000C6B28 File Offset: 0x000C4D28
		public SqlBooleanStorage(DataColumn column) : base(column, typeof(SqlBoolean), SqlBoolean.Null, SqlBoolean.Null, StorageType.SqlBoolean)
		{
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x000C6B54 File Offset: 0x000C4D54
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					SqlBoolean sqlBoolean = true;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlBoolean = SqlBoolean.And(this._values[num], sqlBoolean);
							flag = true;
						}
					}
					if (flag)
					{
						return sqlBoolean;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlBoolean sqlBoolean2 = false;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							sqlBoolean2 = SqlBoolean.Or(this._values[num2], sqlBoolean2);
							flag = true;
						}
					}
					if (flag)
					{
						return sqlBoolean2;
					}
					return this._nullValue;
				}
				case AggregateType.First:
					if (records.Length != 0)
					{
						return this._values[records[0]];
					}
					return this._nullValue;
				case AggregateType.Count:
				{
					int num3 = 0;
					for (int k = 0; k < records.Length; k++)
					{
						if (!this.IsNull(records[k]))
						{
							num3++;
						}
					}
					return num3;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlBoolean));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x000C6CC4 File Offset: 0x000C4EC4
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x000C6CE3 File Offset: 0x000C4EE3
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlBoolean)value);
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x000C6CFC File Offset: 0x000C4EFC
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlBoolean(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x000C6D13 File Offset: 0x000C4F13
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x000C6D2D File Offset: 0x000C4F2D
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x000C6D40 File Offset: 0x000C4F40
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x000C6D53 File Offset: 0x000C4F53
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlBoolean(value);
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x000C6D68 File Offset: 0x000C4F68
		public override void SetCapacity(int capacity)
		{
			SqlBoolean[] array = new SqlBoolean[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x000C6DA8 File Offset: 0x000C4FA8
		public override object ConvertXmlToObject(string s)
		{
			SqlBoolean sqlBoolean = default(SqlBoolean);
			TextReader input = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlBoolean;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlBoolean)xmlSerializable;
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x000C6E10 File Offset: 0x000C5010
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x000C6E60 File Offset: 0x000C5060
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlBoolean[recordCount];
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x000C6E68 File Offset: 0x000C5068
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlBoolean[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x000C6E92 File Offset: 0x000C5092
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlBoolean[])store;
		}

		// Token: 0x04001BBA RID: 7098
		private SqlBoolean[] _values;
	}
}
