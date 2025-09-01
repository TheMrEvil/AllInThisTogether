using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000067 RID: 103
	[Serializable]
	public struct OptionalInputDeviceDriverType
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x000118B5 File Offset: 0x0000FAB5
		public OptionalInputDeviceDriverType(InputDeviceDriverType value)
		{
			this.value = value;
			this.hasValue = true;
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x000118C5 File Offset: 0x0000FAC5
		public bool HasValue
		{
			get
			{
				return this.hasValue;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x000118CD File Offset: 0x0000FACD
		public bool HasNoValue
		{
			get
			{
				return !this.hasValue;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x000118D8 File Offset: 0x0000FAD8
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x000118F3 File Offset: 0x0000FAF3
		public InputDeviceDriverType Value
		{
			get
			{
				if (!this.hasValue)
				{
					throw new OptionalTypeHasNoValueException("Trying to get a value from an OptionalInputDeviceDriverType that has no value.");
				}
				return this.value;
			}
			set
			{
				this.value = value;
				this.hasValue = true;
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00011903 File Offset: 0x0000FB03
		public void Clear()
		{
			this.value = InputDeviceDriverType.Unknown;
			this.hasValue = false;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00011913 File Offset: 0x0000FB13
		public InputDeviceDriverType GetValueOrDefault(InputDeviceDriverType defaultValue)
		{
			if (!this.hasValue)
			{
				return defaultValue;
			}
			return this.value;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00011925 File Offset: 0x0000FB25
		public InputDeviceDriverType GetValueOrZero()
		{
			if (!this.hasValue)
			{
				return InputDeviceDriverType.Unknown;
			}
			return this.value;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00011937 File Offset: 0x0000FB37
		public void SetValue(InputDeviceDriverType value)
		{
			this.value = value;
			this.hasValue = true;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00011947 File Offset: 0x0000FB47
		public override bool Equals(object other)
		{
			return (other == null && !this.hasValue) || this.value.Equals(other);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00011968 File Offset: 0x0000FB68
		public bool Equals(OptionalInputDeviceDriverType other)
		{
			return this.hasValue && other.hasValue && this.value == other.value;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001198A File Offset: 0x0000FB8A
		public bool Equals(InputDeviceDriverType other)
		{
			return this.hasValue && this.value == other;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001199F File Offset: 0x0000FB9F
		public static bool operator ==(OptionalInputDeviceDriverType a, OptionalInputDeviceDriverType b)
		{
			return a.hasValue && b.hasValue && a.value == b.value;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000119C1 File Offset: 0x0000FBC1
		public static bool operator !=(OptionalInputDeviceDriverType a, OptionalInputDeviceDriverType b)
		{
			return !(a == b);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000119CD File Offset: 0x0000FBCD
		public static bool operator ==(OptionalInputDeviceDriverType a, InputDeviceDriverType b)
		{
			return a.hasValue && a.value == b;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000119E2 File Offset: 0x0000FBE2
		public static bool operator !=(OptionalInputDeviceDriverType a, InputDeviceDriverType b)
		{
			return !a.hasValue || a.value != b;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000119FA File Offset: 0x0000FBFA
		private static int CombineHashCodes(int h1, int h2)
		{
			return (h1 << 5) + h1 ^ h2;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00011A03 File Offset: 0x0000FC03
		public override int GetHashCode()
		{
			return OptionalInputDeviceDriverType.CombineHashCodes(this.hasValue.GetHashCode(), this.value.GetHashCode());
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00011A26 File Offset: 0x0000FC26
		public override string ToString()
		{
			if (!this.hasValue)
			{
				return "";
			}
			return this.value.ToString();
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00011A47 File Offset: 0x0000FC47
		public static implicit operator OptionalInputDeviceDriverType(InputDeviceDriverType value)
		{
			return new OptionalInputDeviceDriverType(value);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00011A4F File Offset: 0x0000FC4F
		public static explicit operator InputDeviceDriverType(OptionalInputDeviceDriverType optional)
		{
			return optional.Value;
		}

		// Token: 0x04000406 RID: 1030
		[SerializeField]
		private bool hasValue;

		// Token: 0x04000407 RID: 1031
		[SerializeField]
		private InputDeviceDriverType value;
	}
}
