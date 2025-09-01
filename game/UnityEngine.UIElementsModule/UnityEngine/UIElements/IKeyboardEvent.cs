using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001ED RID: 493
	public interface IKeyboardEvent
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000F7B RID: 3963
		EventModifiers modifiers { get; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000F7C RID: 3964
		char character { get; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000F7D RID: 3965
		KeyCode keyCode { get; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000F7E RID: 3966
		bool shiftKey { get; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000F7F RID: 3967
		bool ctrlKey { get; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000F80 RID: 3968
		bool commandKey { get; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000F81 RID: 3969
		bool altKey { get; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000F82 RID: 3970
		bool actionKey { get; }
	}
}
