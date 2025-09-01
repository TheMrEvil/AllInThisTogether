using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020009FF RID: 2559
	public class IncrementingEventCounter : DiagnosticCounter
	{
		// Token: 0x06005B46 RID: 23366 RVA: 0x00134444 File Offset: 0x00132644
		public IncrementingEventCounter(string name, EventSource eventSource) : base(name, eventSource)
		{
		}

		// Token: 0x06005B47 RID: 23367 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Increment(double increment = 1.0)
		{
		}

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06005B48 RID: 23368 RVA: 0x00134903 File Offset: 0x00132B03
		// (set) Token: 0x06005B49 RID: 23369 RVA: 0x0013490B File Offset: 0x00132B0B
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

		// Token: 0x0400384A RID: 14410
		[CompilerGenerated]
		private TimeSpan <DisplayRateTimeScale>k__BackingField;
	}
}
