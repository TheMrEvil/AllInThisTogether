using System;

namespace Steamworks.Data
{
	// Token: 0x020001E3 RID: 483
	internal struct SteamInventoryResult_t : IEquatable<SteamInventoryResult_t>, IComparable<SteamInventoryResult_t>
	{
		// Token: 0x06000F5F RID: 3935 RVA: 0x00019230 File Offset: 0x00017430
		public static implicit operator SteamInventoryResult_t(int value)
		{
			return new SteamInventoryResult_t
			{
				Value = value
			};
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0001924E File Offset: 0x0001744E
		public static implicit operator int(SteamInventoryResult_t value)
		{
			return value.Value;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00019256 File Offset: 0x00017456
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x00019263 File Offset: 0x00017463
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00019270 File Offset: 0x00017470
		public override bool Equals(object p)
		{
			return this.Equals((SteamInventoryResult_t)p);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0001927E File Offset: 0x0001747E
		public bool Equals(SteamInventoryResult_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0001928E File Offset: 0x0001748E
		public static bool operator ==(SteamInventoryResult_t a, SteamInventoryResult_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00019298 File Offset: 0x00017498
		public static bool operator !=(SteamInventoryResult_t a, SteamInventoryResult_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x000192A5 File Offset: 0x000174A5
		public int CompareTo(SteamInventoryResult_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BD2 RID: 3026
		public int Value;
	}
}
