using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000A97 RID: 2711
	internal sealed class DictionaryKeyCollectionDebugView<TKey, TValue>
	{
		// Token: 0x06006123 RID: 24867 RVA: 0x00145134 File Offset: 0x00143334
		public DictionaryKeyCollectionDebugView(ICollection<TKey> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x06006124 RID: 24868 RVA: 0x00145154 File Offset: 0x00143354
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TKey[] Items
		{
			get
			{
				TKey[] array = new TKey[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x040039E4 RID: 14820
		private readonly ICollection<TKey> _collection;
	}
}
