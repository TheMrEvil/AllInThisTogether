using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000648 RID: 1608
	[DebuggerDisplay("{ToString()}")]
	internal abstract class Query : ResetableIterator
	{
		// Token: 0x0600414D RID: 16717 RVA: 0x00166C66 File Offset: 0x00164E66
		public Query()
		{
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x00166C6E File Offset: 0x00164E6E
		protected Query(Query other) : base(other)
		{
		}

		// Token: 0x0600414F RID: 16719 RVA: 0x00166C77 File Offset: 0x00164E77
		public override bool MoveNext()
		{
			return this.Advance() != null;
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06004150 RID: 16720 RVA: 0x00166C84 File Offset: 0x00164E84
		public override int Count
		{
			get
			{
				if (this.count == -1)
				{
					Query query = (Query)this.Clone();
					query.Reset();
					this.count = 0;
					while (query.MoveNext())
					{
						this.count++;
					}
				}
				return this.count;
			}
		}

		// Token: 0x06004151 RID: 16721 RVA: 0x0000B528 File Offset: 0x00009728
		public virtual void SetXsltContext(XsltContext context)
		{
		}

		// Token: 0x06004152 RID: 16722
		public abstract object Evaluate(XPathNodeIterator nodeIterator);

		// Token: 0x06004153 RID: 16723
		public abstract XPathNavigator Advance();

		// Token: 0x06004154 RID: 16724 RVA: 0x00166CD1 File Offset: 0x00164ED1
		public virtual XPathNavigator MatchNode(XPathNavigator current)
		{
			throw XPathException.Create("'{0}' is an invalid XSLT pattern.");
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06004155 RID: 16725 RVA: 0x00166CDD File Offset: 0x00164EDD
		public virtual double XsltDefaultPriority
		{
			get
			{
				return 0.5;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06004156 RID: 16726
		public abstract XPathResultType StaticType { get; }

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06004157 RID: 16727 RVA: 0x0012B4AA File Offset: 0x001296AA
		public virtual QueryProps Properties
		{
			get
			{
				return QueryProps.Merge;
			}
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x00166CE8 File Offset: 0x00164EE8
		public static Query Clone(Query input)
		{
			if (input != null)
			{
				return (Query)input.Clone();
			}
			return null;
		}

		// Token: 0x06004159 RID: 16729 RVA: 0x00166CFA File Offset: 0x00164EFA
		protected static XPathNodeIterator Clone(XPathNodeIterator input)
		{
			if (input != null)
			{
				return input.Clone();
			}
			return null;
		}

		// Token: 0x0600415A RID: 16730 RVA: 0x00166D07 File Offset: 0x00164F07
		protected static XPathNavigator Clone(XPathNavigator input)
		{
			if (input != null)
			{
				return input.Clone();
			}
			return null;
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x00166D14 File Offset: 0x00164F14
		public static bool Insert(List<XPathNavigator> buffer, XPathNavigator nav)
		{
			int i = 0;
			int num = buffer.Count;
			if (num != 0)
			{
				XmlNodeOrder xmlNodeOrder = Query.CompareNodes(buffer[num - 1], nav);
				if (xmlNodeOrder == XmlNodeOrder.Before)
				{
					buffer.Add(nav.Clone());
					return true;
				}
				if (xmlNodeOrder == XmlNodeOrder.Same)
				{
					return false;
				}
				num--;
			}
			while (i < num)
			{
				int median = Query.GetMedian(i, num);
				XmlNodeOrder xmlNodeOrder = Query.CompareNodes(buffer[median], nav);
				if (xmlNodeOrder != XmlNodeOrder.Before)
				{
					if (xmlNodeOrder == XmlNodeOrder.Same)
					{
						return false;
					}
					num = median;
				}
				else
				{
					i = median + 1;
				}
			}
			buffer.Insert(i, nav.Clone());
			return true;
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x00166D93 File Offset: 0x00164F93
		private static int GetMedian(int l, int r)
		{
			return (int)((uint)(l + r) >> 1);
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x00166D9C File Offset: 0x00164F9C
		public static XmlNodeOrder CompareNodes(XPathNavigator l, XPathNavigator r)
		{
			XmlNodeOrder xmlNodeOrder = l.ComparePosition(r);
			if (xmlNodeOrder == XmlNodeOrder.Unknown)
			{
				XPathNavigator xpathNavigator = l.Clone();
				xpathNavigator.MoveToRoot();
				string baseURI = xpathNavigator.BaseURI;
				if (!xpathNavigator.MoveTo(r))
				{
					xpathNavigator = r.Clone();
				}
				xpathNavigator.MoveToRoot();
				string baseURI2 = xpathNavigator.BaseURI;
				int num = string.CompareOrdinal(baseURI, baseURI2);
				xmlNodeOrder = ((num < 0) ? XmlNodeOrder.Before : ((num > 0) ? XmlNodeOrder.After : XmlNodeOrder.Unknown));
			}
			return xmlNodeOrder;
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x00166E02 File Offset: 0x00165002
		protected XPathResultType GetXPathType(object value)
		{
			if (value is XPathNodeIterator)
			{
				return XPathResultType.NodeSet;
			}
			if (value is string)
			{
				return XPathResultType.String;
			}
			if (value is double)
			{
				return XPathResultType.Number;
			}
			if (value is bool)
			{
				return XPathResultType.Boolean;
			}
			return (XPathResultType)4;
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x00166E2D File Offset: 0x0016502D
		public virtual void PrintQuery(XmlWriter w)
		{
			w.WriteElementString(base.GetType().Name, string.Empty);
		}

		// Token: 0x04002E76 RID: 11894
		public const XPathResultType XPathResultType_Navigator = (XPathResultType)4;
	}
}
