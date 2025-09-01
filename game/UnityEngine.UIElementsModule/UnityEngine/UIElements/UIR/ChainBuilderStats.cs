using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000310 RID: 784
	internal struct ChainBuilderStats
	{
		// Token: 0x04000B3D RID: 2877
		public uint elementsAdded;

		// Token: 0x04000B3E RID: 2878
		public uint elementsRemoved;

		// Token: 0x04000B3F RID: 2879
		public uint recursiveClipUpdates;

		// Token: 0x04000B40 RID: 2880
		public uint recursiveClipUpdatesExpanded;

		// Token: 0x04000B41 RID: 2881
		public uint nonRecursiveClipUpdates;

		// Token: 0x04000B42 RID: 2882
		public uint recursiveTransformUpdates;

		// Token: 0x04000B43 RID: 2883
		public uint recursiveTransformUpdatesExpanded;

		// Token: 0x04000B44 RID: 2884
		public uint recursiveOpacityUpdates;

		// Token: 0x04000B45 RID: 2885
		public uint recursiveOpacityUpdatesExpanded;

		// Token: 0x04000B46 RID: 2886
		public uint colorUpdates;

		// Token: 0x04000B47 RID: 2887
		public uint colorUpdatesExpanded;

		// Token: 0x04000B48 RID: 2888
		public uint recursiveVisualUpdates;

		// Token: 0x04000B49 RID: 2889
		public uint recursiveVisualUpdatesExpanded;

		// Token: 0x04000B4A RID: 2890
		public uint nonRecursiveVisualUpdates;

		// Token: 0x04000B4B RID: 2891
		public uint dirtyProcessed;

		// Token: 0x04000B4C RID: 2892
		public uint nudgeTransformed;

		// Token: 0x04000B4D RID: 2893
		public uint boneTransformed;

		// Token: 0x04000B4E RID: 2894
		public uint skipTransformed;

		// Token: 0x04000B4F RID: 2895
		public uint visualUpdateTransformed;

		// Token: 0x04000B50 RID: 2896
		public uint updatedMeshAllocations;

		// Token: 0x04000B51 RID: 2897
		public uint newMeshAllocations;

		// Token: 0x04000B52 RID: 2898
		public uint groupTransformElementsChanged;

		// Token: 0x04000B53 RID: 2899
		public uint immedateRenderersActive;

		// Token: 0x04000B54 RID: 2900
		public uint textUpdates;
	}
}
