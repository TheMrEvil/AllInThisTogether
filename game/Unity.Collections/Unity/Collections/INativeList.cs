using System;

namespace Unity.Collections
{
	// Token: 0x020000B1 RID: 177
	public interface INativeList<T> : IIndexable<T> where T : struct
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060006C2 RID: 1730
		// (set) Token: 0x060006C3 RID: 1731
		int Capacity { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060006C4 RID: 1732
		bool IsEmpty { get; }

		// Token: 0x170000B6 RID: 182
		T this[int index]
		{
			get;
			set;
		}

		// Token: 0x060006C7 RID: 1735
		void Clear();
	}
}
