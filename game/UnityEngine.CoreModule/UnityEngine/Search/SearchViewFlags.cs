using System;

namespace UnityEngine.Search
{
	// Token: 0x020002D2 RID: 722
	[Flags]
	public enum SearchViewFlags
	{
		// Token: 0x040009B8 RID: 2488
		None = 0,
		// Token: 0x040009B9 RID: 2489
		Debug = 16,
		// Token: 0x040009BA RID: 2490
		NoIndexing = 32,
		// Token: 0x040009BB RID: 2491
		Packages = 256,
		// Token: 0x040009BC RID: 2492
		OpenLeftSidePanel = 2048,
		// Token: 0x040009BD RID: 2493
		OpenInspectorPreview = 4096,
		// Token: 0x040009BE RID: 2494
		Centered = 8192,
		// Token: 0x040009BF RID: 2495
		HideSearchBar = 16384,
		// Token: 0x040009C0 RID: 2496
		CompactView = 32768,
		// Token: 0x040009C1 RID: 2497
		ListView = 65536,
		// Token: 0x040009C2 RID: 2498
		GridView = 131072,
		// Token: 0x040009C3 RID: 2499
		TableView = 262144,
		// Token: 0x040009C4 RID: 2500
		EnableSearchQuery = 524288,
		// Token: 0x040009C5 RID: 2501
		DisableInspectorPreview = 1048576,
		// Token: 0x040009C6 RID: 2502
		DisableSavedSearchQuery = 2097152,
		// Token: 0x040009C7 RID: 2503
		OpenInBuilderMode = 4194304,
		// Token: 0x040009C8 RID: 2504
		OpenInTextMode = 8388608,
		// Token: 0x040009C9 RID: 2505
		DisableBuilderModeToggle = 16777216,
		// Token: 0x040009CA RID: 2506
		Borderless = 33554432
	}
}
