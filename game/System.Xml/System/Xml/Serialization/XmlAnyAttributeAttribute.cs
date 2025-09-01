using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies that the member (a field that returns an array of <see cref="T:System.Xml.XmlAttribute" /> objects) can contain any XML attributes.</summary>
	// Token: 0x020002C3 RID: 707
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class XmlAnyAttributeAttribute : Attribute
	{
		/// <summary>Constructs a new instance of the <see cref="T:System.Xml.Serialization.XmlAnyAttributeAttribute" /> class.</summary>
		// Token: 0x06001ADE RID: 6878 RVA: 0x000021EA File Offset: 0x000003EA
		public XmlAnyAttributeAttribute()
		{
		}
	}
}
