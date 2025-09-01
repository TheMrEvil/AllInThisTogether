using System;
using System.Diagnostics.Tracing;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x020001F6 RID: 502
	[EventSource(Name = "System.Linq.Parallel.PlinqEventSource", Guid = "159eeeec-4a14-4418-a8fe-faabcd987887")]
	internal sealed class PlinqEtwProvider : EventSource
	{
		// Token: 0x06000C51 RID: 3153 RVA: 0x0002B1D0 File Offset: 0x000293D0
		private PlinqEtwProvider()
		{
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002B1D8 File Offset: 0x000293D8
		[NonEvent]
		internal static int NextQueryId()
		{
			return Interlocked.Increment(ref PlinqEtwProvider.s_queryId);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0002B1E4 File Offset: 0x000293E4
		[NonEvent]
		internal void ParallelQueryBegin(int queryId)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				int valueOrDefault = Task.CurrentId.GetValueOrDefault();
				this.ParallelQueryBegin(PlinqEtwProvider.s_defaultSchedulerId, valueOrDefault, queryId);
			}
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002B217 File Offset: 0x00029417
		[Event(1, Level = EventLevel.Informational, Task = (EventTask)1, Opcode = EventOpcode.Start)]
		private void ParallelQueryBegin(int taskSchedulerId, int taskId, int queryId)
		{
			base.WriteEvent(1, taskSchedulerId, taskId, queryId);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0002B224 File Offset: 0x00029424
		[NonEvent]
		internal void ParallelQueryEnd(int queryId)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				int valueOrDefault = Task.CurrentId.GetValueOrDefault();
				this.ParallelQueryEnd(PlinqEtwProvider.s_defaultSchedulerId, valueOrDefault, queryId);
			}
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0002B257 File Offset: 0x00029457
		[Event(2, Level = EventLevel.Informational, Task = (EventTask)1, Opcode = EventOpcode.Stop)]
		private void ParallelQueryEnd(int taskSchedulerId, int taskId, int queryId)
		{
			base.WriteEvent(2, taskSchedulerId, taskId, queryId);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002B264 File Offset: 0x00029464
		[NonEvent]
		internal void ParallelQueryFork(int queryId)
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				int valueOrDefault = Task.CurrentId.GetValueOrDefault();
				this.ParallelQueryFork(PlinqEtwProvider.s_defaultSchedulerId, valueOrDefault, queryId);
			}
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002B297 File Offset: 0x00029497
		[Event(3, Level = EventLevel.Verbose, Task = (EventTask)2, Opcode = EventOpcode.Start)]
		private void ParallelQueryFork(int taskSchedulerId, int taskId, int queryId)
		{
			base.WriteEvent(3, taskSchedulerId, taskId, queryId);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002B2A4 File Offset: 0x000294A4
		[NonEvent]
		internal void ParallelQueryJoin(int queryId)
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				int valueOrDefault = Task.CurrentId.GetValueOrDefault();
				this.ParallelQueryJoin(PlinqEtwProvider.s_defaultSchedulerId, valueOrDefault, queryId);
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002B2D7 File Offset: 0x000294D7
		[Event(4, Level = EventLevel.Verbose, Task = (EventTask)2, Opcode = EventOpcode.Stop)]
		private void ParallelQueryJoin(int taskSchedulerId, int taskId, int queryId)
		{
			base.WriteEvent(4, taskSchedulerId, taskId, queryId);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002B2E3 File Offset: 0x000294E3
		// Note: this type is marked as 'beforefieldinit'.
		static PlinqEtwProvider()
		{
		}

		// Token: 0x040008B3 RID: 2227
		internal static PlinqEtwProvider Log = new PlinqEtwProvider();

		// Token: 0x040008B4 RID: 2228
		private static readonly int s_defaultSchedulerId = TaskScheduler.Default.Id;

		// Token: 0x040008B5 RID: 2229
		private static int s_queryId = 0;

		// Token: 0x040008B6 RID: 2230
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x040008B7 RID: 2231
		private const int PARALLELQUERYBEGIN_EVENTID = 1;

		// Token: 0x040008B8 RID: 2232
		private const int PARALLELQUERYEND_EVENTID = 2;

		// Token: 0x040008B9 RID: 2233
		private const int PARALLELQUERYFORK_EVENTID = 3;

		// Token: 0x040008BA RID: 2234
		private const int PARALLELQUERYJOIN_EVENTID = 4;

		// Token: 0x020001F7 RID: 503
		public class Tasks
		{
			// Token: 0x06000C5C RID: 3164 RVA: 0x00002162 File Offset: 0x00000362
			public Tasks()
			{
			}

			// Token: 0x040008BB RID: 2235
			public const EventTask Query = (EventTask)1;

			// Token: 0x040008BC RID: 2236
			public const EventTask ForkJoin = (EventTask)2;
		}
	}
}
