using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001FA RID: 506
	public class MouseMoveEvent : MouseEventBase<MouseMoveEvent>
	{
		// Token: 0x06000FF3 RID: 4083 RVA: 0x00040BD4 File Offset: 0x0003EDD4
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00040BE5 File Offset: 0x0003EDE5
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00040BF0 File Offset: 0x0003EDF0
		public MouseMoveEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00040C04 File Offset: 0x0003EE04
		public new static MouseMoveEvent GetPooled(Event systemEvent)
		{
			MouseMoveEvent pooled = MouseEventBase<MouseMoveEvent>.GetPooled(systemEvent);
			pooled.button = 0;
			return pooled;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00040C28 File Offset: 0x0003EE28
		internal static MouseMoveEvent GetPooled(PointerMoveEvent pointerEvent)
		{
			return MouseEventBase<MouseMoveEvent>.GetPooled(pointerEvent);
		}
	}
}
