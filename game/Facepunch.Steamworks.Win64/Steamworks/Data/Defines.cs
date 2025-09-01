using System;

namespace Steamworks.Data
{
	// Token: 0x020001A0 RID: 416
	internal static class Defines
	{
		// Token: 0x06000D62 RID: 3426 RVA: 0x0001709C File Offset: 0x0001529C
		// Note: this type is marked as 'beforefieldinit'.
		static Defines()
		{
		}

		// Token: 0x04000ACE RID: 2766
		internal static readonly int k_cubSaltSize = 8;

		// Token: 0x04000ACF RID: 2767
		internal static readonly GID_t k_GIDNil = ulong.MaxValue;

		// Token: 0x04000AD0 RID: 2768
		internal static readonly GID_t k_TxnIDNil = Defines.k_GIDNil;

		// Token: 0x04000AD1 RID: 2769
		internal static readonly GID_t k_TxnIDUnknown = 0UL;

		// Token: 0x04000AD2 RID: 2770
		internal static readonly JobID_t k_JobIDNil = ulong.MaxValue;

		// Token: 0x04000AD3 RID: 2771
		internal static readonly PackageId_t k_uPackageIdInvalid = uint.MaxValue;

		// Token: 0x04000AD4 RID: 2772
		internal static readonly BundleId_t k_uBundleIdInvalid = 0U;

		// Token: 0x04000AD5 RID: 2773
		internal static readonly AppId k_uAppIdInvalid = 0;

		// Token: 0x04000AD6 RID: 2774
		internal static readonly AssetClassId_t k_ulAssetClassIdInvalid = 0UL;

		// Token: 0x04000AD7 RID: 2775
		internal static readonly PhysicalItemId_t k_uPhysicalItemIdInvalid = 0U;

		// Token: 0x04000AD8 RID: 2776
		internal static readonly DepotId_t k_uDepotIdInvalid = 0U;

		// Token: 0x04000AD9 RID: 2777
		internal static readonly CellID_t k_uCellIDInvalid = uint.MaxValue;

		// Token: 0x04000ADA RID: 2778
		internal static readonly SteamAPICall_t k_uAPICallInvalid = 0UL;

		// Token: 0x04000ADB RID: 2779
		internal static readonly PartnerId_t k_uPartnerIdInvalid = 0U;

		// Token: 0x04000ADC RID: 2780
		internal static readonly ManifestId_t k_uManifestIdInvalid = 0UL;

		// Token: 0x04000ADD RID: 2781
		internal static readonly SiteId_t k_ulSiteIdInvalid = 0UL;

		// Token: 0x04000ADE RID: 2782
		internal static readonly PartyBeaconID_t k_ulPartyBeaconIdInvalid = 0UL;

		// Token: 0x04000ADF RID: 2783
		internal static readonly HAuthTicket k_HAuthTicketInvalid = 0U;

		// Token: 0x04000AE0 RID: 2784
		internal static readonly uint k_unSteamAccountIDMask = uint.MaxValue;

		// Token: 0x04000AE1 RID: 2785
		internal static readonly uint k_unSteamAccountInstanceMask = 1048575U;

		// Token: 0x04000AE2 RID: 2786
		internal static readonly uint k_unSteamUserDefaultInstance = 1U;

		// Token: 0x04000AE3 RID: 2787
		internal static readonly int k_cchGameExtraInfoMax = 64;

		// Token: 0x04000AE4 RID: 2788
		internal static readonly int k_cchMaxFriendsGroupName = 64;

		// Token: 0x04000AE5 RID: 2789
		internal static readonly int k_cFriendsGroupLimit = 100;

		// Token: 0x04000AE6 RID: 2790
		internal static readonly FriendsGroupID_t k_FriendsGroupID_Invalid = -1;

		// Token: 0x04000AE7 RID: 2791
		internal static readonly int k_cEnumerateFollowersMax = 50;

		// Token: 0x04000AE8 RID: 2792
		internal static readonly uint k_cubChatMetadataMax = 8192U;

		// Token: 0x04000AE9 RID: 2793
		internal static readonly int k_cbMaxGameServerGameDir = 32;

		// Token: 0x04000AEA RID: 2794
		internal static readonly int k_cbMaxGameServerMapName = 32;

		// Token: 0x04000AEB RID: 2795
		internal static readonly int k_cbMaxGameServerGameDescription = 64;

