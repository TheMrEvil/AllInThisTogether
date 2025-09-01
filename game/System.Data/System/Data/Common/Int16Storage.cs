using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x020003A4 RID: 932
	internal sealed class Int16Storage : DataStorage
	{
		// Token: 0x06002D2C RID: 11564 RVA: 0x000BFB8B File Offset: 0x000BDD8B
		internal Int16Storage(DataColumn column) : base(column, typeof(short), 0, StorageType.Int16)
		{
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x000BFBA8 File Offset: 0x000BDDA8
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					long num = 0L;
					checked
					{
						foreach (int num2 in records)
						{
							if (base.HasValue(num2))
							{
								num += unchecked((long)this._values[num2]);
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
					long num3 = 0L;
					int num4 = 0;
					foreach (int num5 in records)
					{
						if (base.HasValue(num5))
						{
							checked
							{
								num3 += unchecked((long)this._values[num5]);
							}
							num4++;
							flag = true;
						}
					}
					if (flag)
					{
						return checked((short)(num3 / unchecked((long)num4)));
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					short num6 = short.MaxValue;
					foreach (int num7 in records)
					{
						if (base.HasValue(num7))
						{
							num6 = Math.Min(this._values[num7], num6);
							flag = true;
						}
					}
					if (flag)
					{
						return num6;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					short num8 = short.MinValue;
					foreach (int num9 in records)
					{
						if (base.HasValue(num9))
						{
							num8 = Math.Max(this._values[num9], num8);
							flag = true;
						}
					}
					if (flag)
					{
						return num8;
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
					int num10 = 0;
					for (int l = 0; l < records.Length; l++)
					{
						if (base.HasValue(records[l]))
						{
							num10++;
						}
					}
					return num10;
				}
				case AggregateType.Var:
				case AggregateType.StDev:
				{
					int num10 = 0;
					double num11 = 0.0;
					double num12 = 0.0;
					foreach (int num13 in records)
					{
						if (base.HasValue(num13))
						{
							num11 += (double)this._values[num13];
							num12 += (double)this._values[num13] * (double)this._values[num13];
							num10++;
						}
					}
					if (num10 <= 1)
					{
						return this._nullValue;
					}
					double num14 = (double)num10 * num12 - num11 * num11;
					if (num14 / (num11 * num11) < 1E-15 || num14 < 0.0)
					{
						num14 = 0.0;
					}
					else
					{
						num14 /= (double)(num10 * (num10 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(num14);
					}
					return num14;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(short));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x000BFED0 File Offset: 0x000BE0D0
		public override int Compare(int recordNo1, int recordNo2)
		{
			short num = this._values[recordNo1];
			short num2 = this._values[recordNo2];
			if (num == 0 || num2 == 0)
			{
				int num3 = base.CompareBits(recordNo1, recordNo2);
				if (num3 != 0)
				{
					return num3;
				}
			}
			return (int)(num - num2);
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x000BFF08 File Offset: 0x000BE108
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
				short num = this._values[recordNo];
				if (num == 0 && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((short)value);
			}
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x000BFF4F File Offset: 0x000BE14F
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToInt16(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x000BFF80 File Offset: 0x000BE180
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x000BFF9C File Offset: 0x000BE19C
		public override object Get(int record)
		{
			short num = this._values[record];
			if (num != 0)
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x000BFFC3 File Offset: 0x000BE1C3
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = 0;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToInt16(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000C0004 File Offset: 0x000BE204
		public override void SetCapacity(int capacity)
		{
			short[] array = new short[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x000C004A File Offset: 0x000BE24A
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToInt16(s);
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000C0057 File Offset: 0x000BE257
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((short)value);
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x000C0064 File Offset: 0x000BE264
		protected override object GetEmptyStorage(int recordCount)
		{
			return new short[recordCount];
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x000C006C File Offset: 0x000BE26C
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((short[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x000C0091 File Offset: 0x000BE291
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (short[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001B96 RID: 7062
		private const short defaultValue = 0;

		// Token: 0x04001B97 RID: 7063
		private short[] _values;
	}
}
