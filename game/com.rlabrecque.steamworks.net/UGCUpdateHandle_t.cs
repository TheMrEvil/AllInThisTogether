using System;

namespace Steamworks
{
	// Token: 0x020001C8 RID: 456
	[Serializable]
	public struct UGCUpdateHandle_t : IEquatable<UGCUpdateHandle_t>, IComparable<UGCUpdateHandle_t>
	{
		// Token: 0x06000B4D RID: 2893 RVA: 0x000104C3 File Offset: 0x0000E6C3
		public UGCUpdateHandle_t(ulong value)
		{
			this.m_UGCUpdateHandle = value;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x000104CC File Offset: 0x0000E6CC
		public override string ToString()
		{
			return this.m_UGCUpdateHandle.ToString();
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x000104D9 File Offset: 0x0000E6D9
		public override bool Equals(object other)
		{
			return other is UGCUpdateHandle_t && this == (UGCUpdateHandle_t)other;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x000104F6 File Offset: 0x0000E6F6
		public override int GetHashCode()
		{
			return this.m_UGCUpdateHandle.GetHashCode();
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00010503 File Offset: 0x0000E703
		public static bool operator ==(UGCUpdateHandle_t x, UGCUpdateHandle_t y)
		{
			return x.m_UGCUpdateHandle == y.m_UGCUpdateHandle;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00010513 File Offset: 0x0000E713
		public static bool operator !=(UGCUpdateHandle_t x, UGCUpdateHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0001051F File Offset: 0x0000E71F
		public static explicit operator UGCUpdateHandle_t(ulong value)
		{
			return new UGCUpdateHandle_t(value);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00010527 File Offset: 0x0000E727
		public static explicit operator ulong(UGCUpdateHandle_t that)
		{
			return that.m_UGCUpdateHandle;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0001052F File Offset: 0x0000E72F
		public bool Equals(UGCUpdateHandle_t other)
		{
			return this.m_UGCUpdateHandle == other.m_UGCUpdateHandle;
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0001053F File Offset: 0x0000E73F
		public int CompareTo(UGCUpdateHandle_t other)
		{
			return this.m_UGCUpdateHandle.CompareTo(other.m_UGCUpdateHandle);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00010552 File Offset: 0x0000E752
		// Note: this type is marked as 'beforefieldinit'.
		static UGCUpdateHandle_t()
		{
		}

		// Token: 0x04000B04 RID: 2820
		public static readonly UGCUpdateHandle_t Invalid = new UGCUpdateHandle_t(ulong.MaxValue);

		// Token: 0x04000B05 RID: 2821
		public ulong m_UGCUpdateHandle;
	}
}
