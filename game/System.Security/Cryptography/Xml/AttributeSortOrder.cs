using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200001B RID: 27
	internal class AttributeSortOrder : IComparer
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00002145 File Offset: 0x00000345
		internal AttributeSortOrder()
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000032B0 File Offset: 0x000014B0
		public int Compare(object a, object b)
		{
			XmlNode xmlNode = a as XmlNode;
			XmlNode xmlNode2 = b as XmlNode;
			if (xmlNode == null || xmlNode2 == null)
			{
				throw new ArgumentException();
			}
			int num = string.CompareOrdinal(xmlNode.NamespaceURI, xmlNode2.NamespaceURI);
			if (num != 0)
			{
				return num;
			}
			return string.CompareOrdinal(xmlNode.LocalName, xmlNode2.LocalName);
		}
	}
}
