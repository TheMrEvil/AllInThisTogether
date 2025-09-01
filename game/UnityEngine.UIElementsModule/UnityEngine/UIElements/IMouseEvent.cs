using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F5 RID: 501
	public interface IMouseEvent
	{
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000FAC RID: 4012
		EventModifiers modifiers { get; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000FAD RID: 4013
		Vector2 mousePosition { get; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000FAE RID: 4014
		Vector2 localMousePosition { get; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000FAF RID: 4015
		Vector2 mouseDelta { get; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000FB0 RID: 4016
		int clickCount { get; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000FB1 RID: 4017
		int button { get; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000FB2 RID: 4018
		int pressedButtons { get; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000FB3 RID: 4019
		bool shiftKey { get; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000FB4 RID: 4020
		bool ctrlKey { get; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000FB5 RID: 4021
		bool commandKey { get; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000FB6 RID: 4022
		bool altKey { get; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000FB7 RID: 4023
		bool actionKey { get; }
	}
}
