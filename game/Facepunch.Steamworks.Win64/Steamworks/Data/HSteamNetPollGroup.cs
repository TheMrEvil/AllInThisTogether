using System;

namespace Steamworks.Data
{
	// Token: 0x020001E6 RID: 486
	internal struct HSteamNetPollGroup : IEquatable<HSteamNetPollGroup>, IComparable<HSteamNetPollGroup>
	{
		// Token: 0x06000F7A RID: 3962 RVA: 0x000193C8 File Offset: 0x000175C8
		public static implicit operator HSteamNetPollGroup(uint value)
		{
			return new HSteamNetPollGroup
			{
				Value = value
			};
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x000193E6 File Offset: 0x000175E6
		public static implicit operator uint(HSteamNetPollGroup value)
		{
			return value.Value;
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x000193EE File Offset: 0x000175EE
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x000193FB File Offset: 0x000175FB
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00019408 File Offset: 0x00017608
		public override bool Equals(object p)
		{
			return this.Equals((HSteamNetPollGroup)p);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00019416 File Offset: 0x00017616
		public bool Equals(HSteamNetPollGroup p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x00019426 File Offset: 0x00017626
		public static bool operator ==(HSteamNetPollGroup a, HSteamNetPollGroup b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00019430 File Offset: 0x00017630
		public static bool operator !=(HSteamNetPollGroup a, HSteamNetPollGroup b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0001943D File Offset: 0x0001763D
		public int CompareTo(HSteamNetPollGroup other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BD5 RID: 3029
		public uint Value;
	}
}
