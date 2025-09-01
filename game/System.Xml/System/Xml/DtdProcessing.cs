using System;

namespace System.Xml
{
	/// <summary>Specifies the options for processing DTDs. The <see cref="T:System.Xml.DtdProcessing" /> enumeration is used by the <see cref="T:System.Xml.XmlReaderSettings" /> class.</summary>
	// Token: 0x0200002F RID: 47
	public enum DtdProcessing
	{
		/// <summary>Specifies that when a DTD is encountered, an <see cref="T:System.Xml.XmlException" /> is thrown with a message that states that DTDs are prohibited. This is the default behavior.</summary>
		// Token: 0x040005E4 RID: 1508
		Prohibit,
		/// <summary>Causes the DOCTYPE element to be ignored. No DTD processing occurs. </summary>
		// Token: 0x040005E5 RID: 1509
		Ignore,
		/// <summary>Used for parsing DTDs.</summary>
		// Token: 0x040005E6 RID: 1510
		Parse
	}
}
