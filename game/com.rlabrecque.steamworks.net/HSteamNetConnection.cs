using System;

namespace Steamworks
{
	// Token: 0x020001B1 RID: 433
	[Serializable]
	public struct HSteamNetConnection : IEquatable<HSteamNetConnection>, IComparable<HSteamNetConnection>
	{
		// Token: 0x06000A67 RID: 2663 RVA: 0x0000F600 File Offset: 0x0000D800
		public HSteamNetConnection(uint value)
		{
			this.m_HSteamNetConnection = value;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0000F609 File Offset: 0x0000D809
		public override string ToString()
		{
			return this.m_HSteamNetConnection.ToString();
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0000F616 File Offset: 0x0000D816
		public override bool Equals(object other)
		{
			return other is HSteamNetConnection && this == (HSteamNetConnection)other;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0000F633 File Offset: 0x0000D833
		public override int GetHashCode()
		{
			return this.m_HSteamNetConnection.GetHashCode();
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0000F640 File Offset: 0x0000D840
		public static bool operator ==(HSteamNetConnection x, HSteamNetConnection y)
		{
			return x.m_HSteamNetConnection == y.m_HSteamNetConnection;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0000F650 File Offset: 0x0000D850
		public static bool operator !=(HSteamNetConnection x, HSteamNetConnection y)
		{
			return !(x == y);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0000F65C File Offset: 0x0000D85C
		public static explicit operator HSteamNetConnection(uint value)
		{
			return new HSteamNetConnection(value);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0000F664 File Offset: 0x0000D864
		public static explicit operator uint(HSteamNetConnection that)
		{
			return that.m_HSteamNetConnection;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0000F66C File Offset: 0x0000D86C
		public bool Equals(HSteamNetConnection other)
		{
			return this.m_HSteamNetConnection == other.m_HSteamNetConnection;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0000F67C File Offset: 0x0000D87C
		public int CompareTo(HSteamNetConnection other)
		{
			return this.m_HSteamNetConnection.CompareTo(other.m_HSteamNetConnection);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0000F68F File Offset: 0x0000D88F
		// Note: this type is marked as 'beforefieldinit'.
		static HSteamNetConnection()
		{
		}

		// Token: 0x04000AC8 RID: 2760
		public static readonly HSteamNetConnection Invalid = new HSteamNetConnection(0U);

		// Token: 0x04000AC9 RID: 2761
		public uint m_HSteamNetConnection;
	}
}
