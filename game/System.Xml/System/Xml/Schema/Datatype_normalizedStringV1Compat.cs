using System;

namespace System.Xml.Schema
{
	// Token: 0x02000530 RID: 1328
	internal class Datatype_normalizedStringV1Compat : Datatype_string
	{
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06003583 RID: 13699 RVA: 0x0012BC40 File Offset: 0x00129E40
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NormalizedString;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06003584 RID: 13700 RVA: 0x0001222F File Offset: 0x0001042F
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x0012BC44 File Offset: 0x00129E44
		public Datatype_normalizedStringV1Compat()
		{
		}
	}
}
