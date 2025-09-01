using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000E8 RID: 232
	internal sealed class OrderedPartition<TElement> : IPartition<TElement>, IIListProvider<TElement>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06000829 RID: 2089 RVA: 0x0001C48B File Offset: 0x0001A68B
		public OrderedPartition(OrderedEnumerable<TElement> source, int minIdxInclusive, int maxIdxInclusive)
		{
			this._source = source;
			this._minIndexInclusive = minIdxInclusive;
			this._maxIndexInclusive = maxIdxInclusive;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001C4A8 File Offset: 0x0001A6A8
		public IEnumerator<TElement> GetEnumerator()
		{
			return this._source.GetEnumerator(this._minIndexInclusive, this._maxIndexInclusive);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001C4C1 File Offset: 0x0001A6C1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001C4CC File Offset: 0x0001A6CC
		public IPartition<TElement> Skip(int count)
		{
			int num = this._minIndexInclusive + count;
			if (num <= this._maxIndexInclusive)
			{
				return new OrderedPartition<TElement>(this._source, num, this._maxIndexInclusive);
			}
			return EmptyPartition<TElement>.Instance;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001C508 File Offset: 0x0001A708
		public IPartition<TElement> Take(int count)
		{
			int num = this._minIndexInclusive + count - 1;
			if (num >= this._maxIndexInclusive)
			{
				return this;
			}
			return new OrderedPartition<TElement>(this._source, this._minIndexInclusive, num);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001C540 File Offset: 0x0001A740
		public TElement TryGetElementAt(int index, out bool found)
		{
			if (index <= this._maxIndexInclusive - this._minIndexInclusive)
			{
				return this._source.TryGetElementAt(index + this._minIndexInclusive, out found);
			}
			found = false;
			return default(TElement);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001C57E File Offset: 0x0001A77E
		public TElement TryGetFirst(out bool found)
		{
			return this._source.TryGetElementAt(this._minIndexInclusive, out found);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001C592 File Offset: 0x0001A792
		public TElement TryGetLast(out bool found)
		{
			return this._source.TryGetLast(this._minIndexInclusive, this._maxIndexInclusive, out found);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001C5AC File Offset: 0x0001A7AC
		public TElement[] ToArray()
		{
			return this._source.ToArray(this._minIndexInclusive, this._maxIndexInclusive);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001C5C5 File Offset: 0x0001A7C5
		public List<TElement> ToList()
		{
			return this._source.ToList(this._minIndexInclusive, this._maxIndexInclusive);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001C5DE File Offset: 0x0001A7DE
		public int GetCount(bool onlyIfCheap)
		{
			return this._source.GetCount(this._minIndexInclusive, this._maxIndexInclusive, onlyIfCheap);
		}

		// Token: 0x040005B8 RID: 1464
		private readonly OrderedEnumerable<TElement> _source;

		// Token: 0x040005B9 RID: 1465
		private readonly int _minIndexInclusive;

		// Token: 0x040005BA RID: 1466
		private readonly int _maxIndexInclusive;
	}
}
