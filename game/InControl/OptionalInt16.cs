using System;
using System.Globalization;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000069 RID: 105
	[Serializable]
	public struct OptionalInt16
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x00011BFB File Offset: 0x0000FDFB
		public OptionalInt16(short value)
		{
			this.value = value;
			this.hasValue = true;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00011C0B File Offset: 0x0000FE0B
		public bool HasValue
		{
			get
			{
				return this.hasValue;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00011C13 File Offset: 0x0000FE13
		public bool HasNoValue
		{
			get
			{
				return !this.hasValue;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00011C1E File Offset: 0x0000FE1E
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x00011C39 File Offset: 0x0000FE39
		public short Value
		{
			get
			{
				if (!this.hasValue)
				{
					throw new OptionalTypeHasNoValueException("Trying to get a value from an OptionalInt16 that has no value.");
				}
				return this.value;
			}
			set
			{
				this.value = value;
				this.hasValue = true;
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00011C49 File Offset: 0x0000FE49
		public void Clear()
		{
			this.value = 0;
			this.hasValue = false;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00011C59 File Offset: 0x0000FE59
		public short GetValueOrDefault(short defaultValue)
		{
			if (!this.hasValue)
			{
				return defaultValue;
			}
			return this.value;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00011C6B File Offset: 0x0000FE6B
		public short GetValueOrZero()
		{
			if (!this.hasValue)
			{
				return 0;
			}
			return this.value;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00011C7D File Offset: 0x0000FE7D
		public void SetValue(short value)
		{
			this.value = value;
			this.hasValue = true;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00011C8D File Offset: 0x0000FE8D
		public override bool Equals(object other)
		{
			return (other == null && !this.hasValue) || this.value.Equals(other);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00011CA8 File Offset: 0x0000FEA8
		public bool Equals(OptionalInt16 other)
		{
			return this.hasValue && other.hasValue && this.value == other.value;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00011CCA File Offset: 0x0000FECA
		public bool Equals(short other)
		{
			return this.hasValue && this.value == other;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00011CDF File Offset: 0x0000FEDF
		public static bool operator ==(OptionalInt16 a, OptionalInt16 b)
		{
			return a.hasValue && b.hasValue && a.value == b.value;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00011D01 File Offset: 0x0000FF01
		public static bool operator !=(OptionalInt16 a, OptionalInt16 b)
		{
			return !(a == b);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00011D0D File Offset: 0x0000FF0D
		public static bool operator ==(OptionalInt16 a, short b)
		{
			return a.hasValue && a.value == b;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00011D22 File Offset: 0x0000FF22
		public static bool operator !=(OptionalInt16 a, short b)
		{
			return !a.hasValue || a.value != b;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00011D3A File Offset: 0x0000FF3A
		private static int CombineHashCodes(int h1, int h2)
		{
			return (h1 << 5) + h1 ^ h2;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00011D43 File Offset: 0x0000FF43
		public override int GetHashCode()
		{
			return OptionalInt16.CombineHashCodes(this.hasValue.GetHashCode(), this.value.GetHashCode());
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00011D60 File Offset: 0x0000FF60
		public override string ToString()
		{
			if (!this.hasValue)
			{
				return "";
			}
			return this.value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00011D80 File Offset: 0x0000FF80
		public static implicit operator OptionalInt16(short value)
		{
			return new OptionalInt16(value);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00011D88 File Offset: 0x0000FF88
		public static explicit operator short(OptionalInt16 optional)
		{
			return optional.Value;
		}

		// Token: 0x0400040A RID: 1034
		[SerializeField]
		private bool hasValue;

		// Token: 0x0400040B RID: 1035
		[SerializeField]
		private short value;
	}
}
