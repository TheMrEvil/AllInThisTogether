using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x0200000D RID: 13
	internal interface IMonoHttpClientHandler : IDisposable
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005C RID: 92
		bool SupportsAutomaticDecompression { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005D RID: 93
		// (set) Token: 0x0600005E RID: 94
		bool UseCookies { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005F RID: 95
		// (set) Token: 0x06000060 RID: 96
		CookieContainer CookieContainer { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000061 RID: 97
		// (set) Token: 0x06000062 RID: 98
		SslClientAuthenticationOptions SslOptions { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000063 RID: 99
		// (set) Token: 0x06000064 RID: 100
		DecompressionMethods AutomaticDecompression { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000065 RID: 101
		// (set) Token: 0x06000066 RID: 102
		bool UseProxy { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000067 RID: 103
		// (set) Token: 0x06000068 RID: 104
		IWebProxy Proxy { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000069 RID: 105
		// (set) Token: 0x0600006A RID: 106
		ICredentials DefaultProxyCredentials { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006B RID: 107
		// (set) Token: 0x0600006C RID: 108
		bool PreAuthenticate { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006D RID: 109
		// (set) Token: 0x0600006E RID: 110
		ICredentials Credentials { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006F RID: 111
		// (set) Token: 0x06000070 RID: 112
		bool AllowAutoRedirect { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000071 RID: 113
		// (set) Token: 0x06000072 RID: 114
		int MaxAutomaticRedirections { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000073 RID: 115
		// (set) Token: 0x06000074 RID: 116
		int MaxConnectionsPerServer { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000075 RID: 117
		// (set) Token: 0x06000076 RID: 118
		int MaxResponseHeadersLength { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000077 RID: 119
		// (set) Token: 0x06000078 RID: 120
		long MaxRequestContentBufferSize { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000079 RID: 121
		IDictionary<string, object> Properties { get; }

		// Token: 0x0600007A RID: 122
		Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);

		// Token: 0x0600007B RID: 123
		void SetWebRequestTimeout(TimeSpan timeout);
	}
}
