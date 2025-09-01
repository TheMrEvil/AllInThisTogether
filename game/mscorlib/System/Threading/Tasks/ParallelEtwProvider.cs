using System;
using System.Diagnostics.Tracing;

namespace System.Threading.Tasks
{
	// Token: 0x02000325 RID: 805
	[EventSource(Name = "System.Threading.Tasks.Parallel.EventSource")]
	internal sealed class ParallelEtwProvider : EventSource
	{
		// Token: 0x0600222E RID: 8750 RVA: 0x0007B584 File Offset: 0x00079784
		private ParallelEtwProvider()
		{
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x0007B58C File Offset: 0x0007978C
		[Event(1, Level = EventLevel.Informational, Task = (EventTask)1, Opcode = EventOpcode.Start)]
		public unsafe void ParallelLoopBegin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID, ParallelEtwProvider.ForkJoinOperationType OperationType, long InclusiveFrom, long ExclusiveTo)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)6) * (UIntPtr)sizeof(EventSource.EventData))];
				*ptr = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID))
				};
				ptr[1] = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&OriginatingTaskID))
				};
				ptr[2] = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&ForkJoinContextID))
				};
				ptr[3] = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&OperationType))
				};
				ptr[4] = new EventSource.EventData
				{
					Size = 8,
					DataPointer = (IntPtr)((void*)(&InclusiveFrom))
				};
				ptr[5] = new EventSource.EventData
				{
					Size = 8,
					DataPointer = (IntPtr)((void*)(&ExclusiveTo))
				};
				base.WriteEventCore(1, 6, ptr);
			}
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x0007B6D0 File Offset: 0x000798D0
		[Event(2, Level = EventLevel.Informational, Task = (EventTask)1, Opcode = EventOpcode.Stop)]
		public unsafe void ParallelLoopEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID, long TotalIterations)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData))];
				*ptr = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID))
				};
				ptr[1] = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&OriginatingTaskID))
				};
				ptr[2] = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&ForkJoinContextID))
				};
				ptr[3] = new EventSource.EventData
				{
					Size = 8,
					DataPointer = (IntPtr)((void*)(&TotalIterations))
				};
				base.WriteEventCore(2, 4, ptr);
			}
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x0007B7B4 File Offset: 0x000799B4
		[Event(3, Level = EventLevel.Informational, Task = (EventTask)2, Opcode = EventOpcode.Start)]
		public unsafe void ParallelInvokeBegin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID, ParallelEtwProvider.ForkJoinOperationType OperationType, int ActionCount)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)5) * (UIntPtr)sizeof(EventSource.EventData))];
				*ptr = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID))
				};
				ptr[1] = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&OriginatingTaskID))
				};
				ptr[2] = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&ForkJoinContextID))
				};
				ptr[3] = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&OperationType))
				};
				ptr[4] = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&ActionCount))
				};
				base.WriteEventCore(3, 5, ptr);
			}
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x0007B8C7 File Offset: 0x00079AC7
		[Event(4, Level = EventLevel.Informational, Task = (EventTask)2, Opcode = EventOpcode.Stop)]
		public void ParallelInvokeEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				base.WriteEvent(4, OriginatingTaskSchedulerID, OriginatingTaskID, ForkJoinContextID);
			}
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x0007B8DE File Offset: 0x00079ADE
		[Event(5, Level = EventLevel.Verbose, Task = (EventTask)5, Opcode = EventOpcode.Start)]
		public void ParallelFork(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID)
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(5, OriginatingTaskSchedulerID, OriginatingTaskID, ForkJoinContextID);
			}
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x0007B8F5 File Offset: 0x00079AF5
		[Event(6, Level = EventLevel.Verbose, Task = (EventTask)5, Opcode = EventOpcode.Stop)]
		public void ParallelJoin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID)
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(6, OriginatingTaskSchedulerID, OriginatingTaskID, ForkJoinContextID);
			}
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x0007B90C File Offset: 0x00079B0C
		// Note: this type is marked as 'beforefieldinit'.
		static ParallelEtwProvider()
		{
		}

		// Token: 0x04001C24 RID: 7204
		public static readonly ParallelEtwProvider Log = new ParallelEtwProvider();

		// Token: 0x04001C25 RID: 7205
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x04001C26 RID: 7206
		private const int PARALLELLOOPBEGIN_ID = 1;

		// Token: 0x04001C27 RID: 7207
		private const int PARALLELLOOPEND_ID = 2;

		// Token: 0x04001C28 RID: 7208
		private const int PARALLELINVOKEBEGIN_ID = 3;

		// Token: 0x04001C29 RID: 7209
		private const int PARALLELINVOKEEND_ID = 4;

		// Token: 0x04001C2A RID: 7210
		private const int PARALLELFORK_ID = 5;

		// Token: 0x04001C2B RID: 7211
		private const int PARALLELJOIN_ID = 6;

		// Token: 0x02000326 RID: 806
		public enum ForkJoinOperationType
		{
			// Token: 0x04001C2D RID: 7213
			ParallelInvoke = 1,
			// Token: 0x04001C2E RID: 7214
			ParallelFor,
			// Token: 0x04001C2F RID: 7215
			ParallelForEach
		}

		// Token: 0x02000327 RID: 807
		public class Tasks
		{
			// Token: 0x06002236 RID: 8758 RVA: 0x0000259F File Offset: 0x0000079F
			public Tasks()
			{
			}

			// Token: 0x04001C30 RID: 7216
			public const EventTask Loop = (EventTask)1;

			// Token: 0x04001C31 RID: 7217
			public const EventTask Invoke = (EventTask)2;

			// Token: 0x04001C32 RID: 7218
			public const EventTask ForkJoin = (EventTask)5;
		}
	}
}
