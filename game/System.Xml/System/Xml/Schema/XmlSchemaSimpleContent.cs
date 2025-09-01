using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="simpleContent" /> element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is for simple and complex types with simple content model.</summary>
	// Token: 0x020005DA RID: 1498
	public class XmlSchemaSimpleContent : XmlSchemaContentModel
	{
		/// <summary>Gets one of the <see cref="T:System.Xml.Schema.XmlSchemaSimpleContentRestriction" /> or <see cref="T:System.Xml.Schema.XmlSchemaSimpleContentExtension" />.</summary>
		/// <returns>The content contained within the <see langword="XmlSchemaSimpleContentRestriction" /> or <see langword="XmlSchemaSimpleContentExtension" /> class.</returns>
		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x00150EDD File Offset: 0x0014F0DD
		// (set) Token: 0x06003C1F RID: 15391 RVA: 0x00150EE5 File Offset: 0x0014F0E5
		[XmlElement("restriction", typeof(XmlSchemaSimpleContentRestriction))]
		[XmlElement("extension", typeof(XmlSchemaSimpleContentExtension))]
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

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaSimpleContent" /> class.</summary>
		// Token: 0x06003C20 RID: 15392 RVA: 0x0014C8BC File Offset: 0x0014AABC
		public XmlSchemaSimpleContent()
		{
		}

		// Token: 0x04002BBC RID: 11196
		private XmlSchemaContent content;
	}
}
