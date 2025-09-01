using System;

namespace Photon.Realtime
{
	// Token: 0x02000026 RID: 38
	public class ParameterCode
	{
		// Token: 0x06000119 RID: 281 RVA: 0x000083C7 File Offset: 0x000065C7
		public ParameterCode()
		{
		}

		// Token: 0x04000107 RID: 263
		public const byte SuppressRoomEvents = 237;

		// Token: 0x04000108 RID: 264
		public const byte EmptyRoomTTL = 236;

		// Token: 0x04000109 RID: 265
		public const byte PlayerTTL = 235;

		// Token: 0x0400010A RID: 266
		public const byte EventForward = 234;

		// Token: 0x0400010B RID: 267
		[Obsolete("Use: IsInactive")]
		public const byte IsComingBack = 233;

		// Token: 0x0400010C RID: 268
		public const byte IsInactive = 233;

		// Token: 0x0400010D RID: 269
		public const byte CheckUserOnJoin = 232;

		// Token: 0x0400010E RID: 270
		public const byte ExpectedValues = 231;

		// Token: 0x0400010F RID: 271
		public const byte Address = 230;

		// Token: 0x04000110 RID: 272
		public const byte PeerCount = 229;

		// Token: 0x04000111 RID: 273
		public const byte GameCount = 228;

		// Token: 0x04000112 RID: 274
		public const byte MasterPeerCount = 227;

		// Token: 0x04000113 RID: 275
		public const byte UserId = 225;

		// Token: 0x04000114 RID: 276
		public const byte ApplicationId = 224;

		// Token: 0x04000115 RID: 277
		public const byte Position = 223;

		// Token: 0x04000116 RID: 278
		public const byte MatchMakingType = 223;

		// Token: 0x04000117 RID: 279
		public const byte GameList = 222;

		// Token: 0x04000118 RID: 280
		public const byte Token = 221;

		// Token: 0x04000119 RID: 281
		public const byte AppVersion = 220;

		// Token: 0x0400011A RID: 282
		[Obsolete("TCP routing was removed after becoming obsolete.")]
		public const byte AzureNodeInfo = 210;

		// Token: 0x0400011B RID: 283
		[Obsolete("TCP routing was removed after becoming obsolete.")]
		public const byte AzureLocalNodeId = 209;

		// Token: 0x0400011C RID: 284
		[Obsolete("TCP routing was removed after becoming obsolete.")]
		public const byte AzureMasterNodeId = 208;

		// Token: 0x0400011D RID: 285
		public const byte RoomName = 255;

		// Token: 0x0400011E RID: 286
		public const byte Broadcast = 250;

		// Token: 0x0400011F RID: 287
		public const byte ActorList = 252;

		// Token: 0x04000120 RID: 288
		public const byte ActorNr = 254;

		// Token: 0x04000121 RID: 289
		public const byte PlayerProperties = 249;

		// Token: 0x04000122 RID: 290
		public const byte CustomEventContent = 245;

		// Token: 0x04000123 RID: 291
		public const byte Data = 245;

		// Token: 0x04000124 RID: 292
		public const byte Code = 244;

		// Token: 0x04000125 RID: 293
		public const byte GameProperties = 248;

		// Token: 0x04000126 RID: 294
		public const byte Properties = 251;

		// Token: 0x04000127 RID: 295
		public const byte TargetActorNr = 253;

		// Token: 0x04000128 RID: 296
		public const byte ReceiverGroup = 246;

		// Token: 0x04000129 RID: 297
		public const byte Cache = 247;

		// Token: 0x0400012A RID: 298
		public const byte CleanupCacheOnLeave = 241;

		// Token: 0x0400012B RID: 299
		public const byte Group = 240;

		// Token: 0x0400012C RID: 300
		public const byte Remove = 239;

		// Token: 0x0400012D RID: 301
		public const byte PublishUserId = 239;

		// Token: 0x0400012E RID: 302
		public const byte Add = 238;

		// Token: 0x0400012F RID: 303
		public const byte Info = 218;

		// Token: 0x04000130 RID: 304
		public const byte ClientAuthenticationType = 217;

		// Token: 0x04000131 RID: 305
		public const byte ClientAuthenticationParams = 216;

		// Token: 0x04000132 RID: 306
		public const byte JoinMode = 215;

		// Token: 0x04000133 RID: 307
		public const byte ClientAuthenticationData = 214;

		// Token: 0x04000134 RID: 308
		public const byte MasterClientId = 203;

		// Token: 0x04000135 RID: 309
		public const byte FindFriendsRequestList = 1;

		// Token: 0x04000136 RID: 310
		public const byte FindFriendsOptions = 2;

		// Token: 0x04000137 RID: 311
		public const byte FindFriendsResponseOnlineList = 1;

		// Token: 0x04000138 RID: 312
		public const byte FindFriendsResponseRoomIdList = 2;

		// Token: 0x04000139 RID: 313
		public const byte LobbyName = 213;

		// Token: 0x0400013A RID: 314
		public const byte LobbyType = 212;

		// Token: 0x0400013B RID: 315
		public const byte LobbyStats = 211;

		// Token: 0x0400013C RID: 316
		public const byte Region = 210;

		// Token: 0x0400013D RID: 317
		public const byte UriPath = 209;

		// Token: 0x0400013E RID: 318
		public const byte WebRpcParameters = 208;

		// Token: 0x0400013F RID: 319
		public const byte WebRpcReturnCode = 207;

		// Token: 0x04000140 RID: 320
		public const byte WebRpcReturnMessage = 206;

		// Token: 0x04000141 RID: 321
		public const byte CacheSliceIndex = 205;

		// Token: 0x04000142 RID: 322
		public const byte Plugins = 204;

		// Token: 0x04000143 RID: 323
		public const byte NickName = 202;

		// Token: 0x04000144 RID: 324
		public const byte PluginName = 201;

		// Token: 0x04000145 RID: 325
		public const byte PluginVersion = 200;

		// Token: 0x04000146 RID: 326
		public const byte Cluster = 196;

		// Token: 0x04000147 RID: 327
		public const byte ExpectedProtocol = 195;

		// Token: 0x04000148 RID: 328
		public const byte CustomInitData = 194;

		// Token: 0x04000149 RID: 329
		public const byte EncryptionMode = 193;

		// Token: 0x0400014A RID: 330
		public const byte EncryptionData = 192;

		// Token: 0x0400014B RID: 331
		public const byte RoomOptionFlags = 191;

		// Token: 0x0400014C RID: 332
		public const byte Ticket = 190;

		// Token: 0x0400014D RID: 333
		public const byte MatchMakingGroupId = 189;

		// Token: 0x0400014E RID: 334
		public const byte AllowRepeats = 188;

		// Token: 0x0400014F RID: 335
		public const byte ReportQos = 187;
	}
}
