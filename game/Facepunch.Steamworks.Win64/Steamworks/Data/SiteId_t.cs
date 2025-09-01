using System;

namespace Steamworks.Data
{
	// Token: 0x020001C2 RID: 450
	internal struct SiteId_t : IEquatable<SiteId_t>, IComparable<SiteId_t>
	{
		// Token: 0x06000E36 RID: 3638 RVA: 0x00018070 File Offset: 0x00016270
		public static implicit operator SiteId_t(ulong value)
		{
			return new SiteId_t
			{
				Value = value
			};
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0001808E File Offset: 0x0001628E
		public static implicit operator ulong(SiteId_t value)
		{
			return value.Value;
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00018096 File Offset: 0x00016296
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x000180A3 File Offset: 0x000162A3
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x000180B0 File Offset: 0x000162B0
		public override bool Equals(object p)
		{
			return this.Equals((SiteId_t)p);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x000180BE File Offset: 0x000162BE
		public bool Equals(SiteId_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x000180CE File Offset: 0x000162CE
		public static bool operator ==(SiteId_t a, SiteId_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x000180D8 File Offset: 0x000162D8
		public static bool operator !=(SiteId_t a, SiteId_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x000180E5 File Offset: 0x000162E5
		public int CompareTo(SiteId_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BB1 RID: 2993
		public ulong Value;
	}
}
