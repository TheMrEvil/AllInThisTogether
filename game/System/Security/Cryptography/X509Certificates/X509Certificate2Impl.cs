using System;
using System.Collections.Generic;
using System.Text;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D3 RID: 723
	internal abstract class X509Certificate2Impl : X509CertificateImpl
	{
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001682 RID: 5762
		// (set) Token: 0x06001683 RID: 5763
		public abstract bool Archived { get; set; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001684 RID: 5764
		public abstract IEnumerable<X509Extension> Extensions { get; }

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001685 RID: 5765
		// (set) Token: 0x06001686 RID: 5766
		public abstract string FriendlyName { get; set; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001687 RID: 5767
		public abstract X500DistinguishedName IssuerName { get; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001688 RID: 5768
		// (set) Token: 0x06001689 RID: 5769
		public abstract AsymmetricAlgorithm PrivateKey { get; set; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600168A RID: 5770
		public abstract PublicKey PublicKey { get; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600168B RID: 5771
		public abstract string SignatureAlgorithm { get; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600168C RID: 5772
		public abstract X500DistinguishedName SubjectName { get; }

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600168D RID: 5773
		public abstract int Version { get; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600168E RID: 5774
		internal abstract X509CertificateImplCollection IntermediateCertificates { get; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600168F RID: 5775
		internal abstract X509Certificate2Impl FallbackImpl { get; }

		// Token: 0x06001690 RID: 5776
		public abstract string GetNameInfo(X509NameType nameType, bool forIssuer);

		// Token: 0x06001691 RID: 5777
		public abstract bool Verify(X509Certificate2 thisCertificate);

		// Token: 0x06001692 RID: 5778
		public abstract void AppendPrivateKeyInfo(StringBuilder sb);

		// Token: 0x06001693 RID: 5779 RVA: 0x0005A4D8 File Offset: 0x000586D8
		public sealed override X509CertificateImpl CopyWithPrivateKey(RSA privateKey)
		{
			X509Certificate2Impl x509Certificate2Impl = (X509Certificate2Impl)this.Clone();
			x509Certificate2Impl.PrivateKey = privateKey;
			return x509Certificate2Impl;
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0005A4EC File Offset: 0x000586EC
		public sealed override X509Certificate CreateCertificate()
		{
			return new X509Certificate2(this);
		}

		// Token: 0x06001695 RID: 5781
		public abstract void Reset();

		// Token: 0x06001696 RID: 5782 RVA: 0x0005A4F4 File Offset: 0x000586F4
		protected X509Certificate2Impl()
		{
		}
	}
}
