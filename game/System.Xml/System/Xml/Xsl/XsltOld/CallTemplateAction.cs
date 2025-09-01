using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200035A RID: 858
	internal class CallTemplateAction : ContainerAction
	{
		// Token: 0x06002367 RID: 9063 RVA: 0x000DBE11 File Offset: 0x000DA011
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckRequiredAttribute(compiler, this.name, "name");
			this.CompileContent(compiler);
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x000DBE34 File Offset: 0x000DA034
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Name))
			{
				this.name = compiler.CreateXPathQName(value);
				return true;
			}
			return false;
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000DBE7C File Offset: 0x000DA07C
		private void CompileContent(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			if (compiler.Recurse())
			{
				for (;;)
				{
					XPathNodeType nodeType = input.NodeType;
					if (nodeType != XPathNodeType.Element)
					{
						if (nodeType - XPathNodeType.SignificantWhitespace > 3)
						{
							break;
						}
					}
					else
					{
						compiler.PushNamespaceScope();
						string namespaceURI = input.NamespaceURI;
						string localName = input.LocalName;
						if (!Ref.Equal(namespaceURI, input.Atoms.UriXsl) || !Ref.Equal(localName, input.Atoms.WithParam))
						{
							goto IL_79;
						}
						WithParamAction withParamAction = compiler.CreateWithParamAction();
						base.CheckDuplicateParams(withParamAction.Name);
						base.AddAction(withParamAction);
						compiler.PopScope();
					}
					if (!compiler.Advance())
					{
						goto Block_5;
					}
				}
				throw XsltException.Create("The contents of '{0}' are invalid.", new string[]
				{
					"call-template"
				});
				IL_79:
				throw compiler.UnexpectedKeyword();
				Block_5:
				compiler.ToParent();
			}
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x000DBF3C File Offset: 0x000DA13C
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			switch (frame.State)
			{
			case 0:
				processor.ResetParams();
				if (this.containedActions != null && this.containedActions.Count > 0)
				{
					processor.PushActionFrame(frame);
					frame.State = 2;
					return;
				}
				break;
			case 1:
				return;
			case 2:
				break;
			case 3:
				frame.Finished();
				return;
			default:
				return;
			}
			TemplateAction templateAction = processor.Stylesheet.FindTemplate(this.name);
			if (templateAction != null)
			{
				frame.State = 3;
				processor.PushActionFrame(templateAction, frame.NodeSet);
				return;
			}
			throw XsltException.Create("The named template '{0}' does not exist.", new string[]
			{
				this.name.ToString()
			});
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x000DB75C File Offset: 0x000D995C
		public CallTemplateAction()
		{
		}

		// Token: 0x04001C93 RID: 7315
		private const int ProcessedChildren = 2;

		// Token: 0x04001C94 RID: 7316
		private const int ProcessedTemplate = 3;

		// Token: 0x04001C95 RID: 7317
		private XmlQualifiedName name;
	}
}
