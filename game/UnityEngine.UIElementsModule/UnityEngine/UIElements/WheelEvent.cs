using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001FC RID: 508
	public class WheelEvent : MouseEventBase<WheelEvent>
	{
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x00040C49 File Offset: 0x0003EE49
		// (set) Token: 0x06000FFA RID: 4090 RVA: 0x00040C51 File Offset: 0x0003EE51
		public Vector3 delta
		{
			[CompilerGenerated]
			get
			{
				return this.<delta>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<delta>k__BackingField = value;
			}
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00040C5C File Offset: 0x0003EE5C
		public new static WheelEvent GetPooled(Event systemEvent)
		{
			WheelEvent pooled = MouseEventBase<WheelEvent>.GetPooled(systemEvent);
			pooled.imguiEvent = systemEvent;
			bool flag = systemEvent != null;
			if (flag)
			{
				pooled.delta = systemEvent.delta;
			}
			return pooled;
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00040C9C File Offset: 0x0003EE9C
		internal static WheelEvent GetPooled(Vector3 delta, Vector3 mousePosition)
		{
			WheelEvent pooled = EventBase<WheelEvent>.GetPooled();
			pooled.delta = delta;
			pooled.mousePosition = mousePosition;
			return pooled;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00040CCC File Offset: 0x0003EECC
		internal static WheelEvent GetPooled(Vector3 delta, IPointerEvent pointerEvent)
		{
			WheelEvent pooled = MouseEventBase<WheelEvent>.GetPooled(pointerEvent);
			pooled.delta = delta;
			return pooled;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00040CEE File Offset: 0x0003EEEE
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00040CFF File Offset: 0x0003EEFF
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable | EventBase.EventPropagation.SkipDisabledElements);
			this.delta = Vector3.zero;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00040D17 File Offset: 0x0003EF17
		public WheelEvent()
		{
			this.LocalInit();
		}

		// Token: 0x0400072A RID: 1834
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Vector3 <delta>k__BackingField;
	}
}
