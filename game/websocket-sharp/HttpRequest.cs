using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using WebSocketSharp.Net;

namespace WebSocketSharp
{
	// Token: 0x02000016 RID: 22
	internal class HttpRequest : HttpBase
	{
		// Token: 0x06000175 RID: 373 RVA: 0x0000A856 File Offset: 0x00008A56
		private HttpRequest(string method, string uri, Version version, NameValueCollection headers) : base(version, headers)
		{
			this._method = method;
			this._uri = uri;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000A871 File Offset: 0x00008A71
		internal HttpRequest(string method, string uri) : this(method, uri, HttpVersion.Version11, new NameValueCollection())
		{
			base.Headers["User-Agent"] = "websocket-sharp/1.0";
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000A8A0 File Offset: 0x00008AA0
		public AuthenticationResponse AuthenticationResponse
		{
			get
			{
				string text = base.Headers["Authorization"];
				return (text != null && text.Length > 0) ? AuthenticationResponse.Parse(text) : null;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000A8D8 File Offset: 0x00008AD8
		public CookieCollection Cookies
		{
			get
			{
				bool flag = this._cookies == null;
				if (flag)
				{
					this._cookies = base.Headers.GetCookies(false);
				}
				return this._cookies;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000A910 File Offset: 0x00008B10
		public string HttpMethod
		{
			get
			{
				return this._method;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000A928 File Offset: 0x00008B28
		public bool IsWebSocketRequest
		{
			get
			{
				return this._method == "GET" && base.ProtocolVersion > HttpVersion.Version10 && base.Headers.Upgrades("websocket");
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000A974 File Offset: 0x00008B74
		public string RequestUri
		{
			get
			{
				return this._uri;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000A98C File Offset: 0x00008B8C
		internal static HttpRequest CreateConnectRequest(Uri uri)
		{
			string dnsSafeHost = uri.DnsSafeHost;
			int port = uri.Port;
			string text = string.Format("{0}:{1}", dnsSafeHost, port);
			HttpRequest httpRequest = new HttpRequest("CONNECT", text);
			httpRequest.Headers["Host"] = ((port == 80) ? dnsSafeHost : text);
			return httpRequest;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000A9E8 File Offset: 0x00008BE8
		internal static HttpRequest CreateWebSocketRequest(Uri uri)
		{
			HttpRequest httpRequest = new HttpRequest("GET", uri.PathAndQuery);
			NameValueCollection headers = httpRequest.Headers;
			int port = uri.Port;
			string scheme = uri.Scheme;
			headers["Host"] = (((port == 80 && scheme == "ws") || (port == 443 && scheme == "wss")) ? uri.DnsSafeHost : uri.Authority);
			headers["Upgrade"] = "websocket";
			headers["Connection"] = "Upgrade";
			return httpRequest;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000AA88 File Offset: 0x00008C88
		internal HttpResponse GetResponse(Stream stream, int millisecondsTimeout)
		{
			byte[] array = base.ToByteArray();
			stream.Write(array, 0, array.Length);
			return HttpBase.Read<HttpResponse>(stream, new Func<string[], HttpResponse>(HttpResponse.Parse), millisecondsTimeout);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		internal static HttpRequest Parse(string[] headerParts)
		{
			string[] array = headerParts[0].Split(new char[]
			{
				' '
			}, 3);
			bool flag = array.Length != 3;
			if (flag)
			{
				throw new ArgumentException("Invalid request line: " + headerParts[0]);
			}
			WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
			for (int i = 1; i < headerParts.Length; i++)
			{
				webHeaderCollection.InternalSet(headerParts[i], false);
			}
			return new HttpRequest(array[0], array[1], new Version(array[2].Substring(5)), webHeaderCollection);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000AB48 File Offset: 0x00008D48
		internal static HttpRequest Read(Stream stream, int millisecondsTimeout)
		{
			return HttpBase.Read<HttpRequest>(stream, new Func<string[], HttpRequest>(HttpRequest.Parse), millisecondsTimeout);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000AB70 File Offset: 0x00008D70
		public void SetCookies(CookieCollection cookies)
		{
			bool flag = cookies == null || cookies.Count == 0;
			if (!flag)
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				foreach (Cookie cookie in cookies.Sorted)
				{
					bool flag2 = !cookie.Expired;
					if (flag2)
					{
						stringBuilder.AppendFormat("{0}; ", cookie.ToString());
					}
				}
				int length = stringBuilder.Length;
				bool flag3 = length > 2;
				if (flag3)
				{
					stringBuilder.Length = length - 2;
					base.Headers["Cookie"] = stringBuilder.ToString();
				}
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000AC34 File Offset: 0x00008E34
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.AppendFormat("{0} {1} HTTP/{2}{3}", new object[]
			{
				this._method,
				this._uri,
				base.ProtocolVersion,
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

		// Token: 0x0400008F RID: 143
		private CookieCollection _cookies;

		// Token: 0x04000090 RID: 144
		private string _method;

		// Token: 0x04000091 RID: 145
		private string _uri;
	}
}
