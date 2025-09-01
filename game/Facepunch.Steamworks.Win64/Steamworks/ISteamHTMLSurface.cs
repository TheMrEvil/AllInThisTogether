using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000015 RID: 21
	internal class ISteamHTMLSurface : SteamInterface
	{
		// Token: 0x0600022A RID: 554 RVA: 0x00005AA7 File Offset: 0x00003CA7
		internal ISteamHTMLSurface(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x0600022B RID: 555
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamHTMLSurface_v005();

		// Token: 0x0600022C RID: 556 RVA: 0x00005AB9 File Offset: 0x00003CB9
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamHTMLSurface.SteamAPI_SteamHTMLSurface_v005();
		}

		// Token: 0x0600022D RID: 557
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_Init")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _Init(IntPtr self);

		// Token: 0x0600022E RID: 558 RVA: 0x00005AC0 File Offset: 0x00003CC0
		internal bool Init()
		{
			return ISteamHTMLSurface._Init(this.Self);
		}

		// Token: 0x0600022F RID: 559
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_Shutdown")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _Shutdown(IntPtr self);

		// Token: 0x06000230 RID: 560 RVA: 0x00005AE0 File Offset: 0x00003CE0
		internal bool Shutdown()
		{
			return ISteamHTMLSurface._Shutdown(this.Self);
		}

		// Token: 0x06000231 RID: 561
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_CreateBrowser")]
		private static extern SteamAPICall_t _CreateBrowser(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchUserAgent, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchUserCSS);

		// Token: 0x06000232 RID: 562 RVA: 0x00005B00 File Offset: 0x00003D00
		internal CallResult<HTML_BrowserReady_t> CreateBrowser([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchUserAgent, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchUserCSS)
		{
			SteamAPICall_t call = ISteamHTMLSurface._CreateBrowser(this.Self, pchUserAgent, pchUserCSS);
			return new CallResult<HTML_BrowserReady_t>(call, base.IsServer);
		}

		// Token: 0x06000233 RID: 563
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_RemoveBrowser")]
		private static extern void _RemoveBrowser(IntPtr self, HHTMLBrowser unBrowserHandle);

		// Token: 0x06000234 RID: 564 RVA: 0x00005B2C File Offset: 0x00003D2C
		internal void RemoveBrowser(HHTMLBrowser unBrowserHandle)
		{
			ISteamHTMLSurface._RemoveBrowser(this.Self, unBrowserHandle);
		}

		// Token: 0x06000235 RID: 565
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_LoadURL")]
		private static extern void _LoadURL(IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchURL, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPostData);

		// Token: 0x06000236 RID: 566 RVA: 0x00005B3C File Offset: 0x00003D3C
		internal void LoadURL(HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchURL, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPostData)
		{
			ISteamHTMLSurface._LoadURL(this.Self, unBrowserHandle, pchURL, pchPostData);
		}

		// Token: 0x06000237 RID: 567
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetSize")]
		private static extern void _SetSize(IntPtr self, HHTMLBrowser unBrowserHandle, uint unWidth, uint unHeight);

		// Token: 0x06000238 RID: 568 RVA: 0x00005B4E File Offset: 0x00003D4E
		internal void SetSize(HHTMLBrowser unBrowserHandle, uint unWidth, uint unHeight)
		{
			ISteamHTMLSurface._SetSize(this.Self, unBrowserHandle, unWidth, unHeight);
		}

		// Token: 0x06000239 RID: 569
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_StopLoad")]
		private static extern void _StopLoad(IntPtr self, HHTMLBrowser unBrowserHandle);

		// Token: 0x0600023A RID: 570 RVA: 0x00005B60 File Offset: 0x00003D60
		internal void StopLoad(HHTMLBrowser unBrowserHandle)
		{
			ISteamHTMLSurface._StopLoad(this.Self, unBrowserHandle);
		}

		// Token: 0x0600023B RID: 571
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_Reload")]
		private static extern void _Reload(IntPtr self, HHTMLBrowser unBrowserHandle);

		// Token: 0x0600023C RID: 572 RVA: 0x00005B70 File Offset: 0x00003D70
		internal void Reload(HHTMLBrowser unBrowserHandle)
		{
			ISteamHTMLSurface._Reload(this.Self, unBrowserHandle);
		}

		// Token: 0x0600023D RID: 573
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_GoBack")]
		private static extern void _GoBack(IntPtr self, HHTMLBrowser unBrowserHandle);

		// Token: 0x0600023E RID: 574 RVA: 0x00005B80 File Offset: 0x00003D80
		internal void GoBack(HHTMLBrowser unBrowserHandle)
		{
			ISteamHTMLSurface._GoBack(this.Self, unBrowserHandle);
		}

		// Token: 0x0600023F RID: 575
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_GoForward")]
		private static extern void _GoForward(IntPtr self, HHTMLBrowser unBrowserHandle);

		// Token: 0x06000240 RID: 576 RVA: 0x00005B90 File Offset: 0x00003D90
		internal void GoForward(HHTMLBrowser unBrowserHandle)
		{
			ISteamHTMLSurface._GoForward(this.Self, unBrowserHandle);
		}

		// Token: 0x06000241 RID: 577
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_AddHeader")]
		private static extern void _AddHeader(IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValue);

		// Token: 0x06000242 RID: 578 RVA: 0x00005BA0 File Offset: 0x00003DA0
		internal void AddHeader(HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValue)
		{
			ISteamHTMLSurface._AddHeader(this.Self, unBrowserHandle, pchKey, pchValue);
		}

		// Token: 0x06000243 RID: 579
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_ExecuteJavascript")]
		private static extern void _ExecuteJavascript(IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchScript);

		// Token: 0x06000244 RID: 580 RVA: 0x00005BB2 File Offset: 0x00003DB2
		internal void ExecuteJavascript(HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchScript)
		{
			ISteamHTMLSurface._ExecuteJavascript(this.Self, unBrowserHandle, pchScript);
		}

		// Token: 0x06000245 RID: 581
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseUp")]
		private static extern void _MouseUp(IntPtr self, HHTMLBrowser unBrowserHandle, IntPtr eMouseButton);

		// Token: 0x06000246 RID: 582 RVA: 0x00005BC3 File Offset: 0x00003DC3
		internal void MouseUp(HHTMLBrowser unBrowserHandle, IntPtr eMouseButton)
		{
			ISteamHTMLSurface._MouseUp(this.Self, unBrowserHandle, eMouseButton);
		}

		// Token: 0x06000247 RID: 583
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseDown")]
		private static extern void _MouseDown(IntPtr self, HHTMLBrowser unBrowserHandle, IntPtr eMouseButton);

		// Token: 0x06000248 RID: 584 RVA: 0x00005BD4 File Offset: 0x00003DD4
		internal void MouseDown(HHTMLBrowser unBrowserHandle, IntPtr eMouseButton)
		{
			ISteamHTMLSurface._MouseDown(this.Self, unBrowserHandle, eMouseButton);
		}

		// Token: 0x06000249 RID: 585
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseDoubleClick")]
		private static extern void _MouseDoubleClick(IntPtr self, HHTMLBrowser unBrowserHandle, IntPtr eMouseButton);

		// Token: 0x0600024A RID: 586 RVA: 0x00005BE5 File Offset: 0x00003DE5
		internal void MouseDoubleClick(HHTMLBrowser unBrowserHandle, IntPtr eMouseButton)
		{
			ISteamHTMLSurface._MouseDoubleClick(this.Self, unBrowserHandle, eMouseButton);
		}

		// Token: 0x0600024B RID: 587
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseMove")]
		private static extern void _MouseMove(IntPtr self, HHTMLBrowser unBrowserHandle, int x, int y);

		// Token: 0x0600024C RID: 588 RVA: 0x00005BF6 File Offset: 0x00003DF6
		internal void MouseMove(HHTMLBrowser unBrowserHandle, int x, int y)
		{
			ISteamHTMLSurface._MouseMove(this.Self, unBrowserHandle, x, y);
		}

		// Token: 0x0600024D RID: 589
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseWheel")]
		private static extern void _MouseWheel(IntPtr self, HHTMLBrowser unBrowserHandle, int nDelta);

		// Token: 0x0600024E RID: 590 RVA: 0x00005C08 File Offset: 0x00003E08
		internal void MouseWheel(HHTMLBrowser unBrowserHandle, int nDelta)
		{
			ISteamHTMLSurface._MouseWheel(this.Self, unBrowserHandle, nDelta);
		}

		// Token: 0x0600024F RID: 591
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_KeyDown")]
		private static extern void _KeyDown(IntPtr self, HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, IntPtr eHTMLKeyModifiers, [MarshalAs(UnmanagedType.U1)] bool bIsSystemKey);

		// Token: 0x06000250 RID: 592 RVA: 0x00005C19 File Offset: 0x00003E19
		internal void KeyDown(HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, IntPtr eHTMLKeyModifiers, [MarshalAs(UnmanagedType.U1)] bool bIsSystemKey)
		{
			ISteamHTMLSurface._KeyDown(this.Self, unBrowserHandle, nNativeKeyCode, eHTMLKeyModifiers, bIsSystemKey);
		}

		// Token: 0x06000251 RID: 593
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_KeyUp")]
		private static extern void _KeyUp(IntPtr self, HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, IntPtr eHTMLKeyModifiers);

		// Token: 0x06000252 RID: 594 RVA: 0x00005C2D File Offset: 0x00003E2D
		internal void KeyUp(HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, IntPtr eHTMLKeyModifiers)
		{
			ISteamHTMLSurface._KeyUp(this.Self, unBrowserHandle, nNativeKeyCode, eHTMLKeyModifiers);
		}

		// Token: 0x06000253 RID: 595
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_KeyChar")]
		private static extern void _KeyChar(IntPtr self, HHTMLBrowser unBrowserHandle, uint cUnicodeChar, IntPtr eHTMLKeyModifiers);

		// Token: 0x06000254 RID: 596 RVA: 0x00005C3F File Offset: 0x00003E3F
		internal void KeyChar(HHTMLBrowser unBrowserHandle, uint cUnicodeChar, IntPtr eHTMLKeyModifiers)
		{
			ISteamHTMLSurface._KeyChar(this.Self, unBrowserHandle, cUnicodeChar, eHTMLKeyModifiers);
		}

		// Token: 0x06000255 RID: 597
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetHorizontalScroll")]
		private static extern void _SetHorizontalScroll(IntPtr self, HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll);

		// Token: 0x06000256 RID: 598 RVA: 0x00005C51 File Offset: 0x00003E51
		internal void SetHorizontalScroll(HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll)
		{
			ISteamHTMLSurface._SetHorizontalScroll(this.Self, unBrowserHandle, nAbsolutePixelScroll);
		}

		// Token: 0x06000257 RID: 599
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetVerticalScroll")]
		private static extern void _SetVerticalScroll(IntPtr self, HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll);

		// Token: 0x06000258 RID: 600 RVA: 0x00005C62 File Offset: 0x00003E62
		internal void SetVerticalScroll(HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll)
		{
			ISteamHTMLSurface._SetVerticalScroll(this.Self, unBrowserHandle, nAbsolutePixelScroll);
		}

		// Token: 0x06000259 RID: 601
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetKeyFocus")]
		private static extern void _SetKeyFocus(IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool bHasKeyFocus);

		// Token: 0x0600025A RID: 602 RVA: 0x00005C73 File Offset: 0x00003E73
		internal void SetKeyFocus(HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool bHasKeyFocus)
		{
			ISteamHTMLSurface._SetKeyFocus(this.Self, unBrowserHandle, bHasKeyFocus);
		}

		// Token: 0x0600025B RID: 603
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_ViewSource")]
		private static extern void _ViewSource(IntPtr self, HHTMLBrowser unBrowserHandle);

		// Token: 0x0600025C RID: 604 RVA: 0x00005C84 File Offset: 0x00003E84
		internal void ViewSource(HHTMLBrowser unBrowserHandle)
		{
			ISteamHTMLSurface._ViewSource(this.Self, unBrowserHandle);
		}

		// Token: 0x0600025D RID: 605
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_CopyToClipboard")]
		private static extern void _CopyToClipboard(IntPtr self, HHTMLBrowser unBrowserHandle);

		// Token: 0x0600025E RID: 606 RVA: 0x00005C94 File Offset: 0x00003E94
		internal void CopyToClipboard(HHTMLBrowser unBrowserHandle)
		{
			ISteamHTMLSurface._CopyToClipboard(this.Self, unBrowserHandle);
		}

		// Token: 0x0600025F RID: 607
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_PasteFromClipboard")]
		private static extern void _PasteFromClipboard(IntPtr self, HHTMLBrowser unBrowserHandle);

		// Token: 0x06000260 RID: 608 RVA: 0x00005CA4 File Offset: 0x00003EA4
		internal void PasteFromClipboard(HHTMLBrowser unBrowserHandle)
		{
			ISteamHTMLSurface._PasteFromClipboard(this.Self, unBrowserHandle);
		}

		// Token: 0x06000261 RID: 609
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_Find")]
		private static extern void _Find(IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchSearchStr, [MarshalAs(UnmanagedType.U1)] bool bCurrentlyInFind, [MarshalAs(UnmanagedType.U1)] bool bReverse);

		// Token: 0x06000262 RID: 610 RVA: 0x00005CB4 File Offset: 0x00003EB4
		internal void Find(HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchSearchStr, [MarshalAs(UnmanagedType.U1)] bool bCurrentlyInFind, [MarshalAs(UnmanagedType.U1)] bool bReverse)
		{
			ISteamHTMLSurface._Find(this.Self, unBrowserHandle, pchSearchStr, bCurrentlyInFind, bReverse);
		}

		// Token: 0x06000263 RID: 611
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_StopFind")]
		private static extern void _StopFind(IntPtr self, HHTMLBrowser unBrowserHandle);

		// Token: 0x06000264 RID: 612 RVA: 0x00005CC8 File Offset: 0x00003EC8
		internal void StopFind(HHTMLBrowser unBrowserHandle)
		{
			ISteamHTMLSurface._StopFind(this.Self, unBrowserHandle);
		}

		// Token: 0x06000265 RID: 613
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_GetLinkAtPosition")]
		private static extern void _GetLinkAtPosition(IntPtr self, HHTMLBrowser unBrowserHandle, int x, int y);

		// Token: 0x06000266 RID: 614 RVA: 0x00005CD8 File Offset: 0x00003ED8
		internal void GetLinkAtPosition(HHTMLBrowser unBrowserHandle, int x, int y)
		{
			ISteamHTMLSurface._GetLinkAtPosition(this.Self, unBrowserHandle, x, y);
		}

		// Token: 0x06000267 RID: 615
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetCookie")]
		private static extern void _SetCookie(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchHostname, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValue, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPath, RTime32 nExpires, [MarshalAs(UnmanagedType.U1)] bool bSecure, [MarshalAs(UnmanagedType.U1)] bool bHTTPOnly);

		// Token: 0x06000268 RID: 616 RVA: 0x00005CEA File Offset: 0x00003EEA
		internal void SetCookie([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchHostname, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValue, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPath, RTime32 nExpires, [MarshalAs(UnmanagedType.U1)] bool bSecure, [MarshalAs(UnmanagedType.U1)] bool bHTTPOnly)
		{
			ISteamHTMLSurface._SetCookie(this.Self, pchHostname, pchKey, pchValue, pchPath, nExpires, bSecure, bHTTPOnly);
		}

		// Token: 0x06000269 RID: 617
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetPageScaleFactor")]
		private static extern void _SetPageScaleFactor(IntPtr self, HHTMLBrowser unBrowserHandle, float flZoom, int nPointX, int nPointY);

		// Token: 0x0600026A RID: 618 RVA: 0x00005D04 File Offset: 0x00003F04
		internal void SetPageScaleFactor(HHTMLBrowser unBrowserHandle, float flZoom, int nPointX, int nPointY)
		{
			ISteamHTMLSurface._SetPageScaleFactor(this.Self, unBrowserHandle, flZoom, nPointX, nPointY);
		}

		// Token: 0x0600026B RID: 619
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetBackgroundMode")]
		private static extern void _SetBackgroundMode(IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool bBackgroundMode);

		// Token: 0x0600026C RID: 620 RVA: 0x00005D18 File Offset: 0x00003F18
		internal void SetBackgroundMode(HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool bBackgroundMode)
		{
			ISteamHTMLSurface._SetBackgroundMode(this.Self, unBrowserHandle, bBackgroundMode);
		}

		// Token: 0x0600026D RID: 621
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetDPIScalingFactor")]
		private static extern void _SetDPIScalingFactor(IntPtr self, HHTMLBrowser unBrowserHandle, float flDPIScaling);

		// Token: 0x0600026E RID: 622 RVA: 0x00005D29 File Offset: 0x00003F29
		internal void SetDPIScalingFactor(HHTMLBrowser unBrowserHandle, float flDPIScaling)
		{
			ISteamHTMLSurface._SetDPIScalingFactor(this.Self, unBrowserHandle, flDPIScaling);
		}

		// Token: 0x0600026F RID: 623
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_OpenDeveloperTools")]
		private static extern void _OpenDeveloperTools(IntPtr self, HHTMLBrowser unBrowserHandle);

		// Token: 0x06000270 RID: 624 RVA: 0x00005D3A File Offset: 0x00003F3A
		internal void OpenDeveloperTools(HHTMLBrowser unBrowserHandle)
		{
			ISteamHTMLSurface._OpenDeveloperTools(this.Self, unBrowserHandle);
		}

		// Token: 0x06000271 RID: 625
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_AllowStartRequest")]
		private static extern void _AllowStartRequest(IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool bAllowed);

		// Token: 0x06000272 RID: 626 RVA: 0x00005D4A File Offset: 0x00003F4A
		internal void AllowStartRequest(HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool bAllowed)
		{
			ISteamHTMLSurface._AllowStartRequest(this.Self, unBrowserHandle, bAllowed);
		}

		// Token: 0x06000273 RID: 627
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_JSDialogResponse")]
		private static extern void _JSDialogResponse(IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool bResult);

		// Token: 0x06000274 RID: 628 RVA: 0x00005D5B File Offset: 0x00003F5B
		internal void JSDialogResponse(HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool bResult)
		{
			ISteamHTMLSurface._JSDialogResponse(this.Self, unBrowserHandle, bResult);
		}

		// Token: 0x06000275 RID: 629
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_FileLoadDialogResponse")]
		private static extern void _FileLoadDialogResponse(IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchSelectedFiles);

		// Token: 0x06000276 RID: 630 RVA: 0x00005D6C File Offset: 0x00003F6C
		internal void FileLoadDialogResponse(HHTMLBrowser unBrowserHandle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchSelectedFiles)
		{
			ISteamHTMLSurface._FileLoadDialogResponse(this.Self, unBrowserHandle, pchSelectedFiles);
		}
	}
}
