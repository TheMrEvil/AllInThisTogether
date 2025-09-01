using System;

namespace Steamworks.Data
{
	// Token: 0x020001DD RID: 477
	internal struct ControllerAnalogActionHandle_t : IEquatable<ControllerAnalogActionHandle_t>, IComparable<ControllerAnalogActionHandle_t>
	{
		// Token: 0x06000F29 RID: 3881 RVA: 0x00018F00 File Offset: 0x00017100
		public static implicit operator ControllerAnalogActionHandle_t(ulong value)
		{
			return new ControllerAnalogActionHandle_t
			{
				Value = value
			};
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00018F1E File Offset: 0x0001711E
		public static implicit operator ulong(ControllerAnalogActionHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00018F26 File Offset: 0x00017126
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00018F33 File Offset: 0x00017133
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00018F40 File Offset: 0x00017140
		public override bool Equals(object p)
		{
			return this.Equals((ControllerAnalogActionHandle_t)p);
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00018F4E File Offset: 0x0001714E
		public bool Equals(ControllerAnalogActionHandle_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x00018F5E File Offset: 0x0001715E
		public static bool operator ==(ControllerAnalogActionHandle_t a, ControllerAnalogActionHandle_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x00018F68 File Offset: 0x00017168
		public static bool operator !=(ControllerAnalogActionHandle_t a, ControllerAnalogActionHandle_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x00018F75 File Offset: 0x00017175
		public int CompareTo(ControllerAnalogActionHandle_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BCC RID: 3020
		public ulong Value;
	}
}
