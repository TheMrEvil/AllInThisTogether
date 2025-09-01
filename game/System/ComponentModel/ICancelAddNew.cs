using System;

namespace System.ComponentModel
{
	/// <summary>Adds transactional capability when adding a new item to a collection.</summary>
	// Token: 0x020003B1 RID: 945
	public interface ICancelAddNew
	{
		/// <summary>Discards a pending new item from the collection.</summary>
		/// <param name="itemIndex">The index of the item that was previously added to the collection.</param>
		// Token: 0x06001EF6 RID: 7926
		void CancelNew(int itemIndex);

		/// <summary>Commits a pending new item to the collection.</summary>
		/// <param name="itemIndex">The index of the item that was previously added to the collection.</param>
		// Token: 0x06001EF7 RID: 7927
		void EndNew(int itemIndex);
	}
}
