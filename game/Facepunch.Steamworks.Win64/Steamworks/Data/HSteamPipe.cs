using System;

namespace Steamworks.Data
{
	// Token: 0x020001C6 RID: 454
	internal struct HSteamPipe : IEquatable<HSteamPipe>, IComparable<HSteamPipe>
	{
		// Token: 0x06000E5A RID: 3674 RVA: 0x000182AC File Offset: 0x000164AC
		public static implicit operator HSteamPipe(int value)
		{
			return new HSteamPipe
			{
				Value = value
			};
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x000182CA File Offset: 0x000164CA
		public static implicit operator int(HSteamPipe value)
		{
			return value.Value;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x000182D2 File Offset: 0x000164D2
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x000182DF File Offset: 0x000164DF
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000182EC File Offset: 0x000164EC
		public override bool Equals(object p)
		{
			return this.Equals((HSteamPipe)p);
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x000182FA File Offset: 0x000164FA
		public bool Equals(HSteamPipe p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0001830A File Offset: 0x0001650A
		public static bool operator ==(HSteamPipe a, HSteamPipe b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00018314 File Offset: 0x00016514
		public static bool operator !=(HSteamPipe a, HSteamPipe b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00018321 File Offset: 0x00016521
		public int CompareTo(HSteamPipe other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BB5 RID: 2997
		public int Value;
	}
}
