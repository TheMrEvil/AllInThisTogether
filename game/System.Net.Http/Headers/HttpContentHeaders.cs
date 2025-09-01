using System;
using System.Collections.Generic;
using Unity;

namespace System.Net.Http.Headers
{
	/// <summary>Represents the collection of Content Headers as defined in RFC 2616.</summary>
	// Token: 0x02000042 RID: 66
	public sealed class HttpContentHeaders : HttpHeaders
	{
		// Token: 0x0600023C RID: 572 RVA: 0x00008D4C File Offset: 0x00006F4C
		internal HttpContentHeaders(HttpContent content) : base(HttpHeaderKind.Content)
		{
			this.content = content;
		}

		/// <summary>Gets the value of the <see langword="Allow" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Allow" /> header on an HTTP response.</returns>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00008D5C File Offset: 0x00006F5C
		public ICollection<string> Allow
		{
			get
			{
				return base.GetValues<string>("Allow");
			}
		}

		/// <summary>Gets the value of the <see langword="Content-Encoding" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Content-Encoding" /> content header on an HTTP response.</returns>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00008D69 File Offset: 0x00006F69
		public ICollection<string> ContentEncoding
		{
			get
			{
				return base.GetValues<string>("Content-Encoding");
			}
		}

		/// <summary>Gets the value of the <see langword="Content-Disposition" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Content-Disposition" /> content header on an HTTP response.</returns>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00008D76 File Offset: 0x00006F76
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00008D83 File Offset: 0x00006F83
		public ContentDispositionHeaderValue ContentDisposition
		{
			get
			{
				return base.GetValue<ContentDispositionHeaderValue>("Content-Disposition");
			}
			set
			{
				base.AddOrRemove<ContentDispositionHeaderValue>("Content-Disposition", value, null);
			}
		}

		/// <summary>Gets the value of the <see langword="Content-Language" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Content-Language" /> content header on an HTTP response.</returns>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00008D92 File Offset: 0x00006F92
		public ICollection<string> ContentLanguage
		{
			get
			{
				return base.GetValues<string>("Content-Language");
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Content-Length" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Content-Length" /> content header on an HTTP response.</returns>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00008DA0 File Offset: 0x00006FA0
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00008E07 File Offset: 0x00007007
		public long? ContentLength
		{
			get
			{
				long? result = base.GetValue<long?>("Content-Length");
				if (result != null)
				{
					return result;
				}
				result = this.content.LoadedBufferLength;
				if (result != null)
				{
					return result;
				}
				long value;
				if (this.content.TryComputeLength(out value))
				{
					base.SetValue<long>("Content-Length", value, null);
					return new long?(value);
				}
				return null;
			}
			set
			{
				base.AddOrRemove<long>("Content-Length", value);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Content-Location" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Content-Location" /> content header on an HTTP response.</returns>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00008E15 File Offset: 0x00007015
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00008E22 File Offset: 0x00007022
		public Uri ContentLocation
		{
			get
			{
				return base.GetValue<Uri>("Content-Location");
			}
			set
			{
				base.AddOrRemove<Uri>("Content-Location", value, null);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Content-MD5" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Content-MD5" /> content header on an HTTP response.</returns>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00008E31 File Offset: 0x00007031
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00008E3E File Offset: 0x0000703E
		public byte[] ContentMD5
		{
			get
			{
				return base.GetValue<byte[]>("Content-MD5");
			}
			set
			{
				base.AddOrRemove<byte[]>("Content-MD5", value, Parser.MD5.ToString);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Content-Range" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Content-Range" /> content header on an HTTP response.</returns>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00008E51 File Offset: 0x00007051
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00008E5E File Offset: 0x0000705E
		public ContentRangeHeaderValue ContentRange
		{
			get
			{
				return base.GetValue<ContentRangeHeaderValue>("Content-Range");
			}
			set
			{
				base.AddOrRemove<ContentRangeHeaderValue>("Content-Range", value, null);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Content-Type" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Content-Type" /> content header on an HTTP response.</returns>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00008E6D File Offset: 0x0000706D
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00008E7A File Offset: 0x0000707A
		public MediaTypeHeaderValue ContentType
		{
			get
			{
				return base.GetValue<MediaTypeHeaderValue>("Content-Type");
			}
			set
			{
				base.AddOrRemove<MediaTypeHeaderValue>("Content-Type", value, null);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Expires" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Expires" /> content header on an HTTP response.</returns>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00008E89 File Offset: 0x00007089
		// (set) Token: 0x0600024D RID: 589 RVA: 0x00008E96 File Offset: 0x00007096
		public DateTimeOffset? Expires
		{
			get
			{
				return base.GetValue<DateTimeOffset?>("Expires");
			}
			set
			{
				base.AddOrRemove<DateTimeOffset>("Expires", value, Parser.DateTime.ToString);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Last-Modified" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Last-Modified" /> content header on an HTTP response.</returns>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00008EA9 File Offset: 0x000070A9
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00008EB6 File Offset: 0x000070B6
		public DateTimeOffset? LastModified
		{
			get
			{
				return base.GetValue<DateTimeOffset?>("Last-Modified");
			}
			set
			{
				base.AddOrRemove<DateTimeOffset>("Last-Modified", value, Parser.DateTime.ToString);
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008EC9 File Offset: 0x000070C9
		internal HttpContentHeaders()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040000FC RID: 252
		private readonly HttpContent content;
	}
}
