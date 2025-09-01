using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A00 RID: 2560
	public class IncrementingPollingCounter : DiagnosticCounter
	{
		// Token: 0x06005B4A RID: 23370 RVA: 0x00134444 File Offset: 0x00132644
		public IncrementingPollingCounter(string name, EventSource eventSource, Func<double> totalValueProvider) : base(name, eventSource)
		{
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06005B4B RID: 23371 RVA: 0x00134914 File Offset: 0x00132B14
		// (set) Token: 0x06005B4C RID: 23372 RVA: 0x0013491C File Offset: 0x00132B1C
		public TimeSpan DisplayRateTimeScale
		{
			[CompilerGenerated]
			get
			{
				return this.<DisplayRateTimeScale>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DisplayRateTimeScale>k__BackingField = value;
			}
		}

		// Token: 0x0400384B RID: 14411
		[CompilerGenerated]
		private TimeSpan <DisplayRateTimeScale>k__BackingField;
	}
}
