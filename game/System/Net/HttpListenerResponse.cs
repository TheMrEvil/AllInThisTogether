using System;
using System.Globalization;
using System.IO;
using System.Text;
using Unity;

namespace System.Net
{
	/// <summary>Represents a response to a request being handled by an <see cref="T:System.Net.HttpListener" /> object.</summary>
	// Token: 0x02000690 RID: 1680
	public sealed class HttpListenerResponse : IDisposable
	{
		// Token: 0x0600352E RID: 13614 RVA: 0x000B9F4C File Offset: 0x000B814C
		internal HttpListenerResponse(HttpListenerContext context)
		{
			this.headers = new WebHeaderCollection();
			this.keep_alive = true;
			this.version = HttpVersion.Version11;
			this.status_code = 200;
			this.status_description = "OK";
			this.headers_lock = new object();
			base..ctor();
			this.context = context;
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x0600352F RID: 13615 RVA: 0x000B9FA4 File Offset: 0x000B81A4
		internal bool ForceCloseChunked
		{
			get
			{
				return this.force_close_chunked;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Text.Encoding" /> for this response's <see cref="P:System.Net.HttpListenerResponse.OutputStream" />.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object suitable for use with the data in the <see cref="P:System.Net.HttpListenerResponse.OutputStream" /> property, or <see langword="null" /> if no encoding is specified.</returns>
		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06003530 RID: 13616 RVA: 0x000B9FAC File Offset: 0x000B81AC
		// (set) Token: 0x06003531 RID: 13617 RVA: 0x000B9FC7 File Offset: 0x000B81C7
		public Encoding ContentEncoding
		{
			get
			{
				if (this.content_encoding == null)
				{
					this.content_encoding = Encoding.Default;
				}
				return this.content_encoding;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.content_encoding = value;
			}
		}

		/// <summary>Gets or sets the number of bytes in the body data included in the response.</summary>
		/// <returns>The value of the response's <see langword="Content-Length" /> header.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The response is already being sent.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06003532 RID: 13618 RVA: 0x000B9FFC File Offset: 0x000B81FC
		// (set) Token: 0x06003533 RID: 13619 RVA: 0x000BA004 File Offset: 0x000B8204
		public long ContentLength64
		{
			get
			{
				return this.content_length;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("Must be >= 0", "value");
				}
				this.cl_set = true;
				this.content_length = value;
			}
		}

		/// <summary>Gets or sets the MIME type of the content returned.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the text of the response's <see langword="Content-Type" /> header.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is an empty string ("").</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06003534 RID: 13620 RVA: 0x000BA060 File Offset: 0x000B8260
		// (set) Token: 0x06003535 RID: 13621 RVA: 0x000BA068 File Offset: 0x000B8268
		public string ContentType
		{
			get
			{
				return this.content_type;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.content_type = value;
			}
		}

		/// <summary>Gets or sets the collection of cookies returned with the response.</summary>
		/// <returns>A <see cref="T:System.Net.CookieCollection" /> that contains cookies to accompany the response. The collection is empty if no cookies have been added to the response.</returns>
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06003536 RID: 13622 RVA: 0x000BA09D File Offset: 0x000B829D
		// (set) Token: 0x06003537 RID: 13623 RVA: 0x000BA0B8 File Offset: 0x000B82B8
		public CookieCollection Cookies
		{
			get
			{
				if (this.cookies == null)
				{
					this.cookies = new CookieCollection();
				}
				return this.cookies;
			}
			set
			{
				this.cookies = value;
			}
		}

		/// <summary>Gets or sets the collection of header name/value pairs returned by the server.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> instance that contains all the explicitly set HTTP headers to be included in the response.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.WebHeaderCollection" /> instance specified for a set operation is not valid for a response.</exception>
		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06003538 RID: 13624 RVA: 0x000BA0C1 File Offset: 0x000B82C1
		// (set) Token: 0x06003539 RID: 13625 RVA: 0x000BA0C9 File Offset: 0x000B82C9
		public WebHeaderCollection Headers
		{
			get
			{
				return this.headers;
			}
			set
			{
				this.headers = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the server requests a persistent connection.</summary>
		/// <returns>
		///   <see langword="true" /> if the server requests a persistent connection; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x0600353A RID: 13626 RVA: 0x000BA0D2 File Offset: 0x000B82D2
		// (set) Token: 0x0600353B RID: 13627 RVA: 0x000BA0DA File Offset: 0x000B82DA
		public bool KeepAlive
		{
			get
			{
				return this.keep_alive;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.keep_alive = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object to which a response can be written.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object to which a response can be written.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x0600353C RID: 13628 RVA: 0x000BA10F File Offset: 0x000B830F
		public Stream OutputStream
		{
			get
			{
				if (this.output_stream == null)
				{
					this.output_stream = this.context.Connection.GetResponseStream();
				}
				return this.output_stream;
			}
		}

		/// <summary>Gets or sets the HTTP version used for the response.</summary>
		/// <returns>A <see cref="T:System.Version" /> object indicating the version of HTTP used when responding to the client. Note that this property is now obsolete.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation does not have its <see cref="P:System.Version.Major" /> property set to 1 or does not have its <see cref="P:System.Version.Minor" /> property set to either 0 or 1.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x0600353D RID: 13629 RVA: 0x000BA135 File Offset: 0x000B8335
		// (set) Token: 0x0600353E RID: 13630 RVA: 0x000BA140 File Offset: 0x000B8340
		public Version ProtocolVersion
		{
			get
			{
				return this.version;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Major != 1 || (value.Minor != 0 && value.Minor != 1))
				{
					throw new ArgumentException("Must be 1.0 or 1.1", "value");
				}
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				this.version = value;
			}
		}

		/// <summary>Gets or sets the value of the HTTP <see langword="Location" /> header in this response.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the absolute URL to be sent to the client in the <see langword="Location" /> header.</returns>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is an empty string ("").</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x0600353F RID: 13631 RVA: 0x000BA1D7 File Offset: 0x000B83D7
		// (set) Token: 0x06003540 RID: 13632 RVA: 0x000BA1DF File Offset: 0x000B83DF
		public string RedirectLocation
		{
			get
			{
				return this.location;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.location = value;
			}
		}

		/// <summary>Gets or sets whether the response uses chunked transfer encoding.</summary>
		/// <returns>
		///   <see langword="true" /> if the response is set to use chunked transfer encoding; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06003541 RID: 13633 RVA: 0x000BA214 File Offset: 0x000B8414
		// (set) Token: 0x06003542 RID: 13634 RVA: 0x000BA21C File Offset: 0x000B841C
		public bool SendChunked
		{
			get
			{
				return this.chunked;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.chunked = value;
			}
		}

		/// <summary>Gets or sets the HTTP status code to be returned to the client.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that specifies the HTTP status code for the requested resource. The default is <see cref="F:System.Net.HttpStatusCode.OK" />, indicating that the server successfully processed the client's request and included the requested resource in the response body.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">The value specified for a set operation is not valid. Valid values are between 100 and 999 inclusive.</exception>
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06003543 RID: 13635 RVA: 0x000BA251 File Offset: 0x000B8451
		// (set) Token: 0x06003544 RID: 13636 RVA: 0x000BA25C File Offset: 0x000B845C
		public int StatusCode
		{
			get
			{
				return this.status_code;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				if (value < 100 || value > 999)
				{
					throw new ProtocolViolationException("StatusCode must be between 100 and 999.");
				}
				this.status_code = value;
				this.status_description = HttpStatusDescription.Get(value);
			}
		}

		/// <summary>Gets or sets a text description of the HTTP status code returned to the client.</summary>
		/// <returns>The text description of the HTTP status code returned to the client. The default is the RFC 2616 description for the <see cref="P:System.Net.HttpListenerResponse.StatusCode" /> property value, or an empty string ("") if an RFC 2616 description does not exist.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation contains non-printable characters.</exception>
		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06003545 RID: 13637 RVA: 0x000BA2C0 File Offset: 0x000B84C0
		// (set) Token: 0x06003546 RID: 13638 RVA: 0x000BA2C8 File Offset: 0x000B84C8
		public string StatusDescription
		{
			get
			{
				return this.status_description;
			}
			set
			{
				this.status_description = value;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.HttpListenerResponse" />.</summary>
		// Token: 0x06003547 RID: 13639 RVA: 0x000BA2D1 File Offset: 0x000B84D1
		void IDisposable.Dispose()
		{
			this.Close(true);
		}

		/// <summary>Closes the connection to the client without sending a response.</summary>
		// Token: 0x06003548 RID: 13640 RVA: 0x000BA2DA File Offset: 0x000B84DA
		public void Abort()
		{
			if (this.disposed)
			{
				return;
			}
			this.Close(true);
		}

		/// <summary>Adds the specified header and value to the HTTP headers for this response.</summary>
		/// <param name="name">The name of the HTTP header to set.</param>
		/// <param name="value">The value for the <paramref name="name" /> header.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" /> or an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentException">You are not allowed to specify a value for the specified header.  
		///  -or-  
		///  <paramref name="name" /> or <paramref name="value" /> contains invalid characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65,535 characters.</exception>
		// Token: 0x06003549 RID: 13641 RVA: 0x000BA2EC File Offset: 0x000B84EC
		public void AddHeader(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == "")
			{
				throw new ArgumentException("'name' cannot be empty", "name");
			}
			if (value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			this.headers.Set(name, value);
		}

		/// <summary>Adds the specified <see cref="T:System.Net.Cookie" /> to the collection of cookies for this response.</summary>
		/// <param name="cookie">The <see cref="T:System.Net.Cookie" /> to add to the collection to be sent with this response.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookie" /> is <see langword="null" />.</exception>
		// Token: 0x0600354A RID: 13642 RVA: 0x000BA349 File Offset: 0x000B8549
		public void AppendCookie(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			this.Cookies.Add(cookie);
		}

		/// <summary>Appends a value to the specified HTTP header to be sent with this response.</summary>
		/// <param name="name">The name of the HTTP header to append <paramref name="value" /> to.</param>
		/// <param name="value">The value to append to the <paramref name="name" /> header.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is <see langword="null" /> or an empty string ("").  
		/// -or-  
		/// You are not allowed to specify a value for the specified header.  
		/// -or-  
		/// <paramref name="name" /> or <paramref name="value" /> contains invalid characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65,535 characters.</exception>
		// Token: 0x0600354B RID: 13643 RVA: 0x000BA368 File Offset: 0x000B8568
		public void AppendHeader(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == "")
			{
				throw new ArgumentException("'name' cannot be empty", "name");
			}
			if (value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			this.headers.Add(name, value);
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x000BA3C5 File Offset: 0x000B85C5
		private void Close(bool force)
		{
			this.disposed = true;
			this.context.Connection.Close(force);
		}

		/// <summary>Sends the response to the client and releases the resources held by this <see cref="T:System.Net.HttpListenerResponse" /> instance.</summary>
		// Token: 0x0600354D RID: 13645 RVA: 0x000BA3DF File Offset: 0x000B85DF
		public void Close()
		{
			if (this.disposed)
			{
				return;
			}
			this.Close(false);
		}

		/// <summary>Returns the specified byte array to the client and releases the resources held by this <see cref="T:System.Net.HttpListenerResponse" /> instance.</summary>
		/// <param name="responseEntity">A <see cref="T:System.Byte" /> array that contains the response to send to the client.</param>
		/// <param name="willBlock">
		///   <see langword="true" /> to block execution while flushing the stream to the client; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="responseEntity" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x0600354E RID: 13646 RVA: 0x000BA3F1 File Offset: 0x000B85F1
		public void Close(byte[] responseEntity, bool willBlock)
		{
			if (this.disposed)
			{
				return;
			}
			if (responseEntity == null)
			{
				throw new ArgumentNullException("responseEntity");
			}
			this.ContentLength64 = (long)responseEntity.Length;
			this.OutputStream.Write(responseEntity, 0, (int)this.content_length);
			this.Close(false);
		}

		/// <summary>Copies properties from the specified <see cref="T:System.Net.HttpListenerResponse" /> to this response.</summary>
		/// <param name="templateResponse">The <see cref="T:System.Net.HttpListenerResponse" /> instance to copy.</param>
		// Token: 0x0600354F RID: 13647 RVA: 0x000BA430 File Offset: 0x000B8630
		public void CopyFrom(HttpListenerResponse templateResponse)
		{
			this.headers.Clear();
			this.headers.Add(templateResponse.headers);
			this.content_length = templateResponse.content_length;
			this.status_code = templateResponse.status_code;
			this.status_description = templateResponse.status_description;
			this.keep_alive = templateResponse.keep_alive;
			this.version = templateResponse.version;
		}

		/// <summary>Configures the response to redirect the client to the specified URL.</summary>
		/// <param name="url">The URL that the client should use to locate the requested resource.</param>
		// Token: 0x06003550 RID: 13648 RVA: 0x000BA495 File Offset: 0x000B8695
		public void Redirect(string url)
		{
			this.StatusCode = 302;
			this.location = url;
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000BA4AC File Offset: 0x000B86AC
		private bool FindCookie(Cookie cookie)
		{
			string name = cookie.Name;
			string domain = cookie.Domain;
			string path = cookie.Path;
			foreach (object obj in this.cookies)
			{
				Cookie cookie2 = (Cookie)obj;
				if (!(name != cookie2.Name) && !(domain != cookie2.Domain) && path == cookie2.Path)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000BA550 File Offset: 0x000B8750
		internal void SendHeaders(bool closing, MemoryStream ms)
		{
			Encoding @default = this.content_encoding;
			if (@default == null)
			{
				@default = Encoding.Default;
			}
			if (this.content_type != null)
			{
				if (this.content_encoding != null && this.content_type.IndexOf("charset=", StringComparison.Ordinal) == -1)
				{
					string webName = this.content_encoding.WebName;
					this.headers.SetInternal("Content-Type", this.content_type + "; charset=" + webName);
				}
				else
				{
					this.headers.SetInternal("Content-Type", this.content_type);
				}
			}
			if (this.headers["Server"] == null)
			{
				this.headers.SetInternal("Server", "Mono-HTTPAPI/1.0");
			}
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			if (this.headers["Date"] == null)
			{
				this.headers.SetInternal("Date", DateTime.UtcNow.ToString("r", invariantCulture));
			}
			if (!this.chunked)
			{
				if (!this.cl_set && closing)
				{
					this.cl_set = true;
					this.content_length = 0L;
				}
				if (this.cl_set)
				{
					this.headers.SetInternal("Content-Length", this.content_length.ToString(invariantCulture));
				}
			}
			Version protocolVersion = this.context.Request.ProtocolVersion;
			if (!this.cl_set && !this.chunked && protocolVersion >= HttpVersion.Version11)
			{
				this.chunked = true;
			}
			bool flag = this.status_code == 400 || this.status_code == 408 || this.status_code == 411 || this.status_code == 413 || this.status_code == 414 || this.status_code == 500 || this.status_code == 503;
			if (!flag)
			{
				flag = !this.context.Request.KeepAlive;
			}
			if (!this.keep_alive || flag)
			{
				this.headers.SetInternal("Connection", "close");
				flag = true;
			}
			if (this.chunked)
			{
				this.headers.SetInternal("Transfer-Encoding", "chunked");
			}
			int reuses = this.context.Connection.Reuses;
			if (reuses >= 100)
			{
				this.force_close_chunked = true;
				if (!flag)
				{
					this.headers.SetInternal("Connection", "close");
					flag = true;
				}
			}
			if (!flag)
			{
				this.headers.SetInternal("Keep-Alive", string.Format("timeout=15,max={0}", 100 - reuses));
				if (this.context.Request.ProtocolVersion <= HttpVersion.Version10)
				{
					this.headers.SetInternal("Connection", "keep-alive");
				}
			}
			if (this.location != null)
			{
				this.headers.SetInternal("Location", this.location);
			}
			if (this.cookies != null)
			{
				foreach (object obj in this.cookies)
				{
					Cookie cookie = (Cookie)obj;
					this.headers.SetInternal("Set-Cookie", HttpListenerResponse.CookieToClientString(cookie));
				}
			}
			StreamWriter streamWriter = new StreamWriter(ms, @default, 256);
			streamWriter.Write("HTTP/{0} {1} {2}\r\n", this.version, this.status_code, this.status_description);
			string value = HttpListenerResponse.FormatHeaders(this.headers);
			streamWriter.Write(value);
			streamWriter.Flush();
			int num = @default.GetPreamble().Length;
			if (this.output_stream == null)
			{
				this.output_stream = this.context.Connection.GetResponseStream();
			}
			ms.Position = (long)num;
			this.HeadersSent = true;
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000BA90C File Offset: 0x000B8B0C
		private static string FormatHeaders(WebHeaderCollection headers)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < headers.Count; i++)
			{
				string key = headers.GetKey(i);
				if (WebHeaderCollection.AllowMultiValues(key))
				{
					foreach (string value in headers.GetValues(i))
					{
						stringBuilder.Append(key).Append(": ").Append(value).Append("\r\n");
					}
				}
				else
				{
					stringBuilder.Append(key).Append(": ").Append(headers.Get(i)).Append("\r\n");
				}
			}
			return stringBuilder.Append("\r\n").ToString();
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000BA9C4 File Offset: 0x000B8BC4
		private static string CookieToClientString(Cookie cookie)
		{
			if (cookie.Name.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(64);
			if (cookie.Version > 0)
			{
				stringBuilder.Append("Version=").Append(cookie.Version).Append(";");
			}
			stringBuilder.Append(cookie.Name).Append("=").Append(cookie.Value);
			if (cookie.Path != null && cookie.Path.Length != 0)
			{
				stringBuilder.Append(";Path=").Append(HttpListenerResponse.QuotedString(cookie, cookie.Path));
			}
			if (cookie.Domain != null && cookie.Domain.Length != 0)
			{
				stringBuilder.Append(";Domain=").Append(HttpListenerResponse.QuotedString(cookie, cookie.Domain));
			}
			if (cookie.Port != null && cookie.Port.Length != 0)
			{
				stringBuilder.Append(";Port=").Append(cookie.Port);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000BAACE File Offset: 0x000B8CCE
		private static string QuotedString(Cookie cookie, string value)
		{
			if (cookie.Version == 0 || HttpListenerResponse.IsToken(value))
			{
				return value;
			}
			return "\"" + value.Replace("\"", "\\\"") + "\"";
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000BAB04 File Offset: 0x000B8D04
		private static bool IsToken(string value)
		{
			int length = value.Length;
			for (int i = 0; i < length; i++)
			{
				char c = value[i];
				if (c < ' ' || c >= '\u007f' || HttpListenerResponse.tspecials.IndexOf(c) != -1)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Adds or updates a <see cref="T:System.Net.Cookie" /> in the collection of cookies sent with this response.</summary>
		/// <param name="cookie">A <see cref="T:System.Net.Cookie" /> for this response.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookie" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The cookie already exists in the collection and could not be replaced.</exception>
		// Token: 0x06003557 RID: 13655 RVA: 0x000BAB48 File Offset: 0x000B8D48
		public void SetCookie(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			if (this.cookies != null)
			{
				if (this.FindCookie(cookie))
				{
					throw new ArgumentException("The cookie already exists.");
				}
			}
			else
			{
				this.cookies = new CookieCollection();
			}
			this.cookies.Add(cookie);
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x000BAB96 File Offset: 0x000B8D96
		// Note: this type is marked as 'beforefieldinit'.
		static HttpListenerResponse()
		{
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal HttpListenerResponse()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001EFE RID: 7934
		private bool disposed;

		// Token: 0x04001EFF RID: 7935
		private Encoding content_encoding;

		// Token: 0x04001F00 RID: 7936
		private long content_length;

		// Token: 0x04001F01 RID: 7937
		private bool cl_set;

		// Token: 0x04001F02 RID: 7938
		private string content_type;

		// Token: 0x04001F03 RID: 7939
		private CookieCollection cookies;

		// Token: 0x04001F04 RID: 7940
		private WebHeaderCollection headers;

		// Token: 0x04001F05 RID: 7941
		private bool keep_alive;

		// Token: 0x04001F06 RID: 7942
		private ResponseStream output_stream;

		// Token: 0x04001F07 RID: 7943
		private Version version;

		// Token: 0x04001F08 RID: 7944
		private string location;

		// Token: 0x04001F09 RID: 7945
		private int status_code;

		// Token: 0x04001F0A RID: 7946
		private string status_description;

		// Token: 0x04001F0B RID: 7947
		private bool chunked;

		// Token: 0x04001F0C RID: 7948
		private HttpListenerContext context;

		// Token: 0x04001F0D RID: 7949
		internal bool HeadersSent;

		// Token: 0x04001F0E RID: 7950
		internal object headers_lock;

		// Token: 0x04001F0F RID: 7951
		private bool force_close_chunked;

		// Token: 0x04001F10 RID: 7952
		private static string tspecials = "()<>@,;:\\\"/[]?={} \t";
	}
}
