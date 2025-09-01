using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace WebSocketSharp.Net
{
	// Token: 0x02000025 RID: 37
	public sealed class HttpListenerResponse : IDisposable
	{
		// Token: 0x060002AC RID: 684 RVA: 0x00010E96 File Offset: 0x0000F096
		internal HttpListenerResponse(HttpListenerContext context)
		{
			this._context = context;
			this._keepAlive = true;
			this._statusCode = 200;
			this._statusDescription = "OK";
			this._version = HttpVersion.Version11;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00010ED0 File Offset: 0x0000F0D0
		// (set) Token: 0x060002AE RID: 686 RVA: 0x00010EE8 File Offset: 0x0000F0E8
		internal bool CloseConnection
		{
			get
			{
				return this._closeConnection;
			}
			set
			{
				this._closeConnection = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00010EF4 File Offset: 0x0000F0F4
		internal WebHeaderCollection FullHeaders
		{
			get
			{
				WebHeaderCollection webHeaderCollection = new WebHeaderCollection(HttpHeaderType.Response, true);
				bool flag = this._headers != null;
				if (flag)
				{
					webHeaderCollection.Add(this._headers);
				}
				bool flag2 = this._contentType != null;
				if (flag2)
				{
					webHeaderCollection.InternalSet("Content-Type", HttpListenerResponse.createContentTypeHeaderText(this._contentType, this._contentEncoding), true);
				}
				bool flag3 = webHeaderCollection["Server"] == null;
				if (flag3)
				{
					webHeaderCollection.InternalSet("Server", "websocket-sharp/1.0", true);
				}
				bool flag4 = webHeaderCollection["Date"] == null;
				if (flag4)
				{
					webHeaderCollection.InternalSet("Date", DateTime.UtcNow.ToString("r", CultureInfo.InvariantCulture), true);
				}
				bool sendChunked = this._sendChunked;
				if (sendChunked)
				{
					webHeaderCollection.InternalSet("Transfer-Encoding", "chunked", true);
				}
				else
				{
					webHeaderCollection.InternalSet("Content-Length", this._contentLength.ToString(CultureInfo.InvariantCulture), true);
				}
				bool flag5 = !this._context.Request.KeepAlive || !this._keepAlive || this._statusCode == 400 || this._statusCode == 408 || this._statusCode == 411 || this._statusCode == 413 || this._statusCode == 414 || this._statusCode == 500 || this._statusCode == 503;
				int reuses = this._context.Connection.Reuses;
				bool flag6 = flag5 || reuses >= 100;
				if (flag6)
				{
					webHeaderCollection.InternalSet("Connection", "close", true);
				}
				else
				{
					webHeaderCollection.InternalSet("Keep-Alive", string.Format("timeout=15,max={0}", 100 - reuses), true);
					bool flag7 = this._context.Request.ProtocolVersion < HttpVersion.Version11;
					if (flag7)
					{
						webHeaderCollection.InternalSet("Connection", "keep-alive", true);
					}
				}
				bool flag8 = this._redirectLocation != null;
				if (flag8)
				{
					webHeaderCollection.InternalSet("Location", this._redirectLocation.AbsoluteUri, true);
				}
				bool flag9 = this._cookies != null;
				if (flag9)
				{
					foreach (Cookie cookie in this._cookies)
					{
						webHeaderCollection.InternalSet("Set-Cookie", cookie.ToResponseString(), true);
					}
				}
				return webHeaderCollection;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00011198 File Offset: 0x0000F398
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x000111B0 File Offset: 0x0000F3B0
		internal bool HeadersSent
		{
			get
			{
				return this._headersSent;
			}
			set
			{
				this._headersSent = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x000111BC File Offset: 0x0000F3BC
		internal string StatusLine
		{
			get
			{
				return string.Format("HTTP/{0} {1} {2}\r\n", this._version, this._statusCode, this._statusDescription);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x000111F0 File Offset: 0x0000F3F0
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x00011208 File Offset: 0x0000F408
		public Encoding ContentEncoding
		{
			get
			{
				return this._contentEncoding;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					string objectName = base.GetType().ToString();
					throw new ObjectDisposedException(objectName);
				}
				bool headersSent = this._headersSent;
				if (headersSent)
				{
					string message = "The response is already being sent.";
					throw new InvalidOperationException(message);
				}
				this._contentEncoding = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00011254 File Offset: 0x0000F454
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0001126C File Offset: 0x0000F46C
		public long ContentLength64
		{
			get
			{
				return this._contentLength;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					string objectName = base.GetType().ToString();
					throw new ObjectDisposedException(objectName);
				}
				bool headersSent = this._headersSent;
				if (headersSent)
				{
					string message = "The response is already being sent.";
					throw new InvalidOperationException(message);
				}
				bool flag = value < 0L;
				if (flag)
				{
					string paramName = "Less than zero.";
					throw new ArgumentOutOfRangeException(paramName, "value");
				}
				this._contentLength = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x000112D8 File Offset: 0x0000F4D8
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x000112F0 File Offset: 0x0000F4F0
		public string ContentType
		{
			get
			{
				return this._contentType;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					string objectName = base.GetType().ToString();
					throw new ObjectDisposedException(objectName);
				}
				bool headersSent = this._headersSent;
				if (headersSent)
				{
					string message = "The response is already being sent.";
					throw new InvalidOperationException(message);
				}
				bool flag = value == null;
				if (flag)
				{
					this._contentType = null;
				}
				else
				{
					bool flag2 = value.Length == 0;
					if (flag2)
					{
						string message2 = "An empty string.";
						throw new ArgumentException(message2, "value");
					}
					bool flag3 = !HttpListenerResponse.isValidForContentType(value);
					if (flag3)
					{
						string message3 = "It contains an invalid character.";
						throw new ArgumentException(message3, "value");
					}
					this._contentType = value;
				}
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00011398 File Offset: 0x0000F598
		// (set) Token: 0x060002BA RID: 698 RVA: 0x000113C8 File Offset: 0x0000F5C8
		public CookieCollection Cookies
		{
			get
			{
				bool flag = this._cookies == null;
				if (flag)
				{
					this._cookies = new CookieCollection();
				}
				return this._cookies;
			}
			set
			{
				this._cookies = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002BB RID: 699 RVA: 0x000113D4 File Offset: 0x0000F5D4
		// (set) Token: 0x060002BC RID: 700 RVA: 0x00011408 File Offset: 0x0000F608
		public WebHeaderCollection Headers
		{
			get
			{
				bool flag = this._headers == null;
				if (flag)
				{
					this._headers = new WebHeaderCollection(HttpHeaderType.Response, false);
				}
				return this._headers;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this._headers = null;
				}
				else
				{
					bool flag2 = value.State != HttpHeaderType.Response;
					if (flag2)
					{
						string message = "The value is not valid for a response.";
						throw new InvalidOperationException(message);
					}
					this._headers = value;
				}
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00011450 File Offset: 0x0000F650
		// (set) Token: 0x060002BE RID: 702 RVA: 0x00011468 File Offset: 0x0000F668
		public bool KeepAlive
		{
			get
			{
				return this._keepAlive;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					string objectName = base.GetType().ToString();
					throw new ObjectDisposedException(objectName);
				}
				bool headersSent = this._headersSent;
				if (headersSent)
				{
					string message = "The response is already being sent.";
					throw new InvalidOperationException(message);
				}
				this._keepAlive = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002BF RID: 703 RVA: 0x000114B4 File Offset: 0x0000F6B4
		public Stream OutputStream
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					string objectName = base.GetType().ToString();
					throw new ObjectDisposedException(objectName);
				}
				bool flag = this._outputStream == null;
				if (flag)
				{
					this._outputStream = this._context.Connection.GetResponseStream();
				}
				return this._outputStream;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00011510 File Offset: 0x0000F710
		public Version ProtocolVersion
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00011528 File Offset: 0x0000F728
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x00011558 File Offset: 0x0000F758
		public string RedirectLocation
		{
			get
			{
				return (this._redirectLocation != null) ? this._redirectLocation.OriginalString : null;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					string objectName = base.GetType().ToString();
					throw new ObjectDisposedException(objectName);
				}
				bool headersSent = this._headersSent;
				if (headersSent)
				{
					string message = "The response is already being sent.";
					throw new InvalidOperationException(message);
				}
				bool flag = value == null;
				if (flag)
				{
					this._redirectLocation = null;
				}
				else
				{
					bool flag2 = value.Length == 0;
					if (flag2)
					{
						string message2 = "An empty string.";
						throw new ArgumentException(message2, "value");
					}
					Uri redirectLocation;
					bool flag3 = !Uri.TryCreate(value, UriKind.Absolute, out redirectLocation);
					if (flag3)
					{
						string message3 = "Not an absolute URL.";
						throw new ArgumentException(message3, "value");
					}
					this._redirectLocation = redirectLocation;
				}
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00011604 File Offset: 0x0000F804
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0001161C File Offset: 0x0000F81C
		public bool SendChunked
		{
			get
			{
				return this._sendChunked;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					string objectName = base.GetType().ToString();
					throw new ObjectDisposedException(objectName);
				}
				bool headersSent = this._headersSent;
				if (headersSent)
				{
					string message = "The response is already being sent.";
					throw new InvalidOperationException(message);
				}
				this._sendChunked = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00011668 File Offset: 0x0000F868
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x00011680 File Offset: 0x0000F880
		public int StatusCode
		{
			get
			{
				return this._statusCode;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					string objectName = base.GetType().ToString();
					throw new ObjectDisposedException(objectName);
				}
				bool headersSent = this._headersSent;
				if (headersSent)
				{
					string message = "The response is already being sent.";
					throw new InvalidOperationException(message);
				}
				bool flag = value < 100 || value > 999;
				if (flag)
				{
					string message2 = "A value is not between 100 and 999 inclusive.";
					throw new ProtocolViolationException(message2);
				}
				this._statusCode = value;
				this._statusDescription = value.GetStatusDescription();
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00011700 File Offset: 0x0000F900
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00011718 File Offset: 0x0000F918
		public string StatusDescription
		{
			get
			{
				return this._statusDescription;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					string objectName = base.GetType().ToString();
					throw new ObjectDisposedException(objectName);
				}
				bool headersSent = this._headersSent;
				if (headersSent)
				{
					string message = "The response is already being sent.";
					throw new InvalidOperationException(message);
				}
				bool flag = value == null;
				if (flag)
				{
					throw new ArgumentNullException("value");
				}
				bool flag2 = value.Length == 0;
				if (flag2)
				{
					this._statusDescription = this._statusCode.GetStatusDescription();
				}
				else
				{
					bool flag3 = !HttpListenerResponse.isValidForStatusDescription(value);
					if (flag3)
					{
						string message2 = "It contains an invalid character.";
						throw new ArgumentException(message2, "value");
					}
					this._statusDescription = value;
				}
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000117C0 File Offset: 0x0000F9C0
		private bool canSetCookie(Cookie cookie)
		{
			List<Cookie> list = this.findCookie(cookie).ToList<Cookie>();
			bool flag = list.Count == 0;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				int version = cookie.Version;
				foreach (Cookie cookie2 in list)
				{
					bool flag2 = cookie2.Version == version;
					if (flag2)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0001184C File Offset: 0x0000FA4C
		private void close(bool force)
		{
			this._disposed = true;
			this._context.Connection.Close(force);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00011868 File Offset: 0x0000FA68
		private void close(byte[] responseEntity, int bufferLength, bool willBlock)
		{
			Stream outputStream = this.OutputStream;
			if (willBlock)
			{
				outputStream.WriteBytes(responseEntity, bufferLength);
				this.close(false);
			}
			else
			{
				outputStream.WriteBytesAsync(responseEntity, bufferLength, delegate
				{
					this.close(false);
				}, null);
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000118AC File Offset: 0x0000FAAC
		private static string createContentTypeHeaderText(string value, Encoding encoding)
		{
			bool flag = value.IndexOf("charset=", StringComparison.Ordinal) > -1;
			string result;
			if (flag)
			{
				result = value;
			}
			else
			{
				bool flag2 = encoding == null;
				if (flag2)
				{
					result = value;
				}
				else
				{
					result = string.Format("{0}; charset={1}", value, encoding.WebName);
				}
			}
			return result;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000118F2 File Offset: 0x0000FAF2
		private IEnumerable<Cookie> findCookie(Cookie cookie)
		{
			bool flag = this._cookies == null || this._cookies.Count == 0;
			if (flag)
			{
				yield break;
			}
			foreach (Cookie c in this._cookies)
			{
				bool flag2 = c.EqualsWithoutValueAndVersion(cookie);
				if (flag2)
				{
					yield return c;
				}
				c = null;
			}
			IEnumerator<Cookie> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0001190C File Offset: 0x0000FB0C
		private static bool isValidForContentType(string value)
		{
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				bool flag = c < ' ';
				bool result;
				if (flag)
				{
					result = false;
				}
				else
				{
					bool flag2 = c > '~';
					if (flag2)
					{
						result = false;
					}
					else
					{
						bool flag3 = "()<>@:\\[]?{}".IndexOf(c) > -1;
						if (!flag3)
						{
							i++;
							continue;
						}
						result = false;
					}
				}
				return result;
			}
			return true;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00011978 File Offset: 0x0000FB78
		private static bool isValidForStatusDescription(string value)
		{
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				bool flag = c < ' ';
				bool result;
				if (flag)
				{
					result = false;
				}
				else
				{
					bool flag2 = c > '~';
					if (!flag2)
					{
						i++;
						continue;
					}
					result = false;
				}
				return result;
			}
			return true;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000119CC File Offset: 0x0000FBCC
		public void Abort()
		{
			bool disposed = this._disposed;
			if (!disposed)
			{
				this.close(true);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000119EE File Offset: 0x0000FBEE
		public void AppendCookie(Cookie cookie)
		{
			this.Cookies.Add(cookie);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000119FE File Offset: 0x0000FBFE
		public void AppendHeader(string name, string value)
		{
			this.Headers.Add(name, value);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00011A10 File Offset: 0x0000FC10
		public void Close()
		{
			bool disposed = this._disposed;
			if (!disposed)
			{
				this.close(false);
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00011A34 File Offset: 0x0000FC34
		public void Close(byte[] responseEntity, bool willBlock)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				string objectName = base.GetType().ToString();
				throw new ObjectDisposedException(objectName);
			}
			bool flag = responseEntity == null;
			if (flag)
			{
				throw new ArgumentNullException("responseEntity");
			}
			long num = (long)responseEntity.Length;
			bool flag2 = num > 2147483647L;
			if (flag2)
			{
				this.close(responseEntity, 1024, willBlock);
			}
			else
			{
				Stream stream = this.OutputStream;
				if (willBlock)
				{
					stream.Write(responseEntity, 0, (int)num);
					this.close(false);
				}
				else
				{
					stream.BeginWrite(responseEntity, 0, (int)num, delegate(IAsyncResult ar)
					{
						stream.EndWrite(ar);
						this.close(false);
					}, null);
				}
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00011AF4 File Offset: 0x0000FCF4
		public void CopyFrom(HttpListenerResponse templateResponse)
		{
			bool flag = templateResponse == null;
			if (flag)
			{
				throw new ArgumentNullException("templateResponse");
			}
			WebHeaderCollection headers = templateResponse._headers;
			bool flag2 = headers != null;
			if (flag2)
			{
				bool flag3 = this._headers != null;
				if (flag3)
				{
					this._headers.Clear();
				}
				this.Headers.Add(headers);
			}
			else
			{
				this._headers = null;
			}
			this._contentLength = templateResponse._contentLength;
			this._statusCode = templateResponse._statusCode;
			this._statusDescription = templateResponse._statusDescription;
			this._keepAlive = templateResponse._keepAlive;
			this._version = templateResponse._version;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00011B94 File Offset: 0x0000FD94
		public void Redirect(string url)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				string objectName = base.GetType().ToString();
				throw new ObjectDisposedException(objectName);
			}
			bool headersSent = this._headersSent;
			if (headersSent)
			{
				string message = "The response is already being sent.";
				throw new InvalidOperationException(message);
			}
			bool flag = url == null;
			if (flag)
			{
				throw new ArgumentNullException("url");
			}
			bool flag2 = url.Length == 0;
			if (flag2)
			{
				string message2 = "An empty string.";
				throw new ArgumentException(message2, "url");
			}
			Uri redirectLocation;
			bool flag3 = !Uri.TryCreate(url, UriKind.Absolute, out redirectLocation);
			if (flag3)
			{
				string message3 = "Not an absolute URL.";
				throw new ArgumentException(message3, "url");
			}
			this._redirectLocation = redirectLocation;
			this._statusCode = 302;
			this._statusDescription = "Found";
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00011C58 File Offset: 0x0000FE58
		public void SetCookie(Cookie cookie)
		{
			bool flag = cookie == null;
			if (flag)
			{
				throw new ArgumentNullException("cookie");
			}
			bool flag2 = !this.canSetCookie(cookie);
			if (flag2)
			{
				string message = "It cannot be updated.";
				throw new ArgumentException(message, "cookie");
			}
			this.Cookies.Add(cookie);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00011CA7 File Offset: 0x0000FEA7
		public void SetHeader(string name, string value)
		{
			this.Headers.Set(name, value);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00011CB8 File Offset: 0x0000FEB8
		void IDisposable.Dispose()
		{
			bool disposed = this._disposed;
			if (!disposed)
			{
				this.close(true);
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00011CDA File Offset: 0x0000FEDA
		[CompilerGenerated]
		private void <close>b__63_0()
		{
			this.close(false);
		}

		// Token: 0x0400010B RID: 267
		private bool _closeConnection;

		// Token: 0x0400010C RID: 268
		private Encoding _contentEncoding;

		// Token: 0x0400010D RID: 269
		private long _contentLength;

		// Token: 0x0400010E RID: 270
		private string _contentType;

		// Token: 0x0400010F RID: 271
		private HttpListenerContext _context;

		// Token: 0x04000110 RID: 272
		private CookieCollection _cookies;

		// Token: 0x04000111 RID: 273
		private bool _disposed;

		// Token: 0x04000112 RID: 274
		private WebHeaderCollection _headers;

		// Token: 0x04000113 RID: 275
		private bool _headersSent;

		// Token: 0x04000114 RID: 276
		private bool _keepAlive;

		// Token: 0x04000115 RID: 277
		private ResponseStream _outputStream;

		// Token: 0x04000116 RID: 278
		private Uri _redirectLocation;

		// Token: 0x04000117 RID: 279
		private bool _sendChunked;

		// Token: 0x04000118 RID: 280
		private int _statusCode;

		// Token: 0x04000119 RID: 281
		private string _statusDescription;

		// Token: 0x0400011A RID: 282
		private Version _version;

		// Token: 0x0200006D RID: 109
		[CompilerGenerated]
		private sealed class <findCookie>d__65 : IEnumerable<Cookie>, IEnumerable, IEnumerator<Cookie>, IDisposable, IEnumerator
		{
			// Token: 0x060005B5 RID: 1461 RVA: 0x0001ECBD File Offset: 0x0001CEBD
			[DebuggerHidden]
			public <findCookie>d__65(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x060005B6 RID: 1462 RVA: 0x0001ECE0 File Offset: 0x0001CEE0
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x060005B7 RID: 1463 RVA: 0x0001ED20 File Offset: 0x0001CF20
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
					}
					else
					{
						this.<>1__state = -1;
						bool flag = this._cookies == null || this._cookies.Count == 0;
						if (flag)
						{
							return false;
						}
						enumerator = this._cookies.GetEnumerator();
						this.<>1__state = -3;
						goto IL_C5;
					}
					IL_BD:
					c = null;
					IL_C5:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						result = false;
					}
					else
					{
						c = enumerator.Current;
						bool flag2 = c.EqualsWithoutValueAndVersion(cookie);
						if (!flag2)
						{
							goto IL_BD;
						}
						this.<>2__current = c;
						this.<>1__state = 1;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x060005B8 RID: 1464 RVA: 0x0001EE2C File Offset: 0x0001D02C
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0001EE49 File Offset: 0x0001D049
			Cookie IEnumerator<Cookie>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x0001E136 File Offset: 0x0001C336
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001AF RID: 431
			// (get) Token: 0x060005BB RID: 1467 RVA: 0x0001EE49 File Offset: 0x0001D049
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060005BC RID: 1468 RVA: 0x0001EE54 File Offset: 0x0001D054
			[DebuggerHidden]
			IEnumerator<Cookie> IEnumerable<Cookie>.GetEnumerator()
			{
				HttpListenerResponse.<findCookie>d__65 <findCookie>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<findCookie>d__ = this;
				}
				else
				{
					<findCookie>d__ = new HttpListenerResponse.<findCookie>d__65(0);
					<findCookie>d__.<>4__this = this;
				}
				<findCookie>d__.cookie = cookie;
				return <findCookie>d__;
			}

			// Token: 0x060005BD RID: 1469 RVA: 0x0001EEA8 File Offset: 0x0001D0A8
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<WebSocketSharp.Net.Cookie>.GetEnumerator();
			}

			// Token: 0x040002D3 RID: 723
			private int <>1__state;

			// Token: 0x040002D4 RID: 724
			private Cookie <>2__current;

			// Token: 0x040002D5 RID: 725
			private int <>l__initialThreadId;

			// Token: 0x040002D6 RID: 726
			private Cookie cookie;

			// Token: 0x040002D7 RID: 727
			public Cookie <>3__cookie;

			// Token: 0x040002D8 RID: 728
			public HttpListenerResponse <>4__this;

			// Token: 0x040002D9 RID: 729
			private IEnumerator<Cookie> <>s__1;

			// Token: 0x040002DA RID: 730
			private Cookie <c>5__2;
		}

		// Token: 0x0200006E RID: 110
		[CompilerGenerated]
		private sealed class <>c__DisplayClass72_0
		{
			// Token: 0x060005BE RID: 1470 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass72_0()
			{
			}

			// Token: 0x060005BF RID: 1471 RVA: 0x0001EEB0 File Offset: 0x0001D0B0
			internal void <Close>b__0(IAsyncResult ar)
			{
				this.stream.EndWrite(ar);
				this.<>4__this.close(false);
			}

			// Token: 0x040002DB RID: 731
			public Stream stream;

			// Token: 0x040002DC RID: 732
			public HttpListenerResponse <>4__this;
		}
	}
}
