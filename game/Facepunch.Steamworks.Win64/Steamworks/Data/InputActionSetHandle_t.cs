using System;

namespace Steamworks.Data
{
	// Token: 0x020001D7 RID: 471
	internal struct InputActionSetHandle_t : IEquatable<InputActionSetHandle_t>, IComparable<InputActionSetHandle_t>
	{
		// Token: 0x06000EF3 RID: 3827 RVA: 0x00018BD0 File Offset: 0x00016DD0
		public static implicit operator InputActionSetHandle_t(ulong value)
		{
			return new InputActionSetHandle_t
			{
				Value = value
			};
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00018BEE File Offset: 0x00016DEE
		public static implicit operator ulong(InputActionSetHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00018BF6 File Offset: 0x00016DF6
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00018C03 File Offset: 0x00016E03
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00018C10 File Offset: 0x00016E10
		public override bool Equals(object p)
		{
			return this.Equals((InputActionSetHandle_t)p);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00018C1E File Offset: 0x00016E1E
		public bool Equals(InputActionSetHandle_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x00018C2E File Offset: 0x00016E2E
		public static bool operator ==(InputActionSetHandle_t a, InputActionSetHandle_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00018C38 File Offset: 0x00016E38
		public static bool operator !=(InputActionSetHandle_t a, InputActionSetHandle_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00018C45 File Offset: 0x00016E45
		public int CompareTo(InputActionSetHandle_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BC6 RID: 3014
		public ulong Value;
	}
}
