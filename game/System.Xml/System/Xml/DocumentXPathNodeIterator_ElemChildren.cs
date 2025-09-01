using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020001AB RID: 427
	internal class DocumentXPathNodeIterator_ElemChildren : DocumentXPathNodeIterator_ElemDescendants
	{
		// Token: 0x06000F8C RID: 3980 RVA: 0x000657A4 File Offset: 0x000639A4
		internal DocumentXPathNodeIterator_ElemChildren(DocumentXPathNavigator nav, string localNameAtom, string nsAtom) : base(nav)
		{
			this.localNameAtom = localNameAtom;
			this.nsAtom = nsAtom;
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x000657BB File Offset: 0x000639BB
		internal DocumentXPathNodeIterator_ElemChildren(DocumentXPathNodeIterator_ElemChildren other) : base(other)
		{
			this.localNameAtom = other.localNameAtom;
			this.nsAtom = other.nsAtom;
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x000657DC File Offset: 0x000639DC
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren(this);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x000657E4 File Offset: 0x000639E4
		protected override bool Match(XmlNode node)
		{
			return Ref.Equal(node.LocalName, this.localNameAtom) && Ref.Equal(node.NamespaceURI, this.nsAtom);
		}

		// Token: 0x04001000 RID: 4096
		protected string localNameAtom;

		// Token: 0x04001001 RID: 4097
		protected string nsAtom;
	}
}
