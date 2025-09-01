using System;

namespace System.Xml
{
	// Token: 0x0200003A RID: 58
	internal interface IDtdEntityInfo
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001D3 RID: 467
		string Name { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001D4 RID: 468
		bool IsExternal { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001D5 RID: 469
		bool IsDeclaredInExternal { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001D6 RID: 470
		bool IsUnparsedEntity { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001D7 RID: 471
		bool IsParameterEntity { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001D8 RID: 472
		string BaseUriString { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001D9 RID: 473
		string DeclaredUriString { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001DA RID: 474
		string SystemId { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001DB RID: 475
		string PublicId { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001DC RID: 476
		string Text { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001DD RID: 477
		int LineNumber { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001DE RID: 478
		int LinePosition { get; }
	}
}
