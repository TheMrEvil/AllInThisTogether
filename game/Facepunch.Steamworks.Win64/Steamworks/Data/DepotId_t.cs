using System;

namespace Steamworks.Data
{
	// Token: 0x020001BB RID: 443
	internal struct DepotId_t : IEquatable<DepotId_t>, IComparable<DepotId_t>
	{
		// Token: 0x06000DF7 RID: 3575 RVA: 0x00017CB8 File Offset: 0x00015EB8
		public static implicit operator DepotId_t(uint value)
		{
			return new DepotId_t
			{
				Value = value
			};
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00017CD6 File Offset: 0x00015ED6
		public static implicit operator uint(DepotId_t value)
		{
			return value.Value;
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00017CDE File Offset: 0x00015EDE
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00017CEB File Offset: 0x00015EEB
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00017CF8 File Offset: 0x00015EF8
		public override bool Equals(object p)
		{
			return this.Equals((DepotId_t)p);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00017D06 File Offset: 0x00015F06
		public bool Equals(DepotId_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00017D16 File Offset: 0x00015F16
		public static bool operator ==(DepotId_t a, DepotId_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00017D20 File Offset: 0x00015F20
		public static bool operator !=(DepotId_t a, DepotId_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00017D2D File Offset: 0x00015F2D
		public int CompareTo(DepotId_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BAA RID: 2986
		public uint Value;
	}
}
