using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021B RID: 539
	internal interface IPointerEventInternal
	{
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600107A RID: 4218
		// (set) Token: 0x0600107B RID: 4219
		bool triggeredByOS { get; set; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x0600107C RID: 4220
		// (set) Token: 0x0600107D RID: 4221
		bool recomputeTopElementUnderPointer { get; set; }
	}
}
