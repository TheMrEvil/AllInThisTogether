using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x02000013 RID: 19
	public class UserProfile : IUserProfile
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00002E08 File Offset: 0x00001008
		public UserProfile()
		{
			this.m_UserName = "Uninitialized";
			this.m_ID = "0";
			this.m_legacyID = "0";
			this.m_IsFriend = false;
			this.m_State = UserState.Offline;
			this.m_Image = new Texture2D(32, 32);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002E5B File Offset: 0x0000105B
		public UserProfile(string name, string id, bool friend) : this(name, id, friend, UserState.Offline, new Texture2D(0, 0))
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002E70 File Offset: 0x00001070
		public UserProfile(string name, string id, bool friend, UserState state, Texture2D image) : this(name, id, id, friend, state, image)
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002E82 File Offset: 0x00001082
		public UserProfile(string name, string teamId, string gameId, bool friend, UserState state, Texture2D image)
		{
			this.m_UserName = name;
			this.m_ID = teamId;
			this.m_gameID = gameId;
			this.m_IsFriend = friend;
			this.m_State = state;
			this.m_Image = image;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002EBC File Offset: 0x000010BC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.id,
				" - ",
				this.userName,
				" - ",
				this.isFriend.ToString(),
				" - ",
				this.state.ToString()
			});
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002F2B File Offset: 0x0000112B
		public void SetUserName(string name)
		{
			this.m_UserName = name;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002F35 File Offset: 0x00001135
		public void SetUserID(string id)
		{
			this.m_ID = id;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002F3F File Offset: 0x0000113F
		public void SetLegacyUserID(string id)
		{
			this.m_legacyID = id;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002F49 File Offset: 0x00001149
		public void SetUserGameID(string id)
		{
			this.m_gameID = id;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002F53 File Offset: 0x00001153
		public void SetImage(Texture2D image)
		{
			this.m_Image = image;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002F5D File Offset: 0x0000115D
		public void SetIsFriend(bool value)
		{
			this.m_IsFriend = value;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002F67 File Offset: 0x00001167
		public void SetState(UserState state)
		{
			this.m_State = state;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002F74 File Offset: 0x00001174
		public string userName
		{
			get
			{
				return this.m_UserName;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002F8C File Offset: 0x0000118C
		public string id
		{
			get
			{
				return this.m_ID;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002FA4 File Offset: 0x000011A4
		[Obsolete("legacyId returns playerID from GKPlayer, which became obsolete in iOS 12.4 . id returns playerID for devices running versions before iOS 12.4, and the newer teamPlayerID for later versions. Please use IUserProfile.id or UserProfile.id instead (UnityUpgradable) -> id", true)]
		public string legacyId
		{
			get
			{
				throw new NotSupportedException("legacyId returns playerID from GKPlayer, which became obsolete in iOS 12.4 . id returns playerID for devices running versions before iOS 12.4, and the newer teamPlayerID for later versions. Please use IUserProfile.id or UserProfile.id instead");
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002FB4 File Offset: 0x000011B4
		public string gameId
		{
			get
			{
				return this.m_gameID;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002FCC File Offset: 0x000011CC
		public bool isFriend
		{
			get
			{
				return this.m_IsFriend;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002FE4 File Offset: 0x000011E4
		public UserState state
		{
			get
			{
				return this.m_State;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002FFC File Offset: 0x000011FC
		public Texture2D image
		{
			get
			{
				return this.m_Image;
			}
		}

		// Token: 0x0400001E RID: 30
		protected string m_UserName;

		// Token: 0x0400001F RID: 31
		protected string m_ID;

		// Token: 0x04000020 RID: 32
		private string m_legacyID;

		// Token: 0x04000021 RID: 33
		protected bool m_IsFriend;

		// Token: 0x04000022 RID: 34
		protected UserState m_State;

		// Token: 0x04000023 RID: 35
		protected Texture2D m_Image;

		// Token: 0x04000024 RID: 36
		private string m_gameID;

		// Token: 0x04000025 RID: 37
		private const string legacyIdObsoleteMessage = "legacyId returns playerID from GKPlayer, which became obsolete in iOS 12.4 . id returns playerID for devices running versions before iOS 12.4, and the newer teamPlayerID for later versions. Please use IUserProfile.id or UserProfile.id instead";
	}
}
