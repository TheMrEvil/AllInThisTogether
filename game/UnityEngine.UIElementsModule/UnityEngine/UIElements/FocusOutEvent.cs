using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E3 RID: 483
	public class FocusOutEvent : FocusEventBase<FocusOutEvent>
	{
		// Token: 0x06000F5E RID: 3934 RVA: 0x0003F47C File Offset: 0x0003D67C
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0003F48D File Offset: 0x0003D68D
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0003F498 File Offset: 0x0003D698
		public FocusOutEvent()
		{
			this.LocalInit();
		}
	}
}
