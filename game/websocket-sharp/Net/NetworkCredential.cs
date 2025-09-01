using System;

namespace WebSocketSharp.Net
{
	// Token: 0x02000031 RID: 49
	public class NetworkCredential
	{
		// Token: 0x06000394 RID: 916 RVA: 0x0001688C File Offset: 0x00014A8C
		static NetworkCredential()
		{
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001689A File Offset: 0x00014A9A
		public NetworkCredential(string username, string password) : this(username, password, null, null)
		{
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000168A8 File Offset: 0x00014AA8
		public NetworkCredential(string username, string password, string domain, params string[] roles)
		{
			bool flag = username == null;
			if (flag)
			{
				throw new ArgumentNullException("username");
			}
			bool flag2 = username.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("An empty string.", "username");
			}
			this._username = username;
			this._password = password;
			this._domain = domain;
			this._roles = roles;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0001690C File Offset: 0x00014B0C
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0001692D File Offset: 0x00014B2D
		public string Domain
		{
			get
			{
				return this._domain ?? string.Empty;
			}
			internal set
			{
				this._domain = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00016938 File Offset: 0x00014B38
		// (set) Token: 0x0600039A RID: 922 RVA: 0x00016959 File Offset: 0x00014B59
		public string Password
		{
			get
			{
				return this._password ?? string.Empty;
			}
			internal set
			{
				this._password = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00016964 File Offset: 0x00014B64
		// (set) Token: 0x0600039C RID: 924 RVA: 0x00016985 File Offset: 0x00014B85
		public string[] Roles
		{
			get
			{
				return this._roles ?? NetworkCredential._noRoles;
			}
			internal set
			{
				this._roles = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00016990 File Offset: 0x00014B90
		// (set) Token: 0x0600039E RID: 926 RVA: 0x000169A8 File Offset: 0x00014BA8
		public string Username
		{
			get
			{
				return this._username;
			}
			internal set
			{
				this._username = value;
			}
		}

		// Token: 0x04000179 RID: 377
		private string _domain;

		// Token: 0x0400017A RID: 378
		private static readonly string[] _noRoles = new string[0];

		// Token: 0x0400017B RID: 379
		private string _password;

		// Token: 0x0400017C RID: 380
		private string[] _roles;

		// Token: 0x0400017D RID: 381
		private string _username;
	}
}
