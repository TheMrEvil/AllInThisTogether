using System;

namespace System.Xml.Serialization
{
	/// <summary>Delegate used by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class for deserialization of SOAP-encoded XML data. </summary>
	/// <param name="fixup">An instance of the <see cref="T:System.Xml.Serialization.XmlSerializationReader.Fixup" /> class that contains the object to be fixed and the array of string identifiers for the items to fill in.</param>
	// Token: 0x020002EF RID: 751
	// (Invoke) Token: 0x06001DF8 RID: 7672
	public delegate void XmlSerializationFixupCallback(object fixup);
}
