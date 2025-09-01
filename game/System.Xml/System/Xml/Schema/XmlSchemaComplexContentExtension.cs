using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="extension" /> element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is for complex types with complex content model derived by extension. It extends the complex type by adding attributes or elements.</summary>
	// Token: 0x020005A1 RID: 1441
	public class XmlSchemaComplexContentExtension : XmlSchemaContent
	{
		/// <summary>Gets or sets the name of the complex type from which this type is derived by extension.</summary>
		/// <returns>The name of the complex type from which this type is derived by extension.</returns>
		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06003A41 RID: 14913 RVA: 0x0014C8C4 File Offset: 0x0014AAC4
		// (set) Token: 0x06003A42 RID: 14914 RVA: 0x0014C8CC File Offset: 0x0014AACC
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

		/// <summary>Gets or sets one of the <see cref="T:System.Xml.Schema.XmlSchemaGroupRef" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaGroupRef" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</returns>
		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06003A43 RID: 14915 RVA: 0x0014C8E5 File Offset: 0x0014AAE5
		// (set) Token: 0x06003A44 RID: 14916 RVA: 0x0014C8ED File Offset: 0x0014AAED
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		[XmlElement("all", typeof(XmlSchemaAll))]
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
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

		/// <summary>Gets the collection of attributes for the complex content. Contains <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> and <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" /> elements.</summary>
		/// <returns>The collection of attributes for the complex content.</returns>
		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06003A45 RID: 14917 RVA: 0x0014C8F6 File Offset: 0x0014AAF6
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the complex content model.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the complex content model.</returns>
		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06003A46 RID: 14918 RVA: 0x0014C8FE File Offset: 0x0014AAFE
		// (set) Token: 0x06003A47 RID: 14919 RVA: 0x0014C906 File Offset: 0x0014AB06
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

		// Token: 0x06003A48 RID: 14920 RVA: 0x0014C90F File Offset: 0x0014AB0F
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaComplexContentExtension" /> class.</summary>
		// Token: 0x06003A49 RID: 14921 RVA: 0x0014C918 File Offset: 0x0014AB18
		public XmlSchemaComplexContentExtension()
		{
		}

		// Token: 0x04002AFB RID: 11003
		private XmlSchemaParticle particle;

		// Token: 0x04002AFC RID: 11004
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x04002AFD RID: 11005
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04002AFE RID: 11006
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;
	}
}
