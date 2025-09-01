using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020001A8 RID: 424
	internal sealed class DocumentXPathNodeIterator_AllElemChildren_AndSelf : DocumentXPathNodeIterator_AllElemChildren
	{
		// Token: 0x06000F80 RID: 3968 RVA: 0x00065693 File Offset: 0x00063893
		internal DocumentXPathNodeIterator_AllElemChildren_AndSelf(DocumentXPathNavigator nav) : base(nav)
		{
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0006569C File Offset: 0x0006389C
		internal DocumentXPathNodeIterator_AllElemChildren_AndSelf(DocumentXPathNodeIterator_AllElemChildren_AndSelf other) : base(other)
		{
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x000656A5 File Offset: 0x000638A5
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_AllElemChildren_AndSelf(this);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x000656B0 File Offset: 0x000638B0
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
