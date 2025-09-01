using System;

namespace Steamworks.Data
{
	// Token: 0x020001D0 RID: 464
	internal struct SteamLeaderboardEntries_t : IEquatable<SteamLeaderboardEntries_t>, IComparable<SteamLeaderboardEntries_t>
	{
		// Token: 0x06000EB4 RID: 3764 RVA: 0x00018818 File Offset: 0x00016A18
		public static implicit operator SteamLeaderboardEntries_t(ulong value)
		{
			return new SteamLeaderboardEntries_t
			{
				Value = value
			};
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00018836 File Offset: 0x00016A36
		public static implicit operator ulong(SteamLeaderboardEntries_t value)
		{
			return value.Value;
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0001883E File Offset: 0x00016A3E
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0001884B File Offset: 0x00016A4B
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00018858 File Offset: 0x00016A58
		public override bool Equals(object p)
		{
			return this.Equals((SteamLeaderboardEntries_t)p);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00018866 File Offset: 0x00016A66
		public bool Equals(SteamLeaderboardEntries_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00018876 File Offset: 0x00016A76
		public static bool operator ==(SteamLeaderboardEntries_t a, SteamLeaderboardEntries_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00018880 File Offset: 0x00016A80
		public static bool operator !=(SteamLeaderboardEntries_t a, SteamLeaderboardEntries_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0001888D File Offset: 0x00016A8D
		public int CompareTo(SteamLeaderboardEntries_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BBF RID: 3007
		public ulong Value;
	}
}
