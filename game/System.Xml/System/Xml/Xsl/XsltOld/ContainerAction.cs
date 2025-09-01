using System;
using System.Collections;
using System.Globalization;
using System.Xml.XPath;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000363 RID: 867
	internal class ContainerAction : CompiledAction
	{
		// Token: 0x060023E9 RID: 9193 RVA: 0x0000349C File Offset: 0x0000169C
		internal override void Compile(Compiler compiler)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x000DD898 File Offset: 0x000DBA98
		internal void CompileStylesheetAttributes(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			string localName = input.LocalName;
			string text = null;
			string text2 = null;
			if (input.MoveToFirstAttribute())
			{
				for (;;)
				{
					string namespaceURI = input.NamespaceURI;
					string localName2 = input.LocalName;
					if (namespaceURI.Length == 0)
					{
						if (Ref.Equal(localName2, input.Atoms.Version))
						{
							text2 = input.Value;
							if (1.0 <= XmlConvert.ToXPathDouble(text2))
							{
								compiler.ForwardCompatibility = (text2 != "1.0");
							}
							else if (!compiler.ForwardCompatibility)
							{
								break;
							}
						}
						else if (Ref.Equal(localName2, input.Atoms.ExtensionElementPrefixes))
						{
							compiler.InsertExtensionNamespace(input.Value);
						}
						else if (Ref.Equal(localName2, input.Atoms.ExcludeResultPrefixes))
						{
							compiler.InsertExcludedNamespace(input.Value);
						}
						else if (!Ref.Equal(localName2, input.Atoms.Id))
						{
							text = localName2;
						}
					}
					if (!input.MoveToNextAttribute())
					{
						goto Block_8;
					}
				}
				throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[]
				{
					"version",
					text2
				});
				Block_8:
				input.ToParent();
			}
			if (text2 == null)
			{
				throw XsltException.Create("Missing mandatory attribute '{0}'.", new string[]
				{
					"version"
				});
			}
			if (text != null && !compiler.ForwardCompatibility)
			{
				throw XsltException.Create("'{0}' is an invalid attribute for the '{1}' element.", new string[]
				{
					text,
					localName
				});
			}
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x000DD9F0 File Offset: 0x000DBBF0
		internal void CompileSingleTemplate(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			string text = null;
			if (input.MoveToFirstAttribute())
			{
				do
				{
					string namespaceURI = input.NamespaceURI;
					string localName = input.LocalName;
					if (Ref.Equal(namespaceURI, input.Atoms.UriXsl) && Ref.Equal(localName, input.Atoms.Version))
					{
						text = input.Value;
					}
				}
				while (input.MoveToNextAttribute());
				input.ToParent();
			}
			if (text != null)
			{
				compiler.AddTemplate(compiler.CreateSingleTemplateAction());
				return;
			}
			if (Ref.Equal(input.LocalName, input.Atoms.Stylesheet) && input.NamespaceURI == "http://www.w3.org/TR/WD-xsl")
			{
				throw XsltException.Create("The 'http://www.w3.org/TR/WD-xsl' namespace is no longer supported.", Array.Empty<string>());
			}
			throw XsltException.Create("Stylesheet must start either with an 'xsl:stylesheet' or an 'xsl:transform' element, or with a literal result element that has an 'xsl:version' attribute, where prefix 'xsl' denotes the 'http://www.w3.org/1999/XSL/Transform' namespace.", Array.Empty<string>());
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x000DDAB0 File Offset: 0x000DBCB0
		protected void CompileDocument(Compiler compiler, bool inInclude)
		{
			NavigatorInput input = compiler.Input;
			while (input.NodeType != XPathNodeType.Element)
			{
				if (!compiler.Advance())
				{
					throw XsltException.Create("Stylesheet must start either with an 'xsl:stylesheet' or an 'xsl:transform' element, or with a literal result element that has an 'xsl:version' attribute, where prefix 'xsl' denotes the 'http://www.w3.org/1999/XSL/Transform' namespace.", Array.Empty<string>());
				}
			}
			if (Ref.Equal(input.NamespaceURI, input.Atoms.UriXsl))
			{
				if (!Ref.Equal(input.LocalName, input.Atoms.Stylesheet) && !Ref.Equal(input.LocalName, input.Atoms.Transform))
				{
					throw XsltException.Create("Stylesheet must start either with an 'xsl:stylesheet' or an 'xsl:transform' element, or with a literal result element that has an 'xsl:version' attribute, where prefix 'xsl' denotes the 'http://www.w3.org/1999/XSL/Transform' namespace.", Array.Empty<string>());
				}
				compiler.PushNamespaceScope();
				this.CompileStylesheetAttributes(compiler);
				this.CompileTopLevelElements(compiler);
				if (!inInclude)
				{
					this.CompileImports(compiler);
				}
			}
			else
			{
				compiler.PushLiteralScope();
				this.CompileSingleTemplate(compiler);
			}
			compiler.PopScope();
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x000DDB74 File Offset: 0x000DBD74
		internal Stylesheet CompileImport(Compiler compiler, Uri uri, int id)
		{
			NavigatorInput navigatorInput = compiler.ResolveDocument(uri);
			compiler.PushInputDocument(navigatorInput);
			try
			{
				compiler.PushStylesheet(new Stylesheet());
				compiler.Stylesheetid = id;
				this.CompileDocument(compiler, false);
			}
			catch (XsltCompileException)
			{
				throw;
			}
			catch (Exception inner)
			{
				throw new XsltCompileException(inner, navigatorInput.BaseURI, navigatorInput.LineNumber, navigatorInput.LinePosition);
			}
			finally
			{
				compiler.PopInputDocument();
			}
			return compiler.PopStylesheet();
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x000DDBFC File Offset: 0x000DBDFC
		private void CompileImports(Compiler compiler)
		{
			ArrayList imports = compiler.CompiledStylesheet.Imports;
			int stylesheetid = compiler.Stylesheetid;
			int num = imports.Count - 1;
			while (0 <= num)
			{
				Uri uri = imports[num] as Uri;
				ArrayList arrayList = imports;
				int index = num;
				Uri uri2 = uri;
				int id = this.maxid + 1;
				this.maxid = id;
				arrayList[index] = this.CompileImport(compiler, uri2, id);
				num--;
			}
			compiler.Stylesheetid = stylesheetid;
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x000DDC68 File Offset: 0x000DBE68
		private void CompileInclude(Compiler compiler)
		{
			Uri uri = compiler.ResolveUri(compiler.GetSingleAttribute(compiler.Input.Atoms.Href));
			string text = uri.ToString();
			if (compiler.IsCircularReference(text))
			{
				throw XsltException.Create("Stylesheet '{0}' cannot directly or indirectly include or import itself.", new string[]
				{
					text
				});
			}
			NavigatorInput navigatorInput = compiler.ResolveDocument(uri);
			compiler.PushInputDocument(navigatorInput);
			try
			{
				this.CompileDocument(compiler, true);
			}
			catch (XsltCompileException)
			{
				throw;
			}
			catch (Exception inner)
			{
				throw new XsltCompileException(inner, navigatorInput.BaseURI, navigatorInput.LineNumber, navigatorInput.LinePosition);
			}
			finally
			{
				compiler.PopInputDocument();
			}
			base.CheckEmpty(compiler);
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x000DDD24 File Offset: 0x000DBF24
		internal void CompileNamespaceAlias(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			string localName = input.LocalName;
			string text = null;
			string text2 = null;
			string text3 = null;
			string prefix = null;
			if (input.MoveToFirstAttribute())
			{
				string localName2;
				for (;;)
				{
					string namespaceURI = input.NamespaceURI;
					localName2 = input.LocalName;
					if (namespaceURI.Length == 0)
					{
						if (Ref.Equal(localName2, input.Atoms.StylesheetPrefix))
						{
							text3 = input.Value;
							text = compiler.GetNsAlias(ref text3);
						}
						else if (Ref.Equal(localName2, input.Atoms.ResultPrefix))
						{
							prefix = input.Value;
							text2 = compiler.GetNsAlias(ref prefix);
						}
						else if (!compiler.ForwardCompatibility)
						{
							break;
						}
					}
					if (!input.MoveToNextAttribute())
					{
						goto Block_5;
					}
				}
				throw XsltException.Create("'{0}' is an invalid attribute for the '{1}' element.", new string[]
				{
					localName2,
					localName
				});
				Block_5:
				input.ToParent();
			}
			base.CheckRequiredAttribute(compiler, text, "stylesheet-prefix");
			base.CheckRequiredAttribute(compiler, text2, "result-prefix");
			base.CheckEmpty(compiler);
			compiler.AddNamespaceAlias(text, new NamespaceInfo(prefix, text2, compiler.Stylesheetid));
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x000DDE24 File Offset: 0x000DC024
		internal void CompileKey(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			string localName = input.LocalName;
			int num = -1;
			int num2 = -1;
			XmlQualifiedName xmlQualifiedName = null;
			if (input.MoveToFirstAttribute())
			{
				string localName2;
				for (;;)
				{
					string namespaceURI = input.NamespaceURI;
					localName2 = input.LocalName;
					string value = input.Value;
					if (namespaceURI.Length == 0)
					{
						if (Ref.Equal(localName2, input.Atoms.Name))
						{
							xmlQualifiedName = compiler.CreateXPathQName(value);
						}
						else if (Ref.Equal(localName2, input.Atoms.Match))
						{
							num = compiler.AddQuery(value, false, false, true);
						}
						else if (Ref.Equal(localName2, input.Atoms.Use))
						{
							num2 = compiler.AddQuery(value, false, false, false);
						}
						else if (!compiler.ForwardCompatibility)
						{
							break;
						}
					}
					if (!input.MoveToNextAttribute())
					{
						goto Block_6;
					}
				}
				throw XsltException.Create("'{0}' is an invalid attribute for the '{1}' element.", new string[]
				{
					localName2,
					localName
				});
				Block_6:
				input.ToParent();
			}
			base.CheckRequiredAttribute(compiler, num != -1, "match");
			base.CheckRequiredAttribute(compiler, num2 != -1, "use");
			base.CheckRequiredAttribute(compiler, xmlQualifiedName != null, "name");
			compiler.InsertKey(xmlQualifiedName, num, num2);
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x000DDF50 File Offset: 0x000DC150
		protected void CompileDecimalFormat(Compiler compiler)
		{
			NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
			DecimalFormat decimalFormat = new DecimalFormat(numberFormatInfo, '#', '0', ';');
			XmlQualifiedName xmlQualifiedName = null;
			NavigatorInput input = compiler.Input;
			if (input.MoveToFirstAttribute())
			{
				do
				{
					if (input.Prefix.Length == 0)
					{
						string localName = input.LocalName;
						string value = input.Value;
						if (Ref.Equal(localName, input.Atoms.Name))
						{
							xmlQualifiedName = compiler.CreateXPathQName(value);
						}
						else if (Ref.Equal(localName, input.Atoms.DecimalSeparator))
						{
							numberFormatInfo.NumberDecimalSeparator = value;
						}
						else if (Ref.Equal(localName, input.Atoms.GroupingSeparator))
						{
							numberFormatInfo.NumberGroupSeparator = value;
						}
						else if (Ref.Equal(localName, input.Atoms.Infinity))
						{
							numberFormatInfo.PositiveInfinitySymbol = value;
						}
						else if (Ref.Equal(localName, input.Atoms.MinusSign))
						{
							numberFormatInfo.NegativeSign = value;
						}
						else if (Ref.Equal(localName, input.Atoms.NaN))
						{
							numberFormatInfo.NaNSymbol = value;
						}
						else if (Ref.Equal(localName, input.Atoms.Percent))
						{
							numberFormatInfo.PercentSymbol = value;
						}
						else if (Ref.Equal(localName, input.Atoms.PerMille))
						{
							numberFormatInfo.PerMilleSymbol = value;
						}
						else if (Ref.Equal(localName, input.Atoms.Digit))
						{
							if (this.CheckAttribute(value.Length == 1, compiler))
							{
								decimalFormat.digit = value[0];
							}
						}
						else if (Ref.Equal(localName, input.Atoms.ZeroDigit))
						{
							if (this.CheckAttribute(value.Length == 1, compiler))
							{
								decimalFormat.zeroDigit = value[0];
							}
						}
						else if (Ref.Equal(localName, input.Atoms.PatternSeparator) && this.CheckAttribute(value.Length == 1, compiler))
						{
							decimalFormat.patternSeparator = value[0];
						}
					}
				}
				while (input.MoveToNextAttribute());
				input.ToParent();
			}
			numberFormatInfo.NegativeInfinitySymbol = numberFormatInfo.NegativeSign + numberFormatInfo.PositiveInfinitySymbol;
			if (xmlQualifiedName == null)
			{
				xmlQualifiedName = new XmlQualifiedName();
			}
			compiler.AddDecimalFormat(xmlQualifiedName, decimalFormat);
			base.CheckEmpty(compiler);
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x000DE197 File Offset: 0x000DC397
		internal bool CheckAttribute(bool valid, Compiler compiler)
		{
			if (valid)
			{
				return true;
			}
			if (!compiler.ForwardCompatibility)
			{
				throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[]
				{
					compiler.Input.LocalName,
					compiler.Input.Value
				});
			}
			return false;
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x000DE1D4 File Offset: 0x000DC3D4
		protected void CompileSpace(Compiler compiler, bool preserve)
		{
			string[] array = XmlConvert.SplitString(compiler.GetSingleAttribute(compiler.Input.Atoms.Elements));
			for (int i = 0; i < array.Length; i++)
			{
				double priority = this.NameTest(array[i]);
				compiler.CompiledStylesheet.AddSpace(compiler, array[i], priority, preserve);
			}
			base.CheckEmpty(compiler);
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x000DE230 File Offset: 0x000DC430
		private double NameTest(string name)
		{
			if (name == "*")
			{
				return -0.5;
			}
			int num = name.Length - 2;
			if (0 > num || name[num] != ':' || name[num + 1] != '*')
			{
				string text;
				string text2;
				PrefixQName.ParseQualifiedName(name, out text, out text2);
				return 0.0;
			}
			if (!PrefixQName.ValidatePrefix(name.Substring(0, num)))
			{
				throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[]
				{
					"elements",
					name
				});
			}
			return -0.25;
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x000DE2C4 File Offset: 0x000DC4C4
		protected void CompileTopLevelElements(Compiler compiler)
		{
			if (!compiler.Recurse())
			{
				return;
			}
			NavigatorInput input = compiler.Input;
			bool flag = false;
			string text;
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
					string localName = input.LocalName;
					string namespaceURI = input.NamespaceURI;
					if (Ref.Equal(namespaceURI, input.Atoms.UriXsl))
					{
						if (Ref.Equal(localName, input.Atoms.Import))
						{
							if (flag)
							{
								goto Block_6;
							}
							Uri uri = compiler.ResolveUri(compiler.GetSingleAttribute(compiler.Input.Atoms.Href));
							text = uri.ToString();
							if (compiler.IsCircularReference(text))
							{
								goto Block_7;
							}
							compiler.CompiledStylesheet.Imports.Add(uri);
							base.CheckEmpty(compiler);
						}
						else if (Ref.Equal(localName, input.Atoms.Include))
						{
							flag = true;
							this.CompileInclude(compiler);
						}
						else
						{
							flag = true;
							compiler.PushNamespaceScope();
							if (Ref.Equal(localName, input.Atoms.StripSpace))
							{
								this.CompileSpace(compiler, false);
							}
							else if (Ref.Equal(localName, input.Atoms.PreserveSpace))
							{
								this.CompileSpace(compiler, true);
							}
							else if (Ref.Equal(localName, input.Atoms.Output))
							{
								this.CompileOutput(compiler);
							}
							else if (Ref.Equal(localName, input.Atoms.Key))
							{
								this.CompileKey(compiler);
							}
							else if (Ref.Equal(localName, input.Atoms.DecimalFormat))
							{
								this.CompileDecimalFormat(compiler);
							}
							else if (Ref.Equal(localName, input.Atoms.NamespaceAlias))
							{
								this.CompileNamespaceAlias(compiler);
							}
							else if (Ref.Equal(localName, input.Atoms.AttributeSet))
							{
								compiler.AddAttributeSet(compiler.CreateAttributeSetAction());
							}
							else if (Ref.Equal(localName, input.Atoms.Variable))
							{
								VariableAction variableAction = compiler.CreateVariableAction(VariableType.GlobalVariable);
								if (variableAction != null)
								{
									this.AddAction(variableAction);
								}
							}
							else if (Ref.Equal(localName, input.Atoms.Param))
							{
								VariableAction variableAction2 = compiler.CreateVariableAction(VariableType.GlobalParameter);
								if (variableAction2 != null)
								{
									this.AddAction(variableAction2);
								}
							}
							else if (Ref.Equal(localName, input.Atoms.Template))
							{
								compiler.AddTemplate(compiler.CreateTemplateAction());
							}
							else if (!compiler.ForwardCompatibility)
							{
								goto Block_21;
							}
							compiler.PopScope();
						}
					}
					else if (namespaceURI == input.Atoms.UrnMsxsl && localName == input.Atoms.Script)
					{
						this.AddScript(compiler);
					}
					else if (namespaceURI.Length == 0)
					{
						goto Block_24;
					}
				}
				if (!compiler.Advance())
				{
					goto Block_25;
				}
			}
			throw XsltException.Create("The contents of '{0}' are invalid.", new string[]
			{
				"stylesheet"
			});
			Block_6:
			throw XsltException.Create("'xsl:import' instructions must precede all other element children of an 'xsl:stylesheet' element.", Array.Empty<string>());
			Block_7:
			throw XsltException.Create("Stylesheet '{0}' cannot directly or indirectly include or import itself.", new string[]
			{
				text
			});
			Block_21:
			throw compiler.UnexpectedKeyword();
			Block_24:
			throw XsltException.Create("Top-level element '{0}' may not have a null namespace URI.", new string[]
			{
				input.Name
			});
			Block_25:
			compiler.ToParent();
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000DE5C0 File Offset: 0x000DC7C0
		protected void CompileTemplate(Compiler compiler)
		{
			do
			{
				this.CompileOnceTemplate(compiler);
			}
			while (compiler.Advance());
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x000DE5D4 File Offset: 0x000DC7D4
		protected void CompileOnceTemplate(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			if (input.NodeType != XPathNodeType.Element)
			{
				this.CompileLiteral(compiler);
				return;
			}
			string namespaceURI = input.NamespaceURI;
			if (Ref.Equal(namespaceURI, input.Atoms.UriXsl))
			{
				compiler.PushNamespaceScope();
				this.CompileInstruction(compiler);
				compiler.PopScope();
				return;
			}
			compiler.PushLiteralScope();
			compiler.InsertExtensionNamespace();
			if (compiler.IsExtensionNamespace(namespaceURI))
			{
				this.AddAction(compiler.CreateNewInstructionAction());
			}
			else
			{
				this.CompileLiteral(compiler);
			}
			compiler.PopScope();
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x000DE658 File Offset: 0x000DC858
		private void CompileInstruction(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			string localName = input.LocalName;
			CompiledAction action;
			if (Ref.Equal(localName, input.Atoms.ApplyImports))
			{
				action = compiler.CreateApplyImportsAction();
			}
			else if (Ref.Equal(localName, input.Atoms.ApplyTemplates))
			{
				action = compiler.CreateApplyTemplatesAction();
			}
			else if (Ref.Equal(localName, input.Atoms.Attribute))
			{
				action = compiler.CreateAttributeAction();
			}
			else if (Ref.Equal(localName, input.Atoms.CallTemplate))
			{
				action = compiler.CreateCallTemplateAction();
			}
			else if (Ref.Equal(localName, input.Atoms.Choose))
			{
				action = compiler.CreateChooseAction();
			}
			else if (Ref.Equal(localName, input.Atoms.Comment))
			{
				action = compiler.CreateCommentAction();
			}
			else if (Ref.Equal(localName, input.Atoms.Copy))
			{
				action = compiler.CreateCopyAction();
			}
			else if (Ref.Equal(localName, input.Atoms.CopyOf))
			{
				action = compiler.CreateCopyOfAction();
			}
			else if (Ref.Equal(localName, input.Atoms.Element))
			{
				action = compiler.CreateElementAction();
			}
			else
			{
				if (Ref.Equal(localName, input.Atoms.Fallback))
				{
					return;
				}
				if (Ref.Equal(localName, input.Atoms.ForEach))
				{
					action = compiler.CreateForEachAction();
				}
				else if (Ref.Equal(localName, input.Atoms.If))
				{
					action = compiler.CreateIfAction(IfAction.ConditionType.ConditionIf);
				}
				else if (Ref.Equal(localName, input.Atoms.Message))
				{
					action = compiler.CreateMessageAction();
				}
				else if (Ref.Equal(localName, input.Atoms.Number))
				{
					action = compiler.CreateNumberAction();
				}
				else if (Ref.Equal(localName, input.Atoms.ProcessingInstruction))
				{
					action = compiler.CreateProcessingInstructionAction();
				}
				else if (Ref.Equal(localName, input.Atoms.Text))
				{
					action = compiler.CreateTextAction();
				}
				else if (Ref.Equal(localName, input.Atoms.ValueOf))
				{
					action = compiler.CreateValueOfAction();
				}
				else if (Ref.Equal(localName, input.Atoms.Variable))
				{
					action = compiler.CreateVariableAction(VariableType.LocalVariable);
				}
				else
				{
					if (!compiler.ForwardCompatibility)
					{
						throw compiler.UnexpectedKeyword();
					}
					action = compiler.CreateNewInstructionAction();
				}
			}
			this.AddAction(action);
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x000DE8B0 File Offset: 0x000DCAB0
		private void CompileLiteral(Compiler compiler)
		{
			switch (compiler.Input.NodeType)
			{
			case XPathNodeType.Element:
				this.AddEvent(compiler.CreateBeginEvent());
				this.CompileLiteralAttributesAndNamespaces(compiler);
				if (compiler.Recurse())
				{
					this.CompileTemplate(compiler);
					compiler.ToParent();
				}
				this.AddEvent(new EndEvent(XPathNodeType.Element));
				return;
			case XPathNodeType.Attribute:
			case XPathNodeType.Namespace:
			case XPathNodeType.Whitespace:
			case XPathNodeType.ProcessingInstruction:
			case XPathNodeType.Comment:
				break;
			case XPathNodeType.Text:
			case XPathNodeType.SignificantWhitespace:
				this.AddEvent(compiler.CreateTextEvent());
				break;
			default:
				return;
			}
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x000DE934 File Offset: 0x000DCB34
		private void CompileLiteralAttributesAndNamespaces(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			if (input.Navigator.MoveToAttribute("use-attribute-sets", input.Atoms.UriXsl))
			{
				this.AddAction(compiler.CreateUseAttributeSetsAction());
				input.Navigator.MoveToParent();
			}
			compiler.InsertExcludedNamespace();
			if (input.MoveToFirstNamespace())
			{
				do
				{
					string value = input.Value;
					if (!(value == "http://www.w3.org/1999/XSL/Transform") && !compiler.IsExcludedNamespace(value) && !compiler.IsExtensionNamespace(value) && !compiler.IsNamespaceAlias(value))
					{
						this.AddEvent(new NamespaceEvent(input));
					}
				}
				while (input.MoveToNextNamespace());
				input.ToParent();
			}
			if (input.MoveToFirstAttribute())
			{
				do
				{
					if (!Ref.Equal(input.NamespaceURI, input.Atoms.UriXsl))
					{
						this.AddEvent(compiler.CreateBeginEvent());
						this.AddEvents(compiler.CompileAvt(input.Value));
						this.AddEvent(new EndEvent(XPathNodeType.Attribute));
					}
				}
				while (input.MoveToNextAttribute());
				input.ToParent();
			}
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x000DEA2E File Offset: 0x000DCC2E
		private void CompileOutput(Compiler compiler)
		{
			compiler.RootAction.Output.Compile(compiler);
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x000DEA41 File Offset: 0x000DCC41
		internal void AddAction(Action action)
		{
			if (this.containedActions == null)
			{
				this.containedActions = new ArrayList();
			}
			this.containedActions.Add(action);
			this.lastCopyCodeAction = null;
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000DEA6C File Offset: 0x000DCC6C
		private void EnsureCopyCodeAction()
		{
			if (this.lastCopyCodeAction == null)
			{
				CopyCodeAction action = new CopyCodeAction();
				this.AddAction(action);
				this.lastCopyCodeAction = action;
			}
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000DEA95 File Offset: 0x000DCC95
		protected void AddEvent(Event copyEvent)
		{
			this.EnsureCopyCodeAction();
			this.lastCopyCodeAction.AddEvent(copyEvent);
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x000DEAA9 File Offset: 0x000DCCA9
		protected void AddEvents(ArrayList copyEvents)
		{
			this.EnsureCopyCodeAction();
			this.lastCopyCodeAction.AddEvents(copyEvents);
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000DEAC0 File Offset: 0x000DCCC0
		private void AddScript(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			ScriptingLanguage lang = ScriptingLanguage.JScript;
			string text = null;
			if (input.MoveToFirstAttribute())
			{
				string value;
				for (;;)
				{
					if (input.LocalName == input.Atoms.Language)
					{
						value = input.Value;
						if (string.Compare(value, "jscript", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(value, "javascript", StringComparison.OrdinalIgnoreCase) == 0)
						{
							lang = ScriptingLanguage.JScript;
						}
						else if (string.Compare(value, "c#", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(value, "csharp", StringComparison.OrdinalIgnoreCase) == 0)
						{
							lang = ScriptingLanguage.CSharp;
						}
						else
						{
							if (string.Compare(value, "vb", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(value, "visualbasic", StringComparison.OrdinalIgnoreCase) != 0)
							{
								break;
							}
							lang = ScriptingLanguage.VisualBasic;
						}
					}
					else if (input.LocalName == input.Atoms.ImplementsPrefix)
					{
						if (!PrefixQName.ValidatePrefix(input.Value))
						{
							goto Block_6;
						}
						text = compiler.ResolveXmlNamespace(input.Value);
					}
					if (!input.MoveToNextAttribute())
					{
						goto Block_7;
					}
				}
				throw XsltException.Create("Scripting language '{0}' is not supported.", new string[]
				{
					value
				});
				Block_6:
				throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[]
				{
					input.LocalName,
					input.Value
				});
				Block_7:
				input.ToParent();
			}
			if (text == null)
			{
				throw XsltException.Create("Missing mandatory attribute '{0}'.", new string[]
				{
					input.Atoms.ImplementsPrefix
				});
			}
			if (!input.Recurse() || input.NodeType != XPathNodeType.Text)
			{
				throw XsltException.Create("The 'msxsl:script' element cannot be empty.", Array.Empty<string>());
			}
			compiler.AddScript(input.Value, lang, text, input.BaseURI, input.LineNumber);
			input.ToParent();
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000DEC4C File Offset: 0x000DCE4C
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 1)
				{
					return;
				}
				frame.Finished();
				return;
			}
			else
			{
				if (this.containedActions != null && this.containedActions.Count > 0)
				{
					processor.PushActionFrame(frame);
					frame.State = 1;
					return;
				}
				frame.Finished();
				return;
			}
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000DEC9A File Offset: 0x000DCE9A
		internal Action GetAction(int actionIndex)
		{
			if (this.containedActions != null && actionIndex < this.containedActions.Count)
			{
				return (Action)this.containedActions[actionIndex];
			}
			return null;
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000DECC8 File Offset: 0x000DCEC8
		internal void CheckDuplicateParams(XmlQualifiedName name)
		{
			if (this.containedActions != null)
			{
				foreach (object obj in this.containedActions)
				{
					WithParamAction withParamAction = ((CompiledAction)obj) as WithParamAction;
					if (withParamAction != null && withParamAction.Name == name)
					{
						throw XsltException.Create("Value of parameter '{0}' cannot be specified more than once within a single 'xsl:call-template' or 'xsl:apply-templates' element.", new string[]
						{
							name.ToString()
						});
					}
				}
			}
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x000DED54 File Offset: 0x000DCF54
		internal override void ReplaceNamespaceAlias(Compiler compiler)
		{
			if (this.containedActions == null)
			{
				return;
			}
			int count = this.containedActions.Count;
			for (int i = 0; i < this.containedActions.Count; i++)
			{
				((Action)this.containedActions[i]).ReplaceNamespaceAlias(compiler);
			}
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x000DB152 File Offset: 0x000D9352
		public ContainerAction()
		{
		}

		// Token: 0x04001CBF RID: 7359
		internal ArrayList containedActions;

		// Token: 0x04001CC0 RID: 7360
		internal CopyCodeAction lastCopyCodeAction;

		// Token: 0x04001CC1 RID: 7361
		private int maxid;

		// Token: 0x04001CC2 RID: 7362
		protected const int ProcessingChildren = 1;
	}
}
