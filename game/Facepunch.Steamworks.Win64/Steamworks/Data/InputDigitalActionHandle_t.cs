using System;

namespace Steamworks.Data
{
	// Token: 0x020001D8 RID: 472
	internal struct InputDigitalActionHandle_t : IEquatable<InputDigitalActionHandle_t>, IComparable<InputDigitalActionHandle_t>
	{
		// Token: 0x06000EFC RID: 3836 RVA: 0x00018C58 File Offset: 0x00016E58
		public static implicit operator InputDigitalActionHandle_t(ulong value)
		{
			return new InputDigitalActionHandle_t
			{
				Value = value
			};
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00018C76 File Offset: 0x00016E76
		public static implicit operator ulong(InputDigitalActionHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00018C7E File Offset: 0x00016E7E
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00018C8B File Offset: 0x00016E8B
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00018C98 File Offset: 0x00016E98
		public override bool Equals(object p)
		{
			return this.Equals((InputDigitalActionHandle_t)p);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00018CA6 File Offset: 0x00016EA6
		public bool Equals(InputDigitalActionHandle_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00018CB6 File Offset: 0x00016EB6
		public static bool operator ==(InputDigitalActionHandle_t a, InputDigitalActionHandle_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00018CC0 File Offset: 0x00016EC0
		public static bool operator !=(InputDigitalActionHandle_t a, InputDigitalActionHandle_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00018CCD File Offset: 0x00016ECD
		public int CompareTo(InputDigitalActionHandle_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BC7 RID: 3015
		public ulong Value;
	}
}
