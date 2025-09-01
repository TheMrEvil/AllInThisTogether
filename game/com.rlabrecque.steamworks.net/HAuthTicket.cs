using System;

namespace Steamworks
{
	// Token: 0x02000198 RID: 408
	[Serializable]
	public struct HAuthTicket : IEquatable<HAuthTicket>, IComparable<HAuthTicket>
	{
		// Token: 0x0600099B RID: 2459 RVA: 0x0000EB24 File Offset: 0x0000CD24
		public HAuthTicket(uint value)
		{
			this.m_HAuthTicket = value;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0000EB2D File Offset: 0x0000CD2D
		public override string ToString()
		{
			return this.m_HAuthTicket.ToString();
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0000EB3A File Offset: 0x0000CD3A
		public override bool Equals(object other)
		{
			return other is HAuthTicket && this == (HAuthTicket)other;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0000EB57 File Offset: 0x0000CD57
		public override int GetHashCode()
		{
			return this.m_HAuthTicket.GetHashCode();
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0000EB64 File Offset: 0x0000CD64
		public static bool operator ==(HAuthTicket x, HAuthTicket y)
		{
			return x.m_HAuthTicket == y.m_HAuthTicket;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0000EB74 File Offset: 0x0000CD74
		public static bool operator !=(HAuthTicket x, HAuthTicket y)
		{
			return !(x == y);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0000EB80 File Offset: 0x0000CD80
		public static explicit operator HAuthTicket(uint value)
		{
			return new HAuthTicket(value);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0000EB88 File Offset: 0x0000CD88
		public static explicit operator uint(HAuthTicket that)
		{
			return that.m_HAuthTicket;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0000EB90 File Offset: 0x0000CD90
		public bool Equals(HAuthTicket other)
		{
			return this.m_HAuthTicket == other.m_HAuthTicket;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0000EBA0 File Offset: 0x0000CDA0
		public int CompareTo(HAuthTicket other)
		{
			return this.m_HAuthTicket.CompareTo(other.m_HAuthTicket);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0000EBB3 File Offset: 0x0000CDB3
		// Note: this type is marked as 'beforefieldinit'.
		static HAuthTicket()
		{
		}

		// Token: 0x04000A9C RID: 2716
		public static readonly HAuthTicket Invalid = new HAuthTicket(0U);

		// Token: 0x04000A9D RID: 2717
		public uint m_HAuthTicket;
	}
}
