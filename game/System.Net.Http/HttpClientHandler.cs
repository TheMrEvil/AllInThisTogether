using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	/// <summary>The default message handler used by <see cref="T:System.Net.Http.HttpClient" />.</summary>
	// Token: 0x0200000B RID: 11
	public class HttpClientHandler : HttpMessageHandler
	{
		// Token: 0x06000027 RID: 39 RVA: 0x0000286D File Offset: 0x00000A6D
		private static IMonoHttpClientHandler CreateDefaultHandler()
		{
			return new MonoWebRequestHandler();
		}

		/// <summary>Gets a cached delegate that always returns <see langword="true" />.</summary>
		/// <returns>A cached delegate that always returns <see langword="true" />.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002874 File Offset: 0x00000A74
		public static Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> DangerousAcceptAnyServerCertificateValidator
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		/// <summary>Creates an instance of a <see cref="T:System.Net.Http.HttpClientHandler" /> class.</summary>
		// Token: 0x06000029 RID: 41 RVA: 0x0000287B File Offset: 0x00000A7B
		public HttpClientHandler() : this(HttpClientHandler.CreateDefaultHandler())
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002888 File Offset: 0x00000A88
		internal HttpClientHandler(IMonoHttpClientHandler handler)
		{
			this._delegatingHandler = handler;
			this.ClientCertificateOptions = ClientCertificateOption.Manual;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpClientHandler" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		// Token: 0x0600002B RID: 43 RVA: 0x0000289E File Offset: 0x00000A9E
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._delegatingHandler.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>Gets a value that indicates whether the handler supports automatic response content decompression.</summary>
		/// <returns>
		///   <see langword="true" /> if the if the handler supports automatic response content decompression; otherwise <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000028B5 File Offset: 0x00000AB5
		public virtual bool SupportsAutomaticDecompression
		{
			get
			{
				return this._delegatingHandler.SupportsAutomaticDecompression;
			}
		}

		/// <summary>Gets a value that indicates whether the handler supports proxy settings.</summary>
		/// <returns>
		///   <see langword="true" /> if the if the handler supports proxy settings; otherwise <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000028C2 File Offset: 0x00000AC2
		public virtual bool SupportsProxy
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether the handler supports configuration settings for the <see cref="P:System.Net.Http.HttpClientHandler.AllowAutoRedirect" /> and <see cref="P:System.Net.Http.HttpClientHandler.MaxAutomaticRedirections" /> properties.</summary>
		/// <returns>
		///   <see langword="true" /> if the if the handler supports configuration settings for the <see cref="P:System.Net.Http.HttpClientHandler.AllowAutoRedirect" /> and <see cref="P:System.Net.Http.HttpClientHandler.MaxAutomaticRedirections" /> properties; otherwise <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000028C2 File Offset: 0x00000AC2
		public virtual bool SupportsRedirectConfiguration
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the handler uses the  <see cref="P:System.Net.Http.HttpClientHandler.CookieContainer" /> property  to store server cookies and uses these cookies when sending requests.</summary>
		/// <returns>
		///   <see langword="true" /> if the if the handler supports uses the  <see cref="P:System.Net.Http.HttpClientHandler.CookieContainer" /> property  to store server cookies and uses these cookies when sending requests; otherwise <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000028C5 File Offset: 0x00000AC5
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000028D2 File Offset: 0x00000AD2
		public bool UseCookies
		{
			get
			{
				return this._delegatingHandler.UseCookies;
			}
			set
			{
				this._delegatingHandler.UseCookies = value;
			}
		}

		/// <summary>Gets or sets the cookie container used to store server cookies by the handler.</summary>
		/// <returns>The cookie container used to store server cookies by the handler.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000028E0 File Offset: 0x00000AE0
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000028ED File Offset: 0x00000AED
		public CookieContainer CookieContainer
		{
			get
			{
				return this._delegatingHandler.CookieContainer;
			}
			set
			{
				this._delegatingHandler.CookieContainer = value;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028FB File Offset: 0x00000AFB
		private void ThrowForModifiedManagedSslOptionsIfStarted()
		{
			this._delegatingHandler.SslOptions = this._delegatingHandler.SslOptions;
		}

		/// <summary>Gets or sets a value that indicates if the certificate is automatically picked from the certificate store or if the caller is allowed to pass in a specific client certificate.</summary>
		/// <returns>The collection of security certificates associated with this handler.</returns>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002913 File Offset: 0x00000B13
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000291C File Offset: 0x00000B1C
		public ClientCertificateOption ClientCertificateOptions
		{
			get
			{
				return this._clientCertificateOptions;
			}
			set
			{
				if (value == ClientCertificateOption.Manual)
				{
					this.ThrowForModifiedManagedSslOptionsIfStarted();
					this._clientCertificateOptions = value;
					this._delegatingHandler.SslOptions.LocalCertificateSelectionCallback = ((object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers) => CertificateHelper.GetEligibleClientCertificate(this.ClientCertificates));
					return;
				}
				if (value != ClientCertificateOption.Automatic)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ThrowForModifiedManagedSslOptionsIfStarted();
				this._clientCertificateOptions = value;
				this._delegatingHandler.SslOptions.LocalCertificateSelectionCallback = ((object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers) => CertificateHelper.GetEligibleClientCertificate());
			}
		}

		/// <summary>Gets the collection of security certificates that are associated requests to the server.</summary>
		/// <returns>The X509CertificateCollection that is presented to the server when performing certificate based client authentication.</returns>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000029A4 File Offset: 0x00000BA4
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this.ClientCertificateOptions != ClientCertificateOption.Manual)
				{
					throw new InvalidOperationException(SR.Format("The {0} property must be set to '{1}' to use this property.", "ClientCertificateOptions", "Manual"));
				}
				X509CertificateCollection result;
				if ((result = this._delegatingHandler.SslOptions.ClientCertificates) == null)
				{
					result = (this._delegatingHandler.SslOptions.ClientCertificates = new X509CertificateCollection());
				}
				return result;
			}
		}

		/// <summary>Gets or sets a callback method to validate the server certificate.</summary>
		/// <returns>A callback method to validate the server certificate.</returns>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000029FF File Offset: 0x00000BFF
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002A2D File Offset: 0x00000C2D
		public Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> ServerCertificateCustomValidationCallback
		{
			get
			{
				RemoteCertificateValidationCallback remoteCertificateValidationCallback = this._delegatingHandler.SslOptions.RemoteCertificateValidationCallback;
				ConnectHelper.CertificateCallbackMapper certificateCallbackMapper = ((remoteCertificateValidationCallback != null) ? remoteCertificateValidationCallback.Target : null) as ConnectHelper.CertificateCallbackMapper;
				if (certificateCallbackMapper == null)
				{
					return null;
				}
				return certificateCallbackMapper.FromHttpClientHandler;
			}
			set
			{
				this.ThrowForModifiedManagedSslOptionsIfStarted();
				this._delegatingHandler.SslOptions.RemoteCertificateValidationCallback = ((value != null) ? new ConnectHelper.CertificateCallbackMapper(value).ForSocketsHttpHandler : null);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the certificate is checked against the certificate authority revocation list.</summary>
		/// <returns>
		///   <see langword="true" /> if the certificate revocation list is checked; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">.NET Framework 4.7.1 only: This property is not implemented.</exception>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002A56 File Offset: 0x00000C56
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002A6B File Offset: 0x00000C6B
		public bool CheckCertificateRevocationList
		{
			get
			{
				return this._delegatingHandler.SslOptions.CertificateRevocationCheckMode == X509RevocationMode.Online;
			}
			set
			{
				this.ThrowForModifiedManagedSslOptionsIfStarted();
				this._delegatingHandler.SslOptions.CertificateRevocationCheckMode = (value ? X509RevocationMode.Online : X509RevocationMode.NoCheck);
			}
		}

		/// <summary>Gets or sets the TLS/SSL protocol used by the <see cref="T:System.Net.Http.HttpClient" /> objects managed by the HttpClientHandler object.</summary>
		/// <returns>One of the values defined in the <see cref="T:System.Security.Authentication.SslProtocols" /> enumeration.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">.NET Framework 4.7.1 only: This property is not implemented.</exception>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A8A File Offset: 0x00000C8A
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002A9C File Offset: 0x00000C9C
		public SslProtocols SslProtocols
		{
			get
			{
				return this._delegatingHandler.SslOptions.EnabledSslProtocols;
			}
			set
			{
				this.ThrowForModifiedManagedSslOptionsIfStarted();
				this._delegatingHandler.SslOptions.EnabledSslProtocols = value;
			}
		}

		/// <summary>Gets or sets the type of decompression method used by the handler for automatic decompression of the HTTP content response.</summary>
		/// <returns>The automatic decompression method used by the handler.</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002AB5 File Offset: 0x00000CB5
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002AC2 File Offset: 0x00000CC2
		public DecompressionMethods AutomaticDecompression
		{
			get
			{
				return this._delegatingHandler.AutomaticDecompression;
			}
			set
			{
				this._delegatingHandler.AutomaticDecompression = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the handler uses a proxy for requests.</summary>
		/// <returns>
		///   <see langword="true" /> if the handler should use a proxy for requests; otherwise <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002AD0 File Offset: 0x00000CD0
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002ADD File Offset: 0x00000CDD
		public bool UseProxy
		{
			get
			{
				return this._delegatingHandler.UseProxy;
			}
			set
			{
				this._delegatingHandler.UseProxy = value;
			}
		}

		/// <summary>Gets or sets proxy information used by the handler.</summary>
		/// <returns>The proxy information used by the handler. The default value is <see langword="null" />.</returns>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002AEB File Offset: 0x00000CEB
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public IWebProxy Proxy
		{
			get
			{
				return this._delegatingHandler.Proxy;
			}
			set
			{
				this._delegatingHandler.Proxy = value;
			}
		}

		/// <summary>When the default (system) proxy is being used, gets or sets the credentials to submit to the default proxy server for authentication. The default proxy is used only when <see cref="P:System.Net.Http.HttpClientHandler.UseProxy" /> is set to <see langword="true" /> and <see cref="P:System.Net.Http.HttpClientHandler.Proxy" /> is set to <see langword="null" />.</summary>
		/// <returns>The credentials needed to authenticate a request to the default proxy server.</returns>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002B06 File Offset: 0x00000D06
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002B13 File Offset: 0x00000D13
		public ICredentials DefaultProxyCredentials
		{
			get
			{
				return this._delegatingHandler.DefaultProxyCredentials;
			}
			set
			{
				this._delegatingHandler.DefaultProxyCredentials = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the handler sends an Authorization header with the request.</summary>
		/// <returns>
		///   <see langword="true" /> for the handler to send an HTTP Authorization header with requests after authentication has taken place; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002B21 File Offset: 0x00000D21
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002B2E File Offset: 0x00000D2E
		public bool PreAuthenticate
		{
			get
			{
				return this._delegatingHandler.PreAuthenticate;
			}
			set
			{
				this._delegatingHandler.PreAuthenticate = value;
			}
		}

		/// <summary>Gets or sets a value that controls whether default credentials are sent with requests by the handler.</summary>
		/// <returns>
		///   <see langword="true" /> if the default credentials are used; otherwise <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002B3C File Offset: 0x00000D3C
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002B50 File Offset: 0x00000D50
		public bool UseDefaultCredentials
		{
			get
			{
				return this._delegatingHandler.Credentials == CredentialCache.DefaultCredentials;
			}
			set
			{
				if (value)
				{
					this._delegatingHandler.Credentials = CredentialCache.DefaultCredentials;
					return;
				}
				if (this._delegatingHandler.Credentials == CredentialCache.DefaultCredentials)
				{
					this._delegatingHandler.Credentials = null;
				}
			}
		}

		/// <summary>Gets or sets authentication information used by this handler.</summary>
		/// <returns>The authentication credentials associated with the handler. The default is <see langword="null" />.</returns>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002B84 File Offset: 0x00000D84
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002B91 File Offset: 0x00000D91
		public ICredentials Credentials
		{
			get
			{
				return this._delegatingHandler.Credentials;
			}
			set
			{
				this._delegatingHandler.Credentials = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the handler should follow redirection responses.</summary>
		/// <returns>
		///   <see langword="true" /> if the handler should follow redirection responses; otherwise <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002B9F File Offset: 0x00000D9F
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002BAC File Offset: 0x00000DAC
		public bool AllowAutoRedirect
		{
			get
			{
				return this._delegatingHandler.AllowAutoRedirect;
			}
			set
			{
				this._delegatingHandler.AllowAutoRedirect = value;
			}
		}

		/// <summary>Gets or sets the maximum number of redirects that the handler follows.</summary>
		/// <returns>The maximum number of redirection responses that the handler follows. The default value is 50.</returns>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002BBA File Offset: 0x00000DBA
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002BC7 File Offset: 0x00000DC7
		public int MaxAutomaticRedirections
		{
			get
			{
				return this._delegatingHandler.MaxAutomaticRedirections;
			}
			set
			{
				this._delegatingHandler.MaxAutomaticRedirections = value;
			}
		}

		/// <summary>Gets or sets the maximum number of concurrent connections (per server endpoint) allowed when making requests using an <see cref="T:System.Net.Http.HttpClient" /> object. Note that the limit is per server endpoint, so for example a value of 256 would permit 256 concurrent connections to http://www.adatum.com/ and another 256 to http://www.adventure-works.com/.</summary>
		/// <returns>The maximum number of concurrent connections (per server endpoint) allowed by an <see cref="T:System.Net.Http.HttpClient" /> object.</returns>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002BD5 File Offset: 0x00000DD5
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002BE2 File Offset: 0x00000DE2
		public int MaxConnectionsPerServer
		{
			get
			{
				return this._delegatingHandler.MaxConnectionsPerServer;
			}
			set
			{
				this._delegatingHandler.MaxConnectionsPerServer = value;
			}
		}

		/// <summary>Gets or sets the maximum length, in kilobytes (1024 bytes), of the response headers. For example, if the value is 64, then 65536 bytes are allowed for the maximum response headers' length.</summary>
		/// <returns>The maximum length, in kilobytes (1024 bytes), of the response headers.</returns>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002BF0 File Offset: 0x00000DF0
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002BFD File Offset: 0x00000DFD
		public int MaxResponseHeadersLength
		{
			get
			{
				return this._delegatingHandler.MaxResponseHeadersLength;
			}
			set
			{
				this._delegatingHandler.MaxResponseHeadersLength = value;
			}
		}

		/// <summary>Gets or sets the maximum request content buffer size used by the handler.</summary>
		/// <returns>The maximum request content buffer size in bytes. The default value is 2 gigabytes.</returns>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002C0B File Offset: 0x00000E0B
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002C18 File Offset: 0x00000E18
		public long MaxRequestContentBufferSize
		{
			get
			{
				return this._delegatingHandler.MaxRequestContentBufferSize;
			}
			set
			{
				this._delegatingHandler.MaxRequestContentBufferSize = value;
			}
		}

		/// <summary>Gets a writable dictionary (that is, a map) of custom properties for the <see cref="T:System.Net.Http.HttpClient" /> requests. The dictionary is initialized empty; you can insert and query key-value pairs for your custom handlers and special processing.</summary>
		/// <returns>a writable dictionary of custom properties.</returns>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002C26 File Offset: 0x00000E26
		public IDictionary<string, object> Properties
		{
			get
			{
				return this._delegatingHandler.Properties;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002C33 File Offset: 0x00000E33
		internal void SetWebRequestTimeout(TimeSpan timeout)
		{
			this._delegatingHandler.SetWebRequestTimeout(timeout);
		}

		/// <summary>Creates an instance of  <see cref="T:System.Net.Http.HttpResponseMessage" /> based on the information provided in the <see cref="T:System.Net.Http.HttpRequestMessage" /> as an operation that will not block.</summary>
		/// <param name="request">The HTTP request message.</param>
		/// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was <see langword="null" />.</exception>
		// Token: 0x06000057 RID: 87 RVA: 0x00002C41 File Offset: 0x00000E41
		protected internal override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return this._delegatingHandler.SendAsync(request, cancellationToken);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002C50 File Offset: 0x00000E50
		[CompilerGenerated]
		private X509Certificate <set_ClientCertificateOptions>b__23_0(object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)
		{
			return CertificateHelper.GetEligibleClientCertificate(this.ClientCertificates);
		}

		// Token: 0x0400001C RID: 28
		private readonly IMonoHttpClientHandler _delegatingHandler;

		// Token: 0x0400001D RID: 29
		private ClientCertificateOption _clientCertificateOptions;

		// Token: 0x0200000C RID: 12
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000059 RID: 89 RVA: 0x00002C5D File Offset: 0x00000E5D
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600005A RID: 90 RVA: 0x000022B8 File Offset: 0x000004B8
			public <>c()
			{
			}

			// Token: 0x0600005B RID: 91 RVA: 0x00002C69 File Offset: 0x00000E69
			internal X509Certificate <set_ClientCertificateOptions>b__23_1(object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)
			{
				return CertificateHelper.GetEligibleClientCertificate();
			}

			// Token: 0x0400001E RID: 30
			public static readonly HttpClientHandler.<>c <>9 = new HttpClientHandler.<>c();

			// Token: 0x0400001F RID: 31
			public static LocalCertificateSelectionCallback <>9__23_1;
		}
	}
}
