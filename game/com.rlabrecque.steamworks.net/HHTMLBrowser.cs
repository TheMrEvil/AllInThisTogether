using System;

namespace Steamworks
{
	// Token: 0x0200019C RID: 412
	[Serializable]
	public struct HHTMLBrowser : IEquatable<HHTMLBrowser>, IComparable<HHTMLBrowser>
	{
		// Token: 0x060009B3 RID: 2483 RVA: 0x0000EC77 File Offset: 0x0000CE77
		public HHTMLBrowser(uint value)
		{
			this.m_HHTMLBrowser = value;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0000EC80 File Offset: 0x0000CE80
		public override string ToString()
		{
			return this.m_HHTMLBrowser.ToString();
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0000EC8D File Offset: 0x0000CE8D
		public override bool Equals(object other)
		{
			return other is HHTMLBrowser && this == (HHTMLBrowser)other;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0000ECAA File Offset: 0x0000CEAA
		public override int GetHashCode()
		{
			return this.m_HHTMLBrowser.GetHashCode();
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0000ECB7 File Offset: 0x0000CEB7
		public static bool operator ==(HHTMLBrowser x, HHTMLBrowser y)
		{
			return x.m_HHTMLBrowser == y.m_HHTMLBrowser;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0000ECC7 File Offset: 0x0000CEC7
		public static bool operator !=(HHTMLBrowser x, HHTMLBrowser y)
		{
			return !(x == y);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0000ECD3 File Offset: 0x0000CED3
		public static explicit operator HHTMLBrowser(uint value)
		{
			return new HHTMLBrowser(value);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0000ECDB File Offset: 0x0000CEDB
		public static explicit operator uint(HHTMLBrowser that)
		{
			return that.m_HHTMLBrowser;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0000ECE3 File Offset: 0x0000CEE3
		public bool Equals(HHTMLBrowser other)
		{
			return this.m_HHTMLBrowser == other.m_HHTMLBrowser;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0000ECF3 File Offset: 0x0000CEF3
		public int CompareTo(HHTMLBrowser other)
		{
			return this.m_HHTMLBrowser.CompareTo(other.m_HHTMLBrowser);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0000ED06 File Offset: 0x0000CF06
		// Note: this type is marked as 'beforefieldinit'.
		static HHTMLBrowser()
		{
		}

		// Token: 0x04000AAC RID: 2732
		public static readonly HHTMLBrowser Invalid = new HHTMLBrowser(0U);

		// Token: 0x04000AAD RID: 2733
		public uint m_HHTMLBrowser;
	}
}
