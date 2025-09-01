using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001FD RID: 509
	public class MouseEnterEvent : MouseEventBase<MouseEnterEvent>
	{
		// Token: 0x06001001 RID: 4097 RVA: 0x00040D28 File Offset: 0x0003EF28
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00040D39 File Offset: 0x0003EF39
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable | EventBase.EventPropagation.IgnoreCompositeRoots);
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00040D45 File Offset: 0x0003EF45
		public MouseEnterEvent()
		{
			this.LocalInit();
		}
	}
}
