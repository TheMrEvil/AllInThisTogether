using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Collections.ObjectModel
{
	// Token: 0x02000497 RID: 1175
	internal static class EventArgsCache
	{
		// Token: 0x06002584 RID: 9604 RVA: 0x00083787 File Offset: 0x00081987
		// Note: this type is marked as 'beforefieldinit'.
		static EventArgsCache()
		{
		}

		// Token: 0x040014AD RID: 5293
		internal static readonly PropertyChangedEventArgs CountPropertyChanged = new PropertyChangedEventArgs("Count");

		// Token: 0x040014AE RID: 5294
		internal static readonly PropertyChangedEventArgs IndexerPropertyChanged = new PropertyChangedEventArgs("Item[]");

		// Token: 0x040014AF RID: 5295
		internal static readonly NotifyCollectionChangedEventArgs ResetCollectionChanged = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
	}
}
