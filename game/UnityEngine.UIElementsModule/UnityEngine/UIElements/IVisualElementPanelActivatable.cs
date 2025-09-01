using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F0 RID: 240
	internal interface IVisualElementPanelActivatable
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600077D RID: 1917
		VisualElement element { get; }

		// Token: 0x0600077E RID: 1918
		bool CanBeActivated();

		// Token: 0x0600077F RID: 1919
		void OnPanelActivate();

		// Token: 0x06000780 RID: 1920
		void OnPanelDeactivate();
	}
}
