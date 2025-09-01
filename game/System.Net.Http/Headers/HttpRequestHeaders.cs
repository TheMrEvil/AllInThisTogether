using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Headers
{
	/// <summary>Represents the collection of Request Headers as defined in RFC 2616.</summary>
	// Token: 0x02000048 RID: 72
	public sealed class HttpRequestHeaders : HttpHeaders
	{
		// Token: 0x0600028D RID: 653 RVA: 0x00009F77 File Offset: 0x00008177
		internal HttpRequestHeaders() : base(HttpHeaderKind.Request)
		{
		}

		/// <summary>Gets the value of the <see langword="Accept" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Accept" /> header for an HTTP request.</returns>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00009F80 File Offset: 0x00008180
		public HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> Accept
		{
			get
			{
				return base.GetValues<MediaTypeWithQualityHeaderValue>("Accept");
			}
		}

		/// <summary>Gets the value of the <see langword="Accept-Charset" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Accept-Charset" /> header for an HTTP request.</returns>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00009F8D File Offset: 0x0000818D
		public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptCharset
		{
			get
			{
				return base.GetValues<StringWithQualityHeaderValue>("Accept-Charset");
			}
		}

		/// <summary>Gets the value of the <see langword="Accept-Encoding" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Accept-Encoding" /> header for an HTTP request.</returns>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00009F9A File Offset: 0x0000819A
		public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptEncoding
		{
			get
			{
				return base.GetValues<StringWithQualityHeaderValue>("Accept-Encoding");
			}
		}

		/// <summary>Gets the value of the <see langword="Accept-Language" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Accept-Language" /> header for an HTTP request.</returns>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00009FA7 File Offset: 0x000081A7
		public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptLanguage
		{
			get
			{
				return base.GetValues<StringWithQualityHeaderValue>("Accept-Language");
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Authorization" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Authorization" /> header for an HTTP request.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00009FB4 File Offset: 0x000081B4
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00009FC1 File Offset: 0x000081C1
		public AuthenticationHeaderValue Authorization
		{
			get
			{
				return base.GetValue<AuthenticationHeaderValue>("Authorization");
			}
			set
			{
				base.AddOrRemove<AuthenticationHeaderValue>("Authorization", value, null);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Cache-Control" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Cache-Control" /> header for an HTTP request.</returns>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00009FD0 File Offset: 0x000081D0
		// (set) Token: 0x06000295 RID: 661 RVA: 0x00009FDD File Offset: 0x000081DD
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

		/// <summary>Gets the value of the <see langword="Connection" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Connection" /> header for an HTTP request.</returns>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00009FEC File Offset: 0x000081EC
		public HttpHeaderValueCollection<string> Connection
		{
			get
			{
				return base.GetValues<string>("Connection");
			}
		}

		/// <summary>Gets or sets a value that indicates if the <see langword="Connection" /> header for an HTTP request contains Close.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="Connection" /> header contains Close, otherwise <see langword="false" />.</returns>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00009FFC File Offset: 0x000081FC
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000A060 File Offset: 0x00008260
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

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000A0DA File Offset: 0x000082DA
		internal bool ConnectionKeepAlive
		{
			get
			{
				return this.Connection.Find((string l) => string.Equals(l, "Keep-Alive", StringComparison.OrdinalIgnoreCase)) != null;
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Date" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Date" /> header for an HTTP request.</returns>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000A109 File Offset: 0x00008309
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000A116 File Offset: 0x00008316
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

		/// <summary>Gets the value of the <see langword="Expect" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Expect" /> header for an HTTP request.</returns>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000A129 File Offset: 0x00008329
		public HttpHeaderValueCollection<NameValueWithParametersHeaderValue> Expect
		{
			get
			{
				return base.GetValues<NameValueWithParametersHeaderValue>("Expect");
			}
		}

		/// <summary>Gets or sets a value that indicates if the <see langword="Expect" /> header for an HTTP request contains Continue.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="Expect" /> header contains Continue, otherwise <see langword="false" />.</returns>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000A138 File Offset: 0x00008338
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000A198 File Offset: 0x00008398
		public bool? ExpectContinue
		{
			get
			{
				if (this.expectContinue != null)
				{
					return this.expectContinue;
				}
				if (this.TransferEncoding.Find((TransferCodingHeaderValue l) => string.Equals(l.Value, "100-continue", StringComparison.OrdinalIgnoreCase)) == null)
				{
					return null;
				}
				return new bool?(true);
			}
			set
			{
				bool? flag = this.expectContinue;
				bool? flag2 = value;
				if (flag.GetValueOrDefault() == flag2.GetValueOrDefault() & flag != null == (flag2 != null))
				{
					return;
				}
				this.Expect.Remove((NameValueWithParametersHeaderValue l) => l.Name == "100-continue");
				flag2 = value;
				bool flag3 = true;
				if (flag2.GetValueOrDefault() == flag3 & flag2 != null)
				{
					this.Expect.Add(new NameValueWithParametersHeaderValue("100-continue"));
				}
				this.expectContinue = value;
			}
		}

		/// <summary>Gets or sets the value of the <see langword="From" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="From" /> header for an HTTP request.</returns>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000A230 File Offset: 0x00008430
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000A23D File Offset: 0x0000843D
		public string From
		{
			get
			{
				return base.GetValue<string>("From");
			}
			set
			{
				if (!string.IsNullOrEmpty(value) && !Parser.EmailAddress.TryParse(value, out value))
				{
					throw new FormatException();
				}
				base.AddOrRemove("From", value);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Host" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Host" /> header for an HTTP request.</returns>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000A263 File Offset: 0x00008463
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000A270 File Offset: 0x00008470
		public string Host
		{
			get
			{
				return base.GetValue<string>("Host");
			}
			set
			{
				base.AddOrRemove("Host", value);
			}
		}

		/// <summary>Gets the value of the <see langword="If-Match" /> header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.  
		///  The value of the <see langword="If-Match" /> header for an HTTP request.</returns>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000A27E File Offset: 0x0000847E
		public HttpHeaderValueCollection<EntityTagHeaderValue> IfMatch
		{
			get
			{
				return base.GetValues<EntityTagHeaderValue>("If-Match");
			}
		}

		/// <summary>Gets or sets the value of the <see langword="If-Modified-Since" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="If-Modified-Since" /> header for an HTTP request.</returns>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000A28B File Offset: 0x0000848B
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000A298 File Offset: 0x00008498
		public DateTimeOffset? IfModifiedSince
		{
			get
			{
				return base.GetValue<DateTimeOffset?>("If-Modified-Since");
			}
			set
			{
				base.AddOrRemove<DateTimeOffset>("If-Modified-Since", value, Parser.DateTime.ToString);
			}
		}

		/// <summary>Gets the value of the <see langword="If-None-Match" /> header for an HTTP request.</summary>
		/// <returns>Gets the value of the <see langword="If-None-Match" /> header for an HTTP request.</returns>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000A2AB File Offset: 0x000084AB
		public HttpHeaderValueCollection<EntityTagHeaderValue> IfNoneMatch
		{
			get
			{
				return base.GetValues<EntityTagHeaderValue>("If-None-Match");
			}
		}

		/// <summary>Gets or sets the value of the <see langword="If-Range" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="If-Range" /> header for an HTTP request.</returns>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000A2B8 File Offset: 0x000084B8
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000A2C5 File Offset: 0x000084C5
		public RangeConditionHeaderValue IfRange
		{
			get
			{
				return base.GetValue<RangeConditionHeaderValue>("If-Range");
			}
			set
			{
				base.AddOrRemove<RangeConditionHeaderValue>("If-Range", value, null);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="If-Unmodified-Since" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="If-Unmodified-Since" /> header for an HTTP request.</returns>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000A2D4 File Offset: 0x000084D4
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000A2E1 File Offset: 0x000084E1
		public DateTimeOffset? IfUnmodifiedSince
		{
			get
			{
				return base.GetValue<DateTimeOffset?>("If-Unmodified-Since");
			}
			set
			{
				base.AddOrRemove<DateTimeOffset>("If-Unmodified-Since", value, Parser.DateTime.ToString);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Max-Forwards" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Max-Forwards" /> header for an HTTP request.</returns>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000A2F4 File Offset: 0x000084F4
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000A301 File Offset: 0x00008501
		public int? MaxForwards
		{
			get
			{
				return base.GetValue<int?>("Max-Forwards");
			}
			set
			{
				base.AddOrRemove<int>("Max-Forwards", value);
			}
		}

		/// <summary>Gets the value of the <see langword="Pragma" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Pragma" /> header for an HTTP request.</returns>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000A30F File Offset: 0x0000850F
		public HttpHeaderValueCollection<NameValueHeaderValue> Pragma
		{
			get
			{
				return base.GetValues<NameValueHeaderValue>("Pragma");
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Proxy-Authorization" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Proxy-Authorization" /> header for an HTTP request.</returns>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000A31C File Offset: 0x0000851C
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000A329 File Offset: 0x00008529
		public AuthenticationHeaderValue ProxyAuthorization
		{
			get
			{
				return base.GetValue<AuthenticationHeaderValue>("Proxy-Authorization");
			}
			set
			{
				base.AddOrRemove<AuthenticationHeaderValue>("Proxy-Authorization", value, null);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Range" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Range" /> header for an HTTP request.</returns>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000A338 File Offset: 0x00008538
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000A345 File Offset: 0x00008545
		public RangeHeaderValue Range
		{
			get
			{
				return base.GetValue<RangeHeaderValue>("Range");
			}
			set
			{
				base.AddOrRemove<RangeHeaderValue>("Range", value, null);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Referer" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Referer" /> header for an HTTP request.</returns>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000A354 File Offset: 0x00008554
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000A361 File Offset: 0x00008561
		public Uri Referrer
		{
			get
			{
				return base.GetValue<Uri>("Referer");
			}
			set
			{
				base.AddOrRemove<Uri>("Referer", value, null);
			}
		}

		/// <summary>Gets the value of the <see langword="TE" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="TE" /> header for an HTTP request.</returns>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000A370 File Offset: 0x00008570
		public HttpHeaderValueCollection<TransferCodingWithQualityHeaderValue> TE
		{
			get
			{
				return base.GetValues<TransferCodingWithQualityHeaderValue>("TE");
			}
		}

		/// <summary>Gets the value of the <see langword="Trailer" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Trailer" /> header for an HTTP request.</returns>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000A37D File Offset: 0x0000857D
		public HttpHeaderValueCollection<string> Trailer
		{
			get
			{
				return base.GetValues<string>("Trailer");
			}
		}

		/// <summary>Gets the value of the <see langword="Transfer-Encoding" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Transfer-Encoding" /> header for an HTTP request.</returns>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000A38A File Offset: 0x0000858A
		public HttpHeaderValueCollection<TransferCodingHeaderValue> TransferEncoding
		{
			get
			{
				return base.GetValues<TransferCodingHeaderValue>("Transfer-Encoding");
			}
		}

		/// <summary>Gets or sets a value that indicates if the <see langword="Transfer-Encoding" /> header for an HTTP request contains chunked.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="Transfer-Encoding" /> header contains chunked, otherwise <see langword="false" />.</returns>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000A398 File Offset: 0x00008598
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000A3F8 File Offset: 0x000085F8
		public bool? TransferEncodingChunked
		{
			get
			{
				if (this.transferEncodingChunked != null)
				{
					return this.transferEncodingChunked;
				}
				if (this.TransferEncoding.Find((TransferCodingHeaderValue l) => string.Equals(l.Value, "chunked", StringComparison.OrdinalIgnoreCase)) == null)
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

		/// <summary>Gets the value of the <see langword="Upgrade" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Upgrade" /> header for an HTTP request.</returns>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000A490 File Offset: 0x00008690
		public HttpHeaderValueCollection<ProductHeaderValue> Upgrade
		{
			get
			{
				return base.GetValues<ProductHeaderValue>("Upgrade");
			}
		}

		/// <summary>Gets the value of the <see langword="User-Agent" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="User-Agent" /> header for an HTTP request.</returns>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000A49D File Offset: 0x0000869D
		public HttpHeaderValueCollection<ProductInfoHeaderValue> UserAgent
		{
			get
			{
				return base.GetValues<ProductInfoHeaderValue>("User-Agent");
			}
		}

		/// <summary>Gets the value of the <see langword="Via" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Via" /> header for an HTTP request.</returns>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000A4AA File Offset: 0x000086AA
		public HttpHeaderValueCollection<ViaHeaderValue> Via
		{
			get
			{
				return base.GetValues<ViaHeaderValue>("Via");
			}
		}

		/// <summary>Gets the value of the <see langword="Warning" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Warning" /> header for an HTTP request.</returns>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000A4B7 File Offset: 0x000086B7
		public HttpHeaderValueCollection<WarningHeaderValue> Warning
		{
			get
			{
				return base.GetValues<WarningHeaderValue>("Warning");
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000A4C4 File Offset: 0x000086C4
		internal void AddHeaders(HttpRequestHeaders headers)
		{
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in headers)
			{
				base.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x04000112 RID: 274
		private bool? expectContinue;

		// Token: 0x02000049 RID: 73
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060002BE RID: 702 RVA: 0x0000A51C File Offset: 0x0000871C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060002BF RID: 703 RVA: 0x000022B8 File Offset: 0x000004B8
			public <>c()
			{
			}

			// Token: 0x060002C0 RID: 704 RVA: 0x0000A528 File Offset: 0x00008728
			internal bool <get_ConnectionClose>b__19_0(string l)
			{
				return string.Equals(l, "close", StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x060002C1 RID: 705 RVA: 0x00003400 File Offset: 0x00001600
			internal bool <get_ConnectionKeepAlive>b__22_0(string l)
			{
				return string.Equals(l, "Keep-Alive", StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x060002C2 RID: 706 RVA: 0x0000A536 File Offset: 0x00008736
			internal bool <get_ExpectContinue>b__29_0(TransferCodingHeaderValue l)
			{
				return string.Equals(l.Value, "100-continue", StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x060002C3 RID: 707 RVA: 0x0000A549 File Offset: 0x00008749
			internal bool <set_ExpectContinue>b__30_0(NameValueWithParametersHeaderValue l)
			{
				return l.Name == "100-continue";
			}

			// Token: 0x060002C4 RID: 708 RVA: 0x0000A55B File Offset: 0x0000875B
			internal bool <get_TransferEncodingChunked>b__71_0(TransferCodingHeaderValue l)
			{
				return string.Equals(l.Value, "chunked", StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x060002C5 RID: 709 RVA: 0x0000A56E File Offset: 0x0000876E
			internal bool <set_TransferEncodingChunked>b__72_0(TransferCodingHeaderValue l)
			{
				return l.Value == "chunked";
			}

			// Token: 0x04000113 RID: 275
			public static readonly HttpRequestHeaders.<>c <>9 = new HttpRequestHeaders.<>c();

			// Token: 0x04000114 RID: 276
			public static Predicate<string> <>9__19_0;

			// Token: 0x04000115 RID: 277
			public static Predicate<string> <>9__22_0;

			// Token: 0x04000116 RID: 278
			public static Predicate<TransferCodingHeaderValue> <>9__29_0;

			// Token: 0x04000117 RID: 279
			public static Predicate<NameValueWithParametersHeaderValue> <>9__30_0;

			// Token: 0x04000118 RID: 280
			public static Predicate<TransferCodingHeaderValue> <>9__71_0;

			// Token: 0x04000119 RID: 281
			public static Predicate<TransferCodingHeaderValue> <>9__72_0;
		}
	}
}
