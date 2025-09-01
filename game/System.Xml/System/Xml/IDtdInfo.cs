using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x02000036 RID: 54
	internal interface IDtdInfo
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001BB RID: 443
		XmlQualifiedName Name { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001BC RID: 444
		string InternalDtdSubset { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001BD RID: 445
		bool HasDefaultAttributes { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001BE RID: 446
		bool HasNonCDataAttributes { get; }

		// Token: 0x060001BF RID: 447
		IDtdAttributeListInfo LookupAttributeList(string prefix, string localName);

		// Token: 0x060001C0 RID: 448
		IEnumerable<IDtdAttributeListInfo> GetAttributeLists();

		// Token: 0x060001C1 RID: 449
		IDtdEntityInfo LookupEntity(string name);
	}
}
