using System;

namespace Steamworks
{
	// Token: 0x020001C9 RID: 457
	[Serializable]
	public struct SteamLeaderboardEntries_t : IEquatable<SteamLeaderboardEntries_t>, IComparable<SteamLeaderboardEntries_t>
	{
		// Token: 0x06000B58 RID: 2904 RVA: 0x00010560 File Offset: 0x0000E760
		public SteamLeaderboardEntries_t(ulong value)
		{
			this.m_SteamLeaderboardEntries = value;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00010569 File Offset: 0x0000E769
		public override string ToString()
		{
			return this.m_SteamLeaderboardEntries.ToString();
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00010576 File Offset: 0x0000E776
		public override bool Equals(object other)
		{
			return other is SteamLeaderboardEntries_t && this == (SteamLeaderboardEntries_t)other;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00010593 File Offset: 0x0000E793
		public override int GetHashCode()
		{
			return this.m_SteamLeaderboardEntries.GetHashCode();
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x000105A0 File Offset: 0x0000E7A0
		public static bool operator ==(SteamLeaderboardEntries_t x, SteamLeaderboardEntries_t y)
		{
			return x.m_SteamLeaderboardEntries == y.m_SteamLeaderboardEntries;
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x000105B0 File Offset: 0x0000E7B0
		public static bool operator !=(SteamLeaderboardEntries_t x, SteamLeaderboardEntries_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x000105BC File Offset: 0x0000E7BC
		public static explicit operator SteamLeaderboardEntries_t(ulong value)
		{
			return new SteamLeaderboardEntries_t(value);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x000105C4 File Offset: 0x0000E7C4
		public static explicit operator ulong(SteamLeaderboardEntries_t that)
		{
			return that.m_SteamLeaderboardEntries;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x000105CC File Offset: 0x0000E7CC
		public bool Equals(SteamLeaderboardEntries_t other)
		{
			return this.m_SteamLeaderboardEntries == other.m_SteamLeaderboardEntries;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x000105DC File Offset: 0x0000E7DC
		public int CompareTo(SteamLeaderboardEntries_t other)
		{
			return this.m_SteamLeaderboardEntries.CompareTo(other.m_SteamLeaderboardEntries);
		}

		// Token: 0x04000B06 RID: 2822
		public ulong m_SteamLeaderboardEntries;
	}
}
