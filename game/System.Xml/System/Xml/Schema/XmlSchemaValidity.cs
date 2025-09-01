using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the validity of an XML item validated by the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> class.</summary>
	// Token: 0x020005EC RID: 1516
	public enum XmlSchemaValidity
	{
		/// <summary>The validity of the XML item is not known.</summary>
		// Token: 0x04002C2F RID: 11311
		NotKnown,
		/// <summary>The XML item is valid.</summary>
		// Token: 0x04002C30 RID: 11312
		Valid,
		/// <summary>The XML item is invalid.</summary>
		// Token: 0x04002C31 RID: 11313
		Invalid
	}
}
