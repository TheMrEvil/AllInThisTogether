using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>This class represents the <see langword="keyref" /> element from XMLSchema as specified by the World Wide Web Consortium (W3C).</summary>
	// Token: 0x020005C5 RID: 1477
	public class XmlSchemaKeyref : XmlSchemaIdentityConstraint
	{
		/// <summary>Gets or sets the name of the key that this constraint refers to in another simple or complex type.</summary>
		/// <returns>The QName of the key that this constraint refers to.</returns>
		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06003B48 RID: 15176 RVA: 0x0014E25B File Offset: 0x0014C45B
		// (set) Token: 0x06003B49 RID: 15177 RVA: 0x0014E263 File Offset: 0x0014C463
		[XmlAttribute("refer")]
		public XmlQualifiedName Refer
		{
			get
			{
				return this.refer;
			}
			set
			{
				this.refer = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaKeyref" /> class.</summary>
		// Token: 0x06003B4A RID: 15178 RVA: 0x0014E27C File Offset: 0x0014C47C
		public XmlSchemaKeyref()
		{
		}

		// Token: 0x04002B71 RID: 11121
		private XmlQualifiedName refer = XmlQualifiedName.Empty;
	}
}
