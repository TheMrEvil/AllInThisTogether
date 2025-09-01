using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000240 RID: 576
	internal class EventDebuggerTrace
	{
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x0004539A File Offset: 0x0004359A
		public EventDebuggerEventRecord eventBase
		{
			[CompilerGenerated]
			get
			{
				return this.<eventBase>k__BackingField;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x000453A2 File Offset: 0x000435A2
		public IEventHandler focusedElement
		{
			[CompilerGenerated]
			get
			{
				return this.<focusedElement>k__BackingField;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x000453AA File Offset: 0x000435AA
		public IEventHandler mouseCapture
		{
			[CompilerGenerated]
			get
			{
				return this.<mouseCapture>k__BackingField;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x000453B2 File Offset: 0x000435B2
		// (set) Token: 0x06001182 RID: 4482 RVA: 0x000453BA File Offset: 0x000435BA
		public long duration
		{
			[CompilerGenerated]
			get
			{
				return this.<duration>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<duration>k__BackingField = value;
			}
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x000453C4 File Offset: 0x000435C4
		public EventDebuggerTrace(IPanel panel, EventBase evt, long duration, IEventHandler mouseCapture)
		{
			this.eventBase = new EventDebuggerEventRecord(evt);
			object obj;
			if (panel == null)
			{
				obj = null;
			}
			else
			{
				FocusController focusController = panel.focusController;
				obj = ((focusController != null) ? focusController.focusedElement : null);
			}
			this.focusedElement = obj;
			this.mouseCapture = mouseCapture;
			this.duration = duration;
		}

		// Token: 0x040007BE RID: 1982
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly EventDebuggerEventRecord <eventBase>k__BackingField;

		// Token: 0x040007BF RID: 1983
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly IEventHandler <focusedElement>k__BackingField;

		// Token: 0x040007C0 RID: 1984
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly IEventHandler <mouseCapture>k__BackingField;

		// Token: 0x040007C1 RID: 1985
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private long <duration>k__BackingField;
	}
}
