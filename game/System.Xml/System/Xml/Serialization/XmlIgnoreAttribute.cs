using System;

namespace System.Xml.Serialization
{
	/// <summary>Instructs the <see cref="M:System.Xml.Serialization.XmlSerializer.Serialize(System.IO.TextWriter,System.Object)" /> method of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> not to serialize the public field or public read/write property value.</summary>
	// Token: 0x020002D3 RID: 723
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class XmlIgnoreAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlIgnoreAttribute" /> class.</summary>
		// Token: 0x06001BFC RID: 7164 RVA: 0x000021EA File Offset: 0x000003EA
		public XmlIgnoreAttribute()
		{
		}
	}
}
