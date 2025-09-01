using System;

namespace Steamworks
{
	// Token: 0x020001BB RID: 443
	[Serializable]
	public struct PublishedFileId_t : IEquatable<PublishedFileId_t>, IComparable<PublishedFileId_t>
	{
		// Token: 0x06000AC6 RID: 2758 RVA: 0x0000FBA5 File Offset: 0x0000DDA5
		public PublishedFileId_t(ulong value)
		{
			this.m_PublishedFileId = value;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0000FBAE File Offset: 0x0000DDAE
		public override string ToString()
		{
			return this.m_PublishedFileId.ToString();
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0000FBBB File Offset: 0x0000DDBB
		public override bool Equals(object other)
		{
			return other is PublishedFileId_t && this == (PublishedFileId_t)other;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0000FBD8 File Offset: 0x0000DDD8
		public override int GetHashCode()
		{
			return this.m_PublishedFileId.GetHashCode();
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0000FBE5 File Offset: 0x0000DDE5
		public static bool operator ==(PublishedFileId_t x, PublishedFileId_t y)
		{
			return x.m_PublishedFileId == y.m_PublishedFileId;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0000FBF5 File Offset: 0x0000DDF5
		public static bool operator !=(PublishedFileId_t x, PublishedFileId_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0000FC01 File Offset: 0x0000DE01
		public static explicit operator PublishedFileId_t(ulong value)
		{
			return new PublishedFileId_t(value);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0000FC09 File Offset: 0x0000DE09
		public static explicit operator ulong(PublishedFileId_t that)
		{
			return that.m_PublishedFileId;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0000FC11 File Offset: 0x0000DE11
		public bool Equals(PublishedFileId_t other)
		{
			return this.m_PublishedFileId == other.m_PublishedFileId;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0000FC21 File Offset: 0x0000DE21
		public int CompareTo(PublishedFileId_t other)
		{
			return this.m_PublishedFileId.CompareTo(other.m_PublishedFileId);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0000FC34 File Offset: 0x0000DE34
		// Note: this type is marked as 'beforefieldinit'.
		static PublishedFileId_t()
		{
		}

		// Token: 0x04000AEB RID: 2795
		public static readonly PublishedFileId_t Invalid = new PublishedFileId_t(0UL);

		// Token: 0x04000AEC RID: 2796
		public ulong m_PublishedFileId;
	}
}
