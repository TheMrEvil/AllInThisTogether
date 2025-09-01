using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;
using System.Xml.Xsl.XsltOld;
using System.Xml.Xsl.XsltOld.Debugger;

namespace System.Xml.Xsl
{
	/// <summary>Transforms XML data using an Extensible Stylesheet Language for Transformations (XSLT) style sheet.</summary>
	// Token: 0x02000344 RID: 836
	[Obsolete("This class has been deprecated. Please use System.Xml.Xsl.XslCompiledTransform instead. http://go.microsoft.com/fwlink/?linkid=14202")]
	public sealed class XslTransform
	{
		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06002290 RID: 8848 RVA: 0x000DA06B File Offset: 0x000D826B
		private XmlResolver _DocumentResolver
		{
			get
			{
				if (this.isDocumentResolverSet)
				{
					return this._documentResolver;
				}
				return XsltConfigSection.CreateDefaultResolver();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XslTransform" /> class.</summary>
		// Token: 0x06002291 RID: 8849 RVA: 0x0000216B File Offset: 0x0000036B
		public XslTransform()
		{
		}

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> used to resolve external resources when the <see cref="Overload:System.Xml.Xsl.XslTransform.Transform" /> method is called.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlResolver" /> to use during transformation. If set to <see langword="null" />, the XSLT document() function is not resolved.</returns>
		// Token: 0x170006DF RID: 1759
		// (set) Token: 0x06002292 RID: 8850 RVA: 0x000DA081 File Offset: 0x000D8281
		public XmlResolver XmlResolver
		{
			set
			{
				this._documentResolver = value;
				this.isDocumentResolverSet = true;
			}
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XmlReader" /> object that contains the XSLT style sheet. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The current node does not conform to a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded scripts, and the caller does not have <see langword="UnmanagedCode" /> permission. </exception>
		// Token: 0x06002293 RID: 8851 RVA: 0x000DA091 File Offset: 0x000D8291
		public void Load(XmlReader stylesheet)
		{
			this.Load(stylesheet, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XmlReader" /> object that contains the XSLT style sheet. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to load any style sheets referenced in xsl:import and xsl:include elements. If this is <see langword="null" />, external resources are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Load(System.Xml.XmlReader,System.Xml.XmlResolver)" />  method completes. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The current node does not conform to a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded scripts, and the caller does not have <see langword="UnmanagedCode" /> permission. </exception>
		// Token: 0x06002294 RID: 8852 RVA: 0x000DA09F File Offset: 0x000D829F
		public void Load(XmlReader stylesheet, XmlResolver resolver)
		{
			this.Load(new XPathDocument(stylesheet, XmlSpace.Preserve), resolver);
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.IXPathNavigable" />.</summary>
		/// <param name="stylesheet">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the XSLT style sheet. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The loaded resource is not a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded scripts, and the caller does not have <see langword="UnmanagedCode" /> permission. </exception>
		// Token: 0x06002295 RID: 8853 RVA: 0x000DA0AF File Offset: 0x000D82AF
		public void Load(IXPathNavigable stylesheet)
		{
			this.Load(stylesheet, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.IXPathNavigable" />.</summary>
		/// <param name="stylesheet">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the XSLT style sheet. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to load any style sheets referenced in xsl:import and xsl:include elements. If this is <see langword="null" />, external resources are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Load" /> method completes. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The loaded resource is not a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded scripts, and the caller does not have <see langword="UnmanagedCode" /> permission. </exception>
		// Token: 0x06002296 RID: 8854 RVA: 0x000DA0BD File Offset: 0x000D82BD
		public void Load(IXPathNavigable stylesheet, XmlResolver resolver)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			this.Load(stylesheet.CreateNavigator(), resolver);
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object that contains the XSLT style sheet. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The current node does not conform to a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded scripts, and the caller does not have <see langword="UnmanagedCode" /> permission. </exception>
		// Token: 0x06002297 RID: 8855 RVA: 0x000DA0DA File Offset: 0x000D82DA
		public void Load(XPathNavigator stylesheet)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			this.Load(stylesheet, XsltConfigSection.CreateDefaultResolver());
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object that contains the XSLT style sheet. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to load any style sheets referenced in xsl:import and xsl:include elements. If this is <see langword="null" />, external resources are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Load" /> method completes. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The current node does not conform to a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded scripts, and the caller does not have <see langword="UnmanagedCode" /> permission. </exception>
		// Token: 0x06002298 RID: 8856 RVA: 0x000DA0F6 File Offset: 0x000D82F6
		public void Load(XPathNavigator stylesheet, XmlResolver resolver)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			this.Compile(stylesheet, resolver, null);
		}

		/// <summary>Loads the XSLT style sheet specified by a URL.</summary>
		/// <param name="url">The URL that specifies the XSLT style sheet to load. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The loaded resource is not a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded script, and the caller does not have <see langword="UnmanagedCode" /> permission. </exception>
		// Token: 0x06002299 RID: 8857 RVA: 0x000DA110 File Offset: 0x000D8310
		public void Load(string url)
		{
			XmlTextReaderImpl xmlTextReaderImpl = new XmlTextReaderImpl(url);
			Evidence evidence = XmlSecureResolver.CreateEvidenceForUrl(xmlTextReaderImpl.BaseURI);
			this.Compile(Compiler.LoadDocument(xmlTextReaderImpl).CreateNavigator(), XsltConfigSection.CreateDefaultResolver(), evidence);
		}

		/// <summary>Loads the XSLT style sheet specified by a URL.</summary>
		/// <param name="url">The URL that specifies the XSLT style sheet to load. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> to use to load the style sheet and any style sheet(s) referenced in xsl:import and xsl:include elements.If this is <see langword="null" />, a default <see cref="T:System.Xml.XmlUrlResolver" /> with no user credentials is used to open the style sheet. The default <see cref="T:System.Xml.XmlUrlResolver" /> is not used to resolve any external resources in the style sheet, so xsl:import and xsl:include elements are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Load(System.String,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The loaded resource is not a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded script, and the caller does not have <see langword="UnmanagedCode" /> permission. </exception>
		// Token: 0x0600229A RID: 8858 RVA: 0x000DA148 File Offset: 0x000D8348
		public void Load(string url, XmlResolver resolver)
		{
			XmlTextReaderImpl xmlTextReaderImpl = new XmlTextReaderImpl(url);
			xmlTextReaderImpl.XmlResolver = resolver;
			Evidence evidence = XmlSecureResolver.CreateEvidenceForUrl(xmlTextReaderImpl.BaseURI);
			this.Compile(Compiler.LoadDocument(xmlTextReaderImpl).CreateNavigator(), resolver, evidence);
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.IXPathNavigable" />. This method allows you to limit the permissions of the style sheet by specifying evidence.</summary>
		/// <param name="stylesheet">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the XSLT style sheet. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to load any style sheets referenced in xsl:import and xsl:include elements. If this is <see langword="null" />, external resources are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Load" /> method completes. </param>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> set on the assembly generated for the script block in the XSLT style sheet.If this is <see langword="null" />, script blocks are not processed, the XSLT document() function is not supported, and privileged extension objects are disallowed.The caller must have <see langword="ControlEvidence" /> permission in order to supply evidence for the script assembly. Semi-trusted callers can set this parameter to <see langword="null" />. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The loaded resource is not a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The referenced style sheet requires functionality that is not allowed by the evidence provided.The caller tries to supply evidence and does not have <see langword="ControlEvidence" /> permission. </exception>
		// Token: 0x0600229B RID: 8859 RVA: 0x000DA182 File Offset: 0x000D8382
		public void Load(IXPathNavigable stylesheet, XmlResolver resolver, Evidence evidence)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			this.Load(stylesheet.CreateNavigator(), resolver, evidence);
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XmlReader" />. This method allows you to limit the permissions of the style sheet by specifying evidence.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XmlReader" /> object containing the style sheet to load. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to load any style sheets referenced in xsl:import and xsl:include elements. If this is <see langword="null" />, external resources are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Load" /> method completes. </param>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> set on the assembly generated for the script block in the XSLT style sheet.If this is <see langword="null" />, script blocks are not processed, the XSLT document() function is not supported, and privileged extension objects are disallowed.The caller must have <see langword="ControlEvidence" /> permission in order to supply evidence for the script assembly. Semi-trusted callers can set this parameter to <see langword="null" />. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The current node does not conform to a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The referenced style sheet requires functionality that is not allowed by the evidence provided.The caller tries to supply evidence and does not have <see langword="ControlEvidence" /> permission. </exception>
		// Token: 0x0600229C RID: 8860 RVA: 0x000DA1A0 File Offset: 0x000D83A0
		public void Load(XmlReader stylesheet, XmlResolver resolver, Evidence evidence)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			this.Load(new XPathDocument(stylesheet, XmlSpace.Preserve), resolver, evidence);
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.XPathNavigator" />. This method allows you to limit the permissions of the style sheet by specifying evidence.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object containing the style sheet to load. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to load any style sheets referenced in xsl:import and xsl:include elements. If this is <see langword="null" />, external resources are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Load" /> method completes. </param>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> set on the assembly generated for the script block in the XSLT style sheet.If this is <see langword="null" />, script blocks are not processed, the XSLT document() function is not supported, and privileged extension objects are disallowed.The caller must have <see langword="ControlEvidence" /> permission in order to supply evidence for the script assembly. Semi-trusted callers can set this parameter to <see langword="null" />. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The current node does not conform to a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The referenced style sheet requires functionality that is not allowed by the evidence provided.The caller tries to supply evidence and does not have <see langword="ControlEvidence" /> permission. </exception>
		// Token: 0x0600229D RID: 8861 RVA: 0x000DA1BF File Offset: 0x000D83BF
		public void Load(XPathNavigator stylesheet, XmlResolver resolver, Evidence evidence)
		{
			if (stylesheet == null)
			{
				throw new ArgumentNullException("stylesheet");
			}
			this.Compile(stylesheet, resolver, evidence);
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x000DA1D8 File Offset: 0x000D83D8
		private void CheckCommand()
		{
			if (this._CompiledStylesheet == null)
			{
				throw new InvalidOperationException(Res.GetString("No stylesheet was loaded."));
			}
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <paramref name="args" /> and outputs the result to an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is <see langword="null" />, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.XPathNavigator,System.Xml.Xsl.XsltArgumentList,System.Xml.XmlResolver)" /> method completes. </param>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> containing the results of the transformation.</returns>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x0600229F RID: 8863 RVA: 0x000DA1F2 File Offset: 0x000D83F2
		public XmlReader Transform(XPathNavigator input, XsltArgumentList args, XmlResolver resolver)
		{
			this.CheckCommand();
			return new Processor(input, args, resolver, this._CompiledStylesheet, this._QueryStore, this._RootAction, this.debugger).StartReader();
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <paramref name="args" /> and outputs the result to an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> containing the results of the transformation.</returns>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060022A0 RID: 8864 RVA: 0x000DA21F File Offset: 0x000D841F
		public XmlReader Transform(XPathNavigator input, XsltArgumentList args)
		{
			return this.Transform(input, args, this._DocumentResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified args and outputs the result to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is <see langword="null" />, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.XPathNavigator,System.Xml.Xsl.XsltArgumentList,System.Xml.XmlWriter,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060022A1 RID: 8865 RVA: 0x000DA22F File Offset: 0x000D842F
		public void Transform(XPathNavigator input, XsltArgumentList args, XmlWriter output, XmlResolver resolver)
		{
			this.CheckCommand();
			new Processor(input, args, resolver, this._CompiledStylesheet, this._QueryStore, this._RootAction, this.debugger).Execute(output);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified args and outputs the result to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060022A2 RID: 8866 RVA: 0x000DA25E File Offset: 0x000D845E
		public void Transform(XPathNavigator input, XsltArgumentList args, XmlWriter output)
		{
			this.Transform(input, args, output, this._DocumentResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The stream to which you want to output. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is <see langword="null" />, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.XPathNavigator,System.Xml.Xsl.XsltArgumentList,System.IO.Stream,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060022A3 RID: 8867 RVA: 0x000DA26F File Offset: 0x000D846F
		public void Transform(XPathNavigator input, XsltArgumentList args, Stream output, XmlResolver resolver)
		{
			this.CheckCommand();
			new Processor(input, args, resolver, this._CompiledStylesheet, this._QueryStore, this._RootAction, this.debugger).Execute(output);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The stream to which you want to output. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060022A4 RID: 8868 RVA: 0x000DA29E File Offset: 0x000D849E
		public void Transform(XPathNavigator input, XsltArgumentList args, Stream output)
		{
			this.Transform(input, args, output, this._DocumentResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to output. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is <see langword="null" />, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.XPathNavigator,System.Xml.Xsl.XsltArgumentList,System.IO.TextWriter,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060022A5 RID: 8869 RVA: 0x000DA2AF File Offset: 0x000D84AF
		public void Transform(XPathNavigator input, XsltArgumentList args, TextWriter output, XmlResolver resolver)
		{
			this.CheckCommand();
			new Processor(input, args, resolver, this._CompiledStylesheet, this._QueryStore, this._RootAction, this.debugger).Execute(output);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to output. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060022A6 RID: 8870 RVA: 0x000DA2DE File Offset: 0x000D84DE
		public void Transform(XPathNavigator input, XsltArgumentList args, TextWriter output)
		{
			this.CheckCommand();
			new Processor(input, args, this._DocumentResolver, this._CompiledStylesheet, this._QueryStore, this._RootAction, this.debugger).Execute(output);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is <see langword="null" />, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.IXPathNavigable,System.Xml.Xsl.XsltArgumentList,System.Xml.XmlResolver)" /> method completes. </param>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> containing the results of the transformation.</returns>
		// Token: 0x060022A7 RID: 8871 RVA: 0x000DA311 File Offset: 0x000D8511
		public XmlReader Transform(IXPathNavigable input, XsltArgumentList args, XmlResolver resolver)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Transform(input.CreateNavigator(), args, resolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> containing the results of the transformation.</returns>
		// Token: 0x060022A8 RID: 8872 RVA: 0x000DA32F File Offset: 0x000D852F
		public XmlReader Transform(IXPathNavigable input, XsltArgumentList args)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Transform(input.CreateNavigator(), args, this._DocumentResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to output. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is <see langword="null" />, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.IXPathNavigable,System.Xml.Xsl.XsltArgumentList,System.IO.TextWriter,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060022A9 RID: 8873 RVA: 0x000DA352 File Offset: 0x000D8552
		public void Transform(IXPathNavigable input, XsltArgumentList args, TextWriter output, XmlResolver resolver)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.Transform(input.CreateNavigator(), args, output, resolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to output. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060022AA RID: 8874 RVA: 0x000DA372 File Offset: 0x000D8572
		public void Transform(IXPathNavigable input, XsltArgumentList args, TextWriter output)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.Transform(input.CreateNavigator(), args, output, this._DocumentResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The stream to which you want to output. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is <see langword="null" />, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Transform" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060022AB RID: 8875 RVA: 0x000DA396 File Offset: 0x000D8596
		public void Transform(IXPathNavigable input, XsltArgumentList args, Stream output, XmlResolver resolver)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.Transform(input.CreateNavigator(), args, output, resolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The stream to which you want to output. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation.Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060022AC RID: 8876 RVA: 0x000DA3B6 File Offset: 0x000D85B6
		public void Transform(IXPathNavigable input, XsltArgumentList args, Stream output)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.Transform(input.CreateNavigator(), args, output, this._DocumentResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is <see langword="null" />, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.IXPathNavigable,System.Xml.Xsl.XsltArgumentList,System.Xml.XmlWriter,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060022AD RID: 8877 RVA: 0x000DA3DA File Offset: 0x000D85DA
		public void Transform(IXPathNavigable input, XsltArgumentList args, XmlWriter output, XmlResolver resolver)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.Transform(input.CreateNavigator(), args, output, resolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060022AE RID: 8878 RVA: 0x000DA3FA File Offset: 0x000D85FA
		public void Transform(IXPathNavigable input, XsltArgumentList args, XmlWriter output)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.Transform(input.CreateNavigator(), args, output, this._DocumentResolver);
		}

		/// <summary>Transforms the XML data in the input file and outputs the result to an output file.</summary>
		/// <param name="inputfile">The URL of the source document to be transformed. </param>
		/// <param name="outputfile">The URL of the output file. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is <see langword="null" />, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Transform" /> method completes. </param>
		// Token: 0x060022AF RID: 8879 RVA: 0x000DA420 File Offset: 0x000D8620
		public void Transform(string inputfile, string outputfile, XmlResolver resolver)
		{
			FileStream fileStream = null;
			try
			{
				XPathDocument input = new XPathDocument(inputfile);
				fileStream = new FileStream(outputfile, FileMode.Create, FileAccess.ReadWrite);
				this.Transform(input, null, fileStream, resolver);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		/// <summary>Transforms the XML data in the input file and outputs the result to an output file.</summary>
		/// <param name="inputfile">The URL of the source document to be transformed. </param>
		/// <param name="outputfile">The URL of the output file. </param>
		// Token: 0x060022B0 RID: 8880 RVA: 0x000DA468 File Offset: 0x000D8668
		public void Transform(string inputfile, string outputfile)
		{
			this.Transform(inputfile, outputfile, this._DocumentResolver);
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x000DA478 File Offset: 0x000D8678
		private void Compile(XPathNavigator stylesheet, XmlResolver resolver, Evidence evidence)
		{
			Compiler compiler = (this.Debugger == null) ? new Compiler() : new DbgCompiler(this.Debugger);
			NavigatorInput input = new NavigatorInput(stylesheet);
			compiler.Compile(input, resolver ?? XmlNullResolver.Singleton, evidence);
			this._CompiledStylesheet = compiler.CompiledStylesheet;
			this._QueryStore = compiler.QueryStore;
			this._RootAction = compiler.RootAction;
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x000DA4DD File Offset: 0x000D86DD
		internal IXsltDebugger Debugger
		{
			get
			{
				return this.debugger;
			}
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000DA4E5 File Offset: 0x000D86E5
		internal XslTransform(object debugger)
		{
			if (debugger != null)
			{
				this.debugger = new XslTransform.DebuggerAddapter(debugger);
			}
		}

		// Token: 0x04001C45 RID: 7237
		private XmlResolver _documentResolver;

		// Token: 0x04001C46 RID: 7238
		private bool isDocumentResolverSet;

		// Token: 0x04001C47 RID: 7239
		private Stylesheet _CompiledStylesheet;

		// Token: 0x04001C48 RID: 7240
		private List<TheQuery> _QueryStore;

		// Token: 0x04001C49 RID: 7241
		private RootAction _RootAction;

		// Token: 0x04001C4A RID: 7242
		private IXsltDebugger debugger;

		// Token: 0x02000345 RID: 837
		private class DebuggerAddapter : IXsltDebugger
		{
			// Token: 0x060022B4 RID: 8884 RVA: 0x000DA4FC File Offset: 0x000D86FC
			public DebuggerAddapter(object unknownDebugger)
			{
				this.unknownDebugger = unknownDebugger;
				BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
				Type type = unknownDebugger.GetType();
				this.getBltIn = type.GetMethod("GetBuiltInTemplatesUri", bindingAttr);
				this.onCompile = type.GetMethod("OnInstructionCompile", bindingAttr);
				this.onExecute = type.GetMethod("OnInstructionExecute", bindingAttr);
			}

			// Token: 0x060022B5 RID: 8885 RVA: 0x000DA556 File Offset: 0x000D8756
			public string GetBuiltInTemplatesUri()
			{
				if (this.getBltIn == null)
				{
					return null;
				}
				return (string)this.getBltIn.Invoke(this.unknownDebugger, new object[0]);
			}

			// Token: 0x060022B6 RID: 8886 RVA: 0x000DA584 File Offset: 0x000D8784
			public void OnInstructionCompile(XPathNavigator styleSheetNavigator)
			{
				if (this.onCompile != null)
				{
					this.onCompile.Invoke(this.unknownDebugger, new object[]
					{
						styleSheetNavigator
					});
				}
			}

			// Token: 0x060022B7 RID: 8887 RVA: 0x000DA5B0 File Offset: 0x000D87B0
			public void OnInstructionExecute(IXsltProcessor xsltProcessor)
			{
				if (this.onExecute != null)
				{
					this.onExecute.Invoke(this.unknownDebugger, new object[]
					{
						xsltProcessor
					});
				}
			}

			// Token: 0x04001C4B RID: 7243
			private object unknownDebugger;

			// Token: 0x04001C4C RID: 7244
			private MethodInfo getBltIn;

			// Token: 0x04001C4D RID: 7245
			private MethodInfo onCompile;

			// Token: 0x04001C4E RID: 7246
			private MethodInfo onExecute;
		}
	}
}
