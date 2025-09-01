using System;

namespace System.Collections.Specialized
{
	/// <summary>Notifies listeners of dynamic changes, such as when an item is added and removed or the whole list is cleared.</summary>
	// Token: 0x020004B3 RID: 1203
	public interface INotifyCollectionChanged
	{
		/// <summary>Occurs when the collection changes.</summary>
		// Token: 0x14000051 RID: 81
		// (add) Token: 0x060026EA RID: 9962
		// (remove) Token: 0x060026EB RID: 9963
		event NotifyCollectionChangedEventHandler CollectionChanged;
	}
}
