using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Net
{
	/// <summary>Provides an HTTP-specific implementation of the <see cref="T:System.Net.WebResponse" /> class.</summary>
	// Token: 0x0200069C RID: 1692
	[Serializable]
	public class HttpWebResponse : WebResponse, ISerializable, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebResponse" /> class.</summary>
		// Token: 0x06003623 RID: 13859 RVA: 0x000BDC06 File Offset: 0x000BBE06
		public HttpWebResponse()
		{
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x000BDC10 File Offset: 0x000BBE10
		internal HttpWebResponse(Uri uri, string method, HttpStatusCode status, WebHeaderCollection headers)
		{
			this.uri = uri;
			this.method = method;
			this.statusCode = status;
			this.statusDescription = HttpStatusDescription.Get(status);
			this.webHeaders = headers;
			this.version = HttpVersion.Version10;
			this.contentLength = -1L;
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x000BDC60 File Offset: 0x000BBE60
		internal HttpWebResponse(Uri uri, string method, WebResponseStream stream, CookieContainer container)
		{
			this.uri = uri;
			this.method = method;
			this.stream = stream;
			this.webHeaders = (stream.Headers ?? new WebHeaderCollection());
			this.version = stream.Version;
			this.statusCode = stream.StatusCode;
			this.statusDescription = (stream.StatusDescription ?? HttpStatusDescription.Get(this.statusCode));
			this.contentLength = -1L;
			try
			{
				string text = this.webHeaders["Content-Length"];
				if (string.IsNullOrEmpty(text) || !long.TryParse(text, out this.contentLength))
				{
					this.contentLength = -1L;
				}
			}
			catch (Exception)
			{
				this.contentLength = -1L;
			}
			if (container != null)
			{
				this.cookie_container = container;
				this.FillCookies();
			}
			string a = this.webHeaders["Content-Encoding"];
			if (a == "gzip" && (stream.Request.AutomaticDecompression & DecompressionMethods.GZip) != DecompressionMethods.None)
			{
				this.stream = new GZipStream(stream, CompressionMode.Decompress);
				this.webHeaders.Remove(HttpRequestHeader.ContentEncoding);
				return;
			}
			if (a == "deflate" && (stream.Request.AutomaticDecompression & DecompressionMethods.Deflate) != DecompressionMethods.None)
			{
				this.stream = new DeflateStream(stream, CompressionMode.Decompress);
				this.webHeaders.Remove(HttpRequestHeader.ContentEncoding);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebResponse" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.HttpWebRequest" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.HttpWebRequest" />.</param>
		// Token: 0x06003626 RID: 13862 RVA: 0x000BDDB4 File Offset: 0x000BBFB4
		[Obsolete("Serialization is obsoleted for this type", false)]
		protected HttpWebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.uri = (Uri)serializationInfo.GetValue("uri", typeof(Uri));
			this.contentLength = serializationInfo.GetInt64("contentLength");
			this.contentType = serializationInfo.GetString("contentType");
			this.method = serializationInfo.GetString("method");
			this.statusDescription = serializationInfo.GetString("statusDescription");
			this.cookieCollection = (CookieCollection)serializationInfo.GetValue("cookieCollection", typeof(CookieCollection));
			this.version = (Version)serializationInfo.GetValue("version", typeof(Version));
			this.statusCode = (HttpStatusCode)serializationInfo.GetValue("statusCode", typeof(HttpStatusCode));
		}

		/// <summary>Gets the character set of the response.</summary>
		/// <returns>A string that contains the character set of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06003627 RID: 13863 RVA: 0x000BDE90 File Offset: 0x000BC090
		public string CharacterSet
		{
			get
			{
				string text = this.ContentType;
				if (text == null)
				{
					return "ISO-8859-1";
				}
				string text2 = text.ToLower();
				int num = text2.IndexOf("charset=", StringComparison.Ordinal);
				if (num == -1)
				{
					return "ISO-8859-1";
				}
				num += 8;
				int num2 = text2.IndexOf(';', num);
				if (num2 != -1)
				{
					return text.Substring(num, num2 - num);
				}
				return text.Substring(num);
			}
		}

		/// <summary>Gets the method that is used to encode the body of the response.</summary>
		/// <returns>A string that describes the method that is used to encode the body of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06003628 RID: 13864 RVA: 0x000BDEF0 File Offset: 0x000BC0F0
		public string ContentEncoding
		{
			get
			{
				this.CheckDisposed();
				string text = this.webHeaders["Content-Encoding"];
				if (text == null)
				{
					return "";
				}
				return text;
			}
		}

		/// <summary>Gets the length of the content returned by the request.</summary>
		/// <returns>The number of bytes returned by the request. Content length does not include header information.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06003629 RID: 13865 RVA: 0x000BDF1E File Offset: 0x000BC11E
		public override long ContentLength
		{
			get
			{
				return this.contentLength;
			}
		}

		/// <summary>Gets the content type of the response.</summary>
		/// <returns>A string that contains the content type of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x0600362A RID: 13866 RVA: 0x000BDF26 File Offset: 0x000BC126
		public override string ContentType
		{
			get
			{
				this.CheckDisposed();
				if (this.contentType == null)
				{
					this.contentType = this.webHeaders["Content-Type"];
				}
				if (this.contentType == null)
				{
					this.contentType = string.Empty;
				}
				return this.contentType;
			}
		}

		/// <summary>Gets or sets the cookies that are associated with this response.</summary>
		/// <returns>A <see cref="T:System.Net.CookieCollection" /> that contains the cookies that are associated with this response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x0600362B RID: 13867 RVA: 0x000BDF65 File Offset: 0x000BC165
		// (set) Token: 0x0600362C RID: 13868 RVA: 0x000BDF86 File Offset: 0x000BC186
		public virtual CookieCollection Cookies
		{
			get
			{
				this.CheckDisposed();
				if (this.cookieCollection == null)
				{
					this.cookieCollection = new CookieCollection();
				}
				return this.cookieCollection;
			}
			set
			{
				this.CheckDisposed();
				this.cookieCollection = value;
			}
		}

		/// <summary>Gets the headers that are associated with this response from the server.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the header information returned with the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x0600362D RID: 13869 RVA: 0x000BDF95 File Offset: 0x000BC195
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.webHeaders;
			}
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x0001FD2F File Offset: 0x0001DF2F
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether both client and server were authenticated.</summary>
		/// <returns>
		///   <see langword="true" /> if mutual authentication occurred; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x0600362F RID: 13871 RVA: 0x000BDF9D File Offset: 0x000BC19D
		[MonoTODO]
		public override bool IsMutuallyAuthenticated
		{
			get
			{
				throw HttpWebResponse.GetMustImplement();
			}
		}

		/// <summary>Gets the last date and time that the contents of the response were modified.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the date and time that the contents of the response were modified.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06003630 RID: 13872 RVA: 0x000BDFA4 File Offset: 0x000BC1A4
		public DateTime LastModified
		{
			get
			{
				this.CheckDisposed();
				DateTime result;
				try
				{
					result = MonoHttpDate.Parse(this.webHeaders["Last-Modified"]);
				}
				catch (Exception)
				{
					result = DateTime.Now;
				}
				return result;
			}
		}

		/// <summary>Gets the method that is used to return the response.</summary>
		/// <returns>A string that contains the HTTP method that is used to return the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06003631 RID: 13873 RVA: 0x000BDFEC File Offset: 0x000BC1EC
		public virtual string Method
		{
			get
			{
				this.CheckDisposed();
				return this.method;
			}
		}

		/// <summary>Gets the version of the HTTP protocol that is used in the response.</summary>
		/// <returns>A <see cref="T:System.Version" /> that contains the HTTP protocol version of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06003632 RID: 13874 RVA: 0x000BDFFA File Offset: 0x000BC1FA
		public Version ProtocolVersion
		{
			get
			{
				this.CheckDisposed();
				return this.version;
			}
		}

		/// <summary>Gets the URI of the Internet resource that responded to the request.</summary>
		/// <returns>The URI of the Internet resource that responded to the request.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06003633 RID: 13875 RVA: 0x000BE008 File Offset: 0x000BC208
		public override Uri ResponseUri
		{
			get
			{
				this.CheckDisposed();
				return this.uri;
			}
		}

		/// <summary>Gets the name of the server that sent the response.</summary>
		/// <returns>A string that contains the name of the server that sent the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06003634 RID: 13876 RVA: 0x000BE016 File Offset: 0x000BC216
		public string Server
		{
			get
			{
				this.CheckDisposed();
				return this.webHeaders["Server"] ?? "";
			}
		}

		/// <summary>Gets the status of the response.</summary>
		/// <returns>One of the <see cref="T:System.Net.HttpStatusCode" /> values.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06003635 RID: 13877 RVA: 0x000BE037 File Offset: 0x000BC237
		public virtual HttpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		/// <summary>Gets the status description returned with the response.</summary>
		/// <returns>A string that describes the status of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06003636 RID: 13878 RVA: 0x000BE03F File Offset: 0x000BC23F
		public virtual string StatusDescription
		{
			get
			{
				this.CheckDisposed();
				return this.statusDescription;
			}
		}

		/// <summary>Gets a value that indicates whether headers are supported.</summary>
		/// <returns>
		///   <see langword="true" /> if headers are supported; otherwise, <see langword="false" />. Always returns <see langword="true" />.</returns>
		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06003637 RID: 13879 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool SupportsHeaders
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the contents of a header that was returned with the response.</summary>
		/// <param name="headerName">The header value to return.</param>
		/// <returns>The contents of the specified header.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x06003638 RID: 13880 RVA: 0x000BE050 File Offset: 0x000BC250
		public string GetResponseHeader(string headerName)
		{
			this.CheckDisposed();
			string text = this.webHeaders[headerName];
			if (text == null)
			{
				return "";
			}
			return text;
		}

		/// <summary>Gets the stream that is used to read the body of the response from the server.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> containing the body of the response.</returns>
		/// <exception cref="T:System.Net.ProtocolViolationException">There is no response stream.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x06003639 RID: 13881 RVA: 0x000BE07A File Offset: 0x000BC27A
		public override Stream GetResponseStream()
		{
			this.CheckDisposed();
			if (this.stream == null)
			{
				return Stream.Null;
			}
			if (string.Equals(this.method, "HEAD", StringComparison.OrdinalIgnoreCase))
			{
				return Stream.Null;
			}
			return this.stream;
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.HttpWebResponse" /> will be serialized.</param>
		/// <param name="streamingContext">The destination of the serialization.</param>
		// Token: 0x0600363A RID: 13882 RVA: 0x000ABB1C File Offset: 0x000A9D1C
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x0600363B RID: 13883 RVA: 0x000BE0B0 File Offset: 0x000BC2B0
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("uri", this.uri);
			serializationInfo.AddValue("contentLength", this.contentLength);
			serializationInfo.AddValue("contentType", this.contentType);
			serializationInfo.AddValue("method", this.method);
			serializationInfo.AddValue("statusDescription", this.statusDescription);
			serializationInfo.AddValue("cookieCollection", this.cookieCollection);
			serializationInfo.AddValue("version", this.version);
			serializationInfo.AddValue("statusCode", this.statusCode);
		}

		/// <summary>Closes the response stream.</summary>
		/// <exception cref="T:System.ObjectDisposedException">.NET Core only: This <see cref="T:System.Net.HttpWebResponse" /> object has been disposed.</exception>
		// Token: 0x0600363C RID: 13884 RVA: 0x000BE14C File Offset: 0x000BC34C
		public override void Close()
		{
			Stream stream = Interlocked.Exchange<Stream>(ref this.stream, null);
			if (stream != null)
			{
				stream.Close();
			}
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x000BE16F File Offset: 0x000BC36F
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.HttpWebResponse" />, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		// Token: 0x0600363E RID: 13886 RVA: 0x000BE178 File Offset: 0x000BC378
		protected override void Dispose(bool disposing)
		{
			this.disposed = true;
			base.Dispose(true);
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x000BE188 File Offset: 0x000BC388
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x000BE1A4 File Offset: 0x000BC3A4
		private void FillCookies()
		{
			if (this.webHeaders == null)
			{
				return;
			}
			CookieCollection cookieCollection = null;
			try
			{
				string text = this.webHeaders.Get("Set-Cookie");
				if (text != null)
				{
					cookieCollection = this.cookie_container.CookieCutter(this.uri, "Set-Cookie", text, false);
				}
			}
			catch
			{
			}
			try
			{
				string text = this.webHeaders.Get("Set-Cookie2");
				if (text != null)
				{
					CookieCollection cookieCollection2 = this.cookie_container.CookieCutter(this.uri, "Set-Cookie2", text, false);
					if (cookieCollection != null && cookieCollection.Count != 0)
					{
						cookieCollection.Add(cookieCollection2);
					}
					else
					{
						cookieCollection = cookieCollection2;
					}
				}
			}
			catch
			{
			}
			this.cookieCollection = cookieCollection;
		}

		// Token: 0x04001F8B RID: 8075
		private Uri uri;

		// Token: 0x04001F8C RID: 8076
		private WebHeaderCollection webHeaders;

		// Token: 0x04001F8D RID: 8077
		private CookieCollection cookieCollection;

		// Token: 0x04001F8E RID: 8078
		private string method;

		// Token: 0x04001F8F RID: 8079
		private Version version;

		// Token: 0x04001F90 RID: 8080
		private HttpStatusCode statusCode;

		// Token: 0x04001F91 RID: 8081
		private string statusDescription;

		// Token: 0x04001F92 RID: 8082
		private long contentLength;

		// Token: 0x04001F93 RID: 8083
		private string contentType;

		// Token: 0x04001F94 RID: 8084
		private CookieContainer cookie_container;

		// Token: 0x04001F95 RID: 8085
		private bool disposed;

		// Token: 0x04001F96 RID: 8086
		private Stream stream;
	}
}
