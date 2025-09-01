using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C8 RID: 456
	internal class CommandEventDispatchingStrategy : IEventDispatchingStrategy
	{
		// Token: 0x06000E94 RID: 3732 RVA: 0x0003D194 File Offset: 0x0003B394
		public bool CanDispatchEvent(EventBase evt)
		{
			return evt is ICommandEvent;
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0003D1B0 File Offset: 0x0003B3B0
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
						bool flag4 = !evt.isPropagationStopped && evt.propagateToIMGUI;
						if (flag4)
						{
							evt.skipElements.Add(imguicontainer);
							EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
						}
					}
					else
					{
						evt.target = panel.focusController.GetLeafFocusedElement();
						EventDispatchUtilities.PropagateEvent(evt);
						bool flag5 = !evt.isPropagationStopped && evt.propagateToIMGUI;
						if (flag5)
						{
							EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
						}
					}
				}
				else
				{
					EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
				}
			}
			evt.propagateToIMGUI = false;
			evt.stopDispatch = true;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x000020C2 File Offset: 0x000002C2
		public CommandEventDispatchingStrategy()
		{
		}
	}
}
