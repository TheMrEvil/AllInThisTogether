using System;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security.Private
{
	// Token: 0x020000AA RID: 170
	internal static class CallbackHelpers
	{
		// Token: 0x06000362 RID: 866 RVA: 0x0000A6A0 File Offset: 0x000088A0
		internal static MonoRemoteCertificateValidationCallback PublicToMono(RemoteCertificateValidationCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (string h, X509Certificate c, X509Chain ch, MonoSslPolicyErrors e) => callback(h, c, ch, (SslPolicyErrors)e);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000A6D0 File Offset: 0x000088D0
		internal static MonoRemoteCertificateValidationCallback InternalToMono(RemoteCertValidationCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (string h, X509Certificate c, X509Chain ch, MonoSslPolicyErrors e) => callback(h, c, ch, (SslPolicyErrors)e);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000A700 File Offset: 0x00008900
		internal static RemoteCertificateValidationCallback InternalToPublic(string hostname, RemoteCertValidationCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (object s, X509Certificate c, X509Chain ch, SslPolicyErrors e) => callback(hostname, c, ch, e);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000A738 File Offset: 0x00008938
		internal static MonoLocalCertificateSelectionCallback InternalToMono(LocalCertSelectionCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (string t, X509CertificateCollection lc, X509Certificate rc, string[] ai) => callback(t, lc, rc, ai);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000A768 File Offset: 0x00008968
		internal static LocalCertificateSelectionCallback MonoToPublic(MonoLocalCertificateSelectionCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (object s, string t, X509CertificateCollection lc, X509Certificate rc, string[] ai) => callback(t, lc, rc, ai);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000A798 File Offset: 0x00008998
		internal static RemoteCertValidationCallback MonoToInternal(MonoRemoteCertificateValidationCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (string h, X509Certificate c, X509Chain ch, SslPolicyErrors e) => callback(h, c, ch, (MonoSslPolicyErrors)e);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000A7C8 File Offset: 0x000089C8
		internal static LocalCertSelectionCallback MonoToInternal(MonoLocalCertificateSelectionCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (string t, X509CertificateCollection lc, X509Certificate rc, string[] ai) => callback(t, lc, rc, ai);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000A7F8 File Offset: 0x000089F8
		internal static ServerCertificateSelectionCallback MonoToPublic(MonoServerCertificateSelectionCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (object s, string h) => callback(s, h);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000A828 File Offset: 0x00008A28
		internal static MonoServerCertificateSelectionCallback PublicToMono(ServerCertificateSelectionCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (object s, string h) => callback(s, h);
		}

		// Token: 0x020000AB RID: 171
		[CompilerGenerated]
		private sealed class <>c__DisplayClass0_0
		{
			// Token: 0x0600036B RID: 875 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass0_0()
			{
			}

			// Token: 0x0600036C RID: 876 RVA: 0x0000A858 File Offset: 0x00008A58
			internal bool <PublicToMono>b__0(string h, X509Certificate c, X509Chain ch, MonoSslPolicyErrors e)
			{
				return this.callback(h, c, ch, (SslPolicyErrors)e);
			}

			// Token: 0x04000292 RID: 658
			public RemoteCertificateValidationCallback callback;
		}

		// Token: 0x020000AC RID: 172
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x0600036D RID: 877 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x0600036E RID: 878 RVA: 0x0000A86A File Offset: 0x00008A6A
			internal bool <InternalToMono>b__0(string h, X509Certificate c, X509Chain ch, MonoSslPolicyErrors e)
			{
				return this.callback(h, c, ch, (SslPolicyErrors)e);
			}

			// Token: 0x04000293 RID: 659
			public RemoteCertValidationCallback callback;
		}

		// Token: 0x020000AD RID: 173
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0
		{
			// Token: 0x0600036F RID: 879 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x06000370 RID: 880 RVA: 0x0000A87C File Offset: 0x00008A7C
			internal bool <InternalToPublic>b__0(object s, X509Certificate c, X509Chain ch, SslPolicyErrors e)
			{
				return this.callback(this.hostname, c, ch, e);
			}

			// Token: 0x04000294 RID: 660
			public RemoteCertValidationCallback callback;

			// Token: 0x04000295 RID: 661
			public string hostname;
		}

		// Token: 0x020000AE RID: 174
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x06000371 RID: 881 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x06000372 RID: 882 RVA: 0x0000A893 File Offset: 0x00008A93
			internal X509Certificate <InternalToMono>b__0(string t, X509CertificateCollection lc, X509Certificate rc, string[] ai)
			{
				return this.callback(t, lc, rc, ai);
			}

			// Token: 0x04000296 RID: 662
			public LocalCertSelectionCallback callback;
		}

		// Token: 0x020000AF RID: 175
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x06000373 RID: 883 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x06000374 RID: 884 RVA: 0x0000A8A5 File Offset: 0x00008AA5
			internal X509Certificate <MonoToPublic>b__0(object s, string t, X509CertificateCollection lc, X509Certificate rc, string[] ai)
			{
				return this.callback(t, lc, rc, ai);
			}

			// Token: 0x04000297 RID: 663
			public MonoLocalCertificateSelectionCallback callback;
		}

		// Token: 0x020000B0 RID: 176
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x06000375 RID: 885 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x06000376 RID: 886 RVA: 0x0000A8B8 File Offset: 0x00008AB8
			internal bool <MonoToInternal>b__0(string h, X509Certificate c, X509Chain ch, SslPolicyErrors e)
			{
				return this.callback(h, c, ch, (MonoSslPolicyErrors)e);
			}

			// Token: 0x04000298 RID: 664
			public MonoRemoteCertificateValidationCallback callback;
		}

		// Token: 0x020000B1 RID: 177
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x06000377 RID: 887 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x06000378 RID: 888 RVA: 0x0000A8CA File Offset: 0x00008ACA
			internal X509Certificate <MonoToInternal>b__0(string t, X509CertificateCollection lc, X509Certificate rc, string[] ai)
			{
				return this.callback(t, lc, rc, ai);
			}

			// Token: 0x04000299 RID: 665
			public MonoLocalCertificateSelectionCallback callback;
		}

		// Token: 0x020000B2 RID: 178
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x06000379 RID: 889 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x0600037A RID: 890 RVA: 0x0000A8DC File Offset: 0x00008ADC
			internal X509Certificate <MonoToPublic>b__0(object s, string h)
			{
				return this.callback(s, h);
			}

			// Token: 0x0400029A RID: 666
			public MonoServerCertificateSelectionCallback callback;
		}

		// Token: 0x020000B3 RID: 179
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x0600037B RID: 891 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x0600037C RID: 892 RVA: 0x0000A8EB File Offset: 0x00008AEB
			internal X509Certificate <PublicToMono>b__0(object s, string h)
			{
				return this.callback(s, h);
			}

			// Token: 0x0400029B RID: 667
			public ServerCertificateSelectionCallback callback;
		}
	}
}
