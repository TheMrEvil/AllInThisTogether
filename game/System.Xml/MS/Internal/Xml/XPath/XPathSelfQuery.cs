using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000664 RID: 1636
	internal sealed class XPathSelfQuery : BaseAxisQuery
	{
		// Token: 0x06004247 RID: 16967 RVA: 0x001635D7 File Offset: 0x001617D7
		public XPathSelfQuery(Query qyInput, string Name, string Prefix, XPathNodeType Type) : base(qyInput, Name, Prefix, Type)
		{
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x0016563F File Offset: 0x0016383F
		private XPathSelfQuery(XPathSelfQuery other) : base(other)
		{
		}

		// Token: 0x06004249 RID: 16969 RVA: 0x0016A9E0 File Offset: 0x00168BE0
		public override XPathNavigator Advance()
		{
			while ((this.currentNode = this.qyInput.Advance()) != null)
			{
				if (this.matches(this.currentNode))
				{
					this.position = 1;
					return this.currentNode;
				}
			}
			return null;
		}

		// Token: 0x0600424A RID: 16970 RVA: 0x0016AA22 File Offset: 0x00168C22
		public override XPathNodeIterator Clone()
		{
			return new XPathSelfQuery(this);
		}
	}
}
