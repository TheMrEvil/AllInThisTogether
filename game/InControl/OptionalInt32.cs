using System;
using System.Globalization;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200006A RID: 106
	[Serializable]
	public struct OptionalInt32
	{
		// Token: 0x060004FE RID: 1278 RVA: 0x00011D91 File Offset: 0x0000FF91
		public OptionalInt32(int value)
		{
			this.value = value;
			this.hasValue = true;
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00011DA1 File Offset: 0x0000FFA1
		public bool HasValue
		{
			get
			{
				return this.hasValue;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00011DA9 File Offset: 0x0000FFA9
		public bool HasNoValue
		{
			get
			{
				return !this.hasValue;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00011DB4 File Offset: 0x0000FFB4
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x00011DCF File Offset: 0x0000FFCF
		public int Value
		{
			get
			{
				if (!this.hasValue)
				{
					throw new OptionalTypeHasNoValueException("Trying to get a value from an OptionalInt32 that has no value.");
				}
				return this.value;
			}
			set
			{
				this.value = value;
				this.hasValue = true;
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00011DDF File Offset: 0x0000FFDF
		public void Clear()
		{
			this.value = 0;
			this.hasValue = false;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00011DEF File Offset: 0x0000FFEF
		public int GetValueOrDefault(int defaultValue)
		{
			if (!this.hasValue)
			{
				return defaultValue;
			}
			return this.value;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00011E01 File Offset: 0x00010001
		public int GetValueOrZero()
		{
			if (!this.hasValue)
			{
				return 0;
			}
			return this.value;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00011E13 File Offset: 0x00010013
		public void SetValue(int value)
		{
			this.value = value;
			this.hasValue = true;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00011E23 File Offset: 0x00010023
		public override bool Equals(object other)
		{
			return (other == null && !this.hasValue) || this.value.Equals(other);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00011E3E File Offset: 0x0001003E
		public bool Equals(OptionalInt32 other)
		{
			return this.hasValue && other.hasValue && this.value == other.value;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00011E60 File Offset: 0x00010060
		public bool Equals(int other)
		{
			return this.hasValue && this.value == other;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00011E75 File Offset: 0x00010075
		public static bool operator ==(OptionalInt32 a, OptionalInt32 b)
		{
			return a.hasValue && b.hasValue && a.value == b.value;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00011E97 File Offset: 0x00010097
		public static bool operator !=(OptionalInt32 a, OptionalInt32 b)
		{
			return !(a == b);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00011EA3 File Offset: 0x000100A3
		public static bool operator ==(OptionalInt32 a, int b)
		{
			return a.hasValue && a.value == b;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00011EB8 File Offset: 0x000100B8
		public static bool operator !=(OptionalInt32 a, int b)
		{
			return !a.hasValue || a.value != b;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00011ED0 File Offset: 0x000100D0
		private static int CombineHashCodes(int h1, int h2)
		{
			return (h1 << 5) + h1 ^ h2;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00011ED9 File Offset: 0x000100D9
		public override int GetHashCode()
		{
			return OptionalInt32.CombineHashCodes(this.hasValue.GetHashCode(), this.value.GetHashCode());
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00011EF6 File Offset: 0x000100F6
		public override string ToString()
		{
			if (!this.hasValue)
			{
				return "";
			}
			return this.value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00011F16 File Offset: 0x00010116
		public static implicit operator OptionalInt32(int value)
		{
			return new OptionalInt32(value);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00011F1E File Offset: 0x0001011E
		public static explicit operator int(OptionalInt32 optional)
		{
			return optional.Value;
		}

		// Token: 0x0400040C RID: 1036
		[SerializeField]
		private bool hasValue;

		// Token: 0x0400040D RID: 1037
		[SerializeField]
		private int value;
	}
}
