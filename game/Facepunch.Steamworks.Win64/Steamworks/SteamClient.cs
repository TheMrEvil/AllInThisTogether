using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Steamworks
{
	// Token: 0x02000093 RID: 147
	public static class SteamClient
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x0000B32C File Offset: 0x0000952C
		public static void Init(uint appid, bool asyncCallbacks = true)
		{
			bool flag = SteamClient.initialized;
			if (flag)
			{
				throw new Exception("Calling SteamClient.Init but is already initialized");
			}
			Environment.SetEnvironmentVariable("SteamAppId", appid.ToString());
			Environment.SetEnvironmentVariable("SteamGameId", appid.ToString());
			bool flag2 = !SteamAPI.Init();
			if (flag2)
			{
				throw new Exception("SteamApi_Init returned false. Steam isn't running, couldn't find Steam, AppId is ureleased, Don't own AppId.");
			}
			SteamClient.AppId = appid;
			SteamClient.initialized = true;
			Dispatch.Init();
			Dispatch.ClientPipe = SteamAPI.GetHSteamPipe();
			SteamClient.AddInterface<SteamApps>();
			SteamClient.AddInterface<SteamFriends>();
			SteamClient.AddInterface<SteamInput>();
			SteamClient.AddInterface<SteamInventory>();
			SteamClient.AddInterface<SteamMatchmaking>();
			SteamClient.AddInterface<SteamMatchmakingServers>();
			SteamClient.AddInterface<SteamMusic>();
			SteamClient.AddInterface<SteamNetworking>();
			SteamClient.AddInterface<SteamNetworkingSockets>();
			SteamClient.AddInterface<SteamNetworkingUtils>();
			SteamClient.AddInterface<SteamParental>();
			SteamClient.AddInterface<SteamParties>();
			SteamClient.AddInterface<SteamRemoteStorage>();
			SteamClient.AddInterface<SteamScreenshots>();
			SteamClient.AddInterface<SteamUGC>();
			SteamClient.AddInterface<SteamUser>();
			SteamClient.AddInterface<SteamUserStats>();
			SteamClient.AddInterface<SteamUtils>();
			SteamClient.AddInterface<SteamVideo>();
			SteamClient.AddInterface<SteamRemotePlay>();
			if (asyncCallbacks)
			{
				Dispatch.LoopClientAsync();
			}
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0000B434 File Offset: 0x00009634
		internal static void AddInterface<T>() where T : SteamClass, new()
		{
			T t = Activator.CreateInstance<T>();
			t.InitializeInterface(false);
			SteamClient.openInterfaces.Add(t);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0000B468 File Offset: 0x00009668
		internal static void ShutdownInterfaces()
		{
			foreach (SteamClass steamClass in SteamClient.openInterfaces)
			{
				steamClass.DestroyInterface(false);
			}
			SteamClient.openInterfaces.Clear();
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0000B4CC File Offset: 0x000096CC
		public static bool IsValid
		{
			get
			{
				return SteamClient.initialized;
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0000B4D4 File Offset: 0x000096D4
		public static void Shutdown()
		{
			bool flag = !SteamClient.IsValid;
			if (!flag)
			{
				SteamClient.Cleanup();
				SteamAPI.Shutdown();
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0000B4FC File Offset: 0x000096FC
		internal static void Cleanup()
		{
			Dispatch.ShutdownClient();
			SteamClient.initialized = false;
			SteamClient.ShutdownInterfaces();
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0000B514 File Offset: 0x00009714
		public static void RunCallbacks()
		{
			bool flag = Dispatch.ClientPipe != 0;
			if (flag)
			{
				Dispatch.Frame(Dispatch.ClientPipe);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0000B541 File Offset: 0x00009741
		public static bool IsLoggedOn
		{
			get
			{
				return SteamUser.Internal.BLoggedOn();
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0000B54D File Offset: 0x0000974D
		public static SteamId SteamId
		{
			get
			{
				return SteamUser.Internal.GetSteamID();
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0000B559 File Offset: 0x00009759
		public static string Name
		{
			get
			{
				return SteamFriends.Internal.GetPersonaName();
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x0000B565 File Offset: 0x00009765
		public static FriendState State
		{
			get
			{
				return SteamFriends.Internal.GetPersonaState();
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0000B571 File Offset: 0x00009771
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x0000B578 File Offset: 0x00009778
		public static AppId AppId
		{
			[CompilerGenerated]
			get
			{
				return SteamClient.<AppId>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				SteamClient.<AppId>k__BackingField = value;
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0000B580 File Offset: 0x00009780
		public static bool RestartAppIfNecessary(uint appid)
		{
			return SteamAPI.RestartAppIfNecessary(appid);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0000B598 File Offset: 0x00009798
		internal static void ValidCheck()
		{
			bool flag = !SteamClient.IsValid;
			if (flag)
			{
				throw new Exception("SteamClient isn't initialized");
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0000B5BD File Offset: 0x000097BD
		// Note: this type is marked as 'beforefieldinit'.
		static SteamClient()
		{
		}

		// Token: 0x040006DF RID: 1759
		private static bool initialized;

		// Token: 0x040006E0 RID: 1760
		private static readonly List<SteamClass> openInterfaces = new List<SteamClass>();

		// Token: 0x040006E1 RID: 1761
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static AppId <AppId>k__BackingField;
	}
}
