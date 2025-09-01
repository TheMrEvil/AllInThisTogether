using System;

namespace Steamworks.Data
{
	// Token: 0x020001BA RID: 442
	internal struct PhysicalItemId_t : IEquatable<PhysicalItemId_t>, IComparable<PhysicalItemId_t>
	{
		// Token: 0x06000DEE RID: 3566 RVA: 0x00017C30 File Offset: 0x00015E30
		public static implicit operator PhysicalItemId_t(uint value)
		{
			return new PhysicalItemId_t
			{
				Value = value
			};
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00017C4E File Offset: 0x00015E4E
		public static implicit operator uint(PhysicalItemId_t value)
		{
			return value.Value;
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00017C56 File Offset: 0x00015E56
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00017C63 File Offset: 0x00015E63
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00017C70 File Offset: 0x00015E70
		public override bool Equals(object p)
		{
			return this.Equals((PhysicalItemId_t)p);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00017C7E File Offset: 0x00015E7E
		public bool Equals(PhysicalItemId_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00017C8E File Offset: 0x00015E8E
		public static bool operator ==(PhysicalItemId_t a, PhysicalItemId_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00017C98 File Offset: 0x00015E98
		public static bool operator !=(PhysicalItemId_t a, PhysicalItemId_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00017CA5 File Offset: 0x00015EA5
		public int CompareTo(PhysicalItemId_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BA9 RID: 2985
		public uint Value;
	}
}
