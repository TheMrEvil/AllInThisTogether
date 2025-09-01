using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004C6 RID: 1222
	internal sealed class ICollectionDebugView<T>
	{
		// Token: 0x06002797 RID: 10135 RVA: 0x000891AA File Offset: 0x000873AA
		public ICollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002798 RID: 10136 RVA: 0x000891C8 File Offset: 0x000873C8
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				T[] array = new T[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04001558 RID: 5464
		private readonly ICollection<T> _collection;
	}
}
