using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020001A9 RID: 425
	internal class DocumentXPathNodeIterator_ElemChildren_NoLocalName : DocumentXPathNodeIterator_ElemDescendants
	{
		// Token: 0x06000F84 RID: 3972 RVA: 0x000656FC File Offset: 0x000638FC
		internal DocumentXPathNodeIterator_ElemChildren_NoLocalName(DocumentXPathNavigator nav, string nsAtom) : base(nav)
		{
			this.nsAtom = nsAtom;
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0006570C File Offset: 0x0006390C
		internal DocumentXPathNodeIterator_ElemChildren_NoLocalName(DocumentXPathNodeIterator_ElemChildren_NoLocalName other) : base(other)
		{
			this.nsAtom = other.nsAtom;
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00065721 File Offset: 0x00063921
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren_NoLocalName(this);
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00065729 File Offset: 0x00063929
		protected override bool Match(XmlNode node)
		{
			return Ref.Equal(node.NamespaceURI, this.nsAtom);
		}

		// Token: 0x04000FFF RID: 4095
		private string nsAtom;
	}
}
