using System;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the level of impersonation allowed when calling targets of a server application.</summary>
	// Token: 0x0200002E RID: 46
	[Serializable]
	public enum ImpersonationLevelOption
	{
		/// <summary>The client is anonymous to the server. The server process can impersonate the client, but the impersonation token does not contain any information about the client.</summary>
		// Token: 0x04000057 RID: 87
		Anonymous = 1,
		/// <summary>Uses the default impersonation level for the specified authentication service. In COM+, this setting is provided by the <see langword="DefaultImpersonationLevel" /> property in the <see langword="LocalComputer" /> collection.</summary>
		// Token: 0x04000058 RID: 88
		Default = 0,
		/// <summary>The most powerful impersonation level. When this level is selected, the server (whether local or remote) can impersonate the client's security context while acting on behalf of the client</summary>
		// Token: 0x04000059 RID: 89
		Delegate = 4,
		/// <summary>The system default level. The server can obtain the client's identity, and the server can impersonate the client to do ACL checks.</summary>
		// Token: 0x0400005A RID: 90
		Identify = 2,
		/// <summary>The server can impersonate the client's security context while acting on behalf of the client. The server can access local resources as the client.</summary>
		// Token: 0x0400005B RID: 91
		Impersonate
	}
}
