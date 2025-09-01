using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000242 RID: 578
	internal class EventDebuggerDefaultActionTrace : EventDebuggerTrace
	{
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x00045472 File Offset: 0x00043672
		public PropagationPhase phase
		{
			[CompilerGenerated]
			get
			{
				return this.<phase>k__BackingField;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x0004547C File Offset: 0x0004367C
		public string targetName
		{
			get
			{
				return base.eventBase.target.GetType().FullName;
			}
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x000454A3 File Offset: 0x000436A3
		public EventDebuggerDefaultActionTrace(IPanel panel, EventBase evt, PropagationPhase phase, long duration, IEventHandler mouseCapture) : base(panel, evt, duration, mouseCapture)
		{
			this.phase = phase;
		}

		// Token: 0x040007C7 RID: 1991
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly PropagationPhase <phase>k__BackingField;
	}
}
