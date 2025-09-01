using System;

namespace Steamworks
{
	// Token: 0x020001C7 RID: 455
	[Serializable]
	public struct UGCQueryHandle_t : IEquatable<UGCQueryHandle_t>, IComparable<UGCQueryHandle_t>
	{
		// Token: 0x06000B42 RID: 2882 RVA: 0x00010426 File Offset: 0x0000E626
		public UGCQueryHandle_t(ulong value)
		{
			this.m_UGCQueryHandle = value;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0001042F File Offset: 0x0000E62F
		public override string ToString()
		{
			return this.m_UGCQueryHandle.ToString();
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0001043C File Offset: 0x0000E63C
		public override bool Equals(object other)
		{
			return other is UGCQueryHandle_t && this == (UGCQueryHandle_t)other;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00010459 File Offset: 0x0000E659
		public override int GetHashCode()
		{
			return this.m_UGCQueryHandle.GetHashCode();
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00010466 File Offset: 0x0000E666
		public static bool operator ==(UGCQueryHandle_t x, UGCQueryHandle_t y)
		{
			return x.m_UGCQueryHandle == y.m_UGCQueryHandle;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00010476 File Offset: 0x0000E676
		public static bool operator !=(UGCQueryHandle_t x, UGCQueryHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00010482 File Offset: 0x0000E682
		public static explicit operator UGCQueryHandle_t(ulong value)
		{
			return new UGCQueryHandle_t(value);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0001048A File Offset: 0x0000E68A
		public static explicit operator ulong(UGCQueryHandle_t that)
		{
			return that.m_UGCQueryHandle;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00010492 File Offset: 0x0000E692
		public bool Equals(UGCQueryHandle_t other)
		{
			return this.m_UGCQueryHandle == other.m_UGCQueryHandle;
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x000104A2 File Offset: 0x0000E6A2
		public int CompareTo(UGCQueryHandle_t other)
		{
			return this.m_UGCQueryHandle.CompareTo(other.m_UGCQueryHandle);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000104B5 File Offset: 0x0000E6B5
		// Note: this type is marked as 'beforefieldinit'.
		static UGCQueryHandle_t()
		{
		}

		// Token: 0x04000B02 RID: 2818
		public static readonly UGCQueryHandle_t Invalid = new UGCQueryHandle_t(ulong.MaxValue);

		// Token: 0x04000B03 RID: 2819
		public ulong m_UGCQueryHandle;
	}
}
