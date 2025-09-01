using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000241 RID: 577
	internal class EventDebuggerCallTrace : EventDebuggerTrace
	{
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x00045413 File Offset: 0x00043613
		public int callbackHashCode
		{
			[CompilerGenerated]
			get
			{
				return this.<callbackHashCode>k__BackingField;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x0004541B File Offset: 0x0004361B
		public string callbackName
		{
			[CompilerGenerated]
			get
			{
				return this.<callbackName>k__BackingField;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x00045423 File Offset: 0x00043623
		public bool propagationHasStopped
		{
			[CompilerGenerated]
			get
			{
				return this.<propagationHasStopped>k__BackingField;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x0004542B File Offset: 0x0004362B
		public bool immediatePropagationHasStopped
		{
			[CompilerGenerated]
			get
			{
				return this.<immediatePropagationHasStopped>k__BackingField;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x00045433 File Offset: 0x00043633
		public bool defaultHasBeenPrevented
		{
			[CompilerGenerated]
			get
			{
				return this.<defaultHasBeenPrevented>k__BackingField;
			}
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0004543B File Offset: 0x0004363B
		public EventDebuggerCallTrace(IPanel panel, EventBase evt, int cbHashCode, string cbName, bool propagationHasStopped, bool immediatePropagationHasStopped, bool defaultHasBeenPrevented, long duration, IEventHandler mouseCapture) : base(panel, evt, duration, mouseCapture)
		{
			this.callbackHashCode = cbHashCode;
			this.callbackName = cbName;
			this.propagationHasStopped = propagationHasStopped;
			this.immediatePropagationHasStopped = immediatePropagationHasStopped;
			this.defaultHasBeenPrevented = defaultHasBeenPrevented;
		}

		// Token: 0x040007C2 RID: 1986
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly int <callbackHashCode>k__BackingField;

		// Token: 0x040007C3 RID: 1987
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string <callbackName>k__BackingField;

		// Token: 0x040007C4 RID: 1988
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly bool <propagationHasStopped>k__BackingField;

		// Token: 0x040007C5 RID: 1989
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly bool <immediatePropagationHasStopped>k__BackingField;

		// Token: 0x040007C6 RID: 1990
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly bool <defaultHasBeenPrevented>k__BackingField;
	}
}
