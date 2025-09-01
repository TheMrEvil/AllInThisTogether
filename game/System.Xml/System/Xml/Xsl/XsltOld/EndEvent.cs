using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000387 RID: 903
	internal class EndEvent : Event
	{
		// Token: 0x060024BC RID: 9404 RVA: 0x000E0202 File Offset: 0x000DE402
		internal EndEvent(XPathNodeType nodeType)
		{
			this.nodeType = nodeType;
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000E0211 File Offset: 0x000DE411
		public override bool Output(Processor processor, ActionFrame frame)
		{
			return processor.EndEvent(this.nodeType);
		}

		// Token: 0x04001D0A RID: 7434
		private XPathNodeType nodeType;
	}
}
