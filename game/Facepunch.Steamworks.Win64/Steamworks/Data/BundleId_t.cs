using System;

namespace Steamworks.Data
{
	// Token: 0x020001B8 RID: 440
	internal struct BundleId_t : IEquatable<BundleId_t>, IComparable<BundleId_t>
	{
		// Token: 0x06000DDC RID: 3548 RVA: 0x00017B20 File Offset: 0x00015D20
		public static implicit operator BundleId_t(uint value)
		{
			return new BundleId_t
			{
				Value = value
			};
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00017B3E File Offset: 0x00015D3E
		public static implicit operator uint(BundleId_t value)
		{
			return value.Value;
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00017B46 File Offset: 0x00015D46
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00017B53 File Offset: 0x00015D53
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00017B60 File Offset: 0x00015D60
		public override bool Equals(object p)
		{
			return this.Equals((BundleId_t)p);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00017B6E File Offset: 0x00015D6E
		public bool Equals(BundleId_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00017B7E File Offset: 0x00015D7E
		public static bool operator ==(BundleId_t a, BundleId_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00017B88 File Offset: 0x00015D88
		public static bool operator !=(BundleId_t a, BundleId_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00017B95 File Offset: 0x00015D95
		public int CompareTo(BundleId_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BA7 RID: 2983
		public uint Value;
	}
}
