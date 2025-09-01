using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021D RID: 541
	public sealed class PointerDownEvent : PointerEventBase<PointerDownEvent>
	{
		// Token: 0x060010B7 RID: 4279 RVA: 0x00042C9B File Offset: 0x00040E9B
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00042CAC File Offset: 0x00040EAC
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable | EventBase.EventPropagation.SkipDisabledElements);
			((IPointerEventInternal)this).triggeredByOS = true;
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = true;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00042CC8 File Offset: 0x00040EC8
		public PointerDownEvent()
		{
			this.LocalInit();
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00042CDC File Offset: 0x00040EDC
		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = !base.isDefaultPrevented;
			if (flag)
			{
				bool flag2 = panel.ShouldSendCompatibilityMouseEvents(this);
				if (flag2)
				{
					using (MouseDownEvent pooled = MouseDownEvent.GetPooled(this))
					{
						pooled.target = base.target;
						pooled.target.SendEvent(pooled);
					}
				}
			}
			else
			{
				panel.PreventCompatibilityMouseEvents(base.pointerId);
			}
			base.PostDispatch(panel);
		}
	}
}
