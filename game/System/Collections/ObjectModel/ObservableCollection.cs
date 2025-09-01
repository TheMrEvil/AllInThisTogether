using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Collections.ObjectModel
{
	/// <summary>Represents a dynamic data collection that provides notifications when items get added, removed, or when the whole list is refreshed.</summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	// Token: 0x02000495 RID: 1173
	[DebuggerTypeProxy(typeof(CollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class ObservableCollection<T> : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> class.</summary>
		// Token: 0x06002565 RID: 9573 RVA: 0x000833B2 File Offset: 0x000815B2
		public ObservableCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> class that contains elements copied from the specified collection.</summary>
		/// <param name="collection">The collection from which the elements are copied.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collection" /> parameter cannot be <see langword="null" />.</exception>
		// Token: 0x06002566 RID: 9574 RVA: 0x000833BA File Offset: 0x000815BA
		public ObservableCollection(IEnumerable<T> collection) : base(ObservableCollection<T>.CreateCopy(collection, "collection"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> class that contains elements copied from the specified list.</summary>
		/// <param name="list">The list from which the elements are copied.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="list" /> parameter cannot be <see langword="null" />.</exception>
		// Token: 0x06002567 RID: 9575 RVA: 0x000833CD File Offset: 0x000815CD
		public ObservableCollection(List<T> list) : base(ObservableCollection<T>.CreateCopy(list, "list"))
		{
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x000833E0 File Offset: 0x000815E0
		private static List<T> CreateCopy(IEnumerable<T> collection, string paramName)
		{
			if (collection == null)
			{
				throw new ArgumentNullException(paramName);
			}
			return new List<T>(collection);
		}

		/// <summary>Moves the item at the specified index to a new location in the collection.</summary>
		/// <param name="oldIndex">The zero-based index specifying the location of the item to be moved.</param>
		/// <param name="newIndex">The zero-based index specifying the new location of the item.</param>
		// Token: 0x06002569 RID: 9577 RVA: 0x000833F2 File Offset: 0x000815F2
		public void Move(int oldIndex, int newIndex)
		{
			this.MoveItem(oldIndex, newIndex);
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x1400004A RID: 74
		// (add) Token: 0x0600256A RID: 9578 RVA: 0x000833FC File Offset: 0x000815FC
		// (remove) Token: 0x0600256B RID: 9579 RVA: 0x00083405 File Offset: 0x00081605
		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add
			{
				this.PropertyChanged += value;
			}
			remove
			{
				this.PropertyChanged -= value;
			}
		}

		/// <summary>Occurs when an item is added, removed, changed, moved, or the entire list is refreshed.</summary>
		// Token: 0x1400004B RID: 75
		// (add) Token: 0x0600256C RID: 9580 RVA: 0x00083410 File Offset: 0x00081610
		// (remove) Token: 0x0600256D RID: 9581 RVA: 0x00083448 File Offset: 0x00081648
		public virtual event NotifyCollectionChangedEventHandler CollectionChanged
		{
			[CompilerGenerated]
			add
			{
				NotifyCollectionChangedEventHandler notifyCollectionChangedEventHandler = this.CollectionChanged;
				NotifyCollectionChangedEventHandler notifyCollectionChangedEventHandler2;
				do
				{
					notifyCollectionChangedEventHandler2 = notifyCollectionChangedEventHandler;
					NotifyCollectionChangedEventHandler value2 = (NotifyCollectionChangedEventHandler)Delegate.Combine(notifyCollectionChangedEventHandler2, value);
					notifyCollectionChangedEventHandler = Interlocked.CompareExchange<NotifyCollectionChangedEventHandler>(ref this.CollectionChanged, value2, notifyCollectionChangedEventHandler2);
				}
				while (notifyCollectionChangedEventHandler != notifyCollectionChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				NotifyCollectionChangedEventHandler notifyCollectionChangedEventHandler = this.CollectionChanged;
				NotifyCollectionChangedEventHandler notifyCollectionChangedEventHandler2;
				do
				{
					notifyCollectionChangedEventHandler2 = notifyCollectionChangedEventHandler;
					NotifyCollectionChangedEventHandler value2 = (NotifyCollectionChangedEventHandler)Delegate.Remove(notifyCollectionChangedEventHandler2, value);
					notifyCollectionChangedEventHandler = Interlocked.CompareExchange<NotifyCollectionChangedEventHandler>(ref this.CollectionChanged, value2, notifyCollectionChangedEventHandler2);
				}
				while (notifyCollectionChangedEventHandler != notifyCollectionChangedEventHandler2);
			}
		}

		/// <summary>Removes all items from the collection.</summary>
		// Token: 0x0600256E RID: 9582 RVA: 0x0008347D File Offset: 0x0008167D
		protected override void ClearItems()
		{
			this.CheckReentrancy();
			base.ClearItems();
			this.OnCountPropertyChanged();
			this.OnIndexerPropertyChanged();
			this.OnCollectionReset();
		}

		/// <summary>Removes the item at the specified index of the collection.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x0600256F RID: 9583 RVA: 0x000834A0 File Offset: 0x000816A0
		protected override void RemoveItem(int index)
		{
			this.CheckReentrancy();
			T t = base[index];
			base.RemoveItem(index);
			this.OnCountPropertyChanged();
			this.OnIndexerPropertyChanged();
			this.OnCollectionChanged(NotifyCollectionChangedAction.Remove, t, index);
		}

		/// <summary>Inserts an item into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert.</param>
		// Token: 0x06002570 RID: 9584 RVA: 0x000834DC File Offset: 0x000816DC
		protected override void InsertItem(int index, T item)
		{
			this.CheckReentrancy();
			base.InsertItem(index, item);
			this.OnCountPropertyChanged();
			this.OnIndexerPropertyChanged();
			this.OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
		}

		/// <summary>Replaces the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to replace.</param>
		/// <param name="item">The new value for the element at the specified index.</param>
		// Token: 0x06002571 RID: 9585 RVA: 0x00083508 File Offset: 0x00081708
		protected override void SetItem(int index, T item)
		{
			this.CheckReentrancy();
			T t = base[index];
			base.SetItem(index, item);
			this.OnIndexerPropertyChanged();
			this.OnCollectionChanged(NotifyCollectionChangedAction.Replace, t, item, index);
		}

		/// <summary>Moves the item at the specified index to a new location in the collection.</summary>
		/// <param name="oldIndex">The zero-based index specifying the location of the item to be moved.</param>
		/// <param name="newIndex">The zero-based index specifying the new location of the item.</param>
		// Token: 0x06002572 RID: 9586 RVA: 0x00083548 File Offset: 0x00081748
		protected virtual void MoveItem(int oldIndex, int newIndex)
		{
			this.CheckReentrancy();
			T t = base[oldIndex];
			base.RemoveItem(oldIndex);
			base.InsertItem(newIndex, t);
			this.OnIndexerPropertyChanged();
			this.OnCollectionChanged(NotifyCollectionChangedAction.Move, t, newIndex, oldIndex);
		}

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.PropertyChanged" /> event with the provided arguments.</summary>
		/// <param name="e">Arguments of the event being raised.</param>
		// Token: 0x06002573 RID: 9587 RVA: 0x00083587 File Offset: 0x00081787
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, e);
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06002574 RID: 9588 RVA: 0x0008359C File Offset: 0x0008179C
		// (remove) Token: 0x06002575 RID: 9589 RVA: 0x000835D4 File Offset: 0x000817D4
		protected virtual event PropertyChangedEventHandler PropertyChanged
		{
			[CompilerGenerated]
			add
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
		}

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged" /> event with the provided arguments.</summary>
		/// <param name="e">Arguments of the event being raised.</param>
		// Token: 0x06002576 RID: 9590 RVA: 0x0008360C File Offset: 0x0008180C
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
			if (collectionChanged != null)
			{
				this._blockReentrancyCount++;
				try
				{
					collectionChanged(this, e);
				}
				finally
				{
					this._blockReentrancyCount--;
				}
			}
		}

		/// <summary>Disallows reentrant attempts to change this collection.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> object that can be used to dispose of the object.</returns>
		// Token: 0x06002577 RID: 9591 RVA: 0x0008365C File Offset: 0x0008185C
		protected IDisposable BlockReentrancy()
		{
			this._blockReentrancyCount++;
			return this.EnsureMonitorInitialized();
		}

		/// <summary>Checks for reentrant attempts to change this collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">If there was a call to <see cref="M:System.Collections.ObjectModel.ObservableCollection`1.BlockReentrancy" /> of which the <see cref="T:System.IDisposable" /> return value has not yet been disposed of. Typically, this means when there are additional attempts to change this collection during a <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged" /> event. However, it depends on when derived classes choose to call <see cref="M:System.Collections.ObjectModel.ObservableCollection`1.BlockReentrancy" />.</exception>
		// Token: 0x06002578 RID: 9592 RVA: 0x00083672 File Offset: 0x00081872
		protected void CheckReentrancy()
		{
			if (this._blockReentrancyCount > 0)
			{
				NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
				if (collectionChanged != null && collectionChanged.GetInvocationList().Length > 1)
				{
					throw new InvalidOperationException("Cannot change ObservableCollection during a CollectionChanged event.");
				}
			}
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000836A1 File Offset: 0x000818A1
		private void OnCountPropertyChanged()
		{
			this.OnPropertyChanged(EventArgsCache.CountPropertyChanged);
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000836AE File Offset: 0x000818AE
		private void OnIndexerPropertyChanged()
		{
			this.OnPropertyChanged(EventArgsCache.IndexerPropertyChanged);
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000836BB File Offset: 0x000818BB
		private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000836CB File Offset: 0x000818CB
		private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index, int oldIndex)
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x000836DD File Offset: 0x000818DD
		private void OnCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem, int index)
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000836EF File Offset: 0x000818EF
		private void OnCollectionReset()
		{
			this.OnCollectionChanged(EventArgsCache.ResetCollectionChanged);
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000836FC File Offset: 0x000818FC
		private ObservableCollection<T>.SimpleMonitor EnsureMonitorInitialized()
		{
			ObservableCollection<T>.SimpleMonitor result;
			if ((result = this._monitor) == null)
			{
				result = (this._monitor = new ObservableCollection<T>.SimpleMonitor(this));
			}
			return result;
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x00083722 File Offset: 0x00081922
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			this.EnsureMonitorInitialized();
			this._monitor._busyCount = this._blockReentrancyCount;
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x0008373C File Offset: 0x0008193C
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (this._monitor != null)
			{
				this._blockReentrancyCount = this._monitor._busyCount;
				this._monitor._collection = this;
			}
		}

		// Token: 0x040014A7 RID: 5287
		private ObservableCollection<T>.SimpleMonitor _monitor;

		// Token: 0x040014A8 RID: 5288
		[NonSerialized]
		private int _blockReentrancyCount;

		// Token: 0x040014A9 RID: 5289
		[CompilerGenerated]
		[NonSerialized]
		private NotifyCollectionChangedEventHandler CollectionChanged;

		// Token: 0x040014AA RID: 5290
		[CompilerGenerated]
		[NonSerialized]
		private PropertyChangedEventHandler PropertyChanged;

		// Token: 0x02000496 RID: 1174
		[Serializable]
		private sealed class SimpleMonitor : IDisposable
		{
			// Token: 0x06002582 RID: 9602 RVA: 0x00083763 File Offset: 0x00081963
			public SimpleMonitor(ObservableCollection<T> collection)
			{
				this._collection = collection;
			}

			// Token: 0x06002583 RID: 9603 RVA: 0x00083772 File Offset: 0x00081972
			public void Dispose()
			{
				this._collection._blockReentrancyCount--;
			}

			// Token: 0x040014AB RID: 5291
			internal int _busyCount;

			// Token: 0x040014AC RID: 5292
			[NonSerialized]
			internal ObservableCollection<T> _collection;
		}
	}
}
