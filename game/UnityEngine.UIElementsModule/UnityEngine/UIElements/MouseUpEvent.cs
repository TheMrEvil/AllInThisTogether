using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F9 RID: 505
	public class MouseUpEvent : MouseEventBase<MouseUpEvent>
	{
		// Token: 0x06000FEB RID: 4075 RVA: 0x00040AF0 File Offset: 0x0003ECF0
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00040A2B File Offset: 0x0003EC2B
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable | EventBase.EventPropagation.SkipDisabledElements);
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00040B01 File Offset: 0x0003ED01
		public MouseUpEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00040B14 File Offset: 0x0003ED14
		public new static MouseUpEvent GetPooled(Event systemEvent)
		{
			bool flag = systemEvent != null;
			if (flag)
			{
				PointerDeviceState.ReleaseButton(PointerId.mousePointerId, systemEvent.button);
			}
			return MouseEventBase<MouseUpEvent>.GetPooled(systemEvent);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00040B48 File Offset: 0x0003ED48
		private static MouseUpEvent MakeFromPointerEvent(IPointerEvent pointerEvent)
		{
			bool flag = pointerEvent != null && pointerEvent.button >= 0;
			if (flag)
			{
				PointerDeviceState.ReleaseButton(PointerId.mousePointerId, pointerEvent.button);
			}
			return MouseEventBase<MouseUpEvent>.GetPooled(pointerEvent);
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00040B8C File Offset: 0x0003ED8C
		internal static MouseUpEvent GetPooled(PointerUpEvent pointerEvent)
		{
			return MouseUpEvent.MakeFromPointerEvent(pointerEvent);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00040BA4 File Offset: 0x0003EDA4
		internal static MouseUpEvent GetPooled(PointerMoveEvent pointerEvent)
		{
			return MouseUpEvent.MakeFromPointerEvent(pointerEvent);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00040BBC File Offset: 0x0003EDBC
		internal static MouseUpEvent GetPooled(PointerCancelEvent pointerEvent)
		{
			return MouseUpEvent.MakeFromPointerEvent(pointerEvent);
		}
	}
}
