using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E1 RID: 481
	public interface IFocusEvent
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000F50 RID: 3920
		Focusable relatedTarget { get; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000F51 RID: 3921
		FocusChangeDirection direction { get; }
	}
}
