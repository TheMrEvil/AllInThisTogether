using System;

namespace Steamworks.Data
{
	// Token: 0x020001E7 RID: 487
	internal struct SteamNetworkingPOPID : IEquatable<SteamNetworkingPOPID>, IComparable<SteamNetworkingPOPID>
	{
		// Token: 0x06000F83 RID: 3971 RVA: 0x00019450 File Offset: 0x00017650
		public static implicit operator SteamNetworkingPOPID(uint value)
		{
			return new SteamNetworkingPOPID
			{
				Value = value
			};
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0001946E File Offset: 0x0001766E
		public static implicit operator uint(SteamNetworkingPOPID value)
		{
			return value.Value;
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x00019476 File Offset: 0x00017676
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00019483 File Offset: 0x00017683
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00019490 File Offset: 0x00017690
		public override bool Equals(object p)
		{
			return this.Equals((SteamNetworkingPOPID)p);
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0001949E File Offset: 0x0001769E
		public bool Equals(SteamNetworkingPOPID p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x000194AE File Offset: 0x000176AE
		public static bool operator ==(SteamNetworkingPOPID a, SteamNetworkingPOPID b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x000194B8 File Offset: 0x000176B8
		public static bool operator !=(SteamNetworkingPOPID a, SteamNetworkingPOPID b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x000194C5 File Offset: 0x000176C5
		public int CompareTo(SteamNetworkingPOPID other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BD6 RID: 3030
		public uint Value;
	}
}
