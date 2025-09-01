using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="restriction" /> element for simple types from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used restricting <see langword="simpleType" /> element.</summary>
	// Token: 0x020005E0 RID: 1504
	public class XmlSchemaSimpleTypeRestriction : XmlSchemaSimpleTypeContent
	{
		/// <summary>Gets or sets the name of the qualified base type.</summary>
		/// <returns>The qualified name of the simple type restriction base type.</returns>
		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06003C40 RID: 15424 RVA: 0x001510CC File Offset: 0x0014F2CC
		// (set) Token: 0x06003C41 RID: 15425 RVA: 0x001510D4 File Offset: 0x0014F2D4
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

		/// <summary>Gets or sets information on the base type.</summary>
		/// <returns>The base type for the <see langword="simpleType" /> element.</returns>
		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06003C42 RID: 15426 RVA: 0x001510ED File Offset: 0x0014F2ED
		// (set) Token: 0x06003C43 RID: 15427 RVA: 0x001510F5 File Offset: 0x0014F2F5
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
		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06003C44 RID: 15428 RVA: 0x001510FE File Offset: 0x0014F2FE
		[XmlElement("length", typeof(XmlSchemaLengthFacet))]
		[XmlElement("minLength", typeof(XmlSchemaMinLengthFacet))]
		[XmlElement("whiteSpace", typeof(XmlSchemaWhiteSpaceFacet))]
		[XmlElement("maxLength", typeof(XmlSchemaMaxLengthFacet))]
		[XmlElement("enumeration", typeof(XmlSchemaEnumerationFacet))]
		[XmlElement("fractionDigits", typeof(XmlSchemaFractionDigitsFacet))]
		[XmlElement("maxInclusive", typeof(XmlSchemaMaxInclusiveFacet))]
		[XmlElement("pattern", typeof(XmlSchemaPatternFacet))]
		[XmlElement("maxExclusive", typeof(XmlSchemaMaxExclusiveFacet))]
		[XmlElement("minInclusive", typeof(XmlSchemaMinInclusiveFacet))]
		[XmlElement("totalDigits", typeof(XmlSchemaTotalDigitsFacet))]
		[XmlElement("minExclusive", typeof(XmlSchemaMinExclusiveFacet))]
		public XmlSchemaObjectCollection Facets
		{
			get
			{
				return this.facets;
			}
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x00151106 File Offset: 0x0014F306
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)base.MemberwiseClone();
			xmlSchemaSimpleTypeRestriction.BaseTypeName = this.baseTypeName.Clone();
			return xmlSchemaSimpleTypeRestriction;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaSimpleTypeRestriction" /> class.</summary>
		// Token: 0x06003C46 RID: 15430 RVA: 0x00151124 File Offset: 0x0014F324
		public XmlSchemaSimpleTypeRestriction()
		{
		}

		// Token: 0x04002BC9 RID: 11209
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;

		// Token: 0x04002BCA RID: 11210
		private XmlSchemaSimpleType baseType;

		// Token: 0x04002BCB RID: 11211
		private XmlSchemaObjectCollection facets = new XmlSchemaObjectCollection();
	}
}
