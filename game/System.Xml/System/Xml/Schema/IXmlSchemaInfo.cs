using System;

namespace System.Xml.Schema
{
	/// <summary>Defines the post-schema-validation infoset of a validated XML node.</summary>
	// Token: 0x0200055E RID: 1374
	public interface IXmlSchemaInfo
	{
		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaValidity" /> value of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaValidity" /> value of this validated XML node.</returns>
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x0600369B RID: 13979
		XmlSchemaValidity Validity { get; }

		/// <summary>Gets a value indicating if this validated XML node was set as the result of a default being applied during XML Schema Definition Language (XSD) schema validation.</summary>
		/// <returns>
		///     <see langword="true" /> if this validated XML node was set as the result of a default being applied during schema validation; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x0600369C RID: 13980
		bool IsDefault { get; }

		/// <summary>Gets a value indicating if the value for this validated XML node is nil.</summary>
		/// <returns>
		///     <see langword="true" /> if the value for this validated XML node is nil; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x0600369D RID: 13981
		bool IsNil { get; }

		/// <summary>Gets the dynamic schema type for this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> object that represents the dynamic schema type for this validated XML node.</returns>
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x0600369E RID: 13982
		XmlSchemaSimpleType MemberType { get; }

		/// <summary>Gets the static XML Schema Definition Language (XSD) schema type of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> of this validated XML node.</returns>
		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x0600369F RID: 13983
		XmlSchemaType SchemaType { get; }

		/// <summary>Gets the compiled <see cref="T:System.Xml.Schema.XmlSchemaElement" /> that corresponds to this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaElement" /> that corresponds to this validated XML node.</returns>
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x060036A0 RID: 13984
		XmlSchemaElement SchemaElement { get; }

		/// <summary>Gets the compiled <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> that corresponds to this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> that corresponds to this validated XML node.</returns>
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x060036A1 RID: 13985
		XmlSchemaAttribute SchemaAttribute { get; }
	}
}
