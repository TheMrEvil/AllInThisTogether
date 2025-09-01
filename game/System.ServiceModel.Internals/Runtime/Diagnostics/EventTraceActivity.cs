using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Diagnostics
{
	// Token: 0x0200004B RID: 75
	internal class EventTraceActivity
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x0000F9E7 File Offset: 0x0000DBE7
		public EventTraceActivity(bool setOnThread = false) : this(Guid.NewGuid(), setOnThread)
		{
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000F9F5 File Offset: 0x0000DBF5
		public EventTraceActivity(Guid guid, bool setOnThread = false)
		{
			this.ActivityId = guid;
			if (setOnThread)
			{
				this.SetActivityIdOnThread();
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000FA0D File Offset: 0x0000DC0D
		public static EventTraceActivity Empty
		{
			get
			{
				if (EventTraceActivity.empty == null)
				{
					EventTraceActivity.empty = new EventTraceActivity(Guid.Empty, false);
				}
				return EventTraceActivity.empty;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000FA2B File Offset: 0x0000DC2B
		public static string Name
		{
			get
			{
				return "E2EActivity";
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000FA34 File Offset: 0x0000DC34
		[SecuritySafeCritical]
		public static EventTraceActivity GetFromThreadOrCreate(bool clearIdOnThread = false)
		{
			Guid guid = Trace.CorrelationManager.ActivityId;
			if (guid == Guid.Empty)
			{
				guid = Guid.NewGuid();
			}
			else if (clearIdOnThread)
			{
				Trace.CorrelationManager.ActivityId = Guid.Empty;
			}
			return new EventTraceActivity(guid, false);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000FA7A File Offset: 0x0000DC7A
		[SecuritySafeCritical]
		public static Guid GetActivityIdFromThread()
		{
			return Trace.CorrelationManager.ActivityId;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000FA86 File Offset: 0x0000DC86
		public void SetActivityId(Guid guid)
		{
			this.ActivityId = guid;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000FA8F File Offset: 0x0000DC8F
		[SecuritySafeCritical]
		private void SetActivityIdOnThread()
		{
			Trace.CorrelationManager.ActivityId = this.ActivityId;
		}

		// Token: 0x040001E2 RID: 482
		public Guid ActivityId;

		// Token: 0x040001E3 RID: 483
		private static EventTraceActivity empty;
	}
}
