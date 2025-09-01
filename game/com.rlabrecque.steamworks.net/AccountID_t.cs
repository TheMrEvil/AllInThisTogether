using System;

namespace Steamworks
{
	// Token: 0x020001C0 RID: 448
	[Serializable]
	public struct AccountID_t : IEquatable<AccountID_t>, IComparable<AccountID_t>
	{
		// Token: 0x06000AFD RID: 2813 RVA: 0x0000FEB5 File Offset: 0x0000E0B5
		public AccountID_t(uint value)
		{
			this.m_AccountID = value;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0000FEBE File Offset: 0x0000E0BE
		public override string ToString()
		{
			return this.m_AccountID.ToString();
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0000FECB File Offset: 0x0000E0CB
		public override bool Equals(object other)
		{
			return other is AccountID_t && this == (AccountID_t)other;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0000FEE8 File Offset: 0x0000E0E8
		public override int GetHashCode()
		{
			return this.m_AccountID.GetHashCode();
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0000FEF5 File Offset: 0x0000E0F5
		public static bool operator ==(AccountID_t x, AccountID_t y)
		{
			return x.m_AccountID == y.m_AccountID;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0000FF05 File Offset: 0x0000E105
		public static bool operator !=(AccountID_t x, AccountID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0000FF11 File Offset: 0x0000E111
		public static explicit operator AccountID_t(uint value)
		{
			return new AccountID_t(value);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0000FF19 File Offset: 0x0000E119
		public static explicit operator uint(AccountID_t that)
		{
			return that.m_AccountID;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0000FF21 File Offset: 0x0000E121
		public bool Equals(AccountID_t other)
		{
			return this.m_AccountID == other.m_AccountID;
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0000FF31 File Offset: 0x0000E131
		public int CompareTo(AccountID_t other)
		{
			return this.m_AccountID.CompareTo(other.m_AccountID);
		}

		// Token: 0x04000AF5 RID: 2805
		public uint m_AccountID;
	}
}
