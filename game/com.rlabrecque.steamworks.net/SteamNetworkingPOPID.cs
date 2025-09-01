using System;

namespace Steamworks
{
	// Token: 0x020001B9 RID: 441
	[Serializable]
	public struct SteamNetworkingPOPID : IEquatable<SteamNetworkingPOPID>, IComparable<SteamNetworkingPOPID>
	{
		// Token: 0x06000AB2 RID: 2738 RVA: 0x0000FA87 File Offset: 0x0000DC87
		public SteamNetworkingPOPID(uint value)
		{
			this.m_SteamNetworkingPOPID = value;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public override string ToString()
		{
			return this.m_SteamNetworkingPOPID.ToString();
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0000FA9D File Offset: 0x0000DC9D
		public override bool Equals(object other)
		{
			return other is SteamNetworkingPOPID && this == (SteamNetworkingPOPID)other;
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0000FABA File Offset: 0x0000DCBA
		public override int GetHashCode()
		{
			return this.m_SteamNetworkingPOPID.GetHashCode();
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0000FAC7 File Offset: 0x0000DCC7
		public static bool operator ==(SteamNetworkingPOPID x, SteamNetworkingPOPID y)
		{
			return x.m_SteamNetworkingPOPID == y.m_SteamNetworkingPOPID;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0000FAD7 File Offset: 0x0000DCD7
		public static bool operator !=(SteamNetworkingPOPID x, SteamNetworkingPOPID y)
		{
			return !(x == y);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0000FAE3 File Offset: 0x0000DCE3
		public static explicit operator SteamNetworkingPOPID(uint value)
		{
			return new SteamNetworkingPOPID(value);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0000FAEB File Offset: 0x0000DCEB
		public static explicit operator uint(SteamNetworkingPOPID that)
		{
			return that.m_SteamNetworkingPOPID;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0000FAF3 File Offset: 0x0000DCF3
		public bool Equals(SteamNetworkingPOPID other)
		{
			return this.m_SteamNetworkingPOPID == other.m_SteamNetworkingPOPID;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0000FB03 File Offset: 0x0000DD03
		public int CompareTo(SteamNetworkingPOPID other)
		{
			return this.m_SteamNetworkingPOPID.CompareTo(other.m_SteamNetworkingPOPID);
		}

		// Token: 0x04000AE9 RID: 2793
		public uint m_SteamNetworkingPOPID;
	}
}