		// Token: 0x04000AEC RID: 2796
		internal static readonly int k_cbMaxGameServerName = 64;

		// Token: 0x04000AED RID: 2797
		internal static readonly int k_cbMaxGameServerTags = 128;

		// Token: 0x04000AEE RID: 2798
		internal static readonly int k_cbMaxGameServerGameData = 2048;

		// Token: 0x04000AEF RID: 2799
		internal static readonly int HSERVERQUERY_INVALID = -1;

		// Token: 0x04000AF0 RID: 2800
		internal static readonly uint k_unFavoriteFlagNone = 0U;

		// Token: 0x04000AF1 RID: 2801
		internal static readonly uint k_unFavoriteFlagFavorite = 1U;

		// Token: 0x04000AF2 RID: 2802
		internal static readonly uint k_unFavoriteFlagHistory = 2U;

		// Token: 0x04000AF3 RID: 2803
		internal static readonly uint k_unMaxCloudFileChunkSize = 104857600U;

		// Token: 0x04000AF4 RID: 2804
		internal static readonly PublishedFileId k_PublishedFileIdInvalid = 0UL;

		// Token: 0x04000AF5 RID: 2805
		internal static readonly UGCHandle_t k_UGCHandleInvalid = ulong.MaxValue;

		// Token: 0x04000AF6 RID: 2806
		internal static readonly PublishedFileUpdateHandle_t k_PublishedFileUpdateHandleInvalid = ulong.MaxValue;

		// Token: 0x04000AF7 RID: 2807
		internal static readonly UGCFileWriteStreamHandle_t k_UGCFileStreamHandleInvalid = ulong.MaxValue;

		// Token: 0x04000AF8 RID: 2808
		internal static readonly uint k_cchPublishedDocumentTitleMax = 129U;

		// Token: 0x04000AF9 RID: 2809
		internal static readonly uint k_cchPublishedDocumentDescriptionMax = 8000U;

		// Token: 0x04000AFA RID: 2810
		internal static readonly uint k_cchPublishedDocumentChangeDescriptionMax = 8000U;

		// Token: 0x04000AFB RID: 2811
		internal static readonly uint k_unEnumeratePublishedFilesMaxResults = 50U;

		// Token: 0x04000AFC RID: 2812
		internal static readonly uint k_cchTagListMax = 1025U;

		// Token: 0x04000AFD RID: 2813
		internal static readonly uint k_cchFilenameMax = 260U;

		// Token: 0x04000AFE RID: 2814
		internal static readonly uint k_cchPublishedFileURLMax = 256U;

		// Token: 0x04000AFF RID: 2815
		internal static readonly int k_cubAppProofOfPurchaseKeyMax = 240;

		// Token: 0x04000B00 RID: 2816
		internal static readonly uint k_nScreenshotMaxTaggedUsers = 32U;

		// Token: 0x04000B01 RID: 2817
		internal static readonly uint k_nScreenshotMaxTaggedPublishedFiles = 32U;

		// Token: 0x04000B02 RID: 2818
		internal static readonly int k_cubUFSTagTypeMax = 255;

		// Token: 0x04000B03 RID: 2819
		internal static readonly int k_cubUFSTagValueMax = 255;

		// Token: 0x04000B04 RID: 2820
		internal static readonly int k_ScreenshotThumbWidth = 200;

		// Token: 0x04000B05 RID: 2821
		internal static readonly UGCQueryHandle_t k_UGCQueryHandleInvalid = ulong.MaxValue;

		// Token: 0x04000B06 RID: 2822
		internal static readonly UGCUpdateHandle_t k_UGCUpdateHandleInvalid = ulong.MaxValue;

		// Token: 0x04000B07 RID: 2823
		internal static readonly uint kNumUGCResultsPerPage = 50U;

		// Token: 0x04000B08 RID: 2824
		internal static readonly uint k_cchDeveloperMetadataMax = 5000U;

		// Token: 0x04000B09 RID: 2825
		internal static readonly uint INVALID_HTMLBROWSER = 0U;

		// Token: 0x04000B0A RID: 2826
		internal static readonly InventoryItemId k_SteamItemInstanceIDInvalid = ulong.MaxValue;

		// Token: 0x04000B0B RID: 2827
		internal static readonly SteamInventoryResult_t k_SteamInventoryResultInvalid = -1;

		// Token: 0x04000B0C RID: 2828
		internal static readonly SteamInventoryUpdateHandle_t k_SteamInventoryUpdateHandleInvalid = ulong.MaxValue;

