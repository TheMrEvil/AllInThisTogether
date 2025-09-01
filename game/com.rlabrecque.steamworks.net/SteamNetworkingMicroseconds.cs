using System;

namespace Steamworks
{
	// Token: 0x020001B8 RID: 440
	[Serializable]
	public struct SteamNetworkingMicroseconds : IEquatable<SteamNetworkingMicroseconds>, IComparable<SteamNetworkingMicroseconds>
	{
		// Token: 0x06000AA8 RID: 2728 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		public SteamNetworkingMicroseconds(long value)
		{
			this.m_SteamNetworkingMicroseconds = value;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0000FA01 File Offset: 0x0000DC01
		public override string ToString()
		{
			return this.m_SteamNetworkingMicroseconds.ToString();
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0000FA0E File Offset: 0x0000DC0E
		public override bool Equals(object other)
		{
			return other is SteamNetworkingMicroseconds && this == (SteamNetworkingMicroseconds)other;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0000FA2B File Offset: 0x0000DC2B
		public override int GetHashCode()
		{
			return this.m_SteamNetworkingMicroseconds.GetHashCode();
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0000FA38 File Offset: 0x0000DC38
		public static bool operator ==(SteamNetworkingMicroseconds x, SteamNetworkingMicroseconds y)
		{
			return x.m_SteamNetworkingMicroseconds == y.m_SteamNetworkingMicroseconds;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0000FA48 File Offset: 0x0000DC48
		public static bool operator !=(SteamNetworkingMicroseconds x, SteamNetworkingMicroseconds y)
		{
			return !(x == y);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0000FA54 File Offset: 0x0000DC54
		public static explicit operator SteamNetworkingMicroseconds(long value)
		{
			return new SteamNetworkingMicroseconds(value);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0000FA5C File Offset: 0x0000DC5C
		public static explicit operator long(SteamNetworkingMicroseconds that)
		{
			return that.m_SteamNetworkingMicroseconds;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0000FA64 File Offset: 0x0000DC64
		public bool Equals(SteamNetworkingMicroseconds other)
		{
			return this.m_SteamNetworkingMicroseconds == other.m_SteamNetworkingMicroseconds;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0000FA74 File Offset: 0x0000DC74
		public int CompareTo(SteamNetworkingMicroseconds other)
		{
			return this.m_SteamNetworkingMicroseconds.CompareTo(other.m_SteamNetworkingMicroseconds);
		}

		// Token: 0x04000AE8 RID: 2792
		public long m_SteamNetworkingMicroseconds;
	}
}
