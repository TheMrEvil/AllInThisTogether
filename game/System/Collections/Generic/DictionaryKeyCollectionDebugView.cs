using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004C8 RID: 1224
	internal sealed class DictionaryKeyCollectionDebugView<TKey, TValue>
	{
		// Token: 0x0600279B RID: 10139 RVA: 0x00089240 File Offset: 0x00087440
		public DictionaryKeyCollectionDebugView(ICollection<TKey> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x0600279C RID: 10140 RVA: 0x00089260 File Offset: 0x00087460
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

		// Token: 0x0400155A RID: 5466
		private readonly ICollection<TKey> _collection;
	}
}
