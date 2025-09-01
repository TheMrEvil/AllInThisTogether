using System;
using System.Diagnostics.Tracing;

namespace System.Collections.Concurrent
{
	// Token: 0x0200049C RID: 1180
	[EventSource(Name = "System.Collections.Concurrent.ConcurrentCollectionsEventSource", Guid = "35167F8E-49B2-4b96-AB86-435B59336B5E")]
	internal sealed class CDSCollectionETWBCLProvider : EventSource
	{
		// Token: 0x060025D9 RID: 9689 RVA: 0x000848B9 File Offset: 0x00082AB9
		private CDSCollectionETWBCLProvider()
		{
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x000848C1 File Offset: 0x00082AC1
		[Event(1, Level = EventLevel.Warning)]
		public void ConcurrentStack_FastPushFailed(int spinCount)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(1, spinCount);
			}
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x000848D6 File Offset: 0x00082AD6
		[Event(2, Level = EventLevel.Warning)]
		public void ConcurrentStack_FastPopFailed(int spinCount)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(2, spinCount);
			}
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x000848EB File Offset: 0x00082AEB
		[Event(3, Level = EventLevel.Warning)]
		public void ConcurrentDictionary_AcquiringAllLocks(int numOfBuckets)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(3, numOfBuckets);
			}
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x00084900 File Offset: 0x00082B00
		[Event(4, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryTakeSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(4);
			}
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x00084914 File Offset: 0x00082B14
		[Event(5, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryPeekSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(5);
			}
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x00084928 File Offset: 0x00082B28
		// Note: this type is marked as 'beforefieldinit'.
		static CDSCollectionETWBCLProvider()
		{
		}

		// Token: 0x040014C4 RID: 5316
		public static CDSCollectionETWBCLProvider Log = new CDSCollectionETWBCLProvider();

		// Token: 0x040014C5 RID: 5317
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x040014C6 RID: 5318
		private const int CONCURRENTSTACK_FASTPUSHFAILED_ID = 1;

		// Token: 0x040014C7 RID: 5319
		private const int CONCURRENTSTACK_FASTPOPFAILED_ID = 2;

		// Token: 0x040014C8 RID: 5320
		private const int CONCURRENTDICTIONARY_ACQUIRINGALLLOCKS_ID = 3;

		// Token: 0x040014C9 RID: 5321
		private const int CONCURRENTBAG_TRYTAKESTEALS_ID = 4;

		// Token: 0x040014CA RID: 5322
		private const int CONCURRENTBAG_TRYPEEKSTEALS_ID = 5;
	}
}
