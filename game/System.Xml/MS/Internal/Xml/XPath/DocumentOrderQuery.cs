using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000629 RID: 1577
	internal sealed class DocumentOrderQuery : CacheOutputQuery
	{
		// Token: 0x06004077 RID: 16503 RVA: 0x00164887 File Offset: 0x00162A87
		public DocumentOrderQuery(Query qyParent) : base(qyParent)
		{
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x00164890 File Offset: 0x00162A90
		private DocumentOrderQuery(DocumentOrderQuery other) : base(other)
		{
		}

		// Token: 0x06004079 RID: 16505 RVA: 0x0016489C File Offset: 0x00162A9C
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			XPathNavigator nav;
			while ((nav = this.input.Advance()) != null)
			{
				Query.Insert(this.outputBuffer, nav);
			}
			return this;
		}

		// Token: 0x0600407A RID: 16506 RVA: 0x001648D0 File Offset: 0x00162AD0
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			return this.input.MatchNode(context);
		}

		// Token: 0x0600407B RID: 16507 RVA: 0x001648DE File Offset: 0x00162ADE
		public override XPathNodeIterator Clone()
		{
			return new DocumentOrderQuery(this);
		}
	}
}
