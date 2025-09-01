using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000127 RID: 295
	public interface IBindable
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000A0D RID: 2573
		// (set) Token: 0x06000A0E RID: 2574
		IBinding binding { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000A0F RID: 2575
		// (set) Token: 0x06000A10 RID: 2576
		string bindingPath { get; set; }
	}
}
