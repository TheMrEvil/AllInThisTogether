using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x020003C4 RID: 964
	internal sealed class UInt64Storage : DataStorage
	{
		// Token: 0x06002ED5 RID: 11989 RVA: 0x000C8D3A File Offset: 0x000C6F3A
		public UInt64Storage(DataColumn column) : base(column, typeof(ulong), UInt64Storage.s_defaultValue, StorageType.UInt64)
		{
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000C8D5C File Offset: 0x000C6F5C
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					ulong num = UInt64Storage.s_defaultValue;
					checked
					{
						foreach (int num2 in records)
						{
							if (base.HasValue(num2))
							{
								num += this._values[num2];
								flag = true;
							}
						}
						if (flag)
						{
							return num;
						}
						return this._nullValue;
					}
				}
				case AggregateType.Mean:
				{
					decimal d = UInt64Storage.s_defaultValue;
					int num3 = 0;
					foreach (int num4 in records)
					{
						if (base.HasValue(num4))
						{
							d += this._values[num4];
							num3++;
							flag = true;
						}
					}
					if (flag)
					{
						return (ulong)(d / num3);
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					ulong num5 = ulong.MaxValue;
					foreach (int num6 in records)
					{
						if (base.HasValue(num6))
						{
							num5 = Math.Min(this._values[num6], num5);
							flag = true;
						}
					}
					if (flag)
					{
						return num5;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					ulong num7 = 0UL;
					foreach (int num8 in records)
					{
						if (base.HasValue(num8))
						{
							num7 = Math.Max(this._values[num8], num7);
							flag = true;
						}
					}
					if (flag)
					{
						return num7;
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
				case AggregateType.Var:
				case AggregateType.StDev:
				{
					int num9 = 0;
					double num10 = 0.0;
					double num11 = 0.0;
					foreach (int num12 in records)
					{
						if (base.HasValue(num12))
						{
							num10 += this._values[num12];
							num11 += this._values[num12] * this._values[num12];
							num9++;
						}
					}
					if (num9 <= 1)
					{
						return this._nullValue;
					}
					double num13 = (double)num9 * num11 - num10 * num10;
					if (num13 / (num10 * num10) < 1E-15 || num13 < 0.0)
					{
						num13 = 0.0;
					}
					else
					{
						num13 /= (double)(num9 * (num9 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(num13);
					}
					return num13;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(ulong));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x000C907C File Offset: 0x000C727C
		public override int Compare(int recordNo1, int recordNo2)
		{
			ulong num = this._values[recordNo1];
			ulong num2 = this._values[recordNo2];
			if (num.Equals(UInt64Storage.s_defaultValue) || num2.Equals(UInt64Storage.s_defaultValue))
			{
				int num3 = base.CompareBits(recordNo1, recordNo2);
				if (num3 != 0)
				{
					return num3;
				}
			}
			if (num < num2)
			{
				return -1;
			}
			if (num <= num2)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x000C90D4 File Offset: 0x000C72D4
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
				ulong num = this._values[recordNo];
				if (UInt64Storage.s_defaultValue == num && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((ulong)value);
			}
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x000C9120 File Offset: 0x000C7320
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToUInt64(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x000C9151 File Offset: 0x000C7351
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x000C916C File Offset: 0x000C736C
		public override object Get(int record)
		{
			ulong num = this._values[record];
			if (!num.Equals(UInt64Storage.s_defaultValue))
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x000C91A0 File Offset: 0x000C73A0
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = UInt64Storage.s_defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToUInt64(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x000C91F0 File Offset: 0x000C73F0
		public override void SetCapacity(int capacity)
		{
			ulong[] array = new ulong[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x000C9236 File Offset: 0x000C7436
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToUInt64(s);
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x000C9243 File Offset: 0x000C7443
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((ulong)value);
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000C9250 File Offset: 0x000C7450
		protected override object GetEmptyStorage(int recordCount)
		{
			return new ulong[recordCount];
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000C9258 File Offset: 0x000C7458
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((ulong[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000C927D File Offset: 0x000C747D
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (ulong[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001BEE RID: 7150
		private static readonly ulong s_defaultValue;

		// Token: 0x04001BEF RID: 7151
		private ulong[] _values;
	}
}
