using System;

namespace System.Xml.Serialization
{
	/// <summary>Delegate that is used by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class for serialization of types from SOAP-encoded, non-root XML data. </summary>
	/// <param name="o">The object being serialized.</param>
	// Token: 0x020002FA RID: 762
	// (Invoke) Token: 0x06001F3B RID: 7995
	public delegate void XmlSerializationWriteCallback(object o);
}
