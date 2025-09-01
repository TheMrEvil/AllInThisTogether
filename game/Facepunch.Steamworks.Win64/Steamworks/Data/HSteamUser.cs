using System;

namespace Steamworks.Data
{
	// Token: 0x020001C7 RID: 455
	internal struct HSteamUser : IEquatable<HSteamUser>, IComparable<HSteamUser>
	{
		// Token: 0x06000E63 RID: 3683 RVA: 0x00018334 File Offset: 0x00016534
		public static implicit operator HSteamUser(int value)
		{
			return new HSteamUser
			{
				Value = value
			};
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00018352 File Offset: 0x00016552
		public static implicit operator int(HSteamUser value)
		{
			return value.Value;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0001835A File Offset: 0x0001655A
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00018367 File Offset: 0x00016567
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00018374 File Offset: 0x00016574
		public override bool Equals(object p)
		{
			return this.Equals((HSteamUser)p);
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00018382 File Offset: 0x00016582
		public bool Equals(HSteamUser p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00018392 File Offset: 0x00016592
		public static bool operator ==(HSteamUser a, HSteamUser b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0001839C File Offset: 0x0001659C
		public static bool operator !=(HSteamUser a, HSteamUser b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x000183A9 File Offset: 0x000165A9
		public int CompareTo(HSteamUser other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BB6 RID: 2998
		public int Value;
	}
}
