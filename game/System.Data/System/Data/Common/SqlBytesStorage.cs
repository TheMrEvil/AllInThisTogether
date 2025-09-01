using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x020003AD RID: 941
	internal sealed class SqlBytesStorage : DataStorage
	{
		// Token: 0x06002DB1 RID: 11697 RVA: 0x000C30EB File Offset: 0x000C12EB
		public SqlBytesStorage(DataColumn column) : base(column, typeof(SqlBytes), SqlBytes.Null, SqlBytes.Null, StorageType.SqlBytes)
		{
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x000C310C File Offset: 0x000C130C
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
				throw ExprException.Overflow(typeof(SqlBytes));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x00006D64 File Offset: 0x00004F64
		public override int Compare(int recordNo1, int recordNo2)
		{
			return 0;
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x00006D64 File Offset: 0x00004F64
		public override int CompareValueTo(int recordNo, object value)
		{
			return 0;
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x000C3194 File Offset: 0x000C1394
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x000C31A6 File Offset: 0x000C13A6
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x000C31B0 File Offset: 0x000C13B0
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x000C31BF File Offset: 0x000C13BF
		public override void Set(int record, object value)
		{
			if (value == DBNull.Value || value == null)
			{
				this._values[record] = SqlBytes.Null;
				return;
			}
			this._values[record] = (SqlBytes)value;
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x000C31E8 File Offset: 0x000C13E8
		public override void SetCapacity(int capacity)
		{
			SqlBytes[] array = new SqlBytes[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x000C3228 File Offset: 0x000C1428
		public override object ConvertXmlToObject(string s)
		{
			SqlBinary sqlBinary = default(SqlBinary);
			TextReader input = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlBinary;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return new SqlBytes((SqlBinary)xmlSerializable);
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x000C3290 File Offset: 0x000C1490
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x000C32E0 File Offset: 0x000C14E0
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlBytes[recordCount];
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x000C32E8 File Offset: 0x000C14E8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlBytes[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x000C330A File Offset: 0x000C150A
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlBytes[])store;
		}

		// Token: 0x04001BAE RID: 7086
		private SqlBytes[] _values;
	}
}
