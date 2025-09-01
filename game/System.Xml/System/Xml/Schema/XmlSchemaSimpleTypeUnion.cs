using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="union" /> element for simple types from XML Schema as specified by the World Wide Web Consortium (W3C). A <see langword="union" /> datatype can be used to specify the content of a <see langword="simpleType" />. The value of the <see langword="simpleType" /> element must be any one of a set of alternative datatypes specified in the union. Union types are always derived types and must comprise at least two alternative datatypes.</summary>
	// Token: 0x020005E1 RID: 1505
	public class XmlSchemaSimpleTypeUnion : XmlSchemaSimpleTypeContent
	{
		/// <summary>Gets the collection of base types.</summary>
		/// <returns>The collection of simple type base values.</returns>
		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06003C47 RID: 15431 RVA: 0x00151142 File Offset: 0x0014F342
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		public XmlSchemaObjectCollection BaseTypes
		{
			get
			{
				return this.baseTypes;
			}
		}

		/// <summary>Gets or sets the array of qualified member names of built-in data types or <see langword="simpleType" /> elements defined in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>An array that consists of a list of members of built-in data types or simple types.</returns>
		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06003C48 RID: 15432 RVA: 0x0015114A File Offset: 0x0014F34A
		// (set) Token: 0x06003C49 RID: 15433 RVA: 0x00151152 File Offset: 0x0014F352
		[XmlAttribute("memberTypes")]
		public XmlQualifiedName[] MemberTypes
		{
			get
			{
				return this.memberTypes;
			}
			set
			{
				this.memberTypes = value;
			}
		}

		/// <summary>Gets an array of <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> objects representing the type of the <see langword="simpleType" /> element based on the <see cref="P:System.Xml.Schema.XmlSchemaSimpleTypeUnion.BaseTypes" /> and <see cref="P:System.Xml.Schema.XmlSchemaSimpleTypeUnion.MemberTypes" /> values of the simple type.</summary>
		/// <returns>An array of <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> objects representing the type of the <see langword="simpleType" /> element.</returns>
		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06003C4A RID: 15434 RVA: 0x0015115B File Offset: 0x0014F35B
		[XmlIgnore]
		public XmlSchemaSimpleType[] BaseMemberTypes
		{
			get
			{
				return this.baseMemberTypes;
			}
		}

		// Token: 0x06003C4B RID: 15435 RVA: 0x00151163 File Offset: 0x0014F363
		internal void SetBaseMemberTypes(XmlSchemaSimpleType[] baseMemberTypes)
		{
			this.baseMemberTypes = baseMemberTypes;
		}

		// Token: 0x06003C4C RID: 15436 RVA: 0x0015116C File Offset: 0x0014F36C
		internal override XmlSchemaObject Clone()
		{
			if (this.memberTypes != null && this.memberTypes.Length != 0)
			{
				XmlSchemaSimpleTypeUnion xmlSchemaSimpleTypeUnion = (XmlSchemaSimpleTypeUnion)base.MemberwiseClone();
				XmlQualifiedName[] array = new XmlQualifiedName[this.memberTypes.Length];
				for (int i = 0; i < this.memberTypes.Length; i++)
				{
					array[i] = this.memberTypes[i].Clone();
				}
				xmlSchemaSimpleTypeUnion.MemberTypes = array;
				return xmlSchemaSimpleTypeUnion;
			}
			return this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaSimpleTypeUnion" /> class.</summary>
		// Token: 0x06003C4D RID: 15437 RVA: 0x001511D1 File Offset: 0x0014F3D1
		public XmlSchemaSimpleTypeUnion()
		{
		}

		// Token: 0x04002BCC RID: 11212
		private XmlSchemaObjectCollection baseTypes = new XmlSchemaObjectCollection();

		// Token: 0x04002BCD RID: 11213
		private XmlQualifiedName[] memberTypes;

		// Token: 0x04002BCE RID: 11214
		private XmlSchemaSimpleType[] baseMemberTypes;
	}
}
