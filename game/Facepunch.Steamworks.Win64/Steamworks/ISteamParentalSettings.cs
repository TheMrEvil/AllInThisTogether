using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000026 RID: 38
	internal class ISteamParentalSettings : SteamInterface
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x00008345 File Offset: 0x00006545
		internal ISteamParentalSettings(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x060004E0 RID: 1248
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamParentalSettings_v001();

		// Token: 0x060004E1 RID: 1249 RVA: 0x00008357 File Offset: 0x00006557
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamParentalSettings.SteamAPI_SteamParentalSettings_v001();
		}

		// Token: 0x060004E2 RID: 1250
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParentalSettings_BIsParentalLockEnabled")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsParentalLockEnabled(IntPtr self);

		// Token: 0x060004E3 RID: 1251 RVA: 0x00008360 File Offset: 0x00006560
		internal bool BIsParentalLockEnabled()
		{
			return ISteamParentalSettings._BIsParentalLockEnabled(this.Self);
		}

		// Token: 0x060004E4 RID: 1252
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParentalSettings_BIsParentalLockLocked")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsParentalLockLocked(IntPtr self);

		// Token: 0x060004E5 RID: 1253 RVA: 0x00008380 File Offset: 0x00006580
		internal bool BIsParentalLockLocked()
		{
			return ISteamParentalSettings._BIsParentalLockLocked(this.Self);
		}

		// Token: 0x060004E6 RID: 1254
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParentalSettings_BIsAppBlocked")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsAppBlocked(IntPtr self, AppId nAppID);

		// Token: 0x060004E7 RID: 1255 RVA: 0x000083A0 File Offset: 0x000065A0
		internal bool BIsAppBlocked(AppId nAppID)
		{
			return ISteamParentalSettings._BIsAppBlocked(this.Self, nAppID);
		}

		// Token: 0x060004E8 RID: 1256
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParentalSettings_BIsAppInBlockList")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsAppInBlockList(IntPtr self, AppId nAppID);

		// Token: 0x060004E9 RID: 1257 RVA: 0x000083C0 File Offset: 0x000065C0
		internal bool BIsAppInBlockList(AppId nAppID)
		{
			return ISteamParentalSettings._BIsAppInBlockList(this.Self, nAppID);
		}

		// Token: 0x060004EA RID: 1258
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParentalSettings_BIsFeatureBlocked")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsFeatureBlocked(IntPtr self, ParentalFeature eFeature);

		// Token: 0x060004EB RID: 1259 RVA: 0x000083E0 File Offset: 0x000065E0
		internal bool BIsFeatureBlocked(ParentalFeature eFeature)
		{
			return ISteamParentalSettings._BIsFeatureBlocked(this.Self, eFeature);
		}

		// Token: 0x060004EC RID: 1260
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParentalSettings_BIsFeatureInBlockList")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsFeatureInBlockList(IntPtr self, ParentalFeature eFeature);

		// Token: 0x060004ED RID: 1261 RVA: 0x00008400 File Offset: 0x00006600
		internal bool BIsFeatureInBlockList(ParentalFeature eFeature)
		{
			return ISteamParentalSettings._BIsFeatureInBlockList(this.Self, eFeature);
		}
	}
}
