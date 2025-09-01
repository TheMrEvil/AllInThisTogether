using System;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Defines values for the type of authentication used during a Remote Procedure Call (RPC) login to a server. This login occurs when you create a <see cref="T:System.Diagnostics.Eventing.Reader.EventLogSession" /> object that specifies a connection to a remote computer.</summary>
	// Token: 0x0200039C RID: 924
	public enum SessionAuthentication
	{
		/// <summary>Use the default authentication method during RPC login. The default authentication is equivalent to Negotiate.</summary>
		// Token: 0x04000D43 RID: 3395
		Default,
		/// <summary>Use Kerberos authentication during RPC login. </summary>
		// Token: 0x04000D44 RID: 3396
		Kerberos = 2,
		/// <summary>Use the Negotiate authentication method during RPC login. This allows the client application to select the most appropriate authentication method (NTLM or Kerberos) for the situation. </summary>
		// Token: 0x04000D45 RID: 3397
		Negotiate = 1,
		/// <summary>Use Windows NT LAN Manager (NTLM) authentication during RPC login.</summary>
		// Token: 0x04000D46 RID: 3398
		Ntlm = 3
	}
}
