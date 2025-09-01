using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) <see langword="annotation" /> element.</summary>
	// Token: 0x02000594 RID: 1428
	public class XmlSchemaAnnotation : XmlSchemaObject
	{
		/// <summary>Gets or sets the string id.</summary>
		/// <returns>The string id. The default is <see langword="String.Empty" />.Optional.</returns>
		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060039AB RID: 14763 RVA: 0x0014BB96 File Offset: 0x00149D96
		// (set) Token: 0x060039AC RID: 14764 RVA: 0x0014BB9E File Offset: 0x00149D9E
		[XmlAttribute("id", DataType = "ID")]
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		/// <summary>Gets the <see langword="Items" /> collection that is used to store the <see langword="appinfo" /> and <see langword="documentation" /> child elements.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectCollection" /> of <see langword="appinfo" /> and <see langword="documentation" /> child elements.</returns>
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x060039AD RID: 14765 RVA: 0x0014BBA7 File Offset: 0x00149DA7
		[XmlElement("documentation", typeof(XmlSchemaDocumentation))]
		[XmlElement("appinfo", typeof(XmlSchemaAppInfo))]
		public XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets or sets the qualified attributes that do not belong to the schema's target namespace.</summary>
		/// <returns>An array of <see cref="T:System.Xml.XmlAttribute" /> objects that do not belong to the schema's target namespace.</returns>
		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060039AE RID: 14766 RVA: 0x0014BBAF File Offset: 0x00149DAF
		// (set) Token: 0x060039AF RID: 14767 RVA: 0x0014BBB7 File Offset: 0x00149DB7
		[XmlAnyAttribute]
		public XmlAttribute[] UnhandledAttributes
		{
			get
			{
				return this.moreAttributes;
			}
			set
			{
				this.moreAttributes = value;
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060039B0 RID: 14768 RVA: 0x0014BBC0 File Offset: 0x00149DC0
		// (set) Token: 0x060039B1 RID: 14769 RVA: 0x0014BBC8 File Offset: 0x00149DC8
		[XmlIgnore]
		internal override string IdAttribute
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x0014BBB7 File Offset: 0x00149DB7
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaAnnotation" /> class.</summary>
		// Token: 0x060039B3 RID: 14771 RVA: 0x0014BBD1 File Offset: 0x00149DD1
		public XmlSchemaAnnotation()
		{
		}

		// Token: 0x04002ACB RID: 10955
		private string id;

		// Token: 0x04002ACC RID: 10956
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();

		// Token: 0x04002ACD RID: 10957
		private XmlAttribute[] moreAttributes;
	}
}
