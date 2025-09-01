using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001BF RID: 447
	public abstract class PointerCaptureEventBase<T> : EventBase<T>, IPointerCaptureEvent, IPointerCaptureEventInternal where T : PointerCaptureEventBase<T>, new()
	{
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x0003CFED File Offset: 0x0003B1ED
		// (set) Token: 0x06000E7D RID: 3709 RVA: 0x0003CFF5 File Offset: 0x0003B1F5
		public IEventHandler relatedTarget
		{
			[CompilerGenerated]
			get
			{
				return this.<relatedTarget>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<relatedTarget>k__BackingField = value;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0003CFFE File Offset: 0x0003B1FE
		// (set) Token: 0x06000E7F RID: 3711 RVA: 0x0003D006 File Offset: 0x0003B206
		public int pointerId
		{
			[CompilerGenerated]
			get
			{
				return this.<pointerId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<pointerId>k__BackingField = value;
			}
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0003D00F File Offset: 0x0003B20F
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0003D020 File Offset: 0x0003B220
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown);
			this.relatedTarget = null;
			this.pointerId = PointerId.invalidPointerId;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0003D040 File Offset: 0x0003B240
		public static T GetPooled(IEventHandler target, IEventHandler relatedTarget, int pointerId)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.target = target;
			pooled.relatedTarget = relatedTarget;
			pooled.pointerId = pointerId;
			return pooled;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0003D080 File Offset: 0x0003B280
		protected PointerCaptureEventBase()
		{
			this.LocalInit();
		}

		// Token: 0x040006BF RID: 1727
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IEventHandler <relatedTarget>k__BackingField;

		// Token: 0x040006C0 RID: 1728
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <pointerId>k__BackingField;
	}
}
