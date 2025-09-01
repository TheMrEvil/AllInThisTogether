using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E5 RID: 485
	public class FocusInEvent : FocusEventBase<FocusInEvent>
	{
		// Token: 0x06000F63 RID: 3939 RVA: 0x0003F4E7 File Offset: 0x0003D6E7
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0003F48D File Offset: 0x0003D68D
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown);
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0003F4F8 File Offset: 0x0003D6F8
		public FocusInEvent()
		{
			this.LocalInit();
		}
	}
}
