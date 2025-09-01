using System;
using System.Globalization;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	public struct OptionalFloat
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x000116A8 File Offset: 0x0000F8A8
		public OptionalFloat(float value)
		{
			this.value = value;
			this.hasValue = true;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x000116B8 File Offset: 0x0000F8B8
		public bool HasValue
		{
			get
			{
				return this.hasValue;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x000116C0 File Offset: 0x0000F8C0
		public bool HasNoValue
		{
			get
			{
				return !this.hasValue;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x000116CB File Offset: 0x0000F8CB
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x000116E6 File Offset: 0x0000F8E6
		public float Value
		{
			get
			{
				if (!this.hasValue)
				{
					throw new OptionalTypeHasNoValueException("Trying to get a value from an OptionalFloat that has no value.");
				}
				return this.value;
			}
			set
			{
				this.value = value;
				this.hasValue = true;
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000116F6 File Offset: 0x0000F8F6
		public void Clear()
		{
			this.value = 0f;
			this.hasValue = false;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001170A File Offset: 0x0000F90A
		public float GetValueOrDefault(float defaultValue)
		{
			if (!this.hasValue)
			{
				return defaultValue;
			}
			return this.value;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001171C File Offset: 0x0000F91C
		public float GetValueOrZero()
		{
			if (!this.hasValue)
			{
				return 0f;
			}
			return this.value;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00011732 File Offset: 0x0000F932
		public void SetValue(float value)
		{
			this.value = value;
			this.hasValue = true;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00011742 File Offset: 0x0000F942
		public override bool Equals(object other)
		{
			return (other == null && !this.hasValue) || this.value.Equals(other);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001175D File Offset: 0x0000F95D
		public bool Equals(OptionalFloat other)
		{
			return this.hasValue && other.hasValue && OptionalFloat.IsApproximatelyEqual(this.value, other.value);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00011782 File Offset: 0x0000F982
		public bool Equals(float other)
		{
			return this.hasValue && OptionalFloat.IsApproximatelyEqual(this.value, other);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001179A File Offset: 0x0000F99A
		public static bool operator ==(OptionalFloat a, OptionalFloat b)
		{
			return a.hasValue && b.hasValue && OptionalFloat.IsApproximatelyEqual(a.value, b.value);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000117BF File Offset: 0x0000F9BF
		public static bool operator !=(OptionalFloat a, OptionalFloat b)
		{
			return !(a == b);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000117CB File Offset: 0x0000F9CB
		public static bool operator ==(OptionalFloat a, float b)
		{
			return a.hasValue && OptionalFloat.IsApproximatelyEqual(a.value, b);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000117E3 File Offset: 0x0000F9E3
		public static bool operator !=(OptionalFloat a, float b)
		{
			return !a.hasValue || !OptionalFloat.IsApproximatelyEqual(a.value, b);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000117FE File Offset: 0x0000F9FE
		private static int CombineHashCodes(int h1, int h2)
		{
			return (h1 << 5) + h1 ^ h2;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00011807 File Offset: 0x0000FA07
		public override int GetHashCode()
		{
			return OptionalFloat.CombineHashCodes(this.hasValue.GetHashCode(), this.value.GetHashCode());
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00011824 File Offset: 0x0000FA24
		public override string ToString()
		{
			if (!this.hasValue)
			{
				return "";
			}
			return this.value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00011844 File Offset: 0x0000FA44
		public static implicit operator OptionalFloat(float value)
		{
			return new OptionalFloat(value);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001184C File Offset: 0x0000FA4C
		public static explicit operator float(OptionalFloat optional)
		{
			return optional.Value;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00011858 File Offset: 0x0000FA58
		private static bool IsApproximatelyEqual(float a, float b)
		{
			float num = a - b;
			return num >= -1E-07f && num <= 1E-07f;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00011880 File Offset: 0x0000FA80
		public bool ApproximatelyEquals(float other)
		{
			if (!this.hasValue)
			{
				return false;
			}
			float num = this.value - other;
			return num >= -1E-07f && num <= 1E-07f;
		}

		// Token: 0x04000403 RID: 1027
		[SerializeField]
		private bool hasValue;

		// Token: 0x04000404 RID: 1028
		[SerializeField]
		private float value;

		// Token: 0x04000405 RID: 1029
		private const float epsilon = 1E-07f;
	}
}
