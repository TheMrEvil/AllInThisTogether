using System;

namespace System.Net
{
	/// <summary>Provides the base authentication interface for retrieving credentials for Web client authentication.</summary>
	// Token: 0x020005D6 RID: 1494
	public interface ICredentials
	{
		/// <summary>Returns a <see cref="T:System.Net.NetworkCredential" /> object that is associated with the specified URI, and authentication type.</summary>
		/// <param name="uri">The <see cref="T:System.Uri" /> that the client is providing authentication for.</param>
		/// <param name="authType">The type of authentication, as defined in the <see cref="P:System.Net.IAuthenticationModule.AuthenticationType" /> property.</param>
		/// <returns>The <see cref="T:System.Net.NetworkCredential" /> that is associated with the specified URI and authentication type, or, if no credentials are available, <see langword="null" />.</returns>
		// Token: 0x06003029 RID: 12329
		NetworkCredential GetCredential(Uri uri, string authType);
	}
}
