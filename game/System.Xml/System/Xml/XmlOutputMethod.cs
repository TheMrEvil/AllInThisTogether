using System;

namespace System.Xml
{
	/// <summary>Specifies the method used to serialize the <see cref="T:System.Xml.XmlWriter" /> output. </summary>
	// Token: 0x02000182 RID: 386
	public enum XmlOutputMethod
	{
		/// <summary>Serialize according to the XML 1.0 rules.</summary>
		// Token: 0x04000EEB RID: 3819
		Xml,
		/// <summary>Serialize according to the HTML rules specified by XSLT.</summary>
		// Token: 0x04000EEC RID: 3820
		Html,
		/// <summary>Serialize text blocks only.</summary>
		// Token: 0x04000EED RID: 3821
		Text,
		/// <summary>Use the XSLT rules to choose between the <see cref="F:System.Xml.XmlOutputMethod.Xml" /> and <see cref="F:System.Xml.XmlOutputMethod.Html" /> output methods at runtime.</summary>
		// Token: 0x04000EEE RID: 3822
		AutoDetect
	}
}
