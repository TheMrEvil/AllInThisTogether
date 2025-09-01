using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A6 RID: 422
	internal static class DragAndDropUtility
	{
		// Token: 0x06000DF0 RID: 3568 RVA: 0x0003A4C0 File Offset: 0x000386C0
		internal static IDragAndDrop GetDragAndDrop(IPanel panel)
		{
			bool flag = panel.contextType == ContextType.Player;
			IDragAndDrop result;
			if (flag)
			{
				IDragAndDrop dragAndDrop;
				if ((dragAndDrop = DragAndDropUtility.s_DragAndDropPlayMode) == null)
				{
					dragAndDrop = (DragAndDropUtility.s_DragAndDropPlayMode = new DefaultDragAndDropClient());
				}
				result = dragAndDrop;
			}
			else
			{
				IDragAndDrop dragAndDrop2;
				if ((dragAndDrop2 = DragAndDropUtility.s_DragAndDropEditor) == null)
				{
					IDragAndDrop dragAndDrop4;
					if (DragAndDropUtility.s_MakeDragAndDropClientFunc == null)
					{
						IDragAndDrop dragAndDrop3 = new DefaultDragAndDropClient();
						dragAndDrop4 = dragAndDrop3;
					}
					else
					{
						dragAndDrop4 = DragAndDropUtility.s_MakeDragAndDropClientFunc();
					}
					dragAndDrop2 = (DragAndDropUtility.s_DragAndDropEditor = dragAndDrop4);
				}
				result = dragAndDrop2;
			}
			return result;
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0003A520 File Offset: 0x00038720
		internal static void RegisterMakeClientFunc(Func<IDragAndDrop> makeClient)
		{
			DragAndDropUtility.s_MakeDragAndDropClientFunc = makeClient;
			DragAndDropUtility.s_DragAndDropEditor = null;
		}

		// Token: 0x0400067E RID: 1662
		private static Func<IDragAndDrop> s_MakeDragAndDropClientFunc;

		// Token: 0x0400067F RID: 1663
		private static IDragAndDrop s_DragAndDropEditor;

		// Token: 0x04000680 RID: 1664
		private static IDragAndDrop s_DragAndDropPlayMode;
	}
}
