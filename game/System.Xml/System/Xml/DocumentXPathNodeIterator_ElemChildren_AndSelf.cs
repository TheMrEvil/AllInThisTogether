using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020001AC RID: 428
	internal sealed class DocumentXPathNodeIterator_ElemChildren_AndSelf : DocumentXPathNodeIterator_ElemChildren
	{
		// Token: 0x06000F90 RID: 3984 RVA: 0x0006580C File Offset: 0x00063A0C
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf(DocumentXPathNavigator nav, string localNameAtom, string nsAtom) : base(nav, localNameAtom, nsAtom)
		{
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00065817 File Offset: 0x00063A17
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf(DocumentXPathNodeIterator_ElemChildren_AndSelf other) : base(other)
		{
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00065820 File Offset: 0x00063A20
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren_AndSelf(this);
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00065828 File Offset: 0x00063A28
		public override bool MoveNext()
		{
			if (this.CurrentPosition == 0)
			{
				XmlNode xmlNode = (XmlNode)((DocumentXPathNavigator)this.Current).UnderlyingObject;
				if (xmlNode.NodeType == XmlNodeType.Element && this.Match(xmlNode))
				{
					base.SetPosition(1);
					return true;
				}
			}
			return base.MoveNext();
		}
	}
}
