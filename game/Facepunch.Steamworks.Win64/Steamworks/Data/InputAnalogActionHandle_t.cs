using System;

namespace Steamworks.Data
{
	// Token: 0x020001D9 RID: 473
	internal struct InputAnalogActionHandle_t : IEquatable<InputAnalogActionHandle_t>, IComparable<InputAnalogActionHandle_t>
	{
		// Token: 0x06000F05 RID: 3845 RVA: 0x00018CE0 File Offset: 0x00016EE0
		public static implicit operator InputAnalogActionHandle_t(ulong value)
		{
			return new InputAnalogActionHandle_t
			{
				Value = value
			};
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00018CFE File Offset: 0x00016EFE
		public static implicit operator ulong(InputAnalogActionHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00018D06 File Offset: 0x00016F06
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00018D13 File Offset: 0x00016F13
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00018D20 File Offset: 0x00016F20
		public override bool Equals(object p)
		{
			return this.Equals((InputAnalogActionHandle_t)p);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00018D2E File Offset: 0x00016F2E
		public bool Equals(InputAnalogActionHandle_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00018D3E File Offset: 0x00016F3E
		public static bool operator ==(InputAnalogActionHandle_t a, InputAnalogActionHandle_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00018D48 File Offset: 0x00016F48
		public static bool operator !=(InputAnalogActionHandle_t a, InputAnalogActionHandle_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00018D55 File Offset: 0x00016F55
		public int CompareTo(InputAnalogActionHandle_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BC8 RID: 3016
		public ulong Value;
	}
}
