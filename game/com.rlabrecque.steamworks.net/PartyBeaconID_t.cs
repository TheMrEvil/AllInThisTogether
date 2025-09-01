using System;

namespace Steamworks
{
	// Token: 0x020001C3 RID: 451
	[Serializable]
	public struct PartyBeaconID_t : IEquatable<PartyBeaconID_t>, IComparable<PartyBeaconID_t>
	{
		// Token: 0x06000B1D RID: 2845 RVA: 0x0001007C File Offset: 0x0000E27C
		public PartyBeaconID_t(ulong value)
		{
			this.m_PartyBeaconID = value;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00010085 File Offset: 0x0000E285
		public override string ToString()
		{
			return this.m_PartyBeaconID.ToString();
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00010092 File Offset: 0x0000E292
		public override bool Equals(object other)
		{
			return other is PartyBeaconID_t && this == (PartyBeaconID_t)other;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x000100AF File Offset: 0x0000E2AF
		public override int GetHashCode()
		{
			return this.m_PartyBeaconID.GetHashCode();
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000100BC File Offset: 0x0000E2BC
		public static bool operator ==(PartyBeaconID_t x, PartyBeaconID_t y)
		{
			return x.m_PartyBeaconID == y.m_PartyBeaconID;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x000100CC File Offset: 0x0000E2CC
		public static bool operator !=(PartyBeaconID_t x, PartyBeaconID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x000100D8 File Offset: 0x0000E2D8
		public static explicit operator PartyBeaconID_t(ulong value)
		{
			return new PartyBeaconID_t(value);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x000100E0 File Offset: 0x0000E2E0
		public static explicit operator ulong(PartyBeaconID_t that)
		{
			return that.m_PartyBeaconID;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x000100E8 File Offset: 0x0000E2E8
		public bool Equals(PartyBeaconID_t other)
		{
			return this.m_PartyBeaconID == other.m_PartyBeaconID;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x000100F8 File Offset: 0x0000E2F8
		public int CompareTo(PartyBeaconID_t other)
		{
			return this.m_PartyBeaconID.CompareTo(other.m_PartyBeaconID);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0001010B File Offset: 0x0000E30B
		// Note: this type is marked as 'beforefieldinit'.
		static PartyBeaconID_t()
		{
		}

		// Token: 0x04000AFA RID: 2810
		public static readonly PartyBeaconID_t Invalid = new PartyBeaconID_t(0UL);

		// Token: 0x04000AFB RID: 2811
		public ulong m_PartyBeaconID;
	}
}
