using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004C9 RID: 1225
	internal sealed class DictionaryValueCollectionDebugView<TKey, TValue>
	{
		// Token: 0x0600279D RID: 10141 RVA: 0x0008928C File Offset: 0x0008748C
		public DictionaryValueCollectionDebugView(ICollection<TValue> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x0600279E RID: 10142 RVA: 0x000892AC File Offset: 0x000874AC
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

		// Token: 0x0400155B RID: 5467
		private readonly ICollection<TValue> _collection;
	}
}
