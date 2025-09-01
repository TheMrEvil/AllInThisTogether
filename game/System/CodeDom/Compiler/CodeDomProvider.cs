using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides a base class for <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementations. This class is abstract.</summary>
	// Token: 0x02000343 RID: 835
	public abstract class CodeDomProvider : Component
	{
		// Token: 0x06001A95 RID: 6805 RVA: 0x000621DC File Offset: 0x000603DC
		static CodeDomProvider()
		{
			CodeDomProvider.AddCompilerInfo(new CompilerInfo(new CompilerParameters
			{
				WarningLevel = 4
			}, typeof(CSharpCodeProvider).FullName)
			{
				_compilerLanguages = new string[]
				{
					"c#",
					"cs",
					"csharp"
				},
				_compilerExtensions = new string[]
				{
					".cs",
					"cs"
				}
			});
			CodeDomProvider.AddCompilerInfo(new CompilerInfo(new CompilerParameters
			{
				WarningLevel = 4
			}, typeof(VBCodeProvider).FullName)
			{
				_compilerLanguages = new string[]
				{
					"vb",
					"vbs",
					"visualbasic",
					"vbscript"
				},
				_compilerExtensions = new string[]
				{
					".vb",
					"vb"
				}
			});
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x000622E8 File Offset: 0x000604E8
		private static void AddCompilerInfo(CompilerInfo compilerInfo)
		{
			foreach (string key in compilerInfo._compilerLanguages)
			{
				CodeDomProvider.s_compilerLanguages[key] = compilerInfo;
			}
			foreach (string key2 in compilerInfo._compilerExtensions)
			{
				CodeDomProvider.s_compilerExtensions[key2] = compilerInfo;
			}
			CodeDomProvider.s_allCompilerInfo.Add(compilerInfo);
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> instance for the specified language and provider options.</summary>
		/// <param name="language">The language name.</param>
		/// <param name="providerOptions">A collection of provider options from the configuration file.</param>
		/// <returns>A CodeDOM provider that is implemented for the specified language name and options.</returns>
		// Token: 0x06001A97 RID: 6807 RVA: 0x0006234A File Offset: 0x0006054A
		public static CodeDomProvider CreateProvider(string language, IDictionary<string, string> providerOptions)
		{
			return CodeDomProvider.GetCompilerInfo(language).CreateProvider(providerOptions);
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> instance for the specified language.</summary>
		/// <param name="language">The language name.</param>
		/// <returns>A CodeDOM provider that is implemented for the specified language name.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The <paramref name="language" /> does not have a configured provider on this computer.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="language" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06001A98 RID: 6808 RVA: 0x00062358 File Offset: 0x00060558
		public static CodeDomProvider CreateProvider(string language)
		{
			return CodeDomProvider.GetCompilerInfo(language).CreateProvider();
		}

		/// <summary>Returns a language name associated with the specified file name extension, as configured in the <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> compiler configuration section.</summary>
		/// <param name="extension">A file name extension.</param>
		/// <returns>A language name associated with the file name extension, as configured in the <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> compiler configuration settings.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <paramref name="extension" /> does not have a configured language provider on this computer.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The <paramref name="extension" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06001A99 RID: 6809 RVA: 0x00062365 File Offset: 0x00060565
		public static string GetLanguageFromExtension(string extension)
		{
			CompilerInfo compilerInfoForExtensionNoThrow = CodeDomProvider.GetCompilerInfoForExtensionNoThrow(extension);
			if (compilerInfoForExtensionNoThrow == null)
			{
				throw new CodeDomProvider.ConfigurationErrorsException("There is no CodeDom provider defined for the language.");
			}
			return compilerInfoForExtensionNoThrow._compilerLanguages[0];
		}

		/// <summary>Tests whether a language has a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation configured on the computer.</summary>
		/// <param name="language">The language name.</param>
		/// <returns>
		///   <see langword="true" /> if a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation is configured for the specified language; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="language" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06001A9A RID: 6810 RVA: 0x00062382 File Offset: 0x00060582
		public static bool IsDefinedLanguage(string language)
		{
			return CodeDomProvider.GetCompilerInfoForLanguageNoThrow(language) != null;
		}

		/// <summary>Tests whether a file name extension has an associated <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation configured on the computer.</summary>
		/// <param name="extension">A file name extension.</param>
		/// <returns>
		///   <see langword="true" /> if a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation is configured for the specified file name extension; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="extension" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06001A9B RID: 6811 RVA: 0x0006238D File Offset: 0x0006058D
		public static bool IsDefinedExtension(string extension)
		{
			return CodeDomProvider.GetCompilerInfoForExtensionNoThrow(extension) != null;
		}

		/// <summary>Returns the language provider and compiler configuration settings for the specified language.</summary>
		/// <param name="language">A language name.</param>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerInfo" /> object populated with settings of the configured <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <paramref name="language" /> does not have a configured provider on this computer.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The <paramref name="language" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06001A9C RID: 6812 RVA: 0x00062398 File Offset: 0x00060598
		public static CompilerInfo GetCompilerInfo(string language)
		{
			CompilerInfo compilerInfoForLanguageNoThrow = CodeDomProvider.GetCompilerInfoForLanguageNoThrow(language);
			if (compilerInfoForLanguageNoThrow == null)
			{
				throw new CodeDomProvider.ConfigurationErrorsException("There is no CodeDom provider defined for the language.");
			}
			return compilerInfoForLanguageNoThrow;
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000623B0 File Offset: 0x000605B0
		private static CompilerInfo GetCompilerInfoForLanguageNoThrow(string language)
		{
			if (language == null)
			{
				throw new ArgumentNullException("language");
			}
			CompilerInfo result;
			CodeDomProvider.s_compilerLanguages.TryGetValue(language.Trim(), out result);
			return result;
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000623E0 File Offset: 0x000605E0
		private static CompilerInfo GetCompilerInfoForExtensionNoThrow(string extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			CompilerInfo result;
			CodeDomProvider.s_compilerExtensions.TryGetValue(extension.Trim(), out result);
			return result;
		}

		/// <summary>Returns the language provider and compiler configuration settings for this computer.</summary>
		/// <returns>An array of type <see cref="T:System.CodeDom.Compiler.CompilerInfo" /> representing the settings of all configured <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementations.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06001A9F RID: 6815 RVA: 0x0006240F File Offset: 0x0006060F
		public static CompilerInfo[] GetAllCompilerInfo()
		{
			return CodeDomProvider.s_allCompilerInfo.ToArray();
		}

		/// <summary>Gets the default file name extension to use for source code files in the current language.</summary>
		/// <returns>A file name extension corresponding to the extension of the source files of the current language. The base implementation always returns <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x0006241B File Offset: 0x0006061B
		public virtual string FileExtension
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets a language features identifier.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.LanguageOptions" /> that indicates special features of the language.</returns>
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x00003062 File Offset: 0x00001262
		public virtual LanguageOptions LanguageOptions
		{
			get
			{
				return LanguageOptions.None;
			}
		}

		/// <summary>When overridden in a derived class, creates a new code generator.</summary>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> that can be used to generate <see cref="N:System.CodeDom" /> based source code representations.</returns>
		// Token: 0x06001AA2 RID: 6818
		[Obsolete("Callers should not use the ICodeGenerator interface and should instead use the methods directly on the CodeDomProvider class. Those inheriting from CodeDomProvider must still implement this interface, and should exclude this warning or also obsolete this method.")]
		public abstract ICodeGenerator CreateGenerator();

		/// <summary>When overridden in a derived class, creates a new code generator using the specified <see cref="T:System.IO.TextWriter" /> for output.</summary>
		/// <param name="output">A <see cref="T:System.IO.TextWriter" /> to use to output.</param>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> that can be used to generate <see cref="N:System.CodeDom" /> based source code representations.</returns>
		// Token: 0x06001AA3 RID: 6819 RVA: 0x00062422 File Offset: 0x00060622
		public virtual ICodeGenerator CreateGenerator(TextWriter output)
		{
			return this.CreateGenerator();
		}

		/// <summary>When overridden in a derived class, creates a new code generator using the specified file name for output.</summary>
		/// <param name="fileName">The file name to output to.</param>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> that can be used to generate <see cref="N:System.CodeDom" /> based source code representations.</returns>
		// Token: 0x06001AA4 RID: 6820 RVA: 0x00062422 File Offset: 0x00060622
		public virtual ICodeGenerator CreateGenerator(string fileName)
		{
			return this.CreateGenerator();
		}

		/// <summary>When overridden in a derived class, creates a new code compiler.</summary>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeCompiler" /> that can be used for compilation of <see cref="N:System.CodeDom" /> based source code representations.</returns>
		// Token: 0x06001AA5 RID: 6821
		[Obsolete("Callers should not use the ICodeCompiler interface and should instead use the methods directly on the CodeDomProvider class. Those inheriting from CodeDomProvider must still implement this interface, and should exclude this warning or also obsolete this method.")]
		public abstract ICodeCompiler CreateCompiler();

		/// <summary>When overridden in a derived class, creates a new code parser.</summary>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeParser" /> that can be used to parse source code. The base implementation always returns <see langword="null" />.</returns>
		// Token: 0x06001AA6 RID: 6822 RVA: 0x00002F6A File Offset: 0x0000116A
		[Obsolete("Callers should not use the ICodeParser interface and should instead use the methods directly on the CodeDomProvider class. Those inheriting from CodeDomProvider must still implement this interface, and should exclude this warning or also obsolete this method.")]
		public virtual ICodeParser CreateParser()
		{
			return null;
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.TypeConverter" /> for the specified data type.</summary>
		/// <param name="type">The type of object to retrieve a type converter for.</param>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type, or <see langword="null" /> if a <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type cannot be found.</returns>
		// Token: 0x06001AA7 RID: 6823 RVA: 0x0006242A File Offset: 0x0006062A
		public virtual TypeConverter GetConverter(Type type)
		{
			return TypeDescriptor.GetConverter(type);
		}

		/// <summary>Compiles an assembly based on the <see cref="N:System.CodeDom" /> trees contained in the specified array of <see cref="T:System.CodeDom.CodeCompileUnit" /> objects, using the specified compiler settings.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the settings for the compilation.</param>
		/// <param name="compilationUnits">An array of type <see cref="T:System.CodeDom.CodeCompileUnit" /> that indicates the code to compile.</param>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> object that indicates the results of the compilation.</returns>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateCompiler" /> method is overridden in a derived class.</exception>
		// Token: 0x06001AA8 RID: 6824 RVA: 0x00062432 File Offset: 0x00060632
		public virtual CompilerResults CompileAssemblyFromDom(CompilerParameters options, params CodeCompileUnit[] compilationUnits)
		{
			return this.CreateCompilerHelper().CompileAssemblyFromDomBatch(options, compilationUnits);
		}

		/// <summary>Compiles an assembly from the source code contained in the specified files, using the specified compiler settings.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the settings for the compilation.</param>
		/// <param name="fileNames">An array of the names of the files to compile.</param>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> object that indicates the results of compilation.</returns>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateCompiler" /> method is overridden in a derived class.</exception>
		// Token: 0x06001AA9 RID: 6825 RVA: 0x00062441 File Offset: 0x00060641
		public virtual CompilerResults CompileAssemblyFromFile(CompilerParameters options, params string[] fileNames)
		{
			return this.CreateCompilerHelper().CompileAssemblyFromFileBatch(options, fileNames);
		}

		/// <summary>Compiles an assembly from the specified array of strings containing source code, using the specified compiler settings.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler settings for this compilation.</param>
		/// <param name="sources">An array of source code strings to compile.</param>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> object that indicates the results of compilation.</returns>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateCompiler" /> method is overridden in a derived class.</exception>
		// Token: 0x06001AAA RID: 6826 RVA: 0x00062450 File Offset: 0x00060650
		public virtual CompilerResults CompileAssemblyFromSource(CompilerParameters options, params string[] sources)
		{
			return this.CreateCompilerHelper().CompileAssemblyFromSourceBatch(options, sources);
		}

		/// <summary>Returns a value that indicates whether the specified value is a valid identifier for the current language.</summary>
		/// <param name="value">The value to verify as a valid identifier.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter is a valid identifier; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06001AAB RID: 6827 RVA: 0x0006245F File Offset: 0x0006065F
		public virtual bool IsValidIdentifier(string value)
		{
			return this.CreateGeneratorHelper().IsValidIdentifier(value);
		}

		/// <summary>Creates an escaped identifier for the specified value.</summary>
		/// <param name="value">The string for which to create an escaped identifier.</param>
		/// <returns>The escaped identifier for the value.</returns>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06001AAC RID: 6828 RVA: 0x0006246D File Offset: 0x0006066D
		public virtual string CreateEscapedIdentifier(string value)
		{
			return this.CreateGeneratorHelper().CreateEscapedIdentifier(value);
		}

		/// <summary>Creates a valid identifier for the specified value.</summary>
		/// <param name="value">The string for which to generate a valid identifier.</param>
		/// <returns>A valid identifier for the specified value.</returns>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06001AAD RID: 6829 RVA: 0x0006247B File Offset: 0x0006067B
		public virtual string CreateValidIdentifier(string value)
		{
			return this.CreateGeneratorHelper().CreateValidIdentifier(value);
		}

		/// <summary>Gets the type indicated by the specified <see cref="T:System.CodeDom.CodeTypeReference" />.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type to return.</param>
		/// <returns>A text representation of the specified type, formatted for the language in which code is generated by this code generator. In Visual Basic, for example, passing in a <see cref="T:System.CodeDom.CodeTypeReference" /> for the <see cref="T:System.Int32" /> type will return "Integer".</returns>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06001AAE RID: 6830 RVA: 0x00062489 File Offset: 0x00060689
		public virtual string GetTypeOutput(CodeTypeReference type)
		{
			return this.CreateGeneratorHelper().GetTypeOutput(type);
		}

		/// <summary>Returns a value indicating whether the specified code generation support is provided.</summary>
		/// <param name="generatorSupport">A <see cref="T:System.CodeDom.Compiler.GeneratorSupport" /> object that indicates the type of code generation support to verify.</param>
		/// <returns>
		///   <see langword="true" /> if the specified code generation support is provided; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06001AAF RID: 6831 RVA: 0x00062497 File Offset: 0x00060697
		public virtual bool Supports(GeneratorSupport generatorSupport)
		{
			return this.CreateGeneratorHelper().Supports(generatorSupport);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) expression and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> object that indicates the expression for which to generate code.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent.</param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06001AB0 RID: 6832 RVA: 0x000624A5 File Offset: 0x000606A5
		public virtual void GenerateCodeFromExpression(CodeExpression expression, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromExpression(expression, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) statement and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="statement">A <see cref="T:System.CodeDom.CodeStatement" /> containing the CodeDOM elements for which to generate code.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent.</param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06001AB1 RID: 6833 RVA: 0x000624B5 File Offset: 0x000606B5
		public virtual void GenerateCodeFromStatement(CodeStatement statement, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromStatement(statement, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) namespace and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="codeNamespace">A <see cref="T:System.CodeDom.CodeNamespace" /> object that indicates the namespace for which to generate code.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent.</param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06001AB2 RID: 6834 RVA: 0x000624C5 File Offset: 0x000606C5
		public virtual void GenerateCodeFromNamespace(CodeNamespace codeNamespace, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromNamespace(codeNamespace, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) compilation unit and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="compileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> for which to generate code.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which the output code is sent.</param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06001AB3 RID: 6835 RVA: 0x000624D5 File Offset: 0x000606D5
		public virtual void GenerateCodeFromCompileUnit(CodeCompileUnit compileUnit, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromCompileUnit(compileUnit, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) type declaration and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="codeType">A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object that indicates the type for which to generate code.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent.</param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06001AB4 RID: 6836 RVA: 0x000624E5 File Offset: 0x000606E5
		public virtual void GenerateCodeFromType(CodeTypeDeclaration codeType, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromType(codeType, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) member declaration and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="member">A <see cref="T:System.CodeDom.CodeTypeMember" /> object that indicates the member for which to generate code.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent.</param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		/// <exception cref="T:System.NotImplementedException">This method is not overridden in a derived class.</exception>
		// Token: 0x06001AB5 RID: 6837 RVA: 0x000624F5 File Offset: 0x000606F5
		public virtual void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
		{
			throw new NotImplementedException("This CodeDomProvider does not support this method.");
		}

		/// <summary>Compiles the code read from the specified text stream into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
		/// <param name="codeStream">A <see cref="T:System.IO.TextReader" /> object that is used to read the code to be parsed.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> that contains a representation of the parsed code.</returns>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06001AB6 RID: 6838 RVA: 0x00062501 File Offset: 0x00060701
		public virtual CodeCompileUnit Parse(TextReader codeStream)
		{
			return this.CreateParserHelper().Parse(codeStream);
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0006250F File Offset: 0x0006070F
		private ICodeCompiler CreateCompilerHelper()
		{
			ICodeCompiler codeCompiler = this.CreateCompiler();
			if (codeCompiler == null)
			{
				throw new NotImplementedException("This CodeDomProvider does not support this method.");
			}
			return codeCompiler;
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x00062525 File Offset: 0x00060725
		private ICodeGenerator CreateGeneratorHelper()
		{
			ICodeGenerator codeGenerator = this.CreateGenerator();
			if (codeGenerator == null)
			{
				throw new NotImplementedException("This CodeDomProvider does not support this method.");
			}
			return codeGenerator;
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x0006253B File Offset: 0x0006073B
		private ICodeParser CreateParserHelper()
		{
			ICodeParser codeParser = this.CreateParser();
			if (codeParser == null)
			{
				throw new NotImplementedException("This CodeDomProvider does not support this method.");
			}
			return codeParser;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> class.</summary>
		// Token: 0x06001ABA RID: 6842 RVA: 0x000507D0 File Offset: 0x0004E9D0
		protected CodeDomProvider()
		{
		}

		// Token: 0x04000E1D RID: 3613
		private static readonly Dictionary<string, CompilerInfo> s_compilerLanguages = new Dictionary<string, CompilerInfo>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000E1E RID: 3614
		private static readonly Dictionary<string, CompilerInfo> s_compilerExtensions = new Dictionary<string, CompilerInfo>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000E1F RID: 3615
		private static readonly List<CompilerInfo> s_allCompilerInfo = new List<CompilerInfo>();

		// Token: 0x02000344 RID: 836
		private sealed class ConfigurationErrorsException : SystemException
		{
			// Token: 0x06001ABB RID: 6843 RVA: 0x0002F15C File Offset: 0x0002D35C
			public ConfigurationErrorsException(string message) : base(message)
			{
			}

			// Token: 0x06001ABC RID: 6844 RVA: 0x00062551 File Offset: 0x00060751
			public ConfigurationErrorsException(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				throw new PlatformNotSupportedException();
			}
		}
	}
}
