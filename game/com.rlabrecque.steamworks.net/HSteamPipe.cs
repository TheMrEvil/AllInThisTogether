using System;

namespace Steamworks
{
	// Token: 0x020001CB RID: 459
	[Serializable]
	public struct HSteamPipe : IEquatable<HSteamPipe>, IComparable<HSteamPipe>
	{
		// Token: 0x06000B6C RID: 2924 RVA: 0x0001067E File Offset: 0x0000E87E
		public HSteamPipe(int value)
		{
			this.m_HSteamPipe = value;
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00010687 File Offset: 0x0000E887
		public override string ToString()
		{
			return this.m_HSteamPipe.ToString();
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00010694 File Offset: 0x0000E894
		public override bool Equals(object other)
		{
			return other is HSteamPipe && this == (HSteamPipe)other;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x000106B1 File Offset: 0x0000E8B1
		public override int GetHashCode()
		{
			return this.m_HSteamPipe.GetHashCode();
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x000106BE File Offset: 0x0000E8BE
		public static bool operator ==(HSteamPipe x, HSteamPipe y)
		{
			return x.m_HSteamPipe == y.m_HSteamPipe;
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x000106CE File Offset: 0x0000E8CE
		public static bool operator !=(HSteamPipe x, HSteamPipe y)
		{
			return !(x == y);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x000106DA File Offset: 0x0000E8DA
		public static explicit operator HSteamPipe(int value)
		{
			return new HSteamPipe(value);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x000106E2 File Offset: 0x0000E8E2
		public static explicit operator int(HSteamPipe that)
		{
			return that.m_HSteamPipe;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x000106EA File Offset: 0x0000E8EA
		public bool Equals(HSteamPipe other)
		{
			return this.m_HSteamPipe == other.m_HSteamPipe;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x000106FA File Offset: 0x0000E8FA
		public int CompareTo(HSteamPipe other)
		{
			return this.m_HSteamPipe.CompareTo(other.m_HSteamPipe);
		}

		// Token: 0x04000B08 RID: 2824
		public int m_HSteamPipe;
	}
}
