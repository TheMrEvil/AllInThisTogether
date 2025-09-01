using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000068 RID: 104
	internal interface IScheduler
	{
		// Token: 0x060002EE RID: 750
		ScheduledItem ScheduleOnce(Action<TimerState> timerUpdateEvent, long delayMs);

		// Token: 0x060002EF RID: 751
		ScheduledItem ScheduleUntil(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, Func<bool> stopCondition = null);

		// Token: 0x060002F0 RID: 752
		ScheduledItem ScheduleForDuration(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, long durationMs);

		// Token: 0x060002F1 RID: 753
		void Unschedule(ScheduledItem item);

		// Token: 0x060002F2 RID: 754
		void Schedule(ScheduledItem item);

		// Token: 0x060002F3 RID: 755
		void UpdateScheduledEvents();
	}
}
