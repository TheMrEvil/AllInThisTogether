using System;

namespace Steamworks.Data
{
	// Token: 0x020001D1 RID: 465
	internal struct SNetSocket_t : IEquatable<SNetSocket_t>, IComparable<SNetSocket_t>
	{
		// Token: 0x06000EBD RID: 3773 RVA: 0x000188A0 File Offset: 0x00016AA0
		public static implicit operator SNetSocket_t(uint value)
		{
			return new SNetSocket_t
			{
				Value = value
			};
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x000188BE File Offset: 0x00016ABE
		public static implicit operator uint(SNetSocket_t value)
		{
			return value.Value;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x000188C6 File Offset: 0x00016AC6
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x000188D3 File Offset: 0x00016AD3
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x000188E0 File Offset: 0x00016AE0
		public override bool Equals(object p)
		{
			return this.Equals((SNetSocket_t)p);
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x000188EE File Offset: 0x00016AEE
		public bool Equals(SNetSocket_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x000188FE File Offset: 0x00016AFE
		public static bool operator ==(SNetSocket_t a, SNetSocket_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00018908 File Offset: 0x00016B08
		public static bool operator !=(SNetSocket_t a, SNetSocket_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00018915 File Offset: 0x00016B15
		public int CompareTo(SNetSocket_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BC0 RID: 3008
		public uint Value;
	}
}
