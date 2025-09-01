using System;

namespace Steamworks.Data
{
	// Token: 0x020001B5 RID: 437
	internal struct JobID_t : IEquatable<JobID_t>, IComparable<JobID_t>
	{
		// Token: 0x06000DC1 RID: 3521 RVA: 0x00017988 File Offset: 0x00015B88
		public static implicit operator JobID_t(ulong value)
		{
			return new JobID_t
			{
				Value = value
			};
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x000179A6 File Offset: 0x00015BA6
		public static implicit operator ulong(JobID_t value)
		{
			return value.Value;
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x000179AE File Offset: 0x00015BAE
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x000179BB File Offset: 0x00015BBB
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x000179C8 File Offset: 0x00015BC8
		public override bool Equals(object p)
		{
			return this.Equals((JobID_t)p);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x000179D6 File Offset: 0x00015BD6
		public bool Equals(JobID_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x000179E6 File Offset: 0x00015BE6
		public static bool operator ==(JobID_t a, JobID_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x000179F0 File Offset: 0x00015BF0
		public static bool operator !=(JobID_t a, JobID_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x000179FD File Offset: 0x00015BFD
		public int CompareTo(JobID_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BA4 RID: 2980
		public ulong Value;
	}
}
