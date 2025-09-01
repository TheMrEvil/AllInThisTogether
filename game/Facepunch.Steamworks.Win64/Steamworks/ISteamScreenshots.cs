using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200002A RID: 42
	internal class ISteamScreenshots : SteamInterface
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x00008B65 File Offset: 0x00006D65
		internal ISteamScreenshots(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x0600055C RID: 1372
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamScreenshots_v003();

		// Token: 0x0600055D RID: 1373 RVA: 0x00008B77 File Offset: 0x00006D77
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamScreenshots.SteamAPI_SteamScreenshots_v003();
		}

		// Token: 0x0600055E RID: 1374
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_WriteScreenshot")]
		private static extern ScreenshotHandle _WriteScreenshot(IntPtr self, IntPtr pubRGB, uint cubRGB, int nWidth, int nHeight);

		// Token: 0x0600055F RID: 1375 RVA: 0x00008B80 File Offset: 0x00006D80
		internal ScreenshotHandle WriteScreenshot(IntPtr pubRGB, uint cubRGB, int nWidth, int nHeight)
		{
			return ISteamScreenshots._WriteScreenshot(this.Self, pubRGB, cubRGB, nWidth, nHeight);
		}

		// Token: 0x06000560 RID: 1376
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_AddScreenshotToLibrary")]
		private static extern ScreenshotHandle _AddScreenshotToLibrary(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFilename, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchThumbnailFilename, int nWidth, int nHeight);

		// Token: 0x06000561 RID: 1377 RVA: 0x00008BA4 File Offset: 0x00006DA4
		internal ScreenshotHandle AddScreenshotToLibrary([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFilename, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchThumbnailFilename, int nWidth, int nHeight)
		{
			return ISteamScreenshots._AddScreenshotToLibrary(this.Self, pchFilename, pchThumbnailFilename, nWidth, nHeight);
		}

		// Token: 0x06000562 RID: 1378
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_TriggerScreenshot")]
		private static extern void _TriggerScreenshot(IntPtr self);

		// Token: 0x06000563 RID: 1379 RVA: 0x00008BC8 File Offset: 0x00006DC8
		internal void TriggerScreenshot()
		{
			ISteamScreenshots._TriggerScreenshot(this.Self);
		}

		// Token: 0x06000564 RID: 1380
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_HookScreenshots")]
		private static extern void _HookScreenshots(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bHook);

		// Token: 0x06000565 RID: 1381 RVA: 0x00008BD7 File Offset: 0x00006DD7
		internal void HookScreenshots([MarshalAs(UnmanagedType.U1)] bool bHook)
		{
			ISteamScreenshots._HookScreenshots(this.Self, bHook);
		}

		// Token: 0x06000566 RID: 1382
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_SetLocation")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetLocation(IntPtr self, ScreenshotHandle hScreenshot, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchLocation);

		// Token: 0x06000567 RID: 1383 RVA: 0x00008BE8 File Offset: 0x00006DE8
		internal bool SetLocation(ScreenshotHandle hScreenshot, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchLocation)
		{
			return ISteamScreenshots._SetLocation(this.Self, hScreenshot, pchLocation);
		}

		// Token: 0x06000568 RID: 1384
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_TagUser")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _TagUser(IntPtr self, ScreenshotHandle hScreenshot, SteamId steamID);

		// Token: 0x06000569 RID: 1385 RVA: 0x00008C0C File Offset: 0x00006E0C
		internal bool TagUser(ScreenshotHandle hScreenshot, SteamId steamID)
		{
			return ISteamScreenshots._TagUser(this.Self, hScreenshot, steamID);
		}

		// Token: 0x0600056A RID: 1386
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_TagPublishedFile")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _TagPublishedFile(IntPtr self, ScreenshotHandle hScreenshot, PublishedFileId unPublishedFileID);

		// Token: 0x0600056B RID: 1387 RVA: 0x00008C30 File Offset: 0x00006E30
		internal bool TagPublishedFile(ScreenshotHandle hScreenshot, PublishedFileId unPublishedFileID)
		{
			return ISteamScreenshots._TagPublishedFile(this.Self, hScreenshot, unPublishedFileID);
		}

		// Token: 0x0600056C RID: 1388
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_IsScreenshotsHooked")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IsScreenshotsHooked(IntPtr self);

		// Token: 0x0600056D RID: 1389 RVA: 0x00008C54 File Offset: 0x00006E54
		internal bool IsScreenshotsHooked()
		{
			return ISteamScreenshots._IsScreenshotsHooked(this.Self);
		}

		// Token: 0x0600056E RID: 1390
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_AddVRScreenshotToLibrary")]
		private static extern ScreenshotHandle _AddVRScreenshotToLibrary(IntPtr self, VRScreenshotType eType, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFilename, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVRFilename);

		// Token: 0x0600056F RID: 1391 RVA: 0x00008C74 File Offset: 0x00006E74
		internal ScreenshotHandle AddVRScreenshotToLibrary(VRScreenshotType eType, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFilename, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVRFilename)
		{
			return ISteamScreenshots._AddVRScreenshotToLibrary(this.Self, eType, pchFilename, pchVRFilename);
		}
	}
}
