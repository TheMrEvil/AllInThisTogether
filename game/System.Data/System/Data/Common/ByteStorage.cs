using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000375 RID: 885
	internal sealed class ByteStorage : DataStorage
	{
		// Token: 0x06002954 RID: 10580 RVA: 0x000B54EF File Offset: 0x000B36EF
		internal ByteStorage(DataColumn column) : base(column, typeof(byte), 0, StorageType.Byte)
		{
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x000B550C File Offset: 0x000B370C
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					ulong num = 0UL;
					checked
					{
						foreach (int num2 in records)
						{
							if (!this.IsNull(num2))
							{
								num += unchecked((ulong)this._values[num2]);
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
						if (!this.IsNull(num5))
						{
							checked
							{
								num3 += (long)(unchecked((ulong)this._values[num5]));
							}
							num4++;
							flag = true;
						}
					}
					if (flag)
					{
						return checked((byte)(num3 / unchecked((long)num4)));
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					byte b = byte.MaxValue;
					foreach (int num6 in records)
					{
						if (!this.IsNull(num6))
						{
							b = Math.Min(this._values[num6], b);
							flag = true;
						}
					}
					if (flag)
					{
						return b;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					byte b2 = 0;
					foreach (int num7 in records)
					{
						if (!this.IsNull(num7))
						{
							b2 = Math.Max(this._values[num7], b2);
							flag = true;
						}
					}
					if (flag)
					{
						return b2;
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
					int num8 = 0;
					double num9 = 0.0;
					double num10 = 0.0;
					foreach (int num11 in records)
					{
						if (!this.IsNull(num11))
						{
							num9 += (double)this._values[num11];
							num10 += (double)this._values[num11] * (double)this._values[num11];
							num8++;
						}
					}
					if (num8 <= 1)
					{
						return this._nullValue;
					}
					double num12 = (double)num8 * num10 - num9 * num9;
					if (num12 / (num9 * num9) < 1E-15 || num12 < 0.0)
					{
						num12 = 0.0;
					}
					else
					{
						num12 /= (double)(num8 * (num8 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(num12);
					}
					return num12;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(byte));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x000B580C File Offset: 0x000B3A0C
		public override int Compare(int recordNo1, int recordNo2)
		{
			byte b = this._values[recordNo1];
			byte b2 = this._values[recordNo2];
			if (b == 0 || b2 == 0)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return b.CompareTo(b2);
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x000B5848 File Offset: 0x000B3A48
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
				byte b = this._values[recordNo];
				if (b == 0 && this.IsNull(recordNo))
				{
					return -1;
				}
				return b.CompareTo((byte)value);
			}
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x000B588F File Offset: 0x000B3A8F
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToByte(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x000B58C0 File Offset: 0x000B3AC0
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x000B58DC File Offset: 0x000B3ADC
		public override object Get(int record)
		{
			byte b = this._values[record];
			if (b != 0)
			{
				return b;
			}
			return base.GetBits(record);
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x000B5903 File Offset: 0x000B3B03
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = 0;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToByte(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x000B5944 File Offset: 0x000B3B44
		public override void SetCapacity(int capacity)
		{
			byte[] array = new byte[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x000B598A File Offset: 0x000B3B8A
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToByte(s);
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x000B5997 File Offset: 0x000B3B97
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((byte)value);
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x000B59A4 File Offset: 0x000B3BA4
		protected override object GetEmptyStorage(int recordCount)
		{
			return new byte[recordCount];
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x000B59AC File Offset: 0x000B3BAC
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((byte[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x000B59CE File Offset: 0x000B3BCE
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (byte[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001A68 RID: 6760
		private const byte defaultValue = 0;

		// Token: 0x04001A69 RID: 6761
		private byte[] _values;
	}
}
