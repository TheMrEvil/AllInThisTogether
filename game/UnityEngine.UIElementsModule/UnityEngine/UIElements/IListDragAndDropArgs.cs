using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B1 RID: 433
	internal interface IListDragAndDropArgs
	{
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000E35 RID: 3637
		object target { get; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000E36 RID: 3638
		int insertAtIndex { get; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000E37 RID: 3639
		int parentId { get; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000E38 RID: 3640
		int childIndex { get; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000E39 RID: 3641
		IDragAndDropData dragAndDropData { get; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000E3A RID: 3642
		DragAndDropPosition dragAndDropPosition { get; }
	}
}
