using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Net.Http
{
	/// <summary>Represents a HTTP request message.</summary>
	// Token: 0x0200002A RID: 42
	public class HttpRequestMessage : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestMessage" /> class.</summary>
		// Token: 0x0600014D RID: 333 RVA: 0x000059B5 File Offset: 0x00003BB5
		public HttpRequestMessage()
		{
			this.method = HttpMethod.Get;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestMessage" /> class with an HTTP method and a request <see cref="T:System.Uri" />.</summary>
		/// <param name="method">The HTTP method.</param>
		/// <param name="requestUri">A string that represents the request  <see cref="T:System.Uri" />.</param>
		// Token: 0x0600014E RID: 334 RVA: 0x000059C8 File Offset: 0x00003BC8
		public HttpRequestMessage(HttpMethod method, string requestUri) : this(method, string.IsNullOrEmpty(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestMessage" /> class with an HTTP method and a request <see cref="T:System.Uri" />.</summary>
		/// <param name="method">The HTTP method.</param>
		/// <param name="requestUri">The <see cref="T:System.Uri" /> to request.</param>
		// Token: 0x0600014F RID: 335 RVA: 0x000059E3 File Offset: 0x00003BE3
		public HttpRequestMessage(HttpMethod method, Uri requestUri)
		{
			this.Method = method;
			this.RequestUri = requestUri;
		}

		/// <summary>Gets or sets the contents of the HTTP message.</summary>
		/// <returns>The content of a message</returns>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000059F9 File Offset: 0x00003BF9
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00005A01 File Offset: 0x00003C01
		public HttpContent Content
		{
			[CompilerGenerated]
			get
			{
				return this.<Content>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Content>k__BackingField = value;
			}
		}

		/// <summary>Gets the collection of HTTP request headers.</summary>
		/// <returns>The collection of HTTP request headers.</returns>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00005A0C File Offset: 0x00003C0C
		public HttpRequestHeaders Headers
		{
			get
			{
				HttpRequestHeaders result;
				if ((result = this.headers) == null)
				{
					result = (this.headers = new HttpRequestHeaders());
				}
				return result;
			}
		}

		/// <summary>Gets or sets the HTTP method used by the HTTP request message.</summary>
		/// <returns>The HTTP method used by the request message. The default is the GET method.</returns>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00005A31 File Offset: 0x00003C31
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00005A39 File Offset: 0x00003C39
		public HttpMethod Method
		{
			get
			{
				return this.method;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("method");
				}
				this.method = value;
			}
		}

		/// <summary>Gets a set of properties for the HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00005A58 File Offset: 0x00003C58
		public IDictionary<string, object> Properties
		{
			get
			{
				Dictionary<string, object> result;
				if ((result = this.properties) == null)
				{
					result = (this.properties = new Dictionary<string, object>());
				}
				return result;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Uri" /> used for the HTTP request.</summary>
		/// <returns>The <see cref="T:System.Uri" /> used for the HTTP request.</returns>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005A7D File Offset: 0x00003C7D
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00005A85 File Offset: 0x00003C85
		public Uri RequestUri
		{
			get
			{
				return this.uri;
			}
			set
			{
				if (value != null && value.IsAbsoluteUri && !HttpRequestMessage.IsAllowedAbsoluteUri(value))
				{
					throw new ArgumentException("Only http or https scheme is allowed");
				}
				this.uri = value;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005AB4 File Offset: 0x00003CB4
		private static bool IsAllowedAbsoluteUri(Uri uri)
		{
			return uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps || (uri.Scheme == Uri.UriSchemeFile && uri.OriginalString.StartsWith("/", StringComparison.Ordinal));
		}

		/// <summary>Gets or sets the HTTP message version.</summary>
		/// <returns>The HTTP message version. The default is 1.1.</returns>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00005B0F File Offset: 0x00003D0F
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00005B20 File Offset: 0x00003D20
		public Version Version
		{
			get
			{
				return this.version ?? HttpVersion.Version11;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Version");
				}
				this.version = value;
			}
		}

		/// <summary>Releases the unmanaged resources and disposes of the managed resources used by the <see cref="T:System.Net.Http.HttpRequestMessage" />.</summary>
		// Token: 0x0600015B RID: 347 RVA: 0x00005B3D File Offset: 0x00003D3D
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpRequestMessage" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		// Token: 0x0600015C RID: 348 RVA: 0x00005B46 File Offset: 0x00003D46
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				if (this.Content != null)
				{
					this.Content.Dispose();
				}
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005B6D File Offset: 0x00003D6D
		internal bool SetIsUsed()
		{
			if (this.is_used)
			{
				return true;
			}
			this.is_used = true;
			return false;
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string representation of the current object.</returns>
		// Token: 0x0600015E RID: 350 RVA: 0x00005B84 File Offset: 0x00003D84
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Method: ").Append(this.method);
			stringBuilder.Append(", RequestUri: '").Append((this.RequestUri != null) ? this.RequestUri.ToString() : "<null>");
			stringBuilder.Append("', Version: ").Append(this.Version);
			stringBuilder.Append(", Content: ").Append((this.Content != null) ? this.Content.ToString() : "<null>");
			stringBuilder.Append(", Headers:\r\n{\r\n").Append(this.Headers);
			if (this.Content != null)
			{
				stringBuilder.Append(this.Content.Headers);
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x040000B3 RID: 179
		private HttpRequestHeaders headers;

		// Token: 0x040000B4 RID: 180
		private HttpMethod method;

		// Token: 0x040000B5 RID: 181
		private Version version;

		// Token: 0x040000B6 RID: 182
		private Dictionary<string, object> properties;

		// Token: 0x040000B7 RID: 183
		private Uri uri;

		// Token: 0x040000B8 RID: 184
		private bool is_used;

		// Token: 0x040000B9 RID: 185
		private bool disposed;

		// Token: 0x040000BA RID: 186
		[CompilerGenerated]
		private HttpContent <Content>k__BackingField;
	}
}
