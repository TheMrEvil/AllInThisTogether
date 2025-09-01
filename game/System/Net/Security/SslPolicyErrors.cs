using System;

namespace System.Net.Security
{
	/// <summary>Enumerates Secure Socket Layer (SSL) policy errors.</summary>
	// Token: 0x0200085E RID: 2142
	[Flags]
	public enum SslPolicyErrors
	{
		/// <summary>No SSL policy errors.</summary>
		// Token: 0x04002926 RID: 10534
		None = 0,
		/// <summary>Certificate not available.</summary>
		// Token: 0x04002927 RID: 10535
		RemoteCertificateNotAvailable = 1,
		/// <summary>Certificate name mismatch.</summary>
		// Token: 0x04002928 RID: 10536
		RemoteCertificateNameMismatch = 2,
		/// <summary>
		///   <see cref="P:System.Security.Cryptography.X509Certificates.X509Chain.ChainStatus" /> has returned a non empty array.</summary>
		// Token: 0x04002929 RID: 10537
		RemoteCertificateChainErrors = 4
	}
}
