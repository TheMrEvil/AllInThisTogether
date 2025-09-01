using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000483 RID: 1155
	internal abstract class XmlSequenceWriter
	{
		// Token: 0x06002D4C RID: 11596
		public abstract XmlRawWriter StartTree(XPathNodeType rootType, IXmlNamespaceResolver nsResolver, XmlNameTable nameTable);

		// Token: 0x06002D4D RID: 11597
		public abstract void EndTree();

		// Token: 0x06002D4E RID: 11598
		public abstract void WriteItem(XPathItem item);

		// Token: 0x06002D4F RID: 11599 RVA: 0x0000216B File Offset: 0x0000036B
		protected XmlSequenceWriter()
		{
		}
	}
}
