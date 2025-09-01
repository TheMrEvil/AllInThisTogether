using System;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x020000A1 RID: 161
	internal abstract class MobileTlsProvider : MonoTlsProvider
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x000091A0 File Offset: 0x000073A0
		public sealed override IMonoSslStream CreateSslStream(Stream innerStream, bool leaveInnerStreamOpen, MonoTlsSettings settings = null)
		{
			return SslStream.CreateMonoSslStream(innerStream, leaveInnerStreamOpen, this, settings);
		}

		// Token: 0x060002E1 RID: 737
		internal abstract MobileAuthenticatedStream CreateSslStream(SslStream sslStream, Stream innerStream, bool leaveInnerStreamOpen, MonoTlsSettings settings);

		// Token: 0x060002E2 RID: 738
		internal abstract bool ValidateCertificate(ChainValidationHelper validator, string targetHost, bool serverMode, X509CertificateCollection certificates, bool wantsChain, ref X509Chain chain, ref SslPolicyErrors errors, ref int status11);

		// Token: 0x060002E3 RID: 739 RVA: 0x000091AB File Offset: 0x000073AB
		protected MobileTlsProvider()
		{
		}
	}
}
