using System;

namespace Steamworks.Data
{
	// Token: 0x020001CF RID: 463
	internal struct SteamLeaderboard_t : IEquatable<SteamLeaderboard_t>, IComparable<SteamLeaderboard_t>
	{
		// Token: 0x06000EAB RID: 3755 RVA: 0x00018790 File Offset: 0x00016990
		public static implicit operator SteamLeaderboard_t(ulong value)
		{
			return new SteamLeaderboard_t
			{
				Value = value
			};
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x000187AE File Offset: 0x000169AE
		public static implicit operator ulong(SteamLeaderboard_t value)
		{
			return value.Value;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x000187B6 File Offset: 0x000169B6
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x000187C3 File Offset: 0x000169C3
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x000187D0 File Offset: 0x000169D0
		public override bool Equals(object p)
		{
			return this.Equals((SteamLeaderboard_t)p);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x000187DE File Offset: 0x000169DE
		public bool Equals(SteamLeaderboard_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x000187EE File Offset: 0x000169EE
		public static bool operator ==(SteamLeaderboard_t a, SteamLeaderboard_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x000187F8 File Offset: 0x000169F8
		public static bool operator !=(SteamLeaderboard_t a, SteamLeaderboard_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00018805 File Offset: 0x00016A05
		public int CompareTo(SteamLeaderboard_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BBE RID: 3006
		public ulong Value;
	}
}
