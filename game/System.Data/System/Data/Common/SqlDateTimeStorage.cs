using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x020003AF RID: 943
	internal sealed class SqlDateTimeStorage : DataStorage
	{
		// Token: 0x06002DCD RID: 11725 RVA: 0x000C3544 File Offset: 0x000C1744
		public SqlDateTimeStorage(DataColumn column) : base(column, typeof(SqlDateTime), SqlDateTime.Null, SqlDateTime.Null, StorageType.SqlDateTime)
		{
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x000C3570 File Offset: 0x000C1770
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					SqlDateTime sqlDateTime = SqlDateTime.MaxValue;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							if (SqlDateTime.LessThan(this._values[num], sqlDateTime).IsTrue)
							{
								sqlDateTime = this._values[num];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDateTime;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlDateTime sqlDateTime2 = SqlDateTime.MinValue;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							if (SqlDateTime.GreaterThan(this._values[num2], sqlDateTime2).IsTrue)
							{
								sqlDateTime2 = this._values[num2];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDateTime2;
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
				throw ExprException.Overflow(typeof(SqlDateTime));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x000C3708 File Offset: 0x000C1908
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x000C3727 File Offset: 0x000C1927
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlDateTime)value);
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x000C3740 File Offset: 0x000C1940
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlDateTime(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x000C3757 File Offset: 0x000C1957
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x000C3771 File Offset: 0x000C1971
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x000C3784 File Offset: 0x000C1984
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x000C3797 File Offset: 0x000C1997
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlDateTime(value);
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x000C37AC File Offset: 0x000C19AC
		public override void SetCapacity(int capacity)
		{
			SqlDateTime[] array = new SqlDateTime[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x000C37EC File Offset: 0x000C19EC
		public override object ConvertXmlToObject(string s)
		{
			SqlDateTime sqlDateTime = default(SqlDateTime);
			TextReader input = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlDateTime;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlDateTime)xmlSerializable;
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000C3854 File Offset: 0x000C1A54
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000C38A4 File Offset: 0x000C1AA4
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlDateTime[recordCount];
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x000C38AC File Offset: 0x000C1AAC
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlDateTime[])store)[storeIndex] = this._values[record];
			nullbits.Set(record, this.IsNull(record));
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x000C38D5 File Offset: 0x000C1AD5
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlDateTime[])store;
		}

		// Token: 0x04001BB0 RID: 7088
		private SqlDateTime[] _values;
	}
}
