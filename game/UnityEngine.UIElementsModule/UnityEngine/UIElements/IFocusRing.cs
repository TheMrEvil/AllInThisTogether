using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000030 RID: 48
	public interface IFocusRing
	{
		// Token: 0x06000130 RID: 304
		FocusChangeDirection GetFocusChangeDirection(Focusable currentFocusable, EventBase e);

		// Token: 0x06000131 RID: 305
		Focusable GetNextFocusable(Focusable currentFocusable, FocusChangeDirection direction);
	}
}
