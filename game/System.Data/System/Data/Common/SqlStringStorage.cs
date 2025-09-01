using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x020003B8 RID: 952
	internal sealed class SqlStringStorage : DataStorage
	{
		// Token: 0x06002E54 RID: 11860 RVA: 0x000C6744 File Offset: 0x000C4944
		public SqlStringStorage(DataColumn column) : base(column, typeof(SqlString), SqlString.Null, SqlString.Null, StorageType.SqlString)
		{
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x000C6770 File Offset: 0x000C4970
		public override object Aggregate(int[] recordNos, AggregateType kind)
		{
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					int num = -1;
					int i;
					for (i = 0; i < recordNos.Length; i++)
					{
						if (!this.IsNull(recordNos[i]))
						{
							num = recordNos[i];
							break;
						}
					}
					if (num >= 0)
					{
						for (i++; i < recordNos.Length; i++)
						{
							if (!this.IsNull(recordNos[i]) && this.Compare(num, recordNos[i]) > 0)
							{
								num = recordNos[i];
							}
						}
						return this.Get(num);
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					int num2 = -1;
					int i;
					for (i = 0; i < recordNos.Length; i++)
					{
						if (!this.IsNull(recordNos[i]))
						{
							num2 = recordNos[i];
							break;
						}
					}
					if (num2 >= 0)
					{
						for (i++; i < recordNos.Length; i++)
						{
							if (this.Compare(num2, recordNos[i]) < 0)
							{
								num2 = recordNos[i];
							}
						}
						return this.Get(num2);
					}
					return this._nullValue;
				}
				case AggregateType.Count:
				{
					int num3 = 0;
					for (int i = 0; i < recordNos.Length; i++)
					{
						if (!this.IsNull(recordNos[i]))
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
				throw ExprException.Overflow(typeof(SqlString));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x000C68C8 File Offset: 0x000C4AC8
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.Compare(this._values[recordNo1], this._values[recordNo2]);
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x000C68E8 File Offset: 0x000C4AE8
		public int Compare(SqlString valueNo1, SqlString valueNo2)
		{
			if (valueNo1.IsNull && valueNo2.IsNull)
			{
				return 0;
			}
			if (valueNo1.IsNull)
			{
				return -1;
			}
			if (valueNo2.IsNull)
			{
				return 1;
			}
			return this._table.Compare(valueNo1.Value, valueNo2.Value);
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x000C6938 File Offset: 0x000C4B38
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.Compare(this._values[recordNo], (SqlString)value);
		}

		// Token: 0x06002E59 RID: 11865 RVA: 0x000C6952 File Offset: 0x000C4B52
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlString(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x000C6969 File Offset: 0x000C4B69
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x000C6983 File Offset: 0x000C4B83
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x000C6998 File Offset: 0x000C4B98
		public override int GetStringLength(int record)
		{
			SqlString sqlString = this._values[record];
			if (!sqlString.IsNull)
			{
				return sqlString.Value.Length;
			}
			return 0;
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x000C69C9 File Offset: 0x000C4BC9
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x000C69DC File Offset: 0x000C4BDC
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlString(value);
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x000C69F0 File Offset: 0x000C4BF0
		public override void SetCapacity(int capacity)
		{
			SqlString[] array = new SqlString[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x000C6A30 File Offset: 0x000C4C30
		public override object ConvertXmlToObject(string s)
		{
			SqlString sqlString = default(SqlString);
			TextReader input = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlString;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlString)xmlSerializable;
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x000C6A98 File Offset: 0x000C4C98
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x000C6AE8 File Offset: 0x000C4CE8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlString[recordCount];
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x000C6AF0 File Offset: 0x000C4CF0
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlString[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x000C6B1A File Offset: 0x000C4D1A
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlString[])store;
		}

		// Token: 0x04001BB9 RID: 7097
		private SqlString[] _values;
	}
}
