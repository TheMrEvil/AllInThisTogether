using System;
using UnityEngine.Events;

namespace UnityEngine.Device
{
	// Token: 0x02000451 RID: 1105
	public static class Application
	{
		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060026F8 RID: 9976 RVA: 0x00041154 File Offset: 0x0003F354
		public static string absoluteURL
		{
			get
			{
				return Application.absoluteURL;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060026F9 RID: 9977 RVA: 0x0004115B File Offset: 0x0003F35B
		// (set) Token: 0x060026FA RID: 9978 RVA: 0x00041162 File Offset: 0x0003F362
		public static ThreadPriority backgroundLoadingPriority
		{
			get
			{
				return Application.backgroundLoadingPriority;
			}
			set
			{
				Application.backgroundLoadingPriority = value;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060026FB RID: 9979 RVA: 0x0004116B File Offset: 0x0003F36B
		public static string buildGUID
		{
			get
			{
				return Application.buildGUID;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060026FC RID: 9980 RVA: 0x00041172 File Offset: 0x0003F372
		public static string cloudProjectId
		{
			get
			{
				return Application.cloudProjectId;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060026FD RID: 9981 RVA: 0x00041179 File Offset: 0x0003F379
		public static string companyName
		{
			get
			{
				return Application.companyName;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060026FE RID: 9982 RVA: 0x00041180 File Offset: 0x0003F380
		public static string consoleLogPath
		{
			get
			{
				return Application.consoleLogPath;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060026FF RID: 9983 RVA: 0x00041187 File Offset: 0x0003F387
		public static string dataPath
		{
			get
			{
				return Application.dataPath;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002700 RID: 9984 RVA: 0x0004118E File Offset: 0x0003F38E
		public static bool genuine
		{
			get
			{
				return Application.genuine;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002701 RID: 9985 RVA: 0x00041195 File Offset: 0x0003F395
		public static bool genuineCheckAvailable
		{
			get
			{
				return Application.genuineCheckAvailable;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x0004119C File Offset: 0x0003F39C
		public static string identifier
		{
			get
			{
				return Application.identifier;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06002703 RID: 9987 RVA: 0x000411A3 File Offset: 0x0003F3A3
		public static string installerName
		{
			get
			{
				return Application.installerName;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06002704 RID: 9988 RVA: 0x000411AA File Offset: 0x0003F3AA
		public static ApplicationInstallMode installMode
		{
			get
			{
				return Application.installMode;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06002705 RID: 9989 RVA: 0x000411B1 File Offset: 0x0003F3B1
		public static NetworkReachability internetReachability
		{
			get
			{
				return Application.internetReachability;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002706 RID: 9990 RVA: 0x000411B8 File Offset: 0x0003F3B8
		public static bool isBatchMode
		{
			get
			{
				return Application.isBatchMode;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002707 RID: 9991 RVA: 0x000411BF File Offset: 0x0003F3BF
		public static bool isConsolePlatform
		{
			get
			{
				return Application.isConsolePlatform;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002708 RID: 9992 RVA: 0x000411C6 File Offset: 0x0003F3C6
		public static bool isEditor
		{
			get
			{
				return Application.isEditor;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002709 RID: 9993 RVA: 0x000411CD File Offset: 0x0003F3CD
		public static bool isFocused
		{
			get
			{
				return Application.isFocused;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x000411D4 File Offset: 0x0003F3D4
		public static bool isMobilePlatform
		{
			get
			{
				return Application.isMobilePlatform;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x0600270B RID: 9995 RVA: 0x000411DB File Offset: 0x0003F3DB
		public static bool isPlaying
		{
			get
			{
				return Application.isPlaying;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x0600270C RID: 9996 RVA: 0x000411E2 File Offset: 0x0003F3E2
		public static string persistentDataPath
		{
			get
			{
				return Application.persistentDataPath;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x000411E9 File Offset: 0x0003F3E9
		public static RuntimePlatform platform
		{
			get
			{
				return Application.platform;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x0600270E RID: 9998 RVA: 0x000411F0 File Offset: 0x0003F3F0
		public static string productName
		{
			get
			{
				return Application.productName;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x0600270F RID: 9999 RVA: 0x000411F7 File Offset: 0x0003F3F7
		// (set) Token: 0x06002710 RID: 10000 RVA: 0x000411FE File Offset: 0x0003F3FE
		public static bool runInBackground
		{
			get
			{
				return Application.runInBackground;
			}
			set
			{
				Application.runInBackground = value;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x00041207 File Offset: 0x0003F407
		public static ApplicationSandboxType sandboxType
		{
			get
			{
				return Application.sandboxType;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06002712 RID: 10002 RVA: 0x0004120E File Offset: 0x0003F40E
		public static string streamingAssetsPath
		{
			get
			{
				return Application.streamingAssetsPath;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002713 RID: 10003 RVA: 0x00041215 File Offset: 0x0003F415
		public static SystemLanguage systemLanguage
		{
			get
			{
				return Application.systemLanguage;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002714 RID: 10004 RVA: 0x0004121C File Offset: 0x0003F41C
		// (set) Token: 0x06002715 RID: 10005 RVA: 0x00041223 File Offset: 0x0003F423
		public static int targetFrameRate
		{
			get
			{
				return Application.targetFrameRate;
			}
			set
			{
				Application.targetFrameRate = value;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002716 RID: 10006 RVA: 0x0004122C File Offset: 0x0003F42C
		public static string temporaryCachePath
		{
			get
			{
				return Application.temporaryCachePath;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002717 RID: 10007 RVA: 0x00041233 File Offset: 0x0003F433
		public static string unityVersion
		{
			get
			{
				return Application.unityVersion;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002718 RID: 10008 RVA: 0x0004123A File Offset: 0x0003F43A
		public static string version
		{
			get
			{
				return Application.version;
			}
		}

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06002719 RID: 10009 RVA: 0x00041241 File Offset: 0x0003F441
		// (remove) Token: 0x0600271A RID: 10010 RVA: 0x0004124A File Offset: 0x0003F44A
		public static event Action<string> deepLinkActivated
		{
			add
			{
				Application.deepLinkActivated += value;
			}
			remove
			{
				Application.deepLinkActivated -= value;
			}
		}

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x0600271B RID: 10011 RVA: 0x00041253 File Offset: 0x0003F453
		// (remove) Token: 0x0600271C RID: 10012 RVA: 0x0004125C File Offset: 0x0003F45C
		public static event Action<bool> focusChanged
		{
			add
			{
				Application.focusChanged += value;
			}
			remove
			{
				Application.focusChanged -= value;
			}
		}

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x0600271D RID: 10013 RVA: 0x00041265 File Offset: 0x0003F465
		// (remove) Token: 0x0600271E RID: 10014 RVA: 0x0004126E File Offset: 0x0003F46E
		public static event Application.LogCallback logMessageReceived
		{
			add
			{
				Application.logMessageReceived += value;
			}
			remove
			{
				Application.logMessageReceived -= value;
			}
		}

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x0600271F RID: 10015 RVA: 0x00041277 File Offset: 0x0003F477
		// (remove) Token: 0x06002720 RID: 10016 RVA: 0x00041280 File Offset: 0x0003F480
		public static event Application.LogCallback logMessageReceivedThreaded
		{
			add
			{
				Application.logMessageReceivedThreaded += value;
			}
			remove
			{
				Application.logMessageReceivedThreaded -= value;
			}
		}

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06002721 RID: 10017 RVA: 0x00041289 File Offset: 0x0003F489
		// (remove) Token: 0x06002722 RID: 10018 RVA: 0x00041292 File Offset: 0x0003F492
		public static event Application.LowMemoryCallback lowMemory
		{
			add
			{
				Application.lowMemory += value;
			}
			remove
			{
				Application.lowMemory -= value;
			}
		}

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06002723 RID: 10019 RVA: 0x0004129B File Offset: 0x0003F49B
		// (remove) Token: 0x06002724 RID: 10020 RVA: 0x000412A4 File Offset: 0x0003F4A4
		public static event UnityAction onBeforeRender
		{
			add
			{
				Application.onBeforeRender += value;
			}
			remove
			{
				Application.onBeforeRender -= value;
			}
		}

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06002725 RID: 10021 RVA: 0x000412AD File Offset: 0x0003F4AD
		// (remove) Token: 0x06002726 RID: 10022 RVA: 0x000412B6 File Offset: 0x0003F4B6
		public static event Action quitting
		{
			add
			{
				Application.quitting += value;
			}
			remove
			{
				Application.quitting -= value;
			}
		}

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06002727 RID: 10023 RVA: 0x000412BF File Offset: 0x0003F4BF
		// (remove) Token: 0x06002728 RID: 10024 RVA: 0x000412C8 File Offset: 0x0003F4C8
		public static event Func<bool> wantsToQuit
		{
			add
			{
				Application.wantsToQuit += value;
			}
			remove
			{
				Application.wantsToQuit -= value;
			}
		}

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06002729 RID: 10025 RVA: 0x000412D1 File Offset: 0x0003F4D1
		// (remove) Token: 0x0600272A RID: 10026 RVA: 0x000412DA File Offset: 0x0003F4DA
		public static event Action unloading
		{
			add
			{
				Application.unloading += value;
			}
			remove
			{
				Application.unloading -= value;
			}
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x000412E4 File Offset: 0x0003F4E4
		public static bool CanStreamedLevelBeLoaded(int levelIndex)
		{
			return Application.CanStreamedLevelBeLoaded(levelIndex);
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x000412FC File Offset: 0x0003F4FC
		public static bool CanStreamedLevelBeLoaded(string levelName)
		{
			return Application.CanStreamedLevelBeLoaded(levelName);
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x00041314 File Offset: 0x0003F514
		public static string[] GetBuildTags()
		{
			return Application.GetBuildTags();
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x0004132C File Offset: 0x0003F52C
		public static StackTraceLogType GetStackTraceLogType(LogType logType)
		{
			return Application.GetStackTraceLogType(logType);
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x00041344 File Offset: 0x0003F544
		public static bool HasProLicense()
		{
			return Application.HasProLicense();
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x0004135C File Offset: 0x0003F55C
		public static bool HasUserAuthorization(UserAuthorization mode)
		{
			return Application.HasUserAuthorization(mode);
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x00041374 File Offset: 0x0003F574
		public static bool IsPlaying(Object obj)
		{
			return Application.IsPlaying(obj);
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x0004138C File Offset: 0x0003F58C
		public static void OpenURL(string url)
		{
			Application.OpenURL(url);
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x00041396 File Offset: 0x0003F596
		public static void Quit()
		{
			Application.Quit();
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x0004139F File Offset: 0x0003F59F
		public static void Quit(int exitCode)
		{
			Application.Quit(exitCode);
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x000413AC File Offset: 0x0003F5AC
		public static bool RequestAdvertisingIdentifierAsync(Application.AdvertisingIdentifierCallback delegateMethod)
		{
			return Application.RequestAdvertisingIdentifierAsync(delegateMethod);
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x000413C4 File Offset: 0x0003F5C4
		public static AsyncOperation RequestUserAuthorization(UserAuthorization mode)
		{
			return Application.RequestUserAuthorization(mode);
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x000413DC File Offset: 0x0003F5DC
		public static void SetBuildTags(string[] buildTags)
		{
			Application.SetBuildTags(buildTags);
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x000413E6 File Offset: 0x0003F5E6
		public static void SetStackTraceLogType(LogType logType, StackTraceLogType stackTraceType)
		{
			Application.SetStackTraceLogType(logType, stackTraceType);
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x000413F1 File Offset: 0x0003F5F1
		public static void Unload()
		{
			Application.Unload();
		}
	}
}
