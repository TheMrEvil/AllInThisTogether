using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000292 RID: 658
	internal interface IStyleValue<T>
	{
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060015DC RID: 5596
		// (set) Token: 0x060015DD RID: 5597
		T value { get; set; }

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060015DE RID: 5598
		// (set) Token: 0x060015DF RID: 5599
		StyleKeyword keyword { get; set; }
	}
}
