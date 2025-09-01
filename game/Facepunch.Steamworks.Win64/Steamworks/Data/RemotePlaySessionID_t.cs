using System;

namespace Steamworks.Data
{
	// Token: 0x020001E5 RID: 485
	internal struct RemotePlaySessionID_t : IEquatable<RemotePlaySessionID_t>, IComparable<RemotePlaySessionID_t>
	{
		// Token: 0x06000F71 RID: 3953 RVA: 0x00019340 File Offset: 0x00017540
		public static implicit operator RemotePlaySessionID_t(uint value)
		{
			return new RemotePlaySessionID_t
			{
				Value = value
			};
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0001935E File Offset: 0x0001755E
		public static implicit operator uint(RemotePlaySessionID_t value)
		{
			return value.Value;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x00019366 File Offset: 0x00017566
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x00019373 File Offset: 0x00017573
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00019380 File Offset: 0x00017580
		public override bool Equals(object p)
		{
			return this.Equals((RemotePlaySessionID_t)p);
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0001938E File Offset: 0x0001758E
		public bool Equals(RemotePlaySessionID_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0001939E File Offset: 0x0001759E
		public static bool operator ==(RemotePlaySessionID_t a, RemotePlaySessionID_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x000193A8 File Offset: 0x000175A8
		public static bool operator !=(RemotePlaySessionID_t a, RemotePlaySessionID_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x000193B5 File Offset: 0x000175B5
		public int CompareTo(RemotePlaySessionID_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BD4 RID: 3028
		public uint Value;
	}
}
