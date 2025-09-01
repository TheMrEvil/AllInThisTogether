using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000206 RID: 518
	internal class NavigationEventDispatchingStrategy : IEventDispatchingStrategy
	{
		// Token: 0x0600101F RID: 4127 RVA: 0x00041688 File Offset: 0x0003F888
		public bool CanDispatchEvent(EventBase evt)
		{
			return evt is INavigationEvent;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x000416A4 File Offset: 0x0003F8A4
		public void DispatchEvent(EventBase evt, IPanel panel)
		{
			bool flag = panel != null;
			if (flag)
			{
				evt.target = (panel.focusController.GetLeafFocusedElement() ?? panel.visualTree);
				EventDispatchUtilities.PropagateEvent(evt);
			}
			evt.propagateToIMGUI = false;
			evt.stopDispatch = true;
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x000020C2 File Offset: 0x000002C2
		public NavigationEventDispatchingStrategy()
		{
		}
	}
}
