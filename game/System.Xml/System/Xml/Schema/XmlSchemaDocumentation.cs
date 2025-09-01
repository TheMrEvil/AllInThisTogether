using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="documentation" /> element from XML Schema as specified by the World Wide Web Consortium (W3C). This class specifies information to be read or used by humans within an <see langword="annotation" />.</summary>
	// Token: 0x020005AA RID: 1450
	public class XmlSchemaDocumentation : XmlSchemaObject
	{
		/// <summary>Gets or sets the Uniform Resource Identifier (URI) source of the information.</summary>
		/// <returns>A URI reference. The default is <see langword="String.Empty" />.Optional.</returns>
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06003AA4 RID: 15012 RVA: 0x0014D83C File Offset: 0x0014BA3C
		// (set) Token: 0x06003AA5 RID: 15013 RVA: 0x0014D844 File Offset: 0x0014BA44
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

		/// <summary>Gets or sets the <see langword="xml:lang" /> attribute. This serves as an indicator of the language used in the contents.</summary>
		/// <returns>The <see langword="xml:lang" /> attribute.Optional.</returns>
		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06003AA6 RID: 15014 RVA: 0x0014D84D File Offset: 0x0014BA4D
		// (set) Token: 0x06003AA7 RID: 15015 RVA: 0x0014D855 File Offset: 0x0014BA55
		[XmlAttribute("xml:lang")]
		public string Language
		{
			get
			{
				return this.language;
			}
			set
			{
				this.language = (string)XmlSchemaDocumentation.languageType.Datatype.ParseValue(value, null, null);
			}
		}

		/// <summary>Gets or sets an array of <see langword="XmlNodes" /> that represents the documentation child nodes.</summary>
		/// <returns>The array that represents the documentation child nodes.</returns>
		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06003AA8 RID: 15016 RVA: 0x0014D874 File Offset: 0x0014BA74
		// (set) Token: 0x06003AA9 RID: 15017 RVA: 0x0014D87C File Offset: 0x0014BA7C
		[XmlAnyElement]
		[XmlText]
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

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaDocumentation" /> class.</summary>
		// Token: 0x06003AAA RID: 15018 RVA: 0x0014BB8E File Offset: 0x00149D8E
		public XmlSchemaDocumentation()
		{
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x0014D885 File Offset: 0x0014BA85
		// Note: this type is marked as 'beforefieldinit'.
		static XmlSchemaDocumentation()
		{
		}

		// Token: 0x04002B27 RID: 11047
		private string source;

		// Token: 0x04002B28 RID: 11048
		private string language;

		// Token: 0x04002B29 RID: 11049
		private XmlNode[] markup;

		// Token: 0x04002B2A RID: 11050
		private static XmlSchemaSimpleType languageType = DatatypeImplementation.GetSimpleTypeFromXsdType(new XmlQualifiedName("language", "http://www.w3.org/2001/XMLSchema"));
	}
}
