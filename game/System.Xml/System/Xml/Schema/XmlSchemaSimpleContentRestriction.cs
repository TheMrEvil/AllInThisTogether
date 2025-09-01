using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="restriction" /> element for simple content from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to derive simple types by restriction. Such derivations can be used to restrict the range of values for the element to a subset of the values specified in the inherited simple type.</summary>
	// Token: 0x020005DC RID: 1500
	public class XmlSchemaSimpleContentRestriction : XmlSchemaContent
	{
		/// <summary>Gets or sets the name of the built-in data type or simple type from which this type is derived.</summary>
		/// <returns>The name of the base type.</returns>
		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06003C28 RID: 15400 RVA: 0x00150F4F File Offset: 0x0014F14F
		// (set) Token: 0x06003C29 RID: 15401 RVA: 0x00150F57 File Offset: 0x0014F157
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

		/// <summary>Gets or sets the simple type base value.</summary>
		/// <returns>The simple type base value.</returns>
		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06003C2A RID: 15402 RVA: 0x00150F70 File Offset: 0x0014F170
		// (set) Token: 0x06003C2B RID: 15403 RVA: 0x00150F78 File Offset: 0x0014F178
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		public XmlSchemaSimpleType BaseType
		{
			get
			{
				return this.baseType;
			}
			set
			{
				this.baseType = value;
			}
		}

		/// <summary>Gets or sets an Xml Schema facet. </summary>
		/// <returns>One of the following facet classes:
		///     <see cref="T:System.Xml.Schema.XmlSchemaLengthFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMinLengthFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMaxLengthFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaPatternFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaEnumerationFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMaxInclusiveFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMaxExclusiveFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMinInclusiveFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMinExclusiveFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaFractionDigitsFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaTotalDigitsFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaWhiteSpaceFacet" />.</returns>
		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06003C2C RID: 15404 RVA: 0x00150F81 File Offset: 0x0014F181
		[XmlElement("minInclusive", typeof(XmlSchemaMinInclusiveFacet))]
		[XmlElement("minLength", typeof(XmlSchemaMinLengthFacet))]
		[XmlElement("maxLength", typeof(XmlSchemaMaxLengthFacet))]
		[XmlElement("pattern", typeof(XmlSchemaPatternFacet))]
		[XmlElement("enumeration", typeof(XmlSchemaEnumerationFacet))]
		[XmlElement("length", typeof(XmlSchemaLengthFacet))]
		[XmlElement("whiteSpace", typeof(XmlSchemaWhiteSpaceFacet))]
		[XmlElement("fractionDigits", typeof(XmlSchemaFractionDigitsFacet))]
		[XmlElement("totalDigits", typeof(XmlSchemaTotalDigitsFacet))]
		[XmlElement("minExclusive", typeof(XmlSchemaMinExclusiveFacet))]
		[XmlElement("maxInclusive", typeof(XmlSchemaMaxInclusiveFacet))]
		[XmlElement("maxExclusive", typeof(XmlSchemaMaxExclusiveFacet))]
		public XmlSchemaObjectCollection Facets
		{
			get
			{
				return this.facets;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> and <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" /> collection of attributes for the simple type.</summary>
		/// <returns>The collection of attributes for a simple type.</returns>
		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06003C2D RID: 15405 RVA: 0x00150F89 File Offset: 0x0014F189
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> to be used for the attribute value.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> for the attribute value.Optional.</returns>
		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06003C2E RID: 15406 RVA: 0x00150F91 File Offset: 0x0014F191
		// (set) Token: 0x06003C2F RID: 15407 RVA: 0x00150F99 File Offset: 0x0014F199
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

		// Token: 0x06003C30 RID: 15408 RVA: 0x00150FA2 File Offset: 0x0014F1A2
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaSimpleContentRestriction" /> class.</summary>
		// Token: 0x06003C31 RID: 15409 RVA: 0x00150FAB File Offset: 0x0014F1AB
		public XmlSchemaSimpleContentRestriction()
		{
		}

		// Token: 0x04002BC0 RID: 11200
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;

		// Token: 0x04002BC1 RID: 11201
		private XmlSchemaSimpleType baseType;

		// Token: 0x04002BC2 RID: 11202
		private XmlSchemaObjectCollection facets = new XmlSchemaObjectCollection();

		// Token: 0x04002BC3 RID: 11203
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x04002BC4 RID: 11204
		private XmlSchemaAnyAttribute anyAttribute;
	}
}
