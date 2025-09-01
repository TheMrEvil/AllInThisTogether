using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200003B RID: 59
	internal interface ILiveReloadAssetTracker<T> where T : ScriptableObject
	{
		// Token: 0x06000165 RID: 357
		int StartTrackingAsset(T asset);

		// Token: 0x06000166 RID: 358
		void StopTrackingAsset(T asset);

		// Token: 0x06000167 RID: 359
		bool IsTrackingAsset(T asset);

		// Token: 0x06000168 RID: 360
		bool IsTrackingAssets();

		// Token: 0x06000169 RID: 361
		bool CheckTrackedAssetsDirty();

		// Token: 0x0600016A RID: 362
		void UpdateAssetTrackerCounts(T asset, int newDirtyCount, int newElementCount, int newInlinePropertiesCount, int newAttributePropertiesDirtyCount);

		// Token: 0x0600016B RID: 363
		void OnAssetsImported(HashSet<T> changedAssets, HashSet<string> deletedAssets);

		// Token: 0x0600016C RID: 364
		void OnTrackedAssetChanged();
	}
}
