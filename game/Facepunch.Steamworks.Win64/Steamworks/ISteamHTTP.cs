using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000016 RID: 22
	internal class ISteamHTTP : SteamInterface
	{
		// Token: 0x06000277 RID: 631 RVA: 0x00005D7D File Offset: 0x00003F7D
		internal ISteamHTTP(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x06000278 RID: 632
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamHTTP_v003();

		// Token: 0x06000279 RID: 633 RVA: 0x00005D8F File Offset: 0x00003F8F
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamHTTP.SteamAPI_SteamHTTP_v003();
		}

		// Token: 0x0600027A RID: 634
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamGameServerHTTP_v003();

		// Token: 0x0600027B RID: 635 RVA: 0x00005D96 File Offset: 0x00003F96
		public override IntPtr GetServerInterfacePointer()
		{
			return ISteamHTTP.SteamAPI_SteamGameServerHTTP_v003();
		}

		// Token: 0x0600027C RID: 636
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_CreateHTTPRequest")]
		private static extern HTTPRequestHandle _CreateHTTPRequest(IntPtr self, HTTPMethod eHTTPRequestMethod, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchAbsoluteURL);

		// Token: 0x0600027D RID: 637 RVA: 0x00005DA0 File Offset: 0x00003FA0
		internal HTTPRequestHandle CreateHTTPRequest(HTTPMethod eHTTPRequestMethod, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchAbsoluteURL)
		{
			return ISteamHTTP._CreateHTTPRequest(this.Self, eHTTPRequestMethod, pchAbsoluteURL);
		}

		// Token: 0x0600027E RID: 638
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestContextValue")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetHTTPRequestContextValue(IntPtr self, HTTPRequestHandle hRequest, ulong ulContextValue);

		// Token: 0x0600027F RID: 639 RVA: 0x00005DC4 File Offset: 0x00003FC4
		internal bool SetHTTPRequestContextValue(HTTPRequestHandle hRequest, ulong ulContextValue)
		{
			return ISteamHTTP._SetHTTPRequestContextValue(this.Self, hRequest, ulContextValue);
		}

		// Token: 0x06000280 RID: 640
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestNetworkActivityTimeout")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetHTTPRequestNetworkActivityTimeout(IntPtr self, HTTPRequestHandle hRequest, uint unTimeoutSeconds);

		// Token: 0x06000281 RID: 641 RVA: 0x00005DE8 File Offset: 0x00003FE8
		internal bool SetHTTPRequestNetworkActivityTimeout(HTTPRequestHandle hRequest, uint unTimeoutSeconds)
		{
			return ISteamHTTP._SetHTTPRequestNetworkActivityTimeout(this.Self, hRequest, unTimeoutSeconds);
		}

		// Token: 0x06000282 RID: 642
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestHeaderValue")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetHTTPRequestHeaderValue(IntPtr self, HTTPRequestHandle hRequest, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchHeaderName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchHeaderValue);

		// Token: 0x06000283 RID: 643 RVA: 0x00005E0C File Offset: 0x0000400C
		internal bool SetHTTPRequestHeaderValue(HTTPRequestHandle hRequest, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchHeaderName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchHeaderValue)
		{
			return ISteamHTTP._SetHTTPRequestHeaderValue(this.Self, hRequest, pchHeaderName, pchHeaderValue);
		}

		// Token: 0x06000284 RID: 644
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestGetOrPostParameter")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetHTTPRequestGetOrPostParameter(IntPtr self, HTTPRequestHandle hRequest, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchParamName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchParamValue);

		// Token: 0x06000285 RID: 645 RVA: 0x00005E30 File Offset: 0x00004030
		internal bool SetHTTPRequestGetOrPostParameter(HTTPRequestHandle hRequest, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchParamName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchParamValue)
		{
			return ISteamHTTP._SetHTTPRequestGetOrPostParameter(this.Self, hRequest, pchParamName, pchParamValue);
		}

		// Token: 0x06000286 RID: 646
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SendHTTPRequest")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SendHTTPRequest(IntPtr self, HTTPRequestHandle hRequest, ref SteamAPICall_t pCallHandle);

		// Token: 0x06000287 RID: 647 RVA: 0x00005E54 File Offset: 0x00004054
		internal bool SendHTTPRequest(HTTPRequestHandle hRequest, ref SteamAPICall_t pCallHandle)
		{
			return ISteamHTTP._SendHTTPRequest(this.Self, hRequest, ref pCallHandle);
		}

		// Token: 0x06000288 RID: 648
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SendHTTPRequestAndStreamResponse")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SendHTTPRequestAndStreamResponse(IntPtr self, HTTPRequestHandle hRequest, ref SteamAPICall_t pCallHandle);

		// Token: 0x06000289 RID: 649 RVA: 0x00005E78 File Offset: 0x00004078
		internal bool SendHTTPRequestAndStreamResponse(HTTPRequestHandle hRequest, ref SteamAPICall_t pCallHandle)
		{
			return ISteamHTTP._SendHTTPRequestAndStreamResponse(this.Self, hRequest, ref pCallHandle);
		}

		// Token: 0x0600028A RID: 650
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_DeferHTTPRequest")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _DeferHTTPRequest(IntPtr self, HTTPRequestHandle hRequest);

		// Token: 0x0600028B RID: 651 RVA: 0x00005E9C File Offset: 0x0000409C
		internal bool DeferHTTPRequest(HTTPRequestHandle hRequest)
		{
			return ISteamHTTP._DeferHTTPRequest(this.Self, hRequest);
		}

		// Token: 0x0600028C RID: 652
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_PrioritizeHTTPRequest")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _PrioritizeHTTPRequest(IntPtr self, HTTPRequestHandle hRequest);

		// Token: 0x0600028D RID: 653 RVA: 0x00005EBC File Offset: 0x000040BC
		internal bool PrioritizeHTTPRequest(HTTPRequestHandle hRequest)
		{
			return ISteamHTTP._PrioritizeHTTPRequest(this.Self, hRequest);
		}

		// Token: 0x0600028E RID: 654
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseHeaderSize")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetHTTPResponseHeaderSize(IntPtr self, HTTPRequestHandle hRequest, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchHeaderName, ref uint unResponseHeaderSize);

		// Token: 0x0600028F RID: 655 RVA: 0x00005EDC File Offset: 0x000040DC
		internal bool GetHTTPResponseHeaderSize(HTTPRequestHandle hRequest, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchHeaderName, ref uint unResponseHeaderSize)
		{
			return ISteamHTTP._GetHTTPResponseHeaderSize(this.Self, hRequest, pchHeaderName, ref unResponseHeaderSize);
		}

		// Token: 0x06000290 RID: 656
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseHeaderValue")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetHTTPResponseHeaderValue(IntPtr self, HTTPRequestHandle hRequest, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchHeaderName, ref byte pHeaderValueBuffer, uint unBufferSize);

		// Token: 0x06000291 RID: 657 RVA: 0x00005F00 File Offset: 0x00004100
		internal bool GetHTTPResponseHeaderValue(HTTPRequestHandle hRequest, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchHeaderName, ref byte pHeaderValueBuffer, uint unBufferSize)
		{
			return ISteamHTTP._GetHTTPResponseHeaderValue(this.Self, hRequest, pchHeaderName, ref pHeaderValueBuffer, unBufferSize);
		}

		// Token: 0x06000292 RID: 658
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseBodySize")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetHTTPResponseBodySize(IntPtr self, HTTPRequestHandle hRequest, ref uint unBodySize);

		// Token: 0x06000293 RID: 659 RVA: 0x00005F24 File Offset: 0x00004124
		internal bool GetHTTPResponseBodySize(HTTPRequestHandle hRequest, ref uint unBodySize)
		{
			return ISteamHTTP._GetHTTPResponseBodySize(this.Self, hRequest, ref unBodySize);
		}

		// Token: 0x06000294 RID: 660
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseBodyData")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetHTTPResponseBodyData(IntPtr self, HTTPRequestHandle hRequest, ref byte pBodyDataBuffer, uint unBufferSize);

		// Token: 0x06000295 RID: 661 RVA: 0x00005F48 File Offset: 0x00004148
		internal bool GetHTTPResponseBodyData(HTTPRequestHandle hRequest, ref byte pBodyDataBuffer, uint unBufferSize)
		{
			return ISteamHTTP._GetHTTPResponseBodyData(this.Self, hRequest, ref pBodyDataBuffer, unBufferSize);
		}

		// Token: 0x06000296 RID: 662
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPStreamingResponseBodyData")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetHTTPStreamingResponseBodyData(IntPtr self, HTTPRequestHandle hRequest, uint cOffset, ref byte pBodyDataBuffer, uint unBufferSize);

		// Token: 0x06000297 RID: 663 RVA: 0x00005F6C File Offset: 0x0000416C
		internal bool GetHTTPStreamingResponseBodyData(HTTPRequestHandle hRequest, uint cOffset, ref byte pBodyDataBuffer, uint unBufferSize)
		{
			return ISteamHTTP._GetHTTPStreamingResponseBodyData(this.Self, hRequest, cOffset, ref pBodyDataBuffer, unBufferSize);
		}

		// Token: 0x06000298 RID: 664
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_ReleaseHTTPRequest")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _ReleaseHTTPRequest(IntPtr self, HTTPRequestHandle hRequest);

		// Token: 0x06000299 RID: 665 RVA: 0x00005F90 File Offset: 0x00004190
		internal bool ReleaseHTTPRequest(HTTPRequestHandle hRequest)
		{
			return ISteamHTTP._ReleaseHTTPRequest(this.Self, hRequest);
		}

		// Token: 0x0600029A RID: 666
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPDownloadProgressPct")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetHTTPDownloadProgressPct(IntPtr self, HTTPRequestHandle hRequest, ref float pflPercentOut);

		// Token: 0x0600029B RID: 667 RVA: 0x00005FB0 File Offset: 0x000041B0
		internal bool GetHTTPDownloadProgressPct(HTTPRequestHandle hRequest, ref float pflPercentOut)
		{
			return ISteamHTTP._GetHTTPDownloadProgressPct(this.Self, hRequest, ref pflPercentOut);
		}

		// Token: 0x0600029C RID: 668
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestRawPostBody")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetHTTPRequestRawPostBody(IntPtr self, HTTPRequestHandle hRequest, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchContentType, [In] [Out] byte[] pubBody, uint unBodyLen);

		// Token: 0x0600029D RID: 669 RVA: 0x00005FD4 File Offset: 0x000041D4
		internal bool SetHTTPRequestRawPostBody(HTTPRequestHandle hRequest, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchContentType, [In] [Out] byte[] pubBody, uint unBodyLen)
		{
			return ISteamHTTP._SetHTTPRequestRawPostBody(this.Self, hRequest, pchContentType, pubBody, unBodyLen);
		}

		// Token: 0x0600029E RID: 670
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_CreateCookieContainer")]
		private static extern HTTPCookieContainerHandle _CreateCookieContainer(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bAllowResponsesToModify);

		// Token: 0x0600029F RID: 671 RVA: 0x00005FF8 File Offset: 0x000041F8
		internal HTTPCookieContainerHandle CreateCookieContainer([MarshalAs(UnmanagedType.U1)] bool bAllowResponsesToModify)
		{
			return ISteamHTTP._CreateCookieContainer(this.Self, bAllowResponsesToModify);
		}

		// Token: 0x060002A0 RID: 672
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_ReleaseCookieContainer")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _ReleaseCookieContainer(IntPtr self, HTTPCookieContainerHandle hCookieContainer);

		// Token: 0x060002A1 RID: 673 RVA: 0x00006018 File Offset: 0x00004218
		internal bool ReleaseCookieContainer(HTTPCookieContainerHandle hCookieContainer)
		{
			return ISteamHTTP._ReleaseCookieContainer(this.Self, hCookieContainer);
		}

		// Token: 0x060002A2 RID: 674
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetCookie")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetCookie(IntPtr self, HTTPCookieContainerHandle hCookieContainer, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchHost, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchUrl, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchCookie);

		// Token: 0x060002A3 RID: 675 RVA: 0x00006038 File Offset: 0x00004238
		internal bool SetCookie(HTTPCookieContainerHandle hCookieContainer, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchHost, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchUrl, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchCookie)
		{
			return ISteamHTTP._SetCookie(this.Self, hCookieContainer, pchHost, pchUrl, pchCookie);
		}

		// Token: 0x060002A4 RID: 676
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestCookieContainer")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetHTTPRequestCookieContainer(IntPtr self, HTTPRequestHandle hRequest, HTTPCookieContainerHandle hCookieContainer);

		// Token: 0x060002A5 RID: 677 RVA: 0x0000605C File Offset: 0x0000425C
		internal bool SetHTTPRequestCookieContainer(HTTPRequestHandle hRequest, HTTPCookieContainerHandle hCookieContainer)
		{
			return ISteamHTTP._SetHTTPRequestCookieContainer(this.Self, hRequest, hCookieContainer);
		}

		// Token: 0x060002A6 RID: 678
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestUserAgentInfo")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetHTTPRequestUserAgentInfo(IntPtr self, HTTPRequestHandle hRequest, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchUserAgentInfo);

		// Token: 0x060002A7 RID: 679 RVA: 0x00006080 File Offset: 0x00004280
		internal bool SetHTTPRequestUserAgentInfo(HTTPRequestHandle hRequest, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchUserAgentInfo)
		{
			return ISteamHTTP._SetHTTPRequestUserAgentInfo(this.Self, hRequest, pchUserAgentInfo);
		}

		// Token: 0x060002A8 RID: 680
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetHTTPRequestRequiresVerifiedCertificate(IntPtr self, HTTPRequestHandle hRequest, [MarshalAs(UnmanagedType.U1)] bool bRequireVerifiedCertificate);

		// Token: 0x060002A9 RID: 681 RVA: 0x000060A4 File Offset: 0x000042A4
		internal bool SetHTTPRequestRequiresVerifiedCertificate(HTTPRequestHandle hRequest, [MarshalAs(UnmanagedType.U1)] bool bRequireVerifiedCertificate)
		{
			return ISteamHTTP._SetHTTPRequestRequiresVerifiedCertificate(this.Self, hRequest, bRequireVerifiedCertificate);
		}

		// Token: 0x060002AA RID: 682
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetHTTPRequestAbsoluteTimeoutMS(IntPtr self, HTTPRequestHandle hRequest, uint unMilliseconds);

		// Token: 0x060002AB RID: 683 RVA: 0x000060C8 File Offset: 0x000042C8
		internal bool SetHTTPRequestAbsoluteTimeoutMS(HTTPRequestHandle hRequest, uint unMilliseconds)
		{
			return ISteamHTTP._SetHTTPRequestAbsoluteTimeoutMS(this.Self, hRequest, unMilliseconds);
		}

		// Token: 0x060002AC RID: 684
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPRequestWasTimedOut")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetHTTPRequestWasTimedOut(IntPtr self, HTTPRequestHandle hRequest, [MarshalAs(UnmanagedType.U1)] ref bool pbWasTimedOut);

		// Token: 0x060002AD RID: 685 RVA: 0x000060EC File Offset: 0x000042EC
		internal bool GetHTTPRequestWasTimedOut(HTTPRequestHandle hRequest, [MarshalAs(UnmanagedType.U1)] ref bool pbWasTimedOut)
		{
			return ISteamHTTP._GetHTTPRequestWasTimedOut(this.Self, hRequest, ref pbWasTimedOut);
		}
	}
}
