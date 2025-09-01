using System;

namespace System.Xml.XPath
{
	/// <summary>Defines the namespace scope.</summary>
	// Token: 0x02000258 RID: 600
	public enum XPathNamespaceScope
	{
		/// <summary>Returns all namespaces defined in the scope of the current node. This includes the xmlns:xml namespace which is always declared implicitly. The order of the namespaces returned is not defined.</summary>
		// Token: 0x040017FE RID: 6142
		All,
		/// <summary>Returns all namespaces defined in the scope of the current node, excluding the xmlns:xml namespace. The xmlns:xml namespace is always declared implicitly. The order of the namespaces returned is not defined.</summary>
		// Token: 0x040017FF RID: 6143
		ExcludeXml,
		/// <summary>Returns all namespaces that are defined locally at the current node. </summary>
		// Token: 0x04001800 RID: 6144
		Local
	}
}
