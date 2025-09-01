using System;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x0200049B RID: 1179
	internal sealed class BlockingCollectionDebugView<T>
	{
		// Token: 0x060025D7 RID: 9687 RVA: 0x0008488F File Offset: 0x00082A8F
		public BlockingCollectionDebugView(BlockingCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._blockingCollection = collection;
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060025D8 RID: 9688 RVA: 0x000848AC File Offset: 0x00082AAC
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._blockingCollection.ToArray();
			}
		}

		// Token: 0x040014C3 RID: 5315
		private readonly BlockingCollection<T> _blockingCollection;
	}
}
