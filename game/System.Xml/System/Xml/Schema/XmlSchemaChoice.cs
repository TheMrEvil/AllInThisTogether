using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="choice" /> element (compositor) from the XML Schema as specified by the World Wide Web Consortium (W3C). The <see langword="choice" /> allows only one of its children to appear in an instance. </summary>
	// Token: 0x0200059B RID: 1435
	public class XmlSchemaChoice : XmlSchemaGroupBase
	{
		/// <summary>Gets the collection of the elements contained with the compositor (<see langword="choice" />): <see langword="XmlSchemaElement" />, <see langword="XmlSchemaGroupRef" />, <see langword="XmlSchemaChoice" />, <see langword="XmlSchemaSequence" />, or <see langword="XmlSchemaAny" />.</summary>
		/// <returns>The collection of elements contained within <see langword="XmlSchemaChoice" />.</returns>
		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06003A06 RID: 14854 RVA: 0x0014C20D File Offset: 0x0014A40D
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("any", typeof(XmlSchemaAny))]
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		[XmlElement("element", typeof(XmlSchemaElement))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		public override XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06003A07 RID: 14855 RVA: 0x0014C215 File Offset: 0x0014A415
		internal override bool IsEmpty
		{
			get
			{
				return base.IsEmpty;
			}
		}

		// Token: 0x06003A08 RID: 14856 RVA: 0x0014C21D File Offset: 0x0014A41D
		internal override void SetItems(XmlSchemaObjectCollection newItems)
		{
			this.items = newItems;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaChoice" /> class.</summary>
		// Token: 0x06003A09 RID: 14857 RVA: 0x0014C226 File Offset: 0x0014A426
		public XmlSchemaChoice()
		{
		}

		// Token: 0x04002AEA RID: 10986
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();
	}
}
