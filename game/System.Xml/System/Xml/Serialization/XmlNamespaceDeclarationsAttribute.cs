using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies that the target property, parameter, return value, or class member contains prefixes associated with namespaces that are used within an XML document.</summary>
	// Token: 0x020002D9 RID: 729
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class XmlNamespaceDeclarationsAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlNamespaceDeclarationsAttribute" /> class.</summary>
		// Token: 0x06001C29 RID: 7209 RVA: 0x000021EA File Offset: 0x000003EA
		public XmlNamespaceDeclarationsAttribute()
		{
		}
	}
}
