using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="list" /> element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to define a <see langword="simpleType" /> element as a list of values of a specified data type.</summary>
	// Token: 0x020005DF RID: 1503
	public class XmlSchemaSimpleTypeList : XmlSchemaSimpleTypeContent
	{
		/// <summary>Gets or sets the name of a built-in data type or <see langword="simpleType" /> element defined in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>The type name of the simple type list.</returns>
		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06003C38 RID: 15416 RVA: 0x00151058 File Offset: 0x0014F258
		// (set) Token: 0x06003C39 RID: 15417 RVA: 0x00151060 File Offset: 0x0014F260
		[XmlAttribute("itemType")]
		public XmlQualifiedName ItemTypeName
		{
			get
			{
				return this.itemTypeName;
			}
			set
			{
				this.itemTypeName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Gets or sets the <see langword="simpleType" /> element that is derived from the type specified by the base value.</summary>
		/// <returns>The item type for the simple type element.</returns>
		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06003C3A RID: 15418 RVA: 0x00151079 File Offset: 0x0014F279
		// (set) Token: 0x06003C3B RID: 15419 RVA: 0x00151081 File Offset: 0x0014F281
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		public XmlSchemaSimpleType ItemType
		{
			get
			{
				return this.itemType;
			}
			set
			{
				this.itemType = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> representing the type of the <see langword="simpleType" /> element based on the <see cref="P:System.Xml.Schema.XmlSchemaSimpleTypeList.ItemType" /> and <see cref="P:System.Xml.Schema.XmlSchemaSimpleTypeList.ItemTypeName" /> values of the simple type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> representing the type of the <see langword="simpleType" /> element.</returns>
		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06003C3C RID: 15420 RVA: 0x0015108A File Offset: 0x0014F28A
		// (set) Token: 0x06003C3D RID: 15421 RVA: 0x00151092 File Offset: 0x0014F292
		[XmlIgnore]
		public XmlSchemaSimpleType BaseItemType
		{
			get
			{
				return this.baseItemType;
			}
			set
			{
				this.baseItemType = value;
			}
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x0015109B File Offset: 0x0014F29B
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = (XmlSchemaSimpleTypeList)base.MemberwiseClone();
			xmlSchemaSimpleTypeList.ItemTypeName = this.itemTypeName.Clone();
			return xmlSchemaSimpleTypeList;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaSimpleTypeList" /> class.</summary>
		// Token: 0x06003C3F RID: 15423 RVA: 0x001510B9 File Offset: 0x0014F2B9
		public XmlSchemaSimpleTypeList()
		{
		}

		// Token: 0x04002BC6 RID: 11206
		private XmlQualifiedName itemTypeName = XmlQualifiedName.Empty;

		// Token: 0x04002BC7 RID: 11207
		private XmlSchemaSimpleType itemType;

		// Token: 0x04002BC8 RID: 11208
		private XmlSchemaSimpleType baseItemType;
	}
}
