using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="extension" /> element for simple content from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to derive simple types by extension. Such derivations are used to extend the simple type content of the element by adding attributes.</summary>
	// Token: 0x020005DB RID: 1499
	public class XmlSchemaSimpleContentExtension : XmlSchemaContent
	{
		/// <summary>Gets or sets the name of a built-in data type or simple type from which this type is extended.</summary>
		/// <returns>The base type name.</returns>
		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06003C21 RID: 15393 RVA: 0x00150EEE File Offset: 0x0014F0EE
		// (set) Token: 0x06003C22 RID: 15394 RVA: 0x00150EF6 File Offset: 0x0014F0F6
		[XmlAttribute("base")]
		public XmlQualifiedName BaseTypeName
		{
			get
			{
				return this.baseTypeName;
			}
			set
			{
				this.baseTypeName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> and <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" />.</summary>
		/// <returns>The collection of attributes for the <see langword="simpleType" /> element.</returns>
		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06003C23 RID: 15395 RVA: 0x00150F0F File Offset: 0x0014F10F
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the <see langword="XmlSchemaAnyAttribute" /> to be used for the attribute value.</summary>
		/// <returns>The <see langword="XmlSchemaAnyAttribute" />.Optional.</returns>
		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06003C24 RID: 15396 RVA: 0x00150F17 File Offset: 0x0014F117
		// (set) Token: 0x06003C25 RID: 15397 RVA: 0x00150F1F File Offset: 0x0014F11F
		[XmlElement("anyAttribute")]
		public XmlSchemaAnyAttribute AnyAttribute
		{
			get
			{
				return this.anyAttribute;
			}
			set
			{
				this.anyAttribute = value;
			}
		}

		// Token: 0x06003C26 RID: 15398 RVA: 0x00150F28 File Offset: 0x0014F128
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaSimpleContentExtension" /> class.</summary>
		// Token: 0x06003C27 RID: 15399 RVA: 0x00150F31 File Offset: 0x0014F131
		public XmlSchemaSimpleContentExtension()
		{
		}

		// Token: 0x04002BBD RID: 11197
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x04002BBE RID: 11198
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04002BBF RID: 11199
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;
	}
}
