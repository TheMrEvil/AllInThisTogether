using System;

namespace System.Xml.Schema
{
	// Token: 0x02000515 RID: 1301
	internal class Datatype_untypedAtomicType : Datatype_anyAtomicType
	{
		// Token: 0x060034EA RID: 13546 RVA: 0x0012B2AC File Offset: 0x001294AC
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlUntypedConverter.Untyped;
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x060034EB RID: 13547 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x060034EC RID: 13548 RVA: 0x00069829 File Offset: 0x00067A29
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UntypedAtomic;
			}
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x0012B318 File Offset: 0x00129518
		public Datatype_untypedAtomicType()
		{
		}
	}
}
