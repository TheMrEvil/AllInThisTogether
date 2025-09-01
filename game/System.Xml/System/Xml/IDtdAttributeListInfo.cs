using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x02000037 RID: 55
	internal interface IDtdAttributeListInfo
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001C2 RID: 450
		string Prefix { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001C3 RID: 451
		string LocalName { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001C4 RID: 452
		bool HasNonCDataAttributes { get; }

		// Token: 0x060001C5 RID: 453
		IDtdAttributeInfo LookupAttribute(string prefix, string localName);

		// Token: 0x060001C6 RID: 454
		IEnumerable<IDtdDefaultAttributeInfo> LookupDefaultAttributes();

		// Token: 0x060001C7 RID: 455
		IDtdAttributeInfo LookupIdAttribute();
	}
}
