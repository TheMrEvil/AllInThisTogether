using System;

namespace Steamworks.Data
{
	// Token: 0x020001DA RID: 474
	internal struct ControllerHandle_t : IEquatable<ControllerHandle_t>, IComparable<ControllerHandle_t>
	{
		// Token: 0x06000F0E RID: 3854 RVA: 0x00018D68 File Offset: 0x00016F68
		public static implicit operator ControllerHandle_t(ulong value)
		{
			return new ControllerHandle_t
			{
				Value = value
			};
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00018D86 File Offset: 0x00016F86
		public static implicit operator ulong(ControllerHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x00018D8E File Offset: 0x00016F8E
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00018D9B File Offset: 0x00016F9B
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00018DA8 File Offset: 0x00016FA8
		public override bool Equals(object p)
		{
			return this.Equals((ControllerHandle_t)p);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00018DB6 File Offset: 0x00016FB6
		public bool Equals(ControllerHandle_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00018DC6 File Offset: 0x00016FC6
		public static bool operator ==(ControllerHandle_t a, ControllerHandle_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00018DD0 File Offset: 0x00016FD0
		public static bool operator !=(ControllerHandle_t a, ControllerHandle_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00018DDD File Offset: 0x00016FDD
		public int CompareTo(ControllerHandle_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BC9 RID: 3017
		public ulong Value;
	}
}
