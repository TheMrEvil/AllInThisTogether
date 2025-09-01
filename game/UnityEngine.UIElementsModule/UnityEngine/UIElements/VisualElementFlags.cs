using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E0 RID: 224
	[Flags]
	internal enum VisualElementFlags
	{
		// Token: 0x040002E5 RID: 741
		WorldTransformDirty = 1,
		// Token: 0x040002E6 RID: 742
		WorldTransformInverseDirty = 2,
		// Token: 0x040002E7 RID: 743
		WorldClipDirty = 4,
		// Token: 0x040002E8 RID: 744
		BoundingBoxDirty = 8,
		// Token: 0x040002E9 RID: 745
		WorldBoundingBoxDirty = 16,
		// Token: 0x040002EA RID: 746
		LayoutManual = 32,
		// Token: 0x040002EB RID: 747
		CompositeRoot = 64,
		// Token: 0x040002EC RID: 748
		RequireMeasureFunction = 128,
		// Token: 0x040002ED RID: 749
		EnableViewDataPersistence = 256,
		// Token: 0x040002EE RID: 750
		DisableClipping = 512,
		// Token: 0x040002EF RID: 751
		NeedsAttachToPanelEvent = 1024,
		// Token: 0x040002F0 RID: 752
		HierarchyDisplayed = 2048,
		// Token: 0x040002F1 RID: 753
		StyleInitialized = 4096,
		// Token: 0x040002F2 RID: 754
		Init = 2079
	}
}
