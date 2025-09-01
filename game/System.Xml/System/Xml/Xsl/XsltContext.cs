using System;
using System.Xml.XPath;

namespace System.Xml.Xsl
{
	/// <summary>Encapsulates the current execution context of the Extensible Stylesheet Language for Transformations (XSLT) processor allowing XML Path Language (XPath) to resolve functions, parameters, and namespaces within XPath expressions.</summary>
	// Token: 0x0200034B RID: 843
	public abstract class XsltContext : XmlNamespaceManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltContext" /> class with the specified <see cref="T:System.Xml.NameTable" />.</summary>
		/// <param name="table">The <see cref="T:System.Xml.NameTable" /> to use. </param>
		// Token: 0x060022D2 RID: 8914 RVA: 0x000DA738 File Offset: 0x000D8938
		protected XsltContext(NameTable table) : base(table)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltContext" /> class.</summary>
		// Token: 0x060022D3 RID: 8915 RVA: 0x000DA741 File Offset: 0x000D8941
		protected XsltContext() : base(new NameTable())
		{
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x00035911 File Offset: 0x00033B11
		internal XsltContext(bool dummy)
		{
		}

		/// <summary>When overridden in a derived class, resolves a variable reference and returns an <see cref="T:System.Xml.Xsl.IXsltContextVariable" /> representing the variable.</summary>
		/// <param name="prefix">The prefix of the variable as it appears in the XPath expression. </param>
		/// <param name="name">The name of the variable. </param>
		/// <returns>An <see cref="T:System.Xml.Xsl.IXsltContextVariable" /> representing the variable at runtime.</returns>
		// Token: 0x060022D5 RID: 8917
		public abstract IXsltContextVariable ResolveVariable(string prefix, string name);

		/// <summary>When overridden in a derived class, resolves a function reference and returns an <see cref="T:System.Xml.Xsl.IXsltContextFunction" /> representing the function. The <see cref="T:System.Xml.Xsl.IXsltContextFunction" /> is used at execution time to get the return value of the function.</summary>
		/// <param name="prefix">The prefix of the function as it appears in the XPath expression. </param>
		/// <param name="name">The name of the function. </param>
		/// <param name="ArgTypes">An array of argument types for the function being resolved. This allows you to select between methods with the same name (for example, overloaded methods). </param>
		/// <returns>An <see cref="T:System.Xml.Xsl.IXsltContextFunction" /> representing the function.</returns>
		// Token: 0x060022D6 RID: 8918
		public abstract IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] ArgTypes);

		/// <summary>When overridden in a derived class, gets a value indicating whether to include white space nodes in the output.</summary>
		/// <returns>
		///     <see langword="true" /> to check white space nodes in the source document for inclusion in the output; <see langword="false" /> to not evaluate white space nodes. The default is <see langword="true" />.</returns>
		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060022D7 RID: 8919
		public abstract bool Whitespace { get; }

		/// <summary>When overridden in a derived class, evaluates whether to preserve white space nodes or strip them for the given context.</summary>
		/// <param name="node">The white space node that is to be preserved or stripped in the current context. </param>
		/// <returns>Returns <see langword="true" /> if the white space is to be preserved or <see langword="false" /> if the white space is to be stripped.</returns>
		// Token: 0x060022D8 RID: 8920
		public abstract bool PreserveWhitespace(XPathNavigator node);

		/// <summary>When overridden in a derived class, compares the base Uniform Resource Identifiers (URIs) of two documents based upon the order the documents were loaded by the XSLT processor (that is, the <see cref="T:System.Xml.Xsl.XslTransform" /> class).</summary>
		/// <param name="baseUri">The base URI of the first document to compare. </param>
		/// <param name="nextbaseUri">The base URI of the second document to compare. </param>
		/// <returns>An integer value describing the relative order of the two base URIs: <see langword="-" />1 if <paramref name="baseUri" /> occurs before <paramref name="nextbaseUri" />; 0 if the two base URIs are identical; and 1 if <paramref name="baseUri" /> occurs after <paramref name="nextbaseUri" />.</returns>
		// Token: 0x060022D9 RID: 8921
		public abstract int CompareDocument(string baseUri, string nextbaseUri);
	}
}
