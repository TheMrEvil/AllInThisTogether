using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>The base class for any element that can contain annotation elements.</summary>
	// Token: 0x02000593 RID: 1427
	public class XmlSchemaAnnotated : XmlSchemaObject
	{
		/// <summary>Gets or sets the string id.</summary>
		/// <returns>The string id. The default is <see langword="String.Empty" />.Optional.</returns>
		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060039A0 RID: 14752 RVA: 0x0014BB4A File Offset: 0x00149D4A
		// (set) Token: 0x060039A1 RID: 14753 RVA: 0x0014BB52 File Offset: 0x00149D52
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

		/// <summary>Gets or sets the <see langword="annotation" /> property.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaAnnotation" /> representing the <see langword="annotation" /> property.</returns>
		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060039A2 RID: 14754 RVA: 0x0014BB5B File Offset: 0x00149D5B
		// (set) Token: 0x060039A3 RID: 14755 RVA: 0x0014BB63 File Offset: 0x00149D63
		[XmlElement("annotation", typeof(XmlSchemaAnnotation))]
		public XmlSchemaAnnotation Annotation
		{
			get
			{
				return this.annotation;
			}
			set
			{
				this.annotation = value;
			}
		}

		/// <summary>Gets or sets the qualified attributes that do not belong to the current schema's target namespace.</summary>
		/// <returns>An array of qualified <see cref="T:System.Xml.XmlAttribute" /> objects that do not belong to the schema's target namespace.</returns>
		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x060039A4 RID: 14756 RVA: 0x0014BB6C File Offset: 0x00149D6C
		// (set) Token: 0x060039A5 RID: 14757 RVA: 0x0014BB74 File Offset: 0x00149D74
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

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x060039A6 RID: 14758 RVA: 0x0014BB7D File Offset: 0x00149D7D
		// (set) Token: 0x060039A7 RID: 14759 RVA: 0x0014BB85 File Offset: 0x00149D85
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

		// Token: 0x060039A8 RID: 14760 RVA: 0x0014BB74 File Offset: 0x00149D74
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x0014BB63 File Offset: 0x00149D63
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.annotation = annotation;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaAnnotated" /> class.</summary>
		// Token: 0x060039AA RID: 14762 RVA: 0x0014BB8E File Offset: 0x00149D8E
		public XmlSchemaAnnotated()
		{
		}

		// Token: 0x04002AC8 RID: 10952
		private string id;

		// Token: 0x04002AC9 RID: 10953
		private XmlSchemaAnnotation annotation;

		// Token: 0x04002ACA RID: 10954
		private XmlAttribute[] moreAttributes;
	}
}
