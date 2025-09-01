using System;

namespace System.Net.Mail
{
	// Token: 0x02000836 RID: 2102
	internal class CCredentialsByHost : ICredentialsByHost
	{
		// Token: 0x06004309 RID: 17161 RVA: 0x000E9DBD File Offset: 0x000E7FBD
		public CCredentialsByHost(string userName, string password)
		{
			this.userName = userName;
			this.password = password;
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x000E9DD3 File Offset: 0x000E7FD3
		public NetworkCredential GetCredential(string host, int port, string authenticationType)
		{
			return new NetworkCredential(this.userName, this.password);
		}

		// Token: 0x0400289B RID: 10395
		private string userName;

		// Token: 0x0400289C RID: 10396
		private string password;
	}
}
