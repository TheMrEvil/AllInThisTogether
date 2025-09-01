using System;

namespace Steamworks.Data
{
	// Token: 0x020001D3 RID: 467
	internal struct ScreenshotHandle : IEquatable<ScreenshotHandle>, IComparable<ScreenshotHandle>
	{
		// Token: 0x06000ECF RID: 3791 RVA: 0x000189B0 File Offset: 0x00016BB0
		public static implicit operator ScreenshotHandle(uint value)
		{
			return new ScreenshotHandle
			{
				Value = value
			};
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x000189CE File Offset: 0x00016BCE
		public static implicit operator uint(ScreenshotHandle value)
		{
			return value.Value;
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x000189D6 File Offset: 0x00016BD6
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x000189E3 File Offset: 0x00016BE3
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x000189F0 File Offset: 0x00016BF0
		public override bool Equals(object p)
		{
			return this.Equals((ScreenshotHandle)p);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x000189FE File Offset: 0x00016BFE
		public bool Equals(ScreenshotHandle p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00018A0E File Offset: 0x00016C0E
		public static bool operator ==(ScreenshotHandle a, ScreenshotHandle b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00018A18 File Offset: 0x00016C18
		public static bool operator !=(ScreenshotHandle a, ScreenshotHandle b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00018A25 File Offset: 0x00016C25
		public int CompareTo(ScreenshotHandle other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BC2 RID: 3010
		public uint Value;
	}
}
