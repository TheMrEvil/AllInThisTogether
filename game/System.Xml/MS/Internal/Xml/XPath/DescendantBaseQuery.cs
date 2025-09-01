using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000626 RID: 1574
	internal abstract class DescendantBaseQuery : BaseAxisQuery
	{
		// Token: 0x06004068 RID: 16488 RVA: 0x00164577 File Offset: 0x00162777
		public DescendantBaseQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type, bool matchSelf, bool abbrAxis) : base(qyParent, Name, Prefix, Type)
		{
			this.matchSelf = matchSelf;
			this.abbrAxis = abbrAxis;
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x00164594 File Offset: 0x00162794
		public DescendantBaseQuery(DescendantBaseQuery other) : base(other)
		{
			this.matchSelf = other.matchSelf;
			this.abbrAxis = other.abbrAxis;
		}

		// Token: 0x0600406A RID: 16490 RVA: 0x001645B8 File Offset: 0x001627B8
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			if (context != null)
			{
				if (!this.abbrAxis)
				{
					throw XPathException.Create("'{0}' is an invalid XSLT pattern.");
				}
				if (this.matches(context))
				{
					XPathNavigator result;
					if (this.matchSelf && (result = this.qyInput.MatchNode(context)) != null)
					{
						return result;
					}
					XPathNavigator xpathNavigator = context.Clone();
					while (xpathNavigator.MoveToParent())
					{
						if ((result = this.qyInput.MatchNode(xpathNavigator)) != null)
						{
							return result;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x04002E0C RID: 11788
		protected bool matchSelf;

		// Token: 0x04002E0D RID: 11789
		protected bool abbrAxis;
	}
}
