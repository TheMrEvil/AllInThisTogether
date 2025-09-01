using System;

namespace System.Xml
{
	/// <summary>Defines the namespace scope.</summary>
	// Token: 0x0200023D RID: 573
	public enum XmlNamespaceScope
	{
		/// <summary>All namespaces defined in the scope of the current node. This includes the xmlns:xml namespace which is always declared implicitly. The order of the namespaces returned is not defined.</summary>
		// Token: 0x040012DC RID: 4828
		All,
		/// <summary>All namespaces defined in the scope of the current node, excluding the xmlns:xml namespace, which is always declared implicitly. The order of the namespaces returned is not defined.</summary>
		// Token: 0x040012DD RID: 4829
		ExcludeXml,
		/// <summary>All namespaces that are defined locally at the current node.</summary>
		// Token: 0x040012DE RID: 4830
		Local
	}
}
