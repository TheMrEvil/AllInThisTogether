using System;

namespace Steamworks
{
	// Token: 0x020001C2 RID: 450
	[Serializable]
	public struct DepotId_t : IEquatable<DepotId_t>, IComparable<DepotId_t>
	{
		// Token: 0x06000B12 RID: 2834 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		public DepotId_t(uint value)
		{
			this.m_DepotId = value;
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0000FFE9 File Offset: 0x0000E1E9
		public override string ToString()
		{
			return this.m_DepotId.ToString();
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0000FFF6 File Offset: 0x0000E1F6
		public override bool Equals(object other)
		{
			return other is DepotId_t && this == (DepotId_t)other;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00010013 File Offset: 0x0000E213
		public override int GetHashCode()
		{
			return this.m_DepotId.GetHashCode();
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00010020 File Offset: 0x0000E220
		public static bool operator ==(DepotId_t x, DepotId_t y)
		{
			return x.m_DepotId == y.m_DepotId;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00010030 File Offset: 0x0000E230
		public static bool operator !=(DepotId_t x, DepotId_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0001003C File Offset: 0x0000E23C
		public static explicit operator DepotId_t(uint value)
		{
			return new DepotId_t(value);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00010044 File Offset: 0x0000E244
		public static explicit operator uint(DepotId_t that)
		{
			return that.m_DepotId;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0001004C File Offset: 0x0000E24C
		public bool Equals(DepotId_t other)
		{
			return this.m_DepotId == other.m_DepotId;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0001005C File Offset: 0x0000E25C
		public int CompareTo(DepotId_t other)
		{
			return this.m_DepotId.CompareTo(other.m_DepotId);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0001006F File Offset: 0x0000E26F
		// Note: this type is marked as 'beforefieldinit'.
		static DepotId_t()
		{
		}

		// Token: 0x04000AF8 RID: 2808
		public static readonly DepotId_t Invalid = new DepotId_t(0U);

		// Token: 0x04000AF9 RID: 2809
		public uint m_DepotId;
	}
}
