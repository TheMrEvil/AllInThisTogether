using System;

namespace Steamworks
{
	// Token: 0x020001CA RID: 458
	[Serializable]
	public struct SteamLeaderboard_t : IEquatable<SteamLeaderboard_t>, IComparable<SteamLeaderboard_t>
	{
		// Token: 0x06000B62 RID: 2914 RVA: 0x000105EF File Offset: 0x0000E7EF
		public SteamLeaderboard_t(ulong value)
		{
			this.m_SteamLeaderboard = value;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x000105F8 File Offset: 0x0000E7F8
		public override string ToString()
		{
			return this.m_SteamLeaderboard.ToString();
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00010605 File Offset: 0x0000E805
		public override bool Equals(object other)
		{
			return other is SteamLeaderboard_t && this == (SteamLeaderboard_t)other;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00010622 File Offset: 0x0000E822
		public override int GetHashCode()
		{
			return this.m_SteamLeaderboard.GetHashCode();
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0001062F File Offset: 0x0000E82F
		public static bool operator ==(SteamLeaderboard_t x, SteamLeaderboard_t y)
		{
			return x.m_SteamLeaderboard == y.m_SteamLeaderboard;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0001063F File Offset: 0x0000E83F
		public static bool operator !=(SteamLeaderboard_t x, SteamLeaderboard_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0001064B File Offset: 0x0000E84B
		public static explicit operator SteamLeaderboard_t(ulong value)
		{
			return new SteamLeaderboard_t(value);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00010653 File Offset: 0x0000E853
		public static explicit operator ulong(SteamLeaderboard_t that)
		{
			return that.m_SteamLeaderboard;
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0001065B File Offset: 0x0000E85B
		public bool Equals(SteamLeaderboard_t other)
		{
			return this.m_SteamLeaderboard == other.m_SteamLeaderboard;
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0001066B File Offset: 0x0000E86B
		public int CompareTo(SteamLeaderboard_t other)
		{
			return this.m_SteamLeaderboard.CompareTo(other.m_SteamLeaderboard);
		}

		// Token: 0x04000B07 RID: 2823
		public ulong m_SteamLeaderboard;
	}
}
