using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003B1 RID: 945
	internal class TemplateLookupAction : Action
	{
		// Token: 0x060026A0 RID: 9888 RVA: 0x000E7E5D File Offset: 0x000E605D
		internal void Initialize(XmlQualifiedName mode, Stylesheet importsOf)
		{
			this.mode = mode;
			this.importsOf = importsOf;
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x000E7E70 File Offset: 0x000E6070
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			Action action;
			if (this.mode != null)
			{
				action = ((this.importsOf == null) ? processor.Stylesheet.FindTemplate(processor, frame.Node, this.mode) : this.importsOf.FindTemplateImports(processor, frame.Node, this.mode));
			}
			else
			{
				action = ((this.importsOf == null) ? processor.Stylesheet.FindTemplate(processor, frame.Node) : this.importsOf.FindTemplateImports(processor, frame.Node));
			}
			if (action == null)
			{
				action = this.BuiltInTemplate(frame.Node);
			}
			if (action != null)
			{
				frame.SetAction(action);
				return;
			}
			frame.Finished();
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x000E7F1C File Offset: 0x000E611C
		internal Action BuiltInTemplate(XPathNavigator node)
		{
			Action result = null;
			switch (node.NodeType)
			{
			case XPathNodeType.Root:
			case XPathNodeType.Element:
				result = ApplyTemplatesAction.BuiltInRule(this.mode);
				break;
			case XPathNodeType.Attribute:
			case XPathNodeType.Text:
			case XPathNodeType.SignificantWhitespace:
			case XPathNodeType.Whitespace:
				result = ValueOfAction.BuiltInRule();
				break;
			}
			return result;
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x000DC2C5 File Offset: 0x000DA4C5
		public TemplateLookupAction()
		{
		}

		// Token: 0x04001E62 RID: 7778
		protected XmlQualifiedName mode;

		// Token: 0x04001E63 RID: 7779
		protected Stylesheet importsOf;
	}
}
