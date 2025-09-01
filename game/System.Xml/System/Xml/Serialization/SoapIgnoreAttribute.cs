using System;

namespace System.Xml.Serialization
{
	/// <summary>Instructs the <see cref="T:System.Xml.Serialization.XmlSerializer" /> not to serialize the public field or public read/write property value.</summary>
	// Token: 0x020002B1 RID: 689
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class SoapIgnoreAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapIgnoreAttribute" /> class.</summary>
		// Token: 0x060019F2 RID: 6642 RVA: 0x000021EA File Offset: 0x000003EA
		public SoapIgnoreAttribute()
		{
		}
	}
}
