using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000027 RID: 39
	internal class ISteamParties : SteamInterface
	{
		// Token: 0x060004EE RID: 1262 RVA: 0x00008420 File Offset: 0x00006620
		internal ISteamParties(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x060004EF RID: 1263
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamParties_v002();

		// Token: 0x060004F0 RID: 1264 RVA: 0x00008432 File Offset: 0x00006632
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamParties.SteamAPI_SteamParties_v002();
		}

		// Token: 0x060004F1 RID: 1265
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_GetNumActiveBeacons")]
		private static extern uint _GetNumActiveBeacons(IntPtr self);

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000843C File Offset: 0x0000663C
		internal uint GetNumActiveBeacons()
		{
			return ISteamParties._GetNumActiveBeacons(this.Self);
		}

		// Token: 0x060004F3 RID: 1267
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_GetBeaconByIndex")]
		private static extern PartyBeaconID_t _GetBeaconByIndex(IntPtr self, uint unIndex);

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000845C File Offset: 0x0000665C
		internal PartyBeaconID_t GetBeaconByIndex(uint unIndex)
		{
			return ISteamParties._GetBeaconByIndex(this.Self, unIndex);
		}

		// Token: 0x060004F5 RID: 1269
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_GetBeaconDetails")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetBeaconDetails(IntPtr self, PartyBeaconID_t ulBeaconID, ref SteamId pSteamIDBeaconOwner, ref SteamPartyBeaconLocation_t pLocation, IntPtr pchMetadata, int cchMetadata);

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000847C File Offset: 0x0000667C
		internal bool GetBeaconDetails(PartyBeaconID_t ulBeaconID, ref SteamId pSteamIDBeaconOwner, ref SteamPartyBeaconLocation_t pLocation, out string pchMetadata)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			bool result = ISteamParties._GetBeaconDetails(this.Self, ulBeaconID, ref pSteamIDBeaconOwner, ref pLocation, intPtr, 32768);
			pchMetadata = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x060004F7 RID: 1271
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_JoinParty")]
		private static extern SteamAPICall_t _JoinParty(IntPtr self, PartyBeaconID_t ulBeaconID);

		// Token: 0x060004F8 RID: 1272 RVA: 0x000084B4 File Offset: 0x000066B4
		internal CallResult<JoinPartyCallback_t> JoinParty(PartyBeaconID_t ulBeaconID)
		{
			SteamAPICall_t call = ISteamParties._JoinParty(this.Self, ulBeaconID);
			return new CallResult<JoinPartyCallback_t>(call, base.IsServer);
		}

		// Token: 0x060004F9 RID: 1273
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_GetNumAvailableBeaconLocations")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetNumAvailableBeaconLocations(IntPtr self, ref uint puNumLocations);

		// Token: 0x060004FA RID: 1274 RVA: 0x000084E0 File Offset: 0x000066E0
		internal bool GetNumAvailableBeaconLocations(ref uint puNumLocations)
		{
			return ISteamParties._GetNumAvailableBeaconLocations(this.Self, ref puNumLocations);
		}

		// Token: 0x060004FB RID: 1275
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_GetAvailableBeaconLocations")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetAvailableBeaconLocations(IntPtr self, ref SteamPartyBeaconLocation_t pLocationList, uint uMaxNumLocations);

		// Token: 0x060004FC RID: 1276 RVA: 0x00008500 File Offset: 0x00006700
		internal bool GetAvailableBeaconLocations(ref SteamPartyBeaconLocation_t pLocationList, uint uMaxNumLocations)
		{
			return ISteamParties._GetAvailableBeaconLocations(this.Self, ref pLocationList, uMaxNumLocations);
		}

		// Token: 0x060004FD RID: 1277
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_CreateBeacon")]
		private static extern SteamAPICall_t _CreateBeacon(IntPtr self, uint unOpenSlots, ref SteamPartyBeaconLocation_t pBeaconLocation, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchConnectString, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchMetadata);

		// Token: 0x060004FE RID: 1278 RVA: 0x00008524 File Offset: 0x00006724
		internal CallResult<CreateBeaconCallback_t> CreateBeacon(uint unOpenSlots, SteamPartyBeaconLocation_t pBeaconLocation, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchConnectString, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchMetadata)
		{
			SteamAPICall_t call = ISteamParties._CreateBeacon(this.Self, unOpenSlots, ref pBeaconLocation, pchConnectString, pchMetadata);
			return new CallResult<CreateBeaconCallback_t>(call, base.IsServer);
		}

		// Token: 0x060004FF RID: 1279
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_OnReservationCompleted")]
		private static extern void _OnReservationCompleted(IntPtr self, PartyBeaconID_t ulBeacon, SteamId steamIDUser);

		// Token: 0x06000500 RID: 1280 RVA: 0x00008554 File Offset: 0x00006754
		internal void OnReservationCompleted(PartyBeaconID_t ulBeacon, SteamId steamIDUser)
		{
			ISteamParties._OnReservationCompleted(this.Self, ulBeacon, steamIDUser);
		}

		// Token: 0x06000501 RID: 1281
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_CancelReservation")]
		private static extern void _CancelReservation(IntPtr self, PartyBeaconID_t ulBeacon, SteamId steamIDUser);

		// Token: 0x06000502 RID: 1282 RVA: 0x00008565 File Offset: 0x00006765
		internal void CancelReservation(PartyBeaconID_t ulBeacon, SteamId steamIDUser)
		{
			ISteamParties._CancelReservation(this.Self, ulBeacon, steamIDUser);
		}

		// Token: 0x06000503 RID: 1283
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_ChangeNumOpenSlots")]
		private static extern SteamAPICall_t _ChangeNumOpenSlots(IntPtr self, PartyBeaconID_t ulBeacon, uint unOpenSlots);

		// Token: 0x06000504 RID: 1284 RVA: 0x00008578 File Offset: 0x00006778
		internal CallResult<ChangeNumOpenSlotsCallback_t> ChangeNumOpenSlots(PartyBeaconID_t ulBeacon, uint unOpenSlots)
		{
			SteamAPICall_t call = ISteamParties._ChangeNumOpenSlots(this.Self, ulBeacon, unOpenSlots);
			return new CallResult<ChangeNumOpenSlotsCallback_t>(call, base.IsServer);
		}

		// Token: 0x06000505 RID: 1285
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_DestroyBeacon")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _DestroyBeacon(IntPtr self, PartyBeaconID_t ulBeacon);

		// Token: 0x06000506 RID: 1286 RVA: 0x000085A4 File Offset: 0x000067A4
		internal bool DestroyBeacon(PartyBeaconID_t ulBeacon)
		{
			return ISteamParties._DestroyBeacon(this.Self, ulBeacon);
		}

		// Token: 0x06000507 RID: 1287
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_GetBeaconLocationData")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetBeaconLocationData(IntPtr self, SteamPartyBeaconLocation_t BeaconLocation, SteamPartyBeaconLocationData eData, IntPtr pchDataStringOut, int cchDataStringOut);

		// Token: 0x06000508 RID: 1288 RVA: 0x000085C4 File Offset: 0x000067C4
		internal bool GetBeaconLocationData(SteamPartyBeaconLocation_t BeaconLocation, SteamPartyBeaconLocationData eData, out string pchDataStringOut)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			bool result = ISteamParties._GetBeaconLocationData(this.Self, BeaconLocation, eData, intPtr, 32768);
			pchDataStringOut = Helpers.MemoryToString(intPtr);
			return result;
		}
	}
}
