using System;
using System.Collections;
using System.Globalization;
using System.Numerics;

namespace System.Data.Common
{
	// Token: 0x02000373 RID: 883
	internal sealed class BigIntegerStorage : DataStorage
	{
		// Token: 0x06002936 RID: 10550 RVA: 0x000B4C65 File Offset: 0x000B2E65
		internal BigIntegerStorage(DataColumn column) : base(column, typeof(BigInteger), BigInteger.Zero, StorageType.BigInteger)
		{
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x000B07A2 File Offset: 0x000AE9A2
		public override object Aggregate(int[] records, AggregateType kind)
		{
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x000B4C84 File Offset: 0x000B2E84
		public override int Compare(int recordNo1, int recordNo2)
		{
			BigInteger bigInteger = this._values[recordNo1];
			BigInteger other = this._values[recordNo2];
			if (bigInteger.IsZero || other.IsZero)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return bigInteger.CompareTo(other);
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x000B4CD4 File Offset: 0x000B2ED4
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
				BigInteger bigInteger = this._values[recordNo];
				if (bigInteger.IsZero && !base.HasValue(recordNo))
				{
					return -1;
				}
				return bigInteger.CompareTo((BigInteger)value);
			}
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x000B4D28 File Offset: 0x000B2F28
		internal static BigInteger ConvertToBigInteger(object value, IFormatProvider formatProvider)
		{
			if (value.GetType() == typeof(BigInteger))
			{
				return (BigInteger)value;
			}
			if (value.GetType() == typeof(string))
			{
				return BigInteger.Parse((string)value, formatProvider);
			}
			if (value.GetType() == typeof(long))
			{
				return (long)value;
			}
			if (value.GetType() == typeof(int))
			{
				return (int)value;
			}
			if (value.GetType() == typeof(short))
			{
				return (short)value;
			}
			if (value.GetType() == typeof(sbyte))
			{
				return (sbyte)value;
			}
			if (value.GetType() == typeof(ulong))
			{
				return (ulong)value;
			}
			if (value.GetType() == typeof(uint))
			{
				return (uint)value;
			}
			if (value.GetType() == typeof(ushort))
			{
				return (ushort)value;
			}
			if (value.GetType() == typeof(byte))
			{
				return (byte)value;
			}
			throw ExceptionBuilder.ConvertFailed(value.GetType(), typeof(BigInteger));
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x000B4EA4 File Offset: 0x000B30A4
		internal static object ConvertFromBigInteger(BigInteger value, Type type, IFormatProvider formatProvider)
		{
			if (type == typeof(string))
			{
				return value.ToString("D", formatProvider);
			}
			if (type == typeof(sbyte))
			{
				return (sbyte)value;
			}
			if (type == typeof(short))
			{
				return (short)value;
			}
			if (type == typeof(int))
			{
				return (int)value;
			}
			if (type == typeof(long))
			{
				return (long)value;
			}
			if (type == typeof(byte))
			{
				return (byte)value;
			}
			if (type == typeof(ushort))
			{
				return (ushort)value;
			}
			if (type == typeof(uint))
			{
				return (uint)value;
			}
			if (type == typeof(ulong))
			{
				return (ulong)value;
			}
			if (type == typeof(float))
			{
				return (float)value;
			}
			if (type == typeof(double))
			{
				return (double)value;
			}
			if (type == typeof(decimal))
			{
				return (decimal)value;
			}
			if (type == typeof(BigInteger))
			{
				return value;
			}
			throw ExceptionBuilder.ConvertFailed(typeof(BigInteger), type);
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000B5046 File Offset: 0x000B3246
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = BigIntegerStorage.ConvertToBigInteger(value, base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000B5072 File Offset: 0x000B3272
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x000B5094 File Offset: 0x000B3294
		public override object Get(int record)
		{
			BigInteger bigInteger = this._values[record];
			if (!bigInteger.IsZero)
			{
				return bigInteger;
			}
			return base.GetBits(record);
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x000B50C8 File Offset: 0x000B32C8
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = BigInteger.Zero;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = BigIntegerStorage.ConvertToBigInteger(value, base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x000B5118 File Offset: 0x000B3318
		public override void SetCapacity(int capacity)
		{
			BigInteger[] array = new BigInteger[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x000B515E File Offset: 0x000B335E
		public override object ConvertXmlToObject(string s)
		{
			return BigInteger.Parse(s, CultureInfo.InvariantCulture);
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x000B5170 File Offset: 0x000B3370
		public override string ConvertObjectToXml(object value)
		{
			return ((BigInteger)value).ToString("D", CultureInfo.InvariantCulture);
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x000B5195 File Offset: 0x000B3395
		protected override object GetEmptyStorage(int recordCount)
		{
			return new BigInteger[recordCount];
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x000B519D File Offset: 0x000B339D
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((BigInteger[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x000B51CA File Offset: 0x000B33CA
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (BigInteger[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001A65 RID: 6757
		private BigInteger[] _values;
	}
}
