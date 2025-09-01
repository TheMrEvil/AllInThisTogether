using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000374 RID: 884
	internal sealed class BooleanStorage : DataStorage
	{
		// Token: 0x06002946 RID: 10566 RVA: 0x000B51DF File Offset: 0x000B33DF
		internal BooleanStorage(DataColumn column) : base(column, typeof(bool), false, StorageType.Boolean)
		{
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x000B51FC File Offset: 0x000B33FC
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					bool flag2 = true;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							flag2 = (this._values[num] && flag2);
							flag = true;
						}
					}
					if (flag)
					{
						return flag2;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					bool flag3 = false;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							flag3 = (this._values[num2] || flag3);
							flag = true;
						}
					}
					if (flag)
					{
						return flag3;
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
					return base.Aggregate(records, kind);
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(bool));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x000B5318 File Offset: 0x000B3518
		public override int Compare(int recordNo1, int recordNo2)
		{
			bool flag = this._values[recordNo1];
			bool flag2 = this._values[recordNo2];
			if (!flag || !flag2)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return flag.CompareTo(flag2);
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x000B5354 File Offset: 0x000B3554
		public override int CompareValueTo(int recordNo, object value)
		{
			if (this._nullValue == value)
			{
				if (this.IsNull(recordNo))
				{
					return 0;
				}
				return 1;
			}
			else
			{
				bool flag = this._values[recordNo];
				if (!flag && this.IsNull(recordNo))
				{
					return -1;
				}
				return flag.CompareTo((bool)value);
			}
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000B539B File Offset: 0x000B359B
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToBoolean(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000B53CC File Offset: 0x000B35CC
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x000B53E8 File Offset: 0x000B35E8
		public override object Get(int record)
		{
			bool flag = this._values[record];
			if (flag)
			{
				return flag;
			}
			return base.GetBits(record);
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x000B540F File Offset: 0x000B360F
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = false;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToBoolean(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x000B5450 File Offset: 0x000B3650
		public override void SetCapacity(int capacity)
		{
			bool[] array = new bool[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x000B5496 File Offset: 0x000B3696
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToBoolean(s);
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000B54A3 File Offset: 0x000B36A3
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((bool)value);
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x000B54B0 File Offset: 0x000B36B0
		protected override object GetEmptyStorage(int recordCount)
		{
			return new bool[recordCount];
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x000B54B8 File Offset: 0x000B36B8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((bool[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x000B54DA File Offset: 0x000B36DA
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (bool[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001A66 RID: 6758
		private const bool defaultValue = false;

		// Token: 0x04001A67 RID: 6759
		private bool[] _values;
	}
}
