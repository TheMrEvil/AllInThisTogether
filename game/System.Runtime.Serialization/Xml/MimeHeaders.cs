using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Xml
{
	// Token: 0x02000076 RID: 118
	internal class MimeHeaders
	{
		// Token: 0x060006A6 RID: 1702 RVA: 0x0001C82C File Offset: 0x0001AA2C
		public MimeHeaders()
		{
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001C840 File Offset: 0x0001AA40
		public ContentTypeHeader ContentType
		{
			get
			{
				MimeHeader mimeHeader;
				if (this.headers.TryGetValue("content-type", out mimeHeader))
				{
					return mimeHeader as ContentTypeHeader;
				}
				return null;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0001C86C File Offset: 0x0001AA6C
		public ContentIDHeader ContentID
		{
			get
			{
				MimeHeader mimeHeader;
				if (this.headers.TryGetValue("content-id", out mimeHeader))
				{
					return mimeHeader as ContentIDHeader;
				}
				return null;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001C898 File Offset: 0x0001AA98
		public ContentTransferEncodingHeader ContentTransferEncoding
		{
			get
			{
				MimeHeader mimeHeader;
				if (this.headers.TryGetValue("content-transfer-encoding", out mimeHeader))
				{
					return mimeHeader as ContentTransferEncodingHeader;
				}
				return null;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0001C8C4 File Offset: 0x0001AAC4
		public MimeVersionHeader MimeVersion
		{
			get
			{
				MimeHeader mimeHeader;
				if (this.headers.TryGetValue("mime-version", out mimeHeader))
				{
					return mimeHeader as MimeVersionHeader;
				}
				return null;
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001C8F0 File Offset: 0x0001AAF0
		public void Add(string name, string value, ref int remaining)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			if (!(name == "content-type"))
			{
				if (!(name == "content-id"))
				{
					if (!(name == "content-transfer-encoding"))
					{
						if (!(name == "mime-version"))
						{
							remaining += value.Length * 2;
						}
						else
						{
							this.Add(new MimeVersionHeader(value));
						}
					}
					else
					{
						this.Add(new ContentTransferEncodingHeader(value));
					}
				}
				else
				{
					this.Add(new ContentIDHeader(name, value));
				}
			}
			else
			{
				this.Add(new ContentTypeHeader(value));
			}
			remaining += name.Length * 2;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001C9A4 File Offset: 0x0001ABA4
		public void Add(MimeHeader header)
		{
			if (header == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("header");
			}
			MimeHeader mimeHeader;
			if (this.headers.TryGetValue(header.Name, out mimeHeader))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("MIME header '{0}' already exists.", new object[]
				{
					header.Name
				})));
			}
			this.headers.Add(header.Name, header);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001CA0C File Offset: 0x0001AC0C
		public void Release(ref int remaining)
		{
			foreach (MimeHeader mimeHeader in this.headers.Values)
			{
				remaining += mimeHeader.Value.Length * 2;
			}
		}

		// Token: 0x040002EF RID: 751
		private Dictionary<string, MimeHeader> headers = new Dictionary<string, MimeHeader>();

		// Token: 0x02000077 RID: 119
		private static class Constants
		{
			// Token: 0x040002F0 RID: 752
			public const string ContentTransferEncoding = "content-transfer-encoding";

			// Token: 0x040002F1 RID: 753
			public const string ContentID = "content-id";

			// Token: 0x040002F2 RID: 754
			public const string ContentType = "content-type";

			// Token: 0x040002F3 RID: 755
			public const string MimeVersion = "mime-version";
		}
	}
}
