using System;

namespace System.Threading
{
	// Token: 0x020002A5 RID: 677
	internal static class PlatformHelper
	{
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001E06 RID: 7686 RVA: 0x0006F6A0 File Offset: 0x0006D8A0
		internal static int ProcessorCount
		{
			get
			{
				int tickCount = Environment.TickCount;
				int num = PlatformHelper.s_processorCount;
				if (num == 0 || tickCount - PlatformHelper.s_lastProcessorCountRefreshTicks >= 30000)
				{
					num = (PlatformHelper.s_processorCount = Environment.ProcessorCount);
					PlatformHelper.s_lastProcessorCountRefreshTicks = tickCount;
				}
				return num;
			}
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x0006F6E5 File Offset: 0x0006D8E5
		// Note: this type is marked as 'beforefieldinit'.
		static PlatformHelper()
		{
		}

		// Token: 0x04001A73 RID: 6771
		private const int PROCESSOR_COUNT_REFRESH_INTERVAL_MS = 30000;

		// Token: 0x04001A74 RID: 6772
		private static volatile int s_processorCount;

		// Token: 0x04001A75 RID: 6773
		private static volatile int s_lastProcessorCountRefreshTicks;

		// Token: 0x04001A76 RID: 6774
		internal static readonly bool IsSingleProcessor = PlatformHelper.ProcessorCount == 1;
	}
}
