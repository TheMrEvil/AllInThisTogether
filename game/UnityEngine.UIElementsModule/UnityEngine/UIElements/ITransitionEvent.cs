using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200022B RID: 555
	public interface ITransitionEvent
	{
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060010E8 RID: 4328
		StylePropertyNameCollection stylePropertyNames { get; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060010E9 RID: 4329
		double elapsedTime { get; }
	}
}
