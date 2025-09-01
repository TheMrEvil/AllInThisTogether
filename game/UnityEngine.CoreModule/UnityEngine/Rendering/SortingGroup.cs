using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x02000429 RID: 1065
	[RequireComponent(typeof(Transform))]
	[NativeType(Header = "Runtime/2D/Sorting/SortingGroup.h")]
	public sealed class SortingGroup : Behaviour
	{
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06002524 RID: 9508
		[StaticAccessor("SortingGroup", StaticAccessorType.DoubleColon)]
		internal static extern int invalidSortingGroupID { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06002525 RID: 9509
		[StaticAccessor("SortingGroup", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UpdateAllSortingGroups();

		// Token: 0x06002526 RID: 9510
		[StaticAccessor("SortingGroup", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern SortingGroup GetSortingGroupByIndex(int index);

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06002527 RID: 9511
		// (set) Token: 0x06002528 RID: 9512
		public extern string sortingLayerName { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002529 RID: 9513
		// (set) Token: 0x0600252A RID: 9514
		public extern int sortingLayerID { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x0600252B RID: 9515
		// (set) Token: 0x0600252C RID: 9516
		public extern int sortingOrder { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x0600252D RID: 9517
		internal extern int sortingGroupID { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x0600252E RID: 9518
		internal extern int sortingGroupOrder { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x0600252F RID: 9519
		internal extern int index { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06002530 RID: 9520 RVA: 0x000084C0 File Offset: 0x000066C0
		public SortingGroup()
		{
		}
	}
}
