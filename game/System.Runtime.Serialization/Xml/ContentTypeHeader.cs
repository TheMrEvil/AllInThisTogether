using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Xml
{
	// Token: 0x02000079 RID: 121
	internal class ContentTypeHeader : MimeHeader
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x0001CAA4 File Offset: 0x0001ACA4
		public ContentTypeHeader(string value) : base("content-type", value)
		{
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0001CAB2 File Offset: 0x0001ACB2
		public string MediaType
		{
			get
			{
				if (this.mediaType == null && base.Value != null)
				{
					this.ParseValue();
				}
				return this.mediaType;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001CAD0 File Offset: 0x0001ACD0
		public string MediaSubtype
		{
			get
			{
				if (this.subType == null && base.Value != null)
				{
					this.ParseValue();
				}
				return this.subType;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0001CAEE File Offset: 0x0001ACEE
		public Dictionary<string, string> Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					if (base.Value != null)
					{
						this.ParseValue();
					}
					else
					{
						this.parameters = new Dictionary<string, string>();
					}
				}
				return this.parameters;
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001CB1C File Offset: 0x0001AD1C
		private void ParseValue()
		{
			if (this.parameters == null)
			{
				int num = 0;
				this.parameters = new Dictionary<string, string>();
				this.mediaType = MailBnfHelper.ReadToken(base.Value, ref num, null);
				if (num >= base.Value.Length || base.Value[num++] != '/')
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("MIME content type header is invalid.")));
				}
				this.subType = MailBnfHelper.ReadToken(base.Value, ref num, null);
				while (MailBnfHelper.SkipCFWS(base.Value, ref num))
				{
					if (num >= base.Value.Length || base.Value[num++] != ';')
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("MIME content type header is invalid.")));
					}
					if (!MailBnfHelper.SkipCFWS(base.Value, ref num))
					{
						break;
					}
					string text = MailBnfHelper.ReadParameterAttribute(base.Value, ref num, null);
					if (text == null || num >= base.Value.Length || base.Value[num++] != '=')
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("MIME content type header is invalid.")));
					}
					string value = MailBnfHelper.ReadParameterValue(base.Value, ref num, null);
					this.parameters.Add(text.ToLowerInvariant(), value);
				}
				if (this.parameters.ContainsKey(MtomGlobals.StartInfoParam))
				{
					string text2 = this.parameters[MtomGlobals.StartInfoParam];
					int num2 = text2.IndexOf(';');
					if (num2 > -1)
					{
						while (MailBnfHelper.SkipCFWS(text2, ref num2))
						{
							if (text2[num2] != ';')
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("MIME content type header is invalid.")));
							}
							num2++;
							string text3 = MailBnfHelper.ReadParameterAttribute(text2, ref num2, null);
							if (text3 == null || num2 >= text2.Length || text2[num2++] != '=')
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("MIME content type header is invalid.")));
							}
							string value2 = MailBnfHelper.ReadParameterValue(text2, ref num2, null);
							if (text3 == MtomGlobals.ActionParam)
							{
								this.parameters[MtomGlobals.ActionParam] = value2;
							}
						}
					}
				}
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001CD3F File Offset: 0x0001AF3F
		// Note: this type is marked as 'beforefieldinit'.
		static ContentTypeHeader()
		{
		}

		// Token: 0x040002F6 RID: 758
		public static readonly ContentTypeHeader Default = new ContentTypeHeader("application/octet-stream");

		// Token: 0x040002F7 RID: 759
		private string mediaType;

		// Token: 0x040002F8 RID: 760
		private string subType;

		// Token: 0x040002F9 RID: 761
		private Dictionary<string, string> parameters;
	}
}
