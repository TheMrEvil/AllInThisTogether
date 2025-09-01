using System;

namespace Steamworks
{
	// Token: 0x020001BD RID: 445
	[Serializable]
	public struct UGCFileWriteStreamHandle_t : IEquatable<UGCFileWriteStreamHandle_t>, IComparable<UGCFileWriteStreamHandle_t>
	{
		// Token: 0x06000ADC RID: 2780 RVA: 0x0000FCDF File Offset: 0x0000DEDF
		public UGCFileWriteStreamHandle_t(ulong value)
		{
			this.m_UGCFileWriteStreamHandle = value;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
		public override string ToString()
		{
			return this.m_UGCFileWriteStreamHandle.ToString();
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0000FCF5 File Offset: 0x0000DEF5
		public override bool Equals(object other)
		{
			return other is UGCFileWriteStreamHandle_t && this == (UGCFileWriteStreamHandle_t)other;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0000FD12 File Offset: 0x0000DF12
		public override int GetHashCode()
		{
			return this.m_UGCFileWriteStreamHandle.GetHashCode();
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0000FD1F File Offset: 0x0000DF1F
		public static bool operator ==(UGCFileWriteStreamHandle_t x, UGCFileWriteStreamHandle_t y)
		{
			return x.m_UGCFileWriteStreamHandle == y.m_UGCFileWriteStreamHandle;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0000FD2F File Offset: 0x0000DF2F
		public static bool operator !=(UGCFileWriteStreamHandle_t x, UGCFileWriteStreamHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0000FD3B File Offset: 0x0000DF3B
		public static explicit operator UGCFileWriteStreamHandle_t(ulong value)
		{
			return new UGCFileWriteStreamHandle_t(value);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0000FD43 File Offset: 0x0000DF43
		public static explicit operator ulong(UGCFileWriteStreamHandle_t that)
		{
			return that.m_UGCFileWriteStreamHandle;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0000FD4B File Offset: 0x0000DF4B
		public bool Equals(UGCFileWriteStreamHandle_t other)
		{
			return this.m_UGCFileWriteStreamHandle == other.m_UGCFileWriteStreamHandle;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0000FD5B File Offset: 0x0000DF5B
		public int CompareTo(UGCFileWriteStreamHandle_t other)
		{
			return this.m_UGCFileWriteStreamHandle.CompareTo(other.m_UGCFileWriteStreamHandle);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0000FD6E File Offset: 0x0000DF6E
		// Note: this type is marked as 'beforefieldinit'.
		static UGCFileWriteStreamHandle_t()
		{
		}

		// Token: 0x04000AEF RID: 2799
		public static readonly UGCFileWriteStreamHandle_t Invalid = new UGCFileWriteStreamHandle_t(ulong.MaxValue);

		// Token: 0x04000AF0 RID: 2800
		public ulong m_UGCFileWriteStreamHandle;
	}
}
