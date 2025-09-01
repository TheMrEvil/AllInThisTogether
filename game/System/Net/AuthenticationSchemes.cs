using System;

namespace System.Net
{
	/// <summary>Specifies protocols for authentication.</summary>
	// Token: 0x020005C2 RID: 1474
	[Flags]
	public enum AuthenticationSchemes
	{
		/// <summary>No authentication is allowed. A client requesting an <see cref="T:System.Net.HttpListener" /> object with this flag set will always receive a 403 Forbidden status. Use this flag when a resource should never be served to a client.</summary>
		// Token: 0x04001A54 RID: 6740
		None = 0,
		/// <summary>Specifies digest authentication.</summary>
		// Token: 0x04001A55 RID: 6741
		Digest = 1,
		/// <summary>Negotiates with the client to determine the authentication scheme. If both client and server support Kerberos, it is used; otherwise, NTLM is used.</summary>
		// Token: 0x04001A56 RID: 6742
		Negotiate = 2,
		/// <summary>Specifies NTLM authentication.</summary>
		// Token: 0x04001A57 RID: 6743
		Ntlm = 4,
		/// <summary>Specifies basic authentication.</summary>
		// Token: 0x04001A58 RID: 6744
		Basic = 8,
		/// <summary>Specifies anonymous authentication.</summary>
		// Token: 0x04001A59 RID: 6745
		Anonymous = 32768,
		/// <summary>Specifies Windows authentication.</summary>
		// Token: 0x04001A5A RID: 6746
		IntegratedWindowsAuthentication = 6
	}
}
