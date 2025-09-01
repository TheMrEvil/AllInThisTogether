using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	// Token: 0x02000855 RID: 2133
	public class SslServerAuthenticationOptions
	{
		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x060043E2 RID: 17378 RVA: 0x000EC661 File Offset: 0x000EA861
		// (set) Token: 0x060043E3 RID: 17379 RVA: 0x000EC669 File Offset: 0x000EA869
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

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x060043E4 RID: 17380 RVA: 0x000EC672 File Offset: 0x000EA872
		// (set) Token: 0x060043E5 RID: 17381 RVA: 0x000EC67A File Offset: 0x000EA87A
		public bool ClientCertificateRequired
		{
			[CompilerGenerated]
			get
			{
				return this.<ClientCertificateRequired>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ClientCertificateRequired>k__BackingField = value;
			}
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x060043E6 RID: 17382 RVA: 0x000EC683 File Offset: 0x000EA883
		// (set) Token: 0x060043E7 RID: 17383 RVA: 0x000EC68B File Offset: 0x000EA88B
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

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x060043E8 RID: 17384 RVA: 0x000EC694 File Offset: 0x000EA894
		// (set) Token: 0x060043E9 RID: 17385 RVA: 0x000EC69C File Offset: 0x000EA89C
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

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x060043EA RID: 17386 RVA: 0x000EC6A5 File Offset: 0x000EA8A5
		// (set) Token: 0x060043EB RID: 17387 RVA: 0x000EC6AD File Offset: 0x000EA8AD
		public ServerCertificateSelectionCallback ServerCertificateSelectionCallback
		{
			[CompilerGenerated]
			get
			{
				return this.<ServerCertificateSelectionCallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ServerCertificateSelectionCallback>k__BackingField = value;
			}
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x060043EC RID: 17388 RVA: 0x000EC6B6 File Offset: 0x000EA8B6
		// (set) Token: 0x060043ED RID: 17389 RVA: 0x000EC6BE File Offset: 0x000EA8BE
		public X509Certificate ServerCertificate
		{
			[CompilerGenerated]
			get
			{
				return this.<ServerCertificate>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ServerCertificate>k__BackingField = value;
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x060043EE RID: 17390 RVA: 0x000EC6C7 File Offset: 0x000EA8C7
		// (set) Token: 0x060043EF RID: 17391 RVA: 0x000EC6CF File Offset: 0x000EA8CF
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

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x060043F0 RID: 17392 RVA: 0x000EC6D8 File Offset: 0x000EA8D8
		// (set) Token: 0x060043F1 RID: 17393 RVA: 0x000EC6E0 File Offset: 0x000EA8E0
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

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x060043F2 RID: 17394 RVA: 0x000EC70E File Offset: 0x000EA90E
		// (set) Token: 0x060043F3 RID: 17395 RVA: 0x000EC716 File Offset: 0x000EA916
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

		// Token: 0x060043F4 RID: 17396 RVA: 0x000EC744 File Offset: 0x000EA944
		public SslServerAuthenticationOptions()
		{
		}

		// Token: 0x04002907 RID: 10503
		private X509RevocationMode _checkCertificateRevocation;

		// Token: 0x04002908 RID: 10504
		private SslProtocols _enabledSslProtocols;

		// Token: 0x04002909 RID: 10505
		private EncryptionPolicy _encryptionPolicy;

		// Token: 0x0400290A RID: 10506
		private bool _allowRenegotiation = true;

		// Token: 0x0400290B RID: 10507
		[CompilerGenerated]
		private bool <ClientCertificateRequired>k__BackingField;

		// Token: 0x0400290C RID: 10508
		[CompilerGenerated]
		private List<SslApplicationProtocol> <ApplicationProtocols>k__BackingField;

		// Token: 0x0400290D RID: 10509
		[CompilerGenerated]
		private RemoteCertificateValidationCallback <RemoteCertificateValidationCallback>k__BackingField;

		// Token: 0x0400290E RID: 10510
		[CompilerGenerated]
		private ServerCertificateSelectionCallback <ServerCertificateSelectionCallback>k__BackingField;

		// Token: 0x0400290F RID: 10511
		[CompilerGenerated]
		private X509Certificate <ServerCertificate>k__BackingField;
	}
}
