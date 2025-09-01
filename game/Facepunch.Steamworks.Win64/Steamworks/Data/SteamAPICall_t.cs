using System;

namespace Steamworks.Data
{
	// Token: 0x020001BE RID: 446
	internal struct SteamAPICall_t : IEquatable<SteamAPICall_t>, IComparable<SteamAPICall_t>
	{
		// Token: 0x06000E12 RID: 3602 RVA: 0x00017E50 File Offset: 0x00016050
		public static implicit operator SteamAPICall_t(ulong value)
		{
			return new SteamAPICall_t
			{
				Value = value
			};
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00017E6E File Offset: 0x0001606E
		public static implicit operator ulong(SteamAPICall_t value)
		{
			return value.Value;
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00017E76 File Offset: 0x00016076
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00017E83 File Offset: 0x00016083
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00017E90 File Offset: 0x00016090
		public override bool Equals(object p)
		{
			return this.Equals((SteamAPICall_t)p);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00017E9E File Offset: 0x0001609E
		public bool Equals(SteamAPICall_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00017EAE File Offset: 0x000160AE
		public static bool operator ==(SteamAPICall_t a, SteamAPICall_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00017EB8 File Offset: 0x000160B8
		public static bool operator !=(SteamAPICall_t a, SteamAPICall_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00017EC5 File Offset: 0x000160C5
		public int CompareTo(SteamAPICall_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BAD RID: 2989
		public ulong Value;
	}
}
