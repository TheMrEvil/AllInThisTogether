using System;

namespace Steamworks.Data
{
	// Token: 0x020001BF RID: 447
	internal struct AccountID_t : IEquatable<AccountID_t>, IComparable<AccountID_t>
	{
		// Token: 0x06000E1B RID: 3611 RVA: 0x00017ED8 File Offset: 0x000160D8
		public static implicit operator AccountID_t(uint value)
		{
			return new AccountID_t
			{
				Value = value
			};
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00017EF6 File Offset: 0x000160F6
		public static implicit operator uint(AccountID_t value)
		{
			return value.Value;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00017EFE File Offset: 0x000160FE
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00017F0B File Offset: 0x0001610B
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00017F18 File Offset: 0x00016118
		public override bool Equals(object p)
		{
			return this.Equals((AccountID_t)p);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00017F26 File Offset: 0x00016126
		public bool Equals(AccountID_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00017F36 File Offset: 0x00016136
		public static bool operator ==(AccountID_t a, AccountID_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00017F40 File Offset: 0x00016140
		public static bool operator !=(AccountID_t a, AccountID_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00017F4D File Offset: 0x0001614D
		public int CompareTo(AccountID_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BAE RID: 2990
		public uint Value;
	}
}
