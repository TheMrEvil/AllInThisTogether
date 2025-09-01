using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="complexContent" /> element from XML Schema as specified by the World Wide Web Consortium (W3C). This class represents the complex content model for complex types. It contains extensions or restrictions on a complex type that has either only elements or mixed content.</summary>
	// Token: 0x020005A0 RID: 1440
	public class XmlSchemaComplexContent : XmlSchemaContentModel
	{
		/// <summary>Gets or sets information that determines if the type has a mixed content model.</summary>
		/// <returns>If this property is <see langword="true" />, character data is allowed to appear between the child elements of the complex type (mixed content model). The default is <see langword="false" />.Optional.</returns>
		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06003A3B RID: 14907 RVA: 0x0014C88B File Offset: 0x0014AA8B
		// (set) Token: 0x06003A3C RID: 14908 RVA: 0x0014C893 File Offset: 0x0014AA93
		[XmlAttribute("mixed")]
		public bool IsMixed
		{
			get
			{
				return this.isMixed;
			}
			set
			{
				this.isMixed = value;
				this.hasMixedAttribute = true;
			}
		}

		/// <summary>Gets or sets the content.</summary>
		/// <returns>One of either the <see cref="T:System.Xml.Schema.XmlSchemaComplexContentRestriction" /> or <see cref="T:System.Xml.Schema.XmlSchemaComplexContentExtension" /> classes.</returns>
		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06003A3D RID: 14909 RVA: 0x0014C8A3 File Offset: 0x0014AAA3
		// (set) Token: 0x06003A3E RID: 14910 RVA: 0x0014C8AB File Offset: 0x0014AAAB
		[XmlElement("restriction", typeof(XmlSchemaComplexContentRestriction))]
		[XmlElement("extension", typeof(XmlSchemaComplexContentExtension))]
		public override XmlSchemaContent Content
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

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06003A3F RID: 14911 RVA: 0x0014C8B4 File Offset: 0x0014AAB4
		[XmlIgnore]
		internal bool HasMixedAttribute
		{
			get
			{
				return this.hasMixedAttribute;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaComplexContent" /> class.</summary>
		// Token: 0x06003A40 RID: 14912 RVA: 0x0014C8BC File Offset: 0x0014AABC
		public XmlSchemaComplexContent()
		{
		}

		// Token: 0x04002AF8 RID: 11000
		private XmlSchemaContent content;

		// Token: 0x04002AF9 RID: 11001
		private bool isMixed;

		// Token: 0x04002AFA RID: 11002
		private bool hasMixedAttribute;
	}
}
