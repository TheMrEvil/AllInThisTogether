using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200005F RID: 95
	[Conditional("UNITY_EDITOR")]
	[DontApplyToListElements]
	public class SearchableAttribute : Attribute
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00003755 File Offset: 0x00001955
		public SearchableAttribute()
		{
		}

		// Token: 0x04000106 RID: 262
		public bool FuzzySearch = true;

		// Token: 0x04000107 RID: 263
		public SearchFilterOptions FilterOptions = SearchFilterOptions.All;

		// Token: 0x04000108 RID: 264
		public bool Recursive = true;
	}
}
