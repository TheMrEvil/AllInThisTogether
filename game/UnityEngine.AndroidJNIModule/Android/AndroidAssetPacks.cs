using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Android
{
	// Token: 0x02000018 RID: 24
	[StaticAccessor("AndroidAssetPacksBindingsHelpers", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/AndroidJNI/Public/AndroidAssetPacksBindingsHelpers.h")]
	public static class AndroidAssetPacks
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00008454 File Offset: 0x00006654
		public static bool coreUnityAssetPacksDownloaded
		{
			get
			{
				return AndroidAssetPacks.CoreUnityAssetPacksDownloaded();
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000846C File Offset: 0x0000666C
		internal static string dataPackName
		{
			get
			{
				return AndroidAssetPacks.GetDataPackName();
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00008484 File Offset: 0x00006684
		internal static string streamingAssetsPackName
		{
			get
			{
				return AndroidAssetPacks.GetStreamingAssetsPackName();
			}
		}

		// Token: 0x060001AF RID: 431
		[NativeConditional("PLATFORM_ANDROID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CoreUnityAssetPacksDownloaded();

		// Token: 0x060001B0 RID: 432 RVA: 0x0000849C File Offset: 0x0000669C
		public static string[] GetCoreUnityAssetPackNames()
		{
			return new string[0];
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000084B4 File Offset: 0x000066B4
		public static void GetAssetPackStateAsync(string[] assetPackNames, Action<ulong, AndroidAssetPackState[]> callback)
		{
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000084B8 File Offset: 0x000066B8
		public static GetAssetPackStateAsyncOperation GetAssetPackStateAsync(string[] assetPackNames)
		{
			return null;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000084B4 File Offset: 0x000066B4
		public static void DownloadAssetPackAsync(string[] assetPackNames, Action<AndroidAssetPackInfo> callback)
		{
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000084CC File Offset: 0x000066CC
		public static DownloadAssetPackAsyncOperation DownloadAssetPackAsync(string[] assetPackNames)
		{
			return null;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000084B4 File Offset: 0x000066B4
		public static void RequestToUseMobileDataAsync(Action<AndroidAssetPackUseMobileDataRequestResult> callback)
		{
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000084E0 File Offset: 0x000066E0
		public static RequestToUseMobileDataAsyncOperation RequestToUseMobileDataAsync()
		{
			return null;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000084F4 File Offset: 0x000066F4
		public static string GetAssetPackPath(string assetPackName)
		{
			return "";
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000084B4 File Offset: 0x000066B4
		public static void CancelAssetPackDownload(string[] assetPackNames)
		{
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000084B4 File Offset: 0x000066B4
		public static void RemoveAssetPack(string assetPackName)
		{
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000850C File Offset: 0x0000670C
		private static string GetDataPackName()
		{
			return "UnityDataAssetPack";
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008524 File Offset: 0x00006724
		private static string GetStreamingAssetsPackName()
		{
			return "UnityStreamingAssetsPack";
		}
	}
}
