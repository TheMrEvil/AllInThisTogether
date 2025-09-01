using System;

namespace System.Xml.Serialization
{
	/// <summary>Delegate used by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class for deserialization of types from SOAP-encoded, non-root XML data. </summary>
	/// <returns>The object returned by the callback.</returns>
	// Token: 0x020002F1 RID: 753
	// (Invoke) Token: 0x06001E00 RID: 7680
	public delegate object XmlSerializationReadCallback();
}
