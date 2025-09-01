using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000048 RID: 72
	internal class MyXmlDocument : XmlDocument
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x00008D9C File Offset: 0x00006F9C
		protected override XmlAttribute CreateDefaultAttribute(string prefix, string localName, string namespaceURI)
		{
			return this.CreateAttribute(prefix, localName, namespaceURI);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00008DA7 File Offset: 0x00006FA7
		public MyXmlDocument()
		{
		}
	}
}
