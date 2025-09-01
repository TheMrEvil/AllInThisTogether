using System;

namespace System.Net.Security
{
	// Token: 0x02000851 RID: 2129
	internal static class SslClientAuthenticationOptionsExtensions
	{
		// Token: 0x060043A1 RID: 17313 RVA: 0x000EBFE0 File Offset: 0x000EA1E0
		public static SslClientAuthenticationOptions ShallowClone(this SslClientAuthenticationOptions options)
		{
			return new SslClientAuthenticationOptions
			{
				AllowRenegotiation = options.AllowRenegotiation,
				ApplicationProtocols = options.ApplicationProtocols,
				CertificateRevocationCheckMode = options.CertificateRevocationCheckMode,
				ClientCertificates = options.ClientCertificates,
				EnabledSslProtocols = options.EnabledSslProtocols,
				EncryptionPolicy = options.EncryptionPolicy,
				LocalCertificateSelectionCallback = options.LocalCertificateSelectionCallback,
				RemoteCertificateValidationCallback = options.RemoteCertificateValidationCallback,
				TargetHost = options.TargetHost
			};
		}
	}
}
