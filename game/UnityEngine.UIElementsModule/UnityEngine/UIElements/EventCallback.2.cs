using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D5 RID: 469
	// (Invoke) Token: 0x06000F08 RID: 3848
	public delegate void EventCallback<in TEventType, in TCallbackArgs>(TEventType evt, TCallbackArgs userArgs);
}
