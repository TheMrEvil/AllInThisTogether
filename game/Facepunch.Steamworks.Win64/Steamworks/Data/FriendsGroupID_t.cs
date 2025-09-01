using System;

namespace Steamworks.Data
{
	// Token: 0x020001C8 RID: 456
	internal struct FriendsGroupID_t : IEquatable<FriendsGroupID_t>, IComparable<FriendsGroupID_t>
	{
		// Token: 0x06000E6C RID: 3692 RVA: 0x000183BC File Offset: 0x000165BC
		public static implicit operator FriendsGroupID_t(short value)
		{
			return new FriendsGroupID_t
			{
				Value = value
			};
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x000183DA File Offset: 0x000165DA
		public static implicit operator short(FriendsGroupID_t value)
		{
			return value.Value;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x000183E2 File Offset: 0x000165E2
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x000183EF File Offset: 0x000165EF
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x000183FC File Offset: 0x000165FC
		public override bool Equals(object p)
		{
			return this.Equals((FriendsGroupID_t)p);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0001840A File Offset: 0x0001660A
		public bool Equals(FriendsGroupID_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0001841A File Offset: 0x0001661A
		public static bool operator ==(FriendsGroupID_t a, FriendsGroupID_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00018424 File Offset: 0x00016624
		public static bool operator !=(FriendsGroupID_t a, FriendsGroupID_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00018431 File Offset: 0x00016631
		public int CompareTo(FriendsGroupID_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BB7 RID: 2999
		public short Value;
	}
}
