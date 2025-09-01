using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000068 RID: 104
	[Serializable]
	public struct OptionalInputDeviceTransportType
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x00011A58 File Offset: 0x0000FC58
		public OptionalInputDeviceTransportType(InputDeviceTransportType value)
		{
			this.value = value;
			this.hasValue = true;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00011A68 File Offset: 0x0000FC68
		public bool HasValue
		{
			get
			{
				return this.hasValue;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00011A70 File Offset: 0x0000FC70
		public bool HasNoValue
		{
			get
			{
				return !this.hasValue;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00011A7B File Offset: 0x0000FC7B
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x00011A96 File Offset: 0x0000FC96
		public InputDeviceTransportType Value
		{
			get
			{
				if (!this.hasValue)
				{
					throw new OptionalTypeHasNoValueException("Trying to get a value from an OptionalInputDeviceTransportType that has no value.");
				}
				return this.value;
			}
			set
			{
				this.value = value;
				this.hasValue = true;
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00011AA6 File Offset: 0x0000FCA6
		public void Clear()
		{
			this.value = InputDeviceTransportType.Unknown;
			this.hasValue = false;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00011AB6 File Offset: 0x0000FCB6
		public InputDeviceTransportType GetValueOrDefault(InputDeviceTransportType defaultValue)
		{
			if (!this.hasValue)
			{
				return defaultValue;
			}
			return this.value;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00011AC8 File Offset: 0x0000FCC8
		public InputDeviceTransportType GetValueOrZero()
		{
			if (!this.hasValue)
			{
				return InputDeviceTransportType.Unknown;
			}
			return this.value;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00011ADA File Offset: 0x0000FCDA
		public void SetValue(InputDeviceTransportType value)
		{
			this.value = value;
			this.hasValue = true;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00011AEA File Offset: 0x0000FCEA
		public override bool Equals(object other)
		{
			return (other == null && !this.hasValue) || this.value.Equals(other);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00011B0B File Offset: 0x0000FD0B
		public bool Equals(OptionalInputDeviceTransportType other)
		{
			return this.hasValue && other.hasValue && this.value == other.value;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00011B2D File Offset: 0x0000FD2D
		public bool Equals(InputDeviceTransportType other)
		{
			return this.hasValue && this.value == other;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00011B42 File Offset: 0x0000FD42
		public static bool operator ==(OptionalInputDeviceTransportType a, OptionalInputDeviceTransportType b)
		{
			return a.hasValue && b.hasValue && a.value == b.value;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00011B64 File Offset: 0x0000FD64
		public static bool operator !=(OptionalInputDeviceTransportType a, OptionalInputDeviceTransportType b)
		{
			return !(a == b);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00011B70 File Offset: 0x0000FD70
		public static bool operator ==(OptionalInputDeviceTransportType a, InputDeviceTransportType b)
		{
			return a.hasValue && a.value == b;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00011B85 File Offset: 0x0000FD85
		public static bool operator !=(OptionalInputDeviceTransportType a, InputDeviceTransportType b)
		{
			return !a.hasValue || a.value != b;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00011B9D File Offset: 0x0000FD9D
		private static int CombineHashCodes(int h1, int h2)
		{
			return (h1 << 5) + h1 ^ h2;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00011BA6 File Offset: 0x0000FDA6
		public override int GetHashCode()
		{
			return OptionalInputDeviceTransportType.CombineHashCodes(this.hasValue.GetHashCode(), this.value.GetHashCode());
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00011BC9 File Offset: 0x0000FDC9
		public override string ToString()
		{
			if (!this.hasValue)
			{
				return "";
			}
			return this.value.ToString();
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00011BEA File Offset: 0x0000FDEA
		public static implicit operator OptionalInputDeviceTransportType(InputDeviceTransportType value)
		{
			return new OptionalInputDeviceTransportType(value);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00011BF2 File Offset: 0x0000FDF2
		public static explicit operator InputDeviceTransportType(OptionalInputDeviceTransportType optional)
		{
			return optional.Value;
		}

		// Token: 0x04000408 RID: 1032
		[SerializeField]
		private bool hasValue;

		// Token: 0x04000409 RID: 1033
		[SerializeField]
		private InputDeviceTransportType value;
	}
}
