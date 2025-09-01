using System;

namespace Steamworks
{
	// Token: 0x020001BF RID: 447
	[Serializable]
	public struct ScreenshotHandle : IEquatable<ScreenshotHandle>, IComparable<ScreenshotHandle>
	{
		// Token: 0x06000AF2 RID: 2802 RVA: 0x0000FE19 File Offset: 0x0000E019
		public ScreenshotHandle(uint value)
		{
			this.m_ScreenshotHandle = value;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0000FE22 File Offset: 0x0000E022
		public override string ToString()
		{
			return this.m_ScreenshotHandle.ToString();
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0000FE2F File Offset: 0x0000E02F
		public override bool Equals(object other)
		{
			return other is ScreenshotHandle && this == (ScreenshotHandle)other;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0000FE4C File Offset: 0x0000E04C
		public override int GetHashCode()
		{
			return this.m_ScreenshotHandle.GetHashCode();
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0000FE59 File Offset: 0x0000E059
		public static bool operator ==(ScreenshotHandle x, ScreenshotHandle y)
		{
			return x.m_ScreenshotHandle == y.m_ScreenshotHandle;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0000FE69 File Offset: 0x0000E069
		public static bool operator !=(ScreenshotHandle x, ScreenshotHandle y)
		{
			return !(x == y);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0000FE75 File Offset: 0x0000E075
		public static explicit operator ScreenshotHandle(uint value)
		{
			return new ScreenshotHandle(value);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0000FE7D File Offset: 0x0000E07D
		public static explicit operator uint(ScreenshotHandle that)
		{
			return that.m_ScreenshotHandle;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0000FE85 File Offset: 0x0000E085
		public bool Equals(ScreenshotHandle other)
		{
			return this.m_ScreenshotHandle == other.m_ScreenshotHandle;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0000FE95 File Offset: 0x0000E095
		public int CompareTo(ScreenshotHandle other)
		{
			return this.m_ScreenshotHandle.CompareTo(other.m_ScreenshotHandle);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
		// Note: this type is marked as 'beforefieldinit'.
		static ScreenshotHandle()
		{
		}

		// Token: 0x04000AF3 RID: 2803
		public static readonly ScreenshotHandle Invalid = new ScreenshotHandle(0U);

		// Token: 0x04000AF4 RID: 2804
		public uint m_ScreenshotHandle;
	}
}
