using System;

namespace System.Xml.Schema
{
	// Token: 0x0200052F RID: 1327
	internal class Datatype_normalizedString : Datatype_string
	{
		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x0600357F RID: 13695 RVA: 0x0012BC40 File Offset: 0x00129E40
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NormalizedString;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06003580 RID: 13696 RVA: 0x0001222F File Offset: 0x0001042F
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Replace;
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06003581 RID: 13697 RVA: 0x0001222F File Offset: 0x0001042F
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x0012BC44 File Offset: 0x00129E44
		public Datatype_normalizedString()
		{
		}
	}
}
