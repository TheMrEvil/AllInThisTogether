using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200013B RID: 315
	internal interface IGenericMenu
	{
		// Token: 0x06000A66 RID: 2662
		void AddItem(string itemName, bool isChecked, Action action);

		// Token: 0x06000A67 RID: 2663
		void AddItem(string itemName, bool isChecked, Action<object> action, object data);

		// Token: 0x06000A68 RID: 2664
		void AddDisabledItem(string itemName, bool isChecked);

		// Token: 0x06000A69 RID: 2665
		void AddSeparator(string path);

		// Token: 0x06000A6A RID: 2666
		void DropDown(Rect position, VisualElement targetElement = null, bool anchored = false);
	}
}
