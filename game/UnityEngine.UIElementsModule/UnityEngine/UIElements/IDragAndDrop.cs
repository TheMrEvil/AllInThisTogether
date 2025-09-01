using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001AB RID: 427
	internal interface IDragAndDrop
	{
		// Token: 0x06000E15 RID: 3605
		void StartDrag(StartDragArgs args, Vector3 pointerPosition);

		// Token: 0x06000E16 RID: 3606
		void UpdateDrag(Vector3 pointerPosition);

		// Token: 0x06000E17 RID: 3607
		void AcceptDrag();

		// Token: 0x06000E18 RID: 3608
		void DragCleanup();

		// Token: 0x06000E19 RID: 3609
		void SetVisualMode(DragVisualMode visualMode);

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000E1A RID: 3610
		DragAndDropData data { get; }
	}
}
