using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001EC RID: 492
	internal class KeyboardEventDispatchingStrategy : IEventDispatchingStrategy
	{
		// Token: 0x06000F78 RID: 3960 RVA: 0x0003FA40 File Offset: 0x0003DC40
		public bool CanDispatchEvent(EventBase evt)
		{
			return evt is IKeyboardEvent;
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0003FA5C File Offset: 0x0003DC5C
		public void DispatchEvent(EventBase evt, IPanel panel)
		{
			bool flag = panel != null;
			if (flag)
			{
				Focusable leafFocusedElement = panel.focusController.GetLeafFocusedElement();
				bool flag2 = leafFocusedElement != null;
				if (flag2)
				{
					bool isIMGUIContainer = leafFocusedElement.isIMGUIContainer;
					if (isIMGUIContainer)
					{
						IMGUIContainer imguicontainer = (IMGUIContainer)leafFocusedElement;
						bool flag3 = !evt.Skip(imguicontainer) && imguicontainer.SendEventToIMGUI(evt, true, true);
						if (flag3)
						{
							evt.StopPropagation();
							evt.PreventDefault();
						}
					}
					else
					{
						evt.target = leafFocusedElement;
						EventDispatchUtilities.PropagateEvent(evt);
					}
				}
				else
				{
					evt.target = panel.visualTree;
					EventDispatchUtilities.PropagateEvent(evt);
					bool flag4 = !evt.isPropagationStopped;
					if (flag4)
					{
						EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
					}
				}
			}
			evt.propagateToIMGUI = false;
			evt.stopDispatch = true;
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x000020C2 File Offset: 0x000002C2
		public KeyboardEventDispatchingStrategy()
		{
		}
	}
}
