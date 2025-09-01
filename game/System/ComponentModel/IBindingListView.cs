using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Extends the <see cref="T:System.ComponentModel.IBindingList" /> interface by providing advanced sorting and filtering capabilities.</summary>
	// Token: 0x020003B0 RID: 944
	public interface IBindingListView : IBindingList, IList, ICollection, IEnumerable
	{
		/// <summary>Sorts the data source based on the given <see cref="T:System.ComponentModel.ListSortDescriptionCollection" />.</summary>
		/// <param name="sorts">The <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> containing the sorts to apply to the data source.</param>
		// Token: 0x06001EEF RID: 7919
		void ApplySort(ListSortDescriptionCollection sorts);

		/// <summary>Gets or sets the filter to be used to exclude items from the collection of items returned by the data source</summary>
		/// <returns>The string used to filter items out in the item collection returned by the data source.</returns>
		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001EF0 RID: 7920
		// (set) Token: 0x06001EF1 RID: 7921
		string Filter { get; set; }

		/// <summary>Gets the collection of sort descriptions currently applied to the data source.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> currently applied to the data source.</returns>
		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001EF2 RID: 7922
		ListSortDescriptionCollection SortDescriptions { get; }

		/// <summary>Removes the current filter applied to the data source.</summary>
		// Token: 0x06001EF3 RID: 7923
		void RemoveFilter();

		/// <summary>Gets a value indicating whether the data source supports advanced sorting.</summary>
		/// <returns>
		///   <see langword="true" /> if the data source supports advanced sorting; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001EF4 RID: 7924
		bool SupportsAdvancedSorting { get; }

		/// <summary>Gets a value indicating whether the data source supports filtering.</summary>
		/// <returns>
		///   <see langword="true" /> if the data source supports filtering; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001EF5 RID: 7925
		bool SupportsFiltering { get; }
	}
}
