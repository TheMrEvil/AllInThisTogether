using System;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x020000A2 RID: 162
	internal abstract class MonoSslAuthenticationOptions : IMonoAuthenticationOptions
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002E4 RID: 740
		public abstract bool ServerMode { get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002E5 RID: 741
		// (set) Token: 0x060002E6 RID: 742
		public abstract bool AllowRenegotiation { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002E7 RID: 743
		// (set) Token: 0x060002E8 RID: 744
		public abstract RemoteCertificateValidationCallback RemoteCertificateValidationCallback { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002E9 RID: 745
		// (set) Token: 0x060002EA RID: 746
		public abstract SslProtocols EnabledSslProtocols { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002EB RID: 747
		// (set) Token: 0x060002EC RID: 748
		public abstract EncryptionPolicy EncryptionPolicy { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002ED RID: 749
		// (set) Token: 0x060002EE RID: 750
		public abstract X509RevocationMode CertificateRevocationCheckMode { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002EF RID: 751
		// (set) Token: 0x060002F0 RID: 752
		public abstract string TargetHost { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002F1 RID: 753
		// (set) Token: 0x060002F2 RID: 754
		public abstract X509Certificate ServerCertificate { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002F3 RID: 755
		// (set) Token: 0x060002F4 RID: 756
		public abstract X509CertificateCollection ClientCertificates { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002F5 RID: 757
		// (set) Token: 0x060002F6 RID: 758
		public abstract bool ClientCertificateRequired { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x000091B3 File Offset: 0x000073B3
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x000091BB File Offset: 0x000073BB
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

		// Token: 0x060002F9 RID: 761 RVA: 0x0000219B File Offset: 0x0000039B
		protected MonoSslAuthenticationOptions()
		{
		}

		// Token: 0x04000274 RID: 628
		[CompilerGenerated]
		private ServerCertSelectionCallback <ServerCertSelectionDelegate>k__BackingField;
	}
}
