using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using WebSocketSharp.Net;

namespace WebSocketSharp
{
	// Token: 0x02000017 RID: 23
	internal class HttpResponse : HttpBase
	{
		// Token: 0x06000183 RID: 387 RVA: 0x0000ACF7 File Offset: 0x00008EF7
		private HttpResponse(string code, string reason, Version version, NameValueCollection headers) : base(version, headers)
		{
			this._code = code;
			this._reason = reason;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000AD12 File Offset: 0x00008F12
		internal HttpResponse(HttpStatusCode code) : this(code, code.GetDescription())
		{
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000AD24 File Offset: 0x00008F24
		internal HttpResponse(HttpStatusCode code, string reason)
		{
			int num = (int)code;
			this..ctor(num.ToString(), reason, HttpVersion.Version11, new NameValueCollection());
			base.Headers["Server"] = "websocket-sharp/1.0";
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000AD64 File Offset: 0x00008F64
		public CookieCollection Cookies
		{
			get
			{
				return base.Headers.GetCookies(true);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000AD84 File Offset: 0x00008F84
		public bool HasConnectionClose
		{
			get
			{
				StringComparison comparisonTypeForValue = StringComparison.OrdinalIgnoreCase;
				return base.Headers.Contains("Connection", "close", comparisonTypeForValue);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000ADB0 File Offset: 0x00008FB0
		public bool IsProxyAuthenticationRequired
		{
			get
			{
				return this._code == "407";
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000ADD4 File Offset: 0x00008FD4
		public bool IsRedirect
		{
			get
			{
				return this._code == "301" || this._code == "302";
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000AE0C File Offset: 0x0000900C
		public bool IsUnauthorized
		{
			get
			{
				return this._code == "401";
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000AE30 File Offset: 0x00009030
		public bool IsWebSocketResponse
		{
			get
			{
				return base.ProtocolVersion > HttpVersion.Version10 && this._code == "101" && base.Headers.Upgrades("websocket");
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000AE7C File Offset: 0x0000907C
		public string Reason
		{
			get
			{
				return this._reason;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000AE94 File Offset: 0x00009094
		public string StatusCode
		{
			get
			{
				return this._code;
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000AEAC File Offset: 0x000090AC
		internal static HttpResponse CreateCloseResponse(HttpStatusCode code)
		{
			HttpResponse httpResponse = new HttpResponse(code);
			httpResponse.Headers["Connection"] = "close";
			return httpResponse;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000AEDC File Offset: 0x000090DC
		internal static HttpResponse CreateUnauthorizedResponse(string challenge)
		{
			HttpResponse httpResponse = new HttpResponse(HttpStatusCode.Unauthorized);
			httpResponse.Headers["WWW-Authenticate"] = challenge;
			return httpResponse;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000AF0C File Offset: 0x0000910C
		internal static HttpResponse CreateWebSocketResponse()
		{
			HttpResponse httpResponse = new HttpResponse(HttpStatusCode.SwitchingProtocols);
			NameValueCollection headers = httpResponse.Headers;
			headers["Upgrade"] = "websocket";
			headers["Connection"] = "Upgrade";
			return httpResponse;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000AF50 File Offset: 0x00009150
		internal static HttpResponse Parse(string[] headerParts)
		{
			string[] array = headerParts[0].Split(new char[]
			{
				' '
			}, 3);
			bool flag = array.Length != 3;
			if (flag)
			{
				throw new ArgumentException("Invalid status line: " + headerParts[0]);
			}
			WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
			for (int i = 1; i < headerParts.Length; i++)
			{
				webHeaderCollection.InternalSet(headerParts[i], true);
			}
			return new HttpResponse(array[1], array[2], new Version(array[0].Substring(5)), webHeaderCollection);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000AFD8 File Offset: 0x000091D8
		internal static HttpResponse Read(Stream stream, int millisecondsTimeout)
		{
			return HttpBase.Read<HttpResponse>(stream, new Func<string[], HttpResponse>(HttpResponse.Parse), millisecondsTimeout);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000B000 File Offset: 0x00009200
		public void SetCookies(CookieCollection cookies)
		{
			bool flag = cookies == null || cookies.Count == 0;
			if (!flag)
			{
				NameValueCollection headers = base.Headers;
				foreach (Cookie cookie in cookies.Sorted)
				{
					headers.Add("Set-Cookie", cookie.ToResponseString());
				}
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000B078 File Offset: 0x00009278
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.AppendFormat("HTTP/{0} {1} {2}{3}", new object[]
			{
				base.ProtocolVersion,
				this._code,
				this._reason,
				"\r\n"
			});
			NameValueCollection headers = base.Headers;
			foreach (string text in headers.AllKeys)
			{
				stringBuilder.AppendFormat("{0}: {1}{2}", text, headers[text], "\r\n");
			}
			stringBuilder.Append("\r\n");
			string entityBody = base.EntityBody;
			bool flag = entityBody.Length > 0;
			if (flag)
			{
				stringBuilder.Append(entityBody);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000092 RID: 146
		private string _code;

		// Token: 0x04000093 RID: 147
		private string _reason;
	}
}
