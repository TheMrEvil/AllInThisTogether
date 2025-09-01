using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>The base class for all simple types and complex types.</summary>
	// Token: 0x020005E4 RID: 1508
	public class XmlSchemaType : XmlSchemaAnnotated
	{
		/// <summary>Returns an <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> that represents the built-in simple type of the simple type that is specified by the qualified name.</summary>
		/// <param name="qualifiedName">The <see cref="T:System.Xml.XmlQualifiedName" /> of the simple type.</param>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> that represents the built-in simple type.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlQualifiedName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C54 RID: 15444 RVA: 0x00151236 File Offset: 0x0014F436
		public static XmlSchemaSimpleType GetBuiltInSimpleType(XmlQualifiedName qualifiedName)
		{
			if (qualifiedName == null)
			{
				throw new ArgumentNullException("qualifiedName");
			}
			return DatatypeImplementation.GetSimpleTypeFromXsdType(qualifiedName);
		}

		/// <summary>Returns an <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> that represents the built-in simple type of the specified simple type.</summary>
		/// <param name="typeCode">One of the <see cref="T:System.Xml.Schema.XmlTypeCode" /> values representing the simple type.</param>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> that represents the built-in simple type.</returns>
		// Token: 0x06003C55 RID: 15445 RVA: 0x00151252 File Offset: 0x0014F452
		public static XmlSchemaSimpleType GetBuiltInSimpleType(XmlTypeCode typeCode)
		{
			return DatatypeImplementation.GetSimpleTypeFromTypeCode(typeCode);
		}

		/// <summary>Returns an <see cref="T:System.Xml.Schema.XmlSchemaComplexType" /> that represents the built-in complex type of the complex type specified.</summary>
		/// <param name="typeCode">One of the <see cref="T:System.Xml.Schema.XmlTypeCode" /> values representing the complex type.</param>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaComplexType" /> that represents the built-in complex type.</returns>
		// Token: 0x06003C56 RID: 15446 RVA: 0x0015125A File Offset: 0x0014F45A
		public static XmlSchemaComplexType GetBuiltInComplexType(XmlTypeCode typeCode)
		{
			if (typeCode == XmlTypeCode.Item)
			{
				return XmlSchemaComplexType.AnyType;
			}
			return null;
		}

		/// <summary>Returns an <see cref="T:System.Xml.Schema.XmlSchemaComplexType" /> that represents the built-in complex type of the complex type specified by qualified name.</summary>
		/// <param name="qualifiedName">The <see cref="T:System.Xml.XmlQualifiedName" /> of the complex type.</param>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaComplexType" /> that represents the built-in complex type.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlQualifiedName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C57 RID: 15447 RVA: 0x00151268 File Offset: 0x0014F468
		public static XmlSchemaComplexType GetBuiltInComplexType(XmlQualifiedName qualifiedName)
		{
			if (qualifiedName == null)
			{
				throw new ArgumentNullException("qualifiedName");
			}
			if (qualifiedName.Equals(XmlSchemaComplexType.AnyType.QualifiedName))
			{
				return XmlSchemaComplexType.AnyType;
			}
			if (qualifiedName.Equals(XmlSchemaComplexType.UntypedAnyType.QualifiedName))
			{
				return XmlSchemaComplexType.UntypedAnyType;
			}
			return null;
		}

		/// <summary>Gets or sets the name of the type.</summary>
		/// <returns>The name of the type.</returns>
		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06003C58 RID: 15448 RVA: 0x001512BA File Offset: 0x0014F4BA
		// (set) Token: 0x06003C59 RID: 15449 RVA: 0x001512C2 File Offset: 0x0014F4C2
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the final attribute of the type derivation that indicates if further derivations are allowed.</summary>
		/// <returns>One of the valid <see cref="T:System.Xml.Schema.XmlSchemaDerivationMethod" /> values. The default is <see cref="F:System.Xml.Schema.XmlSchemaDerivationMethod.None" />.</returns>
		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06003C5A RID: 15450 RVA: 0x001512CB File Offset: 0x0014F4CB
		// (set) Token: 0x06003C5B RID: 15451 RVA: 0x001512D3 File Offset: 0x0014F4D3
		[XmlAttribute("final")]
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		public XmlSchemaDerivationMethod Final
		{
			get
			{
				return this.final;
			}
			set
			{
				this.final = value;
			}
		}

		/// <summary>Gets the qualified name for the type built from the <see langword="Name" /> attribute of this type. This is a post-schema-compilation property.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlQualifiedName" /> for the type built from the <see langword="Name" /> attribute of this type.</returns>
		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06003C5C RID: 15452 RVA: 0x001512DC File Offset: 0x0014F4DC
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qname;
			}
		}

		/// <summary>Gets the post-compilation value of the <see cref="P:System.Xml.Schema.XmlSchemaType.Final" /> property.</summary>
		/// <returns>The post-compilation value of the <see cref="P:System.Xml.Schema.XmlSchemaType.Final" /> property. The default is the <see langword="finalDefault" /> attribute value of the <see langword="schema" /> element.</returns>
		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06003C5D RID: 15453 RVA: 0x001512E6 File Offset: 0x0014F4E6
		[XmlIgnore]
		public XmlSchemaDerivationMethod FinalResolved
		{
			get
			{
				return this.finalResolved;
			}
		}

		/// <summary>Gets the post-compilation object type or the built-in XML Schema Definition Language (XSD) data type, simpleType element, or complexType element. This is a post-schema-compilation infoset property.</summary>
		/// <returns>The built-in XSD data type, simpleType element, or complexType element.</returns>
		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06003C5E RID: 15454 RVA: 0x001512EE File Offset: 0x0014F4EE
		[Obsolete("This property has been deprecated. Please use BaseXmlSchemaType property that returns a strongly typed base schema type. http://go.microsoft.com/fwlink/?linkid=14202")]
		[XmlIgnore]
		public object BaseSchemaType
		{
			get
			{
				if (this.baseSchemaType == null)
				{
					return null;
				}
				if (this.baseSchemaType.QualifiedName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					return this.baseSchemaType.Datatype;
				}
				return this.baseSchemaType;
			}
		}

		/// <summary>Gets the post-compilation value for the base type of this schema type.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> object representing the base type of this schema type.</returns>
		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06003C5F RID: 15455 RVA: 0x00151328 File Offset: 0x0014F528
		[XmlIgnore]
		public XmlSchemaType BaseXmlSchemaType
		{
			get
			{
				return this.baseSchemaType;
			}
		}

		/// <summary>Gets the post-compilation information on how this element was derived from its base type.</summary>
		/// <returns>One of the valid <see cref="T:System.Xml.Schema.XmlSchemaDerivationMethod" /> values.</returns>
		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06003C60 RID: 15456 RVA: 0x00151330 File Offset: 0x0014F530
		[XmlIgnore]
		public XmlSchemaDerivationMethod DerivedBy
		{
			get
			{
				return this.derivedBy;
			}
		}

		/// <summary>Gets the post-compilation value for the data type of the complex type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> post-schema-compilation value.</returns>
		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06003C61 RID: 15457 RVA: 0x00151338 File Offset: 0x0014F538
		[XmlIgnore]
		public XmlSchemaDatatype Datatype
		{
			get
			{
				return this.datatype;
			}
		}

		/// <summary>Gets or sets a value indicating if this type has a mixed content model. This property is only valid in a complex type.</summary>
		/// <returns>
		///     <see langword="true" /> if the type has a mixed content model; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06003C62 RID: 15458 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		// (set) Token: 0x06003C63 RID: 15459 RVA: 0x0000B528 File Offset: 0x00009728
		[XmlIgnore]
		public virtual bool IsMixed
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlTypeCode" /> of the type.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlTypeCode" /> values.</returns>
		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06003C64 RID: 15460 RVA: 0x00151340 File Offset: 0x0014F540
		[XmlIgnore]
		public XmlTypeCode TypeCode
		{
			get
			{
				if (this == XmlSchemaComplexType.AnyType)
				{
					return XmlTypeCode.Item;
				}
				if (this.datatype == null)
				{
					return XmlTypeCode.None;
				}
				return this.datatype.TypeCode;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06003C65 RID: 15461 RVA: 0x00151361 File Offset: 0x0014F561
		[XmlIgnore]
		internal XmlValueConverter ValueConverter
		{
			get
			{
				if (this.datatype == null)
				{
					return XmlUntypedConverter.Untyped;
				}
				return this.datatype.ValueConverter;
			}
		}

		// Token: 0x06003C66 RID: 15462 RVA: 0x0015137C File Offset: 0x0014F57C
		internal XmlReader Validate(XmlReader reader, XmlResolver resolver, XmlSchemaSet schemaSet, ValidationEventHandler valEventHandler)
		{
			if (schemaSet != null)
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.ValidationType = ValidationType.Schema;
				xmlReaderSettings.Schemas = schemaSet;
				xmlReaderSettings.ValidationEventHandler += valEventHandler;
				return new XsdValidatingReader(reader, resolver, xmlReaderSettings, this);
			}
			return null;
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06003C67 RID: 15463 RVA: 0x001513B3 File Offset: 0x0014F5B3
		internal XmlSchemaContentType SchemaContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x06003C68 RID: 15464 RVA: 0x001513BB File Offset: 0x0014F5BB
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qname = value;
		}

		// Token: 0x06003C69 RID: 15465 RVA: 0x001513C6 File Offset: 0x0014F5C6
		internal void SetFinalResolved(XmlSchemaDerivationMethod value)
		{
			this.finalResolved = value;
		}

		// Token: 0x06003C6A RID: 15466 RVA: 0x001513CF File Offset: 0x0014F5CF
		internal void SetBaseSchemaType(XmlSchemaType value)
		{
			this.baseSchemaType = value;
		}

		// Token: 0x06003C6B RID: 15467 RVA: 0x001513D8 File Offset: 0x0014F5D8
		internal void SetDerivedBy(XmlSchemaDerivationMethod value)
		{
			this.derivedBy = value;
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x001513E1 File Offset: 0x0014F5E1
		internal void SetDatatype(XmlSchemaDatatype value)
		{
			this.datatype = value;
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06003C6D RID: 15469 RVA: 0x001513EA File Offset: 0x0014F5EA
		// (set) Token: 0x06003C6E RID: 15470 RVA: 0x001513F4 File Offset: 0x0014F5F4
		internal SchemaElementDecl ElementDecl
		{
			get
			{
				return this.elementDecl;
			}
			set
			{
				this.elementDecl = value;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06003C6F RID: 15471 RVA: 0x001513FF File Offset: 0x0014F5FF
		// (set) Token: 0x06003C70 RID: 15472 RVA: 0x00151407 File Offset: 0x0014F607
		[XmlIgnore]
		internal XmlSchemaType Redefined
		{
			get
			{
				return this.redefined;
			}
			set
			{
				this.redefined = value;
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06003C71 RID: 15473 RVA: 0x00151410 File Offset: 0x0014F610
		internal virtual XmlQualifiedName DerivedFrom
		{
			get
			{
				return XmlQualifiedName.Empty;
			}
		}

		// Token: 0x06003C72 RID: 15474 RVA: 0x00151417 File Offset: 0x0014F617
		internal void SetContentType(XmlSchemaContentType value)
		{
			this.contentType = value;
		}

		/// <summary>Returns a value indicating if the derived schema type specified is derived from the base schema type specified</summary>
		/// <param name="derivedType">The derived <see cref="T:System.Xml.Schema.XmlSchemaType" /> to test.</param>
		/// <param name="baseType">The base <see cref="T:System.Xml.Schema.XmlSchemaType" /> to test the derived <see cref="T:System.Xml.Schema.XmlSchemaType" /> against.</param>
		/// <param name="except">One of the <see cref="T:System.Xml.Schema.XmlSchemaDerivationMethod" /> values representing a type derivation method to exclude from testing.</param>
		/// <returns>
		///     <see langword="true" /> if the derived type is derived from the base type; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003C73 RID: 15475 RVA: 0x00151420 File Offset: 0x0014F620
		public static bool IsDerivedFrom(XmlSchemaType derivedType, XmlSchemaType baseType, XmlSchemaDerivationMethod except)
		{
			if (derivedType == null || baseType == null)
			{
				return false;
			}
			if (derivedType == baseType)
			{
				return true;
			}
			if (baseType == XmlSchemaComplexType.AnyType)
			{
				return true;
			}
			XmlSchemaSimpleType xmlSchemaSimpleType;
			XmlSchemaSimpleType xmlSchemaSimpleType2;
			for (;;)
			{
				xmlSchemaSimpleType = (derivedType as XmlSchemaSimpleType);
				xmlSchemaSimpleType2 = (baseType as XmlSchemaSimpleType);
				if (xmlSchemaSimpleType2 != null && xmlSchemaSimpleType != null)
				{
					break;
				}
				if ((except & derivedType.DerivedBy) != XmlSchemaDerivationMethod.Empty)
				{
					return false;
				}
				derivedType = derivedType.BaseXmlSchemaType;
				if (derivedType == baseType)
				{
					return true;
				}
				if (derivedType == null)
				{
					return false;
				}
			}
			return xmlSchemaSimpleType2 == DatatypeImplementation.AnySimpleType || ((except & derivedType.DerivedBy) == XmlSchemaDerivationMethod.Empty && xmlSchemaSimpleType.Datatype.IsDerivedFrom(xmlSchemaSimpleType2.Datatype));
		}

		// Token: 0x06003C74 RID: 15476 RVA: 0x001514A2 File Offset: 0x0014F6A2
		internal static bool IsDerivedFromDatatype(XmlSchemaDatatype derivedDataType, XmlSchemaDatatype baseDataType, XmlSchemaDerivationMethod except)
		{
			return DatatypeImplementation.AnySimpleType.Datatype == baseDataType || derivedDataType.IsDerivedFrom(baseDataType);
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06003C75 RID: 15477 RVA: 0x001514BA File Offset: 0x0014F6BA
		// (set) Token: 0x06003C76 RID: 15478 RVA: 0x001514C2 File Offset: 0x0014F6C2
		[XmlIgnore]
		internal override string NameAttribute
		{
			get
			{
				return this.Name;
			}
			set
			{
				this.Name = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaType" /> class.</summary>
		// Token: 0x06003C77 RID: 15479 RVA: 0x001514CB File Offset: 0x0014F6CB
		public XmlSchemaType()
		{
		}

		// Token: 0x04002BD2 RID: 11218
		private string name;

		// Token: 0x04002BD3 RID: 11219
		private XmlSchemaDerivationMethod final = XmlSchemaDerivationMethod.None;

		// Token: 0x04002BD4 RID: 11220
		private XmlSchemaDerivationMethod derivedBy;

		// Token: 0x04002BD5 RID: 11221
		private XmlSchemaType baseSchemaType;

		// Token: 0x04002BD6 RID: 11222
		private XmlSchemaDatatype datatype;

		// Token: 0x04002BD7 RID: 11223
		private XmlSchemaDerivationMethod finalResolved;

		// Token: 0x04002BD8 RID: 11224
		private volatile SchemaElementDecl elementDecl;

		// Token: 0x04002BD9 RID: 11225
		private volatile XmlQualifiedName qname = XmlQualifiedName.Empty;

		// Token: 0x04002BDA RID: 11226
		private XmlSchemaType redefined;

		// Token: 0x04002BDB RID: 11227
		private XmlSchemaContentType contentType;
	}
}
