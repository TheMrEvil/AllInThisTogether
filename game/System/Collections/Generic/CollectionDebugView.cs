using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004EF RID: 1263
	internal sealed class CollectionDebugView<T>
	{
		// Token: 0x06002957 RID: 10583 RVA: 0x0008E84C File Offset: 0x0008CA4C
		public CollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002958 RID: 10584 RVA: 0x0008E86C File Offset: 0x0008CA6C
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

		// Token: 0x040015E1 RID: 5601
		private readonly ICollection<T> _collection;
	}
}
