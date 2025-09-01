using System;
using System.Globalization;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200006B RID: 107
	[Serializable]
	public struct OptionalUInt16
	{
		// Token: 0x06000513 RID: 1299 RVA: 0x00011F27 File Offset: 0x00010127
		public OptionalUInt16(ushort value)
		{
			this.value = value;
			this.hasValue = true;
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00011F37 File Offset: 0x00010137
		public bool HasValue
		{
			get
			{
				return this.hasValue;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00011F3F File Offset: 0x0001013F
		public bool HasNoValue
		{
			get
			{
				return !this.hasValue;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00011F4A File Offset: 0x0001014A
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x00011F65 File Offset: 0x00010165
		public ushort Value
		{
			get
			{
				if (!this.hasValue)
				{
					throw new OptionalTypeHasNoValueException("Trying to get a value from an OptionalUInt16 that has no value.");
				}
				return this.value;
			}
			set
			{
				this.value = value;
				this.hasValue = true;
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00011F75 File Offset: 0x00010175
		public void Clear()
		{
			this.value = 0;
			this.hasValue = false;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00011F85 File Offset: 0x00010185
		public ushort GetValueOrDefault(ushort defaultValue)
		{
			if (!this.hasValue)
			{
				return defaultValue;
			}
			return this.value;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00011F97 File Offset: 0x00010197
		public ushort GetValueOrZero()
		{
			if (!this.hasValue)
			{
				return 0;
			}
			return this.value;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00011FA9 File Offset: 0x000101A9
		public void SetValue(ushort value)
		{
			this.value = value;
			this.hasValue = true;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00011FB9 File Offset: 0x000101B9
		public override bool Equals(object other)
		{
			return (other == null && !this.hasValue) || this.value.Equals(other);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00011FD4 File Offset: 0x000101D4
		public bool Equals(OptionalUInt16 other)
		{
			return this.hasValue && other.hasValue && this.value == other.value;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00011FF6 File Offset: 0x000101F6
		public bool Equals(ushort other)
		{
			return this.hasValue && this.value == other;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001200B File Offset: 0x0001020B
		public static bool operator ==(OptionalUInt16 a, OptionalUInt16 b)
		{
			return a.hasValue && b.hasValue && a.value == b.value;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001202D File Offset: 0x0001022D
		public static bool operator !=(OptionalUInt16 a, OptionalUInt16 b)
		{
			return !(a == b);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00012039 File Offset: 0x00010239
		public static bool operator ==(OptionalUInt16 a, ushort b)
		{
			return a.hasValue && a.value == b;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001204E File Offset: 0x0001024E
		public static bool operator !=(OptionalUInt16 a, ushort b)
		{
			return !a.hasValue || a.value != b;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00012066 File Offset: 0x00010266
		private static int CombineHashCodes(int h1, int h2)
		{
			return (h1 << 5) + h1 ^ h2;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001206F File Offset: 0x0001026F
		public override int GetHashCode()
		{
			return OptionalUInt16.CombineHashCodes(this.hasValue.GetHashCode(), this.value.GetHashCode());
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001208C File Offset: 0x0001028C
		public override string ToString()
		{
			if (!this.hasValue)
			{
				return "";
			}
			return this.value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000120AC File Offset: 0x000102AC
		public static implicit operator OptionalUInt16(ushort value)
		{
			return new OptionalUInt16(value);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000120B4 File Offset: 0x000102B4
		public static explicit operator ushort(OptionalUInt16 optional)
		{
			return optional.Value;
		}

		// Token: 0x0400040E RID: 1038
		[SerializeField]
		private bool hasValue;

		// Token: 0x0400040F RID: 1039
		[SerializeField]
		private ushort value;
	}
}
