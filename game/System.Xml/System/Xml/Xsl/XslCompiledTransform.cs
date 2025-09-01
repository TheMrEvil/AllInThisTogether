using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Security.Permissions;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;
using System.Xml.Xsl.Xslt;

namespace System.Xml.Xsl
{
	/// <summary>Transforms XML data using an XSLT style sheet.</summary>
	// Token: 0x02000343 RID: 835
	public sealed class XslCompiledTransform
	{
		// Token: 0x06002266 RID: 8806 RVA: 0x000D9664 File Offset: 0x000D7864
		static XslCompiledTransform()
		{
			XslCompiledTransform.MemberAccessPermissionSet.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.MemberAccess));
			XslCompiledTransform.ReaderSettings = new XmlReaderSettings();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XslCompiledTransform" /> class. </summary>
		// Token: 0x06002267 RID: 8807 RVA: 0x0000216B File Offset: 0x0000036B
		public XslCompiledTransform()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XslCompiledTransform" /> class with the specified debug setting. </summary>
		/// <param name="enableDebug">
		///       <see langword="true" /> to generate debug information; otherwise <see langword="false" />. Setting this to <see langword="true" /> enables you to debug the style sheet with the Microsoft Visual Studio Debugger.</param>
		// Token: 0x06002268 RID: 8808 RVA: 0x000D968C File Offset: 0x000D788C
		public XslCompiledTransform(bool enableDebug)
		{
			this.enableDebug = enableDebug;
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x000D969B File Offset: 0x000D789B
		private void Reset()
		{
			this.compilerResults = null;
			this.outputSettings = null;
			this.qil = null;
			this.command = null;
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x000D96B9 File Offset: 0x000D78B9
		internal CompilerErrorCollection Errors
		{
			get
			{
				if (this.compilerResults == null)
				{
					return null;
				}
				return this.compilerResults.Errors;
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.XmlWriterSettings" /> object that contains the output information derived from the xsl:output element of the style sheet.</summary>
		/// <returns>A read-only <see cref="T:System.Xml.XmlWriterSettings" /> object that contains the output information derived from the xsl:output element of the style sheet. This value can be <see langword="null" />.</returns>
		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x000D96D0 File Offset: 0x000D78D0
		public XmlWriterSettings OutputSettings
		{
			get
			{
				return this.outputSettings;
			}
		}

		/// <summary>Gets the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> that contains the temporary files generated on disk after a successful call to the <see cref="Overload:System.Xml.Xsl.XslCompiledTransform.Load" /> method. </summary>
		/// <returns>The <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> that contains the temporary files generated on disk. This value is <see langword="null" /> if the <see cref="Overload:System.Xml.Xsl.XslCompiledTransform.Load" /> method has not been successfully called, or if debugging has not been enabled.</returns>
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x000D96D8 File Offset: 0x000D78D8
		public TempFileCollection TemporaryFiles
		{
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			get
			{
				if (this.compilerResults == null)
				{
					return null;
				}
				return this.compilerResults.TempFiles;
			}
		}

		/// <summary>Compiles the style sheet contained in the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XmlReader" /> containing the style sheet.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stylesheet" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">The style sheet contains an error.</exception>
		// Token: 0x0600226D RID: 8813 RVA: 0x000D96EF File Offset: 0x000D78EF
		public void Load(XmlReader stylesheet)
		{
			this.Reset();
			this.LoadInternal(stylesheet, XsltSettings.Default, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Compiles the XSLT style sheet contained in the <see cref="T:System.Xml.XmlReader" />. The <see cref="T:System.Xml.XmlResolver" /> resolves any XSLT import or include elements and the XSLT settings determine the permissions for the style sheet.</summary>
		/// <param name="stylesheet">The <see cref="T:System.Xml.XmlReader" /> containing the style sheet.</param>
		/// <param name="settings">The <see cref="T:System.Xml.Xsl.XsltSettings" /> to apply to the style sheet. If this is <see langword="null" />, the <see cref="P:System.Xml.Xsl.XsltSettings.Default" /> setting is applied.</param>
		/// <param name="stylesheetResolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve any style sheets referenced in XSLT import and include elements. If this is <see langword="null" />, external resources are not resolved.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stylesheet" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">The style sheet contains an error.</exception>
		// Token: 0x0600226E RID: 8814 RVA: 0x000D9709 File Offset: 0x000D7909
		public void Load(XmlReader stylesheet, XsltSettings settings, XmlResolver stylesheetResolver)
		{
			this.Reset();
			this.LoadInternal(stylesheet, settings, stylesheetResolver);
		}

		/// <summary>Compiles the style sheet contained in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object.</summary>
		/// <param name="stylesheet">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the Microsoft .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the style sheet.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stylesheet" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">The style sheet contains an error.</exception>
		// Token: 0x0600226F RID: 8815 RVA: 0x000D96EF File Offset: 0x000D78EF
		public void Load(IXPathNavigable stylesheet)
		{
			this.Reset();
			this.LoadInternal(stylesheet, XsltSettings.Default, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Compiles the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.IXPathNavigable" />. The <see cref="T:System.Xml.XmlResolver" /> resolves any XSLT import or include elements and the XSLT settings determine the permissions for the style sheet.</summary>
		/// <param name="stylesheet">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the Microsoft .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the style sheet.</param>
		/// <param name="settings">The <see cref="T:System.Xml.Xsl.XsltSettings" /> to apply to the style sheet. If this is <see langword="null" />, the <see cref="P:System.Xml.Xsl.XsltSettings.Default" /> setting is applied.</param>
		/// <param name="stylesheetResolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve any style sheets referenced in XSLT import and include elements. If this is <see langword="null" />, external resources are not resolved.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stylesheet" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">The style sheet contains an error.</exception>
		// Token: 0x06002270 RID: 8816 RVA: 0x000D9709 File Offset: 0x000D7909
		public void Load(IXPathNavigable stylesheet, XsltSettings settings, XmlResolver stylesheetResolver)
		{
			this.Reset();
			this.LoadInternal(stylesheet, settings, stylesheetResolver);
		}

		/// <summary>Loads and compiles the style sheet located at the specified URI.</summary>
		/// <param name="stylesheetUri">The URI of the style sheet.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stylesheetUri" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">The style sheet contains an error.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The style sheet cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="stylesheetUri" /> value includes a filename or directory that cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="stylesheetUri" /> value cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///         <paramref name="stylesheetUri" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the style sheet.</exception>
		// Token: 0x06002271 RID: 8817 RVA: 0x000D971B File Offset: 0x000D791B
		public void Load(string stylesheetUri)
		{
			this.Reset();
			if (stylesheetUri == null)
			{
				throw new ArgumentNullException("stylesheetUri");
			}
			this.LoadInternal(stylesheetUri, XsltSettings.Default, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Loads and compiles the XSLT style sheet specified by the URI. The <see cref="T:System.Xml.XmlResolver" /> resolves any XSLT import or include elements and the XSLT settings determine the permissions for the style sheet.</summary>
		/// <param name="stylesheetUri">The URI of the style sheet.</param>
		/// <param name="settings">The <see cref="T:System.Xml.Xsl.XsltSettings" /> to apply to the style sheet. If this is <see langword="null" />, the <see cref="P:System.Xml.Xsl.XsltSettings.Default" /> setting is applied.</param>
		/// <param name="stylesheetResolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the style sheet URI and any style sheets referenced in XSLT import and include elements. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stylesheetUri" /> or <paramref name="stylesheetResolver" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">The style sheet contains an error.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The style sheet cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="stylesheetUri" /> value includes a filename or directory that cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="stylesheetUri" /> value cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///         <paramref name="stylesheetUri" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the style sheet.</exception>
		// Token: 0x06002272 RID: 8818 RVA: 0x000D9743 File Offset: 0x000D7943
		public void Load(string stylesheetUri, XsltSettings settings, XmlResolver stylesheetResolver)
		{
			this.Reset();
			if (stylesheetUri == null)
			{
				throw new ArgumentNullException("stylesheetUri");
			}
			this.LoadInternal(stylesheetUri, settings, stylesheetResolver);
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x000D9764 File Offset: 0x000D7964
		private CompilerResults LoadInternal(object stylesheet, XsltSettings settings, XmlResolver stylesheetResolver)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			if (settings == null)
			{
				settings = XsltSettings.Default;
			}
			this.CompileXsltToQil(stylesheet, settings, stylesheetResolver);
			CompilerError firstError = this.GetFirstError();
			if (firstError != null)
			{
				throw new XslLoadException(firstError);
			}
			if (!settings.CheckOnly)
			{
				this.CompileQilToMsil(settings);
			}
			return this.compilerResults;
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x000D97B8 File Offset: 0x000D79B8
		private void CompileXsltToQil(object stylesheet, XsltSettings settings, XmlResolver stylesheetResolver)
		{
			this.compilerResults = new Compiler(settings, this.enableDebug, null).Compile(stylesheet, stylesheetResolver, out this.qil);
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x000D97DC File Offset: 0x000D79DC
		private CompilerError GetFirstError()
		{
			foreach (object obj in this.compilerResults.Errors)
			{
				CompilerError compilerError = (CompilerError)obj;
				if (!compilerError.IsWarning)
				{
					return compilerError;
				}
			}
			return null;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x000D9844 File Offset: 0x000D7A44
		private void CompileQilToMsil(XsltSettings settings)
		{
			this.command = new XmlILGenerator().Generate(this.qil, null);
			this.outputSettings = this.command.StaticData.DefaultWriterSettings;
			this.qil = null;
		}

		/// <summary>Compiles an XSLT style sheet to a specified type.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XmlReader" /> positioned at the beginning of the style sheet to be compiled.</param>
		/// <param name="settings">The <see cref="T:System.Xml.Xsl.XsltSettings" /> to be applied to the style sheet. If this is <see langword="null" />, the <see cref="P:System.Xml.Xsl.XsltSettings.Default" /> will be applied.</param>
		/// <param name="stylesheetResolver">The <see cref="T:System.Xml.XmlResolver" /> use to resolve style sheet modules referenced in <see langword="xsl:import" /> and <see langword="xsl:include" /> elements. If this is <see langword="null" />, external resources will not be resolved.</param>
		/// <param name="debug">Setting this to <see langword="true" /> enables debugging the style sheet with a debugger.</param>
		/// <param name="typeBuilder">The <see cref="T:System.Reflection.Emit.TypeBuilder" /> used for the style sheet compilation. The provided TypeBuilder is used to generate the resulting type.</param>
		/// <param name="scriptAssemblyPath">The base path for the assemblies generated for <see langword="msxsl:script" /> elements. If only one script assembly is generated, this parameter specifies the path for that assembly. In case of multiple script assemblies, a distinctive suffix will be appended to the file name to ensure uniqueness of assembly names.</param>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> object containing compiler errors and warnings that indicate the results of the compilation.</returns>
		// Token: 0x06002277 RID: 8823 RVA: 0x000D987C File Offset: 0x000D7A7C
		public static CompilerErrorCollection CompileToType(XmlReader stylesheet, XsltSettings settings, XmlResolver stylesheetResolver, bool debug, TypeBuilder typeBuilder, string scriptAssemblyPath)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			if (typeBuilder == null)
			{
				throw new ArgumentNullException("typeBuilder");
			}
			if (settings == null)
			{
				settings = XsltSettings.Default;
			}
			if (settings.EnableScript && scriptAssemblyPath == null)
			{
				throw new ArgumentNullException("scriptAssemblyPath");
			}
			if (scriptAssemblyPath != null)
			{
				scriptAssemblyPath = Path.GetFullPath(scriptAssemblyPath);
			}
			QilExpression query;
			CompilerErrorCollection errors = new Compiler(settings, debug, scriptAssemblyPath).Compile(stylesheet, stylesheetResolver, out query).Errors;
			if (!errors.HasErrors)
			{
				if (XslCompiledTransform.GeneratedCodeCtor == null)
				{
					XslCompiledTransform.GeneratedCodeCtor = typeof(GeneratedCodeAttribute).GetConstructor(new Type[]
					{
						typeof(string),
						typeof(string)
					});
				}
				typeBuilder.SetCustomAttribute(new CustomAttributeBuilder(XslCompiledTransform.GeneratedCodeCtor, new object[]
				{
					typeof(XslCompiledTransform).FullName,
					"4.0.0.0"
				}));
				new XmlILGenerator().Generate(query, typeBuilder);
			}
			return errors;
		}

		/// <summary>Loads the compiled style sheet that was created using the XSLT Compiler (xsltc.exe).</summary>
		/// <param name="compiledStylesheet">The name of the class that contains the compiled style sheet. This is usually the name of the style sheet. Unless otherwise specified, the xsltc.exe tool uses the name of the style sheet for the class and assembly names.</param>
		// Token: 0x06002278 RID: 8824 RVA: 0x000D9984 File Offset: 0x000D7B84
		public void Load(Type compiledStylesheet)
		{
			this.Reset();
			if (compiledStylesheet == null)
			{
				throw new ArgumentNullException("compiledStylesheet");
			}
			object[] customAttributes = compiledStylesheet.GetCustomAttributes(typeof(GeneratedCodeAttribute), false);
			GeneratedCodeAttribute generatedCodeAttribute = (customAttributes.Length != 0) ? ((GeneratedCodeAttribute)customAttributes[0]) : null;
			if (generatedCodeAttribute != null && generatedCodeAttribute.Tool == typeof(XslCompiledTransform).FullName)
			{
				if (new Version("4.0.0.0").CompareTo(new Version(generatedCodeAttribute.Version)) < 0)
				{
					throw new ArgumentException(Res.GetString("Executing a stylesheet that was compiled using a later version of the framework is not supported. Stylesheet Version: {0}. Current Framework Version: {1}.", new object[]
					{
						generatedCodeAttribute.Version,
						"4.0.0.0"
					}), "compiledStylesheet");
				}
				FieldInfo field = compiledStylesheet.GetField("staticData", BindingFlags.Static | BindingFlags.NonPublic);
				FieldInfo field2 = compiledStylesheet.GetField("ebTypes", BindingFlags.Static | BindingFlags.NonPublic);
				if (field != null && field2 != null)
				{
					if (XsltConfigSection.EnableMemberAccessForXslCompiledTransform)
					{
						new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
					}
					byte[] array = field.GetValue(null) as byte[];
					if (array != null)
					{
						MethodInfo method = compiledStylesheet.GetMethod("Execute", BindingFlags.Static | BindingFlags.NonPublic);
						Type[] earlyBoundTypes = (Type[])field2.GetValue(null);
						this.Load(method, array, earlyBoundTypes);
						return;
					}
				}
			}
			if (this.command == null)
			{
				throw new ArgumentException(Res.GetString("Type '{0}' is not a compiled stylesheet class.", new object[]
				{
					compiledStylesheet.FullName
				}), "compiledStylesheet");
			}
		}

		/// <summary>Loads a method from a style sheet compiled using the <see langword="XSLTC.exe" /> utility.</summary>
		/// <param name="executeMethod">A <see cref="T:System.Reflection.MethodInfo" /> object representing the compiler-generated <paramref name="execute" /> method of the compiled style sheet.</param>
		/// <param name="queryData">A byte array of serialized data structures in the <paramref name="staticData" /> field of the compiled style sheet as generated by the <see cref="M:System.Xml.Xsl.XslCompiledTransform.CompileToType(System.Xml.XmlReader,System.Xml.Xsl.XsltSettings,System.Xml.XmlResolver,System.Boolean,System.Reflection.Emit.TypeBuilder,System.String)" /> method.</param>
		/// <param name="earlyBoundTypes">An array of types stored in the compiler-generated <paramref name="ebTypes" /> field of the compiled style sheet.</param>
		// Token: 0x06002279 RID: 8825 RVA: 0x000D9AE4 File Offset: 0x000D7CE4
		public void Load(MethodInfo executeMethod, byte[] queryData, Type[] earlyBoundTypes)
		{
			this.Reset();
			if (executeMethod == null)
			{
				throw new ArgumentNullException("executeMethod");
			}
			if (queryData == null)
			{
				throw new ArgumentNullException("queryData");
			}
			if (!XsltConfigSection.EnableMemberAccessForXslCompiledTransform && executeMethod.DeclaringType != null && !executeMethod.DeclaringType.IsVisible)
			{
				new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
			}
			DynamicMethod dynamicMethod = executeMethod as DynamicMethod;
			Delegate @delegate = (dynamicMethod != null) ? dynamicMethod.CreateDelegate(typeof(ExecuteDelegate)) : Delegate.CreateDelegate(typeof(ExecuteDelegate), executeMethod);
			this.command = new XmlILCommand((ExecuteDelegate)@delegate, new XmlQueryStaticData(queryData, earlyBoundTypes));
			this.outputSettings = this.command.StaticData.DefaultWriterSettings;
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object and outputs the results to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the Microsoft .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x0600227A RID: 8826 RVA: 0x000D9BA7 File Offset: 0x000D7DA7
		public void Transform(IXPathNavigable input, XmlWriter results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			this.Transform(input, null, results, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object and outputs the results to an <see cref="T:System.Xml.XmlWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the Microsoft .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be <see langword="null" />.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x0600227B RID: 8827 RVA: 0x000D9BBE File Offset: 0x000D7DBE
		public void Transform(IXPathNavigable input, XsltArgumentList arguments, XmlWriter results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			this.Transform(input, arguments, results, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object and outputs the results to an <see cref="T:System.IO.TextWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the Microsoft .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be <see langword="null" />.</param>
		/// <param name="results">The <see cref="T:System.IO.TextWriter" /> to which you want to output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x0600227C RID: 8828 RVA: 0x000D9BD8 File Offset: 0x000D7DD8
		public void Transform(IXPathNavigable input, XsltArgumentList arguments, TextWriter results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			using (XmlWriter xmlWriter = XmlWriter.Create(results, this.OutputSettings))
			{
				this.Transform(input, arguments, xmlWriter, XsltConfigSection.CreateDefaultResolver());
				xmlWriter.Close();
			}
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object and outputs the results to a stream. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional runtime arguments.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the Microsoft .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be <see langword="null" />.</param>
		/// <param name="results">The stream to which you want to output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x0600227D RID: 8829 RVA: 0x000D9C2C File Offset: 0x000D7E2C
		public void Transform(IXPathNavigable input, XsltArgumentList arguments, Stream results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			using (XmlWriter xmlWriter = XmlWriter.Create(results, this.OutputSettings))
			{
				this.Transform(input, arguments, xmlWriter, XsltConfigSection.CreateDefaultResolver());
				xmlWriter.Close();
			}
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XmlReader" /> object and outputs the results to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="input">The <see cref="T:System.Xml.XmlReader" /> containing the input document.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x0600227E RID: 8830 RVA: 0x000D9C80 File Offset: 0x000D7E80
		public void Transform(XmlReader input, XmlWriter results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			this.Transform(input, null, results, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XmlReader" /> object and outputs the results to an <see cref="T:System.Xml.XmlWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XmlReader" /> containing the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be <see langword="null" />.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x0600227F RID: 8831 RVA: 0x000D9C97 File Offset: 0x000D7E97
		public void Transform(XmlReader input, XsltArgumentList arguments, XmlWriter results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			this.Transform(input, arguments, results, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XmlReader" /> object and outputs the results to a <see cref="T:System.IO.TextWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XmlReader" /> containing the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be <see langword="null" />.</param>
		/// <param name="results">The <see cref="T:System.IO.TextWriter" /> to which you want to output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x06002280 RID: 8832 RVA: 0x000D9CB0 File Offset: 0x000D7EB0
		public void Transform(XmlReader input, XsltArgumentList arguments, TextWriter results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			using (XmlWriter xmlWriter = XmlWriter.Create(results, this.OutputSettings))
			{
				this.Transform(input, arguments, xmlWriter, XsltConfigSection.CreateDefaultResolver());
				xmlWriter.Close();
			}
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XmlReader" /> object and outputs the results to a stream. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XmlReader" /> containing the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be <see langword="null" />.</param>
		/// <param name="results">The stream to which you want to output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x06002281 RID: 8833 RVA: 0x000D9D04 File Offset: 0x000D7F04
		public void Transform(XmlReader input, XsltArgumentList arguments, Stream results)
		{
			XslCompiledTransform.CheckArguments(input, results);
			using (XmlWriter xmlWriter = XmlWriter.Create(results, this.OutputSettings))
			{
				this.Transform(input, arguments, xmlWriter, XsltConfigSection.CreateDefaultResolver());
				xmlWriter.Close();
			}
		}

		/// <summary>Executes the transform using the input document specified by the URI and outputs the results to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="inputUri">The URI of the input document.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputUri" /> or <paramref name="results" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="inputUri" /> value includes a filename or directory cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="inputUri" /> value cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///         <paramref name="inputUri" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the input document.</exception>
		// Token: 0x06002282 RID: 8834 RVA: 0x000D9D58 File Offset: 0x000D7F58
		public void Transform(string inputUri, XmlWriter results)
		{
			XslCompiledTransform.CheckArguments(inputUri, results);
			using (XmlReader xmlReader = XmlReader.Create(inputUri, XslCompiledTransform.ReaderSettings))
			{
				this.Transform(xmlReader, null, results, XsltConfigSection.CreateDefaultResolver());
			}
		}

		/// <summary>Executes the transform using the input document specified by the URI and outputs the results to an <see cref="T:System.Xml.XmlWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="inputUri">The URI of the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be <see langword="null" />.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputUri" /> or <paramref name="results" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="inputtUri" /> value includes a filename or directory cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="inputUri" /> value cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///         <paramref name="inputUri" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the input document.</exception>
		// Token: 0x06002283 RID: 8835 RVA: 0x000D9DA4 File Offset: 0x000D7FA4
		public void Transform(string inputUri, XsltArgumentList arguments, XmlWriter results)
		{
			XslCompiledTransform.CheckArguments(inputUri, results);
			using (XmlReader xmlReader = XmlReader.Create(inputUri, XslCompiledTransform.ReaderSettings))
			{
				this.Transform(xmlReader, arguments, results, XsltConfigSection.CreateDefaultResolver());
			}
		}

		/// <summary>Executes the transform using the input document specified by the URI and outputs the results to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="inputUri">The URI of the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be <see langword="null" />.</param>
		/// <param name="results">The <see cref="T:System.IO.TextWriter" /> to which you want to output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputUri" /> or <paramref name="results" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="inputUri" /> value includes a filename or directory cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="inputUri" /> value cannot be resolved.-or-An error occurred while processing the request</exception>
		/// <exception cref="T:System.UriFormatException">
		///         <paramref name="inputUri" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the input document.</exception>
		// Token: 0x06002284 RID: 8836 RVA: 0x000D9DF0 File Offset: 0x000D7FF0
		public void Transform(string inputUri, XsltArgumentList arguments, TextWriter results)
		{
			XslCompiledTransform.CheckArguments(inputUri, results);
			using (XmlReader xmlReader = XmlReader.Create(inputUri, XslCompiledTransform.ReaderSettings))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(results, this.OutputSettings))
				{
					this.Transform(xmlReader, arguments, xmlWriter, XsltConfigSection.CreateDefaultResolver());
					xmlWriter.Close();
				}
			}
		}

		/// <summary>Executes the transform using the input document specified by the URI and outputs the results to stream. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="inputUri">The URI of the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be <see langword="null" />.</param>
		/// <param name="results">The stream to which you want to output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputUri" /> or <paramref name="results" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="inputUri" /> value includes a filename or directory cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="inputUri" /> value cannot be resolved.-or-An error occurred while processing the request</exception>
		/// <exception cref="T:System.UriFormatException">
		///         <paramref name="inputUri" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the input document.</exception>
		// Token: 0x06002285 RID: 8837 RVA: 0x000D9E64 File Offset: 0x000D8064
		public void Transform(string inputUri, XsltArgumentList arguments, Stream results)
		{
			XslCompiledTransform.CheckArguments(inputUri, results);
			using (XmlReader xmlReader = XmlReader.Create(inputUri, XslCompiledTransform.ReaderSettings))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(results, this.OutputSettings))
				{
					this.Transform(xmlReader, arguments, xmlWriter, XsltConfigSection.CreateDefaultResolver());
					xmlWriter.Close();
				}
			}
		}

		/// <summary>Executes the transform using the input document specified by the URI and outputs the results to a file.</summary>
		/// <param name="inputUri">The URI of the input document.</param>
		/// <param name="resultsFile">The URI of the output file.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputUri" /> or <paramref name="resultsFile" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The input document cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="inputUri" /> or <paramref name="resultsFile" /> value includes a filename or directory cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="inputUri" /> or <paramref name="resultsFile" /> value cannot be resolved.-or-An error occurred while processing the request</exception>
		/// <exception cref="T:System.UriFormatException">
		///         <paramref name="inputUri" /> or <paramref name="resultsFile" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the input document.</exception>
		// Token: 0x06002286 RID: 8838 RVA: 0x000D9ED8 File Offset: 0x000D80D8
		public void Transform(string inputUri, string resultsFile)
		{
			if (inputUri == null)
			{
				throw new ArgumentNullException("inputUri");
			}
			if (resultsFile == null)
			{
				throw new ArgumentNullException("resultsFile");
			}
			using (XmlReader xmlReader = XmlReader.Create(inputUri, XslCompiledTransform.ReaderSettings))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(resultsFile, this.OutputSettings))
				{
					this.Transform(xmlReader, null, xmlWriter, XsltConfigSection.CreateDefaultResolver());
					xmlWriter.Close();
				}
			}
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XmlReader" /> object and outputs the results to an <see cref="T:System.Xml.XmlWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments and the XmlResolver resolves the XSLT document() function.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XmlReader" /> containing the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be <see langword="null" />.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <param name="documentResolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is <see langword="null" />, the document() function is not resolved.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x06002287 RID: 8839 RVA: 0x000D9F60 File Offset: 0x000D8160
		public void Transform(XmlReader input, XsltArgumentList arguments, XmlWriter results, XmlResolver documentResolver)
		{
			XslCompiledTransform.CheckArguments(input, results);
			this.CheckCommand();
			this.command.Execute(input, documentResolver, arguments, results);
		}

		/// <summary>Executes the transform by using the input document that is specified by the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object and outputs the results to an <see cref="T:System.Xml.XmlWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments and the <see cref="T:System.Xml.XmlResolver" /> resolves the XSLT <see langword="document()" /> function.</summary>
		/// <param name="input">The document to transform that is specified by the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object.</param>
		/// <param name="arguments">Argument list as <see cref="T:System.Xml.Xsl.XsltArgumentList" />.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an <see langword="xsl:output" /> element, you should create the <see cref="T:System.Xml.XmlWriter" /> by using the <see cref="T:System.Xml.XmlWriterSettings" /> object that is returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <param name="documentResolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT <see langword="document()" /> function. If this is <see langword="null" />, the <see langword="document()" /> function is not resolved.</param>
		// Token: 0x06002288 RID: 8840 RVA: 0x000D9F7F File Offset: 0x000D817F
		public void Transform(IXPathNavigable input, XsltArgumentList arguments, XmlWriter results, XmlResolver documentResolver)
		{
			XslCompiledTransform.CheckArguments(input, results);
			this.CheckCommand();
			this.command.Execute(input.CreateNavigator(), documentResolver, arguments, results);
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x000D9FA3 File Offset: 0x000D81A3
		private static void CheckArguments(object input, object results)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (results == null)
			{
				throw new ArgumentNullException("results");
			}
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x000D9FC1 File Offset: 0x000D81C1
		private static void CheckArguments(string inputUri, object results)
		{
			if (inputUri == null)
			{
				throw new ArgumentNullException("inputUri");
			}
			if (results == null)
			{
				throw new ArgumentNullException("results");
			}
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x000D9FDF File Offset: 0x000D81DF
		private void CheckCommand()
		{
			if (this.command == null)
			{
				throw new InvalidOperationException(Res.GetString("No stylesheet was loaded."));
			}
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x000D9FF9 File Offset: 0x000D81F9
		private QilExpression TestCompile(object stylesheet, XsltSettings settings, XmlResolver stylesheetResolver)
		{
			this.Reset();
			this.CompileXsltToQil(stylesheet, settings, stylesheetResolver);
			return this.qil;
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000DA010 File Offset: 0x000D8210
		private void TestGenerate(XsltSettings settings)
		{
			this.CompileQilToMsil(settings);
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x000DA019 File Offset: 0x000D8219
		private void Transform(string inputUri, XsltArgumentList arguments, XmlWriter results, XmlResolver documentResolver)
		{
			this.command.Execute(inputUri, documentResolver, arguments, results);
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x000DA02C File Offset: 0x000D822C
		internal static void PrintQil(object qil, XmlWriter xw, bool printComments, bool printTypes, bool printLineInfo)
		{
			QilExpression node = (QilExpression)qil;
			QilXmlWriter.Options options = QilXmlWriter.Options.None;
			if (printComments)
			{
				options |= QilXmlWriter.Options.Annotations;
			}
			if (printTypes)
			{
				options |= QilXmlWriter.Options.TypeInfo;
			}
			if (printLineInfo)
			{
				options |= QilXmlWriter.Options.LineInfo;
			}
			new QilXmlWriter(xw, options).ToXml(node);
			xw.Flush();
		}

		// Token: 0x04001C3C RID: 7228
		private static readonly XmlReaderSettings ReaderSettings;

		// Token: 0x04001C3D RID: 7229
		private static readonly PermissionSet MemberAccessPermissionSet = new PermissionSet(PermissionState.None);

		// Token: 0x04001C3E RID: 7230
		private const string Version = "4.0.0.0";

		// Token: 0x04001C3F RID: 7231
		private bool enableDebug;

		// Token: 0x04001C40 RID: 7232
		private CompilerResults compilerResults;

		// Token: 0x04001C41 RID: 7233
		private XmlWriterSettings outputSettings;

		// Token: 0x04001C42 RID: 7234
		private QilExpression qil;

		// Token: 0x04001C43 RID: 7235
		private XmlILCommand command;

		// Token: 0x04001C44 RID: 7236
		private static volatile ConstructorInfo GeneratedCodeCtor;
	}
}
