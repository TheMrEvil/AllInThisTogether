using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000220 RID: 544
	public sealed class PointerUpEvent : PointerEventBase<PointerUpEvent>
	{
		// Token: 0x060010C4 RID: 4292 RVA: 0x00042F29 File Offset: 0x00041129
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00042CAC File Offset: 0x00040EAC
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable | EventBase.EventPropagation.SkipDisabledElements);
			((IPointerEventInternal)this).triggeredByOS = true;
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = true;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00042F3A File Offset: 0x0004113A
		public PointerUpEvent()
		{
			this.LocalInit();
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00042F4C File Offset: 0x0004114C
		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = PointerType.IsDirectManipulationDevice(base.pointerType);
			if (flag)
			{
				panel.ReleasePointer(base.pointerId);
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.ClearCachedElementUnderPointer(base.pointerId, this);
				}
			}
			bool flag2 = panel.ShouldSendCompatibilityMouseEvents(this);
			if (flag2)
			{
				using (MouseUpEvent pooled = MouseUpEvent.GetPooled(this))
				{
					pooled.target = base.target;
					pooled.target.SendEvent(pooled);
				}
			}
			base.PostDispatch(panel);
			panel.ActivateCompatibilityMouseEvents(base.pointerId);
		}
	}
}
