using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides an example implementation of the <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> interface. This class is abstract.</summary>
	// Token: 0x02000345 RID: 837
	public abstract class CodeGenerator : ICodeGenerator
	{
		/// <summary>Gets the code type declaration for the current class.</summary>
		/// <returns>The code type declaration for the current class.</returns>
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x00062560 File Offset: 0x00060760
		protected CodeTypeDeclaration CurrentClass
		{
			get
			{
				return this._currentClass;
			}
		}

		/// <summary>Gets the current class name.</summary>
		/// <returns>The current class name.</returns>
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x00062568 File Offset: 0x00060768
		protected string CurrentTypeName
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

		/// <summary>Gets the current member of the class.</summary>
		/// <returns>The current member of the class.</returns>
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x00062583 File Offset: 0x00060783
		protected CodeTypeMember CurrentMember
		{
			get
			{
				return this._currentMember;
			}
		}

		/// <summary>Gets the current member name.</summary>
		/// <returns>The name of the current member.</returns>
		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x0006258B File Offset: 0x0006078B
		protected string CurrentMemberName
		{
			get
			{
				if (this._currentMember == null)
				{
					return "<% unknown %>";
				}
				return this._currentMember.Name;
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is an interface.</summary>
		/// <returns>
		///   <see langword="true" /> if the current object is an interface; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x000625A6 File Offset: 0x000607A6
		protected bool IsCurrentInterface
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsInterface;
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is a class.</summary>
		/// <returns>
		///   <see langword="true" /> if the current object is a class; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x000625CA File Offset: 0x000607CA
		protected bool IsCurrentClass
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsClass;
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is a value type or struct.</summary>
		/// <returns>
		///   <see langword="true" /> if the current object is a value type or struct; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x000625EE File Offset: 0x000607EE
		protected bool IsCurrentStruct
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsStruct;
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is an enumeration.</summary>
		/// <returns>
		///   <see langword="true" /> if the current object is an enumeration; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x00062612 File Offset: 0x00060812
		protected bool IsCurrentEnum
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsEnum;
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is a delegate.</summary>
		/// <returns>
		///   <see langword="true" /> if the current object is a delegate; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x00062636 File Offset: 0x00060836
		protected bool IsCurrentDelegate
		{
			get
			{
				return this._currentClass != null && this._currentClass is CodeTypeDelegate;
			}
		}

		/// <summary>Gets or sets the amount of spaces to indent each indentation level.</summary>
		/// <returns>The number of spaces to indent for each indentation level.</returns>
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x00062650 File Offset: 0x00060850
		// (set) Token: 0x06001AC7 RID: 6855 RVA: 0x0006265D File Offset: 0x0006085D
		protected int Indent
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

		/// <summary>Gets the token that represents <see langword="null" />.</summary>
		/// <returns>The token that represents <see langword="null" />.</returns>
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001AC8 RID: 6856
		protected abstract string NullToken { get; }

		/// <summary>Gets the text writer to use for output.</summary>
		/// <returns>The text writer to use for output.</returns>
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x0006266B File Offset: 0x0006086B
		protected TextWriter Output
		{
			get
			{
				return this._output;
			}
		}

		/// <summary>Gets the options to be used by the code generator.</summary>
		/// <returns>An object that indicates the options for the code generator to use.</returns>
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x00062673 File Offset: 0x00060873
		protected CodeGeneratorOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0006267C File Offset: 0x0006087C
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

		/// <summary>Generates code for the specified code directives.</summary>
		/// <param name="directives">The code directives to generate code for.</param>
		// Token: 0x06001ACC RID: 6860 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void GenerateDirectives(CodeDirectiveCollection directives)
		{
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x000627A0 File Offset: 0x000609A0
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

		// Token: 0x06001ACE RID: 6862 RVA: 0x00062938 File Offset: 0x00060B38
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

		/// <summary>Generates code for the namespaces in the specified compile unit.</summary>
		/// <param name="e">The compile unit to generate namespaces for.</param>
		// Token: 0x06001ACF RID: 6863 RVA: 0x00062A4C File Offset: 0x00060C4C
		protected void GenerateNamespaces(CodeCompileUnit e)
		{
			foreach (object obj in e.Namespaces)
			{
				CodeNamespace e2 = (CodeNamespace)obj;
				((ICodeGenerator)this).GenerateCodeFromNamespace(e2, this._output.InnerWriter, this._options);
			}
		}

		/// <summary>Generates code for the specified namespace and the classes it contains.</summary>
		/// <param name="e">The namespace to generate classes for.</param>
		// Token: 0x06001AD0 RID: 6864 RVA: 0x00062AB8 File Offset: 0x00060CB8
		protected void GenerateTypes(CodeNamespace e)
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

		/// <summary>Gets a value indicating whether the generator provides support for the language features represented by the specified <see cref="T:System.CodeDom.Compiler.GeneratorSupport" /> object.</summary>
		/// <param name="support">The capabilities to test the generator for.</param>
		/// <returns>
		///   <see langword="true" /> if the specified capabilities are supported; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001AD1 RID: 6865 RVA: 0x00062B3C File Offset: 0x00060D3C
		bool ICodeGenerator.Supports(GeneratorSupport support)
		{
			return this.Supports(support);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) type declaration and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">The type to generate code for.</param>
		/// <param name="w">The text writer to output code to.</param>
		/// <param name="o">The options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x06001AD2 RID: 6866 RVA: 0x00062B48 File Offset: 0x00060D48
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

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) expression and outputs it to the specified text writer.</summary>
		/// <param name="e">The expression to generate code for.</param>
		/// <param name="w">The text writer to output code to.</param>
		/// <param name="o">The options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x06001AD3 RID: 6867 RVA: 0x00062BD8 File Offset: 0x00060DD8
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

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) compilation unit and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">The CodeDOM compilation unit to generate code for.</param>
		/// <param name="w">The text writer to output code to.</param>
		/// <param name="o">The options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x06001AD4 RID: 6868 RVA: 0x00062C68 File Offset: 0x00060E68
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

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) namespace and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">The namespace to generate code for.</param>
		/// <param name="w">The text writer to output code to.</param>
		/// <param name="o">The options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x06001AD5 RID: 6869 RVA: 0x00062D0C File Offset: 0x00060F0C
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

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) statement and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">The statement that contains the CodeDOM elements to translate.</param>
		/// <param name="w">The text writer to output code to.</param>
		/// <param name="o">The options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x06001AD6 RID: 6870 RVA: 0x00062D9C File Offset: 0x00060F9C
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

		/// <summary>Generates code for the specified class member using the specified text writer and code generator options.</summary>
		/// <param name="member">The class member to generate code for.</param>
		/// <param name="writer">The text writer to output code to.</param>
		/// <param name="options">The options to use when generating the code.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.CodeDom.Compiler.CodeGenerator.Output" /> property is not <see langword="null" />.</exception>
		// Token: 0x06001AD7 RID: 6871 RVA: 0x00062E2C File Offset: 0x0006102C
		public virtual void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
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

		/// <summary>Gets a value that indicates whether the specified value is a valid identifier for the current language.</summary>
		/// <param name="value">The value to test.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter is a valid identifier; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001AD8 RID: 6872 RVA: 0x00062EB0 File Offset: 0x000610B0
		bool ICodeGenerator.IsValidIdentifier(string value)
		{
			return this.IsValidIdentifier(value);
		}

		/// <summary>Throws an exception if the specified value is not a valid identifier.</summary>
		/// <param name="value">The identifier to validate.</param>
		// Token: 0x06001AD9 RID: 6873 RVA: 0x00062EB9 File Offset: 0x000610B9
		void ICodeGenerator.ValidateIdentifier(string value)
		{
			this.ValidateIdentifier(value);
		}

		/// <summary>Creates an escaped identifier for the specified value.</summary>
		/// <param name="value">The string to create an escaped identifier for.</param>
		/// <returns>The escaped identifier for the value.</returns>
		// Token: 0x06001ADA RID: 6874 RVA: 0x00062EC2 File Offset: 0x000610C2
		string ICodeGenerator.CreateEscapedIdentifier(string value)
		{
			return this.CreateEscapedIdentifier(value);
		}

		/// <summary>Creates a valid identifier for the specified value.</summary>
		/// <param name="value">The string to generate a valid identifier for.</param>
		/// <returns>A valid identifier for the specified value.</returns>
		// Token: 0x06001ADB RID: 6875 RVA: 0x00062ECB File Offset: 0x000610CB
		string ICodeGenerator.CreateValidIdentifier(string value)
		{
			return this.CreateValidIdentifier(value);
		}

		/// <summary>Gets the type indicated by the specified <see cref="T:System.CodeDom.CodeTypeReference" />.</summary>
		/// <param name="type">The type to return.</param>
		/// <returns>The name of the data type reference.</returns>
		// Token: 0x06001ADC RID: 6876 RVA: 0x00062ED4 File Offset: 0x000610D4
		string ICodeGenerator.GetTypeOutput(CodeTypeReference type)
		{
			return this.GetTypeOutput(type);
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00062EE0 File Offset: 0x000610E0
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

		// Token: 0x06001ADE RID: 6878 RVA: 0x00062FF4 File Offset: 0x000611F4
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

		/// <summary>Generates code for the specified code expression.</summary>
		/// <param name="e">The code expression to generate code for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="e" /> is not a valid <see cref="T:System.CodeDom.CodeStatement" />.</exception>
		// Token: 0x06001ADF RID: 6879 RVA: 0x00063108 File Offset: 0x00061308
		protected void GenerateExpression(CodeExpression e)
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

		// Token: 0x06001AE0 RID: 6880 RVA: 0x00063350 File Offset: 0x00061550
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

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00063464 File Offset: 0x00061664
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

		/// <summary>Outputs the code of the specified literal code fragment compile unit.</summary>
		/// <param name="e">The literal code fragment compile unit to generate code for.</param>
		// Token: 0x06001AE2 RID: 6882 RVA: 0x000635A4 File Offset: 0x000617A4
		protected virtual void GenerateSnippetCompileUnit(CodeSnippetCompileUnit e)
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

		// Token: 0x06001AE3 RID: 6883 RVA: 0x00063610 File Offset: 0x00061810
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

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0006375C File Offset: 0x0006195C
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

		/// <summary>Generates code for the specified compile unit.</summary>
		/// <param name="e">The compile unit to generate code for.</param>
		// Token: 0x06001AE5 RID: 6885 RVA: 0x000637F0 File Offset: 0x000619F0
		protected virtual void GenerateCompileUnit(CodeCompileUnit e)
		{
			this.GenerateCompileUnitStart(e);
			this.GenerateNamespaces(e);
			this.GenerateCompileUnitEnd(e);
		}

		/// <summary>Generates code for the specified namespace.</summary>
		/// <param name="e">The namespace to generate code for.</param>
		// Token: 0x06001AE6 RID: 6886 RVA: 0x00063807 File Offset: 0x00061A07
		protected virtual void GenerateNamespace(CodeNamespace e)
		{
			this.GenerateCommentStatements(e.Comments);
			this.GenerateNamespaceStart(e);
			this.GenerateNamespaceImports(e);
			this.Output.WriteLine();
			this.GenerateTypes(e);
			this.GenerateNamespaceEnd(e);
		}

		/// <summary>Generates code for the specified namespace import.</summary>
		/// <param name="e">The namespace import to generate code for.</param>
		// Token: 0x06001AE7 RID: 6887 RVA: 0x0006383C File Offset: 0x00061A3C
		protected void GenerateNamespaceImports(CodeNamespace e)
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

		// Token: 0x06001AE8 RID: 6888 RVA: 0x000638C0 File Offset: 0x00061AC0
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

		/// <summary>Generates code for the specified statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="e" /> is not a valid <see cref="T:System.CodeDom.CodeStatement" />.</exception>
		// Token: 0x06001AE9 RID: 6889 RVA: 0x000639D4 File Offset: 0x00061BD4
		protected void GenerateStatement(CodeStatement e)
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

		/// <summary>Generates code for the specified statement collection.</summary>
		/// <param name="stms">The statements to generate code for.</param>
		// Token: 0x06001AEA RID: 6890 RVA: 0x00063BC4 File Offset: 0x00061DC4
		protected void GenerateStatements(CodeStatementCollection stmts)
		{
			foreach (object obj in stmts)
			{
				CodeStatement e = (CodeStatement)obj;
				((ICodeGenerator)this).GenerateCodeFromStatement(e, this._output.InnerWriter, this._options);
			}
		}

		/// <summary>Generates code for the specified attribute declaration collection.</summary>
		/// <param name="attributes">The attributes to generate code for.</param>
		// Token: 0x06001AEB RID: 6891 RVA: 0x00063C2C File Offset: 0x00061E2C
		protected virtual void OutputAttributeDeclarations(CodeAttributeDeclarationCollection attributes)
		{
			if (attributes.Count == 0)
			{
				return;
			}
			this.GenerateAttributeDeclarationsStart(attributes);
			bool flag = true;
			foreach (object obj in attributes)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.ContinueOnNewLine(", ");
				}
				this.Output.Write(codeAttributeDeclaration.Name);
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
			}
			this.GenerateAttributeDeclarationsEnd(attributes);
		}

		/// <summary>Outputs an argument in an attribute block.</summary>
		/// <param name="arg">The attribute argument to generate code for.</param>
		// Token: 0x06001AEC RID: 6892 RVA: 0x00063D44 File Offset: 0x00061F44
		protected virtual void OutputAttributeArgument(CodeAttributeArgument arg)
		{
			if (!string.IsNullOrEmpty(arg.Name))
			{
				this.OutputIdentifier(arg.Name);
				this.Output.Write('=');
			}
			((ICodeGenerator)this).GenerateCodeFromExpression(arg.Value, this._output.InnerWriter, this._options);
		}

		/// <summary>Generates code for the specified <see cref="T:System.CodeDom.FieldDirection" />.</summary>
		/// <param name="dir">One of the enumeration values that indicates the attribute of the field.</param>
		// Token: 0x06001AED RID: 6893 RVA: 0x00063D94 File Offset: 0x00061F94
		protected virtual void OutputDirection(FieldDirection dir)
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

		/// <summary>Outputs a field scope modifier that corresponds to the specified attributes.</summary>
		/// <param name="attributes">One of the enumeration values that specifies the attributes.</param>
		// Token: 0x06001AEE RID: 6894 RVA: 0x00063DCC File Offset: 0x00061FCC
		protected virtual void OutputFieldScopeModifier(MemberAttributes attributes)
		{
			if ((attributes & MemberAttributes.VTableMask) == MemberAttributes.New)
			{
				this.Output.Write("new ");
			}
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

		/// <summary>Generates code for the specified member access modifier.</summary>
		/// <param name="attributes">One of the enumeration values that indicates the member access modifier to generate code for.</param>
		// Token: 0x06001AEF RID: 6895 RVA: 0x00063E34 File Offset: 0x00062034
		protected virtual void OutputMemberAccessModifier(MemberAttributes attributes)
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

		/// <summary>Generates code for the specified member scope modifier.</summary>
		/// <param name="attributes">One of the enumeration values that indicates the member scope modifier to generate code for.</param>
		// Token: 0x06001AF0 RID: 6896 RVA: 0x00063EE8 File Offset: 0x000620E8
		protected virtual void OutputMemberScopeModifier(MemberAttributes attributes)
		{
			if ((attributes & MemberAttributes.VTableMask) == MemberAttributes.New)
			{
				this.Output.Write("new ");
			}
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
				if (memberAttributes == MemberAttributes.Family || memberAttributes == MemberAttributes.Public)
				{
					this.Output.Write("virtual ");
				}
				return;
			}
			}
		}

		/// <summary>Generates code for the specified type.</summary>
		/// <param name="typeRef">The type to generate code for.</param>
		// Token: 0x06001AF1 RID: 6897
		protected abstract void OutputType(CodeTypeReference typeRef);

		/// <summary>Generates code for the specified type attributes.</summary>
		/// <param name="attributes">One of the enumeration values that indicates the type attributes to generate code for.</param>
		/// <param name="isStruct">
		///   <see langword="true" /> if the type is a struct; otherwise, <see langword="false" />.</param>
		/// <param name="isEnum">
		///   <see langword="true" /> if the type is an enum; otherwise, <see langword="false" />.</param>
		// Token: 0x06001AF2 RID: 6898 RVA: 0x00063F9C File Offset: 0x0006219C
		protected virtual void OutputTypeAttributes(TypeAttributes attributes, bool isStruct, bool isEnum)
		{
			TypeAttributes typeAttributes = attributes & TypeAttributes.VisibilityMask;
			if (typeAttributes - TypeAttributes.Public > 1)
			{
				if (typeAttributes == TypeAttributes.NestedPrivate)
				{
					this.Output.Write("private ");
				}
			}
			else
			{
				this.Output.Write("public ");
			}
			if (isStruct)
			{
				this.Output.Write("struct ");
				return;
			}
			if (isEnum)
			{
				this.Output.Write("enum ");
				return;
			}
			typeAttributes = (attributes & TypeAttributes.ClassSemanticsMask);
			if (typeAttributes == TypeAttributes.NotPublic)
			{
				if ((attributes & TypeAttributes.Sealed) == TypeAttributes.Sealed)
				{
					this.Output.Write("sealed ");
				}
				if ((attributes & TypeAttributes.Abstract) == TypeAttributes.Abstract)
				{
					this.Output.Write("abstract ");
				}
				this.Output.Write("class ");
				return;
			}
			if (typeAttributes != TypeAttributes.ClassSemanticsMask)
			{
				return;
			}
			this.Output.Write("interface ");
		}

		/// <summary>Generates code for the specified object type and name pair.</summary>
		/// <param name="typeRef">The type.</param>
		/// <param name="name">The name for the object.</param>
		// Token: 0x06001AF3 RID: 6899 RVA: 0x0006406E File Offset: 0x0006226E
		protected virtual void OutputTypeNamePair(CodeTypeReference typeRef, string name)
		{
			this.OutputType(typeRef);
			this.Output.Write(' ');
			this.OutputIdentifier(name);
		}

		/// <summary>Outputs the specified identifier.</summary>
		/// <param name="ident">The identifier to output.</param>
		// Token: 0x06001AF4 RID: 6900 RVA: 0x0006408B File Offset: 0x0006228B
		protected virtual void OutputIdentifier(string ident)
		{
			this.Output.Write(ident);
		}

		/// <summary>Generates code for the specified expression list.</summary>
		/// <param name="expressions">The expressions to generate code for.</param>
		// Token: 0x06001AF5 RID: 6901 RVA: 0x00064099 File Offset: 0x00062299
		protected virtual void OutputExpressionList(CodeExpressionCollection expressions)
		{
			this.OutputExpressionList(expressions, false);
		}

		/// <summary>Generates code for the specified expression list.</summary>
		/// <param name="expressions">The expressions to generate code for.</param>
		/// <param name="newlineBetweenItems">
		///   <see langword="true" /> to insert a new line after each item; otherwise, <see langword="false" />.</param>
		// Token: 0x06001AF6 RID: 6902 RVA: 0x000640A4 File Offset: 0x000622A4
		protected virtual void OutputExpressionList(CodeExpressionCollection expressions, bool newlineBetweenItems)
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

		/// <summary>Generates code for the specified operator.</summary>
		/// <param name="op">The operator to generate code for.</param>
		// Token: 0x06001AF7 RID: 6903 RVA: 0x00064158 File Offset: 0x00062358
		protected virtual void OutputOperator(CodeBinaryOperatorType op)
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

		/// <summary>Generates code for the specified parameters.</summary>
		/// <param name="parameters">The parameter declaration expressions to generate code for.</param>
		// Token: 0x06001AF8 RID: 6904 RVA: 0x000642B4 File Offset: 0x000624B4
		protected virtual void OutputParameters(CodeParameterDeclarationExpressionCollection parameters)
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

		/// <summary>Generates code for the specified array creation expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> that indicates the expression to generate code for.</param>
		// Token: 0x06001AF9 RID: 6905
		protected abstract void GenerateArrayCreateExpression(CodeArrayCreateExpression e);

		/// <summary>Generates code for the specified base reference expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeBaseReferenceExpression" /> that indicates the expression to generate code for.</param>
		// Token: 0x06001AFA RID: 6906
		protected abstract void GenerateBaseReferenceExpression(CodeBaseReferenceExpression e);

		/// <summary>Generates code for the specified binary operator expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeBinaryOperatorExpression" /> that indicates the expression to generate code for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> is <see langword="null" />.</exception>
		// Token: 0x06001AFB RID: 6907 RVA: 0x00064360 File Offset: 0x00062560
		protected virtual void GenerateBinaryOperatorExpression(CodeBinaryOperatorExpression e)
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

		/// <summary>Generates a line-continuation character and outputs the specified string on a new line.</summary>
		/// <param name="st">The string to write on the new line.</param>
		// Token: 0x06001AFC RID: 6908 RVA: 0x00064423 File Offset: 0x00062623
		protected virtual void ContinueOnNewLine(string st)
		{
			this.Output.WriteLine(st);
		}

		/// <summary>Generates code for the specified cast expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCastExpression" /> that indicates the expression to generate code for.</param>
		// Token: 0x06001AFD RID: 6909
		protected abstract void GenerateCastExpression(CodeCastExpression e);

		/// <summary>Generates code for the specified delegate creation expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001AFE RID: 6910
		protected abstract void GenerateDelegateCreateExpression(CodeDelegateCreateExpression e);

		/// <summary>Generates code for the specified field reference expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001AFF RID: 6911
		protected abstract void GenerateFieldReferenceExpression(CodeFieldReferenceExpression e);

		/// <summary>Generates code for the specified argument reference expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeArgumentReferenceExpression" /> that indicates the expression to generate code for.</param>
		// Token: 0x06001B00 RID: 6912
		protected abstract void GenerateArgumentReferenceExpression(CodeArgumentReferenceExpression e);

		/// <summary>Generates code for the specified variable reference expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001B01 RID: 6913
		protected abstract void GenerateVariableReferenceExpression(CodeVariableReferenceExpression e);

		/// <summary>Generates code for the specified indexer expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001B02 RID: 6914
		protected abstract void GenerateIndexerExpression(CodeIndexerExpression e);

		/// <summary>Generates code for the specified array indexer expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeArrayIndexerExpression" /> that indicates the expression to generate code for.</param>
		// Token: 0x06001B03 RID: 6915
		protected abstract void GenerateArrayIndexerExpression(CodeArrayIndexerExpression e);

		/// <summary>Outputs the code of the specified literal code fragment expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001B04 RID: 6916
		protected abstract void GenerateSnippetExpression(CodeSnippetExpression e);

		/// <summary>Generates code for the specified method invoke expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001B05 RID: 6917
		protected abstract void GenerateMethodInvokeExpression(CodeMethodInvokeExpression e);

		/// <summary>Generates code for the specified method reference expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001B06 RID: 6918
		protected abstract void GenerateMethodReferenceExpression(CodeMethodReferenceExpression e);

		/// <summary>Generates code for the specified event reference expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001B07 RID: 6919
		protected abstract void GenerateEventReferenceExpression(CodeEventReferenceExpression e);

		/// <summary>Generates code for the specified delegate invoke expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001B08 RID: 6920
		protected abstract void GenerateDelegateInvokeExpression(CodeDelegateInvokeExpression e);

		/// <summary>Generates code for the specified object creation expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001B09 RID: 6921
		protected abstract void GenerateObjectCreateExpression(CodeObjectCreateExpression e);

		/// <summary>Generates code for the specified parameter declaration expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001B0A RID: 6922 RVA: 0x00064434 File Offset: 0x00062634
		protected virtual void GenerateParameterDeclarationExpression(CodeParameterDeclarationExpression e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributeDeclarations(e.CustomAttributes);
				this.Output.Write(' ');
			}
			this.OutputDirection(e.Direction);
			this.OutputTypeNamePair(e.Type, e.Name);
		}

		/// <summary>Generates code for the specified direction expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001B0B RID: 6923 RVA: 0x00064486 File Offset: 0x00062686
		protected virtual void GenerateDirectionExpression(CodeDirectionExpression e)
		{
			this.OutputDirection(e.Direction);
			this.GenerateExpression(e.Expression);
		}

		/// <summary>Generates code for the specified primitive expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="e" /> uses an invalid data type. Only the following data types are valid:  
		///
		/// string  
		///
		/// char  
		///
		/// byte  
		///
		/// Int16  
		///
		/// Int32  
		///
		/// Int64  
		///
		/// Single  
		///
		/// Double  
		///
		/// Decimal</exception>
		// Token: 0x06001B0C RID: 6924 RVA: 0x000644A0 File Offset: 0x000626A0
		protected virtual void GeneratePrimitiveExpression(CodePrimitiveExpression e)
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

		/// <summary>Generates code for a single-precision floating point number.</summary>
		/// <param name="s">The value to generate code for.</param>
		// Token: 0x06001B0D RID: 6925 RVA: 0x000646A0 File Offset: 0x000628A0
		protected virtual void GenerateSingleFloatValue(float s)
		{
			this.Output.Write(s.ToString("R", CultureInfo.InvariantCulture));
		}

		/// <summary>Generates code for a double-precision floating point number.</summary>
		/// <param name="d">The value to generate code for.</param>
		// Token: 0x06001B0E RID: 6926 RVA: 0x000646BE File Offset: 0x000628BE
		protected virtual void GenerateDoubleValue(double d)
		{
			this.Output.Write(d.ToString("R", CultureInfo.InvariantCulture));
		}

		/// <summary>Generates code for the specified decimal value.</summary>
		/// <param name="d">The decimal value to generate code for.</param>
		// Token: 0x06001B0F RID: 6927 RVA: 0x000646DC File Offset: 0x000628DC
		protected virtual void GenerateDecimalValue(decimal d)
		{
			this.Output.Write(d.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>Generates code for the specified reference to a default value.</summary>
		/// <param name="e">The reference to generate code for.</param>
		// Token: 0x06001B10 RID: 6928 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void GenerateDefaultValueExpression(CodeDefaultValueExpression e)
		{
		}

		/// <summary>Generates code for the specified property reference expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001B11 RID: 6929
		protected abstract void GeneratePropertyReferenceExpression(CodePropertyReferenceExpression e);

		/// <summary>Generates code for the specified property set value reference expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001B12 RID: 6930
		protected abstract void GeneratePropertySetValueReferenceExpression(CodePropertySetValueReferenceExpression e);

		/// <summary>Generates code for the specified this reference expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001B13 RID: 6931
		protected abstract void GenerateThisReferenceExpression(CodeThisReferenceExpression e);

		/// <summary>Generates code for the specified type reference expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001B14 RID: 6932 RVA: 0x000646F5 File Offset: 0x000628F5
		protected virtual void GenerateTypeReferenceExpression(CodeTypeReferenceExpression e)
		{
			this.OutputType(e.Type);
		}

		/// <summary>Generates code for the specified type of expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001B15 RID: 6933 RVA: 0x00064703 File Offset: 0x00062903
		protected virtual void GenerateTypeOfExpression(CodeTypeOfExpression e)
		{
			this.Output.Write("typeof(");
			this.OutputType(e.Type);
			this.Output.Write(')');
		}

		/// <summary>Generates code for the specified expression statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06001B16 RID: 6934
		protected abstract void GenerateExpressionStatement(CodeExpressionStatement e);

		/// <summary>Generates code for the specified iteration statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06001B17 RID: 6935
		protected abstract void GenerateIterationStatement(CodeIterationStatement e);

		/// <summary>Generates code for the specified throw exception statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06001B18 RID: 6936
		protected abstract void GenerateThrowExceptionStatement(CodeThrowExceptionStatement e);

		/// <summary>Generates code for the specified comment statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.CodeDom.CodeCommentStatement.Comment" /> property of <paramref name="e" /> is not set.</exception>
		// Token: 0x06001B19 RID: 6937 RVA: 0x0006472E File Offset: 0x0006292E
		protected virtual void GenerateCommentStatement(CodeCommentStatement e)
		{
			if (e.Comment == null)
			{
				throw new ArgumentException(SR.Format("The 'Comment' property of the CodeCommentStatement '{0}' cannot be null.", "e"), "e");
			}
			this.GenerateComment(e.Comment);
		}

		/// <summary>Generates code for the specified comment statements.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001B1A RID: 6938 RVA: 0x00064760 File Offset: 0x00062960
		protected virtual void GenerateCommentStatements(CodeCommentStatementCollection e)
		{
			foreach (object obj in e)
			{
				CodeCommentStatement e2 = (CodeCommentStatement)obj;
				this.GenerateCommentStatement(e2);
			}
		}

		/// <summary>Generates code for the specified comment.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeComment" /> to generate code for.</param>
		// Token: 0x06001B1B RID: 6939
		protected abstract void GenerateComment(CodeComment e);

		/// <summary>Generates code for the specified method return statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06001B1C RID: 6940
		protected abstract void GenerateMethodReturnStatement(CodeMethodReturnStatement e);

		/// <summary>Generates code for the specified conditional statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06001B1D RID: 6941
		protected abstract void GenerateConditionStatement(CodeConditionStatement e);

		/// <summary>Generates code for the specified <see langword="try...catch...finally" /> statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06001B1E RID: 6942
		protected abstract void GenerateTryCatchFinallyStatement(CodeTryCatchFinallyStatement e);

		/// <summary>Generates code for the specified assignment statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeAssignStatement" /> that indicates the statement to generate code for.</param>
		// Token: 0x06001B1F RID: 6943
		protected abstract void GenerateAssignStatement(CodeAssignStatement e);

		/// <summary>Generates code for the specified attach event statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeAttachEventStatement" /> that indicates the statement to generate code for.</param>
		// Token: 0x06001B20 RID: 6944
		protected abstract void GenerateAttachEventStatement(CodeAttachEventStatement e);

		/// <summary>Generates code for the specified remove event statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06001B21 RID: 6945
		protected abstract void GenerateRemoveEventStatement(CodeRemoveEventStatement e);

		/// <summary>Generates code for the specified <see langword="goto" /> statement.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06001B22 RID: 6946
		protected abstract void GenerateGotoStatement(CodeGotoStatement e);

		/// <summary>Generates code for the specified labeled statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06001B23 RID: 6947
		protected abstract void GenerateLabeledStatement(CodeLabeledStatement e);

		/// <summary>Outputs the code of the specified literal code fragment statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06001B24 RID: 6948 RVA: 0x00015660 File Offset: 0x00013860
		protected virtual void GenerateSnippetStatement(CodeSnippetStatement e)
		{
			this.Output.WriteLine(e.Value);
		}

		/// <summary>Generates code for the specified variable declaration statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06001B25 RID: 6949
		protected abstract void GenerateVariableDeclarationStatement(CodeVariableDeclarationStatement e);

		/// <summary>Generates code for the specified line pragma start.</summary>
		/// <param name="e">The start of the line pragma to generate code for.</param>
		// Token: 0x06001B26 RID: 6950
		protected abstract void GenerateLinePragmaStart(CodeLinePragma e);

		/// <summary>Generates code for the specified line pragma end.</summary>
		/// <param name="e">The end of the line pragma to generate code for.</param>
		// Token: 0x06001B27 RID: 6951
		protected abstract void GenerateLinePragmaEnd(CodeLinePragma e);

		/// <summary>Generates code for the specified event.</summary>
		/// <param name="e">The member event to generate code for.</param>
		/// <param name="c">The type of the object that this event occurs on.</param>
		// Token: 0x06001B28 RID: 6952
		protected abstract void GenerateEvent(CodeMemberEvent e, CodeTypeDeclaration c);

		/// <summary>Generates code for the specified member field.</summary>
		/// <param name="e">The field to generate code for.</param>
		// Token: 0x06001B29 RID: 6953
		protected abstract void GenerateField(CodeMemberField e);

		/// <summary>Outputs the code of the specified literal code fragment class member.</summary>
		/// <param name="e">The member to generate code for.</param>
		// Token: 0x06001B2A RID: 6954
		protected abstract void GenerateSnippetMember(CodeSnippetTypeMember e);

		/// <summary>Generates code for the specified entry point method.</summary>
		/// <param name="e">The entry point for the code.</param>
		/// <param name="c">The code that declares the type.</param>
		// Token: 0x06001B2B RID: 6955
		protected abstract void GenerateEntryPointMethod(CodeEntryPointMethod e, CodeTypeDeclaration c);

		/// <summary>Generates code for the specified method.</summary>
		/// <param name="e">The member method to generate code for.</param>
		/// <param name="c">The type of the object that this method occurs on.</param>
		// Token: 0x06001B2C RID: 6956
		protected abstract void GenerateMethod(CodeMemberMethod e, CodeTypeDeclaration c);

		/// <summary>Generates code for the specified property.</summary>
		/// <param name="e">The property to generate code for.</param>
		/// <param name="c">The type of the object that this property occurs on.</param>
		// Token: 0x06001B2D RID: 6957
		protected abstract void GenerateProperty(CodeMemberProperty e, CodeTypeDeclaration c);

		/// <summary>Generates code for the specified constructor.</summary>
		/// <param name="e">The constructor to generate code for.</param>
		/// <param name="c">The type of the object that this constructor constructs.</param>
		// Token: 0x06001B2E RID: 6958
		protected abstract void GenerateConstructor(CodeConstructor e, CodeTypeDeclaration c);

		/// <summary>Generates code for the specified class constructor.</summary>
		/// <param name="e">The class constructor to generate code for.</param>
		// Token: 0x06001B2F RID: 6959
		protected abstract void GenerateTypeConstructor(CodeTypeConstructor e);

		/// <summary>Generates code for the specified start of the class.</summary>
		/// <param name="e">The start of the class to generate code for.</param>
		// Token: 0x06001B30 RID: 6960
		protected abstract void GenerateTypeStart(CodeTypeDeclaration e);

		/// <summary>Generates code for the specified end of the class.</summary>
		/// <param name="e">The end of the class to generate code for.</param>
		// Token: 0x06001B31 RID: 6961
		protected abstract void GenerateTypeEnd(CodeTypeDeclaration e);

		/// <summary>Generates code for the start of a compile unit.</summary>
		/// <param name="e">The compile unit to generate code for.</param>
		// Token: 0x06001B32 RID: 6962 RVA: 0x000647B4 File Offset: 0x000629B4
		protected virtual void GenerateCompileUnitStart(CodeCompileUnit e)
		{
			if (e.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(e.StartDirectives);
			}
		}

		/// <summary>Generates code for the end of a compile unit.</summary>
		/// <param name="e">The compile unit to generate code for.</param>
		// Token: 0x06001B33 RID: 6963 RVA: 0x000647D0 File Offset: 0x000629D0
		protected virtual void GenerateCompileUnitEnd(CodeCompileUnit e)
		{
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		/// <summary>Generates code for the start of a namespace.</summary>
		/// <param name="e">The namespace to generate code for.</param>
		// Token: 0x06001B34 RID: 6964
		protected abstract void GenerateNamespaceStart(CodeNamespace e);

		/// <summary>Generates code for the end of a namespace.</summary>
		/// <param name="e">The namespace to generate code for.</param>
		// Token: 0x06001B35 RID: 6965
		protected abstract void GenerateNamespaceEnd(CodeNamespace e);

		/// <summary>Generates code for the specified namespace import.</summary>
		/// <param name="e">The namespace import to generate code for.</param>
		// Token: 0x06001B36 RID: 6966
		protected abstract void GenerateNamespaceImport(CodeNamespaceImport e);

		/// <summary>Generates code for the specified attribute block start.</summary>
		/// <param name="attributes">A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the start of the attribute block to generate code for.</param>
		// Token: 0x06001B37 RID: 6967
		protected abstract void GenerateAttributeDeclarationsStart(CodeAttributeDeclarationCollection attributes);

		/// <summary>Generates code for the specified attribute block end.</summary>
		/// <param name="attributes">A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the end of the attribute block to generate code for.</param>
		// Token: 0x06001B38 RID: 6968
		protected abstract void GenerateAttributeDeclarationsEnd(CodeAttributeDeclarationCollection attributes);

		/// <summary>Gets a value indicating whether the specified code generation support is provided.</summary>
		/// <param name="support">The type of code generation support to test for.</param>
		/// <returns>
		///   <see langword="true" /> if the specified code generation support is provided; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001B39 RID: 6969
		protected abstract bool Supports(GeneratorSupport support);

		/// <summary>Gets a value indicating whether the specified value is a valid identifier.</summary>
		/// <param name="value">The value to test for conflicts with valid identifiers.</param>
		/// <returns>
		///   <see langword="true" /> if the value is a valid identifier; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001B3A RID: 6970
		protected abstract bool IsValidIdentifier(string value);

		/// <summary>Throws an exception if the specified string is not a valid identifier.</summary>
		/// <param name="value">The identifier to test for validity as an identifier.</param>
		/// <exception cref="T:System.ArgumentException">If the specified identifier is invalid or conflicts with reserved or language keywords.</exception>
		// Token: 0x06001B3B RID: 6971 RVA: 0x000647EC File Offset: 0x000629EC
		protected virtual void ValidateIdentifier(string value)
		{
			if (!this.IsValidIdentifier(value))
			{
				throw new ArgumentException(SR.Format("Identifier '{0}' is not valid.", value));
			}
		}

		/// <summary>Creates an escaped identifier for the specified value.</summary>
		/// <param name="value">The string to create an escaped identifier for.</param>
		/// <returns>The escaped identifier for the value.</returns>
		// Token: 0x06001B3C RID: 6972
		protected abstract string CreateEscapedIdentifier(string value);

		/// <summary>Creates a valid identifier for the specified value.</summary>
		/// <param name="value">A string to create a valid identifier for.</param>
		/// <returns>A valid identifier for the value.</returns>
		// Token: 0x06001B3D RID: 6973
		protected abstract string CreateValidIdentifier(string value);

		/// <summary>Gets the name of the specified data type.</summary>
		/// <param name="value">The type whose name will be returned.</param>
		/// <returns>The name of the data type reference.</returns>
		// Token: 0x06001B3E RID: 6974
		protected abstract string GetTypeOutput(CodeTypeReference value);

		/// <summary>Converts the specified string by formatting it with escape codes.</summary>
		/// <param name="value">The string to convert.</param>
		/// <returns>The converted string.</returns>
		// Token: 0x06001B3F RID: 6975
		protected abstract string QuoteSnippetString(string value);

		/// <summary>Gets a value indicating whether the specified string is a valid identifier.</summary>
		/// <param name="value">The string to test for validity.</param>
		/// <returns>
		///   <see langword="true" /> if the specified string is a valid identifier; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001B40 RID: 6976 RVA: 0x0001EB85 File Offset: 0x0001CD85
		public static bool IsValidLanguageIndependentIdentifier(string value)
		{
			return CSharpHelpers.IsValidTypeNameOrIdentifier(value, false);
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x00064808 File Offset: 0x00062A08
		internal static bool IsValidLanguageIndependentTypeName(string value)
		{
			return CSharpHelpers.IsValidTypeNameOrIdentifier(value, true);
		}

		/// <summary>Attempts to validate each identifier field contained in the specified <see cref="T:System.CodeDom.CodeObject" /> or <see cref="N:System.CodeDom" /> tree.</summary>
		/// <param name="e">An object to test for invalid identifiers.</param>
		/// <exception cref="T:System.ArgumentException">The specified <see cref="T:System.CodeDom.CodeObject" /> contains an invalid identifier.</exception>
		// Token: 0x06001B42 RID: 6978 RVA: 0x00064811 File Offset: 0x00062A11
		public static void ValidateIdentifiers(CodeObject e)
		{
			new CodeValidator().ValidateIdentifiers(e);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CodeGenerator" /> class.</summary>
		// Token: 0x06001B43 RID: 6979 RVA: 0x0000219B File Offset: 0x0000039B
		protected CodeGenerator()
		{
		}

		// Token: 0x04000E20 RID: 3616
		private const int ParameterMultilineThreshold = 15;

		// Token: 0x04000E21 RID: 3617
		private ExposedTabStringIndentedTextWriter _output;

		// Token: 0x04000E22 RID: 3618
		private CodeGeneratorOptions _options;

		// Token: 0x04000E23 RID: 3619
		private CodeTypeDeclaration _currentClass;

		// Token: 0x04000E24 RID: 3620
		private CodeTypeMember _currentMember;

		// Token: 0x04000E25 RID: 3621
		private bool _inNestedBinary;
	}
}
