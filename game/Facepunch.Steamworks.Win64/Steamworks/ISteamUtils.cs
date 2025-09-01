using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200002F RID: 47
	internal class ISteamUtils : SteamInterface
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x0000A452 File Offset: 0x00008652
		internal ISteamUtils(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x060006BF RID: 1727
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamUtils_v009();

		// Token: 0x060006C0 RID: 1728 RVA: 0x0000A464 File Offset: 0x00008664
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamUtils.SteamAPI_SteamUtils_v009();
		}

		// Token: 0x060006C1 RID: 1729
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamGameServerUtils_v009();

		// Token: 0x060006C2 RID: 1730 RVA: 0x0000A46B File Offset: 0x0000866B
		public override IntPtr GetServerInterfacePointer()
		{
			return ISteamUtils.SteamAPI_SteamGameServerUtils_v009();
		}

		// Token: 0x060006C3 RID: 1731
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetSecondsSinceAppActive")]
		private static extern uint _GetSecondsSinceAppActive(IntPtr self);

		// Token: 0x060006C4 RID: 1732 RVA: 0x0000A474 File Offset: 0x00008674
		internal uint GetSecondsSinceAppActive()
		{
			return ISteamUtils._GetSecondsSinceAppActive(this.Self);
		}

		// Token: 0x060006C5 RID: 1733
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetSecondsSinceComputerActive")]
		private static extern uint _GetSecondsSinceComputerActive(IntPtr self);

		// Token: 0x060006C6 RID: 1734 RVA: 0x0000A494 File Offset: 0x00008694
		internal uint GetSecondsSinceComputerActive()
		{
			return ISteamUtils._GetSecondsSinceComputerActive(this.Self);
		}

		// Token: 0x060006C7 RID: 1735
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetConnectedUniverse")]
		private static extern Universe _GetConnectedUniverse(IntPtr self);

		// Token: 0x060006C8 RID: 1736 RVA: 0x0000A4B4 File Offset: 0x000086B4
		internal Universe GetConnectedUniverse()
		{
			return ISteamUtils._GetConnectedUniverse(this.Self);
		}

		// Token: 0x060006C9 RID: 1737
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetServerRealTime")]
		private static extern uint _GetServerRealTime(IntPtr self);

		// Token: 0x060006CA RID: 1738 RVA: 0x0000A4D4 File Offset: 0x000086D4
		internal uint GetServerRealTime()
		{
			return ISteamUtils._GetServerRealTime(this.Self);
		}

		// Token: 0x060006CB RID: 1739
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetIPCountry")]
		private static extern Utf8StringPointer _GetIPCountry(IntPtr self);

		// Token: 0x060006CC RID: 1740 RVA: 0x0000A4F4 File Offset: 0x000086F4
		internal string GetIPCountry()
		{
			Utf8StringPointer p = ISteamUtils._GetIPCountry(this.Self);
			return p;
		}

		// Token: 0x060006CD RID: 1741
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetImageSize")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetImageSize(IntPtr self, int iImage, ref uint pnWidth, ref uint pnHeight);

		// Token: 0x060006CE RID: 1742 RVA: 0x0000A518 File Offset: 0x00008718
		internal bool GetImageSize(int iImage, ref uint pnWidth, ref uint pnHeight)
		{
			return ISteamUtils._GetImageSize(this.Self, iImage, ref pnWidth, ref pnHeight);
		}

		// Token: 0x060006CF RID: 1743
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetImageRGBA")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetImageRGBA(IntPtr self, int iImage, [In] [Out] byte[] pubDest, int nDestBufferSize);

		// Token: 0x060006D0 RID: 1744 RVA: 0x0000A53C File Offset: 0x0000873C
		internal bool GetImageRGBA(int iImage, [In] [Out] byte[] pubDest, int nDestBufferSize)
		{
			return ISteamUtils._GetImageRGBA(this.Self, iImage, pubDest, nDestBufferSize);
		}

		// Token: 0x060006D1 RID: 1745
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetCSERIPPort")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetCSERIPPort(IntPtr self, ref uint unIP, ref ushort usPort);

		// Token: 0x060006D2 RID: 1746 RVA: 0x0000A560 File Offset: 0x00008760
		internal bool GetCSERIPPort(ref uint unIP, ref ushort usPort)
		{
			return ISteamUtils._GetCSERIPPort(this.Self, ref unIP, ref usPort);
		}

		// Token: 0x060006D3 RID: 1747
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetCurrentBatteryPower")]
		private static extern byte _GetCurrentBatteryPower(IntPtr self);

		// Token: 0x060006D4 RID: 1748 RVA: 0x0000A584 File Offset: 0x00008784
		internal byte GetCurrentBatteryPower()
		{
			return ISteamUtils._GetCurrentBatteryPower(this.Self);
		}

		// Token: 0x060006D5 RID: 1749
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetAppID")]
		private static extern uint _GetAppID(IntPtr self);

		// Token: 0x060006D6 RID: 1750 RVA: 0x0000A5A4 File Offset: 0x000087A4
		internal uint GetAppID()
		{
			return ISteamUtils._GetAppID(this.Self);
		}

		// Token: 0x060006D7 RID: 1751
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_SetOverlayNotificationPosition")]
		private static extern void _SetOverlayNotificationPosition(IntPtr self, NotificationPosition eNotificationPosition);

		// Token: 0x060006D8 RID: 1752 RVA: 0x0000A5C3 File Offset: 0x000087C3
		internal void SetOverlayNotificationPosition(NotificationPosition eNotificationPosition)
		{
			ISteamUtils._SetOverlayNotificationPosition(this.Self, eNotificationPosition);
		}

		// Token: 0x060006D9 RID: 1753
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_IsAPICallCompleted")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IsAPICallCompleted(IntPtr self, SteamAPICall_t hSteamAPICall, [MarshalAs(UnmanagedType.U1)] ref bool pbFailed);

		// Token: 0x060006DA RID: 1754 RVA: 0x0000A5D4 File Offset: 0x000087D4
		internal bool IsAPICallCompleted(SteamAPICall_t hSteamAPICall, [MarshalAs(UnmanagedType.U1)] ref bool pbFailed)
		{
			return ISteamUtils._IsAPICallCompleted(this.Self, hSteamAPICall, ref pbFailed);
		}

		// Token: 0x060006DB RID: 1755
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetAPICallFailureReason")]
		private static extern SteamAPICallFailure _GetAPICallFailureReason(IntPtr self, SteamAPICall_t hSteamAPICall);

		// Token: 0x060006DC RID: 1756 RVA: 0x0000A5F8 File Offset: 0x000087F8
		internal SteamAPICallFailure GetAPICallFailureReason(SteamAPICall_t hSteamAPICall)
		{
			return ISteamUtils._GetAPICallFailureReason(this.Self, hSteamAPICall);
		}

		// Token: 0x060006DD RID: 1757
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetAPICallResult")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetAPICallResult(IntPtr self, SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, [MarshalAs(UnmanagedType.U1)] ref bool pbFailed);

		// Token: 0x060006DE RID: 1758 RVA: 0x0000A618 File Offset: 0x00008818
		internal bool GetAPICallResult(SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, [MarshalAs(UnmanagedType.U1)] ref bool pbFailed)
		{
			return ISteamUtils._GetAPICallResult(this.Self, hSteamAPICall, pCallback, cubCallback, iCallbackExpected, ref pbFailed);
		}

		// Token: 0x060006DF RID: 1759
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetIPCCallCount")]
		private static extern uint _GetIPCCallCount(IntPtr self);

		// Token: 0x060006E0 RID: 1760 RVA: 0x0000A640 File Offset: 0x00008840
		internal uint GetIPCCallCount()
		{
			return ISteamUtils._GetIPCCallCount(this.Self);
		}

		// Token: 0x060006E1 RID: 1761
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_SetWarningMessageHook")]
		private static extern void _SetWarningMessageHook(IntPtr self, IntPtr pFunction);

		// Token: 0x060006E2 RID: 1762 RVA: 0x0000A65F File Offset: 0x0000885F
		internal void SetWarningMessageHook(IntPtr pFunction)
		{
			ISteamUtils._SetWarningMessageHook(this.Self, pFunction);
		}

		// Token: 0x060006E3 RID: 1763
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_IsOverlayEnabled")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IsOverlayEnabled(IntPtr self);

		// Token: 0x060006E4 RID: 1764 RVA: 0x0000A670 File Offset: 0x00008870
		internal bool IsOverlayEnabled()
		{
			return ISteamUtils._IsOverlayEnabled(this.Self);
		}

		// Token: 0x060006E5 RID: 1765
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_BOverlayNeedsPresent")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BOverlayNeedsPresent(IntPtr self);

		// Token: 0x060006E6 RID: 1766 RVA: 0x0000A690 File Offset: 0x00008890
		internal bool BOverlayNeedsPresent()
		{
			return ISteamUtils._BOverlayNeedsPresent(this.Self);
		}

		// Token: 0x060006E7 RID: 1767
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_CheckFileSignature")]
		private static extern SteamAPICall_t _CheckFileSignature(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string szFileName);

		// Token: 0x060006E8 RID: 1768 RVA: 0x0000A6B0 File Offset: 0x000088B0
		internal CallResult<CheckFileSignature_t> CheckFileSignature([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string szFileName)
		{
			SteamAPICall_t call = ISteamUtils._CheckFileSignature(this.Self, szFileName);
			return new CallResult<CheckFileSignature_t>(call, base.IsServer);
		}

		// Token: 0x060006E9 RID: 1769
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_ShowGamepadTextInput")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _ShowGamepadTextInput(IntPtr self, GamepadTextInputMode eInputMode, GamepadTextInputLineMode eLineInputMode, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchDescription, uint unCharMax, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchExistingText);

		// Token: 0x060006EA RID: 1770 RVA: 0x0000A6DC File Offset: 0x000088DC
		internal bool ShowGamepadTextInput(GamepadTextInputMode eInputMode, GamepadTextInputLineMode eLineInputMode, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchDescription, uint unCharMax, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchExistingText)
		{
			return ISteamUtils._ShowGamepadTextInput(this.Self, eInputMode, eLineInputMode, pchDescription, unCharMax, pchExistingText);
		}

		// Token: 0x060006EB RID: 1771
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetEnteredGamepadTextLength")]
		private static extern uint _GetEnteredGamepadTextLength(IntPtr self);

		// Token: 0x060006EC RID: 1772 RVA: 0x0000A704 File Offset: 0x00008904
		internal uint GetEnteredGamepadTextLength()
		{
			return ISteamUtils._GetEnteredGamepadTextLength(this.Self);
		}

		// Token: 0x060006ED RID: 1773
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetEnteredGamepadTextInput")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetEnteredGamepadTextInput(IntPtr self, IntPtr pchText, uint cchText);

		// Token: 0x060006EE RID: 1774 RVA: 0x0000A724 File Offset: 0x00008924
		internal bool GetEnteredGamepadTextInput(out string pchText)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			bool result = ISteamUtils._GetEnteredGamepadTextInput(this.Self, intPtr, 32768U);
			pchText = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x060006EF RID: 1775
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetSteamUILanguage")]
		private static extern Utf8StringPointer _GetSteamUILanguage(IntPtr self);

		// Token: 0x060006F0 RID: 1776 RVA: 0x0000A758 File Offset: 0x00008958
		internal string GetSteamUILanguage()
		{
			Utf8StringPointer p = ISteamUtils._GetSteamUILanguage(this.Self);
			return p;
		}

		// Token: 0x060006F1 RID: 1777
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_IsSteamRunningInVR")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IsSteamRunningInVR(IntPtr self);

		// Token: 0x060006F2 RID: 1778 RVA: 0x0000A77C File Offset: 0x0000897C
		internal bool IsSteamRunningInVR()
		{
			return ISteamUtils._IsSteamRunningInVR(this.Self);
		}

		// Token: 0x060006F3 RID: 1779
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_SetOverlayNotificationInset")]
		private static extern void _SetOverlayNotificationInset(IntPtr self, int nHorizontalInset, int nVerticalInset);

		// Token: 0x060006F4 RID: 1780 RVA: 0x0000A79B File Offset: 0x0000899B
		internal void SetOverlayNotificationInset(int nHorizontalInset, int nVerticalInset)
		{
			ISteamUtils._SetOverlayNotificationInset(this.Self, nHorizontalInset, nVerticalInset);
		}

		// Token: 0x060006F5 RID: 1781
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_IsSteamInBigPictureMode")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IsSteamInBigPictureMode(IntPtr self);

		// Token: 0x060006F6 RID: 1782 RVA: 0x0000A7AC File Offset: 0x000089AC
		internal bool IsSteamInBigPictureMode()
		{
			return ISteamUtils._IsSteamInBigPictureMode(this.Self);
		}

		// Token: 0x060006F7 RID: 1783
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_StartVRDashboard")]
		private static extern void _StartVRDashboard(IntPtr self);

		// Token: 0x060006F8 RID: 1784 RVA: 0x0000A7CB File Offset: 0x000089CB
		internal void StartVRDashboard()
		{
			ISteamUtils._StartVRDashboard(this.Self);
		}

		// Token: 0x060006F9 RID: 1785
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_IsVRHeadsetStreamingEnabled")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IsVRHeadsetStreamingEnabled(IntPtr self);

		// Token: 0x060006FA RID: 1786 RVA: 0x0000A7DC File Offset: 0x000089DC
		internal bool IsVRHeadsetStreamingEnabled()
		{
			return ISteamUtils._IsVRHeadsetStreamingEnabled(this.Self);
		}

		// Token: 0x060006FB RID: 1787
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_SetVRHeadsetStreamingEnabled")]
		private static extern void _SetVRHeadsetStreamingEnabled(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bEnabled);

		// Token: 0x060006FC RID: 1788 RVA: 0x0000A7FB File Offset: 0x000089FB
		internal void SetVRHeadsetStreamingEnabled([MarshalAs(UnmanagedType.U1)] bool bEnabled)
		{
			ISteamUtils._SetVRHeadsetStreamingEnabled(this.Self, bEnabled);
		}

		// Token: 0x060006FD RID: 1789
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_IsSteamChinaLauncher")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IsSteamChinaLauncher(IntPtr self);

		// Token: 0x060006FE RID: 1790 RVA: 0x0000A80C File Offset: 0x00008A0C
		internal bool IsSteamChinaLauncher()
		{
			return ISteamUtils._IsSteamChinaLauncher(this.Self);
		}

		// Token: 0x060006FF RID: 1791
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_InitFilterText")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _InitFilterText(IntPtr self);

		// Token: 0x06000700 RID: 1792 RVA: 0x0000A82C File Offset: 0x00008A2C
		internal bool InitFilterText()
		{
			return ISteamUtils._InitFilterText(this.Self);
		}

		// Token: 0x06000701 RID: 1793
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_FilterText")]
		private static extern int _FilterText(IntPtr self, IntPtr pchOutFilteredText, uint nByteSizeOutFilteredText, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchInputMessage, [MarshalAs(UnmanagedType.U1)] bool bLegalOnly);

		// Token: 0x06000702 RID: 1794 RVA: 0x0000A84C File Offset: 0x00008A4C
		internal int FilterText(out string pchOutFilteredText, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchInputMessage, [MarshalAs(UnmanagedType.U1)] bool bLegalOnly)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			int result = ISteamUtils._FilterText(this.Self, intPtr, 32768U, pchInputMessage, bLegalOnly);
			pchOutFilteredText = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x06000703 RID: 1795
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetIPv6ConnectivityState")]
		private static extern SteamIPv6ConnectivityState _GetIPv6ConnectivityState(IntPtr self, SteamIPv6ConnectivityProtocol eProtocol);

		// Token: 0x06000704 RID: 1796 RVA: 0x0000A884 File Offset: 0x00008A84
		internal SteamIPv6ConnectivityState GetIPv6ConnectivityState(SteamIPv6ConnectivityProtocol eProtocol)
		{
			return ISteamUtils._GetIPv6ConnectivityState(this.Self, eProtocol);
		}
	}
}
