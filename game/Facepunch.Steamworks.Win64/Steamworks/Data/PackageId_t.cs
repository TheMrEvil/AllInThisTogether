using System;

namespace Steamworks.Data
{
	// Token: 0x020001B7 RID: 439
	internal struct PackageId_t : IEquatable<PackageId_t>, IComparable<PackageId_t>
	{
		// Token: 0x06000DD3 RID: 3539 RVA: 0x00017A98 File Offset: 0x00015C98
		public static implicit operator PackageId_t(uint value)
		{
			return new PackageId_t
			{
				Value = value
			};
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00017AB6 File Offset: 0x00015CB6
		public static implicit operator uint(PackageId_t value)
		{
			return value.Value;
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00017ABE File Offset: 0x00015CBE
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00017ACB File Offset: 0x00015CCB
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00017AD8 File Offset: 0x00015CD8
		public override bool Equals(object p)
		{
			return this.Equals((PackageId_t)p);
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00017AE6 File Offset: 0x00015CE6
		public bool Equals(PackageId_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00017AF6 File Offset: 0x00015CF6
		public static bool operator ==(PackageId_t a, PackageId_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00017B00 File Offset: 0x00015D00
		public static bool operator !=(PackageId_t a, PackageId_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00017B0D File Offset: 0x00015D0D
		public int CompareTo(PackageId_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BA6 RID: 2982
		public uint Value;
	}
}
