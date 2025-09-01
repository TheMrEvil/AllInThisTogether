using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Provides a generic collection that supports data binding.</summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	// Token: 0x02000385 RID: 901
	[Serializable]
	public class BindingList<T> : Collection<T>, IBindingList, IList, ICollection, IEnumerable, ICancelAddNew, IRaiseItemChangedEvents
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindingList`1" /> class using default values.</summary>
		// Token: 0x06001D8B RID: 7563 RVA: 0x00069210 File Offset: 0x00067410
		public BindingList()
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindingList`1" /> class with the specified list.</summary>
		/// <param name="list">An <see cref="T:System.Collections.Generic.IList`1" /> of items to be contained in the <see cref="T:System.ComponentModel.BindingList`1" />.</param>
		// Token: 0x06001D8C RID: 7564 RVA: 0x00069248 File Offset: 0x00067448
		public BindingList(IList<T> list) : base(list)
		{
			this.Initialize();
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x00069284 File Offset: 0x00067484
		private void Initialize()
		{
			this.allowNew = this.ItemTypeHasDefaultConstructor;
			if (typeof(INotifyPropertyChanged).IsAssignableFrom(typeof(T)))
			{
				this.raiseItemChangedEvents = true;
				foreach (T item in base.Items)
				{
					this.HookPropertyChanged(item);
				}
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x00069300 File Offset: 0x00067500
		private bool ItemTypeHasDefaultConstructor
		{
			get
			{
				Type typeFromHandle = typeof(T);
				return typeFromHandle.IsPrimitive || typeFromHandle.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, Array.Empty<Type>(), null) != null;
			}
		}

		/// <summary>Occurs before an item is added to the list.</summary>
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06001D8F RID: 7567 RVA: 0x0006933A File Offset: 0x0006753A
		// (remove) Token: 0x06001D90 RID: 7568 RVA: 0x00069369 File Offset: 0x00067569
		public event AddingNewEventHandler AddingNew
		{
			add
			{
				bool flag = this.AllowNew;
				this._onAddingNew = (AddingNewEventHandler)Delegate.Combine(this._onAddingNew, value);
				if (flag != this.AllowNew)
				{
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
			remove
			{
				bool flag = this.AllowNew;
				this._onAddingNew = (AddingNewEventHandler)Delegate.Remove(this._onAddingNew, value);
				if (flag != this.AllowNew)
				{
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BindingList`1.AddingNew" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AddingNewEventArgs" /> that contains the event data.</param>
		// Token: 0x06001D91 RID: 7569 RVA: 0x00069398 File Offset: 0x00067598
		protected virtual void OnAddingNew(AddingNewEventArgs e)
		{
			AddingNewEventHandler onAddingNew = this._onAddingNew;
			if (onAddingNew == null)
			{
				return;
			}
			onAddingNew(this, e);
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x000693AC File Offset: 0x000675AC
		private object FireAddingNew()
		{
			AddingNewEventArgs addingNewEventArgs = new AddingNewEventArgs(null);
			this.OnAddingNew(addingNewEventArgs);
			return addingNewEventArgs.NewObject;
		}

		/// <summary>Occurs when the list or an item in the list changes.</summary>
		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06001D93 RID: 7571 RVA: 0x000693CD File Offset: 0x000675CD
		// (remove) Token: 0x06001D94 RID: 7572 RVA: 0x000693E6 File Offset: 0x000675E6
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				this._onListChanged = (ListChangedEventHandler)Delegate.Combine(this._onListChanged, value);
			}
			remove
			{
				this._onListChanged = (ListChangedEventHandler)Delegate.Remove(this._onListChanged, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x06001D95 RID: 7573 RVA: 0x000693FF File Offset: 0x000675FF
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			ListChangedEventHandler onListChanged = this._onListChanged;
			if (onListChanged == null)
			{
				return;
			}
			onListChanged(this, e);
		}

		/// <summary>Gets or sets a value indicating whether adding or removing items within the list raises <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events.</summary>
		/// <returns>
		///   <see langword="true" /> if adding or removing items raises <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x00069413 File Offset: 0x00067613
		// (set) Token: 0x06001D97 RID: 7575 RVA: 0x0006941B File Offset: 0x0006761B
		public bool RaiseListChangedEvents
		{
			get
			{
				return this.raiseListChangedEvents;
			}
			set
			{
				this.raiseListChangedEvents = value;
			}
		}

		/// <summary>Raises a <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> event of type <see cref="F:System.ComponentModel.ListChangedType.Reset" />.</summary>
		// Token: 0x06001D98 RID: 7576 RVA: 0x00069424 File Offset: 0x00067624
		public void ResetBindings()
		{
			this.FireListChanged(ListChangedType.Reset, -1);
		}

		/// <summary>Raises a <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> event of type <see cref="F:System.ComponentModel.ListChangedType.ItemChanged" /> for the item at the specified position.</summary>
		/// <param name="position">A zero-based index of the item to be reset.</param>
		// Token: 0x06001D99 RID: 7577 RVA: 0x0006942E File Offset: 0x0006762E
		public void ResetItem(int position)
		{
			this.FireListChanged(ListChangedType.ItemChanged, position);
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x00069438 File Offset: 0x00067638
		private void FireListChanged(ListChangedType type, int index)
		{
			if (this.raiseListChangedEvents)
			{
				this.OnListChanged(new ListChangedEventArgs(type, index));
			}
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x06001D9B RID: 7579 RVA: 0x00069450 File Offset: 0x00067650
		protected override void ClearItems()
		{
			this.EndNew(this.addNewPos);
			if (this.raiseItemChangedEvents)
			{
				foreach (T item in base.Items)
				{
					this.UnhookPropertyChanged(item);
				}
			}
			base.ClearItems();
			this.FireListChanged(ListChangedType.Reset, -1);
		}

		/// <summary>Inserts the specified item in the list at the specified index.</summary>
		/// <param name="index">The zero-based index where the item is to be inserted.</param>
		/// <param name="item">The item to insert in the list.</param>
		// Token: 0x06001D9C RID: 7580 RVA: 0x000694C0 File Offset: 0x000676C0
		protected override void InsertItem(int index, T item)
		{
			this.EndNew(this.addNewPos);
			base.InsertItem(index, item);
			if (this.raiseItemChangedEvents)
			{
				this.HookPropertyChanged(item);
			}
			this.FireListChanged(ListChangedType.ItemAdded, index);
		}

		/// <summary>Removes the item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">You are removing a newly added item and <see cref="P:System.ComponentModel.IBindingList.AllowRemove" /> is set to <see langword="false" />.</exception>
		// Token: 0x06001D9D RID: 7581 RVA: 0x000694F0 File Offset: 0x000676F0
		protected override void RemoveItem(int index)
		{
			if (!this.allowRemove && (this.addNewPos < 0 || this.addNewPos != index))
			{
				throw new NotSupportedException();
			}
			this.EndNew(this.addNewPos);
			if (this.raiseItemChangedEvents)
			{
				this.UnhookPropertyChanged(base[index]);
			}
			base.RemoveItem(index);
			this.FireListChanged(ListChangedType.ItemDeleted, index);
		}

		/// <summary>Replaces the item at the specified index with the specified item.</summary>
		/// <param name="index">The zero-based index of the item to replace.</param>
		/// <param name="item">The new value for the item at the specified index. The value can be <see langword="null" /> for reference types.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x06001D9E RID: 7582 RVA: 0x0006954D File Offset: 0x0006774D
		protected override void SetItem(int index, T item)
		{
			if (this.raiseItemChangedEvents)
			{
				this.UnhookPropertyChanged(base[index]);
			}
			base.SetItem(index, item);
			if (this.raiseItemChangedEvents)
			{
				this.HookPropertyChanged(item);
			}
			this.FireListChanged(ListChangedType.ItemChanged, index);
		}

		/// <summary>Discards a pending new item.</summary>
		/// <param name="itemIndex">The index of the of the new item to be added</param>
		// Token: 0x06001D9F RID: 7583 RVA: 0x00069583 File Offset: 0x00067783
		public virtual void CancelNew(int itemIndex)
		{
			if (this.addNewPos >= 0 && this.addNewPos == itemIndex)
			{
				this.RemoveItem(this.addNewPos);
				this.addNewPos = -1;
			}
		}

		/// <summary>Commits a pending new item to the collection.</summary>
		/// <param name="itemIndex">The index of the new item to be added.</param>
		// Token: 0x06001DA0 RID: 7584 RVA: 0x000695AA File Offset: 0x000677AA
		public virtual void EndNew(int itemIndex)
		{
			if (this.addNewPos >= 0 && this.addNewPos == itemIndex)
			{
				this.addNewPos = -1;
			}
		}

		/// <summary>Adds a new item to the collection.</summary>
		/// <returns>The item added to the list.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.BindingSource.AllowNew" /> property is set to <see langword="false" />.  
		///  -or-  
		///  A public default constructor could not be found for the current item type.</exception>
		// Token: 0x06001DA1 RID: 7585 RVA: 0x000695C5 File Offset: 0x000677C5
		public T AddNew()
		{
			return (T)((object)((IBindingList)this).AddNew());
		}

		/// <summary>Adds a new item to the list. For more information, see <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
		/// <returns>The item added to the list.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06001DA2 RID: 7586 RVA: 0x000695D4 File Offset: 0x000677D4
		object IBindingList.AddNew()
		{
			object obj = this.AddNewCore();
			this.addNewPos = ((obj != null) ? base.IndexOf((T)((object)obj)) : -1);
			return obj;
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x00069601 File Offset: 0x00067801
		private bool AddingNewHandled
		{
			get
			{
				return this._onAddingNew != null && this._onAddingNew.GetInvocationList().Length != 0;
			}
		}

		/// <summary>Adds a new item to the end of the collection.</summary>
		/// <returns>The item that was added to the collection.</returns>
		/// <exception cref="T:System.InvalidCastException">The new item is not the same type as the objects contained in the <see cref="T:System.ComponentModel.BindingList`1" />.</exception>
		// Token: 0x06001DA4 RID: 7588 RVA: 0x0006961C File Offset: 0x0006781C
		protected virtual object AddNewCore()
		{
			object obj = this.FireAddingNew();
			if (obj == null)
			{
				obj = SecurityUtils.SecureCreateInstance(typeof(T));
			}
			base.Add((T)((object)obj));
			return obj;
		}

		/// <summary>Gets or sets a value indicating whether you can add items to the list using the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method.</summary>
		/// <returns>
		///   <see langword="true" /> if you can add items to the list with the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method; otherwise, <see langword="false" />. The default depends on the underlying type contained in the list.</returns>
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x00069650 File Offset: 0x00067850
		// (set) Token: 0x06001DA6 RID: 7590 RVA: 0x0006966F File Offset: 0x0006786F
		public bool AllowNew
		{
			get
			{
				if (this.userSetAllowNew || this.allowNew)
				{
					return this.allowNew;
				}
				return this.AddingNewHandled;
			}
			set
			{
				bool flag = this.AllowNew;
				this.userSetAllowNew = true;
				this.allowNew = value;
				if (flag != value)
				{
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Gets a value indicating whether new items can be added to the list using the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method.</summary>
		/// <returns>
		///   <see langword="true" /> if you can add items to the list with the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method; otherwise, <see langword="false" />. The default depends on the underlying type contained in the list.</returns>
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x00069690 File Offset: 0x00067890
		bool IBindingList.AllowNew
		{
			get
			{
				return this.AllowNew;
			}
		}

		/// <summary>Gets or sets a value indicating whether items in the list can be edited.</summary>
		/// <returns>
		///   <see langword="true" /> if list items can be edited; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x00069698 File Offset: 0x00067898
		// (set) Token: 0x06001DA9 RID: 7593 RVA: 0x000696A0 File Offset: 0x000678A0
		public bool AllowEdit
		{
			get
			{
				return this.allowEdit;
			}
			set
			{
				if (this.allowEdit != value)
				{
					this.allowEdit = value;
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Gets a value indicating whether items in the list can be edited.</summary>
		/// <returns>
		///   <see langword="true" /> if list items can be edited; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x000696BA File Offset: 0x000678BA
		bool IBindingList.AllowEdit
		{
			get
			{
				return this.AllowEdit;
			}
		}

		/// <summary>Gets or sets a value indicating whether you can remove items from the collection.</summary>
		/// <returns>
		///   <see langword="true" /> if you can remove items from the list with the <see cref="M:System.ComponentModel.BindingList`1.RemoveItem(System.Int32)" /> method otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x000696C2 File Offset: 0x000678C2
		// (set) Token: 0x06001DAC RID: 7596 RVA: 0x000696CA File Offset: 0x000678CA
		public bool AllowRemove
		{
			get
			{
				return this.allowRemove;
			}
			set
			{
				if (this.allowRemove != value)
				{
					this.allowRemove = value;
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Gets a value indicating whether items can be removed from the list.</summary>
		/// <returns>
		///   <see langword="true" /> if you can remove items from the list with the <see cref="M:System.ComponentModel.BindingList`1.RemoveItem(System.Int32)" /> method; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x000696E4 File Offset: 0x000678E4
		bool IBindingList.AllowRemove
		{
			get
			{
				return this.AllowRemove;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</summary>
		/// <returns>
		///   <see langword="true" /> if a <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event is raised when the list changes or when an item changes; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x000696EC File Offset: 0x000678EC
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return this.SupportsChangeNotificationCore;
			}
		}

		/// <summary>Gets a value indicating whether <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events are enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events are supported; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x0000390E File Offset: 0x00001B0E
		protected virtual bool SupportsChangeNotificationCore
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x000696F4 File Offset: 0x000678F4
		bool IBindingList.SupportsSearching
		{
			get
			{
				return this.SupportsSearchingCore;
			}
		}

		/// <summary>Gets a value indicating whether the list supports searching.</summary>
		/// <returns>
		///   <see langword="true" /> if the list supports searching; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual bool SupportsSearchingCore
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the list supports sorting; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x000696FC File Offset: 0x000678FC
		bool IBindingList.SupportsSorting
		{
			get
			{
				return this.SupportsSortingCore;
			}
		}

		/// <summary>Gets a value indicating whether the list supports sorting.</summary>
		/// <returns>
		///   <see langword="true" /> if the list supports sorting; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual bool SupportsSortingCore
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.ComponentModel.IBindingListView.ApplySort(System.ComponentModel.ListSortDescriptionCollection)" /> has been called and <see cref="M:System.ComponentModel.IBindingList.RemoveSort" /> has not been called; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x00069704 File Offset: 0x00067904
		bool IBindingList.IsSorted
		{
			get
			{
				return this.IsSortedCore;
			}
		}

		/// <summary>Gets a value indicating whether the list is sorted.</summary>
		/// <returns>
		///   <see langword="true" /> if the list is sorted; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual bool IsSortedCore
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting.</returns>
		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x0006970C File Offset: 0x0006790C
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				return this.SortPropertyCore;
			}
		}

		/// <summary>Gets the property descriptor that is used for sorting the list if sorting is implemented in a derived class; otherwise, returns <see langword="null" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> used for sorting the list.</returns>
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x00002F6A File Offset: 0x0000116A
		protected virtual PropertyDescriptor SortPropertyCore
		{
			get
			{
				return null;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</returns>
		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x00069714 File Offset: 0x00067914
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				return this.SortDirectionCore;
			}
		}

		/// <summary>Gets the direction the list is sorted.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values. The default is <see cref="F:System.ComponentModel.ListSortDirection.Ascending" />.</returns>
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual ListSortDirection SortDirectionCore
		{
			get
			{
				return ListSortDirection.Ascending;
			}
		}

		/// <summary>Sorts the list based on a <see cref="T:System.ComponentModel.PropertyDescriptor" /> and a <see cref="T:System.ComponentModel.ListSortDirection" />. For a complete description of this member, see <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to sort by.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</param>
		// Token: 0x06001DBA RID: 7610 RVA: 0x0006971C File Offset: 0x0006791C
		void IBindingList.ApplySort(PropertyDescriptor prop, ListSortDirection direction)
		{
			this.ApplySortCore(prop, direction);
		}

		/// <summary>Sorts the items if overridden in a derived class; otherwise, throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="prop">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that specifies the property to sort on.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</param>
		/// <exception cref="T:System.NotSupportedException">Method is not overridden in a derived class.</exception>
		// Token: 0x06001DBB RID: 7611 RVA: 0x000044FA File Offset: 0x000026FA
		protected virtual void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveSort" /></summary>
		// Token: 0x06001DBC RID: 7612 RVA: 0x00069726 File Offset: 0x00067926
		void IBindingList.RemoveSort()
		{
			this.RemoveSortCore();
		}

		/// <summary>Removes any sort applied with <see cref="M:System.ComponentModel.BindingList`1.ApplySortCore(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" /> if sorting is implemented in a derived class; otherwise, raises <see cref="T:System.NotSupportedException" />.</summary>
		/// <exception cref="T:System.NotSupportedException">Method is not overridden in a derived class.</exception>
		// Token: 0x06001DBD RID: 7613 RVA: 0x000044FA File Offset: 0x000026FA
		protected virtual void RemoveSortCore()
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" />.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search on.</param>
		/// <param name="key">The value of the <paramref name="prop" /> parameter to search for.</param>
		/// <returns>The index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		// Token: 0x06001DBE RID: 7614 RVA: 0x0006972E File Offset: 0x0006792E
		int IBindingList.Find(PropertyDescriptor prop, object key)
		{
			return this.FindCore(prop, key);
		}

		/// <summary>Searches for the index of the item that has the specified property descriptor with the specified value, if searching is implemented in a derived class; otherwise, a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search for.</param>
		/// <param name="key">The value of <paramref name="prop" /> to match.</param>
		/// <returns>The zero-based index of the item that matches the property descriptor and contains the specified value.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="M:System.ComponentModel.BindingList`1.FindCore(System.ComponentModel.PropertyDescriptor,System.Object)" /> is not overridden in a derived class.</exception>
		// Token: 0x06001DBF RID: 7615 RVA: 0x000044FA File Offset: 0x000026FA
		protected virtual int FindCore(PropertyDescriptor prop, object key)
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add as a search criteria.</param>
		// Token: 0x06001DC0 RID: 7616 RVA: 0x00003917 File Offset: 0x00001B17
		void IBindingList.AddIndex(PropertyDescriptor prop)
		{
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		/// <param name="prop">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the indexes used for searching.</param>
		// Token: 0x06001DC1 RID: 7617 RVA: 0x00003917 File Offset: 0x00001B17
		void IBindingList.RemoveIndex(PropertyDescriptor prop)
		{
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x00069738 File Offset: 0x00067938
		private void HookPropertyChanged(T item)
		{
			INotifyPropertyChanged notifyPropertyChanged = item as INotifyPropertyChanged;
			if (notifyPropertyChanged != null)
			{
				if (this._propertyChangedEventHandler == null)
				{
					this._propertyChangedEventHandler = new PropertyChangedEventHandler(this.Child_PropertyChanged);
				}
				notifyPropertyChanged.PropertyChanged += this._propertyChangedEventHandler;
			}
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x0006977C File Offset: 0x0006797C
		private void UnhookPropertyChanged(T item)
		{
			INotifyPropertyChanged notifyPropertyChanged = item as INotifyPropertyChanged;
			if (notifyPropertyChanged != null && this._propertyChangedEventHandler != null)
			{
				notifyPropertyChanged.PropertyChanged -= this._propertyChangedEventHandler;
			}
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x000697AC File Offset: 0x000679AC
		private void Child_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (this.RaiseListChangedEvents)
			{
				if (sender == null || e == null || string.IsNullOrEmpty(e.PropertyName))
				{
					this.ResetBindings();
					return;
				}
				T t;
				try
				{
					t = (T)((object)sender);
				}
				catch (InvalidCastException)
				{
					this.ResetBindings();
					return;
				}
				int num = this._lastChangeIndex;
				if (num >= 0 && num < base.Count)
				{
					T t2 = base[num];
					if (t2.Equals(t))
					{
						goto IL_7B;
					}
				}
				num = base.IndexOf(t);
				this._lastChangeIndex = num;
				IL_7B:
				if (num == -1)
				{
					this.UnhookPropertyChanged(t);
					this.ResetBindings();
					return;
				}
				if (this._itemTypeProperties == null)
				{
					this._itemTypeProperties = TypeDescriptor.GetProperties(typeof(T));
				}
				PropertyDescriptor propDesc = this._itemTypeProperties.Find(e.PropertyName, true);
				ListChangedEventArgs e2 = new ListChangedEventArgs(ListChangedType.ItemChanged, num, propDesc);
				this.OnListChanged(e2);
			}
		}

		/// <summary>Gets a value indicating whether item property value changes raise <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events of type <see cref="F:System.ComponentModel.ListChangedType.ItemChanged" />. This member cannot be overridden in a derived class.</summary>
		/// <returns>
		///   <see langword="true" /> if the list type implements <see cref="T:System.ComponentModel.INotifyPropertyChanged" />, otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001DC5 RID: 7621 RVA: 0x00069898 File Offset: 0x00067A98
		bool IRaiseItemChangedEvents.RaisesItemChangedEvents
		{
			get
			{
				return this.raiseItemChangedEvents;
			}
		}

		// Token: 0x04000EE9 RID: 3817
		private int addNewPos = -1;

		// Token: 0x04000EEA RID: 3818
		private bool raiseListChangedEvents = true;

		// Token: 0x04000EEB RID: 3819
		private bool raiseItemChangedEvents;

		// Token: 0x04000EEC RID: 3820
		[NonSerialized]
		private PropertyDescriptorCollection _itemTypeProperties;

		// Token: 0x04000EED RID: 3821
		[NonSerialized]
		private PropertyChangedEventHandler _propertyChangedEventHandler;

		// Token: 0x04000EEE RID: 3822
		[NonSerialized]
		private AddingNewEventHandler _onAddingNew;

		// Token: 0x04000EEF RID: 3823
		[NonSerialized]
		private ListChangedEventHandler _onListChanged;

		// Token: 0x04000EF0 RID: 3824
		[NonSerialized]
		private int _lastChangeIndex = -1;

		// Token: 0x04000EF1 RID: 3825
		private bool allowNew = true;

		// Token: 0x04000EF2 RID: 3826
		private bool allowEdit = true;

		// Token: 0x04000EF3 RID: 3827
		private bool allowRemove = true;

		// Token: 0x04000EF4 RID: 3828
		private bool userSetAllowNew;
	}
}
