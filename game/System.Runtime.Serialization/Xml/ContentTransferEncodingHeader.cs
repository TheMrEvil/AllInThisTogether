using System;

namespace System.Xml
{
	// Token: 0x0200007B RID: 123
	internal class ContentTransferEncodingHeader : MimeHeader
	{
		// Token: 0x060006B7 RID: 1719 RVA: 0x0001CD50 File Offset: 0x0001AF50
		public ContentTransferEncodingHeader(string value) : base("content-transfer-encoding", value.ToLowerInvariant())
		{
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001CD63 File Offset: 0x0001AF63
		public ContentTransferEncodingHeader(ContentTransferEncoding contentTransferEncoding, string value) : base("content-transfer-encoding", null)
		{
			this.contentTransferEncoding = contentTransferEncoding;
			this.contentTransferEncodingValue = value;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001CD7F File Offset: 0x0001AF7F
		public ContentTransferEncoding ContentTransferEncoding
		{
			get
			{
				this.ParseValue();
				return this.contentTransferEncoding;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0001CD8D File Offset: 0x0001AF8D
		public string ContentTransferEncodingValue
		{
			get
			{
				this.ParseValue();
				return this.contentTransferEncodingValue;
			}
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001CD9C File Offset: 0x0001AF9C
		private void ParseValue()
		{
			if (this.contentTransferEncodingValue == null)
			{
				int num = 0;
				this.contentTransferEncodingValue = ((base.Value.Length == 0) ? base.Value : ((base.Value[0] == '"') ? MailBnfHelper.ReadQuotedString(base.Value, ref num, null) : MailBnfHelper.ReadToken(base.Value, ref num, null)));
				string a = this.contentTransferEncodingValue;
				if (a == "7bit")
				{
					this.contentTransferEncoding = ContentTransferEncoding.SevenBit;
					return;
				}
				if (a == "8bit")
				{
					this.contentTransferEncoding = ContentTransferEncoding.EightBit;
					return;
				}
				if (a == "binary")
				{
					this.contentTransferEncoding = ContentTransferEncoding.Binary;
					return;
				}
				this.contentTransferEncoding = ContentTransferEncoding.Other;
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001CE4E File Offset: 0x0001B04E
		// Note: this type is marked as 'beforefieldinit'.
		static ContentTransferEncodingHeader()
		{
		}

		// Token: 0x04000300 RID: 768
		private ContentTransferEncoding contentTransferEncoding;

		// Token: 0x04000301 RID: 769
		private string contentTransferEncodingValue;

		// Token: 0x04000302 RID: 770
		public static readonly ContentTransferEncodingHeader Binary = new ContentTransferEncodingHeader(ContentTransferEncoding.Binary, "binary");

		// Token: 0x04000303 RID: 771
		public static readonly ContentTransferEncodingHeader EightBit = new ContentTransferEncodingHeader(ContentTransferEncoding.EightBit, "8bit");

		// Token: 0x04000304 RID: 772
		public static readonly ContentTransferEncodingHeader SevenBit = new ContentTransferEncodingHeader(ContentTransferEncoding.SevenBit, "7bit");
	}
}
