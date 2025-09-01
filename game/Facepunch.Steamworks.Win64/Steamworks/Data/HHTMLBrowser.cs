using System;

namespace Steamworks.Data
{
	// Token: 0x020001E0 RID: 480
	internal struct HHTMLBrowser : IEquatable<HHTMLBrowser>, IComparable<HHTMLBrowser>
	{
		// Token: 0x06000F44 RID: 3908 RVA: 0x00019098 File Offset: 0x00017298
		public static implicit operator HHTMLBrowser(uint value)
		{
			return new HHTMLBrowser
			{
				Value = value
			};
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x000190B6 File Offset: 0x000172B6
		public static implicit operator uint(HHTMLBrowser value)
		{
			return value.Value;
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x000190BE File Offset: 0x000172BE
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x000190CB File Offset: 0x000172CB
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x000190D8 File Offset: 0x000172D8
		public override bool Equals(object p)
		{
			return this.Equals((HHTMLBrowser)p);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x000190E6 File Offset: 0x000172E6
		public bool Equals(HHTMLBrowser p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x000190F6 File Offset: 0x000172F6
		public static bool operator ==(HHTMLBrowser a, HHTMLBrowser b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00019100 File Offset: 0x00017300
		public static bool operator !=(HHTMLBrowser a, HHTMLBrowser b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0001910D File Offset: 0x0001730D
		public int CompareTo(HHTMLBrowser other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BCF RID: 3023
		public uint Value;
	}
}
