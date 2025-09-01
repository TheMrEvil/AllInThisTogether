using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000025 RID: 37
	internal class ISteamNetworkingUtils : SteamInterface
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x00007F0E File Offset: 0x0000610E
		internal ISteamNetworkingUtils(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x060004A1 RID: 1185
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamNetworkingUtils_v003();

		// Token: 0x060004A2 RID: 1186 RVA: 0x00007F20 File Offset: 0x00006120
		public override IntPtr GetGlobalInterfacePointer()
		{
			return ISteamNetworkingUtils.SteamAPI_SteamNetworkingUtils_v003();
		}

		// Token: 0x060004A3 RID: 1187
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_AllocateMessage")]
		private static extern IntPtr _AllocateMessage(IntPtr self, int cbAllocateBuffer);

		// Token: 0x060004A4 RID: 1188 RVA: 0x00007F28 File Offset: 0x00006128
		internal NetMsg AllocateMessage(int cbAllocateBuffer)
		{
			IntPtr ptr = ISteamNetworkingUtils._AllocateMessage(this.Self, cbAllocateBuffer);
			return ptr.ToType<NetMsg>();
		}

		// Token: 0x060004A5 RID: 1189
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_InitRelayNetworkAccess")]
		private static extern void _InitRelayNetworkAccess(IntPtr self);

		// Token: 0x060004A6 RID: 1190 RVA: 0x00007F4D File Offset: 0x0000614D
		internal void InitRelayNetworkAccess()
		{
			ISteamNetworkingUtils._InitRelayNetworkAccess(this.Self);
		}

		// Token: 0x060004A7 RID: 1191
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetRelayNetworkStatus")]
		private static extern SteamNetworkingAvailability _GetRelayNetworkStatus(IntPtr self, ref SteamRelayNetworkStatus_t pDetails);

		// Token: 0x060004A8 RID: 1192 RVA: 0x00007F5C File Offset: 0x0000615C
		internal SteamNetworkingAvailability GetRelayNetworkStatus(ref SteamRelayNetworkStatus_t pDetails)
		{
			return ISteamNetworkingUtils._GetRelayNetworkStatus(this.Self, ref pDetails);
		}

		// Token: 0x060004A9 RID: 1193
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetLocalPingLocation")]
		private static extern float _GetLocalPingLocation(IntPtr self, ref NetPingLocation result);

		// Token: 0x060004AA RID: 1194 RVA: 0x00007F7C File Offset: 0x0000617C
		internal float GetLocalPingLocation(ref NetPingLocation result)
		{
			return ISteamNetworkingUtils._GetLocalPingLocation(this.Self, ref result);
		}

		// Token: 0x060004AB RID: 1195
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_EstimatePingTimeBetweenTwoLocations")]
		private static extern int _EstimatePingTimeBetweenTwoLocations(IntPtr self, ref NetPingLocation location1, ref NetPingLocation location2);

		// Token: 0x060004AC RID: 1196 RVA: 0x00007F9C File Offset: 0x0000619C
		internal int EstimatePingTimeBetweenTwoLocations(ref NetPingLocation location1, ref NetPingLocation location2)
		{
			return ISteamNetworkingUtils._EstimatePingTimeBetweenTwoLocations(this.Self, ref location1, ref location2);
		}

		// Token: 0x060004AD RID: 1197
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_EstimatePingTimeFromLocalHost")]
		private static extern int _EstimatePingTimeFromLocalHost(IntPtr self, ref NetPingLocation remoteLocation);

		// Token: 0x060004AE RID: 1198 RVA: 0x00007FC0 File Offset: 0x000061C0
		internal int EstimatePingTimeFromLocalHost(ref NetPingLocation remoteLocation)
		{
			return ISteamNetworkingUtils._EstimatePingTimeFromLocalHost(this.Self, ref remoteLocation);
		}

		// Token: 0x060004AF RID: 1199
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_ConvertPingLocationToString")]
		private static extern void _ConvertPingLocationToString(IntPtr self, ref NetPingLocation location, IntPtr pszBuf, int cchBufSize);

		// Token: 0x060004B0 RID: 1200 RVA: 0x00007FE0 File Offset: 0x000061E0
		internal void ConvertPingLocationToString(ref NetPingLocation location, out string pszBuf)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			ISteamNetworkingUtils._ConvertPingLocationToString(this.Self, ref location, intPtr, 32768);
			pszBuf = Helpers.MemoryToString(intPtr);
		}

		// Token: 0x060004B1 RID: 1201
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_ParsePingLocationString")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _ParsePingLocationString(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszString, ref NetPingLocation result);

		// Token: 0x060004B2 RID: 1202 RVA: 0x00008010 File Offset: 0x00006210
		internal bool ParsePingLocationString([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszString, ref NetPingLocation result)
		{
			return ISteamNetworkingUtils._ParsePingLocationString(this.Self, pszString, ref result);
		}

		// Token: 0x060004B3 RID: 1203
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_CheckPingDataUpToDate")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _CheckPingDataUpToDate(IntPtr self, float flMaxAgeSeconds);

		// Token: 0x060004B4 RID: 1204 RVA: 0x00008034 File Offset: 0x00006234
		internal bool CheckPingDataUpToDate(float flMaxAgeSeconds)
		{
			return ISteamNetworkingUtils._CheckPingDataUpToDate(this.Self, flMaxAgeSeconds);
		}

		// Token: 0x060004B5 RID: 1205
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetPingToDataCenter")]
		private static extern int _GetPingToDataCenter(IntPtr self, SteamNetworkingPOPID popID, ref SteamNetworkingPOPID pViaRelayPoP);

		// Token: 0x060004B6 RID: 1206 RVA: 0x00008054 File Offset: 0x00006254
		internal int GetPingToDataCenter(SteamNetworkingPOPID popID, ref SteamNetworkingPOPID pViaRelayPoP)
		{
			return ISteamNetworkingUtils._GetPingToDataCenter(this.Self, popID, ref pViaRelayPoP);
		}

		// Token: 0x060004B7 RID: 1207
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetDirectPingToPOP")]
		private static extern int _GetDirectPingToPOP(IntPtr self, SteamNetworkingPOPID popID);

		// Token: 0x060004B8 RID: 1208 RVA: 0x00008078 File Offset: 0x00006278
		internal int GetDirectPingToPOP(SteamNetworkingPOPID popID)
		{
			return ISteamNetworkingUtils._GetDirectPingToPOP(this.Self, popID);
		}

		// Token: 0x060004B9 RID: 1209
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetPOPCount")]
		private static extern int _GetPOPCount(IntPtr self);

		// Token: 0x060004BA RID: 1210 RVA: 0x00008098 File Offset: 0x00006298
		internal int GetPOPCount()
		{
			return ISteamNetworkingUtils._GetPOPCount(this.Self);
		}

		// Token: 0x060004BB RID: 1211
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetPOPList")]
		private static extern int _GetPOPList(IntPtr self, ref SteamNetworkingPOPID list, int nListSz);

		// Token: 0x060004BC RID: 1212 RVA: 0x000080B8 File Offset: 0x000062B8
		internal int GetPOPList(ref SteamNetworkingPOPID list, int nListSz)
		{
			return ISteamNetworkingUtils._GetPOPList(this.Self, ref list, nListSz);
		}

		// Token: 0x060004BD RID: 1213
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetLocalTimestamp")]
		private static extern long _GetLocalTimestamp(IntPtr self);

		// Token: 0x060004BE RID: 1214 RVA: 0x000080DC File Offset: 0x000062DC
		internal long GetLocalTimestamp()
		{
			return ISteamNetworkingUtils._GetLocalTimestamp(this.Self);
		}

		// Token: 0x060004BF RID: 1215
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetDebugOutputFunction")]
		private static extern void _SetDebugOutputFunction(IntPtr self, NetDebugOutput eDetailLevel, NetDebugFunc pfnFunc);

		// Token: 0x060004C0 RID: 1216 RVA: 0x000080FB File Offset: 0x000062FB
		internal void SetDebugOutputFunction(NetDebugOutput eDetailLevel, NetDebugFunc pfnFunc)
		{
			ISteamNetworkingUtils._SetDebugOutputFunction(this.Self, eDetailLevel, pfnFunc);
		}

		// Token: 0x060004C1 RID: 1217
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetGlobalConfigValueInt32")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetGlobalConfigValueInt32(IntPtr self, NetConfig eValue, int val);

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000810C File Offset: 0x0000630C
		internal bool SetGlobalConfigValueInt32(NetConfig eValue, int val)
		{
			return ISteamNetworkingUtils._SetGlobalConfigValueInt32(this.Self, eValue, val);
		}

		// Token: 0x060004C3 RID: 1219
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetGlobalConfigValueFloat")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetGlobalConfigValueFloat(IntPtr self, NetConfig eValue, float val);

		// Token: 0x060004C4 RID: 1220 RVA: 0x00008130 File Offset: 0x00006330
		internal bool SetGlobalConfigValueFloat(NetConfig eValue, float val)
		{
			return ISteamNetworkingUtils._SetGlobalConfigValueFloat(this.Self, eValue, val);
		}

		// Token: 0x060004C5 RID: 1221
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetGlobalConfigValueString")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetGlobalConfigValueString(IntPtr self, NetConfig eValue, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string val);

		// Token: 0x060004C6 RID: 1222 RVA: 0x00008154 File Offset: 0x00006354
		internal bool SetGlobalConfigValueString(NetConfig eValue, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string val)
		{
			return ISteamNetworkingUtils._SetGlobalConfigValueString(this.Self, eValue, val);
		}

		// Token: 0x060004C7 RID: 1223
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetConnectionConfigValueInt32")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetConnectionConfigValueInt32(IntPtr self, Connection hConn, NetConfig eValue, int val);

		// Token: 0x060004C8 RID: 1224 RVA: 0x00008178 File Offset: 0x00006378
		internal bool SetConnectionConfigValueInt32(Connection hConn, NetConfig eValue, int val)
		{
			return ISteamNetworkingUtils._SetConnectionConfigValueInt32(this.Self, hConn, eValue, val);
		}

		// Token: 0x060004C9 RID: 1225
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetConnectionConfigValueFloat")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetConnectionConfigValueFloat(IntPtr self, Connection hConn, NetConfig eValue, float val);

		// Token: 0x060004CA RID: 1226 RVA: 0x0000819C File Offset: 0x0000639C
		internal bool SetConnectionConfigValueFloat(Connection hConn, NetConfig eValue, float val)
		{
			return ISteamNetworkingUtils._SetConnectionConfigValueFloat(this.Self, hConn, eValue, val);
		}

		// Token: 0x060004CB RID: 1227
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetConnectionConfigValueString")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetConnectionConfigValueString(IntPtr self, Connection hConn, NetConfig eValue, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string val);

		// Token: 0x060004CC RID: 1228 RVA: 0x000081C0 File Offset: 0x000063C0
		internal bool SetConnectionConfigValueString(Connection hConn, NetConfig eValue, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string val)
		{
			return ISteamNetworkingUtils._SetConnectionConfigValueString(this.Self, hConn, eValue, val);
		}

		// Token: 0x060004CD RID: 1229
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetConfigValue")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetConfigValue(IntPtr self, NetConfig eValue, NetConfigScope eScopeType, IntPtr scopeObj, NetConfigType eDataType, IntPtr pArg);

		// Token: 0x060004CE RID: 1230 RVA: 0x000081E4 File Offset: 0x000063E4
		internal bool SetConfigValue(NetConfig eValue, NetConfigScope eScopeType, IntPtr scopeObj, NetConfigType eDataType, IntPtr pArg)
		{
			return ISteamNetworkingUtils._SetConfigValue(this.Self, eValue, eScopeType, scopeObj, eDataType, pArg);
		}

		// Token: 0x060004CF RID: 1231
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetConfigValueStruct")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetConfigValueStruct(IntPtr self, ref NetKeyValue opt, NetConfigScope eScopeType, IntPtr scopeObj);

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000820C File Offset: 0x0000640C
		internal bool SetConfigValueStruct(ref NetKeyValue opt, NetConfigScope eScopeType, IntPtr scopeObj)
		{
			return ISteamNetworkingUtils._SetConfigValueStruct(this.Self, ref opt, eScopeType, scopeObj);
		}

		// Token: 0x060004D1 RID: 1233
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetConfigValue")]
		private static extern NetConfigResult _GetConfigValue(IntPtr self, NetConfig eValue, NetConfigScope eScopeType, IntPtr scopeObj, ref NetConfigType pOutDataType, IntPtr pResult, ref UIntPtr cbResult);

		// Token: 0x060004D2 RID: 1234 RVA: 0x00008230 File Offset: 0x00006430
		internal NetConfigResult GetConfigValue(NetConfig eValue, NetConfigScope eScopeType, IntPtr scopeObj, ref NetConfigType pOutDataType, IntPtr pResult, ref UIntPtr cbResult)
		{
			return ISteamNetworkingUtils._GetConfigValue(this.Self, eValue, eScopeType, scopeObj, ref pOutDataType, pResult, ref cbResult);
		}

		// Token: 0x060004D3 RID: 1235
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetConfigValueInfo")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetConfigValueInfo(IntPtr self, NetConfig eValue, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pOutName, ref NetConfigType pOutDataType, [In] [Out] NetConfigScope[] pOutScope, [In] [Out] NetConfig[] pOutNextValue);

		// Token: 0x060004D4 RID: 1236 RVA: 0x00008258 File Offset: 0x00006458
		internal bool GetConfigValueInfo(NetConfig eValue, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pOutName, ref NetConfigType pOutDataType, [In] [Out] NetConfigScope[] pOutScope, [In] [Out] NetConfig[] pOutNextValue)
		{
			return ISteamNetworkingUtils._GetConfigValueInfo(this.Self, eValue, pOutName, ref pOutDataType, pOutScope, pOutNextValue);
		}

		// Token: 0x060004D5 RID: 1237
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetFirstConfigValue")]
		private static extern NetConfig _GetFirstConfigValue(IntPtr self);

		// Token: 0x060004D6 RID: 1238 RVA: 0x00008280 File Offset: 0x00006480
		internal NetConfig GetFirstConfigValue()
		{
			return ISteamNetworkingUtils._GetFirstConfigValue(this.Self);
		}

		// Token: 0x060004D7 RID: 1239
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SteamNetworkingIPAddr_ToString")]
		private static extern void _SteamNetworkingIPAddr_ToString(IntPtr self, ref NetAddress addr, IntPtr buf, uint cbBuf, [MarshalAs(UnmanagedType.U1)] bool bWithPort);

		// Token: 0x060004D8 RID: 1240 RVA: 0x000082A0 File Offset: 0x000064A0
		internal void SteamNetworkingIPAddr_ToString(ref NetAddress addr, out string buf, [MarshalAs(UnmanagedType.U1)] bool bWithPort)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			ISteamNetworkingUtils._SteamNetworkingIPAddr_ToString(this.Self, ref addr, intPtr, 32768U, bWithPort);
			buf = Helpers.MemoryToString(intPtr);
		}

		// Token: 0x060004D9 RID: 1241
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SteamNetworkingIPAddr_ParseString")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SteamNetworkingIPAddr_ParseString(IntPtr self, ref NetAddress pAddr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszStr);

		// Token: 0x060004DA RID: 1242 RVA: 0x000082D0 File Offset: 0x000064D0
		internal bool SteamNetworkingIPAddr_ParseString(ref NetAddress pAddr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszStr)
		{
			return ISteamNetworkingUtils._SteamNetworkingIPAddr_ParseString(this.Self, ref pAddr, pszStr);
		}

		// Token: 0x060004DB RID: 1243
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SteamNetworkingIdentity_ToString")]
		private static extern void _SteamNetworkingIdentity_ToString(IntPtr self, ref NetIdentity identity, IntPtr buf, uint cbBuf);

		// Token: 0x060004DC RID: 1244 RVA: 0x000082F4 File Offset: 0x000064F4
		internal void SteamNetworkingIdentity_ToString(ref NetIdentity identity, out string buf)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			ISteamNetworkingUtils._SteamNetworkingIdentity_ToString(this.Self, ref identity, intPtr, 32768U);
			buf = Helpers.MemoryToString(intPtr);
		}

		// Token: 0x060004DD RID: 1245
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SteamNetworkingIdentity_ParseString")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SteamNetworkingIdentity_ParseString(IntPtr self, ref NetIdentity pIdentity, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszStr);

		// Token: 0x060004DE RID: 1246 RVA: 0x00008324 File Offset: 0x00006524
		internal bool SteamNetworkingIdentity_ParseString(ref NetIdentity pIdentity, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszStr)
		{
			return ISteamNetworkingUtils._SteamNetworkingIdentity_ParseString(this.Self, ref pIdentity, pszStr);
		}
	}
}
