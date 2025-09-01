using System;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;

namespace Photon.Realtime
{
	// Token: 0x0200002D RID: 45
	public class RoomOptions
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000083D7 File Offset: 0x000065D7
		// (set) Token: 0x0600011C RID: 284 RVA: 0x000083DF File Offset: 0x000065DF
		public bool IsVisible
		{
			get
			{
				return this.isVisible;
			}
			set
			{
				this.isVisible = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000083E8 File Offset: 0x000065E8
		// (set) Token: 0x0600011E RID: 286 RVA: 0x000083F0 File Offset: 0x000065F0
		public bool IsOpen
		{
			get
			{
				return this.isOpen;
			}
			set
			{
				this.isOpen = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000083F9 File Offset: 0x000065F9
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00008401 File Offset: 0x00006601
		public bool CleanupCacheOnLeave
		{
			get
			{
				return this.cleanupCacheOnLeave;
			}
			set
			{
				this.cleanupCacheOnLeave = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000840A File Offset: 0x0000660A
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00008412 File Offset: 0x00006612
		public bool SuppressRoomEvents
		{
			[CompilerGenerated]
			get
			{
				return this.<SuppressRoomEvents>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SuppressRoomEvents>k__BackingField = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000841B File Offset: 0x0000661B
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00008423 File Offset: 0x00006623
		public bool SuppressPlayerInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<SuppressPlayerInfo>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SuppressPlayerInfo>k__BackingField = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000842C File Offset: 0x0000662C
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00008434 File Offset: 0x00006634
		public bool PublishUserId
		{
			[CompilerGenerated]
			get
			{
				return this.<PublishUserId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PublishUserId>k__BackingField = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000843D File Offset: 0x0000663D
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00008445 File Offset: 0x00006645
		public bool DeleteNullProperties
		{
			[CompilerGenerated]
			get
			{
				return this.<DeleteNullProperties>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DeleteNullProperties>k__BackingField = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000844E File Offset: 0x0000664E
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00008456 File Offset: 0x00006656
		public bool BroadcastPropsChangeToAll
		{
			get
			{
				return this.broadcastPropsChangeToAll;
			}
			set
			{
				this.broadcastPropsChangeToAll = value;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000845F File Offset: 0x0000665F
		public RoomOptions()
		{
		}

		// Token: 0x04000183 RID: 387
		private bool isVisible = true;

		// Token: 0x04000184 RID: 388
		private bool isOpen = true;

		// Token: 0x04000185 RID: 389
		public int MaxPlayers;

		// Token: 0x04000186 RID: 390
		public int PlayerTtl;

		// Token: 0x04000187 RID: 391
		public int EmptyRoomTtl;

		// Token: 0x04000188 RID: 392
		private bool cleanupCacheOnLeave = true;

		// Token: 0x04000189 RID: 393
		public Hashtable CustomRoomProperties;

		// Token: 0x0400018A RID: 394
		public string[] CustomRoomPropertiesForLobby = new string[0];

		// Token: 0x0400018B RID: 395
		public string[] Plugins;

		// Token: 0x0400018C RID: 396
		[CompilerGenerated]
		private bool <SuppressRoomEvents>k__BackingField;

		// Token: 0x0400018D RID: 397
		[CompilerGenerated]
		private bool <SuppressPlayerInfo>k__BackingField;

		// Token: 0x0400018E RID: 398
		[CompilerGenerated]
		private bool <PublishUserId>k__BackingField;

		// Token: 0x0400018F RID: 399
		[CompilerGenerated]
		private bool <DeleteNullProperties>k__BackingField;

		// Token: 0x04000190 RID: 400
		private bool broadcastPropsChangeToAll = true;
	}
}
