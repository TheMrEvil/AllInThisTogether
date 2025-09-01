using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001AE RID: 430
	internal interface IDragAndDropController<in TArgs>
	{
		// Token: 0x06000E25 RID: 3621
		bool CanStartDrag(IEnumerable<int> itemIndices);

		// Token: 0x06000E26 RID: 3622
		StartDragArgs SetupDragAndDrop(IEnumerable<int> itemIndices, bool skipText = false);

		// Token: 0x06000E27 RID: 3623
		DragVisualMode HandleDragAndDrop(TArgs args);

		// Token: 0x06000E28 RID: 3624
		void OnDrop(TArgs args);

		// Token: 0x06000E29 RID: 3625
		void DragCleanup();

		// Token: 0x06000E2A RID: 3626
		IEnumerable<int> GetSortedSelectedIds();
	}
}
