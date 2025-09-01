using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020002A5 RID: 677
	internal class XmlAttributeComparer : IComparer
	{
		// Token: 0x0600197C RID: 6524 RVA: 0x00091C04 File Offset: 0x0008FE04
		public int Compare(object o1, object o2)
		{
			XmlAttribute xmlAttribute = (XmlAttribute)o1;
			XmlAttribute xmlAttribute2 = (XmlAttribute)o2;
			int num = string.Compare(xmlAttribute.NamespaceURI, xmlAttribute2.NamespaceURI, StringComparison.Ordinal);
			if (num == 0)
			{
				return string.Compare(xmlAttribute.Name, xmlAttribute2.Name, StringComparison.Ordinal);
			}
			return num;
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0000216B File Offset: 0x0000036B
		public XmlAttributeComparer()
		{
		}
	}
}
