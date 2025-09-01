using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Mono.Net.Security;

namespace Mono.Security.Interface
{
	// Token: 0x02000049 RID: 73
	public static class MonoTlsProviderFactory
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x0000F018 File Offset: 0x0000D218
		public static MonoTlsProvider GetProvider()
		{
			return (MonoTlsProvider)NoReflectionHelper.GetProvider();
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000F024 File Offset: 0x0000D224
		public static bool IsInitialized
		{
			get
			{
				return NoReflectionHelper.IsInitialized;
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000F02B File Offset: 0x0000D22B
		public static void Initialize()
		{
			NoReflectionHelper.Initialize();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000F032 File Offset: 0x0000D232
		public static void Initialize(string provider)
		{
			NoReflectionHelper.Initialize(provider);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000F03A File Offset: 0x0000D23A
		public static bool IsProviderSupported(string provider)
		{
			return NoReflectionHelper.IsProviderSupported(provider);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000F042 File Offset: 0x0000D242
		public static MonoTlsProvider GetProvider(string provider)
		{
			return (MonoTlsProvider)NoReflectionHelper.GetProvider(provider);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000F04F File Offset: 0x0000D24F
		public static HttpWebRequest CreateHttpsRequest(Uri requestUri, MonoTlsProvider provider, MonoTlsSettings settings = null)
		{
			return NoReflectionHelper.CreateHttpsRequest(requestUri, provider, settings);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000F059 File Offset: 0x0000D259
		public static HttpListener CreateHttpListener(X509Certificate certificate, MonoTlsProvider provider = null, MonoTlsSettings settings = null)
		{
			return (HttpListener)NoReflectionHelper.CreateHttpListener(certificate, provider, settings);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000F068 File Offset: 0x0000D268
		public static IMonoSslStream GetMonoSslStream(SslStream stream)
		{
			return (IMonoSslStream)NoReflectionHelper.GetMonoSslStream(stream);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000F075 File Offset: 0x0000D275
		public static IMonoSslStream GetMonoSslStream(HttpListenerContext context)
		{
			return (IMonoSslStream)NoReflectionHelper.GetMonoSslStream(context);
		}

		// Token: 0x04000279 RID: 633
		internal const int InternalVersion = 4;
	}
}
