using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200002B RID: 43
	internal class ISteamTV : SteamInterface
	{
		// Token: 0x06000570 RID: 1392 RVA: 0x00008C96 File Offset: 0x00006E96
		internal ISteamTV(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x06000571 RID: 1393
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamTV_v001();

		// Token: 0x06000572 RID: 1394 RVA: 0x00008CA8 File Offset: 0x00006EA8
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamTV.SteamAPI_SteamTV_v001();
		}

		// Token: 0x06000573 RID: 1395
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTV_IsBroadcasting")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IsBroadcasting(IntPtr self, ref int pnNumViewers);

		// Token: 0x06000574 RID: 1396 RVA: 0x00008CB0 File Offset: 0x00006EB0
		internal bool IsBroadcasting(ref int pnNumViewers)
		{
			return ISteamTV._IsBroadcasting(this.Self, ref pnNumViewers);
		}

		// Token: 0x06000575 RID: 1397
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTV_AddBroadcastGameData")]
		private static extern void _AddBroadcastGameData(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValue);

		// Token: 0x06000576 RID: 1398 RVA: 0x00008CD0 File Offset: 0x00006ED0
		internal void AddBroadcastGameData([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValue)
		{
			ISteamTV._AddBroadcastGameData(this.Self, pchKey, pchValue);
		}

		// Token: 0x06000577 RID: 1399
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTV_RemoveBroadcastGameData")]
		private static extern void _RemoveBroadcastGameData(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey);

		// Token: 0x06000578 RID: 1400 RVA: 0x00008CE1 File Offset: 0x00006EE1
		internal void RemoveBroadcastGameData([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey)
		{
			ISteamTV._RemoveBroadcastGameData(this.Self, pchKey);
		}

		// Token: 0x06000579 RID: 1401
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTV_AddTimelineMarker")]
		private static extern void _AddTimelineMarker(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchTemplateName, [MarshalAs(UnmanagedType.U1)] bool bPersistent, byte nColorR, byte nColorG, byte nColorB);

		// Token: 0x0600057A RID: 1402 RVA: 0x00008CF1 File Offset: 0x00006EF1
		internal void AddTimelineMarker([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchTemplateName, [MarshalAs(UnmanagedType.U1)] bool bPersistent, byte nColorR, byte nColorG, byte nColorB)
		{
			ISteamTV._AddTimelineMarker(this.Self, pchTemplateName, bPersistent, nColorR, nColorG, nColorB);
		}

		// Token: 0x0600057B RID: 1403
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTV_RemoveTimelineMarker")]
		private static extern void _RemoveTimelineMarker(IntPtr self);

		// Token: 0x0600057C RID: 1404 RVA: 0x00008D07 File Offset: 0x00006F07
		internal void RemoveTimelineMarker()
		{
			ISteamTV._RemoveTimelineMarker(this.Self);
		}

		// Token: 0x0600057D RID: 1405
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTV_AddRegion")]
		private static extern uint _AddRegion(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchElementName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchTimelineDataSection, ref SteamTVRegion_t pSteamTVRegion, SteamTVRegionBehavior eSteamTVRegionBehavior);

		// Token: 0x0600057E RID: 1406 RVA: 0x00008D18 File Offset: 0x00006F18
		internal uint AddRegion([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchElementName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchTimelineDataSection, ref SteamTVRegion_t pSteamTVRegion, SteamTVRegionBehavior eSteamTVRegionBehavior)
		{
			return ISteamTV._AddRegion(this.Self, pchElementName, pchTimelineDataSection, ref pSteamTVRegion, eSteamTVRegionBehavior);
		}

		// Token: 0x0600057F RID: 1407
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTV_RemoveRegion")]
		private static extern void _RemoveRegion(IntPtr self, uint unRegionHandle);

		// Token: 0x06000580 RID: 1408 RVA: 0x00008D3C File Offset: 0x00006F3C
		internal void RemoveRegion(uint unRegionHandle)
		{
			ISteamTV._RemoveRegion(this.Self, unRegionHandle);
		}
	}
}
