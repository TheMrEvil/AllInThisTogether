using System;
using System.Collections;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003C0 RID: 960
	internal class WriterOutput : RecordOutput
	{
		// Token: 0x060026E3 RID: 9955 RVA: 0x000E8AB5 File Offset: 0x000E6CB5
		internal WriterOutput(Processor processor, XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
			this.processor = processor;
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x000E8ADC File Offset: 0x000E6CDC
		public Processor.OutputResult RecordDone(RecordBuilder record)
		{
			BuilderInfo mainNode = record.MainNode;
			switch (mainNode.NodeType)
			{
			case XmlNodeType.Element:
				this.writer.WriteStartElement(mainNode.Prefix, mainNode.LocalName, mainNode.NamespaceURI);
				this.WriteAttributes(record.AttributeList, record.AttributeCount);
				if (mainNode.IsEmptyTag)
				{
					this.writer.WriteEndElement();
				}
				break;
			case XmlNodeType.Text:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.writer.WriteString(mainNode.Value);
				break;
			case XmlNodeType.CDATA:
				this.writer.WriteCData(mainNode.Value);
				break;
			case XmlNodeType.EntityReference:
				this.writer.WriteEntityRef(mainNode.LocalName);
				break;
			case XmlNodeType.ProcessingInstruction:
				this.writer.WriteProcessingInstruction(mainNode.LocalName, mainNode.Value);
				break;
			case XmlNodeType.Comment:
				this.writer.WriteComment(mainNode.Value);
				break;
			case XmlNodeType.DocumentType:
				this.writer.WriteRaw(mainNode.Value);
				break;
			case XmlNodeType.EndElement:
				this.writer.WriteFullEndElement();
				break;
			}
			record.Reset();
			return Processor.OutputResult.Continue;
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000E8C1A File Offset: 0x000E6E1A
		public void TheEnd()
		{
			this.writer.Flush();
			this.writer = null;
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x000E8C30 File Offset: 0x000E6E30
		private void WriteAttributes(ArrayList list, int count)
		{
			for (int i = 0; i < count; i++)
			{
				BuilderInfo builderInfo = (BuilderInfo)list[i];
				this.writer.WriteAttributeString(builderInfo.Prefix, builderInfo.LocalName, builderInfo.NamespaceURI, builderInfo.Value);
			}
		}

		// Token: 0x04001E87 RID: 7815
		private XmlWriter writer;

		// Token: 0x04001E88 RID: 7816
		private Processor processor;
	}
}
