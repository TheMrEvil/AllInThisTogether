using System;

namespace Steamworks.Data
{
	// Token: 0x020001C0 RID: 448
	internal struct PartnerId_t : IEquatable<PartnerId_t>, IComparable<PartnerId_t>
	{
		// Token: 0x06000E24 RID: 3620 RVA: 0x00017F60 File Offset: 0x00016160
		public static implicit operator PartnerId_t(uint value)
		{
			return new PartnerId_t
			{
				Value = value
			};
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00017F7E File Offset: 0x0001617E
		public static implicit operator uint(PartnerId_t value)
		{
			return value.Value;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00017F86 File Offset: 0x00016186
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00017F93 File Offset: 0x00016193
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00017FA0 File Offset: 0x000161A0
		public override bool Equals(object p)
		{
			return this.Equals((PartnerId_t)p);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00017FAE File Offset: 0x000161AE
		public bool Equals(PartnerId_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00017FBE File Offset: 0x000161BE
		public static bool operator ==(PartnerId_t a, PartnerId_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00017FC8 File Offset: 0x000161C8
		public static bool operator !=(PartnerId_t a, PartnerId_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00017FD5 File Offset: 0x000161D5
		public int CompareTo(PartnerId_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BAF RID: 2991
		public uint Value;
	}
}
