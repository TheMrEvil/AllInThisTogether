using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020001A7 RID: 423
	internal class DocumentXPathNodeIterator_AllElemChildren : DocumentXPathNodeIterator_ElemDescendants
	{
		// Token: 0x06000F7C RID: 3964 RVA: 0x0006566E File Offset: 0x0006386E
		internal DocumentXPathNodeIterator_AllElemChildren(DocumentXPathNavigator nav) : base(nav)
		{
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00065677 File Offset: 0x00063877
		internal DocumentXPathNodeIterator_AllElemChildren(DocumentXPathNodeIterator_AllElemChildren other) : base(other)
		{
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00065680 File Offset: 0x00063880
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_AllElemChildren(this);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00065688 File Offset: 0x00063888
		protected override bool Match(XmlNode node)
		{
			return node.NodeType == XmlNodeType.Element;
		}
	}
}
