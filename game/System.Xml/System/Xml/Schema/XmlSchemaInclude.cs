using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="include" /> element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is used to include declarations and definitions from an external schema. The included declarations and definitions are then available for processing in the containing schema.</summary>
	// Token: 0x020005C7 RID: 1479
	public class XmlSchemaInclude : XmlSchemaExternal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInclude" /> class.</summary>
		// Token: 0x06003B51 RID: 15185 RVA: 0x0014E2C0 File Offset: 0x0014C4C0
		public XmlSchemaInclude()
		{
			base.Compositor = Compositor.Include;
		}

		/// <summary>Gets or sets the <see langword="annotation" /> property.</summary>
		/// <returns>The annotation.</returns>
		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06003B52 RID: 15186 RVA: 0x0014E2CF File Offset: 0x0014C4CF
		// (set) Token: 0x06003B53 RID: 15187 RVA: 0x0014E2D7 File Offset: 0x0014C4D7
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

		// Token: 0x06003B54 RID: 15188 RVA: 0x0014E2D7 File Offset: 0x0014C4D7
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.annotation = annotation;
		}

		// Token: 0x04002B74 RID: 11124
		private XmlSchemaAnnotation annotation;
	}
}
