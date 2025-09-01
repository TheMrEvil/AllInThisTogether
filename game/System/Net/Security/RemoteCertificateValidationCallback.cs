using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	/// <summary>Verifies the remote Secure Sockets Layer (SSL) certificate used for authentication.</summary>
	/// <param name="sender">An object that contains state information for this validation.</param>
	/// <param name="certificate">The certificate used to authenticate the remote party.</param>
	/// <param name="chain">The chain of certificate authorities associated with the remote certificate.</param>
	/// <param name="sslPolicyErrors">One or more errors associated with the remote certificate.</param>
	/// <returns>A <see cref="T:System.Boolean" /> value that determines whether the specified certificate is accepted for authentication.</returns>
	// Token: 0x0200085D RID: 2141
	// (Invoke) Token: 0x06004440 RID: 17472
	public delegate bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors);
}
