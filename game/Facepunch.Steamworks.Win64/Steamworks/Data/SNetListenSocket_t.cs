using System;

namespace Steamworks.Data
{
	// Token: 0x020001D2 RID: 466
	internal struct SNetListenSocket_t : IEquatable<SNetListenSocket_t>, IComparable<SNetListenSocket_t>
	{
		// Token: 0x06000EC6 RID: 3782 RVA: 0x00018928 File Offset: 0x00016B28
		public static implicit operator SNetListenSocket_t(uint value)
		{
			return new SNetListenSocket_t
			{
				Value = value
			};
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00018946 File Offset: 0x00016B46
		public static implicit operator uint(SNetListenSocket_t value)
		{
			return value.Value;
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0001894E File Offset: 0x00016B4E
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0001895B File Offset: 0x00016B5B
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00018968 File Offset: 0x00016B68
		public override bool Equals(object p)
		{
			return this.Equals((SNetListenSocket_t)p);
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00018976 File Offset: 0x00016B76
		public bool Equals(SNetListenSocket_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x00018986 File Offset: 0x00016B86
		public static bool operator ==(SNetListenSocket_t a, SNetListenSocket_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00018990 File Offset: 0x00016B90
		public static bool operator !=(SNetListenSocket_t a, SNetListenSocket_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0001899D File Offset: 0x00016B9D
		public int CompareTo(SNetListenSocket_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BC1 RID: 3009
		public uint Value;
	}
}
