using System;

namespace Steamworks
{
	// Token: 0x020001A8 RID: 424
	[Serializable]
	public struct SteamItemInstanceID_t : IEquatable<SteamItemInstanceID_t>, IComparable<SteamItemInstanceID_t>
	{
		// Token: 0x06000A20 RID: 2592 RVA: 0x0000F24F File Offset: 0x0000D44F
		public SteamItemInstanceID_t(ulong value)
		{
			this.m_SteamItemInstanceID = value;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0000F258 File Offset: 0x0000D458
		public override string ToString()
		{
			return this.m_SteamItemInstanceID.ToString();
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0000F265 File Offset: 0x0000D465
		public override bool Equals(object other)
		{
			return other is SteamItemInstanceID_t && this == (SteamItemInstanceID_t)other;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0000F282 File Offset: 0x0000D482
		public override int GetHashCode()
		{
			return this.m_SteamItemInstanceID.GetHashCode();
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0000F28F File Offset: 0x0000D48F
		public static bool operator ==(SteamItemInstanceID_t x, SteamItemInstanceID_t y)
		{
			return x.m_SteamItemInstanceID == y.m_SteamItemInstanceID;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0000F29F File Offset: 0x0000D49F
		public static bool operator !=(SteamItemInstanceID_t x, SteamItemInstanceID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0000F2AB File Offset: 0x0000D4AB
		public static explicit operator SteamItemInstanceID_t(ulong value)
		{
			return new SteamItemInstanceID_t(value);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0000F2B3 File Offset: 0x0000D4B3
		public static explicit operator ulong(SteamItemInstanceID_t that)
		{
			return that.m_SteamItemInstanceID;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0000F2BB File Offset: 0x0000D4BB
		public bool Equals(SteamItemInstanceID_t other)
		{
			return this.m_SteamItemInstanceID == other.m_SteamItemInstanceID;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0000F2CB File Offset: 0x0000D4CB
		public int CompareTo(SteamItemInstanceID_t other)
		{
			return this.m_SteamItemInstanceID.CompareTo(other.m_SteamItemInstanceID);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0000F2DE File Offset: 0x0000D4DE
		// Note: this type is marked as 'beforefieldinit'.
		static SteamItemInstanceID_t()
		{
		}

		// Token: 0x04000ABE RID: 2750
		public static readonly SteamItemInstanceID_t Invalid = new SteamItemInstanceID_t(ulong.MaxValue);

		// Token: 0x04000ABF RID: 2751
		public ulong m_SteamItemInstanceID;
	}
}
