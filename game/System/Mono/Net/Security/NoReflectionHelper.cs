using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x020000A8 RID: 168
	internal static class NoReflectionHelper
	{
		// Token: 0x0600034A RID: 842 RVA: 0x00009DEA File Offset: 0x00007FEA
		internal static object GetDefaultValidator(object settings)
		{
			return ChainValidationHelper.GetDefaultValidator((MonoTlsSettings)settings);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00009DF7 File Offset: 0x00007FF7
		internal static object GetProvider()
		{
			return MonoTlsProviderFactory.GetProvider();
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00009DFE File Offset: 0x00007FFE
		internal static bool IsInitialized
		{
			get
			{
				return MonoTlsProviderFactory.IsInitialized;
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00009E05 File Offset: 0x00008005
		internal static void Initialize()
		{
			MonoTlsProviderFactory.Initialize();
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00009E0C File Offset: 0x0000800C
		internal static void Initialize(string provider)
		{
			MonoTlsProviderFactory.Initialize(provider);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00009E14 File Offset: 0x00008014
		internal static HttpWebRequest CreateHttpsRequest(Uri requestUri, object provider, object settings)
		{
			return new HttpWebRequest(requestUri, (MobileTlsProvider)provider, (MonoTlsSettings)settings);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00009E28 File Offset: 0x00008028
		internal static object CreateHttpListener(object certificate, object provider, object settings)
		{
			return new HttpListener((X509Certificate)certificate, (MonoTlsProvider)provider, (MonoTlsSettings)settings);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00009E41 File Offset: 0x00008041
		internal static object GetMonoSslStream(SslStream stream)
		{
			return stream.Impl;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00009E49 File Offset: 0x00008049
		internal static object GetMonoSslStream(HttpListenerContext context)
		{
			SslStream sslStream = context.Connection.SslStream;
			if (sslStream == null)
			{
				return null;
			}
			return sslStream.Impl;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00009E61 File Offset: 0x00008061
		internal static bool IsProviderSupported(string name)
		{
			return MonoTlsProviderFactory.IsProviderSupported(name);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00009E69 File Offset: 0x00008069
		internal static object GetProvider(string name)
		{
			return MonoTlsProviderFactory.GetProvider(name);
		}
	}
}
