using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000200 RID: 512
	public class MouseLeaveWindowEvent : MouseEventBase<MouseLeaveWindowEvent>
	{
		// Token: 0x0600100B RID: 4107 RVA: 0x00040DE6 File Offset: 0x0003EFE6
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x00040DF7 File Offset: 0x0003EFF7
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Cancellable;
			((IMouseEventInternal)this).recomputeTopElementUnderMouse = false;
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00040E0A File Offset: 0x0003F00A
		public MouseLeaveWindowEvent()
		{
			this.LocalInit();
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00040E1C File Offset: 0x0003F01C
		public new static MouseLeaveWindowEvent GetPooled(Event systemEvent)
		{
			bool flag = systemEvent != null;
			if (flag)
			{
				PointerDeviceState.ReleaseAllButtons(PointerId.mousePointerId);
			}
			return MouseEventBase<MouseLeaveWindowEvent>.GetPooled(systemEvent);
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00040E48 File Offset: 0x0003F048
		protected internal override void PostDispatch(IPanel panel)
		{
			EventBase eventBase = ((IMouseEventInternal)this).sourcePointerEvent as EventBase;
			bool flag = eventBase == null;
			if (flag)
			{
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.CommitElementUnderPointers();
				}
			}
			base.PostDispatch(panel);
		}
	}
}
