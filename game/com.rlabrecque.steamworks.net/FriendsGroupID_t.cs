using System;

namespace Steamworks
{
	// Token: 0x0200019B RID: 411
	[Serializable]
	public struct FriendsGroupID_t : IEquatable<FriendsGroupID_t>, IComparable<FriendsGroupID_t>
	{
		// Token: 0x060009A8 RID: 2472 RVA: 0x0000EBDB File Offset: 0x0000CDDB
		public FriendsGroupID_t(short value)
		{
			this.m_FriendsGroupID = value;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0000EBE4 File Offset: 0x0000CDE4
		public override string ToString()
		{
			return this.m_FriendsGroupID.ToString();
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0000EBF1 File Offset: 0x0000CDF1
		public override bool Equals(object other)
		{
			return other is FriendsGroupID_t && this == (FriendsGroupID_t)other;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0000EC0E File Offset: 0x0000CE0E
		public override int GetHashCode()
		{
			return this.m_FriendsGroupID.GetHashCode();
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0000EC1B File Offset: 0x0000CE1B
		public static bool operator ==(FriendsGroupID_t x, FriendsGroupID_t y)
		{
			return x.m_FriendsGroupID == y.m_FriendsGroupID;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0000EC2B File Offset: 0x0000CE2B
		public static bool operator !=(FriendsGroupID_t x, FriendsGroupID_t y)
		{
			return !(x == y);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0000EC37 File Offset: 0x0000CE37
		public static explicit operator FriendsGroupID_t(short value)
		{
			return new FriendsGroupID_t(value);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0000EC3F File Offset: 0x0000CE3F
		public static explicit operator short(FriendsGroupID_t that)
		{
			return that.m_FriendsGroupID;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0000EC47 File Offset: 0x0000CE47
		public bool Equals(FriendsGroupID_t other)
		{
			return this.m_FriendsGroupID == other.m_FriendsGroupID;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0000EC57 File Offset: 0x0000CE57
		public int CompareTo(FriendsGroupID_t other)
		{
			return this.m_FriendsGroupID.CompareTo(other.m_FriendsGroupID);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0000EC6A File Offset: 0x0000CE6A
		// Note: this type is marked as 'beforefieldinit'.
		static FriendsGroupID_t()
		{
		}

		// Token: 0x04000AAA RID: 2730
		public static readonly FriendsGroupID_t Invalid = new FriendsGroupID_t(-1);

		// Token: 0x04000AAB RID: 2731
		public short m_FriendsGroupID;
	}
}
