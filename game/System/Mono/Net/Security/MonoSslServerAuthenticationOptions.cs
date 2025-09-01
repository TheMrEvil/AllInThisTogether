using System;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Mono.Net.Security.Private;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x020000A4 RID: 164
	internal sealed class MonoSslServerAuthenticationOptions : MonoSslAuthenticationOptions, IMonoSslServerAuthenticationOptions, IMonoAuthenticationOptions
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000312 RID: 786 RVA: 0x000092C6 File Offset: 0x000074C6
		public SslServerAuthenticationOptions Options
		{
			[CompilerGenerated]
			get
			{
				return this.<Options>k__BackingField;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool ServerMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000092CE File Offset: 0x000074CE
		public MonoSslServerAuthenticationOptions(SslServerAuthenticationOptions options)
		{
			this.Options = options;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000092DD File Offset: 0x000074DD
		public MonoSslServerAuthenticationOptions()
		{
			this.Options = new SslServerAuthenticationOptions();
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000316 RID: 790 RVA: 0x000092F0 File Offset: 0x000074F0
		// (set) Token: 0x06000317 RID: 791 RVA: 0x000092FD File Offset: 0x000074FD
		public override bool AllowRenegotiation
		{
			get
			{
				return this.Options.AllowRenegotiation;
			}
			set
			{
				this.Options.AllowRenegotiation = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000930B File Offset: 0x0000750B
		// (set) Token: 0x06000319 RID: 793 RVA: 0x00009318 File Offset: 0x00007518
		public override RemoteCertificateValidationCallback RemoteCertificateValidationCallback
		{
			get
			{
				return this.Options.RemoteCertificateValidationCallback;
			}
			set
			{
				this.Options.RemoteCertificateValidationCallback = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00009326 File Offset: 0x00007526
		// (set) Token: 0x0600031B RID: 795 RVA: 0x00009333 File Offset: 0x00007533
		public override X509RevocationMode CertificateRevocationCheckMode
		{
			get
			{
				return this.Options.CertificateRevocationCheckMode;
			}
			set
			{
				this.Options.CertificateRevocationCheckMode = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00009341 File Offset: 0x00007541
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0000934E File Offset: 0x0000754E
		public override EncryptionPolicy EncryptionPolicy
		{
			get
			{
				return this.Options.EncryptionPolicy;
			}
			set
			{
				this.Options.EncryptionPolicy = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000935C File Offset: 0x0000755C
		// (set) Token: 0x0600031F RID: 799 RVA: 0x00009369 File Offset: 0x00007569
		public override SslProtocols EnabledSslProtocols
		{
			get
			{
				return this.Options.EnabledSslProtocols;
			}
			set
			{
				this.Options.EnabledSslProtocols = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00009377 File Offset: 0x00007577
		// (set) Token: 0x06000321 RID: 801 RVA: 0x00009384 File Offset: 0x00007584
		public override bool ClientCertificateRequired
		{
			get
			{
				return this.Options.ClientCertificateRequired;
			}
			set
			{
				this.Options.ClientCertificateRequired = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00009392 File Offset: 0x00007592
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0000939F File Offset: 0x0000759F
		public ServerCertificateSelectionCallback ServerCertificateSelectionCallback
		{
			get
			{
				return this.Options.ServerCertificateSelectionCallback;
			}
			set
			{
				this.Options.ServerCertificateSelectionCallback = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000324 RID: 804 RVA: 0x000093AD File Offset: 0x000075AD
		// (set) Token: 0x06000325 RID: 805 RVA: 0x000093BA File Offset: 0x000075BA
		MonoServerCertificateSelectionCallback IMonoSslServerAuthenticationOptions.ServerCertificateSelectionCallback
		{
			get
			{
				return CallbackHelpers.PublicToMono(this.ServerCertificateSelectionCallback);
			}
			set
			{
				this.ServerCertificateSelectionCallback = CallbackHelpers.MonoToPublic(value);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000326 RID: 806 RVA: 0x000044FA File Offset: 0x000026FA
		// (set) Token: 0x06000327 RID: 807 RVA: 0x000044FA File Offset: 0x000026FA
		public override string TargetHost
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000328 RID: 808 RVA: 0x000093C8 File Offset: 0x000075C8
		// (set) Token: 0x06000329 RID: 809 RVA: 0x000093D5 File Offset: 0x000075D5
		public override X509Certificate ServerCertificate
		{
			get
			{
				return this.Options.ServerCertificate;
			}
			set
			{
				this.Options.ServerCertificate = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600032A RID: 810 RVA: 0x000044FA File Offset: 0x000026FA
		// (set) Token: 0x0600032B RID: 811 RVA: 0x000044FA File Offset: 0x000026FA
		public override X509CertificateCollection ClientCertificates
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04000276 RID: 630
		[CompilerGenerated]
		private readonly SslServerAuthenticationOptions <Options>k__BackingField;
	}
}
