using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="sequence" /> element (compositor) from the XML Schema as specified by the World Wide Web Consortium (W3C). The <see langword="sequence" /> requires the elements in the group to appear in the specified sequence within the containing element.</summary>
	// Token: 0x020005D8 RID: 1496
	public class XmlSchemaSequence : XmlSchemaGroupBase
	{
		/// <summary>The elements contained within the compositor. Collection of <see cref="T:System.Xml.Schema.XmlSchemaElement" />, <see cref="T:System.Xml.Schema.XmlSchemaGroupRef" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaSequence" />, or <see cref="T:System.Xml.Schema.XmlSchemaAny" />.</summary>
		/// <returns>The elements contained within the compositor.</returns>
		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06003BE0 RID: 15328 RVA: 0x0014EFCD File Offset: 0x0014D1CD
		[XmlElement("any", typeof(XmlSchemaAny))]
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		[XmlElement("element", typeof(XmlSchemaElement))]
		public override XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06003BE1 RID: 15329 RVA: 0x0014EFD5 File Offset: 0x0014D1D5
		internal override bool IsEmpty
		{
			get
			{
				return base.IsEmpty || this.items.Count == 0;
			}
		}

		// Token: 0x06003BE2 RID: 15330 RVA: 0x0014EFEF File Offset: 0x0014D1EF
		internal override void SetItems(XmlSchemaObjectCollection newItems)
		{
			this.items = newItems;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> class.</summary>
		// Token: 0x06003BE3 RID: 15331 RVA: 0x0014EFF8 File Offset: 0x0014D1F8
		public XmlSchemaSequence()
		{
		}

		// Token: 0x04002BA7 RID: 11175
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();
	}
}
