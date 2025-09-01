using System;
using System.Diagnostics.Tracing;
using System.Threading;

namespace System.Data
{
	// Token: 0x020000A6 RID: 166
	[EventSource(Name = "System.Data.DataCommonEventSource")]
	internal class DataCommonEventSource : EventSource
	{
		// Token: 0x06000A4A RID: 2634 RVA: 0x0002B3BE File Offset: 0x000295BE
		[Event(1, Level = EventLevel.Informational)]
		internal void Trace(string message)
		{
			base.WriteEvent(1, message);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0002B3C8 File Offset: 0x000295C8
		[NonEvent]
		internal void Trace<T0>(string format, T0 arg0)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, arg0));
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002B3E9 File Offset: 0x000295E9
		[NonEvent]
		internal void Trace<T0, T1>(string format, T0 arg0, T1 arg1)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, arg0, arg1));
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0002B410 File Offset: 0x00029610
		[NonEvent]
		internal void Trace<T0, T1, T2>(string format, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, arg0, arg1, arg2));
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0002B440 File Offset: 0x00029640
		[NonEvent]
		internal void Trace<T0, T1, T2, T3>(string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			}));
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0002B494 File Offset: 0x00029694
		[NonEvent]
		internal void Trace<T0, T1, T2, T3, T4>(string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4
			}));
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0002B4F0 File Offset: 0x000296F0
		[NonEvent]
		internal void Trace<T0, T1, T2, T3, T4, T5, T6>(string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6
			}));
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002B560 File Offset: 0x00029760
		[Event(2, Level = EventLevel.Verbose)]
		internal long EnterScope(string message)
		{
			long num = 0L;
			if (DataCommonEventSource.Log.IsEnabled())
			{
				num = Interlocked.Increment(ref DataCommonEventSource.s_nextScopeId);
				base.WriteEvent(2, num, message);
			}
			return num;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002B591 File Offset: 0x00029791
		[NonEvent]
		internal long EnterScope<T1>(string format, T1 arg1)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return 0L;
			}
			return this.EnterScope(string.Format(format, arg1));
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0002B5B4 File Offset: 0x000297B4
		[NonEvent]
		internal long EnterScope<T1, T2>(string format, T1 arg1, T2 arg2)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return 0L;
			}
			return this.EnterScope(string.Format(format, arg1, arg2));
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002B5DD File Offset: 0x000297DD
		[NonEvent]
		internal long EnterScope<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return 0L;
			}
			return this.EnterScope(string.Format(format, arg1, arg2, arg3));
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002B610 File Offset: 0x00029810
		[NonEvent]
		internal long EnterScope<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return 0L;
			}
			return this.EnterScope(string.Format(format, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4
			}));
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002B664 File Offset: 0x00029864
		[Event(3, Level = EventLevel.Verbose)]
		internal void ExitScope(long scopeId)
		{
			base.WriteEvent(3, scopeId);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002B66E File Offset: 0x0002986E
		public DataCommonEventSource()
		{
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002B676 File Offset: 0x00029876
		// Note: this type is marked as 'beforefieldinit'.
		static DataCommonEventSource()
		{
		}

		// Token: 0x0400076D RID: 1901
		internal static readonly DataCommonEventSource Log = new DataCommonEventSource();

		// Token: 0x0400076E RID: 1902
		private static long s_nextScopeId = 0L;

		// Token: 0x0400076F RID: 1903
		private const int TraceEventId = 1;

		// Token: 0x04000770 RID: 1904
		private const int EnterScopeId = 2;

		// Token: 0x04000771 RID: 1905
		private const int ExitScopeId = 3;
	}
}
