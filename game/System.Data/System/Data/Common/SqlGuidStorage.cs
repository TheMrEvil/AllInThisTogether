using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x020003B2 RID: 946
	internal sealed class SqlGuidStorage : DataStorage
	{
		// Token: 0x06002DFA RID: 11770 RVA: 0x000C454C File Offset: 0x000C274C
		public SqlGuidStorage(DataColumn column) : base(column, typeof(SqlGuid), SqlGuid.Null, SqlGuid.Null, StorageType.SqlGuid)
		{
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x000C4578 File Offset: 0x000C2778
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
				throw ExprException.Overflow(typeof(SqlGuid));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x000C4608 File Offset: 0x000C2808
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x000C4627 File Offset: 0x000C2827
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlGuid)value);
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x000C4640 File Offset: 0x000C2840
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlGuid(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x000C4657 File Offset: 0x000C2857
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x000C4671 File Offset: 0x000C2871
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x000C4684 File Offset: 0x000C2884
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x000C4697 File Offset: 0x000C2897
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlGuid(value);
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000C46AC File Offset: 0x000C28AC
		public override void SetCapacity(int capacity)
		{
			SqlGuid[] array = new SqlGuid[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x000C46EC File Offset: 0x000C28EC
		public override object ConvertXmlToObject(string s)
		{
			SqlGuid sqlGuid = default(SqlGuid);
			TextReader input = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlGuid;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlGuid)xmlSerializable;
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x000C4754 File Offset: 0x000C2954
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x000C47A4 File Offset: 0x000C29A4
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlGuid[recordCount];
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x000C47AC File Offset: 0x000C29AC
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlGuid[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x000C47D6 File Offset: 0x000C29D6
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlGuid[])store;
		}

		// Token: 0x04001BB3 RID: 7091
		private SqlGuid[] _values;
	}
}
