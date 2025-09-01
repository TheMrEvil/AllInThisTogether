using System;

namespace Steamworks
{
	// Token: 0x020001B2 RID: 434
	[Serializable]
	public struct HSteamNetPollGroup : IEquatable<HSteamNetPollGroup>, IComparable<HSteamNetPollGroup>
	{
		// Token: 0x06000A72 RID: 2674 RVA: 0x0000F69C File Offset: 0x0000D89C
		public HSteamNetPollGroup(uint value)
		{
			this.m_HSteamNetPollGroup = value;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0000F6A5 File Offset: 0x0000D8A5
		public override string ToString()
		{
			return this.m_HSteamNetPollGroup.ToString();
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0000F6B2 File Offset: 0x0000D8B2
		public override bool Equals(object other)
		{
			return other is HSteamNetPollGroup && this == (HSteamNetPollGroup)other;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0000F6CF File Offset: 0x0000D8CF
		public override int GetHashCode()
		{
			return this.m_HSteamNetPollGroup.GetHashCode();
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0000F6DC File Offset: 0x0000D8DC
		public static bool operator ==(HSteamNetPollGroup x, HSteamNetPollGroup y)
		{
			return x.m_HSteamNetPollGroup == y.m_HSteamNetPollGroup;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0000F6EC File Offset: 0x0000D8EC
		public static bool operator !=(HSteamNetPollGroup x, HSteamNetPollGroup y)
		{
			return !(x == y);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
		public static explicit operator HSteamNetPollGroup(uint value)
		{
			return new HSteamNetPollGroup(value);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0000F700 File Offset: 0x0000D900
		public static explicit operator uint(HSteamNetPollGroup that)
		{
			return that.m_HSteamNetPollGroup;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0000F708 File Offset: 0x0000D908
		public bool Equals(HSteamNetPollGroup other)
		{
			return this.m_HSteamNetPollGroup == other.m_HSteamNetPollGroup;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0000F718 File Offset: 0x0000D918
		public int CompareTo(HSteamNetPollGroup other)
		{
			return this.m_HSteamNetPollGroup.CompareTo(other.m_HSteamNetPollGroup);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0000F72B File Offset: 0x0000D92B
		// Note: this type is marked as 'beforefieldinit'.
		static HSteamNetPollGroup()
		{
		}

		// Token: 0x04000ACA RID: 2762
		public static readonly HSteamNetPollGroup Invalid = new HSteamNetPollGroup(0U);

		// Token: 0x04000ACB RID: 2763
		public uint m_HSteamNetPollGroup;
	}
}
