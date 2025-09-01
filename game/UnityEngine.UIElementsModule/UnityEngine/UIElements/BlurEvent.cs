using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E4 RID: 484
	public class BlurEvent : FocusEventBase<BlurEvent>
	{
		// Token: 0x06000F61 RID: 3937 RVA: 0x0003F4AC File Offset: 0x0003D6AC
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool flag = base.relatedTarget == null;
			if (flag)
			{
				base.focusController.DoFocusChange(null);
			}
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0003F4DE File Offset: 0x0003D6DE
		public BlurEvent()
		{
		}
	}
}
