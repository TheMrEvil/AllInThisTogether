using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x0200011E RID: 286
	internal class PartitionedStream<TElement, TKey>
	{
		// Token: 0x060008ED RID: 2285 RVA: 0x0001F52A File Offset: 0x0001D72A
		internal PartitionedStream(int partitionCount, IComparer<TKey> keyComparer, OrdinalIndexState indexState)
		{
			this._partitions = new QueryOperatorEnumerator<TElement, TKey>[partitionCount];
			this._keyComparer = keyComparer;
			this._indexState = indexState;
		}

		// Token: 0x170000FF RID: 255
		internal QueryOperatorEnumerator<TElement, TKey> this[int index]
		{
			get
			{
				return this._partitions[index];
			}
			set
			{
				this._partitions[index] = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0001F561 File Offset: 0x0001D761
		public int PartitionCount
		{
			get
			{
				return this._partitions.Length;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0001F56B File Offset: 0x0001D76B
		internal IComparer<TKey> KeyComparer
		{
			get
			{
				return this._keyComparer;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x0001F573 File Offset: 0x0001D773
		internal OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return this._indexState;
			}
		}

		// Token: 0x04000671 RID: 1649
		protected QueryOperatorEnumerator<TElement, TKey>[] _partitions;

		// Token: 0x04000672 RID: 1650
		private readonly IComparer<TKey> _keyComparer;

		// Token: 0x04000673 RID: 1651
		private readonly OrdinalIndexState _indexState;
	}
}
