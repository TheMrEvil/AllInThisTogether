using System;

namespace System.Xml.Schema
{
	// Token: 0x02000532 RID: 1330
	internal class Datatype_tokenV1Compat : Datatype_normalizedStringV1Compat
	{
		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06003589 RID: 13705 RVA: 0x0012BC4C File Offset: 0x00129E4C
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Token;
			}
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x0012BC58 File Offset: 0x00129E58
		public Datatype_tokenV1Compat()
		{
		}
	}
}
