using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000484 RID: 1156
	internal class XmlCachedSequenceWriter : XmlSequenceWriter
	{
		// Token: 0x06002D50 RID: 11600 RVA: 0x001094AF File Offset: 0x001076AF
		public XmlCachedSequenceWriter()
		{
			this.seqTyped = new XmlQueryItemSequence();
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06002D51 RID: 11601 RVA: 0x001094C2 File Offset: 0x001076C2
		public XmlQueryItemSequence ResultSequence
		{
			get
			{
				return this.seqTyped;
			}
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x001094CA File Offset: 0x001076CA
		public override XmlRawWriter StartTree(XPathNodeType rootType, IXmlNamespaceResolver nsResolver, XmlNameTable nameTable)
		{
			this.doc = new XPathDocument(nameTable);
			this.writer = this.doc.LoadFromWriter(XPathDocument.LoadFlags.AtomizeNames | ((rootType == XPathNodeType.Root) ? XPathDocument.LoadFlags.None : XPathDocument.LoadFlags.Fragment), string.Empty);
			this.writer.NamespaceResolver = nsResolver;
			return this.writer;
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x00109509 File Offset: 0x00107709
		public override void EndTree()
		{
			this.writer.Close();
			this.seqTyped.Add(this.doc.CreateNavigator());
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x0010952C File Offset: 0x0010772C
		public override void WriteItem(XPathItem item)
		{
			this.seqTyped.AddClone(item);
		}

		// Token: 0x04002321 RID: 8993
		private XmlQueryItemSequence seqTyped;

		// Token: 0x04002322 RID: 8994
		private XPathDocument doc;

		// Token: 0x04002323 RID: 8995
		private XmlRawWriter writer;
	}
}
