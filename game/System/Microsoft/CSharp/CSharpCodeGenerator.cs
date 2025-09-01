using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Microsoft.CSharp
{
	// Token: 0x0200013A RID: 314
	internal sealed class CSharpCodeGenerator : ICodeCompiler, ICodeGenerator
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x0000219B File Offset: 0x0000039B
		internal CSharpCodeGenerator()
		{
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000185D0 File Offset: 0x000167D0
		internal CSharpCodeGenerator(IDictionary<string, string> providerOptions)
		{
			this._provOptions = providerOptions;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x000185DF File Offset: 0x000167DF
		private string FileExtension
		{
			get
			{
				return ".cs";
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x000185E6 File Offset: 0x000167E6
		private string CompilerName
		{
			get
			{
				return "csc.exe";
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x000185ED File Offset: 0x000167ED
		private string CurrentTypeName
		{
			get
			{
				if (this._currentClass == null)
				{
					return "<% unknown %>";
				}
				return this._currentClass.Name;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00018608 File Offset: 0x00016808
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x00018615 File Offset: 0x00016815
		private int Indent
		{
			get
			{
				return this._output.Indent;
			}
			set
			{
				this._output.Indent = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00018623 File Offset: 0x00016823
		private bool IsCurrentInterface
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsInterface;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00018647 File Offset: 0x00016847
		private bool IsCurrentClass
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsClass;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0001866B File Offset: 0x0001686B
		private bool IsCurrentStruct
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsStruct;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0001868F File Offset: 0x0001688F
		private bool IsCurrentEnum
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsEnum;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x000186B3 File Offset: 0x000168B3
		private bool IsCurrentDelegate
		{
			get
			{
				return this._currentClass != null && this._currentClass is CodeTypeDelegate;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x000186CD File Offset: 0x000168CD
		private string NullToken
		{
			get
			{
				return "null";
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x000186D4 File Offset: 0x000168D4
		private CodeGeneratorOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x000186DC File Offset: 0x000168DC
		private TextWriter Output
		{
			get
			{
				return this._output;
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x000186E4 File Offset: 0x000168E4
		private string QuoteSnippetStringCStyle(string value)
		{
			StringBuilder stringBuilder = new StringBuilder(value.Length + 5);
			Indentation indentation = new Indentation(this._output, this.Indent + 1);
			stringBuilder.Append('"');
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c <= '"')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							stringBuilder.Append("\\t");
							break;
						case '\n':
							stringBuilder.Append("\\n");
							break;
						case '\v':
						case '\f':
							goto IL_104;
						case '\r':
							stringBuilder.Append("\\r");
							break;
						default:
							if (c != '"')
							{
								goto IL_104;
							}
							stringBuilder.Append("\\\"");
							break;
						}
					}
					else
					{
						stringBuilder.Append("\\0");
					}
				}
				else if (c <= '\\')
				{
					if (c != '\'')
					{
						if (c != '\\')
						{
							goto IL_104;
						}
						stringBuilder.Append("\\\\");
					}
					else
					{
						stringBuilder.Append("\\'");
					}
				}
				else
				{
					if (c != '\u2028' && c != '\u2029')
					{
						goto IL_104;
					}
					this.AppendEscapedChar(stringBuilder, value[i]);
				}
				IL_112:
				if (i > 0 && i % 80 == 0)
				{
					if (char.IsHighSurrogate(value[i]) && i < value.Length - 1 && char.IsLowSurrogate(value[i + 1]))
					{
						stringBuilder.Append(value[++i]);
					}
					stringBuilder.Append("\" +");
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append(indentation.IndentationString);
					stringBuilder.Append('"');
				}
				i++;
				continue;
				IL_104:
				stringBuilder.Append(value[i]);
				goto IL_112;
			}
			stringBuilder.Append('"');
			return stringBuilder.ToString();
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00018898 File Offset: 0x00016A98
		private string QuoteSnippetStringVerbatimStyle(string value)
		{
			StringBuilder stringBuilder = new StringBuilder(value.Length + 5);
			stringBuilder.Append("@\"");
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == '"')
				{
					stringBuilder.Append("\"\"");
				}
				else
				{
					stringBuilder.Append(value[i]);
				}
			}
			stringBuilder.Append('"');
			return stringBuilder.ToString();
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00018906 File Offset: 0x00016B06
		private string QuoteSnippetString(string value)
		{
			if (value.Length < 256 || value.Length > 1500 || value.IndexOf('\0') != -1)
			{
				return this.QuoteSnippetStringCStyle(value);
			}
			return this.QuoteSnippetStringVerbatimStyle(value);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001893B File Offset: 0x00016B3B
		private void ContinueOnNewLine(string st)
		{
			this.Output.WriteLine(st);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00018949 File Offset: 0x00016B49
		private void OutputIdentifier(string ident)
		{
			this.Output.Write(this.CreateEscapedIdentifier(ident));
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001895D File Offset: 0x00016B5D
		private void OutputType(CodeTypeReference typeRef)
		{
			this.Output.Write(this.GetTypeOutput(typeRef));
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00018974 File Offset: 0x00016B74
		private void GenerateArrayCreateExpression(CodeArrayCreateExpression e)
		{
			this.Output.Write("new ");
			CodeExpressionCollection initializers = e.Initializers;
			if (initializers.Count > 0)
			{
				this.OutputType(e.CreateType);
				if (e.CreateType.ArrayRank == 0)
				{
					this.Output.Write("[]");
				}
				this.Output.WriteLine(" {");
				int indent = this.Indent;
				this.Indent = indent + 1;
				this.OutputExpressionList(initializers, true);
				indent = this.Indent;
				this.Indent = indent - 1;
				this.Output.Write('}');
				return;
			}
			this.Output.Write(this.GetBaseTypeOutput(e.CreateType, true));
			this.Output.Write('[');
			if (e.SizeExpression != null)
			{
				this.GenerateExpression(e.SizeExpression);
			}
			else
			{
				this.Output.Write(e.Size);
			}
			this.Output.Write(']');
			int nestedArrayDepth = e.CreateType.NestedArrayDepth;
			for (int i = 0; i < nestedArrayDepth - 1; i++)
			{
				this.Output.Write("[]");
			}
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00018A93 File Offset: 0x00016C93
		private void GenerateBaseReferenceExpression(CodeBaseReferenceExpression e)
		{
			this.Output.Write("base");
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00018AA8 File Offset: 0x00016CA8
		private void GenerateBinaryOperatorExpression(CodeBinaryOperatorExpression e)
		{
			bool flag = false;
			this.Output.Write('(');
			this.GenerateExpression(e.Left);
			this.Output.Write(' ');
			if (e.Left is CodeBinaryOperatorExpression || e.Right is CodeBinaryOperatorExpression)
			{
				if (!this._inNestedBinary)
				{
					flag = true;
					this._inNestedBinary = true;
					this.Indent += 3;
				}
				this.ContinueOnNewLine("");
			}
			this.OutputOperator(e.Operator);
			this.Output.Write(' ');
			this.GenerateExpression(e.Right);
			this.Output.Write(')');
			if (flag)
			{
				this.Indent -= 3;
				this._inNestedBinary = false;
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00018B6C File Offset: 0x00016D6C
		private void GenerateCastExpression(CodeCastExpression e)
		{
			this.Output.Write("((");
			this.OutputType(e.TargetType);
			this.Output.Write(")(");
			this.GenerateExpression(e.Expression);
			this.Output.Write("))");
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00018BC4 File Offset: 0x00016DC4
		public void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
		{
			if (this._output != null)
			{
				throw new InvalidOperationException("This code generation API cannot be called while the generator is being used to generate something else.");
			}
			this._options = (options ?? new CodeGeneratorOptions());
			this._output = new ExposedTabStringIndentedTextWriter(writer, this._options.IndentString);
			try
			{
				CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration();
				this._currentClass = codeTypeDeclaration;
				this.GenerateTypeMember(member, codeTypeDeclaration);
			}
			finally
			{
				this._currentClass = null;
				this._output = null;
				this._options = null;
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00018C48 File Offset: 0x00016E48
		private void GenerateDefaultValueExpression(CodeDefaultValueExpression e)
		{
			this.Output.Write("default(");
			this.OutputType(e.Type);
			this.Output.Write(')');
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00018C74 File Offset: 0x00016E74
		private void GenerateDelegateCreateExpression(CodeDelegateCreateExpression e)
		{
			this.Output.Write("new ");
			this.OutputType(e.DelegateType);
			this.Output.Write('(');
			this.GenerateExpression(e.TargetObject);
			this.Output.Write('.');
			this.OutputIdentifier(e.MethodName);
			this.Output.Write(')');
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00018CDC File Offset: 0x00016EDC
		private void GenerateEvents(CodeTypeDeclaration e)
		{
			foreach (object obj in e.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember is CodeMemberEvent)
				{
					this._currentMember = codeTypeMember;
					if (this._options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this._currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this._currentMember.Comments);
					CodeMemberEvent codeMemberEvent = (CodeMemberEvent)codeTypeMember;
					if (codeMemberEvent.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeMemberEvent.LinePragma);
					}
					this.GenerateEvent(codeMemberEvent, e);
					if (codeMemberEvent.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeMemberEvent.LinePragma);
					}
					if (this._currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00018DF0 File Offset: 0x00016FF0
		private void GenerateFields(CodeTypeDeclaration e)
		{
			foreach (object obj in e.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember is CodeMemberField)
				{
					this._currentMember = codeTypeMember;
					if (this._options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this._currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this._currentMember.Comments);
					CodeMemberField codeMemberField = (CodeMemberField)codeTypeMember;
					if (codeMemberField.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeMemberField.LinePragma);
					}
					this.GenerateField(codeMemberField);
					if (codeMemberField.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeMemberField.LinePragma);
					}
					if (this._currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00018F04 File Offset: 0x00017104
		private void GenerateFieldReferenceExpression(CodeFieldReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.GenerateExpression(e.TargetObject);
				this.Output.Write('.');
			}
			this.OutputIdentifier(e.FieldName);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00018F33 File Offset: 0x00017133
		private void GenerateArgumentReferenceExpression(CodeArgumentReferenceExpression e)
		{
			this.OutputIdentifier(e.ParameterName);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00018F41 File Offset: 0x00017141
		private void GenerateVariableReferenceExpression(CodeVariableReferenceExpression e)
		{
			this.OutputIdentifier(e.VariableName);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00018F50 File Offset: 0x00017150
		private void GenerateIndexerExpression(CodeIndexerExpression e)
		{
			this.GenerateExpression(e.TargetObject);
			this.Output.Write('[');
			bool flag = true;
			foreach (object obj in e.Indices)
			{
				CodeExpression e2 = (CodeExpression)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.Output.Write(", ");
				}
				this.GenerateExpression(e2);
			}
			this.Output.Write(']');
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00018FE8 File Offset: 0x000171E8
		private void GenerateArrayIndexerExpression(CodeArrayIndexerExpression e)
		{
			this.GenerateExpression(e.TargetObject);
			this.Output.Write('[');
			bool flag = true;
			foreach (object obj in e.Indices)
			{
				CodeExpression e2 = (CodeExpression)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.Output.Write(", ");
				}
				this.GenerateExpression(e2);
			}
			this.Output.Write(']');
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00019080 File Offset: 0x00017280
		private void GenerateSnippetCompileUnit(CodeSnippetCompileUnit e)
		{
			this.GenerateDirectives(e.StartDirectives);
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaStart(e.LinePragma);
			}
			this.Output.WriteLine(e.Value);
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(e.LinePragma);
			}
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x000190EC File Offset: 0x000172EC
		private void GenerateSnippetExpression(CodeSnippetExpression e)
		{
			this.Output.Write(e.Value);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x000190FF File Offset: 0x000172FF
		private void GenerateMethodInvokeExpression(CodeMethodInvokeExpression e)
		{
			this.GenerateMethodReferenceExpression(e.Method);
			this.Output.Write('(');
			this.OutputExpressionList(e.Parameters);
			this.Output.Write(')');
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00019134 File Offset: 0x00017334
		private void GenerateMethodReferenceExpression(CodeMethodReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				if (e.TargetObject is CodeBinaryOperatorExpression)
				{
					this.Output.Write('(');
					this.GenerateExpression(e.TargetObject);
					this.Output.Write(')');
				}
				else
				{
					this.GenerateExpression(e.TargetObject);
				}
				this.Output.Write('.');
			}
			this.OutputIdentifier(e.MethodName);
			if (e.TypeArguments.Count > 0)
			{
				this.Output.Write(this.GetTypeArgumentsOutput(e.TypeArguments));
			}
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x000191C8 File Offset: 0x000173C8
		private bool GetUserData(CodeObject e, string property, bool defaultValue)
		{
			object obj = e.UserData[property];
			if (obj != null && obj is bool)
			{
				return (bool)obj;
			}
			return defaultValue;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000191F8 File Offset: 0x000173F8
		private void GenerateNamespace(CodeNamespace e)
		{
			this.GenerateCommentStatements(e.Comments);
			this.GenerateNamespaceStart(e);
			if (this.GetUserData(e, "GenerateImports", true))
			{
				this.GenerateNamespaceImports(e);
			}
			this.Output.WriteLine();
			this.GenerateTypes(e);
			this.GenerateNamespaceEnd(e);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00019248 File Offset: 0x00017448
		private void GenerateStatement(CodeStatement e)
		{
			if (e.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(e.StartDirectives);
			}
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaStart(e.LinePragma);
			}
			if (e is CodeCommentStatement)
			{
				this.GenerateCommentStatement((CodeCommentStatement)e);
			}
			else if (e is CodeMethodReturnStatement)
			{
				this.GenerateMethodReturnStatement((CodeMethodReturnStatement)e);
			}
			else if (e is CodeConditionStatement)
			{
				this.GenerateConditionStatement((CodeConditionStatement)e);
			}
			else if (e is CodeTryCatchFinallyStatement)
			{
				this.GenerateTryCatchFinallyStatement((CodeTryCatchFinallyStatement)e);
			}
			else if (e is CodeAssignStatement)
			{
				this.GenerateAssignStatement((CodeAssignStatement)e);
			}
			else if (e is CodeExpressionStatement)
			{
				this.GenerateExpressionStatement((CodeExpressionStatement)e);
			}
			else if (e is CodeIterationStatement)
			{
				this.GenerateIterationStatement((CodeIterationStatement)e);
			}
			else if (e is CodeThrowExceptionStatement)
			{
				this.GenerateThrowExceptionStatement((CodeThrowExceptionStatement)e);
			}
			else if (e is CodeSnippetStatement)
			{
				int indent = this.Indent;
				this.Indent = 0;
				this.GenerateSnippetStatement((CodeSnippetStatement)e);
				this.Indent = indent;
			}
			else if (e is CodeVariableDeclarationStatement)
			{
				this.GenerateVariableDeclarationStatement((CodeVariableDeclarationStatement)e);
			}
			else if (e is CodeAttachEventStatement)
			{
				this.GenerateAttachEventStatement((CodeAttachEventStatement)e);
			}
			else if (e is CodeRemoveEventStatement)
			{
				this.GenerateRemoveEventStatement((CodeRemoveEventStatement)e);
			}
			else if (e is CodeGotoStatement)
			{
				this.GenerateGotoStatement((CodeGotoStatement)e);
			}
			else
			{
				if (!(e is CodeLabeledStatement))
				{
					throw new ArgumentException(SR.Format("Element type {0} is not supported.", e.GetType().FullName), "e");
				}
				this.GenerateLabeledStatement((CodeLabeledStatement)e);
			}
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(e.LinePragma);
			}
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00019438 File Offset: 0x00017638
		private void GenerateStatements(CodeStatementCollection stmts)
		{
			foreach (object obj in stmts)
			{
				CodeStatement e = (CodeStatement)obj;
				((ICodeGenerator)this).GenerateCodeFromStatement(e, this._output.InnerWriter, this._options);
			}
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000194A0 File Offset: 0x000176A0
		private void GenerateNamespaceImports(CodeNamespace e)
		{
			foreach (object obj in e.Imports)
			{
				CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj;
				if (codeNamespaceImport.LinePragma != null)
				{
					this.GenerateLinePragmaStart(codeNamespaceImport.LinePragma);
				}
				this.GenerateNamespaceImport(codeNamespaceImport);
				if (codeNamespaceImport.LinePragma != null)
				{
					this.GenerateLinePragmaEnd(codeNamespaceImport.LinePragma);
				}
			}
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00019524 File Offset: 0x00017724
		private void GenerateEventReferenceExpression(CodeEventReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.GenerateExpression(e.TargetObject);
				this.Output.Write('.');
			}
			this.OutputIdentifier(e.EventName);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00019553 File Offset: 0x00017753
		private void GenerateDelegateInvokeExpression(CodeDelegateInvokeExpression e)
		{
			if (e.TargetObject != null)
			{
				this.GenerateExpression(e.TargetObject);
			}
			this.Output.Write('(');
			this.OutputExpressionList(e.Parameters);
			this.Output.Write(')');
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00019590 File Offset: 0x00017790
		private void GenerateObjectCreateExpression(CodeObjectCreateExpression e)
		{
			this.Output.Write("new ");
			this.OutputType(e.CreateType);
			this.Output.Write('(');
			this.OutputExpressionList(e.Parameters);
			this.Output.Write(')');
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000195E0 File Offset: 0x000177E0
		private void GeneratePrimitiveExpression(CodePrimitiveExpression e)
		{
			if (e.Value is char)
			{
				this.GeneratePrimitiveChar((char)e.Value);
				return;
			}
			if (e.Value is sbyte)
			{
				this.Output.Write(((sbyte)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is ushort)
			{
				this.Output.Write(((ushort)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is uint)
			{
				this.Output.Write(((uint)e.Value).ToString(CultureInfo.InvariantCulture));
				this.Output.Write('u');
				return;
			}
			if (e.Value is ulong)
			{
				this.Output.Write(((ulong)e.Value).ToString(CultureInfo.InvariantCulture));
				this.Output.Write("ul");
				return;
			}
			this.GeneratePrimitiveExpressionBase(e);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000196F4 File Offset: 0x000178F4
		private void GeneratePrimitiveExpressionBase(CodePrimitiveExpression e)
		{
			if (e.Value == null)
			{
				this.Output.Write(this.NullToken);
				return;
			}
			if (e.Value is string)
			{
				this.Output.Write(this.QuoteSnippetString((string)e.Value));
				return;
			}
			if (e.Value is char)
			{
				this.Output.Write("'" + e.Value.ToString() + "'");
				return;
			}
			if (e.Value is byte)
			{
				this.Output.Write(((byte)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is short)
			{
				this.Output.Write(((short)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is int)
			{
				this.Output.Write(((int)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is long)
			{
				this.Output.Write(((long)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is float)
			{
				this.GenerateSingleFloatValue((float)e.Value);
				return;
			}
			if (e.Value is double)
			{
				this.GenerateDoubleValue((double)e.Value);
				return;
			}
			if (e.Value is decimal)
			{
				this.GenerateDecimalValue((decimal)e.Value);
				return;
			}
			if (!(e.Value is bool))
			{
				throw new ArgumentException(SR.Format("Invalid Primitive Type: {0}. Consider using CodeObjectCreateExpression.", e.Value.GetType().ToString()));
			}
			if ((bool)e.Value)
			{
				this.Output.Write("true");
				return;
			}
			this.Output.Write("false");
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x000198F4 File Offset: 0x00017AF4
		private void GeneratePrimitiveChar(char c)
		{
			this.Output.Write('\'');
			if (c > '\'')
			{
				if (c <= '\u0084')
				{
					if (c == '\\')
					{
						this.Output.Write("\\\\");
						goto IL_143;
					}
					if (c != '\u0084')
					{
						goto IL_125;
					}
				}
				else if (c != '\u0085' && c != '\u2028' && c != '\u2029')
				{
					goto IL_125;
				}
				this.AppendEscapedChar(null, c);
				goto IL_143;
			}
			if (c <= '\r')
			{
				if (c == '\0')
				{
					this.Output.Write("\\0");
					goto IL_143;
				}
				switch (c)
				{
				case '\t':
					this.Output.Write("\\t");
					goto IL_143;
				case '\n':
					this.Output.Write("\\n");
					goto IL_143;
				case '\r':
					this.Output.Write("\\r");
					goto IL_143;
				}
			}
			else
			{
				if (c == '"')
				{
					this.Output.Write("\\\"");
					goto IL_143;
				}
				if (c == '\'')
				{
					this.Output.Write("\\'");
					goto IL_143;
				}
			}
			IL_125:
			if (char.IsSurrogate(c))
			{
				this.AppendEscapedChar(null, c);
			}
			else
			{
				this.Output.Write(c);
			}
			IL_143:
			this.Output.Write('\'');
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00019A54 File Offset: 0x00017C54
		private void AppendEscapedChar(StringBuilder b, char value)
		{
			int num;
			if (b == null)
			{
				this.Output.Write("\\u");
				TextWriter output = this.Output;
				num = (int)value;
				output.Write(num.ToString("X4", CultureInfo.InvariantCulture));
				return;
			}
			b.Append("\\u");
			num = (int)value;
			b.Append(num.ToString("X4", CultureInfo.InvariantCulture));
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00019AB9 File Offset: 0x00017CB9
		private void GeneratePropertySetValueReferenceExpression(CodePropertySetValueReferenceExpression e)
		{
			this.Output.Write("value");
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00019ACB File Offset: 0x00017CCB
		private void GenerateThisReferenceExpression(CodeThisReferenceExpression e)
		{
			this.Output.Write("this");
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00019ADD File Offset: 0x00017CDD
		private void GenerateExpressionStatement(CodeExpressionStatement e)
		{
			this.GenerateExpression(e.Expression);
			if (!this._generatingForLoop)
			{
				this.Output.WriteLine(';');
			}
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00019B00 File Offset: 0x00017D00
		private void GenerateIterationStatement(CodeIterationStatement e)
		{
			this._generatingForLoop = true;
			this.Output.Write("for (");
			this.GenerateStatement(e.InitStatement);
			this.Output.Write("; ");
			this.GenerateExpression(e.TestExpression);
			this.Output.Write("; ");
			this.GenerateStatement(e.IncrementStatement);
			this.Output.Write(')');
			this.OutputStartingBrace();
			this._generatingForLoop = false;
			int indent = this.Indent;
			this.Indent = indent + 1;
			this.GenerateStatements(e.Statements);
			indent = this.Indent;
			this.Indent = indent - 1;
			this.Output.WriteLine('}');
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00019BBB File Offset: 0x00017DBB
		private void GenerateThrowExceptionStatement(CodeThrowExceptionStatement e)
		{
			this.Output.Write("throw");
			if (e.ToThrow != null)
			{
				this.Output.Write(' ');
				this.GenerateExpression(e.ToThrow);
			}
			this.Output.WriteLine(';');
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00019BFC File Offset: 0x00017DFC
		private void GenerateComment(CodeComment e)
		{
			string value = e.DocComment ? "///" : "//";
			this.Output.Write(value);
			this.Output.Write(' ');
			string text = e.Text;
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] != '\0')
				{
					this.Output.Write(text[i]);
					if (text[i] == '\r')
					{
						if (i < text.Length - 1 && text[i + 1] == '\n')
						{
							this.Output.Write('\n');
							i++;
						}
						this._output.InternalOutputTabs();
						this.Output.Write(value);
					}
					else if (text[i] == '\n')
					{
						this._output.InternalOutputTabs();
						this.Output.Write(value);
					}
					else if (text[i] == '\u2028' || text[i] == '\u2029' || text[i] == '\u0085')
					{
						this.Output.Write(value);
					}
				}
			}
			this.Output.WriteLine();
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00019D25 File Offset: 0x00017F25
		private void GenerateCommentStatement(CodeCommentStatement e)
		{
			if (e.Comment == null)
			{
				throw new ArgumentException(SR.Format("The 'Comment' property of the CodeCommentStatement '{0}' cannot be null.", "e"), "e");
			}
			this.GenerateComment(e.Comment);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00019D58 File Offset: 0x00017F58
		private void GenerateCommentStatements(CodeCommentStatementCollection e)
		{
			foreach (object obj in e)
			{
				CodeCommentStatement e2 = (CodeCommentStatement)obj;
				this.GenerateCommentStatement(e2);
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00019DAC File Offset: 0x00017FAC
		private void GenerateMethodReturnStatement(CodeMethodReturnStatement e)
		{
			this.Output.Write("return");
			if (e.Expression != null)
			{
				this.Output.Write(' ');
				this.GenerateExpression(e.Expression);
			}
			this.Output.WriteLine(';');
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00019DEC File Offset: 0x00017FEC
		private void GenerateConditionStatement(CodeConditionStatement e)
		{
			this.Output.Write("if (");
			this.GenerateExpression(e.Condition);
			this.Output.Write(')');
			this.OutputStartingBrace();
			int indent = this.Indent;
			this.Indent = indent + 1;
			this.GenerateStatements(e.TrueStatements);
			indent = this.Indent;
			this.Indent = indent - 1;
			if (e.FalseStatements.Count > 0)
			{
				this.Output.Write('}');
				if (this.Options.ElseOnClosing)
				{
					this.Output.Write(' ');
				}
				else
				{
					this.Output.WriteLine();
				}
				this.Output.Write("else");
				this.OutputStartingBrace();
				indent = this.Indent;
				this.Indent = indent + 1;
				this.GenerateStatements(e.FalseStatements);
				indent = this.Indent;
				this.Indent = indent - 1;
			}
			this.Output.WriteLine('}');
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00019EE8 File Offset: 0x000180E8
		private void GenerateTryCatchFinallyStatement(CodeTryCatchFinallyStatement e)
		{
			this.Output.Write("try");
			this.OutputStartingBrace();
			int indent = this.Indent;
			this.Indent = indent + 1;
			this.GenerateStatements(e.TryStatements);
			indent = this.Indent;
			this.Indent = indent - 1;
			CodeCatchClauseCollection catchClauses = e.CatchClauses;
			if (catchClauses.Count > 0)
			{
				foreach (object obj in catchClauses)
				{
					CodeCatchClause codeCatchClause = (CodeCatchClause)obj;
					this.Output.Write('}');
					if (this.Options.ElseOnClosing)
					{
						this.Output.Write(' ');
					}
					else
					{
						this.Output.WriteLine();
					}
					this.Output.Write("catch (");
					this.OutputType(codeCatchClause.CatchExceptionType);
					this.Output.Write(' ');
					this.OutputIdentifier(codeCatchClause.LocalName);
					this.Output.Write(')');
					this.OutputStartingBrace();
					indent = this.Indent;
					this.Indent = indent + 1;
					this.GenerateStatements(codeCatchClause.Statements);
					indent = this.Indent;
					this.Indent = indent - 1;
				}
			}
			CodeStatementCollection finallyStatements = e.FinallyStatements;
			if (finallyStatements.Count > 0)
			{
				this.Output.Write('}');
				if (this.Options.ElseOnClosing)
				{
					this.Output.Write(' ');
				}
				else
				{
					this.Output.WriteLine();
				}
				this.Output.Write("finally");
				this.OutputStartingBrace();
				indent = this.Indent;
				this.Indent = indent + 1;
				this.GenerateStatements(finallyStatements);
				indent = this.Indent;
				this.Indent = indent - 1;
			}
			this.Output.WriteLine('}');
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001A0D0 File Offset: 0x000182D0
		private void GenerateAssignStatement(CodeAssignStatement e)
		{
			this.GenerateExpression(e.Left);
			this.Output.Write(" = ");
			this.GenerateExpression(e.Right);
			if (!this._generatingForLoop)
			{
				this.Output.WriteLine(';');
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001A10F File Offset: 0x0001830F
		private void GenerateAttachEventStatement(CodeAttachEventStatement e)
		{
			this.GenerateEventReferenceExpression(e.Event);
			this.Output.Write(" += ");
			this.GenerateExpression(e.Listener);
			this.Output.WriteLine(';');
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001A146 File Offset: 0x00018346
		private void GenerateRemoveEventStatement(CodeRemoveEventStatement e)
		{
			this.GenerateEventReferenceExpression(e.Event);
			this.Output.Write(" -= ");
			this.GenerateExpression(e.Listener);
			this.Output.WriteLine(';');
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001A17D File Offset: 0x0001837D
		private void GenerateSnippetStatement(CodeSnippetStatement e)
		{
			this.Output.WriteLine(e.Value);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001A190 File Offset: 0x00018390
		private void GenerateGotoStatement(CodeGotoStatement e)
		{
			this.Output.Write("goto ");
			this.Output.Write(e.Label);
			this.Output.WriteLine(';');
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001A1C0 File Offset: 0x000183C0
		private void GenerateLabeledStatement(CodeLabeledStatement e)
		{
			int indent = this.Indent;
			this.Indent = indent - 1;
			this.Output.Write(e.Label);
			this.Output.WriteLine(':');
			indent = this.Indent;
			this.Indent = indent + 1;
			if (e.Statement != null)
			{
				this.GenerateStatement(e.Statement);
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001A220 File Offset: 0x00018420
		private void GenerateVariableDeclarationStatement(CodeVariableDeclarationStatement e)
		{
			this.OutputTypeNamePair(e.Type, e.Name);
			if (e.InitExpression != null)
			{
				this.Output.Write(" = ");
				this.GenerateExpression(e.InitExpression);
			}
			if (!this._generatingForLoop)
			{
				this.Output.WriteLine(';');
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001A278 File Offset: 0x00018478
		private void GenerateLinePragmaStart(CodeLinePragma e)
		{
			this.Output.WriteLine();
			this.Output.Write("#line ");
			this.Output.Write(e.LineNumber);
			this.Output.Write(" \"");
			this.Output.Write(e.FileName);
			this.Output.Write('"');
			this.Output.WriteLine();
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001A2EA File Offset: 0x000184EA
		private void GenerateLinePragmaEnd(CodeLinePragma e)
		{
			this.Output.WriteLine();
			this.Output.WriteLine("#line default");
			this.Output.WriteLine("#line hidden");
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001A318 File Offset: 0x00018518
		private void GenerateEvent(CodeMemberEvent e, CodeTypeDeclaration c)
		{
			if (this.IsCurrentDelegate || this.IsCurrentEnum)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			if (e.PrivateImplementationType == null)
			{
				this.OutputMemberAccessModifier(e.Attributes);
			}
			this.Output.Write("event ");
			string text = e.Name;
			if (e.PrivateImplementationType != null)
			{
				text = this.GetBaseTypeOutput(e.PrivateImplementationType, false) + "." + text;
			}
			this.OutputTypeNamePair(e.Type, text);
			this.Output.WriteLine(';');
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0001A3B8 File Offset: 0x000185B8
		private void GenerateExpression(CodeExpression e)
		{
			if (e is CodeArrayCreateExpression)
			{
				this.GenerateArrayCreateExpression((CodeArrayCreateExpression)e);
				return;
			}
			if (e is CodeBaseReferenceExpression)
			{
				this.GenerateBaseReferenceExpression((CodeBaseReferenceExpression)e);
				return;
			}
			if (e is CodeBinaryOperatorExpression)
			{
				this.GenerateBinaryOperatorExpression((CodeBinaryOperatorExpression)e);
				return;
			}
			if (e is CodeCastExpression)
			{
				this.GenerateCastExpression((CodeCastExpression)e);
				return;
			}
			if (e is CodeDelegateCreateExpression)
			{
				this.GenerateDelegateCreateExpression((CodeDelegateCreateExpression)e);
				return;
			}
			if (e is CodeFieldReferenceExpression)
			{
				this.GenerateFieldReferenceExpression((CodeFieldReferenceExpression)e);
				return;
			}
			if (e is CodeArgumentReferenceExpression)
			{
				this.GenerateArgumentReferenceExpression((CodeArgumentReferenceExpression)e);
				return;
			}
			if (e is CodeVariableReferenceExpression)
			{
				this.GenerateVariableReferenceExpression((CodeVariableReferenceExpression)e);
				return;
			}
			if (e is CodeIndexerExpression)
			{
				this.GenerateIndexerExpression((CodeIndexerExpression)e);
				return;
			}
			if (e is CodeArrayIndexerExpression)
			{
				this.GenerateArrayIndexerExpression((CodeArrayIndexerExpression)e);
				return;
			}
			if (e is CodeSnippetExpression)
			{
				this.GenerateSnippetExpression((CodeSnippetExpression)e);
				return;
			}
			if (e is CodeMethodInvokeExpression)
			{
				this.GenerateMethodInvokeExpression((CodeMethodInvokeExpression)e);
				return;
			}
			if (e is CodeMethodReferenceExpression)
			{
				this.GenerateMethodReferenceExpression((CodeMethodReferenceExpression)e);
				return;
			}
			if (e is CodeEventReferenceExpression)
			{
				this.GenerateEventReferenceExpression((CodeEventReferenceExpression)e);
				return;
			}
			if (e is CodeDelegateInvokeExpression)
			{
				this.GenerateDelegateInvokeExpression((CodeDelegateInvokeExpression)e);
				return;
			}
			if (e is CodeObjectCreateExpression)
			{
				this.GenerateObjectCreateExpression((CodeObjectCreateExpression)e);
				return;
			}
			if (e is CodeParameterDeclarationExpression)
			{
				this.GenerateParameterDeclarationExpression((CodeParameterDeclarationExpression)e);
				return;
			}
			if (e is CodeDirectionExpression)
			{
				this.GenerateDirectionExpression((CodeDirectionExpression)e);
				return;
			}
			if (e is CodePrimitiveExpression)
			{
				this.GeneratePrimitiveExpression((CodePrimitiveExpression)e);
				return;
			}
			if (e is CodePropertyReferenceExpression)
			{
				this.GeneratePropertyReferenceExpression((CodePropertyReferenceExpression)e);
				return;
			}
			if (e is CodePropertySetValueReferenceExpression)
			{
				this.GeneratePropertySetValueReferenceExpression((CodePropertySetValueReferenceExpression)e);
				return;
			}
			if (e is CodeThisReferenceExpression)
			{
				this.GenerateThisReferenceExpression((CodeThisReferenceExpression)e);
				return;
			}
			if (e is CodeTypeReferenceExpression)
			{
				this.GenerateTypeReferenceExpression((CodeTypeReferenceExpression)e);
				return;
			}
			if (e is CodeTypeOfExpression)
			{
				this.GenerateTypeOfExpression((CodeTypeOfExpression)e);
				return;
			}
			if (e is CodeDefaultValueExpression)
			{
				this.GenerateDefaultValueExpression((CodeDefaultValueExpression)e);
				return;
			}
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			throw new ArgumentException(SR.Format("Element type {0} is not supported.", e.GetType().FullName), "e");
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001A600 File Offset: 0x00018800
		private void GenerateField(CodeMemberField e)
		{
			if (this.IsCurrentDelegate || this.IsCurrentInterface)
			{
				return;
			}
			if (this.IsCurrentEnum)
			{
				if (e.CustomAttributes.Count > 0)
				{
					this.GenerateAttributes(e.CustomAttributes);
				}
				this.OutputIdentifier(e.Name);
				if (e.InitExpression != null)
				{
					this.Output.Write(" = ");
					this.GenerateExpression(e.InitExpression);
				}
				this.Output.WriteLine(',');
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			this.OutputMemberAccessModifier(e.Attributes);
			this.OutputVTableModifier(e.Attributes);
			this.OutputFieldScopeModifier(e.Attributes);
			this.OutputTypeNamePair(e.Type, e.Name);
			if (e.InitExpression != null)
			{
				this.Output.Write(" = ");
				this.GenerateExpression(e.InitExpression);
			}
			this.Output.WriteLine(';');
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001A6FF File Offset: 0x000188FF
		private void GenerateSnippetMember(CodeSnippetTypeMember e)
		{
			this.Output.Write(e.Text);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001A712 File Offset: 0x00018912
		private void GenerateParameterDeclarationExpression(CodeParameterDeclarationExpression e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes, null, true);
			}
			this.OutputDirection(e.Direction);
			this.OutputTypeNamePair(e.Type, e.Name);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001A750 File Offset: 0x00018950
		private void GenerateEntryPointMethod(CodeEntryPointMethod e, CodeTypeDeclaration c)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			this.Output.Write("public static ");
			this.OutputType(e.ReturnType);
			this.Output.Write(" Main()");
			this.OutputStartingBrace();
			int indent = this.Indent;
			this.Indent = indent + 1;
			this.GenerateStatements(e.Statements);
			indent = this.Indent;
			this.Indent = indent - 1;
			this.Output.WriteLine('}');
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001A7E4 File Offset: 0x000189E4
		private void GenerateMethods(CodeTypeDeclaration e)
		{
			foreach (object obj in e.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember is CodeMemberMethod && !(codeTypeMember is CodeTypeConstructor) && !(codeTypeMember is CodeConstructor))
				{
					this._currentMember = codeTypeMember;
					if (this._options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this._currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this._currentMember.Comments);
					CodeMemberMethod codeMemberMethod = (CodeMemberMethod)codeTypeMember;
					if (codeMemberMethod.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeMemberMethod.LinePragma);
					}
					if (codeTypeMember is CodeEntryPointMethod)
					{
						this.GenerateEntryPointMethod((CodeEntryPointMethod)codeTypeMember, e);
					}
					else
					{
						this.GenerateMethod(codeMemberMethod, e);
					}
					if (codeMemberMethod.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeMemberMethod.LinePragma);
					}
					if (this._currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001A930 File Offset: 0x00018B30
		private void GenerateMethod(CodeMemberMethod e, CodeTypeDeclaration c)
		{
			if (!this.IsCurrentClass && !this.IsCurrentStruct && !this.IsCurrentInterface)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			if (e.ReturnTypeCustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.ReturnTypeCustomAttributes, "return: ");
			}
			if (!this.IsCurrentInterface)
			{
				if (e.PrivateImplementationType == null)
				{
					this.OutputMemberAccessModifier(e.Attributes);
					this.OutputVTableModifier(e.Attributes);
					this.OutputMemberScopeModifier(e.Attributes);
				}
			}
			else
			{
				this.OutputVTableModifier(e.Attributes);
			}
			this.OutputType(e.ReturnType);
			this.Output.Write(' ');
			if (e.PrivateImplementationType != null)
			{
				this.Output.Write(this.GetBaseTypeOutput(e.PrivateImplementationType, false));
				this.Output.Write('.');
			}
			this.OutputIdentifier(e.Name);
			this.OutputTypeParameters(e.TypeParameters);
			this.Output.Write('(');
			this.OutputParameters(e.Parameters);
			this.Output.Write(')');
			this.OutputTypeParameterConstraints(e.TypeParameters);
			if (!this.IsCurrentInterface && (e.Attributes & MemberAttributes.ScopeMask) != MemberAttributes.Abstract)
			{
				this.OutputStartingBrace();
				int indent = this.Indent;
				this.Indent = indent + 1;
				this.GenerateStatements(e.Statements);
				indent = this.Indent;
				this.Indent = indent - 1;
				this.Output.WriteLine('}');
				return;
			}
			this.Output.WriteLine(';');
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001AAC4 File Offset: 0x00018CC4
		private void GenerateProperties(CodeTypeDeclaration e)
		{
			foreach (object obj in e.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember is CodeMemberProperty)
				{
					this._currentMember = codeTypeMember;
					if (this._options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this._currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this._currentMember.Comments);
					CodeMemberProperty codeMemberProperty = (CodeMemberProperty)codeTypeMember;
					if (codeMemberProperty.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeMemberProperty.LinePragma);
					}
					this.GenerateProperty(codeMemberProperty, e);
					if (codeMemberProperty.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeMemberProperty.LinePragma);
					}
					if (this._currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001ABD8 File Offset: 0x00018DD8
		private void GenerateProperty(CodeMemberProperty e, CodeTypeDeclaration c)
		{
			if (!this.IsCurrentClass && !this.IsCurrentStruct && !this.IsCurrentInterface)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			if (!this.IsCurrentInterface)
			{
				if (e.PrivateImplementationType == null)
				{
					this.OutputMemberAccessModifier(e.Attributes);
					this.OutputVTableModifier(e.Attributes);
					this.OutputMemberScopeModifier(e.Attributes);
				}
			}
			else
			{
				this.OutputVTableModifier(e.Attributes);
			}
			this.OutputType(e.Type);
			this.Output.Write(' ');
			if (e.PrivateImplementationType != null && !this.IsCurrentInterface)
			{
				this.Output.Write(this.GetBaseTypeOutput(e.PrivateImplementationType, false));
				this.Output.Write('.');
			}
			if (e.Parameters.Count > 0 && string.Equals(e.Name, "Item", StringComparison.OrdinalIgnoreCase))
			{
				this.Output.Write("this[");
				this.OutputParameters(e.Parameters);
				this.Output.Write(']');
			}
			else
			{
				this.OutputIdentifier(e.Name);
			}
			this.OutputStartingBrace();
			int indent = this.Indent;
			this.Indent = indent + 1;
			if (e.HasGet)
			{
				if (this.IsCurrentInterface || (e.Attributes & MemberAttributes.ScopeMask) == MemberAttributes.Abstract)
				{
					this.Output.WriteLine("get;");
				}
				else
				{
					this.Output.Write("get");
					this.OutputStartingBrace();
					indent = this.Indent;
					this.Indent = indent + 1;
					this.GenerateStatements(e.GetStatements);
					indent = this.Indent;
					this.Indent = indent - 1;
					this.Output.WriteLine('}');
				}
			}
			if (e.HasSet)
			{
				if (this.IsCurrentInterface || (e.Attributes & MemberAttributes.ScopeMask) == MemberAttributes.Abstract)
				{
					this.Output.WriteLine("set;");
				}
				else
				{
					this.Output.Write("set");
					this.OutputStartingBrace();
					indent = this.Indent;
					this.Indent = indent + 1;
					this.GenerateStatements(e.SetStatements);
					indent = this.Indent;
					this.Indent = indent - 1;
					this.Output.WriteLine('}');
				}
			}
			indent = this.Indent;
			this.Indent = indent - 1;
			this.Output.WriteLine('}');
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001AE30 File Offset: 0x00019030
		private void GenerateSingleFloatValue(float s)
		{
			if (float.IsNaN(s))
			{
				this.Output.Write("float.NaN");
				return;
			}
			if (float.IsNegativeInfinity(s))
			{
				this.Output.Write("float.NegativeInfinity");
				return;
			}
			if (float.IsPositiveInfinity(s))
			{
				this.Output.Write("float.PositiveInfinity");
				return;
			}
			this.Output.Write(s.ToString(CultureInfo.InvariantCulture));
			this.Output.Write('F');
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001AEAC File Offset: 0x000190AC
		private void GenerateDoubleValue(double d)
		{
			if (double.IsNaN(d))
			{
				this.Output.Write("double.NaN");
				return;
			}
			if (double.IsNegativeInfinity(d))
			{
				this.Output.Write("double.NegativeInfinity");
				return;
			}
			if (double.IsPositiveInfinity(d))
			{
				this.Output.Write("double.PositiveInfinity");
				return;
			}
			this.Output.Write(d.ToString("R", CultureInfo.InvariantCulture));
			this.Output.Write('D');
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001AF2D File Offset: 0x0001912D
		private void GenerateDecimalValue(decimal d)
		{
			this.Output.Write(d.ToString(CultureInfo.InvariantCulture));
			this.Output.Write('m');
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001AF53 File Offset: 0x00019153
		private void OutputVTableModifier(MemberAttributes attributes)
		{
			if ((attributes & MemberAttributes.VTableMask) == MemberAttributes.New)
			{
				this.Output.Write("new ");
			}
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001AF70 File Offset: 0x00019170
		private void OutputMemberAccessModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.AccessMask;
			if (memberAttributes <= MemberAttributes.Family)
			{
				if (memberAttributes == MemberAttributes.Assembly)
				{
					this.Output.Write("internal ");
					return;
				}
				if (memberAttributes == MemberAttributes.FamilyAndAssembly)
				{
					this.Output.Write("internal ");
					return;
				}
				if (memberAttributes != MemberAttributes.Family)
				{
					return;
				}
				this.Output.Write("protected ");
				return;
			}
			else
			{
				if (memberAttributes == MemberAttributes.FamilyOrAssembly)
				{
					this.Output.Write("protected internal ");
					return;
				}
				if (memberAttributes == MemberAttributes.Private)
				{
					this.Output.Write("private ");
					return;
				}
				if (memberAttributes != MemberAttributes.Public)
				{
					return;
				}
				this.Output.Write("public ");
				return;
			}
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001B024 File Offset: 0x00019224
		private void OutputMemberScopeModifier(MemberAttributes attributes)
		{
			switch (attributes & MemberAttributes.ScopeMask)
			{
			case MemberAttributes.Abstract:
				this.Output.Write("abstract ");
				return;
			case MemberAttributes.Final:
				this.Output.Write("");
				return;
			case MemberAttributes.Static:
				this.Output.Write("static ");
				return;
			case MemberAttributes.Override:
				this.Output.Write("override ");
				return;
			default:
			{
				MemberAttributes memberAttributes = attributes & MemberAttributes.AccessMask;
				if (memberAttributes == MemberAttributes.Assembly || memberAttributes == MemberAttributes.Family || memberAttributes == MemberAttributes.Public)
				{
					this.Output.Write("virtual ");
				}
				return;
			}
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001B0C4 File Offset: 0x000192C4
		private void OutputOperator(CodeBinaryOperatorType op)
		{
			switch (op)
			{
			case CodeBinaryOperatorType.Add:
				this.Output.Write('+');
				return;
			case CodeBinaryOperatorType.Subtract:
				this.Output.Write('-');
				return;
			case CodeBinaryOperatorType.Multiply:
				this.Output.Write('*');
				return;
			case CodeBinaryOperatorType.Divide:
				this.Output.Write('/');
				return;
			case CodeBinaryOperatorType.Modulus:
				this.Output.Write('%');
				return;
			case CodeBinaryOperatorType.Assign:
				this.Output.Write('=');
				return;
			case CodeBinaryOperatorType.IdentityInequality:
				this.Output.Write("!=");
				return;
			case CodeBinaryOperatorType.IdentityEquality:
				this.Output.Write("==");
				return;
			case CodeBinaryOperatorType.ValueEquality:
				this.Output.Write("==");
				return;
			case CodeBinaryOperatorType.BitwiseOr:
				this.Output.Write('|');
				return;
			case CodeBinaryOperatorType.BitwiseAnd:
				this.Output.Write('&');
				return;
			case CodeBinaryOperatorType.BooleanOr:
				this.Output.Write("||");
				return;
			case CodeBinaryOperatorType.BooleanAnd:
				this.Output.Write("&&");
				return;
			case CodeBinaryOperatorType.LessThan:
				this.Output.Write('<');
				return;
			case CodeBinaryOperatorType.LessThanOrEqual:
				this.Output.Write("<=");
				return;
			case CodeBinaryOperatorType.GreaterThan:
				this.Output.Write('>');
				return;
			case CodeBinaryOperatorType.GreaterThanOrEqual:
				this.Output.Write(">=");
				return;
			default:
				return;
			}
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001B220 File Offset: 0x00019420
		private void OutputFieldScopeModifier(MemberAttributes attributes)
		{
			switch (attributes & MemberAttributes.ScopeMask)
			{
			case MemberAttributes.Final:
			case MemberAttributes.Override:
				break;
			case MemberAttributes.Static:
				this.Output.Write("static ");
				return;
			case MemberAttributes.Const:
				this.Output.Write("const ");
				break;
			default:
				return;
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001B26C File Offset: 0x0001946C
		private void GeneratePropertyReferenceExpression(CodePropertyReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.GenerateExpression(e.TargetObject);
				this.Output.Write('.');
			}
			this.OutputIdentifier(e.PropertyName);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001B29C File Offset: 0x0001949C
		private void GenerateConstructors(CodeTypeDeclaration e)
		{
			foreach (object obj in e.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember is CodeConstructor)
				{
					this._currentMember = codeTypeMember;
					if (this._options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this._currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this._currentMember.Comments);
					CodeConstructor codeConstructor = (CodeConstructor)codeTypeMember;
					if (codeConstructor.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeConstructor.LinePragma);
					}
					this.GenerateConstructor(codeConstructor, e);
					if (codeConstructor.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeConstructor.LinePragma);
					}
					if (this._currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001B3B0 File Offset: 0x000195B0
		private void GenerateConstructor(CodeConstructor e, CodeTypeDeclaration c)
		{
			if (!this.IsCurrentClass && !this.IsCurrentStruct)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			this.OutputMemberAccessModifier(e.Attributes);
			this.OutputIdentifier(this.CurrentTypeName);
			this.Output.Write('(');
			this.OutputParameters(e.Parameters);
			this.Output.Write(')');
			CodeExpressionCollection baseConstructorArgs = e.BaseConstructorArgs;
			CodeExpressionCollection chainedConstructorArgs = e.ChainedConstructorArgs;
			int indent;
			if (baseConstructorArgs.Count > 0)
			{
				this.Output.WriteLine(" : ");
				indent = this.Indent;
				this.Indent = indent + 1;
				indent = this.Indent;
				this.Indent = indent + 1;
				this.Output.Write("base(");
				this.OutputExpressionList(baseConstructorArgs);
				this.Output.Write(')');
				indent = this.Indent;
				this.Indent = indent - 1;
				indent = this.Indent;
				this.Indent = indent - 1;
			}
			if (chainedConstructorArgs.Count > 0)
			{
				this.Output.WriteLine(" : ");
				indent = this.Indent;
				this.Indent = indent + 1;
				indent = this.Indent;
				this.Indent = indent + 1;
				this.Output.Write("this(");
				this.OutputExpressionList(chainedConstructorArgs);
				this.Output.Write(')');
				indent = this.Indent;
				this.Indent = indent - 1;
				indent = this.Indent;
				this.Indent = indent - 1;
			}
			this.OutputStartingBrace();
			indent = this.Indent;
			this.Indent = indent + 1;
			this.GenerateStatements(e.Statements);
			indent = this.Indent;
			this.Indent = indent - 1;
			this.Output.WriteLine('}');
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001B570 File Offset: 0x00019770
		private void GenerateTypeConstructor(CodeTypeConstructor e)
		{
			if (!this.IsCurrentClass && !this.IsCurrentStruct)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			this.Output.Write("static ");
			this.Output.Write(this.CurrentTypeName);
			this.Output.Write("()");
			this.OutputStartingBrace();
			int indent = this.Indent;
			this.Indent = indent + 1;
			this.GenerateStatements(e.Statements);
			indent = this.Indent;
			this.Indent = indent - 1;
			this.Output.WriteLine('}');
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001B618 File Offset: 0x00019818
		private void GenerateTypeReferenceExpression(CodeTypeReferenceExpression e)
		{
			this.OutputType(e.Type);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001B626 File Offset: 0x00019826
		private void GenerateTypeOfExpression(CodeTypeOfExpression e)
		{
			this.Output.Write("typeof(");
			this.OutputType(e.Type);
			this.Output.Write(')');
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001B654 File Offset: 0x00019854
		private void GenerateType(CodeTypeDeclaration e)
		{
			this._currentClass = e;
			if (e.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(e.StartDirectives);
			}
			this.GenerateCommentStatements(e.Comments);
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaStart(e.LinePragma);
			}
			this.GenerateTypeStart(e);
			if (this.Options.VerbatimOrder)
			{
				using (IEnumerator enumerator = e.Members.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						CodeTypeMember member = (CodeTypeMember)obj;
						this.GenerateTypeMember(member, e);
					}
					goto IL_CA;
				}
			}
			this.GenerateFields(e);
			this.GenerateSnippetMembers(e);
			this.GenerateTypeConstructors(e);
			this.GenerateConstructors(e);
			this.GenerateProperties(e);
			this.GenerateEvents(e);
			this.GenerateMethods(e);
			this.GenerateNestedTypes(e);
			IL_CA:
			this._currentClass = e;
			this.GenerateTypeEnd(e);
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(e.LinePragma);
			}
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001B778 File Offset: 0x00019978
		private void GenerateTypes(CodeNamespace e)
		{
			foreach (object obj in e.Types)
			{
				CodeTypeDeclaration e2 = (CodeTypeDeclaration)obj;
				if (this._options.BlankLinesBetweenMembers)
				{
					this.Output.WriteLine();
				}
				((ICodeGenerator)this).GenerateCodeFromType(e2, this._output.InnerWriter, this._options);
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001B7FC File Offset: 0x000199FC
		private void GenerateTypeStart(CodeTypeDeclaration e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			if (this.IsCurrentDelegate)
			{
				TypeAttributes typeAttributes = e.TypeAttributes & TypeAttributes.VisibilityMask;
				if (typeAttributes != TypeAttributes.NotPublic && typeAttributes == TypeAttributes.Public)
				{
					this.Output.Write("public ");
				}
				CodeTypeDelegate codeTypeDelegate = (CodeTypeDelegate)e;
				this.Output.Write("delegate ");
				this.OutputType(codeTypeDelegate.ReturnType);
				this.Output.Write(' ');
				this.OutputIdentifier(e.Name);
				this.Output.Write('(');
				this.OutputParameters(codeTypeDelegate.Parameters);
				this.Output.WriteLine(");");
				return;
			}
			this.OutputTypeAttributes(e);
			this.OutputIdentifier(e.Name);
			this.OutputTypeParameters(e.TypeParameters);
			bool flag = true;
			foreach (object obj in e.BaseTypes)
			{
				CodeTypeReference typeRef = (CodeTypeReference)obj;
				if (flag)
				{
					this.Output.Write(" : ");
					flag = false;
				}
				else
				{
					this.Output.Write(", ");
				}
				this.OutputType(typeRef);
			}
			this.OutputTypeParameterConstraints(e.TypeParameters);
			this.OutputStartingBrace();
			int indent = this.Indent;
			this.Indent = indent + 1;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001B974 File Offset: 0x00019B74
		private void GenerateTypeMember(CodeTypeMember member, CodeTypeDeclaration declaredType)
		{
			if (this._options.BlankLinesBetweenMembers)
			{
				this.Output.WriteLine();
			}
			if (member is CodeTypeDeclaration)
			{
				((ICodeGenerator)this).GenerateCodeFromType((CodeTypeDeclaration)member, this._output.InnerWriter, this._options);
				this._currentClass = declaredType;
				return;
			}
			if (member.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(member.StartDirectives);
			}
			this.GenerateCommentStatements(member.Comments);
			if (member.LinePragma != null)
			{
				this.GenerateLinePragmaStart(member.LinePragma);
			}
			if (member is CodeMemberField)
			{
				this.GenerateField((CodeMemberField)member);
			}
			else if (member is CodeMemberProperty)
			{
				this.GenerateProperty((CodeMemberProperty)member, declaredType);
			}
			else if (member is CodeMemberMethod)
			{
				if (member is CodeConstructor)
				{
					this.GenerateConstructor((CodeConstructor)member, declaredType);
				}
				else if (member is CodeTypeConstructor)
				{
					this.GenerateTypeConstructor((CodeTypeConstructor)member);
				}
				else if (member is CodeEntryPointMethod)
				{
					this.GenerateEntryPointMethod((CodeEntryPointMethod)member, declaredType);
				}
				else
				{
					this.GenerateMethod((CodeMemberMethod)member, declaredType);
				}
			}
			else if (member is CodeMemberEvent)
			{
				this.GenerateEvent((CodeMemberEvent)member, declaredType);
			}
			else if (member is CodeSnippetTypeMember)
			{
				int indent = this.Indent;
				this.Indent = 0;
				this.GenerateSnippetMember((CodeSnippetTypeMember)member);
				this.Indent = indent;
				this.Output.WriteLine();
			}
			if (member.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(member.LinePragma);
			}
			if (member.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(member.EndDirectives);
			}
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001BB0C File Offset: 0x00019D0C
		private void GenerateTypeConstructors(CodeTypeDeclaration e)
		{
			foreach (object obj in e.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember is CodeTypeConstructor)
				{
					this._currentMember = codeTypeMember;
					if (this._options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this._currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this._currentMember.Comments);
					CodeTypeConstructor codeTypeConstructor = (CodeTypeConstructor)codeTypeMember;
					if (codeTypeConstructor.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeTypeConstructor.LinePragma);
					}
					this.GenerateTypeConstructor(codeTypeConstructor);
					if (codeTypeConstructor.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeTypeConstructor.LinePragma);
					}
					if (this._currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001BC20 File Offset: 0x00019E20
		private void GenerateSnippetMembers(CodeTypeDeclaration e)
		{
			bool flag = false;
			foreach (object obj in e.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember is CodeSnippetTypeMember)
				{
					flag = true;
					this._currentMember = codeTypeMember;
					if (this._options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this._currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this._currentMember.Comments);
					CodeSnippetTypeMember codeSnippetTypeMember = (CodeSnippetTypeMember)codeTypeMember;
					if (codeSnippetTypeMember.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeSnippetTypeMember.LinePragma);
					}
					int indent = this.Indent;
					this.Indent = 0;
					this.GenerateSnippetMember(codeSnippetTypeMember);
					this.Indent = indent;
					if (codeSnippetTypeMember.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeSnippetTypeMember.LinePragma);
					}
					if (this._currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this._currentMember.EndDirectives);
					}
				}
			}
			if (flag)
			{
				this.Output.WriteLine();
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001BD60 File Offset: 0x00019F60
		private void GenerateNestedTypes(CodeTypeDeclaration e)
		{
			foreach (object obj in e.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember is CodeTypeDeclaration)
				{
					if (this._options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					CodeTypeDeclaration e2 = (CodeTypeDeclaration)codeTypeMember;
					((ICodeGenerator)this).GenerateCodeFromType(e2, this._output.InnerWriter, this._options);
				}
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001BDF4 File Offset: 0x00019FF4
		private void GenerateNamespaces(CodeCompileUnit e)
		{
			foreach (object obj in e.Namespaces)
			{
				CodeNamespace e2 = (CodeNamespace)obj;
				((ICodeGenerator)this).GenerateCodeFromNamespace(e2, this._output.InnerWriter, this._options);
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001BE60 File Offset: 0x0001A060
		private void OutputAttributeArgument(CodeAttributeArgument arg)
		{
			if (!string.IsNullOrEmpty(arg.Name))
			{
				this.OutputIdentifier(arg.Name);
				this.Output.Write('=');
			}
			((ICodeGenerator)this).GenerateCodeFromExpression(arg.Value, this._output.InnerWriter, this._options);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001BEB0 File Offset: 0x0001A0B0
		private void OutputDirection(FieldDirection dir)
		{
			switch (dir)
			{
			case FieldDirection.In:
				break;
			case FieldDirection.Out:
				this.Output.Write("out ");
				return;
			case FieldDirection.Ref:
				this.Output.Write("ref ");
				break;
			default:
				return;
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001BEE6 File Offset: 0x0001A0E6
		private void OutputExpressionList(CodeExpressionCollection expressions)
		{
			this.OutputExpressionList(expressions, false);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001BEF0 File Offset: 0x0001A0F0
		private void OutputExpressionList(CodeExpressionCollection expressions, bool newlineBetweenItems)
		{
			bool flag = true;
			int indent = this.Indent;
			this.Indent = indent + 1;
			foreach (object obj in expressions)
			{
				CodeExpression e = (CodeExpression)obj;
				if (flag)
				{
					flag = false;
				}
				else if (newlineBetweenItems)
				{
					this.ContinueOnNewLine(",");
				}
				else
				{
					this.Output.Write(", ");
				}
				((ICodeGenerator)this).GenerateCodeFromExpression(e, this._output.InnerWriter, this._options);
			}
			indent = this.Indent;
			this.Indent = indent - 1;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001BFA4 File Offset: 0x0001A1A4
		private void OutputParameters(CodeParameterDeclarationExpressionCollection parameters)
		{
			bool flag = true;
			bool flag2 = parameters.Count > 15;
			if (flag2)
			{
				this.Indent += 3;
			}
			foreach (object obj in parameters)
			{
				CodeParameterDeclarationExpression e = (CodeParameterDeclarationExpression)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.Output.Write(", ");
				}
				if (flag2)
				{
					this.ContinueOnNewLine("");
				}
				this.GenerateExpression(e);
			}
			if (flag2)
			{
				this.Indent -= 3;
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001C050 File Offset: 0x0001A250
		private void OutputTypeNamePair(CodeTypeReference typeRef, string name)
		{
			this.OutputType(typeRef);
			this.Output.Write(' ');
			this.OutputIdentifier(name);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001C070 File Offset: 0x0001A270
		private void OutputTypeParameters(CodeTypeParameterCollection typeParameters)
		{
			if (typeParameters.Count == 0)
			{
				return;
			}
			this.Output.Write('<');
			bool flag = true;
			for (int i = 0; i < typeParameters.Count; i++)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.Output.Write(", ");
				}
				if (typeParameters[i].CustomAttributes.Count > 0)
				{
					this.GenerateAttributes(typeParameters[i].CustomAttributes, null, true);
					this.Output.Write(' ');
				}
				this.Output.Write(typeParameters[i].Name);
			}
			this.Output.Write('>');
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001C118 File Offset: 0x0001A318
		private void OutputTypeParameterConstraints(CodeTypeParameterCollection typeParameters)
		{
			if (typeParameters.Count == 0)
			{
				return;
			}
			for (int i = 0; i < typeParameters.Count; i++)
			{
				this.Output.WriteLine();
				int indent = this.Indent;
				this.Indent = indent + 1;
				bool flag = true;
				if (typeParameters[i].Constraints.Count > 0)
				{
					foreach (object obj in typeParameters[i].Constraints)
					{
						CodeTypeReference typeRef = (CodeTypeReference)obj;
						if (flag)
						{
							this.Output.Write("where ");
							this.Output.Write(typeParameters[i].Name);
							this.Output.Write(" : ");
							flag = false;
						}
						else
						{
							this.Output.Write(", ");
						}
						this.OutputType(typeRef);
					}
				}
				if (typeParameters[i].HasConstructorConstraint)
				{
					if (flag)
					{
						this.Output.Write("where ");
						this.Output.Write(typeParameters[i].Name);
						this.Output.Write(" : new()");
					}
					else
					{
						this.Output.Write(", new ()");
					}
				}
				indent = this.Indent;
				this.Indent = indent - 1;
			}
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001C288 File Offset: 0x0001A488
		private void OutputTypeAttributes(CodeTypeDeclaration e)
		{
			if ((e.Attributes & MemberAttributes.New) != (MemberAttributes)0)
			{
				this.Output.Write("new ");
			}
			TypeAttributes typeAttributes = e.TypeAttributes;
			switch (typeAttributes & TypeAttributes.VisibilityMask)
			{
			case TypeAttributes.NotPublic:
			case TypeAttributes.NestedAssembly:
			case TypeAttributes.NestedFamANDAssem:
				this.Output.Write("internal ");
				break;
			case TypeAttributes.Public:
			case TypeAttributes.NestedPublic:
				this.Output.Write("public ");
				break;
			case TypeAttributes.NestedPrivate:
				this.Output.Write("private ");
				break;
			case TypeAttributes.NestedFamily:
				this.Output.Write("protected ");
				break;
			case TypeAttributes.VisibilityMask:
				this.Output.Write("protected internal ");
				break;
			}
			if (e.IsStruct)
			{
				if (e.IsPartial)
				{
					this.Output.Write("partial ");
				}
				this.Output.Write("struct ");
				return;
			}
			if (e.IsEnum)
			{
				this.Output.Write("enum ");
				return;
			}
			TypeAttributes typeAttributes2 = typeAttributes & TypeAttributes.ClassSemanticsMask;
			if (typeAttributes2 == TypeAttributes.NotPublic)
			{
				if ((typeAttributes & TypeAttributes.Sealed) == TypeAttributes.Sealed)
				{
					this.Output.Write("sealed ");
				}
				if ((typeAttributes & TypeAttributes.Abstract) == TypeAttributes.Abstract)
				{
					this.Output.Write("abstract ");
				}
				if (e.IsPartial)
				{
					this.Output.Write("partial ");
				}
				this.Output.Write("class ");
				return;
			}
			if (typeAttributes2 != TypeAttributes.ClassSemanticsMask)
			{
				return;
			}
			if (e.IsPartial)
			{
				this.Output.Write("partial ");
			}
			this.Output.Write("interface ");
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001C420 File Offset: 0x0001A620
		private void GenerateTypeEnd(CodeTypeDeclaration e)
		{
			if (!this.IsCurrentDelegate)
			{
				int indent = this.Indent;
				this.Indent = indent - 1;
				this.Output.WriteLine('}');
			}
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001C454 File Offset: 0x0001A654
		private void GenerateNamespaceStart(CodeNamespace e)
		{
			if (!string.IsNullOrEmpty(e.Name))
			{
				this.Output.Write("namespace ");
				string[] array = e.Name.Split(CSharpCodeGenerator.s_periodArray);
				this.OutputIdentifier(array[0]);
				for (int i = 1; i < array.Length; i++)
				{
					this.Output.Write('.');
					this.OutputIdentifier(array[i]);
				}
				this.OutputStartingBrace();
				int indent = this.Indent;
				this.Indent = indent + 1;
			}
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001C4D2 File Offset: 0x0001A6D2
		private void GenerateCompileUnit(CodeCompileUnit e)
		{
			this.GenerateCompileUnitStart(e);
			this.GenerateNamespaces(e);
			this.GenerateCompileUnitEnd(e);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001C4EC File Offset: 0x0001A6EC
		private void GenerateCompileUnitStart(CodeCompileUnit e)
		{
			if (e.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(e.StartDirectives);
			}
			this.Output.WriteLine("//------------------------------------------------------------------------------");
			this.Output.Write("// <");
			this.Output.WriteLine("auto-generated>");
			this.Output.Write("//     ");
			this.Output.WriteLine("This code was generated by a tool.");
			this.Output.Write("//     ");
			this.Output.Write("Runtime Version:");
			this.Output.WriteLine(Environment.Version.ToString());
			this.Output.WriteLine("//");
			this.Output.Write("//     ");
			this.Output.WriteLine("Changes to this file may cause incorrect behavior and will be lost if");
			this.Output.Write("//     ");
			this.Output.WriteLine("the code is regenerated.");
			this.Output.Write("// </");
			this.Output.WriteLine("auto-generated>");
			this.Output.WriteLine("//------------------------------------------------------------------------------");
			this.Output.WriteLine();
			SortedSet<string> sortedSet = new SortedSet<string>(StringComparer.Ordinal);
			foreach (object obj in e.Namespaces)
			{
				CodeNamespace codeNamespace = (CodeNamespace)obj;
				if (string.IsNullOrEmpty(codeNamespace.Name))
				{
					codeNamespace.UserData["GenerateImports"] = false;
					foreach (object obj2 in codeNamespace.Imports)
					{
						CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj2;
						sortedSet.Add(codeNamespaceImport.Namespace);
					}
				}
			}
			foreach (string ident in sortedSet)
			{
				this.Output.Write("using ");
				this.OutputIdentifier(ident);
				this.Output.WriteLine(';');
			}
			if (sortedSet.Count > 0)
			{
				this.Output.WriteLine();
			}
			if (e.AssemblyCustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.AssemblyCustomAttributes, "assembly: ");
				this.Output.WriteLine();
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001C788 File Offset: 0x0001A988
		private void GenerateCompileUnitEnd(CodeCompileUnit e)
		{
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001C7A4 File Offset: 0x0001A9A4
		private void GenerateDirectionExpression(CodeDirectionExpression e)
		{
			this.OutputDirection(e.Direction);
			this.GenerateExpression(e.Expression);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001C7C0 File Offset: 0x0001A9C0
		private void GenerateDirectives(CodeDirectiveCollection directives)
		{
			for (int i = 0; i < directives.Count; i++)
			{
				CodeDirective codeDirective = directives[i];
				if (codeDirective is CodeChecksumPragma)
				{
					this.GenerateChecksumPragma((CodeChecksumPragma)codeDirective);
				}
				else if (codeDirective is CodeRegionDirective)
				{
					this.GenerateCodeRegionDirective((CodeRegionDirective)codeDirective);
				}
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001C810 File Offset: 0x0001AA10
		private void GenerateChecksumPragma(CodeChecksumPragma checksumPragma)
		{
			this.Output.Write("#pragma checksum \"");
			this.Output.Write(checksumPragma.FileName);
			this.Output.Write("\" \"");
			this.Output.Write(checksumPragma.ChecksumAlgorithmId.ToString("B", CultureInfo.InvariantCulture));
			this.Output.Write("\" \"");
			if (checksumPragma.ChecksumData != null)
			{
				foreach (byte b in checksumPragma.ChecksumData)
				{
					this.Output.Write(b.ToString("X2", CultureInfo.InvariantCulture));
				}
			}
			this.Output.WriteLine("\"");
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001C8D0 File Offset: 0x0001AAD0
		private void GenerateCodeRegionDirective(CodeRegionDirective regionDirective)
		{
			if (regionDirective.RegionMode == CodeRegionMode.Start)
			{
				this.Output.Write("#region ");
				this.Output.WriteLine(regionDirective.RegionText);
				return;
			}
			if (regionDirective.RegionMode == CodeRegionMode.End)
			{
				this.Output.WriteLine("#endregion");
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001C924 File Offset: 0x0001AB24
		private void GenerateNamespaceEnd(CodeNamespace e)
		{
			if (!string.IsNullOrEmpty(e.Name))
			{
				int indent = this.Indent;
				this.Indent = indent - 1;
				this.Output.WriteLine('}');
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001C95B File Offset: 0x0001AB5B
		private void GenerateNamespaceImport(CodeNamespaceImport e)
		{
			this.Output.Write("using ");
			this.OutputIdentifier(e.Namespace);
			this.Output.WriteLine(';');
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001C986 File Offset: 0x0001AB86
		private void GenerateAttributeDeclarationsStart(CodeAttributeDeclarationCollection attributes)
		{
			this.Output.Write('[');
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001C995 File Offset: 0x0001AB95
		private void GenerateAttributeDeclarationsEnd(CodeAttributeDeclarationCollection attributes)
		{
			this.Output.Write(']');
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001C9A4 File Offset: 0x0001ABA4
		private void GenerateAttributes(CodeAttributeDeclarationCollection attributes)
		{
			this.GenerateAttributes(attributes, null, false);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001C9AF File Offset: 0x0001ABAF
		private void GenerateAttributes(CodeAttributeDeclarationCollection attributes, string prefix)
		{
			this.GenerateAttributes(attributes, prefix, false);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001C9BC File Offset: 0x0001ABBC
		private void GenerateAttributes(CodeAttributeDeclarationCollection attributes, string prefix, bool inLine)
		{
			if (attributes.Count == 0)
			{
				return;
			}
			bool flag = false;
			foreach (object obj in attributes)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj;
				if (codeAttributeDeclaration.Name.Equals("system.paramarrayattribute", StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
				}
				else
				{
					this.GenerateAttributeDeclarationsStart(attributes);
					if (prefix != null)
					{
						this.Output.Write(prefix);
					}
					if (codeAttributeDeclaration.AttributeType != null)
					{
						this.Output.Write(this.GetTypeOutput(codeAttributeDeclaration.AttributeType));
					}
					this.Output.Write('(');
					bool flag2 = true;
					foreach (object obj2 in codeAttributeDeclaration.Arguments)
					{
						CodeAttributeArgument arg = (CodeAttributeArgument)obj2;
						if (flag2)
						{
							flag2 = false;
						}
						else
						{
							this.Output.Write(", ");
						}
						this.OutputAttributeArgument(arg);
					}
					this.Output.Write(')');
					this.GenerateAttributeDeclarationsEnd(attributes);
					if (inLine)
					{
						this.Output.Write(' ');
					}
					else
					{
						this.Output.WriteLine();
					}
				}
			}
			if (flag)
			{
				if (prefix != null)
				{
					this.Output.Write(prefix);
				}
				this.Output.Write("params");
				if (inLine)
				{
					this.Output.Write(' ');
					return;
				}
				this.Output.WriteLine();
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001718F File Offset: 0x0001538F
		public bool Supports(GeneratorSupport support)
		{
			return (support & (GeneratorSupport.ArraysOfArrays | GeneratorSupport.EntryPointMethod | GeneratorSupport.GotoStatements | GeneratorSupport.MultidimensionalArrays | GeneratorSupport.StaticConstructors | GeneratorSupport.TryCatchStatements | GeneratorSupport.ReturnTypeAttributes | GeneratorSupport.DeclareValueTypes | GeneratorSupport.DeclareEnums | GeneratorSupport.DeclareDelegates | GeneratorSupport.DeclareInterfaces | GeneratorSupport.DeclareEvents | GeneratorSupport.AssemblyAttributes | GeneratorSupport.ParameterAttributes | GeneratorSupport.ReferenceParameters | GeneratorSupport.ChainedConstructorArguments | GeneratorSupport.NestedTypes | GeneratorSupport.MultipleInterfaceMembers | GeneratorSupport.PublicStaticMembers | GeneratorSupport.ComplexExpressions | GeneratorSupport.Win32Resources | GeneratorSupport.Resources | GeneratorSupport.PartialTypes | GeneratorSupport.GenericTypeReference | GeneratorSupport.GenericTypeDeclaration | GeneratorSupport.DeclareIndexerProperties)) == support;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001CB6C File Offset: 0x0001AD6C
		public bool IsValidIdentifier(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return false;
			}
			if (value.Length > 512)
			{
				return false;
			}
			if (value[0] != '@')
			{
				if (CSharpHelpers.IsKeyword(value))
				{
					return false;
				}
			}
			else
			{
				value = value.Substring(1);
			}
			return CodeGenerator.IsValidLanguageIndependentIdentifier(value);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001CBAB File Offset: 0x0001ADAB
		public void ValidateIdentifier(string value)
		{
			if (!this.IsValidIdentifier(value))
			{
				throw new ArgumentException(SR.Format("Identifier '{0}' is not valid.", value));
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001CBC7 File Offset: 0x0001ADC7
		public string CreateValidIdentifier(string name)
		{
			if (CSharpHelpers.IsPrefixTwoUnderscore(name))
			{
				name = "_" + name;
			}
			while (CSharpHelpers.IsKeyword(name))
			{
				name = "_" + name;
			}
			return name;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001CBF6 File Offset: 0x0001ADF6
		public string CreateEscapedIdentifier(string name)
		{
			return CSharpHelpers.CreateEscapedIdentifier(name);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001CC00 File Offset: 0x0001AE00
		private string GetBaseTypeOutput(CodeTypeReference typeRef, bool preferBuiltInTypes = true)
		{
			string baseType = typeRef.BaseType;
			if (preferBuiltInTypes)
			{
				if (baseType.Length == 0)
				{
					return "void";
				}
				string text = baseType.ToLower(CultureInfo.InvariantCulture).Trim();
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2218649502U)
				{
					if (num <= 574663925U)
					{
						if (num <= 503664103U)
						{
							if (num != 425110298U)
							{
								if (num == 503664103U)
								{
									if (text == "system.string")
									{
										return "string";
									}
								}
							}
							else if (text == "system.char")
							{
								return "char";
							}
						}
						else if (num != 507700544U)
						{
							if (num == 574663925U)
							{
								if (text == "system.uint16")
								{
									return "ushort";
								}
							}
						}
						else if (text == "system.uint64")
						{
							return "ulong";
						}
					}
					else if (num <= 872348156U)
					{
						if (num != 801448826U)
						{
							if (num == 872348156U)
							{
								if (text == "system.byte")
								{
									return "byte";
								}
							}
						}
						else if (text == "system.int32")
						{
							return "int";
						}
					}
					else if (num != 1487069339U)
					{
						if (num == 2218649502U)
						{
							if (text == "system.boolean")
							{
								return "bool";
							}
						}
					}
					else if (text == "system.double")
					{
						return "double";
					}
				}
				else if (num <= 2679997701U)
				{
					if (num <= 2613725868U)
					{
						if (num != 2446023237U)
						{
							if (num == 2613725868U)
							{
								if (text == "system.int16")
								{
									return "short";
								}
							}
						}
						else if (text == "system.decimal")
						{
							return "decimal";
						}
					}
					else if (num != 2647511797U)
					{
						if (num == 2679997701U)
						{
							if (text == "system.int64")
							{
								return "long";
							}
						}
					}
					else if (text == "system.object")
					{
						return "object";
					}
				}
				else if (num <= 2923133227U)
				{
					if (num != 2790997960U)
					{
						if (num == 2923133227U)
						{
							if (text == "system.uint32")
							{
								return "uint";
							}
						}
					}
					else if (text == "system.void")
					{
						return "void";
					}
				}
				else if (num != 3248684926U)
				{
					if (num == 3680803037U)
					{
						if (text == "system.sbyte")
						{
							return "sbyte";
						}
					}
				}
				else if (text == "system.single")
				{
					return "float";
				}
			}
			StringBuilder stringBuilder = new StringBuilder(baseType.Length + 10);
			if ((typeRef.Options & CodeTypeReferenceOptions.GlobalReference) != (CodeTypeReferenceOptions)0)
			{
				stringBuilder.Append("global::");
			}
			string baseType2 = typeRef.BaseType;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < baseType2.Length; i++)
			{
				char c = baseType2[i];
				if (c != '+' && c != '.')
				{
					if (c == '`')
					{
						stringBuilder.Append(this.CreateEscapedIdentifier(baseType2.Substring(num2, i - num2)));
						i++;
						int num4 = 0;
						while (i < baseType2.Length && baseType2[i] >= '0' && baseType2[i] <= '9')
						{
							num4 = num4 * 10 + (int)(baseType2[i] - '0');
							i++;
						}
						this.GetTypeArgumentsOutput(typeRef.TypeArguments, num3, num4, stringBuilder);
						num3 += num4;
						if (i < baseType2.Length && (baseType2[i] == '+' || baseType2[i] == '.'))
						{
							stringBuilder.Append('.');
							i++;
						}
						num2 = i;
					}
				}
				else
				{
					stringBuilder.Append(this.CreateEscapedIdentifier(baseType2.Substring(num2, i - num2)));
					stringBuilder.Append('.');
					i++;
					num2 = i;
				}
			}
			if (num2 < baseType2.Length)
			{
				stringBuilder.Append(this.CreateEscapedIdentifier(baseType2.Substring(num2)));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001D084 File Offset: 0x0001B284
		private string GetTypeArgumentsOutput(CodeTypeReferenceCollection typeArguments)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			this.GetTypeArgumentsOutput(typeArguments, 0, typeArguments.Count, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001D0B4 File Offset: 0x0001B2B4
		private void GetTypeArgumentsOutput(CodeTypeReferenceCollection typeArguments, int start, int length, StringBuilder sb)
		{
			sb.Append('<');
			bool flag = true;
			for (int i = start; i < start + length; i++)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					sb.Append(", ");
				}
				if (i < typeArguments.Count)
				{
					sb.Append(this.GetTypeOutput(typeArguments[i]));
				}
			}
			sb.Append('>');
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001D118 File Offset: 0x0001B318
		public string GetTypeOutput(CodeTypeReference typeRef)
		{
			string text = string.Empty;
			CodeTypeReference codeTypeReference = typeRef;
			while (codeTypeReference.ArrayElementType != null)
			{
				codeTypeReference = codeTypeReference.ArrayElementType;
			}
			text += this.GetBaseTypeOutput(codeTypeReference, true);
			while (typeRef != null && typeRef.ArrayRank > 0)
			{
				char[] array = new char[typeRef.ArrayRank + 1];
				array[0] = '[';
				array[typeRef.ArrayRank] = ']';
				for (int i = 1; i < typeRef.ArrayRank; i++)
				{
					array[i] = ',';
				}
				text += new string(array);
				typeRef = typeRef.ArrayElementType;
			}
			return text;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001D1A4 File Offset: 0x0001B3A4
		private void OutputStartingBrace()
		{
			if (this.Options.BracingStyle == "C")
			{
				this.Output.WriteLine();
				this.Output.WriteLine('{');
				return;
			}
			this.Output.WriteLine(" {");
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001D1F4 File Offset: 0x0001B3F4
		CompilerResults ICodeCompiler.CompileAssemblyFromDom(CompilerParameters options, CodeCompileUnit e)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults result;
			try
			{
				result = this.FromDom(options, e);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return result;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001D238 File Offset: 0x0001B438
		CompilerResults ICodeCompiler.CompileAssemblyFromFile(CompilerParameters options, string fileName)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults result;
			try
			{
				result = this.FromFile(options, fileName);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return result;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001D27C File Offset: 0x0001B47C
		CompilerResults ICodeCompiler.CompileAssemblyFromSource(CompilerParameters options, string source)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults result;
			try
			{
				result = this.FromSource(options, source);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return result;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001D2C0 File Offset: 0x0001B4C0
		CompilerResults ICodeCompiler.CompileAssemblyFromSourceBatch(CompilerParameters options, string[] sources)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults result;
			try
			{
				result = this.FromSourceBatch(options, sources);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return result;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001D304 File Offset: 0x0001B504
		CompilerResults ICodeCompiler.CompileAssemblyFromFileBatch(CompilerParameters options, string[] fileNames)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			CompilerResults result;
			try
			{
				for (int i = 0; i < fileNames.Length; i++)
				{
					File.OpenRead(fileNames[i]).Dispose();
				}
				result = this.FromFileBatch(options, fileNames);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return result;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001D374 File Offset: 0x0001B574
		CompilerResults ICodeCompiler.CompileAssemblyFromDomBatch(CompilerParameters options, CodeCompileUnit[] ea)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults result;
			try
			{
				result = this.FromDomBatch(options, ea);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return result;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001D3B8 File Offset: 0x0001B5B8
		private CompilerResults FromDom(CompilerParameters options, CodeCompileUnit e)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return this.FromDomBatch(options, new CodeCompileUnit[]
			{
				e
			});
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001D3D9 File Offset: 0x0001B5D9
		private CompilerResults FromFile(CompilerParameters options, string fileName)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			File.OpenRead(fileName).Dispose();
			return this.FromFileBatch(options, new string[]
			{
				fileName
			});
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001D413 File Offset: 0x0001B613
		private CompilerResults FromSource(CompilerParameters options, string source)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return this.FromSourceBatch(options, new string[]
			{
				source
			});
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001D434 File Offset: 0x0001B634
		private CompilerResults FromDomBatch(CompilerParameters options, CodeCompileUnit[] ea)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (ea == null)
			{
				throw new ArgumentNullException("ea");
			}
			string[] array = new string[ea.Length];
			for (int i = 0; i < ea.Length; i++)
			{
				if (ea[i] != null)
				{
					this.ResolveReferencedAssemblies(options, ea[i]);
					array[i] = options.TempFiles.AddExtension(i.ToString() + this.FileExtension);
					using (FileStream fileStream = new FileStream(array[i], FileMode.Create, FileAccess.Write, FileShare.Read))
					{
						using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
						{
							((ICodeGenerator)this).GenerateCodeFromCompileUnit(ea[i], streamWriter, this.Options);
							streamWriter.Flush();
						}
					}
				}
			}
			return this.FromFileBatch(options, array);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001D50C File Offset: 0x0001B70C
		private void ResolveReferencedAssemblies(CompilerParameters options, CodeCompileUnit e)
		{
			if (e.ReferencedAssemblies.Count > 0)
			{
				foreach (string value in e.ReferencedAssemblies)
				{
					if (!options.ReferencedAssemblies.Contains(value))
					{
						options.ReferencedAssemblies.Add(value);
					}
				}
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001D584 File Offset: 0x0001B784
		private CompilerResults FromSourceBatch(CompilerParameters options, string[] sources)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			string[] array = new string[sources.Length];
			for (int i = 0; i < sources.Length; i++)
			{
				string text = options.TempFiles.AddExtension(i.ToString() + this.FileExtension);
				using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.Read))
				{
					using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
					{
						streamWriter.Write(sources[i]);
						streamWriter.Flush();
					}
				}
				array[i] = text;
			}
			return this.FromFileBatch(options, array);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001D64C File Offset: 0x0001B84C
		private static string JoinStringArray(string[] sa, string separator)
		{
			if (sa == null || sa.Length == 0)
			{
				return string.Empty;
			}
			if (sa.Length == 1)
			{
				return "\"" + sa[0] + "\"";
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < sa.Length - 1; i++)
			{
				stringBuilder.Append('"');
				stringBuilder.Append(sa[i]);
				stringBuilder.Append('"');
				stringBuilder.Append(separator);
			}
			stringBuilder.Append('"');
			stringBuilder.Append(sa[sa.Length - 1]);
			stringBuilder.Append('"');
			return stringBuilder.ToString();
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001D6E0 File Offset: 0x0001B8E0
		void ICodeGenerator.GenerateCodeFromType(CodeTypeDeclaration e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this._output != null && w != this._output.InnerWriter)
			{
				throw new InvalidOperationException("The output writer for code generation and the writer supplied don't match and cannot be used. This is generally caused by a bad implementation of a CodeGenerator derived class.");
			}
			if (this._output == null)
			{
				flag = true;
				this._options = (o ?? new CodeGeneratorOptions());
				this._output = new ExposedTabStringIndentedTextWriter(w, this._options.IndentString);
			}
			try
			{
				this.GenerateType(e);
			}
			finally
			{
				if (flag)
				{
					this._output = null;
					this._options = null;
				}
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001D770 File Offset: 0x0001B970
		void ICodeGenerator.GenerateCodeFromExpression(CodeExpression e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this._output != null && w != this._output.InnerWriter)
			{
				throw new InvalidOperationException("The output writer for code generation and the writer supplied don't match and cannot be used. This is generally caused by a bad implementation of a CodeGenerator derived class.");
			}
			if (this._output == null)
			{
				flag = true;
				this._options = (o ?? new CodeGeneratorOptions());
				this._output = new ExposedTabStringIndentedTextWriter(w, this._options.IndentString);
			}
			try
			{
				this.GenerateExpression(e);
			}
			finally
			{
				if (flag)
				{
					this._output = null;
					this._options = null;
				}
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001D800 File Offset: 0x0001BA00
		void ICodeGenerator.GenerateCodeFromCompileUnit(CodeCompileUnit e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this._output != null && w != this._output.InnerWriter)
			{
				throw new InvalidOperationException("The output writer for code generation and the writer supplied don't match and cannot be used. This is generally caused by a bad implementation of a CodeGenerator derived class.");
			}
			if (this._output == null)
			{
				flag = true;
				this._options = (o ?? new CodeGeneratorOptions());
				this._output = new ExposedTabStringIndentedTextWriter(w, this._options.IndentString);
			}
			try
			{
				if (e is CodeSnippetCompileUnit)
				{
					this.GenerateSnippetCompileUnit((CodeSnippetCompileUnit)e);
				}
				else
				{
					this.GenerateCompileUnit(e);
				}
			}
			finally
			{
				if (flag)
				{
					this._output = null;
					this._options = null;
				}
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001D8A4 File Offset: 0x0001BAA4
		void ICodeGenerator.GenerateCodeFromNamespace(CodeNamespace e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this._output != null && w != this._output.InnerWriter)
			{
				throw new InvalidOperationException("The output writer for code generation and the writer supplied don't match and cannot be used. This is generally caused by a bad implementation of a CodeGenerator derived class.");
			}
			if (this._output == null)
			{
				flag = true;
				this._options = (o ?? new CodeGeneratorOptions());
				this._output = new ExposedTabStringIndentedTextWriter(w, this._options.IndentString);
			}
			try
			{
				this.GenerateNamespace(e);
			}
			finally
			{
				if (flag)
				{
					this._output = null;
					this._options = null;
				}
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001D934 File Offset: 0x0001BB34
		void ICodeGenerator.GenerateCodeFromStatement(CodeStatement e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this._output != null && w != this._output.InnerWriter)
			{
				throw new InvalidOperationException("The output writer for code generation and the writer supplied don't match and cannot be used. This is generally caused by a bad implementation of a CodeGenerator derived class.");
			}
			if (this._output == null)
			{
				flag = true;
				this._options = (o ?? new CodeGeneratorOptions());
				this._output = new ExposedTabStringIndentedTextWriter(w, this._options.IndentString);
			}
			try
			{
				this.GenerateStatement(e);
			}
			finally
			{
				if (flag)
				{
					this._output = null;
					this._options = null;
				}
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001D9C4 File Offset: 0x0001BBC4
		private CompilerResults FromFileBatch(CompilerParameters options, string[] fileNames)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			CompilerResults results = new CompilerResults(options.TempFiles);
			Process process = new Process();
			if (Path.DirectorySeparatorChar == '\\')
			{
				process.StartInfo.FileName = MonoToolsLocator.Mono;
				process.StartInfo.Arguments = "\"" + MonoToolsLocator.McsCSharpCompiler + "\" ";
			}
			else
			{
				process.StartInfo.FileName = MonoToolsLocator.McsCSharpCompiler;
			}
			ProcessStartInfo startInfo = process.StartInfo;
			startInfo.Arguments += CSharpCodeGenerator.BuildArgs(options, fileNames, this._provOptions);
			ManualResetEvent stderr_completed = new ManualResetEvent(false);
			ManualResetEvent stdout_completed = new ManualResetEvent(false);
			process.StartInfo.EnvironmentVariables.Remove("MONO_GC_PARAMS");
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			process.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs args)
			{
				if (args.Data != null)
				{
					results.Output.Add(args.Data);
					return;
				}
				stderr_completed.Set();
			};
			process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs args)
			{
				if (args.Data == null)
				{
					stdout_completed.Set();
				}
			};
			process.StartInfo.StandardOutputEncoding = (process.StartInfo.StandardErrorEncoding = Encoding.UTF8);
			try
			{
				process.Start();
			}
			catch (Exception ex)
			{
				Win32Exception ex2 = ex as Win32Exception;
				if (ex2 != null)
				{
					throw new SystemException(string.Format("Error running {0}: {1}", process.StartInfo.FileName, Win32Exception.GetErrorMessage(ex2.NativeErrorCode)));
				}
				throw;
			}
			try
			{
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();
				process.WaitForExit();
				results.NativeCompilerReturnValue = process.ExitCode;
			}
			finally
			{
				stderr_completed.WaitOne(TimeSpan.FromSeconds(30.0));
				stdout_completed.WaitOne(TimeSpan.FromSeconds(30.0));
				process.Close();
			}
			bool flag = true;
			foreach (string error_string in results.Output)
			{
				CompilerError compilerError = CSharpCodeGenerator.CreateErrorFromString(error_string);
				if (compilerError != null)
				{
					results.Errors.Add(compilerError);
					if (!compilerError.IsWarning)
					{
						flag = false;
					}
				}
			}
			if (results.Output.Count > 0)
			{
				results.Output.Insert(0, process.StartInfo.FileName + " " + process.StartInfo.Arguments + Environment.NewLine);
			}
			if (flag)
			{
				if (!File.Exists(options.OutputAssembly))
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (string str in results.Output)
					{
						stringBuilder.Append(str + Environment.NewLine);
					}
					throw new Exception("Compiler failed to produce the assembly. Output: '" + stringBuilder.ToString() + "'");
				}
				if (options.GenerateInMemory)
				{
					using (FileStream fileStream = File.OpenRead(options.OutputAssembly))
					{
						byte[] array = new byte[fileStream.Length];
						fileStream.Read(array, 0, array.Length);
						results.CompiledAssembly = Assembly.Load(array, null);
						fileStream.Close();
						goto IL_39F;
					}
				}
				results.PathToAssembly = options.OutputAssembly;
			}
			else
			{
				results.CompiledAssembly = null;
			}
			IL_39F:
			return results;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0001DDB8 File Offset: 0x0001BFB8
		private static string BuildArgs(CompilerParameters options, string[] fileNames, IDictionary<string, string> providerOptions)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (options.GenerateExecutable)
			{
				stringBuilder.Append("/target:exe ");
			}
			else
			{
				stringBuilder.Append("/target:library ");
			}
			string privateBinPath = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
			if (privateBinPath != null && privateBinPath.Length > 0)
			{
				stringBuilder.AppendFormat("/lib:\"{0}\" ", privateBinPath);
			}
			if (options.Win32Resource != null)
			{
				stringBuilder.AppendFormat("/win32res:\"{0}\" ", options.Win32Resource);
			}
			if (options.IncludeDebugInformation)
			{
				stringBuilder.Append("/debug+ /optimize- ");
			}
			else
			{
				stringBuilder.Append("/debug- /optimize+ ");
			}
			if (options.TreatWarningsAsErrors)
			{
				stringBuilder.Append("/warnaserror ");
			}
			if (options.WarningLevel >= 0)
			{
				stringBuilder.AppendFormat("/warn:{0} ", options.WarningLevel);
			}
			if (options.OutputAssembly == null || options.OutputAssembly.Length == 0)
			{
				string extension = options.GenerateExecutable ? "exe" : "dll";
				options.OutputAssembly = CSharpCodeGenerator.GetTempFileNameWithExtension(options.TempFiles, extension, !options.GenerateInMemory);
			}
			stringBuilder.AppendFormat("/out:\"{0}\" ", options.OutputAssembly);
			foreach (string text in options.ReferencedAssemblies)
			{
				if (text != null && text.Length != 0)
				{
					stringBuilder.AppendFormat("/r:\"{0}\" ", text);
				}
			}
			if (options.CompilerOptions != null)
			{
				stringBuilder.Append(options.CompilerOptions);
				stringBuilder.Append(" ");
			}
			foreach (string arg in options.EmbeddedResources)
			{
				stringBuilder.AppendFormat("/resource:\"{0}\" ", arg);
			}
			foreach (string arg2 in options.LinkedResources)
			{
				stringBuilder.AppendFormat("/linkresource:\"{0}\" ", arg2);
			}
			if (providerOptions != null && providerOptions.Count > 0)
			{
				string text2;
				if (!providerOptions.TryGetValue("CompilerVersion", out text2))
				{
					text2 = "3.5";
				}
				if (text2.Length >= 1 && text2[0] == 'v')
				{
					text2 = text2.Substring(1);
				}
				if (!(text2 == "2.0"))
				{
					if (!(text2 == "3.5"))
					{
					}
				}
				else
				{
					stringBuilder.Append("/langversion:ISO-2 ");
				}
			}
			stringBuilder.Append("/noconfig ");
			stringBuilder.Append(" -- ");
			foreach (string arg3 in fileNames)
			{
				stringBuilder.AppendFormat("\"{0}\" ", arg3);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001E0B0 File Offset: 0x0001C2B0
		private static CompilerError CreateErrorFromString(string error_string)
		{
			if (error_string.StartsWith("BETA"))
			{
				return null;
			}
			if (error_string == null || error_string == "")
			{
				return null;
			}
			CompilerError compilerError = new CompilerError();
			Match match = new Regex("\n\t\t\t^\n\t\t\t(\\s*(?<file>[^\\(]+)                         # filename (optional)\n\t\t\t (\\((?<line>\\d*)(,(?<column>\\d*[\\+]*))?\\))? # line+column (optional)\n\t\t\t :\\s+)?\n\t\t\t(?<level>\\w+)                               # error|warning\n\t\t\t\\s+\n\t\t\t(?<number>[^:]*\\d)                          # CS1234\n\t\t\t:\n\t\t\t\\s*\n\t\t\t(?<message>.*)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace).Match(error_string);
			if (match.Success)
			{
				if (string.Empty != match.Result("${file}"))
				{
					compilerError.FileName = match.Result("${file}");
				}
				if (string.Empty != match.Result("${line}"))
				{
					compilerError.Line = int.Parse(match.Result("${line}"));
				}
				if (string.Empty != match.Result("${column}"))
				{
					compilerError.Column = int.Parse(match.Result("${column}").Trim('+'));
				}
				string a = match.Result("${level}");
				if (a == "warning")
				{
					compilerError.IsWarning = true;
				}
				else if (a != "error")
				{
					return null;
				}
				compilerError.ErrorNumber = match.Result("${number}");
				compilerError.ErrorText = match.Result("${message}");
				return compilerError;
			}
			match = CSharpCodeGenerator.RelatedSymbolsRegex.Match(error_string);
			if (!match.Success)
			{
				compilerError.ErrorText = error_string;
				compilerError.IsWarning = false;
				compilerError.ErrorNumber = "";
				return compilerError;
			}
			return null;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00017D19 File Offset: 0x00015F19
		private static string GetTempFileNameWithExtension(TempFileCollection temp_files, string extension, bool keepFile)
		{
			return temp_files.AddExtension(extension, keepFile);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001E210 File Offset: 0x0001C410
		// Note: this type is marked as 'beforefieldinit'.
		static CSharpCodeGenerator()
		{
		}

		// Token: 0x04000525 RID: 1317
		private static readonly char[] s_periodArray = new char[]
		{
			'.'
		};

		// Token: 0x04000526 RID: 1318
		private ExposedTabStringIndentedTextWriter _output;

		// Token: 0x04000527 RID: 1319
		private CodeGeneratorOptions _options;

		// Token: 0x04000528 RID: 1320
		private CodeTypeDeclaration _currentClass;

		// Token: 0x04000529 RID: 1321
		private CodeTypeMember _currentMember;

		// Token: 0x0400052A RID: 1322
		private bool _inNestedBinary;

		// Token: 0x0400052B RID: 1323
		private readonly IDictionary<string, string> _provOptions;

		// Token: 0x0400052C RID: 1324
		private const int ParameterMultilineThreshold = 15;

		// Token: 0x0400052D RID: 1325
		private const int MaxLineLength = 80;

		// Token: 0x0400052E RID: 1326
		private const GeneratorSupport LanguageSupport = GeneratorSupport.ArraysOfArrays | GeneratorSupport.EntryPointMethod | GeneratorSupport.GotoStatements | GeneratorSupport.MultidimensionalArrays | GeneratorSupport.StaticConstructors | GeneratorSupport.TryCatchStatements | GeneratorSupport.ReturnTypeAttributes | GeneratorSupport.DeclareValueTypes | GeneratorSupport.DeclareEnums | GeneratorSupport.DeclareDelegates | GeneratorSupport.DeclareInterfaces | GeneratorSupport.DeclareEvents | GeneratorSupport.AssemblyAttributes | GeneratorSupport.ParameterAttributes | GeneratorSupport.ReferenceParameters | GeneratorSupport.ChainedConstructorArguments | GeneratorSupport.NestedTypes | GeneratorSupport.MultipleInterfaceMembers | GeneratorSupport.PublicStaticMembers | GeneratorSupport.ComplexExpressions | GeneratorSupport.Win32Resources | GeneratorSupport.Resources | GeneratorSupport.PartialTypes | GeneratorSupport.GenericTypeReference | GeneratorSupport.GenericTypeDeclaration | GeneratorSupport.DeclareIndexerProperties;

		// Token: 0x0400052F RID: 1327
		private static readonly string[][] s_keywords = new string[][]
		{
			null,
			new string[]
			{
				"as",
				"do",
				"if",
				"in",
				"is"
			},
			new string[]
			{
				"for",
				"int",
				"new",
				"out",
				"ref",
				"try"
			},
			new string[]
			{
				"base",
				"bool",
				"byte",
				"case",
				"char",
				"else",
				"enum",
				"goto",
				"lock",
				"long",
				"null",
				"this",
				"true",
				"uint",
				"void"
			},
			new string[]
			{
				"break",
				"catch",
				"class",
				"const",
				"event",
				"false",
				"fixed",
				"float",
				"sbyte",
				"short",
				"throw",
				"ulong",
				"using",
				"while"
			},
			new string[]
			{
				"double",
				"extern",
				"object",
				"params",
				"public",
				"return",
				"sealed",
				"sizeof",
				"static",
				"string",
				"struct",
				"switch",
				"typeof",
				"unsafe",
				"ushort"
			},
			new string[]
			{
				"checked",
				"decimal",
				"default",
				"finally",
				"foreach",
				"private",
				"virtual"
			},
			new string[]
			{
				"abstract",
				"continue",
				"delegate",
				"explicit",
				"implicit",
				"internal",
				"operator",
				"override",
				"readonly",
				"volatile"
			},
			new string[]
			{
				"__arglist",
				"__makeref",
				"__reftype",
				"interface",
				"namespace",
				"protected",
				"unchecked"
			},
			new string[]
			{
				"__refvalue",
				"stackalloc"
			}
		};

		// Token: 0x04000530 RID: 1328
		private bool _generatingForLoop;

		// Token: 0x04000531 RID: 1329
		private const string ErrorRegexPattern = "\n\t\t\t^\n\t\t\t(\\s*(?<file>[^\\(]+)                         # filename (optional)\n\t\t\t (\\((?<line>\\d*)(,(?<column>\\d*[\\+]*))?\\))? # line+column (optional)\n\t\t\t :\\s+)?\n\t\t\t(?<level>\\w+)                               # error|warning\n\t\t\t\\s+\n\t\t\t(?<number>[^:]*\\d)                          # CS1234\n\t\t\t:\n\t\t\t\\s*\n\t\t\t(?<message>.*)$";

		// Token: 0x04000532 RID: 1330
		private static readonly Regex RelatedSymbolsRegex = new Regex("\n            \\(Location\\ of\\ the\\ symbol\\ related\\ to\\ previous\\ (warning|error)\\)\n\t\t\t", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x0200013B RID: 315
		[CompilerGenerated]
		private sealed class <>c__DisplayClass180_0
		{
			// Token: 0x06000869 RID: 2153 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass180_0()
			{
			}

			// Token: 0x0600086A RID: 2154 RVA: 0x0001E53A File Offset: 0x0001C73A
			internal void <FromFileBatch>b__0(object sender, DataReceivedEventArgs args)
			{
				if (args.Data != null)
				{
					this.results.Output.Add(args.Data);
					return;
				}
				this.stderr_completed.Set();
			}

			// Token: 0x0600086B RID: 2155 RVA: 0x0001E568 File Offset: 0x0001C768
			internal void <FromFileBatch>b__1(object sender, DataReceivedEventArgs args)
			{
				if (args.Data == null)
				{
					this.stdout_completed.Set();
				}
			}

			// Token: 0x04000533 RID: 1331
			public CompilerResults results;

			// Token: 0x04000534 RID: 1332
			public ManualResetEvent stderr_completed;

			// Token: 0x04000535 RID: 1333
			public ManualResetEvent stdout_completed;
		}
	}
}
