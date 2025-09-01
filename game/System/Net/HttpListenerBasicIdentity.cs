using System;
using System.Security.Principal;

namespace System.Net
{
	/// <summary>Holds the user name and password from a basic authentication request.</summary>
	// Token: 0x0200068A RID: 1674
	public class HttpListenerBasicIdentity : GenericIdentity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerBasicIdentity" /> class using the specified user name and password.</summary>
		/// <param name="username">The user name.</param>
		/// <param name="password">The password.</param>
		// Token: 0x060034DC RID: 13532 RVA: 0x000B8F4D File Offset: 0x000B714D
		public HttpListenerBasicIdentity(string username, string password) : base(username, "Basic")
		{
			this.password = password;
		}

		/// <summary>Indicates the password from a basic authentication attempt.</summary>
		/// <returns>A <see cref="T:System.String" /> that holds the password.</returns>
		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x000B8F62 File Offset: 0x000B7162
		public virtual string Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x04001EDF RID: 7903
		private string password;
	}
}
