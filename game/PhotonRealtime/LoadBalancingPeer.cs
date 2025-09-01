using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;

namespace Photon.Realtime
{
	// Token: 0x0200001D RID: 29
	public class LoadBalancingPeer : PhotonPeer
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00007098 File Offset: 0x00005298
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x0000709F File Offset: 0x0000529F
		[Obsolete("Use RegionHandler.PingImplementation directly.")]
		protected internal static Type PingImplementation
		{
			get
			{
				return RegionHandler.PingImplementation;
			}
			set
			{
				RegionHandler.PingImplementation = value;
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000070A8 File Offset: 0x000052A8
		public LoadBalancingPeer(ConnectionProtocol protocolType) : base(protocolType)
		{
			this.ConfigUnitySockets();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000710C File Offset: 0x0000530C
		public LoadBalancingPeer(IPhotonPeerListener listener, ConnectionProtocol protocolType) : this(protocolType)
		{
			base.Listener = listener;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000711C File Offset: 0x0000531C
		[Conditional("SUPPORTED_UNITY")]
		private void ConfigUnitySockets()
		{
			Type type = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, PhotonWebSocket", false);
			if (type == null)
			{
				type = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp-firstpass", false);
			}
			if (type == null)
			{
				type = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp", false);
			}
			if (type != null)
			{
				this.SocketImplementationConfig[ConnectionProtocol.WebSocket] = type;
				this.SocketImplementationConfig[ConnectionProtocol.WebSocketSecure] = type;
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00007184 File Offset: 0x00005384
		public virtual bool OpGetRegions(string appId)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>(1);
			dictionary[224] = appId;
			return this.SendOperation(220, dictionary, new SendOptions
			{
				Reliability = true,
				Encrypt = true
			});
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000071CC File Offset: 0x000053CC
		public virtual bool OpJoinLobby(TypedLobby lobby = null)
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "OpJoinLobby()");
			}
			Dictionary<byte, object> dictionary = null;
			if (lobby != null && !lobby.IsDefault)
			{
				dictionary = new Dictionary<byte, object>();
				dictionary[213] = lobby.Name;
				dictionary[212] = (byte)lobby.Type;
			}
			return this.SendOperation(229, dictionary, SendOptions.SendReliable);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000723E File Offset: 0x0000543E
		public virtual bool OpLeaveLobby()
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "OpLeaveLobby()");
			}
			return this.SendOperation(228, null, SendOptions.SendReliable);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000726C File Offset: 0x0000546C
		private void RoomOptionsToOpParameters(Dictionary<byte, object> op, RoomOptions roomOptions, bool usePropertiesKey = false)
		{
			if (roomOptions == null)
			{
				roomOptions = new RoomOptions();
			}
			Hashtable hashtable = new Hashtable();
			hashtable[253] = roomOptions.IsOpen;
			hashtable[254] = roomOptions.IsVisible;
			hashtable[250] = ((roomOptions.CustomRoomPropertiesForLobby == null) ? new string[0] : roomOptions.CustomRoomPropertiesForLobby);
			hashtable.MergeStringKeys(roomOptions.CustomRoomProperties);
			if (roomOptions.MaxPlayers > 0)
			{
				byte b = (roomOptions.MaxPlayers <= 255) ? ((byte)roomOptions.MaxPlayers) : 0;
				hashtable[byte.MaxValue] = b;
				hashtable[243] = roomOptions.MaxPlayers;
			}
			if (!usePropertiesKey)
			{
				op[248] = hashtable;
			}
			else
			{
				op[251] = hashtable;
			}
			int num = 0;
			if (roomOptions.CleanupCacheOnLeave)
			{
				op[241] = true;
				num |= 2;
			}
			else
			{
				op[241] = false;
				hashtable[249] = false;
			}
			num |= 1;
			op[232] = true;
			if (roomOptions.PlayerTtl > 0 || roomOptions.PlayerTtl == -1)
			{
				op[235] = roomOptions.PlayerTtl;
			}
			if (roomOptions.EmptyRoomTtl > 0)
			{
				op[236] = roomOptions.EmptyRoomTtl;
			}
			if (roomOptions.SuppressRoomEvents)
			{
				num |= 4;
				op[237] = true;
			}
			if (roomOptions.SuppressPlayerInfo)
			{
				num |= 64;
			}
			if (roomOptions.Plugins != null)
			{
				op[204] = roomOptions.Plugins;
			}
			if (roomOptions.PublishUserId)
			{
				num |= 8;
				op[239] = true;
			}
			if (roomOptions.DeleteNullProperties)
			{
				num |= 16;
			}
			if (roomOptions.BroadcastPropsChangeToAll)
			{
				num |= 32;
			}
			op[191] = num;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00007474 File Offset: 0x00005674
		public virtual bool OpCreateRoom(EnterRoomParams opParams)
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "OpCreateRoom()");
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			SendOptions sendOptions = new SendOptions
			{
				Reliability = true
			};
			if (!string.IsNullOrEmpty(opParams.RoomName))
			{
				dictionary[byte.MaxValue] = opParams.RoomName;
			}
			if (opParams.Lobby != null && !opParams.Lobby.IsDefault)
			{
				dictionary[213] = opParams.Lobby.Name;
				dictionary[212] = (byte)opParams.Lobby.Type;
			}
			if (opParams.ExpectedUsers != null && opParams.ExpectedUsers.Length != 0)
			{
				dictionary[238] = opParams.ExpectedUsers;
				sendOptions.Encrypt = true;
			}
			if (opParams.Ticket != null)
			{
				dictionary[190] = opParams.Ticket;
			}
			if (opParams.OnGameServer)
			{
				if (opParams.PlayerProperties != null && opParams.PlayerProperties.Count > 0)
				{
					dictionary[249] = opParams.PlayerProperties;
				}
				dictionary[250] = true;
				this.RoomOptionsToOpParameters(dictionary, opParams.RoomOptions, false);
			}
			return this.SendOperation(227, dictionary, sendOptions);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000075B8 File Offset: 0x000057B8
		public virtual bool OpJoinRoom(EnterRoomParams opParams)
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "OpJoinRoom()");
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			SendOptions sendOptions = new SendOptions
			{
				Reliability = true
			};
			if (!string.IsNullOrEmpty(opParams.RoomName))
			{
				dictionary[byte.MaxValue] = opParams.RoomName;
			}
			if (opParams.JoinMode == JoinMode.CreateIfNotExists)
			{
				dictionary[215] = 1;
				if (opParams.Lobby != null && !opParams.Lobby.IsDefault)
				{
					dictionary[213] = opParams.Lobby.Name;
					dictionary[212] = (byte)opParams.Lobby.Type;
				}
			}
			else if (opParams.JoinMode == JoinMode.RejoinOnly)
			{
				dictionary[215] = 3;
			}
			if (opParams.ExpectedUsers != null && opParams.ExpectedUsers.Length != 0)
			{
				dictionary[238] = opParams.ExpectedUsers;
				sendOptions.Encrypt = true;
			}
			if (opParams.Ticket != null)
			{
				dictionary[190] = opParams.Ticket;
			}
			if (opParams.OnGameServer)
			{
				if (opParams.PlayerProperties != null && opParams.PlayerProperties.Count > 0)
				{
					dictionary[249] = opParams.PlayerProperties;
				}
				dictionary[250] = true;
				this.RoomOptionsToOpParameters(dictionary, opParams.RoomOptions, false);
			}
			return this.SendOperation(226, dictionary, sendOptions);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00007730 File Offset: 0x00005930
		public virtual bool OpJoinRandomRoom(OpJoinRandomRoomParams opJoinRandomRoomParams)
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "OpJoinRandomRoom()");
			}
			Hashtable hashtable = new Hashtable();
			hashtable.MergeStringKeys(opJoinRandomRoomParams.ExpectedCustomRoomProperties);
			if (opJoinRandomRoomParams.ExpectedMaxPlayers > 0)
			{
				byte b = (opJoinRandomRoomParams.ExpectedMaxPlayers <= 255) ? ((byte)opJoinRandomRoomParams.ExpectedMaxPlayers) : 0;
				hashtable[byte.MaxValue] = b;
				if (opJoinRandomRoomParams.ExpectedMaxPlayers > 255)
				{
					hashtable[243] = opJoinRandomRoomParams.ExpectedMaxPlayers;
				}
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			SendOptions sendOptions = new SendOptions
			{
				Reliability = true
			};
			if (hashtable.Count > 0)
			{
				dictionary[248] = hashtable;
			}
			if (opJoinRandomRoomParams.MatchingType != MatchmakingMode.FillRoom)
			{
				dictionary[223] = (byte)opJoinRandomRoomParams.MatchingType;
			}
			if (opJoinRandomRoomParams.TypedLobby != null && !opJoinRandomRoomParams.TypedLobby.IsDefault)
			{
				dictionary[213] = opJoinRandomRoomParams.TypedLobby.Name;
				dictionary[212] = (byte)opJoinRandomRoomParams.TypedLobby.Type;
			}
			if (!string.IsNullOrEmpty(opJoinRandomRoomParams.SqlLobbyFilter))
			{
				dictionary[245] = opJoinRandomRoomParams.SqlLobbyFilter;
			}
			if (opJoinRandomRoomParams.ExpectedUsers != null && opJoinRandomRoomParams.ExpectedUsers.Length != 0)
			{
				dictionary[238] = opJoinRandomRoomParams.ExpectedUsers;
				sendOptions.Encrypt = true;
			}
			if (opJoinRandomRoomParams.Ticket != null)
			{
				dictionary[190] = opJoinRandomRoomParams.Ticket;
			}
			dictionary[188] = true;
			return this.SendOperation(225, dictionary, sendOptions);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000078D0 File Offset: 0x00005AD0
		public virtual bool OpJoinRandomOrCreateRoom(OpJoinRandomRoomParams opJoinRandomRoomParams, EnterRoomParams createRoomParams)
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "OpJoinRandomOrCreateRoom()");
			}
			Hashtable hashtable = new Hashtable();
			hashtable.MergeStringKeys(opJoinRandomRoomParams.ExpectedCustomRoomProperties);
			if (opJoinRandomRoomParams.ExpectedMaxPlayers > 0)
			{
				byte b = (opJoinRandomRoomParams.ExpectedMaxPlayers <= 255) ? ((byte)opJoinRandomRoomParams.ExpectedMaxPlayers) : 0;
				hashtable[byte.MaxValue] = b;
				if (opJoinRandomRoomParams.ExpectedMaxPlayers > 255)
				{
					hashtable[243] = opJoinRandomRoomParams.ExpectedMaxPlayers;
				}
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			SendOptions sendOptions = new SendOptions
			{
				Reliability = true
			};
			if (hashtable.Count > 0)
			{
				dictionary[248] = hashtable;
			}
			if (opJoinRandomRoomParams.MatchingType != MatchmakingMode.FillRoom)
			{
				dictionary[223] = (byte)opJoinRandomRoomParams.MatchingType;
			}
			if (opJoinRandomRoomParams.TypedLobby != null && !opJoinRandomRoomParams.TypedLobby.IsDefault)
			{
				dictionary[213] = opJoinRandomRoomParams.TypedLobby.Name;
				dictionary[212] = (byte)opJoinRandomRoomParams.TypedLobby.Type;
			}
			if (!string.IsNullOrEmpty(opJoinRandomRoomParams.SqlLobbyFilter))
			{
				dictionary[245] = opJoinRandomRoomParams.SqlLobbyFilter;
			}
			if (opJoinRandomRoomParams.ExpectedUsers != null && opJoinRandomRoomParams.ExpectedUsers.Length != 0)
			{
				dictionary[238] = opJoinRandomRoomParams.ExpectedUsers;
				sendOptions.Encrypt = true;
			}
			if (opJoinRandomRoomParams.Ticket != null)
			{
				dictionary[190] = opJoinRandomRoomParams.Ticket;
			}
			dictionary[215] = 1;
			dictionary[188] = true;
			if (createRoomParams != null && !string.IsNullOrEmpty(createRoomParams.RoomName))
			{
				dictionary[byte.MaxValue] = createRoomParams.RoomName;
			}
			return this.SendOperation(225, dictionary, sendOptions);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00007AA4 File Offset: 0x00005CA4
		public virtual bool OpLeaveRoom(bool becomeInactive, bool sendAuthCookie = false)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			if (becomeInactive)
			{
				dictionary[233] = true;
			}
			if (sendAuthCookie)
			{
				dictionary[234] = 2;
			}
			return this.SendOperation(254, dictionary, SendOptions.SendReliable);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00007AF0 File Offset: 0x00005CF0
		public virtual bool OpGetGameList(TypedLobby lobby, string queryData)
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "OpGetGameList()");
			}
			if (lobby == null)
			{
				if (this.DebugOut >= DebugLevel.INFO)
				{
					base.Listener.DebugReturn(DebugLevel.INFO, "OpGetGameList not sent. Lobby cannot be null.");
				}
				return false;
			}
			if (lobby.Type != LobbyType.SqlLobby)
			{
				if (this.DebugOut >= DebugLevel.INFO)
				{
					base.Listener.DebugReturn(DebugLevel.INFO, "OpGetGameList not sent. LobbyType must be SqlLobby.");
				}
				return false;
			}
			if (lobby.IsDefault)
			{
				if (this.DebugOut >= DebugLevel.INFO)
				{
					base.Listener.DebugReturn(DebugLevel.INFO, "OpGetGameList not sent. LobbyName must be not null and not empty.");
				}
				return false;
			}
			if (string.IsNullOrEmpty(queryData))
			{
				if (this.DebugOut >= DebugLevel.INFO)
				{
					base.Listener.DebugReturn(DebugLevel.INFO, "OpGetGameList not sent. queryData must be not null and not empty.");
				}
				return false;
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary[213] = lobby.Name;
			dictionary[212] = (byte)lobby.Type;
			dictionary[245] = queryData;
			return this.SendOperation(217, dictionary, SendOptions.SendReliable);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00007BF0 File Offset: 0x00005DF0
		public virtual bool OpFindFriends(string[] friendsToFind, FindFriendsOptions options = null)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			if (friendsToFind != null && friendsToFind.Length != 0)
			{
				dictionary[1] = friendsToFind;
			}
			if (options != null)
			{
				dictionary[2] = options.ToIntFlags();
			}
			SendOptions sendOptions = new SendOptions
			{
				Reliability = true,
				Encrypt = true
			};
			return this.SendOperation(222, dictionary, sendOptions);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00007C4E File Offset: 0x00005E4E
		public bool OpSetCustomPropertiesOfActor(int actorNr, Hashtable actorProperties)
		{
			return this.OpSetPropertiesOfActor(actorNr, actorProperties.StripToStringKeys(), null, null);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00007C60 File Offset: 0x00005E60
		protected internal bool OpSetPropertiesOfActor(int actorNr, Hashtable actorProperties, Hashtable expectedProperties = null, WebFlags webflags = null)
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "OpSetPropertiesOfActor()");
			}
			if (actorNr <= 0 || actorProperties == null || actorProperties.Count == 0)
			{
				if (this.DebugOut >= DebugLevel.INFO)
				{
					base.Listener.DebugReturn(DebugLevel.INFO, "OpSetPropertiesOfActor not sent. ActorNr must be > 0 and actorProperties must be not null nor empty.");
				}
				return false;
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary.Add(251, actorProperties);
			dictionary.Add(254, actorNr);
			dictionary.Add(250, true);
			if (expectedProperties != null && expectedProperties.Count != 0)
			{
				dictionary.Add(231, expectedProperties);
			}
			if (webflags != null && webflags.HttpForward)
			{
				dictionary[234] = webflags.WebhookFlags;
			}
			return this.SendOperation(252, dictionary, SendOptions.SendReliable);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00007D34 File Offset: 0x00005F34
		protected bool OpSetPropertyOfRoom(byte propCode, object value)
		{
			Hashtable hashtable = new Hashtable();
			hashtable[propCode] = value;
			return this.OpSetPropertiesOfRoom(hashtable, null, null);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00007D58 File Offset: 0x00005F58
		public bool OpSetCustomPropertiesOfRoom(Hashtable gameProperties)
		{
			return this.OpSetPropertiesOfRoom(gameProperties.StripToStringKeys(), null, null);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00007D68 File Offset: 0x00005F68
		protected internal bool OpSetPropertiesOfRoom(Hashtable gameProperties, Hashtable expectedProperties = null, WebFlags webflags = null)
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "OpSetPropertiesOfRoom()");
			}
			if (gameProperties == null || gameProperties.Count == 0)
			{
				if (this.DebugOut >= DebugLevel.INFO)
				{
					base.Listener.DebugReturn(DebugLevel.INFO, "OpSetPropertiesOfRoom not sent. gameProperties must be not null nor empty.");
				}
				return false;
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary.Add(251, gameProperties);
			dictionary.Add(250, true);
			if (expectedProperties != null && expectedProperties.Count != 0)
			{
				dictionary.Add(231, expectedProperties);
			}
			if (webflags != null && webflags.HttpForward)
			{
				dictionary[234] = webflags.WebhookFlags;
			}
			return this.SendOperation(252, dictionary, SendOptions.SendReliable);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00007E24 File Offset: 0x00006024
		public virtual bool OpAuthenticate(string appId, string appVersion, AuthenticationValues authValues, string regionCode, bool getLobbyStatistics)
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "OpAuthenticate()");
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			if (getLobbyStatistics)
			{
				dictionary[211] = true;
			}
			if (authValues != null && authValues.Token != null)
			{
				dictionary[221] = authValues.Token;
				return this.SendOperation(230, dictionary, SendOptions.SendReliable);
			}
			dictionary[220] = appVersion;
			dictionary[224] = appId;
			if (!string.IsNullOrEmpty(regionCode))
			{
				dictionary[210] = regionCode;
			}
			if (authValues != null)
			{
				if (!string.IsNullOrEmpty(authValues.UserId))
				{
					dictionary[225] = authValues.UserId;
				}
				if (authValues.AuthType != CustomAuthenticationType.None)
				{
					dictionary[217] = (byte)authValues.AuthType;
					if (!string.IsNullOrEmpty(authValues.AuthGetParameters))
					{
						dictionary[216] = authValues.AuthGetParameters;
					}
					if (authValues.AuthPostData != null)
					{
						dictionary[214] = authValues.AuthPostData;
					}
				}
			}
			return this.SendOperation(230, dictionary, new SendOptions
			{
				Reliability = true,
				Encrypt = true
			});
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00007F64 File Offset: 0x00006164
		public virtual bool OpAuthenticateOnce(string appId, string appVersion, AuthenticationValues authValues, string regionCode, EncryptionMode encryptionMode, ConnectionProtocol expectedProtocol)
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, string.Concat(new string[]
				{
					"OpAuthenticateOnce(): authValues = ",
					(authValues != null) ? authValues.ToString() : null,
					", region = ",
					regionCode,
					", encryption = ",
					encryptionMode.ToString()
				}));
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			if (authValues != null && authValues.Token != null)
			{
				dictionary[221] = authValues.Token;
				return this.SendOperation(231, dictionary, SendOptions.SendReliable);
			}
			if (encryptionMode == EncryptionMode.DatagramEncryptionGCM && expectedProtocol != ConnectionProtocol.Udp)
			{
				throw new NotSupportedException("Expected protocol set to UDP, due to encryption mode DatagramEncryptionGCM.");
			}
			dictionary[195] = (byte)expectedProtocol;
			dictionary[193] = (byte)encryptionMode;
			dictionary[220] = appVersion;
			dictionary[224] = appId;
			if (!string.IsNullOrEmpty(regionCode))
			{
				dictionary[210] = regionCode;
			}
			if (authValues != null)
			{
				if (!string.IsNullOrEmpty(authValues.UserId))
				{
					dictionary[225] = authValues.UserId;
				}
				if (authValues.AuthType != CustomAuthenticationType.None)
				{
					dictionary[217] = (byte)authValues.AuthType;
					if (authValues.Token != null)
					{
						dictionary[221] = authValues.Token;
					}
					else
					{
						if (!string.IsNullOrEmpty(authValues.AuthGetParameters))
						{
							dictionary[216] = authValues.AuthGetParameters;
						}
						if (authValues.AuthPostData != null)
						{
							dictionary[214] = authValues.AuthPostData;
						}
					}
				}
			}
			return this.SendOperation(231, dictionary, new SendOptions
			{
				Reliability = true,
				Encrypt = true
			});
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00008128 File Offset: 0x00006328
		public virtual bool OpChangeGroups(byte[] groupsToRemove, byte[] groupsToAdd)
		{
			if (this.DebugOut >= DebugLevel.ALL)
			{
				base.Listener.DebugReturn(DebugLevel.ALL, "OpChangeGroups()");
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			if (groupsToRemove != null)
			{
				dictionary[239] = groupsToRemove;
			}
			if (groupsToAdd != null)
			{
				dictionary[238] = groupsToAdd;
			}
			return this.SendOperation(248, dictionary, SendOptions.SendReliable);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00008184 File Offset: 0x00006384
		public virtual bool OpRaiseEvent(byte eventCode, object customEventContent, RaiseEventOptions raiseEventOptions, SendOptions sendOptions)
		{
			ParameterDictionary parameterDictionary = this.paramDictionaryPool.Acquire();
			bool result;
			try
			{
				if (raiseEventOptions != null)
				{
					if (raiseEventOptions.CachingOption != EventCaching.DoNotCache)
					{
						parameterDictionary.Add(247, (byte)raiseEventOptions.CachingOption);
					}
					switch (raiseEventOptions.CachingOption)
					{
					case EventCaching.RemoveFromRoomCache:
						if (raiseEventOptions.TargetActors != null)
						{
							parameterDictionary.Add(252, raiseEventOptions.TargetActors);
							goto IL_114;
						}
						goto IL_114;
					case EventCaching.RemoveFromRoomCacheForActorsLeft:
					case EventCaching.SliceIncreaseIndex:
						return this.SendOperation(253, parameterDictionary, sendOptions);
					case EventCaching.SliceSetIndex:
					case EventCaching.SlicePurgeIndex:
					case EventCaching.SlicePurgeUpToIndex:
						return this.SendOperation(253, parameterDictionary, sendOptions);
					}
					if (raiseEventOptions.TargetActors != null)
					{
						parameterDictionary.Add(252, raiseEventOptions.TargetActors);
					}
					else if (raiseEventOptions.InterestGroup != 0)
					{
						parameterDictionary.Add(240, raiseEventOptions.InterestGroup);
					}
					else if (raiseEventOptions.Receivers != ReceiverGroup.Others)
					{
						parameterDictionary.Add(246, (byte)raiseEventOptions.Receivers);
					}
					if (raiseEventOptions.Flags.HttpForward)
					{
						parameterDictionary.Add(234, raiseEventOptions.Flags.WebhookFlags);
					}
				}
				IL_114:
				parameterDictionary.Add(244, eventCode);
				if (customEventContent != null)
				{
					parameterDictionary.Add(245, customEventContent);
				}
				result = this.SendOperation(253, parameterDictionary, sendOptions);
			}
			finally
			{
				this.paramDictionaryPool.Release(parameterDictionary);
			}
			return result;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000082FC File Offset: 0x000064FC
		public virtual bool OpSettings(bool receiveLobbyStats)
		{
			if (this.DebugOut >= DebugLevel.ALL)
			{
				base.Listener.DebugReturn(DebugLevel.ALL, "OpSettings()");
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			if (receiveLobbyStats)
			{
				dictionary[0] = receiveLobbyStats;
			}
			return dictionary.Count == 0 || this.SendOperation(218, dictionary, SendOptions.SendReliable);
		}

		// Token: 0x040000B1 RID: 177
		private readonly Pool<ParameterDictionary> paramDictionaryPool = new Pool<ParameterDictionary>(() => new ParameterDictionary(), delegate(ParameterDictionary x)
		{
			x.Clear();
		}, 1);

		// Token: 0x02000045 RID: 69
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000226 RID: 550 RVA: 0x0000B666 File Offset: 0x00009866
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000227 RID: 551 RVA: 0x0000B672 File Offset: 0x00009872
			public <>c()
			{
			}

			// Token: 0x06000228 RID: 552 RVA: 0x0000B67A File Offset: 0x0000987A
			internal ParameterDictionary <.ctor>b__4_0()
			{
				return new ParameterDictionary();
			}

			// Token: 0x06000229 RID: 553 RVA: 0x0000B681 File Offset: 0x00009881
			internal void <.ctor>b__4_1(ParameterDictionary x)
			{
				x.Clear();
			}

			// Token: 0x04000221 RID: 545
			public static readonly LoadBalancingPeer.<>c <>9 = new LoadBalancingPeer.<>c();

			// Token: 0x04000222 RID: 546
			public static Func<ParameterDictionary> <>9__4_0;

			// Token: 0x04000223 RID: 547
			public static Action<ParameterDictionary> <>9__4_1;
		}
	}
}
