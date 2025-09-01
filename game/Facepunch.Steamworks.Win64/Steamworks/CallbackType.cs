using System;

namespace Steamworks
{
	// Token: 0x0200000B RID: 11
	public enum CallbackType
	{
		// Token: 0x0400000F RID: 15
		SteamServersConnected = 101,
		// Token: 0x04000010 RID: 16
		SteamServerConnectFailure,
		// Token: 0x04000011 RID: 17
		SteamServersDisconnected,
		// Token: 0x04000012 RID: 18
		ClientGameServerDeny = 113,
		// Token: 0x04000013 RID: 19
		GSPolicyResponse = 115,
		// Token: 0x04000014 RID: 20
		IPCFailure = 117,
		// Token: 0x04000015 RID: 21
		LicensesUpdated = 125,
		// Token: 0x04000016 RID: 22
		ValidateAuthTicketResponse = 143,
		// Token: 0x04000017 RID: 23
		MicroTxnAuthorizationResponse = 152,
		// Token: 0x04000018 RID: 24
		EncryptedAppTicketResponse = 154,
		// Token: 0x04000019 RID: 25
		GetAuthSessionTicketResponse = 163,
		// Token: 0x0400001A RID: 26
		GameWebCallback,
		// Token: 0x0400001B RID: 27
		StoreAuthURLResponse,
		// Token: 0x0400001C RID: 28
		MarketEligibilityResponse,
		// Token: 0x0400001D RID: 29
		DurationControl,
		// Token: 0x0400001E RID: 30
		GSClientApprove = 201,
		// Token: 0x0400001F RID: 31
		GSClientDeny,
		// Token: 0x04000020 RID: 32
		GSClientKick,
		// Token: 0x04000021 RID: 33
		GSClientAchievementStatus = 206,
		// Token: 0x04000022 RID: 34
		GSGameplayStats,
		// Token: 0x04000023 RID: 35
		GSClientGroupStatus,
		// Token: 0x04000024 RID: 36
		GSReputation,
		// Token: 0x04000025 RID: 37
		AssociateWithClanResult,
		// Token: 0x04000026 RID: 38
		ComputeNewPlayerCompatibilityResult,
		// Token: 0x04000027 RID: 39
		PersonaStateChange = 304,
		// Token: 0x04000028 RID: 40
		GameOverlayActivated = 331,
		// Token: 0x04000029 RID: 41
		GameServerChangeRequested,
		// Token: 0x0400002A RID: 42
		GameLobbyJoinRequested,
		// Token: 0x0400002B RID: 43
		AvatarImageLoaded,
		// Token: 0x0400002C RID: 44
		ClanOfficerListResponse,
		// Token: 0x0400002D RID: 45
		FriendRichPresenceUpdate,
		// Token: 0x0400002E RID: 46
		GameRichPresenceJoinRequested,
		// Token: 0x0400002F RID: 47
		GameConnectedClanChatMsg,
		// Token: 0x04000030 RID: 48
		GameConnectedChatJoin,
		// Token: 0x04000031 RID: 49
		GameConnectedChatLeave,
		// Token: 0x04000032 RID: 50
		DownloadClanActivityCountsResult,
		// Token: 0x04000033 RID: 51
		JoinClanChatRoomCompletionResult,
		// Token: 0x04000034 RID: 52
		GameConnectedFriendChatMsg,
		// Token: 0x04000035 RID: 53
		FriendsGetFollowerCount,
		// Token: 0x04000036 RID: 54
		FriendsIsFollowing,
		// Token: 0x04000037 RID: 55
		FriendsEnumerateFollowingList,
		// Token: 0x04000038 RID: 56
		SetPersonaNameResponse,
		// Token: 0x04000039 RID: 57
		UnreadChatMessagesChanged,
		// Token: 0x0400003A RID: 58
		FavoritesListChanged = 502,
		// Token: 0x0400003B RID: 59
		LobbyInvite,
		// Token: 0x0400003C RID: 60
		LobbyEnter,
		// Token: 0x0400003D RID: 61
		LobbyDataUpdate,
		// Token: 0x0400003E RID: 62
		LobbyChatUpdate,
		// Token: 0x0400003F RID: 63
		LobbyChatMsg,
		// Token: 0x04000040 RID: 64
		LobbyGameCreated = 509,
		// Token: 0x04000041 RID: 65
		LobbyMatchList,
		// Token: 0x04000042 RID: 66
		LobbyKicked = 512,
		// Token: 0x04000043 RID: 67
		LobbyCreated,
		// Token: 0x04000044 RID: 68
		PSNGameBootInviteResult = 515,
		// Token: 0x04000045 RID: 69
		FavoritesListAccountsUpdated,
		// Token: 0x04000046 RID: 70
		IPCountry = 701,
		// Token: 0x04000047 RID: 71
		LowBatteryPower,
		// Token: 0x04000048 RID: 72
		SteamAPICallCompleted,
		// Token: 0x04000049 RID: 73
		SteamShutdown,
		// Token: 0x0400004A RID: 74
		CheckFileSignature,
		// Token: 0x0400004B RID: 75
		GamepadTextInputDismissed = 714,
		// Token: 0x0400004C RID: 76
		DlcInstalled = 1005,
		// Token: 0x0400004D RID: 77
		RegisterActivationCodeResponse = 1008,
		// Token: 0x0400004E RID: 78
		NewUrlLaunchParameters = 1014,
		// Token: 0x0400004F RID: 79
		AppProofOfPurchaseKeyResponse = 1021,
		// Token: 0x04000050 RID: 80
		FileDetailsResult = 1023,
		// Token: 0x04000051 RID: 81
		UserStatsReceived = 1101,
		// Token: 0x04000052 RID: 82
		UserStatsStored,
		// Token: 0x04000053 RID: 83
		UserAchievementStored,
		// Token: 0x04000054 RID: 84
		LeaderboardFindResult,
		// Token: 0x04000055 RID: 85
		LeaderboardScoresDownloaded,
		// Token: 0x04000056 RID: 86
		LeaderboardScoreUploaded,
		// Token: 0x04000057 RID: 87
		NumberOfCurrentPlayers,
		// Token: 0x04000058 RID: 88
		UserStatsUnloaded,
		// Token: 0x04000059 RID: 89
		GSStatsUnloaded = 1108,
		// Token: 0x0400005A RID: 90
		UserAchievementIconFetched,
		// Token: 0x0400005B RID: 91
		GlobalAchievementPercentagesReady,
		// Token: 0x0400005C RID: 92
		LeaderboardUGCSet,
		// Token: 0x0400005D RID: 93
		GlobalStatsReceived,
		// Token: 0x0400005E RID: 94
		P2PSessionRequest = 1202,
		// Token: 0x0400005F RID: 95
		P2PSessionConnectFail,
		// Token: 0x04000060 RID: 96
		SteamNetConnectionStatusChangedCallback = 1221,
		// Token: 0x04000061 RID: 97
		SteamNetAuthenticationStatus,
		// Token: 0x04000062 RID: 98
		SteamRelayNetworkStatus = 1281,
		// Token: 0x04000063 RID: 99
		RemoteStorageAppSyncedClient = 1301,
		// Token: 0x04000064 RID: 100
		RemoteStorageAppSyncedServer,
		// Token: 0x04000065 RID: 101
		RemoteStorageAppSyncProgress,
		// Token: 0x04000066 RID: 102
		RemoteStorageAppSyncStatusCheck = 1305,
		// Token: 0x04000067 RID: 103
		RemoteStorageFileShareResult = 1307,
		// Token: 0x04000068 RID: 104
		RemoteStoragePublishFileResult = 1309,
		// Token: 0x04000069 RID: 105
		RemoteStorageDeletePublishedFileResult = 1311,
		// Token: 0x0400006A RID: 106
		RemoteStorageEnumerateUserPublishedFilesResult,
		// Token: 0x0400006B RID: 107
		RemoteStorageSubscribePublishedFileResult,
		// Token: 0x0400006C RID: 108
		RemoteStorageEnumerateUserSubscribedFilesResult,
		// Token: 0x0400006D RID: 109
		RemoteStorageUnsubscribePublishedFileResult,
		// Token: 0x0400006E RID: 110
		RemoteStorageUpdatePublishedFileResult,
		// Token: 0x0400006F RID: 111
		RemoteStorageDownloadUGCResult,
		// Token: 0x04000070 RID: 112
		RemoteStorageGetPublishedFileDetailsResult,
		// Token: 0x04000071 RID: 113
		RemoteStorageEnumerateWorkshopFilesResult,
		// Token: 0x04000072 RID: 114
		RemoteStorageGetPublishedItemVoteDetailsResult,
		// Token: 0x04000073 RID: 115
		RemoteStoragePublishedFileSubscribed,
		// Token: 0x04000074 RID: 116
		RemoteStoragePublishedFileUnsubscribed,
		// Token: 0x04000075 RID: 117
		RemoteStoragePublishedFileDeleted,
		// Token: 0x04000076 RID: 118
		RemoteStorageUpdateUserPublishedItemVoteResult,
		// Token: 0x04000077 RID: 119
		RemoteStorageUserVoteDetails,
		// Token: 0x04000078 RID: 120
		RemoteStorageEnumerateUserSharedWorkshopFilesResult,
		// Token: 0x04000079 RID: 121
		RemoteStorageSetUserPublishedFileActionResult,
		// Token: 0x0400007A RID: 122
		RemoteStorageEnumeratePublishedFilesByUserActionResult,
		// Token: 0x0400007B RID: 123
		RemoteStoragePublishFileProgress,
		// Token: 0x0400007C RID: 124
		RemoteStoragePublishedFileUpdated,
		// Token: 0x0400007D RID: 125
		RemoteStorageFileWriteAsyncComplete,
		// Token: 0x0400007E RID: 126
		RemoteStorageFileReadAsyncComplete,
		// Token: 0x0400007F RID: 127
		GSStatsReceived = 1800,
		// Token: 0x04000080 RID: 128
		GSStatsStored,
		// Token: 0x04000081 RID: 129
		HTTPRequestCompleted = 2101,
		// Token: 0x04000082 RID: 130
		HTTPRequestHeadersReceived,
		// Token: 0x04000083 RID: 131
		HTTPRequestDataReceived,
		// Token: 0x04000084 RID: 132
		ScreenshotReady = 2301,
		// Token: 0x04000085 RID: 133
		ScreenshotRequested,
		// Token: 0x04000086 RID: 134
		SteamUGCQueryCompleted = 3401,
		// Token: 0x04000087 RID: 135
		SteamUGCRequestUGCDetailsResult,
		// Token: 0x04000088 RID: 136
		CreateItemResult,
		// Token: 0x04000089 RID: 137
		SubmitItemUpdateResult,
		// Token: 0x0400008A RID: 138
		ItemInstalled,
		// Token: 0x0400008B RID: 139
		DownloadItemResult,
		// Token: 0x0400008C RID: 140
		UserFavoriteItemsListChanged,
		// Token: 0x0400008D RID: 141
		SetUserItemVoteResult,
		// Token: 0x0400008E RID: 142
		GetUserItemVoteResult,
		// Token: 0x0400008F RID: 143
		StartPlaytimeTrackingResult,
		// Token: 0x04000090 RID: 144
		StopPlaytimeTrackingResult,
		// Token: 0x04000091 RID: 145
		AddUGCDependencyResult,
		// Token: 0x04000092 RID: 146
		RemoveUGCDependencyResult,
		// Token: 0x04000093 RID: 147
		AddAppDependencyResult,
		// Token: 0x04000094 RID: 148
		RemoveAppDependencyResult,
		// Token: 0x04000095 RID: 149
		GetAppDependenciesResult,
		// Token: 0x04000096 RID: 150
		DeleteItemResult,
		// Token: 0x04000097 RID: 151
		SteamAppInstalled = 3901,
		// Token: 0x04000098 RID: 152
		SteamAppUninstalled,
		// Token: 0x04000099 RID: 153
		PlaybackStatusHasChanged = 4001,
		// Token: 0x0400009A RID: 154
		VolumeHasChanged,
		// Token: 0x0400009B RID: 155
		MusicPlayerWantsVolume = 4011,
		// Token: 0x0400009C RID: 156
		MusicPlayerSelectsQueueEntry,
		// Token: 0x0400009D RID: 157
		MusicPlayerSelectsPlaylistEntry,
		// Token: 0x0400009E RID: 158
		MusicPlayerRemoteWillActivate = 4101,
		// Token: 0x0400009F RID: 159
		MusicPlayerRemoteWillDeactivate,
		// Token: 0x040000A0 RID: 160
		MusicPlayerRemoteToFront,
		// Token: 0x040000A1 RID: 161
		MusicPlayerWillQuit,
		// Token: 0x040000A2 RID: 162
		MusicPlayerWantsPlay,
		// Token: 0x040000A3 RID: 163
		MusicPlayerWantsPause,
		// Token: 0x040000A4 RID: 164
		MusicPlayerWantsPlayPrevious,
		// Token: 0x040000A5 RID: 165
		MusicPlayerWantsPlayNext,
		// Token: 0x040000A6 RID: 166
		MusicPlayerWantsShuffled,
		// Token: 0x040000A7 RID: 167
		MusicPlayerWantsLooped,
		// Token: 0x040000A8 RID: 168
		MusicPlayerWantsPlayingRepeatStatus = 4114,
		// Token: 0x040000A9 RID: 169
		HTML_BrowserReady = 4501,
		// Token: 0x040000AA RID: 170
		HTML_NeedsPaint,
		// Token: 0x040000AB RID: 171
		HTML_StartRequest,
		// Token: 0x040000AC RID: 172
		HTML_CloseBrowser,
		// Token: 0x040000AD RID: 173
		HTML_URLChanged,
		// Token: 0x040000AE RID: 174
		HTML_FinishedRequest,
		// Token: 0x040000AF RID: 175
		HTML_OpenLinkInNewTab,
		// Token: 0x040000B0 RID: 176
		HTML_ChangedTitle,
		// Token: 0x040000B1 RID: 177
		HTML_SearchResults,
		// Token: 0x040000B2 RID: 178
		HTML_CanGoBackAndForward,
		// Token: 0x040000B3 RID: 179
		HTML_HorizontalScroll,
		// Token: 0x040000B4 RID: 180
		HTML_VerticalScroll,
		// Token: 0x040000B5 RID: 181
		HTML_LinkAtPosition,
		// Token: 0x040000B6 RID: 182
		HTML_JSAlert,
		// Token: 0x040000B7 RID: 183
		HTML_JSConfirm,
		// Token: 0x040000B8 RID: 184
		HTML_FileOpenDialog,
		// Token: 0x040000B9 RID: 185
		HTML_NewWindow = 4521,
		// Token: 0x040000BA RID: 186
		HTML_SetCursor,
		// Token: 0x040000BB RID: 187
		HTML_StatusText,
		// Token: 0x040000BC RID: 188
		HTML_ShowToolTip,
		// Token: 0x040000BD RID: 189
		HTML_UpdateToolTip,
		// Token: 0x040000BE RID: 190
		HTML_HideToolTip,
		// Token: 0x040000BF RID: 191
		HTML_BrowserRestarted,
		// Token: 0x040000C0 RID: 192
		BroadcastUploadStart = 4604,
		// Token: 0x040000C1 RID: 193
		BroadcastUploadStop,
		// Token: 0x040000C2 RID: 194
		GetVideoURLResult = 4611,
		// Token: 0x040000C3 RID: 195
		GetOPFSettingsResult = 4624,
		// Token: 0x040000C4 RID: 196
		SteamInventoryResultReady = 4700,
		// Token: 0x040000C5 RID: 197
		SteamInventoryFullUpdate,
		// Token: 0x040000C6 RID: 198
		SteamInventoryDefinitionUpdate,
		// Token: 0x040000C7 RID: 199
		SteamInventoryEligiblePromoItemDefIDs,
		// Token: 0x040000C8 RID: 200
		SteamInventoryStartPurchaseResult,
		// Token: 0x040000C9 RID: 201
		SteamInventoryRequestPricesResult,
		// Token: 0x040000CA RID: 202
		SteamParentalSettingsChanged = 5001,
		// Token: 0x040000CB RID: 203
		SearchForGameProgressCallback = 5201,
		// Token: 0x040000CC RID: 204
		SearchForGameResultCallback,
		// Token: 0x040000CD RID: 205
		RequestPlayersForGameProgressCallback = 5211,
		// Token: 0x040000CE RID: 206
		RequestPlayersForGameResultCallback,
		// Token: 0x040000CF RID: 207
		RequestPlayersForGameFinalResultCallback,
		// Token: 0x040000D0 RID: 208
		SubmitPlayerResultResultCallback,
		// Token: 0x040000D1 RID: 209
		EndGameResultCallback,
		// Token: 0x040000D2 RID: 210
		JoinPartyCallback = 5301,
		// Token: 0x040000D3 RID: 211
		CreateBeaconCallback,
		// Token: 0x040000D4 RID: 212
		ReservationNotificationCallback,
		// Token: 0x040000D5 RID: 213
		ChangeNumOpenSlotsCallback,
		// Token: 0x040000D6 RID: 214
		AvailableBeaconLocationsUpdated,
		// Token: 0x040000D7 RID: 215
		ActiveBeaconsUpdated,
		// Token: 0x040000D8 RID: 216
		SteamRemotePlaySessionConnected = 5701,
		// Token: 0x040000D9 RID: 217
		SteamRemotePlaySessionDisconnected
	}
}
