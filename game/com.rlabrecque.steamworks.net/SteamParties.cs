using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000017 RID: 23
	public static class SteamParties
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x00008495 File Offset: 0x00006695
		public static uint GetNumActiveBeacons()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamParties_GetNumActiveBeacons(CSteamAPIContext.GetSteamParties());
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000084A6 File Offset: 0x000066A6
		public static PartyBeaconID_t GetBeaconByIndex(uint unIndex)
		{
			InteropHelp.TestIfAvailableClient();
			return (PartyBeaconID_t)NativeMethods.ISteamParties_GetBeaconByIndex(CSteamAPIContext.GetSteamParties(), unIndex);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000084C0 File Offset: 0x000066C0
		public static bool GetBeaconDetails(PartyBeaconID_t ulBeaconID, out CSteamID pSteamIDBeaconOwner, out SteamPartyBeaconLocation_t pLocation, out string pchMetadata, int cchMetadata)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cchMetadata);
			bool flag = NativeMethods.ISteamParties_GetBeaconDetails(CSteamAPIContext.GetSteamParties(), ulBeaconID, out pSteamIDBeaconOwner, out pLocation, intPtr, cchMetadata);
			pchMetadata = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00008500 File Offset: 0x00006700
		public static SteamAPICall_t JoinParty(PartyBeaconID_t ulBeaconID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamParties_JoinParty(CSteamAPIContext.GetSteamParties(), ulBeaconID);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00008517 File Offset: 0x00006717
		public static bool GetNumAvailableBeaconLocations(out uint puNumLocations)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamParties_GetNumAvailableBeaconLocations(CSteamAPIContext.GetSteamParties(), out puNumLocations);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00008529 File Offset: 0x00006729
		public static bool GetAvailableBeaconLocations(SteamPartyBeaconLocation_t[] pLocationList, uint uMaxNumLocations)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamParties_GetAvailableBeaconLocations(CSteamAPIContext.GetSteamParties(), pLocationList, uMaxNumLocations);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000853C File Offset: 0x0000673C
		public static SteamAPICall_t CreateBeacon(uint unOpenSlots, ref SteamPartyBeaconLocation_t pBeaconLocation, string pchConnectString, string pchMetadata)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchConnectString))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchMetadata))
				{
					result = (SteamAPICall_t)NativeMethods.ISteamParties_CreateBeacon(CSteamAPIContext.GetSteamParties(), unOpenSlots, ref pBeaconLocation, utf8StringHandle, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000085A4 File Offset: 0x000067A4
		public static void OnReservationCompleted(PartyBeaconID_t ulBeacon, CSteamID steamIDUser)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamParties_OnReservationCompleted(CSteamAPIContext.GetSteamParties(), ulBeacon, steamIDUser);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000085B7 File Offset: 0x000067B7
		public static void CancelReservation(PartyBeaconID_t ulBeacon, CSteamID steamIDUser)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamParties_CancelReservation(CSteamAPIContext.GetSteamParties(), ulBeacon, steamIDUser);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000085CA File Offset: 0x000067CA
		public static SteamAPICall_t ChangeNumOpenSlots(PartyBeaconID_t ulBeacon, uint unOpenSlots)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamParties_ChangeNumOpenSlots(CSteamAPIContext.GetSteamParties(), ulBeacon, unOpenSlots);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000085E2 File Offset: 0x000067E2
		public static bool DestroyBeacon(PartyBeaconID_t ulBeacon)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamParties_DestroyBeacon(CSteamAPIContext.GetSteamParties(), ulBeacon);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000085F4 File Offset: 0x000067F4
		public static bool GetBeaconLocationData(SteamPartyBeaconLocation_t BeaconLocation, ESteamPartyBeaconLocationData eData, out string pchDataStringOut, int cchDataStringOut)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cchDataStringOut);
			bool flag = NativeMethods.ISteamParties_GetBeaconLocationData(CSteamAPIContext.GetSteamParties(), BeaconLocation, eData, intPtr, cchDataStringOut);
			pchDataStringOut = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}
	}
}
