using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000025 RID: 37
	public static class SteamUtils
	{
		// Token: 0x06000477 RID: 1143 RVA: 0x0000BA5C File Offset: 0x00009C5C
		public static uint GetSecondsSinceAppActive()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetSecondsSinceAppActive(CSteamAPIContext.GetSteamUtils());
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000BA6D File Offset: 0x00009C6D
		public static uint GetSecondsSinceComputerActive()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetSecondsSinceComputerActive(CSteamAPIContext.GetSteamUtils());
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000BA7E File Offset: 0x00009C7E
		public static EUniverse GetConnectedUniverse()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetConnectedUniverse(CSteamAPIContext.GetSteamUtils());
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000BA8F File Offset: 0x00009C8F
		public static uint GetServerRealTime()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetServerRealTime(CSteamAPIContext.GetSteamUtils());
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000BAA0 File Offset: 0x00009CA0
		public static string GetIPCountry()
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamUtils_GetIPCountry(CSteamAPIContext.GetSteamUtils()));
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000BAB6 File Offset: 0x00009CB6
		public static bool GetImageSize(int iImage, out uint pnWidth, out uint pnHeight)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetImageSize(CSteamAPIContext.GetSteamUtils(), iImage, out pnWidth, out pnHeight);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000BACA File Offset: 0x00009CCA
		public static bool GetImageRGBA(int iImage, byte[] pubDest, int nDestBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetImageRGBA(CSteamAPIContext.GetSteamUtils(), iImage, pubDest, nDestBufferSize);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000BADE File Offset: 0x00009CDE
		public static byte GetCurrentBatteryPower()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetCurrentBatteryPower(CSteamAPIContext.GetSteamUtils());
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000BAEF File Offset: 0x00009CEF
		public static AppId_t GetAppID()
		{
			InteropHelp.TestIfAvailableClient();
			return (AppId_t)NativeMethods.ISteamUtils_GetAppID(CSteamAPIContext.GetSteamUtils());
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000BB05 File Offset: 0x00009D05
		public static void SetOverlayNotificationPosition(ENotificationPosition eNotificationPosition)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUtils_SetOverlayNotificationPosition(CSteamAPIContext.GetSteamUtils(), eNotificationPosition);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000BB17 File Offset: 0x00009D17
		public static bool IsAPICallCompleted(SteamAPICall_t hSteamAPICall, out bool pbFailed)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_IsAPICallCompleted(CSteamAPIContext.GetSteamUtils(), hSteamAPICall, out pbFailed);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000BB2A File Offset: 0x00009D2A
		public static ESteamAPICallFailure GetAPICallFailureReason(SteamAPICall_t hSteamAPICall)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetAPICallFailureReason(CSteamAPIContext.GetSteamUtils(), hSteamAPICall);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000BB3C File Offset: 0x00009D3C
		public static bool GetAPICallResult(SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, out bool pbFailed)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetAPICallResult(CSteamAPIContext.GetSteamUtils(), hSteamAPICall, pCallback, cubCallback, iCallbackExpected, out pbFailed);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000BB53 File Offset: 0x00009D53
		public static uint GetIPCCallCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetIPCCallCount(CSteamAPIContext.GetSteamUtils());
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000BB64 File Offset: 0x00009D64
		public static void SetWarningMessageHook(SteamAPIWarningMessageHook_t pFunction)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUtils_SetWarningMessageHook(CSteamAPIContext.GetSteamUtils(), pFunction);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000BB76 File Offset: 0x00009D76
		public static bool IsOverlayEnabled()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_IsOverlayEnabled(CSteamAPIContext.GetSteamUtils());
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000BB87 File Offset: 0x00009D87
		public static bool BOverlayNeedsPresent()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_BOverlayNeedsPresent(CSteamAPIContext.GetSteamUtils());
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000BB98 File Offset: 0x00009D98
		public static SteamAPICall_t CheckFileSignature(string szFileName)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(szFileName))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamUtils_CheckFileSignature(CSteamAPIContext.GetSteamUtils(), utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000BBE0 File Offset: 0x00009DE0
		public static bool ShowGamepadTextInput(EGamepadTextInputMode eInputMode, EGamepadTextInputLineMode eLineInputMode, string pchDescription, uint unCharMax, string pchExistingText)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchDescription))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchExistingText))
				{
					result = NativeMethods.ISteamUtils_ShowGamepadTextInput(CSteamAPIContext.GetSteamUtils(), eInputMode, eLineInputMode, utf8StringHandle, unCharMax, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000BC44 File Offset: 0x00009E44
		public static uint GetEnteredGamepadTextLength()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetEnteredGamepadTextLength(CSteamAPIContext.GetSteamUtils());
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000BC58 File Offset: 0x00009E58
		public static bool GetEnteredGamepadTextInput(out string pchText, uint cchText)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchText);
			bool flag = NativeMethods.ISteamUtils_GetEnteredGamepadTextInput(CSteamAPIContext.GetSteamUtils(), intPtr, cchText);
			pchText = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000BC93 File Offset: 0x00009E93
		public static string GetSteamUILanguage()
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamUtils_GetSteamUILanguage(CSteamAPIContext.GetSteamUtils()));
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000BCA9 File Offset: 0x00009EA9
		public static bool IsSteamRunningInVR()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_IsSteamRunningInVR(CSteamAPIContext.GetSteamUtils());
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000BCBA File Offset: 0x00009EBA
		public static void SetOverlayNotificationInset(int nHorizontalInset, int nVerticalInset)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUtils_SetOverlayNotificationInset(CSteamAPIContext.GetSteamUtils(), nHorizontalInset, nVerticalInset);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000BCCD File Offset: 0x00009ECD
		public static bool IsSteamInBigPictureMode()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_IsSteamInBigPictureMode(CSteamAPIContext.GetSteamUtils());
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000BCDE File Offset: 0x00009EDE
		public static void StartVRDashboard()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUtils_StartVRDashboard(CSteamAPIContext.GetSteamUtils());
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000BCEF File Offset: 0x00009EEF
		public static bool IsVRHeadsetStreamingEnabled()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_IsVRHeadsetStreamingEnabled(CSteamAPIContext.GetSteamUtils());
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000BD00 File Offset: 0x00009F00
		public static void SetVRHeadsetStreamingEnabled(bool bEnabled)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUtils_SetVRHeadsetStreamingEnabled(CSteamAPIContext.GetSteamUtils(), bEnabled);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000BD12 File Offset: 0x00009F12
		public static bool IsSteamChinaLauncher()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_IsSteamChinaLauncher(CSteamAPIContext.GetSteamUtils());
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000BD23 File Offset: 0x00009F23
		public static bool InitFilterText(uint unFilterOptions = 0U)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_InitFilterText(CSteamAPIContext.GetSteamUtils(), unFilterOptions);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000BD38 File Offset: 0x00009F38
		public static int FilterText(ETextFilteringContext eContext, CSteamID sourceSteamID, string pchInputMessage, out string pchOutFilteredText, uint nByteSizeOutFilteredText)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal((int)nByteSizeOutFilteredText);
			int result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchInputMessage))
			{
				int num = NativeMethods.ISteamUtils_FilterText(CSteamAPIContext.GetSteamUtils(), eContext, sourceSteamID, utf8StringHandle, intPtr, nByteSizeOutFilteredText);
				pchOutFilteredText = ((num != -1) ? InteropHelp.PtrToStringUTF8(intPtr) : null);
				Marshal.FreeHGlobal(intPtr);
				result = num;
			}
			return result;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000BDA0 File Offset: 0x00009FA0
		public static ESteamIPv6ConnectivityState GetIPv6ConnectivityState(ESteamIPv6ConnectivityProtocol eProtocol)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetIPv6ConnectivityState(CSteamAPIContext.GetSteamUtils(), eProtocol);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000BDB2 File Offset: 0x00009FB2
		public static bool IsSteamRunningOnSteamDeck()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_IsSteamRunningOnSteamDeck(CSteamAPIContext.GetSteamUtils());
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000BDC3 File Offset: 0x00009FC3
		public static bool ShowFloatingGamepadTextInput(EFloatingGamepadTextInputMode eKeyboardMode, int nTextFieldXPosition, int nTextFieldYPosition, int nTextFieldWidth, int nTextFieldHeight)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_ShowFloatingGamepadTextInput(CSteamAPIContext.GetSteamUtils(), eKeyboardMode, nTextFieldXPosition, nTextFieldYPosition, nTextFieldWidth, nTextFieldHeight);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000BDDA File Offset: 0x00009FDA
		public static void SetGameLauncherMode(bool bLauncherMode)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUtils_SetGameLauncherMode(CSteamAPIContext.GetSteamUtils(), bLauncherMode);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000BDEC File Offset: 0x00009FEC
		public static bool DismissFloatingGamepadTextInput()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_DismissFloatingGamepadTextInput(CSteamAPIContext.GetSteamUtils());
		}
	}
}
