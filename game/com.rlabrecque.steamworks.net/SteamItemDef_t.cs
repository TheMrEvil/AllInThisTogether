using System;

namespace Steamworks
{
	// Token: 0x020001A7 RID: 423
	[Serializable]
	public struct SteamItemDef_t : IEquatable<SteamItemDef_t>, IComparable<SteamItemDef_t>
	{
		// Token: 0x06000A16 RID: 2582 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
		public SteamItemDef_t(int value)
		{
			this.m_SteamItemDef = value;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0000F1C9 File Offset: 0x0000D3C9
		public override string ToString()
		{
			return this.m_SteamItemDef.ToString();
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0000F1D6 File Offset: 0x0000D3D6
		public override bool Equals(object other)
		{
			return other is SteamItemDef_t && this == (SteamItemDef_t)other;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0000F1F3 File Offset: 0x0000D3F3
		public override int GetHashCode()
		{
			return this.m_SteamItemDef.GetHashCode();
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0000F200 File Offset: 0x0000D400
		public static bool operator ==(SteamItemDef_t x, SteamItemDef_t y)
		{
			return x.m_SteamItemDef == y.m_SteamItemDef;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0000F210 File Offset: 0x0000D410
		public static bool operator !=(SteamItemDef_t x, SteamItemDef_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0000F21C File Offset: 0x0000D41C
		public static explicit operator SteamItemDef_t(int value)
		{
			return new SteamItemDef_t(value);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0000F224 File Offset: 0x0000D424
		public static explicit operator int(SteamItemDef_t that)
		{
			return that.m_SteamItemDef;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0000F22C File Offset: 0x0000D42C
		public bool Equals(SteamItemDef_t other)
		{
			return this.m_SteamItemDef == other.m_SteamItemDef;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0000F23C File Offset: 0x0000D43C
		public int CompareTo(SteamItemDef_t other)
		{
			return this.m_SteamItemDef.CompareTo(other.m_SteamItemDef);
		}

		// Token: 0x04000ABD RID: 2749
		public int m_SteamItemDef;
	}
}
