using System;

namespace Steamworks
{
	// Token: 0x02000196 RID: 406
	[Serializable]
	public struct CGameID : IEquatable<CGameID>, IComparable<CGameID>
	{
		// Token: 0x0600095B RID: 2395 RVA: 0x0000E4EB File Offset: 0x0000C6EB
		public CGameID(ulong GameID)
		{
			this.m_GameID = GameID;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0000E4F4 File Offset: 0x0000C6F4
		public CGameID(AppId_t nAppID)
		{
			this.m_GameID = 0UL;
			this.SetAppID(nAppID);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0000E505 File Offset: 0x0000C705
		public CGameID(AppId_t nAppID, uint nModID)
		{
			this.m_GameID = 0UL;
			this.SetAppID(nAppID);
			this.SetType(CGameID.EGameIDType.k_EGameIDTypeGameMod);
			this.SetModID(nModID);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0000E524 File Offset: 0x0000C724
		public bool IsSteamApp()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeApp;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0000E52F File Offset: 0x0000C72F
		public bool IsMod()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeGameMod;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0000E53A File Offset: 0x0000C73A
		public bool IsShortcut()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeShortcut;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0000E545 File Offset: 0x0000C745
		public bool IsP2PFile()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeP2P;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0000E550 File Offset: 0x0000C750
		public AppId_t AppID()
		{
			return new AppId_t((uint)(this.m_GameID & 16777215UL));
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0000E565 File Offset: 0x0000C765
		public CGameID.EGameIDType Type()
		{
			return (CGameID.EGameIDType)(this.m_GameID >> 24 & 255UL);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0000E578 File Offset: 0x0000C778
		public uint ModID()
		{
			return (uint)(this.m_GameID >> 32 & (ulong)-1);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0000E588 File Offset: 0x0000C788
		public bool IsValid()
		{
			switch (this.Type())
			{
			case CGameID.EGameIDType.k_EGameIDTypeApp:
				return this.AppID() != AppId_t.Invalid;
			case CGameID.EGameIDType.k_EGameIDTypeGameMod:
				return this.AppID() != AppId_t.Invalid && (this.ModID() & 2147483648U) > 0U;
			case CGameID.EGameIDType.k_EGameIDTypeShortcut:
				return (this.ModID() & 2147483648U) > 0U;
			case CGameID.EGameIDType.k_EGameIDTypeP2P:
				return this.AppID() == AppId_t.Invalid && (this.ModID() & 2147483648U) > 0U;
			default:
				return false;
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0000E61E File Offset: 0x0000C81E
		public void Reset()
		{
			this.m_GameID = 0UL;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0000E628 File Offset: 0x0000C828
		public void Set(ulong GameID)
		{
			this.m_GameID = GameID;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0000E631 File Offset: 0x0000C831
		private void SetAppID(AppId_t other)
		{
			this.m_GameID = ((this.m_GameID & 18446744073692774400UL) | ((ulong)((uint)other) & 16777215UL));
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0000E655 File Offset: 0x0000C855
		private void SetType(CGameID.EGameIDType other)
		{
			this.m_GameID = ((this.m_GameID & 18446744069431361535UL) | (ulong)((ulong)((long)other & 255L) << 24));
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0000E67A File Offset: 0x0000C87A
		private void SetModID(uint other)
		{
			this.m_GameID = ((this.m_GameID & (ulong)-1) | ((ulong)other & (ulong)-1) << 32);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0000E694 File Offset: 0x0000C894
		public override string ToString()
		{
			return this.m_GameID.ToString();
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0000E6A1 File Offset: 0x0000C8A1
		public override bool Equals(object other)
		{
			return other is CGameID && this == (CGameID)other;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0000E6BE File Offset: 0x0000C8BE
		public override int GetHashCode()
		{
			return this.m_GameID.GetHashCode();
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0000E6CB File Offset: 0x0000C8CB
		public static bool operator ==(CGameID x, CGameID y)
		{
			return x.m_GameID == y.m_GameID;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0000E6DB File Offset: 0x0000C8DB
		public static bool operator !=(CGameID x, CGameID y)
		{
			return !(x == y);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0000E6E7 File Offset: 0x0000C8E7
		public static explicit operator CGameID(ulong value)
		{
			return new CGameID(value);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0000E6EF File Offset: 0x0000C8EF
		public static explicit operator ulong(CGameID that)
		{
			return that.m_GameID;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0000E6F7 File Offset: 0x0000C8F7
		public bool Equals(CGameID other)
		{
			return this.m_GameID == other.m_GameID;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0000E707 File Offset: 0x0000C907
		public int CompareTo(CGameID other)
		{
			return this.m_GameID.CompareTo(other.m_GameID);
		}

		// Token: 0x04000A95 RID: 2709
		public ulong m_GameID;

		// Token: 0x020001ED RID: 493
		public enum EGameIDType
		{
			// Token: 0x04000B22 RID: 2850
			k_EGameIDTypeApp,
			// Token: 0x04000B23 RID: 2851
			k_EGameIDTypeGameMod,
			// Token: 0x04000B24 RID: 2852
			k_EGameIDTypeShortcut,
			// Token: 0x04000B25 RID: 2853
			k_EGameIDTypeP2P
		}
	}
}
