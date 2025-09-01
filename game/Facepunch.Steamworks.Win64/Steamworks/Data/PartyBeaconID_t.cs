using System;

namespace Steamworks.Data
{
	// Token: 0x020001C3 RID: 451
	internal struct PartyBeaconID_t : IEquatable<PartyBeaconID_t>, IComparable<PartyBeaconID_t>
	{
		// Token: 0x06000E3F RID: 3647 RVA: 0x000180F8 File Offset: 0x000162F8
		public static implicit operator PartyBeaconID_t(ulong value)
		{
			return new PartyBeaconID_t
			{
				Value = value
			};
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00018116 File Offset: 0x00016316
		public static implicit operator ulong(PartyBeaconID_t value)
		{
			return value.Value;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0001811E File Offset: 0x0001631E
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0001812B File Offset: 0x0001632B
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00018138 File Offset: 0x00016338
		public override bool Equals(object p)
		{
			return this.Equals((PartyBeaconID_t)p);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00018146 File Offset: 0x00016346
		public bool Equals(PartyBeaconID_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00018156 File Offset: 0x00016356
		public static bool operator ==(PartyBeaconID_t a, PartyBeaconID_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00018160 File Offset: 0x00016360
		public static bool operator !=(PartyBeaconID_t a, PartyBeaconID_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0001816D File Offset: 0x0001636D
		public int CompareTo(PartyBeaconID_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BB2 RID: 2994
		public ulong Value;
	}
}
