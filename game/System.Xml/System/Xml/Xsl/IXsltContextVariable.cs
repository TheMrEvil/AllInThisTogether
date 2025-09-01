using System;
using System.Xml.XPath;

namespace System.Xml.Xsl
{
	/// <summary>Provides an interface to a given variable that is defined in the style sheet during runtime execution.</summary>
	// Token: 0x0200034A RID: 842
	public interface IXsltContextVariable
	{
		/// <summary>Gets a value indicating whether the variable is local.</summary>
		/// <returns>
		///     <see langword="true" /> if the variable is a local variable in the current context; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060022CE RID: 8910
		bool IsLocal { get; }

		/// <summary>Gets a value indicating whether the variable is an Extensible Stylesheet Language Transformations (XSLT) parameter. This can be a parameter to a style sheet or a template.</summary>
		/// <returns>
		///     <see langword="true" /> if the variable is an XSLT parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060022CF RID: 8911
		bool IsParam { get; }

		/// <summary>Gets the <see cref="T:System.Xml.XPath.XPathResultType" /> representing the XML Path Language (XPath) type of the variable.</summary>
		/// <returns>The <see cref="T:System.Xml.XPath.XPathResultType" /> representing the XPath type of the variable.</returns>
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060022D0 RID: 8912
		XPathResultType VariableType { get; }

		/// <summary>Evaluates the variable at runtime and returns an object that represents the value of the variable.</summary>
		/// <param name="xsltContext">An <see cref="T:System.Xml.Xsl.XsltContext" /> representing the execution context of the variable. </param>
		/// <returns>An <see cref="T:System.Object" /> representing the value of the variable. Possible return types include number, string, Boolean, document fragment, or node set.</returns>
		// Token: 0x060022D1 RID: 8913
		object Evaluate(XsltContext xsltContext);
	}
}
