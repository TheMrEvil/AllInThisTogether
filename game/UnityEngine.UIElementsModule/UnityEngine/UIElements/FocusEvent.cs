using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E6 RID: 486
	public class FocusEvent : FocusEventBase<FocusEvent>
	{
		// Token: 0x06000F66 RID: 3942 RVA: 0x0003F509 File Offset: 0x0003D709
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			base.focusController.DoFocusChange(base.target as Focusable);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0003F52B File Offset: 0x0003D72B
		public FocusEvent()
		{
		}
	}
}
