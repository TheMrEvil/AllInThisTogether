using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) <see langword="appinfo" /> element.</summary>
	// Token: 0x02000597 RID: 1431
	public class XmlSchemaAppInfo : XmlSchemaObject
	{
		/// <summary>Gets or sets the source of the application information.</summary>
		/// <returns>A Uniform Resource Identifier (URI) reference. The default is <see langword="String.Empty" />.Optional.</returns>
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x060039CD RID: 14797 RVA: 0x0014BED5 File Offset: 0x0014A0D5
		// (set) Token: 0x060039CE RID: 14798 RVA: 0x0014BEDD File Offset: 0x0014A0DD
		[XmlAttribute("source", DataType = "anyURI")]
		public string Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		/// <summary>Gets or sets an array of <see cref="T:System.Xml.XmlNode" /> objects that represents the <see langword="appinfo" /> child nodes.</summary>
		/// <returns>An array of <see cref="T:System.Xml.XmlNode" /> objects that represents the <see langword="appinfo" /> child nodes.</returns>
		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x060039CF RID: 14799 RVA: 0x0014BEE6 File Offset: 0x0014A0E6
		// (set) Token: 0x060039D0 RID: 14800 RVA: 0x0014BEEE File Offset: 0x0014A0EE
		[XmlText]
		[XmlAnyElement]
		public XmlNode[] Markup
		{
			get
			{
				return this.markup;
			}
			set
			{
				this.markup = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaAppInfo" /> class.</summary>
		// Token: 0x060039D1 RID: 14801 RVA: 0x0014BB8E File Offset: 0x00149D8E
		public XmlSchemaAppInfo()
		{
		}

		// Token: 0x04002AD4 RID: 10964
		private string source;

		// Token: 0x04002AD5 RID: 10965
		private XmlNode[] markup;
	}
}
