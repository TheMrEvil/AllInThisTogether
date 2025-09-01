using System;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Headers
{
	/// <summary>Represents the collection of Response Headers as defined in RFC 2616.</summary>
	// Token: 0x0200004A RID: 74
	public sealed class HttpResponseHeaders : HttpHeaders
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000A580 File Offset: 0x00008780
		internal HttpResponseHeaders() : base(HttpHeaderKind.Response)
		{
		}

		/// <summary>Gets the value of the <see langword="Accept-Ranges" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Accept-Ranges" /> header for an HTTP response.</returns>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000A589 File Offset: 0x00008789
		public HttpHeaderValueCollection<string> AcceptRanges
		{
			get
			{
				return base.GetValues<string>("Accept-Ranges");
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Age" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Age" /> header for an HTTP response.</returns>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000A596 File Offset: 0x00008796
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0000A5A3 File Offset: 0x000087A3
		public TimeSpan? Age
		{
			get
			{
				return base.GetValue<TimeSpan?>("Age");
			}
			set
			{
				base.AddOrRemove<TimeSpan>("Age", value, (object l) => ((long)((TimeSpan)l).TotalSeconds).ToString());
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Cache-Control" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Cache-Control" /> header for an HTTP response.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00009FD0 File Offset: 0x000081D0
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00009FDD File Offset: 0x000081DD
		public CacheControlHeaderValue CacheControl
		{
			get
			{
				return base.GetValue<CacheControlHeaderValue>("Cache-Control");
			}
			set
			{
				base.AddOrRemove<CacheControlHeaderValue>("Cache-Control", value, null);
			}
		}

		/// <summary>Gets the value of the <see langword="Connection" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Connection" /> header for an HTTP response.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00009FEC File Offset: 0x000081EC
		public HttpHeaderValueCollection<string> Connection
		{
			get
			{
				return base.GetValues<string>("Connection");
			}
		}

		/// <summary>Gets or sets a value that indicates if the <see langword="Connection" /> header for an HTTP response contains Close.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="Connection" /> header contains Close, otherwise <see langword="false" />.</returns>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000A5D0 File Offset: 0x000087D0
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000A634 File Offset: 0x00008834
		public bool? ConnectionClose
		{
			get
			{
				bool? connectionclose = this.connectionclose;
				bool flag = true;
				if (!(connectionclose.GetValueOrDefault() == flag & connectionclose != null))
				{
					if (this.Connection.Find((string l) => string.Equals(l, "close", StringComparison.OrdinalIgnoreCase)) == null)
					{
						return this.connectionclose;
					}
				}
				return new bool?(true);
			}
			set
			{
				bool? connectionclose = this.connectionclose;
				bool? flag = value;
				if (connectionclose.GetValueOrDefault() == flag.GetValueOrDefault() & connectionclose != null == (flag != null))
				{
					return;
				}
				this.Connection.Remove("close");
				flag = value;
				bool flag2 = true;
				if (flag.GetValueOrDefault() == flag2 & flag != null)
				{
					this.Connection.Add("close");
				}
				this.connectionclose = value;
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Date" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Date" /> header for an HTTP response.</returns>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000A109 File Offset: 0x00008309
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000A116 File Offset: 0x00008316
		public DateTimeOffset? Date
		{
			get
			{
				return base.GetValue<DateTimeOffset?>("Date");
			}
			set
			{
				base.AddOrRemove<DateTimeOffset>("Date", value, Parser.DateTime.ToString);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="ETag" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="ETag" /> header for an HTTP response.</returns>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000A6AE File Offset: 0x000088AE
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000A6BB File Offset: 0x000088BB
		public EntityTagHeaderValue ETag
		{
			get
			{
				return base.GetValue<EntityTagHeaderValue>("ETag");
			}
			set
			{
				base.AddOrRemove<EntityTagHeaderValue>("ETag", value, null);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Location" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Location" /> header for an HTTP response.</returns>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000A6CA File Offset: 0x000088CA
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000A6D7 File Offset: 0x000088D7
		public Uri Location
		{
			get
			{
				return base.GetValue<Uri>("Location");
			}
			set
			{
				base.AddOrRemove<Uri>("Location", value, null);
			}
		}

		/// <summary>Gets the value of the <see langword="Pragma" /> header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.  
		///  The value of the <see langword="Pragma" /> header for an HTTP response.</returns>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000A30F File Offset: 0x0000850F
		public HttpHeaderValueCollection<NameValueHeaderValue> Pragma
		{
			get
			{
				return base.GetValues<NameValueHeaderValue>("Pragma");
			}
		}

		/// <summary>Gets the value of the <see langword="Proxy-Authenticate" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Proxy-Authenticate" /> header for an HTTP response.</returns>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000A6E6 File Offset: 0x000088E6
		public HttpHeaderValueCollection<AuthenticationHeaderValue> ProxyAuthenticate
		{
			get
			{
				return base.GetValues<AuthenticationHeaderValue>("Proxy-Authenticate");
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Retry-After" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Retry-After" /> header for an HTTP response.</returns>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000A6F3 File Offset: 0x000088F3
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000A700 File Offset: 0x00008900
		public RetryConditionHeaderValue RetryAfter
		{
			get
			{
				return base.GetValue<RetryConditionHeaderValue>("Retry-After");
			}
			set
			{
				base.AddOrRemove<RetryConditionHeaderValue>("Retry-After", value, null);
			}
		}

		/// <summary>Gets the value of the <see langword="Server" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Server" /> header for an HTTP response.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000A70F File Offset: 0x0000890F
		public HttpHeaderValueCollection<ProductInfoHeaderValue> Server
		{
			get
			{
				return base.GetValues<ProductInfoHeaderValue>("Server");
			}
		}

		/// <summary>Gets the value of the <see langword="Trailer" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Trailer" /> header for an HTTP response.</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000A37D File Offset: 0x0000857D
		public HttpHeaderValueCollection<string> Trailer
		{
			get
			{
				return base.GetValues<string>("Trailer");
			}
		}

		/// <summary>Gets the value of the <see langword="Transfer-Encoding" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Transfer-Encoding" /> header for an HTTP response.</returns>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000A38A File Offset: 0x0000858A
		public HttpHeaderValueCollection<TransferCodingHeaderValue> TransferEncoding
		{
			get
			{
				return base.GetValues<TransferCodingHeaderValue>("Transfer-Encoding");
			}
		}

		/// <summary>Gets or sets a value that indicates if the <see langword="Transfer-Encoding" /> header for an HTTP response contains chunked.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="Transfer-Encoding" /> header contains chunked, otherwise <see langword="false" />.</returns>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000A71C File Offset: 0x0000891C
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000A77C File Offset: 0x0000897C
		public bool? TransferEncodingChunked
		{
			get
			{
				if (this.transferEncodingChunked != null)
				{
					return this.transferEncodingChunked;
				}
				if (this.TransferEncoding.Find((TransferCodingHeaderValue l) => StringComparer.OrdinalIgnoreCase.Equals(l.Value, "chunked")) == null)
				{
					return null;
				}
				return new bool?(true);
			}
			set
			{
				bool? flag = value;
				bool? flag2 = this.transferEncodingChunked;
				if (flag.GetValueOrDefault() == flag2.GetValueOrDefault() & flag != null == (flag2 != null))
				{
					return;
				}
				this.TransferEncoding.Remove((TransferCodingHeaderValue l) => l.Value == "chunked");
				flag2 = value;
				bool flag3 = true;
				if (flag2.GetValueOrDefault() == flag3 & flag2 != null)
				{
					this.TransferEncoding.Add(new TransferCodingHeaderValue("chunked"));
				}
				this.transferEncodingChunked = value;
			}
		}

		/// <summary>Gets the value of the <see langword="Upgrade" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Upgrade" /> header for an HTTP response.</returns>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000A490 File Offset: 0x00008690
		public HttpHeaderValueCollection<ProductHeaderValue> Upgrade
		{
			get
			{
				return base.GetValues<ProductHeaderValue>("Upgrade");
			}
		}

		/// <summary>Gets the value of the <see langword="Vary" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Vary" /> header for an HTTP response.</returns>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000A814 File Offset: 0x00008A14
		public HttpHeaderValueCollection<string> Vary
		{
			get
			{
				return base.GetValues<string>("Vary");
			}
		}

		/// <summary>Gets the value of the <see langword="Via" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Via" /> header for an HTTP response.</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000A4AA File Offset: 0x000086AA
		public HttpHeaderValueCollection<ViaHeaderValue> Via
		{
			get
			{
				return base.GetValues<ViaHeaderValue>("Via");
			}
		}

		/// <summary>Gets the value of the <see langword="Warning" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Warning" /> header for an HTTP response.</returns>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000A4B7 File Offset: 0x000086B7
		public HttpHeaderValueCollection<WarningHeaderValue> Warning
		{
			get
			{
				return base.GetValues<WarningHeaderValue>("Warning");
			}
		}

		/// <summary>Gets the value of the <see langword="WWW-Authenticate" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="WWW-Authenticate" /> header for an HTTP response.</returns>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000A821 File Offset: 0x00008A21
		public HttpHeaderValueCollection<AuthenticationHeaderValue> WwwAuthenticate
		{
			get
			{
				return base.GetValues<AuthenticationHeaderValue>("WWW-Authenticate");
			}
		}

		// Token: 0x0200004B RID: 75
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060002E3 RID: 739 RVA: 0x0000A82E File Offset: 0x00008A2E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060002E4 RID: 740 RVA: 0x000022B8 File Offset: 0x000004B8
			public <>c()
			{
			}

			// Token: 0x060002E5 RID: 741 RVA: 0x0000A83C File Offset: 0x00008A3C
			internal string <set_Age>b__5_0(object l)
			{
				return ((long)((TimeSpan)l).TotalSeconds).ToString();
			}

			// Token: 0x060002E6 RID: 742 RVA: 0x0000A528 File Offset: 0x00008728
			internal bool <get_ConnectionClose>b__12_0(string l)
			{
				return string.Equals(l, "close", StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x060002E7 RID: 743 RVA: 0x0000A860 File Offset: 0x00008A60
			internal bool <get_TransferEncodingChunked>b__37_0(TransferCodingHeaderValue l)
			{
				return StringComparer.OrdinalIgnoreCase.Equals(l.Value, "chunked");
			}

			// Token: 0x060002E8 RID: 744 RVA: 0x0000A56E File Offset: 0x0000876E
			internal bool <set_TransferEncodingChunked>b__38_0(TransferCodingHeaderValue l)
			{
				return l.Value == "chunked";
			}

			// Token: 0x0400011A RID: 282
			public static readonly HttpResponseHeaders.<>c <>9 = new HttpResponseHeaders.<>c();

			// Token: 0x0400011B RID: 283
			public static Func<object, string> <>9__5_0;

			// Token: 0x0400011C RID: 284
			public static Predicate<string> <>9__12_0;

			// Token: 0x0400011D RID: 285
			public static Predicate<TransferCodingHeaderValue> <>9__37_0;

			// Token: 0x0400011E RID: 286
			public static Predicate<TransferCodingHeaderValue> <>9__38_0;
		}
	}
}
