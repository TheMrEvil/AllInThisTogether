using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Specifies the order and structure of the child elements of a type.</summary>
	// Token: 0x020005A5 RID: 1445
	public abstract class XmlSchemaContentModel : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the content of the type.</summary>
		/// <returns>Provides the content of the type.</returns>
		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06003A7F RID: 14975
		// (set) Token: 0x06003A80 RID: 14976
		[XmlIgnore]
		public abstract XmlSchemaContent Content { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaContentModel" /> class.</summary>
		// Token: 0x06003A81 RID: 14977 RVA: 0x0014BECD File Offset: 0x0014A0CD
		protected XmlSchemaContentModel()
		{
		}
	}
}
