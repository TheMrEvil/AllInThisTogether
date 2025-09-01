using System;
using System.Diagnostics;

namespace System.Xml
{
	// Token: 0x020001D1 RID: 465
	[DebuggerDisplay("{ToString()}")]
	internal struct DebuggerDisplayXmlNodeProxy
	{
		// Token: 0x06001236 RID: 4662 RVA: 0x0006E327 File Offset: 0x0006C527
		public DebuggerDisplayXmlNodeProxy(XmlNode node)
		{
			this.node = node;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0006E330 File Offset: 0x0006C530
		public override string ToString()
		{
			XmlNodeType nodeType = this.node.NodeType;
			string text = nodeType.ToString();
			switch (nodeType)
			{
			case XmlNodeType.Element:
			case XmlNodeType.EntityReference:
				text = text + ", Name=\"" + this.node.Name + "\"";
				break;
			case XmlNodeType.Attribute:
			case XmlNodeType.ProcessingInstruction:
				text = string.Concat(new string[]
				{
					text,
					", Name=\"",
					this.node.Name,
					"\", Value=\"",
					XmlConvert.EscapeValueForDebuggerDisplay(this.node.Value),
					"\""
				});
				break;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Comment:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
			case XmlNodeType.XmlDeclaration:
				text = text + ", Value=\"" + XmlConvert.EscapeValueForDebuggerDisplay(this.node.Value) + "\"";
				break;
			case XmlNodeType.DocumentType:
			{
				XmlDocumentType xmlDocumentType = (XmlDocumentType)this.node;
				text = string.Concat(new string[]
				{
					text,
					", Name=\"",
					xmlDocumentType.Name,
					"\", SYSTEM=\"",
					xmlDocumentType.SystemId,
					"\", PUBLIC=\"",
					xmlDocumentType.PublicId,
					"\", Value=\"",
					XmlConvert.EscapeValueForDebuggerDisplay(xmlDocumentType.InternalSubset),
					"\""
				});
				break;
			}
			}
			return text;
		}

		// Token: 0x040010B8 RID: 4280
		private XmlNode node;
	}
}
