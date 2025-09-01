using System;
using System.Collections;
using MS.Internal.Xml.XPath;

namespace System.Xml.XPath
{
	/// <summary>Provides a typed class that represents a compiled XPath expression.</summary>
	// Token: 0x02000256 RID: 598
	public abstract class XPathExpression
	{
		// Token: 0x060015ED RID: 5613 RVA: 0x0000216B File Offset: 0x0000036B
		internal XPathExpression()
		{
		}

		/// <summary>When overridden in a derived class, gets a <see langword="string" /> representation of the <see cref="T:System.Xml.XPath.XPathExpression" />.</summary>
		/// <returns>A <see langword="string" /> representation of the <see cref="T:System.Xml.XPath.XPathExpression" />.</returns>
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060015EE RID: 5614
		public abstract string Expression { get; }

		/// <summary>When overridden in a derived class, sorts the nodes selected by the XPath expression according to the specified <see cref="T:System.Collections.IComparer" /> object.</summary>
		/// <param name="expr">An object representing the sort key. This can be the <see langword="string" /> value of the node or an <see cref="T:System.Xml.XPath.XPathExpression" /> object with a compiled XPath expression.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.IComparer" /> object that provides the specific data type comparisons for comparing two objects for equivalence. </param>
		/// <exception cref="T:System.Xml.XPath.XPathException">The <see cref="T:System.Xml.XPath.XPathExpression" /> or sort key includes a prefix and either an <see cref="T:System.Xml.XmlNamespaceManager" /> is not provided, or the prefix cannot be found in the supplied <see cref="T:System.Xml.XmlNamespaceManager" />.</exception>
		// Token: 0x060015EF RID: 5615
		public abstract void AddSort(object expr, IComparer comparer);

		/// <summary>When overridden in a derived class, sorts the nodes selected by the XPath expression according to the supplied parameters.</summary>
		/// <param name="expr">An object representing the sort key. This can be the <see langword="string" /> value of the node or an <see cref="T:System.Xml.XPath.XPathExpression" /> object with a compiled XPath expression. </param>
		/// <param name="order">An <see cref="T:System.Xml.XPath.XmlSortOrder" /> value indicating the sort order. </param>
		/// <param name="caseOrder">An <see cref="T:System.Xml.XPath.XmlCaseOrder" /> value indicating how to sort uppercase and lowercase letters.</param>
		/// <param name="lang">The language to use for comparison. Uses the <see cref="T:System.Globalization.CultureInfo" /> class that can be passed to the <see cref="Overload:System.String.Compare" /> method for the language types, for example, "us-en" for U.S. English. If an empty string is specified, the system environment is used to determine the <see cref="T:System.Globalization.CultureInfo" />. </param>
		/// <param name="dataType">An <see cref="T:System.Xml.XPath.XmlDataType" /> value indicating the sort order for the data type. </param>
		/// <exception cref="T:System.Xml.XPath.XPathException">The <see cref="T:System.Xml.XPath.XPathExpression" /> or sort key includes a prefix and either an <see cref="T:System.Xml.XmlNamespaceManager" /> is not provided, or the prefix cannot be found in the supplied <see cref="T:System.Xml.XmlNamespaceManager" />. </exception>
		// Token: 0x060015F0 RID: 5616
		public abstract void AddSort(object expr, XmlSortOrder order, XmlCaseOrder caseOrder, string lang, XmlDataType dataType);

		/// <summary>When overridden in a derived class, returns a clone of this <see cref="T:System.Xml.XPath.XPathExpression" />.</summary>
		/// <returns>A new <see cref="T:System.Xml.XPath.XPathExpression" /> object.</returns>
		// Token: 0x060015F1 RID: 5617
		public abstract XPathExpression Clone();

		/// <summary>When overridden in a derived class, specifies the <see cref="T:System.Xml.XmlNamespaceManager" /> object to use for namespace resolution.</summary>
		/// <param name="nsManager">An <see cref="T:System.Xml.XmlNamespaceManager" /> object to use for namespace resolution. </param>
		/// <exception cref="T:System.Xml.XPath.XPathException">The <see cref="T:System.Xml.XmlNamespaceManager" /> object parameter is not derived from the <see cref="T:System.Xml.XmlNamespaceManager" /> class. </exception>
		// Token: 0x060015F2 RID: 5618
		public abstract void SetContext(XmlNamespaceManager nsManager);

		/// <summary>When overridden in a derived class, specifies the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object to use for namespace resolution.</summary>
		/// <param name="nsResolver">An object that implements the <see cref="T:System.Xml.IXmlNamespaceResolver" /> interface to use for namespace resolution.</param>
		/// <exception cref="T:System.Xml.XPath.XPathException">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object parameter is not derived from <see cref="T:System.Xml.IXmlNamespaceResolver" />. </exception>
		// Token: 0x060015F3 RID: 5619
		public abstract void SetContext(IXmlNamespaceResolver nsResolver);

		/// <summary>When overridden in a derived class, gets the result type of the XPath expression.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathResultType" /> value representing the result type of the XPath expression.</returns>
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060015F4 RID: 5620
		public abstract XPathResultType ReturnType { get; }

		/// <summary>Compiles the XPath expression specified and returns an <see cref="T:System.Xml.XPath.XPathExpression" /> object representing the XPath expression.</summary>
		/// <param name="xpath">An XPath expression.</param>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathExpression" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The XPath expression parameter is not a valid XPath expression.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x060015F5 RID: 5621 RVA: 0x000855D3 File Offset: 0x000837D3
		public static XPathExpression Compile(string xpath)
		{
			return XPathExpression.Compile(xpath, null);
		}

		/// <summary>Compiles the specified XPath expression, with the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified for namespace resolution, and returns an <see cref="T:System.Xml.XPath.XPathExpression" /> object that represents the XPath expression.</summary>
		/// <param name="xpath">An XPath expression.</param>
		/// <param name="nsResolver">An object that implements the <see cref="T:System.Xml.IXmlNamespaceResolver" /> interface for namespace resolution.</param>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathExpression" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The XPath expression parameter is not a valid XPath expression.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x060015F6 RID: 5622 RVA: 0x000855DC File Offset: 0x000837DC
		public static XPathExpression Compile(string xpath, IXmlNamespaceResolver nsResolver)
		{
			bool needContext;
			CompiledXpathExpr compiledXpathExpr = new CompiledXpathExpr(new QueryBuilder().Build(xpath, out needContext), xpath, needContext);
			if (nsResolver != null)
			{
				compiledXpathExpr.SetContext(nsResolver);
			}
			return compiledXpathExpr;
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00085609 File Offset: 0x00083809
		private void PrintQuery(XmlWriter w)
		{
			((CompiledXpathExpr)this).QueryTree.PrintQuery(w);
		}
	}
}
