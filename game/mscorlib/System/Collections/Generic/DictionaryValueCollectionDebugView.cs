using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000A98 RID: 2712
	internal sealed class DictionaryValueCollectionDebugView<TKey, TValue>
	{
		// Token: 0x06006125 RID: 24869 RVA: 0x00145180 File Offset: 0x00143380
		public DictionaryValueCollectionDebugView(ICollection<TValue> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x06006126 RID: 24870 RVA: 0x001451A0 File Offset: 0x001433A0
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TValue[] Items
		{
			get
			{
				TValue[] array = new TValue[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x040039E5 RID: 14821
		private readonly ICollection<TValue> _collection;
	}
}
