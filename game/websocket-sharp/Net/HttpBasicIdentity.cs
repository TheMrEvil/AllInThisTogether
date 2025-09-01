using System;
using System.Security.Principal;

namespace WebSocketSharp.Net
{
	// Token: 0x0200002F RID: 47
	public class HttpBasicIdentity : GenericIdentity
	{
		// Token: 0x06000387 RID: 903 RVA: 0x00016683 File Offset: 0x00014883
		internal HttpBasicIdentity(string username, string password) : base(username, "Basic")
		{
			this._password = password;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0001669C File Offset: 0x0001489C
		public virtual string Password
		{
			get
			{
				return this._password;
			}
		}

		// Token: 0x04000177 RID: 375
		private string _password;
	}
}
