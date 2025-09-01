using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000376 RID: 886
	internal sealed class CharStorage : DataStorage
	{
		// Token: 0x06002962 RID: 10594 RVA: 0x000B59E3 File Offset: 0x000B3BE3
		internal CharStorage(DataColumn column) : base(column, typeof(char), '\0', StorageType.Char)
		{
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x000B5A00 File Offset: 0x000B3C00
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					char c = char.MaxValue;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							c = ((this._values[num] < c) ? this._values[num] : c);
							flag = true;
						}
					}
					if (flag)
					{
						return c;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					char c2 = '\0';
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							c2 = ((this._values[num2] > c2) ? this._values[num2] : c2);
							flag = true;
						}
					}
					if (flag)
					{
						return c2;
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
				throw ExprException.Overflow(typeof(char));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000B5B38 File Offset: 0x000B3D38
		public override int Compare(int recordNo1, int recordNo2)
		{
			char c = this._values[recordNo1];
			char c2 = this._values[recordNo2];
			if (c == '\0' || c2 == '\0')
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return c.CompareTo(c2);
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x000B5B74 File Offset: 0x000B3D74
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
				char c = this._values[recordNo];
				if (c == '\0' && this.IsNull(recordNo))
				{
					return -1;
				}
				return c.CompareTo((char)value);
			}
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x000B5BBB File Offset: 0x000B3DBB
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToChar(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x000B5BEC File Offset: 0x000B3DEC
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x000B5C08 File Offset: 0x000B3E08
		public override object Get(int record)
		{
			char c = this._values[record];
			if (c != '\0')
			{
				return c;
			}
			return base.GetBits(record);
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x000B5C30 File Offset: 0x000B3E30
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = '\0';
				base.SetNullBit(record, true);
				return;
			}
			char c = ((IConvertible)value).ToChar(base.FormatProvider);
			if ((c >= '\ud800' && c <= '\udfff') || (c < '!' && (c == '\t' || c == '\n' || c == '\r')))
			{
				throw ExceptionBuilder.ProblematicChars(c);
			}
			this._values[record] = c;
			base.SetNullBit(record, false);
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x000B5CA8 File Offset: 0x000B3EA8
		public override void SetCapacity(int capacity)
		{
			char[] array = new char[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000B5CEE File Offset: 0x000B3EEE
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToChar(s);
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x000B5CFB File Offset: 0x000B3EFB
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((char)value);
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x000B5D08 File Offset: 0x000B3F08
		protected override object GetEmptyStorage(int recordCount)
		{
			return new char[recordCount];
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000B5D10 File Offset: 0x000B3F10
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((char[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000B5D32 File Offset: 0x000B3F32
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (char[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001A6A RID: 6762
		private const char defaultValue = '\0';

		// Token: 0x04001A6B RID: 6763
		private char[] _values;
	}
}
