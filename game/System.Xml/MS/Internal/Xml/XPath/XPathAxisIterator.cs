using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200065A RID: 1626
	internal abstract class XPathAxisIterator : XPathNodeIterator
	{
		// Token: 0x060041E1 RID: 16865 RVA: 0x00168A6B File Offset: 0x00166C6B
		public XPathAxisIterator(XPathNavigator nav, bool matchSelf)
		{
			this.nav = nav;
			this.matchSelf = matchSelf;
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x00168A88 File Offset: 0x00166C88
		public XPathAxisIterator(XPathNavigator nav, XPathNodeType type, bool matchSelf) : this(nav, matchSelf)
		{
			this.type = type;
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x00168A99 File Offset: 0x00166C99
		public XPathAxisIterator(XPathNavigator nav, string name, string namespaceURI, bool matchSelf) : this(nav, matchSelf)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			this.name = name;
			this.uri = namespaceURI;
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x00168AD0 File Offset: 0x00166CD0
		public XPathAxisIterator(XPathAxisIterator it)
		{
			this.nav = it.nav.Clone();
			this.type = it.type;
			this.name = it.name;
			this.uri = it.uri;
			this.position = it.position;
			this.matchSelf = it.matchSelf;
			this.first = it.first;
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x060041E5 RID: 16869 RVA: 0x00168B43 File Offset: 0x00166D43
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x060041E6 RID: 16870 RVA: 0x00168B4B File Offset: 0x00166D4B
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x060041E7 RID: 16871 RVA: 0x00168B54 File Offset: 0x00166D54
		protected virtual bool Matches
		{
			get
			{
				if (this.name == null)
				{
					return this.type == this.nav.NodeType || this.type == XPathNodeType.All || (this.type == XPathNodeType.Text && (this.nav.NodeType == XPathNodeType.Whitespace || this.nav.NodeType == XPathNodeType.SignificantWhitespace));
				}
				return this.nav.NodeType == XPathNodeType.Element && (this.name.Length == 0 || this.name == this.nav.LocalName) && this.uri == this.nav.NamespaceURI;
			}
		}

		// Token: 0x04002EA4 RID: 11940
		internal XPathNavigator nav;

		// Token: 0x04002EA5 RID: 11941
		internal XPathNodeType type;

		// Token: 0x04002EA6 RID: 11942
		internal string name;

		// Token: 0x04002EA7 RID: 11943
		internal string uri;

		// Token: 0x04002EA8 RID: 11944
		internal int position;

		// Token: 0x04002EA9 RID: 11945
		internal bool matchSelf;

		// Token: 0x04002EAA RID: 11946
		internal bool first = true;
	}
}
