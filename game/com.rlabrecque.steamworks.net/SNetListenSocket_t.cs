using System;

namespace Steamworks
{
	// Token: 0x020001AB RID: 427
	[Serializable]
	public struct SNetListenSocket_t : IEquatable<SNetListenSocket_t>, IComparable<SNetListenSocket_t>
	{
		// Token: 0x06000A40 RID: 2624 RVA: 0x0000F41B File Offset: 0x0000D61B
		public SNetListenSocket_t(uint value)
		{
			this.m_SNetListenSocket = value;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0000F424 File Offset: 0x0000D624
		public override string ToString()
		{
			return this.m_SNetListenSocket.ToString();
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0000F431 File Offset: 0x0000D631
		public override bool Equals(object other)
		{
			return other is SNetListenSocket_t && this == (SNetListenSocket_t)other;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0000F44E File Offset: 0x0000D64E
		public override int GetHashCode()
		{
			return this.m_SNetListenSocket.GetHashCode();
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0000F45B File Offset: 0x0000D65B
		public static bool operator ==(SNetListenSocket_t x, SNetListenSocket_t y)
		{
			return x.m_SNetListenSocket == y.m_SNetListenSocket;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0000F46B File Offset: 0x0000D66B
		public static bool operator !=(SNetListenSocket_t x, SNetListenSocket_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0000F477 File Offset: 0x0000D677
		public static explicit operator SNetListenSocket_t(uint value)
		{
			return new SNetListenSocket_t(value);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0000F47F File Offset: 0x0000D67F
		public static explicit operator uint(SNetListenSocket_t that)
		{
			return that.m_SNetListenSocket;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0000F487 File Offset: 0x0000D687
		public bool Equals(SNetListenSocket_t other)
		{
			return this.m_SNetListenSocket == other.m_SNetListenSocket;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0000F497 File Offset: 0x0000D697
		public int CompareTo(SNetListenSocket_t other)
		{
			return this.m_SNetListenSocket.CompareTo(other.m_SNetListenSocket);
		}

		// Token: 0x04000AC4 RID: 2756
		public uint m_SNetListenSocket;
	}
}
