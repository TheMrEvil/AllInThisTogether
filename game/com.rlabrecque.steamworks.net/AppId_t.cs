using System;

namespace Steamworks
{
	// Token: 0x020001C1 RID: 449
	[Serializable]
	public struct AppId_t : IEquatable<AppId_t>, IComparable<AppId_t>
	{
		// Token: 0x06000B07 RID: 2823 RVA: 0x0000FF44 File Offset: 0x0000E144
		public AppId_t(uint value)
		{
			this.m_AppId = value;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0000FF4D File Offset: 0x0000E14D
		public override string ToString()
		{
			return this.m_AppId.ToString();
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0000FF5A File Offset: 0x0000E15A
		public override bool Equals(object other)
		{
			return other is AppId_t && this == (AppId_t)other;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0000FF77 File Offset: 0x0000E177
		public override int GetHashCode()
		{
			return this.m_AppId.GetHashCode();
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0000FF84 File Offset: 0x0000E184
		public static bool operator ==(AppId_t x, AppId_t y)
		{
			return x.m_AppId == y.m_AppId;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0000FF94 File Offset: 0x0000E194
		public static bool operator !=(AppId_t x, AppId_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0000FFA0 File Offset: 0x0000E1A0
		public static explicit operator AppId_t(uint value)
		{
			return new AppId_t(value);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0000FFA8 File Offset: 0x0000E1A8
		public static explicit operator uint(AppId_t that)
		{
			return that.m_AppId;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0000FFB0 File Offset: 0x0000E1B0
		public bool Equals(AppId_t other)
		{
			return this.m_AppId == other.m_AppId;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0000FFC0 File Offset: 0x0000E1C0
		public int CompareTo(AppId_t other)
		{
			return this.m_AppId.CompareTo(other.m_AppId);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0000FFD3 File Offset: 0x0000E1D3
		// Note: this type is marked as 'beforefieldinit'.
		static AppId_t()
		{
		}

		// Token: 0x04000AF6 RID: 2806
		public static readonly AppId_t Invalid = new AppId_t(0U);

		// Token: 0x04000AF7 RID: 2807
		public uint m_AppId;
	}
}
