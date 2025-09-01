using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	// Token: 0x02000854 RID: 2132
	public class SslClientAuthenticationOptions
	{
		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x060043CF RID: 17359 RVA: 0x000EC56F File Offset: 0x000EA76F
		// (set) Token: 0x060043D0 RID: 17360 RVA: 0x000EC577 File Offset: 0x000EA777
		public bool AllowRenegotiation
		{
			get
			{
				return this._allowRenegotiation;
			}
			set
			{
				this._allowRenegotiation = value;
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x060043D1 RID: 17361 RVA: 0x000EC580 File Offset: 0x000EA780
		// (set) Token: 0x060043D2 RID: 17362 RVA: 0x000EC588 File Offset: 0x000EA788
		public LocalCertificateSelectionCallback LocalCertificateSelectionCallback
		{
			[CompilerGenerated]
			get
			{
				return this.<LocalCertificateSelectionCallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LocalCertificateSelectionCallback>k__BackingField = value;
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x060043D3 RID: 17363 RVA: 0x000EC591 File Offset: 0x000EA791
		// (set) Token: 0x060043D4 RID: 17364 RVA: 0x000EC599 File Offset: 0x000EA799
		public RemoteCertificateValidationCallback RemoteCertificateValidationCallback
		{
			[CompilerGenerated]
			get
			{
				return this.<RemoteCertificateValidationCallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RemoteCertificateValidationCallback>k__BackingField = value;
			}
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x060043D5 RID: 17365 RVA: 0x000EC5A2 File Offset: 0x000EA7A2
		// (set) Token: 0x060043D6 RID: 17366 RVA: 0x000EC5AA File Offset: 0x000EA7AA
		public List<SslApplicationProtocol> ApplicationProtocols
		{
			[CompilerGenerated]
			get
			{
				return this.<ApplicationProtocols>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ApplicationProtocols>k__BackingField = value;
			}
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x060043D7 RID: 17367 RVA: 0x000EC5B3 File Offset: 0x000EA7B3
		// (set) Token: 0x060043D8 RID: 17368 RVA: 0x000EC5BB File Offset: 0x000EA7BB
		public string TargetHost
		{
			[CompilerGenerated]
			get
			{
				return this.<TargetHost>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TargetHost>k__BackingField = value;
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x060043D9 RID: 17369 RVA: 0x000EC5C4 File Offset: 0x000EA7C4
		// (set) Token: 0x060043DA RID: 17370 RVA: 0x000EC5CC File Offset: 0x000EA7CC
		public X509CertificateCollection ClientCertificates
		{
			[CompilerGenerated]
			get
			{
				return this.<ClientCertificates>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ClientCertificates>k__BackingField = value;
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x060043DB RID: 17371 RVA: 0x000EC5D5 File Offset: 0x000EA7D5
		// (set) Token: 0x060043DC RID: 17372 RVA: 0x000EC5DD File Offset: 0x000EA7DD
		public X509RevocationMode CertificateRevocationCheckMode
		{
			get
			{
				return this._checkCertificateRevocation;
			}
			set
			{
				if (value != X509RevocationMode.NoCheck && value != X509RevocationMode.Offline && value != X509RevocationMode.Online)
				{
					throw new ArgumentException(SR.Format("The specified value is not valid in the '{0}' enumeration.", "X509RevocationMode"), "value");
				}
				this._checkCertificateRevocation = value;
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x060043DD RID: 17373 RVA: 0x000EC60B File Offset: 0x000EA80B
		// (set) Token: 0x060043DE RID: 17374 RVA: 0x000EC613 File Offset: 0x000EA813
		public EncryptionPolicy EncryptionPolicy
		{
			get
			{
				return this._encryptionPolicy;
			}
			set
			{
				if (value != EncryptionPolicy.RequireEncryption && value != EncryptionPolicy.AllowNoEncryption && value != EncryptionPolicy.NoEncryption)
				{
					throw new ArgumentException(SR.Format("The specified value is not valid in the '{0}' enumeration.", "EncryptionPolicy"), "value");
				}
				this._encryptionPolicy = value;
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x060043DF RID: 17375 RVA: 0x000EC641 File Offset: 0x000EA841
		// (set) Token: 0x060043E0 RID: 17376 RVA: 0x000EC649 File Offset: 0x000EA849
		public SslProtocols EnabledSslProtocols
		{
			get
			{
				return this._enabledSslProtocols;
			}
			set
			{
				this._enabledSslProtocols = value;
			}
		}

		// Token: 0x060043E1 RID: 17377 RVA: 0x000EC652 File Offset: 0x000EA852
		public SslClientAuthenticationOptions()
		{
		}

		// Token: 0x040028FE RID: 10494
		private EncryptionPolicy _encryptionPolicy;

		// Token: 0x040028FF RID: 10495
		private X509RevocationMode _checkCertificateRevocation;

		// Token: 0x04002900 RID: 10496
		private SslProtocols _enabledSslProtocols;

		// Token: 0x04002901 RID: 10497
		private bool _allowRenegotiation = true;

		// Token: 0x04002902 RID: 10498
		[CompilerGenerated]
		private LocalCertificateSelectionCallback <LocalCertificateSelectionCallback>k__BackingField;

		// Token: 0x04002903 RID: 10499
		[CompilerGenerated]
		private RemoteCertificateValidationCallback <RemoteCertificateValidationCallback>k__BackingField;

		// Token: 0x04002904 RID: 10500
		[CompilerGenerated]
		private List<SslApplicationProtocol> <ApplicationProtocols>k__BackingField;

		// Token: 0x04002905 RID: 10501
		[CompilerGenerated]
		private string <TargetHost>k__BackingField;

		// Token: 0x04002906 RID: 10502
		[CompilerGenerated]
		private X509CertificateCollection <ClientCertificates>k__BackingField;
	}
}
