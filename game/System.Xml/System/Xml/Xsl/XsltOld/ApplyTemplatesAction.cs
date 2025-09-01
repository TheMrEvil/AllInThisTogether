using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000353 RID: 851
	internal class ApplyTemplatesAction : ContainerAction
	{
		// Token: 0x06002333 RID: 9011 RVA: 0x000DB15A File Offset: 0x000D935A
		internal static ApplyTemplatesAction BuiltInRule()
		{
			return ApplyTemplatesAction.s_BuiltInRule;
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000DB161 File Offset: 0x000D9361
		internal static ApplyTemplatesAction BuiltInRule(XmlQualifiedName mode)
		{
			if (!(mode == null) && !mode.IsEmpty)
			{
				return new ApplyTemplatesAction(mode);
			}
			return ApplyTemplatesAction.BuiltInRule();
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x000DB180 File Offset: 0x000D9380
		internal ApplyTemplatesAction()
		{
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x000DB18F File Offset: 0x000D938F
		private ApplyTemplatesAction(XmlQualifiedName mode)
		{
			this.mode = mode;
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000DB1A5 File Offset: 0x000D93A5
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			this.CompileContent(compiler);
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x000DB1B8 File Offset: 0x000D93B8
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Select))
			{
				this.selectKey = compiler.AddQuery(value);
			}
			else
			{
				if (!Ref.Equal(localName, compiler.Atoms.Mode))
				{
					return false;
				}
				if (compiler.AllowBuiltInMode && value == "*")
				{
					this.mode = Compiler.BuiltInMode;
				}
				else
				{
					this.mode = compiler.CreateXPathQName(value);
				}
			}
			return true;
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x000DB248 File Offset: 0x000D9448
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
						if (!Ref.Equal(namespaceURI, input.Atoms.UriXsl))
						{
							goto IL_A7;
						}
						if (Ref.Equal(localName, input.Atoms.Sort))
						{
							base.AddAction(compiler.CreateSortAction());
						}
						else
						{
							if (!Ref.Equal(localName, input.Atoms.WithParam))
							{
								goto IL_A0;
							}
							WithParamAction withParamAction = compiler.CreateWithParamAction();
							base.CheckDuplicateParams(withParamAction.Name);
							base.AddAction(withParamAction);
						}
						compiler.PopScope();
					}
					if (!compiler.Advance())
					{
						goto Block_6;
					}
				}
				throw XsltException.Create("The contents of '{0}' are invalid.", new string[]
				{
					"apply-templates"
				});
				IL_A0:
				throw compiler.UnexpectedKeyword();
				IL_A7:
				throw compiler.UnexpectedKeyword();
				Block_6:
				compiler.ToParent();
			}
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x000DB338 File Offset: 0x000D9538
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			switch (frame.State)
			{
			case 0:
				processor.ResetParams();
				processor.InitSortArray();
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
				goto IL_C2;
			case 4:
				goto IL_DB;
			case 5:
				frame.State = 3;
				goto IL_C2;
			default:
				return;
			}
			if (this.selectKey == -1)
			{
				if (!frame.Node.HasChildren)
				{
					frame.Finished();
					return;
				}
				frame.InitNewNodeSet(frame.Node.SelectChildren(XPathNodeType.All));
			}
			else
			{
				frame.InitNewNodeSet(processor.StartQuery(frame.NodeSet, this.selectKey));
			}
			if (processor.SortArray.Count != 0)
			{
				frame.SortNewNodeSet(processor, processor.SortArray);
			}
			frame.State = 3;
			IL_C2:
			if (!frame.NewNextNode(processor))
			{
				frame.Finished();
				return;
			}
			frame.State = 4;
			IL_DB:
			processor.PushTemplateLookup(frame.NewNodeSet, this.mode, null);
			frame.State = 5;
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000DB444 File Offset: 0x000D9644
		// Note: this type is marked as 'beforefieldinit'.
		static ApplyTemplatesAction()
		{
		}

		// Token: 0x04001C6F RID: 7279
		private const int ProcessedChildren = 2;

		// Token: 0x04001C70 RID: 7280
		private const int ProcessNextNode = 3;

		// Token: 0x04001C71 RID: 7281
		private const int PositionAdvanced = 4;

		// Token: 0x04001C72 RID: 7282
		private const int TemplateProcessed = 5;

		// Token: 0x04001C73 RID: 7283
		private int selectKey = -1;

		// Token: 0x04001C74 RID: 7284
		private XmlQualifiedName mode;

		// Token: 0x04001C75 RID: 7285
		private static ApplyTemplatesAction s_BuiltInRule = new ApplyTemplatesAction();
	}
}
