using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000223 RID: 547
	public sealed class PointerEnterEvent : PointerEventBase<PointerEnterEvent>
	{
		// Token: 0x060010D0 RID: 4304 RVA: 0x00043122 File Offset: 0x00041322
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00043133 File Offset: 0x00041333
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.IgnoreCompositeRoots);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0004313F File Offset: 0x0004133F
		public PointerEnterEvent()
		{
			this.LocalInit();
		}
	}
}
