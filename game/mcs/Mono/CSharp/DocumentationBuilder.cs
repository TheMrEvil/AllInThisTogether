using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace Mono.CSharp
{
	// Token: 0x02000198 RID: 408
	public class DocumentationBuilder
	{
		// Token: 0x060015EC RID: 5612 RVA: 0x000691BC File Offset: 0x000673BC
		public DocumentationBuilder(ModuleContainer module)
		{
			this.doc_module = new ModuleContainer(module.Compiler);
			this.doc_module.DocumentationBuilder = this;
			this.module = module;
			this.XmlDocumentation = new XmlDocument();
			this.XmlDocumentation.PreserveWhitespace = false;
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x00069215 File Offset: 0x00067415
		private Report Report
		{
			get
			{
				return this.module.Compiler.Report;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x00069227 File Offset: 0x00067427
		// (set) Token: 0x060015EF RID: 5615 RVA: 0x0006922F File Offset: 0x0006742F
		public MemberName ParsedName
		{
			[CompilerGenerated]
			get
			{
				return this.<ParsedName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ParsedName>k__BackingField = value;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x00069238 File Offset: 0x00067438
		// (set) Token: 0x060015F1 RID: 5617 RVA: 0x00069240 File Offset: 0x00067440
		public List<DocumentationParameter> ParsedParameters
		{
			[CompilerGenerated]
			get
			{
				return this.<ParsedParameters>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ParsedParameters>k__BackingField = value;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x00069249 File Offset: 0x00067449
		// (set) Token: 0x060015F3 RID: 5619 RVA: 0x00069251 File Offset: 0x00067451
		public TypeExpression ParsedBuiltinType
		{
			[CompilerGenerated]
			get
			{
				return this.<ParsedBuiltinType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ParsedBuiltinType>k__BackingField = value;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x0006925A File Offset: 0x0006745A
		// (set) Token: 0x060015F5 RID: 5621 RVA: 0x00069262 File Offset: 0x00067462
		public Operator.OpType? ParsedOperator
		{
			[CompilerGenerated]
			get
			{
				return this.<ParsedOperator>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ParsedOperator>k__BackingField = value;
			}
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x0006926C File Offset: 0x0006746C
		private XmlNode GetDocCommentNode(MemberCore mc, string name)
		{
			XmlDocument xmlDocumentation = this.XmlDocumentation;
			XmlNode result;
			try
			{
				XmlElement xmlElement = xmlDocumentation.CreateElement("member");
				xmlElement.SetAttribute("name", name);
				string docComment = mc.DocComment;
				xmlElement.InnerXml = docComment;
				string[] array = docComment.Split(new char[]
				{
					'\n'
				});
				int count = 0;
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i].TrimEnd(new char[0]);
					if (text.Length > 0)
					{
						array[count++] = text;
					}
				}
				xmlElement.InnerXml = DocumentationBuilder.line_head + string.Join(DocumentationBuilder.line_head, array, 0, count);
				result = xmlElement;
			}
			catch (Exception ex)
			{
				this.Report.Warning(1570, 1, mc.Location, "XML documentation comment on `{0}' is not well-formed XML markup ({1})", mc.GetSignatureForError(), ex.Message);
				result = xmlDocumentation.CreateComment(string.Format("FIXME: Invalid documentation markup was found for member {0}", name));
			}
			return result;
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00069368 File Offset: 0x00067568
		public void GenerateDocumentationForMember(MemberCore mc)
		{
			string name = mc.DocCommentHeader + mc.GetSignatureForDocumentation();
			XmlNode docCommentNode = this.GetDocCommentNode(mc, name);
			XmlElement xmlElement = docCommentNode as XmlElement;
			if (xmlElement != null)
			{
				IParametersMember parametersMember = mc as IParametersMember;
				if (parametersMember != null)
				{
					this.CheckParametersComments(mc, parametersMember, xmlElement);
				}
				XmlNodeList xmlNodeList = docCommentNode.SelectNodes(".//include");
				if (xmlNodeList.Count > 0)
				{
					List<XmlNode> list = new List<XmlNode>(xmlNodeList.Count);
					foreach (object obj in xmlNodeList)
					{
						XmlNode item = (XmlNode)obj;
						list.Add(item);
					}
					foreach (XmlNode xmlNode in list)
					{
						XmlElement xmlElement2 = (XmlElement)xmlNode;
						if (!this.HandleInclude(mc, xmlElement2))
						{
							xmlElement2.ParentNode.RemoveChild(xmlElement2);
						}
					}
				}
				foreach (object obj2 in docCommentNode.SelectNodes(".//see"))
				{
					XmlElement see = (XmlElement)obj2;
					this.HandleSee(mc, see);
				}
				foreach (object obj3 in docCommentNode.SelectNodes(".//seealso"))
				{
					XmlElement seealso = (XmlElement)obj3;
					this.HandleSeeAlso(mc, seealso);
				}
				foreach (object obj4 in docCommentNode.SelectNodes(".//exception"))
				{
					XmlElement seealso2 = (XmlElement)obj4;
					this.HandleException(mc, seealso2);
				}
				foreach (object obj5 in docCommentNode.SelectNodes(".//typeparam"))
				{
					XmlElement node = (XmlElement)obj5;
					DocumentationBuilder.HandleTypeParam(mc, node);
				}
				foreach (object obj6 in docCommentNode.SelectNodes(".//typeparamref"))
				{
					XmlElement node2 = (XmlElement)obj6;
					DocumentationBuilder.HandleTypeParamRef(mc, node2);
				}
			}
			docCommentNode.WriteTo(this.XmlCommentOutput);
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00069640 File Offset: 0x00067840
		private bool HandleInclude(MemberCore mc, XmlElement el)
		{
			bool result = false;
			string attribute = el.GetAttribute("file");
			string attribute2 = el.GetAttribute("path");
			if (attribute == "")
			{
				this.Report.Warning(1590, 1, mc.Location, "Invalid XML `include' element. Missing `file' attribute");
				el.ParentNode.InsertBefore(el.OwnerDocument.CreateComment(" Include tag is invalid "), el);
				result = true;
			}
			else if (attribute2.Length == 0)
			{
				this.Report.Warning(1590, 1, mc.Location, "Invalid XML `include' element. Missing `path' attribute");
				el.ParentNode.InsertBefore(el.OwnerDocument.CreateComment(" Include tag is invalid "), el);
				result = true;
			}
			else
			{
				Exception ex = null;
				string text = Path.Combine(Path.GetDirectoryName(mc.Location.NameFullPath), attribute);
				XmlDocument xmlDocument;
				if (!this.StoredDocuments.TryGetValue(text, out xmlDocument))
				{
					try
					{
						xmlDocument = new XmlDocument();
						xmlDocument.Load(text);
						this.StoredDocuments.Add(text, xmlDocument);
					}
					catch (Exception ex)
					{
						el.ParentNode.InsertBefore(el.OwnerDocument.CreateComment(string.Format(" Badly formed XML in at comment file `{0}': cannot be included ", attribute)), el);
					}
				}
				if (xmlDocument != null)
				{
					try
					{
						XmlNodeList xmlNodeList = xmlDocument.SelectNodes(attribute2);
						if (xmlNodeList.Count == 0)
						{
							el.ParentNode.InsertBefore(el.OwnerDocument.CreateComment(" No matching elements were found for the include tag embedded here. "), el);
							result = true;
						}
						foreach (object obj in xmlNodeList)
						{
							XmlNode node = (XmlNode)obj;
							el.ParentNode.InsertBefore(el.OwnerDocument.ImportNode(node, true), el);
						}
					}
					catch (Exception ex)
					{
						el.ParentNode.InsertBefore(el.OwnerDocument.CreateComment(" Failed to insert some or all of included XML "), el);
					}
				}
				if (ex != null)
				{
					this.Report.Warning(1589, 1, mc.Location, "Unable to include XML fragment `{0}' of file `{1}'. {2}", new object[]
					{
						attribute2,
						attribute,
						ex.Message
					});
				}
			}
			return result;
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x0006987C File Offset: 0x00067A7C
		private void HandleSee(MemberCore mc, XmlElement see)
		{
			this.HandleXrefCommon(mc, see);
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x0006987C File Offset: 0x00067A7C
		private void HandleSeeAlso(MemberCore mc, XmlElement seealso)
		{
			this.HandleXrefCommon(mc, seealso);
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x0006987C File Offset: 0x00067A7C
		private void HandleException(MemberCore mc, XmlElement seealso)
		{
			this.HandleXrefCommon(mc, seealso);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00069888 File Offset: 0x00067A88
		private static void HandleTypeParam(MemberCore mc, XmlElement node)
		{
			if (!node.HasAttribute("name"))
			{
				return;
			}
			string attribute = node.GetAttribute("name");
			if (mc.CurrentTypeParameters != null && mc.CurrentTypeParameters.Find(attribute) != null)
			{
				return;
			}
			mc.Compiler.Report.Warning(1711, 2, mc.Location, "XML comment on `{0}' has a typeparam name `{1}' but there is no type parameter by that name", mc.GetSignatureForError(), attribute);
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x000698F0 File Offset: 0x00067AF0
		private static void HandleTypeParamRef(MemberCore mc, XmlElement node)
		{
			if (!node.HasAttribute("name"))
			{
				return;
			}
			string attribute = node.GetAttribute("name");
			MemberCore memberCore = mc;
			while (memberCore.CurrentTypeParameters == null || memberCore.CurrentTypeParameters.Find(attribute) == null)
			{
				memberCore = memberCore.Parent;
				if (memberCore == null)
				{
					mc.Compiler.Report.Warning(1735, 2, mc.Location, "XML comment on `{0}' has a typeparamref name `{1}' that could not be resolved", mc.GetSignatureForError(), attribute);
					return;
				}
			}
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00069964 File Offset: 0x00067B64
		private FullNamedExpression ResolveMemberName(IMemberContext context, MemberName mn)
		{
			if (mn.Left == null)
			{
				return context.LookupNamespaceOrType(mn.Name, mn.Arity, LookupMode.Probing, Location.Null);
			}
			FullNamedExpression fullNamedExpression = this.ResolveMemberName(context, mn.Left);
			NamespaceExpression namespaceExpression = fullNamedExpression as NamespaceExpression;
			if (namespaceExpression != null)
			{
				return namespaceExpression.LookupTypeOrNamespace(context, mn.Name, mn.Arity, LookupMode.Probing, Location.Null);
			}
			TypeExpr typeExpr = fullNamedExpression as TypeExpr;
			if (typeExpr == null)
			{
				return fullNamedExpression;
			}
			TypeSpec typeSpec = MemberCache.FindNestedType(typeExpr.Type, mn.Name, mn.Arity);
			if (typeSpec != null)
			{
				return new TypeExpression(typeSpec, Location.Null);
			}
			return null;
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x000699F8 File Offset: 0x00067BF8
		private void HandleXrefCommon(MemberCore mc, XmlElement xref)
		{
			string text = xref.GetAttribute("cref");
			if (!xref.HasAttribute("cref"))
			{
				return;
			}
			if (text.Length > 2 && text[1] == ':')
			{
				return;
			}
			text = text.Replace('{', '<').Replace('}', '>');
			Encoding encoding = this.module.Compiler.Settings.Encoding;
			Stream stream = new MemoryStream(encoding.GetBytes(text));
			CompilationSourceFile file = new CompilationSourceFile(this.doc_module, mc.Location.SourceFile);
			Report report = new Report(this.doc_module.Compiler, new NullReportPrinter());
			if (this.session == null)
			{
				this.session = new ParserSession
				{
					UseJayGlobalArrays = true
				};
			}
			CSharpParser csharpParser = new CSharpParser(new SeekableStreamReader(stream, encoding, this.session.StreamReaderBuffer), file, report, this.session);
			this.ParsedParameters = null;
			this.ParsedName = null;
			this.ParsedBuiltinType = null;
			this.ParsedOperator = null;
			csharpParser.Lexer.putback_char = 1048579;
			csharpParser.Lexer.parsing_generic_declaration_doc = true;
			csharpParser.parse();
			if (report.Errors > 0)
			{
				this.Report.Warning(1584, 1, mc.Location, "XML comment on `{0}' has syntactically incorrect cref attribute `{1}'", mc.GetSignatureForError(), text);
				xref.SetAttribute("cref", "!:" + text);
				return;
			}
			FullNamedExpression fullNamedExpression = null;
			MemberSpec memberSpec;
			if (this.ParsedBuiltinType != null && (this.ParsedParameters == null || this.ParsedName != null))
			{
				memberSpec = this.ParsedBuiltinType.Type;
			}
			else
			{
				memberSpec = null;
			}
			if (this.ParsedName != null || this.ParsedOperator != null)
			{
				TypeSpec typeSpec = null;
				string text2 = null;
				if (memberSpec == null)
				{
					if (this.ParsedOperator != null)
					{
						typeSpec = mc.CurrentType;
					}
					else if (this.ParsedName.Left != null)
					{
						fullNamedExpression = this.ResolveMemberName(mc, this.ParsedName.Left);
						if (fullNamedExpression != null)
						{
							NamespaceExpression namespaceExpression = fullNamedExpression as NamespaceExpression;
							if (namespaceExpression != null)
							{
								fullNamedExpression = namespaceExpression.LookupTypeOrNamespace(mc, this.ParsedName.Name, this.ParsedName.Arity, LookupMode.Probing, Location.Null);
								if (fullNamedExpression != null)
								{
									memberSpec = fullNamedExpression.Type;
								}
							}
							else
							{
								typeSpec = fullNamedExpression.Type;
							}
						}
					}
					else
					{
						fullNamedExpression = this.ResolveMemberName(mc, this.ParsedName);
						if (fullNamedExpression == null)
						{
							typeSpec = mc.CurrentType;
						}
						else if (this.ParsedParameters == null)
						{
							memberSpec = fullNamedExpression.Type;
						}
						else if (fullNamedExpression.Type.MemberDefinition == mc.CurrentType.MemberDefinition)
						{
							text2 = Constructor.ConstructorName;
							typeSpec = fullNamedExpression.Type;
						}
					}
				}
				else
				{
					typeSpec = (TypeSpec)memberSpec;
					memberSpec = null;
				}
				if (this.ParsedParameters != null)
				{
					ReportPrinter printer = mc.Module.Compiler.Report.SetPrinter(new NullReportPrinter());
					try
					{
						DocumentationMemberContext context = new DocumentationMemberContext(mc, this.ParsedName ?? MemberName.Null);
						foreach (DocumentationParameter documentationParameter in this.ParsedParameters)
						{
							documentationParameter.Resolve(context);
						}
					}
					finally
					{
						mc.Module.Compiler.Report.SetPrinter(printer);
					}
				}
				if (typeSpec != null)
				{
					if (text2 == null)
					{
						text2 = ((this.ParsedOperator != null) ? Operator.GetMetadataName(this.ParsedOperator.Value) : this.ParsedName.Name);
					}
					int num;
					if (this.ParsedOperator == Operator.OpType.Explicit || this.ParsedOperator == Operator.OpType.Implicit)
					{
						num = this.ParsedParameters.Count - 1;
					}
					else if (this.ParsedParameters != null)
					{
						num = this.ParsedParameters.Count;
					}
					else
					{
						num = 0;
					}
					int num2 = -1;
					do
					{
						IList<MemberSpec> list = MemberCache.FindMembers(typeSpec, text2, true);
						if (list != null)
						{
							foreach (MemberSpec memberSpec2 in list)
							{
								if (this.ParsedName == null || memberSpec2.Arity == this.ParsedName.Arity)
								{
									if (this.ParsedParameters != null)
									{
										IParametersMember parametersMember = memberSpec2 as IParametersMember;
										if (parametersMember == null || (memberSpec2.Kind == MemberKind.Operator && this.ParsedOperator == null))
										{
											continue;
										}
										AParametersCollection parameters = parametersMember.Parameters;
										int i;
										for (i = 0; i < num; i++)
										{
											DocumentationParameter documentationParameter2 = this.ParsedParameters[i];
											if (i >= parameters.Count || documentationParameter2 == null || documentationParameter2.TypeSpec == null || !TypeSpecComparer.Override.IsEqual(documentationParameter2.TypeSpec, parameters.Types[i]) || (documentationParameter2.Modifier & Parameter.Modifier.RefOutMask) != (parameters.FixedParameters[i].ModFlags & Parameter.Modifier.RefOutMask))
											{
												if (i > num2)
												{
													num2 = i;
												}
												i = -1;
												break;
											}
										}
										if (i < 0)
										{
											continue;
										}
										if (this.ParsedOperator == Operator.OpType.Explicit || this.ParsedOperator == Operator.OpType.Implicit)
										{
											if (parametersMember.MemberType != this.ParsedParameters[num].TypeSpec)
											{
												num2 = num + 1;
												continue;
											}
										}
										else if (num != parameters.Count)
										{
											continue;
										}
									}
									if (memberSpec != null)
									{
										this.Report.Warning(419, 3, mc.Location, "Ambiguous reference in cref attribute `{0}'. Assuming `{1}' but other overloads including `{2}' have also matched", new object[]
										{
											text,
											memberSpec.GetSignatureForError(),
											memberSpec2.GetSignatureForError()
										});
										break;
									}
									memberSpec = memberSpec2;
								}
							}
						}
						if (memberSpec == null)
						{
							typeSpec = typeSpec.DeclaringType;
						}
						else
						{
							typeSpec = null;
						}
					}
					while (typeSpec != null);
					if (memberSpec == null && num2 >= 0)
					{
						for (int j = num2; j < num; j++)
						{
							this.Report.Warning(1580, 1, mc.Location, "Invalid type for parameter `{0}' in XML comment cref attribute `{1}'", (j + 1).ToString(), text);
						}
						if (num2 == num + 1)
						{
							this.Report.Warning(1581, 1, mc.Location, "Invalid return type in XML comment cref attribute `{0}'", text);
						}
					}
				}
			}
			if (memberSpec == null)
			{
				this.Report.Warning(1574, 1, mc.Location, "XML comment on `{0}' has cref attribute `{1}' that could not be resolved", mc.GetSignatureForError(), text);
				text = "!:" + text;
			}
			else if (memberSpec == InternalType.Namespace)
			{
				text = "N:" + fullNamedExpression.GetSignatureForError();
			}
			else
			{
				text = DocumentationBuilder.GetMemberDocHead(memberSpec) + memberSpec.GetSignatureForDocumentation();
			}
			xref.SetAttribute("cref", text);
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x0006A110 File Offset: 0x00068310
		private static string GetMemberDocHead(MemberSpec type)
		{
			if (type is FieldSpec)
			{
				return "F:";
			}
			if (type is MethodSpec)
			{
				return "M:";
			}
			if (type is EventSpec)
			{
				return "E:";
			}
			if (type is PropertySpec)
			{
				return "P:";
			}
			if (type is TypeSpec)
			{
				return "T:";
			}
			throw new NotImplementedException(type.GetType().ToString());
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x0006A174 File Offset: 0x00068374
		private void CheckParametersComments(MemberCore member, IParametersMember paramMember, XmlElement el)
		{
			HashSet<string> hashSet = null;
			foreach (object obj in el.SelectNodes("param"))
			{
				string attribute = ((XmlElement)obj).GetAttribute("name");
				if (attribute.Length != 0)
				{
					if (hashSet == null)
					{
						hashSet = new HashSet<string>();
					}
					if (attribute != "" && paramMember.Parameters.GetParameterIndexByName(attribute) < 0)
					{
						this.Report.Warning(1572, 2, member.Location, "XML comment on `{0}' has a param tag for `{1}', but there is no parameter by that name", member.GetSignatureForError(), attribute);
					}
					else if (hashSet.Contains(attribute))
					{
						this.Report.Warning(1571, 2, member.Location, "XML comment on `{0}' has a duplicate param tag for `{1}'", member.GetSignatureForError(), attribute);
					}
					else
					{
						hashSet.Add(attribute);
					}
				}
			}
			if (hashSet != null)
			{
				foreach (Parameter parameter in paramMember.Parameters.FixedParameters)
				{
					if (!hashSet.Contains(parameter.Name) && !(parameter is ArglistParameter))
					{
						this.Report.Warning(1573, 4, member.Location, "Parameter `{0}' has no matching param tag in the XML comment for `{1}'", parameter.Name, member.GetSignatureForError());
					}
				}
			}
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0006A2D8 File Offset: 0x000684D8
		public bool OutputDocComment(string asmfilename, string xmlFileName)
		{
			XmlTextWriter xmlTextWriter = null;
			bool result;
			try
			{
				xmlTextWriter = new XmlTextWriter(xmlFileName, null);
				xmlTextWriter.Indentation = 4;
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.WriteStartDocument();
				xmlTextWriter.WriteStartElement("doc");
				xmlTextWriter.WriteStartElement("assembly");
				xmlTextWriter.WriteStartElement("name");
				xmlTextWriter.WriteString(Path.GetFileNameWithoutExtension(asmfilename));
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("members");
				this.XmlCommentOutput = xmlTextWriter;
				this.module.GenerateDocComment(this);
				xmlTextWriter.WriteFullEndElement();
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteWhitespace(Environment.NewLine);
				xmlTextWriter.WriteEndDocument();
				result = true;
			}
			catch (Exception ex)
			{
				this.Report.Error(1569, "Error generating XML documentation file `{0}' (`{1}')", xmlFileName, ex.Message);
				result = false;
			}
			finally
			{
				if (xmlTextWriter != null)
				{
					xmlTextWriter.Close();
				}
			}
			return result;
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0006A3C4 File Offset: 0x000685C4
		// Note: this type is marked as 'beforefieldinit'.
		static DocumentationBuilder()
		{
		}

		// Token: 0x04000929 RID: 2345
		private readonly XmlDocument XmlDocumentation;

		// Token: 0x0400092A RID: 2346
		private readonly ModuleContainer module;

		// Token: 0x0400092B RID: 2347
		private readonly ModuleContainer doc_module;

		// Token: 0x0400092C RID: 2348
		private XmlWriter XmlCommentOutput;

		// Token: 0x0400092D RID: 2349
		private static readonly string line_head = Environment.NewLine + "            ";

		// Token: 0x0400092E RID: 2350
		private Dictionary<string, XmlDocument> StoredDocuments = new Dictionary<string, XmlDocument>();

		// Token: 0x0400092F RID: 2351
		private ParserSession session;

		// Token: 0x04000930 RID: 2352
		[CompilerGenerated]
		private MemberName <ParsedName>k__BackingField;

		// Token: 0x04000931 RID: 2353
		[CompilerGenerated]
		private List<DocumentationParameter> <ParsedParameters>k__BackingField;

		// Token: 0x04000932 RID: 2354
		[CompilerGenerated]
		private TypeExpression <ParsedBuiltinType>k__BackingField;

		// Token: 0x04000933 RID: 2355
		[CompilerGenerated]
		private Operator.OpType? <ParsedOperator>k__BackingField;
	}
}
