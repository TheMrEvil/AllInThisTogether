using System;

namespace Steamworks
{
	// Token: 0x020001A5 RID: 421
	[Serializable]
	public struct SteamInventoryResult_t : IEquatable<SteamInventoryResult_t>, IComparable<SteamInventoryResult_t>
	{
		// Token: 0x06000A00 RID: 2560 RVA: 0x0000F087 File Offset: 0x0000D287
		public SteamInventoryResult_t(int value)
		{
			this.m_SteamInventoryResult = value;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0000F090 File Offset: 0x0000D290
		public override string ToString()
		{
			return this.m_SteamInventoryResult.ToString();
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0000F09D File Offset: 0x0000D29D
		public override bool Equals(object other)
		{
			return other is SteamInventoryResult_t && this == (SteamInventoryResult_t)other;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0000F0BA File Offset: 0x0000D2BA
		public override int GetHashCode()
		{
			return this.m_SteamInventoryResult.GetHashCode();
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0000F0C7 File Offset: 0x0000D2C7
		public static bool operator ==(SteamInventoryResult_t x, SteamInventoryResult_t y)
		{
			return x.m_SteamInventoryResult == y.m_SteamInventoryResult;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0000F0D7 File Offset: 0x0000D2D7
		public static bool operator !=(SteamInventoryResult_t x, SteamInventoryResult_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0000F0E3 File Offset: 0x0000D2E3
		public static explicit operator SteamInventoryResult_t(int value)
		{
			return new SteamInventoryResult_t(value);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0000F0EB File Offset: 0x0000D2EB
		public static explicit operator int(SteamInventoryResult_t that)
		{
			return that.m_SteamInventoryResult;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0000F0F3 File Offset: 0x0000D2F3
		public bool Equals(SteamInventoryResult_t other)
		{
			return this.m_SteamInventoryResult == other.m_SteamInventoryResult;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0000F103 File Offset: 0x0000D303
		public int CompareTo(SteamInventoryResult_t other)
		{
			return this.m_SteamInventoryResult.CompareTo(other.m_SteamInventoryResult);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0000F116 File Offset: 0x0000D316
		// Note: this type is marked as 'beforefieldinit'.
		static SteamInventoryResult_t()
		{
		}

		// Token: 0x04000AB9 RID: 2745
		public static readonly SteamInventoryResult_t Invalid = new SteamInventoryResult_t(-1);

		// Token: 0x04000ABA RID: 2746
		public int m_SteamInventoryResult;
	}
}
