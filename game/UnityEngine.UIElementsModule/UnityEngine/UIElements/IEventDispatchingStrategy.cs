using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E8 RID: 488
	internal interface IEventDispatchingStrategy
	{
		// Token: 0x06000F68 RID: 3944
		bool CanDispatchEvent(EventBase evt);

		// Token: 0x06000F69 RID: 3945
		void DispatchEvent(EventBase evt, IPanel panel);
	}
}
