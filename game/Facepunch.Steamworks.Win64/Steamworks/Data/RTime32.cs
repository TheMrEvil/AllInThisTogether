using System;

namespace Steamworks.Data
{
	// Token: 0x020001BC RID: 444
	internal struct RTime32 : IEquatable<RTime32>, IComparable<RTime32>
	{
		// Token: 0x06000E00 RID: 3584 RVA: 0x00017D40 File Offset: 0x00015F40
		public static implicit operator RTime32(uint value)
		{
			return new RTime32
			{
				Value = value
			};
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00017D5E File Offset: 0x00015F5E
		public static implicit operator uint(RTime32 value)
		{
			return value.Value;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00017D66 File Offset: 0x00015F66
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00017D73 File Offset: 0x00015F73
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00017D80 File Offset: 0x00015F80
		public override bool Equals(object p)
		{
			return this.Equals((RTime32)p);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00017D8E File Offset: 0x00015F8E
		public bool Equals(RTime32 p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00017D9E File Offset: 0x00015F9E
		public static bool operator ==(RTime32 a, RTime32 b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00017DA8 File Offset: 0x00015FA8
		public static bool operator !=(RTime32 a, RTime32 b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00017DB5 File Offset: 0x00015FB5
		public int CompareTo(RTime32 other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BAB RID: 2987
		public uint Value;
	}
}
