using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>An abstract class for <see cref="T:System.Xml.Schema.XmlSchemaAll" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" />.</summary>
	// Token: 0x020005BF RID: 1471
	public abstract class XmlSchemaGroupBase : XmlSchemaParticle
	{
		/// <summary>This collection is used to add new elements to the compositor.</summary>
		/// <returns>An <see langword="XmlSchemaObjectCollection" />.</returns>
		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06003B2D RID: 15149
		[XmlIgnore]
		public abstract XmlSchemaObjectCollection Items { get; }

		// Token: 0x06003B2E RID: 15150
		internal abstract void SetItems(XmlSchemaObjectCollection newItems);

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaGroupBase" /> class.</summary>
		// Token: 0x06003B2F RID: 15151 RVA: 0x0014BD7B File Offset: 0x00149F7B
		protected XmlSchemaGroupBase()
		{
		}
	}
}
