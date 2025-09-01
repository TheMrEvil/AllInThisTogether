using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000358 RID: 856
	internal class BeginEvent : Event
	{
		// Token: 0x0600234F RID: 9039 RVA: 0x000DB9F0 File Offset: 0x000D9BF0
		public BeginEvent(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			this.nodeType = input.NodeType;
			this.namespaceUri = input.NamespaceURI;
			this.name = input.LocalName;
			this.prefix = input.Prefix;
			this.empty = input.IsEmptyTag;
			if (this.nodeType == XPathNodeType.Element)
			{
				this.htmlProps = HtmlElementProps.GetProps(this.name);
				return;
			}
			if (this.nodeType == XPathNodeType.Attribute)
			{
				this.htmlProps = HtmlAttributeProps.GetProps(this.name);
			}
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x000DBA7C File Offset: 0x000D9C7C
		public override void ReplaceNamespaceAlias(Compiler compiler)
		{
			if (this.nodeType == XPathNodeType.Attribute && this.namespaceUri.Length == 0)
			{
				return;
			}
			NamespaceInfo namespaceInfo = compiler.FindNamespaceAlias(this.namespaceUri);
			if (namespaceInfo != null)
			{
				this.namespaceUri = namespaceInfo.nameSpace;
				if (namespaceInfo.prefix != null)
				{
					this.prefix = namespaceInfo.prefix;
				}
			}
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x000DBAD0 File Offset: 0x000D9CD0
		public override bool Output(Processor processor, ActionFrame frame)
		{
			return processor.BeginEvent(this.nodeType, this.prefix, this.name, this.namespaceUri, this.empty, this.htmlProps, false);
		}

		// Token: 0x04001C81 RID: 7297
		private XPathNodeType nodeType;

		// Token: 0x04001C82 RID: 7298
		private string namespaceUri;

		// Token: 0x04001C83 RID: 7299
		private string name;

		// Token: 0x04001C84 RID: 7300
		private string prefix;

		// Token: 0x04001C85 RID: 7301
		private bool empty;

		// Token: 0x04001C86 RID: 7302
		private object htmlProps;
	}
}
