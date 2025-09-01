using System;

namespace System.Xml.Schema
{
	// Token: 0x02000531 RID: 1329
	internal class Datatype_token : Datatype_normalizedString
	{
		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06003586 RID: 13702 RVA: 0x0012BC4C File Offset: 0x00129E4C
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Token;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06003587 RID: 13703 RVA: 0x00066748 File Offset: 0x00064948
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x0012BC50 File Offset: 0x00129E50
		public Datatype_token()
		{
		}
	}
}
