using System;

namespace Steamworks
{
	// Token: 0x020001BE RID: 446
	[Serializable]
	public struct UGCHandle_t : IEquatable<UGCHandle_t>, IComparable<UGCHandle_t>
	{
		// Token: 0x06000AE7 RID: 2791 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		public UGCHandle_t(ulong value)
		{
			this.m_UGCHandle = value;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0000FD85 File Offset: 0x0000DF85
		public override string ToString()
		{
			return this.m_UGCHandle.ToString();
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0000FD92 File Offset: 0x0000DF92
		public override bool Equals(object other)
		{
			return other is UGCHandle_t && this == (UGCHandle_t)other;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0000FDAF File Offset: 0x0000DFAF
		public override int GetHashCode()
		{
			return this.m_UGCHandle.GetHashCode();
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0000FDBC File Offset: 0x0000DFBC
		public static bool operator ==(UGCHandle_t x, UGCHandle_t y)
		{
			return x.m_UGCHandle == y.m_UGCHandle;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0000FDCC File Offset: 0x0000DFCC
		public static bool operator !=(UGCHandle_t x, UGCHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0000FDD8 File Offset: 0x0000DFD8
		public static explicit operator UGCHandle_t(ulong value)
		{
			return new UGCHandle_t(value);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0000FDE0 File Offset: 0x0000DFE0
		public static explicit operator ulong(UGCHandle_t that)
		{
			return that.m_UGCHandle;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0000FDE8 File Offset: 0x0000DFE8
		public bool Equals(UGCHandle_t other)
		{
			return this.m_UGCHandle == other.m_UGCHandle;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0000FDF8 File Offset: 0x0000DFF8
		public int CompareTo(UGCHandle_t other)
		{
			return this.m_UGCHandle.CompareTo(other.m_UGCHandle);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0000FE0B File Offset: 0x0000E00B
		// Note: this type is marked as 'beforefieldinit'.
		static UGCHandle_t()
		{
		}

		// Token: 0x04000AF1 RID: 2801
		public static readonly UGCHandle_t Invalid = new UGCHandle_t(ulong.MaxValue);

		// Token: 0x04000AF2 RID: 2802
		public ulong m_UGCHandle;
	}
}
