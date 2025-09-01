using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000221 RID: 545
	public sealed class PointerCancelEvent : PointerEventBase<PointerCancelEvent>
	{
		// Token: 0x060010C8 RID: 4296 RVA: 0x00042FF4 File Offset: 0x000411F4
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00043005 File Offset: 0x00041205
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements);
			((IPointerEventInternal)this).triggeredByOS = true;
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = true;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00043021 File Offset: 0x00041221
		public PointerCancelEvent()
		{
			this.LocalInit();
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00043034 File Offset: 0x00041234
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
					base.target.SendEvent(pooled);
				}
			}
			base.PostDispatch(panel);
			panel.ActivateCompatibilityMouseEvents(base.pointerId);
		}
	}
}
