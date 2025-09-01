using System;

namespace Steamworks.Data
{
	// Token: 0x020001DC RID: 476
	internal struct ControllerDigitalActionHandle_t : IEquatable<ControllerDigitalActionHandle_t>, IComparable<ControllerDigitalActionHandle_t>
	{
		// Token: 0x06000F20 RID: 3872 RVA: 0x00018E78 File Offset: 0x00017078
		public static implicit operator ControllerDigitalActionHandle_t(ulong value)
		{
			return new ControllerDigitalActionHandle_t
			{
				Value = value
			};
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00018E96 File Offset: 0x00017096
		public static implicit operator ulong(ControllerDigitalActionHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x00018E9E File Offset: 0x0001709E
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00018EAB File Offset: 0x000170AB
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x00018EB8 File Offset: 0x000170B8
		public override bool Equals(object p)
		{
			return this.Equals((ControllerDigitalActionHandle_t)p);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00018EC6 File Offset: 0x000170C6
		public bool Equals(ControllerDigitalActionHandle_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x00018ED6 File Offset: 0x000170D6
		public static bool operator ==(ControllerDigitalActionHandle_t a, ControllerDigitalActionHandle_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x00018EE0 File Offset: 0x000170E0
		public static bool operator !=(ControllerDigitalActionHandle_t a, ControllerDigitalActionHandle_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00018EED File Offset: 0x000170ED
		public int CompareTo(ControllerDigitalActionHandle_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BCB RID: 3019
		public ulong Value;
	}
}
