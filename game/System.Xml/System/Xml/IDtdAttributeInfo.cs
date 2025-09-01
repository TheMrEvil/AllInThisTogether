using System;

namespace System.Xml
{
	// Token: 0x02000038 RID: 56
	internal interface IDtdAttributeInfo
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001C8 RID: 456
		string Prefix { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001C9 RID: 457
		string LocalName { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001CA RID: 458
		int LineNumber { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001CB RID: 459
		int LinePosition { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001CC RID: 460
		bool IsNonCDataType { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001CD RID: 461
		bool IsDeclaredInExternal { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001CE RID: 462
		bool IsXmlAttribute { get; }
	}
}
