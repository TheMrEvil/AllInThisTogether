using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Cache;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mono.Net.Security;
using Mono.Security.Interface;
using Unity;

namespace System.Net
{
	/// <summary>Provides an HTTP-specific implementation of the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x02000694 RID: 1684
	[Serializable]
	public class HttpWebRequest : WebRequest, ISerializable
	{
		// Token: 0x06003570 RID: 13680 RVA: 0x000BAD00 File Offset: 0x000B8F00
		static HttpWebRequest()
		{
			NetConfig netConfig = ConfigurationSettings.GetConfig("system.net/settings") as NetConfig;
			if (netConfig != null)
			{
				HttpWebRequest.defaultMaxResponseHeadersLength = netConfig.MaxResponseHeadersLength;
			}
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x000BAD44 File Offset: 0x000B8F44
		internal HttpWebRequest(Uri uri)
		{
			this.allowAutoRedirect = true;
			this.allowBuffering = true;
			this.contentLength = -1L;
			this.keepAlive = true;
			this.maxAutoRedirect = 50;
			this.mediaType = string.Empty;
			this.method = "GET";
			this.initialMethod = "GET";
			this.pipelined = true;
			this.version = HttpVersion.Version11;
			this.timeout = 100000;
			this.continueTimeout = 350;
			this.locker = new object();
			this.readWriteTimeout = 300000;
			base..ctor();
			this.requestUri = uri;
			this.actualUri = uri;
			this.proxy = WebRequest.InternalDefaultWebProxy;
			this.webHeaders = new WebHeaderCollection(WebHeaderCollectionType.HttpWebRequest);
			this.ThrowOnError = true;
			this.ResetAuthorization();
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x000BAE0D File Offset: 0x000B900D
		internal HttpWebRequest(Uri uri, MobileTlsProvider tlsProvider, MonoTlsSettings settings = null) : this(uri)
		{
			this.tlsProvider = tlsProvider;
			this.tlsSettings = settings;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebRequest" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes. This constructor is obsolete.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the new <see cref="T:System.Net.HttpWebRequest" /> object.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the new <see cref="T:System.Net.HttpWebRequest" /> object.</param>
		// Token: 0x06003573 RID: 13683 RVA: 0x000BAE24 File Offset: 0x000B9024
		[Obsolete("Serialization is obsoleted for this type.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected HttpWebRequest(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.allowAutoRedirect = true;
			this.allowBuffering = true;
			this.contentLength = -1L;
			this.keepAlive = true;
			this.maxAutoRedirect = 50;
			this.mediaType = string.Empty;
			this.method = "GET";
			this.initialMethod = "GET";
			this.pipelined = true;
			this.version = HttpVersion.Version11;
			this.timeout = 100000;
			this.continueTimeout = 350;
			this.locker = new object();
			this.readWriteTimeout = 300000;
			base..ctor();
			throw new SerializationException();
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x000BAEC0 File Offset: 0x000B90C0
		private void ResetAuthorization()
		{
			this.auth_state = new HttpWebRequest.AuthorizationState(this, false);
			this.proxy_auth_state = new HttpWebRequest.AuthorizationState(this, true);
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000BAEDC File Offset: 0x000B90DC
		private void SetSpecialHeaders(string HeaderName, string value)
		{
			value = WebHeaderCollection.CheckBadChars(value, true);
			this.webHeaders.RemoveInternal(HeaderName);
			if (value.Length != 0)
			{
				this.webHeaders.AddInternal(HeaderName, value);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Accept" /> HTTP header.</summary>
		/// <returns>The value of the <see langword="Accept" /> HTTP header. The default value is <see langword="null" />.</returns>
		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06003576 RID: 13686 RVA: 0x000BAF08 File Offset: 0x000B9108
		// (set) Token: 0x06003577 RID: 13687 RVA: 0x000BAF1A File Offset: 0x000B911A
		public string Accept
		{
			get
			{
				return this.webHeaders["Accept"];
			}
			set
			{
				this.CheckRequestStarted();
				this.SetSpecialHeaders("Accept", value);
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the Internet resource that actually responds to the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that identifies the Internet resource that actually responds to the request. The default is the URI used by the <see cref="M:System.Net.WebRequest.Create(System.String)" /> method to initialize the request.</returns>
		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06003578 RID: 13688 RVA: 0x000BAF2E File Offset: 0x000B912E
		// (set) Token: 0x06003579 RID: 13689 RVA: 0x000BAF36 File Offset: 0x000B9136
		public Uri Address
		{
			get
			{
				return this.actualUri;
			}
			internal set
			{
				this.actualUri = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the request should follow redirection responses.</summary>
		/// <returns>
		///   <see langword="true" /> if the request should automatically follow redirection responses from the Internet resource; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x0600357A RID: 13690 RVA: 0x000BAF3F File Offset: 0x000B913F
		// (set) Token: 0x0600357B RID: 13691 RVA: 0x000BAF47 File Offset: 0x000B9147
		public virtual bool AllowAutoRedirect
		{
			get
			{
				return this.allowAutoRedirect;
			}
			set
			{
				this.allowAutoRedirect = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to buffer the data sent to the Internet resource.</summary>
		/// <returns>
		///   <see langword="true" /> to enable buffering of the data sent to the Internet resource; <see langword="false" /> to disable buffering. The default is <see langword="true" />.</returns>
		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x0600357C RID: 13692 RVA: 0x000BAF50 File Offset: 0x000B9150
		// (set) Token: 0x0600357D RID: 13693 RVA: 0x000BAF58 File Offset: 0x000B9158
		public virtual bool AllowWriteStreamBuffering
		{
			get
			{
				return this.allowBuffering;
			}
			set
			{
				this.allowBuffering = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to buffer the received from the Internet resource.</summary>
		/// <returns>
		///   <see langword="true" /> to enable buffering of the data received from the Internet resource; <see langword="false" /> to disable buffering. The default is <see langword="false" />.</returns>
		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x0600357E RID: 13694 RVA: 0x000BAF61 File Offset: 0x000B9161
		// (set) Token: 0x0600357F RID: 13695 RVA: 0x000BAF69 File Offset: 0x000B9169
		public virtual bool AllowReadStreamBuffering
		{
			get
			{
				return this.allowReadStreamBuffering;
			}
			set
			{
				this.allowReadStreamBuffering = value;
			}
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x0001FD2F File Offset: 0x0001DF2F
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Gets or sets the type of decompression that is used.</summary>
		/// <returns>A <see cref="T:System.Net.DecompressionMethods" /> object that indicates the type of decompression that is used.</returns>
		/// <exception cref="T:System.InvalidOperationException">The object's current state does not allow this property to be set.</exception>
		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06003581 RID: 13697 RVA: 0x000BAF72 File Offset: 0x000B9172
		// (set) Token: 0x06003582 RID: 13698 RVA: 0x000BAF7A File Offset: 0x000B917A
		public DecompressionMethods AutomaticDecompression
		{
			get
			{
				return this.auto_decomp;
			}
			set
			{
				this.CheckRequestStarted();
				this.auto_decomp = value;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06003583 RID: 13699 RVA: 0x000BAF89 File Offset: 0x000B9189
		internal bool InternalAllowBuffering
		{
			get
			{
				return this.allowBuffering && this.MethodWithBuffer;
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06003584 RID: 13700 RVA: 0x000BAF9C File Offset: 0x000B919C
		private bool MethodWithBuffer
		{
			get
			{
				return this.method != "HEAD" && this.method != "GET" && this.method != "MKCOL" && this.method != "CONNECT" && this.method != "TRACE";
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06003585 RID: 13701 RVA: 0x000BB003 File Offset: 0x000B9203
		internal MobileTlsProvider TlsProvider
		{
			get
			{
				return this.tlsProvider;
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06003586 RID: 13702 RVA: 0x000BB00B File Offset: 0x000B920B
		internal MonoTlsSettings TlsSettings
		{
			get
			{
				return this.tlsSettings;
			}
		}

		/// <summary>Gets or sets the collection of security certificates that are associated with this request.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> that contains the security certificates associated with this request.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is <see langword="null" />.</exception>
		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06003587 RID: 13703 RVA: 0x000BB013 File Offset: 0x000B9213
		// (set) Token: 0x06003588 RID: 13704 RVA: 0x000BB02E File Offset: 0x000B922E
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this.certificates == null)
				{
					this.certificates = new X509CertificateCollection();
				}
				return this.certificates;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.certificates = value;
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Connection" /> HTTP header.</summary>
		/// <returns>The value of the <see langword="Connection" /> HTTP header. The default value is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">The value of <see cref="P:System.Net.HttpWebRequest.Connection" /> is set to Keep-alive or Close.</exception>
		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06003589 RID: 13705 RVA: 0x000BB045 File Offset: 0x000B9245
		// (set) Token: 0x0600358A RID: 13706 RVA: 0x000BB058 File Offset: 0x000B9258
		public string Connection
		{
			get
			{
				return this.webHeaders["Connection"];
			}
			set
			{
				this.CheckRequestStarted();
				if (string.IsNullOrWhiteSpace(value))
				{
					this.webHeaders.RemoveInternal("Connection");
					return;
				}
				string text = value.ToLowerInvariant();
				if (text.Contains("keep-alive") || text.Contains("close"))
				{
					throw new ArgumentException("Keep-Alive and Close may not be set using this property.", "value");
				}
				string value2 = HttpValidationHelpers.CheckBadHeaderValueChars(value);
				this.webHeaders.CheckUpdate("Connection", value2);
			}
		}

		/// <summary>Gets or sets the name of the connection group for the request.</summary>
		/// <returns>The name of the connection group for this request. The default value is <see langword="null" />.</returns>
		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x0600358B RID: 13707 RVA: 0x000BB0CD File Offset: 0x000B92CD
		// (set) Token: 0x0600358C RID: 13708 RVA: 0x000BB0D5 File Offset: 0x000B92D5
		public override string ConnectionGroupName
		{
			get
			{
				return this.connectionGroup;
			}
			set
			{
				this.connectionGroup = value;
			}
		}

		/// <summary>Gets or sets the <see langword="Content-length" /> HTTP header.</summary>
		/// <returns>The number of bytes of data to send to the Internet resource. The default is -1, which indicates the property has not been set and that there is no request data to send.</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has been started by calling the <see cref="M:System.Net.HttpWebRequest.GetRequestStream" />, <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />, <see cref="M:System.Net.HttpWebRequest.GetResponse" />, or <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The new <see cref="P:System.Net.HttpWebRequest.ContentLength" /> value is less than 0.</exception>
		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x0600358D RID: 13709 RVA: 0x000BB0DE File Offset: 0x000B92DE
		// (set) Token: 0x0600358E RID: 13710 RVA: 0x000BB0E6 File Offset: 0x000B92E6
		public override long ContentLength
		{
			get
			{
				return this.contentLength;
			}
			set
			{
				this.CheckRequestStarted();
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Content-Length must be >= 0");
				}
				this.contentLength = value;
				this.haveContentLength = true;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (set) Token: 0x0600358F RID: 13711 RVA: 0x000BB111 File Offset: 0x000B9311
		internal long InternalContentLength
		{
			set
			{
				this.contentLength = value;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06003590 RID: 13712 RVA: 0x000BB11A File Offset: 0x000B931A
		// (set) Token: 0x06003591 RID: 13713 RVA: 0x000BB122 File Offset: 0x000B9322
		internal bool ThrowOnError
		{
			[CompilerGenerated]
			get
			{
				return this.<ThrowOnError>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ThrowOnError>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Content-type" /> HTTP header.</summary>
		/// <returns>The value of the <see langword="Content-type" /> HTTP header. The default value is <see langword="null" />.</returns>
		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06003592 RID: 13714 RVA: 0x000BB12B File Offset: 0x000B932B
		// (set) Token: 0x06003593 RID: 13715 RVA: 0x000BB13D File Offset: 0x000B933D
		public override string ContentType
		{
			get
			{
				return this.webHeaders["Content-Type"];
			}
			set
			{
				this.SetSpecialHeaders("Content-Type", value);
			}
		}

		/// <summary>Gets or sets the delegate method called when an HTTP 100-continue response is received from the Internet resource.</summary>
		/// <returns>A delegate that implements the callback method that executes when an HTTP Continue response is returned from the Internet resource. The default value is <see langword="null" />.</returns>
		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06003594 RID: 13716 RVA: 0x000BB14B File Offset: 0x000B934B
		// (set) Token: 0x06003595 RID: 13717 RVA: 0x000BB153 File Offset: 0x000B9353
		public HttpContinueDelegate ContinueDelegate
		{
			get
			{
				return this.continueDelegate;
			}
			set
			{
				this.continueDelegate = value;
			}
		}

		/// <summary>Gets or sets the cookies associated with the request.</summary>
		/// <returns>A <see cref="T:System.Net.CookieContainer" /> that contains the cookies associated with this request.</returns>
		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06003596 RID: 13718 RVA: 0x000BB15C File Offset: 0x000B935C
		// (set) Token: 0x06003597 RID: 13719 RVA: 0x000BB164 File Offset: 0x000B9364
		public virtual CookieContainer CookieContainer
		{
			get
			{
				return this.cookieContainer;
			}
			set
			{
				this.cookieContainer = value;
			}
		}

		/// <summary>Gets or sets authentication information for the request.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> that contains the authentication credentials associated with the request. The default is <see langword="null" />.</returns>
		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06003598 RID: 13720 RVA: 0x000BB16D File Offset: 0x000B936D
		// (set) Token: 0x06003599 RID: 13721 RVA: 0x000BB175 File Offset: 0x000B9375
		public override ICredentials Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.credentials = value;
			}
		}

		/// <summary>Gets or sets the <see langword="Date" /> HTTP header value to use in an HTTP request.</summary>
		/// <returns>The Date header value in the HTTP request.</returns>
		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x0600359A RID: 13722 RVA: 0x000BB180 File Offset: 0x000B9380
		// (set) Token: 0x0600359B RID: 13723 RVA: 0x000BB1BF File Offset: 0x000B93BF
		public DateTime Date
		{
			get
			{
				string text = this.webHeaders["Date"];
				if (text == null)
				{
					return DateTime.MinValue;
				}
				return DateTime.ParseExact(text, "r", CultureInfo.InvariantCulture).ToLocalTime();
			}
			set
			{
				this.SetDateHeaderHelper("Date", value);
			}
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x000BB1CD File Offset: 0x000B93CD
		private void SetDateHeaderHelper(string headerName, DateTime dateTime)
		{
			if (dateTime == DateTime.MinValue)
			{
				this.SetSpecialHeaders(headerName, null);
				return;
			}
			this.SetSpecialHeaders(headerName, HttpProtocolUtils.date2string(dateTime));
		}

		/// <summary>Gets or sets the default cache policy for this request.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> that specifies the cache policy in effect for this request when no other policy is applicable.</returns>
		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x0600359D RID: 13725 RVA: 0x000BB1F2 File Offset: 0x000B93F2
		// (set) Token: 0x0600359E RID: 13726 RVA: 0x000BB1F9 File Offset: 0x000B93F9
		[MonoTODO]
		public new static RequestCachePolicy DefaultCachePolicy
		{
			get
			{
				return HttpWebRequest.defaultCachePolicy;
			}
			set
			{
				HttpWebRequest.defaultCachePolicy = value;
			}
		}

		/// <summary>Gets or sets the default maximum length of an HTTP error response.</summary>
		/// <returns>The default maximum length of an HTTP error response.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0 and is not equal to -1.</exception>
		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x0600359F RID: 13727 RVA: 0x000BB201 File Offset: 0x000B9401
		// (set) Token: 0x060035A0 RID: 13728 RVA: 0x000BB208 File Offset: 0x000B9408
		[MonoTODO]
		public static int DefaultMaximumErrorResponseLength
		{
			get
			{
				return HttpWebRequest.defaultMaximumErrorResponseLength;
			}
			set
			{
				HttpWebRequest.defaultMaximumErrorResponseLength = value;
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Expect" /> HTTP header.</summary>
		/// <returns>The contents of the <see langword="Expect" /> HTTP header. The default value is <see langword="null" />.  
		///
		///  The value for this property is stored in <see cref="T:System.Net.WebHeaderCollection" />. If WebHeaderCollection is set, the property value is lost.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see langword="Expect" /> is set to a string that contains "100-continue" as a substring.</exception>
		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x060035A1 RID: 13729 RVA: 0x000BB210 File Offset: 0x000B9410
		// (set) Token: 0x060035A2 RID: 13730 RVA: 0x000BB224 File Offset: 0x000B9424
		public string Expect
		{
			get
			{
				return this.webHeaders["Expect"];
			}
			set
			{
				this.CheckRequestStarted();
				string text = value;
				if (text != null)
				{
					text = text.Trim().ToLower();
				}
				if (text == null || text.Length == 0)
				{
					this.webHeaders.RemoveInternal("Expect");
					return;
				}
				if (text == "100-continue")
				{
					throw new ArgumentException("100-Continue cannot be set with this property.", "value");
				}
				this.webHeaders.CheckUpdate("Expect", value);
			}
		}

		/// <summary>Gets a value that indicates whether a response has been received from an Internet resource.</summary>
		/// <returns>
		///   <see langword="true" /> if a response has been received; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060035A3 RID: 13731 RVA: 0x000BB292 File Offset: 0x000B9492
		public virtual bool HaveResponse
		{
			get
			{
				return this.haveResponse;
			}
		}

		/// <summary>Specifies a collection of the name/value pairs that make up the HTTP headers.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the name/value pairs that make up the headers for the HTTP request.</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has been started by calling the <see cref="M:System.Net.HttpWebRequest.GetRequestStream" />, <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />, <see cref="M:System.Net.HttpWebRequest.GetResponse" />, or <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> method.</exception>
		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060035A4 RID: 13732 RVA: 0x000BB29A File Offset: 0x000B949A
		// (set) Token: 0x060035A5 RID: 13733 RVA: 0x000BB2A4 File Offset: 0x000B94A4
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.webHeaders;
			}
			set
			{
				this.CheckRequestStarted();
				WebHeaderCollection webHeaderCollection = new WebHeaderCollection(WebHeaderCollectionType.HttpWebRequest);
				foreach (string name in value.AllKeys)
				{
					webHeaderCollection.Add(name, value[name]);
				}
				this.webHeaders = webHeaderCollection;
			}
		}

		/// <summary>Gets or sets the Host header value to use in an HTTP request independent from the request URI.</summary>
		/// <returns>The Host header value in the HTTP request.</returns>
		/// <exception cref="T:System.ArgumentNullException">The Host header cannot be set to <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The Host header cannot be set to an invalid value.</exception>
		/// <exception cref="T:System.InvalidOperationException">The Host header cannot be set after the <see cref="T:System.Net.HttpWebRequest" /> has already started to be sent.</exception>
		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x060035A6 RID: 13734 RVA: 0x000BB2F4 File Offset: 0x000B94F4
		// (set) Token: 0x060035A7 RID: 13735 RVA: 0x000BB35C File Offset: 0x000B955C
		public string Host
		{
			get
			{
				Uri uri = this.hostUri ?? this.Address;
				if ((!(this.hostUri == null) && this.hostHasPort) || !this.Address.IsDefaultPort)
				{
					return uri.Host + ":" + uri.Port.ToString();
				}
				return uri.Host;
			}
			set
			{
				this.CheckRequestStarted();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				Uri uri;
				if (value.IndexOf('/') != -1 || !this.TryGetHostUri(value, out uri))
				{
					throw new ArgumentException("The specified value is not a valid Host header string.", "value");
				}
				this.hostUri = uri;
				if (!this.hostUri.IsDefaultPort)
				{
					this.hostHasPort = true;
					return;
				}
				if (value.IndexOf(':') == -1)
				{
					this.hostHasPort = false;
					return;
				}
				int num = value.IndexOf(']');
				this.hostHasPort = (num == -1 || value.LastIndexOf(':') > num);
			}
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x000BB3F3 File Offset: 0x000B95F3
		private bool TryGetHostUri(string hostName, out Uri hostUri)
		{
			return Uri.TryCreate(this.Address.Scheme + "://" + hostName + this.Address.PathAndQuery, UriKind.Absolute, out hostUri);
		}

		/// <summary>Gets or sets the value of the <see langword="If-Modified-Since" /> HTTP header.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the contents of the <see langword="If-Modified-Since" /> HTTP header. The default value is the current date and time.</returns>
		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060035A9 RID: 13737 RVA: 0x000BB420 File Offset: 0x000B9620
		// (set) Token: 0x060035AA RID: 13738 RVA: 0x000BB46C File Offset: 0x000B966C
		public DateTime IfModifiedSince
		{
			get
			{
				string text = this.webHeaders["If-Modified-Since"];
				if (text == null)
				{
					return DateTime.Now;
				}
				DateTime result;
				try
				{
					result = MonoHttpDate.Parse(text);
				}
				catch (Exception)
				{
					result = DateTime.Now;
				}
				return result;
			}
			set
			{
				this.CheckRequestStarted();
				this.webHeaders.SetInternal("If-Modified-Since", value.ToUniversalTime().ToString("r", null));
			}
		}

		/// <summary>Gets or sets a value that indicates whether to make a persistent connection to the Internet resource.</summary>
		/// <returns>
		///   <see langword="true" /> if the request to the Internet resource should contain a <see langword="Connection" /> HTTP header with the value Keep-alive; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x060035AB RID: 13739 RVA: 0x000BB4A4 File Offset: 0x000B96A4
		// (set) Token: 0x060035AC RID: 13740 RVA: 0x000BB4AC File Offset: 0x000B96AC
		public bool KeepAlive
		{
			get
			{
				return this.keepAlive;
			}
			set
			{
				this.keepAlive = value;
			}
		}

		/// <summary>Gets or sets the maximum number of redirects that the request follows.</summary>
		/// <returns>The maximum number of redirection responses that the request follows. The default value is 50.</returns>
		/// <exception cref="T:System.ArgumentException">The value is set to 0 or less.</exception>
		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x060035AD RID: 13741 RVA: 0x000BB4B5 File Offset: 0x000B96B5
		// (set) Token: 0x060035AE RID: 13742 RVA: 0x000BB4BD File Offset: 0x000B96BD
		public int MaximumAutomaticRedirections
		{
			get
			{
				return this.maxAutoRedirect;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("Must be > 0", "value");
				}
				this.maxAutoRedirect = value;
			}
		}

		/// <summary>Gets or sets the maximum allowed length of the response headers.</summary>
		/// <returns>The length, in kilobytes (1024 bytes), of the response headers.</returns>
		/// <exception cref="T:System.InvalidOperationException">The property is set after the request has already been submitted.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0 and is not equal to -1.</exception>
		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x060035AF RID: 13743 RVA: 0x000BB4DA File Offset: 0x000B96DA
		// (set) Token: 0x060035B0 RID: 13744 RVA: 0x000BB4E2 File Offset: 0x000B96E2
		[MonoTODO("Use this")]
		public int MaximumResponseHeadersLength
		{
			get
			{
				return this.maxResponseHeadersLength;
			}
			set
			{
				this.CheckRequestStarted();
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", "The specified value must be greater than 0.");
				}
				this.maxResponseHeadersLength = value;
			}
		}

		/// <summary>Gets or sets the default for the <see cref="P:System.Net.HttpWebRequest.MaximumResponseHeadersLength" /> property.</summary>
		/// <returns>The length, in kilobytes (1024 bytes), of the default maximum for response headers received. The default configuration file sets this value to 64 kilobytes.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is not equal to -1 and is less than zero.</exception>
		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x060035B1 RID: 13745 RVA: 0x000BB509 File Offset: 0x000B9709
		// (set) Token: 0x060035B2 RID: 13746 RVA: 0x000BB510 File Offset: 0x000B9710
		[MonoTODO("Use this")]
		public static int DefaultMaximumResponseHeadersLength
		{
			get
			{
				return HttpWebRequest.defaultMaxResponseHeadersLength;
			}
			set
			{
				HttpWebRequest.defaultMaxResponseHeadersLength = value;
			}
		}

		/// <summary>Gets or sets a time-out in milliseconds when writing to or reading from a stream.</summary>
		/// <returns>The number of milliseconds before the writing or reading times out. The default value is 300,000 milliseconds (5 minutes).</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has already been sent.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /></exception>
		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x060035B3 RID: 13747 RVA: 0x000BB518 File Offset: 0x000B9718
		// (set) Token: 0x060035B4 RID: 13748 RVA: 0x000BB520 File Offset: 0x000B9720
		public int ReadWriteTimeout
		{
			get
			{
				return this.readWriteTimeout;
			}
			set
			{
				this.CheckRequestStarted();
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value > 0.");
				}
				this.readWriteTimeout = value;
			}
		}

		/// <summary>Gets or sets a timeout, in milliseconds, to wait until the 100-Continue is received from the server.</summary>
		/// <returns>The timeout, in milliseconds, to wait until the 100-Continue is received.</returns>
		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x060035B5 RID: 13749 RVA: 0x000BB547 File Offset: 0x000B9747
		// (set) Token: 0x060035B6 RID: 13750 RVA: 0x000BB54F File Offset: 0x000B974F
		[MonoTODO]
		public int ContinueTimeout
		{
			get
			{
				return this.continueTimeout;
			}
			set
			{
				this.CheckRequestStarted();
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value >= 0.");
				}
				this.continueTimeout = value;
			}
		}

		/// <summary>Gets or sets the media type of the request.</summary>
		/// <returns>The media type of the request. The default value is <see langword="null" />.</returns>
		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x060035B7 RID: 13751 RVA: 0x000BB576 File Offset: 0x000B9776
		// (set) Token: 0x060035B8 RID: 13752 RVA: 0x000BB57E File Offset: 0x000B977E
		public string MediaType
		{
			get
			{
				return this.mediaType;
			}
			set
			{
				this.mediaType = value;
			}
		}

		/// <summary>Gets or sets the method for the request.</summary>
		/// <returns>The request method to use to contact the Internet resource. The default value is GET.</returns>
		/// <exception cref="T:System.ArgumentException">No method is supplied.  
		///  -or-  
		///  The method string contains invalid characters.</exception>
		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x060035B9 RID: 13753 RVA: 0x000BB587 File Offset: 0x000B9787
		// (set) Token: 0x060035BA RID: 13754 RVA: 0x000BB590 File Offset: 0x000B9790
		public override string Method
		{
			get
			{
				return this.method;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("Cannot set null or blank methods on request.", "value");
				}
				if (HttpValidationHelpers.IsInvalidMethodOrHeaderString(value))
				{
					throw new ArgumentException("Cannot set null or blank methods on request.", "value");
				}
				this.method = value.ToUpperInvariant();
				if (this.method != "HEAD" && this.method != "GET" && this.method != "POST" && this.method != "PUT" && this.method != "DELETE" && this.method != "CONNECT" && this.method != "TRACE" && this.method != "MKCOL")
				{
					this.method = value;
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether to pipeline the request to the Internet resource.</summary>
		/// <returns>
		///   <see langword="true" /> if the request should be pipelined; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x060035BB RID: 13755 RVA: 0x000BB673 File Offset: 0x000B9873
		// (set) Token: 0x060035BC RID: 13756 RVA: 0x000BB67B File Offset: 0x000B987B
		public bool Pipelined
		{
			get
			{
				return this.pipelined;
			}
			set
			{
				this.pipelined = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to send an Authorization header with the request.</summary>
		/// <returns>
		///   <see langword="true" /> to send an  HTTP Authorization header with requests after authentication has taken place; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x060035BD RID: 13757 RVA: 0x000BB684 File Offset: 0x000B9884
		// (set) Token: 0x060035BE RID: 13758 RVA: 0x000BB68C File Offset: 0x000B988C
		public override bool PreAuthenticate
		{
			get
			{
				return this.preAuthenticate;
			}
			set
			{
				this.preAuthenticate = value;
			}
		}

		/// <summary>Gets or sets the version of HTTP to use for the request.</summary>
		/// <returns>The HTTP version to use for the request. The default is <see cref="F:System.Net.HttpVersion.Version11" />.</returns>
		/// <exception cref="T:System.ArgumentException">The HTTP version is set to a value other than 1.0 or 1.1.</exception>
		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x060035BF RID: 13759 RVA: 0x000BB695 File Offset: 0x000B9895
		// (set) Token: 0x060035C0 RID: 13760 RVA: 0x000BB69D File Offset: 0x000B989D
		public Version ProtocolVersion
		{
			get
			{
				return this.version;
			}
			set
			{
				if (value != HttpVersion.Version10 && value != HttpVersion.Version11)
				{
					throw new ArgumentException("Only HTTP/1.0 and HTTP/1.1 version requests are currently supported.", "value");
				}
				this.force_version = true;
				this.version = value;
			}
		}

		/// <summary>Gets or sets proxy information for the request.</summary>
		/// <returns>The <see cref="T:System.Net.IWebProxy" /> object to use to proxy the request. The default value is set by calling the <see cref="P:System.Net.GlobalProxySelection.Select" /> property.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Net.HttpWebRequest.Proxy" /> is set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The request has been started by calling <see cref="M:System.Net.HttpWebRequest.GetRequestStream" />, <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />, <see cref="M:System.Net.HttpWebRequest.GetResponse" />, or <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have permission for the requested operation.</exception>
		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x060035C1 RID: 13761 RVA: 0x000BB6D7 File Offset: 0x000B98D7
		// (set) Token: 0x060035C2 RID: 13762 RVA: 0x000BB6DF File Offset: 0x000B98DF
		public override IWebProxy Proxy
		{
			get
			{
				return this.proxy;
			}
			set
			{
				this.CheckRequestStarted();
				this.proxy = value;
				this.servicePoint = null;
				this.GetServicePoint();
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Referer" /> HTTP header.</summary>
		/// <returns>The value of the <see langword="Referer" /> HTTP header. The default value is <see langword="null" />.</returns>
		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x060035C3 RID: 13763 RVA: 0x000BB6FC File Offset: 0x000B98FC
		// (set) Token: 0x060035C4 RID: 13764 RVA: 0x000BB70E File Offset: 0x000B990E
		public string Referer
		{
			get
			{
				return this.webHeaders["Referer"];
			}
			set
			{
				this.CheckRequestStarted();
				if (value == null || value.Trim().Length == 0)
				{
					this.webHeaders.RemoveInternal("Referer");
					return;
				}
				this.webHeaders.SetInternal("Referer", value);
			}
		}

		/// <summary>Gets the original Uniform Resource Identifier (URI) of the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI of the Internet resource passed to the <see cref="M:System.Net.WebRequest.Create(System.String)" /> method.</returns>
		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x060035C5 RID: 13765 RVA: 0x000BB748 File Offset: 0x000B9948
		public override Uri RequestUri
		{
			get
			{
				return this.requestUri;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to send data in segments to the Internet resource.</summary>
		/// <returns>
		///   <see langword="true" /> to send data to the Internet resource in segments; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has been started by calling the <see cref="M:System.Net.HttpWebRequest.GetRequestStream" />, <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />, <see cref="M:System.Net.HttpWebRequest.GetResponse" />, or <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> method.</exception>
		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x060035C6 RID: 13766 RVA: 0x000BB750 File Offset: 0x000B9950
		// (set) Token: 0x060035C7 RID: 13767 RVA: 0x000BB758 File Offset: 0x000B9958
		public bool SendChunked
		{
			get
			{
				return this.sendChunked;
			}
			set
			{
				this.CheckRequestStarted();
				this.sendChunked = value;
			}
		}

		/// <summary>Gets the service point to use for the request.</summary>
		/// <returns>A <see cref="T:System.Net.ServicePoint" /> that represents the network connection to the Internet resource.</returns>
		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x060035C8 RID: 13768 RVA: 0x000BB767 File Offset: 0x000B9967
		public ServicePoint ServicePoint
		{
			get
			{
				return this.GetServicePoint();
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x060035C9 RID: 13769 RVA: 0x000BB76F File Offset: 0x000B996F
		internal ServicePoint ServicePointNoLock
		{
			get
			{
				return this.servicePoint;
			}
		}

		/// <summary>Gets a value that indicates whether the request provides support for a <see cref="T:System.Net.CookieContainer" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the request provides support for a <see cref="T:System.Net.CookieContainer" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x060035CA RID: 13770 RVA: 0x0000390E File Offset: 0x00001B0E
		public virtual bool SupportsCookieContainer
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the time-out value in milliseconds for the <see cref="M:System.Net.HttpWebRequest.GetResponse" /> and <see cref="M:System.Net.HttpWebRequest.GetRequestStream" /> methods.</summary>
		/// <returns>The number of milliseconds to wait before the request times out. The default value is 100,000 milliseconds (100 seconds).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than zero and is not <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x060035CB RID: 13771 RVA: 0x000BB777 File Offset: 0x000B9977
		// (set) Token: 0x060035CC RID: 13772 RVA: 0x000BB77F File Offset: 0x000B997F
		public override int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.timeout = value;
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Transfer-encoding" /> HTTP header.</summary>
		/// <returns>The value of the <see langword="Transfer-encoding" /> HTTP header. The default value is <see langword="null" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set when <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is <see langword="false" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to the value "Chunked".</exception>
		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x060035CD RID: 13773 RVA: 0x000BB797 File Offset: 0x000B9997
		// (set) Token: 0x060035CE RID: 13774 RVA: 0x000BB7AC File Offset: 0x000B99AC
		public string TransferEncoding
		{
			get
			{
				return this.webHeaders["Transfer-Encoding"];
			}
			set
			{
				this.CheckRequestStarted();
				if (string.IsNullOrWhiteSpace(value))
				{
					this.webHeaders.RemoveInternal("Transfer-Encoding");
					return;
				}
				if (value.ToLower().Contains("chunked"))
				{
					throw new ArgumentException("Chunked encoding must be set via the SendChunked property.", "value");
				}
				if (!this.SendChunked)
				{
					throw new InvalidOperationException("TransferEncoding requires the SendChunked property to be set to true.");
				}
				string value2 = HttpValidationHelpers.CheckBadHeaderValueChars(value);
				this.webHeaders.CheckUpdate("Transfer-Encoding", value2);
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether default credentials are sent with requests.</summary>
		/// <returns>
		///   <see langword="true" /> if the default credentials are used; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">You attempted to set this property after the request was sent.</exception>
		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x060035CF RID: 13775 RVA: 0x000BB825 File Offset: 0x000B9A25
		// (set) Token: 0x060035D0 RID: 13776 RVA: 0x000BB834 File Offset: 0x000B9A34
		public override bool UseDefaultCredentials
		{
			get
			{
				return CredentialCache.DefaultCredentials == this.Credentials;
			}
			set
			{
				this.Credentials = (value ? CredentialCache.DefaultCredentials : null);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="User-agent" /> HTTP header.</summary>
		/// <returns>The value of the <see langword="User-agent" /> HTTP header. The default value is <see langword="null" />.  
		///
		///  The value for this property is stored in <see cref="T:System.Net.WebHeaderCollection" />. If WebHeaderCollection is set, the property value is lost.</returns>
		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x060035D1 RID: 13777 RVA: 0x000BB847 File Offset: 0x000B9A47
		// (set) Token: 0x060035D2 RID: 13778 RVA: 0x000BB859 File Offset: 0x000B9A59
		public string UserAgent
		{
			get
			{
				return this.webHeaders["User-Agent"];
			}
			set
			{
				this.webHeaders.SetInternal("User-Agent", value);
			}
		}

		/// <summary>Gets or sets a value that indicates whether to allow high-speed NTLM-authenticated connection sharing.</summary>
		/// <returns>
		///   <see langword="true" /> to keep the authenticated connection open; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060035D3 RID: 13779 RVA: 0x000BB86C File Offset: 0x000B9A6C
		// (set) Token: 0x060035D4 RID: 13780 RVA: 0x000BB874 File Offset: 0x000B9A74
		public bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return this.unsafe_auth_blah;
			}
			set
			{
				this.unsafe_auth_blah = value;
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060035D5 RID: 13781 RVA: 0x000BB87D File Offset: 0x000B9A7D
		internal bool GotRequestStream
		{
			get
			{
				return this.gotRequestStream;
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060035D6 RID: 13782 RVA: 0x000BB885 File Offset: 0x000B9A85
		// (set) Token: 0x060035D7 RID: 13783 RVA: 0x000BB88D File Offset: 0x000B9A8D
		internal bool ExpectContinue
		{
			get
			{
				return this.expectContinue;
			}
			set
			{
				this.expectContinue = value;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060035D8 RID: 13784 RVA: 0x000BAF2E File Offset: 0x000B912E
		internal Uri AuthUri
		{
			get
			{
				return this.actualUri;
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x060035D9 RID: 13785 RVA: 0x000BB896 File Offset: 0x000B9A96
		internal bool ProxyQuery
		{
			get
			{
				return this.servicePoint.UsesProxy && !this.servicePoint.UseConnect;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x060035DA RID: 13786 RVA: 0x000BB8B5 File Offset: 0x000B9AB5
		internal ServerCertValidationCallback ServerCertValidationCallback
		{
			get
			{
				return this.certValidationCallback;
			}
		}

		/// <summary>Gets or sets a callback function to validate the server certificate.</summary>
		/// <returns>A callback function to validate the server certificate.</returns>
		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x060035DB RID: 13787 RVA: 0x000BB8BD File Offset: 0x000B9ABD
		// (set) Token: 0x060035DC RID: 13788 RVA: 0x000BB8D4 File Offset: 0x000B9AD4
		public RemoteCertificateValidationCallback ServerCertificateValidationCallback
		{
			get
			{
				if (this.certValidationCallback == null)
				{
					return null;
				}
				return this.certValidationCallback.ValidationCallback;
			}
			set
			{
				if (value == null)
				{
					this.certValidationCallback = null;
					return;
				}
				this.certValidationCallback = new ServerCertValidationCallback(value);
			}
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000BB8F0 File Offset: 0x000B9AF0
		internal ServicePoint GetServicePoint()
		{
			object obj = this.locker;
			lock (obj)
			{
				if (this.hostChanged || this.servicePoint == null)
				{
					this.servicePoint = ServicePointManager.FindServicePoint(this.actualUri, this.proxy);
					this.hostChanged = false;
				}
			}
			return this.servicePoint;
		}

		/// <summary>Adds a byte range header to a request for a specific range from the beginning or end of the requested data.</summary>
		/// <param name="range">The starting or ending point of the range.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added.</exception>
		// Token: 0x060035DE RID: 13790 RVA: 0x000BB960 File Offset: 0x000B9B60
		public void AddRange(int range)
		{
			this.AddRange("bytes", (long)range);
		}

		/// <summary>Adds a byte range header to the request for a specified range.</summary>
		/// <param name="from">The position at which to start sending data.</param>
		/// <param name="to">The position at which to stop sending data.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="from" /> is greater than <paramref name="to" />  
		/// -or-  
		/// <paramref name="from" /> or <paramref name="to" /> is less than 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added.</exception>
		// Token: 0x060035DF RID: 13791 RVA: 0x000BB96F File Offset: 0x000B9B6F
		public void AddRange(int from, int to)
		{
			this.AddRange("bytes", (long)from, (long)to);
		}

		/// <summary>Adds a Range header to a request for a specific range from the beginning or end of the requested data.</summary>
		/// <param name="rangeSpecifier">The description of the range.</param>
		/// <param name="range">The starting or ending point of the range.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rangeSpecifier" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added.</exception>
		// Token: 0x060035E0 RID: 13792 RVA: 0x000BB980 File Offset: 0x000B9B80
		public void AddRange(string rangeSpecifier, int range)
		{
			this.AddRange(rangeSpecifier, (long)range);
		}

		/// <summary>Adds a range header to a request for a specified range.</summary>
		/// <param name="rangeSpecifier">The description of the range.</param>
		/// <param name="from">The position at which to start sending data.</param>
		/// <param name="to">The position at which to stop sending data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rangeSpecifier" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="from" /> is greater than <paramref name="to" />  
		/// -or-  
		/// <paramref name="from" /> or <paramref name="to" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added.</exception>
		// Token: 0x060035E1 RID: 13793 RVA: 0x000BB98B File Offset: 0x000B9B8B
		public void AddRange(string rangeSpecifier, int from, int to)
		{
			this.AddRange(rangeSpecifier, (long)from, (long)to);
		}

		/// <summary>Adds a byte range header to a request for a specific range from the beginning or end of the requested data.</summary>
		/// <param name="range">The starting or ending point of the range.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added.</exception>
		// Token: 0x060035E2 RID: 13794 RVA: 0x000BB998 File Offset: 0x000B9B98
		public void AddRange(long range)
		{
			this.AddRange("bytes", range);
		}

		/// <summary>Adds a byte range header to the request for a specified range.</summary>
		/// <param name="from">The position at which to start sending data.</param>
		/// <param name="to">The position at which to stop sending data.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="from" /> is greater than <paramref name="to" />  
		/// -or-  
		/// <paramref name="from" /> or <paramref name="to" /> is less than 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added.</exception>
		// Token: 0x060035E3 RID: 13795 RVA: 0x000BB9A6 File Offset: 0x000B9BA6
		public void AddRange(long from, long to)
		{
			this.AddRange("bytes", from, to);
		}

		/// <summary>Adds a Range header to a request for a specific range from the beginning or end of the requested data.</summary>
		/// <param name="rangeSpecifier">The description of the range.</param>
		/// <param name="range">The starting or ending point of the range.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rangeSpecifier" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added.</exception>
		// Token: 0x060035E4 RID: 13796 RVA: 0x000BB9B8 File Offset: 0x000B9BB8
		public void AddRange(string rangeSpecifier, long range)
		{
			if (rangeSpecifier == null)
			{
				throw new ArgumentNullException("rangeSpecifier");
			}
			if (!WebHeaderCollection.IsValidToken(rangeSpecifier))
			{
				throw new ArgumentException("Invalid range specifier", "rangeSpecifier");
			}
			string text = this.webHeaders["Range"];
			if (text == null)
			{
				text = rangeSpecifier + "=";
			}
			else
			{
				if (string.Compare(text.Substring(0, text.IndexOf('=')), rangeSpecifier, StringComparison.OrdinalIgnoreCase) != 0)
				{
					throw new InvalidOperationException("A different range specifier is already in use");
				}
				text += ",";
			}
			string text2 = range.ToString(CultureInfo.InvariantCulture);
			if (range < 0L)
			{
				text = text + "0" + text2;
			}
			else
			{
				text = text + text2 + "-";
			}
			this.webHeaders.ChangeInternal("Range", text);
		}

		/// <summary>Adds a range header to a request for a specified range.</summary>
		/// <param name="rangeSpecifier">The description of the range.</param>
		/// <param name="from">The position at which to start sending data.</param>
		/// <param name="to">The position at which to stop sending data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rangeSpecifier" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="from" /> is greater than <paramref name="to" />  
		/// -or-  
		/// <paramref name="from" /> or <paramref name="to" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added.</exception>
		// Token: 0x060035E5 RID: 13797 RVA: 0x000BBA7C File Offset: 0x000B9C7C
		public void AddRange(string rangeSpecifier, long from, long to)
		{
			if (rangeSpecifier == null)
			{
				throw new ArgumentNullException("rangeSpecifier");
			}
			if (!WebHeaderCollection.IsValidToken(rangeSpecifier))
			{
				throw new ArgumentException("Invalid range specifier", "rangeSpecifier");
			}
			if (from > to || from < 0L)
			{
				throw new ArgumentOutOfRangeException("from");
			}
			if (to < 0L)
			{
				throw new ArgumentOutOfRangeException("to");
			}
			string text = this.webHeaders["Range"];
			if (text == null)
			{
				text = rangeSpecifier + "=";
			}
			else
			{
				text += ",";
			}
			text = string.Format("{0}{1}-{2}", text, from, to);
			this.webHeaders.ChangeInternal("Range", text);
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x000BBB2C File Offset: 0x000B9D2C
		private WebOperation SendRequest(bool redirecting, BufferOffsetSize writeBuffer, CancellationToken cancellationToken)
		{
			object obj = this.locker;
			WebOperation result;
			lock (obj)
			{
				if (!redirecting && this.requestSent)
				{
					WebOperation webOperation = this.currentOperation;
					if (webOperation == null)
					{
						throw new InvalidOperationException("Should never happen!");
					}
					result = webOperation;
				}
				else
				{
					WebOperation webOperation = new WebOperation(this, writeBuffer, false, cancellationToken);
					if (Interlocked.CompareExchange<WebOperation>(ref this.currentOperation, webOperation, null) != null)
					{
						throw new InvalidOperationException("Invalid nested call.");
					}
					this.requestSent = true;
					if (!redirecting)
					{
						this.redirects = 0;
					}
					this.servicePoint = this.GetServicePoint();
					this.servicePoint.SendRequest(webOperation, this.connectionGroup);
					result = webOperation;
				}
			}
			return result;
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x000BBBE0 File Offset: 0x000B9DE0
		private Task<Stream> MyGetRequestStreamAsync(CancellationToken cancellationToken)
		{
			if (this.Aborted)
			{
				throw HttpWebRequest.CreateRequestAbortedException();
			}
			bool flag = !(this.method == "GET") && !(this.method == "CONNECT") && !(this.method == "HEAD") && !(this.method == "TRACE");
			if (this.method == null || !flag)
			{
				throw new ProtocolViolationException("Cannot send a content-body with this verb-type.");
			}
			if (this.contentLength == -1L && !this.sendChunked && !this.allowBuffering && this.KeepAlive)
			{
				throw new ProtocolViolationException("Content-Length not set");
			}
			string transferEncoding = this.TransferEncoding;
			if (!this.sendChunked && transferEncoding != null && transferEncoding.Trim() != "")
			{
				throw new InvalidOperationException("TransferEncoding requires the SendChunked property to be set to true.");
			}
			object obj = this.locker;
			WebOperation webOperation;
			lock (obj)
			{
				if (this.getResponseCalled)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				webOperation = this.currentOperation;
				if (webOperation == null)
				{
					this.initialMethod = this.method;
					this.gotRequestStream = true;
					webOperation = this.SendRequest(false, null, cancellationToken);
				}
			}
			return webOperation.GetRequestStream();
		}

		/// <summary>Begins an asynchronous request for a <see cref="T:System.IO.Stream" /> object to use to write data.</summary>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">The state object for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.HttpWebRequest.Method" /> property is GET or HEAD.  
		///  -or-  
		///  <see cref="P:System.Net.HttpWebRequest.KeepAlive" /> is <see langword="true" />, <see cref="P:System.Net.HttpWebRequest.AllowWriteStreamBuffering" /> is <see langword="false" />, <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is -1, <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is <see langword="false" />, and <see cref="P:System.Net.HttpWebRequest.Method" /> is POST or PUT.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is being used by a previous call to <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />  
		///  -or-  
		///  <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to a value and <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is <see langword="false" />.  
		///  -or-  
		///  The thread pool is running out of threads.</exception>
		/// <exception cref="T:System.NotSupportedException">The request cache validator indicated that the response for this request can be served from the cache; however, requests that write data must not use the cache. This exception can occur if you are using a custom cache validator that is incorrectly implemented.</exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.</exception>
		/// <exception cref="T:System.ObjectDisposedException">In a .NET Compact Framework application, a request stream with zero content length was not obtained and closed correctly. For more information about handling zero content length requests, see Network Programming in the .NET Compact Framework.</exception>
		// Token: 0x060035E8 RID: 13800 RVA: 0x000BBD2C File Offset: 0x000B9F2C
		public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.RunWithTimeout<Stream>(new Func<CancellationToken, Task<Stream>>(this.MyGetRequestStreamAsync)), callback, state);
		}

		/// <summary>Ends an asynchronous request for a <see cref="T:System.IO.Stream" /> object to use to write data.</summary>
		/// <param name="asyncResult">The pending request for a stream.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> to use to write request data.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">The request did not complete, and no stream is available.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by the current instance from a call to <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This method was called previously using <paramref name="asyncResult" />.</exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.  
		/// -or-  
		/// An error occurred while processing the request.</exception>
		// Token: 0x060035E9 RID: 13801 RVA: 0x000BBD48 File Offset: 0x000B9F48
		public override Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Stream result;
			try
			{
				result = TaskToApm.End<Stream>(asyncResult);
			}
			catch (Exception e)
			{
				throw this.GetWebException(e);
			}
			return result;
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object to use to write request data.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> to use to write request data.</returns>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.HttpWebRequest.Method" /> property is GET or HEAD.  
		///  -or-  
		///  <see cref="P:System.Net.HttpWebRequest.KeepAlive" /> is <see langword="true" />, <see cref="P:System.Net.HttpWebRequest.AllowWriteStreamBuffering" /> is <see langword="false" />, <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is -1, <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is <see langword="false" />, and <see cref="P:System.Net.HttpWebRequest.Method" /> is POST or PUT.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.HttpWebRequest.GetRequestStream" /> method is called more than once.  
		///  -or-  
		///  <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to a value and <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is <see langword="false" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The request cache validator indicated that the response for this request can be served from the cache; however, requests that write data must not use the cache. This exception can occur if you are using a custom cache validator that is incorrectly implemented.</exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.  
		/// -or-  
		/// The time-out period for the request expired.  
		/// -or-  
		/// An error occurred while processing the request.</exception>
		/// <exception cref="T:System.ObjectDisposedException">In a .NET Compact Framework application, a request stream with zero content length was not obtained and closed correctly. For more information about handling zero content length requests, see Network Programming in the .NET Compact Framework.</exception>
		// Token: 0x060035EA RID: 13802 RVA: 0x000BBD88 File Offset: 0x000B9F88
		public override Stream GetRequestStream()
		{
			Stream result;
			try
			{
				result = this.GetRequestStreamAsync().Result;
			}
			catch (Exception e)
			{
				throw this.GetWebException(e);
			}
			return result;
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object to use to write request data and outputs the <see cref="T:System.Net.TransportContext" /> associated with the stream.</summary>
		/// <param name="context">The <see cref="T:System.Net.TransportContext" /> for the <see cref="T:System.IO.Stream" />.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> to use to write request data.</returns>
		/// <exception cref="T:System.Exception">The <see cref="M:System.Net.HttpWebRequest.GetRequestStream" /> method was unable to obtain the <see cref="T:System.IO.Stream" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.HttpWebRequest.GetRequestStream" /> method is called more than once.  
		///  -or-  
		///  <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to a value and <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is <see langword="false" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The request cache validator indicated that the response for this request can be served from the cache; however, requests that write data must not use the cache. This exception can occur if you are using a custom cache validator that is incorrectly implemented.</exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.HttpWebRequest.Method" /> property is GET or HEAD.  
		///  -or-  
		///  <see cref="P:System.Net.HttpWebRequest.KeepAlive" /> is <see langword="true" />, <see cref="P:System.Net.HttpWebRequest.AllowWriteStreamBuffering" /> is <see langword="false" />, <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is -1, <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is <see langword="false" />, and <see cref="P:System.Net.HttpWebRequest.Method" /> is POST or PUT.</exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.  
		/// -or-  
		/// The time-out period for the request expired.  
		/// -or-  
		/// An error occurred while processing the request.</exception>
		// Token: 0x060035EB RID: 13803 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public Stream GetRequestStream(out TransportContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x000BBDC0 File Offset: 0x000B9FC0
		public override Task<Stream> GetRequestStreamAsync()
		{
			return this.RunWithTimeout<Stream>(new Func<CancellationToken, Task<Stream>>(this.MyGetRequestStreamAsync));
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000BBDD4 File Offset: 0x000B9FD4
		internal static Task<T> RunWithTimeout<T>(Func<CancellationToken, Task<T>> func, int timeout, Action abort, Func<bool> aborted, CancellationToken cancellationToken)
		{
			CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			return HttpWebRequest.RunWithTimeoutWorker<T>(func(cancellationTokenSource.Token), timeout, abort, aborted, cancellationTokenSource);
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x000BBE00 File Offset: 0x000BA000
		private static Task<T> RunWithTimeoutWorker<T>(Task<T> workerTask, int timeout, Action abort, Func<bool> aborted, CancellationTokenSource cts)
		{
			HttpWebRequest.<RunWithTimeoutWorker>d__244<T> <RunWithTimeoutWorker>d__;
			<RunWithTimeoutWorker>d__.workerTask = workerTask;
			<RunWithTimeoutWorker>d__.timeout = timeout;
			<RunWithTimeoutWorker>d__.abort = abort;
			<RunWithTimeoutWorker>d__.aborted = aborted;
			<RunWithTimeoutWorker>d__.cts = cts;
			<RunWithTimeoutWorker>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<RunWithTimeoutWorker>d__.<>1__state = -1;
			<RunWithTimeoutWorker>d__.<>t__builder.Start<HttpWebRequest.<RunWithTimeoutWorker>d__244<T>>(ref <RunWithTimeoutWorker>d__);
			return <RunWithTimeoutWorker>d__.<>t__builder.Task;
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000BBE64 File Offset: 0x000BA064
		private Task<T> RunWithTimeout<T>(Func<CancellationToken, Task<T>> func)
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			return HttpWebRequest.RunWithTimeoutWorker<T>(func(cancellationTokenSource.Token), this.timeout, new Action(this.Abort), () => this.Aborted, cancellationTokenSource);
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x000BBEA8 File Offset: 0x000BA0A8
		private Task<HttpWebResponse> MyGetResponseAsync(CancellationToken cancellationToken)
		{
			HttpWebRequest.<MyGetResponseAsync>d__246 <MyGetResponseAsync>d__;
			<MyGetResponseAsync>d__.<>4__this = this;
			<MyGetResponseAsync>d__.cancellationToken = cancellationToken;
			<MyGetResponseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<HttpWebResponse>.Create();
			<MyGetResponseAsync>d__.<>1__state = -1;
			<MyGetResponseAsync>d__.<>t__builder.Start<HttpWebRequest.<MyGetResponseAsync>d__246>(ref <MyGetResponseAsync>d__);
			return <MyGetResponseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x000BBEF4 File Offset: 0x000BA0F4
		[return: TupleElementNames(new string[]
		{
			"response",
			"redirect",
			"mustReadAll",
			"writeBuffer",
			"ntlm"
		})]
		private Task<ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation>> GetResponseFromData(WebResponseStream stream, CancellationToken cancellationToken)
		{
			HttpWebRequest.<GetResponseFromData>d__247 <GetResponseFromData>d__;
			<GetResponseFromData>d__.<>4__this = this;
			<GetResponseFromData>d__.stream = stream;
			<GetResponseFromData>d__.cancellationToken = cancellationToken;
			<GetResponseFromData>d__.<>t__builder = AsyncTaskMethodBuilder<ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation>>.Create();
			<GetResponseFromData>d__.<>1__state = -1;
			<GetResponseFromData>d__.<>t__builder.Start<HttpWebRequest.<GetResponseFromData>d__247>(ref <GetResponseFromData>d__);
			return <GetResponseFromData>d__.<>t__builder.Task;
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x000BBF48 File Offset: 0x000BA148
		internal static Exception FlattenException(Exception e)
		{
			AggregateException ex = e as AggregateException;
			if (ex != null)
			{
				ex = ex.Flatten();
				if (ex.InnerExceptions.Count == 1)
				{
					return ex.InnerException;
				}
			}
			return e;
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x000BBF7C File Offset: 0x000BA17C
		private WebException GetWebException(Exception e)
		{
			return HttpWebRequest.GetWebException(e, this.Aborted);
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000BBF8C File Offset: 0x000BA18C
		private static WebException GetWebException(Exception e, bool aborted)
		{
			e = HttpWebRequest.FlattenException(e);
			WebException ex = e as WebException;
			if (ex != null && (!aborted || ex.Status == WebExceptionStatus.RequestCanceled || ex.Status == WebExceptionStatus.Timeout))
			{
				return ex;
			}
			if (aborted || e is OperationCanceledException || e is ObjectDisposedException)
			{
				return HttpWebRequest.CreateRequestAbortedException();
			}
			return new WebException(e.Message, e, WebExceptionStatus.UnknownError, null);
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x000BBFEB File Offset: 0x000BA1EB
		internal static WebException CreateRequestAbortedException()
		{
			return new WebException(SR.Format("The request was aborted: The request was canceled.", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
		}

		/// <summary>Begins an asynchronous request to an Internet resource.</summary>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate</param>
		/// <param name="state">The state object for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request for a response.</returns>
		/// <exception cref="T:System.InvalidOperationException">The stream is already in use by a previous call to <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />  
		///  -or-  
		///  <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to a value and <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is <see langword="false" />.  
		///  -or-  
		///  The thread pool is running out of threads.</exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">
		///   <see cref="P:System.Net.HttpWebRequest.Method" /> is GET or HEAD, and either <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is greater than zero or <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is <see langword="true" />.  
		/// -or-  
		/// <see cref="P:System.Net.HttpWebRequest.KeepAlive" /> is <see langword="true" />, <see cref="P:System.Net.HttpWebRequest.AllowWriteStreamBuffering" /> is <see langword="false" />, and either <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is -1, <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is <see langword="false" /> and <see cref="P:System.Net.HttpWebRequest.Method" /> is POST or PUT.  
		/// -or-  
		/// The <see cref="T:System.Net.HttpWebRequest" /> has an entity body but the <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> method is called without calling the <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" /> method.  
		/// -or-  
		/// The <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is greater than zero, but the application does not write all of the promised data.</exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.</exception>
		// Token: 0x060035F6 RID: 13814 RVA: 0x000BC004 File Offset: 0x000BA204
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			if (this.Aborted)
			{
				throw HttpWebRequest.CreateRequestAbortedException();
			}
			string transferEncoding = this.TransferEncoding;
			if (!this.sendChunked && transferEncoding != null && transferEncoding.Trim() != "")
			{
				throw new InvalidOperationException("TransferEncoding requires the SendChunked property to be set to true.");
			}
			return TaskToApm.Begin(this.RunWithTimeout<HttpWebResponse>(new Func<CancellationToken, Task<HttpWebResponse>>(this.MyGetResponseAsync)), callback, state);
		}

		/// <summary>Ends an asynchronous request to an Internet resource.</summary>
		/// <param name="asyncResult">The pending request for a response.</param>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains the response from the Internet resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This method was called previously using <paramref name="asyncResult." />  
		///  -or-  
		///  The <see cref="P:System.Net.HttpWebRequest.ContentLength" /> property is greater than 0 but the data has not been written to the request stream.</exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.  
		/// -or-  
		/// An error occurred while processing the request.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by the current instance from a call to <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />.</exception>
		// Token: 0x060035F7 RID: 13815 RVA: 0x000BC068 File Offset: 0x000BA268
		public override WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			WebResponse result;
			try
			{
				result = TaskToApm.End<HttpWebResponse>(asyncResult);
			}
			catch (Exception e)
			{
				throw this.GetWebException(e);
			}
			return result;
		}

		/// <summary>Ends an asynchronous request for a <see cref="T:System.IO.Stream" /> object to use to write data and outputs the <see cref="T:System.Net.TransportContext" /> associated with the stream.</summary>
		/// <param name="asyncResult">The pending request for a stream.</param>
		/// <param name="context">The <see cref="T:System.Net.TransportContext" /> for the <see cref="T:System.IO.Stream" />.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> to use to write request data.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by the current instance from a call to <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This method was called previously using <paramref name="asyncResult" />.</exception>
		/// <exception cref="T:System.IO.IOException">The request did not complete, and no stream is available.</exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.  
		/// -or-  
		/// An error occurred while processing the request.</exception>
		// Token: 0x060035F8 RID: 13816 RVA: 0x000BC0A8 File Offset: 0x000BA2A8
		public Stream EndGetRequestStream(IAsyncResult asyncResult, out TransportContext context)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			context = null;
			return this.EndGetRequestStream(asyncResult);
		}

		/// <summary>Returns a response from an Internet resource.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains the response from the Internet resource.</returns>
		/// <exception cref="T:System.InvalidOperationException">The stream is already in use by a previous call to <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />.  
		///  -or-  
		///  <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to a value and <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is <see langword="false" />.</exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">
		///   <see cref="P:System.Net.HttpWebRequest.Method" /> is GET or HEAD, and either <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is greater or equal to zero or <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is <see langword="true" />.  
		/// -or-  
		/// <see cref="P:System.Net.HttpWebRequest.KeepAlive" /> is <see langword="true" />, <see cref="P:System.Net.HttpWebRequest.AllowWriteStreamBuffering" /> is <see langword="false" />, <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is -1, <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is <see langword="false" />, and <see cref="P:System.Net.HttpWebRequest.Method" /> is POST or PUT.  
		/// -or-  
		/// The <see cref="T:System.Net.HttpWebRequest" /> has an entity body but the <see cref="M:System.Net.HttpWebRequest.GetResponse" /> method is called without calling the <see cref="M:System.Net.HttpWebRequest.GetRequestStream" /> method.  
		/// -or-  
		/// The <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is greater than zero, but the application does not write all of the promised data.</exception>
		/// <exception cref="T:System.NotSupportedException">The request cache validator indicated that the response for this request can be served from the cache; however, this request includes data to be sent to the server. Requests that send data must not use the cache. This exception can occur if you are using a custom cache validator that is incorrectly implemented.</exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.  
		/// -or-  
		/// The time-out period for the request expired.  
		/// -or-  
		/// An error occurred while processing the request.</exception>
		// Token: 0x060035F9 RID: 13817 RVA: 0x000BC0C4 File Offset: 0x000BA2C4
		public override WebResponse GetResponse()
		{
			WebResponse result;
			try
			{
				result = this.GetResponseAsync().Result;
			}
			catch (Exception e)
			{
				throw this.GetWebException(e);
			}
			return result;
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x060035FA RID: 13818 RVA: 0x000BC0FC File Offset: 0x000BA2FC
		// (set) Token: 0x060035FB RID: 13819 RVA: 0x000BC104 File Offset: 0x000BA304
		internal bool FinishedReading
		{
			get
			{
				return this.finished_reading;
			}
			set
			{
				this.finished_reading = value;
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x060035FC RID: 13820 RVA: 0x000BC10D File Offset: 0x000BA30D
		internal bool Aborted
		{
			get
			{
				return Interlocked.CompareExchange(ref this.aborted, 0, 0) == 1;
			}
		}

		/// <summary>Cancels a request to an Internet resource.</summary>
		// Token: 0x060035FD RID: 13821 RVA: 0x000BC120 File Offset: 0x000BA320
		public override void Abort()
		{
			if (Interlocked.CompareExchange(ref this.aborted, 1, 0) == 1)
			{
				return;
			}
			this.haveResponse = true;
			WebOperation webOperation = this.currentOperation;
			if (webOperation != null)
			{
				webOperation.Abort();
			}
			WebCompletionSource webCompletionSource = this.responseTask;
			if (webCompletionSource != null)
			{
				webCompletionSource.TrySetCanceled();
			}
			if (this.webResponse != null)
			{
				try
				{
					this.webResponse.Close();
					this.webResponse = null;
				}
				catch
				{
				}
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x060035FE RID: 13822 RVA: 0x000BC198 File Offset: 0x000BA398
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new SerializationException();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data required to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x060035FF RID: 13823 RVA: 0x000BC198 File Offset: 0x000BA398
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new SerializationException();
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x000BC19F File Offset: 0x000BA39F
		private void CheckRequestStarted()
		{
			if (this.requestSent)
			{
				throw new InvalidOperationException("request started");
			}
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x000BC1B4 File Offset: 0x000BA3B4
		internal void DoContinueDelegate(int statusCode, WebHeaderCollection headers)
		{
			if (this.continueDelegate != null)
			{
				this.continueDelegate(statusCode, headers);
			}
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x000BC1CB File Offset: 0x000BA3CB
		private void RewriteRedirectToGet()
		{
			this.method = "GET";
			this.webHeaders.RemoveInternal("Transfer-Encoding");
			this.sendChunked = false;
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x000BC1F0 File Offset: 0x000BA3F0
		private bool Redirect(HttpStatusCode code, WebResponse response)
		{
			this.redirects++;
			Exception ex = null;
			string text = null;
			switch (code)
			{
			case HttpStatusCode.MultipleChoices:
				ex = new WebException("Ambiguous redirect.");
				goto IL_97;
			case HttpStatusCode.MovedPermanently:
			case HttpStatusCode.Found:
				if (this.method == "POST")
				{
					this.RewriteRedirectToGet();
					goto IL_97;
				}
				goto IL_97;
			case HttpStatusCode.SeeOther:
				this.RewriteRedirectToGet();
				goto IL_97;
			case HttpStatusCode.NotModified:
				return false;
			case HttpStatusCode.UseProxy:
				ex = new NotImplementedException("Proxy support not available.");
				goto IL_97;
			case HttpStatusCode.TemporaryRedirect:
				goto IL_97;
			}
			string str = "Invalid status code: ";
			int num = (int)code;
			ex = new ProtocolViolationException(str + num.ToString());
			IL_97:
			if (this.method != "GET" && !this.InternalAllowBuffering && this.ResendContentFactory == null && (this.writeStream.WriteBufferLength > 0 || this.contentLength > 0L))
			{
				ex = new WebException("The request requires buffering data to succeed.", null, WebExceptionStatus.ProtocolError, response);
			}
			if (ex != null)
			{
				throw ex;
			}
			if (this.AllowWriteStreamBuffering || this.method == "GET")
			{
				this.contentLength = -1L;
			}
			text = response.Headers["Location"];
			if (text == null)
			{
				throw new WebException(string.Format("No Location header found for {0}", (int)code), null, WebExceptionStatus.ProtocolError, response);
			}
			Uri uri = this.actualUri;
			try
			{
				this.actualUri = new Uri(this.actualUri, text);
			}
			catch (Exception)
			{
				throw new WebException(string.Format("Invalid URL ({0}) for {1}", text, (int)code), null, WebExceptionStatus.ProtocolError, response);
			}
			this.hostChanged = (this.actualUri.Scheme != uri.Scheme || this.Host != uri.Authority);
			return true;
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x000BC3AC File Offset: 0x000BA5AC
		private string GetHeaders()
		{
			bool flag = false;
			if (this.sendChunked)
			{
				flag = true;
				this.webHeaders.ChangeInternal("Transfer-Encoding", "chunked");
				this.webHeaders.RemoveInternal("Content-Length");
			}
			else if (this.contentLength != -1L)
			{
				if (this.auth_state.NtlmAuthState == HttpWebRequest.NtlmAuthState.Challenge || this.proxy_auth_state.NtlmAuthState == HttpWebRequest.NtlmAuthState.Challenge)
				{
					if (this.haveContentLength || this.gotRequestStream || this.contentLength > 0L)
					{
						this.webHeaders.SetInternal("Content-Length", "0");
					}
					else
					{
						this.webHeaders.RemoveInternal("Content-Length");
					}
				}
				else
				{
					if (this.contentLength > 0L)
					{
						flag = true;
					}
					if (this.haveContentLength || this.gotRequestStream || this.contentLength > 0L)
					{
						this.webHeaders.SetInternal("Content-Length", this.contentLength.ToString());
					}
				}
				this.webHeaders.RemoveInternal("Transfer-Encoding");
			}
			else
			{
				this.webHeaders.RemoveInternal("Content-Length");
			}
			if (this.actualVersion == HttpVersion.Version11 && flag && this.servicePoint.SendContinue)
			{
				this.webHeaders.ChangeInternal("Expect", "100-continue");
				this.expectContinue = true;
			}
			else
			{
				this.webHeaders.RemoveInternal("Expect");
				this.expectContinue = false;
			}
			bool proxyQuery = this.ProxyQuery;
			string name = proxyQuery ? "Proxy-Connection" : "Connection";
			this.webHeaders.RemoveInternal((!proxyQuery) ? "Proxy-Connection" : "Connection");
			Version protocolVersion = this.servicePoint.ProtocolVersion;
			bool flag2 = protocolVersion == null || protocolVersion == HttpVersion.Version10;
			if (this.keepAlive && (this.version == HttpVersion.Version10 || flag2))
			{
				if (this.webHeaders[name] == null || this.webHeaders[name].IndexOf("keep-alive", StringComparison.OrdinalIgnoreCase) == -1)
				{
					this.webHeaders.ChangeInternal(name, "keep-alive");
				}
			}
			else if (!this.keepAlive && this.version == HttpVersion.Version11)
			{
				this.webHeaders.ChangeInternal(name, "close");
			}
			string components;
			if (this.hostUri != null)
			{
				if (this.hostHasPort)
				{
					components = this.hostUri.GetComponents(UriComponents.HostAndPort, UriFormat.Unescaped);
				}
				else
				{
					components = this.hostUri.GetComponents(UriComponents.Host, UriFormat.Unescaped);
				}
			}
			else if (this.Address.IsDefaultPort)
			{
				components = this.Address.GetComponents(UriComponents.Host, UriFormat.Unescaped);
			}
			else
			{
				components = this.Address.GetComponents(UriComponents.HostAndPort, UriFormat.Unescaped);
			}
			this.webHeaders.SetInternal("Host", components);
			if (this.cookieContainer != null)
			{
				string cookieHeader = this.cookieContainer.GetCookieHeader(this.actualUri);
				if (cookieHeader != "")
				{
					this.webHeaders.ChangeInternal("Cookie", cookieHeader);
				}
				else
				{
					this.webHeaders.RemoveInternal("Cookie");
				}
			}
			string text = null;
			if ((this.auto_decomp & DecompressionMethods.GZip) != DecompressionMethods.None)
			{
				text = "gzip";
			}
			if ((this.auto_decomp & DecompressionMethods.Deflate) != DecompressionMethods.None)
			{
				text = ((text != null) ? "gzip, deflate" : "deflate");
			}
			if (text != null)
			{
				this.webHeaders.ChangeInternal("Accept-Encoding", text);
			}
			if (!this.usedPreAuth && this.preAuthenticate)
			{
				this.DoPreAuthenticate();
			}
			return this.webHeaders.ToString();
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x000BC720 File Offset: 0x000BA920
		private void DoPreAuthenticate()
		{
			bool flag = this.proxy != null && !this.proxy.IsBypassed(this.actualUri);
			ICredentials credentials = (!flag || this.credentials != null) ? this.credentials : this.proxy.Credentials;
			Authorization authorization = AuthenticationManager.PreAuthenticate(this, credentials);
			if (authorization == null)
			{
				return;
			}
			this.webHeaders.RemoveInternal("Proxy-Authorization");
			this.webHeaders.RemoveInternal("Authorization");
			string name = (flag && this.credentials == null) ? "Proxy-Authorization" : "Authorization";
			this.webHeaders[name] = authorization.Message;
			this.usedPreAuth = true;
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x000BC7CC File Offset: 0x000BA9CC
		internal byte[] GetRequestHeaders()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text;
			if (!this.ProxyQuery)
			{
				text = this.actualUri.PathAndQuery;
			}
			else
			{
				text = string.Format("{0}://{1}{2}", this.actualUri.Scheme, this.Host, this.actualUri.PathAndQuery);
			}
			if (!this.force_version && this.servicePoint.ProtocolVersion != null && this.servicePoint.ProtocolVersion < this.version)
			{
				this.actualVersion = this.servicePoint.ProtocolVersion;
			}
			else
			{
				this.actualVersion = this.version;
			}
			stringBuilder.AppendFormat("{0} {1} HTTP/{2}.{3}\r\n", new object[]
			{
				this.method,
				text,
				this.actualVersion.Major,
				this.actualVersion.Minor
			});
			stringBuilder.Append(this.GetHeaders());
			string s = stringBuilder.ToString();
			return Encoding.UTF8.GetBytes(s);
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x000BC8D4 File Offset: 0x000BAAD4
		private ValueTuple<WebOperation, bool> HandleNtlmAuth(WebResponseStream stream, HttpWebResponse response, BufferOffsetSize writeBuffer, CancellationToken cancellationToken)
		{
			bool flag = response.StatusCode == HttpStatusCode.ProxyAuthenticationRequired;
			if ((flag ? this.proxy_auth_state : this.auth_state).NtlmAuthState == HttpWebRequest.NtlmAuthState.None)
			{
				return new ValueTuple<WebOperation, bool>(null, false);
			}
			bool flag2 = this.auth_state.NtlmAuthState == HttpWebRequest.NtlmAuthState.Challenge || this.proxy_auth_state.NtlmAuthState == HttpWebRequest.NtlmAuthState.Challenge;
			WebOperation webOperation = new WebOperation(this, writeBuffer, flag2, cancellationToken);
			stream.Operation.SetPriorityRequest(webOperation);
			ICredentials credentials = (!flag || this.proxy == null) ? this.credentials : this.proxy.Credentials;
			if (credentials != null)
			{
				stream.Connection.NtlmCredential = credentials.GetCredential(this.requestUri, "NTLM");
				stream.Connection.UnsafeAuthenticatedConnectionSharing = this.unsafe_auth_blah;
			}
			return new ValueTuple<WebOperation, bool>(webOperation, flag2);
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000BC9A0 File Offset: 0x000BABA0
		private bool CheckAuthorization(WebResponse response, HttpStatusCode code)
		{
			if (code != HttpStatusCode.ProxyAuthenticationRequired)
			{
				return this.auth_state.CheckAuthorization(response, code);
			}
			return this.proxy_auth_state.CheckAuthorization(response, code);
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x000BC9C8 File Offset: 0x000BABC8
		[return: TupleElementNames(new string[]
		{
			"task",
			"throwMe"
		})]
		private ValueTuple<Task<BufferOffsetSize>, WebException> GetRewriteHandler(HttpWebResponse response, bool redirect)
		{
			if (redirect)
			{
				if (!this.MethodWithBuffer)
				{
					return new ValueTuple<Task<BufferOffsetSize>, WebException>(null, null);
				}
				if (this.writeStream.WriteBufferLength == 0 || this.contentLength == 0L)
				{
					return new ValueTuple<Task<BufferOffsetSize>, WebException>(null, null);
				}
			}
			if (this.AllowWriteStreamBuffering)
			{
				return new ValueTuple<Task<BufferOffsetSize>, WebException>(Task.FromResult<BufferOffsetSize>(this.writeStream.GetWriteBuffer()), null);
			}
			if (this.ResendContentFactory == null)
			{
				return new ValueTuple<Task<BufferOffsetSize>, WebException>(null, new WebException("The request requires buffering data to succeed.", null, WebExceptionStatus.ProtocolError, response));
			}
			return new ValueTuple<Task<BufferOffsetSize>, WebException>(delegate
			{
				HttpWebRequest.<<GetRewriteHandler>b__274_0>d <<GetRewriteHandler>b__274_0>d;
				<<GetRewriteHandler>b__274_0>d.<>4__this = this;
				<<GetRewriteHandler>b__274_0>d.<>t__builder = AsyncTaskMethodBuilder<BufferOffsetSize>.Create();
				<<GetRewriteHandler>b__274_0>d.<>1__state = -1;
				<<GetRewriteHandler>b__274_0>d.<>t__builder.Start<HttpWebRequest.<<GetRewriteHandler>b__274_0>d>(ref <<GetRewriteHandler>b__274_0>d);
				return <<GetRewriteHandler>b__274_0>d.<>t__builder.Task;
			}(), null);
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x000BCA58 File Offset: 0x000BAC58
		[return: TupleElementNames(new string[]
		{
			"redirect",
			"mustReadAll",
			"writeBuffer",
			"throwMe"
		})]
		private ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException> CheckFinalStatus(HttpWebResponse response)
		{
			WebException ex = null;
			bool item = false;
			Task<BufferOffsetSize> item2 = null;
			HttpStatusCode statusCode = response.StatusCode;
			if (((!this.auth_state.IsCompleted && statusCode == HttpStatusCode.Unauthorized && this.credentials != null) || (this.ProxyQuery && !this.proxy_auth_state.IsCompleted && statusCode == HttpStatusCode.ProxyAuthenticationRequired)) && !this.usedPreAuth && this.CheckAuthorization(response, statusCode))
			{
				item = true;
				if (!this.MethodWithBuffer)
				{
					return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(true, item, null, null);
				}
				ValueTuple<Task<BufferOffsetSize>, WebException> rewriteHandler = this.GetRewriteHandler(response, false);
				item2 = rewriteHandler.Item1;
				ex = rewriteHandler.Item2;
				if (ex == null)
				{
					return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(true, item, item2, null);
				}
				if (!this.ThrowOnError)
				{
					return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(false, item, null, null);
				}
				this.writeStream.InternalClose();
				this.writeStream = null;
				response.Close();
				return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(false, item, null, ex);
			}
			else
			{
				if (statusCode >= HttpStatusCode.BadRequest)
				{
					ex = new WebException(string.Format("The remote server returned an error: ({0}) {1}.", (int)statusCode, response.StatusDescription), null, WebExceptionStatus.ProtocolError, response);
					item = true;
				}
				else if (statusCode == HttpStatusCode.NotModified && this.allowAutoRedirect)
				{
					ex = new WebException(string.Format("The remote server returned an error: ({0}) {1}.", (int)statusCode, response.StatusDescription), null, WebExceptionStatus.ProtocolError, response);
				}
				else if (statusCode >= HttpStatusCode.MultipleChoices && this.allowAutoRedirect && this.redirects >= this.maxAutoRedirect)
				{
					ex = new WebException("Max. redirections exceeded.", null, WebExceptionStatus.ProtocolError, response);
					item = true;
				}
				if (ex == null)
				{
					int num = (int)statusCode;
					bool flag = false;
					if (this.allowAutoRedirect && num >= 300)
					{
						flag = this.Redirect(statusCode, response);
						ValueTuple<Task<BufferOffsetSize>, WebException> rewriteHandler2 = this.GetRewriteHandler(response, true);
						item2 = rewriteHandler2.Item1;
						ex = rewriteHandler2.Item2;
						if (flag && !this.unsafe_auth_blah)
						{
							this.auth_state.Reset();
							this.proxy_auth_state.Reset();
						}
					}
					if (num >= 300 && num != 304)
					{
						item = true;
					}
					if (ex == null)
					{
						return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(flag, item, item2, null);
					}
				}
				if (!this.ThrowOnError)
				{
					return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(false, item, null, null);
				}
				if (this.writeStream != null)
				{
					this.writeStream.InternalClose();
					this.writeStream = null;
				}
				return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(false, item, null, ex);
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x0600360B RID: 13835 RVA: 0x000BCC6C File Offset: 0x000BAE6C
		// (set) Token: 0x0600360C RID: 13836 RVA: 0x000BCC74 File Offset: 0x000BAE74
		internal bool ReuseConnection
		{
			[CompilerGenerated]
			get
			{
				return this.<ReuseConnection>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ReuseConnection>k__BackingField = value;
			}
		}

		// Token: 0x0600360D RID: 13837 RVA: 0x000BCC80 File Offset: 0x000BAE80
		internal static StringBuilder GenerateConnectionGroup(string connectionGroupName, bool unsafeConnectionGroup, bool isInternalGroup)
		{
			StringBuilder stringBuilder = new StringBuilder(connectionGroupName);
			stringBuilder.Append(unsafeConnectionGroup ? "U>" : "S>");
			if (isInternalGroup)
			{
				stringBuilder.Append("I>");
			}
			return stringBuilder;
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x000BCCBA File Offset: 0x000BAEBA
		[CompilerGenerated]
		private bool <RunWithTimeout>b__245_0<T>()
		{
			return this.Aborted;
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x000BCCC4 File Offset: 0x000BAEC4
		[CompilerGenerated]
		private Task<BufferOffsetSize> <GetRewriteHandler>b__274_0()
		{
			HttpWebRequest.<<GetRewriteHandler>b__274_0>d <<GetRewriteHandler>b__274_0>d;
			<<GetRewriteHandler>b__274_0>d.<>4__this = this;
			<<GetRewriteHandler>b__274_0>d.<>t__builder = AsyncTaskMethodBuilder<BufferOffsetSize>.Create();
			<<GetRewriteHandler>b__274_0>d.<>1__state = -1;
			<<GetRewriteHandler>b__274_0>d.<>t__builder.Start<HttpWebRequest.<<GetRewriteHandler>b__274_0>d>(ref <<GetRewriteHandler>b__274_0>d);
			return <<GetRewriteHandler>b__274_0>d.<>t__builder.Task;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebRequest" /> class. This constructor is obsolete.</summary>
		// Token: 0x06003610 RID: 13840 RVA: 0x00013BCA File Offset: 0x00011DCA
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public HttpWebRequest()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001F1B RID: 7963
		private Uri requestUri;

		// Token: 0x04001F1C RID: 7964
		private Uri actualUri;

		// Token: 0x04001F1D RID: 7965
		private bool hostChanged;

		// Token: 0x04001F1E RID: 7966
		private bool allowAutoRedirect;

		// Token: 0x04001F1F RID: 7967
		private bool allowBuffering;

		// Token: 0x04001F20 RID: 7968
		private bool allowReadStreamBuffering;

		// Token: 0x04001F21 RID: 7969
		private X509CertificateCollection certificates;

		// Token: 0x04001F22 RID: 7970
		private string connectionGroup;

		// Token: 0x04001F23 RID: 7971
		private bool haveContentLength;

		// Token: 0x04001F24 RID: 7972
		private long contentLength;

		// Token: 0x04001F25 RID: 7973
		private HttpContinueDelegate continueDelegate;

		// Token: 0x04001F26 RID: 7974
		private CookieContainer cookieContainer;

		// Token: 0x04001F27 RID: 7975
		private ICredentials credentials;

		// Token: 0x04001F28 RID: 7976
		private bool haveResponse;

		// Token: 0x04001F29 RID: 7977
		private bool requestSent;

		// Token: 0x04001F2A RID: 7978
		private WebHeaderCollection webHeaders;

		// Token: 0x04001F2B RID: 7979
		private bool keepAlive;

		// Token: 0x04001F2C RID: 7980
		private int maxAutoRedirect;

		// Token: 0x04001F2D RID: 7981
		private string mediaType;

		// Token: 0x04001F2E RID: 7982
		private string method;

		// Token: 0x04001F2F RID: 7983
		private string initialMethod;

		// Token: 0x04001F30 RID: 7984
		private bool pipelined;

		// Token: 0x04001F31 RID: 7985
		private bool preAuthenticate;

		// Token: 0x04001F32 RID: 7986
		private bool usedPreAuth;

		// Token: 0x04001F33 RID: 7987
		private Version version;

		// Token: 0x04001F34 RID: 7988
		private bool force_version;

		// Token: 0x04001F35 RID: 7989
		private Version actualVersion;

		// Token: 0x04001F36 RID: 7990
		private IWebProxy proxy;

		// Token: 0x04001F37 RID: 7991
		private bool sendChunked;

		// Token: 0x04001F38 RID: 7992
		private ServicePoint servicePoint;

		// Token: 0x04001F39 RID: 7993
		private int timeout;

		// Token: 0x04001F3A RID: 7994
		private int continueTimeout;

		// Token: 0x04001F3B RID: 7995
		private WebRequestStream writeStream;

		// Token: 0x04001F3C RID: 7996
		private HttpWebResponse webResponse;

		// Token: 0x04001F3D RID: 7997
		private WebCompletionSource responseTask;

		// Token: 0x04001F3E RID: 7998
		private WebOperation currentOperation;

		// Token: 0x04001F3F RID: 7999
		private int aborted;

		// Token: 0x04001F40 RID: 8000
		private bool gotRequestStream;

		// Token: 0x04001F41 RID: 8001
		private int redirects;

		// Token: 0x04001F42 RID: 8002
		private bool expectContinue;

		// Token: 0x04001F43 RID: 8003
		private bool getResponseCalled;

		// Token: 0x04001F44 RID: 8004
		private object locker;

		// Token: 0x04001F45 RID: 8005
		private bool finished_reading;

		// Token: 0x04001F46 RID: 8006
		private DecompressionMethods auto_decomp;

		// Token: 0x04001F47 RID: 8007
		private int maxResponseHeadersLength;

		// Token: 0x04001F48 RID: 8008
		private static int defaultMaxResponseHeadersLength = 64;

		// Token: 0x04001F49 RID: 8009
		private static int defaultMaximumErrorResponseLength = 64;

		// Token: 0x04001F4A RID: 8010
		private static RequestCachePolicy defaultCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);

		// Token: 0x04001F4B RID: 8011
		private int readWriteTimeout;

		// Token: 0x04001F4C RID: 8012
		private MobileTlsProvider tlsProvider;

		// Token: 0x04001F4D RID: 8013
		private MonoTlsSettings tlsSettings;

		// Token: 0x04001F4E RID: 8014
		private ServerCertValidationCallback certValidationCallback;

		// Token: 0x04001F4F RID: 8015
		private bool hostHasPort;

		// Token: 0x04001F50 RID: 8016
		private Uri hostUri;

		// Token: 0x04001F51 RID: 8017
		private HttpWebRequest.AuthorizationState auth_state;

		// Token: 0x04001F52 RID: 8018
		private HttpWebRequest.AuthorizationState proxy_auth_state;

		// Token: 0x04001F53 RID: 8019
		[NonSerialized]
		internal Func<Stream, Task> ResendContentFactory;

		// Token: 0x04001F54 RID: 8020
		internal readonly int ID;

		// Token: 0x04001F55 RID: 8021
		[CompilerGenerated]
		private bool <ThrowOnError>k__BackingField;

		// Token: 0x04001F56 RID: 8022
		private bool unsafe_auth_blah;

		// Token: 0x04001F57 RID: 8023
		[CompilerGenerated]
		private bool <ReuseConnection>k__BackingField;

		// Token: 0x02000695 RID: 1685
		private enum NtlmAuthState
		{
			// Token: 0x04001F59 RID: 8025
			None,
			// Token: 0x04001F5A RID: 8026
			Challenge,
			// Token: 0x04001F5B RID: 8027
			Response
		}

		// Token: 0x02000696 RID: 1686
		private struct AuthorizationState
		{
			// Token: 0x17000B26 RID: 2854
			// (get) Token: 0x06003611 RID: 13841 RVA: 0x000BCD07 File Offset: 0x000BAF07
			public bool IsCompleted
			{
				get
				{
					return this.isCompleted;
				}
			}

			// Token: 0x17000B27 RID: 2855
			// (get) Token: 0x06003612 RID: 13842 RVA: 0x000BCD0F File Offset: 0x000BAF0F
			public HttpWebRequest.NtlmAuthState NtlmAuthState
			{
				get
				{
					return this.ntlm_auth_state;
				}
			}

			// Token: 0x17000B28 RID: 2856
			// (get) Token: 0x06003613 RID: 13843 RVA: 0x000BCD17 File Offset: 0x000BAF17
			public bool IsNtlmAuthenticated
			{
				get
				{
					return this.isCompleted && this.ntlm_auth_state > HttpWebRequest.NtlmAuthState.None;
				}
			}

			// Token: 0x06003614 RID: 13844 RVA: 0x000BCD2C File Offset: 0x000BAF2C
			public AuthorizationState(HttpWebRequest request, bool isProxy)
			{
				this.request = request;
				this.isProxy = isProxy;
				this.isCompleted = false;
				this.ntlm_auth_state = HttpWebRequest.NtlmAuthState.None;
			}

			// Token: 0x06003615 RID: 13845 RVA: 0x000BCD4C File Offset: 0x000BAF4C
			public bool CheckAuthorization(WebResponse response, HttpStatusCode code)
			{
				this.isCompleted = false;
				if (code == HttpStatusCode.Unauthorized && this.request.credentials == null)
				{
					return false;
				}
				if (this.isProxy != (code == HttpStatusCode.ProxyAuthenticationRequired))
				{
					return false;
				}
				if (this.isProxy && (this.request.proxy == null || this.request.proxy.Credentials == null))
				{
					return false;
				}
				string[] values = response.Headers.GetValues(this.isProxy ? "Proxy-Authenticate" : "WWW-Authenticate");
				if (values == null || values.Length == 0)
				{
					return false;
				}
				ICredentials credentials = (!this.isProxy) ? this.request.credentials : this.request.proxy.Credentials;
				Authorization authorization = null;
				string[] array = values;
				for (int i = 0; i < array.Length; i++)
				{
					authorization = AuthenticationManager.Authenticate(array[i], this.request, credentials);
					if (authorization != null)
					{
						break;
					}
				}
				if (authorization == null)
				{
					return false;
				}
				this.request.webHeaders[this.isProxy ? "Proxy-Authorization" : "Authorization"] = authorization.Message;
				this.isCompleted = authorization.Complete;
				if (authorization.ModuleAuthenticationType == "NTLM")
				{
					this.ntlm_auth_state++;
				}
				return true;
			}

			// Token: 0x06003616 RID: 13846 RVA: 0x000BCE87 File Offset: 0x000BB087
			public void Reset()
			{
				this.isCompleted = false;
				this.ntlm_auth_state = HttpWebRequest.NtlmAuthState.None;
				this.request.webHeaders.RemoveInternal(this.isProxy ? "Proxy-Authorization" : "Authorization");
			}

			// Token: 0x06003617 RID: 13847 RVA: 0x000BCEBB File Offset: 0x000BB0BB
			public override string ToString()
			{
				return string.Format("{0}AuthState [{1}:{2}]", this.isProxy ? "Proxy" : "", this.isCompleted, this.ntlm_auth_state);
			}

			// Token: 0x04001F5C RID: 8028
			private readonly HttpWebRequest request;

			// Token: 0x04001F5D RID: 8029
			private readonly bool isProxy;

			// Token: 0x04001F5E RID: 8030
			private bool isCompleted;

			// Token: 0x04001F5F RID: 8031
			private HttpWebRequest.NtlmAuthState ntlm_auth_state;
		}

		// Token: 0x02000697 RID: 1687
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__244<T>
		{
			// Token: 0x06003618 RID: 13848 RVA: 0x000BCEF1 File Offset: 0x000BB0F1
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__244()
			{
			}

			// Token: 0x06003619 RID: 13849 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__244()
			{
			}

			// Token: 0x0600361A RID: 13850 RVA: 0x000BCF00 File Offset: 0x000BB100
			internal int? <RunWithTimeoutWorker>b__244_0(Task<T> t)
			{
				AggregateException exception = t.Exception;
				if (exception == null)
				{
					return null;
				}
				return new int?(exception.GetHashCode());
			}

			// Token: 0x04001F60 RID: 8032
			public static readonly HttpWebRequest.<>c__244<T> <>9 = new HttpWebRequest.<>c__244<T>();

			// Token: 0x04001F61 RID: 8033
			public static Func<Task<T>, int?> <>9__244_0;
		}

		// Token: 0x02000698 RID: 1688
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <RunWithTimeoutWorker>d__244<T> : IAsyncStateMachine
		{
			// Token: 0x0600361B RID: 13851 RVA: 0x000BCF2C File Offset: 0x000BB12C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				T result;
				try
				{
					try
					{
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = ServicePointScheduler.WaitAsync(this.workerTask, this.timeout).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, HttpWebRequest.<RunWithTimeoutWorker>d__244<T>>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						if (!awaiter.GetResult())
						{
							try
							{
								this.cts.Cancel();
								this.abort();
							}
							catch
							{
							}
							this.workerTask.ContinueWith<int?>(new Func<Task<T>, int?>(HttpWebRequest.<>c__244<T>.<>9.<RunWithTimeoutWorker>b__244_0), TaskContinuationOptions.OnlyOnFaulted);
							throw new WebException("The operation has timed out.", WebExceptionStatus.Timeout);
						}
						result = this.workerTask.Result;
					}
					catch (Exception e)
					{
						throw HttpWebRequest.GetWebException(e, this.aborted());
					}
					finally
					{
						if (num < 0)
						{
							this.cts.Dispose();
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600361C RID: 13852 RVA: 0x000BD0D4 File Offset: 0x000BB2D4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001F62 RID: 8034
			public int <>1__state;

			// Token: 0x04001F63 RID: 8035
			public AsyncTaskMethodBuilder<T> <>t__builder;

			// Token: 0x04001F64 RID: 8036
			public Task<T> workerTask;

			// Token: 0x04001F65 RID: 8037
			public int timeout;

			// Token: 0x04001F66 RID: 8038
			public CancellationTokenSource cts;

			// Token: 0x04001F67 RID: 8039
			public Action abort;

			// Token: 0x04001F68 RID: 8040
			public Func<bool> aborted;

			// Token: 0x04001F69 RID: 8041
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000699 RID: 1689
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <MyGetResponseAsync>d__246 : IAsyncStateMachine
		{
			// Token: 0x0600361D RID: 13853 RVA: 0x000BD0E4 File Offset: 0x000BB2E4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				HttpWebRequest httpWebRequest = this.<>4__this;
				HttpWebResponse webResponse;
				try
				{
					if (num <= 3)
					{
						goto IL_121;
					}
					if (num == 4)
					{
						goto IL_404;
					}
					if (httpWebRequest.Aborted)
					{
						throw HttpWebRequest.CreateRequestAbortedException();
					}
					this.<completion>5__2 = new WebCompletionSource();
					object locker = httpWebRequest.locker;
					bool flag = false;
					try
					{
						Monitor.Enter(locker, ref flag);
						httpWebRequest.getResponseCalled = true;
						WebCompletionSource webCompletionSource = Interlocked.CompareExchange<WebCompletionSource>(ref httpWebRequest.responseTask, this.<completion>5__2, null);
						if (webCompletionSource != null)
						{
							webCompletionSource.ThrowOnError();
							if (httpWebRequest.haveResponse && webCompletionSource.Task.IsCompleted)
							{
								webResponse = httpWebRequest.webResponse;
								goto IL_5A1;
							}
							throw new InvalidOperationException("Cannot re-call start of asynchronous method while a previous call is still in progress.");
						}
						else
						{
							this.<operation>5__3 = httpWebRequest.currentOperation;
							if (httpWebRequest.currentOperation != null)
							{
								httpWebRequest.writeStream = httpWebRequest.currentOperation.WriteStream;
							}
							httpWebRequest.initialMethod = httpWebRequest.method;
							this.<operation>5__3 = httpWebRequest.SendRequest(false, null, this.cancellationToken);
						}
					}
					finally
					{
						if (num < 0 && flag)
						{
							Monitor.Exit(locker);
						}
					}
					IL_F0:
					this.<throwMe>5__4 = null;
					this.<response>5__5 = null;
					this.<stream>5__6 = null;
					this.<redirect>5__7 = false;
					this.<mustReadAll>5__8 = false;
					this.<ntlm>5__9 = null;
					this.<writeBuffer>5__10 = null;
					IL_121:
					try
					{
						ConfiguredTaskAwaitable<WebRequestStream>.ConfiguredTaskAwaiter awaiter;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
						TaskAwaiter<WebResponseStream> awaiter3;
						ConfiguredTaskAwaitable<ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation>>.ConfiguredTaskAwaiter awaiter4;
						switch (num)
						{
						case 0:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<WebRequestStream>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							break;
						case 1:
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_226;
						case 2:
							awaiter3 = this.<>u__3;
							this.<>u__3 = default(TaskAwaiter<WebResponseStream>);
							num = (this.<>1__state = -1);
							goto IL_289;
						case 3:
							awaiter4 = this.<>u__4;
							this.<>u__4 = default(ConfiguredTaskAwaitable<ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation>>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_307;
						default:
							this.cancellationToken.ThrowIfCancellationRequested();
							awaiter = this.<operation>5__3.GetRequestStreamInternal().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<WebRequestStream>.ConfiguredTaskAwaiter, HttpWebRequest.<MyGetResponseAsync>d__246>(ref awaiter, ref this);
								return;
							}
							break;
						}
						WebRequestStream result = awaiter.GetResult();
						httpWebRequest.writeStream = result;
						awaiter2 = httpWebRequest.writeStream.WriteRequestAsync(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (this.<>1__state = 1);
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, HttpWebRequest.<MyGetResponseAsync>d__246>(ref awaiter2, ref this);
							return;
						}
						IL_226:
						awaiter2.GetResult();
						awaiter3 = this.<operation>5__3.GetResponseStream().GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (this.<>1__state = 2);
							this.<>u__3 = awaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<WebResponseStream>, HttpWebRequest.<MyGetResponseAsync>d__246>(ref awaiter3, ref this);
							return;
						}
						IL_289:
						WebResponseStream result2 = awaiter3.GetResult();
						this.<stream>5__6 = result2;
						awaiter4 = httpWebRequest.GetResponseFromData(this.<stream>5__6, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter4.IsCompleted)
						{
							num = (this.<>1__state = 3);
							this.<>u__4 = awaiter4;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation>>.ConfiguredTaskAwaiter, HttpWebRequest.<MyGetResponseAsync>d__246>(ref awaiter4, ref this);
							return;
						}
						IL_307:
						ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation> result3 = awaiter4.GetResult();
						this.<response>5__5 = result3.Item1;
						this.<redirect>5__7 = result3.Item2;
						this.<mustReadAll>5__8 = result3.Item3;
						this.<writeBuffer>5__10 = result3.Item4;
						this.<ntlm>5__9 = result3.Item5;
					}
					catch (Exception e)
					{
						this.<throwMe>5__4 = httpWebRequest.GetWebException(e);
					}
					locker = httpWebRequest.locker;
					flag = false;
					try
					{
						Monitor.Enter(locker, ref flag);
						if (this.<throwMe>5__4 != null)
						{
							httpWebRequest.haveResponse = true;
							this.<completion>5__2.TrySetException(this.<throwMe>5__4);
							throw this.<throwMe>5__4;
						}
						if (!this.<redirect>5__7)
						{
							httpWebRequest.haveResponse = true;
							httpWebRequest.webResponse = this.<response>5__5;
							this.<completion>5__2.TrySetCompleted();
							webResponse = this.<response>5__5;
							goto IL_5A1;
						}
						httpWebRequest.finished_reading = false;
						httpWebRequest.haveResponse = false;
						httpWebRequest.webResponse = null;
						httpWebRequest.currentOperation = this.<ntlm>5__9;
					}
					finally
					{
						if (num < 0 && flag)
						{
							Monitor.Exit(locker);
						}
					}
					IL_404:
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
						if (num != 4)
						{
							if (!this.<mustReadAll>5__8)
							{
								goto IL_49B;
							}
							awaiter2 = this.<stream>5__6.ReadAllAsync(this.<redirect>5__7 || this.<ntlm>5__9 != null, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 4);
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, HttpWebRequest.<MyGetResponseAsync>d__246>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						awaiter2.GetResult();
						IL_49B:
						this.<operation>5__3.Finish(true, null);
						this.<response>5__5.Close();
					}
					catch (Exception e2)
					{
						this.<throwMe>5__4 = httpWebRequest.GetWebException(e2);
					}
					locker = httpWebRequest.locker;
					flag = false;
					try
					{
						Monitor.Enter(locker, ref flag);
						if (this.<throwMe>5__4 != null)
						{
							httpWebRequest.haveResponse = true;
							WebResponseStream webResponseStream = this.<stream>5__6;
							if (webResponseStream != null)
							{
								webResponseStream.Close();
							}
							this.<completion>5__2.TrySetException(this.<throwMe>5__4);
							throw this.<throwMe>5__4;
						}
						if (this.<ntlm>5__9 == null)
						{
							this.<operation>5__3 = httpWebRequest.SendRequest(true, this.<writeBuffer>5__10, this.cancellationToken);
						}
						else
						{
							this.<operation>5__3 = this.<ntlm>5__9;
						}
					}
					finally
					{
						if (num < 0 && flag)
						{
							Monitor.Exit(locker);
						}
					}
					this.<throwMe>5__4 = null;
					this.<response>5__5 = null;
					this.<stream>5__6 = null;
					this.<ntlm>5__9 = null;
					this.<writeBuffer>5__10 = null;
					goto IL_F0;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<completion>5__2 = null;
					this.<operation>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_5A1:
				this.<>1__state = -2;
				this.<completion>5__2 = null;
				this.<operation>5__3 = null;
				this.<>t__builder.SetResult(webResponse);
			}

			// Token: 0x0600361E RID: 13854 RVA: 0x000BD748 File Offset: 0x000BB948
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001F6A RID: 8042
			public int <>1__state;

			// Token: 0x04001F6B RID: 8043
			public AsyncTaskMethodBuilder<HttpWebResponse> <>t__builder;

			// Token: 0x04001F6C RID: 8044
			public HttpWebRequest <>4__this;

			// Token: 0x04001F6D RID: 8045
			public CancellationToken cancellationToken;

			// Token: 0x04001F6E RID: 8046
			private WebCompletionSource <completion>5__2;

			// Token: 0x04001F6F RID: 8047
			private WebOperation <operation>5__3;

			// Token: 0x04001F70 RID: 8048
			private WebException <throwMe>5__4;

			// Token: 0x04001F71 RID: 8049
			private HttpWebResponse <response>5__5;

			// Token: 0x04001F72 RID: 8050
			private WebResponseStream <stream>5__6;

			// Token: 0x04001F73 RID: 8051
			private bool <redirect>5__7;

			// Token: 0x04001F74 RID: 8052
			private bool <mustReadAll>5__8;

			// Token: 0x04001F75 RID: 8053
			private WebOperation <ntlm>5__9;

			// Token: 0x04001F76 RID: 8054
			private BufferOffsetSize <writeBuffer>5__10;

			// Token: 0x04001F77 RID: 8055
			private ConfiguredTaskAwaitable<WebRequestStream>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04001F78 RID: 8056
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x04001F79 RID: 8057
			private TaskAwaiter<WebResponseStream> <>u__3;

			// Token: 0x04001F7A RID: 8058
			[TupleElementNames(new string[]
			{
				"response",
				"redirect",
				"mustReadAll",
				"writeBuffer",
				"ntlm"
			})]
			private ConfiguredTaskAwaitable<ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation>>.ConfiguredTaskAwaiter <>u__4;
		}

		// Token: 0x0200069A RID: 1690
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <GetResponseFromData>d__247 : IAsyncStateMachine
		{
			// Token: 0x0600361F RID: 13855 RVA: 0x000BD758 File Offset: 0x000BB958
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				HttpWebRequest httpWebRequest = this.<>4__this;
				ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation> result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						WebOperation item;
						BufferOffsetSize bufferOffsetSize;
						object locker;
						bool flag;
						ConfiguredTaskAwaitable<BufferOffsetSize>.ConfiguredTaskAwaiter awaiter2;
						if (num != 1)
						{
							this.<response>5__2 = new HttpWebResponse(httpWebRequest.actualUri, httpWebRequest.method, this.stream, httpWebRequest.cookieContainer);
							this.<throwMe>5__3 = null;
							this.<redirect>5__4 = false;
							this.<mustReadAll>5__5 = false;
							item = null;
							Task<BufferOffsetSize> task = null;
							bufferOffsetSize = null;
							locker = httpWebRequest.locker;
							flag = false;
							try
							{
								Monitor.Enter(locker, ref flag);
								ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException> valueTuple = httpWebRequest.CheckFinalStatus(this.<response>5__2);
								this.<redirect>5__4 = valueTuple.Item1;
								this.<mustReadAll>5__5 = valueTuple.Item2;
								task = valueTuple.Item3;
								this.<throwMe>5__3 = valueTuple.Item4;
							}
							finally
							{
								if (num < 0 && flag)
								{
									Monitor.Exit(locker);
								}
							}
							if (this.<throwMe>5__3 != null)
							{
								if (!this.<mustReadAll>5__5)
								{
									goto IL_146;
								}
								awaiter = this.stream.ReadAllAsync(false, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 0);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, HttpWebRequest.<GetResponseFromData>d__247>(ref awaiter, ref this);
									return;
								}
								goto IL_13F;
							}
							else
							{
								if (task == null)
								{
									goto IL_1B7;
								}
								awaiter2 = task.ConfigureAwait(false).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (this.<>1__state = 1);
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<BufferOffsetSize>.ConfiguredTaskAwaiter, HttpWebRequest.<GetResponseFromData>d__247>(ref awaiter2, ref this);
									return;
								}
							}
						}
						else
						{
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<BufferOffsetSize>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						bufferOffsetSize = awaiter2.GetResult();
						IL_1B7:
						locker = httpWebRequest.locker;
						flag = false;
						try
						{
							Monitor.Enter(locker, ref flag);
							bool flag2 = httpWebRequest.ProxyQuery && httpWebRequest.proxy != null && !httpWebRequest.proxy.IsBypassed(httpWebRequest.actualUri);
							if (!this.<redirect>5__4)
							{
								if ((flag2 ? httpWebRequest.proxy_auth_state : httpWebRequest.auth_state).IsNtlmAuthenticated && this.<response>5__2.StatusCode < HttpStatusCode.BadRequest)
								{
									this.stream.Connection.NtlmAuthenticated = true;
								}
								if (httpWebRequest.writeStream != null)
								{
									httpWebRequest.writeStream.KillBuffer();
								}
								result = new ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation>(this.<response>5__2, false, false, bufferOffsetSize, null);
								goto IL_2F7;
							}
							if (httpWebRequest.sendChunked)
							{
								httpWebRequest.sendChunked = false;
								httpWebRequest.webHeaders.RemoveInternal("Transfer-Encoding");
							}
							item = httpWebRequest.HandleNtlmAuth(this.stream, this.<response>5__2, bufferOffsetSize, this.cancellationToken).Item1;
						}
						finally
						{
							if (num < 0 && flag)
							{
								Monitor.Exit(locker);
							}
						}
						result = new ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation>(this.<response>5__2, true, this.<mustReadAll>5__5, bufferOffsetSize, item);
						goto IL_2F7;
					}
					awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					num = (this.<>1__state = -1);
					IL_13F:
					awaiter.GetResult();
					IL_146:
					throw this.<throwMe>5__3;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<response>5__2 = null;
					this.<throwMe>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_2F7:
				this.<>1__state = -2;
				this.<response>5__2 = null;
				this.<throwMe>5__3 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06003620 RID: 13856 RVA: 0x000BDACC File Offset: 0x000BBCCC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001F7B RID: 8059
			public int <>1__state;

			// Token: 0x04001F7C RID: 8060
			[TupleElementNames(new string[]
			{
				"response",
				"redirect",
				"mustReadAll",
				"writeBuffer",
				"ntlm"
			})]
			public AsyncTaskMethodBuilder<ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation>> <>t__builder;

			// Token: 0x04001F7D RID: 8061
			public HttpWebRequest <>4__this;

			// Token: 0x04001F7E RID: 8062
			public WebResponseStream stream;

			// Token: 0x04001F7F RID: 8063
			public CancellationToken cancellationToken;

			// Token: 0x04001F80 RID: 8064
			private HttpWebResponse <response>5__2;

			// Token: 0x04001F81 RID: 8065
			private WebException <throwMe>5__3;

			// Token: 0x04001F82 RID: 8066
			private bool <redirect>5__4;

			// Token: 0x04001F83 RID: 8067
			private bool <mustReadAll>5__5;

			// Token: 0x04001F84 RID: 8068
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04001F85 RID: 8069
			private ConfiguredTaskAwaitable<BufferOffsetSize>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x0200069B RID: 1691
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <<GetRewriteHandler>b__274_0>d : IAsyncStateMachine
		{
			// Token: 0x06003621 RID: 13857 RVA: 0x000BDADC File Offset: 0x000BBCDC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				HttpWebRequest httpWebRequest = this.<>4__this;
				BufferOffsetSize result;
				try
				{
					if (num != 0)
					{
						this.<ms>5__2 = new MemoryStream();
					}
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = httpWebRequest.ResendContentFactory(this.<ms>5__2).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, HttpWebRequest.<<GetRewriteHandler>b__274_0>d>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						awaiter.GetResult();
						byte[] array = this.<ms>5__2.ToArray();
						result = new BufferOffsetSize(array, 0, array.Length, false);
					}
					finally
					{
						if (num < 0 && this.<ms>5__2 != null)
						{
							((IDisposable)this.<ms>5__2).Dispose();
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06003622 RID: 13858 RVA: 0x000BDBF8 File Offset: 0x000BBDF8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001F86 RID: 8070
			public int <>1__state;

			// Token: 0x04001F87 RID: 8071
			public AsyncTaskMethodBuilder<BufferOffsetSize> <>t__builder;

			// Token: 0x04001F88 RID: 8072
			public HttpWebRequest <>4__this;

			// Token: 0x04001F89 RID: 8073
			private MemoryStream <ms>5__2;

			// Token: 0x04001F8A RID: 8074
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
