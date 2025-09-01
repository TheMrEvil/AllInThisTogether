using System;

namespace Steamworks.Data
{
	// Token: 0x020001E4 RID: 484
	internal struct SteamInventoryUpdateHandle_t : IEquatable<SteamInventoryUpdateHandle_t>, IComparable<SteamInventoryUpdateHandle_t>
	{
		// Token: 0x06000F68 RID: 3944 RVA: 0x000192B8 File Offset: 0x000174B8
		public static implicit operator SteamInventoryUpdateHandle_t(ulong value)
		{
			return new SteamInventoryUpdateHandle_t
			{
				Value = value
			};
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x000192D6 File Offset: 0x000174D6
		public static implicit operator ulong(SteamInventoryUpdateHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x000192DE File Offset: 0x000174DE
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x000192EB File Offset: 0x000174EB
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x000192F8 File Offset: 0x000174F8
		public override bool Equals(object p)
		{
			return this.Equals((SteamInventoryUpdateHandle_t)p);
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00019306 File Offset: 0x00017506
		public bool Equals(SteamInventoryUpdateHandle_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00019316 File Offset: 0x00017516
		public static bool operator ==(SteamInventoryUpdateHandle_t a, SteamInventoryUpdateHandle_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00019320 File Offset: 0x00017520
		public static bool operator !=(SteamInventoryUpdateHandle_t a, SteamInventoryUpdateHandle_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0001932D File Offset: 0x0001752D
		public int CompareTo(SteamInventoryUpdateHandle_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BD3 RID: 3027
		public ulong Value;
	}
}
