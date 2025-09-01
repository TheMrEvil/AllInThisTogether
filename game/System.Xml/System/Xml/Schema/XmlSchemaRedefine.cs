using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="redefine" /> element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to allow simple and complex types, groups and attribute groups from external schema files to be redefined in the current schema. This class can also be used to provide versioning for the schema elements.</summary>
	// Token: 0x020005D7 RID: 1495
	public class XmlSchemaRedefine : XmlSchemaExternal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaRedefine" /> class.</summary>
		// Token: 0x06003BDA RID: 15322 RVA: 0x0014EF63 File Offset: 0x0014D163
		public XmlSchemaRedefine()
		{
			base.Compositor = Compositor.Redefine;
		}

		/// <summary>Gets the collection of the following classes: <see cref="T:System.Xml.Schema.XmlSchemaAnnotation" />, <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroup" />, <see cref="T:System.Xml.Schema.XmlSchemaComplexType" />, <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" />, and <see cref="T:System.Xml.Schema.XmlSchemaGroup" />.</summary>
		/// <returns>The elements contained within the redefine element.</returns>
		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06003BDB RID: 15323 RVA: 0x0014EF9E File Offset: 0x0014D19E
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		[XmlElement("annotation", typeof(XmlSchemaAnnotation))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroup))]
		[XmlElement("complexType", typeof(XmlSchemaComplexType))]
		[XmlElement("group", typeof(XmlSchemaGroup))]
		public XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> , for all attributes in the schema, which holds the post-compilation value of the <see langword="AttributeGroups" /> property.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> for all attributes in the schema. The post-compilation value of the <see langword="AttributeGroups" /> property.</returns>
		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06003BDC RID: 15324 RVA: 0x0014EFA6 File Offset: 0x0014D1A6
		[XmlIgnore]
		public XmlSchemaObjectTable AttributeGroups
		{
			get
			{
				return this.attributeGroups;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />, for all simple and complex types in the schema, which holds the post-compilation value of the <see langword="SchemaTypes" /> property.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> for all schema types in the schema. The post-compilation value of the <see langword="SchemaTypes" /> property.</returns>
		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06003BDD RID: 15325 RVA: 0x0014EFAE File Offset: 0x0014D1AE
		[XmlIgnore]
		public XmlSchemaObjectTable SchemaTypes
		{
			get
			{
				return this.types;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />, for all groups in the schema, which holds the post-compilation value of the <see langword="Groups" /> property.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> for all groups in the schema. The post-compilation value of the <see langword="Groups" /> property.</returns>
		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06003BDE RID: 15326 RVA: 0x0014EFB6 File Offset: 0x0014D1B6
		[XmlIgnore]
		public XmlSchemaObjectTable Groups
		{
			get
			{
				return this.groups;
			}
		}

		// Token: 0x06003BDF RID: 15327 RVA: 0x0014EFBE File Offset: 0x0014D1BE
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.items.Add(annotation);
		}

		// Token: 0x04002BA3 RID: 11171
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();

		// Token: 0x04002BA4 RID: 11172
		private XmlSchemaObjectTable attributeGroups = new XmlSchemaObjectTable();

		// Token: 0x04002BA5 RID: 11173
		private XmlSchemaObjectTable types = new XmlSchemaObjectTable();

		// Token: 0x04002BA6 RID: 11174
		private XmlSchemaObjectTable groups = new XmlSchemaObjectTable();
	}
}
