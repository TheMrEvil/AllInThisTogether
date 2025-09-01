using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006B RID: 107
	internal class TimerEventScheduler : IScheduler
	{
		// Token: 0x06000307 RID: 775 RVA: 0x0000AD50 File Offset: 0x00008F50
		public void Schedule(ScheduledItem item)
		{
			bool flag = item == null;
			if (!flag)
			{
				bool flag2 = item == null;
				if (flag2)
				{
					throw new NotSupportedException("Scheduled Item type is not supported by this scheduler");
				}
				bool transactionMode = this.m_TransactionMode;
				if (transactionMode)
				{
					bool flag3 = this.m_UnscheduleTransactions.Remove(item);
					if (!flag3)
					{
						bool flag4 = this.m_ScheduledItems.Contains(item) || this.m_ScheduleTransactions.Contains(item);
						if (flag4)
						{
							throw new ArgumentException("Cannot schedule function " + item + " more than once");
						}
						this.m_ScheduleTransactions.Add(item);
					}
				}
				else
				{
					bool flag5 = this.m_ScheduledItems.Contains(item);
					if (flag5)
					{
						throw new ArgumentException("Cannot schedule function " + item + " more than once");
					}
					this.m_ScheduledItems.Add(item);
				}
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000AE28 File Offset: 0x00009028
		public ScheduledItem ScheduleOnce(Action<TimerState> timerUpdateEvent, long delayMs)
		{
			TimerEventScheduler.TimerEventSchedulerItem timerEventSchedulerItem = new TimerEventScheduler.TimerEventSchedulerItem(timerUpdateEvent)
			{
				delayMs = delayMs
			};
			this.Schedule(timerEventSchedulerItem);
			return timerEventSchedulerItem;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000AE54 File Offset: 0x00009054
		public ScheduledItem ScheduleUntil(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, Func<bool> stopCondition)
		{
			TimerEventScheduler.TimerEventSchedulerItem timerEventSchedulerItem = new TimerEventScheduler.TimerEventSchedulerItem(timerUpdateEvent)
			{
				delayMs = delayMs,
				intervalMs = intervalMs,
				timerUpdateStopCondition = stopCondition
			};
			this.Schedule(timerEventSchedulerItem);
			return timerEventSchedulerItem;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000AE90 File Offset: 0x00009090
		public ScheduledItem ScheduleForDuration(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, long durationMs)
		{
			TimerEventScheduler.TimerEventSchedulerItem timerEventSchedulerItem = new TimerEventScheduler.TimerEventSchedulerItem(timerUpdateEvent)
			{
				delayMs = delayMs,
				intervalMs = intervalMs,
				timerUpdateStopCondition = null
			};
			timerEventSchedulerItem.SetDuration(durationMs);
			this.Schedule(timerEventSchedulerItem);
			return timerEventSchedulerItem;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000AED4 File Offset: 0x000090D4
		private bool RemovedScheduledItemAt(int index)
		{
			bool flag = index >= 0;
			bool result;
			if (flag)
			{
				bool flag2 = index <= this.m_LastUpdatedIndex;
				if (flag2)
				{
					this.m_LastUpdatedIndex--;
				}
				this.m_ScheduledItems.RemoveAt(index);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000AF24 File Offset: 0x00009124
		public void Unschedule(ScheduledItem item)
		{
			bool flag = item != null;
			if (flag)
			{
				bool transactionMode = this.m_TransactionMode;
				if (transactionMode)
				{
					bool flag2 = this.m_UnscheduleTransactions.Contains(item);
					if (flag2)
					{
						throw new ArgumentException("Cannot unschedule scheduled function twice" + ((item != null) ? item.ToString() : null));
					}
					bool flag3 = this.m_ScheduleTransactions.Remove(item);
					if (!flag3)
					{
						bool flag4 = this.m_ScheduledItems.Contains(item);
						if (!flag4)
						{
							throw new ArgumentException("Cannot unschedule unknown scheduled function " + ((item != null) ? item.ToString() : null));
						}
						this.m_UnscheduleTransactions.Add(item);
					}
				}
				else
				{
					bool flag5 = !this.PrivateUnSchedule(item);
					if (flag5)
					{
						throw new ArgumentException("Cannot unschedule unknown scheduled function " + ((item != null) ? item.ToString() : null));
					}
				}
				item.OnItemUnscheduled();
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000B010 File Offset: 0x00009210
		private bool PrivateUnSchedule(ScheduledItem sItem)
		{
			return this.m_ScheduleTransactions.Remove(sItem) || this.RemovedScheduledItemAt(this.m_ScheduledItems.IndexOf(sItem));
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000B048 File Offset: 0x00009248
		public void UpdateScheduledEvents()
		{
			try
			{
				this.m_TransactionMode = true;
				long num = Panel.TimeSinceStartupMs();
				int count = this.m_ScheduledItems.Count;
				int num2 = this.m_LastUpdatedIndex + 1;
				bool flag = num2 >= count;
				if (flag)
				{
					num2 = 0;
				}
				for (int i = 0; i < count; i++)
				{
					int num3 = num2 + i;
					bool flag2 = num3 >= count;
					if (flag2)
					{
						num3 -= count;
					}
					ScheduledItem scheduledItem = this.m_ScheduledItems[num3];
					bool flag3 = false;
					bool flag4 = num - scheduledItem.delayMs >= scheduledItem.startMs;
					if (flag4)
					{
						TimerState state = new TimerState
						{
							start = scheduledItem.startMs,
							now = num
						};
						bool flag5 = !this.m_UnscheduleTransactions.Contains(scheduledItem);
						if (flag5)
						{
							scheduledItem.PerformTimerUpdate(state);
						}
						scheduledItem.startMs = num;
						scheduledItem.delayMs = scheduledItem.intervalMs;
						bool flag6 = scheduledItem.ShouldUnschedule();
						if (flag6)
						{
							flag3 = true;
						}
					}
					bool flag7 = flag3 || (scheduledItem.endTimeMs > 0L && num > scheduledItem.endTimeMs);
					if (flag7)
					{
						bool flag8 = !this.m_UnscheduleTransactions.Contains(scheduledItem);
						if (flag8)
						{
							this.Unschedule(scheduledItem);
						}
					}
					this.m_LastUpdatedIndex = num3;
				}
			}
			finally
			{
				this.m_TransactionMode = false;
				foreach (ScheduledItem sItem in this.m_UnscheduleTransactions)
				{
					this.PrivateUnSchedule(sItem);
				}
				this.m_UnscheduleTransactions.Clear();
				foreach (ScheduledItem item in this.m_ScheduleTransactions)
				{
					this.Schedule(item);
				}
				this.m_ScheduleTransactions.Clear();
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000B2A0 File Offset: 0x000094A0
		public TimerEventScheduler()
		{
		}

		// Token: 0x0400015C RID: 348
		private readonly List<ScheduledItem> m_ScheduledItems = new List<ScheduledItem>();

		// Token: 0x0400015D RID: 349
		private bool m_TransactionMode;

		// Token: 0x0400015E RID: 350
		private readonly List<ScheduledItem> m_ScheduleTransactions = new List<ScheduledItem>();

		// Token: 0x0400015F RID: 351
		private readonly HashSet<ScheduledItem> m_UnscheduleTransactions = new HashSet<ScheduledItem>();

		// Token: 0x04000160 RID: 352
		internal bool disableThrottling = false;

		// Token: 0x04000161 RID: 353
		private int m_LastUpdatedIndex = -1;

		// Token: 0x0200006C RID: 108
		private class TimerEventSchedulerItem : ScheduledItem
		{
			// Token: 0x06000310 RID: 784 RVA: 0x0000B2D8 File Offset: 0x000094D8
			public TimerEventSchedulerItem(Action<TimerState> updateEvent)
			{
				this.m_TimerUpdateEvent = updateEvent;
			}

			// Token: 0x06000311 RID: 785 RVA: 0x0000B2E9 File Offset: 0x000094E9
			public override void PerformTimerUpdate(TimerState state)
			{
				Action<TimerState> timerUpdateEvent = this.m_TimerUpdateEvent;
				if (timerUpdateEvent != null)
				{
					timerUpdateEvent(state);
				}
			}

			// Token: 0x06000312 RID: 786 RVA: 0x0000B300 File Offset: 0x00009500
			public override string ToString()
			{
				return this.m_TimerUpdateEvent.ToString();
			}

			// Token: 0x04000162 RID: 354
			private readonly Action<TimerState> m_TimerUpdateEvent;
		}
	}
}
