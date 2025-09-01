using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="restriction" /> element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is for complex types with a complex content model derived by restriction. It restricts the contents of the complex type to a subset of the inherited complex type.</summary>
	// Token: 0x020005A2 RID: 1442
	public class XmlSchemaComplexContentRestriction : XmlSchemaContent
	{
		/// <summary>Gets or sets the name of a complex type from which this type is derived by restriction.</summary>
		/// <returns>The name of the complex type from which this type is derived by restriction.</returns>
		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06003A4A RID: 14922 RVA: 0x0014C936 File Offset: 0x0014AB36
		// (set) Token: 0x06003A4B RID: 14923 RVA: 0x0014C93E File Offset: 0x0014AB3E
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
		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06003A4C RID: 14924 RVA: 0x0014C957 File Offset: 0x0014AB57
		// (set) Token: 0x06003A4D RID: 14925 RVA: 0x0014C95F File Offset: 0x0014AB5F
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("all", typeof(XmlSchemaAll))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
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

		/// <summary>Gets the collection of attributes for the complex type. Contains the <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> and <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" /> elements.</summary>
		/// <returns>The collection of attributes for the complex type.</returns>
		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06003A4E RID: 14926 RVA: 0x0014C968 File Offset: 0x0014AB68
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
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06003A4F RID: 14927 RVA: 0x0014C970 File Offset: 0x0014AB70
		// (set) Token: 0x06003A50 RID: 14928 RVA: 0x0014C978 File Offset: 0x0014AB78
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

		// Token: 0x06003A51 RID: 14929 RVA: 0x0014C981 File Offset: 0x0014AB81
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaComplexContentRestriction" /> class.</summary>
		// Token: 0x06003A52 RID: 14930 RVA: 0x0014C98A File Offset: 0x0014AB8A
		public XmlSchemaComplexContentRestriction()
		{
		}

		// Token: 0x04002AFF RID: 11007
		private XmlSchemaParticle particle;

		// Token: 0x04002B00 RID: 11008
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x04002B01 RID: 11009
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04002B02 RID: 11010
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;
	}
}
