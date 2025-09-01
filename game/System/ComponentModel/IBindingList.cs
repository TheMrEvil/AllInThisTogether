using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Provides the features required to support both complex and simple scenarios when binding to a data source.</summary>
	// Token: 0x020003AF RID: 943
	public interface IBindingList : IList, ICollection, IEnumerable
	{
		/// <summary>Gets whether you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
		/// <returns>
		///   <see langword="true" /> if you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001EDE RID: 7902
		bool AllowNew { get; }

		/// <summary>Adds a new item to the list.</summary>
		/// <returns>The item added to the list.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.AllowNew" /> is <see langword="false" />.</exception>
		// Token: 0x06001EDF RID: 7903
		object AddNew();

		/// <summary>Gets whether you can update items in the list.</summary>
		/// <returns>
		///   <see langword="true" /> if you can update the items in the list; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001EE0 RID: 7904
		bool AllowEdit { get; }

		/// <summary>Gets whether you can remove items from the list, using <see cref="M:System.Collections.IList.Remove(System.Object)" /> or <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
		/// <returns>
		///   <see langword="true" /> if you can remove items from the list; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001EE1 RID: 7905
		bool AllowRemove { get; }

		/// <summary>Gets whether a <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event is raised when the list changes or an item in the list changes.</summary>
		/// <returns>
		///   <see langword="true" /> if a <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event is raised when the list changes or when an item changes; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001EE2 RID: 7906
		bool SupportsChangeNotification { get; }

		/// <summary>Gets whether the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method.</summary>
		/// <returns>
		///   <see langword="true" /> if the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001EE3 RID: 7907
		bool SupportsSearching { get; }

		/// <summary>Gets whether the list supports sorting.</summary>
		/// <returns>
		///   <see langword="true" /> if the list supports sorting; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001EE4 RID: 7908
		bool SupportsSorting { get; }

		/// <summary>Gets whether the items in the list are sorted.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" /> has been called and <see cref="M:System.ComponentModel.IBindingList.RemoveSort" /> has not been called; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is <see langword="false" />.</exception>
		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001EE5 RID: 7909
		bool IsSorted { get; }

		/// <summary>Gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is <see langword="false" />.</exception>
		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001EE6 RID: 7910
		PropertyDescriptor SortProperty { get; }

		/// <summary>Gets the direction of the sort.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is <see langword="false" />.</exception>
		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001EE7 RID: 7911
		ListSortDirection SortDirection { get; }

		/// <summary>Occurs when the list changes or an item in the list changes.</summary>
		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06001EE8 RID: 7912
		// (remove) Token: 0x06001EE9 RID: 7913
		event ListChangedEventHandler ListChanged;

		/// <summary>Adds the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the indexes used for searching.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the indexes used for searching.</param>
		// Token: 0x06001EEA RID: 7914
		void AddIndex(PropertyDescriptor property);

		/// <summary>Sorts the list based on a <see cref="T:System.ComponentModel.PropertyDescriptor" /> and a <see cref="T:System.ComponentModel.ListSortDirection" />.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to sort by.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is <see langword="false" />.</exception>
		// Token: 0x06001EEB RID: 7915
		void ApplySort(PropertyDescriptor property, ListSortDirection direction);

		/// <summary>Returns the index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search on.</param>
		/// <param name="key">The value of the <paramref name="property" /> parameter to search for.</param>
		/// <returns>The index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" /> is <see langword="false" />.</exception>
		// Token: 0x06001EEC RID: 7916
		int Find(PropertyDescriptor property, object key);

		/// <summary>Removes the <see cref="T:System.ComponentModel.PropertyDescriptor" /> from the indexes used for searching.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the indexes used for searching.</param>
		// Token: 0x06001EED RID: 7917
		void RemoveIndex(PropertyDescriptor property);

		/// <summary>Removes any sort applied using <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />.</summary>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is <see langword="false" />.</exception>
		// Token: 0x06001EEE RID: 7918
		void RemoveSort();
	}
}
