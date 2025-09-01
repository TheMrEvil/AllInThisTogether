using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000361 RID: 865
	internal sealed class ICollectionDebugView<T>
	{
		// Token: 0x06001A68 RID: 6760 RVA: 0x00058FD0 File Offset: 0x000571D0
		public ICollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x00058FF0 File Offset: 0x000571F0
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

		// Token: 0x04000CA4 RID: 3236
		private readonly ICollection<T> _collection;
	}
}
