using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	/// <summary>Provides custom formatting for XML serialization and deserialization.</summary>
	// Token: 0x0200027B RID: 635
	public interface IXmlSerializable
	{
		/// <summary>This method is reserved and should not be used. When implementing the <see langword="IXmlSerializable" /> interface, you should return <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute" /> to the class.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> method.</returns>
		// Token: 0x0600180D RID: 6157
		XmlSchema GetSchema();

		/// <summary>Generates an object from its XML representation.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> stream from which the object is deserialized. </param>
		// Token: 0x0600180E RID: 6158
		void ReadXml(XmlReader reader);

		/// <summary>Converts an object into its XML representation.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized. </param>
		// Token: 0x0600180F RID: 6159
		void WriteXml(XmlWriter writer);
	}
}
