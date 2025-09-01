using System;

namespace Steamworks
{
	// Token: 0x020001C5 RID: 453
	[Serializable]
	public struct SteamAPICall_t : IEquatable<SteamAPICall_t>, IComparable<SteamAPICall_t>
	{
		// Token: 0x06000B32 RID: 2866 RVA: 0x000101A8 File Offset: 0x0000E3A8
		public SteamAPICall_t(ulong value)
		{
			this.m_SteamAPICall = value;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x000101B1 File Offset: 0x0000E3B1
		public override string ToString()
		{
			return this.m_SteamAPICall.ToString();
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000101BE File Offset: 0x0000E3BE
		public override bool Equals(object other)
		{
			return other is SteamAPICall_t && this == (SteamAPICall_t)other;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x000101DB File Offset: 0x0000E3DB
		public override int GetHashCode()
		{
			return this.m_SteamAPICall.GetHashCode();
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x000101E8 File Offset: 0x0000E3E8
		public static bool operator ==(SteamAPICall_t x, SteamAPICall_t y)
		{
			return x.m_SteamAPICall == y.m_SteamAPICall;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000101F8 File Offset: 0x0000E3F8
		public static bool operator !=(SteamAPICall_t x, SteamAPICall_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00010204 File Offset: 0x0000E404
		public static explicit operator SteamAPICall_t(ulong value)
		{
			return new SteamAPICall_t(value);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0001020C File Offset: 0x0000E40C
		public static explicit operator ulong(SteamAPICall_t that)
		{
			return that.m_SteamAPICall;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00010214 File Offset: 0x0000E414
		public bool Equals(SteamAPICall_t other)
		{
			return this.m_SteamAPICall == other.m_SteamAPICall;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00010224 File Offset: 0x0000E424
		public int CompareTo(SteamAPICall_t other)
		{
			return this.m_SteamAPICall.CompareTo(other.m_SteamAPICall);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00010237 File Offset: 0x0000E437
		// Note: this type is marked as 'beforefieldinit'.
		static SteamAPICall_t()
		{
		}

		// Token: 0x04000AFD RID: 2813
		public static readonly SteamAPICall_t Invalid = new SteamAPICall_t(0UL);

		// Token: 0x04000AFE RID: 2814
		public ulong m_SteamAPICall;
	}
}
