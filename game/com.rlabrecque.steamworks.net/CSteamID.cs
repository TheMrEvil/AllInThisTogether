using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000197 RID: 407
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct CSteamID : IEquatable<CSteamID>, IComparable<CSteamID>
	{
		// Token: 0x06000974 RID: 2420 RVA: 0x0000E71A File Offset: 0x0000C91A
		public CSteamID(AccountID_t unAccountID, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.m_SteamID = 0UL;
			this.Set(unAccountID, eUniverse, eAccountType);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0000E72D File Offset: 0x0000C92D
		public CSteamID(AccountID_t unAccountID, uint unAccountInstance, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.m_SteamID = 0UL;
			this.InstancedSet(unAccountID, unAccountInstance, eUniverse, eAccountType);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0000E742 File Offset: 0x0000C942
		public CSteamID(ulong ulSteamID)
		{
			this.m_SteamID = ulSteamID;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0000E74B File Offset: 0x0000C94B
		public void Set(AccountID_t unAccountID, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.SetAccountID(unAccountID);
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(eAccountType);
			if (eAccountType == EAccountType.k_EAccountTypeClan || eAccountType == EAccountType.k_EAccountTypeGameServer)
			{
				this.SetAccountInstance(0U);
				return;
			}
			this.SetAccountInstance(1U);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0000E779 File Offset: 0x0000C979
		public void InstancedSet(AccountID_t unAccountID, uint unInstance, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.SetAccountID(unAccountID);
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(eAccountType);
			this.SetAccountInstance(unInstance);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0000E798 File Offset: 0x0000C998
		public void Clear()
		{
			this.m_SteamID = 0UL;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0000E7A2 File Offset: 0x0000C9A2
		public void CreateBlankAnonLogon(EUniverse eUniverse)
		{
			this.SetAccountID(new AccountID_t(0U));
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(EAccountType.k_EAccountTypeAnonGameServer);
			this.SetAccountInstance(0U);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0000E7C5 File Offset: 0x0000C9C5
		public void CreateBlankAnonUserLogon(EUniverse eUniverse)
		{
			this.SetAccountID(new AccountID_t(0U));
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(EAccountType.k_EAccountTypeAnonUser);
			this.SetAccountInstance(0U);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0000E7E9 File Offset: 0x0000C9E9
		public bool BBlankAnonAccount()
		{
			return this.GetAccountID() == new AccountID_t(0U) && this.BAnonAccount() && this.GetUnAccountInstance() == 0U;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0000E811 File Offset: 0x0000CA11
		public bool BGameServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeGameServer || this.GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0000E827 File Offset: 0x0000CA27
		public bool BPersistentGameServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeGameServer;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0000E832 File Offset: 0x0000CA32
		public bool BAnonGameServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0000E83D File Offset: 0x0000CA3D
		public bool BContentServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeContentServer;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0000E848 File Offset: 0x0000CA48
		public bool BClanAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeClan;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0000E853 File Offset: 0x0000CA53
		public bool BChatAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeChat;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0000E85E File Offset: 0x0000CA5E
		public bool IsLobby()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeChat && (this.GetUnAccountInstance() & 262144U) > 0U;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0000E87A File Offset: 0x0000CA7A
		public bool BIndividualAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeIndividual || this.GetEAccountType() == EAccountType.k_EAccountTypeConsoleUser;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0000E891 File Offset: 0x0000CA91
		public bool BAnonAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeAnonUser || this.GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0000E8A8 File Offset: 0x0000CAA8
		public bool BAnonUserAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeAnonUser;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0000E8B4 File Offset: 0x0000CAB4
		public bool BConsoleUserAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeConsoleUser;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
		public void SetAccountID(AccountID_t other)
		{
			this.m_SteamID = ((this.m_SteamID & 18446744069414584320UL) | ((ulong)((uint)other) & (ulong)-1));
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0000E8E3 File Offset: 0x0000CAE3
		public void SetAccountInstance(uint other)
		{
			this.m_SteamID = ((this.m_SteamID & 18442240478377148415UL) | ((ulong)other & 1048575UL) << 32);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0000E908 File Offset: 0x0000CB08
		public void SetEAccountType(EAccountType other)
		{
			this.m_SteamID = ((this.m_SteamID & 18379190079298994175UL) | (ulong)((ulong)((long)other & 15L) << 52));
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0000E92A File Offset: 0x0000CB2A
		public void SetEUniverse(EUniverse other)
		{
			this.m_SteamID = ((this.m_SteamID & 72057594037927935UL) | (ulong)((ulong)((long)other & 255L) << 56));
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0000E94F File Offset: 0x0000CB4F
		public AccountID_t GetAccountID()
		{
			return new AccountID_t((uint)(this.m_SteamID & (ulong)-1));
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0000E960 File Offset: 0x0000CB60
		public uint GetUnAccountInstance()
		{
			return (uint)(this.m_SteamID >> 32 & 1048575UL);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0000E973 File Offset: 0x0000CB73
		public EAccountType GetEAccountType()
		{
			return (EAccountType)(this.m_SteamID >> 52 & 15UL);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0000E983 File Offset: 0x0000CB83
		public EUniverse GetEUniverse()
		{
			return (EUniverse)(this.m_SteamID >> 56 & 255UL);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0000E998 File Offset: 0x0000CB98
		public bool IsValid()
		{
			return this.GetEAccountType() > EAccountType.k_EAccountTypeInvalid && this.GetEAccountType() < EAccountType.k_EAccountTypeMax && this.GetEUniverse() > EUniverse.k_EUniverseInvalid && this.GetEUniverse() < EUniverse.k_EUniverseMax && (this.GetEAccountType() != EAccountType.k_EAccountTypeIndividual || (!(this.GetAccountID() == new AccountID_t(0U)) && this.GetUnAccountInstance() <= 1U)) && (this.GetEAccountType() != EAccountType.k_EAccountTypeClan || (!(this.GetAccountID() == new AccountID_t(0U)) && this.GetUnAccountInstance() == 0U)) && (this.GetEAccountType() != EAccountType.k_EAccountTypeGameServer || !(this.GetAccountID() == new AccountID_t(0U)));
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0000EA3A File Offset: 0x0000CC3A
		public override string ToString()
		{
			return this.m_SteamID.ToString();
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0000EA47 File Offset: 0x0000CC47
		public override bool Equals(object other)
		{
			return other is CSteamID && this == (CSteamID)other;
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0000EA64 File Offset: 0x0000CC64
		public override int GetHashCode()
		{
			return this.m_SteamID.GetHashCode();
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0000EA71 File Offset: 0x0000CC71
		public static bool operator ==(CSteamID x, CSteamID y)
		{
			return x.m_SteamID == y.m_SteamID;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0000EA81 File Offset: 0x0000CC81
		public static bool operator !=(CSteamID x, CSteamID y)
		{
			return !(x == y);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0000EA8D File Offset: 0x0000CC8D
		public static explicit operator CSteamID(ulong value)
		{
			return new CSteamID(value);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0000EA95 File Offset: 0x0000CC95
		public static explicit operator ulong(CSteamID that)
		{
			return that.m_SteamID;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0000EA9D File Offset: 0x0000CC9D
		public bool Equals(CSteamID other)
		{
			return this.m_SteamID == other.m_SteamID;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0000EAAD File Offset: 0x0000CCAD
		public int CompareTo(CSteamID other)
		{
			return this.m_SteamID.CompareTo(other.m_SteamID);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0000EAC0 File Offset: 0x0000CCC0
		// Note: this type is marked as 'beforefieldinit'.
		static CSteamID()
		{
		}

		// Token: 0x04000A96 RID: 2710
		public static readonly CSteamID Nil = default(CSteamID);

		// Token: 0x04000A97 RID: 2711
		public static readonly CSteamID OutofDateGS = new CSteamID(new AccountID_t(0U), 0U, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000A98 RID: 2712
		public static readonly CSteamID LanModeGS = new CSteamID(new AccountID_t(0U), 0U, EUniverse.k_EUniversePublic, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000A99 RID: 2713
		public static readonly CSteamID NotInitYetGS = new CSteamID(new AccountID_t(1U), 0U, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000A9A RID: 2714
		public static readonly CSteamID NonSteamGS = new CSteamID(new AccountID_t(2U), 0U, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000A9B RID: 2715
		public ulong m_SteamID;
	}
}
