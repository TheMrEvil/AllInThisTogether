using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x020003A0 RID: 928
	internal sealed class DecimalStorage : DataStorage
	{
		// Token: 0x06002D0F RID: 11535 RVA: 0x000BF09C File Offset: 0x000BD29C
		internal DecimalStorage(DataColumn column) : base(column, typeof(decimal), DecimalStorage.s_defaultValue, StorageType.Decimal)
		{
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000BF0BC File Offset: 0x000BD2BC
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					decimal num = DecimalStorage.s_defaultValue;
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
				case AggregateType.Mean:
				{
					decimal d = DecimalStorage.s_defaultValue;
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
						return d / num3;
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					decimal num5 = decimal.MaxValue;
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
					decimal num7 = decimal.MinValue;
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
					double num10 = (double)DecimalStorage.s_defaultValue;
					(double)DecimalStorage.s_defaultValue;
					double num11 = (double)DecimalStorage.s_defaultValue;
					double num12 = (double)DecimalStorage.s_defaultValue;
					foreach (int num13 in records)
					{
						if (base.HasValue(num13))
						{
							num11 += (double)this._values[num13];
							num12 += (double)this._values[num13] * (double)this._values[num13];
							num9++;
						}
					}
					if (num9 <= 1)
					{
						return this._nullValue;
					}
					num10 = (double)num9 * num12 - num11 * num11;
					if (num10 / (num11 * num11) < 1E-15 || num10 < 0.0)
					{
						num10 = 0.0;
					}
					else
					{
						num10 /= (double)(num9 * (num9 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(num10);
					}
					return num10;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(decimal));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000BF41C File Offset: 0x000BD61C
		public override int Compare(int recordNo1, int recordNo2)
		{
			decimal d = this._values[recordNo1];
			decimal num = this._values[recordNo2];
			if (d == DecimalStorage.s_defaultValue || num == DecimalStorage.s_defaultValue)
			{
				int num2 = base.CompareBits(recordNo1, recordNo2);
				if (num2 != 0)
				{
					return num2;
				}
			}
			return decimal.Compare(d, num);
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000BF474 File Offset: 0x000BD674
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
				decimal num = this._values[recordNo];
				if (DecimalStorage.s_defaultValue == num && !base.HasValue(recordNo))
				{
					return -1;
				}
				return decimal.Compare(num, (decimal)value);
			}
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x000BF4C8 File Offset: 0x000BD6C8
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToDecimal(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000BF4F9 File Offset: 0x000BD6F9
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000BF51B File Offset: 0x000BD71B
		public override object Get(int record)
		{
			if (!base.HasValue(record))
			{
				return this._nullValue;
			}
			return this._values[record];
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000BF540 File Offset: 0x000BD740
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = DecimalStorage.s_defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToDecimal(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000BF598 File Offset: 0x000BD798
		public override void SetCapacity(int capacity)
		{
			decimal[] array = new decimal[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000BF5DE File Offset: 0x000BD7DE
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToDecimal(s);
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x000BF5EB File Offset: 0x000BD7EB
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((decimal)value);
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x000BF5F8 File Offset: 0x000BD7F8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new decimal[recordCount];
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x000BF600 File Offset: 0x000BD800
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((decimal[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x000BF62D File Offset: 0x000BD82D
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (decimal[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001B8C RID: 7052
		private static readonly decimal s_defaultValue;

		// Token: 0x04001B8D RID: 7053
		private decimal[] _values;
	}
}
