using System;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000097 RID: 151
	[Flags]
	public enum SearchFilterOptions
	{
		// Token: 0x040008AA RID: 2218
		PropertyName = 1,
		// Token: 0x040008AB RID: 2219
		PropertyNiceName = 2,
		// Token: 0x040008AC RID: 2220
		TypeOfValue = 4,
		// Token: 0x040008AD RID: 2221
		ValueToString = 8,
		// Token: 0x040008AE RID: 2222
		ISearchFilterableInterface = 16,
		// Token: 0x040008AF RID: 2223
		All = -1
	}
}
