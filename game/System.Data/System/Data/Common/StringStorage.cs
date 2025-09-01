using System;
using System.Collections;

namespace System.Data.Common
{
	// Token: 0x020003BF RID: 959
	internal sealed class StringStorage : DataStorage
	{
		// Token: 0x06002E99 RID: 11929 RVA: 0x000C7962 File Offset: 0x000C5B62
		public StringStorage(DataColumn column) : base(column, typeof(string), string.Empty, StorageType.String)
		{
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000C797C File Offset: 0x000C5B7C
		public override object Aggregate(int[] recordNos, AggregateType kind)
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
					if (this._values[recordNos[i]] != null)
					{
						num3++;
					}
				}
				return num3;
			}
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x000C7A90 File Offset: 0x000C5C90
		public override int Compare(int recordNo1, int recordNo2)
		{
			string text = this._values[recordNo1];
			string text2 = this._values[recordNo2];
			if (text == text2)
			{
				return 0;
			}
			if (text == null)
			{
				return -1;
			}
			if (text2 == null)
			{
				return 1;
			}
			return this._table.Compare(text, text2);
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x000C7ACC File Offset: 0x000C5CCC
		public override int CompareValueTo(int recordNo, object value)
		{
			string text = this._values[recordNo];
			if (text == null)
			{
				if (this._nullValue == value)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (this._nullValue == value)
				{
					return 1;
				}
				return this._table.Compare(text, (string)value);
			}
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x000C7B0F File Offset: 0x000C5D0F
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = value.ToString();
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x000C7B30 File Offset: 0x000C5D30
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x000C7B44 File Offset: 0x000C5D44
		public override object Get(int recordNo)
		{
			string text = this._values[recordNo];
			if (text != null)
			{
				return text;
			}
			return this._nullValue;
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x000C7B68 File Offset: 0x000C5D68
		public override int GetStringLength(int record)
		{
			string text = this._values[record];
			if (text == null)
			{
				return 0;
			}
			return text.Length;
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x000C7B89 File Offset: 0x000C5D89
		public override bool IsNull(int record)
		{
			return this._values[record] == null;
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x000C7B96 File Offset: 0x000C5D96
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = null;
				return;
			}
			this._values[record] = value.ToString();
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x000C7BBC File Offset: 0x000C5DBC
		public override void SetCapacity(int capacity)
		{
			string[] array = new string[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000056BA File Offset: 0x000038BA
		public override object ConvertXmlToObject(string s)
		{
			return s;
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x000C7BFB File Offset: 0x000C5DFB
		public override string ConvertObjectToXml(object value)
		{
			return (string)value;
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x000C7C03 File Offset: 0x000C5E03
		protected override object GetEmptyStorage(int recordCount)
		{
			return new string[recordCount];
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x000C7C0B File Offset: 0x000C5E0B
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((string[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x000C7C2D File Offset: 0x000C5E2D
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (string[])store;
		}

		// Token: 0x04001BE1 RID: 7137
		private string[] _values;
	}
}
