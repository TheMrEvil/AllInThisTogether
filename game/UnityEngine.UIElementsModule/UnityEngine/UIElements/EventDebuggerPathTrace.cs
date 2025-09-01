using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000243 RID: 579
	internal class EventDebuggerPathTrace : EventDebuggerTrace
	{
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x000454BA File Offset: 0x000436BA
		public PropagationPaths paths
		{
			[CompilerGenerated]
			get
			{
				return this.<paths>k__BackingField;
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x000454C2 File Offset: 0x000436C2
		public EventDebuggerPathTrace(IPanel panel, EventBase evt, PropagationPaths paths) : base(panel, evt, -1L, null)
		{
			this.paths = paths;
		}

		// Token: 0x040007C8 RID: 1992
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly PropagationPaths <paths>k__BackingField;
	}
}
