using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x02000012 RID: 18
	public class LocalUser : UserProfile, ILocalUser, IUserProfile
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00002D40 File Offset: 0x00000F40
		public LocalUser()
		{
			IUserProfile[] friends = new UserProfile[0];
			this.m_Friends = friends;
			this.m_Authenticated = false;
			this.m_Underage = false;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002D71 File Offset: 0x00000F71
		public void Authenticate(Action<bool> callback)
		{
			ActivePlatform.Instance.Authenticate(this, callback);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002D81 File Offset: 0x00000F81
		public void Authenticate(Action<bool, string> callback)
		{
			ActivePlatform.Instance.Authenticate(this, callback);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002D91 File Offset: 0x00000F91
		public void LoadFriends(Action<bool> callback)
		{
			ActivePlatform.Instance.LoadFriends(this, callback);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002DA1 File Offset: 0x00000FA1
		public void SetFriends(IUserProfile[] friends)
		{
			this.m_Friends = friends;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002DAB File Offset: 0x00000FAB
		public void SetAuthenticated(bool value)
		{
			this.m_Authenticated = value;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002DB5 File Offset: 0x00000FB5
		public void SetUnderage(bool value)
		{
			this.m_Underage = value;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public IUserProfile[] friends
		{
			get
			{
				return this.m_Friends;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public bool authenticated
		{
			get
			{
				return this.m_Authenticated;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public bool underage
		{
			get
			{
				return this.m_Underage;
			}
		}

		// Token: 0x0400001B RID: 27
		private IUserProfile[] m_Friends;

		// Token: 0x0400001C RID: 28
		private bool m_Authenticated;

		// Token: 0x0400001D RID: 29
		private bool m_Underage;
	}
}
