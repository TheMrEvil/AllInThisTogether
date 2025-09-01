using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;

namespace Photon.Realtime
{
	// Token: 0x02000037 RID: 55
	public class Player
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000089A8 File Offset: 0x00006BA8
		// (set) Token: 0x06000152 RID: 338 RVA: 0x000089B0 File Offset: 0x00006BB0
		protected internal Room RoomReference
		{
			[CompilerGenerated]
			get
			{
				return this.<RoomReference>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RoomReference>k__BackingField = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000089B9 File Offset: 0x00006BB9
		public int ActorNumber
		{
			get
			{
				return this.actorNumber;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000089C1 File Offset: 0x00006BC1
		// (set) Token: 0x06000155 RID: 341 RVA: 0x000089C9 File Offset: 0x00006BC9
		public bool HasRejoined
		{
			[CompilerGenerated]
			get
			{
				return this.<HasRejoined>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<HasRejoined>k__BackingField = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000089D2 File Offset: 0x00006BD2
		// (set) Token: 0x06000157 RID: 343 RVA: 0x000089DA File Offset: 0x00006BDA
		public string NickName
		{
			get
			{
				return this.nickName;
			}
			set
			{
				if (!string.IsNullOrEmpty(this.nickName) && this.nickName.Equals(value))
				{
					return;
				}
				this.nickName = value;
				if (this.IsLocal)
				{
					this.SetPlayerNameProperty();
				}
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00008A0E File Offset: 0x00006C0E
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00008A16 File Offset: 0x00006C16
		public string UserId
		{
			[CompilerGenerated]
			get
			{
				return this.<UserId>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<UserId>k__BackingField = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00008A1F File Offset: 0x00006C1F
		public bool IsMasterClient
		{
			get
			{
				return this.RoomReference != null && this.ActorNumber == this.RoomReference.MasterClientId;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00008A3E File Offset: 0x00006C3E
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00008A46 File Offset: 0x00006C46
		public bool IsInactive
		{
			[CompilerGenerated]
			get
			{
				return this.<IsInactive>k__BackingField;
			}
			[CompilerGenerated]
			protected internal set
			{
				this.<IsInactive>k__BackingField = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00008A4F File Offset: 0x00006C4F
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00008A57 File Offset: 0x00006C57
		public Hashtable CustomProperties
		{
			[CompilerGenerated]
			get
			{
				return this.<CustomProperties>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CustomProperties>k__BackingField = value;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00008A60 File Offset: 0x00006C60
		protected internal Player(string nickName, int actorNumber, bool isLocal) : this(nickName, actorNumber, isLocal, null)
		{
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008A6C File Offset: 0x00006C6C
		protected internal Player(string nickName, int actorNumber, bool isLocal, Hashtable playerProperties)
		{
			this.IsLocal = isLocal;
			this.actorNumber = actorNumber;
			this.NickName = nickName;
			this.CustomProperties = new Hashtable();
			this.InternalCacheProperties(playerProperties);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008AB9 File Offset: 0x00006CB9
		public Player Get(int id)
		{
			if (this.RoomReference == null)
			{
				return null;
			}
			return this.RoomReference.GetPlayer(id, false);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008AD2 File Offset: 0x00006CD2
		public Player GetNext()
		{
			return this.GetNextFor(this.ActorNumber);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00008AE0 File Offset: 0x00006CE0
		public Player GetNextFor(Player currentPlayer)
		{
			if (currentPlayer == null)
			{
				return null;
			}
			return this.GetNextFor(currentPlayer.ActorNumber);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00008AF4 File Offset: 0x00006CF4
		public Player GetNextFor(int currentPlayerId)
		{
			if (this.RoomReference == null || this.RoomReference.Players == null || this.RoomReference.Players.Count < 2)
			{
				return null;
			}
			Dictionary<int, Player> players = this.RoomReference.Players;
			int num = int.MaxValue;
			int num2 = currentPlayerId;
			foreach (int num3 in players.Keys)
			{
				if (num3 < num2)
				{
					num2 = num3;
				}
				else if (num3 > currentPlayerId && num3 < num)
				{
					num = num3;
				}
			}
			if (num == 2147483647)
			{
				return players[num2];
			}
			return players[num];
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008BB0 File Offset: 0x00006DB0
		protected internal virtual void InternalCacheProperties(Hashtable properties)
		{
			if (properties == null || properties.Count == 0 || this.CustomProperties.Equals(properties))
			{
				return;
			}
			if (!this.IsLocal && properties.ContainsKey(255))
			{
				string text = (string)properties[byte.MaxValue];
				this.NickName = text;
			}
			if (properties.ContainsKey(253))
			{
				this.UserId = (string)properties[253];
			}
			if (properties.ContainsKey(254))
			{
				this.IsInactive = (bool)properties[254];
			}
			this.CustomProperties.MergeStringKeys(properties);
			this.CustomProperties.StripKeysWithNullValues();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008C61 File Offset: 0x00006E61
		public override string ToString()
		{
			return string.Format("#{0:00} '{1}'", this.ActorNumber, this.NickName);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008C80 File Offset: 0x00006E80
		public string ToStringFull()
		{
			return string.Format("#{0:00} '{1}'{2} {3}", new object[]
			{
				this.ActorNumber,
				this.NickName,
				this.IsInactive ? " (inactive)" : "",
				this.CustomProperties.ToStringFull()
			});
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00008CDC File Offset: 0x00006EDC
		public override bool Equals(object p)
		{
			Player player = p as Player;
			return player != null && this.GetHashCode() == player.GetHashCode();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008D03 File Offset: 0x00006F03
		public override int GetHashCode()
		{
			return this.ActorNumber;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00008D0B File Offset: 0x00006F0B
		protected internal void ChangeLocalID(int newID)
		{
			if (!this.IsLocal)
			{
				return;
			}
			this.actorNumber = newID;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00008D20 File Offset: 0x00006F20
		public bool SetCustomProperties(Hashtable propertiesToSet, Hashtable expectedValues = null, WebFlags webFlags = null)
		{
			if (propertiesToSet == null || propertiesToSet.Count == 0)
			{
				return false;
			}
			Hashtable hashtable = propertiesToSet.StripToStringKeys();
			if (this.RoomReference == null)
			{
				if (this.IsLocal)
				{
					if (hashtable.Count == 0)
					{
						return false;
					}
					if (expectedValues == null && webFlags == null)
					{
						this.CustomProperties.Merge(hashtable);
						this.CustomProperties.StripKeysWithNullValues();
						return true;
					}
				}
				return false;
			}
			if (!this.RoomReference.IsOffline)
			{
				Hashtable expectedProperties = expectedValues.StripToStringKeys();
				return this.RoomReference.LoadBalancingClient.OpSetPropertiesOfActor(this.actorNumber, hashtable, expectedProperties, webFlags);
			}
			if (hashtable.Count == 0)
			{
				return false;
			}
			this.CustomProperties.Merge(hashtable);
			this.CustomProperties.StripKeysWithNullValues();
			this.RoomReference.LoadBalancingClient.InRoomCallbackTargets.OnPlayerPropertiesUpdate(this, hashtable);
			return true;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008DE4 File Offset: 0x00006FE4
		internal bool UpdateNickNameOnJoined()
		{
			if (this.RoomReference == null || this.RoomReference.CustomProperties == null || !this.IsLocal)
			{
				return false;
			}
			string b = this.RoomReference.CustomProperties.ContainsKey(byte.MaxValue) ? (this.RoomReference.CustomProperties[byte.MaxValue] as string) : string.Empty;
			return string.Equals(this.NickName, b) || this.SetPlayerNameProperty();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00008E60 File Offset: 0x00007060
		private bool SetPlayerNameProperty()
		{
			if (this.RoomReference != null && !this.RoomReference.IsOffline)
			{
				Hashtable hashtable = new Hashtable();
				hashtable[byte.MaxValue] = this.nickName;
				return this.RoomReference.LoadBalancingClient.OpSetPropertiesOfActor(this.ActorNumber, hashtable, null, null);
			}
			return false;
		}

		// Token: 0x040001C1 RID: 449
		[CompilerGenerated]
		private Room <RoomReference>k__BackingField;

		// Token: 0x040001C2 RID: 450
		private int actorNumber = -1;

		// Token: 0x040001C3 RID: 451
		public readonly bool IsLocal;

		// Token: 0x040001C4 RID: 452
		[CompilerGenerated]
		private bool <HasRejoined>k__BackingField;

		// Token: 0x040001C5 RID: 453
		private string nickName = string.Empty;

		// Token: 0x040001C6 RID: 454
		[CompilerGenerated]
		private string <UserId>k__BackingField;

		// Token: 0x040001C7 RID: 455
		[CompilerGenerated]
		private bool <IsInactive>k__BackingField;

		// Token: 0x040001C8 RID: 456
		[CompilerGenerated]
		private Hashtable <CustomProperties>k__BackingField;

		// Token: 0x040001C9 RID: 457
		public object TagObject;
	}
}
