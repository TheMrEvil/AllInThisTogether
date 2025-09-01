using System;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;

namespace Photon.Realtime
{
	// Token: 0x0200003D RID: 61
	public class RoomInfo
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000A32E File Offset: 0x0000852E
		public Hashtable CustomProperties
		{
			get
			{
				return this.customProperties;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000A336 File Offset: 0x00008536
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000A33E File Offset: 0x0000853E
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x0000A346 File Offset: 0x00008546
		public int PlayerCount
		{
			[CompilerGenerated]
			get
			{
				return this.<PlayerCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<PlayerCount>k__BackingField = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000A34F File Offset: 0x0000854F
		public int MaxPlayers
		{
			get
			{
				return this.maxPlayers;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000A357 File Offset: 0x00008557
		public bool IsOpen
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000A35F File Offset: 0x0000855F
		public bool IsVisible
		{
			get
			{
				return this.isVisible;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000A367 File Offset: 0x00008567
		protected internal RoomInfo(string roomName, Hashtable roomProperties)
		{
			this.InternalCacheProperties(roomProperties);
			this.name = roomName;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000A3A0 File Offset: 0x000085A0
		public override bool Equals(object other)
		{
			RoomInfo roomInfo = other as RoomInfo;
			return roomInfo != null && this.Name.Equals(roomInfo.name);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000A3CA File Offset: 0x000085CA
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000A3D8 File Offset: 0x000085D8
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

		// Token: 0x060001DD RID: 477 RVA: 0x0000A448 File Offset: 0x00008648
		public string ToStringFull()
		{
			return string.Format("Room: '{0}' {1},{2} {4}/{3} players.\ncustomProps: {5}", new object[]
			{
				this.name,
				this.isVisible ? "visible" : "hidden",
				this.isOpen ? "open" : "closed",
				this.maxPlayers,
				this.PlayerCount,
				this.customProperties.ToStringFull()
			});
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000A4C8 File Offset: 0x000086C8
		protected internal virtual void InternalCacheProperties(Hashtable propertiesToCache)
		{
			if (propertiesToCache == null || propertiesToCache.Count == 0 || this.customProperties.Equals(propertiesToCache))
			{
				return;
			}
			if (propertiesToCache.ContainsKey(251))
			{
				this.RemovedFromList = (bool)propertiesToCache[251];
				if (this.RemovedFromList)
				{
					return;
				}
			}
			if (propertiesToCache.ContainsKey(243))
			{
				this.maxPlayers = Convert.ToInt32(propertiesToCache[243]);
			}
			else if (propertiesToCache.ContainsKey(255))
			{
				this.maxPlayers = Convert.ToInt32(propertiesToCache[byte.MaxValue]);
			}
			if (propertiesToCache.ContainsKey(253))
			{
				this.isOpen = (bool)propertiesToCache[253];
			}
			if (propertiesToCache.ContainsKey(254))
			{
				this.isVisible = (bool)propertiesToCache[254];
			}
			if (propertiesToCache.ContainsKey(252))
			{
				this.PlayerCount = Convert.ToInt32(propertiesToCache[252]);
			}
			if (propertiesToCache.ContainsKey(249))
			{
				this.autoCleanUp = (bool)propertiesToCache[249];
			}
			if (propertiesToCache.ContainsKey(248))
			{
				this.masterClientId = (int)propertiesToCache[248];
			}
			if (propertiesToCache.ContainsKey(250))
			{
				this.propertiesListedInLobby = (propertiesToCache[250] as string[]);
			}
			if (propertiesToCache.ContainsKey(247))
			{
				this.expectedUsers = (string[])propertiesToCache[247];
			}
			if (propertiesToCache.ContainsKey(245))
			{
				this.emptyRoomTtl = (int)propertiesToCache[245];
			}
			if (propertiesToCache.ContainsKey(246))
			{
				this.playerTtl = (int)propertiesToCache[246];
			}
			this.customProperties.MergeStringKeys(propertiesToCache);
			this.customProperties.StripKeysWithNullValues();
		}

		// Token: 0x040001F2 RID: 498
		public bool RemovedFromList;

		// Token: 0x040001F3 RID: 499
		private Hashtable customProperties = new Hashtable();

		// Token: 0x040001F4 RID: 500
		protected int maxPlayers;

		// Token: 0x040001F5 RID: 501
		protected int emptyRoomTtl;

		// Token: 0x040001F6 RID: 502
		protected int playerTtl;

		// Token: 0x040001F7 RID: 503
		protected string[] expectedUsers;

		// Token: 0x040001F8 RID: 504
		protected bool isOpen = true;

		// Token: 0x040001F9 RID: 505
		protected bool isVisible = true;

		// Token: 0x040001FA RID: 506
		protected bool autoCleanUp = true;

		// Token: 0x040001FB RID: 507
		protected string name;

		// Token: 0x040001FC RID: 508
		public int masterClientId;

		// Token: 0x040001FD RID: 509
		protected string[] propertiesListedInLobby;

		// Token: 0x040001FE RID: 510
		[CompilerGenerated]
		private int <PlayerCount>k__BackingField;
	}
}
