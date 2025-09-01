using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="complexType" /> element from XML Schema as specified by the World Wide Web Consortium (W3C). This class defines a complex type that determines the set of attributes and content of an element.</summary>
	// Token: 0x020005A3 RID: 1443
	public class XmlSchemaComplexType : XmlSchemaType
	{
		// Token: 0x06003A53 RID: 14931 RVA: 0x0014C9A8 File Offset: 0x0014ABA8
		static XmlSchemaComplexType()
		{
			XmlSchemaComplexType.untypedAnyType.SetQualifiedName(new XmlQualifiedName("untypedAny", "http://www.w3.org/2003/11/xpath-datatypes"));
			XmlSchemaComplexType.untypedAnyType.IsMixed = true;
			XmlSchemaComplexType.untypedAnyType.SetContentTypeParticle(XmlSchemaComplexType.anyTypeLax.ContentTypeParticle);
			XmlSchemaComplexType.untypedAnyType.SetContentType(XmlSchemaContentType.Mixed);
			XmlSchemaComplexType.untypedAnyType.ElementDecl = SchemaElementDecl.CreateAnyTypeElementDecl();
			XmlSchemaComplexType.untypedAnyType.ElementDecl.SchemaType = XmlSchemaComplexType.untypedAnyType;
			XmlSchemaComplexType.untypedAnyType.ElementDecl.ContentValidator = XmlSchemaComplexType.AnyTypeContentValidator;
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x0014CA50 File Offset: 0x0014AC50
		private static XmlSchemaComplexType CreateAnyType(XmlSchemaContentProcessing processContents)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.SetQualifiedName(DatatypeImplementation.QnAnyType);
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.MinOccurs = 0m;
			xmlSchemaAny.MaxOccurs = decimal.MaxValue;
			xmlSchemaAny.ProcessContents = processContents;
			xmlSchemaAny.BuildNamespaceList(null);
			xmlSchemaComplexType.SetContentTypeParticle(new XmlSchemaSequence
			{
				Items = 
				{
					xmlSchemaAny
				}
			});
			xmlSchemaComplexType.SetContentType(XmlSchemaContentType.Mixed);
			xmlSchemaComplexType.ElementDecl = SchemaElementDecl.CreateAnyTypeElementDecl();
			xmlSchemaComplexType.ElementDecl.SchemaType = xmlSchemaComplexType;
			ParticleContentValidator particleContentValidator = new ParticleContentValidator(XmlSchemaContentType.Mixed);
			particleContentValidator.Start();
			particleContentValidator.OpenGroup();
			particleContentValidator.AddNamespaceList(xmlSchemaAny.NamespaceList, xmlSchemaAny);
			particleContentValidator.AddStar();
			particleContentValidator.CloseGroup();
			ContentValidator contentValidator = particleContentValidator.Finish(true);
			xmlSchemaComplexType.ElementDecl.ContentValidator = contentValidator;
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = new XmlSchemaAnyAttribute();
			xmlSchemaAnyAttribute.ProcessContents = processContents;
			xmlSchemaAnyAttribute.BuildNamespaceList(null);
			xmlSchemaComplexType.SetAttributeWildcard(xmlSchemaAnyAttribute);
			xmlSchemaComplexType.ElementDecl.AnyAttribute = xmlSchemaAnyAttribute;
			return xmlSchemaComplexType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaComplexType" /> class.</summary>
		// Token: 0x06003A55 RID: 14933 RVA: 0x0014CB40 File Offset: 0x0014AD40
		public XmlSchemaComplexType()
		{
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06003A56 RID: 14934 RVA: 0x0014CB5E File Offset: 0x0014AD5E
		[XmlIgnore]
		internal static XmlSchemaComplexType AnyType
		{
			get
			{
				return XmlSchemaComplexType.anyTypeLax;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06003A57 RID: 14935 RVA: 0x0014CB65 File Offset: 0x0014AD65
		[XmlIgnore]
		internal static XmlSchemaComplexType UntypedAnyType
		{
			get
			{
				return XmlSchemaComplexType.untypedAnyType;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06003A58 RID: 14936 RVA: 0x0014CB6C File Offset: 0x0014AD6C
		[XmlIgnore]
		internal static XmlSchemaComplexType AnyTypeSkip
		{
			get
			{
				return XmlSchemaComplexType.anyTypeSkip;
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06003A59 RID: 14937 RVA: 0x0014CB73 File Offset: 0x0014AD73
		internal static ContentValidator AnyTypeContentValidator
		{
			get
			{
				return XmlSchemaComplexType.anyTypeLax.ElementDecl.ContentValidator;
			}
		}

		/// <summary>Gets or sets the information that determines if the <see langword="complexType" /> element can be used in the instance document.</summary>
		/// <returns>If <see langword="true" />, an element cannot use this <see langword="complexType" /> element directly and must use a complex type that is derived from this <see langword="complexType" /> element. The default is <see langword="false" />.Optional.</returns>
		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06003A5A RID: 14938 RVA: 0x0014CB84 File Offset: 0x0014AD84
		// (set) Token: 0x06003A5B RID: 14939 RVA: 0x0014CB91 File Offset: 0x0014AD91
		[XmlAttribute("abstract")]
		[DefaultValue(false)]
		public bool IsAbstract
		{
			get
			{
				return (this.pvFlags & 4) > 0;
			}
			set
			{
				if (value)
				{
					this.pvFlags |= 4;
					return;
				}
				this.pvFlags = (byte)((int)this.pvFlags & -5);
			}
		}

		/// <summary>Gets or sets the <see langword="block" /> attribute.</summary>
		/// <returns>The <see langword="block" /> attribute prevents a complex type from being used in the specified type of derivation. The default is <see langword="XmlSchemaDerivationMethod.None" />.Optional.</returns>
		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06003A5C RID: 14940 RVA: 0x0014CBB6 File Offset: 0x0014ADB6
		// (set) Token: 0x06003A5D RID: 14941 RVA: 0x0014CBBE File Offset: 0x0014ADBE
		[XmlAttribute("block")]
		[DefaultValue(XmlSchemaDerivationMethod.None)]
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

		/// <summary>Gets or sets information that determines if the complex type has a mixed content model (markup within the content).</summary>
		/// <returns>
		///     <see langword="true" />, if character data can appear between child elements of this complex type; otherwise, <see langword="false" />. The default is <see langword="false" />.Optional.</returns>
		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06003A5E RID: 14942 RVA: 0x0014CBC7 File Offset: 0x0014ADC7
		// (set) Token: 0x06003A5F RID: 14943 RVA: 0x0014CBD4 File Offset: 0x0014ADD4
		[XmlAttribute("mixed")]
		[DefaultValue(false)]
		public override bool IsMixed
		{
			get
			{
				return (this.pvFlags & 2) > 0;
			}
			set
			{
				if (value)
				{
					this.pvFlags |= 2;
					return;
				}
				this.pvFlags = (byte)((int)this.pvFlags & -3);
			}
		}

		/// <summary>Gets or sets the post-compilation <see cref="T:System.Xml.Schema.XmlSchemaContentModel" /> of this complex type.</summary>
		/// <returns>The content model type that is one of the <see cref="T:System.Xml.Schema.XmlSchemaSimpleContent" /> or <see cref="T:System.Xml.Schema.XmlSchemaComplexContent" /> classes.</returns>
		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06003A60 RID: 14944 RVA: 0x0014CBF9 File Offset: 0x0014ADF9
		// (set) Token: 0x06003A61 RID: 14945 RVA: 0x0014CC01 File Offset: 0x0014AE01
		[XmlElement("complexContent", typeof(XmlSchemaComplexContent))]
		[XmlElement("simpleContent", typeof(XmlSchemaSimpleContent))]
		public XmlSchemaContentModel ContentModel
		{
			get
			{
				return this.contentModel;
			}
			set
			{
				this.contentModel = value;
			}
		}

		/// <summary>Gets or sets the compositor type as one of the <see cref="T:System.Xml.Schema.XmlSchemaGroupRef" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</summary>
		/// <returns>The compositor type.</returns>
		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x0014CC0A File Offset: 0x0014AE0A
		// (set) Token: 0x06003A63 RID: 14947 RVA: 0x0014CC12 File Offset: 0x0014AE12
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		[XmlElement("all", typeof(XmlSchemaAll))]
		public XmlSchemaParticle Particle
		{
			get
			{
				return this.particle;
			}
			set
			{
				this.particle = value;
			}
		}

		/// <summary>Gets the collection of attributes for the complex type.</summary>
		/// <returns>Contains <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> and <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" /> classes.</returns>
		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06003A64 RID: 14948 RVA: 0x0014CC1B File Offset: 0x0014AE1B
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new XmlSchemaObjectCollection();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the value for the <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the complex type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the complex type.</returns>
		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06003A65 RID: 14949 RVA: 0x0014CC36 File Offset: 0x0014AE36
		// (set) Token: 0x06003A66 RID: 14950 RVA: 0x0014CC3E File Offset: 0x0014AE3E
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

		/// <summary>Gets the content model of the complex type which holds the post-compilation value.</summary>
		/// <returns>The post-compilation value of the content model for the complex type.</returns>
		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06003A67 RID: 14951 RVA: 0x0014CC47 File Offset: 0x0014AE47
		[XmlIgnore]
		public XmlSchemaContentType ContentType
		{
			get
			{
				return base.SchemaContentType;
			}
		}

		/// <summary>Gets the particle that holds the post-compilation value of the <see cref="P:System.Xml.Schema.XmlSchemaComplexType.ContentType" /> particle.</summary>
		/// <returns>The particle for the content type. The post-compilation value of the <see cref="P:System.Xml.Schema.XmlSchemaComplexType.ContentType" /> particle.</returns>
		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06003A68 RID: 14952 RVA: 0x0014CC4F File Offset: 0x0014AE4F
		[XmlIgnore]
		public XmlSchemaParticle ContentTypeParticle
		{
			get
			{
				return this.contentTypeParticle;
			}
		}

		/// <summary>Gets the value after the type has been compiled to the post-schema-validation information set (infoset). This value indicates how the type is enforced when <see langword="xsi:type" /> is used in the instance document.</summary>
		/// <returns>The post-schema-validated infoset value. The default is <see langword="BlockDefault" /> value on the <see langword="schema" /> element.</returns>
		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06003A69 RID: 14953 RVA: 0x0014CC57 File Offset: 0x0014AE57
		[XmlIgnore]
		public XmlSchemaDerivationMethod BlockResolved
		{
			get
			{
				return this.blockResolved;
			}
		}

		/// <summary>Gets the collection of all the complied attributes of this complex type and its base types.</summary>
		/// <returns>The collection of all the attributes from this complex type and its base types. The post-compilation value of the <see langword="AttributeUses" /> property.</returns>
		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06003A6A RID: 14954 RVA: 0x0014CC5F File Offset: 0x0014AE5F
		[XmlIgnore]
		public XmlSchemaObjectTable AttributeUses
		{
			get
			{
				if (this.attributeUses == null)
				{
					this.attributeUses = new XmlSchemaObjectTable();
				}
				return this.attributeUses;
			}
		}

		/// <summary>Gets the post-compilation value for <see langword="anyAttribute" /> for this complex type and its base type(s).</summary>
		/// <returns>The post-compilation value of the <see langword="anyAttribute" /> element.</returns>
		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06003A6B RID: 14955 RVA: 0x0014CC7A File Offset: 0x0014AE7A
		[XmlIgnore]
		public XmlSchemaAnyAttribute AttributeWildcard
		{
			get
			{
				return this.attributeWildcard;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06003A6C RID: 14956 RVA: 0x0014CC82 File Offset: 0x0014AE82
		[XmlIgnore]
		internal XmlSchemaObjectTable LocalElements
		{
			get
			{
				if (this.localElements == null)
				{
					this.localElements = new XmlSchemaObjectTable();
				}
				return this.localElements;
			}
		}

		// Token: 0x06003A6D RID: 14957 RVA: 0x0014CC9D File Offset: 0x0014AE9D
		internal void SetContentTypeParticle(XmlSchemaParticle value)
		{
			this.contentTypeParticle = value;
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x0014CCA6 File Offset: 0x0014AEA6
		internal void SetBlockResolved(XmlSchemaDerivationMethod value)
		{
			this.blockResolved = value;
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x0014CCAF File Offset: 0x0014AEAF
		internal void SetAttributeWildcard(XmlSchemaAnyAttribute value)
		{
			this.attributeWildcard = value;
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06003A70 RID: 14960 RVA: 0x0014CCB8 File Offset: 0x0014AEB8
		// (set) Token: 0x06003A71 RID: 14961 RVA: 0x0014CCC5 File Offset: 0x0014AEC5
		internal bool HasWildCard
		{
			get
			{
				return (this.pvFlags & 1) > 0;
			}
			set
			{
				if (value)
				{
					this.pvFlags |= 1;
					return;
				}
				this.pvFlags = (byte)((int)this.pvFlags & -2);
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06003A72 RID: 14962 RVA: 0x0014CCEC File Offset: 0x0014AEEC
		internal override XmlQualifiedName DerivedFrom
		{
			get
			{
				if (this.contentModel == null)
				{
					return XmlQualifiedName.Empty;
				}
				if (this.contentModel.Content is XmlSchemaComplexContentRestriction)
				{
					return ((XmlSchemaComplexContentRestriction)this.contentModel.Content).BaseTypeName;
				}
				if (this.contentModel.Content is XmlSchemaComplexContentExtension)
				{
					return ((XmlSchemaComplexContentExtension)this.contentModel.Content).BaseTypeName;
				}
				if (this.contentModel.Content is XmlSchemaSimpleContentRestriction)
				{
					return ((XmlSchemaSimpleContentRestriction)this.contentModel.Content).BaseTypeName;
				}
				if (this.contentModel.Content is XmlSchemaSimpleContentExtension)
				{
					return ((XmlSchemaSimpleContentExtension)this.contentModel.Content).BaseTypeName;
				}
				return XmlQualifiedName.Empty;
			}
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x0014CDAC File Offset: 0x0014AFAC
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x0014CDB8 File Offset: 0x0014AFB8
		internal bool ContainsIdAttribute(bool findAll)
		{
			int num = 0;
			foreach (object obj in this.AttributeUses.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj;
				if (xmlSchemaAttribute.Use != XmlSchemaUse.Prohibited)
				{
					XmlSchemaDatatype datatype = xmlSchemaAttribute.Datatype;
					if (datatype != null && datatype.TypeCode == XmlTypeCode.Id)
					{
						num++;
						if (num > 1)
						{
							break;
						}
					}
				}
			}
			if (!findAll)
			{
				return num > 0;
			}
			return num > 1;
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x0014CE48 File Offset: 0x0014B048
		internal override XmlSchemaObject Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x06003A76 RID: 14966 RVA: 0x0014CE54 File Offset: 0x0014B054
		internal XmlSchemaObject Clone(XmlSchema parentSchema)
		{
			XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)base.MemberwiseClone();
			if (xmlSchemaComplexType.ContentModel != null)
			{
				XmlSchemaSimpleContent xmlSchemaSimpleContent = xmlSchemaComplexType.ContentModel as XmlSchemaSimpleContent;
				if (xmlSchemaSimpleContent != null)
				{
					XmlSchemaSimpleContent xmlSchemaSimpleContent2 = (XmlSchemaSimpleContent)xmlSchemaSimpleContent.Clone();
					XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = xmlSchemaSimpleContent.Content as XmlSchemaSimpleContentExtension;
					if (xmlSchemaSimpleContentExtension != null)
					{
						XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension2 = (XmlSchemaSimpleContentExtension)xmlSchemaSimpleContentExtension.Clone();
						xmlSchemaSimpleContentExtension2.BaseTypeName = xmlSchemaSimpleContentExtension.BaseTypeName.Clone();
						xmlSchemaSimpleContentExtension2.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaSimpleContentExtension.Attributes));
						xmlSchemaSimpleContent2.Content = xmlSchemaSimpleContentExtension2;
					}
					else
					{
						XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = (XmlSchemaSimpleContentRestriction)xmlSchemaSimpleContent.Content;
						XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction2 = (XmlSchemaSimpleContentRestriction)xmlSchemaSimpleContentRestriction.Clone();
						xmlSchemaSimpleContentRestriction2.BaseTypeName = xmlSchemaSimpleContentRestriction.BaseTypeName.Clone();
						xmlSchemaSimpleContentRestriction2.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaSimpleContentRestriction.Attributes));
						xmlSchemaSimpleContent2.Content = xmlSchemaSimpleContentRestriction2;
					}
					xmlSchemaComplexType.ContentModel = xmlSchemaSimpleContent2;
				}
				else
				{
					XmlSchemaComplexContent xmlSchemaComplexContent = (XmlSchemaComplexContent)xmlSchemaComplexType.ContentModel;
					XmlSchemaComplexContent xmlSchemaComplexContent2 = (XmlSchemaComplexContent)xmlSchemaComplexContent.Clone();
					XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = xmlSchemaComplexContent.Content as XmlSchemaComplexContentExtension;
					if (xmlSchemaComplexContentExtension != null)
					{
						XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension2 = (XmlSchemaComplexContentExtension)xmlSchemaComplexContentExtension.Clone();
						xmlSchemaComplexContentExtension2.BaseTypeName = xmlSchemaComplexContentExtension.BaseTypeName.Clone();
						xmlSchemaComplexContentExtension2.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaComplexContentExtension.Attributes));
						if (XmlSchemaComplexType.HasParticleRef(xmlSchemaComplexContentExtension.Particle, parentSchema))
						{
							xmlSchemaComplexContentExtension2.Particle = XmlSchemaComplexType.CloneParticle(xmlSchemaComplexContentExtension.Particle, parentSchema);
						}
						xmlSchemaComplexContent2.Content = xmlSchemaComplexContentExtension2;
					}
					else
					{
						XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = xmlSchemaComplexContent.Content as XmlSchemaComplexContentRestriction;
						XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction2 = (XmlSchemaComplexContentRestriction)xmlSchemaComplexContentRestriction.Clone();
						xmlSchemaComplexContentRestriction2.BaseTypeName = xmlSchemaComplexContentRestriction.BaseTypeName.Clone();
						xmlSchemaComplexContentRestriction2.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaComplexContentRestriction.Attributes));
						if (XmlSchemaComplexType.HasParticleRef(xmlSchemaComplexContentRestriction2.Particle, parentSchema))
						{
							xmlSchemaComplexContentRestriction2.Particle = XmlSchemaComplexType.CloneParticle(xmlSchemaComplexContentRestriction2.Particle, parentSchema);
						}
						xmlSchemaComplexContent2.Content = xmlSchemaComplexContentRestriction2;
					}
					xmlSchemaComplexType.ContentModel = xmlSchemaComplexContent2;
				}
			}
			else
			{
				if (XmlSchemaComplexType.HasParticleRef(xmlSchemaComplexType.Particle, parentSchema))
				{
					xmlSchemaComplexType.Particle = XmlSchemaComplexType.CloneParticle(xmlSchemaComplexType.Particle, parentSchema);
				}
				xmlSchemaComplexType.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaComplexType.Attributes));
			}
			xmlSchemaComplexType.ClearCompiledState();
			return xmlSchemaComplexType;
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x0014D074 File Offset: 0x0014B274
		private void ClearCompiledState()
		{
			this.attributeUses = null;
			this.localElements = null;
			this.attributeWildcard = null;
			this.contentTypeParticle = XmlSchemaParticle.Empty;
			this.blockResolved = XmlSchemaDerivationMethod.None;
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x0014D0A4 File Offset: 0x0014B2A4
		internal static XmlSchemaObjectCollection CloneAttributes(XmlSchemaObjectCollection attributes)
		{
			if (XmlSchemaComplexType.HasAttributeQNameRef(attributes))
			{
				XmlSchemaObjectCollection xmlSchemaObjectCollection = attributes.Clone();
				for (int i = 0; i < attributes.Count; i++)
				{
					XmlSchemaObject xmlSchemaObject = attributes[i];
					XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = xmlSchemaObject as XmlSchemaAttributeGroupRef;
					if (xmlSchemaAttributeGroupRef != null)
					{
						XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef2 = (XmlSchemaAttributeGroupRef)xmlSchemaAttributeGroupRef.Clone();
						xmlSchemaAttributeGroupRef2.RefName = xmlSchemaAttributeGroupRef.RefName.Clone();
						xmlSchemaObjectCollection[i] = xmlSchemaAttributeGroupRef2;
					}
					else
					{
						XmlSchemaAttribute xmlSchemaAttribute = xmlSchemaObject as XmlSchemaAttribute;
						if (!xmlSchemaAttribute.RefName.IsEmpty || !xmlSchemaAttribute.SchemaTypeName.IsEmpty)
						{
							xmlSchemaObjectCollection[i] = xmlSchemaAttribute.Clone();
						}
					}
				}
				return xmlSchemaObjectCollection;
			}
			return attributes;
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x0014D14C File Offset: 0x0014B34C
		private static XmlSchemaObjectCollection CloneGroupBaseParticles(XmlSchemaObjectCollection groupBaseParticles, XmlSchema parentSchema)
		{
			XmlSchemaObjectCollection xmlSchemaObjectCollection = groupBaseParticles.Clone();
			for (int i = 0; i < groupBaseParticles.Count; i++)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)groupBaseParticles[i];
				xmlSchemaObjectCollection[i] = XmlSchemaComplexType.CloneParticle(xmlSchemaParticle, parentSchema);
			}
			return xmlSchemaObjectCollection;
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x0014D190 File Offset: 0x0014B390
		internal static XmlSchemaParticle CloneParticle(XmlSchemaParticle particle, XmlSchema parentSchema)
		{
			XmlSchemaGroupBase xmlSchemaGroupBase = particle as XmlSchemaGroupBase;
			if (xmlSchemaGroupBase != null)
			{
				XmlSchemaObjectCollection items = XmlSchemaComplexType.CloneGroupBaseParticles(xmlSchemaGroupBase.Items, parentSchema);
				XmlSchemaGroupBase xmlSchemaGroupBase2 = (XmlSchemaGroupBase)xmlSchemaGroupBase.Clone();
				xmlSchemaGroupBase2.SetItems(items);
				return xmlSchemaGroupBase2;
			}
			if (particle is XmlSchemaGroupRef)
			{
				XmlSchemaGroupRef xmlSchemaGroupRef = (XmlSchemaGroupRef)particle.Clone();
				xmlSchemaGroupRef.RefName = xmlSchemaGroupRef.RefName.Clone();
				return xmlSchemaGroupRef;
			}
			XmlSchemaElement xmlSchemaElement = particle as XmlSchemaElement;
			if (xmlSchemaElement != null && (!xmlSchemaElement.RefName.IsEmpty || !xmlSchemaElement.SchemaTypeName.IsEmpty || XmlSchemaComplexType.GetResolvedElementForm(parentSchema, xmlSchemaElement) == XmlSchemaForm.Qualified))
			{
				return (XmlSchemaElement)xmlSchemaElement.Clone(parentSchema);
			}
			return particle;
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x0014D228 File Offset: 0x0014B428
		private static XmlSchemaForm GetResolvedElementForm(XmlSchema parentSchema, XmlSchemaElement element)
		{
			if (element.Form == XmlSchemaForm.None && parentSchema != null)
			{
				return parentSchema.ElementFormDefault;
			}
			return element.Form;
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x0014D244 File Offset: 0x0014B444
		internal static bool HasParticleRef(XmlSchemaParticle particle, XmlSchema parentSchema)
		{
			XmlSchemaGroupBase xmlSchemaGroupBase = particle as XmlSchemaGroupBase;
			if (xmlSchemaGroupBase != null)
			{
				bool flag = false;
				int num = 0;
				while (num < xmlSchemaGroupBase.Items.Count && !flag)
				{
					XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)xmlSchemaGroupBase.Items[num++];
					if (xmlSchemaParticle is XmlSchemaGroupRef)
					{
						flag = true;
					}
					else
					{
						XmlSchemaElement xmlSchemaElement = xmlSchemaParticle as XmlSchemaElement;
						flag = ((xmlSchemaElement != null && (!xmlSchemaElement.RefName.IsEmpty || !xmlSchemaElement.SchemaTypeName.IsEmpty || XmlSchemaComplexType.GetResolvedElementForm(parentSchema, xmlSchemaElement) == XmlSchemaForm.Qualified)) || XmlSchemaComplexType.HasParticleRef(xmlSchemaParticle, parentSchema));
					}
				}
				return flag;
			}
			return particle is XmlSchemaGroupRef;
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x0014D2E0 File Offset: 0x0014B4E0
		internal static bool HasAttributeQNameRef(XmlSchemaObjectCollection attributes)
		{
			for (int i = 0; i < attributes.Count; i++)
			{
				if (attributes[i] is XmlSchemaAttributeGroupRef)
				{
					return true;
				}
				XmlSchemaAttribute xmlSchemaAttribute = attributes[i] as XmlSchemaAttribute;
				if (!xmlSchemaAttribute.RefName.IsEmpty || !xmlSchemaAttribute.SchemaTypeName.IsEmpty)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002B03 RID: 11011
		private XmlSchemaDerivationMethod block = XmlSchemaDerivationMethod.None;

		// Token: 0x04002B04 RID: 11012
		private XmlSchemaContentModel contentModel;

		// Token: 0x04002B05 RID: 11013
		private XmlSchemaParticle particle;

		// Token: 0x04002B06 RID: 11014
		private XmlSchemaObjectCollection attributes;

		// Token: 0x04002B07 RID: 11015
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04002B08 RID: 11016
		private XmlSchemaParticle contentTypeParticle = XmlSchemaParticle.Empty;

		// Token: 0x04002B09 RID: 11017
		private XmlSchemaDerivationMethod blockResolved;

		// Token: 0x04002B0A RID: 11018
		private XmlSchemaObjectTable localElements;

		// Token: 0x04002B0B RID: 11019
		private XmlSchemaObjectTable attributeUses;

		// Token: 0x04002B0C RID: 11020
		private XmlSchemaAnyAttribute attributeWildcard;

		// Token: 0x04002B0D RID: 11021
		private static XmlSchemaComplexType anyTypeLax = XmlSchemaComplexType.CreateAnyType(XmlSchemaContentProcessing.Lax);

		// Token: 0x04002B0E RID: 11022
		private static XmlSchemaComplexType anyTypeSkip = XmlSchemaComplexType.CreateAnyType(XmlSchemaContentProcessing.Skip);

		// Token: 0x04002B0F RID: 11023
		private static XmlSchemaComplexType untypedAnyType = new XmlSchemaComplexType();

		// Token: 0x04002B10 RID: 11024
		private byte pvFlags;

		// Token: 0x04002B11 RID: 11025
		private const byte wildCardMask = 1;

		// Token: 0x04002B12 RID: 11026
		private const byte isMixedMask = 2;

		// Token: 0x04002B13 RID: 11027
		private const byte isAbstractMask = 4;
	}
}
