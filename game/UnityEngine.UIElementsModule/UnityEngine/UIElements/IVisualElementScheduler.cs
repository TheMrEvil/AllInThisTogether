using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000EF RID: 239
	public interface IVisualElementScheduler
	{
		// Token: 0x0600077B RID: 1915
		IVisualElementScheduledItem Execute(Action<TimerState> timerUpdateEvent);

		// Token: 0x0600077C RID: 1916
		IVisualElementScheduledItem Execute(Action updateEvent);
	}
}
