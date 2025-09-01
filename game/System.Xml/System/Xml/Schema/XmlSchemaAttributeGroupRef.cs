using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="attributeGroup" /> element with the <see langword="ref" /> attribute from the XML Schema as specified by the World Wide Web Consortium (W3C). AttributesGroupRef is the reference for an attributeGroup, name property contains the attribute group being referenced. </summary>
	// Token: 0x0200059A RID: 1434
	public class XmlSchemaAttributeGroupRef : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the name of the referenced <see langword="attributeGroup" /> element.</summary>
		/// <returns>The name of the referenced attribute group. The value must be a QName.</returns>
		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06003A03 RID: 14851 RVA: 0x0014C1D9 File Offset: 0x0014A3D9
		// (set) Token: 0x06003A04 RID: 14852 RVA: 0x0014C1E1 File Offset: 0x0014A3E1
		[XmlAttribute("ref")]
		public XmlQualifiedName RefName
		{
			get
			{
				return this.refName;
			}
			set
			{
				this.refName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" /> class.</summary>
		// Token: 0x06003A05 RID: 14853 RVA: 0x0014C1FA File Offset: 0x0014A3FA
		public XmlSchemaAttributeGroupRef()
		{
		}

		// Token: 0x04002AE9 RID: 10985
		private XmlQualifiedName refName = XmlQualifiedName.Empty;
	}
}
