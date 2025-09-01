using System;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000410 RID: 1040
	internal class XslAstAnalyzer : XslVisitor<XslFlags>
	{
		// Token: 0x060028F0 RID: 10480 RVA: 0x000F54C4 File Offset: 0x000F36C4
		public XslFlags Analyze(Compiler compiler)
		{
			this.compiler = compiler;
			this.scope = new CompilerScopeManager<VarPar>();
			this.xpathAnalyzer = new XslAstAnalyzer.XPathAnalyzer(compiler, this.scope);
			foreach (VarPar varPar in compiler.ExternalPars)
			{
				this.scope.AddVariable(varPar.Name, varPar);
			}
			foreach (VarPar varPar2 in compiler.GlobalVars)
			{
				this.scope.AddVariable(varPar2.Name, varPar2);
			}
			foreach (VarPar varPar3 in compiler.ExternalPars)
			{
				this.Visit(varPar3);
				varPar3.Flags |= XslFlags.TypeFilter;
			}
			foreach (VarPar node in compiler.GlobalVars)
			{
				this.Visit(node);
			}
			XslFlags xslFlags = XslFlags.None;
			foreach (ProtoTemplate node2 in compiler.AllTemplates)
			{
				this.currentTemplate = node2;
				xslFlags |= this.Visit(node2);
			}
			foreach (ProtoTemplate protoTemplate in compiler.AllTemplates)
			{
				foreach (XslNode xslNode in protoTemplate.Content)
				{
					if (xslNode.NodeType != XslNodeType.Text)
					{
						if (xslNode.NodeType != XslNodeType.Param)
						{
							break;
						}
						VarPar varPar4 = (VarPar)xslNode;
						if ((varPar4.Flags & XslFlags.MayBeDefault) != XslFlags.None)
						{
							varPar4.Flags |= varPar4.DefValueFlags;
						}
					}
				}
			}
			for (int num = 32; num != 0; num >>= 1)
			{
				this.dataFlow.PropagateFlag((XslFlags)num);
			}
			this.dataFlow = null;
			foreach (KeyValuePair<Template, Stylesheet> keyValuePair in this.fwdApplyImportsGraph)
			{
				foreach (Stylesheet sheet in keyValuePair.Value.Imports)
				{
					this.AddImportDependencies(sheet, keyValuePair.Key);
				}
			}
			this.fwdApplyImportsGraph = null;
			if ((xslFlags & XslFlags.Current) != XslFlags.None)
			{
				this.revCall0Graph.PropagateFlag(XslFlags.Current);
			}
			if ((xslFlags & XslFlags.Position) != XslFlags.None)
			{
				this.revCall0Graph.PropagateFlag(XslFlags.Position);
			}
			if ((xslFlags & XslFlags.Last) != XslFlags.None)
			{
				this.revCall0Graph.PropagateFlag(XslFlags.Last);
			}
			if ((xslFlags & XslFlags.SideEffects) != XslFlags.None)
			{
				this.PropagateSideEffectsFlag();
			}
			this.revCall0Graph = null;
			this.revCall1Graph = null;
			this.revApplyTemplatesGraph = null;
			this.FillModeFlags(compiler.Root.ModeFlags, compiler.Root.Imports[0]);
			this.TraceResults();
			return xslFlags;
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x000F5870 File Offset: 0x000F3A70
		private void AddImportDependencies(Stylesheet sheet, Template focusDonor)
		{
			foreach (Template template in sheet.Templates)
			{
				if (template.Mode.Equals(focusDonor.Mode))
				{
					this.revCall0Graph.AddEdge(template, focusDonor);
				}
			}
			foreach (Stylesheet sheet2 in sheet.Imports)
			{
				this.AddImportDependencies(sheet2, focusDonor);
			}
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x000F5900 File Offset: 0x000F3B00
		private void FillModeFlags(Dictionary<QilName, XslFlags> parentModeFlags, Stylesheet sheet)
		{
			foreach (Stylesheet sheet2 in sheet.Imports)
			{
				this.FillModeFlags(sheet.ModeFlags, sheet2);
			}
			foreach (KeyValuePair<QilName, XslFlags> keyValuePair in sheet.ModeFlags)
			{
				XslFlags xslFlags;
				if (!parentModeFlags.TryGetValue(keyValuePair.Key, out xslFlags))
				{
					xslFlags = XslFlags.None;
				}
				parentModeFlags[keyValuePair.Key] = (xslFlags | keyValuePair.Value);
			}
			foreach (Template template in sheet.Templates)
			{
				XslFlags xslFlags2 = template.Flags & (XslFlags.Current | XslFlags.Position | XslFlags.Last | XslFlags.SideEffects);
				if (xslFlags2 != XslFlags.None)
				{
					XslFlags xslFlags3;
					if (!parentModeFlags.TryGetValue(template.Mode, out xslFlags3))
					{
						xslFlags3 = XslFlags.None;
					}
					parentModeFlags[template.Mode] = (xslFlags3 | xslFlags2);
				}
			}
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x0000B528 File Offset: 0x00009728
		private void TraceResults()
		{
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000F5A14 File Offset: 0x000F3C14
		protected override XslFlags Visit(XslNode node)
		{
			this.scope.EnterScope(node.Namespaces);
			XslFlags result = base.Visit(node);
			this.scope.ExitScope();
			if (this.currentTemplate != null && (node.NodeType == XslNodeType.Variable || node.NodeType == XslNodeType.Param))
			{
				this.scope.AddVariable(node.Name, (VarPar)node);
			}
			return result;
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x000F5A78 File Offset: 0x000F3C78
		protected override XslFlags VisitChildren(XslNode node)
		{
			XslFlags xslFlags = XslFlags.None;
			foreach (XslNode node2 in node.Content)
			{
				xslFlags |= this.Visit(node2);
			}
			return xslFlags;
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x000F5ACC File Offset: 0x000F3CCC
		protected override XslFlags VisitAttributeSet(AttributeSet node)
		{
			node.Flags = this.VisitChildren(node);
			return node.Flags;
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000F5ACC File Offset: 0x000F3CCC
		protected override XslFlags VisitTemplate(Template node)
		{
			node.Flags = this.VisitChildren(node);
			return node.Flags;
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x000F5AE1 File Offset: 0x000F3CE1
		protected override XslFlags VisitApplyImports(XslNode node)
		{
			this.fwdApplyImportsGraph[(Template)this.currentTemplate] = (Stylesheet)node.Arg;
			return XslFlags.Rtf | XslFlags.Current | XslFlags.HasCalls;
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000F5B0C File Offset: 0x000F3D0C
		protected override XslFlags VisitApplyTemplates(XslNode node)
		{
			XslFlags xslFlags = this.ProcessExpr(node.Select);
			foreach (XslNode xslNode in node.Content)
			{
				xslFlags |= this.Visit(xslNode);
				if (xslNode.NodeType == XslNodeType.WithParam)
				{
					XslAstAnalyzer.ModeName key = new XslAstAnalyzer.ModeName(node.Name, xslNode.Name);
					VarPar varPar;
					if (!this.applyTemplatesParams.TryGetValue(key, out varPar))
					{
						varPar = (this.applyTemplatesParams[key] = AstFactory.WithParam(xslNode.Name));
					}
					if (this.typeDonor != null)
					{
						this.dataFlow.AddEdge(this.typeDonor, varPar);
					}
					else
					{
						varPar.Flags |= (xslNode.Flags & XslFlags.TypeFilter);
					}
				}
			}
			if (this.currentTemplate != null)
			{
				this.AddApplyTemplatesEdge(node.Name, this.currentTemplate);
			}
			return XslFlags.Rtf | XslFlags.HasCalls | xslFlags;
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x000F5C10 File Offset: 0x000F3E10
		protected override XslFlags VisitAttribute(NodeCtor node)
		{
			return XslFlags.Rtf | this.ProcessAvt(node.NameAvt) | this.ProcessAvt(node.NsAvt) | this.VisitChildren(node);
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x000F5C38 File Offset: 0x000F3E38
		protected override XslFlags VisitCallTemplate(XslNode node)
		{
			XslFlags xslFlags = XslFlags.None;
			Template template;
			if (this.compiler.NamedTemplates.TryGetValue(node.Name, out template) && this.currentTemplate != null)
			{
				if (this.forEachDepth == 0)
				{
					this.revCall0Graph.AddEdge(template, this.currentTemplate);
				}
				else
				{
					this.revCall1Graph.AddEdge(template, this.currentTemplate);
				}
			}
			VarPar[] array = new VarPar[node.Content.Count];
			int num = 0;
			foreach (XslNode node2 in node.Content)
			{
				xslFlags |= this.Visit(node2);
				array[num++] = this.typeDonor;
			}
			if (template != null)
			{
				foreach (XslNode xslNode in template.Content)
				{
					if (xslNode.NodeType != XslNodeType.Text)
					{
						if (xslNode.NodeType != XslNodeType.Param)
						{
							break;
						}
						VarPar varPar = (VarPar)xslNode;
						VarPar varPar2 = null;
						num = 0;
						foreach (XslNode xslNode2 in node.Content)
						{
							if (xslNode2.Name.Equals(varPar.Name))
							{
								varPar2 = (VarPar)xslNode2;
								this.typeDonor = array[num];
								break;
							}
							num++;
						}
						if (varPar2 != null)
						{
							if (this.typeDonor != null)
							{
								this.dataFlow.AddEdge(this.typeDonor, varPar);
							}
							else
							{
								varPar.Flags |= (varPar2.Flags & XslFlags.TypeFilter);
							}
						}
						else
						{
							varPar.Flags |= XslFlags.MayBeDefault;
						}
					}
				}
			}
			return XslFlags.Rtf | XslFlags.HasCalls | xslFlags;
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x000F5E34 File Offset: 0x000F4034
		protected override XslFlags VisitComment(XslNode node)
		{
			return XslFlags.Rtf | this.VisitChildren(node);
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x000F5E40 File Offset: 0x000F4040
		protected override XslFlags VisitCopy(XslNode node)
		{
			return XslFlags.Rtf | XslFlags.Current | this.VisitChildren(node);
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x000F5E4F File Offset: 0x000F404F
		protected override XslFlags VisitCopyOf(XslNode node)
		{
			return XslFlags.Rtf | this.ProcessExpr(node.Select);
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x000F5C10 File Offset: 0x000F3E10
		protected override XslFlags VisitElement(NodeCtor node)
		{
			return XslFlags.Rtf | this.ProcessAvt(node.NameAvt) | this.ProcessAvt(node.NsAvt) | this.VisitChildren(node);
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x000F5E60 File Offset: 0x000F4060
		protected override XslFlags VisitError(XslNode node)
		{
			return (this.VisitChildren(node) & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf)) | XslFlags.SideEffects;
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x000F5E74 File Offset: 0x000F4074
		protected override XslFlags VisitForEach(XslNode node)
		{
			XslFlags xslFlags = this.ProcessExpr(node.Select);
			this.forEachDepth++;
			foreach (XslNode xslNode in node.Content)
			{
				if (xslNode.NodeType == XslNodeType.Sort)
				{
					xslFlags |= this.Visit(xslNode);
				}
				else
				{
					xslFlags |= (this.Visit(xslNode) & ~(XslFlags.Current | XslFlags.Position | XslFlags.Last));
				}
			}
			this.forEachDepth--;
			return xslFlags;
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x000F5F0C File Offset: 0x000F410C
		protected override XslFlags VisitIf(XslNode node)
		{
			return this.ProcessExpr(node.Select) | this.VisitChildren(node);
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x000F5F22 File Offset: 0x000F4122
		protected override XslFlags VisitLiteralAttribute(XslNode node)
		{
			return XslFlags.Rtf | this.ProcessAvt(node.Select) | this.VisitChildren(node);
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x000F5E34 File Offset: 0x000F4034
		protected override XslFlags VisitLiteralElement(XslNode node)
		{
			return XslFlags.Rtf | this.VisitChildren(node);
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x000F5E60 File Offset: 0x000F4060
		protected override XslFlags VisitMessage(XslNode node)
		{
			return (this.VisitChildren(node) & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf)) | XslFlags.SideEffects;
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x000F5F3C File Offset: 0x000F413C
		protected override XslFlags VisitNumber(Number node)
		{
			return XslFlags.Rtf | this.ProcessPattern(node.Count) | this.ProcessPattern(node.From) | ((node.Value != null) ? this.ProcessExpr(node.Value) : XslFlags.Current) | this.ProcessAvt(node.Format) | this.ProcessAvt(node.Lang) | this.ProcessAvt(node.LetterValue) | this.ProcessAvt(node.GroupingSeparator) | this.ProcessAvt(node.GroupingSize);
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x000F5F22 File Offset: 0x000F4122
		protected override XslFlags VisitPI(XslNode node)
		{
			return XslFlags.Rtf | this.ProcessAvt(node.Select) | this.VisitChildren(node);
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x000F5FC4 File Offset: 0x000F41C4
		protected override XslFlags VisitSort(Sort node)
		{
			return (this.ProcessExpr(node.Select) & ~(XslFlags.Current | XslFlags.Position | XslFlags.Last)) | this.ProcessAvt(node.Lang) | this.ProcessAvt(node.DataType) | this.ProcessAvt(node.Order) | this.ProcessAvt(node.CaseOrder);
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x000F5E34 File Offset: 0x000F4034
		protected override XslFlags VisitText(Text node)
		{
			return XslFlags.Rtf | this.VisitChildren(node);
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x000F6018 File Offset: 0x000F4218
		protected override XslFlags VisitUseAttributeSet(XslNode node)
		{
			AttributeSet v;
			if (this.compiler.AttributeSets.TryGetValue(node.Name, out v) && this.currentTemplate != null)
			{
				if (this.forEachDepth == 0)
				{
					this.revCall0Graph.AddEdge(v, this.currentTemplate);
				}
				else
				{
					this.revCall1Graph.AddEdge(v, this.currentTemplate);
				}
			}
			return XslFlags.Rtf | XslFlags.HasCalls;
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x000F5E4F File Offset: 0x000F404F
		protected override XslFlags VisitValueOf(XslNode node)
		{
			return XslFlags.Rtf | this.ProcessExpr(node.Select);
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x000F5E4F File Offset: 0x000F404F
		protected override XslFlags VisitValueOfDoe(XslNode node)
		{
			return XslFlags.Rtf | this.ProcessExpr(node.Select);
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x000F607C File Offset: 0x000F427C
		protected override XslFlags VisitParam(VarPar node)
		{
			Template template = this.currentTemplate as Template;
			if (template != null && template.Match != null)
			{
				node.Flags |= XslFlags.MayBeDefault;
				XslAstAnalyzer.ModeName key = new XslAstAnalyzer.ModeName(template.Mode, node.Name);
				VarPar v;
				if (!this.applyTemplatesParams.TryGetValue(key, out v))
				{
					v = (this.applyTemplatesParams[key] = AstFactory.WithParam(node.Name));
				}
				this.dataFlow.AddEdge(v, node);
			}
			node.DefValueFlags = this.ProcessVarPar(node);
			return node.DefValueFlags & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf);
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x000F6113 File Offset: 0x000F4313
		protected override XslFlags VisitVariable(VarPar node)
		{
			node.Flags = this.ProcessVarPar(node);
			return node.Flags & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf);
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x000F6113 File Offset: 0x000F4313
		protected override XslFlags VisitWithParam(VarPar node)
		{
			node.Flags = this.ProcessVarPar(node);
			return node.Flags & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf);
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x000F612C File Offset: 0x000F432C
		private XslFlags ProcessVarPar(VarPar node)
		{
			XslFlags result;
			if (node.Select != null)
			{
				if (node.Content.Count != 0)
				{
					result = (this.xpathAnalyzer.Analyze(node.Select) | this.VisitChildren(node) | XslFlags.TypeFilter);
					this.typeDonor = null;
				}
				else
				{
					result = this.xpathAnalyzer.Analyze(node.Select);
					this.typeDonor = this.xpathAnalyzer.TypeDonor;
					if (this.typeDonor != null && node.NodeType != XslNodeType.WithParam)
					{
						this.dataFlow.AddEdge(this.typeDonor, node);
					}
				}
			}
			else if (node.Content.Count != 0)
			{
				result = (XslFlags.Rtf | this.VisitChildren(node));
				this.typeDonor = null;
			}
			else
			{
				result = XslFlags.String;
				this.typeDonor = null;
			}
			return result;
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x000F61E8 File Offset: 0x000F43E8
		private XslFlags ProcessExpr(string expr)
		{
			return this.xpathAnalyzer.Analyze(expr) & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf);
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x000F61F9 File Offset: 0x000F43F9
		private XslFlags ProcessAvt(string avt)
		{
			return this.xpathAnalyzer.AnalyzeAvt(avt) & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf);
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x000F620A File Offset: 0x000F440A
		private XslFlags ProcessPattern(string pattern)
		{
			return this.xpathAnalyzer.Analyze(pattern) & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf) & ~(XslFlags.Current | XslFlags.Position | XslFlags.Last);
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x000F6224 File Offset: 0x000F4424
		private void AddApplyTemplatesEdge(QilName mode, ProtoTemplate dependentTemplate)
		{
			List<ProtoTemplate> list;
			if (!this.revApplyTemplatesGraph.TryGetValue(mode, out list))
			{
				list = new List<ProtoTemplate>();
				this.revApplyTemplatesGraph.Add(mode, list);
			}
			else if (list[list.Count - 1] == dependentTemplate)
			{
				return;
			}
			list.Add(dependentTemplate);
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x000F6270 File Offset: 0x000F4470
		private void PropagateSideEffectsFlag()
		{
			foreach (ProtoTemplate protoTemplate in this.revCall0Graph.Keys)
			{
				protoTemplate.Flags &= ~XslFlags.Stop;
			}
			foreach (ProtoTemplate protoTemplate2 in this.revCall1Graph.Keys)
			{
				protoTemplate2.Flags &= ~XslFlags.Stop;
			}
			foreach (ProtoTemplate protoTemplate3 in this.revCall0Graph.Keys)
			{
				if ((protoTemplate3.Flags & XslFlags.Stop) == XslFlags.None && (protoTemplate3.Flags & XslFlags.SideEffects) != XslFlags.None)
				{
					this.DepthFirstSearch(protoTemplate3);
				}
			}
			foreach (ProtoTemplate protoTemplate4 in this.revCall1Graph.Keys)
			{
				if ((protoTemplate4.Flags & XslFlags.Stop) == XslFlags.None && (protoTemplate4.Flags & XslFlags.SideEffects) != XslFlags.None)
				{
					this.DepthFirstSearch(protoTemplate4);
				}
			}
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x000F63E8 File Offset: 0x000F45E8
		private void DepthFirstSearch(ProtoTemplate t)
		{
			t.Flags |= (XslFlags.SideEffects | XslFlags.Stop);
			foreach (ProtoTemplate protoTemplate in this.revCall0Graph.GetAdjList(t))
			{
				if ((protoTemplate.Flags & XslFlags.Stop) == XslFlags.None)
				{
					this.DepthFirstSearch(protoTemplate);
				}
			}
			foreach (ProtoTemplate protoTemplate2 in this.revCall1Graph.GetAdjList(t))
			{
				if ((protoTemplate2.Flags & XslFlags.Stop) == XslFlags.None)
				{
					this.DepthFirstSearch(protoTemplate2);
				}
			}
			Template template = t as Template;
			List<ProtoTemplate> list;
			if (template != null && this.revApplyTemplatesGraph.TryGetValue(template.Mode, out list))
			{
				this.revApplyTemplatesGraph.Remove(template.Mode);
				foreach (ProtoTemplate protoTemplate3 in list)
				{
					if ((protoTemplate3.Flags & XslFlags.Stop) == XslFlags.None)
					{
						this.DepthFirstSearch(protoTemplate3);
					}
				}
			}
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000F6530 File Offset: 0x000F4730
		public XslAstAnalyzer()
		{
		}

		// Token: 0x04002074 RID: 8308
		private CompilerScopeManager<VarPar> scope;

		// Token: 0x04002075 RID: 8309
		private Compiler compiler;

		// Token: 0x04002076 RID: 8310
		private int forEachDepth;

		// Token: 0x04002077 RID: 8311
		private XslAstAnalyzer.XPathAnalyzer xpathAnalyzer;

		// Token: 0x04002078 RID: 8312
		private ProtoTemplate currentTemplate;

		// Token: 0x04002079 RID: 8313
		private VarPar typeDonor;

		// Token: 0x0400207A RID: 8314
		private XslAstAnalyzer.Graph<ProtoTemplate> revCall0Graph = new XslAstAnalyzer.Graph<ProtoTemplate>();

		// Token: 0x0400207B RID: 8315
		private XslAstAnalyzer.Graph<ProtoTemplate> revCall1Graph = new XslAstAnalyzer.Graph<ProtoTemplate>();

		// Token: 0x0400207C RID: 8316
		private Dictionary<Template, Stylesheet> fwdApplyImportsGraph = new Dictionary<Template, Stylesheet>();

		// Token: 0x0400207D RID: 8317
		private Dictionary<QilName, List<ProtoTemplate>> revApplyTemplatesGraph = new Dictionary<QilName, List<ProtoTemplate>>();

		// Token: 0x0400207E RID: 8318
		private XslAstAnalyzer.Graph<VarPar> dataFlow = new XslAstAnalyzer.Graph<VarPar>();

		// Token: 0x0400207F RID: 8319
		private Dictionary<XslAstAnalyzer.ModeName, VarPar> applyTemplatesParams = new Dictionary<XslAstAnalyzer.ModeName, VarPar>();

		// Token: 0x02000411 RID: 1041
		internal class Graph<V> : Dictionary<V, List<V>> where V : XslNode
		{
			// Token: 0x06002918 RID: 10520 RVA: 0x000F6588 File Offset: 0x000F4788
			public IEnumerable<V> GetAdjList(V v)
			{
				List<V> list;
				if (base.TryGetValue(v, out list) && list != null)
				{
					return list;
				}
				return XslAstAnalyzer.Graph<V>.empty;
			}

			// Token: 0x06002919 RID: 10521 RVA: 0x000F65AC File Offset: 0x000F47AC
			public void AddEdge(V v1, V v2)
			{
				if (v1 == v2)
				{
					return;
				}
				List<V> list;
				if (!base.TryGetValue(v1, out list) || list == null)
				{
					list = (base[v1] = new List<V>());
				}
				list.Add(v2);
				if (!base.TryGetValue(v2, out list))
				{
					base[v2] = null;
				}
			}

			// Token: 0x0600291A RID: 10522 RVA: 0x000F6600 File Offset: 0x000F4800
			public void PropagateFlag(XslFlags flag)
			{
				foreach (V v in base.Keys)
				{
					v.Flags &= ~XslFlags.Stop;
				}
				foreach (V v2 in base.Keys)
				{
					if ((v2.Flags & XslFlags.Stop) == XslFlags.None && (v2.Flags & flag) != XslFlags.None)
					{
						this.DepthFirstSearch(v2, flag);
					}
				}
			}

			// Token: 0x0600291B RID: 10523 RVA: 0x000F66C8 File Offset: 0x000F48C8
			private void DepthFirstSearch(V v, XslFlags flag)
			{
				v.Flags |= (flag | XslFlags.Stop);
				foreach (V v2 in this.GetAdjList(v))
				{
					if ((v2.Flags & XslFlags.Stop) == XslFlags.None)
					{
						this.DepthFirstSearch(v2, flag);
					}
				}
			}

			// Token: 0x0600291C RID: 10524 RVA: 0x000F6744 File Offset: 0x000F4944
			public Graph()
			{
			}

			// Token: 0x0600291D RID: 10525 RVA: 0x000F674C File Offset: 0x000F494C
			// Note: this type is marked as 'beforefieldinit'.
			static Graph()
			{
			}

			// Token: 0x04002080 RID: 8320
			private static IList<V> empty = new List<V>().AsReadOnly();
		}

		// Token: 0x02000412 RID: 1042
		internal struct ModeName
		{
			// Token: 0x0600291E RID: 10526 RVA: 0x000F675D File Offset: 0x000F495D
			public ModeName(QilName mode, QilName name)
			{
				this.Mode = mode;
				this.Name = name;
			}

			// Token: 0x0600291F RID: 10527 RVA: 0x000F676D File Offset: 0x000F496D
			public override int GetHashCode()
			{
				return this.Mode.GetHashCode() ^ this.Name.GetHashCode();
			}

			// Token: 0x04002081 RID: 8321
			public QilName Mode;

			// Token: 0x04002082 RID: 8322
			public QilName Name;
		}

		// Token: 0x02000413 RID: 1043
		internal struct NullErrorHelper : IErrorHelper
		{
			// Token: 0x06002920 RID: 10528 RVA: 0x0000B528 File Offset: 0x00009728
			public void ReportError(string res, params string[] args)
			{
			}

			// Token: 0x06002921 RID: 10529 RVA: 0x0000B528 File Offset: 0x00009728
			public void ReportWarning(string res, params string[] args)
			{
			}
		}

		// Token: 0x02000414 RID: 1044
		internal class XPathAnalyzer : IXPathBuilder<XslFlags>
		{
			// Token: 0x170007DD RID: 2013
			// (get) Token: 0x06002922 RID: 10530 RVA: 0x000F6786 File Offset: 0x000F4986
			public VarPar TypeDonor
			{
				get
				{
					return this.typeDonor;
				}
			}

			// Token: 0x06002923 RID: 10531 RVA: 0x000F678E File Offset: 0x000F498E
			public XPathAnalyzer(Compiler compiler, CompilerScopeManager<VarPar> scope)
			{
				this.compiler = compiler;
				this.scope = scope;
			}

			// Token: 0x06002924 RID: 10532 RVA: 0x000F67B0 File Offset: 0x000F49B0
			public XslFlags Analyze(string xpathExpr)
			{
				this.typeDonor = null;
				if (xpathExpr == null)
				{
					return XslFlags.None;
				}
				XslFlags result;
				try
				{
					this.xsltCurrentNeeded = false;
					XPathScanner scanner = new XPathScanner(xpathExpr);
					XslFlags xslFlags = this.xpathParser.Parse(scanner, this, LexKind.Eof);
					if (this.xsltCurrentNeeded)
					{
						xslFlags |= XslFlags.Current;
					}
					result = xslFlags;
				}
				catch (XslLoadException)
				{
					result = (XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf | XslFlags.Current | XslFlags.Position | XslFlags.Last);
				}
				return result;
			}

			// Token: 0x06002925 RID: 10533 RVA: 0x000F6818 File Offset: 0x000F4A18
			public XslFlags AnalyzeAvt(string source)
			{
				this.typeDonor = null;
				if (source == null)
				{
					return XslFlags.None;
				}
				XslFlags result;
				try
				{
					this.xsltCurrentNeeded = false;
					XslFlags xslFlags = XslFlags.None;
					int i = 0;
					while (i < source.Length)
					{
						i = source.IndexOf('{', i);
						if (i == -1)
						{
							break;
						}
						i++;
						if (i < source.Length && source[i] == '{')
						{
							i++;
						}
						else if (i < source.Length)
						{
							XPathScanner xpathScanner = new XPathScanner(source, i);
							xslFlags |= this.xpathParser.Parse(xpathScanner, this, LexKind.RBrace);
							i = xpathScanner.LexStart + 1;
						}
					}
					if (this.xsltCurrentNeeded)
					{
						xslFlags |= XslFlags.Current;
					}
					result = (xslFlags & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf));
				}
				catch (XslLoadException)
				{
					result = XslFlags.FocusFilter;
				}
				return result;
			}

			// Token: 0x06002926 RID: 10534 RVA: 0x000F68D4 File Offset: 0x000F4AD4
			private VarPar ResolveVariable(string prefix, string name)
			{
				string text = this.ResolvePrefix(prefix);
				if (text == null)
				{
					return null;
				}
				return this.scope.LookupVariable(name, text);
			}

			// Token: 0x06002927 RID: 10535 RVA: 0x000F68FB File Offset: 0x000F4AFB
			private string ResolvePrefix(string prefix)
			{
				if (prefix.Length == 0)
				{
					return string.Empty;
				}
				return this.scope.LookupNamespace(prefix);
			}

			// Token: 0x06002928 RID: 10536 RVA: 0x0000B528 File Offset: 0x00009728
			public virtual void StartBuild()
			{
			}

			// Token: 0x06002929 RID: 10537 RVA: 0x0000206B File Offset: 0x0000026B
			public virtual XslFlags EndBuild(XslFlags result)
			{
				return result;
			}

			// Token: 0x0600292A RID: 10538 RVA: 0x000F6917 File Offset: 0x000F4B17
			public virtual XslFlags String(string value)
			{
				this.typeDonor = null;
				return XslFlags.String;
			}

			// Token: 0x0600292B RID: 10539 RVA: 0x000F6921 File Offset: 0x000F4B21
			public virtual XslFlags Number(double value)
			{
				this.typeDonor = null;
				return XslFlags.Number;
			}

			// Token: 0x0600292C RID: 10540 RVA: 0x000F692B File Offset: 0x000F4B2B
			public virtual XslFlags Operator(XPathOperator op, XslFlags left, XslFlags right)
			{
				this.typeDonor = null;
				return ((left | right) & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf)) | XslAstAnalyzer.XPathAnalyzer.OperatorType[(int)op];
			}

			// Token: 0x0600292D RID: 10541 RVA: 0x000F6942 File Offset: 0x000F4B42
			public virtual XslFlags Axis(XPathAxis xpathAxis, XPathNodeType nodeType, string prefix, string name)
			{
				this.typeDonor = null;
				if (xpathAxis == XPathAxis.Self && nodeType == XPathNodeType.All && prefix == null && name == null)
				{
					return XslFlags.Node | XslFlags.Current;
				}
				return XslFlags.Nodeset | XslFlags.Current;
			}

			// Token: 0x0600292E RID: 10542 RVA: 0x000F6967 File Offset: 0x000F4B67
			public virtual XslFlags JoinStep(XslFlags left, XslFlags right)
			{
				this.typeDonor = null;
				return (left & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf)) | XslFlags.Nodeset;
			}

			// Token: 0x0600292F RID: 10543 RVA: 0x000F6977 File Offset: 0x000F4B77
			public virtual XslFlags Predicate(XslFlags nodeset, XslFlags predicate, bool isReverseStep)
			{
				this.typeDonor = null;
				return (nodeset & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf)) | XslFlags.Nodeset | (predicate & XslFlags.SideEffects);
			}

			// Token: 0x06002930 RID: 10544 RVA: 0x000F698F File Offset: 0x000F4B8F
			public virtual XslFlags Variable(string prefix, string name)
			{
				this.typeDonor = this.ResolveVariable(prefix, name);
				if (this.typeDonor == null)
				{
					return XslFlags.TypeFilter;
				}
				return XslFlags.None;
			}

			// Token: 0x06002931 RID: 10545 RVA: 0x000F69AC File Offset: 0x000F4BAC
			public virtual XslFlags Function(string prefix, string name, IList<XslFlags> args)
			{
				this.typeDonor = null;
				XslFlags xslFlags = XslFlags.None;
				foreach (XslFlags xslFlags2 in args)
				{
					xslFlags |= xslFlags2;
				}
				XslFlags xslFlags3 = XslFlags.None;
				if (prefix.Length == 0)
				{
					XPathBuilder.FunctionInfo<XPathBuilder.FuncId> functionInfo;
					XPathBuilder.FunctionInfo<QilGenerator.FuncId> functionInfo2;
					if (XPathBuilder.FunctionTable.TryGetValue(name, out functionInfo))
					{
						XPathBuilder.FuncId id = functionInfo.id;
						xslFlags3 = XslAstAnalyzer.XPathAnalyzer.XPathFunctionFlags[(int)id];
						if (args.Count == 0 && (id == XPathBuilder.FuncId.LocalName || id == XPathBuilder.FuncId.NamespaceUri || id == XPathBuilder.FuncId.Name || id == XPathBuilder.FuncId.String || id == XPathBuilder.FuncId.Number || id == XPathBuilder.FuncId.StringLength || id == XPathBuilder.FuncId.Normalize))
						{
							xslFlags3 |= XslFlags.Current;
						}
					}
					else if (QilGenerator.FunctionTable.TryGetValue(name, out functionInfo2))
					{
						QilGenerator.FuncId id2 = functionInfo2.id;
						xslFlags3 = XslAstAnalyzer.XPathAnalyzer.XsltFunctionFlags[(int)id2];
						if (id2 == QilGenerator.FuncId.Current)
						{
							this.xsltCurrentNeeded = true;
						}
						else if (id2 == QilGenerator.FuncId.GenerateId && args.Count == 0)
						{
							xslFlags3 |= XslFlags.Current;
						}
					}
				}
				else
				{
					string text = this.ResolvePrefix(prefix);
					if (text == "urn:schemas-microsoft-com:xslt")
					{
						uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
						if (num <= 1033099933U)
						{
							if (num <= 467038368U)
							{
								if (num != 325300801U)
								{
									if (num == 467038368U)
									{
										if (name == "number")
										{
											xslFlags3 = XslFlags.Number;
										}
									}
								}
								else if (name == "format-date")
								{
									xslFlags3 = XslFlags.String;
								}
							}
							else if (num != 999037500U)
							{
								if (num == 1033099933U)
								{
									if (name == "utc")
									{
										xslFlags3 = XslFlags.String;
									}
								}
							}
							else if (name == "local-name")
							{
								xslFlags3 = XslFlags.String;
							}
						}
						else if (num <= 2518485839U)
						{
							if (num != 2056321742U)
							{
								if (num == 2518485839U)
								{
									if (name == "namespace-uri")
									{
										xslFlags3 = (XslFlags.String | XslFlags.Current);
									}
								}
							}
							else if (name == "string-compare")
							{
								xslFlags3 = XslFlags.Number;
							}
						}
						else if (num != 3208980016U)
						{
							if (num == 3804234668U)
							{
								if (name == "format-time")
								{
									xslFlags3 = XslFlags.String;
								}
							}
						}
						else if (name == "node-set")
						{
							xslFlags3 = XslFlags.Nodeset;
						}
					}
					else if (text == "http://exslt.org/common")
					{
						if (!(name == "node-set"))
						{
							if (name == "object-type")
							{
								xslFlags3 = XslFlags.String;
							}
						}
						else
						{
							xslFlags3 = XslFlags.Nodeset;
						}
					}
					if (xslFlags3 == XslFlags.None)
					{
						xslFlags3 = XslFlags.TypeFilter;
						if (this.compiler.Settings.EnableScript && text != null)
						{
							XmlExtensionFunction xmlExtensionFunction = this.compiler.Scripts.ResolveFunction(name, text, args.Count, default(XslAstAnalyzer.NullErrorHelper));
							if (xmlExtensionFunction != null)
							{
								XmlQueryType xmlReturnType = xmlExtensionFunction.XmlReturnType;
								if (xmlReturnType == XmlQueryTypeFactory.StringX)
								{
									xslFlags3 = XslFlags.String;
								}
								else if (xmlReturnType == XmlQueryTypeFactory.DoubleX)
								{
									xslFlags3 = XslFlags.Number;
								}
								else if (xmlReturnType == XmlQueryTypeFactory.BooleanX)
								{
									xslFlags3 = XslFlags.Boolean;
								}
								else if (xmlReturnType == XmlQueryTypeFactory.NodeNotRtf)
								{
									xslFlags3 = XslFlags.Node;
								}
								else if (xmlReturnType == XmlQueryTypeFactory.NodeSDod)
								{
									xslFlags3 = XslFlags.Nodeset;
								}
								else if (xmlReturnType == XmlQueryTypeFactory.ItemS)
								{
									xslFlags3 = XslFlags.TypeFilter;
								}
								else if (xmlReturnType == XmlQueryTypeFactory.Empty)
								{
									xslFlags3 = XslFlags.Nodeset;
								}
							}
						}
						xslFlags3 |= XslFlags.SideEffects;
					}
				}
				return (xslFlags & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf)) | xslFlags3;
			}

			// Token: 0x06002932 RID: 10546 RVA: 0x000F6D2C File Offset: 0x000F4F2C
			// Note: this type is marked as 'beforefieldinit'.
			static XPathAnalyzer()
			{
			}

			// Token: 0x04002083 RID: 8323
			private XPathParser<XslFlags> xpathParser = new XPathParser<XslFlags>();

			// Token: 0x04002084 RID: 8324
			private CompilerScopeManager<VarPar> scope;

			// Token: 0x04002085 RID: 8325
			private Compiler compiler;

			// Token: 0x04002086 RID: 8326
			private bool xsltCurrentNeeded;

			// Token: 0x04002087 RID: 8327
			private VarPar typeDonor;

			// Token: 0x04002088 RID: 8328
			private static XslFlags[] OperatorType = new XslFlags[]
			{
				XslFlags.TypeFilter,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Number,
				XslFlags.Number,
				XslFlags.Number,
				XslFlags.Number,
				XslFlags.Number,
				XslFlags.Number,
				XslFlags.Nodeset
			};

			// Token: 0x04002089 RID: 8329
			private static XslFlags[] XPathFunctionFlags = new XslFlags[]
			{
				XslFlags.Number | XslFlags.Last,
				XslFlags.Number | XslFlags.Position,
				XslFlags.Number,
				XslFlags.String,
				XslFlags.String,
				XslFlags.String,
				XslFlags.String,
				XslFlags.Number,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Nodeset | XslFlags.Current,
				XslFlags.String,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.String,
				XslFlags.String,
				XslFlags.String,
				XslFlags.Number,
				XslFlags.String,
				XslFlags.String,
				XslFlags.Boolean | XslFlags.Current,
				XslFlags.Number,
				XslFlags.Number,
				XslFlags.Number,
				XslFlags.Number
			};

			// Token: 0x0400208A RID: 8330
			private static XslFlags[] XsltFunctionFlags = new XslFlags[]
			{
				XslFlags.Node,
				XslFlags.Nodeset,
				XslFlags.Nodeset | XslFlags.Current,
				XslFlags.String,
				XslFlags.String,
				XslFlags.String,
				XslFlags.String | XslFlags.Number,
				XslFlags.Boolean,
				XslFlags.Boolean
			};
		}
	}
}
