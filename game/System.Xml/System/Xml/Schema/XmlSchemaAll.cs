using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) <see langword="all" /> element (compositor).</summary>
	// Token: 0x02000592 RID: 1426
	public class XmlSchemaAll : XmlSchemaGroupBase
	{
		/// <summary>Gets the collection of <see langword="XmlSchemaElement" /> elements contained within the <see langword="all" /> compositor.</summary>
		/// <returns>The collection of elements contained in <see langword="XmlSchemaAll" />.</returns>
		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x0600399C RID: 14748 RVA: 0x0014BB0C File Offset: 0x00149D0C
		[XmlElement("element", typeof(XmlSchemaElement))]
		public override XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x0600399D RID: 14749 RVA: 0x0014BB14 File Offset: 0x00149D14
		internal override bool IsEmpty
		{
			get
			{
				return base.IsEmpty || this.items.Count == 0;
			}
		}

		// Token: 0x0600399E RID: 14750 RVA: 0x0014BB2E File Offset: 0x00149D2E
		internal override void SetItems(XmlSchemaObjectCollection newItems)
		{
			this.items = newItems;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaAll" /> class.</summary>
		// Token: 0x0600399F RID: 14751 RVA: 0x0014BB37 File Offset: 0x00149D37
		public XmlSchemaAll()
		{
		}

		// Token: 0x04002AC7 RID: 10951
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();
	}
}
