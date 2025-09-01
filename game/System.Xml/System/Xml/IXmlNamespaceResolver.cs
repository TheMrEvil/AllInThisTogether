using System;
using System.Collections.Generic;

namespace System.Xml
{
	/// <summary>Provides read-only access to a set of prefix and namespace mappings.</summary>
	// Token: 0x020001E5 RID: 485
	public interface IXmlNamespaceResolver
	{
		/// <summary>Gets a collection of defined prefix-namespace mappings that are currently in scope.</summary>
		/// <param name="scope">An <see cref="T:System.Xml.XmlNamespaceScope" /> value that specifies the type of namespace nodes to return.</param>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> that contains the current in-scope namespaces.</returns>
		// Token: 0x06001327 RID: 4903
		IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope);

		/// <summary>Gets the namespace URI mapped to the specified prefix.</summary>
		/// <param name="prefix">The prefix whose namespace URI you wish to find.</param>
		/// <returns>The namespace URI that is mapped to the prefix; <see langword="null" /> if the prefix is not mapped to a namespace URI.</returns>
		// Token: 0x06001328 RID: 4904
		string LookupNamespace(string prefix);

		/// <summary>Gets the prefix that is mapped to the specified namespace URI.</summary>
		/// <param name="namespaceName">The namespace URI whose prefix you wish to find.</param>
		/// <returns>The prefix that is mapped to the namespace URI; <see langword="null" /> if the namespace URI is not mapped to a prefix.</returns>
		// Token: 0x06001329 RID: 4905
		string LookupPrefix(string namespaceName);
	}
}
