using System;

namespace System.ComponentModel
{
	/// <summary>Specifies how the collection is changed.</summary>
	// Token: 0x0200038A RID: 906
	public enum CollectionChangeAction
	{
		/// <summary>Specifies that an element was added to the collection.</summary>
		// Token: 0x04000EF7 RID: 3831
		Add = 1,
		/// <summary>Specifies that an element was removed from the collection.</summary>
		// Token: 0x04000EF8 RID: 3832
		Remove,
		/// <summary>Specifies that the entire collection has changed. This is caused by using methods that manipulate the entire collection, such as <see cref="M:System.Collections.CollectionBase.Clear" />.</summary>
		// Token: 0x04000EF9 RID: 3833
		Refresh
	}
}
