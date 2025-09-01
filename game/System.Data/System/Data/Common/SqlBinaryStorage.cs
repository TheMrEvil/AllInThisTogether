using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x020003AB RID: 939
	internal sealed class SqlBinaryStorage : DataStorage
	{
		// Token: 0x06002D93 RID: 11667 RVA: 0x000C2813 File Offset: 0x000C0A13
		public SqlBinaryStorage(DataColumn column) : base(column, typeof(SqlBinary), SqlBinary.Null, SqlBinary.Null, StorageType.SqlBinary)
		{
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x000C283C File Offset: 0x000C0A3C
		public override object Aggregate(int[] records, AggregateType kind)
		{
			try
			{
				if (kind != AggregateType.First)
				{
					if (kind == AggregateType.Count)
					{
						int num = 0;
						for (int i = 0; i < records.Length; i++)
						{
							if (!this.IsNull(records[i]))
							{
								num++;
							}
						}
						return num;
					}
				}
				else
				{
					if (records.Length != 0)
					{
						return this._values[records[0]];
					}
					return null;
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlBinary));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x000C28CC File Offset: 0x000C0ACC
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x000C28EB File Offset: 0x000C0AEB
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlBinary)value);
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x000C2904 File Offset: 0x000C0B04
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlBinary(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x000C291B File Offset: 0x000C0B1B
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x000C2935 File Offset: 0x000C0B35
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x000C2948 File Offset: 0x000C0B48
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x000C295B File Offset: 0x000C0B5B
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlBinary(value);
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x000C2970 File Offset: 0x000C0B70
		public override void SetCapacity(int capacity)
		{
			SqlBinary[] array = new SqlBinary[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x000C29B0 File Offset: 0x000C0BB0
		public override object ConvertXmlToObject(string s)
		{
			SqlBinary sqlBinary = default(SqlBinary);
			TextReader input = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlBinary;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlBinary)xmlSerializable;
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x000C2A18 File Offset: 0x000C0C18
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x000C2A68 File Offset: 0x000C0C68
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlBinary[recordCount];
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x000C2A70 File Offset: 0x000C0C70
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlBinary[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x000C2A9A File Offset: 0x000C0C9A
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlBinary[])store;
		}

		// Token: 0x04001BAC RID: 7084
		private SqlBinary[] _values;
	}
}
