using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Cache;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Mono.Net.Security;
using Mono.Security.Interface;

namespace System.Net.Http
{
	// Token: 0x0200000E RID: 14
	internal class MonoWebRequestHandler : IMonoHttpClientHandler, IDisposable
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00002C70 File Offset: 0x00000E70
		public MonoWebRequestHandler()
		{
			this.allowAutoRedirect = true;
			this.maxAutomaticRedirections = 50;
			this.maxRequestContentBufferSize = 2147483647L;
			this.useCookies = true;
			this.useProxy = true;
			this.allowPipelining = true;
			this.authenticationLevel = AuthenticationLevel.MutualAuthRequested;
			this.cachePolicy = WebRequest.DefaultCachePolicy;
			this.continueTimeout = TimeSpan.FromMilliseconds(350.0);
			this.impersonationLevel = TokenImpersonationLevel.Delegation;
			this.maxResponseHeadersLength = HttpWebRequest.DefaultMaximumResponseHeadersLength;
			this.readWriteTimeout = 300000;
			this.serverCertificateValidationCallback = null;
			this.unsafeAuthenticatedConnectionSharing = false;
			this.connectionGroupName = "HttpClientHandler" + Interlocked.Increment(ref MonoWebRequestHandler.groupCounter).ToString();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002D26 File Offset: 0x00000F26
		internal void EnsureModifiability()
		{
			if (this.sentRequest)
			{
				throw new InvalidOperationException("This instance has already started one or more requests. Properties can only be modified before sending the first request.");
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002D3B File Offset: 0x00000F3B
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00002D43 File Offset: 0x00000F43
		public bool AllowAutoRedirect
		{
			get
			{
				return this.allowAutoRedirect;
			}
			set
			{
				this.EnsureModifiability();
				this.allowAutoRedirect = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002D52 File Offset: 0x00000F52
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002D5A File Offset: 0x00000F5A
		public DecompressionMethods AutomaticDecompression
		{
			get
			{
				return this.automaticDecompression;
			}
			set
			{
				this.EnsureModifiability();
				this.automaticDecompression = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002D6C File Offset: 0x00000F6C
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002D91 File Offset: 0x00000F91
		public CookieContainer CookieContainer
		{
			get
			{
				CookieContainer result;
				if ((result = this.cookieContainer) == null)
				{
					result = (this.cookieContainer = new CookieContainer());
				}
				return result;
			}
			set
			{
				this.EnsureModifiability();
				this.cookieContainer = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002DA0 File Offset: 0x00000FA0
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public ICredentials Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.EnsureModifiability();
				this.credentials = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002DB7 File Offset: 0x00000FB7
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00002DBF File Offset: 0x00000FBF
		public int MaxAutomaticRedirections
		{
			get
			{
				return this.maxAutomaticRedirections;
			}
			set
			{
				this.EnsureModifiability();
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.maxAutomaticRedirections = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002DD8 File Offset: 0x00000FD8
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public long MaxRequestContentBufferSize
		{
			get
			{
				return this.maxRequestContentBufferSize;
			}
			set
			{
				this.EnsureModifiability();
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.maxRequestContentBufferSize = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002DFA File Offset: 0x00000FFA
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00002E02 File Offset: 0x00001002
		public bool PreAuthenticate
		{
			get
			{
				return this.preAuthenticate;
			}
			set
			{
				this.EnsureModifiability();
				this.preAuthenticate = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002E11 File Offset: 0x00001011
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002E19 File Offset: 0x00001019
		public IWebProxy Proxy
		{
			get
			{
				return this.proxy;
			}
			set
			{
				this.EnsureModifiability();
				if (!this.UseProxy)
				{
					throw new InvalidOperationException();
				}
				this.proxy = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000028C2 File Offset: 0x00000AC2
		public virtual bool SupportsAutomaticDecompression
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000028C2 File Offset: 0x00000AC2
		public virtual bool SupportsProxy
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000028C2 File Offset: 0x00000AC2
		public virtual bool SupportsRedirectConfiguration
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002E36 File Offset: 0x00001036
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00002E3E File Offset: 0x0000103E
		public bool UseCookies
		{
			get
			{
				return this.useCookies;
			}
			set
			{
				this.EnsureModifiability();
				this.useCookies = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002E4D File Offset: 0x0000104D
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00002E55 File Offset: 0x00001055
		public bool UseProxy
		{
			get
			{
				return this.useProxy;
			}
			set
			{
				this.EnsureModifiability();
				this.useProxy = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002E64 File Offset: 0x00001064
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00002E6C File Offset: 0x0000106C
		public bool AllowPipelining
		{
			get
			{
				return this.allowPipelining;
			}
			set
			{
				this.EnsureModifiability();
				this.allowPipelining = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002E7B File Offset: 0x0000107B
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00002E83 File Offset: 0x00001083
		public RequestCachePolicy CachePolicy
		{
			get
			{
				return this.cachePolicy;
			}
			set
			{
				this.EnsureModifiability();
				this.cachePolicy = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002E92 File Offset: 0x00001092
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00002E9A File Offset: 0x0000109A
		public AuthenticationLevel AuthenticationLevel
		{
			get
			{
				return this.authenticationLevel;
			}
			set
			{
				this.EnsureModifiability();
				this.authenticationLevel = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002EA9 File Offset: 0x000010A9
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00002EB1 File Offset: 0x000010B1
		[MonoTODO]
		public TimeSpan ContinueTimeout
		{
			get
			{
				return this.continueTimeout;
			}
			set
			{
				this.EnsureModifiability();
				this.continueTimeout = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002EC0 File Offset: 0x000010C0
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00002EC8 File Offset: 0x000010C8
		public TokenImpersonationLevel ImpersonationLevel
		{
			get
			{
				return this.impersonationLevel;
			}
			set
			{
				this.EnsureModifiability();
				this.impersonationLevel = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00002ED7 File Offset: 0x000010D7
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00002EDF File Offset: 0x000010DF
		public int MaxResponseHeadersLength
		{
			get
			{
				return this.maxResponseHeadersLength;
			}
			set
			{
				this.EnsureModifiability();
				this.maxResponseHeadersLength = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00002EEE File Offset: 0x000010EE
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00002EF6 File Offset: 0x000010F6
		public int ReadWriteTimeout
		{
			get
			{
				return this.readWriteTimeout;
			}
			set
			{
				this.EnsureModifiability();
				this.readWriteTimeout = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00002F05 File Offset: 0x00001105
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00002F0D File Offset: 0x0000110D
		public RemoteCertificateValidationCallback ServerCertificateValidationCallback
		{
			get
			{
				return this.serverCertificateValidationCallback;
			}
			set
			{
				this.EnsureModifiability();
				this.serverCertificateValidationCallback = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002F1C File Offset: 0x0000111C
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00002F24 File Offset: 0x00001124
		public bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return this.unsafeAuthenticatedConnectionSharing;
			}
			set
			{
				this.EnsureModifiability();
				this.unsafeAuthenticatedConnectionSharing = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002F34 File Offset: 0x00001134
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002F59 File Offset: 0x00001159
		public SslClientAuthenticationOptions SslOptions
		{
			get
			{
				SslClientAuthenticationOptions result;
				if ((result = this.sslOptions) == null)
				{
					result = (this.sslOptions = new SslClientAuthenticationOptions());
				}
				return result;
			}
			set
			{
				this.EnsureModifiability();
				this.sslOptions = value;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002F68 File Offset: 0x00001168
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002F71 File Offset: 0x00001171
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				Volatile.Write(ref this.disposed, true);
				ServicePointManager.CloseConnectionGroup(this.connectionGroupName);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002F95 File Offset: 0x00001195
		private bool GetConnectionKeepAlive(HttpRequestHeaders headers)
		{
			return headers.Connection.Any((string l) => string.Equals(l, "Keep-Alive", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00002FC4 File Offset: 0x000011C4
		internal virtual HttpWebRequest CreateWebRequest(HttpRequestMessage request)
		{
			HttpWebRequest httpWebRequest;
			if (HttpUtilities.IsSupportedSecureScheme(request.RequestUri.Scheme))
			{
				httpWebRequest = new HttpWebRequest(request.RequestUri, MonoTlsProviderFactory.GetProviderInternal(), MonoTlsSettings.CopyDefaultSettings());
				httpWebRequest.TlsSettings.ClientCertificateSelectionCallback = ((string t, X509CertificateCollection lc, X509Certificate rc, string[] ai) => this.SslOptions.LocalCertificateSelectionCallback(this, t, lc, rc, ai));
			}
			else
			{
				httpWebRequest = new HttpWebRequest(request.RequestUri);
			}
			httpWebRequest.ThrowOnError = false;
			httpWebRequest.AllowWriteStreamBuffering = false;
			if (request.Version == HttpVersion.Version20)
			{
				httpWebRequest.ProtocolVersion = HttpVersion.Version11;
			}
			else
			{
				httpWebRequest.ProtocolVersion = request.Version;
			}
			httpWebRequest.ConnectionGroupName = this.connectionGroupName;
			httpWebRequest.Method = request.Method.Method;
			bool? flag;
			bool flag2;
			if (httpWebRequest.ProtocolVersion == HttpVersion.Version10)
			{
				httpWebRequest.KeepAlive = this.GetConnectionKeepAlive(request.Headers);
			}
			else
			{
				HttpWebRequest httpWebRequest2 = httpWebRequest;
				flag = request.Headers.ConnectionClose;
				flag2 = true;
				httpWebRequest2.KeepAlive = !(flag.GetValueOrDefault() == flag2 & flag != null);
			}
			if (this.allowAutoRedirect)
			{
				httpWebRequest.AllowAutoRedirect = true;
				httpWebRequest.MaximumAutomaticRedirections = this.maxAutomaticRedirections;
			}
			else
			{
				httpWebRequest.AllowAutoRedirect = false;
			}
			httpWebRequest.AutomaticDecompression = this.automaticDecompression;
			httpWebRequest.PreAuthenticate = this.preAuthenticate;
			if (this.useCookies)
			{
				httpWebRequest.CookieContainer = this.CookieContainer;
			}
			httpWebRequest.Credentials = this.credentials;
			if (this.useProxy)
			{
				httpWebRequest.Proxy = this.proxy;
			}
			else
			{
				httpWebRequest.Proxy = null;
			}
			ServicePoint servicePoint = httpWebRequest.ServicePoint;
			flag = request.Headers.ExpectContinue;
			flag2 = true;
			servicePoint.Expect100Continue = (flag.GetValueOrDefault() == flag2 & flag != null);
			if (this.timeout != null)
			{
				httpWebRequest.Timeout = (int)this.timeout.Value.TotalMilliseconds;
			}
			httpWebRequest.ServerCertificateValidationCallback = this.SslOptions.RemoteCertificateValidationCallback;
			WebHeaderCollection headers = httpWebRequest.Headers;
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in request.Headers)
			{
				IEnumerable<string> enumerable = keyValuePair.Value;
				if (keyValuePair.Key == "Host")
				{
					httpWebRequest.Host = request.Headers.Host;
				}
				else
				{
					if (keyValuePair.Key == "Transfer-Encoding")
					{
						enumerable = from l in enumerable
						where l != "chunked"
						select l;
					}
					string singleHeaderString = PlatformHelper.GetSingleHeaderString(keyValuePair.Key, enumerable);
					if (singleHeaderString != null)
					{
						headers.AddInternal(keyValuePair.Key, singleHeaderString);
					}
				}
			}
			return httpWebRequest;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000327C File Offset: 0x0000147C
		private HttpResponseMessage CreateResponseMessage(HttpWebResponse wr, HttpRequestMessage requestMessage, CancellationToken cancellationToken)
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage(wr.StatusCode);
			httpResponseMessage.RequestMessage = requestMessage;
			httpResponseMessage.ReasonPhrase = wr.StatusDescription;
			httpResponseMessage.Content = PlatformHelper.CreateStreamContent(wr.GetResponseStream(), cancellationToken);
			WebHeaderCollection headers = wr.Headers;
			for (int i = 0; i < headers.Count; i++)
			{
				string key = headers.GetKey(i);
				string[] values = headers.GetValues(i);
				HttpHeaders headers2;
				if (PlatformHelper.IsContentHeader(key))
				{
					headers2 = httpResponseMessage.Content.Headers;
				}
				else
				{
					headers2 = httpResponseMessage.Headers;
				}
				headers2.TryAddWithoutValidation(key, values);
			}
			requestMessage.RequestUri = wr.ResponseUri;
			return httpResponseMessage;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000331C File Offset: 0x0000151C
		private static bool MethodHasBody(HttpMethod method)
		{
			string method2 = method.Method;
			return !(method2 == "HEAD") && !(method2 == "GET") && !(method2 == "MKCOL") && !(method2 == "CONNECT") && !(method2 == "TRACE");
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003374 File Offset: 0x00001574
		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			MonoWebRequestHandler.<SendAsync>d__99 <SendAsync>d__;
			<SendAsync>d__.<>4__this = this;
			<SendAsync>d__.request = request;
			<SendAsync>d__.cancellationToken = cancellationToken;
			<SendAsync>d__.<>t__builder = AsyncTaskMethodBuilder<HttpResponseMessage>.Create();
			<SendAsync>d__.<>1__state = -1;
			<SendAsync>d__.<>t__builder.Start<MonoWebRequestHandler.<SendAsync>d__99>(ref <SendAsync>d__);
			return <SendAsync>d__.<>t__builder.Task;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000033C7 File Offset: 0x000015C7
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000033C7 File Offset: 0x000015C7
		public ICredentials DefaultProxyCredentials
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000033C7 File Offset: 0x000015C7
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000033C7 File Offset: 0x000015C7
		public int MaxConnectionsPerServer
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000033C7 File Offset: 0x000015C7
		public IDictionary<string, object> Properties
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000033CE File Offset: 0x000015CE
		void IMonoHttpClientHandler.SetWebRequestTimeout(TimeSpan timeout)
		{
			this.timeout = new TimeSpan?(timeout);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000033DC File Offset: 0x000015DC
		[CompilerGenerated]
		private X509Certificate <CreateWebRequest>b__96_0(string t, X509CertificateCollection lc, X509Certificate rc, string[] ai)
		{
			return this.SslOptions.LocalCertificateSelectionCallback(this, t, lc, rc, ai);
		}

		// Token: 0x04000020 RID: 32
		private static long groupCounter;

		// Token: 0x04000021 RID: 33
		private bool allowAutoRedirect;

		// Token: 0x04000022 RID: 34
		private DecompressionMethods automaticDecompression;

		// Token: 0x04000023 RID: 35
		private CookieContainer cookieContainer;

		// Token: 0x04000024 RID: 36
		private ICredentials credentials;

		// Token: 0x04000025 RID: 37
		private int maxAutomaticRedirections;

		// Token: 0x04000026 RID: 38
		private long maxRequestContentBufferSize;

		// Token: 0x04000027 RID: 39
		private bool preAuthenticate;

		// Token: 0x04000028 RID: 40
		private IWebProxy proxy;

		// Token: 0x04000029 RID: 41
		private bool useCookies;

		// Token: 0x0400002A RID: 42
		private bool useProxy;

		// Token: 0x0400002B RID: 43
		private SslClientAuthenticationOptions sslOptions;

		// Token: 0x0400002C RID: 44
		private bool allowPipelining;

		// Token: 0x0400002D RID: 45
		private RequestCachePolicy cachePolicy;

		// Token: 0x0400002E RID: 46
		private AuthenticationLevel authenticationLevel;

		// Token: 0x0400002F RID: 47
		private TimeSpan continueTimeout;

		// Token: 0x04000030 RID: 48
		private TokenImpersonationLevel impersonationLevel;

		// Token: 0x04000031 RID: 49
		private int maxResponseHeadersLength;

		// Token: 0x04000032 RID: 50
		private int readWriteTimeout;

		// Token: 0x04000033 RID: 51
		private RemoteCertificateValidationCallback serverCertificateValidationCallback;

		// Token: 0x04000034 RID: 52
		private bool unsafeAuthenticatedConnectionSharing;

		// Token: 0x04000035 RID: 53
		private bool sentRequest;

		// Token: 0x04000036 RID: 54
		private string connectionGroupName;

		// Token: 0x04000037 RID: 55
		private TimeSpan? timeout;

		// Token: 0x04000038 RID: 56
		private bool disposed;

		// Token: 0x0200000F RID: 15
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060000B7 RID: 183 RVA: 0x000033F4 File Offset: 0x000015F4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060000B8 RID: 184 RVA: 0x000022B8 File Offset: 0x000004B8
			public <>c()
			{
			}

			// Token: 0x060000B9 RID: 185 RVA: 0x00003400 File Offset: 0x00001600
			internal bool <GetConnectionKeepAlive>b__95_0(string l)
			{
				return string.Equals(l, "Keep-Alive", StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x060000BA RID: 186 RVA: 0x0000340E File Offset: 0x0000160E
			internal bool <CreateWebRequest>b__96_1(string l)
			{
				return l != "chunked";
			}

			// Token: 0x060000BB RID: 187 RVA: 0x0000341B File Offset: 0x0000161B
			internal void <SendAsync>b__99_0(object l)
			{
				((HttpWebRequest)l).Abort();
			}

			// Token: 0x04000039 RID: 57
			public static readonly MonoWebRequestHandler.<>c <>9 = new MonoWebRequestHandler.<>c();

			// Token: 0x0400003A RID: 58
			public static Func<string, bool> <>9__95_0;

			// Token: 0x0400003B RID: 59
			public static Func<string, bool> <>9__96_1;

			// Token: 0x0400003C RID: 60
			public static Action<object> <>9__99_0;
		}

		// Token: 0x02000010 RID: 16
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SendAsync>d__99 : IAsyncStateMachine
		{
			// Token: 0x060000BC RID: 188 RVA: 0x00003428 File Offset: 0x00001628
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				MonoWebRequestHandler monoWebRequestHandler = this.<>4__this;
				HttpResponseMessage result3;
				try
				{
					TaskAwaiter<HttpResponseMessage> awaiter;
					if (num > 3)
					{
						if (num == 4)
						{
							awaiter = this.<>u__4;
							this.<>u__4 = default(TaskAwaiter<HttpResponseMessage>);
							num = (this.<>1__state = -1);
							goto IL_589;
						}
						if (monoWebRequestHandler.disposed)
						{
							throw new ObjectDisposedException(monoWebRequestHandler.GetType().ToString());
						}
						FieldInfo field = typeof(CancellationToken).GetField("_source", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
						CancellationTokenSource obj = (CancellationTokenSource)field.GetValue(this.cancellationToken);
						field = typeof(CancellationTokenSource).GetField("_timer", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
						Timer timer = (Timer)field.GetValue(obj);
						if (timer != null)
						{
							field = typeof(Timer).GetField("due_time_ms", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
							monoWebRequestHandler.timeout = new TimeSpan?(TimeSpan.FromMilliseconds((double)((long)field.GetValue(timer))));
						}
						Volatile.Write(ref monoWebRequestHandler.sentRequest, true);
						this.<wrequest>5__2 = monoWebRequestHandler.CreateWebRequest(this.request);
						this.<wresponse>5__3 = null;
					}
					try
					{
						if (num > 3)
						{
							this.<>7__wrap3 = this.cancellationToken.Register(new Action<object>(MonoWebRequestHandler.<>c.<>9.<SendAsync>b__99_0), this.<wrequest>5__2);
						}
						try
						{
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
							ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter awaiter3;
							ConfiguredTaskAwaitable<WebResponse>.ConfiguredTaskAwaiter awaiter4;
							switch (num)
							{
							case 0:
								awaiter2 = this.<>u__1;
								this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								num = (this.<>1__state = -1);
								break;
							case 1:
								awaiter3 = this.<>u__2;
								this.<>u__2 = default(ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter);
								num = (this.<>1__state = -1);
								goto IL_378;
							case 2:
								IL_389:
								try
								{
									if (num != 2)
									{
										awaiter2 = this.request.Content.CopyToAsync(this.<stream>5__6).ConfigureAwait(false).GetAwaiter();
										if (!awaiter2.IsCompleted)
										{
											num = (this.<>1__state = 2);
											this.<>u__1 = awaiter2;
											this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, MonoWebRequestHandler.<SendAsync>d__99>(ref awaiter2, ref this);
											return;
										}
									}
									else
									{
										awaiter2 = this.<>u__1;
										this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
										num = (this.<>1__state = -1);
									}
									awaiter2.GetResult();
								}
								finally
								{
									if (num < 0 && this.<stream>5__6 != null)
									{
										((IDisposable)this.<stream>5__6).Dispose();
									}
								}
								this.<stream>5__6 = null;
								goto IL_448;
							case 3:
								awaiter4 = this.<>u__3;
								this.<>u__3 = default(ConfiguredTaskAwaitable<WebResponse>.ConfiguredTaskAwaiter);
								num = (this.<>1__state = -1);
								goto IL_4AE;
							default:
								this.<content>5__5 = this.request.Content;
								if (this.<content>5__5 != null)
								{
									WebHeaderCollection headers = this.<wrequest>5__2.Headers;
									IEnumerator<KeyValuePair<string, IEnumerable<string>>> enumerator = this.<content>5__5.Headers.GetEnumerator();
									try
									{
										while (enumerator.MoveNext())
										{
											KeyValuePair<string, IEnumerable<string>> keyValuePair = enumerator.Current;
											IEnumerator<string> enumerator2 = keyValuePair.Value.GetEnumerator();
											try
											{
												while (enumerator2.MoveNext())
												{
													string value = enumerator2.Current;
													headers.AddInternal(keyValuePair.Key, value);
												}
											}
											finally
											{
												if (num < 0 && enumerator2 != null)
												{
													enumerator2.Dispose();
												}
											}
										}
									}
									finally
									{
										if (num < 0 && enumerator != null)
										{
											enumerator.Dispose();
										}
									}
									bool? transferEncodingChunked = this.request.Headers.TransferEncodingChunked;
									bool flag = true;
									if (transferEncodingChunked.GetValueOrDefault() == flag & transferEncodingChunked != null)
									{
										this.<wrequest>5__2.SendChunked = true;
										goto IL_2F6;
									}
									long? contentLength = this.<content>5__5.Headers.ContentLength;
									if (contentLength != null)
									{
										this.<wrequest>5__2.ContentLength = contentLength.Value;
										goto IL_2F6;
									}
									if (monoWebRequestHandler.MaxRequestContentBufferSize == 0L)
									{
										throw new InvalidOperationException("The content length of the request content can't be determined. Either set TransferEncodingChunked to true, load content into buffer, or set MaxRequestContentBufferSize.");
									}
									awaiter2 = this.<content>5__5.LoadIntoBufferAsync(monoWebRequestHandler.MaxRequestContentBufferSize).ConfigureAwait(false).GetAwaiter();
									if (!awaiter2.IsCompleted)
									{
										num = (this.<>1__state = 0);
										this.<>u__1 = awaiter2;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, MonoWebRequestHandler.<SendAsync>d__99>(ref awaiter2, ref this);
										return;
									}
								}
								else
								{
									if (MonoWebRequestHandler.MethodHasBody(this.request.Method))
									{
										this.<wrequest>5__2.ContentLength = 0L;
										goto IL_448;
									}
									goto IL_448;
								}
								break;
							}
							awaiter2.GetResult();
							this.<wrequest>5__2.ContentLength = this.<content>5__5.Headers.ContentLength.Value;
							IL_2F6:
							this.<wrequest>5__2.ResendContentFactory = new Func<Stream, Task>(this.<content>5__5.CopyToAsync);
							awaiter3 = this.<wrequest>5__2.GetRequestStreamAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter3.IsCompleted)
							{
								num = (this.<>1__state = 1);
								this.<>u__2 = awaiter3;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter, MonoWebRequestHandler.<SendAsync>d__99>(ref awaiter3, ref this);
								return;
							}
							IL_378:
							Stream result = awaiter3.GetResult();
							this.<stream>5__6 = result;
							goto IL_389;
							IL_448:
							awaiter4 = this.<wrequest>5__2.GetResponseAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter4.IsCompleted)
							{
								num = (this.<>1__state = 3);
								this.<>u__3 = awaiter4;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<WebResponse>.ConfiguredTaskAwaiter, MonoWebRequestHandler.<SendAsync>d__99>(ref awaiter4, ref this);
								return;
							}
							IL_4AE:
							WebResponse result2 = awaiter4.GetResult();
							this.<wresponse>5__3 = (HttpWebResponse)result2;
							this.<content>5__5 = null;
						}
						finally
						{
							if (num < 0)
							{
								((IDisposable)this.<>7__wrap3).Dispose();
							}
						}
						this.<>7__wrap3 = default(CancellationTokenRegistration);
					}
					catch (WebException ex)
					{
						if (ex.Status != WebExceptionStatus.RequestCanceled)
						{
							throw new HttpRequestException("An error occurred while sending the request", ex);
						}
					}
					catch (IOException inner)
					{
						throw new HttpRequestException("An error occurred while sending the request", inner);
					}
					if (!this.cancellationToken.IsCancellationRequested)
					{
						result3 = monoWebRequestHandler.CreateResponseMessage(this.<wresponse>5__3, this.request, this.cancellationToken);
						goto IL_5D5;
					}
					TaskCompletionSource<HttpResponseMessage> taskCompletionSource = new TaskCompletionSource<HttpResponseMessage>();
					taskCompletionSource.SetCanceled();
					awaiter = taskCompletionSource.Task.GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (this.<>1__state = 4);
						this.<>u__4 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<HttpResponseMessage>, MonoWebRequestHandler.<SendAsync>d__99>(ref awaiter, ref this);
						return;
					}
					IL_589:
					result3 = awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<wrequest>5__2 = null;
					this.<wresponse>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_5D5:
				this.<>1__state = -2;
				this.<wrequest>5__2 = null;
				this.<wresponse>5__3 = null;
				this.<>t__builder.SetResult(result3);
			}

			// Token: 0x060000BD RID: 189 RVA: 0x00003AD8 File Offset: 0x00001CD8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400003D RID: 61
			public int <>1__state;

			// Token: 0x0400003E RID: 62
			public AsyncTaskMethodBuilder<HttpResponseMessage> <>t__builder;

			// Token: 0x0400003F RID: 63
			public MonoWebRequestHandler <>4__this;

			// Token: 0x04000040 RID: 64
			public CancellationToken cancellationToken;

			// Token: 0x04000041 RID: 65
			public HttpRequestMessage request;

			// Token: 0x04000042 RID: 66
			private HttpWebRequest <wrequest>5__2;

			// Token: 0x04000043 RID: 67
			private HttpWebResponse <wresponse>5__3;

			// Token: 0x04000044 RID: 68
			private CancellationTokenRegistration <>7__wrap3;

			// Token: 0x04000045 RID: 69
			private HttpContent <content>5__5;

			// Token: 0x04000046 RID: 70
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000047 RID: 71
			private Stream <stream>5__6;

			// Token: 0x04000048 RID: 72
			private ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x04000049 RID: 73
			private ConfiguredTaskAwaitable<WebResponse>.ConfiguredTaskAwaiter <>u__3;

			// Token: 0x0400004A RID: 74
			private TaskAwaiter<HttpResponseMessage> <>u__4;
		}
	}
}
