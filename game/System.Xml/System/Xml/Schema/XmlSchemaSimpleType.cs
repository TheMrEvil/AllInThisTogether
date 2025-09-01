using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="simpleType" /> element for simple content from XML Schema as specified by the World Wide Web Consortium (W3C). This class defines a simple type. Simple types can specify information and constraints for the value of attributes or elements with text-only content.</summary>
	// Token: 0x020005DD RID: 1501
	public class XmlSchemaSimpleType : XmlSchemaType
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> class.</summary>
		// Token: 0x06003C32 RID: 15410 RVA: 0x00150FD4 File Offset: 0x0014F1D4
		public XmlSchemaSimpleType()
		{
		}

		/// <summary>Gets or sets one of <see cref="T:System.Xml.Schema.XmlSchemaSimpleTypeUnion" />, <see cref="T:System.Xml.Schema.XmlSchemaSimpleTypeList" />, or <see cref="T:System.Xml.Schema.XmlSchemaSimpleTypeRestriction" />.</summary>
		/// <returns>One of <see langword="XmlSchemaSimpleTypeUnion" />, <see langword="XmlSchemaSimpleTypeList" />, or <see langword="XmlSchemaSimpleTypeRestriction" />.</returns>
		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06003C33 RID: 15411 RVA: 0x00150FDC File Offset: 0x0014F1DC
		// (set) Token: 0x06003C34 RID: 15412 RVA: 0x00150FE4 File Offset: 0x0014F1E4
		[XmlElement("restriction", typeof(XmlSchemaSimpleTypeRestriction))]
		[XmlElement("list", typeof(XmlSchemaSimpleTypeList))]
		[XmlElement("union", typeof(XmlSchemaSimpleTypeUnion))]
		public XmlSchemaSimpleTypeContent Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06003C35 RID: 15413 RVA: 0x00150FED File Offset: 0x0014F1ED
		internal override XmlQualifiedName DerivedFrom
		{
			get
			{
				if (this.content == null)
				{
					return XmlQualifiedName.Empty;
				}
				if (this.content is XmlSchemaSimpleTypeRestriction)
				{
					return ((XmlSchemaSimpleTypeRestriction)this.content).BaseTypeName;
				}
				return XmlQualifiedName.Empty;
			}
		}

		// Token: 0x06003C36 RID: 15414 RVA: 0x00151020 File Offset: 0x0014F220
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = (XmlSchemaSimpleType)base.MemberwiseClone();
			if (this.content != null)
			{
				xmlSchemaSimpleType.Content = (XmlSchemaSimpleTypeContent)this.content.Clone();
			}
			return xmlSchemaSimpleType;
		}

		// Token: 0x04002BC5 RID: 11205
		private XmlSchemaSimpleTypeContent content;
	}
}
