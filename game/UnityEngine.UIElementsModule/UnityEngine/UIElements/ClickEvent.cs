using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000222 RID: 546
	public sealed class ClickEvent : PointerEventBase<ClickEvent>
	{
		// Token: 0x060010CC RID: 4300 RVA: 0x000430DC File Offset: 0x000412DC
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00040A2B File Offset: 0x0003EC2B
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable | EventBase.EventPropagation.SkipDisabledElements);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x000430ED File Offset: 0x000412ED
		public ClickEvent()
		{
			this.LocalInit();
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00043100 File Offset: 0x00041300
		internal static ClickEvent GetPooled(PointerUpEvent pointerEvent, int clickCount)
		{
			ClickEvent pooled = PointerEventBase<ClickEvent>.GetPooled(pointerEvent);
			pooled.clickCount = clickCount;
			return pooled;
		}
	}
}
