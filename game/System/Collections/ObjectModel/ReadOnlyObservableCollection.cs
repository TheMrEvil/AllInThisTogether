using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.ObjectModel
{
	/// <summary>Represents a read-only <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />.</summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	// Token: 0x02000498 RID: 1176
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(CollectionDebugView<>))]
	[Serializable]
	public class ReadOnlyObservableCollection<T> : ReadOnlyCollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ReadOnlyObservableCollection`1" /> class that serves as a wrapper around the specified <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />.</summary>
		/// <param name="list">The <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> with which to create this instance of the <see cref="T:System.Collections.ObjectModel.ReadOnlyObservableCollection`1" /> class.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is <see langword="null" />.</exception>
		// Token: 0x06002585 RID: 9605 RVA: 0x000837B4 File Offset: 0x000819B4
		public ReadOnlyObservableCollection(ObservableCollection<T> list) : base(list)
		{
			((INotifyCollectionChanged)base.Items).CollectionChanged += this.HandleCollectionChanged;
			((INotifyPropertyChanged)base.Items).PropertyChanged += this.HandlePropertyChanged;
		}

		/// <summary>Occurs when the collection changes.</summary>
		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06002586 RID: 9606 RVA: 0x00083800 File Offset: 0x00081A00
		// (remove) Token: 0x06002587 RID: 9607 RVA: 0x00083809 File Offset: 0x00081A09
		event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
		{
			add
			{
				this.CollectionChanged += value;
			}
			remove
			{
				this.CollectionChanged -= value;
			}
		}

		/// <summary>Occurs when an item is added or removed.</summary>
		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06002588 RID: 9608 RVA: 0x00083814 File Offset: 0x00081A14
		// (remove) Token: 0x06002589 RID: 9609 RVA: 0x0008384C File Offset: 0x00081A4C
		protected virtual event NotifyCollectionChangedEventHandler CollectionChanged
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

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ReadOnlyObservableCollection`1.CollectionChanged" /> event using the provided arguments.</summary>
		/// <param name="args">Arguments of the event being raised.</param>
		// Token: 0x0600258A RID: 9610 RVA: 0x00083881 File Offset: 0x00081A81
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, args);
			}
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x1400004F RID: 79
		// (add) Token: 0x0600258B RID: 9611 RVA: 0x00083898 File Offset: 0x00081A98
		// (remove) Token: 0x0600258C RID: 9612 RVA: 0x000838A1 File Offset: 0x00081AA1
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

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x14000050 RID: 80
		// (add) Token: 0x0600258D RID: 9613 RVA: 0x000838AC File Offset: 0x00081AAC
		// (remove) Token: 0x0600258E RID: 9614 RVA: 0x000838E4 File Offset: 0x00081AE4
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

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ReadOnlyObservableCollection`1.PropertyChanged" /> event using the provided arguments.</summary>
		/// <param name="args">Arguments of the event being raised.</param>
		// Token: 0x0600258F RID: 9615 RVA: 0x00083919 File Offset: 0x00081B19
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, args);
			}
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x00083930 File Offset: 0x00081B30
		private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			this.OnCollectionChanged(e);
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x00083939 File Offset: 0x00081B39
		private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			this.OnPropertyChanged(e);
		}

		// Token: 0x040014B0 RID: 5296
		[CompilerGenerated]
		[NonSerialized]
		private NotifyCollectionChangedEventHandler CollectionChanged;

		// Token: 0x040014B1 RID: 5297
		[CompilerGenerated]
		[NonSerialized]
		private PropertyChangedEventHandler PropertyChanged;
	}
}
