using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003F0 RID: 1008
	internal class QilGenerator : IErrorHelper, IXPathEnvironment, IFocus
	{
		// Token: 0x060027D9 RID: 10201 RVA: 0x000ED07A File Offset: 0x000EB27A
		public static QilExpression CompileStylesheet(Compiler compiler)
		{
			return new QilGenerator(compiler.IsDebug).Compile(compiler);
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x000ED090 File Offset: 0x000EB290
		private QilGenerator(bool debug)
		{
			this.scope = new CompilerScopeManager<QilIterator>();
			this.outputScope = new OutputScopeManager();
			this.prefixesInUse = new HybridDictionary();
			this.f = new XsltQilFactory(new QilFactory(), debug);
			this.xpathBuilder = new XPathBuilder(this);
			this.xpathParser = new XPathParser<QilNode>();
			this.ptrnBuilder = new XPathPatternBuilder(this);
			this.ptrnParser = new XPathPatternParser();
			this.refReplacer = new ReferenceReplacer(this.f.BaseFactory);
			this.invkGen = new InvokeGenerator(this.f, debug);
			this.matcherBuilder = new MatcherBuilder(this.f, this.refReplacer, this.invkGen);
			this.singlFocus = new SingletonFocus(this.f);
			this.funcFocus = default(FunctionFocus);
			this.curLoop = new LoopFocus(this.f);
			this.strConcat = new QilStrConcatenator(this.f);
			this.varHelper = new QilGenerator.VariableHelper(this.f);
			this.elementOrDocumentType = XmlQueryTypeFactory.DocumentOrElement;
			this.textOrAttributeType = XmlQueryTypeFactory.NodeChoice(XmlNodeKindFlags.Attribute | XmlNodeKindFlags.Text);
			this.nameCurrent = this.f.QName("current", "urn:schemas-microsoft-com:xslt-debug");
			this.namePosition = this.f.QName("position", "urn:schemas-microsoft-com:xslt-debug");
			this.nameLast = this.f.QName("last", "urn:schemas-microsoft-com:xslt-debug");
			this.nameNamespaces = this.f.QName("namespaces", "urn:schemas-microsoft-com:xslt-debug");
			this.nameInit = this.f.QName("init", "urn:schemas-microsoft-com:xslt-debug");
			this.formatterCnt = 0;
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x060027DB RID: 10203 RVA: 0x000ED25E File Offset: 0x000EB45E
		private bool IsDebug
		{
			get
			{
				return this.compiler.IsDebug;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x000ED26B File Offset: 0x000EB46B
		private bool EvaluateFuncCalls
		{
			get
			{
				return !this.IsDebug;
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x060027DD RID: 10205 RVA: 0x000ED26B File Offset: 0x000EB46B
		private bool InferXPathTypes
		{
			get
			{
				return !this.IsDebug;
			}
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x000ED278 File Offset: 0x000EB478
		private QilExpression Compile(Compiler compiler)
		{
			this.compiler = compiler;
			this.functions = this.f.FunctionList();
			this.extPars = this.f.GlobalParameterList();
			this.gloVars = this.f.GlobalVariableList();
			this.nsVars = this.f.GlobalVariableList();
			compiler.Scripts.CompileScripts();
			new XslAstRewriter().Rewrite(compiler);
			if (!this.IsDebug)
			{
				new XslAstAnalyzer().Analyze(compiler);
			}
			this.CreateGlobalVarPars();
			try
			{
				this.CompileKeys();
				this.CompileAndSortMatches(compiler.Root.Imports[0]);
				this.PrecompileProtoTemplatesHeaders();
				this.CompileGlobalVariables();
				foreach (ProtoTemplate tmpl in compiler.AllTemplates)
				{
					this.CompileProtoTemplate(tmpl);
				}
			}
			catch (XslLoadException ex)
			{
				ex.SetSourceLineInfo(this.lastScope.SourceLine);
				throw;
			}
			catch (Exception ex2)
			{
				if (!XmlException.IsCatchableException(ex2))
				{
					throw;
				}
				throw new XslLoadException(ex2, this.lastScope.SourceLine);
			}
			this.CompileInitializationCode();
			QilNode root = this.CompileRootExpression(compiler.StartApplyTemplates);
			foreach (ProtoTemplate protoTemplate in compiler.AllTemplates)
			{
				foreach (QilNode qilNode in protoTemplate.Function.Arguments)
				{
					QilParameter qilParameter = (QilParameter)qilNode;
					if (!this.IsDebug || qilParameter.Name.Equals(this.nameNamespaces))
					{
						qilParameter.DefaultValue = null;
					}
				}
			}
			Dictionary<string, Type> scriptClasses = compiler.Scripts.ScriptClasses;
			List<EarlyBoundInfo> list = new List<EarlyBoundInfo>(scriptClasses.Count);
			foreach (KeyValuePair<string, Type> keyValuePair in scriptClasses)
			{
				if (keyValuePair.Value != null)
				{
					list.Add(new EarlyBoundInfo(keyValuePair.Key, keyValuePair.Value));
				}
			}
			QilExpression qilExpression = this.f.QilExpression(root, this.f.BaseFactory);
			qilExpression.EarlyBoundTypes = list;
			qilExpression.FunctionList = this.functions;
			qilExpression.GlobalParameterList = this.extPars;
			qilExpression.GlobalVariableList = this.gloVars;
			qilExpression.WhitespaceRules = compiler.WhitespaceRules;
			qilExpression.IsDebug = this.IsDebug;
			qilExpression.DefaultWriterSettings = compiler.Output.Settings;
			QilDepthChecker.Check(qilExpression);
			return qilExpression;
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x000ED550 File Offset: 0x000EB750
		private QilNode InvokeOnCurrentNodeChanged()
		{
			return this.f.Loop(this.f.Let(this.f.InvokeOnCurrentNodeChanged(this.curLoop.GetCurrent())), this.f.Sequence());
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		private void CheckSingletonFocus()
		{
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x000ED598 File Offset: 0x000EB798
		private void CompileInitializationCode()
		{
			QilNode qilNode = this.f.Int32(0);
			if (this.formatNumberDynamicUsed || this.IsDebug)
			{
				bool flag = false;
				foreach (DecimalFormatDecl decimalFormatDecl in this.compiler.DecimalFormats)
				{
					qilNode = this.f.Add(qilNode, this.f.InvokeRegisterDecimalFormat(decimalFormatDecl));
					flag |= (decimalFormatDecl.Name == DecimalFormatDecl.Default.Name);
				}
				if (!flag)
				{
					qilNode = this.f.Add(qilNode, this.f.InvokeRegisterDecimalFormat(DecimalFormatDecl.Default));
				}
			}
			foreach (string nsUri in this.compiler.Scripts.ScriptClasses.Keys)
			{
				qilNode = this.f.Add(qilNode, this.f.InvokeCheckScriptNamespace(nsUri));
			}
			if (qilNode.NodeType == QilNodeType.Add)
			{
				QilFunction qilFunction = this.f.Function(this.f.FormalParameterList(), qilNode, this.f.True());
				qilFunction.DebugName = "Init";
				this.functions.Add(qilFunction);
				QilNode qilNode2 = this.f.Invoke(qilFunction, this.f.ActualParameterList());
				if (this.IsDebug)
				{
					qilNode2 = this.f.TypeAssert(qilNode2, XmlQueryTypeFactory.ItemS);
				}
				QilIterator qilIterator = this.f.Let(qilNode2);
				qilIterator.DebugName = this.nameInit.ToString();
				this.gloVars.Insert(0, qilIterator);
			}
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x000ED76C File Offset: 0x000EB96C
		private QilNode CompileRootExpression(XslNode applyTmpls)
		{
			this.singlFocus.SetFocus(SingletonFocusType.InitialContextNode);
			QilNode child = this.GenerateApply(this.compiler.Root, applyTmpls);
			this.singlFocus.SetFocus(null);
			return this.f.DocumentCtor(child);
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x000ED7B0 File Offset: 0x000EB9B0
		private QilList EnterScope(XslNode node)
		{
			this.lastScope = node;
			this.xslVersion = node.XslVersion;
			if (this.scope.EnterScope(node.Namespaces))
			{
				return this.BuildDebuggerNamespaces();
			}
			return null;
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x000ED7E0 File Offset: 0x000EB9E0
		private void ExitScope()
		{
			this.scope.ExitScope();
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000ED7F0 File Offset: 0x000EB9F0
		private QilList BuildDebuggerNamespaces()
		{
			if (this.IsDebug)
			{
				QilList qilList = this.f.BaseFactory.Sequence();
				foreach (CompilerScopeManager<QilIterator>.ScopeRecord scopeRecord in this.scope)
				{
					qilList.Add(this.f.NamespaceDecl(this.f.String(scopeRecord.ncName), this.f.String(scopeRecord.nsUri)));
				}
				return qilList;
			}
			return null;
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x000ED86B File Offset: 0x000EBA6B
		private QilNode GetCurrentNode()
		{
			if (this.curLoop.IsFocusSet)
			{
				return this.curLoop.GetCurrent();
			}
			if (this.funcFocus.IsFocusSet)
			{
				return this.funcFocus.GetCurrent();
			}
			return this.singlFocus.GetCurrent();
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x000ED8AA File Offset: 0x000EBAAA
		private QilNode GetCurrentPosition()
		{
			if (this.curLoop.IsFocusSet)
			{
				return this.curLoop.GetPosition();
			}
			if (this.funcFocus.IsFocusSet)
			{
				return this.funcFocus.GetPosition();
			}
			return this.singlFocus.GetPosition();
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x000ED8E9 File Offset: 0x000EBAE9
		private QilNode GetLastPosition()
		{
			if (this.curLoop.IsFocusSet)
			{
				return this.curLoop.GetLast();
			}
			if (this.funcFocus.IsFocusSet)
			{
				return this.funcFocus.GetLast();
			}
			return this.singlFocus.GetLast();
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x000ED928 File Offset: 0x000EBB28
		private XmlQueryType ChooseBestType(VarPar var)
		{
			if (this.IsDebug || !this.InferXPathTypes)
			{
				return XmlQueryTypeFactory.ItemS;
			}
			XslFlags xslFlags = var.Flags & XslFlags.TypeFilter;
			if (xslFlags <= (XslFlags.Node | XslFlags.Nodeset))
			{
				if (xslFlags <= XslFlags.Node)
				{
					switch (xslFlags)
					{
					case XslFlags.String:
						return XmlQueryTypeFactory.StringX;
					case XslFlags.Number:
						return XmlQueryTypeFactory.DoubleX;
					case XslFlags.String | XslFlags.Number:
						break;
					case XslFlags.Boolean:
						return XmlQueryTypeFactory.BooleanX;
					default:
						if (xslFlags == XslFlags.Node)
						{
							return XmlQueryTypeFactory.NodeNotRtf;
						}
						break;
					}
				}
				else
				{
					if (xslFlags == XslFlags.Nodeset)
					{
						return XmlQueryTypeFactory.NodeNotRtfS;
					}
					if (xslFlags == (XslFlags.Node | XslFlags.Nodeset))
					{
						return XmlQueryTypeFactory.NodeNotRtfS;
					}
				}
			}
			else if (xslFlags <= (XslFlags.Node | XslFlags.Rtf))
			{
				if (xslFlags == XslFlags.Rtf)
				{
					return XmlQueryTypeFactory.Node;
				}
				if (xslFlags == (XslFlags.Node | XslFlags.Rtf))
				{
					return XmlQueryTypeFactory.Node;
				}
			}
			else
			{
				if (xslFlags == (XslFlags.Nodeset | XslFlags.Rtf))
				{
					return XmlQueryTypeFactory.NodeS;
				}
				if (xslFlags == (XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf))
				{
					return XmlQueryTypeFactory.NodeS;
				}
			}
			return XmlQueryTypeFactory.ItemS;
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x000ED9E8 File Offset: 0x000EBBE8
		private QilIterator GetNsVar(QilList nsList)
		{
			foreach (QilNode qilNode in this.nsVars)
			{
				QilIterator qilIterator = (QilIterator)qilNode;
				QilList qilList = (QilList)qilIterator.Binding;
				if (qilList.Count == nsList.Count)
				{
					bool flag = true;
					for (int i = 0; i < nsList.Count; i++)
					{
						if (((QilLiteral)((QilBinary)nsList[i]).Right).Value != ((QilLiteral)((QilBinary)qilList[i]).Right).Value || ((QilLiteral)((QilBinary)nsList[i]).Left).Value != ((QilLiteral)((QilBinary)qilList[i]).Left).Value)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return qilIterator;
					}
				}
			}
			QilIterator qilIterator2 = this.f.Let(nsList);
			qilIterator2.DebugName = this.f.QName("ns" + this.nsVars.Count.ToString(), "urn:schemas-microsoft-com:xslt-debug").ToString();
			this.gloVars.Add(qilIterator2);
			this.nsVars.Add(qilIterator2);
			return qilIterator2;
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x000EDB58 File Offset: 0x000EBD58
		private void PrecompileProtoTemplatesHeaders()
		{
			List<VarPar> list = null;
			Dictionary<VarPar, Template> dictionary = null;
			Dictionary<VarPar, QilFunction> dictionary2 = null;
			foreach (ProtoTemplate protoTemplate in this.compiler.AllTemplates)
			{
				QilList qilList = this.f.FormalParameterList();
				XslFlags xslFlags = (!this.IsDebug) ? protoTemplate.Flags : XslFlags.FocusFilter;
				QilList qilList2 = this.EnterScope(protoTemplate);
				if ((xslFlags & XslFlags.Current) != XslFlags.None)
				{
					qilList.Add(this.CreateXslParam(this.CloneName(this.nameCurrent), XmlQueryTypeFactory.NodeNotRtf));
				}
				if ((xslFlags & XslFlags.Position) != XslFlags.None)
				{
					qilList.Add(this.CreateXslParam(this.CloneName(this.namePosition), XmlQueryTypeFactory.DoubleX));
				}
				if ((xslFlags & XslFlags.Last) != XslFlags.None)
				{
					qilList.Add(this.CreateXslParam(this.CloneName(this.nameLast), XmlQueryTypeFactory.DoubleX));
				}
				if (this.IsDebug && qilList2 != null)
				{
					QilParameter qilParameter = this.CreateXslParam(this.CloneName(this.nameNamespaces), XmlQueryTypeFactory.NamespaceS);
					qilParameter.DefaultValue = this.GetNsVar(qilList2);
					qilList.Add(qilParameter);
				}
				Template template = protoTemplate as Template;
				if (template != null)
				{
					this.funcFocus.StartFocus(qilList, xslFlags);
					for (int i = 0; i < protoTemplate.Content.Count; i++)
					{
						XslNode xslNode = protoTemplate.Content[i];
						if (xslNode.NodeType != XslNodeType.Text)
						{
							if (xslNode.NodeType != XslNodeType.Param)
							{
								break;
							}
							VarPar varPar = (VarPar)xslNode;
							this.EnterScope(varPar);
							if (this.scope.IsLocalVariable(varPar.Name.LocalName, varPar.Name.NamespaceUri))
							{
								this.ReportError("The variable or parameter '{0}' was duplicated within the same scope.", new string[]
								{
									varPar.Name.QualifiedName
								});
							}
							QilParameter qilParameter2 = this.CreateXslParam(varPar.Name, this.ChooseBestType(varPar));
							if (this.IsDebug)
							{
								qilParameter2.Annotation = varPar;
							}
							else if ((varPar.DefValueFlags & XslFlags.HasCalls) == XslFlags.None)
							{
								qilParameter2.DefaultValue = this.CompileVarParValue(varPar);
							}
							else
							{
								QilList qilList3 = this.f.FormalParameterList();
								QilList qilList4 = this.f.ActualParameterList();
								for (int j = 0; j < qilList.Count; j++)
								{
									QilParameter qilParameter3 = this.f.Parameter(qilList[j].XmlType);
									qilParameter3.DebugName = ((QilParameter)qilList[j]).DebugName;
									qilParameter3.Name = this.CloneName(((QilParameter)qilList[j]).Name);
									QilGenerator.SetLineInfo(qilParameter3, qilList[j].SourceLine);
									qilList3.Add(qilParameter3);
									qilList4.Add(qilList[j]);
								}
								varPar.Flags |= (template.Flags & XslFlags.FocusFilter);
								QilFunction qilFunction = this.f.Function(qilList3, this.f.Boolean((varPar.DefValueFlags & XslFlags.SideEffects) > XslFlags.None), this.ChooseBestType(varPar));
								qilFunction.SourceLine = SourceLineInfo.NoSource;
								qilFunction.DebugName = "<xsl:param name=\"" + varPar.Name.QualifiedName + "\">";
								qilParameter2.DefaultValue = this.f.Invoke(qilFunction, qilList4);
								if (list == null)
								{
									list = new List<VarPar>();
									dictionary = new Dictionary<VarPar, Template>();
									dictionary2 = new Dictionary<VarPar, QilFunction>();
								}
								list.Add(varPar);
								dictionary.Add(varPar, template);
								dictionary2.Add(varPar, qilFunction);
							}
							QilGenerator.SetLineInfo(qilParameter2, varPar.SourceLine);
							this.ExitScope();
							this.scope.AddVariable(varPar.Name, qilParameter2);
							qilList.Add(qilParameter2);
						}
					}
					this.funcFocus.StopFocus();
				}
				this.ExitScope();
				protoTemplate.Function = this.f.Function(qilList, this.f.Boolean((protoTemplate.Flags & XslFlags.SideEffects) > XslFlags.None), (protoTemplate is AttributeSet) ? XmlQueryTypeFactory.AttributeS : XmlQueryTypeFactory.NodeNotRtfS);
				protoTemplate.Function.DebugName = protoTemplate.GetDebugName();
				QilGenerator.SetLineInfo(protoTemplate.Function, protoTemplate.SourceLine ?? SourceLineInfo.NoSource);
				this.functions.Add(protoTemplate.Function);
			}
			if (list != null)
			{
				foreach (VarPar varPar2 in list)
				{
					Template node = dictionary[varPar2];
					QilFunction qilFunction2 = dictionary2[varPar2];
					this.funcFocus.StartFocus(qilFunction2.Arguments, varPar2.Flags);
					this.EnterScope(node);
					this.EnterScope(varPar2);
					foreach (QilNode qilNode in qilFunction2.Arguments)
					{
						QilParameter qilParameter4 = (QilParameter)qilNode;
						this.scope.AddVariable(qilParameter4.Name, qilParameter4);
					}
					qilFunction2.Definition = this.CompileVarParValue(varPar2);
					QilGenerator.SetLineInfo(qilFunction2.Definition, varPar2.SourceLine);
					this.ExitScope();
					this.ExitScope();
					this.funcFocus.StopFocus();
					this.functions.Add(qilFunction2);
				}
			}
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x000EE138 File Offset: 0x000EC338
		private QilParameter CreateXslParam(QilName name, XmlQueryType xt)
		{
			QilParameter qilParameter = this.f.Parameter(xt);
			qilParameter.DebugName = name.ToString();
			qilParameter.Name = name;
			return qilParameter;
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x000EE15C File Offset: 0x000EC35C
		private void CompileProtoTemplate(ProtoTemplate tmpl)
		{
			this.EnterScope(tmpl);
			this.funcFocus.StartFocus(tmpl.Function.Arguments, (!this.IsDebug) ? tmpl.Flags : XslFlags.FocusFilter);
			foreach (QilNode qilNode in tmpl.Function.Arguments)
			{
				QilParameter qilParameter = (QilParameter)qilNode;
				if (qilParameter.Name.NamespaceUri != "urn:schemas-microsoft-com:xslt-debug")
				{
					if (this.IsDebug)
					{
						VarPar node = (VarPar)qilParameter.Annotation;
						QilList nsList = this.EnterScope(node);
						qilParameter.DefaultValue = this.CompileVarParValue(node);
						this.ExitScope();
						qilParameter.DefaultValue = this.SetDebugNs(qilParameter.DefaultValue, nsList);
					}
					this.scope.AddVariable(qilParameter.Name, qilParameter);
				}
			}
			tmpl.Function.Definition = this.CompileInstructions(tmpl.Content);
			this.funcFocus.StopFocus();
			this.ExitScope();
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x000EE274 File Offset: 0x000EC474
		private QilList InstructionList()
		{
			return this.f.BaseFactory.Sequence();
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x000EE286 File Offset: 0x000EC486
		private QilNode CompileInstructions(IList<XslNode> instructions)
		{
			return this.CompileInstructions(instructions, 0, this.InstructionList());
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x000EE296 File Offset: 0x000EC496
		private QilNode CompileInstructions(IList<XslNode> instructions, int from)
		{
			return this.CompileInstructions(instructions, from, this.InstructionList());
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000EE2A6 File Offset: 0x000EC4A6
		private QilNode CompileInstructions(IList<XslNode> instructions, QilList content)
		{
			return this.CompileInstructions(instructions, 0, content);
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x000EE2B4 File Offset: 0x000EC4B4
		private QilNode CompileInstructions(IList<XslNode> instructions, int from, QilList content)
		{
			for (int i = from; i < instructions.Count; i++)
			{
				XslNode xslNode = instructions[i];
				XslNodeType nodeType = xslNode.NodeType;
				if (nodeType != XslNodeType.Param)
				{
					QilList nsList = this.EnterScope(xslNode);
					QilNode qilNode;
					switch (nodeType)
					{
					case XslNodeType.ApplyImports:
						qilNode = this.CompileApplyImports(xslNode);
						break;
					case XslNodeType.ApplyTemplates:
						qilNode = this.CompileApplyTemplates((XslNodeEx)xslNode);
						break;
					case XslNodeType.Attribute:
						qilNode = this.CompileAttribute((NodeCtor)xslNode);
						break;
					case XslNodeType.AttributeSet:
					case XslNodeType.Key:
					case XslNodeType.Otherwise:
					case XslNodeType.Param:
					case XslNodeType.Sort:
					case XslNodeType.Template:
						goto IL_1FD;
					case XslNodeType.CallTemplate:
						qilNode = this.CompileCallTemplate((XslNodeEx)xslNode);
						break;
					case XslNodeType.Choose:
						qilNode = this.CompileChoose(xslNode);
						break;
					case XslNodeType.Comment:
						qilNode = this.CompileComment(xslNode);
						break;
					case XslNodeType.Copy:
						qilNode = this.CompileCopy(xslNode);
						break;
					case XslNodeType.CopyOf:
						qilNode = this.CompileCopyOf(xslNode);
						break;
					case XslNodeType.Element:
						qilNode = this.CompileElement((NodeCtor)xslNode);
						break;
					case XslNodeType.Error:
						qilNode = this.CompileError(xslNode);
						break;
					case XslNodeType.ForEach:
						qilNode = this.CompileForEach((XslNodeEx)xslNode);
						break;
					case XslNodeType.If:
						qilNode = this.CompileIf(xslNode);
						break;
					case XslNodeType.List:
						qilNode = this.CompileList(xslNode);
						break;
					case XslNodeType.LiteralAttribute:
						qilNode = this.CompileLiteralAttribute(xslNode);
						break;
					case XslNodeType.LiteralElement:
						qilNode = this.CompileLiteralElement(xslNode);
						break;
					case XslNodeType.Message:
						qilNode = this.CompileMessage(xslNode);
						break;
					case XslNodeType.Nop:
						qilNode = this.CompileNop(xslNode);
						break;
					case XslNodeType.Number:
						qilNode = this.CompileNumber((Number)xslNode);
						break;
					case XslNodeType.PI:
						qilNode = this.CompilePI(xslNode);
						break;
					case XslNodeType.Text:
						qilNode = this.CompileText((Text)xslNode);
						break;
					case XslNodeType.UseAttributeSet:
						qilNode = this.CompileUseAttributeSet(xslNode);
						break;
					case XslNodeType.ValueOf:
						qilNode = this.CompileValueOf(xslNode);
						break;
					case XslNodeType.ValueOfDoe:
						qilNode = this.CompileValueOfDoe(xslNode);
						break;
					case XslNodeType.Variable:
						qilNode = this.CompileVariable(xslNode);
						break;
					default:
						goto IL_1FD;
					}
					IL_200:
					this.ExitScope();
					if (qilNode.NodeType != QilNodeType.Sequence || qilNode.Count != 0)
					{
						if (nodeType != XslNodeType.LiteralAttribute && nodeType != XslNodeType.UseAttributeSet)
						{
							this.SetLineInfoCheck(qilNode, xslNode.SourceLine);
						}
						qilNode = this.SetDebugNs(qilNode, nsList);
						if (nodeType == XslNodeType.Variable)
						{
							QilIterator qilIterator = this.f.Let(qilNode);
							qilIterator.DebugName = xslNode.Name.ToString();
							this.scope.AddVariable(xslNode.Name, qilIterator);
							qilNode = this.f.Loop(qilIterator, this.CompileInstructions(instructions, i + 1));
							i = instructions.Count;
						}
						content.Add(qilNode);
						goto IL_2A1;
					}
					goto IL_2A1;
					IL_1FD:
					qilNode = null;
					goto IL_200;
				}
				IL_2A1:;
			}
			if (!this.IsDebug && content.Count == 1)
			{
				return content[0];
			}
			return content;
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000EE58C File Offset: 0x000EC78C
		private QilNode CompileList(XslNode node)
		{
			return this.CompileInstructions(node.Content);
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000EE59A File Offset: 0x000EC79A
		private QilNode CompileNop(XslNode node)
		{
			return this.f.Nop(this.f.Sequence());
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000EE5B4 File Offset: 0x000EC7B4
		private void AddNsDecl(QilList content, string prefix, string nsUri)
		{
			if (this.outputScope.LookupNamespace(prefix) == nsUri)
			{
				return;
			}
			this.outputScope.AddNamespace(prefix, nsUri);
			content.Add(this.f.NamespaceDecl(this.f.String(prefix), this.f.String(nsUri)));
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000EE60C File Offset: 0x000EC80C
		private QilNode CompileLiteralElement(XslNode node)
		{
			bool flag = true;
			QilName name;
			string prefix;
			string namespaceUri;
			QilList content;
			for (;;)
			{
				IL_02:
				this.prefixesInUse.Clear();
				name = node.Name;
				prefix = name.Prefix;
				namespaceUri = name.NamespaceUri;
				this.compiler.ApplyNsAliases(ref prefix, ref namespaceUri);
				if (flag)
				{
					this.prefixesInUse.Add(prefix, namespaceUri);
				}
				else
				{
					prefix = name.Prefix;
				}
				this.outputScope.PushScope();
				content = this.InstructionList();
				foreach (CompilerScopeManager<QilIterator>.ScopeRecord scopeRecord in this.scope)
				{
					string ncName = scopeRecord.ncName;
					string nsUri = scopeRecord.nsUri;
					if (nsUri != "http://www.w3.org/1999/XSL/Transform" && !this.scope.IsExNamespace(nsUri))
					{
						this.compiler.ApplyNsAliases(ref ncName, ref nsUri);
						if (flag)
						{
							if (this.prefixesInUse.Contains(ncName))
							{
								if ((string)this.prefixesInUse[ncName] != nsUri)
								{
									this.outputScope.PopScope();
									flag = false;
									goto IL_02;
								}
							}
							else
							{
								this.prefixesInUse.Add(ncName, nsUri);
							}
						}
						else
						{
							ncName = scopeRecord.ncName;
						}
						this.AddNsDecl(content, ncName, nsUri);
					}
				}
				break;
			}
			QilNode content2 = this.CompileInstructions(node.Content, content);
			this.outputScope.PopScope();
			name.Prefix = prefix;
			name.NamespaceUri = namespaceUri;
			return this.f.ElementCtor(name, content2);
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x000EE77C File Offset: 0x000EC97C
		private QilNode CompileElement(NodeCtor node)
		{
			QilNode qilNode = this.CompileStringAvt(node.NsAvt);
			QilNode qilNode2 = this.CompileStringAvt(node.NameAvt);
			QilNode name;
			if (qilNode2.NodeType == QilNodeType.LiteralString && (qilNode == null || qilNode.NodeType == QilNodeType.LiteralString))
			{
				string qname = (QilLiteral)qilNode2;
				string prefix;
				string local;
				bool flag = this.compiler.ParseQName(qname, out prefix, out local, this);
				string uri;
				if (qilNode == null)
				{
					uri = (flag ? this.ResolvePrefix(false, prefix) : this.compiler.CreatePhantomNamespace());
				}
				else
				{
					uri = (QilLiteral)qilNode;
				}
				name = this.f.QName(local, uri, prefix);
			}
			else if (qilNode != null)
			{
				name = this.f.StrParseQName(qilNode2, qilNode);
			}
			else
			{
				name = this.ResolveQNameDynamic(false, qilNode2);
			}
			this.outputScope.PushScope();
			this.outputScope.InvalidateAllPrefixes();
			QilNode content = this.CompileInstructions(node.Content);
			this.outputScope.PopScope();
			return this.f.ElementCtor(name, content);
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x000EE874 File Offset: 0x000ECA74
		private QilNode CompileLiteralAttribute(XslNode node)
		{
			QilName name = node.Name;
			string prefix = name.Prefix;
			string namespaceUri = name.NamespaceUri;
			if (prefix.Length != 0)
			{
				this.compiler.ApplyNsAliases(ref prefix, ref namespaceUri);
			}
			name.Prefix = prefix;
			name.NamespaceUri = namespaceUri;
			return this.f.AttributeCtor(name, this.CompileTextAvt(node.Select));
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000EE8D4 File Offset: 0x000ECAD4
		private QilNode CompileAttribute(NodeCtor node)
		{
			QilNode qilNode = this.CompileStringAvt(node.NsAvt);
			QilNode qilNode2 = this.CompileStringAvt(node.NameAvt);
			bool flag = false;
			QilNode name;
			if (qilNode2.NodeType == QilNodeType.LiteralString && (qilNode == null || qilNode.NodeType == QilNodeType.LiteralString))
			{
				string text = (QilLiteral)qilNode2;
				string prefix;
				string text2;
				bool flag2 = this.compiler.ParseQName(text, out prefix, out text2, this);
				string text3;
				if (qilNode == null)
				{
					text3 = (flag2 ? this.ResolvePrefix(true, prefix) : this.compiler.CreatePhantomNamespace());
				}
				else
				{
					text3 = (QilLiteral)qilNode;
					flag = true;
				}
				if (text == "xmlns" || (text2 == "xmlns" && text3.Length == 0))
				{
					this.ReportError("An attribute with a local name 'xmlns' and a null namespace URI cannot be created.", new string[]
					{
						"name",
						text
					});
				}
				name = this.f.QName(text2, text3, prefix);
			}
			else if (qilNode != null)
			{
				name = this.f.StrParseQName(qilNode2, qilNode);
			}
			else
			{
				name = this.ResolveQNameDynamic(true, qilNode2);
			}
			if (flag)
			{
				this.outputScope.InvalidateNonDefaultPrefixes();
			}
			return this.f.AttributeCtor(name, this.CompileInstructions(node.Content));
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x000EEA04 File Offset: 0x000ECC04
		private QilNode ExtractText(string source, ref int pos)
		{
			int num = pos;
			this.unescapedText.Length = 0;
			int i;
			for (i = pos; i < source.Length; i++)
			{
				char c = source[i];
				if (c == '{' || c == '}')
				{
					if (i + 1 < source.Length && source[i + 1] == c)
					{
						i++;
						this.unescapedText.Append(source, num, i - num);
						num = i + 1;
					}
					else
					{
						if (c == '{')
						{
							break;
						}
						pos = source.Length;
						if (this.xslVersion != XslVersion.ForwardsCompatible)
						{
							this.ReportError("The right curly brace in an attribute value template '{0}' outside an expression must be doubled.", new string[]
							{
								source
							});
							return null;
						}
						return this.f.Error(this.lastScope.SourceLine, "The right curly brace in an attribute value template '{0}' outside an expression must be doubled.", new string[]
						{
							source
						});
					}
				}
			}
			pos = i;
			if (this.unescapedText.Length != 0)
			{
				this.unescapedText.Append(source, num, i - num);
				return this.f.String(this.unescapedText.ToString());
			}
			if (i <= num)
			{
				return null;
			}
			return this.f.String(source.Substring(num, i - num));
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x000EEB28 File Offset: 0x000ECD28
		private QilNode CompileAvt(string source)
		{
			QilList qilList = this.f.BaseFactory.Sequence();
			int i = 0;
			while (i < source.Length)
			{
				QilNode qilNode = this.ExtractText(source, ref i);
				if (qilNode != null)
				{
					qilList.Add(qilNode);
				}
				if (i < source.Length)
				{
					i++;
					QilNode n = this.CompileXPathExpressionWithinAvt(source, ref i);
					qilList.Add(this.f.ConvertToString(n));
				}
			}
			if (qilList.Count == 1)
			{
				return qilList[0];
			}
			return qilList;
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x000EEBA2 File Offset: 0x000ECDA2
		private QilNode CompileStringAvt(string avt)
		{
			if (avt == null)
			{
				return null;
			}
			if (avt.IndexOfAny(QilGenerator.curlyBraces) == -1)
			{
				return this.f.String(avt);
			}
			return this.f.StrConcat(this.CompileAvt(avt));
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x000EEBD8 File Offset: 0x000ECDD8
		private QilNode CompileTextAvt(string avt)
		{
			if (avt.IndexOfAny(QilGenerator.curlyBraces) == -1)
			{
				return this.f.TextCtor(this.f.String(avt));
			}
			QilNode qilNode = this.CompileAvt(avt);
			if (qilNode.NodeType == QilNodeType.Sequence)
			{
				QilList qilList = this.InstructionList();
				foreach (QilNode content in qilNode)
				{
					qilList.Add(this.f.TextCtor(content));
				}
				return qilList;
			}
			return this.f.TextCtor(qilNode);
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x000EEC78 File Offset: 0x000ECE78
		private QilNode CompileText(Text node)
		{
			if (node.Hints == SerializationHints.None)
			{
				return this.f.TextCtor(this.f.String(node.Select));
			}
			return this.f.RawTextCtor(this.f.String(node.Select));
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x000EECC8 File Offset: 0x000ECEC8
		private QilNode CompilePI(XslNode node)
		{
			QilNode qilNode = this.CompileStringAvt(node.Select);
			if (qilNode.NodeType == QilNodeType.LiteralString)
			{
				string name = (QilLiteral)qilNode;
				this.compiler.ValidatePiName(name, this);
			}
			return this.f.PICtor(qilNode, this.CompileInstructions(node.Content));
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x000EED1D File Offset: 0x000ECF1D
		private QilNode CompileComment(XslNode node)
		{
			return this.f.CommentCtor(this.CompileInstructions(node.Content));
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x000EED36 File Offset: 0x000ECF36
		private QilNode CompileError(XslNode node)
		{
			return this.f.Error(this.f.String(node.Select));
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x000EED54 File Offset: 0x000ECF54
		private QilNode WrapLoopBody(ISourceLineInfo before, QilNode expr, ISourceLineInfo after)
		{
			if (this.IsDebug)
			{
				return this.f.Sequence(new QilNode[]
				{
					QilGenerator.SetLineInfo(this.InvokeOnCurrentNodeChanged(), before),
					expr,
					QilGenerator.SetLineInfo(this.f.Nop(this.f.Sequence()), after)
				});
			}
			return expr;
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x000EEDB0 File Offset: 0x000ECFB0
		private QilNode CompileForEach(XslNodeEx node)
		{
			IList<XslNode> content = node.Content;
			LoopFocus loopFocus = this.curLoop;
			QilIterator focus = this.f.For(this.CompileNodeSetExpression(node.Select));
			this.curLoop.SetFocus(focus);
			int varScope = this.varHelper.StartVariables();
			this.curLoop.Sort(this.CompileSorts(content, ref loopFocus));
			QilNode qilNode = this.CompileInstructions(content);
			qilNode = this.WrapLoopBody(node.ElemNameLi, qilNode, node.EndTagLi);
			qilNode = this.AddCurrentPositionLast(qilNode);
			qilNode = this.curLoop.ConstructLoop(qilNode);
			qilNode = this.varHelper.FinishVariables(qilNode, varScope);
			this.curLoop = loopFocus;
			return qilNode;
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x000EEE58 File Offset: 0x000ED058
		private QilNode CompileApplyTemplates(XslNodeEx node)
		{
			IList<XslNode> content = node.Content;
			int varScope = this.varHelper.StartVariables();
			QilIterator qilIterator = this.f.Let(this.CompileNodeSetExpression(node.Select));
			this.varHelper.AddVariable(qilIterator);
			for (int i = 0; i < content.Count; i++)
			{
				VarPar varPar = content[i] as VarPar;
				if (varPar != null)
				{
					this.CompileWithParam(varPar);
					QilNode value = varPar.Value;
					if (this.IsDebug || (!(value is QilIterator) && !(value is QilLiteral)))
					{
						QilIterator qilIterator2 = this.f.Let(value);
						qilIterator2.DebugName = this.f.QName("with-param " + varPar.Name.QualifiedName, "urn:schemas-microsoft-com:xslt-debug").ToString();
						this.varHelper.AddVariable(qilIterator2);
						varPar.Value = qilIterator2;
					}
				}
			}
			LoopFocus loopFocus = this.curLoop;
			QilIterator focus = this.f.For(qilIterator);
			this.curLoop.SetFocus(focus);
			this.curLoop.Sort(this.CompileSorts(content, ref loopFocus));
			QilNode qilNode = this.GenerateApply(this.compiler.Root, node);
			qilNode = this.WrapLoopBody(node.ElemNameLi, qilNode, node.EndTagLi);
			qilNode = this.AddCurrentPositionLast(qilNode);
			qilNode = this.curLoop.ConstructLoop(qilNode);
			this.curLoop = loopFocus;
			return this.varHelper.FinishVariables(qilNode, varScope);
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000EEFD9 File Offset: 0x000ED1D9
		private QilNode CompileApplyImports(XslNode node)
		{
			return this.GenerateApply((StylesheetLevel)node.Arg, node);
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x000EEFF0 File Offset: 0x000ED1F0
		private QilNode CompileCallTemplate(XslNodeEx node)
		{
			int varScope = this.varHelper.StartVariables();
			IList<XslNode> content = node.Content;
			foreach (XslNode xslNode in content)
			{
				VarPar varPar = (VarPar)xslNode;
				this.CompileWithParam(varPar);
				if (this.IsDebug)
				{
					QilNode value = varPar.Value;
					QilIterator qilIterator = this.f.Let(value);
					qilIterator.DebugName = this.f.QName("with-param " + varPar.Name.QualifiedName, "urn:schemas-microsoft-com:xslt-debug").ToString();
					this.varHelper.AddVariable(qilIterator);
					varPar.Value = qilIterator;
				}
			}
			Template template;
			QilNode qilNode;
			if (this.compiler.NamedTemplates.TryGetValue(node.Name, out template))
			{
				qilNode = this.invkGen.GenerateInvoke(template.Function, this.AddRemoveImplicitArgs(node.Content, template.Flags));
			}
			else
			{
				if (!this.compiler.IsPhantomName(node.Name))
				{
					this.compiler.ReportError(node.SourceLine, "The named template '{0}' does not exist.", new string[]
					{
						node.Name.QualifiedName
					});
				}
				qilNode = this.f.Sequence();
			}
			if (content.Count > 0)
			{
				qilNode = QilGenerator.SetLineInfo(qilNode, node.ElemNameLi);
			}
			qilNode = this.varHelper.FinishVariables(qilNode, varScope);
			if (this.IsDebug)
			{
				return this.f.Nop(qilNode);
			}
			return qilNode;
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x000EF184 File Offset: 0x000ED384
		private QilNode CompileUseAttributeSet(XslNode node)
		{
			this.outputScope.InvalidateAllPrefixes();
			AttributeSet attributeSet;
			if (this.compiler.AttributeSets.TryGetValue(node.Name, out attributeSet))
			{
				return this.invkGen.GenerateInvoke(attributeSet.Function, this.AddRemoveImplicitArgs(node.Content, attributeSet.Flags));
			}
			if (!this.compiler.IsPhantomName(node.Name))
			{
				this.compiler.ReportError(node.SourceLine, "A reference to attribute set '{0}' cannot be resolved. An 'xsl:attribute-set' of this name must be declared at the top level of the stylesheet.", new string[]
				{
					node.Name.QualifiedName
				});
			}
			return this.f.Sequence();
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x000EF224 File Offset: 0x000ED424
		private QilNode CompileCopy(XslNode copy)
		{
			QilNode currentNode = this.GetCurrentNode();
			if ((currentNode.XmlType.NodeKinds & (XmlNodeKindFlags.Attribute | XmlNodeKindFlags.Namespace)) != XmlNodeKindFlags.None)
			{
				this.outputScope.InvalidateAllPrefixes();
			}
			if (currentNode.XmlType.NodeKinds == XmlNodeKindFlags.Element)
			{
				QilList qilList = this.InstructionList();
				qilList.Add(this.f.XPathNamespace(currentNode));
				this.outputScope.PushScope();
				this.outputScope.InvalidateAllPrefixes();
				QilNode content = this.CompileInstructions(copy.Content, qilList);
				this.outputScope.PopScope();
				return this.f.ElementCtor(this.f.NameOf(currentNode), content);
			}
			if (currentNode.XmlType.NodeKinds == XmlNodeKindFlags.Document)
			{
				return this.CompileInstructions(copy.Content);
			}
			if ((currentNode.XmlType.NodeKinds & (XmlNodeKindFlags.Document | XmlNodeKindFlags.Element)) == XmlNodeKindFlags.None)
			{
				return currentNode;
			}
			return this.f.XsltCopy(currentNode, this.CompileInstructions(copy.Content));
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x000EF308 File Offset: 0x000ED508
		private QilNode CompileCopyOf(XslNode node)
		{
			QilNode qilNode = this.CompileXPathExpression(node.Select);
			if (qilNode.XmlType.IsNode)
			{
				if ((qilNode.XmlType.NodeKinds & (XmlNodeKindFlags.Attribute | XmlNodeKindFlags.Namespace)) != XmlNodeKindFlags.None)
				{
					this.outputScope.InvalidateAllPrefixes();
				}
				if (qilNode.XmlType.IsNotRtf && (qilNode.XmlType.NodeKinds & XmlNodeKindFlags.Document) == XmlNodeKindFlags.None)
				{
					return qilNode;
				}
				if (qilNode.XmlType.IsSingleton)
				{
					return this.f.XsltCopyOf(qilNode);
				}
				QilIterator expr;
				return this.f.Loop(expr = this.f.For(qilNode), this.f.XsltCopyOf(expr));
			}
			else
			{
				if (qilNode.XmlType.IsAtomicValue)
				{
					return this.f.TextCtor(this.f.ConvertToString(qilNode));
				}
				this.outputScope.InvalidateAllPrefixes();
				QilIterator expr2;
				return this.f.Loop(expr2 = this.f.For(qilNode), this.f.Conditional(this.f.IsType(expr2, XmlQueryTypeFactory.Node), this.f.XsltCopyOf(this.f.TypeAssert(expr2, XmlQueryTypeFactory.Node)), this.f.TextCtor(this.f.XsltConvert(expr2, XmlQueryTypeFactory.StringX))));
			}
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x000EF445 File Offset: 0x000ED645
		private QilNode CompileValueOf(XslNode valueOf)
		{
			return this.f.TextCtor(this.f.ConvertToString(this.CompileXPathExpression(valueOf.Select)));
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x000EF469 File Offset: 0x000ED669
		private QilNode CompileValueOfDoe(XslNode valueOf)
		{
			return this.f.RawTextCtor(this.f.ConvertToString(this.CompileXPathExpression(valueOf.Select)));
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000EF48D File Offset: 0x000ED68D
		private QilNode CompileWhen(XslNode whenNode, QilNode otherwise)
		{
			return this.f.Conditional(this.f.ConvertToBoolean(this.CompileXPathExpression(whenNode.Select)), this.CompileInstructions(whenNode.Content), otherwise);
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x000EF4BE File Offset: 0x000ED6BE
		private QilNode CompileIf(XslNode ifNode)
		{
			return this.CompileWhen(ifNode, this.InstructionList());
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x000EF4D0 File Offset: 0x000ED6D0
		private QilNode CompileChoose(XslNode node)
		{
			IList<XslNode> content = node.Content;
			QilNode qilNode = null;
			int num = content.Count - 1;
			while (0 <= num)
			{
				XslNode xslNode = content[num];
				QilList nsList = this.EnterScope(xslNode);
				if (xslNode.NodeType == XslNodeType.Otherwise)
				{
					qilNode = this.CompileInstructions(xslNode.Content);
				}
				else
				{
					qilNode = this.CompileWhen(xslNode, qilNode ?? this.InstructionList());
				}
				this.ExitScope();
				this.SetLineInfoCheck(qilNode, xslNode.SourceLine);
				qilNode = this.SetDebugNs(qilNode, nsList);
				num--;
			}
			if (qilNode == null)
			{
				return this.f.Sequence();
			}
			if (!this.IsDebug)
			{
				return qilNode;
			}
			return this.f.Sequence(qilNode);
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000EF578 File Offset: 0x000ED778
		private QilNode CompileMessage(XslNode node)
		{
			string uri = this.lastScope.SourceLine.Uri;
			QilNode qilNode = this.f.RtfCtor(this.CompileInstructions(node.Content), this.f.String(uri));
			qilNode = this.f.InvokeOuterXml(qilNode);
			if (!(bool)node.Arg)
			{
				return this.f.Warning(qilNode);
			}
			QilIterator text;
			return this.f.Loop(text = this.f.Let(qilNode), this.f.Sequence(this.f.Warning(text), this.f.Error(text)));
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x000EF620 File Offset: 0x000ED820
		private QilNode CompileVariable(XslNode node)
		{
			if (this.scope.IsLocalVariable(node.Name.LocalName, node.Name.NamespaceUri))
			{
				this.ReportError("The variable or parameter '{0}' was duplicated within the same scope.", new string[]
				{
					node.Name.QualifiedName
				});
			}
			return this.CompileVarParValue(node);
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x000EF678 File Offset: 0x000ED878
		private QilNode CompileVarParValue(XslNode node)
		{
			string uri = this.lastScope.SourceLine.Uri;
			IList<XslNode> content = node.Content;
			string select = node.Select;
			QilNode qilNode;
			if (select != null)
			{
				QilList qilList = this.InstructionList();
				qilList.Add(this.CompileXPathExpression(select));
				qilNode = this.CompileInstructions(content, qilList);
			}
			else if (content.Count != 0)
			{
				this.outputScope.PushScope();
				this.outputScope.InvalidateAllPrefixes();
				qilNode = this.f.RtfCtor(this.CompileInstructions(content), this.f.String(uri));
				this.outputScope.PopScope();
			}
			else
			{
				qilNode = this.f.String(string.Empty);
			}
			if (this.IsDebug)
			{
				qilNode = this.f.TypeAssert(qilNode, XmlQueryTypeFactory.ItemS);
			}
			return qilNode;
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x000EF740 File Offset: 0x000ED940
		private void CompileWithParam(VarPar withParam)
		{
			QilList nsList = this.EnterScope(withParam);
			QilNode qilNode = this.CompileVarParValue(withParam);
			this.ExitScope();
			QilGenerator.SetLineInfo(qilNode, withParam.SourceLine);
			qilNode = this.SetDebugNs(qilNode, nsList);
			withParam.Value = qilNode;
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x000EF780 File Offset: 0x000ED980
		private QilNode CompileSorts(IList<XslNode> content, ref LoopFocus parentLoop)
		{
			QilList qilList = this.f.BaseFactory.SortKeyList();
			int i = 0;
			while (i < content.Count)
			{
				Sort sort = content[i] as Sort;
				if (sort != null)
				{
					this.CompileSort(sort, qilList, ref parentLoop);
					content.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
			if (qilList.Count == 0)
			{
				return null;
			}
			return qilList;
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x000EF7DC File Offset: 0x000ED9DC
		private QilNode CompileLangAttribute(string attValue, bool fwdCompat)
		{
			QilNode qilNode = this.CompileStringAvt(attValue);
			if (qilNode != null)
			{
				if (qilNode.NodeType == QilNodeType.LiteralString)
				{
					if (XsltLibrary.LangToLcidInternal((QilLiteral)qilNode, fwdCompat, this) == 127)
					{
						qilNode = null;
					}
				}
				else
				{
					QilIterator qilIterator;
					qilNode = this.f.Loop(qilIterator = this.f.Let(qilNode), this.f.Conditional(this.f.Eq(this.f.InvokeLangToLcid(qilIterator, fwdCompat), this.f.Int32(127)), this.f.String(string.Empty), qilIterator));
				}
			}
			return qilNode;
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x000EF874 File Offset: 0x000EDA74
		private QilNode CompileLangAttributeToLcid(string attValue, bool fwdCompat)
		{
			return this.CompileLangToLcid(this.CompileStringAvt(attValue), fwdCompat);
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x000EF884 File Offset: 0x000EDA84
		private QilNode CompileLangToLcid(QilNode lang, bool fwdCompat)
		{
			if (lang == null)
			{
				return this.f.Double(127.0);
			}
			if (lang.NodeType == QilNodeType.LiteralString)
			{
				return this.f.Double((double)XsltLibrary.LangToLcidInternal((QilLiteral)lang, fwdCompat, this));
			}
			return this.f.XsltConvert(this.f.InvokeLangToLcid(lang, fwdCompat), XmlQueryTypeFactory.DoubleX);
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x000EF8F0 File Offset: 0x000EDAF0
		private void CompileDataTypeAttribute(string attValue, bool fwdCompat, ref QilNode select, out QilNode select2)
		{
			QilNode qilNode = this.CompileStringAvt(attValue);
			if (qilNode != null)
			{
				if (qilNode.NodeType != QilNodeType.LiteralString)
				{
					QilIterator qilIterator;
					qilNode = this.f.Loop(qilIterator = this.f.Let(qilNode), this.f.Conditional(this.f.Eq(qilIterator, this.f.String("number")), this.f.False(), this.f.Conditional(this.f.Eq(qilIterator, this.f.String("text")), this.f.True(), fwdCompat ? this.f.True() : this.f.Loop(this.f.Let(this.ResolveQNameDynamic(true, qilIterator)), this.f.Error(this.lastScope.SourceLine, "The value of the '{0}' attribute must be '{1}' or '{2}'.", new string[]
					{
						"data-type",
						"text",
						"number"
					})))));
					QilIterator qilIterator2 = this.f.Let(qilNode);
					this.varHelper.AddVariable(qilIterator2);
					select2 = select.DeepClone(this.f.BaseFactory);
					select = this.f.Conditional(qilIterator2, this.f.ConvertToString(select), this.f.String(string.Empty));
					select2 = this.f.Conditional(qilIterator2, this.f.Double(0.0), this.f.ConvertToNumber(select2));
					return;
				}
				string text = (QilLiteral)qilNode;
				if (text == "number")
				{
					select = this.f.ConvertToNumber(select);
					select2 = null;
					return;
				}
				if (!(text == "text") && !fwdCompat)
				{
					string prefix;
					string text2;
					int length = (this.compiler.ParseQName(text, out prefix, out text2, this) ? this.ResolvePrefix(true, prefix) : this.compiler.CreatePhantomNamespace()).Length;
					this.ReportError("The value of the '{0}' attribute must be '{1}' or '{2}'.", new string[]
					{
						"data-type",
						"text",
						"number"
					});
				}
			}
			select = this.f.ConvertToString(select);
			select2 = null;
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x000EFB48 File Offset: 0x000EDD48
		private QilNode CompileOrderAttribute(string attName, string attValue, string value0, string value1, bool fwdCompat)
		{
			QilNode qilNode = this.CompileStringAvt(attValue);
			if (qilNode != null)
			{
				if (qilNode.NodeType == QilNodeType.LiteralString)
				{
					string a = (QilLiteral)qilNode;
					if (a == value1)
					{
						qilNode = this.f.String("1");
					}
					else
					{
						if (a != value0 && !fwdCompat)
						{
							this.ReportError("The value of the '{0}' attribute must be '{1}' or '{2}'.", new string[]
							{
								attName,
								value0,
								value1
							});
						}
						qilNode = this.f.String("0");
					}
				}
				else
				{
					QilIterator left;
					qilNode = this.f.Loop(left = this.f.Let(qilNode), this.f.Conditional(this.f.Eq(left, this.f.String(value1)), this.f.String("1"), fwdCompat ? this.f.String("0") : this.f.Conditional(this.f.Eq(left, this.f.String(value0)), this.f.String("0"), this.f.Error(this.lastScope.SourceLine, "The value of the '{0}' attribute must be '{1}' or '{2}'.", new string[]
					{
						attName,
						value0,
						value1
					}))));
				}
			}
			return qilNode;
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x000EFCA0 File Offset: 0x000EDEA0
		private void CompileSort(Sort sort, QilList keyList, ref LoopFocus parentLoop)
		{
			this.EnterScope(sort);
			bool forwardsCompatible = sort.ForwardsCompatible;
			QilNode qilNode = this.CompileXPathExpression(sort.Select);
			QilNode value;
			QilNode qilNode2;
			QilNode qilNode3;
			QilNode qilNode4;
			if (sort.Lang != null || sort.DataType != null || sort.Order != null || sort.CaseOrder != null)
			{
				LoopFocus loopFocus = this.curLoop;
				this.curLoop = parentLoop;
				value = this.CompileLangAttribute(sort.Lang, forwardsCompatible);
				this.CompileDataTypeAttribute(sort.DataType, forwardsCompatible, ref qilNode, out qilNode2);
				qilNode3 = this.CompileOrderAttribute("order", sort.Order, "ascending", "descending", forwardsCompatible);
				qilNode4 = this.CompileOrderAttribute("case-order", sort.CaseOrder, "lower-first", "upper-first", forwardsCompatible);
				this.curLoop = loopFocus;
			}
			else
			{
				qilNode = this.f.ConvertToString(qilNode);
				value = (qilNode2 = (qilNode3 = (qilNode4 = null)));
			}
			this.strConcat.Reset();
			this.strConcat.Append("http://collations.microsoft.com");
			this.strConcat.Append('/');
			this.strConcat.Append(value);
			char value2 = '?';
			if (qilNode3 != null)
			{
				this.strConcat.Append(value2);
				this.strConcat.Append("descendingOrder=");
				this.strConcat.Append(qilNode3);
				value2 = '&';
			}
			if (qilNode4 != null)
			{
				this.strConcat.Append(value2);
				this.strConcat.Append("upperFirst=");
				this.strConcat.Append(qilNode4);
			}
			QilNode qilNode5 = this.strConcat.ToQil();
			QilSortKey node = this.f.SortKey(qilNode, qilNode5);
			keyList.Add(node);
			if (qilNode2 != null)
			{
				node = this.f.SortKey(qilNode2, qilNode5.DeepClone(this.f.BaseFactory));
				keyList.Add(node);
			}
			this.ExitScope();
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x000EFE6C File Offset: 0x000EE06C
		private QilNode MatchPattern(QilNode pattern, QilIterator testNode)
		{
			if (pattern.NodeType == QilNodeType.Error)
			{
				return pattern;
			}
			QilList qilList;
			if (pattern.NodeType == QilNodeType.Sequence)
			{
				qilList = (QilList)pattern;
			}
			else
			{
				qilList = this.f.BaseFactory.Sequence();
				qilList.Add(pattern);
			}
			QilNode qilNode = this.f.False();
			int num = qilList.Count - 1;
			while (0 <= num)
			{
				QilLoop qilLoop = (QilLoop)qilList[num];
				qilNode = this.f.Or(this.refReplacer.Replace(qilLoop.Body, qilLoop.Variable, testNode), qilNode);
				num--;
			}
			return qilNode;
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x000EFF04 File Offset: 0x000EE104
		private QilNode MatchCountPattern(QilNode countPattern, QilIterator testNode)
		{
			if (countPattern != null)
			{
				return this.MatchPattern(countPattern, testNode);
			}
			QilNode currentNode = this.GetCurrentNode();
			XmlNodeKindFlags nodeKinds = currentNode.XmlType.NodeKinds;
			if ((nodeKinds & nodeKinds - 1) != XmlNodeKindFlags.None)
			{
				return this.f.InvokeIsSameNodeSort(testNode, currentNode);
			}
			if (nodeKinds <= XmlNodeKindFlags.Text)
			{
				QilNode left;
				switch (nodeKinds)
				{
				case XmlNodeKindFlags.Document:
					return this.f.IsType(testNode, XmlQueryTypeFactory.Document);
				case XmlNodeKindFlags.Element:
					left = this.f.IsType(testNode, XmlQueryTypeFactory.Element);
					break;
				case XmlNodeKindFlags.Document | XmlNodeKindFlags.Element:
					goto IL_154;
				case XmlNodeKindFlags.Attribute:
					left = this.f.IsType(testNode, XmlQueryTypeFactory.Attribute);
					break;
				default:
					if (nodeKinds != XmlNodeKindFlags.Text)
					{
						goto IL_154;
					}
					return this.f.IsType(testNode, XmlQueryTypeFactory.Text);
				}
				return this.f.And(left, this.f.And(this.f.Eq(this.f.LocalNameOf(testNode), this.f.LocalNameOf(currentNode)), this.f.Eq(this.f.NamespaceUriOf(testNode), this.f.NamespaceUriOf(this.GetCurrentNode()))));
			}
			if (nodeKinds == XmlNodeKindFlags.Comment)
			{
				return this.f.IsType(testNode, XmlQueryTypeFactory.Comment);
			}
			if (nodeKinds == XmlNodeKindFlags.PI)
			{
				return this.f.And(this.f.IsType(testNode, XmlQueryTypeFactory.PI), this.f.Eq(this.f.LocalNameOf(testNode), this.f.LocalNameOf(currentNode)));
			}
			if (nodeKinds == XmlNodeKindFlags.Namespace)
			{
				return this.f.And(this.f.IsType(testNode, XmlQueryTypeFactory.Namespace), this.f.Eq(this.f.LocalNameOf(testNode), this.f.LocalNameOf(currentNode)));
			}
			IL_154:
			return this.f.False();
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x000F00D4 File Offset: 0x000EE2D4
		private QilNode PlaceMarker(QilNode countPattern, QilNode fromPattern, bool multiple)
		{
			QilNode countPattern2 = (countPattern != null) ? countPattern.DeepClone(this.f.BaseFactory) : null;
			QilIterator qilIterator;
			QilNode qilNode = this.f.Filter(qilIterator = this.f.For(this.f.AncestorOrSelf(this.GetCurrentNode())), this.MatchCountPattern(countPattern, qilIterator));
			QilNode qilNode2;
			if (multiple)
			{
				qilNode2 = this.f.DocOrderDistinct(qilNode);
			}
			else
			{
				qilNode2 = this.f.Filter(qilIterator = this.f.For(qilNode), this.f.Eq(this.f.PositionOf(qilIterator), this.f.Int32(1)));
			}
			QilNode binding;
			QilIterator qilIterator2;
			if (fromPattern == null)
			{
				binding = qilNode2;
			}
			else
			{
				QilNode binding2 = this.f.Filter(qilIterator = this.f.For(this.f.AncestorOrSelf(this.GetCurrentNode())), this.MatchPattern(fromPattern, qilIterator));
				QilNode binding3 = this.f.Filter(qilIterator = this.f.For(binding2), this.f.Eq(this.f.PositionOf(qilIterator), this.f.Int32(1)));
				binding = this.f.Loop(qilIterator = this.f.For(binding3), this.f.Filter(qilIterator2 = this.f.For(qilNode2), this.f.Before(qilIterator, qilIterator2)));
			}
			return this.f.Loop(qilIterator2 = this.f.For(binding), this.f.Add(this.f.Int32(1), this.f.Length(this.f.Filter(qilIterator = this.f.For(this.f.PrecedingSibling(qilIterator2)), this.MatchCountPattern(countPattern2, qilIterator)))));
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x000F02B0 File Offset: 0x000EE4B0
		private QilNode PlaceMarkerAny(QilNode countPattern, QilNode fromPattern)
		{
			QilNode child;
			QilIterator qilIterator2;
			if (fromPattern == null)
			{
				QilNode binding = this.f.NodeRange(this.f.Root(this.GetCurrentNode()), this.GetCurrentNode());
				QilIterator qilIterator;
				child = this.f.Filter(qilIterator = this.f.For(binding), this.MatchCountPattern(countPattern, qilIterator));
			}
			else
			{
				QilIterator qilIterator;
				QilNode binding2 = this.f.Filter(qilIterator = this.f.For(this.f.Preceding(this.GetCurrentNode())), this.MatchPattern(fromPattern, qilIterator));
				QilNode binding3 = this.f.Filter(qilIterator = this.f.For(binding2), this.f.Eq(this.f.PositionOf(qilIterator), this.f.Int32(1)));
				QilIterator right;
				child = this.f.Loop(qilIterator = this.f.For(binding3), this.f.Filter(right = this.f.For(this.f.Filter(qilIterator2 = this.f.For(this.f.NodeRange(qilIterator, this.GetCurrentNode())), this.MatchCountPattern(countPattern, qilIterator2))), this.f.Not(this.f.Is(qilIterator, right))));
			}
			return this.f.Loop(qilIterator2 = this.f.Let(this.f.Length(child)), this.f.Conditional(this.f.Eq(qilIterator2, this.f.Int32(0)), this.f.Sequence(), qilIterator2));
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x000F0458 File Offset: 0x000EE658
		private QilNode CompileLetterValueAttribute(string attValue, bool fwdCompat)
		{
			QilNode qilNode = this.CompileStringAvt(attValue);
			if (qilNode == null)
			{
				return this.f.String("default");
			}
			if (qilNode.NodeType == QilNodeType.LiteralString)
			{
				string a = (QilLiteral)qilNode;
				if (a != "alphabetic" && a != "traditional")
				{
					if (fwdCompat)
					{
						return this.f.String("default");
					}
					this.ReportError("The value of the '{0}' attribute must be '{1}' or '{2}'.", new string[]
					{
						"letter-value",
						"alphabetic",
						"traditional"
					});
				}
				return qilNode;
			}
			QilIterator qilIterator = this.f.Let(qilNode);
			return this.f.Loop(qilIterator, this.f.Conditional(this.f.Or(this.f.Eq(qilIterator, this.f.String("alphabetic")), this.f.Eq(qilIterator, this.f.String("traditional"))), qilIterator, fwdCompat ? this.f.String("default") : this.f.Error(this.lastScope.SourceLine, "The value of the '{0}' attribute must be '{1}' or '{2}'.", new string[]
			{
				"letter-value",
				"alphabetic",
				"traditional"
			})));
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x000F05AC File Offset: 0x000EE7AC
		private QilNode CompileGroupingSeparatorAttribute(string attValue, bool fwdCompat)
		{
			QilNode qilNode = this.CompileStringAvt(attValue);
			if (qilNode == null)
			{
				qilNode = this.f.String(string.Empty);
			}
			else if (qilNode.NodeType == QilNodeType.LiteralString)
			{
				if (((QilLiteral)qilNode).Length != 1)
				{
					if (!fwdCompat)
					{
						this.ReportError("The value of the '{0}' attribute must be a single character.", new string[]
						{
							"grouping-separator"
						});
					}
					qilNode = this.f.String(string.Empty);
				}
			}
			else
			{
				QilIterator qilIterator = this.f.Let(qilNode);
				qilNode = this.f.Loop(qilIterator, this.f.Conditional(this.f.Eq(this.f.StrLength(qilIterator), this.f.Int32(1)), qilIterator, fwdCompat ? this.f.String(string.Empty) : this.f.Error(this.lastScope.SourceLine, "The value of the '{0}' attribute must be a single character.", new string[]
				{
					"grouping-separator"
				})));
			}
			return qilNode;
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000F06B4 File Offset: 0x000EE8B4
		private QilNode CompileGroupingSizeAttribute(string attValue, bool fwdCompat)
		{
			QilNode qilNode = this.CompileStringAvt(attValue);
			if (qilNode == null)
			{
				return this.f.Double(0.0);
			}
			if (qilNode.NodeType != QilNodeType.LiteralString)
			{
				QilIterator qilIterator = this.f.Let(this.f.ConvertToNumber(qilNode));
				return this.f.Loop(qilIterator, this.f.Conditional(this.f.And(this.f.Lt(this.f.Double(0.0), qilIterator), this.f.Lt(qilIterator, this.f.Double(2147483647.0))), qilIterator, this.f.Double(0.0)));
			}
			double num = XsltFunctions.Round(XPathConvert.StringToDouble((QilLiteral)qilNode));
			if (0.0 <= num && num <= 2147483647.0)
			{
				return this.f.Double(num);
			}
			return this.f.Double(0.0);
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000F07CC File Offset: 0x000EE9CC
		private QilNode CompileNumber(Number num)
		{
			QilNode value;
			if (num.Value != null)
			{
				value = this.f.ConvertToNumber(this.CompileXPathExpression(num.Value));
			}
			else
			{
				QilNode countPattern = (num.Count != null) ? this.CompileNumberPattern(num.Count) : null;
				QilNode fromPattern = (num.From != null) ? this.CompileNumberPattern(num.From) : null;
				NumberLevel level = num.Level;
				if (level != NumberLevel.Single)
				{
					if (level != NumberLevel.Multiple)
					{
						value = this.PlaceMarkerAny(countPattern, fromPattern);
					}
					else
					{
						value = this.PlaceMarker(countPattern, fromPattern, true);
					}
				}
				else
				{
					value = this.PlaceMarker(countPattern, fromPattern, false);
				}
			}
			bool forwardsCompatible = num.ForwardsCompatible;
			return this.f.TextCtor(this.f.InvokeNumberFormat(value, this.CompileStringAvt(num.Format), this.CompileLangAttributeToLcid(num.Lang, forwardsCompatible), this.CompileLetterValueAttribute(num.LetterValue, forwardsCompatible), this.CompileGroupingSeparatorAttribute(num.GroupingSeparator, forwardsCompatible), this.CompileGroupingSizeAttribute(num.GroupingSize, forwardsCompatible)));
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x000F08C0 File Offset: 0x000EEAC0
		private void CompileAndSortMatches(Stylesheet sheet)
		{
			foreach (Template template in sheet.Templates)
			{
				if (template.Match != null)
				{
					this.EnterScope(template);
					QilNode qilNode = this.CompileMatchPattern(template.Match);
					if (qilNode.NodeType == QilNodeType.Sequence)
					{
						QilList qilList = (QilList)qilNode;
						for (int i = 0; i < qilList.Count; i++)
						{
							sheet.AddTemplateMatch(template, (QilLoop)qilList[i]);
						}
					}
					else
					{
						sheet.AddTemplateMatch(template, (QilLoop)qilNode);
					}
					this.ExitScope();
				}
			}
			sheet.SortTemplateMatches();
			foreach (Stylesheet sheet2 in sheet.Imports)
			{
				this.CompileAndSortMatches(sheet2);
			}
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x000F09A8 File Offset: 0x000EEBA8
		private void CompileKeys()
		{
			for (int i = 0; i < this.compiler.Keys.Count; i++)
			{
				foreach (Key key in this.compiler.Keys[i])
				{
					this.EnterScope(key);
					QilParameter qilParameter = this.f.Parameter(XmlQueryTypeFactory.NodeNotRtf);
					this.singlFocus.SetFocus(qilParameter);
					QilIterator qilIterator = this.f.For(this.f.OptimizeBarrier(this.CompileKeyMatch(key.Match)));
					this.singlFocus.SetFocus(qilIterator);
					QilIterator qilIterator2 = this.f.For(this.CompileKeyUse(key));
					qilIterator2 = this.f.For(this.f.OptimizeBarrier(this.f.Loop(qilIterator2, this.f.ConvertToString(qilIterator2))));
					QilParameter qilParameter2 = this.f.Parameter(XmlQueryTypeFactory.StringX);
					QilFunction qilFunction = this.f.Function(this.f.FormalParameterList(qilParameter, qilParameter2), this.f.Filter(qilIterator, this.f.Not(this.f.IsEmpty(this.f.Filter(qilIterator2, this.f.Eq(qilIterator2, qilParameter2))))), this.f.False());
					qilFunction.DebugName = key.GetDebugName();
					QilGenerator.SetLineInfo(qilFunction, key.SourceLine);
					key.Function = qilFunction;
					this.functions.Add(qilFunction);
					this.ExitScope();
				}
			}
			this.singlFocus.SetFocus(null);
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000F0B88 File Offset: 0x000EED88
		private void CreateGlobalVarPars()
		{
			foreach (VarPar varPar in this.compiler.ExternalPars)
			{
				this.CreateGlobalVarPar(varPar);
			}
			foreach (VarPar varPar2 in this.compiler.GlobalVars)
			{
				this.CreateGlobalVarPar(varPar2);
			}
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x000F0C28 File Offset: 0x000EEE28
		private void CreateGlobalVarPar(VarPar varPar)
		{
			XmlQueryType t = this.ChooseBestType(varPar);
			QilIterator qilIterator;
			if (varPar.NodeType == XslNodeType.Variable)
			{
				qilIterator = this.f.Let(this.f.Unknown(t));
			}
			else
			{
				qilIterator = this.f.Parameter(null, varPar.Name, t);
			}
			qilIterator.DebugName = varPar.Name.ToString();
			varPar.Value = qilIterator;
			QilGenerator.SetLineInfo(qilIterator, varPar.SourceLine);
			this.scope.AddVariable(varPar.Name, qilIterator);
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x000F0CAC File Offset: 0x000EEEAC
		private void CompileGlobalVariables()
		{
			this.singlFocus.SetFocus(SingletonFocusType.InitialDocumentNode);
			foreach (VarPar varPar in this.compiler.ExternalPars)
			{
				this.extPars.Add(this.CompileGlobalVarPar(varPar));
			}
			foreach (VarPar varPar2 in this.compiler.GlobalVars)
			{
				this.gloVars.Add(this.CompileGlobalVarPar(varPar2));
			}
			this.singlFocus.SetFocus(null);
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x000F0D7C File Offset: 0x000EEF7C
		private QilIterator CompileGlobalVarPar(VarPar varPar)
		{
			QilIterator qilIterator = (QilIterator)varPar.Value;
			QilList nsList = this.EnterScope(varPar);
			QilNode qilNode = this.CompileVarParValue(varPar);
			QilGenerator.SetLineInfo(qilNode, qilIterator.SourceLine);
			qilNode = this.AddCurrentPositionLast(qilNode);
			qilNode = this.SetDebugNs(qilNode, nsList);
			qilIterator.SourceLine = SourceLineInfo.NoSource;
			qilIterator.Binding = qilNode;
			this.ExitScope();
			return qilIterator;
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x000F0DDC File Offset: 0x000EEFDC
		private void ReportErrorInXPath(XslLoadException e)
		{
			XPathCompileException ex = e as XPathCompileException;
			string text = (ex != null) ? ex.FormatDetailedMessage() : e.Message;
			this.compiler.ReportError(this.lastScope.SourceLine, "{0}", new string[]
			{
				text
			});
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x000F0E27 File Offset: 0x000EF027
		private QilNode PhantomXPathExpression()
		{
			return this.f.TypeAssert(this.f.Sequence(), XmlQueryTypeFactory.ItemS);
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x000F0E44 File Offset: 0x000EF044
		private QilNode PhantomKeyMatch()
		{
			return this.f.TypeAssert(this.f.Sequence(), XmlQueryTypeFactory.NodeNotRtfS);
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x000F0E64 File Offset: 0x000EF064
		private QilNode CompileXPathExpression(string expr)
		{
			this.SetEnvironmentFlags(true, true, true);
			QilNode qilNode;
			if (expr == null)
			{
				qilNode = this.PhantomXPathExpression();
			}
			else
			{
				try
				{
					XPathScanner scanner = new XPathScanner(expr);
					qilNode = this.xpathParser.Parse(scanner, this.xpathBuilder, LexKind.Eof);
				}
				catch (XslLoadException ex)
				{
					if (this.xslVersion != XslVersion.ForwardsCompatible)
					{
						this.ReportErrorInXPath(ex);
					}
					qilNode = this.f.Error(this.f.String(ex.Message));
				}
			}
			if (qilNode is QilIterator)
			{
				qilNode = this.f.Nop(qilNode);
			}
			return qilNode;
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x000F0EFC File Offset: 0x000EF0FC
		private QilNode CompileNodeSetExpression(string expr)
		{
			QilNode qilNode = this.f.TryEnsureNodeSet(this.CompileXPathExpression(expr));
			if (qilNode == null)
			{
				XPathCompileException ex = new XPathCompileException(expr, 0, expr.Length, "Expression must evaluate to a node-set.", null);
				if (this.xslVersion != XslVersion.ForwardsCompatible)
				{
					this.ReportErrorInXPath(ex);
				}
				qilNode = this.f.Error(this.f.String(ex.Message));
			}
			return qilNode;
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x000F0F64 File Offset: 0x000EF164
		private QilNode CompileXPathExpressionWithinAvt(string expr, ref int pos)
		{
			this.SetEnvironmentFlags(true, true, true);
			QilNode qilNode;
			try
			{
				XPathScanner xpathScanner = new XPathScanner(expr, pos);
				qilNode = this.xpathParser.Parse(xpathScanner, this.xpathBuilder, LexKind.RBrace);
				pos = xpathScanner.LexStart + 1;
			}
			catch (XslLoadException ex)
			{
				if (this.xslVersion != XslVersion.ForwardsCompatible)
				{
					this.ReportErrorInXPath(ex);
				}
				qilNode = this.f.Error(this.f.String(ex.Message));
				pos = expr.Length;
			}
			if (qilNode is QilIterator)
			{
				qilNode = this.f.Nop(qilNode);
			}
			return qilNode;
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x000F1004 File Offset: 0x000EF204
		private QilNode CompileMatchPattern(string pttrn)
		{
			this.SetEnvironmentFlags(false, false, true);
			QilNode qilNode;
			try
			{
				XPathScanner scanner = new XPathScanner(pttrn);
				qilNode = this.ptrnParser.Parse(scanner, this.ptrnBuilder);
			}
			catch (XslLoadException ex)
			{
				if (this.xslVersion != XslVersion.ForwardsCompatible)
				{
					this.ReportErrorInXPath(ex);
				}
				qilNode = this.f.Loop(this.f.For(this.ptrnBuilder.FixupNode), this.f.Error(this.f.String(ex.Message)));
				XPathPatternBuilder.SetPriority(qilNode, 0.5);
			}
			return qilNode;
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x000F10A8 File Offset: 0x000EF2A8
		private QilNode CompileNumberPattern(string pttrn)
		{
			this.SetEnvironmentFlags(true, false, true);
			QilNode result;
			try
			{
				XPathScanner scanner = new XPathScanner(pttrn);
				result = this.ptrnParser.Parse(scanner, this.ptrnBuilder);
			}
			catch (XslLoadException ex)
			{
				if (this.xslVersion != XslVersion.ForwardsCompatible)
				{
					this.ReportErrorInXPath(ex);
				}
				result = this.f.Error(this.f.String(ex.Message));
			}
			return result;
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x000F111C File Offset: 0x000EF31C
		private QilNode CompileKeyMatch(string pttrn)
		{
			if (this.keyMatchBuilder == null)
			{
				this.keyMatchBuilder = new KeyMatchBuilder(this);
			}
			this.SetEnvironmentFlags(false, false, false);
			QilNode result;
			if (pttrn == null)
			{
				result = this.PhantomKeyMatch();
			}
			else
			{
				try
				{
					XPathScanner scanner = new XPathScanner(pttrn);
					result = this.ptrnParser.Parse(scanner, this.keyMatchBuilder);
				}
				catch (XslLoadException ex)
				{
					if (this.xslVersion != XslVersion.ForwardsCompatible)
					{
						this.ReportErrorInXPath(ex);
					}
					result = this.f.Error(this.f.String(ex.Message));
				}
			}
			return result;
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x000F11B0 File Offset: 0x000EF3B0
		private QilNode CompileKeyUse(Key key)
		{
			string use = key.Use;
			this.SetEnvironmentFlags(false, true, false);
			QilNode qilNode;
			if (use == null)
			{
				qilNode = this.f.Error(this.f.String(XslLoadException.CreateMessage(key.SourceLine, "Missing mandatory attribute '{0}'.", new string[]
				{
					"use"
				})));
			}
			else
			{
				try
				{
					XPathScanner scanner = new XPathScanner(use);
					qilNode = this.xpathParser.Parse(scanner, this.xpathBuilder, LexKind.Eof);
				}
				catch (XslLoadException ex)
				{
					if (this.xslVersion != XslVersion.ForwardsCompatible)
					{
						this.ReportErrorInXPath(ex);
					}
					qilNode = this.f.Error(this.f.String(ex.Message));
				}
			}
			if (qilNode is QilIterator)
			{
				qilNode = this.f.Nop(qilNode);
			}
			return qilNode;
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x000F127C File Offset: 0x000EF47C
		private QilNode ResolveQNameDynamic(bool ignoreDefaultNs, QilNode qilName)
		{
			QilList qilList = this.f.BaseFactory.Sequence();
			if (ignoreDefaultNs)
			{
				qilList.Add(this.f.NamespaceDecl(this.f.String(string.Empty), this.f.String(string.Empty)));
			}
			foreach (CompilerScopeManager<QilIterator>.ScopeRecord scopeRecord in this.scope)
			{
				string ncName = scopeRecord.ncName;
				string nsUri = scopeRecord.nsUri;
				if (!ignoreDefaultNs || ncName.Length != 0)
				{
					qilList.Add(this.f.NamespaceDecl(this.f.String(ncName), this.f.String(nsUri)));
				}
			}
			return this.f.StrParseQName(qilName, qilList);
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x000F133A File Offset: 0x000EF53A
		private QilNode GenerateApply(StylesheetLevel sheet, XslNode node)
		{
			if (this.compiler.Settings.CheckOnly)
			{
				return this.f.Sequence();
			}
			return this.InvokeApplyFunction(sheet, node.Name, node.Content);
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x000F1370 File Offset: 0x000EF570
		private void SetArg(IList<XslNode> args, int pos, QilName name, QilNode value)
		{
			VarPar varPar;
			if (args.Count <= pos || args[pos].Name != name)
			{
				varPar = AstFactory.WithParam(name);
				args.Insert(pos, varPar);
			}
			else
			{
				varPar = (VarPar)args[pos];
			}
			varPar.Value = value;
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x000F13C0 File Offset: 0x000EF5C0
		private IList<XslNode> AddRemoveImplicitArgs(IList<XslNode> args, XslFlags flags)
		{
			if (this.IsDebug)
			{
				flags = XslFlags.FocusFilter;
			}
			if ((flags & XslFlags.FocusFilter) != XslFlags.None)
			{
				if (args == null || args.IsReadOnly)
				{
					args = new List<XslNode>(3);
				}
				int num = 0;
				if ((flags & XslFlags.Current) != XslFlags.None)
				{
					this.SetArg(args, num++, this.nameCurrent, this.GetCurrentNode());
				}
				if ((flags & XslFlags.Position) != XslFlags.None)
				{
					this.SetArg(args, num++, this.namePosition, this.GetCurrentPosition());
				}
				if ((flags & XslFlags.Last) != XslFlags.None)
				{
					this.SetArg(args, num++, this.nameLast, this.GetLastPosition());
				}
			}
			return args;
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x000F1460 File Offset: 0x000EF660
		private bool FillupInvokeArgs(IList<QilNode> formalArgs, IList<XslNode> actualArgs, QilList invokeArgs)
		{
			if (actualArgs.Count != formalArgs.Count)
			{
				return false;
			}
			invokeArgs.Clear();
			for (int i = 0; i < formalArgs.Count; i++)
			{
				QilName name = ((QilParameter)formalArgs[i]).Name;
				XmlQueryType xmlType = formalArgs[i].XmlType;
				QilNode qilNode = null;
				int j = 0;
				while (j < actualArgs.Count)
				{
					VarPar varPar = (VarPar)actualArgs[j];
					if (name.Equals(varPar.Name))
					{
						QilNode value = varPar.Value;
						XmlQueryType xmlType2 = value.XmlType;
						if (xmlType2 != xmlType && (!xmlType2.IsNode || !xmlType.IsNode || !xmlType2.IsSubtypeOf(xmlType)))
						{
							return false;
						}
						qilNode = value;
						break;
					}
					else
					{
						j++;
					}
				}
				if (qilNode == null)
				{
					return false;
				}
				invokeArgs.Add(qilNode);
			}
			return true;
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x000F153C File Offset: 0x000EF73C
		private QilNode InvokeApplyFunction(StylesheetLevel sheet, QilName mode, IList<XslNode> actualArgs)
		{
			XslFlags xslFlags;
			if (!sheet.ModeFlags.TryGetValue(mode, out xslFlags))
			{
				xslFlags = XslFlags.None;
			}
			xslFlags |= XslFlags.Current;
			actualArgs = this.AddRemoveImplicitArgs(actualArgs, xslFlags);
			QilList qilList = this.f.ActualParameterList();
			QilFunction qilFunction = null;
			List<QilFunction> list;
			if (!sheet.ApplyFunctions.TryGetValue(mode, out list))
			{
				list = (sheet.ApplyFunctions[mode] = new List<QilFunction>());
			}
			foreach (QilFunction qilFunction2 in list)
			{
				if (this.FillupInvokeArgs(qilFunction2.Arguments, actualArgs, qilList))
				{
					qilFunction = qilFunction2;
					break;
				}
			}
			if (qilFunction == null)
			{
				qilList.Clear();
				QilList qilList2 = this.f.FormalParameterList();
				for (int i = 0; i < actualArgs.Count; i++)
				{
					VarPar varPar = (VarPar)actualArgs[i];
					qilList.Add(varPar.Value);
					QilParameter qilParameter = this.f.Parameter((i == 0) ? XmlQueryTypeFactory.NodeNotRtf : varPar.Value.XmlType);
					qilParameter.Name = this.CloneName(varPar.Name);
					qilList2.Add(qilParameter);
					varPar.Value = qilParameter;
				}
				qilFunction = this.f.Function(qilList2, this.f.Boolean((xslFlags & XslFlags.SideEffects) > XslFlags.None), XmlQueryTypeFactory.NodeNotRtfS);
				string str = (mode.LocalName.Length == 0) ? string.Empty : (" mode=\"" + mode.QualifiedName + "\"");
				qilFunction.DebugName = ((sheet is RootLevel) ? "<xsl:apply-templates" : "<xsl:apply-imports") + str + ">";
				list.Add(qilFunction);
				this.functions.Add(qilFunction);
				QilIterator qilIterator = (QilIterator)qilList2[0];
				QilIterator qilIterator2 = this.f.For(this.f.Content(qilIterator));
				QilNode qilNode = this.f.Filter(qilIterator2, this.f.IsType(qilIterator2, XmlQueryTypeFactory.Content));
				qilNode.XmlType = XmlQueryTypeFactory.ContentS;
				LoopFocus loopFocus = this.curLoop;
				this.curLoop.SetFocus(this.f.For(qilNode));
				QilNode qilNode2 = this.InvokeApplyFunction(this.compiler.Root, mode, null);
				if (this.IsDebug)
				{
					qilNode2 = this.f.Sequence(this.InvokeOnCurrentNodeChanged(), qilNode2);
				}
				QilLoop center = this.curLoop.ConstructLoop(qilNode2);
				this.curLoop = loopFocus;
				QilTernary otherwise = this.f.BaseFactory.Conditional(this.f.IsType(qilIterator, this.elementOrDocumentType), center, this.f.Conditional(this.f.IsType(qilIterator, this.textOrAttributeType), this.f.TextCtor(this.f.XPathNodeValue(qilIterator)), this.f.Sequence()));
				this.matcherBuilder.CollectPatterns(sheet, mode);
				qilFunction.Definition = this.matcherBuilder.BuildMatcher(qilIterator, actualArgs, otherwise);
			}
			return this.f.Invoke(qilFunction, qilList);
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000F186C File Offset: 0x000EFA6C
		public void ReportError(string res, params string[] args)
		{
			this.compiler.ReportError(this.lastScope.SourceLine, res, args);
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x000F1886 File Offset: 0x000EFA86
		public void ReportWarning(string res, params string[] args)
		{
			this.compiler.ReportWarning(this.lastScope.SourceLine, res, args);
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		private void VerifyXPathQName(QilName qname)
		{
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x000F18A0 File Offset: 0x000EFAA0
		private string ResolvePrefix(bool ignoreDefaultNs, string prefix)
		{
			if (ignoreDefaultNs && prefix.Length == 0)
			{
				return string.Empty;
			}
			string text = this.scope.LookupNamespace(prefix);
			if (text == null)
			{
				if (prefix.Length == 0)
				{
					text = string.Empty;
				}
				else
				{
					this.ReportError("Prefix '{0}' is not defined.", new string[]
					{
						prefix
					});
					text = this.compiler.CreatePhantomNamespace();
				}
			}
			return text;
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x000F1900 File Offset: 0x000EFB00
		private void SetLineInfoCheck(QilNode n, ISourceLineInfo lineInfo)
		{
			if (n.SourceLine == null)
			{
				QilGenerator.SetLineInfo(n, lineInfo);
			}
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x000F1914 File Offset: 0x000EFB14
		private static QilNode SetLineInfo(QilNode n, ISourceLineInfo lineInfo)
		{
			if (lineInfo != null && 0 < lineInfo.Start.Line && lineInfo.Start.LessOrEqual(lineInfo.End))
			{
				n.SourceLine = lineInfo;
			}
			return n;
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x000F1954 File Offset: 0x000EFB54
		private QilNode AddDebugVariable(QilName name, QilNode value, QilNode content)
		{
			QilIterator qilIterator = this.f.Let(value);
			qilIterator.DebugName = name.ToString();
			return this.f.Loop(qilIterator, content);
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x000F1988 File Offset: 0x000EFB88
		private QilNode SetDebugNs(QilNode n, QilList nsList)
		{
			if (n != null && nsList != null)
			{
				QilNode qilNode = this.GetNsVar(nsList);
				if (qilNode.XmlType.Cardinality == XmlQueryCardinality.One)
				{
					qilNode = this.f.TypeAssert(qilNode, XmlQueryTypeFactory.NamespaceS);
				}
				n = this.AddDebugVariable(this.CloneName(this.nameNamespaces), qilNode, n);
			}
			return n;
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x000F19E4 File Offset: 0x000EFBE4
		private QilNode AddCurrentPositionLast(QilNode content)
		{
			if (this.IsDebug)
			{
				content = this.AddDebugVariable(this.CloneName(this.nameLast), this.GetLastPosition(), content);
				content = this.AddDebugVariable(this.CloneName(this.namePosition), this.GetCurrentPosition(), content);
				content = this.AddDebugVariable(this.CloneName(this.nameCurrent), this.GetCurrentNode(), content);
			}
			return content;
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x000F1A4B File Offset: 0x000EFC4B
		private QilName CloneName(QilName name)
		{
			return (QilName)name.ShallowClone(this.f.BaseFactory);
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x000F1A63 File Offset: 0x000EFC63
		private void SetEnvironmentFlags(bool allowVariables, bool allowCurrent, bool allowKey)
		{
			this.allowVariables = allowVariables;
			this.allowCurrent = allowCurrent;
			this.allowKey = allowKey;
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002843 RID: 10307 RVA: 0x000F1A7A File Offset: 0x000EFC7A
		XPathQilFactory IXPathEnvironment.Factory
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x000F1A82 File Offset: 0x000EFC82
		QilNode IFocus.GetCurrent()
		{
			return this.GetCurrentNode();
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000F1A8A File Offset: 0x000EFC8A
		QilNode IFocus.GetPosition()
		{
			return this.GetCurrentPosition();
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x000F1A92 File Offset: 0x000EFC92
		QilNode IFocus.GetLast()
		{
			return this.GetLastPosition();
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000F1A9A File Offset: 0x000EFC9A
		string IXPathEnvironment.ResolvePrefix(string prefix)
		{
			return this.ResolvePrefixThrow(true, prefix);
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x000F1AA4 File Offset: 0x000EFCA4
		QilNode IXPathEnvironment.ResolveVariable(string prefix, string name)
		{
			if (!this.allowVariables)
			{
				throw new XslLoadException("Variables cannot be used within this expression.", Array.Empty<string>());
			}
			string uri = this.ResolvePrefixThrow(true, prefix);
			QilNode qilNode = this.scope.LookupVariable(name, uri);
			if (qilNode == null)
			{
				throw new XslLoadException("The variable or parameter '{0}' is either not defined or it is out of scope.", new string[]
				{
					Compiler.ConstructQName(prefix, name)
				});
			}
			XmlQueryType xmlType = qilNode.XmlType;
			if (qilNode.NodeType == QilNodeType.Parameter && xmlType.IsNode && xmlType.IsNotRtf && xmlType.MaybeMany && !xmlType.IsDod)
			{
				qilNode = this.f.TypeAssert(qilNode, XmlQueryTypeFactory.NodeSDod);
			}
			return qilNode;
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x000F1B44 File Offset: 0x000EFD44
		QilNode IXPathEnvironment.ResolveFunction(string prefix, string name, IList<QilNode> args, IFocus env)
		{
			if (prefix.Length != 0)
			{
				string text = this.ResolvePrefixThrow(true, prefix);
				if (text == "urn:schemas-microsoft-com:xslt")
				{
					if (name == "node-set")
					{
						XPathBuilder.FunctionInfo<QilGenerator.FuncId>.CheckArity(1, 1, name, args.Count);
						return this.CompileMsNodeSet(args[0]);
					}
					if (name == "string-compare")
					{
						XPathBuilder.FunctionInfo<QilGenerator.FuncId>.CheckArity(2, 4, name, args.Count);
						return this.f.InvokeMsStringCompare(this.f.ConvertToString(args[0]), this.f.ConvertToString(args[1]), (2 < args.Count) ? this.f.ConvertToString(args[2]) : this.f.String(string.Empty), (3 < args.Count) ? this.f.ConvertToString(args[3]) : this.f.String(string.Empty));
					}
					if (name == "utc")
					{
						XPathBuilder.FunctionInfo<QilGenerator.FuncId>.CheckArity(1, 1, name, args.Count);
						return this.f.InvokeMsUtc(this.f.ConvertToString(args[0]));
					}
					if (name == "format-date" || name == "format-time")
					{
						XPathBuilder.FunctionInfo<QilGenerator.FuncId>.CheckArity(1, 3, name, args.Count);
						XslVersion xslVersion = this.xslVersion;
						return this.f.InvokeMsFormatDateTime(this.f.ConvertToString(args[0]), (1 < args.Count) ? this.f.ConvertToString(args[1]) : this.f.String(string.Empty), (2 < args.Count) ? this.f.ConvertToString(args[2]) : this.f.String(string.Empty), this.f.Boolean(name == "format-date"));
					}
					if (name == "local-name")
					{
						XPathBuilder.FunctionInfo<QilGenerator.FuncId>.CheckArity(1, 1, name, args.Count);
						return this.f.InvokeMsLocalName(this.f.ConvertToString(args[0]));
					}
					if (name == "namespace-uri")
					{
						XPathBuilder.FunctionInfo<QilGenerator.FuncId>.CheckArity(1, 1, name, args.Count);
						return this.f.InvokeMsNamespaceUri(this.f.ConvertToString(args[0]), env.GetCurrent());
					}
					if (name == "number")
					{
						XPathBuilder.FunctionInfo<QilGenerator.FuncId>.CheckArity(1, 1, name, args.Count);
						return this.f.InvokeMsNumber(args[0]);
					}
				}
				if (text == "http://exslt.org/common")
				{
					if (name == "node-set")
					{
						XPathBuilder.FunctionInfo<QilGenerator.FuncId>.CheckArity(1, 1, name, args.Count);
						return this.CompileMsNodeSet(args[0]);
					}
					if (name == "object-type")
					{
						XPathBuilder.FunctionInfo<QilGenerator.FuncId>.CheckArity(1, 1, name, args.Count);
						return this.EXslObjectType(args[0]);
					}
				}
				for (int i = 0; i < args.Count; i++)
				{
					args[i] = this.f.SafeDocOrderDistinct(args[i]);
				}
				if (this.compiler.Settings.EnableScript)
				{
					XmlExtensionFunction xmlExtensionFunction = this.compiler.Scripts.ResolveFunction(name, text, args.Count, this);
					if (xmlExtensionFunction != null)
					{
						return this.GenerateScriptCall(this.f.QName(name, text, prefix), xmlExtensionFunction, args);
					}
				}
				else if (this.compiler.Scripts.ScriptClasses.ContainsKey(text))
				{
					this.ReportWarning("Execution of scripts was prohibited. Use the XsltSettings.EnableScript property to enable it.", Array.Empty<string>());
					return this.f.Error(this.lastScope.SourceLine, "Execution of scripts was prohibited. Use the XsltSettings.EnableScript property to enable it.", Array.Empty<string>());
				}
				return this.f.XsltInvokeLateBound(this.f.QName(name, text, prefix), args);
			}
			XPathBuilder.FunctionInfo<QilGenerator.FuncId> functionInfo;
			if (!QilGenerator.FunctionTable.TryGetValue(name, out functionInfo))
			{
				throw new XslLoadException("'{0}()' is an unknown XSLT function.", new string[]
				{
					Compiler.ConstructQName(prefix, name)
				});
			}
			functionInfo.CastArguments(args, name, this.f);
			switch (functionInfo.id)
			{
			case QilGenerator.FuncId.Current:
				if (!this.allowCurrent)
				{
					throw new XslLoadException("The 'current()' function cannot be used in a pattern.", Array.Empty<string>());
				}
				return ((IFocus)this).GetCurrent();
			case QilGenerator.FuncId.Document:
				return this.CompileFnDocument(args[0], (args.Count > 1) ? args[1] : null);
			case QilGenerator.FuncId.Key:
				if (!this.allowKey)
				{
					throw new XslLoadException("The 'key()' function cannot be used in 'use' and 'match' attributes of 'xsl:key' element.", Array.Empty<string>());
				}
				return this.CompileFnKey(args[0], args[1], env);
			case QilGenerator.FuncId.FormatNumber:
				return this.CompileFormatNumber(args[0], args[1], (args.Count > 2) ? args[2] : null);
			case QilGenerator.FuncId.UnparsedEntityUri:
				return this.CompileUnparsedEntityUri(args[0]);
			case QilGenerator.FuncId.GenerateId:
				return this.CompileGenerateId((args.Count > 0) ? args[0] : env.GetCurrent());
			case QilGenerator.FuncId.SystemProperty:
				return this.CompileSystemProperty(args[0]);
			case QilGenerator.FuncId.ElementAvailable:
				return this.CompileElementAvailable(args[0]);
			case QilGenerator.FuncId.FunctionAvailable:
				return this.CompileFunctionAvailable(args[0]);
			default:
				return null;
			}
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x000F2080 File Offset: 0x000F0280
		private QilNode GenerateScriptCall(QilName name, XmlExtensionFunction scrFunc, IList<QilNode> args)
		{
			for (int i = 0; i < args.Count; i++)
			{
				XmlQueryType xmlArgumentType = scrFunc.GetXmlArgumentType(i);
				XmlTypeCode typeCode = xmlArgumentType.TypeCode;
				if (typeCode != XmlTypeCode.Item)
				{
					if (typeCode != XmlTypeCode.Node)
					{
						switch (typeCode)
						{
						case XmlTypeCode.String:
							args[i] = this.f.ConvertToString(args[i]);
							break;
						case XmlTypeCode.Boolean:
							args[i] = this.f.ConvertToBoolean(args[i]);
							break;
						case XmlTypeCode.Double:
							args[i] = this.f.ConvertToNumber(args[i]);
							break;
						}
					}
					else
					{
						args[i] = (xmlArgumentType.IsSingleton ? this.f.ConvertToNode(args[i]) : this.f.ConvertToNodeSet(args[i]));
					}
				}
			}
			return this.f.XsltInvokeEarlyBound(name, scrFunc.Method, scrFunc.XmlReturnType, args);
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x000F2180 File Offset: 0x000F0380
		private string ResolvePrefixThrow(bool ignoreDefaultNs, string prefix)
		{
			if (ignoreDefaultNs && prefix.Length == 0)
			{
				return string.Empty;
			}
			string text = this.scope.LookupNamespace(prefix);
			if (text == null)
			{
				if (prefix.Length != 0)
				{
					throw new XslLoadException("Prefix '{0}' is not defined.", new string[]
					{
						prefix
					});
				}
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x000F21D4 File Offset: 0x000F03D4
		private static Dictionary<string, XPathBuilder.FunctionInfo<QilGenerator.FuncId>> CreateFunctionTable()
		{
			return new Dictionary<string, XPathBuilder.FunctionInfo<QilGenerator.FuncId>>(16)
			{
				{
					"current",
					new XPathBuilder.FunctionInfo<QilGenerator.FuncId>(QilGenerator.FuncId.Current, 0, 0, null)
				},
				{
					"document",
					new XPathBuilder.FunctionInfo<QilGenerator.FuncId>(QilGenerator.FuncId.Document, 1, 2, QilGenerator.argFnDocument)
				},
				{
					"key",
					new XPathBuilder.FunctionInfo<QilGenerator.FuncId>(QilGenerator.FuncId.Key, 2, 2, QilGenerator.argFnKey)
				},
				{
					"format-number",
					new XPathBuilder.FunctionInfo<QilGenerator.FuncId>(QilGenerator.FuncId.FormatNumber, 2, 3, QilGenerator.argFnFormatNumber)
				},
				{
					"unparsed-entity-uri",
					new XPathBuilder.FunctionInfo<QilGenerator.FuncId>(QilGenerator.FuncId.UnparsedEntityUri, 1, 1, XPathBuilder.argString)
				},
				{
					"generate-id",
					new XPathBuilder.FunctionInfo<QilGenerator.FuncId>(QilGenerator.FuncId.GenerateId, 0, 1, XPathBuilder.argNodeSet)
				},
				{
					"system-property",
					new XPathBuilder.FunctionInfo<QilGenerator.FuncId>(QilGenerator.FuncId.SystemProperty, 1, 1, XPathBuilder.argString)
				},
				{
					"element-available",
					new XPathBuilder.FunctionInfo<QilGenerator.FuncId>(QilGenerator.FuncId.ElementAvailable, 1, 1, XPathBuilder.argString)
				},
				{
					"function-available",
					new XPathBuilder.FunctionInfo<QilGenerator.FuncId>(QilGenerator.FuncId.FunctionAvailable, 1, 1, XPathBuilder.argString)
				}
			};
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x000F22BC File Offset: 0x000F04BC
		public static bool IsFunctionAvailable(string localName, string nsUri)
		{
			if (XPathBuilder.IsFunctionAvailable(localName, nsUri))
			{
				return true;
			}
			if (nsUri.Length == 0)
			{
				return QilGenerator.FunctionTable.ContainsKey(localName) && localName != "unparsed-entity-uri";
			}
			if (nsUri == "urn:schemas-microsoft-com:xslt")
			{
				return localName == "node-set" || localName == "format-date" || localName == "format-time" || localName == "local-name" || localName == "namespace-uri" || localName == "number" || localName == "string-compare" || localName == "utc";
			}
			return nsUri == "http://exslt.org/common" && (localName == "node-set" || localName == "object-type");
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x000F2398 File Offset: 0x000F0598
		public static bool IsElementAvailable(XmlQualifiedName name)
		{
			if (name.Namespace == "http://www.w3.org/1999/XSL/Transform")
			{
				string name2 = name.Name;
				return name2 == "apply-imports" || name2 == "apply-templates" || name2 == "attribute" || name2 == "call-template" || name2 == "choose" || name2 == "comment" || name2 == "copy" || name2 == "copy-of" || name2 == "element" || name2 == "fallback" || name2 == "for-each" || name2 == "if" || name2 == "message" || name2 == "number" || name2 == "processing-instruction" || name2 == "text" || name2 == "value-of" || name2 == "variable";
			}
			return false;
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x000F24C8 File Offset: 0x000F06C8
		private QilNode CompileFnKey(QilNode name, QilNode keys, IFocus env)
		{
			QilNode collection;
			if (keys.XmlType.IsNode)
			{
				if (keys.XmlType.IsSingleton)
				{
					collection = this.CompileSingleKey(name, this.f.ConvertToString(keys), env);
				}
				else
				{
					QilIterator n;
					collection = this.f.Loop(n = this.f.For(keys), this.CompileSingleKey(name, this.f.ConvertToString(n), env));
				}
			}
			else if (keys.XmlType.IsAtomicValue)
			{
				collection = this.CompileSingleKey(name, this.f.ConvertToString(keys), env);
			}
			else
			{
				QilIterator n;
				QilIterator name2;
				QilIterator expr;
				collection = this.f.Loop(name2 = this.f.Let(name), this.f.Loop(expr = this.f.Let(keys), this.f.Conditional(this.f.Not(this.f.IsType(expr, XmlQueryTypeFactory.AnyAtomicType)), this.f.Loop(n = this.f.For(this.f.TypeAssert(expr, XmlQueryTypeFactory.NodeS)), this.CompileSingleKey(name2, this.f.ConvertToString(n), env)), this.CompileSingleKey(name2, this.f.XsltConvert(expr, XmlQueryTypeFactory.StringX), env))));
			}
			return this.f.DocOrderDistinct(collection);
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x000F2620 File Offset: 0x000F0820
		private QilNode CompileSingleKey(QilNode name, QilNode key, IFocus env)
		{
			QilNode qilNode;
			if (name.NodeType == QilNodeType.LiteralString)
			{
				string text = (QilLiteral)name;
				string prefix;
				string local;
				this.compiler.ParseQName(text, out prefix, out local, default(QilGenerator.ThrowErrorHelper));
				string uri = this.ResolvePrefixThrow(true, prefix);
				QilName key2 = this.f.QName(local, uri, prefix);
				if (!this.compiler.Keys.Contains(key2))
				{
					throw new XslLoadException("A reference to key '{0}' cannot be resolved. An 'xsl:key' of this name must be declared at the top level of the stylesheet.", new string[]
					{
						text
					});
				}
				qilNode = this.CompileSingleKey(this.compiler.Keys[key2], key, env);
			}
			else
			{
				if (this.generalKey == null)
				{
					this.generalKey = this.CreateGeneralKeyFunction();
				}
				QilIterator qilIterator = this.f.Let(name);
				QilNode qilNode2 = this.ResolveQNameDynamic(true, qilIterator);
				qilNode = this.f.Invoke(this.generalKey, this.f.ActualParameterList(new QilNode[]
				{
					qilIterator,
					qilNode2,
					key,
					env.GetCurrent()
				}));
				qilNode = this.f.Loop(qilIterator, qilNode);
			}
			return qilNode;
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000F2740 File Offset: 0x000F0940
		private QilNode CompileSingleKey(List<Key> defList, QilNode key, IFocus env)
		{
			if (defList.Count == 1)
			{
				return this.f.Invoke(defList[0].Function, this.f.ActualParameterList(env.GetCurrent(), key));
			}
			QilIterator qilIterator = this.f.Let(key);
			QilNode qilNode = this.f.Sequence();
			foreach (Key key2 in defList)
			{
				qilNode.Add(this.f.Invoke(key2.Function, this.f.ActualParameterList(env.GetCurrent(), qilIterator)));
			}
			return this.f.Loop(qilIterator, qilNode);
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x000F280C File Offset: 0x000F0A0C
		private QilNode CompileSingleKey(List<Key> defList, QilIterator key, QilIterator context)
		{
			QilList qilList = this.f.BaseFactory.Sequence();
			QilNode qilNode = null;
			foreach (Key key2 in defList)
			{
				qilNode = this.f.Invoke(key2.Function, this.f.ActualParameterList(context, key));
				qilList.Add(qilNode);
			}
			if (defList.Count != 1)
			{
				return qilList;
			}
			return qilNode;
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x000F2898 File Offset: 0x000F0A98
		private QilFunction CreateGeneralKeyFunction()
		{
			QilIterator qilIterator = this.f.Parameter(XmlQueryTypeFactory.StringX);
			QilIterator qilIterator2 = this.f.Parameter(XmlQueryTypeFactory.QNameX);
			QilIterator qilIterator3 = this.f.Parameter(XmlQueryTypeFactory.StringX);
			QilIterator qilIterator4 = this.f.Parameter(XmlQueryTypeFactory.NodeNotRtf);
			QilNode qilNode = this.f.Error("A reference to key '{0}' cannot be resolved. An 'xsl:key' of this name must be declared at the top level of the stylesheet.", qilIterator);
			for (int i = 0; i < this.compiler.Keys.Count; i++)
			{
				qilNode = this.f.Conditional(this.f.Eq(qilIterator2, this.compiler.Keys[i][0].Name.DeepClone(this.f.BaseFactory)), this.CompileSingleKey(this.compiler.Keys[i], qilIterator3, qilIterator4), qilNode);
			}
			QilFunction qilFunction = this.f.Function(this.f.FormalParameterList(new QilNode[]
			{
				qilIterator,
				qilIterator2,
				qilIterator3,
				qilIterator4
			}), qilNode, this.f.False());
			qilFunction.DebugName = "key";
			this.functions.Add(qilFunction);
			return qilFunction;
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x000F29D4 File Offset: 0x000F0BD4
		private QilNode CompileFnDocument(QilNode uris, QilNode baseNode)
		{
			if (!this.compiler.Settings.EnableDocumentFunction)
			{
				this.ReportWarning("Execution of the 'document()' function was prohibited. Use the XsltSettings.EnableDocumentFunction property to enable it.", Array.Empty<string>());
				return this.f.Error(this.lastScope.SourceLine, "Execution of the 'document()' function was prohibited. Use the XsltSettings.EnableDocumentFunction property to enable it.", Array.Empty<string>());
			}
			QilNode qilNode;
			if (uris.XmlType.IsNode)
			{
				QilIterator qilIterator;
				qilNode = this.f.DocOrderDistinct(this.f.Loop(qilIterator = this.f.For(uris), this.CompileSingleDocument(this.f.ConvertToString(qilIterator), baseNode ?? qilIterator)));
			}
			else if (uris.XmlType.IsAtomicValue)
			{
				qilNode = this.CompileSingleDocument(this.f.ConvertToString(uris), baseNode);
			}
			else
			{
				QilIterator qilIterator2 = this.f.Let(uris);
				QilIterator qilIterator3 = (baseNode != null) ? this.f.Let(baseNode) : null;
				QilIterator qilIterator;
				qilNode = this.f.Conditional(this.f.Not(this.f.IsType(qilIterator2, XmlQueryTypeFactory.AnyAtomicType)), this.f.DocOrderDistinct(this.f.Loop(qilIterator = this.f.For(this.f.TypeAssert(qilIterator2, XmlQueryTypeFactory.NodeS)), this.CompileSingleDocument(this.f.ConvertToString(qilIterator), qilIterator3 ?? qilIterator))), this.CompileSingleDocument(this.f.XsltConvert(qilIterator2, XmlQueryTypeFactory.StringX), qilIterator3));
				qilNode = ((baseNode != null) ? this.f.Loop(qilIterator3, qilNode) : qilNode);
				qilNode = this.f.Loop(qilIterator2, qilNode);
			}
			return qilNode;
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x000F2B68 File Offset: 0x000F0D68
		private QilNode CompileSingleDocument(QilNode uri, QilNode baseNode)
		{
			QilNode baseUri;
			if (baseNode == null)
			{
				baseUri = this.f.String(this.lastScope.SourceLine.Uri);
			}
			else if (baseNode.XmlType.IsSingleton)
			{
				baseUri = this.f.InvokeBaseUri(baseNode);
			}
			else
			{
				QilIterator n;
				baseUri = this.f.StrConcat(this.f.Loop(n = this.f.FirstNode(baseNode), this.f.InvokeBaseUri(n)));
			}
			return this.f.DataSource(uri, baseUri);
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x000F2BF0 File Offset: 0x000F0DF0
		private QilNode CompileFormatNumber(QilNode value, QilNode formatPicture, QilNode formatName)
		{
			XmlQualifiedName xmlQualifiedName;
			if (formatName == null)
			{
				xmlQualifiedName = new XmlQualifiedName();
				formatName = this.f.String(string.Empty);
			}
			else if (formatName.NodeType == QilNodeType.LiteralString)
			{
				xmlQualifiedName = this.ResolveQNameThrow(true, formatName);
			}
			else
			{
				xmlQualifiedName = null;
			}
			if (!(xmlQualifiedName != null))
			{
				this.formatNumberDynamicUsed = true;
				QilIterator qilIterator = this.f.Let(formatName);
				QilNode decimalFormatName = this.ResolveQNameDynamic(true, qilIterator);
				return this.f.Loop(qilIterator, this.f.InvokeFormatNumberDynamic(value, formatPicture, decimalFormatName, qilIterator));
			}
			DecimalFormatDecl format;
			if (this.compiler.DecimalFormats.Contains(xmlQualifiedName))
			{
				format = this.compiler.DecimalFormats[xmlQualifiedName];
			}
			else
			{
				if (xmlQualifiedName != DecimalFormatDecl.Default.Name)
				{
					throw new XslLoadException("Decimal format '{0}' is not defined.", new string[]
					{
						(QilLiteral)formatName
					});
				}
				format = DecimalFormatDecl.Default;
			}
			if (formatPicture.NodeType == QilNodeType.LiteralString)
			{
				QilIterator qilIterator2 = this.f.Let(this.f.InvokeRegisterDecimalFormatter(formatPicture, format));
				QilReference qilReference = qilIterator2;
				QilPatternFactory qilPatternFactory = this.f;
				string str = "formatter";
				int num = this.formatterCnt;
				this.formatterCnt = num + 1;
				qilReference.DebugName = qilPatternFactory.QName(str + num.ToString(), "urn:schemas-microsoft-com:xslt-debug").ToString();
				this.gloVars.Add(qilIterator2);
				return this.f.InvokeFormatNumberStatic(value, qilIterator2);
			}
			this.formatNumberDynamicUsed = true;
			QilNode decimalFormatName2 = this.f.QName(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
			return this.f.InvokeFormatNumberDynamic(value, formatPicture, decimalFormatName2, formatName);
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000F2D83 File Offset: 0x000F0F83
		private QilNode CompileUnparsedEntityUri(QilNode n)
		{
			return this.f.Error(this.lastScope.SourceLine, "'{0}()' is an unsupported XSLT function.", new string[]
			{
				"unparsed-entity-uri"
			});
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x000F2DB0 File Offset: 0x000F0FB0
		private QilNode CompileGenerateId(QilNode n)
		{
			if (n.XmlType.IsSingleton)
			{
				return this.f.XsltGenerateId(n);
			}
			QilIterator expr;
			return this.f.StrConcat(this.f.Loop(expr = this.f.FirstNode(n), this.f.XsltGenerateId(expr)));
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x000F2E08 File Offset: 0x000F1008
		private XmlQualifiedName ResolveQNameThrow(bool ignoreDefaultNs, QilNode qilName)
		{
			string qname = (QilLiteral)qilName;
			string prefix;
			string name;
			this.compiler.ParseQName(qname, out prefix, out name, default(QilGenerator.ThrowErrorHelper));
			string ns = this.ResolvePrefixThrow(ignoreDefaultNs, prefix);
			return new XmlQualifiedName(name, ns);
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x000F2E54 File Offset: 0x000F1054
		private QilNode CompileSystemProperty(QilNode name)
		{
			if (name.NodeType == QilNodeType.LiteralString)
			{
				XmlQualifiedName xmlQualifiedName = this.ResolveQNameThrow(true, name);
				if (this.EvaluateFuncCalls)
				{
					XPathItem xpathItem = XsltFunctions.SystemProperty(xmlQualifiedName);
					if (xpathItem.ValueType == XsltConvert.StringType)
					{
						return this.f.String(xpathItem.Value);
					}
					return this.f.Double(xpathItem.ValueAsDouble);
				}
				else
				{
					name = this.f.QName(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
				}
			}
			else
			{
				name = this.ResolveQNameDynamic(true, name);
			}
			return this.f.InvokeSystemProperty(name);
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x000F2EEC File Offset: 0x000F10EC
		private QilNode CompileElementAvailable(QilNode name)
		{
			if (name.NodeType == QilNodeType.LiteralString)
			{
				XmlQualifiedName xmlQualifiedName = this.ResolveQNameThrow(false, name);
				if (this.EvaluateFuncCalls)
				{
					return this.f.Boolean(QilGenerator.IsElementAvailable(xmlQualifiedName));
				}
				name = this.f.QName(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
			}
			else
			{
				name = this.ResolveQNameDynamic(false, name);
			}
			return this.f.InvokeElementAvailable(name);
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x000F2F58 File Offset: 0x000F1158
		private QilNode CompileFunctionAvailable(QilNode name)
		{
			if (name.NodeType == QilNodeType.LiteralString)
			{
				XmlQualifiedName xmlQualifiedName = this.ResolveQNameThrow(true, name);
				if (this.EvaluateFuncCalls && (xmlQualifiedName.Namespace.Length == 0 || xmlQualifiedName.Namespace == "http://www.w3.org/1999/XSL/Transform"))
				{
					return this.f.Boolean(QilGenerator.IsFunctionAvailable(xmlQualifiedName.Name, xmlQualifiedName.Namespace));
				}
				name = this.f.QName(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
			}
			else
			{
				name = this.ResolveQNameDynamic(true, name);
			}
			return this.f.InvokeFunctionAvailable(name);
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x000F2FED File Offset: 0x000F11ED
		private QilNode CompileMsNodeSet(QilNode n)
		{
			if (n.XmlType.IsNode && n.XmlType.IsNotRtf)
			{
				return n;
			}
			return this.f.XsltConvert(n, XmlQueryTypeFactory.NodeSDod);
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000F301C File Offset: 0x000F121C
		private QilNode EXslObjectType(QilNode n)
		{
			if (this.EvaluateFuncCalls)
			{
				switch (n.XmlType.TypeCode)
				{
				case XmlTypeCode.String:
					return this.f.String("string");
				case XmlTypeCode.Boolean:
					return this.f.String("boolean");
				case XmlTypeCode.Double:
					return this.f.String("number");
				}
				if (n.XmlType.IsNode && n.XmlType.IsNotRtf)
				{
					return this.f.String("node-set");
				}
			}
			return this.f.InvokeEXslObjectType(n);
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000F30CC File Offset: 0x000F12CC
		// Note: this type is marked as 'beforefieldinit'.
		static QilGenerator()
		{
		}

		// Token: 0x04001FB0 RID: 8112
		private CompilerScopeManager<QilIterator> scope;

		// Token: 0x04001FB1 RID: 8113
		private OutputScopeManager outputScope;

		// Token: 0x04001FB2 RID: 8114
		private HybridDictionary prefixesInUse;

		// Token: 0x04001FB3 RID: 8115
		private XsltQilFactory f;

		// Token: 0x04001FB4 RID: 8116
		private XPathBuilder xpathBuilder;

		// Token: 0x04001FB5 RID: 8117
		private XPathParser<QilNode> xpathParser;

		// Token: 0x04001FB6 RID: 8118
		private XPathPatternBuilder ptrnBuilder;

		// Token: 0x04001FB7 RID: 8119
		private XPathPatternParser ptrnParser;

		// Token: 0x04001FB8 RID: 8120
		private ReferenceReplacer refReplacer;

		// Token: 0x04001FB9 RID: 8121
		private KeyMatchBuilder keyMatchBuilder;

		// Token: 0x04001FBA RID: 8122
		private InvokeGenerator invkGen;

		// Token: 0x04001FBB RID: 8123
		private MatcherBuilder matcherBuilder;

		// Token: 0x04001FBC RID: 8124
		private QilStrConcatenator strConcat;

		// Token: 0x04001FBD RID: 8125
		private QilGenerator.VariableHelper varHelper;

		// Token: 0x04001FBE RID: 8126
		private Compiler compiler;

		// Token: 0x04001FBF RID: 8127
		private QilList functions;

		// Token: 0x04001FC0 RID: 8128
		private QilFunction generalKey;

		// Token: 0x04001FC1 RID: 8129
		private bool formatNumberDynamicUsed;

		// Token: 0x04001FC2 RID: 8130
		private QilList extPars;

		// Token: 0x04001FC3 RID: 8131
		private QilList gloVars;

		// Token: 0x04001FC4 RID: 8132
		private QilList nsVars;

		// Token: 0x04001FC5 RID: 8133
		private XmlQueryType elementOrDocumentType;

		// Token: 0x04001FC6 RID: 8134
		private XmlQueryType textOrAttributeType;

		// Token: 0x04001FC7 RID: 8135
		private XslNode lastScope;

		// Token: 0x04001FC8 RID: 8136
		private XslVersion xslVersion;

		// Token: 0x04001FC9 RID: 8137
		private QilName nameCurrent;

		// Token: 0x04001FCA RID: 8138
		private QilName namePosition;

		// Token: 0x04001FCB RID: 8139
		private QilName nameLast;

		// Token: 0x04001FCC RID: 8140
		private QilName nameNamespaces;

		// Token: 0x04001FCD RID: 8141
		private QilName nameInit;

		// Token: 0x04001FCE RID: 8142
		private SingletonFocus singlFocus;

		// Token: 0x04001FCF RID: 8143
		private FunctionFocus funcFocus;

		// Token: 0x04001FD0 RID: 8144
		private LoopFocus curLoop;

		// Token: 0x04001FD1 RID: 8145
		private int formatterCnt;

		// Token: 0x04001FD2 RID: 8146
		private readonly StringBuilder unescapedText = new StringBuilder();

		// Token: 0x04001FD3 RID: 8147
		private static readonly char[] curlyBraces = new char[]
		{
			'{',
			'}'
		};

		// Token: 0x04001FD4 RID: 8148
		private const XmlNodeKindFlags InvalidatingNodes = XmlNodeKindFlags.Attribute | XmlNodeKindFlags.Namespace;

		// Token: 0x04001FD5 RID: 8149
		private bool allowVariables = true;

		// Token: 0x04001FD6 RID: 8150
		private bool allowCurrent = true;

		// Token: 0x04001FD7 RID: 8151
		private bool allowKey = true;

		// Token: 0x04001FD8 RID: 8152
		private static readonly XmlTypeCode[] argFnDocument = new XmlTypeCode[]
		{
			XmlTypeCode.Item,
			XmlTypeCode.Node
		};

		// Token: 0x04001FD9 RID: 8153
		private static readonly XmlTypeCode[] argFnKey = new XmlTypeCode[]
		{
			XmlTypeCode.String,
			XmlTypeCode.Item
		};

		// Token: 0x04001FDA RID: 8154
		private static readonly XmlTypeCode[] argFnFormatNumber = new XmlTypeCode[]
		{
			XmlTypeCode.Double,
			XmlTypeCode.String,
			XmlTypeCode.String
		};

		// Token: 0x04001FDB RID: 8155
		public static Dictionary<string, XPathBuilder.FunctionInfo<QilGenerator.FuncId>> FunctionTable = QilGenerator.CreateFunctionTable();

		// Token: 0x020003F1 RID: 1009
		private class VariableHelper
		{
			// Token: 0x06002860 RID: 10336 RVA: 0x000F3135 File Offset: 0x000F1335
			public VariableHelper(XPathQilFactory f)
			{
				this.f = f;
			}

			// Token: 0x06002861 RID: 10337 RVA: 0x000F314F File Offset: 0x000F134F
			public int StartVariables()
			{
				return this.vars.Count;
			}

			// Token: 0x06002862 RID: 10338 RVA: 0x000F315C File Offset: 0x000F135C
			public void AddVariable(QilIterator let)
			{
				this.vars.Push(let);
			}

			// Token: 0x06002863 RID: 10339 RVA: 0x000F316C File Offset: 0x000F136C
			public QilNode FinishVariables(QilNode node, int varScope)
			{
				int num = this.vars.Count - varScope;
				while (num-- != 0)
				{
					node = this.f.Loop(this.vars.Pop(), node);
				}
				return node;
			}

			// Token: 0x06002864 RID: 10340 RVA: 0x0000B528 File Offset: 0x00009728
			[Conditional("DEBUG")]
			public void CheckEmpty()
			{
			}

			// Token: 0x04001FDC RID: 8156
			private Stack<QilIterator> vars = new Stack<QilIterator>();

			// Token: 0x04001FDD RID: 8157
			private XPathQilFactory f;
		}

		// Token: 0x020003F2 RID: 1010
		private struct ThrowErrorHelper : IErrorHelper
		{
			// Token: 0x06002865 RID: 10341 RVA: 0x000F31AA File Offset: 0x000F13AA
			public void ReportError(string res, params string[] args)
			{
				throw new XslLoadException("{0}", new string[]
				{
					res
				});
			}

			// Token: 0x06002866 RID: 10342 RVA: 0x0000B528 File Offset: 0x00009728
			public void ReportWarning(string res, params string[] args)
			{
			}
		}

		// Token: 0x020003F3 RID: 1011
		public enum FuncId
		{
			// Token: 0x04001FDF RID: 8159
			Current,
			// Token: 0x04001FE0 RID: 8160
			Document,
			// Token: 0x04001FE1 RID: 8161
			Key,
			// Token: 0x04001FE2 RID: 8162
			FormatNumber,
			// Token: 0x04001FE3 RID: 8163
			UnparsedEntityUri,
			// Token: 0x04001FE4 RID: 8164
			GenerateId,
			// Token: 0x04001FE5 RID: 8165
			SystemProperty,
			// Token: 0x04001FE6 RID: 8166
			ElementAvailable,
			// Token: 0x04001FE7 RID: 8167
			FunctionAvailable
		}
	}
}
