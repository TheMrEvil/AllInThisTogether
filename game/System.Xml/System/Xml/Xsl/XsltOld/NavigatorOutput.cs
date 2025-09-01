using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000394 RID: 916
	internal class NavigatorOutput : RecordOutput
	{
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002528 RID: 9512 RVA: 0x000E160C File Offset: 0x000DF80C
		internal XPathNavigator Navigator
		{
			get
			{
				return ((IXPathNavigable)this.doc).CreateNavigator();
			}
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000E1619 File Offset: 0x000DF819
		internal NavigatorOutput(string baseUri)
		{
			this.doc = new XPathDocument();
			this.wr = this.doc.LoadFromWriter(XPathDocument.LoadFlags.AtomizeNames, baseUri);
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000E1640 File Offset: 0x000DF840
		public Processor.OutputResult RecordDone(RecordBuilder record)
		{
			BuilderInfo mainNode = record.MainNode;
			this.documentIndex++;
			switch (mainNode.NodeType)
			{
			case XmlNodeType.Element:
				this.wr.WriteStartElement(mainNode.Prefix, mainNode.LocalName, mainNode.NamespaceURI);
				for (int i = 0; i < record.AttributeCount; i++)
				{
					this.documentIndex++;
					BuilderInfo builderInfo = (BuilderInfo)record.AttributeList[i];
					if (builderInfo.NamespaceURI == "http://www.w3.org/2000/xmlns/")
					{
						if (builderInfo.Prefix.Length == 0)
						{
							this.wr.WriteNamespaceDeclaration(string.Empty, builderInfo.Value);
						}
						else
						{
							this.wr.WriteNamespaceDeclaration(builderInfo.LocalName, builderInfo.Value);
						}
					}
					else
					{
						this.wr.WriteAttributeString(builderInfo.Prefix, builderInfo.LocalName, builderInfo.NamespaceURI, builderInfo.Value);
					}
				}
				this.wr.StartElementContent();
				if (mainNode.IsEmptyTag)
				{
					this.wr.WriteEndElement(mainNode.Prefix, mainNode.LocalName, mainNode.NamespaceURI);
				}
				break;
			case XmlNodeType.Text:
				this.wr.WriteString(mainNode.Value);
				break;
			case XmlNodeType.ProcessingInstruction:
				this.wr.WriteProcessingInstruction(mainNode.LocalName, mainNode.Value);
				break;
			case XmlNodeType.Comment:
				this.wr.WriteComment(mainNode.Value);
				break;
			case XmlNodeType.SignificantWhitespace:
				this.wr.WriteString(mainNode.Value);
				break;
			case XmlNodeType.EndElement:
				this.wr.WriteEndElement(mainNode.Prefix, mainNode.LocalName, mainNode.NamespaceURI);
				break;
			}
			record.Reset();
			return Processor.OutputResult.Continue;
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000E1824 File Offset: 0x000DFA24
		public void TheEnd()
		{
			this.wr.Close();
		}

		// Token: 0x04001D39 RID: 7481
		private XPathDocument doc;

		// Token: 0x04001D3A RID: 7482
		private int documentIndex;

		// Token: 0x04001D3B RID: 7483
		private XmlRawWriter wr;
	}
}
