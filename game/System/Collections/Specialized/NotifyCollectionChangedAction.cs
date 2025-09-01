using System;

namespace System.Collections.Specialized
{
	/// <summary>Describes the action that caused a <see cref="E:System.Collections.Specialized.INotifyCollectionChanged.CollectionChanged" /> event.</summary>
	// Token: 0x020004B4 RID: 1204
	public enum NotifyCollectionChangedAction
	{
		/// <summary>An item was added to the collection.</summary>
		// Token: 0x04001516 RID: 5398
		Add,
		/// <summary>An item was removed from the collection.</summary>
		// Token: 0x04001517 RID: 5399
		Remove,
		/// <summary>An item was replaced in the collection.</summary>
		// Token: 0x04001518 RID: 5400
		Replace,
		/// <summary>An item was moved within the collection.</summary>
		// Token: 0x04001519 RID: 5401
		Move,
		/// <summary>The content of the collection was cleared.</summary>
		// Token: 0x0400151A RID: 5402
		Reset
	}
}
