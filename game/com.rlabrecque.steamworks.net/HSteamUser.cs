using System;

namespace Steamworks
{
	// Token: 0x020001CC RID: 460
	[Serializable]
	public struct HSteamUser : IEquatable<HSteamUser>, IComparable<HSteamUser>
	{
		// Token: 0x06000B76 RID: 2934 RVA: 0x0001070D File Offset: 0x0000E90D
		public HSteamUser(int value)
		{
			this.m_HSteamUser = value;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00010716 File Offset: 0x0000E916
		public override string ToString()
		{
			return this.m_HSteamUser.ToString();
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00010723 File Offset: 0x0000E923
		public override bool Equals(object other)
		{
			return other is HSteamUser && this == (HSteamUser)other;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00010740 File Offset: 0x0000E940
		public override int GetHashCode()
		{
			return this.m_HSteamUser.GetHashCode();
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0001074D File Offset: 0x0000E94D
		public static bool operator ==(HSteamUser x, HSteamUser y)
		{
			return x.m_HSteamUser == y.m_HSteamUser;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0001075D File Offset: 0x0000E95D
		public static bool operator !=(HSteamUser x, HSteamUser y)
		{
			return !(x == y);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00010769 File Offset: 0x0000E969
		public static explicit operator HSteamUser(int value)
		{
			return new HSteamUser(value);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00010771 File Offset: 0x0000E971
		public static explicit operator int(HSteamUser that)
		{
			return that.m_HSteamUser;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00010779 File Offset: 0x0000E979
		public bool Equals(HSteamUser other)
		{
			return this.m_HSteamUser == other.m_HSteamUser;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00010789 File Offset: 0x0000E989
		public int CompareTo(HSteamUser other)
		{
			return this.m_HSteamUser.CompareTo(other.m_HSteamUser);
		}

		// Token: 0x04000B09 RID: 2825
		public int m_HSteamUser;
	}
}
