using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="import" /> element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is used to import schema components from other schemas.</summary>
	// Token: 0x020005C6 RID: 1478
	public class XmlSchemaImport : XmlSchemaExternal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaImport" /> class.</summary>
		// Token: 0x06003B4B RID: 15179 RVA: 0x0014E28F File Offset: 0x0014C48F
		public XmlSchemaImport()
		{
			base.Compositor = Compositor.Import;
		}

		/// <summary>Gets or sets the target namespace for the imported schema as a Uniform Resource Identifier (URI) reference.</summary>
		/// <returns>The target namespace for the imported schema as a URI reference.Optional.</returns>
		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06003B4C RID: 15180 RVA: 0x0014E29E File Offset: 0x0014C49E
		// (set) Token: 0x06003B4D RID: 15181 RVA: 0x0014E2A6 File Offset: 0x0014C4A6
		[XmlAttribute("namespace", DataType = "anyURI")]
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		/// <summary>Gets or sets the <see langword="annotation" /> property.</summary>
		/// <returns>The annotation.</returns>
		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06003B4E RID: 15182 RVA: 0x0014E2AF File Offset: 0x0014C4AF
		// (set) Token: 0x06003B4F RID: 15183 RVA: 0x0014E2B7 File Offset: 0x0014C4B7
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

		// Token: 0x06003B50 RID: 15184 RVA: 0x0014E2B7 File Offset: 0x0014C4B7
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.annotation = annotation;
		}

		// Token: 0x04002B72 RID: 11122
		private string ns;

		// Token: 0x04002B73 RID: 11123
		private XmlSchemaAnnotation annotation;
	}
}
