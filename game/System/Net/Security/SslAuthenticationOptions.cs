using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	// Token: 0x02000853 RID: 2131
	internal class SslAuthenticationOptions
	{
		// Token: 0x060043AE RID: 17326 RVA: 0x000EC338 File Offset: 0x000EA538
		internal SslAuthenticationOptions(SslClientAuthenticationOptions sslClientAuthenticationOptions, RemoteCertValidationCallback remoteCallback, LocalCertSelectionCallback localCallback)
		{
			this.AllowRenegotiation = sslClientAuthenticationOptions.AllowRenegotiation;
			this.ApplicationProtocols = sslClientAuthenticationOptions.ApplicationProtocols;
			this.CertValidationDelegate = remoteCallback;
			this.CheckCertName = true;
			this.EnabledSslProtocols = sslClientAuthenticationOptions.EnabledSslProtocols;
			this.EncryptionPolicy = sslClientAuthenticationOptions.EncryptionPolicy;
			this.IsServer = false;
			this.RemoteCertRequired = true;
			this.RemoteCertificateValidationCallback = sslClientAuthenticationOptions.RemoteCertificateValidationCallback;
			this.TargetHost = sslClientAuthenticationOptions.TargetHost;
			this.CertSelectionDelegate = localCallback;
			this.CertificateRevocationCheckMode = sslClientAuthenticationOptions.CertificateRevocationCheckMode;
			this.ClientCertificates = sslClientAuthenticationOptions.ClientCertificates;
			this.LocalCertificateSelectionCallback = sslClientAuthenticationOptions.LocalCertificateSelectionCallback;
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x000EC3DC File Offset: 0x000EA5DC
		internal SslAuthenticationOptions(SslServerAuthenticationOptions sslServerAuthenticationOptions)
		{
			this.AllowRenegotiation = sslServerAuthenticationOptions.AllowRenegotiation;
			this.ApplicationProtocols = sslServerAuthenticationOptions.ApplicationProtocols;
			this.CheckCertName = false;
			this.EnabledSslProtocols = sslServerAuthenticationOptions.EnabledSslProtocols;
			this.EncryptionPolicy = sslServerAuthenticationOptions.EncryptionPolicy;
			this.IsServer = true;
			this.RemoteCertRequired = sslServerAuthenticationOptions.ClientCertificateRequired;
			this.RemoteCertificateValidationCallback = sslServerAuthenticationOptions.RemoteCertificateValidationCallback;
			this.TargetHost = string.Empty;
			this.CertificateRevocationCheckMode = sslServerAuthenticationOptions.CertificateRevocationCheckMode;
			this.ServerCertificate = sslServerAuthenticationOptions.ServerCertificate;
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x060043B0 RID: 17328 RVA: 0x000EC468 File Offset: 0x000EA668
		// (set) Token: 0x060043B1 RID: 17329 RVA: 0x000EC470 File Offset: 0x000EA670
		internal bool AllowRenegotiation
		{
			[CompilerGenerated]
			get
			{
				return this.<AllowRenegotiation>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AllowRenegotiation>k__BackingField = value;
			}
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x060043B2 RID: 17330 RVA: 0x000EC479 File Offset: 0x000EA679
		// (set) Token: 0x060043B3 RID: 17331 RVA: 0x000EC481 File Offset: 0x000EA681
		internal string TargetHost
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

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x060043B4 RID: 17332 RVA: 0x000EC48A File Offset: 0x000EA68A
		// (set) Token: 0x060043B5 RID: 17333 RVA: 0x000EC492 File Offset: 0x000EA692
		internal X509CertificateCollection ClientCertificates
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

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x060043B6 RID: 17334 RVA: 0x000EC49B File Offset: 0x000EA69B
		internal List<SslApplicationProtocol> ApplicationProtocols
		{
			[CompilerGenerated]
			get
			{
				return this.<ApplicationProtocols>k__BackingField;
			}
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x060043B7 RID: 17335 RVA: 0x000EC4A3 File Offset: 0x000EA6A3
		// (set) Token: 0x060043B8 RID: 17336 RVA: 0x000EC4AB File Offset: 0x000EA6AB
		internal bool IsServer
		{
			[CompilerGenerated]
			get
			{
				return this.<IsServer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsServer>k__BackingField = value;
			}
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x060043B9 RID: 17337 RVA: 0x000EC4B4 File Offset: 0x000EA6B4
		// (set) Token: 0x060043BA RID: 17338 RVA: 0x000EC4BC File Offset: 0x000EA6BC
		internal RemoteCertificateValidationCallback RemoteCertificateValidationCallback
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

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x060043BB RID: 17339 RVA: 0x000EC4C5 File Offset: 0x000EA6C5
		// (set) Token: 0x060043BC RID: 17340 RVA: 0x000EC4CD File Offset: 0x000EA6CD
		internal LocalCertificateSelectionCallback LocalCertificateSelectionCallback
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

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x060043BD RID: 17341 RVA: 0x000EC4D6 File Offset: 0x000EA6D6
		// (set) Token: 0x060043BE RID: 17342 RVA: 0x000EC4DE File Offset: 0x000EA6DE
		internal X509Certificate ServerCertificate
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

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x060043BF RID: 17343 RVA: 0x000EC4E7 File Offset: 0x000EA6E7
		// (set) Token: 0x060043C0 RID: 17344 RVA: 0x000EC4EF File Offset: 0x000EA6EF
		internal SslProtocols EnabledSslProtocols
		{
			[CompilerGenerated]
			get
			{
				return this.<EnabledSslProtocols>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EnabledSslProtocols>k__BackingField = value;
			}
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x060043C1 RID: 17345 RVA: 0x000EC4F8 File Offset: 0x000EA6F8
		// (set) Token: 0x060043C2 RID: 17346 RVA: 0x000EC500 File Offset: 0x000EA700
		internal X509RevocationMode CertificateRevocationCheckMode
		{
			[CompilerGenerated]
			get
			{
				return this.<CertificateRevocationCheckMode>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CertificateRevocationCheckMode>k__BackingField = value;
			}
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x060043C3 RID: 17347 RVA: 0x000EC509 File Offset: 0x000EA709
		// (set) Token: 0x060043C4 RID: 17348 RVA: 0x000EC511 File Offset: 0x000EA711
		internal EncryptionPolicy EncryptionPolicy
		{
			[CompilerGenerated]
			get
			{
				return this.<EncryptionPolicy>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EncryptionPolicy>k__BackingField = value;
			}
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x060043C5 RID: 17349 RVA: 0x000EC51A File Offset: 0x000EA71A
		// (set) Token: 0x060043C6 RID: 17350 RVA: 0x000EC522 File Offset: 0x000EA722
		internal bool RemoteCertRequired
		{
			[CompilerGenerated]
			get
			{
				return this.<RemoteCertRequired>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RemoteCertRequired>k__BackingField = value;
			}
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x060043C7 RID: 17351 RVA: 0x000EC52B File Offset: 0x000EA72B
		// (set) Token: 0x060043C8 RID: 17352 RVA: 0x000EC533 File Offset: 0x000EA733
		internal bool CheckCertName
		{
			[CompilerGenerated]
			get
			{
				return this.<CheckCertName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CheckCertName>k__BackingField = value;
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x060043C9 RID: 17353 RVA: 0x000EC53C File Offset: 0x000EA73C
		// (set) Token: 0x060043CA RID: 17354 RVA: 0x000EC544 File Offset: 0x000EA744
		internal RemoteCertValidationCallback CertValidationDelegate
		{
			[CompilerGenerated]
			get
			{
				return this.<CertValidationDelegate>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CertValidationDelegate>k__BackingField = value;
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x060043CB RID: 17355 RVA: 0x000EC54D File Offset: 0x000EA74D
		// (set) Token: 0x060043CC RID: 17356 RVA: 0x000EC555 File Offset: 0x000EA755
		internal LocalCertSelectionCallback CertSelectionDelegate
		{
			[CompilerGenerated]
			get
			{
				return this.<CertSelectionDelegate>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CertSelectionDelegate>k__BackingField = value;
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x060043CD RID: 17357 RVA: 0x000EC55E File Offset: 0x000EA75E
		// (set) Token: 0x060043CE RID: 17358 RVA: 0x000EC566 File Offset: 0x000EA766
		internal ServerCertSelectionCallback ServerCertSelectionDelegate
		{
			[CompilerGenerated]
			get
			{
				return this.<ServerCertSelectionDelegate>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ServerCertSelectionDelegate>k__BackingField = value;
			}
		}

		// Token: 0x040028EE RID: 10478
		[CompilerGenerated]
		private bool <AllowRenegotiation>k__BackingField;

		// Token: 0x040028EF RID: 10479
		[CompilerGenerated]
		private string <TargetHost>k__BackingField;

		// Token: 0x040028F0 RID: 10480
		[CompilerGenerated]
		private X509CertificateCollection <ClientCertificates>k__BackingField;

		// Token: 0x040028F1 RID: 10481
		[CompilerGenerated]
		private readonly List<SslApplicationProtocol> <ApplicationProtocols>k__BackingField;

		// Token: 0x040028F2 RID: 10482
		[CompilerGenerated]
		private bool <IsServer>k__BackingField;

		// Token: 0x040028F3 RID: 10483
		[CompilerGenerated]
		private RemoteCertificateValidationCallback <RemoteCertificateValidationCallback>k__BackingField;

		// Token: 0x040028F4 RID: 10484
		[CompilerGenerated]
		private LocalCertificateSelectionCallback <LocalCertificateSelectionCallback>k__BackingField;

		// Token: 0x040028F5 RID: 10485
		[CompilerGenerated]
		private X509Certificate <ServerCertificate>k__BackingField;

		// Token: 0x040028F6 RID: 10486
		[CompilerGenerated]
		private SslProtocols <EnabledSslProtocols>k__BackingField;

		// Token: 0x040028F7 RID: 10487
		[CompilerGenerated]
		private X509RevocationMode <CertificateRevocationCheckMode>k__BackingField;

		// Token: 0x040028F8 RID: 10488
		[CompilerGenerated]
		private EncryptionPolicy <EncryptionPolicy>k__BackingField;

		// Token: 0x040028F9 RID: 10489
		[CompilerGenerated]
		private bool <RemoteCertRequired>k__BackingField;

		// Token: 0x040028FA RID: 10490
		[CompilerGenerated]
		private bool <CheckCertName>k__BackingField;

		// Token: 0x040028FB RID: 10491
		[CompilerGenerated]
		private RemoteCertValidationCallback <CertValidationDelegate>k__BackingField;

		// Token: 0x040028FC RID: 10492
		[CompilerGenerated]
		private LocalCertSelectionCallback <CertSelectionDelegate>k__BackingField;

		// Token: 0x040028FD RID: 10493
		[CompilerGenerated]
		private ServerCertSelectionCallback <ServerCertSelectionDelegate>k__BackingField;
	}
}
