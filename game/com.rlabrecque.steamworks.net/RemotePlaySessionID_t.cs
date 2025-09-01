using System;

namespace Steamworks
{
	// Token: 0x020001BA RID: 442
	[Serializable]
	public struct RemotePlaySessionID_t : IEquatable<RemotePlaySessionID_t>, IComparable<RemotePlaySessionID_t>
	{
		// Token: 0x06000ABC RID: 2748 RVA: 0x0000FB16 File Offset: 0x0000DD16
		public RemotePlaySessionID_t(uint value)
		{
			this.m_RemotePlaySessionID = value;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0000FB1F File Offset: 0x0000DD1F
		public override string ToString()
		{
			return this.m_RemotePlaySessionID.ToString();
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0000FB2C File Offset: 0x0000DD2C
		public override bool Equals(object other)
		{
			return other is RemotePlaySessionID_t && this == (RemotePlaySessionID_t)other;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0000FB49 File Offset: 0x0000DD49
		public override int GetHashCode()
		{
			return this.m_RemotePlaySessionID.GetHashCode();
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0000FB56 File Offset: 0x0000DD56
		public static bool operator ==(RemotePlaySessionID_t x, RemotePlaySessionID_t y)
		{
			return x.m_RemotePlaySessionID == y.m_RemotePlaySessionID;
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0000FB66 File Offset: 0x0000DD66
		public static bool operator !=(RemotePlaySessionID_t x, RemotePlaySessionID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0000FB72 File Offset: 0x0000DD72
		public static explicit operator RemotePlaySessionID_t(uint value)
		{
			return new RemotePlaySessionID_t(value);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0000FB7A File Offset: 0x0000DD7A
		public static explicit operator uint(RemotePlaySessionID_t that)
		{
			return that.m_RemotePlaySessionID;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0000FB82 File Offset: 0x0000DD82
		public bool Equals(RemotePlaySessionID_t other)
		{
			return this.m_RemotePlaySessionID == other.m_RemotePlaySessionID;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0000FB92 File Offset: 0x0000DD92
		public int CompareTo(RemotePlaySessionID_t other)
		{
			return this.m_RemotePlaySessionID.CompareTo(other.m_RemotePlaySessionID);
		}

		// Token: 0x04000AEA RID: 2794
		public uint m_RemotePlaySessionID;
	}
}
