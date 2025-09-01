using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001FF RID: 511
	public class MouseEnterWindowEvent : MouseEventBase<MouseEnterWindowEvent>
	{
		// Token: 0x06001007 RID: 4103 RVA: 0x00040D78 File Offset: 0x0003EF78
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00040D89 File Offset: 0x0003EF89
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Cancellable;
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00040D94 File Offset: 0x0003EF94
		public MouseEnterWindowEvent()
		{
			this.LocalInit();
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00040DA8 File Offset: 0x0003EFA8
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