		// Token: 0x04000B0D RID: 2829
		internal static readonly Connection k_HSteamNetConnection_Invalid = 0U;

		// Token: 0x04000B0E RID: 2830
		internal static readonly Socket k_HSteamListenSocket_Invalid = 0U;

		// Token: 0x04000B0F RID: 2831
		internal static readonly HSteamNetPollGroup k_HSteamNetPollGroup_Invalid = 0U;

		// Token: 0x04000B10 RID: 2832
		internal static readonly int k_cchMaxSteamNetworkingErrMsg = 1024;

		// Token: 0x04000B11 RID: 2833
		internal static readonly int k_cchSteamNetworkingMaxConnectionCloseReason = 128;

		// Token: 0x04000B12 RID: 2834
		internal static readonly int k_cchSteamNetworkingMaxConnectionDescription = 128;

		// Token: 0x04000B13 RID: 2835
		internal static readonly int k_cbMaxSteamNetworkingSocketsMessageSizeSend = 524288;

		// Token: 0x04000B14 RID: 2836
		internal static readonly int k_nSteamNetworkingSend_Unreliable = 0;

		// Token: 0x04000B15 RID: 2837
		internal static readonly int k_nSteamNetworkingSend_NoNagle = 1;

		// Token: 0x04000B16 RID: 2838
		internal static readonly int k_nSteamNetworkingSend_UnreliableNoNagle = Defines.k_nSteamNetworkingSend_Unreliable | Defines.k_nSteamNetworkingSend_NoNagle;

		// Token: 0x04000B17 RID: 2839
		internal static readonly int k_nSteamNetworkingSend_NoDelay = 4;

		// Token: 0x04000B18 RID: 2840
		internal static readonly int k_nSteamNetworkingSend_UnreliableNoDelay = Defines.k_nSteamNetworkingSend_Unreliable | Defines.k_nSteamNetworkingSend_NoDelay | Defines.k_nSteamNetworkingSend_NoNagle;

		// Token: 0x04000B19 RID: 2841
		internal static readonly int k_nSteamNetworkingSend_Reliable = 8;

		// Token: 0x04000B1A RID: 2842
		internal static readonly int k_nSteamNetworkingSend_ReliableNoNagle = Defines.k_nSteamNetworkingSend_Reliable | Defines.k_nSteamNetworkingSend_NoNagle;

		// Token: 0x04000B1B RID: 2843
		internal static readonly int k_nSteamNetworkingSend_UseCurrentThread = 16;

		// Token: 0x04000B1C RID: 2844
		internal static readonly int k_cchMaxSteamNetworkingPingLocationString = 1024;

		// Token: 0x04000B1D RID: 2845
		internal static readonly int k_nSteamNetworkingPing_Failed = -1;

		// Token: 0x04000B1E RID: 2846
		internal static readonly int k_nSteamNetworkingPing_Unknown = -2;

		// Token: 0x04000B1F RID: 2847
		internal static readonly SteamNetworkingPOPID k_SteamDatagramPOPID_dev = 6579574U;

		// Token: 0x04000B20 RID: 2848
		internal static readonly uint k_unServerFlagNone = 0U;

		// Token: 0x04000B21 RID: 2849
		internal static readonly uint k_unServerFlagActive = 1U;

		// Token: 0x04000B22 RID: 2850
		internal static readonly uint k_unServerFlagSecure = 2U;

		// Token: 0x04000B23 RID: 2851
		internal static readonly uint k_unServerFlagDedicated = 4U;

		// Token: 0x04000B24 RID: 2852
		internal static readonly uint k_unServerFlagLinux = 8U;

		// Token: 0x04000B25 RID: 2853
		internal static readonly uint k_unServerFlagPassworded = 16U;

		// Token: 0x04000B26 RID: 2854
		internal static readonly uint k_unServerFlagPrivate = 32U;

		// Token: 0x04000B27 RID: 2855
		internal static readonly uint k_cbSteamDatagramMaxSerializedTicket = 512U;

		// Token: 0x04000B28 RID: 2856
		internal static readonly uint k_cbMaxSteamDatagramGameCoordinatorServerLoginAppData = 2048U;

		// Token: 0x04000B29 RID: 2857
		internal static readonly uint k_cbMaxSteamDatagramGameCoordinatorServerLoginSerialized = 4096U;
	}
}
