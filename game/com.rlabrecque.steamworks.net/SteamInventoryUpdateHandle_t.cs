using System;

namespace Steamworks
{
	// Token: 0x020001A6 RID: 422
	[Serializable]
	public struct SteamInventoryUpdateHandle_t : IEquatable<SteamInventoryUpdateHandle_t>, IComparable<SteamInventoryUpdateHandle_t>
	{
		// Token: 0x06000A0B RID: 2571 RVA: 0x0000F123 File Offset: 0x0000D323
		public SteamInventoryUpdateHandle_t(ulong value)
		{
			this.m_SteamInventoryUpdateHandle = value;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0000F12C File Offset: 0x0000D32C
		public override string ToString()
		{
			return this.m_SteamInventoryUpdateHandle.ToString();
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0000F139 File Offset: 0x0000D339
		public override bool Equals(object other)
		{
			return other is SteamInventoryUpdateHandle_t && this == (SteamInventoryUpdateHandle_t)other;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0000F156 File Offset: 0x0000D356
		public override int GetHashCode()
		{
			return this.m_SteamInventoryUpdateHandle.GetHashCode();
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0000F163 File Offset: 0x0000D363
		public static bool operator ==(SteamInventoryUpdateHandle_t x, SteamInventoryUpdateHandle_t y)
		{
			return x.m_SteamInventoryUpdateHandle == y.m_SteamInventoryUpdateHandle;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0000F173 File Offset: 0x0000D373
		public static bool operator !=(SteamInventoryUpdateHandle_t x, SteamInventoryUpdateHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0000F17F File Offset: 0x0000D37F
		public static explicit operator SteamInventoryUpdateHandle_t(ulong value)
		{
			return new SteamInventoryUpdateHandle_t(value);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0000F187 File Offset: 0x0000D387
		public static explicit operator ulong(SteamInventoryUpdateHandle_t that)
		{
			return that.m_SteamInventoryUpdateHandle;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0000F18F File Offset: 0x0000D38F
		public bool Equals(SteamInventoryUpdateHandle_t other)
		{
			return this.m_SteamInventoryUpdateHandle == other.m_SteamInventoryUpdateHandle;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0000F19F File Offset: 0x0000D39F
		public int CompareTo(SteamInventoryUpdateHandle_t other)
		{
			return this.m_SteamInventoryUpdateHandle.CompareTo(other.m_SteamInventoryUpdateHandle);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0000F1B2 File Offset: 0x0000D3B2
		// Note: this type is marked as 'beforefieldinit'.
		static SteamInventoryUpdateHandle_t()
		{
		}

		// Token: 0x04000ABB RID: 2747
		public static readonly SteamInventoryUpdateHandle_t Invalid = new SteamInventoryUpdateHandle_t(ulong.MaxValue);

		// Token: 0x04000ABC RID: 2748
		public ulong m_SteamInventoryUpdateHandle;
	}
}
