using System;

namespace Steamworks
{
	// Token: 0x020001BC RID: 444
	[Serializable]
	public struct PublishedFileUpdateHandle_t : IEquatable<PublishedFileUpdateHandle_t>, IComparable<PublishedFileUpdateHandle_t>
	{
		// Token: 0x06000AD1 RID: 2769 RVA: 0x0000FC42 File Offset: 0x0000DE42
		public PublishedFileUpdateHandle_t(ulong value)
		{
			this.m_PublishedFileUpdateHandle = value;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0000FC4B File Offset: 0x0000DE4B
		public override string ToString()
		{
			return this.m_PublishedFileUpdateHandle.ToString();
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0000FC58 File Offset: 0x0000DE58
		public override bool Equals(object other)
		{
			return other is PublishedFileUpdateHandle_t && this == (PublishedFileUpdateHandle_t)other;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0000FC75 File Offset: 0x0000DE75
		public override int GetHashCode()
		{
			return this.m_PublishedFileUpdateHandle.GetHashCode();
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0000FC82 File Offset: 0x0000DE82
		public static bool operator ==(PublishedFileUpdateHandle_t x, PublishedFileUpdateHandle_t y)
		{
			return x.m_PublishedFileUpdateHandle == y.m_PublishedFileUpdateHandle;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0000FC92 File Offset: 0x0000DE92
		public static bool operator !=(PublishedFileUpdateHandle_t x, PublishedFileUpdateHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0000FC9E File Offset: 0x0000DE9E
		public static explicit operator PublishedFileUpdateHandle_t(ulong value)
		{
			return new PublishedFileUpdateHandle_t(value);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0000FCA6 File Offset: 0x0000DEA6
		public static explicit operator ulong(PublishedFileUpdateHandle_t that)
		{
			return that.m_PublishedFileUpdateHandle;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0000FCAE File Offset: 0x0000DEAE
		public bool Equals(PublishedFileUpdateHandle_t other)
		{
			return this.m_PublishedFileUpdateHandle == other.m_PublishedFileUpdateHandle;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0000FCBE File Offset: 0x0000DEBE
		public int CompareTo(PublishedFileUpdateHandle_t other)
		{
			return this.m_PublishedFileUpdateHandle.CompareTo(other.m_PublishedFileUpdateHandle);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0000FCD1 File Offset: 0x0000DED1
		// Note: this type is marked as 'beforefieldinit'.
		static PublishedFileUpdateHandle_t()
		{
		}

		// Token: 0x04000AED RID: 2797
		public static readonly PublishedFileUpdateHandle_t Invalid = new PublishedFileUpdateHandle_t(ulong.MaxValue);

		// Token: 0x04000AEE RID: 2798
		public ulong m_PublishedFileUpdateHandle;
	}
}
