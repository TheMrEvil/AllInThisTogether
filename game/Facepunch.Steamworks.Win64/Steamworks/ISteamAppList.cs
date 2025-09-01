using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200000D RID: 13
	internal class ISteamAppList : SteamInterface
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00003BC4 File Offset: 0x00001DC4
		internal ISteamAppList(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x0600002B RID: 43
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamAppList_v001();

		// Token: 0x0600002C RID: 44 RVA: 0x00003BD6 File Offset: 0x00001DD6
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamAppList.SteamAPI_SteamAppList_v001();
		}

		// Token: 0x0600002D RID: 45
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamAppList_GetNumInstalledApps")]
		private static extern uint _GetNumInstalledApps(IntPtr self);

		// Token: 0x0600002E RID: 46 RVA: 0x00003BE0 File Offset: 0x00001DE0
		internal uint GetNumInstalledApps()
		{
			return ISteamAppList._GetNumInstalledApps(this.Self);
		}

		// Token: 0x0600002F RID: 47
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamAppList_GetInstalledApps")]
		private static extern uint _GetInstalledApps(IntPtr self, [In] [Out] AppId[] pvecAppID, uint unMaxAppIDs);

		// Token: 0x06000030 RID: 48 RVA: 0x00003C00 File Offset: 0x00001E00
		internal uint GetInstalledApps([In] [Out] AppId[] pvecAppID, uint unMaxAppIDs)
		{
			return ISteamAppList._GetInstalledApps(this.Self, pvecAppID, unMaxAppIDs);
		}

		// Token: 0x06000031 RID: 49
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamAppList_GetAppName")]
		private static extern int _GetAppName(IntPtr self, AppId nAppID, IntPtr pchName, int cchNameMax);

		// Token: 0x06000032 RID: 50 RVA: 0x00003C24 File Offset: 0x00001E24
		internal int GetAppName(AppId nAppID, out string pchName)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			int result = ISteamAppList._GetAppName(this.Self, nAppID, intPtr, 32768);
			pchName = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x06000033 RID: 51
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamAppList_GetAppInstallDir")]
		private static extern int _GetAppInstallDir(IntPtr self, AppId nAppID, IntPtr pchDirectory, int cchNameMax);

		// Token: 0x06000034 RID: 52 RVA: 0x00003C58 File Offset: 0x00001E58
		internal int GetAppInstallDir(AppId nAppID, out string pchDirectory)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			int result = ISteamAppList._GetAppInstallDir(this.Self, nAppID, intPtr, 32768);
			pchDirectory = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x06000035 RID: 53
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamAppList_GetAppBuildId")]
		private static extern int _GetAppBuildId(IntPtr self, AppId nAppID);

		// Token: 0x06000036 RID: 54 RVA: 0x00003C8C File Offset: 0x00001E8C
		internal int GetAppBuildId(AppId nAppID)
		{
			return ISteamAppList._GetAppBuildId(this.Self, nAppID);
		}
	}
}
