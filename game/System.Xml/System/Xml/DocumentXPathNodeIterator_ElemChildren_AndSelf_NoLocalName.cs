using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020001AA RID: 426
	internal sealed class DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName : DocumentXPathNodeIterator_ElemChildren_NoLocalName
	{
		// Token: 0x06000F88 RID: 3976 RVA: 0x0006573C File Offset: 0x0006393C
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName(DocumentXPathNavigator nav, string nsAtom) : base(nav, nsAtom)
		{
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x00065746 File Offset: 0x00063946
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName(DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName other) : base(other)
		{
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0006574F File Offset: 0x0006394F
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName(this);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x00065758 File Offset: 0x00063958
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
