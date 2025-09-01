using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000441 RID: 1089
	internal sealed class RtfTreeNavigator : RtfNavigator
	{
		// Token: 0x06002B0C RID: 11020 RVA: 0x00103044 File Offset: 0x00101244
		public RtfTreeNavigator(XmlEventCache events, XmlNameTable nameTable)
		{
			this.events = events;
			this.constr = new NavigatorConstructor();
			this.nameTable = nameTable;
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x00103065 File Offset: 0x00101265
		public RtfTreeNavigator(RtfTreeNavigator that)
		{
			this.events = that.events;
			this.constr = that.constr;
			this.nameTable = that.nameTable;
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x00103091 File Offset: 0x00101291
		public override void CopyToWriter(XmlWriter writer)
		{
			this.events.EventsToWriter(writer);
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x0010309F File Offset: 0x0010129F
		public override XPathNavigator ToNavigator()
		{
			return this.constr.GetNavigator(this.events, this.nameTable);
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x001030B8 File Offset: 0x001012B8
		public override string Value
		{
			get
			{
				return this.events.EventsToString();
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06002B11 RID: 11025 RVA: 0x001030C5 File Offset: 0x001012C5
		public override string BaseURI
		{
			get
			{
				return this.events.BaseUri;
			}
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x001030D2 File Offset: 0x001012D2
		public override XPathNavigator Clone()
		{
			return new RtfTreeNavigator(this);
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x001030DC File Offset: 0x001012DC
		public override bool MoveTo(XPathNavigator other)
		{
			RtfTreeNavigator rtfTreeNavigator = other as RtfTreeNavigator;
			if (rtfTreeNavigator != null)
			{
				this.events = rtfTreeNavigator.events;
				this.constr = rtfTreeNavigator.constr;
				this.nameTable = rtfTreeNavigator.nameTable;
				return true;
			}
			return false;
		}

		// Token: 0x04002209 RID: 8713
		private XmlEventCache events;

		// Token: 0x0400220A RID: 8714
		private NavigatorConstructor constr;

		// Token: 0x0400220B RID: 8715
		private XmlNameTable nameTable;
	}
}
