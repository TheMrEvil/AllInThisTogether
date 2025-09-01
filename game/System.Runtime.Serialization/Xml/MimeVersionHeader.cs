using System;
using System.Runtime.Serialization;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200007D RID: 125
	internal class MimeVersionHeader : MimeHeader
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x0001CE8A File Offset: 0x0001B08A
		public MimeVersionHeader(string value) : base("mime-version", value)
		{
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001CE98 File Offset: 0x0001B098
		public string Version
		{
			get
			{
				if (this.version == null && base.Value != null)
				{
					this.ParseValue();
				}
				return this.version;
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001CEB8 File Offset: 0x0001B0B8
		private void ParseValue()
		{
			if (base.Value == "1.0")
			{
				this.version = "1.0";
				return;
			}
			int num = 0;
			if (!MailBnfHelper.SkipCFWS(base.Value, ref num))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("MIME version header is invalid.")));
			}
			StringBuilder stringBuilder = new StringBuilder();
			MailBnfHelper.ReadDigits(base.Value, ref num, stringBuilder);
			if (!MailBnfHelper.SkipCFWS(base.Value, ref num) || num >= base.Value.Length || base.Value[num++] != '.' || !MailBnfHelper.SkipCFWS(base.Value, ref num))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("MIME version header is invalid.")));
			}
			stringBuilder.Append('.');
			MailBnfHelper.ReadDigits(base.Value, ref num, stringBuilder);
			this.version = stringBuilder.ToString();
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001CF97 File Offset: 0x0001B197
		// Note: this type is marked as 'beforefieldinit'.
		static MimeVersionHeader()
		{
		}

		// Token: 0x04000305 RID: 773
		public static readonly MimeVersionHeader Default = new MimeVersionHeader("1.0");

		// Token: 0x04000306 RID: 774
		private string version;
	}
}
