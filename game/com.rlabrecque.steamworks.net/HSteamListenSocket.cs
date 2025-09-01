using System;

namespace Steamworks
{
	// Token: 0x020001B0 RID: 432
	[Serializable]
	public struct HSteamListenSocket : IEquatable<HSteamListenSocket>, IComparable<HSteamListenSocket>
	{
		// Token: 0x06000A5C RID: 2652 RVA: 0x0000F564 File Offset: 0x0000D764
		public HSteamListenSocket(uint value)
		{
			this.m_HSteamListenSocket = value;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0000F56D File Offset: 0x0000D76D
		public override string ToString()
		{
			return this.m_HSteamListenSocket.ToString();
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0000F57A File Offset: 0x0000D77A
		public override bool Equals(object other)
		{
			return other is HSteamListenSocket && this == (HSteamListenSocket)other;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0000F597 File Offset: 0x0000D797
		public override int GetHashCode()
		{
			return this.m_HSteamListenSocket.GetHashCode();
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0000F5A4 File Offset: 0x0000D7A4
		public static bool operator ==(HSteamListenSocket x, HSteamListenSocket y)
		{
			return x.m_HSteamListenSocket == y.m_HSteamListenSocket;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0000F5B4 File Offset: 0x0000D7B4
		public static bool operator !=(HSteamListenSocket x, HSteamListenSocket y)
		{
			return !(x == y);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0000F5C0 File Offset: 0x0000D7C0
		public static explicit operator HSteamListenSocket(uint value)
		{
			return new HSteamListenSocket(value);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0000F5C8 File Offset: 0x0000D7C8
		public static explicit operator uint(HSteamListenSocket that)
		{
			return that.m_HSteamListenSocket;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0000F5D0 File Offset: 0x0000D7D0
		public bool Equals(HSteamListenSocket other)
		{
			return this.m_HSteamListenSocket == other.m_HSteamListenSocket;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0000F5E0 File Offset: 0x0000D7E0
		public int CompareTo(HSteamListenSocket other)
		{
			return this.m_HSteamListenSocket.CompareTo(other.m_HSteamListenSocket);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0000F5F3 File Offset: 0x0000D7F3
		// Note: this type is marked as 'beforefieldinit'.
		static HSteamListenSocket()
		{
		}

		// Token: 0x04000AC6 RID: 2758
		public static readonly HSteamListenSocket Invalid = new HSteamListenSocket(0U);

		// Token: 0x04000AC7 RID: 2759
		public uint m_HSteamListenSocket;
	}
}
