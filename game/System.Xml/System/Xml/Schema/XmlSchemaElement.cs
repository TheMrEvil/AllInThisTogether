using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="element" /> element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is the base class for all particle types and is used to describe an element in an XML document.</summary>
	// Token: 0x020005AB RID: 1451
	public class XmlSchemaElement : XmlSchemaParticle
	{
		/// <summary>Gets or sets information to indicate if the element can be used in an instance document.</summary>
		/// <returns>If <see langword="true" />, the element cannot appear in the instance document. The default is <see langword="false" />.Optional.</returns>
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06003AAC RID: 15020 RVA: 0x0014D8A0 File Offset: 0x0014BAA0
		// (set) Token: 0x06003AAD RID: 15021 RVA: 0x0014D8A8 File Offset: 0x0014BAA8
		[DefaultValue(false)]
		[XmlAttribute("abstract")]
		public bool IsAbstract
		{
			get
			{
				return this.isAbstract;
			}
			set
			{
				this.isAbstract = value;
				this.hasAbstractAttribute = true;
			}
		}

		/// <summary>Gets or sets a <see langword="Block" /> derivation.</summary>
		/// <returns>The attribute used to block a type derivation. Default value is <see langword="XmlSchemaDerivationMethod.None" />.Optional.</returns>
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06003AAE RID: 15022 RVA: 0x0014D8B8 File Offset: 0x0014BAB8
		// (set) Token: 0x06003AAF RID: 15023 RVA: 0x0014D8C0 File Offset: 0x0014BAC0
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		[XmlAttribute("block")]
		public XmlSchemaDerivationMethod Block
		{
			get
			{
				return this.block;
			}
			set
			{
				this.block = value;
			}
		}

		/// <summary>Gets or sets the default value of the element if its content is a simple type or content of the element is <see langword="textOnly" />.</summary>
		/// <returns>The default value for the element. The default is a null reference.Optional.</returns>
		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06003AB0 RID: 15024 RVA: 0x0014D8C9 File Offset: 0x0014BAC9
		// (set) Token: 0x06003AB1 RID: 15025 RVA: 0x0014D8D1 File Offset: 0x0014BAD1
		[DefaultValue(null)]
		[XmlAttribute("default")]
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		/// <summary>Gets or sets the <see langword="Final" /> property to indicate that no further derivations are allowed.</summary>
		/// <returns>The <see langword="Final" /> property. The default is <see langword="XmlSchemaDerivationMethod.None" />.Optional.</returns>
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06003AB2 RID: 15026 RVA: 0x0014D8DA File Offset: 0x0014BADA
		// (set) Token: 0x06003AB3 RID: 15027 RVA: 0x0014D8E2 File Offset: 0x0014BAE2
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		[XmlAttribute("final")]
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

		/// <summary>Gets or sets the fixed value.</summary>
		/// <returns>The fixed value that is predetermined and unchangeable. The default is a null reference.Optional.</returns>
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06003AB4 RID: 15028 RVA: 0x0014D8EB File Offset: 0x0014BAEB
		// (set) Token: 0x06003AB5 RID: 15029 RVA: 0x0014D8F3 File Offset: 0x0014BAF3
		[DefaultValue(null)]
		[XmlAttribute("fixed")]
		public string FixedValue
		{
			get
			{
				return this.fixedValue;
			}
			set
			{
				this.fixedValue = value;
			}
		}

		/// <summary>Gets or sets the form for the element.</summary>
		/// <returns>The form for the element. The default is the <see cref="P:System.Xml.Schema.XmlSchema.ElementFormDefault" /> value.Optional.</returns>
		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06003AB6 RID: 15030 RVA: 0x0014D8FC File Offset: 0x0014BAFC
		// (set) Token: 0x06003AB7 RID: 15031 RVA: 0x0014D904 File Offset: 0x0014BB04
		[DefaultValue(XmlSchemaForm.None)]
		[XmlAttribute("form")]
		public XmlSchemaForm Form
		{
			get
			{
				return this.form;
			}
			set
			{
				this.form = value;
			}
		}

		/// <summary>Gets or sets the name of the element.</summary>
		/// <returns>The name of the element. The default is <see langword="String.Empty" />.</returns>
		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06003AB8 RID: 15032 RVA: 0x0014D90D File Offset: 0x0014BB0D
		// (set) Token: 0x06003AB9 RID: 15033 RVA: 0x0014D915 File Offset: 0x0014BB15
		[DefaultValue("")]
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

		/// <summary>Gets or sets information that indicates if <see langword="xsi:nil" /> can occur in the instance data. Indicates if an explicit nil value can be assigned to the element.</summary>
		/// <returns>If nillable is <see langword="true" />, this enables an instance of the element to have the <see langword="nil" /> attribute set to <see langword="true" />. The <see langword="nil" /> attribute is defined as part of the XML Schema namespace for instances. The default is <see langword="false" />.Optional.</returns>
		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06003ABA RID: 15034 RVA: 0x0014D91E File Offset: 0x0014BB1E
		// (set) Token: 0x06003ABB RID: 15035 RVA: 0x0014D926 File Offset: 0x0014BB26
		[DefaultValue(false)]
		[XmlAttribute("nillable")]
		public bool IsNillable
		{
			get
			{
				return this.isNillable;
			}
			set
			{
				this.isNillable = value;
				this.hasNillableAttribute = true;
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06003ABC RID: 15036 RVA: 0x0014D936 File Offset: 0x0014BB36
		[XmlIgnore]
		internal bool HasNillableAttribute
		{
			get
			{
				return this.hasNillableAttribute;
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06003ABD RID: 15037 RVA: 0x0014D93E File Offset: 0x0014BB3E
		[XmlIgnore]
		internal bool HasAbstractAttribute
		{
			get
			{
				return this.hasAbstractAttribute;
			}
		}

		/// <summary>Gets or sets the reference name of an element declared in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>The reference name of the element.</returns>
		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06003ABE RID: 15038 RVA: 0x0014D946 File Offset: 0x0014BB46
		// (set) Token: 0x06003ABF RID: 15039 RVA: 0x0014D94E File Offset: 0x0014BB4E
		[XmlAttribute("ref")]
		public XmlQualifiedName RefName
		{
			get
			{
				return this.refName;
			}
			set
			{
				this.refName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Gets or sets the name of an element that is being substituted by this element.</summary>
		/// <returns>The qualified name of an element that is being substituted by this element.Optional.</returns>
		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06003AC0 RID: 15040 RVA: 0x0014D967 File Offset: 0x0014BB67
		// (set) Token: 0x06003AC1 RID: 15041 RVA: 0x0014D96F File Offset: 0x0014BB6F
		[XmlAttribute("substitutionGroup")]
		public XmlQualifiedName SubstitutionGroup
		{
			get
			{
				return this.substitutionGroup;
			}
			set
			{
				this.substitutionGroup = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Gets or sets the name of a built-in data type defined in this schema or another schema indicated by the specified namespace.</summary>
		/// <returns>The name of the built-in data type.</returns>
		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06003AC2 RID: 15042 RVA: 0x0014D988 File Offset: 0x0014BB88
		// (set) Token: 0x06003AC3 RID: 15043 RVA: 0x0014D990 File Offset: 0x0014BB90
		[XmlAttribute("type")]
		public XmlQualifiedName SchemaTypeName
		{
			get
			{
				return this.typeName;
			}
			set
			{
				this.typeName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Gets or sets the type of the element. This can either be a complex type or a simple type.</summary>
		/// <returns>The type of the element.</returns>
		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06003AC4 RID: 15044 RVA: 0x0014D9A9 File Offset: 0x0014BBA9
		// (set) Token: 0x06003AC5 RID: 15045 RVA: 0x0014D9B1 File Offset: 0x0014BBB1
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		[XmlElement("complexType", typeof(XmlSchemaComplexType))]
		public XmlSchemaType SchemaType
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		/// <summary>Gets the collection of constraints on the element.</summary>
		/// <returns>The collection of constraints.</returns>
		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06003AC6 RID: 15046 RVA: 0x0014D9BA File Offset: 0x0014BBBA
		[XmlElement("key", typeof(XmlSchemaKey))]
		[XmlElement("keyref", typeof(XmlSchemaKeyref))]
		[XmlElement("unique", typeof(XmlSchemaUnique))]
		public XmlSchemaObjectCollection Constraints
		{
			get
			{
				if (this.constraints == null)
				{
					this.constraints = new XmlSchemaObjectCollection();
				}
				return this.constraints;
			}
		}

		/// <summary>Gets the actual qualified name for the given element. </summary>
		/// <returns>The qualified name of the element. The post-compilation value of the <see langword="QualifiedName" /> property.</returns>
		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06003AC7 RID: 15047 RVA: 0x0014D9D5 File Offset: 0x0014BBD5
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		/// <summary>Gets a common language runtime (CLR) object based on the <see cref="T:System.Xml.Schema.XmlSchemaElement" /> or <see cref="T:System.Xml.Schema.XmlSchemaElement" /> of the element, which holds the post-compilation value of the <see langword="ElementType" /> property.</summary>
		/// <returns>The common language runtime object. The post-compilation value of the <see langword="ElementType" /> property.</returns>
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06003AC8 RID: 15048 RVA: 0x0014D9DD File Offset: 0x0014BBDD
		[XmlIgnore]
		[Obsolete("This property has been deprecated. Please use ElementSchemaType property that returns a strongly typed element type. http://go.microsoft.com/fwlink/?linkid=14202")]
		public object ElementType
		{
			get
			{
				if (this.elementType == null)
				{
					return null;
				}
				if (this.elementType.QualifiedName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					return this.elementType.Datatype;
				}
				return this.elementType;
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.Schema.XmlSchemaType" /> object representing the type of the element based on the <see cref="P:System.Xml.Schema.XmlSchemaElement.SchemaType" /> or <see cref="P:System.Xml.Schema.XmlSchemaElement.SchemaTypeName" /> values of the element.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> object.</returns>
		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06003AC9 RID: 15049 RVA: 0x0014DA17 File Offset: 0x0014BC17
		[XmlIgnore]
		public XmlSchemaType ElementSchemaType
		{
			get
			{
				return this.elementType;
			}
		}

		/// <summary>Gets the post-compilation value of the <see langword="Block" /> property.</summary>
		/// <returns>The post-compilation value of the <see langword="Block" /> property. The default is the <see langword="BlockDefault" /> value on the <see langword="schema" /> element.</returns>
		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06003ACA RID: 15050 RVA: 0x0014DA1F File Offset: 0x0014BC1F
		[XmlIgnore]
		public XmlSchemaDerivationMethod BlockResolved
		{
			get
			{
				return this.blockResolved;
			}
		}

		/// <summary>Gets the post-compilation value of the <see langword="Final" /> property.</summary>
		/// <returns>The post-compilation value of the <see langword="Final" /> property. Default value is the <see langword="FinalDefault" /> value on the <see langword="schema" /> element.</returns>
		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06003ACB RID: 15051 RVA: 0x0014DA27 File Offset: 0x0014BC27
		[XmlIgnore]
		public XmlSchemaDerivationMethod FinalResolved
		{
			get
			{
				return this.finalResolved;
			}
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x0014DA30 File Offset: 0x0014BC30
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

		// Token: 0x06003ACD RID: 15053 RVA: 0x0014DA67 File Offset: 0x0014BC67
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qualifiedName = value;
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x0014DA70 File Offset: 0x0014BC70
		internal void SetElementType(XmlSchemaType value)
		{
			this.elementType = value;
		}

		// Token: 0x06003ACF RID: 15055 RVA: 0x0014DA79 File Offset: 0x0014BC79
		internal void SetBlockResolved(XmlSchemaDerivationMethod value)
		{
			this.blockResolved = value;
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x0014DA82 File Offset: 0x0014BC82
		internal void SetFinalResolved(XmlSchemaDerivationMethod value)
		{
			this.finalResolved = value;
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x0014DA8B File Offset: 0x0014BC8B
		[XmlIgnore]
		internal bool HasDefault
		{
			get
			{
				return this.defaultValue != null && this.defaultValue.Length > 0;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06003AD2 RID: 15058 RVA: 0x0014DAA5 File Offset: 0x0014BCA5
		internal bool HasConstraints
		{
			get
			{
				return this.constraints != null && this.constraints.Count > 0;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06003AD3 RID: 15059 RVA: 0x0014DABF File Offset: 0x0014BCBF
		// (set) Token: 0x06003AD4 RID: 15060 RVA: 0x0014DAC7 File Offset: 0x0014BCC7
		internal bool IsLocalTypeDerivationChecked
		{
			get
			{
				return this.isLocalTypeDerivationChecked;
			}
			set
			{
				this.isLocalTypeDerivationChecked = value;
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06003AD5 RID: 15061 RVA: 0x0014DAD0 File Offset: 0x0014BCD0
		// (set) Token: 0x06003AD6 RID: 15062 RVA: 0x0014DAD8 File Offset: 0x0014BCD8
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

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06003AD7 RID: 15063 RVA: 0x0014DAE1 File Offset: 0x0014BCE1
		// (set) Token: 0x06003AD8 RID: 15064 RVA: 0x0014DAE9 File Offset: 0x0014BCE9
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

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06003AD9 RID: 15065 RVA: 0x0014DAF2 File Offset: 0x0014BCF2
		[XmlIgnore]
		internal override string NameString
		{
			get
			{
				return this.qualifiedName.ToString();
			}
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x0014DAFF File Offset: 0x0014BCFF
		internal override XmlSchemaObject Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x0014DB08 File Offset: 0x0014BD08
		internal XmlSchemaObject Clone(XmlSchema parentSchema)
		{
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)base.MemberwiseClone();
			xmlSchemaElement.refName = this.refName.Clone();
			xmlSchemaElement.substitutionGroup = this.substitutionGroup.Clone();
			xmlSchemaElement.typeName = this.typeName.Clone();
			xmlSchemaElement.qualifiedName = this.qualifiedName.Clone();
			XmlSchemaComplexType xmlSchemaComplexType = this.type as XmlSchemaComplexType;
			if (xmlSchemaComplexType != null && xmlSchemaComplexType.QualifiedName.IsEmpty)
			{
				xmlSchemaElement.type = (XmlSchemaType)xmlSchemaComplexType.Clone(parentSchema);
			}
			xmlSchemaElement.constraints = null;
			return xmlSchemaElement;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaElement" /> class.</summary>
		// Token: 0x06003ADC RID: 15068 RVA: 0x0014DB9C File Offset: 0x0014BD9C
		public XmlSchemaElement()
		{
		}

		// Token: 0x04002B2B RID: 11051
		private bool isAbstract;

		// Token: 0x04002B2C RID: 11052
		private bool hasAbstractAttribute;

		// Token: 0x04002B2D RID: 11053
		private bool isNillable;

		// Token: 0x04002B2E RID: 11054
		private bool hasNillableAttribute;

		// Token: 0x04002B2F RID: 11055
		private bool isLocalTypeDerivationChecked;

		// Token: 0x04002B30 RID: 11056
		private XmlSchemaDerivationMethod block = XmlSchemaDerivationMethod.None;

		// Token: 0x04002B31 RID: 11057
		private XmlSchemaDerivationMethod final = XmlSchemaDerivationMethod.None;

		// Token: 0x04002B32 RID: 11058
		private XmlSchemaForm form;

		// Token: 0x04002B33 RID: 11059
		private string defaultValue;

		// Token: 0x04002B34 RID: 11060
		private string fixedValue;

		// Token: 0x04002B35 RID: 11061
		private string name;

		// Token: 0x04002B36 RID: 11062
		private XmlQualifiedName refName = XmlQualifiedName.Empty;

		// Token: 0x04002B37 RID: 11063
		private XmlQualifiedName substitutionGroup = XmlQualifiedName.Empty;

		// Token: 0x04002B38 RID: 11064
		private XmlQualifiedName typeName = XmlQualifiedName.Empty;

		// Token: 0x04002B39 RID: 11065
		private XmlSchemaType type;

		// Token: 0x04002B3A RID: 11066
		private XmlQualifiedName qualifiedName = XmlQualifiedName.Empty;

		// Token: 0x04002B3B RID: 11067
		private XmlSchemaType elementType;

		// Token: 0x04002B3C RID: 11068
		private XmlSchemaDerivationMethod blockResolved;

		// Token: 0x04002B3D RID: 11069
		private XmlSchemaDerivationMethod finalResolved;

		// Token: 0x04002B3E RID: 11070
		private XmlSchemaObjectCollection constraints;

		// Token: 0x04002B3F RID: 11071
		private SchemaElementDecl elementDecl;
	}
}
