using System;
using System.Xml.Linq;

namespace System.Xml.XPath
{
	// Token: 0x02000004 RID: 4
	internal static class XAttributeExtensions
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000020C4 File Offset: 0x000002C4
		public static string GetPrefixOfNamespace(this XAttribute attribute, XNamespace ns)
		{
			string namespaceName = ns.NamespaceName;
			if (namespaceName.Length == 0)
			{
				return string.Empty;
			}
			if (attribute.GetParent() != null)
			{
				return ((XElement)attribute.GetParent()).GetPrefixOfNamespace(ns);
			}
			if (namespaceName == XNodeNavigator.xmlPrefixNamespace)
			{
				return "xml";
			}
			if (namespaceName == XNodeNavigator.xmlnsPrefixNamespace)
			{
				return "xmlns";
			}
			return null;
		}
	}
}
