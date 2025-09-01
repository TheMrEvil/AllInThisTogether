using System;

namespace Steamworks
{
	// Token: 0x020001C4 RID: 452
	[Serializable]
	public struct RTime32 : IEquatable<RTime32>, IComparable<RTime32>
	{
		// Token: 0x06000B28 RID: 2856 RVA: 0x00010119 File Offset: 0x0000E319
		public RTime32(uint value)
		{
			this.m_RTime32 = value;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00010122 File Offset: 0x0000E322
		public override string ToString()
		{
			return this.m_RTime32.ToString();
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0001012F File Offset: 0x0000E32F
		public override bool Equals(object other)
		{
			return other is RTime32 && this == (RTime32)other;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0001014C File Offset: 0x0000E34C
		public override int GetHashCode()
		{
			return this.m_RTime32.GetHashCode();
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00010159 File Offset: 0x0000E359
		public static bool operator ==(RTime32 x, RTime32 y)
		{
			return x.m_RTime32 == y.m_RTime32;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00010169 File Offset: 0x0000E369
		public static bool operator !=(RTime32 x, RTime32 y)
		{
			return !(x == y);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00010175 File Offset: 0x0000E375
		public static explicit operator RTime32(uint value)
		{
			return new RTime32(value);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0001017D File Offset: 0x0000E37D
		public static explicit operator uint(RTime32 that)
		{
			return that.m_RTime32;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00010185 File Offset: 0x0000E385
		public bool Equals(RTime32 other)
		{
			return this.m_RTime32 == other.m_RTime32;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00010195 File Offset: 0x0000E395
		public int CompareTo(RTime32 other)
		{
			return this.m_RTime32.CompareTo(other.m_RTime32);
		}

		// Token: 0x04000AFC RID: 2812
		public uint m_RTime32;
	}
}
