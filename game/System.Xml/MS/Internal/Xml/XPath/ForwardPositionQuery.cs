using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000630 RID: 1584
	internal class ForwardPositionQuery : CacheOutputQuery
	{
		// Token: 0x060040AB RID: 16555 RVA: 0x00164887 File Offset: 0x00162A87
		public ForwardPositionQuery(Query input) : base(input)
		{
		}

		// Token: 0x060040AC RID: 16556 RVA: 0x00164890 File Offset: 0x00162A90
		protected ForwardPositionQuery(ForwardPositionQuery other) : base(other)
		{
		}

		// Token: 0x060040AD RID: 16557 RVA: 0x0016527C File Offset: 0x0016347C
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.input.Advance()) != null)
			{
				this.outputBuffer.Add(xpathNavigator.Clone());
			}
			return this;
		}

		// Token: 0x060040AE RID: 16558 RVA: 0x001648D0 File Offset: 0x00162AD0
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			return this.input.MatchNode(context);
		}

		// Token: 0x060040AF RID: 16559 RVA: 0x001652B4 File Offset: 0x001634B4
		public override XPathNodeIterator Clone()
		{
			return new ForwardPositionQuery(this);
		}
	}
}
