using System;

namespace System.Xml.Schema
{
	// Token: 0x02000514 RID: 1300
	internal class Datatype_anyAtomicType : Datatype_anySimpleType
	{
		// Token: 0x060034E6 RID: 13542 RVA: 0x0012B309 File Offset: 0x00129509
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlAnyConverter.AnyAtomic;
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x060034E7 RID: 13543 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x060034E8 RID: 13544 RVA: 0x000699F5 File Offset: 0x00067BF5
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyAtomicType;
			}
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x0012B310 File Offset: 0x00129510
		public Datatype_anyAtomicType()
		{
		}
	}
}
