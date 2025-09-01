using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000388 RID: 904
	internal sealed class DateTimeOffsetStorage : DataStorage
	{
		// Token: 0x06002B02 RID: 11010 RVA: 0x000BA9B5 File Offset: 0x000B8BB5
		internal DateTimeOffsetStorage(DataColumn column) : base(column, typeof(DateTimeOffset), DateTimeOffsetStorage.s_defaultValue, StorageType.DateTimeOffset)
		{
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000BA9D4 File Offset: 0x000B8BD4
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					DateTimeOffset dateTimeOffset = DateTimeOffset.MaxValue;
					foreach (int num in records)
					{
						if (base.HasValue(num))
						{
							dateTimeOffset = ((DateTimeOffset.Compare(this._values[num], dateTimeOffset) < 0) ? this._values[num] : dateTimeOffset);
							flag = true;
						}
					}
					if (flag)
					{
						return dateTimeOffset;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					DateTimeOffset dateTimeOffset2 = DateTimeOffset.MinValue;
					foreach (int num2 in records)
					{
						if (base.HasValue(num2))
						{
							dateTimeOffset2 = ((DateTimeOffset.Compare(this._values[num2], dateTimeOffset2) >= 0) ? this._values[num2] : dateTimeOffset2);
							flag = true;
						}
					}
					if (flag)
					{
						return dateTimeOffset2;
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
				throw ExprException.Overflow(typeof(DateTimeOffset));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x000BAB64 File Offset: 0x000B8D64
		public override int Compare(int recordNo1, int recordNo2)
		{
			DateTimeOffset dateTimeOffset = this._values[recordNo1];
			DateTimeOffset dateTimeOffset2 = this._values[recordNo2];
			if (dateTimeOffset == DateTimeOffsetStorage.s_defaultValue || dateTimeOffset2 == DateTimeOffsetStorage.s_defaultValue)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return DateTimeOffset.Compare(dateTimeOffset, dateTimeOffset2);
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000BABBC File Offset: 0x000B8DBC
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
				DateTimeOffset dateTimeOffset = this._values[recordNo];
				if (DateTimeOffsetStorage.s_defaultValue == dateTimeOffset && !base.HasValue(recordNo))
				{
					return -1;
				}
				return DateTimeOffset.Compare(dateTimeOffset, (DateTimeOffset)value);
			}
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x000BAC10 File Offset: 0x000B8E10
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = (DateTimeOffset)value;
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x000BAC36 File Offset: 0x000B8E36
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000BAC58 File Offset: 0x000B8E58
		public override object Get(int record)
		{
			DateTimeOffset dateTimeOffset = this._values[record];
			if (dateTimeOffset != DateTimeOffsetStorage.s_defaultValue || base.HasValue(record))
			{
				return dateTimeOffset;
			}
			return this._nullValue;
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000BAC95 File Offset: 0x000B8E95
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = DateTimeOffsetStorage.s_defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = (DateTimeOffset)value;
			base.SetNullBit(record, false);
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x000BACD4 File Offset: 0x000B8ED4
		public override void SetCapacity(int capacity)
		{
			DateTimeOffset[] array = new DateTimeOffset[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x000BAD1A File Offset: 0x000B8F1A
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToDateTimeOffset(s);
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x000BAD27 File Offset: 0x000B8F27
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((DateTimeOffset)value);
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000BAD34 File Offset: 0x000B8F34
		protected override object GetEmptyStorage(int recordCount)
		{
			return new DateTimeOffset[recordCount];
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000BAD3C File Offset: 0x000B8F3C
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((DateTimeOffset[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000BAD69 File Offset: 0x000B8F69
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (DateTimeOffset[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000BAD7E File Offset: 0x000B8F7E
		// Note: this type is marked as 'beforefieldinit'.
		static DateTimeOffsetStorage()
		{
		}

		// Token: 0x04001B16 RID: 6934
		private static readonly DateTimeOffset s_defaultValue = DateTimeOffset.MinValue;

		// Token: 0x04001B17 RID: 6935
		private DateTimeOffset[] _values;
	}
}
