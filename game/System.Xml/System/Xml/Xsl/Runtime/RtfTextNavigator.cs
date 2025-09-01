using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000442 RID: 1090
	internal sealed class RtfTextNavigator : RtfNavigator
	{
		// Token: 0x06002B14 RID: 11028 RVA: 0x0010311A File Offset: 0x0010131A
		public RtfTextNavigator(string text, string baseUri)
		{
			this.text = text;
			this.baseUri = baseUri;
			this.constr = new NavigatorConstructor();
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x0010313B File Offset: 0x0010133B
		public RtfTextNavigator(RtfTextNavigator that)
		{
			this.text = that.text;
			this.baseUri = that.baseUri;
			this.constr = that.constr;
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x00103167 File Offset: 0x00101367
		public override void CopyToWriter(XmlWriter writer)
		{
			writer.WriteString(this.Value);
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x00103175 File Offset: 0x00101375
		public override XPathNavigator ToNavigator()
		{
			return this.constr.GetNavigator(this.text, this.baseUri, new NameTable());
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002B18 RID: 11032 RVA: 0x00103193 File Offset: 0x00101393
		public override string Value
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x0010319B File Offset: 0x0010139B
		public override string BaseURI
		{
			get
			{
				return this.baseUri;
			}
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x001031A3 File Offset: 0x001013A3
		public override XPathNavigator Clone()
		{
			return new RtfTextNavigator(this);
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x001031AC File Offset: 0x001013AC
		public override bool MoveTo(XPathNavigator other)
		{
			RtfTextNavigator rtfTextNavigator = other as RtfTextNavigator;
			if (rtfTextNavigator != null)
			{
				this.text = rtfTextNavigator.text;
				this.baseUri = rtfTextNavigator.baseUri;
				this.constr = rtfTextNavigator.constr;
				return true;
			}
			return false;
		}

		// Token: 0x0400220C RID: 8716
		private string text;

		// Token: 0x0400220D RID: 8717
		private string baseUri;

		// Token: 0x0400220E RID: 8718
		private NavigatorConstructor constr;
	}
}
