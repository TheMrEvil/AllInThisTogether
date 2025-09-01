using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;

namespace Photon.Realtime
{
	// Token: 0x0200003C RID: 60
	public class Room : RoomInfo
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00009CA6 File Offset: 0x00007EA6
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00009CAE File Offset: 0x00007EAE
		public LoadBalancingClient LoadBalancingClient
		{
			[CompilerGenerated]
			get
			{
				return this.<LoadBalancingClient>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LoadBalancingClient>k__BackingField = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00009CB7 File Offset: 0x00007EB7
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00009CBF File Offset: 0x00007EBF
		public new string Name
		{
			get
			{
				return this.name;
			}
			internal set
			{
				this.name = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00009CC8 File Offset: 0x00007EC8
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00009CD0 File Offset: 0x00007ED0
		public bool IsOffline
		{
			get
			{
				return this.isOffline;
			}
			private set
			{
				this.isOffline = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00009CD9 File Offset: 0x00007ED9
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00009CE1 File Offset: 0x00007EE1
		public new bool IsOpen
		{
			get
			{
				return this.isOpen;
			}
			set
			{
				if (value != this.isOpen && !this.isOffline)
				{
					this.LoadBalancingClient.OpSetPropertiesOfRoom(new Hashtable
					{
						{
							253,
							value
						}
					}, null, null);
				}
				this.isOpen = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00009D1F File Offset: 0x00007F1F
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00009D27 File Offset: 0x00007F27
		public new bool IsVisible
		{
			get
			{
				return this.isVisible;
			}
			set
			{
				if (value != this.isVisible && !this.isOffline)
				{
					this.LoadBalancingClient.OpSetPropertiesOfRoom(new Hashtable
					{
						{
							254,
							value
						}
					}, null, null);
				}
				this.isVisible = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00009D65 File Offset: 0x00007F65
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00009D70 File Offset: 0x00007F70
		public new int MaxPlayers
		{
			get
			{
				return this.maxPlayers;
			}
			set
			{
				if (value >= 0 && value != this.maxPlayers)
				{
					this.maxPlayers = value;
					byte b = (value <= 255) ? ((byte)value) : 0;
					if (!this.isOffline)
					{
						this.LoadBalancingClient.OpSetPropertiesOfRoom(new Hashtable
						{
							{
								byte.MaxValue,
								b
							},
							{
								243,
								this.maxPlayers
							}
						}, null, null);
					}
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00009DE1 File Offset: 0x00007FE1
		public new int PlayerCount
		{
			get
			{
				if (this.Players == null)
				{
					return 0;
				}
				return (int)((byte)this.Players.Count);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00009DF9 File Offset: 0x00007FF9
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00009E01 File Offset: 0x00008001
		public Dictionary<int, Player> Players
		{
			get
			{
				return this.players;
			}
			private set
			{
				this.players = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00009E0A File Offset: 0x0000800A
		public string[] ExpectedUsers
		{
			get
			{
				return this.expectedUsers;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00009E12 File Offset: 0x00008012
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00009E1A File Offset: 0x0000801A
		public int PlayerTtl
		{
			get
			{
				return this.playerTtl;
			}
			set
			{
				if (value != this.playerTtl && !this.isOffline)
				{
					this.LoadBalancingClient.OpSetPropertyOfRoom(246, value);
				}
				this.playerTtl = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00009E4B File Offset: 0x0000804B
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00009E53 File Offset: 0x00008053
		public int EmptyRoomTtl
		{
			get
			{
				return this.emptyRoomTtl;
			}
			set
			{
				if (value != this.emptyRoomTtl && !this.isOffline)
				{
					this.LoadBalancingClient.OpSetPropertyOfRoom(245, value);
				}
				this.emptyRoomTtl = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00009E84 File Offset: 0x00008084
		public int MasterClientId
		{
			get
			{
				return this.masterClientId;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00009E8C File Offset: 0x0000808C
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00009E94 File Offset: 0x00008094
		public string[] PropertiesListedInLobby
		{
			get
			{
				return this.propertiesListedInLobby;
			}
			private set
			{
				this.propertiesListedInLobby = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00009E9D File Offset: 0x0000809D
		public bool AutoCleanUp
		{
			get
			{
				return this.autoCleanUp;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00009EA5 File Offset: 0x000080A5
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00009EAD File Offset: 0x000080AD
		public bool BroadcastPropertiesChangeToAll
		{
			[CompilerGenerated]
			get
			{
				return this.<BroadcastPropertiesChangeToAll>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<BroadcastPropertiesChangeToAll>k__BackingField = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00009EB6 File Offset: 0x000080B6
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00009EBE File Offset: 0x000080BE
		public bool SuppressRoomEvents
		{
			[CompilerGenerated]
			get
			{
				return this.<SuppressRoomEvents>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SuppressRoomEvents>k__BackingField = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00009EC7 File Offset: 0x000080C7
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00009ECF File Offset: 0x000080CF
		public bool SuppressPlayerInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<SuppressPlayerInfo>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SuppressPlayerInfo>k__BackingField = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00009ED8 File Offset: 0x000080D8
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00009EE0 File Offset: 0x000080E0
		public bool PublishUserId
		{
			[CompilerGenerated]
			get
			{
				return this.<PublishUserId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<PublishUserId>k__BackingField = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00009EE9 File Offset: 0x000080E9
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00009EF1 File Offset: 0x000080F1
		public bool DeleteNullProperties
		{
			[CompilerGenerated]
			get
			{
				return this.<DeleteNullProperties>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<DeleteNullProperties>k__BackingField = value;
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00009EFC File Offset: 0x000080FC
		public Room(string roomName, RoomOptions options, bool isOffline = false) : base(roomName, (options != null) ? options.CustomRoomProperties : null)
		{
			if (options != null)
			{
				this.isVisible = options.IsVisible;
				this.isOpen = options.IsOpen;
				this.maxPlayers = options.MaxPlayers;
				this.propertiesListedInLobby = options.CustomRoomPropertiesForLobby;
			}
			this.isOffline = isOffline;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00009F64 File Offset: 0x00008164
		internal void InternalCacheRoomFlags(int roomFlags)
		{
			this.BroadcastPropertiesChangeToAll = ((roomFlags & 32) != 0);
			this.SuppressRoomEvents = ((roomFlags & 4) != 0);
			this.SuppressPlayerInfo = ((roomFlags & 64) != 0);
			this.PublishUserId = ((roomFlags & 8) != 0);
			this.DeleteNullProperties = ((roomFlags & 16) != 0);
			this.autoCleanUp = ((roomFlags & 2) != 0);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009FBC File Offset: 0x000081BC
		protected internal override void InternalCacheProperties(Hashtable propertiesToCache)
		{
			int masterClientId = this.masterClientId;
			base.InternalCacheProperties(propertiesToCache);
			if (masterClientId != 0 && this.masterClientId != masterClientId)
			{
				this.LoadBalancingClient.InRoomCallbackTargets.OnMasterClientSwitched(this.GetPlayer(this.masterClientId, false));
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000A000 File Offset: 0x00008200
		public virtual bool SetCustomProperties(Hashtable propertiesToSet, Hashtable expectedProperties = null, WebFlags webFlags = null)
		{
			if (propertiesToSet == null || propertiesToSet.Count == 0)
			{
				return false;
			}
			Hashtable hashtable = propertiesToSet.StripToStringKeys();
			if (!this.isOffline)
			{
				return this.LoadBalancingClient.OpSetPropertiesOfRoom(hashtable, expectedProperties, webFlags);
			}
			if (hashtable.Count == 0)
			{
				return false;
			}
			base.CustomProperties.Merge(hashtable);
			base.CustomProperties.StripKeysWithNullValues();
			this.LoadBalancingClient.InRoomCallbackTargets.OnRoomPropertiesUpdate(propertiesToSet);
			return true;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000A070 File Offset: 0x00008270
		public bool SetPropertiesListedInLobby(string[] lobbyProps)
		{
			if (this.isOffline)
			{
				return false;
			}
			Hashtable hashtable = new Hashtable();
			hashtable[250] = lobbyProps;
			return this.LoadBalancingClient.OpSetPropertiesOfRoom(hashtable, null, null);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000A0A7 File Offset: 0x000082A7
		protected internal virtual void RemovePlayer(Player player)
		{
			this.Players.Remove(player.ActorNumber);
			player.RoomReference = null;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000A0C2 File Offset: 0x000082C2
		protected internal virtual void RemovePlayer(int id)
		{
			this.RemovePlayer(this.GetPlayer(id, false));
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000A0D4 File Offset: 0x000082D4
		public bool SetMasterClient(Player masterClientPlayer)
		{
			if (this.isOffline)
			{
				return false;
			}
			Hashtable gameProperties = new Hashtable
			{
				{
					248,
					masterClientPlayer.ActorNumber
				}
			};
			Hashtable expectedProperties = new Hashtable
			{
				{
					248,
					this.MasterClientId
				}
			};
			return this.LoadBalancingClient.OpSetPropertiesOfRoom(gameProperties, expectedProperties, null);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000A131 File Offset: 0x00008331
		public virtual bool AddPlayer(Player player)
		{
			if (!this.Players.ContainsKey(player.ActorNumber))
			{
				this.StorePlayer(player);
				return true;
			}
			return false;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000A151 File Offset: 0x00008351
		public virtual Player StorePlayer(Player player)
		{
			this.Players[player.ActorNumber] = player;
			player.RoomReference = this;
			return player;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000A170 File Offset: 0x00008370
		public virtual Player GetPlayer(int id, bool findMaster = false)
		{
			int key = (findMaster && id == 0) ? this.MasterClientId : id;
			Player result = null;
			this.Players.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000A19F File Offset: 0x0000839F
		public bool ClearExpectedUsers()
		{
			return this.ExpectedUsers != null && this.ExpectedUsers.Length != 0 && this.SetExpectedUsers(new string[0], this.ExpectedUsers);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000A1C6 File Offset: 0x000083C6
		public bool SetExpectedUsers(string[] newExpectedUsers)
		{
			if (newExpectedUsers == null || newExpectedUsers.Length == 0)
			{
				this.LoadBalancingClient.DebugReturn(DebugLevel.ERROR, "newExpectedUsers array is null or empty, call Room.ClearExpectedUsers() instead if this is what you want.");
				return false;
			}
			return this.SetExpectedUsers(newExpectedUsers, this.ExpectedUsers);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000A1F0 File Offset: 0x000083F0
		private bool SetExpectedUsers(string[] newExpectedUsers, string[] oldExpectedUsers)
		{
			if (this.isOffline)
			{
				return false;
			}
			Hashtable hashtable = new Hashtable(1);
			hashtable.Add(247, newExpectedUsers);
			Hashtable hashtable2 = null;
			if (oldExpectedUsers != null)
			{
				hashtable2 = new Hashtable(1);
				hashtable2.Add(247, oldExpectedUsers);
			}
			return this.LoadBalancingClient.OpSetPropertiesOfRoom(hashtable, hashtable2, null);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000A240 File Offset: 0x00008440
		public override string ToString()
		{
			return string.Format("Room: '{0}' {1},{2} {4}/{3} players.", new object[]
			{
				this.name,
				this.isVisible ? "visible" : "hidden",
				this.isOpen ? "open" : "closed",
				this.maxPlayers,
				this.PlayerCount
			});
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000A2B0 File Offset: 0x000084B0
		public new string ToStringFull()
		{
			return string.Format("Room: '{0}' {1},{2} {4}/{3} players.\ncustomProps: {5}", new object[]
			{
				this.name,
				this.isVisible ? "visible" : "hidden",
				this.isOpen ? "open" : "closed",
				this.maxPlayers,
				this.PlayerCount,
				base.CustomProperties.ToStringFull()
			});
		}

		// Token: 0x040001EA RID: 490
		[CompilerGenerated]
		private LoadBalancingClient <LoadBalancingClient>k__BackingField;

		// Token: 0x040001EB RID: 491
		private bool isOffline;

		// Token: 0x040001EC RID: 492
		private Dictionary<int, Player> players = new Dictionary<int, Player>();

		// Token: 0x040001ED RID: 493
		[CompilerGenerated]
		private bool <BroadcastPropertiesChangeToAll>k__BackingField;

		// Token: 0x040001EE RID: 494
		[CompilerGenerated]
		private bool <SuppressRoomEvents>k__BackingField;

		// Token: 0x040001EF RID: 495
		[CompilerGenerated]
		private bool <SuppressPlayerInfo>k__BackingField;

		// Token: 0x040001F0 RID: 496
		[CompilerGenerated]
		private bool <PublishUserId>k__BackingField;

		// Token: 0x040001F1 RID: 497
		[CompilerGenerated]
		private bool <DeleteNullProperties>k__BackingField;
	}
}
