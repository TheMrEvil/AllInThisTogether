using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x020003AE RID: 942
	internal sealed class SqlCharsStorage : DataStorage
	{
		// Token: 0x06002DBF RID: 11711 RVA: 0x000C3318 File Offset: 0x000C1518
		public SqlCharsStorage(DataColumn column) : base(column, typeof(SqlChars), SqlChars.Null, SqlChars.Null, StorageType.SqlChars)
		{
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x000C3338 File Offset: 0x000C1538
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
				throw ExprException.Overflow(typeof(SqlChars));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x00006D64 File Offset: 0x00004F64
		public override int Compare(int recordNo1, int recordNo2)
		{
			return 0;
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x00006D64 File Offset: 0x00004F64
		public override int CompareValueTo(int recordNo, object value)
		{
			return 0;
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x000C33C0 File Offset: 0x000C15C0
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x000C33D2 File Offset: 0x000C15D2
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x000C33DC File Offset: 0x000C15DC
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x000C33EB File Offset: 0x000C15EB
		public override void Set(int record, object value)
		{
			if (value == DBNull.Value || value == null)
			{
				this._values[record] = SqlChars.Null;
				return;
			}
			this._values[record] = (SqlChars)value;
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000C3414 File Offset: 0x000C1614
		public override void SetCapacity(int capacity)
		{
			SqlChars[] array = new SqlChars[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x000C3454 File Offset: 0x000C1654
		public override object ConvertXmlToObject(string s)
		{
			SqlString sqlString = default(SqlString);
			TextReader input = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlString;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return new SqlChars((SqlString)xmlSerializable);
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x000C34BC File Offset: 0x000C16BC
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000C350C File Offset: 0x000C170C
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlChars[recordCount];
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x000C3514 File Offset: 0x000C1714
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlChars[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000C3536 File Offset: 0x000C1736
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlChars[])store;
		}

		// Token: 0x04001BAF RID: 7087
		private SqlChars[] _values;
	}
}
