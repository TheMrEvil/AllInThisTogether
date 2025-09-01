using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000C3 RID: 195
	internal struct SigningCertificateAsn
	{
		// Token: 0x04000376 RID: 886
		public EssCertId[] Certs;

		// Token: 0x04000377 RID: 887
		[OptionalValue]
		public PolicyInformation[] Policies;
	}
}
