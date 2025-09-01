using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000224 RID: 548
	public sealed class PointerLeaveEvent : PointerEventBase<PointerLeaveEvent>
	{
		// Token: 0x060010D3 RID: 4307 RVA: 0x00043150 File Offset: 0x00041350
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00043133 File Offset: 0x00041333
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.IgnoreCompositeRoots);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00043161 File Offset: 0x00041361
		public PointerLeaveEvent()
		{
			this.LocalInit();
		}
	}
}
