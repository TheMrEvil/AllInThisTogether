using System;
using System.Globalization;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200006C RID: 108
	[Serializable]
	public struct OptionalUInt32
	{
		// Token: 0x06000528 RID: 1320 RVA: 0x000120BD File Offset: 0x000102BD
		public OptionalUInt32(uint value)
		{
			this.value = value;
			this.hasValue = true;
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x000120CD File Offset: 0x000102CD
		public bool HasValue
		{
			get
			{
				return this.hasValue;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x000120D5 File Offset: 0x000102D5
		public bool HasNoValue
		{
			get
			{
				return !this.hasValue;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x000120E0 File Offset: 0x000102E0
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x000120FB File Offset: 0x000102FB
		public uint Value
		{
			get
			{
				if (!this.hasValue)
				{
					throw new OptionalTypeHasNoValueException("Trying to get a value from an OptionalUInt32 that has no value.");
				}
				return this.value;
			}
			set
			{
				this.value = value;
				this.hasValue = true;
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001210B File Offset: 0x0001030B
		public void Clear()
		{
			this.value = 0U;
			this.hasValue = false;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001211B File Offset: 0x0001031B
		public uint GetValueOrDefault(uint defaultValue)
		{
			if (!this.hasValue)
			{
				return defaultValue;
			}
			return this.value;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001212D File Offset: 0x0001032D
		public uint GetValueOrZero()
		{
			if (!this.hasValue)
			{
				return 0U;
			}
			return this.value;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001213F File Offset: 0x0001033F
		public void SetValue(uint value)
		{
			this.value = value;
			this.hasValue = true;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001214F File Offset: 0x0001034F
		public override bool Equals(object other)
		{
			return (other == null && !this.hasValue) || this.value.Equals(other);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001216A File Offset: 0x0001036A
		public bool Equals(OptionalUInt32 other)
		{
			return this.hasValue && other.hasValue && this.value == other.value;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001218C File Offset: 0x0001038C
		public bool Equals(uint other)
		{
			return this.hasValue && this.value == other;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000121A1 File Offset: 0x000103A1
		public static bool operator ==(OptionalUInt32 a, OptionalUInt32 b)
		{
			return a.hasValue && b.hasValue && a.value == b.value;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000121C3 File Offset: 0x000103C3
		public static bool operator !=(OptionalUInt32 a, OptionalUInt32 b)
		{
			return !(a == b);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x000121CF File Offset: 0x000103CF
		public static bool operator ==(OptionalUInt32 a, uint b)
		{
			return a.hasValue && a.value == b;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000121E4 File Offset: 0x000103E4
		public static bool operator !=(OptionalUInt32 a, uint b)
		{
			return !a.hasValue || a.value != b;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000121FC File Offset: 0x000103FC
		private static int CombineHashCodes(int h1, int h2)
		{
			return (h1 << 5) + h1 ^ h2;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00012205 File Offset: 0x00010405
		public override int GetHashCode()
		{
			return OptionalUInt32.CombineHashCodes(this.hasValue.GetHashCode(), this.value.GetHashCode());
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00012222 File Offset: 0x00010422
		public override string ToString()
		{
			if (!this.hasValue)
			{
				return "";
			}
			return this.value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00012242 File Offset: 0x00010442
		public static implicit operator OptionalUInt32(uint value)
		{
			return new OptionalUInt32(value);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001224A File Offset: 0x0001044A
		public static explicit operator uint(OptionalUInt32 optional)
		{
			return optional.Value;
		}

		// Token: 0x04000410 RID: 1040
		[SerializeField]
		private bool hasValue;

		// Token: 0x04000411 RID: 1041
		[SerializeField]
		private uint value;
	}
}
