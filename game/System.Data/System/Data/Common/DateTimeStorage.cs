using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000389 RID: 905
	internal sealed class DateTimeStorage : DataStorage
	{
		// Token: 0x06002B11 RID: 11025 RVA: 0x000BAD8A File Offset: 0x000B8F8A
		internal DateTimeStorage(DataColumn column) : base(column, typeof(DateTime), DateTimeStorage.s_defaultValue, StorageType.DateTime)
		{
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000BADAC File Offset: 0x000B8FAC
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					DateTime dateTime = DateTime.MaxValue;
					foreach (int num in records)
					{
						if (base.HasValue(num))
						{
							dateTime = ((DateTime.Compare(this._values[num], dateTime) < 0) ? this._values[num] : dateTime);
							flag = true;
						}
					}
					if (flag)
					{
						return dateTime;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					DateTime dateTime2 = DateTime.MinValue;
					foreach (int num2 in records)
					{
						if (base.HasValue(num2))
						{
							dateTime2 = ((DateTime.Compare(this._values[num2], dateTime2) >= 0) ? this._values[num2] : dateTime2);
							flag = true;
						}
					}
					if (flag)
					{
						return dateTime2;
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
						if (base.HasValue(records[k]))
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
				throw ExprException.Overflow(typeof(DateTime));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000BAF3C File Offset: 0x000B913C
		public override int Compare(int recordNo1, int recordNo2)
		{
			DateTime dateTime = this._values[recordNo1];
			DateTime dateTime2 = this._values[recordNo2];
			if (dateTime == DateTimeStorage.s_defaultValue || dateTime2 == DateTimeStorage.s_defaultValue)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return DateTime.Compare(dateTime, dateTime2);
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000BAF94 File Offset: 0x000B9194
		public override int CompareValueTo(int recordNo, object value)
		{
			if (this._nullValue == value)
			{
				if (!base.HasValue(recordNo))
				{
					return 0;
				}
				return 1;
			}
			else
			{
				DateTime dateTime = this._values[recordNo];
				if (DateTimeStorage.s_defaultValue == dateTime && !base.HasValue(recordNo))
				{
					return -1;
				}
				return DateTime.Compare(dateTime, (DateTime)value);
			}
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000BAFE8 File Offset: 0x000B91E8
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToDateTime(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x000BB019 File Offset: 0x000B9219
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000BB03C File Offset: 0x000B923C
		public override object Get(int record)
		{
			DateTime dateTime = this._values[record];
			if (dateTime != DateTimeStorage.s_defaultValue || base.HasValue(record))
			{
				return dateTime;
			}
			return this._nullValue;
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000BB07C File Offset: 0x000B927C
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = DateTimeStorage.s_defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			DateTime dateTime = ((IConvertible)value).ToDateTime(base.FormatProvider);
			DateTime dateTime2;
			switch (base.DateTimeMode)
			{
			case DataSetDateTime.Local:
				if (dateTime.Kind == DateTimeKind.Local)
				{
					dateTime2 = dateTime;
				}
				else if (dateTime.Kind == DateTimeKind.Utc)
				{
					dateTime2 = dateTime.ToLocalTime();
				}
				else
				{
					dateTime2 = DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
				}
				break;
			case DataSetDateTime.Unspecified:
			case DataSetDateTime.UnspecifiedLocal:
				dateTime2 = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
				break;
			case DataSetDateTime.Utc:
				if (dateTime.Kind == DateTimeKind.Utc)
				{
					dateTime2 = dateTime;
				}
				else if (dateTime.Kind == DateTimeKind.Local)
				{
					dateTime2 = dateTime.ToUniversalTime();
				}
				else
				{
					dateTime2 = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
				}
				break;
			default:
				throw ExceptionBuilder.InvalidDateTimeMode(base.DateTimeMode);
			}
			this._values[record] = dateTime2;
			base.SetNullBit(record, false);
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x000BB164 File Offset: 0x000B9364
		public override void SetCapacity(int capacity)
		{
			DateTime[] array = new DateTime[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x000BB1AC File Offset: 0x000B93AC
		public override object ConvertXmlToObject(string s)
		{
			object result;
			if (base.DateTimeMode == DataSetDateTime.UnspecifiedLocal)
			{
				result = XmlConvert.ToDateTime(s, XmlDateTimeSerializationMode.Unspecified);
			}
			else
			{
				result = XmlConvert.ToDateTime(s, XmlDateTimeSerializationMode.RoundtripKind);
			}
			return result;
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000BB1E0 File Offset: 0x000B93E0
		public override string ConvertObjectToXml(object value)
		{
			string result;
			if (base.DateTimeMode == DataSetDateTime.UnspecifiedLocal)
			{
				result = XmlConvert.ToString((DateTime)value, XmlDateTimeSerializationMode.Local);
			}
			else
			{
				result = XmlConvert.ToString((DateTime)value, XmlDateTimeSerializationMode.RoundtripKind);
			}
			return result;
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000BB213 File Offset: 0x000B9413
		protected override object GetEmptyStorage(int recordCount)
		{
			return new DateTime[recordCount];
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000BB21C File Offset: 0x000B941C
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			DateTime[] array = (DateTime[])store;
			bool flag = !base.HasValue(record);
			if (flag || (base.DateTimeMode & DataSetDateTime.Local) == (DataSetDateTime)0)
			{
				array[storeIndex] = this._values[record];
			}
			else
			{
				array[storeIndex] = this._values[record].ToUniversalTime();
			}
			nullbits.Set(storeIndex, flag);
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x000BB280 File Offset: 0x000B9480
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (DateTime[])store;
			base.SetNullStorage(nullbits);
			if (base.DateTimeMode == DataSetDateTime.UnspecifiedLocal)
			{
				for (int i = 0; i < this._values.Length; i++)
				{
					if (base.HasValue(i))
					{
						this._values[i] = DateTime.SpecifyKind(this._values[i].ToLocalTime(), DateTimeKind.Unspecified);
					}
				}
				return;
			}
			if (base.DateTimeMode == DataSetDateTime.Local)
			{
				for (int j = 0; j < this._values.Length; j++)
				{
					if (base.HasValue(j))
					{
						this._values[j] = this._values[j].ToLocalTime();
					}
				}
			}
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x000BB32B File Offset: 0x000B952B
		// Note: this type is marked as 'beforefieldinit'.
		static DateTimeStorage()
		{
		}

		// Token: 0x04001B18 RID: 6936
		private static readonly DateTime s_defaultValue = DateTime.MinValue;

		// Token: 0x04001B19 RID: 6937
		private DateTime[] _values;
	}
}
