using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001DF RID: 479
	public interface IEventHandler
	{
		// Token: 0x06000F3B RID: 3899
		void SendEvent(EventBase e);

		// Token: 0x06000F3C RID: 3900
		void HandleEvent(EventBase evt);

		// Token: 0x06000F3D RID: 3901
		bool HasTrickleDownHandlers();

		// Token: 0x06000F3E RID: 3902
		bool HasBubbleUpHandlers();
	}
}
